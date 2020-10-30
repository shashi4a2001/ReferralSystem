 /*
 CREATED BY	: XCEEDANCE
 DATE		: 10/10/2017
 DESCRIPTION: VALIDATE USER FOR LOGGIN INTO THE SYSTEM
 */
alter PROCEDURE DBO.USP_VALIDATE_USER_LOGIN    
@P_USER_ID			VARCHAR(15),  
@P_EMAIL			VARCHAR(50),  
@P_PASSWORD			VARCHAR(10) 
AS    
BEGIN    
   
	DECLARE @W_ISACTIVATED CHAR(1)  
	DECLARE @W_FIRSTTIME_LOGGEDIN CHAR(1) 

	SELECT  USERID,EmailId,PASSWORD,SEGMENT,ISACTIVE,FIRSTTIMELOGGEDIN 
	INTO #TEMP_PORTAL_LOGIN_MASTER
	FROM PORTAL_LOGIN_MASTER  (NOLOCK) 
	WHERE USERID    =@P_USER_ID 
	AND  PASSWORD   =@P_PASSWORD 
	--AND SEGMENT=@P_USER_TYPE
	--VARIFY LOGIN DETAILS  

	IF NOT EXISTS(SELECT '1' FROM #TEMP_PORTAL_LOGIN_MASTER 
	WHERE   
	USERID    =@P_USER_ID   
	AND  EMAILID  =@P_EMAIL  
	AND  PASSWORD =@P_PASSWORD  
	--AND SEGMENT=@P_USER_TYPE 
	)  
	BEGIN  
		SELECT 'ERROR' AS ERROR, 'INVALID LOGIN DETAILS.' AS MESSAGE    
		RETURN    
	END  

	IF EXISTS(SELECT '1' FROM #TEMP_PORTAL_LOGIN_MASTER
		WHERE   
		USERID			=@P_USER_ID   
		AND  EMAILID	=@P_EMAIL  
		AND  PASSWORD	=@P_PASSWORD  
		AND	 ISACTIVE	='N'
	)  
	BEGIN  
		SELECT 'ERROR' AS ERROR, 'YOUR ACCOUNT HAS BEEN DEACTIVATED.' AS MESSAGE    
		RETURN    
	END    

	SELECT @W_ISACTIVATED= ISACTIVATED 
	FROM EMPLOYEE_MASTER (NOLOCK)  
	WHERE USERID	=@P_USER_ID 
	AND EMAILID		=@P_EMAIL     

	IF(@W_ISACTIVATED='N')    
	BEGIN    
		SELECT 'ERROR' AS ERROR, 'YOUR ACCOUNT IS NOT ACTIVATED.PLEASE CONTACT THE ADMIN OFFICE.' AS MESSAGE    
		RETURN    
	END    

	--VARIFY LOGIN DETAILS  
	SELECT @W_FIRSTTIME_LOGGEDIN =COALESCE(FIRSTTIMELOGGEDIN,'Y') 
	FROM #TEMP_PORTAL_LOGIN_MASTER
	WHERE USERID    =@P_USER_ID 
	AND  PASSWORD	=@P_PASSWORD 
	--AND SEGMENT=@P_USER_TYPE

	SELECT   ID,
			 EMPLOYEECODE, 
			 [NAME] AS EMPLOYEENAME,
			 USERID, ISACTIVATED  
			,@W_FIRSTTIME_LOGGEDIN AS   'FIRSTTIMELOGGEDIN'
	FROM EMPLOYEE_MASTER(NOLOCK)  
    WHERE USERID	=@P_USER_ID 
	AND EMAILID		=@P_EMAIL  
END    

GO

  -- =============================================
