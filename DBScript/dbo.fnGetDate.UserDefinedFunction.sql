
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[fnGetDate]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
Begin
	Drop Function fnGetDate
End 
Go
create function [dbo].[fnGetDate] ()
returns DateTime
As
Begin
 Return getdate()
End
GO