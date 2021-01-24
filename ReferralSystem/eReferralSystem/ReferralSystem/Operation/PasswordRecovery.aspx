<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage2.master" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="Operation_PasswordRecovery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
    <head>
        <!-- Website Title -->
        <title>Change Password</title>
        <link href="../Common/CSS/Admin.css" type="text/css" rel="Stylesheet" />
        <script type="text/javascript">
            function HideLabel() {
                var seconds = 2;
                setTimeout(function () {
                    document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
            };
        </script>
        <style type="text/css">
            .style1 {
                color: #FF0000;
                height: 100px;
            }

            .style2 {
                color: #000099;
            }
        </style>


        <style type="text/css">
            #wrapper {
                width: 100%;
                margin: 0 auto;
                padding: 0;
                overflow: auto;
                background-color: red;
                padding: 0;
                margin: 0;
                display: block;
                border: 1px solid white;
                position: fixed;
            }

            #rightcolumn {
                width: 100%;
                background-color: yellow;
                display: block;
                float: left;
                border: 1px solid white;
            }

            .FixedHeader {
                position: absolute;
                font-weight: bold;
            }
        </style>

    </head>
    <body>
        <div class="content_wrapper" style="height: 100%; width: 100%;">
            <div id="content">

                <asp:Panel ID="Panel1" runat="server">
                <div>

                    <table style="margin-left: 25%; margin-top: 5%; margin-bottom: 2%;">
                        <tr>
                            <td colspan="2">
                                <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                                    <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px">Recover password for 
                            </td>
                            <td style="height: 30px">
                                <asp:Label ID="lblUserId" runat="server" Font-Bold="true" ForeColor="black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px">New Password<span style="color:red;">*</span>
                            </td>
                            <td style="height: 30px">
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="hasDatepicker" MaxLength="20"
                                    Width="257px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPassword"
                                    CssClass="RequiredFieldValidatorColor" ForeColor="Red" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px">Confirm Password<span style="color:red;">*</span>
                            </td>
                            <td style="height: 30px">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="hasDatepicker" MaxLength="20"
                                    TextMode="Password" Width="257px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirmPassword"
                                    CssClass="RequiredFieldValidatorColor" ForeColor="Red" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <br />
                                 
                                    <asp:Button ID="btnSubmit" runat="server" Text="Set Password" OnClick="btnSubmit_Click" /> 
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <span class="style2"><strong>Password must contain</strong></span><br />
                                <span class="style1">*At least 7 chars<br />
                                    *At least 1 uppercase char (A-Z)<br />
                                    *At least 1 number (0-9)<br />
                                    *At least one special char</span><br />
                            </td>
                        </tr>
                    </table>
                </div>
                </asp:Panel>

                <asp:Panel ID="Panel2" runat="server" Visible="false" >
                    <table style="margin-left: 25%; margin-top: 5%; margin-bottom: 2%;">
                        <tr>
                            <td>
                                <br />
                                <br />
                                <br />
                                <div style="color:green; font-size:22px;">Your password has been set successfully...</div>
                                <div style="font-size:16px;">Go To <a href="../Login.aspx">Login Page</a></div>

                            </td>
                            </tr></table>
                     

                </asp:Panel>
            </div>
        </div>

    </body>
</asp:Content>
