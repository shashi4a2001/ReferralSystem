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
         ,[CanMakeNationalHead]
         ,[CanMakeRegionalHead]
         ,[CanMakeStateFranchisee]
         ,[CanMakeDistrictFranchisee]
         ,[CanMakeIndividualAgent]
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
         ,d.[CanMakeNationalHead]
         ,d.[CanMakeRegionalHead]
         ,d.[CanMakeStateFranchisee]
         ,d.[CanMakeDistrictFranchisee]
         ,d.[CanMakeIndividualAgent]
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
         OR ISNULL(i.[CanMakeNationalHead],0)<> ISNULL(d.[CanMakeNationalHead],0)
         OR ISNULL(i.[CanMakeRegionalHead],0)<> ISNULL(d.[CanMakeRegionalHead],0)
         OR ISNULL(i.[CanMakeStateFranchisee],0)<> ISNULL(d.[CanMakeStateFranchisee],0)
         OR ISNULL(i.[CanMakeDistrictFranchisee],0)<> ISNULL(d.[CanMakeDistrictFranchisee],0)
         OR ISNULL(i.[CanMakeIndividualAgent],0)<> ISNULL(d.[CanMakeIndividualAgent],0)
		 OR ISNULL(i.[CanMakeIndividual],0)<> ISNULL(d.[CanMakeIndividual],0)
         OR ISNULL(i.[CreatedBy],'')<> ISNULL(d.[CreatedBy],'')
         OR ISNULL(i.[CreatedDate],'')<> ISNULL(d.[CreatedDate],'')
         OR ISNULL(i.[ModifiedBy],'')<> ISNULL(d.[ModifiedBy],'')
         OR ISNULL(i.[ModifiedDate],'')<> ISNULL(d.[ModifiedDate],'')
    )
GO