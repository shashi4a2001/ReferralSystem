
if Exists(Select 1 from sys.procedures where name='usp_UpdateUserPassword')
Begin	
	Drop Procedure usp_UpdateUserPassword
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_UpdateUserPassword
@UserId varchar(100),
@OldPassword varchar(500),
@NewPassword varchar(500)
As
Begin
	If Not Exists (Select 1 From UserMaster With(NoLock) Where UserId = @UserId)
	Begin
		Select 'Invalid User...'
		Return
	End
	If Not Exists (Select 1 From UserMaster With(NoLock) Where UserId = @UserId and LoginPwd =@OldPassword)
	Begin
		Select 'Invalid Old Password...'
		Return
	End

	Declare @LoginPwd varchar(300)
	Declare @LoginPwd1 varchar(300)
	Declare @LoginPwd2 varchar(300)

	Select 
	@LoginPwd=LoginPwd,
	@LoginPwd1=LoginPwd1,
	@LoginPwd2=LoginPwd2 
	From UserMaster With(NoLock) 
	Where UserId = @UserId

	Update UserMaster Set 
	LoginPwd  = @NewPassword,
	LoginPwd1 = @LoginPwd,
	LoginPwd2 = @LoginPwd1,
	LoginPwd3 = @LoginPwd2,
	IsFirstTimeLogin = 0,
	PwdExpireOn=Cast(Convert(varchar,getdate(),106)As DateTime)+30,
	ModifiedDate=dbo.fnGetDate() 
	Where UserId = @UserId

	Select 'Success'

	 
End