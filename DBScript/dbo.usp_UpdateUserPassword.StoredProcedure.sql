
if Exists(Select 1 from sys.procedures where name='usp_UpdateUserPassword')
Begin	
	Drop Procedure usp_UpdateUserPassword
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_UpdateUserPassword
@LoginId varchar(100),
@OldPassword varchar(500),
@NewPassword varchar(500)
As
Begin
	If Not Exists (Select 1 From ClientMaster With(NoLock) Where LoginId = @LoginId)
	Begin
		Select 'Invalid User...'
		Return
	End
	If Not Exists (Select 1 From ClientMaster With(NoLock) Where LoginId = @LoginId and LoginPassword =@OldPassword)
	Begin
		Select 'Invalid Old Password...'
		Return
	End

	Update ClientMaster Set LoginPassword = @NewPassword,ModifiedDate=GETDATE() Where LoginId = @LoginId

	Select 'Success'

	 
End