go
if Exists(Select 1 from sys.Triggers where name='Trg_ClientTypeMaster')
Begin
	Drop Trigger Trg_ClientTypeMaster
End

Go

CREATE Trigger [Trg_ClientTypeMaster]
ON ClientTypeMaster
FOR UPDATE,DELETE 
AS
    INSERT INTO ClientTypeMaster_Audit
    (
          [ClientTypeLevel]
         ,[ClientTypeCode]
         ,[ClientTypeName]
         ,[CanMakeSuperAdmin]
         ,[CanMakeMasterAgent]
         ,[CanMakeSubAgent]
         ,[CanMakeFranchisee]
         ,[CanMakeFranchiseeAgent]
         ,[CanMakeIndividual]
         ,[CreatedBy]
         ,[CreatedDate]
         ,[ModifiedBy]
         ,[ModifiedDate]
         ,[AuditCreatedBy]
         ,[AuditDate]
    )
    Select 
          d.[ClientTypeLevel]
         ,d.[ClientTypeCode]
         ,d.[ClientTypeName]
         ,d.[CanMakeSuperAdmin]
         ,d.[CanMakeMasterAgent]
         ,d.[CanMakeSubAgent]
         ,d.[CanMakeFranchisee]
         ,d.[CanMakeFranchiseeAgent]
         ,d.[CanMakeIndividual]
         ,d.[CreatedBy]
         ,d.[CreatedDate]
         ,d.[ModifiedBy]
         ,d.[ModifiedDate]
         ,i.[ModifiedBy]
         ,dbo.fnGetDate()
    FROM deleted d
    LEFT JOIN inserted i on i.ClientTypeLevel  = d.ClientTypeLevel
    WHERE
    (
            ISNULL(i.[ClientTypeLevel],0)<> ISNULL(d.[ClientTypeLevel],0)
         OR ISNULL(i.[ClientTypeCode],'')<> ISNULL(d.[ClientTypeCode],'')
         OR ISNULL(i.[ClientTypeName],'')<> ISNULL(d.[ClientTypeName],'')
         OR ISNULL(i.[CanMakeSuperAdmin],0)<> ISNULL(d.[CanMakeSuperAdmin],0)
         OR ISNULL(i.[CanMakeMasterAgent],0)<> ISNULL(d.[CanMakeMasterAgent],0)
         OR ISNULL(i.[CanMakeSubAgent],0)<> ISNULL(d.[CanMakeSubAgent],0)
         OR ISNULL(i.[CanMakeFranchisee],0)<> ISNULL(d.[CanMakeFranchisee],0)
         OR ISNULL(i.[CanMakeFranchiseeAgent],0)<> ISNULL(d.[CanMakeFranchiseeAgent],0)
         OR ISNULL(i.[CanMakeIndividual],0)<> ISNULL(d.[CanMakeIndividual],0)
         OR ISNULL(i.[CreatedBy],'')<> ISNULL(d.[CreatedBy],'')
         OR ISNULL(i.[CreatedDate],'')<> ISNULL(d.[CreatedDate],'')
         OR ISNULL(i.[ModifiedBy],'')<> ISNULL(d.[ModifiedBy],'')
         OR ISNULL(i.[ModifiedDate],'')<> ISNULL(d.[ModifiedDate],'')
    )
GO