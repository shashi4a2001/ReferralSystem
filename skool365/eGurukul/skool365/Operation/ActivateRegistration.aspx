<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="ActivateRegistration.aspx.cs" Inherits="Operation_ActivateRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">

    <script type="text/javascript">
        function HideLabel() {
            var second = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, second * 1000);
        };

        function ClearInputSearch() {
            if (document.getElementById('<%=txtSearch.ClientID %>').value == "") {
                return false;
            }
            else {
                document.getElementById('<%=txtSearch.ClientID %>').value = "";
                return true;
            }
        }

        //Confirmation Message
        function Confirm(MSG) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(MSG)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function setFocusToEntryTab(id) {
            if (id == "EntryTab") {
                document.getElementById("basicform").style.display = 'block';
                document.getElementById("UserList").style.display = 'none';
                //document.getElementById("txtUSerID").focus();
                document.getElementById("lstListtab").className = 'current';
                document.getElementById("lstEntrytab").className = '';
            }
            if (id == "ListUser") {
                document.getElementById("UserList").style.display = "block";
                document.getElementById("basicform").style.display = "none";
                document.getElementById("lstListtab").className = '';
                document.getElementById("lstEntrytab").className = 'current';
            }
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
        .MyCalendar .ajax__calendar_container
        {
            border: 1px solid #646464;
            background-color: lemonchiffon;
            color: red;
        }

        .MyCalendar .ajax__calendar_other .ajax__calendar_day, .MyCalendar .ajax__calendar_other .ajax__calendar_year
        {
            color: black;
        }

        .MyCalendar .ajax__calendar_hover .ajax__calendar_day, .MyCalendar .ajax__calendar_hover .ajax__calendar_month, .MyCalendar .ajax__calendar_hover .ajax__calendar_year
        {
            color: black;
        }

        .MyCalendar .ajax__calendar_active .ajax__calendar_day, .MyCalendar .ajax__calendar_active .ajax__calendar_month, .MyCalendar .ajax__calendar_active .ajax__calendar_year
        {
            color: black;
            font-weight: bold;
        }

        .modalPopup
        {
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

    <div>
        <ul class="hornav">
            <li class="" id="lstListtab"><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">Pending/Deactivated Registrations</a></li>
        </ul>
    </div>

    <div id="contentwrapper" class="subcontent">
        <div id="UserList" class="subcontent" style="display: block;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
                        <div style="margin-top: 20px">
                            <span>
                                <asp:RadioButton ID="rdoPendindg" Text="Pending" runat="server" Checked="true" GroupName="status"></asp:RadioButton>
                                <asp:RadioButton ID="rdoDeactivated" Text="Deactivated" runat="server" Checked="false" GroupName="status"></asp:RadioButton>
                                <asp:RadioButton ID="rdoActive" Text="Active" runat="server" Checked="false" GroupName="status"></asp:RadioButton>
                            </span>
                            <span><b style="font-size: 14px; padding-left: 7px;">*Search </b>
                                <asp:TextBox ID="txtSearch" Text="" MaxLength="100" runat="server" autocomplete="off">
                                </asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="*Search" CausesValidation="false"
                                    OnClick="btnSearch_Click" Visible="true" />
                                <asp:Button ID="btnClear" Width="70px" runat="server" Text="Clear" ToolTip="Reset the grid with default search" CausesValidation="false" OnClientClick=" return ClearInputSearch();" OnClick="btnSearch_Click" />
                            </span>
                        </div>
                        <div style="float: right; font-weight: 700; color: #717171; font-size: 14px;">
                            <asp:Label ID="lblAccInfo" runat="server" /></div>
                        <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 380px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="SIGNUP_ID"
                               OnRowDeleting   ="grdStyled_RowDeleting"
                                Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdStyled_PageIndexChanging" 
                                Height="95%" OnRowDataBound="grdStyled_RowDataBound" OnRowCommand="grdStyled_RowCommand" GridLines="Both">
                                <EmptyDataRowStyle HorizontalAlign="Left" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Confirmation">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ConfirmButton" CausesValidation="false" CommandName="Confirm"
                                                ImageUrl="~/Common/images/icon_accept.png" OnClientClick="if (!window.confirm('Are you sure you want to confirm the registration?')) return false;"
                                                title='Confirm Registration' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Activate">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="~/Common/Images/icon_accept.png" OnClientClick="if (!window.confirm('Are you sure you want to Active the registration?')) return false;"
                                                title='Activate Registration' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="STS_DESC" HeaderText="Status" ItemStyle-Width="120px" />
                                    <asp:BoundField DataField="SIGNUP_ID" HeaderText="Signup ID" ItemStyle-Width="120px" />
                                    <asp:BoundField DataField="STUDENT_NAME" HeaderText="Student Name*" ItemStyle-Width="300px" />
                                    <asp:BoundField DataField="COURSE_NAME" HeaderText="Course Name*" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="MOBILE_NO" HeaderText="Mobile*" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="EMAIL_ID" HeaderText="Email*" HeaderStyle-Width="300px"
                                        ItemStyle-Width="300px" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                    <asp:BoundField DataField="FATHER_NAME" HeaderText="Father Name*" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="MOTHER_NAME" HeaderText="Mother Name*" ItemStyle-Width="200px" HeaderStyle-Wrap="true" />

                                    <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="MODIFIED_BY" HeaderText="Modified By" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="MODIFIED_ON" HeaderText="Modified On" ItemStyle-Width="100px" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdStyled" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
