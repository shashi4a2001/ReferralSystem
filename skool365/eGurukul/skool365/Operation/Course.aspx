<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Operation_Course" %>

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
            <li class="" id="lstEntrytab"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">Course Entry</a></li>
            <li class="current" id="lstListtab"><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">Course(s) List</a></li>
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
                                    <strong>Segment</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCourseSegment" runat="server" Style="width: 200px; border: #aaaaaa 1px solid; background-color: #fbfbfb;">
                                        <asp:ListItem Text="Paramedical" Value="1"> Flight Attendant</asp:ListItem>
                                        <asp:ListItem Text="BasicScience" Value="2">Ground Handling</asp:ListItem>
                                        <asp:ListItem Text="Management" Value="3">Drone Pilot Training</asp:ListItem>

                                        <asp:ListItem Text="Paramedical" Value="4">Commercial Pilot</asp:ListItem>

                                        <asp:ListItem Text="Paramedical" Value="5">Aircraft Engineer</asp:ListItem>

                                        <asp:ListItem Text="Paramedical" Value="6">Airline Solutions</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblAccountStatus" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Course Code<span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCourseCode" runat="server" MaxLength="30" Width="350px" CssClass="hasDatepicker" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxtCourseCode" runat="server" ControlToValidate="txtCourseCode"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Course Name <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCourseName" runat="server" MaxLength="100" Width="350px" CssClass="hasDatepicker" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxtCourseName" runat="server" ControlToValidate="txtCourseName"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>Course Duration <span style="color: red;">*</span></strong>
                                </td>
                                <td style="height: 30px">
                                    <asp:TextBox ID="txtSemesterCount" runat="server" Width="350px" MaxLength="1" autocomplete="off" onkeypress="return fnCommonAcceptNumberOnly(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxtSemesterCount" runat="server" ControlToValidate="txtSemesterCount"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr id="rwStatus" runat="server" visible="false">
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="chkActivated" runat="server" Enabled="false" Checked="true" Text="Check if change course's status status to Active" />
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

                        <div style="float: right; font-weight: 700; color: #717171; font-size: 14px;">[Account Status :- A:Active I:Inactive]</div>
                        <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 380px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="Course_ID,Segment_id"
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" OnRowDeleting="grdStyled_RowDeleting"
                                Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdStyled_PageIndexChanging" Height="95%" OnRowDataBound="grdStyled_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="30px"
                                        ItemStyle-Height="10px" SelectText="<img src='../Common/Images/edit1.png' border='0' title='Edit Record'>"></asp:CommandField>

                                    <asp:BoundField DataField="COURSE_CODE" HeaderText="Course Code*" />
                                    <asp:BoundField DataField="COURSE_name" HeaderText="Course Name*" />
                                    <asp:BoundField DataField="SEGMENT_NAME" HeaderText="Branch *" />
                                    <asp:BoundField DataField="TOTAL_SEMESTER" HeaderText="Durations" />
                                    <asp:BoundField DataField="STS" HeaderText="Status" />
                                    <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                                    <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" />
                                    <asp:BoundField DataField="MODIFIED_BY" HeaderText="Last Upd By" />
                                    <asp:BoundField DataField="MODIFIED_DATE" HeaderText="Last Upd Date" />
                                    <asp:TemplateField HeaderText="Inactive">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="../Common/images/icon_delete.png" OnClientClick="if (!window.confirm('Are you sure you want to Inactive the Course?')) return false;"
                                                title='Inactive Course' />
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
