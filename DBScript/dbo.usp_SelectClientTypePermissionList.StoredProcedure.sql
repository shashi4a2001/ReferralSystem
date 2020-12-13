
if Exists(Select 1 from sys.procedures where name='usp_SelectClientTypePermissionList')
Begin	
	Drop Procedure usp_SelectClientTypePermissionList
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectClientTypePermissionList
@ClientTypeCode varchar(50)
As
Begin
	 
	Select * Into #t1 From ClientTypeMaster With(NoLock)	
	Where ClientTypeCode = @ClientTypeCode

	Select ClientTypeLevel,ClientTypeCode,ClientTypeName 
	Into #t2
	From ClientTypeMaster With(NoLock)	
	
	If Not Exists (Select 1 From #t1 Where CanMakeSuperAdmin =1)
	Begin
		Delete From #t2 Where ClientTypeCode='100'
	End
	If Not Exists (Select 1 From #t1 Where CanMakeNationalHead =1)
	Begin
		Delete From #t2 Where ClientTypeCode='101'
	End
	If Not Exists (Select 1 From #t1 Where CanMakeRegionalHead =1)
	Begin
		Delete From #t2 Where ClientTypeCode='102'
	End
	If Not Exists (Select 1 From #t1 Where CanMakeStateFranchisee =1)
	Begin
		Delete From #t2 Where ClientTypeCode='103'
	End
	If Not Exists (Select 1 From #t1 Where CanMakeDistrictFranchisee =1)
	Begin
		Delete From #t2 Where ClientTypeCode='104'
	End
	If Not Exists (Select 1 From #t1 Where CanMakeIndividualAgent =1)
	Begin
		Delete From #t2 Where ClientTypeCode='105'
	End
	If Not Exists (Select 1 From #t1 Where CanMakeIndividual =1)
	Begin
		Delete From #t2 Where ClientTypeCode='106'
	End

	Select * From #t2 Order By ClientTypeLevel 
End