
if Exists(Select 1 from sys.procedures where name='usp_SelectClientByClientId')
Begin	
	Drop Procedure usp_SelectClientByClientId
End
GO
--Exec usp_SelectClientByClientId '113'
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectClientByClientId
@ClientId BigInt
As
Begin
	 select Clientid,ClientCode,ClientName,EmailId,LoginId from ClientMaster where clientid=@ClientId
End