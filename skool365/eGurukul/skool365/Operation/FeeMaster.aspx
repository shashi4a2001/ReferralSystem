﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="FeeMaster.aspx.cs" Inherits="Operation_FeeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
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

        //Total Amount Calculation
        function AnnualFee() {
            var admission = 0;
            var gv = document.getElementById("<%=grdAdmissionFee.ClientID %>");
            var gvRowCount = gv.rows.length;
            var txtAmount;
            var i = 1;

            for (i; i <= gvRowCount - 1; i++) {
                txtAmount = $("input[id*=txtAdmnAmount]");
                admission = parseInt(admission) + parseInt(gv.rows[i].cells[1].childNodes[1].value);
            }

            //total Admission Fee
            document.getElementById('<% = txtTotalAnnualFee.ClientID %>').value = admission;
            document.getElementById('<% = hdValAdmission.ClientID %>').value = admission;
            return true;
        }

        function SemesterFee() {
            var semesterfee = 0;
            var gv = document.getElementById("<%=grdSemesterFee.ClientID %>");
            var gvRowCount = gv.rows.length;
            var txtAmount;
            var i = 1;

            for (i; i <= gvRowCount - 1; i++) {
                txtAmount = $("input[id*=txtSemAmount]");
                semesterfee = parseInt(semesterfee) + parseInt(gv.rows[i].cells[1].childNodes[1].value);
            }

            //total Admission Fee
            document.getElementById('<% = txtTotalSemesterFee.ClientID %>').value = semesterfee;
            document.getElementById('<% = hdValSemester.ClientID %>').value = semesterfee;
            return true;
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
        .auto-style1
        {
            height: 20px;
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
            <li class="" id="lstEntrytab"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">Fee Header Entry</a></li>
            <li class="current" id="lstListtab"><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">Header(s) List</a></li>
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
                                <td colspan="2">
                                    <asp:Label ID="lblAccountStatus" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Segment/Course</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSegment" runat="server" Style="width: 140px; border: #aaaaaa 1px solid; background-color: #fbfbfb;">
                                        <asp:ListItem Text="Admission" Value="Admission">Admission</asp:ListItem>
                                        <asp:ListItem Text="Semester" Value="Semester">Semester</asp:ListItem>
                                        <asp:ListItem Text="Miscellaneous" Value="Miscellaneous">Miscellaneous</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="ddlCourse" runat="server" Style="width: 300px; border: #aaaaaa 1px solid; background-color: #fbfbfb;">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnGetData" runat="server" CausesValidation="False" Text="Show" CssClass="btngray" OnClick="btnGetData_Click" />

                                </td>
                            </tr>

                            <tr id="rwAnnual" runat="server" visible="false">
                                <td>
                                    <strong>Annual Fee</strong>
                                </td>
                                <td>

                                    <asp:GridView ID="grdAdmissionFee" runat="server" AutoGenerateColumns="False" Width="300px"
                                        BackColor="White" BorderColor="LightBlue" BorderStyle="Solid" BorderWidth="2px"
                                        CellPadding="4" OnRowCreated="grdAdmissionFee_RowCreated" OnRowDataBound="grdAdmissionFee_RowDataBound"
                                        DataKeyNames="ID,STS">
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Section" Visible="true" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdmnSection" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HEADER_NAME") %>'> </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount" Visible="true" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAdmnAmount" Width="80px" MaxLength="8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT") %> '> </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No records found
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                    </asp:GridView>


                                    <strong>Total </strong>
                                    <asp:TextBox ID="txtTotalAnnualFee" Width="80px" runat="server" Font-Bold="True"
                                        ForeColor="#000066" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="rwSemester" runat="server" visible="false">
                                <td><strong>Semester Fee</strong></td>
                                <td>
                                    <asp:GridView ID="grdSemesterFee" runat="server" AutoGenerateColumns="False" Width="300px"
                                        BackColor="White" BorderColor="LightBlue" BorderStyle="Solid" BorderWidth="2px"
                                        CellPadding="4" OnRowCreated="grdSemesterFee_RowCreated" OnRowDataBound="grdSemesterFee_RowDataBound"
                                        DataKeyNames="ID,STS">
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Section" Visible="true" ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSemSection" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HEADER_NAME") %>'> </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount" Visible="true" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSemAmount" Width="80px" MaxLength="8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AMOUNT") %> '> </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No records found
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                        <RowStyle BackColor="White" ForeColor="#003399" />
                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                    </asp:GridView>


                                    <strong>Total </strong>
                                    <asp:TextBox ID="txtTotalSemesterFee" Width="80px" runat="server" Font-Bold="True"
                                        ForeColor="#003300" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>

                            <tr id="rwOperation" runat="server" visible="false">
                                <td>&nbsp;
                                </td>
                                <td>
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btnblack"
                                        OnClientClick="if (!window.confirm('Are you sure you want to save/update this record')) return false;" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btngray" />
                                    <asp:HiddenField ID="hdId" runat="server" />
                                    <asp:HiddenField ID="hdValAdmission" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdValSemester" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="3" class="auto-style1"></td>
                            </tr>


                            <tr id="rwStatus" runat="server" visible="false">
                                <td colspan="2">
                                    <strong>Header Status:</strong><br />
                                    <asp:CheckBox ID="chkStatus1" runat="server" Enabled="false" BackColor="Green" ForeColor="white" Text="Active headers." />
                                    <br />
                                    <asp:CheckBox ID="chkStatus2" runat="server" Enabled="false" BackColor="Yellow" ForeColor="black" Text="Newly created headers, not defined at the time of master fee definition." /><br />
                                    <asp:CheckBox ID="chkStatus3" runat="server" Enabled="false" BackColor="Gray" ForeColor="white" Text="Inactive headers, selection is not applicable." />
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
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="COURSE_ID"
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" OnRowDeleting="grdStyled_RowDeleting"
                                Width="100%" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdStyled_PageIndexChanging" Height="95%" OnRowDataBound="grdStyled_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="30px"
                                        ItemStyle-Height="10px" SelectText="<img src='../Common/Images/edit1.png' border='0' title='Edit Record'>"></asp:CommandField>
                                    <asp:BoundField DataField="SESSION" HeaderText="Session *" />
                                    <asp:BoundField DataField="COURSE_NAME" HeaderText="Course Name*" />
                                    <asp:BoundField DataField="STS" HeaderText="Status" />
                                    <asp:BoundField DataField="CREATED_BY" HeaderText="Created By" />
                                    <asp:BoundField DataField="CREATED_DATE" HeaderText="Created Date" />
                                    <asp:BoundField DataField="MODIFIED_BY" HeaderText="Last Upd By" />
                                    <asp:BoundField DataField="MODIFIED_DATE" HeaderText="Last Upd Date" />
                                    <%-- <asp:TemplateField HeaderText="Inactive">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="../Common/images/icon_delete.png" OnClientClick="if (!window.confirm('Are you sure you want to Inactive the fee header?')) return false;"
                                                title='Inactive Header' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
