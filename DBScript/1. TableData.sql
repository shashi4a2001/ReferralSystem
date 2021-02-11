GO
If Exists (Select 1 From ClientTypeMaster Where ClientTypeCode='101' and ClientTypeName='Master Agent')
Begin
	Truncate Table ClientTypeMaster
End
If Not Exists(Select 1 From ClientTypeMaster )
Begin	
	Insert Into ClientTypeMaster
	(ClientTypeLevel,ClientTypeCode,ClientTypeName,
	 CanMakeSuperAdmin,CanMakeNationalHead,CanMakeRegionalHead,
	 CanMakeStateFranchisee,CanMakeDistrictFranchisee,CanMakeIndividualAgent,CanMakeIndividual,
	 CreatedBy,CreatedDate)
	 Select 0,'100','Super Admin',1,1,1,1,1,1,1,'sys',getdate()
	 Union All
	 Select 1,'101','National Head',0,0,1,1,1,1,1,'sys',getdate()
	 Union All
	 Select 2,'102','Regional Head',0,0,0,1,1,1,1,'sys',getdate()
	 Union All
	 Select 3,'103','State Franchisee',0,0,0,0,1,1,1,'sys',getdate()
	 Union All
	 Select 4,'104','District Franchisee',0,0,0,0,0,1,1,'sys',getdate()
	 Union All
	 Select 5,'105','Individual Agent',0,0,0,0,0,1,1,'sys',getdate()
	 Union All
	 Select 5,'106','Individual',0,0,0,0,0,1,1,'sys',getdate()
End

 
If Not Exists (Select 1 From ClientMaster Where MobileNo ='9999999999')
Begin
	Insert Into ClientMaster(MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,
	EmailId,LandlineNo,Address,LoginId,
	BankName,ClientNameAsPerBank,AccountNo,IFSCCode,
	SelfReferralCode,ReferredReferralCode,ReferralAmount,ReferredReferralRevenue,
	CreatedBy,CreatedDate)
	values ('9999999999','1001','dr101','Deepak Rawat',100,'Deepak Rawat',
	'abc@bcd.com','','','Deepak19',
	'ICICI Bank','Deepak Rawat','013232323','ICICI011888',
	'100_1','',0,0,
	'sys',getdate()
	)
End
 
--GO
--If Not Exists (Select 1 From SMTPServerDetails)
--Begin
--	Insert Into SMTPServerDetails(HostName,PortNo,ReqAuth,EnableSSL,UserId,UserPWD,
--	SenderEmailID,DisplayName,Status,CreatedBy,CreationDate,ChangedRemarks)
--	values ('smtp.gmail.com','587','Yes','Yes','shashi4a2001@gmail.com','goo461983',
--	'shashi4a2001@gmail.com','shashi kant kumar','Active','sys',getdate(),'')
--End

If Not Exists (Select 1 From EmailTemplateMaster Where TemplateType ='PasswordChanged') 
Begin
	Insert Into EmailTemplateMaster (TemplateType,SubjectLine,TemplateText)
	values ('PasswordChanged',
	'Your password has been changed...',
	'<table><tr><td colspan=''4''> Dear ,</td></tr><tr><td colspan=''4''></td></tr><tr><td>Your Password has been changed successfully..   </td><td></td><td colspan=''2''></td></tr><tr><td colspan=''4''></td></tr><tr><td colspan=''4''></td></tr><tr><td colspan=''4''></td></tr><tr><td colspan=''4''>Happy Investing!</td></tr><tr><td colspan=''4''>Regards,<span style=''mso-fareast-font-family: ''Times New Roman''''><o:p></o:p></span></td></tr><tr><td colspan=''4''>Invest 19</td></tr></table>')
End

If Not Exists (Select 1 From EmailTemplateMaster Where TemplateType ='LoginCreation') 
Begin
	Insert Into EmailTemplateMaster (TemplateType,SubjectLine,TemplateText)
	values ('LoginCreation',
	'Login Detail...',
	'<table><tr><td colspan=''4''> Dear ,</td></tr>
	<tr><td colspan=''4''></td></tr>
	<tr>
		<td colspan=''4''>Your login has been created successfully..   </td>		
	</tr>
	<tr> <td>Login Id: </td><td>(LoginId)</td> <td colspan=''2''></td></tr>
	<tr> <td>Password: </td><td>(Password)</td> <td colspan=''2''></td></tr>
	<tr><td colspan=''4''></td></tr>
	<tr><td colspan=''4''>Happy Investing!</td></tr>
	<tr><td colspan=''4''>Regards,<span style=''mso-fareast-font-family: ''Times New Roman''''><o:p></o:p></span></td></tr>
	<tr><td colspan=''4''>Invest 19</td></tr>
	</table>')
End

If Not Exists (Select 1 From EmailTemplateMaster Where TemplateType ='ForgotPassword') 
Begin
	Insert Into EmailTemplateMaster (TemplateType,SubjectLine,TemplateText)
	values ('ForgotPassword',
	'Password Recovery...',
	'<table><tr><td colspan=''4''> Dear ,</td></tr>
	<tr><td colspan=''4''></td></tr>
	<tr>
		<td colspan=''4''>Your login detail is..   </td>		
	</tr>
	<tr> <td>Login Id: </td><td>(LoginId)</td> <td colspan=''2''></td></tr>
	<tr> <td>Password: </td><td>(Password)</td> <td colspan=''2''></td></tr>
	<tr><td colspan=''4''></td></tr>
	<tr><td colspan=''4''>Happy Investing!</td></tr>
	<tr><td colspan=''4''>Regards,<span style=''mso-fareast-font-family: ''Times New Roman''''><o:p></o:p></span></td></tr>
	<tr><td colspan=''4''>Invest 19</td></tr>
	</table>')
End


IF (EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'SharingPercentage' AND Object_ID = Object_ID(N'ClientMaster')))
BEGIN
	EXEC sp_RENAME 'ClientMaster.SharingPercentage' , 'ReferralSharingPercentage', 'COLUMN'
END
GO
IF (NOT EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'RevenueSharingPercentage' AND Object_ID = Object_ID(N'ClientMaster')))
BEGIN
  Alter Table ClientMaster ADD RevenueSharingPercentage  Numeric(18,2)
END
GO
IF (EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'LoginPassword' AND Object_ID = Object_ID(N'ClientMaster')))
BEGIN
	Alter Table ClientMaster Drop Column LoginPassword
END
GO
IF (Not EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'CreatedByInSystem' AND Object_ID = Object_ID(N'ClientMaster')))
BEGIN
	Alter Table ClientMaster Add	CreatedByInSystem varchar(100),
									CreatedDateInSystem DateTime
END

--update EmailTemplateMaster set TemplateText='<table><tr><td> Dear ,</td></tr><tr><td></td></tr><tr>    <td>Your password recovery link is : ..   </td>     </tr><tr> <td>http://localhost:11510/Operation/PasswordRecovery.aspx?hlink=(ParamPasswordRecovery)</td></tr><tr><td>&nbsp;</td></tr><tr><td>Happy Investing!</td></tr><tr><td>Regards,<span style=''mso-fareast-font-family: Times New Roman''><o:p></o:p></span></td></tr><tr><td>Invest 19</td></tr>   </table>' where templatetype='ForgotPassword'