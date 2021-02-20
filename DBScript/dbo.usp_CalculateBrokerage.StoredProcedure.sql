
if Exists(Select 1 from sys.procedures where name='usp_CalculateBrokerage')
Begin	
	Drop Procedure usp_CalculateBrokerage
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
--Exec usp_AccountOpenReport '101'
Create Procedure usp_CalculateBrokerage
@AmountToClient BigInt,
@DateFrom DateTime = Null,
@DateTo DateTime = Null,
@BrokerageType varchar(50), --Referral(101)/Revenue(102)
@BrokerageAmount Numeric(18,2),
@UserId varchar(50)
As
Begin
 
	 If Exists (
				Select 1 From BrokerageSummary 
				Where 
					BrokerageType=@BrokerageType 
					and AmountToClient=@AmountToClient
					And ( (DateFrom Between @DateFrom And @DateTo) Or (DateTo Between @DateFrom And @DateTo) )
				)
	 Begin
		Select 'Brokerage already calculated for this period'
		Return
	 End

	 Declare @Id BigInt
	 Insert Into BrokerageSummary(AmountToClient,DateFrom,DateTo,BrokerageType,BrokerageAmount,CreatedBy,CreatedDate)
						   values(@AmountToClient,@DateFrom,@DateTo,@BrokerageType,@BrokerageAmount,@UserId,getDate())

	Set @Id=@@IDENTITY

	
End
