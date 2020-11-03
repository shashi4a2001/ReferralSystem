
if Exists(Select 1 from sys.procedures where name='usp_Dashboard')
Begin	
	Drop Procedure usp_Dashboard
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_Dashboard '101'
Create Procedure usp_Dashboard
@ClientId BigInt = Null
As
Begin
	
	Select 
	a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
	a.MobileNo,a.LandlineNo,a.Address,a.LoginId,a.BankName,a.ClientNameAsPerBank,a.AccountNo,
	a.IFSCCode,a.SelfReferralCode,a.ReferredReferralCode,
	a.SharingPercentage As [SharingPercentage],a.ReferralAmount As [Referral Amount],
	a.ReferredReferralRevenue As [ReferredReferralRevenue], 
	CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
	From ClientMaster a With(NoLock)
	Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
	Where a.ClientId = @ClientId
		 
	Declare @StartDateOfCurrentMonth DateTime
	Declare @CurrentDate DateTime

	Declare @StartDateOfPreviousMonth DateTime
	Declare @EndDateOfPreviousMonth DateTime
	
	Set @StartDateOfCurrentMonth = DATEADD(m, DATEDIFF(m, 0, GETDATE()), 0)
	Set @CurrentDate=getdate()
	Set @StartDateOfPreviousMonth = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
	Set @EndDateOfPreviousMonth = DATEADD(ms,-2,DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0))


	Declare @SelfReferralCode varchar(100)
	Select @SelfReferralCode=SelfReferralCode From ClientMaster With(NoLock) Where ClientId=@ClientId
		
	Select ClientId,ClientTypeCode,CreatedDate 
	Into #Dashboard 
	From ClientMaster With(NoLock) Where ReferredReferralCode=@SelfReferralCode

		
	Select 'Total Account Opened (Current Month)..' As [Summary],IsNull(Count(1),0) As [Count]
	From #Dashboard Where CreatedDate Between @StartDateOfCurrentMonth And @CurrentDate
		Union All
	Select 'Total Account Opened (Previous Month)..' As [Summary],IsNull(Count(1),0) As [Count]
	From #Dashboard Where CreatedDate Between @StartDateOfPreviousMonth And @EndDateOfPreviousMonth
		Union All
	Select 'Total Account Opened..' As [Summary],IsNull(Count(1),0) As [Count]
	From #Dashboard

		 

End