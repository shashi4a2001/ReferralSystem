    <%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Operation_ManageUsers" %>

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


        /////////////////////////////////////////////////
        ///Function Name: fnCommonAcceptNumberOnly
        ///Written By   : Shashi
        ///Return Type  : True or False
        ///               If Any Character Except (0-9 and Enter and Back Space 
        ///               Then Return False Else Return True)                    
        ///Task         : Accept Number Only Also Accept Back Space and Enter Key
        /////////////////////////////////////////////////
        //---------------------------------------------
        //Method to calling to this procedure
        //onkeypress="return fnCommonAcceptNumberOnly(event)"
        //---------------------------------------------
        function fnCommonAcceptNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 32)
                return true;
            if (charCode == 13)
                return true;

            if ((charCode >= 48 && charCode <= 57))
                return true;
  
            return false;
        }

    </script>

    <style type="text/css">
        .MyCalendar .ajax__calendar_container {
            border: 1px solid #646464;
            background-color: lemonchiffon;
            color: red;
        }

        .MyCalendar .ajax__calendar_other .ajax__calendar_day, .MyCalendar .ajax__calendar_other .ajax__calendar_year {
            color: black;
        }

        .MyCalendar .ajax__calendar_hover .ajax__calendar_day, .MyCalendar .ajax__calendar_hover .ajax__calendar_month, .MyCalendar .ajax__calendar_hover .ajax__calendar_year {
            color: black;
        }

        .MyCalendar .ajax__calendar_active .ajax__calendar_day, .MyCalendar .ajax__calendar_active .ajax__calendar_month, .MyCalendar .ajax__calendar_active .ajax__calendar_year {
            color: black;
            font-weight: bold;
        }

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
                        style="position: static; width: 60px; height:60px;"> </img>
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
            <li class="" id="lstEntrytab"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">New User</a></li>
            <li class="current" id="lstListtab"><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">User(s) List</a></li>
        </ul>
    </div>

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>

                    <div style="height: 95%;">
                        <br />
                        <table width="60%" style="margin-left: 250px;" cellspacing="8px;">

                            <tr>
                                <td>
                                    <strong>User Role</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUserRole" runat="server" Style="width: 200px; border: #aaaaaa 1px solid; background-color: #fbfbfb;">
                                        <asp:ListItem Text="Underwriter" Value="1">Underwriter</asp:ListItem>
                                        <asp:ListItem Text="Underwriting" Value="2">Underwriting Technician</asp:ListItem>
                                        <asp:ListItem Text="Cat Modeler" Value="3">Cat Modeler</asp:ListItem>
                                        <asp:ListItem Text="Underwriting Officer" Value="4">Underwriting Officer</asp:ListItem>
                                        <asp:ListItem Text="Cat Manager" Value="5">Cat Manager</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblAccountStatus" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" />
                                </td>
                            </tr>
                            <tr id="rwUser" runat="server" visible="false">
                                <td>
                                    <strong>User ID <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUSerID" runat="server" MaxLength="15" Width="350px" CssClass="hasDatepicker" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxtUSerID" runat="server" ControlToValidate="txtUSerID"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>User Name <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="100" Width="350px" CssClass="hasDatepicker" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxtName" runat="server" ControlToValidate="txtName"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>Email ID <span style="color: red;">*</span></strong>
                                </td>
                                <td style="height: 30px">
                                    <asp:TextBox ID="txtEEmail" runat="server" Width="350px" MaxLength="100" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxtEEmail" runat="server" ControlToValidate="txtEEmail"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regexptxtEEmail" runat="server" ControlToValidate="txtEEmail"
                                        ErrorMessage="Invalid E-Mail Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="RegularExpressionValidatorColor" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Mobile No</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobileNo" runat="server" Width="350px" MaxLength="10" autocomplete="off" 
                                        onkeypress="return fnCommonAcceptNumberOnly(event)" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="rwBranch" runat="server" visible="false">
                                <td>
                                    <strong>Submission Branch </strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubmissionBranch" runat="server" Width="350px" MaxLength="256" autocomplete="off"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="rwRegion" runat="server" visible="false">
                                <td>
                                    <strong>Region </strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtregion" runat="server" Width="350px" MaxLength="256" autocomplete="off"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="rwStatus" runat="server" visible="false">
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="chkActivated" runat="server" Enabled="false" Checked="true" Text="Check if change user's account status to Active" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="chkByPassAccountLock" ForeColor="Red" runat="server" Enabled="true" Checked="false" Text="Check if Account Lock check not applicable" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btnblack"
                                        OnClientClick="if (!window.confirm('Are you sure you want to save/update this record')) return false;" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btngray" />
                                    <asp:HiddenField ID="hdId" runat="server" />
                                    <asp:HiddenField ID="hdUserGroupId" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div id="UserList" class="subcontent" style="display: none;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-top: 20px">
                            <span><b style="font-size: 14px; padding-left: 7px;">*Search </b>
                                <asp:TextBox ID="txtSearch" Text="" MaxLength="100" runat="server" autocomplete="off">
                                </asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="*Search" CausesValidation="false"
                                    OnClick="btnSearch_Click" Visible="true" />
                                <asp:Button ID="btnClear" Width="70px" runat="server" Text="Clear" ToolTip="Reset the grid with default search" CausesValidation="false" OnClientClick=" return ClearInputSearch();" OnClick="btnSearch_Click" />
                            </span>
                        </div>

                        <div style="float: right; font-weight: 700; color: #717171; font-size: 14px;">[Account Status :- A:Active I:Inactive L:Account Locked]</div>
                        <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 380px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="UID,RoleId"
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" OnRowDeleting="grdStyled_RowDeleting"
                                Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdStyled_PageIndexChanging" Height="95%" OnRowDataBound="grdStyled_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="30px"
                                        ItemStyle-Height="10px" SelectText="<img src='../Common/Images/edit1.png' border='0' title='Edit Record'>"></asp:CommandField>

                                    <asp:BoundField DataField="User_ID" HeaderText="User ID*" />
                                    <asp:BoundField DataField="User_Name" HeaderText="User Name*" />
                                    <asp:BoundField DataField="RoleName" HeaderText="Role*" />
                                    <asp:BoundField DataField="EmailID" HeaderText="Email" />
                                    <asp:BoundField DataField="Mobile_no" HeaderText="Mobile No" />
<%--                                    <asp:BoundField DataField="SubmissionBranch" HeaderText="Submission Branch*" />
                                    <asp:BoundField DataField="Region" HeaderText="Region*" />--%>
                                    <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                    <asp:BoundField DataField="FIRST_TIME_LOGGEDIN" HeaderText="First Time Logged In" />
                                    <asp:BoundField DataField="NO_LOCKCHECK" HeaderText="By Pass Account Lock Check" />
                                    <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                                    <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" />
                                    <asp:BoundField DataField="MODIFIED_BY" HeaderText="Last Upd By" />
                                    <asp:BoundField DataField="MODIFIED_DATE" HeaderText="Last Upd Date" />
                                    <asp:TemplateField HeaderText="Inactive">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="../Common/images/icon_delete.png" OnClientClick="if (!window.confirm('Are you sure you want to Inactive the user?')) return false;"
                                                title='Inactive Usert' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
