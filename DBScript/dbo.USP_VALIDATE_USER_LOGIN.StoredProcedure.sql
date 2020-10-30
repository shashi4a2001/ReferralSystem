
if Exists(Select 1 from sys.procedures where name='USP_VALIDATE_USER_LOGIN')
Begin	
	Drop Procedure USP_VALIDATE_USER_LOGIN
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure USP_VALIDATE_USER_LOGIN
@LoginId varchar(50),
@LoginPassword varchar(200)
As
Begin
	
	If Exists (select 1 From ClientMaster With(NoLock) Where LoginId =@LoginId and LoginPassword = @LoginPassword)
	Begin
		Declare @ClientTypeCode varchar(50)
		Declare @ClientId BigInt
		select @ClientId=ClientId, @ClientTypeCode=ClientTypeCode From ClientMaster With(NoLock) Where LoginId =@LoginId
		select 'USEREXISTS' As [Msg],@ClientId as ClientId,@ClientTypeCode As [ClientTypeCode]
		Return
	End 	
End