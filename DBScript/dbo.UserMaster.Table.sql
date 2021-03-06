GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Not Exists(Select 1 from SYS.Tables where name='UserMaster' and type_desc='USER_TABLE')
Begin
CREATE TABLE [dbo].[UserMaster](
ClientId BigInt,
UserName varchar(200) NOT NULL,
UserId varchar(100),
LoginPwd varchar(300),
IsFirstTimeLogin Bit,
PwdExpireOn DateTime,
LoginPwd1 varchar(300),
LoginPwd2 varchar(300),
LoginPwd3 varchar(300),
CreatedBy varchar(100),
CreatedDate DateTime,
ModifiedBy varchar(100),
ModifiedDate DateTime
)
End
GO
