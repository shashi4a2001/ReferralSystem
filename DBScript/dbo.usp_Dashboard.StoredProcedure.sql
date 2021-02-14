
if Exists(Select 1 from sys.procedures where name='usp_Dashboard')
Begin	
	Drop Procedure usp_Dashboard
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_Dashboard '111'
Create Procedure usp_Dashboard
@ClientId BigInt = Null
As
Begin
	
	Select 
	a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
	a.MobileNo,a.LandlineNo,a.Address,a.LoginId,a.BankName,a.ClientNameAsPerBank,a.AccountNo,
	a.IFSCCode,a.SelfReferralCode,a.ReferredReferralCode,
	a.ReferralSharingPercentage As [ReferralSharingPercentage],a.ReferralAmount As [Referral Amount],
	a.ReferredReferralRevenue As [ReferredReferralRevenue],IsNull(a.RevenueSharingPercentage,0) As [RevenueSharingPercentage], 
	CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
	From ClientMaster a With(NoLock)
	Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
	Where a.ClientId = @ClientId
		 
	Declare @StartDateOfCurrentMonth DateTime
	Declare @CurrentDate DateTime

	Declare @StartDateOfPreviousMonth DateTime
	Declare @EndDateOfPreviousMonth DateTime
	
	Set @StartDateOfCurrentMonth = DATEADD(m, DATEDIFF(m, 0, dbo.fnGetDate()), 0)
	Set @CurrentDate=dbo.fnGetDate()
	Set @StartDateOfPreviousMonth = DATEADD(MONTH, DATEDIFF(MONTH, 0, dbo.fnGetDate()) - 1, 0)
	Set @EndDateOfPreviousMonth = DATEADD(ms,-2,DATEADD(month, DATEDIFF(month, 0, dbo.fnGetDate()), 0))


	Declare @SelfReferralCode varchar(100)
	Declare @ClientTypeCode varchar(10)
	Select @SelfReferralCode=SelfReferralCode,@ClientTypeCode=ClientTypeCode 
	From ClientMaster With(NoLock) Where ClientId=@ClientId
	
	Create table #Dashboard(ClientId BigInt,ClientTypeCode varchar(10),CreatedDate DateTime)
	
	If @ClientTypeCode='100'--Super Admin
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ClientTypeCode<>'100'-- ReferredReferralCode=@SelfReferralCode
	End
	Else
	Begin
		Insert Into #Dashboard
		Select a.ClientId,a.ClientTypeCode,b.CreatedDate
		From ClientHierarchy a With(NoLock) Inner Join ClientMaster b With(NoLock) on a.ClientId=b.ClientId
		Where a.ReferredClientId=@ClientId
	End
	/*
	Else If @ClientTypeCode='101'--National Head
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ClientTypeCode In ('102','103','104','105','106')
	End
	Else If @ClientTypeCode='102'--Regional Head
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ClientTypeCode In ('103','104','105','106')
	End
	Else If @ClientTypeCode='103'--State Franchisee
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ClientTypeCode In ('104','105','106')
	End
	Else If @ClientTypeCode='104'--District Franchisee
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ClientTypeCode In ('105','106')
	End
	Else If @ClientTypeCode='105'--Individual Agent
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ReferredReferralCode=@SelfReferralCode
	End
	Else If @ClientTypeCode='106'--Individual	
	Begin
		Insert Into #Dashboard
		Select ClientId,ClientTypeCode,CreatedDate 		
		From ClientMaster With(NoLock) Where ReferredReferralCode=@SelfReferralCode
	End
	*/
		
	Select 'Total Account Opened (Current Month)..' As [Summary],IsNull(Count(1),0) As [Count]
	From #Dashboard Where CreatedDate Between @StartDateOfCurrentMonth And @CurrentDate
		Union All
	Select 'Total Account Opened (Previous Month)..' As [Summary],IsNull(Count(1),0) As [Count]
	From #Dashboard Where CreatedDate Between @StartDateOfPreviousMonth And @EndDateOfPreviousMonth
		Union All
	Select 'Total Account Opened..' As [Summary],IsNull(Count(1),0) As [Count]
	From #Dashboard

		 

End