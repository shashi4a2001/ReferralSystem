using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using DLCMR;

public partial class Operation_ForgotPassword : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnClick_Click(object sender, EventArgs e)
    {
        clsCommon objclsCommon = new clsCommon();
        string ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["CMRConnection"]);
        ConnectionString = objclsCommon.DecryptData(ConnectionString);

        DLClsGeneric objDLGeneric = new DLClsGeneric();
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@UserId", txtUserId.Text.Trim());
        DataTable dt = objDLGeneric.SpDataTable ("usp_SelectClientByUserId", cmd, ConnectionString);
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.Rows[0]["LoginPassword"] = objclsCommon.DecryptData(dt.Rows[0]["LoginPassword"].ToString());

            bool status=SendMail(dt.Rows[0]["ClientId"].ToString(), dt.Rows[0]["LoginPassword"].ToString(), ConnectionString);
            if (status == true)
            {
                lblmsg.Text = "Your Login detail has been sent to your registered email id..";
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblmsg.Text = "Some problem in sending mail..Please try again later..";
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
    }

    private bool SendMail(string clientid, string password, string ConnectionString)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();

        bool status = false;
        try
        {
            
            ObjSendEmail SendMail = new ObjSendEmail();

            DLClsGeneric objDLGeneric = new DLClsGeneric();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ClientId", clientid);
            cmd.Parameters.AddWithValue("@TemplateType", "ForgotPassword");
            DataSet ds = objDLGeneric.SpDataSet("usp_SelectEmailMsg", cmd, ConnectionString);

            DataTable dtSMTP = ds.Tables[0];
            DataTable dtEmailTemplate = ds.Tables[1];

            if (ds != null && dtSMTP != null && dtEmailTemplate != null &&
                dtSMTP.Rows.Count > 0 && dtEmailTemplate.Rows.Count > 0 &&
                Convert.ToString(dtEmailTemplate.Rows[0]["ToEmailId"]) != ""
                )
            {
                if (dtEmailTemplate.Rows[0]["TemplateText"].ToString().Contains("(Password)"))
                {
                    dtEmailTemplate.Rows[0]["TemplateText"] = dtEmailTemplate.Rows[0]["TemplateText"].ToString().Replace("(Password)", password);
                }

                SendMail.From = Convert.ToString(dtSMTP.Rows[0]["SenderEmailID"]);
                //SendMail.CC = objClsCommon.getConfigKeyValue("EMAIL IDS FOR CC OF MAIL MESSAGE FOR EACH OPERATION", "EMAIL RECEPIENTS GROUP", user.ConnectionString);
                SendMail.SMTPHost = Convert.ToString(dtSMTP.Rows[0]["HostName"]);
                SendMail.SMTPPort = Convert.ToInt16(dtSMTP.Rows[0]["PortNo"]);
                SendMail.UserId = Convert.ToString(dtSMTP.Rows[0]["UserId"]);
                SendMail.Password = Convert.ToString(dtSMTP.Rows[0]["UserPwd"]);
                string enablessl = Convert.ToString(dtSMTP.Rows[0]["EnableSSL"]).ToLower();
                if (enablessl == "y" || enablessl == "yes" || enablessl == "1")
                    SendMail.SMTPSSL = true;
                else
                    SendMail.SMTPSSL = false;



                SendMail.Subject = Convert.ToString(dtEmailTemplate.Rows[0]["SubjectLine"]);
                SendMail.To = Convert.ToString(dtEmailTemplate.Rows[0]["ToEmailId"]);
                SendMail.MailBody = Convert.ToString(dtEmailTemplate.Rows[0]["TemplateText"]);

                SendMail.ConnectionString = ConnectionString;

                clsCommon objclsCommon = new clsCommon();
                objclsCommon.SendEmail(SendMail);
            }
        }
        catch (Exception ex)
        {
            status = false;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "Problem In Sending Mail To ClientId: " + clientid, ConnectionString, ex);

            return status;
        }

        status = true;
        return status;
    }

}