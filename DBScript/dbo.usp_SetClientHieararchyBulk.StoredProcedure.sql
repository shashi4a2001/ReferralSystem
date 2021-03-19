
if Exists(Select 1 from sys.procedures where name='usp_SetClientHieararchyBulk')
Begin	
	Drop Procedure usp_SetClientHieararchyBulk
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SetClientHieararchyBulk
@CreatedDate DateTime=null
As
Begin


	Create table #ClientMasterAll(id int Identity(1,1),ClientId Bigint )

	If @CreatedDate Is Null
	Begin
		Insert Into #ClientMasterAll(ClientId)
		Select ClientId From ClientMaster  Order By ClientTypeCode desc
	End
	Else
	Begin
		Insert Into #ClientMasterAll(ClientId)
		Select ClientId From ClientMaster  
		Where Cast(Convert(varchar,CreatedDateInSystem,101) As DateTime)=Cast(Convert(varchar,@CreatedDate,101) As DateTime)
		Order By ClientTypeCode desc
	End

	Declare @iLoop Int
	Set @iLoop=1
	Declare @totcnt Int
	Set @totcnt = (Select Count(1) From #ClientMasterAll)
	While @totcnt>=@iLoop
	Begin
	--Print @iLoop
		Declare @ClientId BigInt
		Set @ClientId=null

		Select @ClientId=ClientId From #ClientMasterAll Where Id = @iLoop
	 
		If IsNull(@ClientId,0)>0
		Begin
			Exec usp_SetClientHieararchy @ClientId =@ClientId
		End

		Set @iLoop=@iLoop+1
	End

 
 
End