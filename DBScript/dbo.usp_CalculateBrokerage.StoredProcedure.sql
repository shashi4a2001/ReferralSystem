
if Exists(Select 1 from sys.procedures where name='usp_CalculateBrokerage')
Begin	
	Drop Procedure usp_CalculateBrokerage
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_AccountOpenReport '101'
Create Procedure usp_CalculateBrokerage
@ClientId BigInt = Null,
@DateFrom DateTime = Null,
@DateTo DateTime = Null,
@BrokerageType varchar(50) = Null, --Referral(101)/Revenue(102)
@BrokerageAmount Numeric(18,2) = Null,
@UserId varchar(50) = Null
As
Begin
 /*
Exec usp_CalculateBrokerage @ClientId =10804, @DateFrom ='2021-01-29', @DateTo = '2021-01-29', @BrokerageType ='101',
@BrokerageAmount =200, @UserId ='shashi'
 */

 /*
 

Exec usp_CalculateBrokerage @ClientId =10804, @DateFrom ='2021-01-29', @DateTo = '2021-01-29', @BrokerageType ='101',
@BrokerageAmount =200, @UserId ='shashi'

 
 
select * from clienttypemaster
select createddate, * from ClientMaster where ClientTypeCode=106
Update ClientMaster Set CreatedDate='2021-01-29 11:33:12.023' where clientid=642

Update ClientMaster Set ReferralSharingPercentage=10 where clientid=10805
Update ClientMaster Set ReferralSharingPercentage=20 where clientid=10805
select * from ClientHierarchy 

 Select b.CreatedDate,*
		From ClientHierarchy a With(NoLock) 
		Inner Join ClientMaster b With(NoLock) on a.ClientId=b.ClientId  
		where a.ReferredClientId='10804' And b.ClientTypeCode='106' 
			And b.CreatedDate <'2021-01-30'




select * from ClientTypeMaster
select * from ClientMaster where ClientTypeCode='102'
select * from ClientMaster where ReferredReferralCode in (
select isnull(SelfReferralCode,'') from ClientMaster where ReferredReferralCode ='STF/RJ/01' And ClientTypeCode ='105')

select SelfReferralCode,ReferredReferralCode,* from ClientMaster where Clientid='10806'
select SelfReferralCode,ReferredReferralCode,* from ClientMaster where ReferredReferralCode='DF11'
 */
	If Not Exists (Select 1 From ClientMaster Where ClientTypeCode='101' And ClientId =@ClientId)
	Begin
		Select 'Invalid National Head..'
		Return
	End

	 If Exists (
				Select 1 From BrokerageSummary 
				Where 
					BrokerageType=@BrokerageType 
					and AmountToClient=@ClientId
					And ( (DateFrom Between @DateFrom And @DateTo) Or (DateTo Between @DateFrom And @DateTo) )
				)
	 Begin
		Select 'Brokerage already calculated for this period'
		Return
	 End
	 


	 Declare @Id BigInt
	 Insert Into BrokerageSummary(AmountToClient,DateFrom,DateTo,BrokerageType,BrokerageAmount,CreatedBy,CreatedDate)
						   values(@ClientId,@DateFrom,@DateTo,@BrokerageType,@BrokerageAmount,@UserId,getDate())

	Set @Id=@@IDENTITY
	
	Set @DateTo = DateAdd (Second,-1, DATEADD(day,1,Cast(Convert(varchar,@DateTo,101)As DateTime)))
	
	Declare @ReferralClientCount Int
	
	Declare @BalBrkAmt Numeric(18,2)
	Declare @SharingPercentage Numeric(18,2)
	Declare @AmountEarnedPerClient Numeric(18,2)
	Declare @AmountEarnedTotal Numeric(18,2)
	Declare @SelfReferralCode varchar(50)
	 
	
	-------Start -  Calculation From National Head(101) -----------------------------------
	If @BrokerageType='101'
	Begin
		Select @ReferralClientCount=Count(1) 
		From ClientHierarchy a With(NoLock) 
		Inner Join ClientMaster b With(NoLock) on a.ClientId=b.ClientId  
		where a.ReferredClientId=@ClientId And b.ClientTypeCode='106'
			And b.CreatedDate Between @DateFrom And @DateTo 
	End
	Else If @BrokerageType='102'
	Begin
		Set @ReferralClientCount=1
	End

	Set @ReferralClientCount=IsNull(@ReferralClientCount,0)

	Select 	@SharingPercentage = ReferralSharingPercentage,@SelfReferralCode=IsNull(SelfReferralCode,'')	
	From ClientMaster With(NoLock) Where ClientId=@ClientId

	Set @SharingPercentage=IsNull(@SharingPercentage,0)

	Set @BalBrkAmt=@BrokerageAmount
	Set @AmountEarnedPerClient = ((@BalBrkAmt*@SharingPercentage)/100)
	Set @AmountEarnedTotal = (@AmountEarnedPerClient*@ReferralClientCount)

	Select BrokerageType,ClientId,SharingPercentage,AmountEarnedPerClient,ReferralAmountBalance,ReferralClientCount,
	AmountEarnedTotal,Cast('' As varchar(50)) As [SelfReferralCode] 
	Into #BrokerageDetail 
	From BrokerageDetail Where 1=2

	Insert Into #BrokerageDetail
	(
		BrokerageType,ClientId,SharingPercentage,AmountEarnedPerClient,
		ReferralAmountBalance,ReferralClientCount,AmountEarnedTotal,SelfReferralCode
	)
	Values 
	(
		@BrokerageType,@ClientId,@SharingPercentage,@AmountEarnedPerClient,
		@BalBrkAmt,@ReferralClientCount,@AmountEarnedTotal,@SelfReferralCode
	)

	-------End -  Calculation From National Head(101) -----------------------------------

	Create Table #Clients(Id Int Identity(1,1),ClientId BigInt,ReferralSharingPercentage Numeric(18,2),
	RevenueSharingPercentage Numeric(18,2),SelfReferralCode varchar(50),ReferredReferralCode varchar(50))

	Insert Into #Clients(ClientId,ReferralSharingPercentage,RevenueSharingPercentage,SelfReferralCode,ReferredReferralCode)
	Select ClientId,IsNull(ReferralSharingPercentage,0),IsNull(RevenueSharingPercentage,0),IsNull(SelfReferralCode,''),IsNull(ReferredReferralCode,'')
	From ClientMaster With(NoLock) Where ClientTypeCode In ('102','103','104','105') Order By ClientTypeCode


	Declare @ClientId_1 BigInt
	Declare @ReferralSharingPercentage_1 Numeric(18,2)
	Declare @RevenueSharingPercentage_1  Numeric(18,2)
	Declare @SelfReferralCode_1 varchar(50)
	Declare @ReferredReferralCode_1 varchar(50)

	Declare @icnt Int
	Declare @cCount Int
	Set @icnt=1
	Select @cCount=Count(1) From #Clients

	While (@cCount>=@icnt)
	Begin

		Set @ClientId_1 =0
		Set @ReferralSharingPercentage_1 =0
		Set @RevenueSharingPercentage_1  =0
		Set @ReferredReferralCode_1 =''

		Select	
		@ClientId_1=ClientId,@ReferralSharingPercentage_1=ReferralSharingPercentage,@RevenueSharingPercentage_1=RevenueSharingPercentage,
		@SelfReferralCode_1=SelfReferralCode,@ReferredReferralCode_1=ReferredReferralCode 
		From #Clients Where Id=@icnt
		

		Set @ReferralClientCount=0
		Set @BalBrkAmt=0
		Set @SharingPercentage=0
		Set @SelfReferralCode=''
		Set @AmountEarnedPerClient=0
		Set @AmountEarnedTotal=0

		Set @SharingPercentage = @ReferralSharingPercentage_1
		Set @SelfReferralCode=@SelfReferralCode_1
		Set @ClientId=@ClientId_1

		If @BrokerageType='101'
		Begin
			Select @ReferralClientCount=Count(1) 
			From ClientHierarchy a With(NoLock) 
			Inner Join ClientMaster b With(NoLock) on a.ClientId=b.ClientId  
			where a.ReferredClientId=@ClientId And b.ClientTypeCode='106'
				And b.CreatedDate Between @DateFrom And @DateTo 
		End
		Else If @BrokerageType='102'
		Begin
			Set @ReferralClientCount=1
		End

		Set @ReferralClientCount=IsNull(@ReferralClientCount,0)

		Set @SharingPercentage=IsNull(@SharingPercentage,0)

		 
		Select @BalBrkAmt=(ReferralAmountBalance-AmountEarnedPerClient) From #BrokerageDetail Where SelfReferralCode=@ReferredReferralCode_1 And @ReferredReferralCode_1<>''
		Set @AmountEarnedPerClient = ((@BalBrkAmt*@SharingPercentage)/100)
		Set @AmountEarnedTotal = (@AmountEarnedPerClient*@ReferralClientCount)


		Insert Into #BrokerageDetail
		(
			BrokerageType,ClientId,SharingPercentage,AmountEarnedPerClient,
			ReferralAmountBalance,ReferralClientCount,AmountEarnedTotal,SelfReferralCode
		)
		Values 
		(
			@BrokerageType,@ClientId,@SharingPercentage,@AmountEarnedPerClient,
			@BalBrkAmt,@ReferralClientCount,@AmountEarnedTotal,@SelfReferralCode
		)



		Set @icnt=@icnt+1
	End
	
	--Finally Insert Calculated Brokerage in Table
	Insert Into BrokerageDetail
	(FKId,BrokerageType,ClientId,SharingPercentage,AmountEarnedPerClient,ReferralAmountBalance,
	ReferralClientCount,AmountEarnedTotal,CreatedBy,CreatedDate)
	Select @Id,BrokerageType,ClientId,SharingPercentage,AmountEarnedPerClient,ReferralAmountBalance,
	ReferralClientCount,AmountEarnedTotal,@UserId,GETDATE()
	From #BrokerageDetail



	--Select * from BrokerageSummary
select b.ClientTypeCode,c.ClientTypeName, a.* from brokeragedetail a 
Inner Join ClientMaster b on a.ClientId=b.ClientId 
Inner Join ClientTypeMaster c On b.ClientTypeCode=c.ClientTypeCode
order by b.ClientTypeCode

	 Truncate table BrokerageSummary
Truncate table brokeragedetail

End