-- CRETED BY:   XCEEDANCE
-- CREATE DATE: 10/10/2017
-- DESCRIPTION:	KEEP USER INFORMATION
-- MODIFIED BY:  
-- MODIFIED DATE:  
-- DESCRIPTION:	<[USP_INS_DEL_UPD_DBO.EMPLOYEE_MASTER] @P_MODE= 'S'>
-- =============================================
ALTER Procedure [dbo].[USP_INS_DEL_UPD_EMPLOYEE_MASTER]
	@P_Mode				Char(1)='I',
	@P_EmployeeCode		VARCHAR(15)=NULL,
	@P_TITLE			VARCHAR(6)='',
	@P_Name				VARCHAR(100)=NULL,
	@P_Sex				CHAR(1)=NULL,
	@P_DOB				datetime=NULL,
	@P_Designation		VARCHAR(50)=NULL,
	@P_MobileNo			VARCHAR(10)=NULL,
	@P_Address			VARCHAR(500)=NULL,
	@P_EmailId			VARCHAR(50)=NULL,
	@P_FatherName		VARCHAR(100)=NULL,
	@P_IsActivated		CHAR(1)='N',
	@P_MotherName		VARCHAR(100)=NULL,
	@P_ImagePath		VARCHAR(500)=NULL,
	@P_DOJ				datetime=NULL,
	@P_BloodGroup		varchar(4)=NULL,
	@P_Nature_Bussiness varchar(4)=NULL,
	@P_CreatedBy		VARCHAR(50)=NULL 

as
Begin

IF (@P_Mode='I')
BEGIN
		DECLARE @W_EMPCODE VARCHAR(15)
		DECLARE @W_PREFIX VARCHAR(15)
		DECLARE @W_EMPID INT
		DECLARE @W_RUNNING_ID	INT

		SELECT @W_PREFIX =[VALUE] FROM [dbo].[Seetings](NOLOCK) WHERE [KEY]='EMPLOYEE CODE PREFIX'
		
		--DOB CHECK
		IF( DATEDIFF(YEAR, @P_DOB, dbo.fnGetIndianTime())<14)
		BEGIN
			select  'Invalid Date of Birth. Please check.'  As 'ERROR'
			Return
		END

		IF EXISTS(SELECT '1' FROM dbo.EMPLOYEE_MASTER (NOLOCK)where REPLACE(Name,' ','')=REPLACE(@P_Name,' ','') AND DOB=@P_DOB)
		Begin
			Select  'Duplicate Entry. Employee Already Added.'  As 'ERROR'
			Return
		End

		If Exists(Select '1' from dbo.EMPLOYEE_MASTER where EmailId=@P_EmailId)
		Begin
			Select  'Duplicate Email ID Provided. Please check.'  As 'ERROR'
			Return
		End

		BEGIN TRAN
			--PROCESS FOR NEW ENTRY
			Insert Into dbo.EMPLOYEE_MASTER(
			[EmployeeCode]   ,
			[UserID],
			[Name]  ,
			[Sex]  ,
			[DOB]  ,
			[Designation]  ,
			[MobileNo]  ,
			[Address]  ,
			[EmailId]  ,
			[FatherName]  ,
			[MotherName]  ,
			[ImagePath],
			[DOJ],
			[BloodGroup],
			[IsActivated],
			[NatureOfBusiness],
			[CreatedBy],
			[CreatedOn]
			)
			Values 
			(
			@P_EmployeeCode  ,
			@P_EmployeeCode,
			@P_Name  ,
			@P_Sex   ,
			@P_DOB   ,
			@P_Designation   ,
			@P_MobileNo  ,
			@P_Address  ,
			@P_EmailId ,
			@P_FatherName  ,
			@P_MotherName  ,
			@P_ImagePath,
			@P_DOJ,
			@P_BloodGroup,
			@P_IsActivated,
			@P_Nature_Bussiness,
			@P_CreatedBy ,
			dbo.fnGetIndianTime()
			)

			If @@ERROR<>0
			Begin
				Select  'Error Occurred while Create Employee Detail. Please inform the Administrator'  As 'ERROR'
				ROLLBACK TRAN
				Return
			End

			--GENERATED EMP ID
			SELECT @W_RUNNING_ID=@@IDENTITY

			--LAST EMP CODE
			SELECT @W_EMPID=CAST(COALESCE([VALUE],0) AS INT)+1 FROM SEETINGS WHERE [KEY]='EMPLOYEE ID RUNNING SEQ NO'

			--UPDATE EMPLOYEE CODE AND USER ID
			SELECT @W_EMPCODE=@W_PREFIX+ CAST(@W_EMPID AS VARCHAR) 

			UPDATE dbo.EMPLOYEE_MASTER SET EmployeeCode=@W_EMPCODE,USERID=@W_EMPCODE WHERE ID=@W_RUNNING_ID
		
			 If @@ERROR<>0
			 Begin
				Select  'Error Occurred while updating Employee Detail. Please inform the Administrator'  As 'ERROR'
				ROLLBACK TRAN
				Return
			End

			--UPDATE RUNNING SEQ NO
			UPDATE SEETINGS SET [VALUE]=CAST(@W_EMPID AS VARCHAR) WHERE [KEY]='EMPLOYEE ID RUNNING SEQ NO'
			 
			 If @@ERROR<>0
			 Begin
				Select  'Error Occurred while updating Employee Running Seq No. Please inform the Administrator'  As 'ERROR'
				ROLLBACK TRAN
				Return
			End
		
			 --final details
			COMMIT TRAN

			Select '' As 'Error'   ,
					'Employee Registered Successfully.' As 'Success',	
					'automailer@soaneemrana.com' AS [FROM],
					'ccashoka@gmail.com' AS [TO],
					'mail.soaneemrana.com' AS HOST,
					25 AS [PORT],	
					'automailer@soaneemrana.com' AS [USER_ID],
					'12Soa123' AS [PASSWORD],
					@W_EMPCODE AS 'EMP_CDOE'

