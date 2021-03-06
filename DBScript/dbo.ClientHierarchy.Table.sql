GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='ClientHierarchy' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[ClientHierarchy](
Id BigInt Identity(1,1),
ClientId BigInt,
ClientTypeCode varchar(10),
ReferredClientId BigInt,
ReferredClientTypeCode varchar(10),
ReferredOrder Int,
CreatedBy varchar(100),
CreatedDate DateTime,
ModifiedBy varchar(100),
ModifiedDate DateTime
)
End
GO
