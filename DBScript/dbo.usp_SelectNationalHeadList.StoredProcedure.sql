
if Exists(Select 1 from sys.procedures where name='usp_SelectNationalHeadList')
Begin	
	Drop Procedure usp_SelectNationalHeadList
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_SelectNationalHeadList
Create Procedure usp_SelectNationalHeadList
 
As
Begin
Select ClientId,ClientName From
	(
	Select 0 As [ClientId],'--Select--' As [ClientName]
	Union All
	Select ClientId,ClientName From ClientMaster Where ClientTypeCode=101
	) a Order By a.ClientId
End
