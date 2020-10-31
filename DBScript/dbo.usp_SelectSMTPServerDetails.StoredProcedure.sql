
if Exists(Select 1 from sys.procedures where name='usp_SelectSMTPServerDetails')
Begin	
	Drop Procedure usp_SelectSMTPServerDetails
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectSMTPServerDetails
As
Begin
	Select * from SMTPServerDetails
End