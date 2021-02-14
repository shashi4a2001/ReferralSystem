
if Exists(Select 1 from sys.procedures where name='usp_SetClientHieararchy')
Begin	
	Drop Procedure usp_SetClientHieararchy
End
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SetClientHieararchy
@ClientId BigInt
As
Begin

/*

	Create table #ClientMasterAll(id int Identity(1,1),ClientId Bigint )
	Insert Into #ClientMasterAll(ClientId)
	Select ClientId From ClientMaster Order By ClientId

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

 
*/
	If IsNull(@ClientId,0)=0
	Begin
		Return
	End

	Declare @ClientTypeCode varchar(10)
	Declare @ReferredReferralCode varchar(50)


	Declare @IAReferralCode varchar(50)
	Declare @DFReferralCode varchar(50)
	Declare @SFReferralCode varchar(50)
	Declare @RHReferralCode varchar(50)
	Declare @NHReferralCode varchar(50)
	
	Set @IAReferralCode =''
	Set @DFReferralCode =''
	Set @SFReferralCode =''
	Set @RHReferralCode =''
	Set @NHReferralCode =''
	

	Declare @HrchyClientTypeCode varchar(50)
	Declare @HrchyNewSelfReferralCode varchar(50)
	Declare @HrchyNewReferredReferralCode varchar(50)
	
	 Select @ClientId=ClientId,@ClientTypeCode=ClientTypeCode,@ReferredReferralCode=ReferredReferralCode
	 From ClientMaster With(NoLock)
	 Where ClientId = @ClientId

	 If LTrim(RTrim(IsNull(@ReferredReferralCode,'')))In ('','NA')
	Begin
		Set @ReferredReferralCode=''
	End


	 Declare @iCnt Int
	 Set @iCnt=1

	 If IsNull(@ClientId,0)>0 
		And IsNull(@ClientTypeCode,'') In ('101','102','103','104','105','106')
		And IsNull(@ReferredReferralCode,'') <>''
	 Begin

		While (@iCnt<=7)
		Begin
			Set @HrchyClientTypeCode=''
			Set @HrchyNewReferredReferralCode=''

			If IsNull(@ReferredReferralCode,'')<>''
			Begin
			Print @ReferredReferralCode
				Select 
				@HrchyClientTypeCode=ClientTypeCode,
				@HrchyNewSelfReferralCode=SelfReferralCode,
				@HrchyNewReferredReferralCode=ReferredReferralCode
				From ClientMaster With(NoLock) 
				Where SelfReferralCode=@ReferredReferralCode

				If LTrim(RTrim(IsNull(@HrchyClientTypeCode,'')))In ('','NA')
				Begin
					Set @HrchyClientTypeCode=''
				End
				If LTrim(RTrim(IsNull(@HrchyNewSelfReferralCode,'')))In ('','NA')
				Begin
					Set @HrchyNewSelfReferralCode=''
				End
				If LTrim(RTrim(IsNull(@HrchyNewReferredReferralCode,'')))In ('','NA')
				Begin
					Set @HrchyNewReferredReferralCode=''
				End

				-- Set New Referral code to search
				Set @ReferredReferralCode=@HrchyNewReferredReferralCode 

				If @HrchyClientTypeCode='105' --Individual Agent
				Begin
					Set @IAReferralCode=@HrchyNewSelfReferralCode
				End
				Else If @HrchyClientTypeCode='104' --District Franchisee
				Begin
					Set @DFReferralCode=@HrchyNewSelfReferralCode
				End
				Else If @HrchyClientTypeCode='103' --State Franchisee
				Begin
					Set @SFReferralCode=@HrchyNewSelfReferralCode
				End
				Else If @HrchyClientTypeCode='102' --Regional Head
				Begin
					Set @RHReferralCode=@HrchyNewSelfReferralCode
				End
				Else If @HrchyClientTypeCode='101' --National Head
				Begin
					Set @NHReferralCode=@HrchyNewSelfReferralCode
				End
			End
			
			Set @iCnt=@iCnt+1
		End

		If Exists 
		(
			Select 1 From ClientMaster 
			Where ClientId =@ClientId
				And 
				(
						IsNull(NHReferralCode,'')<>IsNull(@NHReferralCode,'')
					OR IsNull(RHReferralCode,'')<>IsNull(@RHReferralCode,'')
					OR IsNull(SFReferralCode,'')<>IsNull(@SFReferralCode,'')
					OR IsNull(DFReferralCode,'')<>IsNull(@DFReferralCode,'')
					OR IsNull(IAReferralCode,'')<>IsNull(@IAReferralCode,'')
				)
		)
		Begin
			Update ClientMaster Set
			NHReferralCode=@NHReferralCode,
			RHReferralCode=@RHReferralCode,
			SFReferralCode=@SFReferralCode,
			DFReferralCode=@DFReferralCode,
			IAReferralCode=@IAReferralCode,
			ModifiedBy='HierarchyUpdate',
			ModifiedDate=GETDATE()
			Where ClientId =@ClientId
		End
		
	 End
	 
End