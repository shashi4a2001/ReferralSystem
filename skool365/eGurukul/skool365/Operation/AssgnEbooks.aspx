<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="AssgnEbooks.aspx.cs" Inherits="Operation_AssgnEbooks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <link href="../Common/CSS/Admin.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript">

        function setFocusToEntryTab(id) {
            if (id == "EntryTab") {
                document.getElementById("basicform").style.display = 'block';
                document.getElementById("UserList").style.display = 'none';
            }
            if (id == "ListUser") {
                document.getElementById("UserList").style.display = "block";
                document.getElementById("basicform").style.display = "none";
            }
        }


        function validatePageData() {

            var rdoAssignment = document.getElementById('<%= rdoAssignment.ClientID %>');

            if (document.getElementById('<%= txtSubject.ClientID %>').value == '') {
                alert('Title cannot be left blank');
                document.getElementById('<%= txtSubject.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= txtDescription.ClientID %>').value == '') {
                alert('Description cannot be left blank');
                document.getElementById('<%= txtDescription.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= rdoLiveUpdate.ClientID %>').checked == true) {
                if (document.getElementById('<%= txtPostedDate.ClientID %>').value == '') {
                    alert('Please availability till date(It will be accessible for all)');
                    document.getElementById('<%= txtPostedDate.ClientID %>').focus();
                    return false;
                }
            }

            //For Ebooks
            if (rdoAssignment.checked == false) {
                if (confirm('Do you really wish to uplaod the ebooks?') == true) {
                    return true;
                }
                else
                    return false;
            }

            //For Assinment
            if (rdoAssignment.checked == true) {

                var totalStudentSeletected = 0;
                var totalStudent = 0;

                var chkNotification = document.getElementById('<%= chkNotification.ClientID %>');

                if (document.getElementById("<%= grdDetails.ClientID %>") != null) {
                    totalStudent = document.getElementById("<%= grdDetails.ClientID %>").rows.length - 1;
                    var GridView = document.getElementById('<%= grdDetails.ClientID %>');

                    $("#<%=grdDetails.ClientID%> input[id*='chkAccessibility']:checkbox").each(function (index) {
                        if ($(this).is(':checked'))
                            totalStudentSeletected++;
                    });


                    if (totalStudentSeletected == 0 && chkNotification.checked == true) {
                        if (confirm('Do you really wish really wish to upload the assigment without sending notification to student, as you have not selected any student(s)?') == true) {
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (totalStudent != totalStudentSeletected && chkNotification.checked == true && totalStudentSeletected > 0) {
                        if (confirm('Do you really wish really wish to upload the assigment with sendning notification to some of student(s) ' + totalStudentSeletected.toString() + '/' + totalStudent.toString() + '  ?') == true) {
                            return true;
                        }
                        else
                            return false;
                    }
                    else if (chkNotification.checked == false) {
                        if (confirm('Do you really wish really wish to upload the assigment without sendning notification to student(s) as you have not checked the option [Send Notification] ?') == true) {
                            return true;
                        }
                        else
                            return false;
                    }
                    else {
                        if (confirm('Do you really wish really wish to upload the assigment with sending notification to selected student(s)?') == true) {
                            return true;
                        }
                        else
                            return false;
                    }
                }
                else {
                    if (confirm('Do you really wish really wish to upload the assigment without sending notification to student, as you have not populated any student grid?') == true) {
                        return true;
                    }
                    else
                        return false;
                }
            }
        }


        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        //  row.style.backgroundColor = "#c4edb7";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        //                        if (row.rowIndex % 2 == 0) {

                        //                            //Alternating Row Color

                        //                            row.style.backgroundColor = "#e6e9f2";

                        //                        }

                        //                        else {

                        //                            row.style.backgroundColor = "white";

                        //                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

        function onRadioChange(Option) {
            if (Option == "1") {
                document.getElementById('<%= txtPostedDate.ClientID %>').disabled = false;
                document.getElementById('<%= txtPostedDate.ClientID %>').value = "";
            }
            else {
                document.getElementById('<%= txtPostedDate.ClientID %>').disabled = true;
                document.getElementById('<%= txtPostedDate.ClientID %>').value = "";
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
    
    <uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Manage Assignments & eBooks [Student Portal]" />
   
     <div>
        <%--  <span class="pagedesc">Manage Users of the software</span>--%>
        <ul class="hornav">
            <li class="current"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">Assignments & eBooks Entry</a></li>
            <li class=""><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">Assignments & eBooks List</a></li>
        </ul>
    </div>
    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="Image1" ImageUrl="~/Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="60px" Height="60px" />
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
                    <div>
                        <table style="margin-left: 200px;">
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkNotification" Text="Check if send notification to Student(s)"
                                        runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Blue" />
                                </td>
                            </tr>
                            <tr>
                                <td>Title
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" runat="server" Width="615px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                        ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Description
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" Width="615px" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Type
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="dpdType" runat="server" Width="117px">
                                        <asp:ListItem Text="Assignment" Value="Assignment" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="eBook" Value="eBook" Selected="false"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:RadioButton ID="rdoAssignment" Text="Assignment" GroupName="Upload" Checked="true"
                                        runat="server" AutoPostBack="true" CssClass="Mandatory" Font-Bold="true" OnCheckedChanged="rdoAssignment_CheckedChanged" />
                                    <asp:RadioButton ID="rdoEbook" Text="eBook" GroupName="Upload" Checked="false" runat="server"
                                        CssClass="Mandatory" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="rdoAssignment_CheckedChanged" Enabled="false" />
                                </td>
                            </tr>
                            <tr id="rowStudent" runat="server" visible="false">
                                <td></td>
                                <td>
                                    <table cellpadding="1px" cellspacing="1px" border="1px" style="border-color: Black; border-style: solid;">
                                        <tr>
                                            <td>
                                                <table style="font-family: Verdana">
                                                    <tr>
                                                        <td class="style4">Session<span class="RequiredStarColor">*</span>
                                                        </td>
                                                        <td>
                                                            <%--OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"--%>
                                                            <asp:DropDownList ID="ddlSession" Width="150px" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style4">Course<span class="RequiredStarColor">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList Width="508px" ID="ddlCourse" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <span style="margin-left: 200px;">
                                                                <asp:Button ID="btnPopulate" CssClass="buttonCss" runat="server" Text="Populate Student(s)"
                                                                    OnClick="btnPopulate_Click" Width="200px" />
                                                                <asp:Button ID="btnClear" CssClass="buttonCss" runat="server" Text="Enable" OnClick="btnClear_Click"
                                                                    Width="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkMailAsGroupMail" Text="Keep all recepient(s) in Single Mail"
                                                                runat="server" Style="color: #000066" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="ChkAttachAssignment" Text="Send Assignment with Mail" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Special Comments in mail if any
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtComments" runat="server" Height="100px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ForeColor="Red" ID="lblstatusMsg" runat="server"></asp:Label>
                                                            <div class="maingrid" style="width: 100%; height: 80%; overflow: scroll;">
                                                                <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                                    Width="100%" OnRowDataBound="grdDetails_RowDataBound" Height="60%" GridLines="Both" >
                                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Checked="true" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkAccessibility" runat="server" onclick="Check_Click(this)" CausesValidation="false"
                                                                                    Checked="true" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:TemplateField HeaderText="Accessible">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkAccessibility" runat="server" CausesValidation="false" Checked="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="Student Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("STUDENT_NAME") %>' class="style4"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Email ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Bind("EMAIL_ID") %>' class="style4"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Mobile No">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("MOBILE_NO") %>' class="style4"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No records found
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b><u>Assignment Properties</u></b>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:RadioButton ID="rdoLiveUpdate" runat="server" onclick="javascript:onRadioChange(1);"
                                        Text="Available Till (For Students)" ForeColor="Maroon" GroupName="Live" Checked="true" />
                                    <asp:TextBox ID="txtPostedDate" runat="server" Width="117px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="PostedDateCaledar" runat="server" TargetControlID="txtPostedDate"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                    <br />
                                    <asp:RadioButton ID="rdoAlwaysLive" GroupName="Live" ForeColor="Maroon"
                                        onclick="javascript:onRadioChange(2);" runat="server" Text="Available for life time" /><br />
                                    <asp:CheckBox ID="chkResponseReq" runat="server" ForeColor="Maroon"
                                        Text="Need to submit Assignment Response" />
                                </td>
                            </tr>
                            <tr>
                                <td>Attachment
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="233px" />
                                    <asp:Label ID="lblUploadRemarks" ForeColor="Red" runat="server" Visible="false" Text="*Please do not ulpload the assignment if you wish to preserve the previous one, otherwise previous will be deleted"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" CausesValidation="true" runat="server" Text="Save" OnClick="btnSubmit_Click"
                                        CssClass="btnblack" OnClientClick="return validatePageData();" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btngray" />
                                    <span>
                                        <asp:Label ID="lblMessage" runat="server" class="lblMessageClassss" ForeColor="Red"
                                            Text="" Visible="True"></asp:Label>
                                    </span>
                                    <asp:HiddenField ID="hdId" runat="server" />
                                    <asp:HiddenField ID="hdBatchID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:PostBackTrigger ControlID="rdoAssignment" />
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
                                <asp:Button ID="Button1" Width="70px" runat="server" Text="Clear" ToolTip="Reset the grid with default search" CausesValidation="false" OnClientClick=" return ClearInputSearch();" OnClick="btnSearch_Click" />
                            </span>
                        </div>
                        <br />
                            <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 380px;">
                                <asp:GridView ID="grdEvents" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="AssignmentId,AttachmentPath" OnRowDeleting="grdStyled_RowDeleting"
                                    OnSelectedIndexChanged="grdEvents_SelectedIndexChanged" Width="80%" GridLines="Both"
                                    BorderStyle="Solid" BorderColor="Black" 
                                    OnRowDataBound="grdEvents_RowDataBound" CssClass="mGrid" >

                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="20px"
                                            ItemStyle-Height="20px" SelectText="<img src='../Common/Images/edit.png' border='0' title='Edit Record'>"></asp:CommandField>

                                        <asp:TemplateField HeaderText="Attachment" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <a target="_blank" href='UploadedDocument/Assignment/<%#  Eval("AttachmentPath") %>'>
                                                    <%# Eval("AttachmentPath").ToString().Equals("#") ? "" : "View Document"%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="AssignmentTitle" HeaderText="Title*" >
                                            <HeaderStyle Width="5000px" /></asp:BoundField>
                                        <asp:BoundField DataField="AssignmentSummary" HeaderText="Description*"  HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="AssignmentType" HeaderText="Type*"  HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="Is_ForLifeTime" HeaderText="Life Time Accessible" />
                                        <asp:BoundField DataField="ExpiryDate" HeaderText="Accessible Till" />
                                        <asp:BoundField DataField="Is_Submission_Required" HeaderText="Submission Req?" />
                                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date*" />
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By*"  HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="MODIFIED_ON" HeaderText="Modified Date"  HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="MODIFIED_BY" HeaderText="Modified By*" />
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                    ImageUrl="~/Common/Images/delete.png" OnClientClick="if (!window.confirm('Are you sure you want to delete this record')) return false;"
                                                    title='Delete Record' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="imgAudit" class="customLnk" Style="width: 15px; height: 15px;"
                                                    src='Common/Images/search.gif' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Grid_Qry" HeaderText="" />
                                        <asp:BoundField DataField="IsExpuired" HeaderText="" />
                                        <asp:BoundField DataField="IsAssigned" HeaderText="" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No records found
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </div>
      
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdEvents" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
