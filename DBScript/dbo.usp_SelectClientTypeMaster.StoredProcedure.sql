
if Exists(Select 1 from sys.procedures where name='usp_SelectClientTypeMaster')
Begin	
	Drop Procedure usp_SelectClientTypeMaster
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectClientTypeMaster
As
Begin
	Select ClientTypeLevel,ClientTypeCode,ClientTypeName,
	CanMakeSuperAdmin,CanMakeNationalHead,CanMakeRegionalHead,
	CanMakeStateFranchisee,CanMakeDistrictFranchisee,CanMakeIndividualAgent,CanMakeIndividual 
	From ClientTypeMaster With(NoLock)	
End