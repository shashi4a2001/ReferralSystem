<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RolePrivileges.aspx.cs" Inherits="Operation_RolePrivileges"
    MasterPageFile="~/Operation/CRMOperationArea.Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">

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

    <script type="text/javascript">
        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

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
    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Role Privileges" />--%>
    <div id="contentwrapper" class="contentwrapper">
        <div id="dvMessage" runat="server">
        </div>
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="trvMenu" EventName="TreeNodeCheckChanged"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="trvMenu" EventName="TreeNodeCheckChanged"></asp:AsyncPostBackTrigger>
                </Triggers>
                <ContentTemplate>
                    <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="margin-left: 2%;">
                        <div class="stdform stdform2" style="margin-top: 10px; height: 80%">

                            <label>
                                Role Name :
                            </label>
                            <span class="field">
                                <asp:DropDownList ID="ddlUserRole" runat="server" AutoPostBack="True"
                                    class="chzn-select" OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged">
                                </asp:DropDownList>
                            </span>

                        </div>
                        <%-- <div class="maingrid" style="margin-top: 20px;">--%>
                        <table>
                            <tr>
                                <td>
                                    <asp:TreeView ID="trvMenu" runat="server"
                                        ShowCheckBoxes="Leaf" OnTreeNodeCheckChanged="trvMenu_TreeNodeCheckChanged" ShowExpandCollapse="false">
                                        <ParentNodeStyle Font-Bold="True" ForeColor="#333300" />
                                        <RootNodeStyle Font-Bold="True" ForeColor="#CC3300" />
                                    </asp:TreeView>

                                </td>
                            </tr>



                        </table>
                        <hr />

                        <table>
                            <tr>
                                <td><b>Landing Page (After Log In)</b></td>
                                <td style="margin-left: 20px;">
                                    <asp:DropDownList ID="ddlLandingPage" runat="server" AutoPostBack="True"
                                        class="chzn-select">
                                        <asp:ListItem Text="Dashboard" Value="Operation/dashboard.aspx" />
                                       <%-- <asp:ListItem Text="Manage Request" Value="Operation/MngRequests.aspx" />
                                        <asp:ListItem Text="Selet UW" Value="Operation/UWSelect.aspx" />--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td><b>Accessible in Dashboard</b></td>
                                <td style="margin-left: 20px;">
                                    <asp:CheckBox ID="chkChangePassword" Text="Change Password" runat="server" class="chzn-select" ForeColor="#3366FF" />
                                    <asp:CheckBox ID="chkSelectUW" Text="Select Acting User" Visible="false" runat="server" class="chzn-select" ForeColor="#3366FF" />
                                    <asp:CheckBox ID="chkAccessDashboard" Text="Access Dashboard" runat="server" class="chzn-select" ForeColor="#3366FF" />
                                </td>
                            </tr>

                            <tr style="height: 10px">
                                <td></td>
                                <td></td>
                            </tr>
                        </table>

                        <div>
                            <asp:Button ID="btnSubmit" runat="server" Text="Assign Access Right" OnClick="btnSubmit_Click"
                                Visible="true" />
                        </div>

                    </div>
                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="trvMenu" EventName="TreeNodeCheckChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


</asp:Content>
