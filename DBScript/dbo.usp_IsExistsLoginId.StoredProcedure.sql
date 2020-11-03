
if Exists(Select 1 from sys.procedures where name='usp_IsExistsLoginId')
Begin	
	Drop Procedure usp_IsExistsLoginId
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_IsExistsLoginId
@LoginId varchar(100)
As
Begin
	If Exists (Select 1 From ClientMaster With(NoLock) Where LoginId = @LoginId)
	Begin
		Select 'Oops Login Id unavailable..'
		Return
	End
	Select 'Success - Login Id is available..'
End