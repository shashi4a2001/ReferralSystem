SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
If Not Exists(Select 1 from sys.tables where name ='SMTPServerDetails')
Begin
CREATE TABLE [dbo].[SMTPServerDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HostName] [varchar](60) NOT NULL,
	[PortNo] [numeric](18, 0) NULL,
	[ReqAuth] [char](5) NULL,
	[EnableSSL] [varchar](50) NULL,
	[UserId] [varchar](50) NULL,
	[UserPWD] [varchar](200) NULL,
	[SenderEmailID] [varchar](200) NULL,
	[DisplayName] [varchar](200) NULL,
	[Status] [varchar](10) NULL,
	[CreatedBy] [varchar](20) NULL,
	[CreationDate] [datetime] NULL,
	[ChangedRemarks] [varchar](200) NULL,
 CONSTRAINT [PK_SMTPServerDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[HostName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
End
GO