END
ELSE IF (@P_Mode='U')
BEGIN

		--DOB CHECK
		IF( DATEDIFF(YEAR, @P_DOB, dbo.fnGetIndianTime())<14)
		BEGIN
			select  'Invalid Date of Birth. Please check.'  As 'ERROR'
			Return
		END

		IF EXISTS(SELECT '1' FROM dbo.EMPLOYEE_MASTER (NOLOCK)where REPLACE(Name,' ','')=REPLACE(@P_Name,' ','') AND DOB=@P_DOB and [EmployeeCode]<> @P_EmployeeCode  )
		Begin
			Select  'Duplicate Entry. Employee Already Added.'  As 'ERROR'
			Return
		End

		If Exists(Select '1' from dbo.EMPLOYEE_MASTER  (NOLOCK) where EmailId=@P_EmailId and [EmployeeCode]<> @P_EmployeeCode )
		Begin
			Select  'Email ID registered with other employee. Please check.'  As 'ERROR'
			Return
		End

		UPDATE   dbo.EMPLOYEE_MASTER 
		SET
		[Name]				=@P_Name ,
		[Sex]				=@P_Sex ,
		[DOB]				=@P_DOB,
		[Designation]		=@P_Designation,
		[MobileNo]			=@P_MobileNo ,
		[Address]			=@P_Address ,
		[EmailId]			=@P_EmailId,
		[FatherName]		=@P_FatherName  ,
		[MotherName]		=@P_MotherName  ,
		[ImagePath]			=@P_ImagePath,
		[DOJ]				=@P_DOJ,
		[BloodGroup]		=@P_BloodGroup,
		[IsActivated]		=@P_IsActivated,
		[NatureOfBusiness]	=@P_Nature_Bussiness,
		[ModifiedBy]		=@P_CreatedBy,
		[ModifiedOn]		=dbo.fnGetIndianTime()
		WHERE 
		[EmployeeCode]=@P_EmployeeCode 

		If @@ERROR<>0
		Begin
			Select  'Error Occurred while updating Employee Detail. Please inform the Administrator'  As 'ERROR','' AS 'Success'
			Return
		End

		--INACTIVE PORTAL LOGIN
		IF (@P_IsActivated='N')
		BEGIN 
			UPDATE PORTAL_LOGIN_MASTER 
						SET IsActive='N', 
							ModifiedOn=dbo.fnGetIndianTime() ,
							ModifiedBy=@P_CreatedBy 
			WHERE USERID= @P_EmployeeCode
		END
	    
		SELECT '' AS 'ERROR','Employee information updated successfully.'   AS 'Success'
END
ELSE IF (@P_Mode='P')--PICTURE UPDATE
BEGIN
		UPDATE   dbo.EMPLOYEE_MASTER 
		SET
		[ImagePath]			=@P_ImagePath,
		[ModifiedBy]		=@P_CreatedBy,
		[ModifiedOn]		=dbo.fnGetIndianTime()
		WHERE 
		[EmployeeCode]=@P_EmployeeCode 

	    SELECT '' AS 'ERROR','Employee image updated Successfully.' 'Success'
END
ELSE IF (@P_Mode='D')
BEGIN

		UPDATE   dbo.EMPLOYEE_MASTER 
	     SET
		[IsActivated] ='N',
		[ModifiedBy]=@P_CreatedBy,
		[ModifiedOn]=dbo.fnGetIndianTime()
		WHERE 
		[EmployeeCode]=@P_EmployeeCode

		--INACTIVE PORTAL LOGIN
		UPDATE PORTAL_LOGIN_MASTER 
				SET IsActive='N', 
					ModifiedOn=dbo.fnGetIndianTime() ,
					ModifiedBy=@P_CreatedBy 
		WHERE USERID= @P_EmployeeCode
	
		SELECT '' AS 'ERROR','Employee has been made deactivated.' 'Success'

