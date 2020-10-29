<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserPrivileges.aspx.cs" Inherits="Operation_UserPrivileges"
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
    </script>

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

    <div id="Div1" class="contentwrapper">
        <div style="position: center;">
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <div>
                        <img runat="server" src="../Common/Images/waitTran.gif"
                            style="position: static; width: 60px; height: 60px;"> </img>
                        <%--<asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />--%>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>

    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="User Privileges" />--%>
    <div id="contentwrapper" class="contentwrapper">
        <div id="dvMessage" runat="server">
        </div>
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
                <ContentTemplate>
                    <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="margin-left: 2%;">
                        <div class="stdform stdform2" style="margin-top: 10px;">
                            <p>
                                <label>
                                    User(s) :
                                </label>
                                <span class="field">
                                    <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </div>

                        <div class="maingrid" style="margin-top: 10px;">
                            <asp:TreeView ID="trvMenu" runat="server" ShowCheckBoxes="Leaf"
                                OnTreeNodeCheckChanged="trvMenu_TreeNodeCheckChanged" ShowExpandCollapse="false">
                                <ParentNodeStyle Font-Bold="True" ForeColor="#333300" />
                                <RootNodeStyle Font-Bold="True" ForeColor="#CC3300" />
                            </asp:TreeView>
                        </div>
                        <div>
                            <br />
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Assign Access Right" OnClick="btnSubmit_Click"
                                Visible="true" />

                        </div>

                        <triggers>
                <asp:AsyncPostBackTrigger  ControlID="trvMenu" EventName="TreeNodeCheckChanged"/></triggers>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
