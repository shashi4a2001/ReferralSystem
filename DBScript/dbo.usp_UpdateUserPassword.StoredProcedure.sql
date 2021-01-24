
if Exists(Select 1 from sys.procedures where name='usp_UpdateUserPassword')
Begin	
	Drop Procedure usp_UpdateUserPassword
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_UpdateUserPassword
@ClientId varchar(100),
@OldPassword varchar(500)=null,
@NewPassword varchar(500),
@Type varchar(5) -- R=Password Recovery  | C=Change Password
As
Begin
	If Not Exists (Select 1 From UserMaster With(NoLock) Where ClientId = @ClientId)
	Begin
		Select 'Invalid User...'
		Return
	End

	If @Type='C'
	Begin
		If IsNull(@OldPassword,'')=''
		Begin
			Select 'Invalid Old Password...'
			Return
		End

		If Not Exists (Select 1 From UserMaster With(NoLock) Where ClientId = @ClientId and LoginPwd =@OldPassword)
		Begin
			Select 'Invalid Old Password...'
			Return
		End
	End

	Declare @LoginPwd varchar(300)
	Declare @LoginPwd1 varchar(300)
	Declare @LoginPwd2 varchar(300)

	Select 
	@LoginPwd=LoginPwd,
	@LoginPwd1=LoginPwd1,
	@LoginPwd2=LoginPwd2 
	From UserMaster With(NoLock) 
	Where ClientId = @ClientId

	Update UserMaster Set 
	LoginPwd  = @NewPassword,
	LoginPwd1 = @LoginPwd,
	LoginPwd2 = @LoginPwd1,
	LoginPwd3 = @LoginPwd2,
	IsFirstTimeLogin = 0,
	PwdExpireOn=Cast(Convert(varchar,getdate(),106)As DateTime)+30,
	ModifiedDate=dbo.fnGetDate() 
	Where ClientId = @ClientId

	Select 'Success'

	 
End