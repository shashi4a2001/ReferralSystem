using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLCMR;
using System.Configuration;
using System.Data;
using System.Collections;

public partial class Operation_Holidays : System.Web.UI.Page
{
    ObjPortalUser user;
    string _strAlertMessage = string.Empty;
    string _strMsgType = string.Empty;
    Hashtable HolidayList;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();
        try
        {
            //Validate User Authetication Starts
            if (Context.User.Identity.IsAuthenticated)
            {
                //Check User Session and Connection String 
                if (Session["PortalUserDtl"] != null)
                {
                    user = (ObjPortalUser)Session["PortalUserDtl"];

                    //Check Browser Address
                    if (objClsCommon.GetIPAddress() != user.IPAddress)
                    {
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();
                        Response.Redirect("login.aspx?Exp=Y", false);
                    }
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Response.Redirect("login.aspx?Exp=Y", false);
                    return;
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("login.aspx?Exp=Y", false);
                return;
            }

            if (IsPostBack == false)
            {
                ////Bind combo box
                //BindMailFormat();
                ////Get Configuration Settings value
                //getConfigurationSettings();
            }

            HolidayList = Getholiday();
            CMRCalendar.Caption = "CMR Holiday Calendar";
            CMRCalendar.FirstDayOfWeek = FirstDayOfWeek.Monday;
            CMRCalendar.NextPrevFormat = NextPrevFormat.FullMonth;
            CMRCalendar.TitleFormat = TitleFormat.Month;
            CMRCalendar.ShowGridLines = true;
            CMRCalendar.DayStyle.Height = new Unit(50);
            CMRCalendar.DayStyle.Width = new Unit(150);
            CMRCalendar.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            CMRCalendar.DayStyle.VerticalAlign = VerticalAlign.Middle;

            //CMRCalendar.OtherMonthDayStyle.BackColor = System.Drawing.Color.AliceBlue;

            //Check Page Accessibility
            if (user != null)
            {
                if (objClsCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
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
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objClsCommon = null;
        }
    }


    private Hashtable Getholiday()

    {

        Hashtable holiday = new Hashtable();

        holiday["1/1/2018"] = "New Year";

        holiday["1/5/2018"] = "Guru Govind Singh Jayanti";

        holiday["1/8/2018"] = "Muharam (Al Hijra)";

        holiday["1/14/2018"] = "Pongal";

        holiday["1/26/2018"] = "Republic Day";

        holiday["2/23/2018"] = "Maha Shivaratri";

        holiday["3/10/2018"] = "Milad un Nabi (Birthday of the Prophet";

        holiday["3/21/2018"] = "Holi";

        holiday["3/21/2018"] = "Telugu New Year";

        holiday["4/3/2018"] = "Ram Navmi";

        holiday["4/7/2018"] = "Mahavir Jayanti";

        holiday["4/10/2018"] = "Good Friday";

        holiday["4/12/2018"] = "Easter";

        holiday["4/14/2018"] = "Tamil New Year and Dr Ambedkar Birth Day";

        holiday["5/1/2018"] = "May Day";

        holiday["5/9/2018"] = "Buddha Jayanti and Buddha Purnima";

        holiday["6/24/2018"] = "Rath yatra";

        holiday["8/13/2018"] = "Krishna Jayanthi";

        holiday["8/14/2018"] = "Janmashtami";

        holiday["8/15/2018"] = "Independence Day";

        holiday["8/19/2018"] = "Parsi New Year";

        holiday["8/23/2018"] = "Vinayaka Chaturthi";

        holiday["9/2/2018"] = "Onam";

        holiday["9/5/2018"] = "Teachers Day";

        holiday["9/21/2018"] = "Ramzan";

        holiday["9/27/2018"] = "Ayutha Pooja";

        holiday["9/28/2018"] = "Vijaya Dasami (Dusherra)";

        holiday["10/2/2018"] = "Gandhi Jayanti";

        holiday["10/17/2018"] = "Diwali & Govardhan Puja";

        holiday["10/19/2018"] = "Bhaidooj";

        holiday["11/2/2018"] = "Guru Nanak Jayanti";

        holiday["11/14/2018"] = "Children's Day";

        holiday["11/28/2018"] = "Bakrid";

        holiday["12/25/2018"] = "Christmas";

        holiday["12/28/2018"] = "Muharram";

        return holiday;

    }

    protected void CMRCalendar_DayRender(object sender, DayRenderEventArgs e)

    {

        clsErrorLogger objclsErrorLogger = new clsErrorLogger();
        clsCommon objClsCommon = new clsCommon();
        try
        {
            if (HolidayList[e.Day.Date.ToShortDateString()] != null)

            {

                Literal literal1 = new Literal();

                literal1.Text = "<br/>";

                e.Cell.Controls.Add(literal1);

                Label label1 = new Label();

                label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];

                label1.Font.Size = new FontUnit(FontSize.Small);

                e.Cell.Controls.Add(label1);

            }
            //Check Page Accessibility
            if (user != null)
            {
                if (objClsCommon.checkPageAccessiblity(user.EmailId, System.IO.Path.GetFileName(Request.Path), user.ConnectionString) != "Y")
                    Response.Redirect("~/Operation/AccessDenied.aspx", false);
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
                objClsCommon.setLabelMessage(lblMessage, _strMsgType);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "AutoHide", "<script>HideLabel();</script>", false);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + _strAlertMessage + "');</script>", false);
            }
            objclsErrorLogger = null;
            objClsCommon = null;
        }

    }

    protected void CMRCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)

    {

        lblAction.Text = "Month changed to :" + e.NewDate.ToShortDateString();

    }

    protected void CMRCalendar_SelectionChanged(object sender, EventArgs e)

    {

        lblAction.Text = "Date changed to :" + CMRCalendar.SelectedDate.ToShortDateString();

    }
}
 