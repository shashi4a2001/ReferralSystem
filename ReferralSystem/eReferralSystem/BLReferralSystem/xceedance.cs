using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;

namespace BLCMR
{
    public class Objxceedance
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _EMP_CODE;
        public string EMP_CODE
        {
            get { return _EMP_CODE; }
            set { _EMP_CODE = value; }
        }

        private string _Emp_Name;
        public string Emp_Name
        {
            get { return _Emp_Name; }
            set { _Emp_Name = value; }
        }

        private string _FIRST_NAME;
        public string FIRST_NAME
        {
            get { return _FIRST_NAME; }
            set { _FIRST_NAME = value; }
        }

        private string _LAST_NAME;
        public string LAST_NAME
        {
            get { return _LAST_NAME; }
            set { _LAST_NAME = value; }
        }

        private string _FATHER_NAME;
        public string FATHER_NAME
        {
            get { return _FATHER_NAME; }
            set { _FATHER_NAME = value; }
        }

        private string _MOTHER_NAME;
        public string MOTHER_NAME
        {
            get { return _MOTHER_NAME; }
            set { _MOTHER_NAME = value; }
        }

        private string _JOINING_DATE;
        public string JOINING_DATE
        {
            get { return _JOINING_DATE; }
            set { _JOINING_DATE = value; }
        }
    }

    public class ObjUser 
    {
        private int _LogId;
        public int LogId
        {
            get { return _LogId; }
            set { _LogId = value; }
        }
        private int _schoolID;
        public int SchoolID
        {
            get { return _schoolID; }
            set { _schoolID = value; }
        }
        private int _Uid;
        public int UId
        {
            get { return _Uid; }
            set { _Uid = value; }
        }

        private string _UserId;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private int _UserGroupId;
        public int UserGroupId
        {
            get { return _UserGroupId; }
            set { _UserGroupId = value; }
        }

        private string _EmailId;
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        private string _MobileNo;
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }


        private string _UserRole;
        public string UserRole
        {
            get { return _UserRole; }
            set { _UserRole = value; }
        }


        private int _UserRoleId;
        public int UserRoleId
        {
            get { return _UserRoleId; }
            set { _UserRoleId = value; }
        }
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _FirstTimeLoggedIn;
        public string FirstTimeLoggedIn
        {
            get { return _FirstTimeLoggedIn; }
            set { _FirstTimeLoggedIn = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private int _LoggedInID;
        public int LoggedInID
        {
            get { return _LoggedInID; }
            set { _LoggedInID = value; }
        }

        private string _LastLogInTime;
        public string LastLogInTime
        {
            get { return _LastLogInTime; }
            set { _LastLogInTime = value; }
        }

        private string _LastPasswordChangeTime;
        public string LastPasswordChangeTime
        {
            get { return _LastPasswordChangeTime; }
            set { _LastPasswordChangeTime = value; }
        }

        private string _State;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private string _LogInMachine;
        public string LogInMachine
        {
            get { return _LogInMachine; }
            set { _LogInMachine = value; }
        }

        private string _AccountLock;
        public string AccountLock
        {
            get { return _AccountLock; }
            set { _AccountLock = value; }
        }

        private string _UserLogInType;
        public string UserLogInType
        {
            get { return _UserLogInType; }
            set { _UserLogInType = value; }
        }


        private string _WorkingLocation;
        public string WorkingLocation
        {
            get { return _WorkingLocation; }
            set { _WorkingLocation = value; }
        }


        private string _WorkingOrganisation;
        public string WorkingOrganisation
        {
            get { return _WorkingOrganisation; }
            set { _WorkingOrganisation = value; }
        }


        private string _ConnectionString;
        public string ConnectionString
        {
            get { return  _ConnectionString ; }
            set { _ConnectionString = value; }
        }

        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        private string _SubmissionBranch;
        public string SubmissionBranch
        {
            get { return _SubmissionBranch; }
            set { _SubmissionBranch = value; }
        }

        private string _Region;
        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }


        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }


    }

    public class ObjPortalUser : clsCommon
    {
        private int _schoolID;
        public int SchoolID
        {
            get { return _schoolID; }
            set { _schoolID = value; }
        }
        private int _LogId;
        public int LogId
        {
            get { return _LogId; }
            set { _LogId = value; }
        }

        private int _Uid;
        public int UId
        {
            get { return _Uid; }
            set { _Uid = value; }
        }

        private string _UserId;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserRole;
        public string UserRole
        {
            get { return _UserRole; }
            set { _UserRole = value; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        private string _EmailId;
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }

        private string _MobileNo;
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _FirstTimeLoggedIn;
        public string FirstTimeLoggedIn
        {
            get { return _FirstTimeLoggedIn; }
            set { _FirstTimeLoggedIn = value; }
        }

        private string _SOVFileExtns;
        public string  SOVFileExtns
        {
            get { return _SOVFileExtns; }
            set { _SOVFileExtns = value; }
        }

        private int _SOVFileSize;
        public int SOVFileSize
        {
            get { return _SOVFileSize; }
            set { _SOVFileSize = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private int _LoggedInID;
        public int LoggedInID
        {
            get { return _LoggedInID; }
            set { _LoggedInID = value; }
        }

        private string _LastLogInTime;
        public string LastLogInTime
        {
            get { return _LastLogInTime; }
            set { _LastLogInTime = value; }
        }

        private string _LastPasswordChangeTime;
        public string LastPasswordChangeTime
        {
            get { return _LastPasswordChangeTime; }
            set { _LastPasswordChangeTime = value; }
        }

        private string _State;
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private string _LogInMachine;
        public string LogInMachine
        {
            get { return _LogInMachine; }
            set { _LogInMachine = value; }
        }

        private string _AccountLock;
        public string AccountLock
        {
            get { return _AccountLock; }
            set { _AccountLock = value; }
        }

        private string _UserLogInType;
        public string UserLogInType
        {
            get { return _UserLogInType; }
            set { _UserLogInType = value; }
        }


        private string _WorkingLocation;
        public string WorkingLocation
        {
            get { return _WorkingLocation; }
            set { _WorkingLocation = value; }
        }


        private string _ConnectionString;
        public string ConnectionString
        {
            get {

                if (_ConnectionString == null)
                {
                    return ""; 
                }

               return  DecryptData(_ConnectionString); 
            }
            set { _ConnectionString = value; }
        }
 
        private string  _IPAddress ;
        public string  IPAddress
        {
            get { return  DecryptData(_IPAddress) ; }
            set { _IPAddress = value; }
        }

        //For SMTP/POP3 Information

        private string _SMTPPort;
        public string SMTPPort
        {
            get { return _SMTPPort; }
            set { _SMTPPort = value; }
        }

        private string _SMTPUSerID;
        public string SMTPUSerID
        {
            get { return _SMTPUSerID; }
            set { _SMTPUSerID = value; }
        }

        private string _SMTPServer;
        public string SMTPServer
        {
            get { return _SMTPServer; }
            set { _SMTPServer = value; }
        }

        private string _SMTPPassword;
        public string SMTPPassword
        {
            get { return DecryptData(_SMTPPassword); }
            set { _SMTPPassword = value; }
        }

        private string _SMTPssl;
        public string  SMTPSSL
        {
            get { return _SMTPssl; }
            set { _SMTPssl = value; }
        }

        //PO3
        private string _POPPort;
        public string POPPort
        {
            get { return _POPPort; }
            set { _POPPort = value; }
        }

        private string _POPUSerID;
        public string POPUSerID
        {
            get { return _POPUSerID; }
            set { _POPUSerID = value; }
        }

        private string _POPServer;
        public string POPServer
        {
            get { return _POPServer; }
            set { _POPServer = value; }
        }

        private string _POPPassword;
        public string POPPassword
        {
            get { return DecryptData(_POPPassword); }
            set { _POPPassword = value; }
        }

        private string _POPByDefaultPort;
        public string POPByDefaultPort
        {
            get { return _POPByDefaultPort; }
            set { _POPByDefaultPort = value; }
        }

        private string _POPDefaultPort;
        public string POPDefaultPort
        {
            get { return _POPDefaultPort; }
            set { _POPDefaultPort = value; }
        }

        private string _POPssl;
        public string POPSSL
        {
            get { return _POPssl; }
            set { _POPssl = value; }
        }
        private string _ActingUW;
        public string ActingUW
        {
            get { return _ActingUW; }
            set { _ActingUW = value; }
        }

        private int _ActingUW_ID;
        public int ActingUW_ID
        {
            get { return _ActingUW_ID; }
            set { _ActingUW_ID = value; }
        }

        private string _ISUW;
        public string IsUW
        {
            get { return _ISUW; }
            set { _ISUW = value; }
        }

        private string _ReqDashobard;
        public string ReqDashobard
        {
            get { return _ReqDashobard; }
            set { _ReqDashobard = value; }
        }

        private string _LandingPage;
        public string LandingPage
        {
            get { return _LandingPage; }
            set { _LandingPage = value; }
        }

        private string _ReqChangePassword;
        public string ReqChangePassword
        {
            get { return _ReqChangePassword; }
            set { _ReqChangePassword = value; }
        }

        private string _SelectUW;
        public string SelectUW
        {
            get { return _SelectUW; }
            set { _SelectUW = value; }
        }

        private string _RestrictedInputChar;
        public string RestrictedInputChar
        {
            get { return _RestrictedInputChar; }
            set { _RestrictedInputChar = value; }
        }

        private string _CaptchaCode;
        public string CaptchaCode
        {
            get { return _CaptchaCode; }
            set { _CaptchaCode = value; }
        }

        private string _CaptchaValidity;
        public string  CaptchaValidity
        {
            get { return _CaptchaValidity; }
            set { _CaptchaValidity = value; }
        }


        //Organizational detail
        private string _Orig_Code;
        public string Orig_Code
        {
            get { return _Orig_Code; }
            set { _Orig_Code = value; }
        }

        private string _Orig_Name;
        public string Orig_Name
        {
            get { return _Orig_Name; }
            set { _Orig_Name = value; }
        }

        private string _Orig_Address1;
        public string Orig_Address1
        {
            get { return _Orig_Address1; }
            set { _Orig_Address1 = value; }
        }


        private string _Orig_Address2;
        public string Orig_Address2
        {
            get { return _Orig_Address2; }
            set { _Orig_Address2 = value; }
        }


        private string _Orig_Address3;
        public string Orig_Address3
        {
            get { return _Orig_Address3; }
            set { _Orig_Address3 = value; }
        }

        private string _Orig_Phone1;
        public string Orig_Phone1
        {
            get { return _Orig_Phone1; }
            set { _Orig_Phone1 = value; }
        }
        private string _Orig_Phone2;
        public string Orig_Phone2
        {
            get { return _Orig_Phone2; }
            set { _Orig_Phone2 = value; }
        }

        private string _Orig_Url;
        public string Orig_Url
        {
            get { return _Orig_Url; }
            set { _Orig_Url = value; }
        }

        private string _Orig_Email1;
        public string Orig_Email1
        {
            get { return _Orig_Email1; }
            set { _Orig_Email1 = value; }
        }

        private string _Orig_Email2;
        public string Orig_Email2
        {
            get { return _Orig_Email2; }
            set { _Orig_Email2 = value; }
        }


        private string _Orig_Logo1;
        public string Orig_Logo1
        {
            get { return _Orig_Logo1; }
            set { _Orig_Logo1 = value; }
        }

        private string _Orig_Logo2;
        public string Orig_Logo2
        {
            get { return _Orig_Logo2; }
            set { _Orig_Logo2 = value; }
        }

        private string _Orig_Year_Reserved;
        public string Orig_Year_Reserved
        {
            get { return _Orig_Year_Reserved; }
            set { _Orig_Year_Reserved = value; }
        }

        private string _GSTNO;
        public string Orig_GSTNO
        {
            get { return _GSTNO; }
            set { _GSTNO = value; }
        }

        private double _CGSTPer;
        public double CGSTPer
        {
            get { return _CGSTPer; }
            set { _CGSTPer = value; }
        }

        private double _SGSTPer;
        public double SGSTPer
        {
            get { return _SGSTPer; }
            set { _SGSTPer = value; }
        }

        private double _IGSTPer;
        public double IGSTPer
        {
            get { return _IGSTPer; }
            set { _IGSTPer = value; }
        }



    }

    public class ObjSendEmail 
    {
        clsCommon objCommon = new clsCommon();
        
        private string _From;
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }

        private string _MailType;
        public string MailType
        {
            get { return _MailType; }
            set { _MailType = value; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _To;
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }

        private string _CC;
        public string CC
        {
            get { return _CC; }
            set { _CC = value; }
        }

        private string _BCC;
        public string BCC
        {
            get { return _BCC; }
            set { _BCC = value; }
        }

        private string _Subject;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        private string _MailBody;
        public string MailBody
        {
            get { return _MailBody; }
            set { _MailBody = value; }
        }
        private string _BodyFormat;
        public string BodyFormat
        {
            get { return _BodyFormat; }
            set { _BodyFormat = value; }
        }

        private string _SMTPHost;
        public string SMTPHost
        {
            get { return _SMTPHost; }
            set { _SMTPHost = value; }
        }

        private int _SMTPPort;
        public int SMTPPort
        {
            get { return _SMTPPort; }
            set { _SMTPPort = value; }
        }

        private string _UserId;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _Password;
        public string Password
        {
            get { return objCommon.DecryptData ( _Password); }
            set { _Password =  objCommon.EncryptData (value); }
        }

        private bool _SMTPSSL;
        public bool SMTPSSL
        {
            get { return _SMTPSSL; }
            set { _SMTPSSL = value; }
        }

        private string _MobileNo;
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        private string _Remarks;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
 
        private string _Attachment;
        public string Attachment
        {
            get { return _Attachment; }
            set { _Attachment = value; }
        }
 
        private string _Conn;
        public string ConnectionString
        {
            get { return  _Conn ; }
            set { _Conn = value; }
        }

        private string _RequestID;
        public string RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }


        private string _IsUnderwriter;
        public string IsUnderwriter
        {
            get { return _IsUnderwriter; }
            set { _IsUnderwriter = value; }
        }


    }

    public class ObjUserMenuPrevilege
    {

        private int _RoleId;
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }



        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private int _MenuId;
        public int MenuId
        {
            get { return _MenuId; }
            set { _MenuId = value; }
        }

        private string _InsertQuery;
        public string InsertQuery
        {
            get { return _InsertQuery; }
            set { _InsertQuery = value; }
        }

        private string _DeleteQuery;
        public string DeleteQuery
        {
            get { return _DeleteQuery; }
            set { _DeleteQuery = value; }
        }


        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
    }

    public class ObjUserMenuAccessRights
    {

        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _MenuGroupName;
        public string MenuGroupName
        {
            get { return _MenuGroupName; }
            set { _MenuGroupName = value; }
        }
    }

    public class objUnderWriter
    {
        private int _AssignmentID;
        public int AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _AccountDesc;
        public string AccountDesc
        {
            get { return _AccountDesc; }
            set { _AccountDesc = value; }
        }

        private string _AccountName;
        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }

        private string _RequestType;
        public string RequestType
        {
            get { return _RequestType; }
            set { _RequestType = value; }
        }

        private string _MachineIP;
        public string MachineIP
        {
            get { return _MachineIP; }
            set { _MachineIP = value; }
        }

        private string _NoofLocations;
        public string NoofLocations
        {
            get { return _NoofLocations; }
            set { _NoofLocations = value; }
        }

        private string _DueDate;
        public string DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }

        private string _SOVName;
        public string SOVName
        {
            get { return _SOVName; }
            set { _SOVName = value; }
        }

        private string _TIV;
        public string TIV
        {
            get { return _TIV; }
            set { _TIV = value; }
        }


        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _UnderWriter;
        public string UnderWriter
        {
            get { return _UnderWriter; }
            set { _UnderWriter = value; }
        }

        private string _RemodelReason;
        public string RemodelReason
        {
            get { return _RemodelReason; }
            set { _RemodelReason = value; }
        }

        private string _UnderWriterid;
        public string UnderWriterID
        {
            get { return _UnderWriterid; }
            set { _UnderWriterid = value; }
        }

        private int _Year;
        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        private string _MISReporttype;
        public string MISReporttype
        {
            get { return _MISReporttype; }
            set { _MISReporttype = value; }
        }

        private string _Comment;
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        private string _SubmitDate;
        public string SubmitDate
        {
            get { return _SubmitDate; }
            set { _SubmitDate = value; }
        }

        private string _searchText;
        public string searchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }

        private int _RequetID;
        public int RequetID
        {
            get { return _RequetID; }
            set { _RequetID = value; }
        }

        private int _ReGnO;
        public int  ReGnO
        {
            get { return _ReGnO; }
            set { _ReGnO = value; }
        }

        private string _MISDateFrom;
        public string MISDateFrom
        {
            get { return _MISDateFrom; }
            set { _MISDateFrom = value; }
        }

        private string _MISDateTo;

        public string MISDateTo
        {
            get { return _MISDateTo; }
            set { _MISDateTo = value; }
        }
        private string _UWEmailId;

        public string  UWEmailId
        {
            get { return _UWEmailId; }
            set { _UWEmailId = value; }
        }

    }

    public class objOlympiad
    {
        private int _signupID;
        public int SignupId
        {
            get { return _signupID; }
            set { _signupID = value; }
        }

        private string _ApplicantName;
        public string ApplicantName
        {
            get { return _ApplicantName; }
            set { _ApplicantName = value; }
        }

        private string _PinCode;
        public string PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Occupation;
        public string Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }

        private string _mobileNo;
        public string MobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; }
        }

        private string _EmailID;
        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }
        private string _FatherName;
        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }
        private string _MotherName;
        public string MotherName
        {
            get { return _MotherName; }
            set { _MotherName = value; }
        }
        private string _Dob;
        public string DOB
        {
            get { return _Dob; }
            set { _Dob = value; }
        }
        private string _MartialStatus;
        public string MartialStatus
        {
            get { return _MartialStatus; }
            set { _MartialStatus = value; }
        }
        private string _Nationality;
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
        private int _stateID;
        public int StateID
        {
            get { return _stateID; }
            set { _stateID = value; }
        }

        private int _courseID;
        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }

        private string _machineIP;
        public string MachineIP
        {
            get { return _machineIP; }
            set { _machineIP = value; }
        }

        private int _schoolID;
        public int SchoolID
        {
            get { return _schoolID; }
            set { _schoolID = value; }
        }

        private string _principlaName;
        public string PrincipalName
        {
            get { return _principlaName; }
            set { _principlaName = value; }
        }

        private string _schoolName;
        public string SchoolName
        {
            get { return _schoolName; }
            set { _schoolName = value; }
        }

        private string _board;
        public string Board
        {
            get { return _board; }
            set { _board = value; }
        }

        private string _schoolAddress;
        public string SchoolAdress
        {
            get { return _schoolAddress; }
            set { _schoolAddress = value; }
        }

        private string _cityVillageTown;
        public string CityVillageTown
        {
            get { return _cityVillageTown; }
            set { _cityVillageTown = value; }
        }

        private string _district;
        public string District
        {
            get { return _district; }
            set { _district = value; }
        }

        private int _Pin;
        public int PIN
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        private string _contactNo;
        public string ContactNo
        {
            get { return _contactNo; }
            set { _contactNo = value; }
        }
 
        private string _webUrl;
        public string SchoolURL
        {
            get { return _webUrl; }
            set { _webUrl = value; }
        }

        private string _userID;
        public string USerID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _sts;
        public string Status
        {
            get { return _sts; }
            set { _sts = value; }
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }


        private string _searchText;
        public string searchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }

        private string _strPassword;
        public string Password
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }
    }

    public class ObjMaster
    {
        private int _LogId;
        public int LogId
        {
            get { return _LogId; }
            set { _LogId = value; }
        }

        private int _CourseId;
        public int  CourseId
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }

        private int _SegmentId;
        public int SegmentId
        {
            get { return _SegmentId; }
            set { _SegmentId = value; }
        }

        private string _CourseName;
        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }

        private string _CourseCode;
        public string CourseCode
        {
            get { return _CourseCode; }
            set { _CourseCode = value; }
        }
 
        private int _SemesterCount;
        public int  SemesterCount
        {
            get { return _SemesterCount; }
            set { _SemesterCount = value; }
        }
     
        private int _Status;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
 
        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
     
        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
      
        private string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
  
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; }
        }

        private string  _SegmentName;
        public string SegmentName
        {
            get { return _SegmentName; }
            set { _SegmentName = value; }
        }

        private string _HeaderCode;
        public string  HeaderCode
        {
            get { return _HeaderCode; }
            set { _HeaderCode = value; }
        }

        private string _HeaderName;
        public string HeaderName
        {
            get { return _HeaderName; }
            set { _HeaderName = value; }
        }

        private int _HeaderID;
        public int HeaderID
        {
            get { return _HeaderID; }
            set { _HeaderID = value; }
        }

        private string _strQuery;
        public string strQuery
        {
            get { return _strQuery; }
            set { _strQuery = value; }
        }


        private string _strSession;
        public string Session
        {
            get { return _strSession; }
            set { _strSession = value; }
        }

        private int  _GST_Applicable;

        public int GSTApplicable
        {
            get { return _GST_Applicable; }
            set { _GST_Applicable = value; }
        }

    }

    public class ObjEnquiry
    {
        private int _Enquiryid;
        public int EnquiryId
        {
            get { return _Enquiryid; }
            set { _Enquiryid = value; }
        }


        private string _CandidateName;
        public string CandidateName
        {
            get { return _CandidateName; }
            set { _CandidateName = value; }
        }

        private string _DOB;
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        private string _Telephone;
        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        private string _MobileNo;
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        private string _Citizenship;
        public string Citizenship
        {
            get { return _Citizenship; }
            set { _Citizenship = value; }
        }

        private int _CourseAppliedFor;
        public int CourseAppliedFor
        {
            get { return _CourseAppliedFor; }
            set { _CourseAppliedFor = value; }
        }

        private string _ContactMeBy;
        public string ContactMeBy
        {
            get { return _ContactMeBy; }
            set { _ContactMeBy = value; }
        }

        private char _Subscription;
        public char Subscription
        {
            get { return _Subscription; }
            set { _Subscription = value; }
        }

        private string _Enquiry;
        public string Enquiry
        {
            get { return _Enquiry; }
            set { _Enquiry = value; }
        }

        private string _Question;
        public string Question
        {
            get { return _Question; }
            set { _Question = value; }
        }

        private string _FromDt;
        public string FromDt
        {
            get { return _FromDt; }
            set { _FromDt = value; }
        }

        private string _ToDt;
        public string ToDt
        {
            get { return _ToDt; }
            set { _ToDt = value; }
        }
    }

    //Migration
    public class ObjFeeMaster
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _RegNo;
        public int RegistrationNo
        {
            get { return _RegNo; }
            set { _RegNo = value; }
        }

        private int _FromRegNo;
        public int FromRegNO
        {
            get { return _FromRegNo; }
            set { _FromRegNo = value; }
        }

        private int _ToRegNo;
        public int ToRegNO
        {
            get { return _ToRegNo; }
            set { _ToRegNo = value; }
        }

        private int _RoomType;
        public int RoomType
        {
            get { return _RoomType; }
            set { _RoomType = value; }
        }

        private int _CourseId;
        public int CourseID
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }

        private int _BatchId;
        public int BatchId
        {
            get { return _BatchId; }
            set { _BatchId = value; }
        }

        private int _SemesterId;
        public int SemesterId
        {
            get { return _SemesterId; }
            set { _SemesterId = value; }
        }

        private string _SectionName;
        public string SectionName
        {
            get { return _SectionName; }
            set { _SectionName = value; }
        }
        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        private int _SectionID;
        public int SectionID
        {
            get { return _SectionID; }
            set { _SectionID = value; }
        }


        private Double _Amount;
        public Double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        private string _SegmentName;
        public string SegmentName
        {
            get { return _SegmentName; }
            set { _SegmentName = value; }
        }

        private string _Session;
        public string Session
        {
            get { return _Session; }
            set { _Session = value; }
        }

        private string _JoiningDate;
        public string JoiningDate
        {
            get { return _JoiningDate; }
            set { _JoiningDate = value; }
        }

        private string _LeavingDate;
        public string LeavingDate
        {
            get { return _LeavingDate; }
            set { _LeavingDate = value; }
        }


        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _strInsertQuery;
        public string strInsertQuery
        {
            get { return _strInsertQuery; }
            set { _strInsertQuery = value; }
        }

        private Double _ReceiptAmount;
        public Double ReceiptAmount
        {
            get { return _ReceiptAmount; }
            set { _ReceiptAmount = value; }
        }

        private Double _Concession;
        public Double Concession
        {
            get { return _Concession; }
            set { _Concession = value; }
        }
        private string _PaymentMode;
        public string PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }
        private string _OnlineTranId;
        public string OnlineTranId
        {
            get { return _OnlineTranId; }
            set { _OnlineTranId = value; }
        }
        private string _MachineIP;
        public string MachineIP
        {
            get { return _MachineIP; }
            set { _MachineIP = value; }
        }
        private string _Transaction_Type;
        public string Transaction_Type
        {
            get { return _Transaction_Type; }
            set { _Transaction_Type = value; }
        }
        private string _ChequeDDNo;
        public string ChequeDDNo
        {
            get { return _ChequeDDNo; }
            set { _ChequeDDNo = value; }
        }

        private string _ChequeDate;
        public string ChequeDate
        {
            get { return _ChequeDate; }
            set { _ChequeDate = value; }
        }

        private string _IssuedBank;
        public string IssuedBank
        {
            get { return _IssuedBank; }
            set { _IssuedBank = value; }
        }

        private string _Remarks;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        private Double _ChequeDDReceiptAmount;
        public Double ChequeDDReceiptAmount
        {
            get { return _ChequeDDReceiptAmount; }
            set { _ChequeDDReceiptAmount = value; }
        }

        private int _TransStatus;
        public int TransStatus
        {
            get { return _TransStatus; }
            set { _TransStatus = value; }
        }

        private int _ActivePlanID;
        public int ActivePlanID
        {
            get { return _ActivePlanID; }
            set { _ActivePlanID = value; }
        }


        private Double _Fine;
        public Double Fine
        {
            get { return _Fine; }
            set { _Fine = value; }
        }


        private int _ReceiptNo;
        public int ReceiptNo
        {
            get { return _ReceiptNo; }
            set { _ReceiptNo = value; }
        }
        //for Cancel Receipt
        private int _CancelID;
        public int CancelID
        {
            get { return _CancelID; }
            set { _CancelID = value; }
        }
        private string _RequestedBy;
        public string RequestedBy
        {
            get { return _RequestedBy; }
            set { _RequestedBy = value; }
        }
        private string _ReasonToCancel;
        public string ReasonToCancel
        {
            get { return _ReasonToCancel; }
            set { _ReasonToCancel = value; }
        }
        private string _Approved;
        public string Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }
        private string _CancelBy;
        public string CancelBy
        {
            get { return _CancelBy; }
            set { _CancelBy = value; }
        }

        private string _ClosureDate;
        public string ClosureDate
        {
            get { return _ClosureDate; }
            set { _ClosureDate = value; }
        }

        private double _PreviousBalAmount;
        public double PreviousBalAmount
        {
            get { return _PreviousBalAmount; }
            set { _PreviousBalAmount = value; }
        }

        private double _RefundAmount;
        public double RefundAmount
        {
            get { return _RefundAmount; }
            set { _RefundAmount = value; }
        }

        private double _ConsiderableBalAmount;
        public double ConsiderableBalAmount
        {
            get { return _ConsiderableBalAmount; }
            set { _ConsiderableBalAmount = value; }
        }

        private double _ConsiderableRefundAmount;
        public double ConsiderableRefundAmount
        {
            get { return _ConsiderableRefundAmount; }
            set { _ConsiderableRefundAmount = value; }
        }
        private double _PrvAdmissionFeePaid;
        public double PrvAdmissionFeePaid
        {
            get { return _PrvAdmissionFeePaid; }
            set { _PrvAdmissionFeePaid = value; }
        }


        private double _PrvAdmissionFeeActual;
        public double PrvAdmissionFeeActual
        {
            get { return _PrvAdmissionFeeActual; }
            set { _PrvAdmissionFeeActual = value; }
        }

        string _FromDate;
        public string DateFrom
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        private string _ToDate;
        public string DateTo
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        private string _FineLogNo;
        public string FineLogNo
        {
            get { return _FineLogNo; }
            set { _FineLogNo = value; }
        }

        private int _PORTAL;
        public int CALLED_FROM_PORTAL
        {
            get { return _PORTAL; }
            set { _PORTAL = value; }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private string _Reconcile_Doc_Name_Chq;
        public string Reconcile_Doc_Name_Chq
        {
            get { return _Reconcile_Doc_Name_Chq; }
            set { _Reconcile_Doc_Name_Chq = value; }
        }

        private string _Reconcile_Doc_Name_NEFT;
        public string Reconcile_Doc_Name_NEFT
        {
            get { return _Reconcile_Doc_Name_NEFT; }
            set { _Reconcile_Doc_Name_NEFT = value; }
        }

        private string _Reconcile_Doc_Name_Online;
        public string Reconcile_Doc_Name_Online
        {
            get { return _Reconcile_Doc_Name_Online; }
            set { _Reconcile_Doc_Name_Online = value; }
        }

        private string _Reconcile_Doc_Name_Cash;
        public string Reconcile_Doc_Name_Cash
        {
            get { return _Reconcile_Doc_Name_Cash; }
            set { _Reconcile_Doc_Name_Cash = value; }
        }


        //Organizational detail
        private string _Orig_Code;
        public string Orig_Code
        {
            get { return _Orig_Code; }
            set { _Orig_Code = value; }
        }

        private string _Orig_Name;
        public string Orig_Name
        {
            get { return _Orig_Name; }
            set { _Orig_Name = value; }
        }

        private string _Orig_Address1;
        public string Orig_Address1
        {
            get { return _Orig_Address1; }
            set { _Orig_Address1 = value; }
        }


        private string _Orig_Address2;
        public string Orig_Address2
        {
            get { return _Orig_Address2; }
            set { _Orig_Address2 = value; }
        }


        private string _Orig_Address3;
        public string Orig_Address3
        {
            get { return _Orig_Address3; }
            set { _Orig_Address3 = value; }
        }

        private string _Orig_Phone1;
        public string Orig_Phone1
        {
            get { return _Orig_Phone1; }
            set { _Orig_Phone1 = value; }
        }
        private string _Orig_Phone2;
        public string Orig_Phone2
        {
            get { return _Orig_Phone2; }
            set { _Orig_Phone2 = value; }
        }

        private string _Orig_Url;
        public string Orig_Url
        {
            get { return _Orig_Url; }
            set { _Orig_Url = value; }
        }

        private string _Orig_Email1;
        public string Orig_Email1
        {
            get { return _Orig_Email1; }
            set { _Orig_Email1 = value; }
        }

        private string _Orig_Email2;
        public string Orig_Email2
        {
            get { return _Orig_Email2; }
            set { _Orig_Email2 = value; }
        }


        private string _Orig_Logo1;
        public string Orig_Logo1
        {
            get { return _Orig_Logo1; }
            set { _Orig_Logo1 = value; }
        }

        private string _Orig_Logo2;
        public string Orig_Logo2
        {
            get { return _Orig_Logo2; }
            set { _Orig_Logo2 = value; }
        }

        private string _Orig_Year_Reserved;
        public string Orig_Year_Reserved
        {
            get { return _Orig_Year_Reserved; }
            set { _Orig_Year_Reserved = value; }
        }
        private Int16 _GST_Applicable;

        public Int16 GSTApplicable
        {
            get { return _GST_Applicable; }
            set { _GST_Applicable = value; }
        }
    }

    public class ObjAdmissionAcademicCriteria
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _RegNo;
        public int RegNo
        {
            get { return _RegNo; }
            set { _RegNo = value; }
        }

        private int _CourseId;
        public int CourseID
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }

        private string _SectionName;
        public string SectionName
        {
            get { return _SectionName; }
            set { _SectionName = value; }
        }

        private string _SegmentName;
        public string SegmentName
        {
            get { return _SegmentName; }
            set { _SegmentName = value; }
        }

        private string _Session;
        public string Session
        {
            get { return _Session; }
            set { _Session = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        private string _strInsertQuery;
        public string strInsertQuery
        {
            get { return _strInsertQuery; }
            set { _strInsertQuery = value; }
        }
    }


    public class ObjStudentRegistrationInfo
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _RegistrationNumber;
        public string RegistrationNumber
        {
            get { return _RegistrationNumber; }
            set { _RegistrationNumber = value; }
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _MiddleName;
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private string _DOB;
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }

        private string _Nationality;
        public string Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }

        private string _EducationStatus;
        public string EducationStatus
        {
            get { return _EducationStatus; }
            set { _EducationStatus = value; }
        }

        private string _Caste;
        public string Caste
        {
            get { return _Caste; }
            set { _Caste = value; }
        }
        // 

        private string _Category;
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        private string _Religion;
        public string Religion
        {
            get { return _Religion; }
            set { _Religion = value; }
        }
        //

        private string _MobileNumber;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }
        //
        private string _AdmissionStatus;
        public string AdmissionStatus
        {
            get { return _AdmissionStatus; }
            set { _AdmissionStatus = value; }
        }

        //ResidencePhone
        private string _ResidencePhone;
        public string ResidencePhone
        {
            get { return _ResidencePhone; }
            set { _ResidencePhone = value; }
        }

        //Email
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        //CorrespondenceAddress
        private string _CorrespondenceAddress;
        public string CorrespondenceAddress
        {
            get { return _CorrespondenceAddress; }
            set { _CorrespondenceAddress = value; }
        }
        //PinCode
        private string _PinCode;
        public string PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }

        //PermanentAddress,
        private string _PermanentAddress;
        public string PermanentAddress
        {
            get { return _PermanentAddress; }
            set { _PermanentAddress = value; }
        }

        //ParAddressPinCode
        private string _ParAddressPinCode;
        public string ParAddressPinCode
        {
            get { return _ParAddressPinCode; }
            set { _ParAddressPinCode = value; }
        }

        //PassportNumber
        private string _PassportNumber;
        public string PassportNumber
        {
            get { return _PassportNumber; }
            set { _PassportNumber = value; }
        }

        //PassportIssuedAt
        private string _PassportIssuedAt;
        public string PassportIssuedAt
        {
            get { return _PassportIssuedAt; }
            set { _PassportIssuedAt = value; }
        }

        //CountriesTraveled
        private string _CountriesTraveled;
        public string CountriesTraveled
        {
            get { return _CountriesTraveled; }
            set { _CountriesTraveled = value; }
        }
        //FatherName
        private string _FatherName;
        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }
        //MotherName
        private string _MotherName;
        public string MotherName
        {
            get { return _MotherName; }
            set { _MotherName = value; }
        }
        //FatherMobile
        private string _FatherMobile;
        public string FatherMobile
        {
            get { return _FatherMobile; }
            set { _FatherMobile = value; }
        }
        //MotherMobile
        private string _MotherMobile;
        public string MotherMobile
        {
            get { return _MotherMobile; }
            set { _MotherMobile = value; }
        }
        //FatherEmail
        private string _FatherEmail;
        public string FatherEmail
        {
            get { return _FatherEmail; }
            set { _FatherEmail = value; }
        }
        //MotherEmail
        private string _MotherEmail;
        public string MotherEmail
        {
            get { return _MotherEmail; }
            set { _MotherEmail = value; }
        }
        //Address
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        //NameOfCourseApplyingFor
        private string _NameOfCourseApplyingFor;
        public string NameOfCourseApplyingFor
        {
            get { return _NameOfCourseApplyingFor; }
            set { _NameOfCourseApplyingFor = value; }
        }
        //NameOfCourseApplyingFor
        private string _CourseName;
        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }
        //CourseCode
        private string _CourseCode;
        public string CourseCode
        {
            get { return _CourseCode; }
            set { _CourseCode = value; }
        }
        //AcedemicYear
        private string _AcedemicYear;
        public string AcedemicYear
        {
            get { return _AcedemicYear; }
            set { _AcedemicYear = value; }
        }

        //NameOfCourseApplyingFor(Financial)
        private string _NameOfCourseApplyingFor_fin;
        public string NameOfCourseApplyingFor_fin
        {
            get { return _NameOfCourseApplyingFor_fin; }
            set { _NameOfCourseApplyingFor_fin = value; }
        }
        //CourseCode(Financial)
        private string _CourseCode_fin;
        public string CourseCode_fin
        {
            get { return _CourseCode_fin; }
            set { _CourseCode_fin = value; }
        }
        //AcedemicYear(Financial)
        private string _AcedemicYear_fin;
        public string AcedemicYear_fin
        {
            get { return _AcedemicYear_fin; }
            set { _AcedemicYear_fin = value; }
        }

        //AreApplyAsNRIStudent
        private Boolean _AreApplyAsNRIStudent;
        public Boolean AreApplyAsNRIStudent
        {
            get { return _AreApplyAsNRIStudent; }
            set { _AreApplyAsNRIStudent = value; }
        }
        //EntTestName1
        private string _EntTestName1;
        public string EntTestName1
        {
            get { return _EntTestName1; }
            set { _EntTestName1 = value; }
        }
        //EntTestDate1
        private string _EntTestDate1;
        public string EntTestDate1
        {
            get { return _EntTestDate1; }
            set { _EntTestDate1 = value; }
        }
        //EntTestPlace1
        private string _EntTestPlace1;
        public string EntTestPlace1
        {
            get { return _EntTestPlace1; }
            set { _EntTestPlace1 = value; }
        }
        //EntTestRank1
        private string _EntTestRank1;
        public string EntTestRank1
        {
            get { return _EntTestRank1; }
            set { _EntTestRank1 = value; }
        }

        //----------------------------------
        //EntTestName2
        private string _EntTestName2;
        public string EntTestName2
        {
            get { return _EntTestName2; }
            set { _EntTestName2 = value; }
        }
        //EntTestDate2
        private string _EntTestDate2;
        public string EntTestDate2
        {
            get { return _EntTestDate2; }
            set { _EntTestDate2 = value; }
        }
        //EntTestPlace2
        private string _EntTestPlace2;
        public string EntTestPlace2
        {
            get { return _EntTestPlace2; }
            set { _EntTestPlace2 = value; }
        }
        //EntTestRank2
        private string _EntTestRank2;
        public string EntTestRank2
        {
            get { return _EntTestRank2; }
            set { _EntTestRank2 = value; }
        }

        //----------------------------------
        //EntTestName3
        private string _EntTestName3;
        public string EntTestName3
        {
            get { return _EntTestName3; }
            set { _EntTestName3 = value; }
        }
        //EntTestDate3
        private string _EntTestDate3;
        public string EntTestDate3
        {
            get { return _EntTestDate3; }
            set { _EntTestDate3 = value; }
        }
        //EntTestPlace3
        private string _EntTestPlace3;
        public string EntTestPlace3
        {
            get { return _EntTestPlace3; }
            set { _EntTestPlace3 = value; }
        }
        //EntTestRank3
        private string _EntTestRank3;
        public string EntTestRank3
        {
            get { return _EntTestRank3; }
            set { _EntTestRank3 = value; }
        }

        //----------------------------------
        //EntTestName4
        private string _EntTestName4;
        public string EntTestName4
        {
            get { return _EntTestName4; }
            set { _EntTestName4 = value; }
        }
        //EntTestDate4
        private string _EntTestDate4;
        public string EntTestDate4
        {
            get { return _EntTestDate4; }
            set { _EntTestDate4 = value; }
        }
        //EntTestPlace4
        private string _EntTestPlace4;
        public string EntTestPlace4
        {
            get { return _EntTestPlace4; }
            set { _EntTestPlace4 = value; }
        }
        //EntTestRank4
        private string _EntTestRank4;
        public string EntTestRank4
        {
            get { return _EntTestRank4; }
            set { _EntTestRank4 = value; }
        }

        //ReasonForApply
        private string _ReasonForApply;
        public string ReasonForApply
        {
            get { return _ReasonForApply; }
            set { _ReasonForApply = value; }
        }


        //EduQualificationClassX_RollNo
        private string _EduQualificationClassX_RollNo;
        public string EduQualificationClassX_RollNo
        {
            get { return _EduQualificationClassX_RollNo; }
            set { _EduQualificationClassX_RollNo = value; }
        }
        //EduQualificationClassX_Year
        private string _EduQualificationClassX_Year;
        public string EduQualificationClassX_Year
        {
            get { return _EduQualificationClassX_Year; }
            set { _EduQualificationClassX_Year = value; }
        }
        //EduQualificationClassX_Stream
        private string _EduQualificationClassX_Stream;
        public string EduQualificationClassX_Stream
        {
            get { return _EduQualificationClassX_Stream; }
            set { _EduQualificationClassX_Stream = value; }
        }
        //EduQualificationClassX_Board
        private string _EduQualificationClassX_Board;
        public string EduQualificationClassX_Board
        {
            get { return _EduQualificationClassX_Board; }
            set { _EduQualificationClassX_Board = value; }
        }
        //EduQualificationClassX_Marks
        private string _EduQualificationClassX_Marks;
        public string EduQualificationClassX_Marks
        {
            get { return _EduQualificationClassX_Marks; }
            set { _EduQualificationClassX_Marks = value; }
        }
        //EduQualificationClassX_MaxMarks
        private string _EduQualificationClassX_MaxMarks;
        public string EduQualificationClassX_MaxMarks
        {
            get { return _EduQualificationClassX_MaxMarks; }
            set { _EduQualificationClassX_MaxMarks = value; }
        }
        //EduQualificationClassX_Percent
        private string _EduQualificationClassX_Percent;
        public string EduQualificationClassX_Percent
        {
            get { return _EduQualificationClassX_Percent; }
            set { _EduQualificationClassX_Percent = value; }
        }
        //EduQualificationClassX_Result
        private string _EduQualificationClassX_Result;
        public string EduQualificationClassX_Result
        {
            get { return _EduQualificationClassX_Result; }
            set { _EduQualificationClassX_Result = value; }
        }
        //-------------------------------------------------------------------------------------------

        //EduQualificationClassXII_RollNo
        private string _EduQualificationClassXII_RollNo;
        public string EduQualificationClassXII_RollNo
        {
            get { return _EduQualificationClassXII_RollNo; }
            set { _EduQualificationClassXII_RollNo = value; }
        }
        //EduQualificationClassXII_Year
        private string _EduQualificationClassXII_Year;
        public string EduQualificationClassXII_Year
        {
            get { return _EduQualificationClassXII_Year; }
            set { _EduQualificationClassXII_Year = value; }
        }
        //EduQualificationClassXII_Stream
        private string _EduQualificationClassXII_Stream;
        public string EduQualificationClassXII_Stream
        {
            get { return _EduQualificationClassXII_Stream; }
            set { _EduQualificationClassXII_Stream = value; }
        }
        //EduQualificationClassXII_Board
        private string _EduQualificationClassXII_Board;
        public string EduQualificationClassXII_Board
        {
            get { return _EduQualificationClassXII_Board; }
            set { _EduQualificationClassXII_Board = value; }
        }
        //EduQualificationClassXII_Marks
        private string _EduQualificationClassXII_Marks;
        public string EduQualificationClassXII_Marks
        {
            get { return _EduQualificationClassXII_Marks; }
            set { _EduQualificationClassXII_Marks = value; }
        }
        //EduQualificationClassXII_MaXIIMarks
        private string _EduQualificationClassXII_MaxMarks;
        public string EduQualificationClassXII_MaxMarks
        {
            get { return _EduQualificationClassXII_MaxMarks; }
            set { _EduQualificationClassXII_MaxMarks = value; }
        }
        //EduQualificationClassXII_Percent
        private string _EduQualificationClassXII_Percent;
        public string EduQualificationClassXII_Percent
        {
            get { return _EduQualificationClassXII_Percent; }
            set { _EduQualificationClassXII_Percent = value; }
        }
        //EduQualificationClassXII_Result
        private string _EduQualificationClassXII_Result;
        public string EduQualificationClassXII_Result
        {
            get { return _EduQualificationClassXII_Result; }
            set { _EduQualificationClassXII_Result = value; }
        }

        //-------------------------------------------------------------------------------------------
        //EduQualificationClassGraduation_RollNo
        private string _EduQualificationClassGraduation_RollNo;
        public string EduQualificationClassGraduation_RollNo
        {
            get { return _EduQualificationClassGraduation_RollNo; }
            set { _EduQualificationClassGraduation_RollNo = value; }
        }
        //EduQualificationClassGraduation_Year
        private string _EduQualificationClassGraduation_Year;
        public string EduQualificationClassGraduation_Year
        {
            get { return _EduQualificationClassGraduation_Year; }
            set { _EduQualificationClassGraduation_Year = value; }
        }
        //EduQualificationClassGraduation_Stream
        private string _EduQualificationClassGraduation_Stream;
        public string EduQualificationClassGraduation_Stream
        {
            get { return _EduQualificationClassGraduation_Stream; }
            set { _EduQualificationClassGraduation_Stream = value; }
        }
        //EduQualificationClassGraduation_Board
        private string _EduQualificationClassGraduation_Board;
        public string EduQualificationClassGraduation_Board
        {
            get { return _EduQualificationClassGraduation_Board; }
            set { _EduQualificationClassGraduation_Board = value; }
        }
        //EduQualificationClassGraduation_Marks
        private string _EduQualificationClassGraduation_Marks;
        public string EduQualificationClassGraduation_Marks
        {
            get { return _EduQualificationClassGraduation_Marks; }
            set { _EduQualificationClassGraduation_Marks = value; }
        }
        //EduQualificationClassGraduation_MaGraduationMarks
        private string _EduQualificationClassGraduation_MaxMarks;
        public string EduQualificationClassGraduation_MaxMarks
        {
            get { return _EduQualificationClassGraduation_MaxMarks; }
            set { _EduQualificationClassGraduation_MaxMarks = value; }
        }
        //EduQualificationClassGraduation_Percent
        private string _EduQualificationClassGraduation_Percent;
        public string EduQualificationClassGraduation_Percent
        {
            get { return _EduQualificationClassGraduation_Percent; }
            set { _EduQualificationClassGraduation_Percent = value; }
        }
        //EduQualificationClassGraduation_Result
        private string _EduQualificationClassGraduation_Result;
        public string EduQualificationClassGraduation_Result
        {
            get { return _EduQualificationClassGraduation_Result; }
            set { _EduQualificationClassGraduation_Result = value; }
        }
        //-------------------------------------------------------------------------------------------
        //EduQualificationClassOther_RollNo
        private string _EduQualificationClassOther_RollNo;
        public string EduQualificationClassOther_RollNo
        {
            get { return _EduQualificationClassOther_RollNo; }
            set { _EduQualificationClassOther_RollNo = value; }
        }
        //EduQualificationClassOther_Year
        private string _EduQualificationClassOther_Year;
        public string EduQualificationClassOther_Year
        {
            get { return _EduQualificationClassOther_Year; }
            set { _EduQualificationClassOther_Year = value; }
        }
        //EduQualificationClassOther_Stream
        private string _EduQualificationClassOther_Stream;
        public string EduQualificationClassOther_Stream
        {
            get { return _EduQualificationClassOther_Stream; }
            set { _EduQualificationClassOther_Stream = value; }
        }
        //EduQualificationClassOther_Board
        private string _EduQualificationClassOther_Board;
        public string EduQualificationClassOther_Board
        {
            get { return _EduQualificationClassOther_Board; }
            set { _EduQualificationClassOther_Board = value; }
        }
        //EduQualificationClassOther_Marks
        private string _EduQualificationClassOther_Marks;
        public string EduQualificationClassOther_Marks
        {
            get { return _EduQualificationClassOther_Marks; }
            set { _EduQualificationClassOther_Marks = value; }
        }
        //EduQualificationClassOther_MaOtherMarks
        private string _EduQualificationClassOther_MaxOtherMarks;
        public string EduQualificationClassOther_MaxOtherMarks
        {
            get { return _EduQualificationClassOther_MaxOtherMarks; }
            set { _EduQualificationClassOther_MaxOtherMarks = value; }
        }
        //EduQualificationClassOther_Percent
        private string _EduQualificationClassOther_Percent;
        public string EduQualificationClassOther_Percent
        {
            get { return _EduQualificationClassOther_Percent; }
            set { _EduQualificationClassOther_Percent = value; }
        }
        //EduQualificationClassOther_Result
        private string _EduQualificationClassOther_Result;
        public string EduQualificationClassOther_Result
        {
            get { return _EduQualificationClassOther_Result; }
            set { _EduQualificationClassOther_Result = value; }
        }

        //Profession_OtherQualificationAwards_Text
        private string _Profession_OtherQualificationAwards_Text;
        public string Profession_OtherQualificationAwards_Text
        {
            get { return _Profession_OtherQualificationAwards_Text; }
            set { _Profession_OtherQualificationAwards_Text = value; }
        }
        //DetailsWorkExperience_Text
        private string _DetailsWorkExperience_Text;
        public string DetailsWorkExperience_Text
        {
            get { return _DetailsWorkExperience_Text; }
            set { _DetailsWorkExperience_Text = value; }
        }
        //HobbiesAndInterest_Text
        private string _HobbiesAndInterest_Text;
        public string HobbiesAndInterest_Text
        {
            get { return _HobbiesAndInterest_Text; }
            set { _HobbiesAndInterest_Text = value; }
        }
        //MaritalSatus
        private string _MaritalSatus;
        public string MaritalSatus
        {
            get { return _MaritalSatus; }
            set { _MaritalSatus = value; }
        }
        //NameOfSpouse
        private string _NameOfSpouse;
        public string NameOfSpouse
        {
            get { return _NameOfSpouse; }
            set { _NameOfSpouse = value; }
        }
        //BloodGroup
        private string _BloodGroup;
        public string BloodGroup
        {
            get { return _BloodGroup; }
            set { _BloodGroup = value; }
        }
        //IsPhysicalDisable
        private Boolean _IsPhysicalDisable;
        public Boolean IsPhysicalDisable
        {
            get { return _IsPhysicalDisable; }
            set { _IsPhysicalDisable = value; }
        }

        //DetailsPhysicalDisable_Text
        private string _DetailsPhysicalDisable_Text;
        public string DetailsPhysicalDisable_Text
        {
            get { return _DetailsPhysicalDisable_Text; }
            set { _DetailsPhysicalDisable_Text = value; }
        }
        //InforationGotFrom_Newspaper
        private Boolean _InforationGotFrom_Newspaper;
        public Boolean InforationGotFrom_Newspaper
        {
            get { return _InforationGotFrom_Newspaper; }
            set { _InforationGotFrom_Newspaper = value; }
        }

        //InforationGotFrom_Hoarding
        private Boolean _InforationGotFrom_Hoarding;
        public Boolean InforationGotFrom_Hoarding
        {
            get { return _InforationGotFrom_Hoarding; }
            set { _InforationGotFrom_Hoarding = value; }
        }

        //InforationGotFrom_Internet
        private Boolean _InforationGotFrom_Internet;
        public Boolean InforationGotFrom_Internet
        {
            get { return _InforationGotFrom_Internet; }
            set { _InforationGotFrom_Internet = value; }
        }

        //InforationGotFrom_Representative
        private Boolean _InforationGotFrom_Representative;
        public Boolean InforationGotFrom_Representative
        {
            get { return _InforationGotFrom_Representative; }
            set { _InforationGotFrom_Representative = value; }
        }

        //InforationGotFrom_Direct
        private Boolean _InforationGotFrom_Direct;
        public Boolean InforationGotFrom_Direct
        {
            get { return _InforationGotFrom_Direct; }
            set { _InforationGotFrom_Direct = value; }
        }

        //InforationGotFrom_Friends
        private Boolean _InforationGotFrom_Friends;
        public Boolean InforationGotFrom_Friends
        {
            get { return _InforationGotFrom_Friends; }
            set { _InforationGotFrom_Friends = value; }
        }

        //InforationGotFrom_OtherText
        private string _InforationGotFrom_OtherText;
        public string InforationGotFrom_OtherText
        {
            get { return _InforationGotFrom_OtherText; }
            set { _InforationGotFrom_OtherText = value; }
        }

        //ProffOfIdentity_VoterCard 
        private Boolean _ProffOfIdentity_VoterCard;
        public Boolean ProffOfIdentity_VoterCard
        {
            get { return _ProffOfIdentity_VoterCard; }
            set { _ProffOfIdentity_VoterCard = value; }
        }

        //ProffOfIdentity_DL
        private Boolean _ProffOfIdentity_DL;
        public Boolean ProffOfIdentity_DL
        {
            get { return _ProffOfIdentity_DL; }
            set { _ProffOfIdentity_DL = value; }
        }

        //ProffOfIdentity_Passport
        private Boolean _ProffOfIdentity_Passport;
        public Boolean ProffOfIdentity_Passport
        {
            get { return _ProffOfIdentity_Passport; }
            set { _ProffOfIdentity_Passport = value; }
        }

        //ProffOfIdentity_PANCard
        private Boolean _ProffOfIdentity_PANCard;
        public Boolean ProffOfIdentity_PANCard
        {
            get { return _ProffOfIdentity_PANCard; }
            set { _ProffOfIdentity_PANCard = value; }
        }

        //ProffOfIdentity_AdharCard
        private Boolean _ProffOfIdentity_AdharCard;
        public Boolean ProffOfIdentity_AdharCard
        {
            get { return _ProffOfIdentity_AdharCard; }
            set { _ProffOfIdentity_AdharCard = value; }
        }

        //OtherDoc_TransriptsOfQualification
        private Boolean _OtherDoc_TransriptsOfQualification;
        public Boolean OtherDoc_TransriptsOfQualification
        {
            get { return _OtherDoc_TransriptsOfQualification; }
            set { _OtherDoc_TransriptsOfQualification = value; }
        }

        //OtherDoc_CasteCertificate
        private Boolean _OtherDoc_CasteCertificate;
        public Boolean OtherDoc_CasteCertificate
        {
            get { return _OtherDoc_CasteCertificate; }
            set { _OtherDoc_CasteCertificate = value; }
        }

        //OtherDoc_Photographs
        private Boolean _OtherDoc_Photographs;
        public Boolean OtherDoc_Photographs
        {
            get { return _OtherDoc_Photographs; }
            set { _OtherDoc_Photographs = value; }
        }

        //OtherDoc_ExpCertificate
        private Boolean _OtherDoc_ExpCertificate;
        public Boolean OtherDoc_ExpCertificate
        {
            get { return _OtherDoc_ExpCertificate; }
            set { _OtherDoc_ExpCertificate = value; }
        }

        //OtherDoc_AddProof
        private Boolean _OtherDoc_AddProof;
        public Boolean OtherDoc_AddProof
        {
            get { return _OtherDoc_AddProof; }
            set { _OtherDoc_AddProof = value; }
        }

        //OtherDoc_TFWSIncomeCertificate
        private Boolean _OtherDoc_TFWSIncomeCertificate;
        public Boolean OtherDoc_TFWSIncomeCertificate
        {
            get { return _OtherDoc_TFWSIncomeCertificate; }
            set { _OtherDoc_TFWSIncomeCertificate = value; }
        }

        //OtherDoc_DomicileCertificate
        private Boolean _OtherDoc_DomicileCertificate;
        public Boolean OtherDoc_DomicileCertificate
        {
            get { return _OtherDoc_DomicileCertificate; }
            set { _OtherDoc_DomicileCertificate = value; }
        }

        //OtherDoc_MedicalCertificate
        private Boolean _OtherDoc_MedicalCertificate;
        public Boolean OtherDoc_MedicalCertificate
        {
            get { return _OtherDoc_MedicalCertificate; }
            set { _OtherDoc_MedicalCertificate = value; }
        }

        //Declaration_InformationGivenIsTrue
        private Boolean _Declaration_InformationGivenIsTrue;
        public Boolean Declaration_InformationGivenIsTrue
        {
            get { return _Declaration_InformationGivenIsTrue; }
            set { _Declaration_InformationGivenIsTrue = value; }
        }

        //Declaration_FormDataWillNotProvided_ExternalOrganisation
        private Boolean _Declaration_FormDataWillNotProvided_ExternalOrganisation;
        public Boolean Declaration_FormDataWillNotProvided_ExternalOrganisation
        {
            get { return _Declaration_FormDataWillNotProvided_ExternalOrganisation; }
            set { _Declaration_FormDataWillNotProvided_ExternalOrganisation = value; }
        }

        //Declaration_ConfirmingMinimumEligCriteria
        private Boolean _Declaration_ConfirmingMinimumEligCriteria;
        public Boolean Declaration_ConfirmingMinimumEligCriteria
        {
            get { return _Declaration_ConfirmingMinimumEligCriteria; }
            set { _Declaration_ConfirmingMinimumEligCriteria = value; }
        }

        //Declaration_ReadEnclosedDeclaration
        private Boolean _Declaration_ReadEnclosedDeclaration;
        public Boolean Declaration_ReadEnclosedDeclaration
        {
            get { return _Declaration_ReadEnclosedDeclaration; }
            set { _Declaration_ReadEnclosedDeclaration = value; }
        }

        //Declaration_ReadEnclosedDeclaration
        private Int16 _IsDiplomaStudent;
        public Int16 IsDiplomaStudent
        {
            get { return _IsDiplomaStudent; }
            set { _IsDiplomaStudent = value; }
        }

        //DraftNo
        private string _DraftNo;
        public string DraftNo
        {
            get { return _DraftNo; }
            set { _DraftNo = value; }
        }

        //BankName
        private string _BankName;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        //Amount
        private string _Amount;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //DraftDate
        private string _DraftDate;
        public string DraftDate
        {
            get { return _DraftDate; }
            set { _DraftDate = value; }
        }

        //CurrentDate
        private string _CurrentDate;
        public string CurrentDate
        {
            get { return _CurrentDate; }
            set { _CurrentDate = value; }
        }

        //Place
        private string _Place;
        public string Place
        {
            get { return _Place; }
            set { _Place = value; }
        }

        //RegistrationAccepted
        private Boolean _RegistrationAccepted;
        public Boolean RegistrationAccepted
        {
            get { return _RegistrationAccepted; }
            set { _RegistrationAccepted = value; }
        }

        //Image
        private string _ImageName;
        public string ImageName
        {
            get { return _ImageName; }
            set { _ImageName = value; }
        }

        //Signature
        private string _SignatureName;
        public string SignatureName
        {
            get { return _SignatureName; }
            set { _SignatureName = value; }
        }
        //Submitted
        private int _Submitted;
        public int Submitted
        {
            get { return _Submitted; }
            set { _Submitted = value; }
        }

        //Query
        private string _Centers;
        public string ChosenCenters
        {
            get { return _Centers; }
            set { _Centers = value; }
        }

        //IsConfirmed User from Admin user
        private string _ConfirmedExamCenter;
        public string ConfirmedExamCenter
        {
            get { return _ConfirmedExamCenter; }
            set { _ConfirmedExamCenter = value; }
        }

        //Online Enrolled Student
        private int _IsOnlineStudent;
        public int IsOnlineStudent
        {
            get { return _IsOnlineStudent; }
            set { _IsOnlineStudent = value; }
        }

        //Online enrolled student's RollNumber
        private string _OnLineRegistrationNumber;
        public string OnLineRegistrationNumber
        {
            get { return _OnLineRegistrationNumber; }
            set { _OnLineRegistrationNumber = value; }
        }

        //Admission Stage
        private string _AdmissionStage;
        public string AdmissionStage
        {
            get { return _AdmissionStage; }
            set { _AdmissionStage = value; }
        }
        //Alloted Center
        private int _AllotedCenter;
        public int AllotedCenter
        {
            get { return _AllotedCenter; }
            set { _AllotedCenter = value; }
        }

        //Alloted Center
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        //Alloted Center
        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        //ONLINE EXAM DATE VAL 
        private string _ONLINEEXAMDATEVAL;
        public string ONLINEEXAMDATEVAL
        {
            get { return _ONLINEEXAMDATEVAL; }
            set { _ONLINEEXAMDATEVAL = value; }
        }


        private string _EnrollmentStatus;
        public string EnrollmentStatus
        {
            get { return _EnrollmentStatus; }
            set { _EnrollmentStatus = value; }
        }

        private string _EnrollmentStatusMode;
        public string EnrollmentStatusMode
        {
            get { return _EnrollmentStatusMode; }
            set { _EnrollmentStatusMode = value; }
        }

        private string _EnrollmentStatusDate;
        public string EnrollmentStatusDate
        {
            get { return _EnrollmentStatusDate; }
            set { _EnrollmentStatusDate = value; }
        }

        private string _EnrollmentStatusSaveBy;
        public string EnrollmentStatusSaveBy
        {
            get { return _EnrollmentStatusSaveBy; }
            set { _EnrollmentStatusSaveBy = value; }
        }

        private int _IsOptedOnlineExamination;
        public int IsOptedOnlineExamination
        {
            get { return _IsOptedOnlineExamination; }
            set { _IsOptedOnlineExamination = value; }
        }
        private string _StudyCenter;
        public string StudyCenter
        {
            get { return _StudyCenter; }
            set { _StudyCenter = value; }
        }

        private string _OptedOnlineExaminatioDateVal;
        public string OptedOnlineExaminatioDateVal
        {
            get { return _OptedOnlineExaminatioDateVal; }
            set { _OptedOnlineExaminatioDateVal = value; }
        }

        private string _OptedOnlineExaminatioDate;
        public string OptedOnlineExaminatioDate
        {
            get { return _OptedOnlineExaminatioDate; }
            set { _OptedOnlineExaminatioDate = value; }
        }

        private string _OptedAdditionalSubject;
        public string OptedAdditionalSubject
        {
            get { return _OptedAdditionalSubject; }
            set { _OptedAdditionalSubject = value; }
        }

        private string _InsertedBy;
        public string InsertedBy
        {
            get { return _InsertedBy; }
            set { _InsertedBy = value; }
        }

        private int _SchoolID;
        public int SchoolID
        {
            get { return _SchoolID; }
            set { _SchoolID = value; }
        }

        private string _SchoolEmailId;
        public string SchoolEmailId
        {
            get { return _SchoolEmailId; }
            set { _SchoolEmailId = value; }
        }

        private string _CouncellorID;
        public string CouncellorID
        {
            get { return _CouncellorID; }
            set { _CouncellorID = value; }
        }

        private string _CouncellorName;
        public string CouncellorName
        {
            get { return _CouncellorName; }
            set { _CouncellorName = value; }
        }

        private string _ConsultantName;
        public string ConsultantName
        {
            get { return _ConsultantName; }
            set { _ConsultantName = value; }
        }

        private string _CouncellorEmail;
        public string CouncellorEmail
        {
            get { return _CouncellorEmail; }
            set { _CouncellorEmail = value; }
        }

        private string _CouncellorMobile;
        public string CouncellorMobile
        {
            get { return _CouncellorMobile; }
            set { _CouncellorMobile = value; }
        }

        private string _MobileNumberDirector;
        public string MobileNumberDirector
        {
            get { return _MobileNumberDirector; }
            set { _MobileNumberDirector = value; }
        }

        private string _RegistrationDate;
        public string RegistrationDate
        {
            get { return _RegistrationDate; }
            set { _RegistrationDate = value; }
        }

        private string _EntryMadeBy;
        public string EntryMadeBy
        {
            get { return _EntryMadeBy; }
            set { _EntryMadeBy = value; }
        }

        private string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

    }

    public class ObjStudentRegistrationSearch
    {
        private string _RegistrationNumber;
        public string RegistrationNumber
        {
            get { return _RegistrationNumber; }
            set { _RegistrationNumber = value; }
        }

        private string _DOB;
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }

        //FatherName
        private string _FatherName;
        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }

        //CheckedOtherSearch
        private Boolean _CheckedOtherSearch;
        public Boolean CheckedOtherSearch
        {
            get { return _CheckedOtherSearch; }
            set { _CheckedOtherSearch = value; }
        }


        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private string _Session;
        public string Session
        {
            get { return _Session; }
            set { _Session = value; }
        }

        private int _CourseID;
        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }
 
    }
 
    public class ObjAdmission
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int _CourseId;
        public int CourseID
        {
            get { return _CourseId; }
            set { _CourseId = value; }
        }

        private int _IsMendatoryForDiplomaStudent;
        public int IsMendatoryForDiplomaStudent
        {
            get { return _IsMendatoryForDiplomaStudent; }
            set { _IsMendatoryForDiplomaStudent = value; }
        }


        private int _SubjectId;
        public int SubjetID
        {
            get { return _SubjectId; }
            set { _SubjectId = value; }
        }

        private string _SubjectName;
        public string SubjectName
        {
            get { return _SubjectName; }
            set { _SubjectName = value; }
        }


        private string _Session;
        public string Session
        {
            get { return _Session; }
            set { _Session = value; }
        }

        private int _PassMarks;
        public int PassMarks
        {
            get { return _PassMarks; }
            set { _PassMarks = value; }
        }

        private string _CreatedBy;
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        private string _CreatedDate;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _ModifiedBy;
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        private int _MendatoryForDiplomaStudent;
        public int MendatoryForDiplomaStudent
        {
            get { return _MendatoryForDiplomaStudent; }
            set { _MendatoryForDiplomaStudent = value; }
        }
        private string _ModifiedDate;
        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
