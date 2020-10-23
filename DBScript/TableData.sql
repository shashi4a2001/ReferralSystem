GO

If Not Exists(Select 1 From ClientTypeMaster )
Begin	
	Insert Into ClientTypeMaster
	(ClientTypeLevel,ClientTypeCode,ClientTypeName,
	 CanMakeSuperAdmin,CanMakeMasterAgent,CanMakeSubAgent,
	 CanMakeFranchisee,CanMakeFranchiseeAgent,CanMakeIndividual,
	 CreatedBy,CreatedDate)
	 Select 0,'100','Super Admin',1,1,1,1,1,1,'sys',getdate()
	 Union All
	 Select 1,'101','Master Agent',0,0,1,1,1,1,'sys',getdate()
	 Union All
	 Select 2,'102','Sub Agent',0,0,0,1,1,1,'sys',getdate()
	 Union All
	 Select 3,'103','Franchisee',0,0,0,0,1,1,'sys',getdate()
	 Union All
	 Select 4,'104','Franchisee Agent',0,0,0,0,0,1,'sys',getdate()
	 Union All
	 Select 5,'105','Individual',0,0,0,0,0,1,'sys',getdate()
End

 
If Not Exists (Select 1 From ClientMaster Where MobileNo ='9999999999')
Begin
	Insert Into ClientMaster(MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,
	EmailId,LandlineNo,Address,LoginId,LoginPassword,
	BankName,ClientNameAsPerBank,AccountNo,IFSCCode,
	SharingPercentage,SelfReferralCode,ReferredReferralCode,ReferralAmount,ReferredReferralRevenue,
	CreatedBy,CreatedDate)
	values ('9999999999','1001','sa101','shashi super',100,'shashi kant kumar',
	'abc@bcd.com','','','sa101_01','9999999999',
	'ICICI Bank','shashi kant kumar','013232323','ICICI011888',
	0,'100_1','',0,0,
	'sys',getdate()
	)
End

If Not Exists (Select 1 From ClientMaster Where MobileNo ='9999999998')
Begin
	Insert Into ClientMaster(MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,
	EmailId,LandlineNo,Address,LoginId,LoginPassword,
	BankName,ClientNameAsPerBank,AccountNo,IFSCCode,
	SharingPercentage,SelfReferralCode,ReferredReferralCode,ReferralAmount,ReferredReferralRevenue,
	CreatedBy,CreatedDate)
	values ('9999999998','1002','ma101','master agent 1',101,'master agent 1',
	'bcd@bcd.com','','','ma101_01','9999999998',
	'ICICI Bank','master agent 1','013232567','ICICI011888',
	80,'101_1','100_1',100,80,
	'sys',getdate()
	)
End

If Not Exists (Select 1 From ClientMaster Where MobileNo ='9999999997')
Begin
	Insert Into ClientMaster(MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,
	EmailId,LandlineNo,Address,LoginId,LoginPassword,
	BankName,ClientNameAsPerBank,AccountNo,IFSCCode,
	SharingPercentage,SelfReferralCode,ReferredReferralCode,ReferralAmount,ReferredReferralRevenue,
	CreatedBy,CreatedDate)
	values ('9999999997','1003','ma102','master agent 2',101,'master agent 2',
	'bce@bcd.com','','','ma101_02','9999999997',
	'ICICI Bank','master agent 2','013232569','ICICI011888',
	80,'101_2','100_1',100,80,
	'sys',getdate()
	)
End

If Not Exists (Select 1 From ClientMaster Where MobileNo ='8999999997')
Begin
	Insert Into ClientMaster(MobileNo,ClientRefId,ClientCode,ClientName,ClientTypeCode,ContactPerson,
	EmailId,LandlineNo,Address,LoginId,LoginPassword,
	BankName,ClientNameAsPerBank,AccountNo,IFSCCode,
	SharingPercentage,SelfReferralCode,ReferredReferralCode,ReferralAmount,ReferredReferralRevenue,
	CreatedBy,CreatedDate)
	values ('8999999997','1004','sag101','sub agent 1',102,'sub agent 1',
	'bcf@bcd.com','','','sag101_01','8999999997',
	'ICICI Bank','sub agent 1','0132325678','ICICI011888',
	70,'102_1','101_1',100,70,
	'sys',getdate()
	)
End