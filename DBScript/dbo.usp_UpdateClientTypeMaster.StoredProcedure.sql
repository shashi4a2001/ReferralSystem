
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
@CanMakeMasterAgent Bit,
@CanMakeSubAgent Bit,
@CanMakeFranchisee Bit,
@CanMakeFranchiseeAgent Bit,
@CanMakeIndividual Bit,
@UserId varchar(100)
As
Begin

	Update ClientTypeMaster Set 
	CanMakeSuperAdmin=@CanMakeSuperAdmin,
	CanMakeMasterAgent=@CanMakeMasterAgent,
	CanMakeSubAgent=@CanMakeSubAgent,
	CanMakeFranchisee=@CanMakeFranchisee,
	CanMakeFranchiseeAgent=@CanMakeFranchiseeAgent,
	CanMakeIndividual=@CanMakeIndividual,
	ModifiedBy=@UserId,
	ModifiedDate=dbo.fnGetDate()
	Where ClientTypeCode=@ClientTypeCode

End