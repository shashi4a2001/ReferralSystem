
if Exists(Select 1 from sys.procedures where name='usp_InsertClientMaster')
Begin	
	Drop Procedure usp_InsertClientMaster
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_InsertClientMaster
@MobileNo varchar(20),
@ClientTypeCode varchar(10),
@ClientRefId BigInt =Null,
@ClientCode varchar(100),
@ClientName varchar(200),
@ContactPerson varchar(200),
@EmailId varchar(100),
@LandlineNo varchar(100),
@Address varchar(500),
@LoginId varchar(100),
@LoginPassword varchar(100),
@BankName varchar(200),
@ClientNameAsPerBank varchar(300),
@AccountNo varchar(100),
@IFSCCode varchar(100),
@SharingPercentage Numeric(18,2),
@SelfReferralCode varchar(50),
@ReferredReferralCode varchar(50),
@UserId varchar(100)
As
Begin
	Declare @ReferralAmount Numeric(18,2)
	Declare @ReferredReferralRevenue Numeric(18,2)

	Insert Into ClientMaster(
	MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,EmailId,
	LandlineNo,Address,LoginId,LoginPassword,BankName,ClientNameAsPerBank,AccountNo,
	IFSCCode,SharingPercentage,SelfReferralCode,ReferredReferralCode,ReferralAmount,
	ReferredReferralRevenue,CreatedBy,CreatedDate)
	values(
	@MobileNo,@ClientRefId,@ClientCode,@ClientName,@ClientTypeCode,@ContactPerson,@EmailId,
	@LandlineNo,@Address,@LoginId,@LoginPassword,@BankName,@ClientNameAsPerBank,@AccountNo,
	@IFSCCode,@SharingPercentage,@SelfReferralCode,@ReferredReferralCode,@ReferralAmount,
	@ReferredReferralRevenue,@UserId,getdate())

	Select 'Success' As [Result]

End