END
ELSE IF (@P_Mode='S')
BEGIN
		SELECT
		[ID], 
     	[EmployeeCode]   ,
		[Name] + '('+[EmployeeCode]+')' AS 'EMP_CODE',
		[UserID],
		[Name]  ,
		[Sex]  ,
		CONVERT(VARCHAR(10),[DOB] ,103) AS [DOB] ,
		[Designation]  ,
		[MobileNo]  ,
		[Address]  ,
		[EmailId]  ,
		[FatherName]  ,
		[MotherName],
		COALESCE([NatureOfBusiness],'N') AS [NatureOfBusiness],
		COALESCE(ImagePath,'') AS ImagePath,
		COALESCE(BloodGroup,'') AS 'BloodGroup',
		COALESCE(CONVERT(VARCHAR(10),[DOJ] ,103)  ,'') AS 'DOJ',
		[IsActivated],
		CASE WHEN COALESCE(ImagePath,'')='' THEN '~\Uploads\ManageUsers\SampleImage.jpg' 
								ELSE '~\Uploads\ManageUsers\'+COALESCE(ImagePath,'')  END AS 'ImageFullPath'
		FROM dbo.EMPLOYEE_MASTER(NOLOCK)
		WHERE [EmployeeCode]=COALESCE(@P_EmployeeCode,[EmployeeCode])
		ORDER BY [EmployeeCode]

 END
 ELSE IF (@P_Mode='G')--GRADING COMMITTE AND OTHERS
 BEGIN
		SELECT
		[ID], 
     	[EmployeeCode]   ,
		[Name] + '('+[EmployeeCode]+')' AS 'EMP_CODE',
		[UserID],
		[EmployeeCode]+'-'+ [Name] AS [Name]  ,
		[Sex]  ,
		CONVERT(VARCHAR(10),[DOB] ,103) AS [DOB] ,
		[Designation]  ,
		[MobileNo]  ,
		[Address]  ,
		[EmailId]  ,
		[FatherName]  ,
		[MotherName],
		COALESCE(ImagePath,'') AS ImagePath,
		COALESCE(BloodGroup,'') AS 'BloodGroup',
		COALESCE(CONVERT(VARCHAR(10),[DOJ] ,103)  ,'') AS 'DOJ',
		[IsActivated],
		CASE WHEN COALESCE(ImagePath,'')='' THEN '~\Uploads\ManageUsers\SampleImage.jpg' 
								ELSE '~\Uploads\ManageUsers\'+COALESCE(ImagePath,'')  END AS 'ImageFullPath'
		FROM dbo.EMPLOYEE_MASTER(NOLOCK)
		WHERE [EmployeeCode]=COALESCE(@P_EmployeeCode,[EmployeeCode])
		AND  NAME <>'MISSING INFO'  AND EMAILID<>'MISSING INFO'
		AND COALESCE([NatureOfBusiness],'')='Y'
		AND [IsActivated] ='Y'
		ORDER BY [EmployeeCode]

  END
END
 



 GO


  /*************************************************NEW SCRIPT BLOCK STARTS*********************************************/
DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='SMTP_HOST_ADDRESS'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'SMTP_HOST_ADDRESS','mail.xxxxxx.com','SMTP DETAILS', 1, 'XCEEDANCE', GETDATE()

DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='SMTP_PORT_NO'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'SMTP_PORT_NO','01','SMTP DETAILS', 1, 'XCEEDANCE', GETDATE()

DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='SMTP_USER_ID'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'SMTP_USER_ID','automailer@xxxxxx.com','SMTP DETAILS', 1, 'XCEEDANCE', GETDATE()

DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='SMTP_PASSWORD'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'SMTP_PASSWORD','xxxxxx@123','SMTP DETAILS', 1, 'XCEEDANCE', GETDATE()


DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='ALLOW_MODIFY_MASTER_DATA'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'ALLOW_MODIFY_MASTER_DATA','N','', 1, 'XCEEDANCE', GETDATE()

DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='WIN_SERVICE_TIMER'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'WIN_SERVICE_TIMER','2','WINDOWS SERVICE TIMER', 1, 'XCEEDANCE', GETDATE()


DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='TCIP_DEFAULT_PORT'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'TCIP_DEFAULT_PORT','110','POP3 DETAILS', 1, 'XCEEDANCE', GETDATE()


DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='RETRIEVE_USING_TCIP_CLIENT'  
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'RETRIEVE_USING_TCIP_CLIENT','Y','POP3 DETAILS', 1, 'XCEEDANCE', GETDATE()



DELETE FROM [dbo].CONFIGURATION WHERE [KEY]='SMS_Link'
INSERT INTO [dbo].CONFIGURATION ([KEY], [VALUE], [DESCRIPTION],[ISACTIVE], CREATED_BY,CREATED_DATE)
SELECT 'SMS_Link','http://210.210.26.40/sendsms/push_sms.php?user=soa_new&pwd=soa_new&from=SOA&to=$MOBILE$&msg=$MSG$','', 1, 'XCEEDANCE', GETDATE()


DELETE FROM CONFIGURATION WHERE [KEY]='MAIL ON UDERWRITER REQUEST'
INSERT INTO CONFIGURATION([KEY],[VALUE],[DESCRIPTION],ISACTIVE,CREATED_BY, CREATED_DATE)
SELECT 'MAIL ON UDERWRITER REQUEST','','MailFormat',1,'XCEEDANCE',GETDATE()

DELETE FROM CONFIGURATION WHERE [KEY]='MAIL ON UDERWRITER REQUEST(ADMIN)'
INSERT INTO CONFIGURATION([KEY],[VALUE],[DESCRIPTION],ISACTIVE,CREATED_BY, CREATED_DATE)
SELECT 'MAIL ON UDERWRITER REQUEST(ADMIN)','','MailFormat',1,'XCEEDANCE',GETDATE()
 

DELETE FROM CONFIGURATION WHERE [KEY]='RECEPIENTS EMAIL ID FOR ON EACH UNDERWRITER REQUEST'
INSERT INTO CONFIGURATION([KEY],[VALUE],[DESCRIPTION],ISACTIVE,CREATED_BY, CREATED_DATE)
SELECT 'RECEPIENTS EMAIL ID FOR ON EACH UNDERWRITER REQUEST','GUNJESH_ranjan@yahoo.com','EMAIL RECEPIENTS GROUP',1,'XCEEDANCE',GETDATE()


 
 /*************************************************NEW SCRIPT BLOCK ENDS*********************************************/


 TRUNCATE TABLE ROLE_MASTER
 GO

 INSERT INTO ROLE_MASTER
 SELECT 'Underwriting'	,'Underwriting'	,'ACTIVE'
UNION ALL
SELECT 'Underwriting Technician',	'Underwriting Technician',	'ACTIVE'
UNION ALL
SELECT 'Cat Modeler', 'Cat Modeler'	,'ACTIVE'
UNION ALL
SELECT 'Underwriting Officer',	'Underwriting Officer'	,'ACTIVE'
UNION ALL
SELECT 'Cat Manager',	'Cat Manager'	,'ACTIVE'

GO



 
 menus


 drop table [MENU_MASTER]
  
