GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='BrokerageDetail' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[BrokerageDetail](
Id BigInt Identity(1,1),
FKId BigInt,
BrokerageType varchar(50), --Referral(101)/Revenue(102)
ClientId BigInt,
SharingPercentage Numeric(18,2),--Actual Sharing Percentage
AmountEarnedPerClient Numeric(18,2), -- Per Client Earned Amount (Not Required in 102)
ReferralAmountBalance Numeric(18,2), --Balance amount of that Row for Per Client amount calculation
ReferralClientCount Numeric(18,2), --Client Count
AmountEarnedTotal Numeric(18,2), --Total and Actual Earned Amount
CreatedBy varchar(100),
CreatedDate DateTime
)
End
GO
