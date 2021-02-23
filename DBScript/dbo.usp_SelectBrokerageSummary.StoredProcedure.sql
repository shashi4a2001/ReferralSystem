
if Exists(Select 1 from sys.procedures where name='usp_SelectBrokerageSummary')
Begin	
	Drop Procedure usp_SelectBrokerageSummary
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_SelectBrokerageSummary '101'
Create Procedure usp_SelectBrokerageSummary
@BrokerageType varchar(10)

As
Begin

	 Select 
	 a.Id,
	 b.ClientName As [Shared To],
	 (Case a.BrokerageType When '101' Then 'Referral' When '102' then 'Revenue' Else '' End) As [BrokerageType],
	 a.BrokerageAmount As [Amount Shared (Per Client)],
	 Replace(Convert(varchar, a.DateFrom,106),' ','-') As [DateFrom],
	 Replace(Convert(varchar, a.DateTo,106),' ','-') As [DateTo],	 
	 a.CreatedBy As [Process Run By],
	 a.CreatedDate As [Processed Date] 
	 From BrokerageSummary a Inner Join ClientMaster b On a.AmountToClient=b.ClientId
	 Where a.BrokerageType=@BrokerageType
	 Order By a.DateFrom Desc,a.DateTo Desc
End
