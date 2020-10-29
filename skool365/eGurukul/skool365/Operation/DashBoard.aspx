<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Operation_DashBoard" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
    <title></title>
    <link href="../Common/css/blue/screen.css" rel="stylesheet" type="text/css" media="all" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        .foot
        {
            background-color: #1f3e8e !important;
            height: 2px;
            color: #FFFFFF;
            font-size: 12px;
            text-align: center;
            border-top: #7b7a7a dashed 1px;
            line-height: 15px;
            margin-top: 90%;
            position: sticky;
            bottom: 0;
            width: 100%;
            padding-top: 5px;
        }

        .logo
        {
            font-size: 38px;
            font-weight: 600;
            float: left;
            padding-top: 0;
            margin-top: -8px;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-left: 10px;
            font-family: Arial, Helvetica, sans-serif;
            color: #fff;
            text-shadow: 0 1px 0 #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content_wrapper">
            <!-- Begin header -->
            <div id="header" style="height: 90px;">
                <div class="logo" style="margin-top: 3px;">
                    <img src="../Common/Images/LNVMLOGO.png" height="70px" />
                </div>
                <div style="text-align: left; margin: 10px 10px 0 0; font-size: 13px; margin-top: 15px; float: right;">

                    <%--<a href="UWSelect.aspx" id="SelectUW" runat="server">Select UW |</a>--%>
                    Role:
                <asp:Label ID="lblUserRole" runat="server" Font-Bold="true" ForeColor="White"></asp:Label>
                    <img src="../Common/images/icon_online.png" alt="Online" class="mid_align" />
                    Welcome <a href="#" class="active">
                        <asp:Literal ID="ltrlUserName" runat="server"></asp:Literal></a> | <a href="LogOut.aspx">Logout</a> | <a href="ChangePassword.aspx" id="changePwd" runat="server">Change Password</a>

                    <br />

                    Last Logged In :
                        <asp:Label ID="lblLastLogIn" ForeColor="Black" runat="server"  />
                    |
                    Last Password Changed :
                        <asp:Label ID="lblLastPasswordChange" ForeColor="Black" runat="server"  />
                    <br />

                    Organization:
                        <asp:Label ID="lblOrganization" runat="server" ForeColor="Orange" Font-Bold="true"></asp:Label>
                    | Work Location :
                        <asp:Label ID="lblWorkLocation" runat="server" ForeColor="Orange"></asp:Label>
                </div>
            </div>


            <!-- End header -->

            <!-- Begin left panel -->
            <div id="left_menu">
            </div>
            <!-- End left panel -->
            <!-- Begin content -->
            <div id="content">
                <div style="position: fixed; top: 8em; z-index: 999999; right: 33px;">
                    <img src="../Common/images/shortcut/dashboardwatermark.png" alt="Access Panel" />
                </div>
                <div class="inner">
                    <h1 style="color: #1f3e8e!important;">My Dashboard <span style="font-size: 16px; color: #000; font-style: italic;">[
                        <asp:Literal ID="ltrlDashBoardUserName" runat="server"></asp:Literal>
                        ]</span>
                    </h1>
                    <hr />
                    <div>
                        <ul id="DashBoardShortcut" runat="server">
                        </ul>
                    </div>
                </div>
                <%--   <div style="height: 400px;">
                </div>
                <br class="clear" />--%>
                <!-- Begin footer -->
                <!-- End footer -->
            </div>

            <%--  <div class="foot" style="height: 80px;">
                <img src="../Common/Images/lnvmOnline.png" alt="Posts" height="50" width="650" />
                <br />
                &copy; COPYRIGHT © 2019. ALL RIGHTS RESERVED
            </div>--%>
            <!-- End content -->

        </div>
    </form>
</body>
</html>
