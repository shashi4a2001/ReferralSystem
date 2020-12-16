
if Exists(Select 1 from sys.procedures where name='usp_CreateMissingLogin')
Begin	
	Drop Procedure usp_CreateMissingLogin
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_CreateMissingLogin
As
Begin

Insert Into UserMaster(ClientId,UserName,UserId,LoginPwd,IsFirstTimeLogin,PwdExpireOn)
select ClientId,ClientName,LoginId,'xMycjpL7vseSZizZEO8rkw==',0,GETDATE()+90
from ClientMaster b where b.ClientId Not In (Select a.ClientId from UserMaster a)
End