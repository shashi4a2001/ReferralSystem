<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigMails.aspx.cs" Inherits="Operation_ConfigMails"
    MasterPageFile="~/Operation/CRMOperationArea2.Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

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
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>

    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Configure Mails Format" />  class="contentwrapper" --%>
    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <img id="Img1" runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height: 60px;"> </img>
                    <%--<asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>
    <div id="contentwrapper" class="contentwrapper">

        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                <ContentTemplate>

                    <div style="margin-left: 2%; margin-top: 2%; height: 100%;">

                        <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>

                        <table>
                            <caption>

                                <tr>
                                    <td colspan="2" style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td width="100px">
                                        <b>Mail Type</b>
                                    </td>
                                    <td style="width: 620px">
                                        <asp:DropDownList ID="ddlMailFormat" runat="server" Width="628px" OnSelectedIndexChanged="ddlMailFormat_SelectedIndexChanged"
                                            AutoPostBack="True" Height="28px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Mail Body<span style="color: red;">*</span></b>
                                    </td>
                                    <td style="width: 620px">
                                        <asp:TextBox ID="txtMailBody" MaxLength="8000" runat="server" TextMode="MultiLine"
                                            Width="608px" Height="380px" autocomplete="off"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="ReqValtxtMailBody" runat="server" ControlToValidate="txtMailBody"
                                            CssClass="RequiredFieldValidatorColor" ForeColor="Red" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 5px;"></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td style="width: 620px; text-align: center">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btnblack" OnClick="btnSubmit_Click"
                                            Text="Update" Width="86px" />

                                    </td>
                                </tr>
                            </caption>

                        </table>


                    </div>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:HiddenField ID="hdId" runat="server" />
        </div>
    </div>
</asp:Content>
