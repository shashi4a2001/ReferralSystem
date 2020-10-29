using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLCMR
{
    public class EC_SMS_SendMessage
    {
        
            private string _SMS_Msg;
            public string SMS_Msg
            {
                get { return _SMS_Msg; }
                set { _SMS_Msg = value; }
            }

            private string _SMS_URLLink;
            public string SMS_URLLink
            {
                get { return _SMS_URLLink; }
                set { _SMS_URLLink = value; }
            }

            private string _SMS_TranDate;
            public string SMS_TranDate
            {
                get { return _SMS_TranDate; }
                set { _SMS_TranDate = value; }
            }

            private double _SMS_TranAmt;
            public double SMS_TranAmt
            {
                get { return _SMS_TranAmt; }
                set { _SMS_TranAmt = value; }
            }

            private string _SMS_Section;
            public string SMS_Section
            {
                get { return _SMS_Section; }
                set { _SMS_Section = value; }
            }

            private string _SMS_TranMode;
            public string SMS_TranMode
            {
                get { return _SMS_TranMode; }
                set { _SMS_TranMode = value; }
            }

            private string _SMS_OnlineTranId;
            public string SMS_OnlineTranId
            {
                get { return _SMS_OnlineTranId; }
                set { _SMS_OnlineTranId = value; }
            }

            private string _SMS_MobileNo;
            public string SMS_MobileNo
            {
                get { return _SMS_MobileNo; }
                set { _SMS_MobileNo = value; }
            }

            private string _SMS_Error;
            public string SMS_Error
            {
                get { return _SMS_Error; }
                set { _SMS_Error = value; }
            }

            private Boolean _SMS_Processed;
            public Boolean SMS_Processed
            {
                get { return _SMS_Processed; }
                set { _SMS_Processed = value; }
            }

            private int _SMS_ReceiptNo;
            public int SMS_ReceiptNo
            {
                get { return _SMS_ReceiptNo; }
                set { _SMS_ReceiptNo = value; }
            }

            private int _SMS_RegistrationNo;
            public int SMS_RegistrationNo
            {
                get { return _SMS_RegistrationNo; }
                set { _SMS_RegistrationNo = value; }
            }

            private string _SMS_KEY;
            public string SMS_KEY
            {
                get { return _SMS_KEY; }
                set { _SMS_KEY = value; }
            }

            private string _SMS_VALUE;
            public string SMS_VALUE
            {
                get { return _SMS_VALUE; }
                set { _SMS_VALUE = value; }
            }

            private string _SMS_Desc;
            public string SMS_Desc
            {
                get { return _SMS_Desc; }
                set { _SMS_Desc = value; }
            }

            private int _SMS_days;
            public int SMS_Days
            {
                get { return _SMS_days; }
                set { _SMS_days = value; }
            }

            private string _SMS_FromDate;
            public string  SMS_FromDate
            {
                get { return _SMS_FromDate; }
                set { _SMS_FromDate = value; }
            }

            private string _SMS_ToDate;
            public string SMS_ToDate
            {
                get { return _SMS_ToDate; }
                set { _SMS_ToDate = value; }
            }

            private string _SMS_LeaveId;
            public string  SMS_LeaveId
            {
                get { return _SMS_LeaveId; }
                set { _SMS_LeaveId = value; }
            }
    }
}
