GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='BrokerageSummary' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[BrokerageSummary](
Id BigInt Identity(1,1),
AmountToClient BigInt,
DateFrom DateTime,
DateTo DateTime,
BrokerageType varchar(50), --Referral(101)/Revenue(102)
BrokerageAmount Numeric(18,2),
CreatedBy varchar(100),
CreatedDate DateTime,
ModifiedBy varchar(100),
ModifiedDate DateTime
)
End
GO
