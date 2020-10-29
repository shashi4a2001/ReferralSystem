<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Err.aspx.cs" Inherits="Operation_Err" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="../Common/css/style.default.css" type="text/css" />
    <link href="../Common/css/blue/screen.css" rel="stylesheet" type="text/css" media="all" />
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
            margin-top: 25px;
            position: absolute;
            bottom: 0;
            width: 100%;
            padding-top: 5px;
        }

        .style1
        {
            font-size: 26px;
        }

        #header
        {
            width: 100%;
            min-width: 960px;
            height: 65px;
            color: #fff;
            background-color: #1f3e8e !important;
        }

        .logon
        {
            font-size: 38px;
            font-weight: 600;
            text-transform: uppercase;
            padding: 24px 0 0 10px;
            background-color: #1f3e8e !important;
        }

        .logoTitle
        {
            font-size: 38px;
            font-weight: 600;
            float: left;
            padding-top: 0;
            margin-top: -8px;
            font-family: Arial, Helvetica, sans-serif;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-left: 10px;
            color: #fff;
            margin-top: 3px;
            text-shadow: 0 1px 0 #fff;
        }

        .logo
        {
            font-size: 38px;
            font-weight: 600;
            float: left;
            padding-top: 0;
            margin-top: -8px;
            font-family: Arial, Helvetica, sans-serif;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-left: 10px;
            color: #fff;
            margin-top: 10px;
            text-shadow: 0 1px 0 #fff;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="content_wrapper">
            <!-- Begin header -->
            <div id="header" style="height: 90px">
                <div class="logo" style="margin-top: 3px;">
                    <img src="../Common/Images/LNVMLOGO.png" height="70px" width="200px" />
                </div>

                <div style="text-align: left; margin: 16px 10px 0 0; font-size: 13px; margin-top: 15px; float: left;">

                    <asp:Label ID="lblOrganization" ForeColor="White" runat="server" Font-Bold="true" />
                    <br />
                    <asp:Label ID="lblWorkLocation" runat="server" ForeColor="White"></asp:Label>
                    <br />
                    <asp:Label ID="lblPhone" runat="server" ForeColor="White"></asp:Label>
                    |
                    <asp:Label ID="lblEmail" runat="server" ForeColor="White"></asp:Label>
                </div>


                <div style="text-align: left; margin: 16px 10px 0 0; font-size: 13px; margin-top: 30px; float: right;">
                    <a href="~/Login.aspx" id="changePwd" runat="server">LogIn</a>
                </div>
            </div>
        </div>
        <!-- End header -->
        <asp:ScriptManager ID="ManageOperation" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>

                    <center style="height: 100px;">
                        <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="Red" />
                        </div>
                    </center>
                </div>
                <div class="foot" style="position: absolute; bottom: 0; width: 100%; padding: 10px 0; text-transform: uppercase; height: 20px;">
                    &nbsp;&copy; COPYRIGHT © 2019. ALL RIGHTS RESERVED
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
