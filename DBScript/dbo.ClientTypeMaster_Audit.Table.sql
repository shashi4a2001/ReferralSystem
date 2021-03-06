GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='ClientTypeMaster_Audit' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[ClientTypeMaster_Audit](
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
ModifiedDate DateTime,
AuditCreatedBy varchar(100),
AuditDate DateTime
)
End
GO
