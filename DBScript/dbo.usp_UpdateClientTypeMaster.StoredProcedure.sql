
if Exists(Select 1 from sys.procedures where name='usp_UpdateClientTypeMaster')
Begin	
	Drop Procedure usp_UpdateClientTypeMaster
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_UpdateClientTypeMaster
@ClientTypeCode varchar(10),
@CanMakeSuperAdmin Bit,
@CanMakeNationalHead Bit,
@CanMakeRegionalHead Bit,
@CanMakeStateFranchisee Bit,
@CanMakeDistrictFranchisee Bit,
@CanMakeIndividualAgent Bit,
@CanMakeIndividual Bit,
@UserId varchar(100)
As
Begin

	Update ClientTypeMaster Set 
	CanMakeSuperAdmin=@CanMakeSuperAdmin,
	CanMakeNationalHead=@CanMakeNationalHead,
	CanMakeRegionalHead=@CanMakeRegionalHead,
	CanMakeStateFranchisee=@CanMakeStateFranchisee,
	CanMakeDistrictFranchisee=@CanMakeDistrictFranchisee,
	CanMakeIndividualAgent=@CanMakeIndividualAgent,
	CanMakeIndividual=@CanMakeIndividual,
	ModifiedBy=@UserId,
	ModifiedDate=dbo.fnGetDate()
	Where ClientTypeCode=@ClientTypeCode

End