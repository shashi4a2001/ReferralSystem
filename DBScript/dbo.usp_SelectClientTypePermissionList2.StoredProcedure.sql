
if Exists(Select 1 from sys.procedures where name='usp_SelectClientTypePermissionList2')
Begin	
	Drop Procedure usp_SelectClientTypePermissionList2
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectClientTypePermissionList2
@ClientTypeCode varchar(50)
As
Begin
	
	If @ClientTypeCode='106'
	Begin
		select a.ClientTypeLevel,a.ClientTypeCode,a.ClientTypeName from ClientTypeMaster a With(NoLock) 
		where a.ClientTypeLevel=6
	End
	Else
	Begin 
		select a.ClientTypeLevel,a.ClientTypeCode,a.ClientTypeName from ClientTypeMaster a With(NoLock) 
		where a.ClientTypeLevel>(
									Select b.ClientTypeLevel From ClientTypeMaster b with(NoLock) 
									Where b.ClientTypeCode=@ClientTypeCode
								)
	End

End