
if Exists(Select 1 from sys.procedures where name='usp_SelectClientByUserId')
Begin	
	Drop Procedure usp_SelectClientByUserId
End
GO
--Exec usp_SelectClientByUserId 'abc'
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectClientByUserId
@UserId varchar(100) 
As
Begin
	 Select a.ClientId,a.ClientCode,
	 (Select LoginPWd From UserMaster b With(NoLock) Where b.UserId=@UserId)LoginPassword 
	 From ClientMaster a With(NoLock) Where a.LoginId=@UserId
End