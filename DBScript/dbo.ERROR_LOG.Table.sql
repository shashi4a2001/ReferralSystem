GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='ERROR_LOG' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[ERROR_LOG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FORM_NAME] [varchar](200) NULL,
	[CUSTOM_MSG] [varchar](1000) NULL,
	[ERROR_MSG] [varchar](2000) NULL,
	[STACK_TRACE] [varchar](max) NULL,
	[IP_ADDRESS] [varchar](100) NULL,
	[LOG_TIME] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
End
GO
