using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Text;
using System.IO;

public partial class Operation_SOVTree : System.Web.UI.Page
    {
        ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objCommon = new clsCommon();

        string _strAccountName = string.Empty;

        try
        {
            user = new ObjPortalUser();

            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];
                    //Check Browser Address
                    if (objCommon.GetIPAddress() != user.IPAddress)
                    {
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        return;
                    }
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    return;
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                return;
            }

            if (Page.IsPostBack == false)
            {
                //Display SOV Files
                _strAccountName = Request.QueryString["Acc"].ToString();
                DirectoryInfo rootInfo = new DirectoryInfo(Server.MapPath("~/Operation/Uploads/ManageRequests/" + _strAccountName + "/"));
                this.PopulateTreeView(rootInfo, null);
            }
        }
        catch (Exception ex)
        {
            _strMsgType = "Error";
            _strAlertMessage = ex.Message;
            objclsErrorLogger.WriteError(System.IO.Path.GetFileName(Request.Path), "", user == null ? "" : user.ConnectionString, ex);
            Response.Redirect("~/Operation/Err.aspx");
        }
        finally
        {
            //Alert Message
            if (_strAlertMessage != string.Empty && _strAlertMessage.Trim() != "")
            {
                lblMessage.Text = _strAlertMessage;
                objCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }

            objCommon = null;
            objclsErrorLogger = null;

        }
    }

        private void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode)
        {
            foreach (DirectoryInfo directory in dirInfo.GetDirectories())
            {
                TreeNode directoryNode = new TreeNode
                {
                    Text = directory.Name,
                    Value = directory.FullName
                };

                if (treeNode == null)
                {
                    //If Root Node, add to TreeView.
                    trSOV.Nodes.Add(directoryNode);
                }
                else
                {
                    //If Child Node, add to Parent Node.
                    treeNode.ChildNodes.Add(directoryNode);
                }

                //Get all files in the Directory.
                string strNavigateURL = string.Empty;
                foreach (FileInfo file in directory.GetFiles())
                {
                    //Add each file as Child Node.
                    TreeNode fileNode = new TreeNode
                    {
                        Text = file.Name,
                        Value = file.FullName,
                        Target = "_blank",
                        NavigateUrl = (new Uri(Server.MapPath("~/Operation/"))).MakeRelativeUri(new Uri(file.FullName)).ToString(),
                    };

                    directoryNode.ChildNodes.Add(fileNode);
                }
                PopulateTreeView(directory, directoryNode);
            }
        }
    }
 