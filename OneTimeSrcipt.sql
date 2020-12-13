
IF (EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'CanMakeMasterAgent' AND Object_ID = Object_ID(N'ClientTypeMaster')))
BEGIN
	 Drop Table ClientTypeMaster
END
GO
IF (EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'CanMakeMasterAgent' AND Object_ID = Object_ID(N'ClientTypeMaster_Audit')))
BEGIN
	 Drop Table ClientTypeMaster_Audit
END