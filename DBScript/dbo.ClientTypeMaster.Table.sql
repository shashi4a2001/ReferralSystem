GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='ClientTypeMaster' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[ClientTypeMaster](
ClientTypeLevel Int,
ClientTypeCode varchar(10),
ClientTypeName varchar(50),
CanMakeSuperAdmin Bit,
CanMakeNationalHead Bit,
CanMakeRegionalHead Bit,
CanMakeStateFranchisee Bit,
CanMakeDistrictFranchisee Bit,
CanMakeIndividualAgent Bit,
CanMakeIndividual Bit,
CreatedBy varchar(100),
CreatedDate DateTime,
ModifiedBy varchar(100),
ModifiedDate DateTime
)
End
GO






