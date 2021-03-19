
if Exists(Select 1 from sys.procedures where name='usp_UploadClientMasterComplete')
Begin	
	Drop Procedure usp_UploadClientMasterComplete
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_UploadClientMasterComplete
As
Begin
	Exec usp_SetClientHieararchyBulk
End