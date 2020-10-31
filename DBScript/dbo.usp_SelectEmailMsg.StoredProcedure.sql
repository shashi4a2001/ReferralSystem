
if Exists(Select 1 from sys.procedures where name='usp_SelectEmailMsg')
Begin	
	Drop Procedure usp_SelectEmailMsg
End
GO
--Exec usp_SelectEmailMsg 'abc','PasswordChanged'
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
Create Procedure usp_SelectEmailMsg
@UserId varchar(100),
@TemplateType varchar(100)
As
Begin
	Declare @SubjectLine varchar(150)
	Declare @TemplateText varchar(max)
	Declare @ToEmailId varchar(200)

	set @SubjectLine =''
	set @TemplateText =''
	set @ToEmailId =''


	if (@TemplateType='PasswordChanged')
	Begin

		Select * From SMTPServerDetails With(NoLock)

		Select @ToEmailId=IsNull(EmailId,'') From ClientMaster With(NoLock) Where LoginId=@UserId
		Select @SubjectLine=SubjectLine,@TemplateText=TemplateText 
		From EmailTemplateMaster With(NoLock) Where TemplateType='PasswordChanged'

		Select @ToEmailId As [ToEmailId],@SubjectLine As [SubjectLine],@TemplateText As [TemplateText]

	End
End