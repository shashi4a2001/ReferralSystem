
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
@ParentClientId BigInt = Null,
@ClientId BigInt = Null,
@DateFrom DateTime = Null,
@DateTo DateTime = Null
As
Begin
 
	
End
