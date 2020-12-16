
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
	Declare @ClientType_Code varchar(10)
	Select @SelfReferralCode=SelfReferralCode,@ClientType_Code=ClientTypeCode  
	From ClientMaster With(NoLock) Where ClientId=@ClientId
	
	If @ReportType='Detail'
	Begin	

		--If @ClientTypeCode='100'--Super Admin
		--Begin
		--End
		--Else If @ClientTypeCode='101'--National Head
		--Begin
		--End
		--Else If @ClientTypeCode='102'--Regional Head
		--Begin
		--End
		--Else If @ClientTypeCode='103'--State Franchisee
		--Begin
		--End
		--Else If @ClientTypeCode='104'--District Franchisee
		--Begin
		--End
		--Else If @ClientTypeCode='105'--Individual Agent
		--Begin
		--End
		--Else If @ClientTypeCode='106'--Individual	
		--Begin
		--End

		If @ClientType_Code='100'--Super Admin
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode <>'100'
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
		End
		Else If @ClientType_Code='101'--National Head
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('102','103','104','105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
		End
		Else If @ClientType_Code='102'--Regional Head
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('103','104','105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
		End
		Else If @ClientType_Code='103'--State Franchisee
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('104','105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
		End
		Else If @ClientType_Code='104'--District Franchisee
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo

		End
		Else If @ClientType_Code='105'--Individual Agent
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			ReferredReferralCode=@SelfReferralCode 
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
		End
		Else If @ClientType_Code='106'--Individual	
		Begin
			Select 
			a.ClientId,a.ClientCode,a.ClientName,b.ClientTypeName As [ClientType],a.ContactPerson,a.EmailId,
			a.MobileNo, a.ReferralSharingPercentage As [Referral Sharing Percentage],a.ReferralAmount As [Referral Amount],
			a.ReferredReferralRevenue As [Referred Referral Revenue], 
			CONVERT(varchar,a.CreatedDate,9) As [Account Opening Date]  
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			ReferredReferralCode=@SelfReferralCode 
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
		End

		
	End
	If @ReportType='Summary'
	Begin	
		If @ClientType_Code='100'--Super Admin
		Begin
			Select 
			b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
			Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode <>'100'
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
			Group By b.ClientTypeName
		End
		Else If @ClientType_Code='101'--National Head
		Begin
			Select 
			b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
			Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('102','103','104','105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
			Group By b.ClientTypeName
		End
		Else If @ClientType_Code='102'--Regional Head
		Begin
			Select 
			b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
			Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('103','104','105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
			Group By b.ClientTypeName
		End
		Else If @ClientType_Code='103'--State Franchisee
		Begin
			Select 
			b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
			Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('104','105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
			Group By b.ClientTypeName
		End
		Else If @ClientType_Code='104'--District Franchisee
		Begin
			Select 
			b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
			Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]
			From ClientMaster a With(NoLock)
			Inner Join ClientTypeMaster b With(NoLock) ON a.ClientTypeCode = b.ClientTypeCode  
			Where 
			a.ClientTypeCode In  ('105','106')
			and a.ClientTypeCode =IsNull(@ClientTypeCode,a.ClientTypeCode)
			and a.CreatedDate Between @DateFrom And @DateTo
			Group By b.ClientTypeName
		End
		Else If @ClientType_Code='105'--Individual Agent
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
		Else If @ClientType_Code='106'--Individual	
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

End