CREATE TABLE [dbo].[MENU_MASTER](
	[MENU_ID]			[INT] IDENTITY(1,1),
	[MENU_NAME]			[VARCHAR](100) NOT NULL,
	[MENU_PARENT_ID]	[INT] NULL,
	[MENU_URL]			[VARCHAR](50) NULL,
	[ICON_IMAGE]		VARCHAR(500),
	[ICON_HEIGHT]		INT,
	[ICON_WIDTH]		INT,
	[ICON_TITLE]		VARCHAR(100),
	[MENU_ORDER]		INT,
	[STATUS]			INT,
	CREATED_BY			VARCHAR(15), 
	CREATED_DATE		DATETIME,
	MODIFIED_BY			VARCHAR(15), 
	MODIFIED_DATE		DATETIME,

 CONSTRAINT [PK_MENU] PRIMARY KEY CLUSTERED 
(
	[MENU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
 
 INSERT INTO [MENU_MASTER](
	 [MENU_NAME],	 [MENU_PARENT_ID], [MENU_URL], [ICON_IMAGE] ,[MENU_ORDER],		
	 [STATUS],	 CREATED_BY,CREATED_DATE )	
SELECT 'Administration',NULL,NULL,NULL,NULL,0,'XCEEDANCE',GETDATE()		
UNION ALL
SELECT 'Configuration',NULL,NULL,NULL,NULL,0,'XCEEDANCE',GETDATE()	
UNION ALL
SELECT 'Manage Request',NULL,NULL,NULL,NULL,0,'XCEEDANCE',GETDATE()	


SELECT * FROM [MENU_MASTER]

 INSERT INTO [MENU_MASTER](
	 [MENU_NAME],	 [MENU_PARENT_ID], [MENU_URL], [ICON_IMAGE], [ICON_HEIGHT], [ICON_WIDTH], [ICON_TITLE],[MENU_ORDER],		
	 [STATUS],	 CREATED_BY,CREATED_DATE )	
 
SELECT 'User Management',1,'ManageUsers.aspx','images/shortcut/SignupUsers.jpg',80,80,'Click me to Sign Up Users', 1,0,'XCEEDANCE',GETDATE()	
UNION ALL
SELECT 'Role Privileges',1,'RolePrivileges.aspx','images/shortcut/RolePrivileges.jpg',80,80,'Click me to set Role Privileges',2,0,'XCEEDANCE',GETDATE()	
UNION ALL
SELECT 'User Privileges',1,'UserPrivileges.aspx','images/shortcut/Permissions.jpg',80,80,'Click me to User Privileges',3,0,'XCEEDANCE',GETDATE()	


 INSERT INTO [MENU_MASTER](
	 [MENU_NAME],	 [MENU_PARENT_ID], [MENU_URL], [ICON_IMAGE],[ICON_HEIGHT], [ICON_WIDTH], [ICON_TITLE],[MENU_ORDER],		
	 [STATUS],	 CREATED_BY,CREATED_DATE )	
 
SELECT 'Application Configuration',2,'ConfigMgr.aspx','images/shortcut/Configuration.jpg',80,80,'Click me for Configuration',1,0,'XCEEDANCE',GETDATE()	
UNION ALL
SELECT 'Mail Format Configuration',2,'ConfigMails.aspx','images/shortcut/MailFormats.jpg',80,80,'Click me for open Mail Formats',2,0,'XCEEDANCE',GETDATE()	
UNION ALL
SELECT 'Email Group Recepients Configuration',2,'ConfigureRecepients.aspx','images/shortcut/MailRecepients.png',80,80,'Configure Mail Recepients',3,0,'XCEEDANCE',GETDATE()	

 
 INSERT INTO [MENU_MASTER](
	 [MENU_NAME],	 [MENU_PARENT_ID], [MENU_URL], [ICON_IMAGE],[ICON_HEIGHT], [ICON_WIDTH], [ICON_TITLE],[MENU_ORDER],		
	 [STATUS],	 CREATED_BY,CREATED_DATE )	
 
SELECT 'Application Configuration',3,'MngRequests.aspx','images/shortcut/Submit Request.jpg',80,80,'Manage CRM Request',1,0,'XCEEDANCE',GETDATE()	
 

  INSERT INTO [MENU_MASTER](
	 [MENU_NAME],	 [MENU_PARENT_ID], [MENU_URL], [ICON_IMAGE],[ICON_HEIGHT], [ICON_WIDTH], [ICON_TITLE],[MENU_ORDER],		
	 [STATUS],	 CREATED_BY,CREATED_DATE )	
 
SELECT 'Raise Query',3,'ReponseUWRequests.aspx','../Common/images/shortcut/manage.png',80,80,'Raise Query',4,0,'XCEEDANCE',GETDATE()	
 

 SELECT * FROM [MENU_MASTER]
 select * from role_master






 TRUNCATE TABLE PORTAL_LOGIN_MASTER


 SELECT * FROM USER_MASTER
 SELECT * FROM PORTAL_LOGIN_MASTER

 update user_master set emailid='vivek.dhiman@xceedance.com'
 INSERT INTO USER_MASTER(USER_ID, USER_NAME,ROLE_ID, MOBILE_NO,STATUS, FIRST_TIME_LOGGEDIN, CREATED_BY, CREATED_DATE)
 SELECT 'admin', 'Administrator',1,'',0, 'N','xceedance', getdate()


 insert into PORTAL_LOGIN_MASTER

 select 'admin','vivek.dhiman@xceedance.com','admin@123','Portal','xceedance',getdate(),null,null, 'Y','N'

truncate table [User_Menu_Privilege]