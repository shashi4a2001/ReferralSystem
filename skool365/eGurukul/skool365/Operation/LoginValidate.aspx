<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginValidate.aspx.cs" Inherits="Operation_LoginValidate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="../Common/css/style.default.css" type="text/css" />
    <link href="../Common/css/blue/screen.css" rel="stylesheet" type="text/css" media="all" />
    <style type="text/css">
        .foot {
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

        .style1 {
            font-size: 26px;
        }

        #header {
            width: 100%;
            min-width: 960px;
            height: 65px;
            color: #fff;
            background-color: #1f3e8e !important;
        }

        .logon {
            font-size: 38px;
            font-weight: 600;
            text-transform: uppercase;
            padding: 24px 0 0 10px;
            background-color: #1f3e8e !important;
        }

        .logoTitle {
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
    </style>
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 6;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content_wrapper">
            <!-- Begin header -->
            <div id="header">
                <div class="logoTitle">
                    cmr
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
                <div class="loginbox">
                    <div class="loginboxinner">
                        <div class="logo">
                            <h1>
                                <span class="style1">Enter Captcha</span><span style="color: red;">*</span>
                            </h1>
                        </div>
                        <div class="username">
                            <div class="usernameinner">
                                <asp:TextBox ID="txtCaptcha" runat="server" Width="190px" MaxLength="8" TextMode="Password">
                                </asp:TextBox>

                                <asp:RequiredFieldValidator ID="regtxtCaptcha" runat="server" ControlToValidate="txtCaptcha"
                                    ErrorMessage="" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" Text="Validate" Height="50px" Width="309px"
                            OnClick="btnLogin_Click" OnClientClick="return GetSelectedTextValue()" />
                        <div style="background-color: white; color: Red; font-size: 14px; line-height: 25px; padding-left: 10px;">
                            <asp:Label ID="lblMessage" Text="" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="foot" style="height: 80px;">
                    <img src="../Common/Images/lnvmOnline.png" alt="Posts" height="50" width="300" />
                    <br />
                    &copy; COPYRIGHT © 2017. ALL RIGHTS RESERVED
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
