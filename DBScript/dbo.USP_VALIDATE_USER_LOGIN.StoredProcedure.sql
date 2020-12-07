
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
	Declare @ClientId BigInt
	Declare @IsFirstTimeLogin Bit
	Declare @PwdExpireOn DateTime
	Set @IsFirstTimeLogin=1
	Set @PwdExpireOn='01/01/1900'

	Select 
	@ClientId=ClientId,
	@IsFirstTimeLogin=IsNull(IsFirstTimeLogin,1),
	@PwdExpireOn=IsNull(PwdExpireOn,'01/01/1900')
	From UserMaster With(NoLock) Where UserId =@LoginId and LoginPwd = @LoginPassword

	If IsNull(@ClientId,0)>0
	Begin
		Declare @ClientTypeCode varchar(50)
		Declare @ClientName varchar(2000)


		select @ClientId=ClientId, @ClientTypeCode=ClientTypeCode,@ClientName=ClientName From ClientMaster With(NoLock) Where ClientId =@ClientId
		select 'USEREXISTS' As [Msg],@ClientId as ClientId,@ClientTypeCode As [ClientTypeCode],
				@ClientName As [ClientName],@IsFirstTimeLogin As [IsFirstTimeLogin],
				Cast(Convert(varchar,@PwdExpireOn,103) As DateTime) As [PwdExpireOn],
				dbo.fnGetDate() As [CurrentDate]
		Return
	End 	
End