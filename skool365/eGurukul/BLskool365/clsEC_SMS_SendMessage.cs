using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLCMR;
using BLCMR;
using System.Data.SqlClient;
namespace BLCMR
{
    public class clsEC_SMS_SendMessage
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public void SendSMSWithEmail(EC_SMS_SendMessage objECMessages, string strCon)
        {
            string strResponse = string.Empty;
            DataTable dtReceipt = new DataTable();
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            //get receipt details
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@P_KEY", objECMessages.SMS_KEY);
            cmd.Parameters.AddWithValue("@P_Desc", objECMessages.SMS_Desc);
            cmd.Parameters.AddWithValue("@P_ReceiptNo", objECMessages.SMS_ReceiptNo);

            if (objECMessages.SMS_ReceiptNo == -1 || objECMessages.SMS_ReceiptNo == -99) //-99-online cet registration
            {
                cmd.Parameters.AddWithValue("@P_ONLINETRANID", objECMessages.SMS_OnlineTranId);
            }
            dtReceipt = objDLGeneric.SpDataTable("USP_GET_SMS_BODY", cmd, strCon);

            if (dtReceipt != null && dtReceipt.Rows.Count > 0)
            {
                //SMS BODY  
                objECMessages.SMS_Msg = dtReceipt.Rows[0]["SMS_MSG"].ToString();
                string[] strSMS = objECMessages.SMS_Msg.Split('~');
                objECMessages.SMS_Msg = strSMS[0];

                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Section#", dtReceipt.Rows[0]["SECTION"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Amt#", dtReceipt.Rows[0]["AMOUNT"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Date#", dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Mode#", dtReceipt.Rows[0]["PAYMENT_MODE"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#TranID#", dtReceipt.Rows[0]["ONLINE_TRANID"].ToString());
                objECMessages.SMS_TranDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                //if (dtReceipt.Rows[0]["PAYMENT_MODE"].ToString().ToUpper() == "ONLINE")
                //{
                //    objECMessages.SMS_Msg = objECMessages.SMS_Msg + "Online Tran Id is " + dtReceipt.Rows[0]["ONLINE_TRANID"].ToString();
                //}

                //objECMessages.SMS_Msg = objECMessages.SMS_Msg + " For your ref. Receipt No." + Convert.ToString(objECMessages.SMS_ReceiptNo) + ".";
                objECMessages.SMS_Msg = objECMessages.SMS_Msg + strSMS[1];

                //SMS LINK
                objECMessages.SMS_URLLink = dtReceipt.Rows[0]["SMS_LINK"].ToString();
                objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", dtReceipt.Rows[0]["MOBILE_NO"].ToString());
                objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);

                //SENDING SMS USING URL
                try
                {
                    System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    strResponse = sr.ReadToEnd().Trim();
                }
                catch (Exception ex)
                {
                }

                //SEND MAIL

                    clsCommon objCommon = new clsCommon();
                    ObjSendEmail SendMail = new ObjSendEmail();
                    try
                    {
                    SendMail.From = dtReceipt.Rows[0]["FROM"].ToString();
                    SendMail.To = dtReceipt.Rows[0]["EMAIL"].ToString();
                    if ( dtReceipt.Rows[0]["FATHER_EMAIL"].ToString().Trim() != "")
                    {
                       SendMail.To = SendMail.To+ "," + dtReceipt.Rows[0]["FATHER_EMAIL"].ToString();
                    }
                    SendMail.CC = dtReceipt.Rows[0]["FATHER_EMAIL"].ToString();
                    SendMail.SMTPHost = dtReceipt.Rows[0]["HOST"].ToString();
                    SendMail.SMTPPort = Convert.ToInt32(dtReceipt.Rows[0]["PORT"].ToString());
                    SendMail.UserId = dtReceipt.Rows[0]["USER_ID"].ToString();
                    SendMail.Password = dtReceipt.Rows[0]["PASSWORD"].ToString();
                    if (objECMessages.SMS_ReceiptNo == -1)//failed transaction
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_FAILED"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#", objECMessages.SMS_TranDate);
                        SendMail.Subject = "Online Payment Failed for Transaction No:" + objECMessages.SMS_OnlineTranId + "";
                    }
                    else if (objECMessages.SMS_ReceiptNo == -2 || objECMessages.SMS_ReceiptNo == -99) //Proceed for verification //-99-online cet registration
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_PROCEED"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#", objECMessages.SMS_TranDate);
                        SendMail.Subject = "Online Payment Proceed for verification for Transaction No:" + objECMessages.SMS_OnlineTranId + "";
                    }
                    else if (objECMessages.SMS_ReceiptNo > 0)//final receipt
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_RECEIPT"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#", objECMessages.SMS_TranDate);
                        SendMail.MailBody = SendMail.MailBody.Replace("#VerfiedOn#", objECMessages.SMS_TranDate);
                        SendMail.Subject = "Payment successfully received for Receipt No :" + objECMessages.SMS_ReceiptNo.ToString () ;
                    }

                    if (SendMail.To.Trim() != "")
                    {
                        objCommon.SendEmail(SendMail );
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                      objCommon =null;
                      SendMail = null;
                }
            }
        }
        public void SendSMSWithEmail(DataTable dtReceipt,int ReceiptNo, string strCon)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            EC_SMS_SendMessage objECMessages = new EC_SMS_SendMessage();
            string strResponse = string.Empty;
            objECMessages.SMS_ReceiptNo = ReceiptNo;

             //get receipt details
            if (dtReceipt != null && dtReceipt.Rows.Count > 0)
            {
                //SMS BODY  
                objECMessages.SMS_Msg = dtReceipt.Rows[0]["SMS_MSG"].ToString();
                string[] strSMS = objECMessages.SMS_Msg.Split('~');
                objECMessages.SMS_Msg = strSMS[0];

                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Section#", dtReceipt.Rows[0]["SECTION"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Amt#", dtReceipt.Rows[0]["AMOUNT"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Date#", dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Mode#", dtReceipt.Rows[0]["PAYMENT_MODE"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#TranID#", dtReceipt.Rows[0]["ONLINE_TRANID"].ToString());
                objECMessages.SMS_TranDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                objECMessages.SMS_Msg = objECMessages.SMS_Msg + strSMS[1];

                //SMS LINK
                //SEND SMS TO STUDENT
                if (dtReceipt.Rows[0]["MOBILE_NO"].ToString().Trim() != "")
                {
                    objECMessages.SMS_URLLink = dtReceipt.Rows[0]["SMS_LINK"].ToString();
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", dtReceipt.Rows[0]["MOBILE_NO"].ToString());
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);

                    //SENDING SMS USING URL
                    try
                    {
                        System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        strResponse = sr.ReadToEnd().Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                //SEND SMS TO father
                if (dtReceipt.Rows[0]["FATHER_MOBILE_NO"].ToString().Trim() != "")
                {
                    objECMessages.SMS_URLLink = dtReceipt.Rows[0]["SMS_LINK"].ToString();
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", dtReceipt.Rows[0]["FATHER_MOBILE_NO"].ToString());
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);

                    //SENDING SMS USING URL
                    try
                    {
                        System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        strResponse = sr.ReadToEnd().Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                }


                //SEND MAIL
                clsCommon objCommon = new clsCommon();
                ObjSendEmail SendMail = new ObjSendEmail();
                try
                {
                    SendMail.From = dtReceipt.Rows[0]["FROM"].ToString();
                    SendMail.To = dtReceipt.Rows[0]["EMAIL"].ToString();
                  
                    if (dtReceipt.Rows[0]["FATHER_EMAIL"].ToString().Trim() != "")
                    {
                        SendMail.To = SendMail.To + "," + dtReceipt.Rows[0]["FATHER_EMAIL"].ToString();
                    }

                    SendMail.CC = objCommon.getConfigKeyValue("EMAIL IDS FOR RECEIPT AGAINST CASH/DRAFT/NEFT PAYMENT TO ADMIN", "EMAIL RECEPIENTS GROUP", strCon);
                    SendMail.SMTPHost = dtReceipt.Rows[0]["HOST"].ToString();
                    SendMail.SMTPPort = Convert.ToInt32(dtReceipt.Rows[0]["PORT"].ToString());
                    SendMail.UserId = dtReceipt.Rows[0]["USER_ID"].ToString();
                    SendMail.Password = dtReceipt.Rows[0]["PASSWORD"].ToString();

                    if (objECMessages.SMS_ReceiptNo == -1)//failed transaction
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_FAILED"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#", dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());//objECMessages.SMS_TranDate);
                        SendMail.Subject = "Online Payment Failed for Transaction No:" + dtReceipt.Rows[0]["ONLINE_TRANID"].ToString();// objECMessages.SMS_OnlineTranId + "";
                    }
                    else if (objECMessages.SMS_ReceiptNo > 0)//final receipt
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_RECEIPT"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#",dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());// objECMessages.SMS_TranDate);
                        SendMail.MailBody = SendMail.MailBody.Replace("#VerfiedOn#", objECMessages.SMS_TranDate);
                        SendMail.Subject = "Payment successfully received for Receipt No :" + objECMessages.SMS_ReceiptNo.ToString();
                    }
 
                    if (SendMail.To.Trim() != "")
                    {
                        objCommon.SendEmail(SendMail);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    objCommon = null;
                    SendMail = null;
                }
            }
        }
        public void SendSMSWithEmail(DataTable dtReceipt, int ReceiptNo, ObjFeeMaster feeMaster)
        {
            DLClsGeneric objDLGeneric = new DLClsGeneric();
            EC_SMS_SendMessage objECMessages = new EC_SMS_SendMessage();
            string strResponse = string.Empty;
            objECMessages.SMS_ReceiptNo = ReceiptNo;

            //get receipt details
            if (dtReceipt != null && dtReceipt.Rows.Count > 0)
            {
                //SMS BODY  
                objECMessages.SMS_Msg = dtReceipt.Rows[0]["SMS_MSG"].ToString();
                string[] strSMS = objECMessages.SMS_Msg.Split('~');
                objECMessages.SMS_Msg = strSMS[0];

                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Section#", dtReceipt.Rows[0]["SECTION"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Amt#", dtReceipt.Rows[0]["AMOUNT"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Date#", dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Mode#", dtReceipt.Rows[0]["PAYMENT_MODE"].ToString());
                objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#TranID#", dtReceipt.Rows[0]["ONLINE_TRANID"].ToString());
                objECMessages.SMS_TranDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                objECMessages.SMS_Msg = objECMessages.SMS_Msg + strSMS[1];

                //SMS LINK
                //SEND SMS TO STUDENT
                if (dtReceipt.Rows[0]["MOBILE_NO"].ToString().Trim() != "")
                {
                    objECMessages.SMS_URLLink = dtReceipt.Rows[0]["SMS_LINK"].ToString();
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", dtReceipt.Rows[0]["MOBILE_NO"].ToString());
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);

                    //SENDING SMS USING URL
                    try
                    {
                        System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        strResponse = sr.ReadToEnd().Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                //SEND SMS TO father
                if (dtReceipt.Rows[0]["FATHER_MOBILE_NO"].ToString().Trim() != "")
                {
                    objECMessages.SMS_URLLink = dtReceipt.Rows[0]["SMS_LINK"].ToString();
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", dtReceipt.Rows[0]["FATHER_MOBILE_NO"].ToString());
                    objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);

                    //SENDING SMS USING URL
                    try
                    {
                        System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        strResponse = sr.ReadToEnd().Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                }


                //SEND MAIL
                clsCommon objCommon = new clsCommon();
                ObjSendEmail SendMail = new ObjSendEmail();
                try
                {
                    SendMail.From = dtReceipt.Rows[0]["FROM"].ToString();
                    SendMail.To = dtReceipt.Rows[0]["EMAIL"].ToString();

                    if (dtReceipt.Rows[0]["FATHER_EMAIL"].ToString().Trim() != "")
                    {
                        SendMail.To = SendMail.To + "," + dtReceipt.Rows[0]["FATHER_EMAIL"].ToString();
                    }

                    SendMail.CC = objCommon.getConfigKeyValue("EMAIL IDS FOR RECEIPT AGAINST CASH/DRAFT/NEFT PAYMENT TO ADMIN", "EMAIL RECEPIENTS GROUP", feeMaster.ConnectionString);
                    SendMail.SMTPHost = dtReceipt.Rows[0]["HOST"].ToString();
                    SendMail.SMTPPort = Convert.ToInt32(dtReceipt.Rows[0]["PORT"].ToString());
                    SendMail.UserId = dtReceipt.Rows[0]["USER_ID"].ToString();
                    SendMail.Password = dtReceipt.Rows[0]["PASSWORD"].ToString();

                    if (objECMessages.SMS_ReceiptNo == -1)//failed transaction
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_FAILED"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#", dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());//objECMessages.SMS_TranDate);
                        SendMail.Subject = "Online Payment Failed for Transaction No:" + dtReceipt.Rows[0]["ONLINE_TRANID"].ToString();// objECMessages.SMS_OnlineTranId + "";
                    }
                    else if (objECMessages.SMS_ReceiptNo > 0)//final receipt
                    {
                        SendMail.MailBody = dtReceipt.Rows[0]["MAIL_BODY_RECEIPT"].ToString();
                        SendMail.MailBody = SendMail.MailBody.Replace("#Date#", dtReceipt.Rows[0]["RECEIPT_DATE"].ToString());// objECMessages.SMS_TranDate);
                        SendMail.MailBody = SendMail.MailBody.Replace("#VerfiedOn#", objECMessages.SMS_TranDate);
                        SendMail.Subject = "Payment successfully received for Receipt No :" + objECMessages.SMS_ReceiptNo.ToString();
                    }

                    //Organization Info
                    SendMail.MailBody = SendMail.MailBody.Replace("#Organization#", feeMaster.Orig_Name);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Address1#", feeMaster.Orig_Address1);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Address2#", feeMaster.Orig_Address2);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Phone1#", feeMaster.Orig_Phone1);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Phone2#", feeMaster.Orig_Phone2);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Email1#", feeMaster.Orig_Email1);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Email2#", feeMaster.Orig_Email2);
                    SendMail.MailBody = SendMail.MailBody.Replace("#Orig_Url#", feeMaster.Orig_Url);

                    if (SendMail.To.Trim() != "")
                    {
                        objCommon.SendEmail(SendMail);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    objCommon = null;
                    SendMail = null;
                }
            }
        }
        public void SendSMSOnLeave(EC_SMS_SendMessage objECMessages)
        {
            try
            {
                string strResponse = string.Empty;

                if (objECMessages.SMS_Section == "On Applying Leave Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Days#", Convert.ToString(objECMessages.SMS_Days));
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#From#", objECMessages.SMS_FromDate);
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#To#", objECMessages.SMS_ToDate);
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", objECMessages.SMS_LeaveId);
                }
                else if (objECMessages.SMS_Section == "SMS to parent On Applying Leave Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Days#", Convert.ToString(objECMessages.SMS_Days));
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#From#", objECMessages.SMS_FromDate);
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#To#", objECMessages.SMS_ToDate);
                }
                else if (objECMessages.SMS_Section == "Leave Request approved by Approver1")
                { 
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                }
                else if (objECMessages.SMS_Section == "Leave Request approved by Approver2")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Days#", Convert.ToString(objECMessages.SMS_Days));
                }
                else if (objECMessages.SMS_Section == "On Rejection of Leave Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                }
                else if (objECMessages.SMS_Section == "SMS to parent On the Approval of Leave Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Days#", Convert.ToString(objECMessages.SMS_Days));
                }
                else if (objECMessages.SMS_Section == "SMS to parent On the Rejection of Leave Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                }
                else if (objECMessages.SMS_Section == "SMS to Approver1 On Applying Leave Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                }
                else if (objECMessages.SMS_Section == "SMS to Approver2 On leave Request Approved by Approver1")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Days#", Convert.ToString(objECMessages.SMS_Days));
                }
                else if (objECMessages.SMS_Section == "SMS to Approver1 On leave Request Approved by Approver2")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Days#", Convert.ToString(objECMessages.SMS_Days));
                }
                else if (objECMessages.SMS_Section == "SMS to Approver1 On leave Request rejected by Approver2")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                }
                else if (objECMessages.SMS_Section == "SMS to Parent On Leave Request approved by Approver1")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#LeaveId#", Convert.ToString(objECMessages.SMS_LeaveId));
                }
                else if (objECMessages.SMS_Section == "SMS To Parent On Absent")
                {
                }
                else if (objECMessages.SMS_Section == "SMS To Parent On Dues")
                {
                }
                else if (objECMessages.SMS_Section == "SMS To Director On Attendance Complete")
                {
                }
                else if (objECMessages.SMS_Section == "SMS to Approver On Cancel Receipt Request")
                {
                    objECMessages.SMS_Msg = objECMessages.SMS_Msg.Replace("#Receipt#", Convert.ToString(objECMessages.SMS_LeaveId));  
                }
                
                //SMS LINK
                objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", objECMessages.SMS_MobileNo);
                objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);
                //SENDING SMS USING URL
                System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                strResponse = sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
            }
        }
        public void SendSMS(string strLink, string strMsg, string strMobile)
        {
            try
            {
                string strResponse = string.Empty;
                //SMS LINK
                strLink =  strLink.Replace("$MOBILE$", strMobile);
                strLink = strLink.Replace("$MSG$", strMsg);
                //SENDING SMS USING URL
                System.Net.WebRequest req = System.Net.WebRequest.Create(strLink);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                strResponse = sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
            }
        }
        public void SendSMS(EC_SMS_SendMessage objECMessages)
        {
            try
            {
                string strResponse = string.Empty;
                //SMS LINK
                objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MOBILE$", objECMessages.SMS_MobileNo);
                objECMessages.SMS_URLLink = objECMessages.SMS_URLLink.Replace("$MSG$", objECMessages.SMS_Msg);
                //SENDING SMS USING URL
                System.Net.WebRequest req = System.Net.WebRequest.Create(objECMessages.SMS_URLLink);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                strResponse = sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
