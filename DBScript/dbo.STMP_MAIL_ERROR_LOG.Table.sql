GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='STMP_MAIL_ERROR_LOG' and type_desc='USER_TABLE')
Begin

CREATE TABLE [dbo].[STMP_MAIL_ERROR_LOG](
	[LOG_ID] [int] IDENTITY(1,1) NOT NULL,
	[TO] [varchar](max) NULL,
	[FROM] [varchar](100) NULL,
	[SUBJECT] [varchar](500) NULL,
	[MAIL_BODY] [varchar](max) NULL,
	[STATUS] [varchar](10) NULL,
	[ERROR] [varchar](max) NULL,
	[LOG_DATE] [datetime] NULL,
	[IS_UNDERWRITER] [char](1) NULL,
	[MAIL_TYPE] [char](1) NULL,
	[REQUEST_ID] [varchar](100) NULL,
	[MAIL_MSG_ID] [varchar](100) NULL,
	[CC] [varchar](8000) NULL,
	[BCC] [varchar](8000) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

End
GO
ALTER TABLE [dbo].[STMP_MAIL_ERROR_LOG] ADD  DEFAULT ('I') FOR [MAIL_TYPE]
GO