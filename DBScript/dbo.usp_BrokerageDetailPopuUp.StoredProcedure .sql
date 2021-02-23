
if Exists(Select 1 from sys.procedures where name='usp_BrokerageDetailPopuUp')
Begin	
	Drop Procedure usp_BrokerageDetailPopuUp
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_BrokerageDetailPopuUp '10804','102'
Create Procedure usp_BrokerageDetailPopuUp
@ClientId BigInt = Null,
@BrokerageType varchar(10)

As
Begin

	Select * Into #BrokerageDetail From BrokerageDetail With(NoLock) Where ClientId=@ClientId and BrokerageType=@BrokerageType

	If @BrokerageType='101'
	Begin
		Select 
		Replace(Convert(varchar, b.DateFrom,106),' ','-') As [DateFrom],
		Replace(Convert(varchar, b.DateTo,106),' ','-') As [DateTo],
		a.SharingPercentage,
		a.ReferralClientCount,
		a.AmountEarnedPerClient,
		a.AmountEarnedTotal,
		a.CreatedDate As [CalculatedDate] 
		From #BrokerageDetail a Inner Join BrokerageSummary b on a.FKId=b.Id
		Order By b.DateFrom, b.DateTo
	End
	
	If @BrokerageType='102'
	Begin
		Select 
		Replace(Convert(varchar, b.DateFrom,106),' ','-') As [DateFrom],
		Replace(Convert(varchar, b.DateTo,106),' ','-') As [DateTo],
		a.SharingPercentage,
		--a.AmountEarnedPerClient,
		--a.ReferralClientCount,
		a.AmountEarnedTotal,
		a.CreatedDate As [CalculatedDate] 
		From #BrokerageDetail a Inner Join BrokerageSummary b on a.FKId=b.Id
		Order By b.DateFrom, b.DateTo
	End

End
