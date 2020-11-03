
if Exists(Select 1 from sys.procedures where name='usp_AccountOpenReport')
Begin	
	Drop Procedure usp_AccountOpenReport
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_AccountOpenReport '101'
Create Procedure usp_AccountOpenReport
@ClientTypeCode BigInt =Null,
@ClientId BigInt = Null,
@DateFrom DateTime,
@DateTo DateTime,
@ReportType varchar(50)
As
Begin

	IF @ClientTypeCode=-1
	Begin
		Set @ClientTypeCode=null
	End

	Set @DateTo=DATEADD(s,-1,DATEADD(dd,1, @DateTo))

	Declare @SelfReferralCode varchar(100)
	Select @SelfReferralCode=SelfReferralCode From ClientMaster With(NoLock) Where ClientId=@ClientId
	
	If @ReportType='Detail'
	Begin	
		Select 
		a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
		a.MobileNo, a.SharingPercentage As [Sharing Percentage],a.ReferralAmount As [Referral Amount],
		a.ReferredReferralRevenue As [Referred Referral Revenue], 
		CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
		From ClientMaster a With(NoLock)
		Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
		Where 
		ReferredReferralCode=@SelfReferralCode 
		and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
		and a.CreatedDate Between @DateFrom And @DateTo
	End
	If @ReportType='Summary'
	Begin	
		Select 
		b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
		Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]
		From ClientMaster a With(NoLock)
		Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
		Where 
		ReferredReferralCode=@SelfReferralCode 
		and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
		and a.CreatedDate Between @DateFrom And @DateTo
		Group By b.ClientTypeName
	End

End