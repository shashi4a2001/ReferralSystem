
if Exists(Select 1 from sys.procedures where name='usp_SelectClientByUserId')
Begin	
	Drop Procedure usp_SelectClientByUserId
End
GO
--Exec usp_SelectEmailMsg 'abc','PasswordChanged'
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectClientByUserId
@UserId varchar(100) 
As
Begin
	 Select ClientId,ClientCode,LoginPassword From ClientMaster With(NoLock) Where LoginId=@UserId
End