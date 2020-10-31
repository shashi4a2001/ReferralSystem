
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
If Not Exists(Select 1 from sys.tables where name ='EmailTemplateMaster')
Begin
CREATE TABLE [dbo].[EmailTemplateMaster](
	[TemplateType] [varchar](100) NOT NULL,
	[SubjectLine] [varchar](150) NULL,
	[TemplateText] [text] NULL,
 CONSTRAINT [PK_EmailTemplateMaster] PRIMARY KEY CLUSTERED 
(
	[TemplateType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
End
GO