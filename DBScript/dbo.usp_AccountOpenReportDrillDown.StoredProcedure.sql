
if Exists(Select 1 from sys.procedures where name='usp_AccountOpenReportDrillDown')
Begin	
	Drop Procedure usp_AccountOpenReportDrillDown
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_AccountOpenReport '101'
Create Procedure usp_AccountOpenReportDrillDown
@ParentClientId BigInt = Null,
@ClientId BigInt = Null,
@DateFrom DateTime = Null,
@DateTo DateTime = Null
As
Begin

--b.ClientTypeName As [ClientType],Count(1) As [No .Of Account],
--			Sum(a.ReferralAmount) As [Referral Amount],Sum(a.ReferredReferralRevenue) As [Referred Referral Revenue]

	
	Select  b.ClientTypeCode,c.ClientTypeName,b.ClientName,(b.ReferralAmount) As [Referral Amount],(b.ReferredReferralRevenue) As [Referred Referral Revenue],b.ContactPerson,b.EmailId,b.ClientId 
	Into #t1
	from ClientHierarchy a With(NoLock)
	Inner Join ClientMaster b With(NoLock) On a.ClientId=b.ClientId
	Inner Join ClientTypeMaster c With(NoLock) On a.ClientTypeCode=c.ClientTypeCode
	where a.ReferredClientId=@ClientId and ReferredOrder=1 order by ReferredOrder,b.ClientTypeCode
	
	Select ClientTypeName,Count(1)As [Count] From #t1 Group By ClientTypeCode,ClientTypeName Order By ClientTypeCode
	Select * From #t1 Order By ClientTypeCode
	
End
