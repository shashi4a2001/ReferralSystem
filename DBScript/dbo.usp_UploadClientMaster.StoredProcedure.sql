
if Exists(Select 1 from sys.procedures where name='usp_UploadClientMaster')
Begin	
	Drop Procedure usp_UploadClientMaster
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_UploadClientMaster
@MobileNo varchar(50),
@ClientTypeCode varchar(10),
@ClientRefId BigInt =Null,
@ClientCode varchar(100),
@ClientName varchar(200),
@ContactPerson varchar(200),
@EmailId varchar(100),
@LandlineNo varchar(100),
@Address varchar(500),
@LoginId varchar(100),
@BankName varchar(200),
@ClientNameAsPerBank varchar(300),
@AccountNo varchar(100),
@IFSCCode varchar(100),
@ReferralSharingPercentage Numeric(18,2)=null,
@SelfReferralCode varchar(50),
@ReferredReferralCode varchar(50),
@RevenueSharingPercentage Numeric(18,2)=null,
@UserId varchar(100)
As
Begin
	
	If Exists (Select 1 From ClientMaster With(NoLock) Where MobileNo =@MobileNo)
	Begin
		Select 'Message : Mobile No. already exists..' As [Result]
		return
	End
	If Exists (Select 1 From ClientMaster With(NoLock) Where LoginId =@LoginId)
	Begin
		Select 'Message : Login Id already exists..' As [Result]
		return
	End
	If @ClientCode<>'NA' and @ClientCode<>''
	Begin
		If Exists (Select 1 From ClientMaster With(NoLock) Where ClientCode =@ClientCode)
		Begin
			Select 'Message : Client Code already exists..' As [Result]
			return
		End
	End
	If Exists (Select 1 From ClientMaster With(NoLock) Where EmailId =@EmailId)
	Begin
		Select 'Message : Email Id already exists..' As [Result]
		return
	End
	Declare @ClientId BigInt



	Insert Into ClientMaster(
	MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,EmailId,
	LandlineNo,Address,LoginId,BankName,ClientNameAsPerBank,AccountNo,
	IFSCCode,ReferralSharingPercentage,SelfReferralCode,ReferredReferralCode,
	RevenueSharingPercentage,CreatedBy,CreatedDate)
	values(
	@MobileNo,@ClientRefId,@ClientCode,@ClientName,@ClientTypeCode,@ContactPerson,@EmailId,
	@LandlineNo,@Address,@LoginId,@BankName,@ClientNameAsPerBank,@AccountNo,
	@IFSCCode,@ReferralSharingPercentage,@SelfReferralCode,@ReferredReferralCode,
	@RevenueSharingPercentage,@UserId,dbo.fnGetDate())

	Set @ClientId=@@Identity

	Insert Into UserMaster(ClientId,UserName,UserId,LoginPwd,IsFirstTimeLogin,
							PwdExpireOn,LoginPwd1,CreatedBy,CreatedDate)
				values	  (@ClientId,@ClientName,@LoginId,'xMycjpL7vseSZizZEO8rkw==',1,
						   '01/01/1900','xMycjpL7vseSZizZEO8rkw==',@UserId,dbo.fnGetDate())

	Select 'Success : Upload' As [Result],SCOPE_IDENTITY() as ClientId

End