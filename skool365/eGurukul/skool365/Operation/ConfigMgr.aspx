<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigMgr.aspx.cs" Inherits="Operation_ConfigMgr"
    MasterPageFile="~/Operation/CRMOperationArea.Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">

        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        }

        function IsNumeric(val) {

            if (!(event.keyCode > 47 && event.keyCode < 58)) {
                alert("Enter Numerics Only")
                event.srcElement.focus();
                return false;
            }
        }

        function Confirm(Msg) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(Msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            while (confirm_value.lastChild) {
                confirm_value.removeChild(confirm_value.lastChild);
            }
            document.forms[0].appendChild(confirm_value);
        }

        //Used to display Modal dailog while processing of records
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }

    </script>
     <style type="text/css">
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <img runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height: 60px;"> </img>
                    <%--<asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>
    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Configuration Manager" />--%>
    <div id="contentwrapper" class="contentwrapper">
        <div id="dvMessage" runat="server">
        </div>
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
                <ContentTemplate>
                    <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="margin-left: 2%; margin-top: 1%;">
                        <table>
                            <caption>
                                <tr>
                                    <td colspan="2" bgcolor="#CCCCCC" style="color: #000000; text-align: center; padding: 3px 0;">
                                        <strong>SMTP Server Detail</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        <b>Host <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSMTPHost" runat="server" MaxLength="100" Enabled="True" ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="regtxtSMTPHost" runat="server" ControlToValidate="txtSMTPHost"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Port <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSMTPPort" runat="server" MaxLength="4" Enabled="True" ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="regtxtSMTPPort" runat="server" ControlToValidate="txtSMTPPort"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>User ID <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSMTPUSerID" runat="server" MaxLength="100" Enabled="True" ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regtxtSMTPUSerID" runat="server" ControlToValidate="txtSMTPUSerID"
                                            ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Password <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSMTPPassword" runat="server" MaxLength="30" Enabled="True" TextMode="Password"
                                            ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="regtxtSMTPPassword" runat="server" ControlToValidate="txtSMTPPassword"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="chkEnabelSSL" Text="SSL Applied" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" bgcolor="#CCCCCC" style="color: #000000; text-align: center; padding: 3px 0;">
                                        <strong>POP3 Server Detail </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Host <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPOP3Host" runat="server" MaxLength="100" Enabled="True" ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtPOP3Host" runat="server" ControlToValidate="txtPOP3Host"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Port <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPOP3Port" runat="server" MaxLength="5" Enabled="True" ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtPOP3Port" runat="server" ControlToValidate="txtPOP3Port"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>User ID <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPOP3User" runat="server" MaxLength="100" Enabled="True" ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="exptxtPOP3User" runat="server" ControlToValidate="txtPOP3User"
                                            ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Password <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPOP3Password" runat="server" MaxLength="30" Enabled="True" TextMode="Password"
                                            ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPOP3Password"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="chkPOP3SSL" Text="SSL Applied" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <b>Default Port</b>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkPOPByDefualt" Text="Use By Default Port" runat="server" />
                                        <asp:TextBox ID="txtDefaultPOP3Port" Width="340px" runat="server" MaxLength="5" Enabled="True"
                                            ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--  <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkEditMasterData" runat="server" Text="Allow Modification of Master Records"
                                            Checked="false"></asp:CheckBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="2" bgcolor="#CCCCCC" style="color: #000000; text-align: center; padding: 3px 0;">
                                        <strong>Windows Service</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        <b>Windows Service Timer <span style="color: red;">*</span></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWidnowsTimer" runat="server" MaxLength="4" Enabled="True"
                                            ForeColor="#000099" autocomplete="off"></asp:TextBox>
                                        (In Minutes)
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWidnowsTimer"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" bgcolor="#CCCCCC" style="color: #000000; text-align: center; padding: 3px 0;">
                                        <strong>Miscellaneous Service</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        <b>SOV Files Extenstion</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSOVExtesntion" runat="server" MaxLength="100" Enabled="True"
                                            ForeColor="#000099" ToolTip="" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="100px">
                                        <b>SOV Files Size in  KB</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSOVFileSize" runat="server" MaxLength="5" Enabled="True"
                                            ForeColor="#000099" ToolTip="" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="100px">
                                        <b>Restricted Input
                                            <br />
                                            Chatacters (Separated by ~)</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInputRestVal" runat="server" MaxLength="20" Enabled="True"
                                            ForeColor="#000099" ToolTip="" autocomplete="off"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="100px">
                                        <b>Relogin (Active Login state)
                                            <br />
                                            using Captcha</b>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkAplliedLoginCaptcha" Text="" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td width="100px">
                                        <b>Submission of Max
                                            <br />
                                            Request in a due date</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaxRequestSubmit" runat="server" MaxLength="2" Enabled="True"
                                            ForeColor="#000099" ToolTip="" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqValtxtMaxRequestSubmit" runat="server" ControlToValidate="txtMaxRequestSubmit"
                                            ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btnblack" OnClick="btnSubmit_Click"
                                            Text="Update Configuration" Style="margin-left: 150px;" CausesValidation="true" />
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
