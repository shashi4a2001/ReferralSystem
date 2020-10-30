<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Operation_Login" ErrorPage="~/Operation/Err.aspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
        }

        .header
        {
            height: 58px;
            background-color: #1f3e8e !important;
        }

            .header img
            {
                padding: 15px;
                height: 33px;
                width: 57px;
            }

        #wrapper
        {
            margin-left: 50%;
            width: 75%;
            margin: 0 auto;
            background: url(Common/LoginPageFiles/images/lnvmOnline.png) left no-repeat;
            height: 500px;
            margin-top: 1%;
        }

        .rightpanel
        {
            width: 420px;
            float: right;
            margin-right: 3%;
            margin-top: 7%;
        }

        .rigteditpan
        {
            background: url(Common/LoginPageFiles/images/loginpanel.png) no-repeat;
            height: 360px;
            margin-top: 12px;
        }

        .username
        {
            padding-top: 6em;
            padding-left: 3em;
        }

        .OLDSTD
        {
            margin-right: 10%;
            background: url(Common/LoginPageFiles/images/toppanel.png) no-repeat;
            height: 95PX;
            width: 417PX;
        }

        .username input[type='text'], input[type='password']
        {
            border: #8ac9f6 1px solid;
            border-radius: 4px; /* height: 36px; */
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f2f2f4;
            width: 310px;
            margin-bottom: 15px;
            padding: 8px;
        }

        .spantxt
        {
            width: 130px;
            float: left;
            font-family: Arial, Helvetica, sans-serif;
            color: #2b2827;
        }

        .btn
        {
            background: url(Common/LoginPageFiles/images/loginbtn.png) no-repeat;
            width: 76px;
            height: 35px;
            float: right;
            color: #FFFFFF;
            margin-top: 17px;
            margin-right: 4.5em;
            border: none;
        }

        .foot
        {
            background-color: #1f3e8e !important;
            height: 2px;
            color: #FFFFFF;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: center;
            border-top: #14285d dashed 1px;
            line-height: 15px;
            margin-top: 25px;
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

    <script type="text/javascript">
        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function validateLogin() {
            if (document.getElementById("<%=txtEmail.ClientID %>").value.trim() == '' || document.getElementById("<%=txtPassword.ClientID %>").value.trim() == '') {
                return false;
            }
            return true;
        };
    </script>
</head>
<body>
    <div class="header">
        <div class="logo" style="margin-top: 3px;">
           <asp:Label ID="lblWorkLocation"  runat="server" />
        </div>
    </div>
    <div id="wrapper">
        <div class="rightpanel">
            <div class="rigteditpan">
                <div class="username">
                    <form id="form1" runat="server">
                        
                        <div style="padding-top: 12px; display: none;">
                            <label class="spantxt" for="password">
                                <b>Password1</b></label>
                        </div>
                        <div>
                            <%-- <label class="spantxt" for="username">
                            <b>Email Id</b></label>--%>
                            <asp:TextBox ID="txtEmail" runat="server" size="45" title="Email ID" MaxLength="100"
                                AutoCompleteType="Disabled" class="validate[required] text-input" placeholder="Login ID"
                                tooltipText="Type Your Login ID" autocomplete="off"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="regtxtEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                        </div>
                       
                        <div>
                            <%-- <label class="spantxt" for="username">
                            <b>Password</b></label>--%>
                            <asp:TextBox ID="txtPassword" runat="server" size="45" title="Password" MaxLength="20"
                                AutoCompleteType="Disabled" class="validate[required] text-input" tooltipText="Type Your Password"
                                placeholder="Password" TextMode="Password"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="regtxtPassword" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <div>
                            
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" OnClientClick="return validateLogin();"
                                ValidationGroup="Login" CausesValidation="true" />
                            
                        </div>
                        <div style="clear: both;">
                        </div>
                        <div>
                            <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>

    <div class="foot" style="position: absolute; bottom: 0; width: 100%; padding: 10px 0; text-transform: uppercase; height: 20px;">
        &nbsp;&copy; COPYRIGHT © 2019. ALL RIGHTS RESERVED
    </div>
</body>
</html>
