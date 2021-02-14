
if Exists(Select 1 from sys.procedures where name='usp_InsertClientMaster')
Begin	
	Drop Procedure usp_InsertClientMaster
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_InsertClientMaster
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
@LoginPassword varchar(100),
@BankName varchar(200),
@ClientNameAsPerBank varchar(300),
@AccountNo varchar(100),
@IFSCCode varchar(100),
@ReferralSharingPercentage Numeric(18,2),
@SelfReferralCode varchar(50),
@ReferredReferralCode varchar(50),
@RevenueSharingPercentage Numeric(18,2),
@UserId varchar(100)
As
Begin
	
	Declare @CreatorClientTypeCode varchar(10)
	set @CreatorClientTypeCode=''
	Select @CreatorClientTypeCode=ClientTypeCode From ClientMaster With(NoLock) Where ClientId = @UserId

	Create table #TypePermission
	(
		ClientTypeLevel Int,
		ClientTypeCode varchar(10),
		ClientTypeName varchar(50)
	)

	Insert Into #TypePermission(ClientTypeLevel,ClientTypeCode,ClientTypeName)
	Exec usp_SelectClientTypePermissionList @ClientTypeCode= @CreatorClientTypeCode

	If Not Exists (Select 1 From #TypePermission Where ClientTypeCode=@ClientTypeCode)
	Begin
		Select 'You dont have permission to create this type of client..' As [Result]
		return
	End


	If Exists (Select 1 From ClientMaster With(NoLock) Where MobileNo =@MobileNo)
	Begin
		Select 'Mobile No. already exists..' As [Result]
		return
	End
	If Exists (Select 1 From ClientMaster With(NoLock) Where LoginId =@LoginId)
	Begin
		Select 'Login Id already exists..' As [Result]
		return
	End
	If Exists (Select 1 From ClientMaster With(NoLock) Where ClientCode =@ClientCode)
	Begin
		Select 'Client Code already exists..' As [Result]
		return
	End
	If Exists (Select 1 From ClientMaster With(NoLock) Where EmailId =@EmailId)
	Begin
		Select 'Email Id already exists..' As [Result]
		return
	End
	Declare @ClientId BigInt

	Set @ReferredReferralCode=''
	Select @ReferredReferralCode=SelfReferralCode From ClientMaster With(NoLock) Where ClientId = @UserId

	Declare @ReferralAmount Numeric(18,2)
	Declare @ReferredReferralRevenue Numeric(18,2)
	Set @ReferralAmount=100
	Set @ReferredReferralRevenue=(@ReferralAmount*@ReferralSharingPercentage)/100

	Insert Into ClientMaster(
	MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,EmailId,
	LandlineNo,Address,LoginId,BankName,ClientNameAsPerBank,AccountNo,
	IFSCCode,ReferralSharingPercentage,SelfReferralCode,ReferredReferralCode,ReferralAmount,
	ReferredReferralRevenue,RevenueSharingPercentage,CreatedBy,CreatedDate)
	values(
	@MobileNo,@ClientRefId,@ClientCode,@ClientName,@ClientTypeCode,@ContactPerson,@EmailId,
	@LandlineNo,@Address,@LoginId,@BankName,@ClientNameAsPerBank,@AccountNo,
	@IFSCCode,@ReferralSharingPercentage,@SelfReferralCode,@ReferredReferralCode,@ReferralAmount,
	@ReferredReferralRevenue,@RevenueSharingPercentage,@UserId,dbo.fnGetDate())

	Set @ClientId=@@Identity

	If @ClientTypeCode<>'106'
	Begin
		Insert Into UserMaster(ClientId,UserName,UserId,LoginPwd,IsFirstTimeLogin,
								PwdExpireOn,LoginPwd1,CreatedBy,CreatedDate)
					values	  (@ClientId,@ClientName,@LoginId,@LoginPassword,1,
							   '01/01/1900',@LoginPassword,@UserId,dbo.fnGetDate())

		Exec usp_SetClientHieararchy @ClientId =@ClientId
	End

	Select 'Success' As [Result],SCOPE_IDENTITY() as ClientId

End