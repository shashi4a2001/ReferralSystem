GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='ClientMaster' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[ClientMaster](
ClientId BigInt Identity(100,1),
MobileNo varchar(50), --Fetch these value from API on the basis of mobile no.:  ClientName(Client_name),ClientCode(Clint_bow_id),ClientRefid(Client_inv_id),ReferralCode(Agent_Generated_Referral_Code)
ClientRefId BigInt NULL, --Client_Company_id
ClientCode varchar(100) NOT NULL,
ClientName varchar(200) NOT NULL,
ClientTypeCode varchar(10),
ContactPerson varchar(200),
EmailId varchar(100),
LandlineNo varchar(100),
Address varchar(500),
LoginId varchar(100),
BankName varchar(200),
ClientNameAsPerBank varchar(300),
AccountNo varchar(100),
IFSCCode varchar(100),
ReferralSharingPercentage Numeric(18,2),
SelfReferralCode varchar(50),
ReferredReferralCode varchar(50),
ReferralAmount Numeric(18,2),
ReferredReferralRevenue  Numeric(18,2),
RevenueSharingPercentage  Numeric(18,2),
CreatedBy varchar(100),
CreatedDate DateTime,
ModifiedBy varchar(100),
ModifiedDate DateTime,
CreatedByInSystem varchar(100),
CreatedDateInSystem DateTime
)
End
GO
