<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="AssMapping.aspx.cs" Inherits="Operation_AssMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">

   <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css"/>
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

        function IsNumeric(val) {
            if (!(event.keyCode > 47 && event.keyCode < 58)) {
                alert("Enter Numeric values Only!")
                event.srcElement.focus();
                return false;
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

        // Get the modal
        var modal = document.getElementById('id01');

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
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

        function displayMarksEntryPanel(id, marksSring) {

            if (marksSring == "") {

                document.getElementById('<%=txtReading.ClientID %>').value = '';
                document.getElementById('<%=txtListening.ClientID %>').value = '';
                document.getElementById('<%=txtWriting1.ClientID %>').value = '';
                document.getElementById('<%=txtWriting2.ClientID %>').value = '';
                document.getElementById('<%=txtSpeaking.ClientID %>').value = '';
                document.getElementById('<%=txtRemarks.ClientID %>').value = '';
 
                document.getElementById('<%=btnSave.ClientID %>').disabled = false;
                document.getElementById('<%=btnSave.ClientID %>').style.cssText = 'opacity: 1;';

                document.getElementById('<%=btnUpdate.ClientID %>').disabled = true;
                document.getElementById('<%=btnUpdate.ClientID %>').style.cssText = 'opacity: 0.3;';

                document.getElementById('<%=btnDelete.ClientID %>').disabled = true;
                document.getElementById('<%=btnDelete.ClientID %>').style.cssText = 'opacity: 0.3;';
            }
            else {

                document.getElementById('<%=btnSave.ClientID %>').disabled = true;
                document.getElementById('<%=btnSave.ClientID %>').style.cssText = 'opacity: 0.3;';

                document.getElementById('<%=btnUpdate.ClientID %>').disabled = false;
                document.getElementById('<%=btnUpdate.ClientID %>').style.cssText = 'opacity: 1;';

                document.getElementById('<%=btnDelete.ClientID %>').disabled = false;
                document.getElementById('<%=btnDelete.ClientID %>').style.cssText = 'opacity: 1;';

                // Reading|23.00; Listening|44.00; Writing1|; Writing2|; Speaking|; Remarks|DDD
                var input = marksSring;// 'john smith~123 Street~Apt 4~New York~NY~12345';
                var inputInner = ''

                var fields = input.split(';');
                var fieldsInner = fields[0].split('|')
                document.getElementById('<%=txtReading.ClientID %>').value = fieldsInner[1];

                fieldsInner = fields[1].split('|')
                document.getElementById('<%=txtListening.ClientID %>').value = fieldsInner[1];

                fieldsInner = fields[2].split('|')
                document.getElementById('<%=txtWriting1.ClientID %>').value = fieldsInner[1];

                fieldsInner = fields[3].split('|')
                document.getElementById('<%=txtWriting2.ClientID %>').value = fieldsInner[1];

                fieldsInner = fields[4].split('|')
                document.getElementById('<%=txtSpeaking.ClientID %>').value = fieldsInner[1];

                fieldsInner = fields[5].split('|')
                document.getElementById('<%=txtRemarks.ClientID %>').value = fieldsInner[1];
            }
            //var name = fields[0];
            //var street = fields[1];
 
            if (id != '0') {

                document.getElementById('<% = hdAssignmentID.ClientID %>').value = id;
                var message = "Do you wish to open marks entry panel?";
                var result = confirm(message);
                if (result == true) {
                    document.getElementById('id01').style.display = 'block';
                }
                else {
                    return false;
                }
            }
            else {
                alert(mode);
                document.getElementById('id01').style.display = 'block';
                return false;
            }
        }


        function validateMarks(mode) {

            var message = '';
            var Reading=document.getElementById('<%=txtReading.ClientID %>').value;
            var Listening=document.getElementById('<%=txtListening.ClientID %>').value;
            var Writing1=document.getElementById('<%=txtWriting1.ClientID %>').value;
            var Writing2=document.getElementById('<%=txtWriting2.ClientID %>').value;
            var Speaking=document.getElementById('<%=txtSpeaking.ClientID %>').value;

            //validations
            if (Reading == "" && Listening == "" && Writing1 == "" && Writing2 == "" && Speaking == "") {
                alert('Please enter the marks in corresponding sections!');
                document.getElementById('<%=txtReading.ClientID %>').focus();
                return false;
            }

            if (mode == "I")
                message = "Do you really wish to save marks?";
            if (mode == "U")
                message = "Do you really wish to update marks?";
            if (mode == "D")
                message = "Do you really wish to delete the marks, once you delete the marks it will not be accessible to student?";
                 
            var result = confirm(message);

            if (result == true) {
                return true;
            }
            else {
                return false;
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

        .flex-container
        {
            display: flex;
            border: 2px solid thick;
        }

        .flex-child
        {
            flex: 1;
            border: 2px solid gray;
        }

        .flex-child:first-child
        {
           margin-right: 20px;
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
            <li class="" id="lstEntrytab"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">Map Assignment(s)</a></li>
            <li class="current" id="lstListtab" style="visibility:hidden" ><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">User(s) List</a></li>
        </ul>
    </div>

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="height: 95%;">

                        <asp:CheckBox ID="chkAcknowledgement" runat="server" Checked="true" Text ="Send Acknowledgement" />

                        <br />
                        <table width="60%" style="margin-left: 250px;" cellspacing="8px;">

                            <tr>
                                <td>
                                    <strong>Session/Course</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSession" Width="150px" runat="server" AutoPostBack="false" />
                                    <asp:DropDownList Width="400px" ID="ddlCourse" runat="server" AutoPostBack="false" />

                                    <asp:Button ID="btnGetData" runat="server" CausesValidation="False" Text="Populate Students" CssClass="btngray" OnClick="btnGetData_Click" />
                                    <asp:Button ID="btnREset" runat="server" CausesValidation="False" Text="Reset" 
                                        CssClass="btngray" OnClick="btnREset_Click" />

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <strong>Students</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStudents" runat="server" Style="width: 400px; border: #aaaaaa 1px solid; background-color: #fbfbfb;"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlStudents_SelectedIndexChanged" />
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table id="tblAssignment" runat="server" visible="false" >

                            <tr>
                                <td colspan="2">

                                    <div class="flex-container" style="width: 100%; overflow: auto; max-height: 80%;">

                                        <div class="flex-child magenta" style="width: 80%; overflow: auto; max-height: 80%;">
                                            <b>Available <asp:Label ID="lblAvailable" Width="350px" Text="" runat="server" ForeColor="Blue" Font-Bold="true" /></b>
                                            <br />

                                            <div style="margin-top: 20px">
                                                <span><b style="font-size: 14px; padding-left: 7px;">*Search </b>
                                                    <asp:TextBox ID="txtAvailableSearch" Width="350px" Text="" MaxLength="100" runat="server" autocomplete="off">
                                                    </asp:TextBox>
                                                    <asp:Button ID="btnAvailableSearch" runat="server" Text="*Search" CausesValidation="false"
                                                        OnClick="btnAvailableSearch_Click" Visible="true" />
                                                </span>
                                            </div>

                                            <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 450px;">
                                                <asp:GridView ID="grdAvailable" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="AssignmentId,AttachmentPath" Width="100%" GridLines="Both"
                                                    BorderStyle="Solid" BorderColor="Black" CssClass="mGrid">

                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Checked="false" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAccessibility" runat="server" onclick="Check_Click(this)" CausesValidation="false"
                                                                    Checked="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attachment" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <a target="_blank" href='UploadedDocument/Assignment/<%#  Eval("AttachmentPath") %>'>
                                                                    <%# Eval("AttachmentPath").ToString().Equals("#") ? "" : "View Document"%></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="AssignmentTitle" HeaderText="Title*">
                                                            <HeaderStyle Width="5000px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AssignmentSummary" HeaderText="Description*" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No records found
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>


                                        <div class="flex-child green" style="width: 80%; overflow: auto; max-height: 80%;">
                                            <b>Assigned <asp:Label ID="lblAssigned" Width="350px" Text="" runat="server" ForeColor="Blue" Font-Bold="true" /></b>
                                            <br />

                                            <div style="margin-top: 20px">
                                                <span><b style="font-size: 14px; padding-left: 7px;">*Search </b>
                                                    <asp:TextBox ID="txtAssignedSearch" Text="" Width="350px" MaxLength="100" runat="server" autocomplete="off">
                                                    </asp:TextBox>
                                                    <asp:Button ID="btnAssignedSearch" runat="server" Text="*Search" CausesValidation="false"
                                                        OnClick="btnAssignedSearch_Click" Visible="true" />
                                                </span>
                                            </div>
                                            <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 450px;">
                                                <asp:GridView ID="grdAssigned" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="AssignmentId,AttachmentPath" Width="100%" GridLines="Both"
                                                    BorderStyle="Solid" BorderColor="Black" CssClass="mGrid" OnRowDataBound="grdAssigned_RowDataBound">

                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                          <%--  <HeaderTemplate>
                                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Checked="false" />
                                                            </HeaderTemplate>--%>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAccessibility" runat="server" onclick="Check_Click(this)" CausesValidation="false"
                                                                    Checked="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attachment" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <a target="_blank" href='UploadedDocument/Assignment/<%#  Eval("AttachmentPath") %>'>
                                                                    <%# Eval("AttachmentPath").ToString().Equals("#") ? "" : "View Document"%></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  

                                                        <asp:BoundField DataField="AssignmentTitle" HeaderText="Title*">
                                                            <HeaderStyle Width="5000px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AssignmentSummary" HeaderText="Description*" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="GRD_STATUS" HeaderText="Status" ItemStyle-Font-Bold="true" />
                                                        <asp:TemplateField>
                                                        <ItemTemplate> <%--MARKS_DTL--%>
                                                                <asp:Button ID="buttonSaveMarks" runat="server" Text ="Save Marks"
                                                                    OnClientClick='<%# String.Format("javascript:return displayMarksEntryPanel(\"{0}\", \"{1}\")", Eval("AssignmentId").ToString(),Eval("MARKS_DTL").ToString()) %>' class="w3-button w3-black" /> 
                                                         </ItemTemplate>  
                                                         </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No records found
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </div>

                                </td>

                            </tr>
                            <tr>
                                <td style="width: 50%;"><asp:Button ID="btnMapAssignment" runat="server" Text="Map Assignment(s)" CausesValidation="false"
                                                        Visible="false" OnClick="btnMapAssignment_Click" /></td>
                                <td style="width: 50%;"><asp:Button ID="btnActivateAssignment" runat="server" Text="Activate Assignment(s)" CausesValidation="false"
                                                         Visible="false" OnClick="btnActivateAssignment_Click" /></td>
                            </tr>
 
                        </table>
                    </div>
                    <asp:HiddenField ID="hdAssignmentID" runat="server" Value="" />
                </ContentTemplate>
                <%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </div>
              <asp:UpdatePanel runat="server" ID='MarksEntryPanel' UpdateMode="Conditional">
                <ContentTemplate>
                  
                    <div id="id01" class="w3-modal">
                        <div class="w3-modal-content w3-card-4">
                            <header class="w3-container w3-teal"> 
                            <span onclick="document.getElementById('id01').style.display='none'" 
                                class="w3-button w3-display-topright">&times;</span>
                                <h2>Marks Entry Panel</h2>
                            </header>
                            <div class="w3-container">
                                <p>Please input marks/remarks in corresponding sections:</p>
                                <table>
                                    <tr>
                                        <td style="width: 200px;"><b>Reading</b></td>
                                         <td><asp:TextBox ID="txtReading" runat="server" MaxLength="6" Width="150px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;"><b>Listening</b></td>
                                         <td><asp:TextBox ID="txtListening" runat="server" MaxLength="6" Width="150px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;"><b>Writing1</b></td>
                                         <td><asp:TextBox ID="txtWriting1" runat="server" MaxLength="6" Width="150px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;"><b>Writing2</b></td>
                                         <td><asp:TextBox ID="txtWriting2" runat="server" MaxLength="6" Width="150px" /></td>
                                    </tr>
                                      <tr>
                                        <td style="width: 200px;"><b>Speaking</b></td>
                                         <td><asp:TextBox ID="txtSpeaking" runat="server" MaxLength="6" Width="150px" /></td>
                                    </tr>
                                </table>
                             
                                <p><span><b>Special Note/Remarks if any</b></span> </p>
                                <p>
                                    <span>
                                        <asp:TextBox ID="txtRemarks" runat="server" MaxLength="500"  Width="800px"  />
                                    </span>
                                </p>

                                 <p><span><b>Operations</b></span></p>
                                 <p>
                                     <span>
                                         <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" OnClientClick="return validateMarks('I');"  />
                                         <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" Text="Update" OnClientClick="return validateMarks('U');"  />
                                         <asp:Button ID="btnDelete" OnClick="btnDelete_Click" runat="server" Text="Delete" OnClientClick="return validateMarks('D');"  />
                                     </span>
                                </p> 

                                <p>Click on the "x" or click anywhere outside of the modal!</p>
                            </div>
                            <footer class="w3-container w3-teal"></footer>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>


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
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged"  
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
