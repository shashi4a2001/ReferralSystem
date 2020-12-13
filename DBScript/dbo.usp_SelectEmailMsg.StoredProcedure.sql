
if Exists(Select 1 from sys.procedures where name='usp_SelectEmailMsg')
Begin	
	Drop Procedure usp_SelectEmailMsg
End
GO
--Exec usp_SelectEmailMsg '100','PasswordChanged'
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectEmailMsg
@ClientId varchar(100)=null,
@TemplateType varchar(100)
As
Begin
	Declare @SubjectLine varchar(150)
	Declare @TemplateText varchar(max)
	Declare @ToEmailId varchar(200)
	Declare @LoginId varchar(100)

	set @SubjectLine =''
	set @TemplateText =''
	set @ToEmailId =''
	set @LoginId=''

	Select * From SMTPServerDetails With(NoLock)

	Select @ToEmailId=IsNull(EmailId,''),@LoginId=LoginId From ClientMaster With(NoLock) Where ClientId=@ClientId
	Select @SubjectLine=SubjectLine,@TemplateText=TemplateText 
	From EmailTemplateMaster With(NoLock) Where TemplateType=@TemplateType


	if (@TemplateType='PasswordChanged')
	Begin

		
		Select @ToEmailId As [ToEmailId],@SubjectLine As [SubjectLine],@TemplateText As [TemplateText]

	End
	if (@TemplateType='LoginCreation')
	Begin

		Set @TemplateText=Replace(@TemplateText,'(LoginId)',@LoginId)
		Select @ToEmailId As [ToEmailId],@SubjectLine As [SubjectLine],@TemplateText As [TemplateText]

	End
	if (@TemplateType='ForgotPassword')
	Begin

		Set @TemplateText=Replace(@TemplateText,'(LoginId)',@LoginId)
		Select @ToEmailId As [ToEmailId],@SubjectLine As [SubjectLine],@TemplateText As [TemplateText]

	End
	
End