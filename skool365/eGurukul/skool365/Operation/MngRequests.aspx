<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master" AutoEventWireup="true" CodeFile="MngRequests.aspx.cs" Inherits="Operation_MngRequests" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script src="../Common/JS/Calendar.js" type="text/javascript"></script>
    <link href="../Common/CSS/Admin.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function HideLabel() {
            var seconds = 4;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function HideLabelMsg() {
            document.getElementById("<%=lblMessage.ClientID %>").value = "";
            document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        };

        function HideImage() {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        };

        function dateSelectionChanged(sender, args) {
            document.getElementById('<%=hdCalendarVal.ClientID%>').value = "";
            var selectedDate = sender.get_selectedDate();
            document.getElementById('<%=hdCalendarVal.ClientID%>').value = document.getElementById('<%=txtDueDate.ClientID%>').value;
            (document.getElementById('<%=dummybtn.ClientID%>')).click();
        };
 
        function ClearInputSearch() {
            if (document.getElementById('<%=txtSearch.ClientID %>').value == "") {
                return false;
            }
            else {
                document.getElementById('<%=txtSearch.ClientID %>').value = "";
                return true;
            }
        };

        function displayEntryPanel() {

            document.getElementById("basicform").style.display = 'block';
            document.getElementById("UWReqList").style.display = 'none';
            document.getElementById("lstListtab").className = 'current';
            document.getElementById("lstEntrytab").className = '';

            //Clear Values
            document.getElementById('<%=btnSubmit.ClientID %>').value = "Submit Request";
            document.getElementById('<%=txAssigmentName.ClientID %>').value = "";
            document.getElementById('<%=txtDueDate.ClientID %>').value = "";
            document.getElementById('<%=txtSOVName.ClientID %>').value = "";

            <%--document.getElementById('<%=txtTIV.ClientID %>').value = "";
            <%--document.getElementById('<%=rdoTypeNew.ClientID %>').checked = true;
            document.getElementById('<%=rdoTypeNew.ClientID %>').style.visibility = 'visible';
            document.getElementById('<%=rdoTypeRemodel.ClientID %>').checked = false;
            document.getElementById('<%=chkInternationalExposure.ClientID %>').checked = false;
            var objddlLocation = document.getElementById("ddlLocation")
            objddlLocation.selectedIndex = 0;--%>

            return true;
        };

        function displayReasonPanel() {

            var message = "Do you wish to submit the request for the selected account?";
            var Confirmmessage = document.getElementById('<%=hdAlertMessage.ClientID %>').value;

            var result = confirm(Confirmmessage);

            if (Confirmmessage != message) {
                if (!result) {
                    if (document.getElementById('<%=btnSubmit.ClientID %>').value != "Update") {
                        //For New Request
                        return true;
                    } else if (document.getElementById('<%=btnSubmit.ClientID %>').value == "Update") {
                        //For Remodel
                        document.getElementById("divPanel1").style.display = 'block';
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                if (result) {
                    if (document.getElementById('<%=btnSubmit.ClientID %>').value != "Update") {
                        //For New Request
                        return true;
                    } else if (document.getElementById('<%=btnSubmit.ClientID %>').value == "Update") {
                        //For Remodel
                        document.getElementById("divPanel1").style.display = 'block';
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
        };

        function setFocusToEntryTab(id) {
            if (id == "EntryTab") {
                document.getElementById("basicform").style.display = 'block';
                document.getElementById("UWReqList").style.display = 'none';
                document.getElementById("lstListtab").className = 'current';
                document.getElementById("lstEntrytab").className = '';
            }
            if (id == "UWList") {
                document.getElementById("UWReqList").style.display = "block";
                document.getElementById("basicform").style.display = "none";
                document.getElementById("lstListtab").className = '';
                document.getElementById("lstEntrytab").className = 'current';
            }
        }

        function callme() {

            var fileUpload = document.getElementById('<%= fleUpdSOVName.ClientID %>');
            var validateFiles = fileUpload.files;
            var name = fileUpload.name;

            document.getElementById('<%=txtSOVName.ClientID%>').value = "";
            for (var i = 0; i < validateFiles.length; i++) {
                var myfile = validateFiles[i].name;
                if (document.getElementById('<%=txtSOVName.ClientID%>').value != "") {
                    document.getElementById('<%=txtSOVName.ClientID%>').value = document.getElementById('<%=txtSOVName.ClientID%>').value + ",";
                }
                document.getElementById('<%=txtSOVName.ClientID%>').value = document.getElementById('<%=txtSOVName.ClientID%>').value + myfile;
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


        function decodeHTMLEntities(text) {
            var entities = [
                ['amp', '&'],
                ['apos', '\''],
                ['#x27', '\''],
                ['#x2F', '/'],
                ['#39', '\''],
                ['#47', '/'],
                ['lt', '<'],
                ['gt', '>'],
                ['nbsp', ' '],
                ['quot', '"']
            ];

            for (var i = 0, max = entities.length; i < max; ++i)
                text = text.replace(new RegExp('&' + entities[i][0] + ';', 'g'), entities[i][1]);

            return text;
        }

        function showdetail(cnt, To, From, CC, Subject, Message, RequestID, LogCreateDate) {

            document.getElementById('<%=hdReplyTo.ClientID%>').value = To;
            document.getElementById('<%=hdReplyFrom.ClientID%>').value = From;
            document.getElementById('<%=hdReplyCC.ClientID%>').value = CC;
            document.getElementById('<%=hdReplyDate.ClientID%>').value = LogCreateDate;
            document.getElementById('<%=hdSubject.ClientID%>').value = Subject;
            document.getElementById('<%=hdMessage.ClientID%>').value = Message;
            document.getElementById('<%=hdRequestID.ClientID%>').value = RequestID;

            document.getElementById('<%=txtReply.ClientID%>').value = "";
            document.getElementById('txtdescmsg').innerHTML = "";

            var i = 'dvdtl' + cnt;
            document.getElementById('txtdescmsg').innerHTML = decodeHTMLEntities(document.getElementById(i).innerHTML);
            document.getElementById('customlightbox').style.display = 'block';

        }

        function hidememsg() {
            document.getElementById('customlightbox').style.display = 'none';
        }

        <%--function RemodelReason() {
            if (document.getElementById("<%=txtRemodelReason.ClientID %>").value.trim() == "") {
               alert('Please enter Remodel reason');
               document.getElementById("<%=txtRemodelReason.ClientID %>").focus();
                return true;
            }
            else {
                document.getElementById("divPanel1").style.display = 'none';
                return false;
            }
        };--%>

        function ValidategrdRemodeReason(sender, args) {
            var chechBoxChecked = 'N';
            var gridView = document.getElementById("<%=grdRemodeReason.ClientID %>");

            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {

                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked && chechBoxChecked == 'N') {
                    chechBoxChecked = 'Y';
                    args.IsValid = true;
                }

                if (chechBoxChecked == 'Y') {
                    //if any check box is checked, then check if value is not entered for others
                    if (checkBoxes[i].type == "text" && checkBoxes[i - 1].checked) {
                        if (checkBoxes[i].value.trim() == '') {
                            args.IsValid = false;
                            return;
                        }
                    }
                }
            }

            //Check box is checked
            if (chechBoxChecked == 'Y') {
                document.getElementById("divPanel1").style.display = 'none';
                args.IsValid = true;
                return true;
            }
            //No Check box is checked
            if (chechBoxChecked == 'N') {
                args.IsValid = false;
                return true;
            }

        }
        function SOVClose() {
            document.getElementById("basicform").style.display = 'none';
            document.getElementById("UWReqList").style.display = 'block';
            var e = $find("ModelPopUp1");
            e.hide();
            return false;
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
        .web_dialog
        {
            cursor: move;
            display: block;
            position: fixed;
            width: 510px;
            height: 165px;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }

        .web_dialog_title a
        {
            color: White;
            text-decoration: none;
        }

        .align_right
        {
            text-align: right;
        }
    </style>

    <style type="text/css">
        #wrapper
        {
            width: 100%;
            margin: 0 auto;
            padding: 0;
            overflow: auto;
            background-color: red;
            padding: 0;
            margin: 0;
            display: block;
            border: 1px solid white;
            position: fixed;
        }

        #rightcolumn
        {
            width: 100%;
            background-color: yellow;
            display: block;
            float: left;
            border: 1px solid white;
        }

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

        .FixedHeader
        {
            position: absolute;
            font-weight: bold;
        }
    </style>


    <div style="position: fixed; z-index: 999; height: 75%; width: 75%; top: 20px; left: 20px; background-color: #e6f3f7; padding: 5px; border-color: #024e66; border-width: 2px; border-radius: 5px; display: none;"
        id="customlightbox">

        <span style="float: right; cursor: pointer;" title="Close" onclick="hidememsg()">
            <img src="../Common/images/icon_close.gif" />&nbsp;</span>
        <div style="width: 100%; cursor: move; background-color: #d1e7f9;" id='customlightboxHeader'>
            <span style="color: #050a11;">Reply </span>
            <br />
            <asp:TextBox ID="txtReply" Text="" TextMode="MultiLine" runat="server" Width="85%"
                Height="180px" autocomplete="off"></asp:TextBox>
            <asp:Button ID="btnReply" Text="Reply" runat="server" CausesValidation="false" OnClick="btnReply_Click"></asp:Button>
        </div>
        <hr />
        <div style="height: 70%; width: 100%; background-color: #e6f3f7;">
            <span style="color: #050a11;">Message Trails </span>
            <div id="txtdescmsg" style="height: 300px; width: 900px; overflow: auto; background-color: #ced8e0;">
            </div>
        </div>
    </div>
 
    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <%--<img runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height: 60px;" id="Image1"> </img>--%>
                    <asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />
                </ProgressTemplate>
            </asp:UpdateProgress>

            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>
    <div>
        <ul class="hornav">
            <li class="" id="lstEntrytab"><a href="#" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">New Request</a></li>
            <li class="current" id="lstListtab"><a href="#" id="UWList" onclick="setFocusToEntryTab('UWList')">Assignment Request List</a></li>
        </ul>
        <asp:UpdatePanel runat="server" ID='updatepnlddlUWList' UpdateMode="Conditional">
            <ContentTemplate>

                <span>
                    <b>Assignment Request Status</b>|<asp:Label ID="lblPendingUnderUW" Text="Total Assignment" runat="server" Style="color: blue; font-size: small; font-weight: bold;" />
                    :<asp:Label ID="lblPendingUnderUWCnt" Text="" runat="server" Style="color: #FF0000; font-size: small; font-weight: bold;" />
                </span>

                <span>|<asp:Label ID="lblTotalPendingUnderAll" Text="Assignment(Pending)" runat="server" Style="color: blue; font-size: small; font-weight: bold;" />
                    :<asp:Label ID="lblTotalPendingUnderAllCnt" Text="" runat="server" Style="color: #FF0000; font-size: small; font-weight: bold;" />
                </span>

                <span>|<asp:Label ID="lblTotalApprovedInYear" Text="Assignment(Submitted)" runat="server" Style="color: blue; font-size: small; font-weight: bold;" />
                    :<asp:Label ID="lblTotalApprovedInYearCnt" Text="" runat="server" Style="color: #00FF00; font-size: small; font-weight: bold;" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

                    <div style="height: 90%;">
                        <table width="60%" style="margin-left: 250px;" cellspacing="8px;">
                            <tr>
                                <td style="width: 180px">
                                    <strong>Select Assigment</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAssignments" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlAssignments_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </td>
                                <td rowspan="18">&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="height: 30px">
                                    <strong>Assigment Name <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txAssigmentName" runat="server" MaxLength="255" CssClass="hasDatepicker" autocomplete="off"
                                        ondrop="return false;"
                                        onpaste="return false;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="regtxAssigmentName" runat="server" ControlToValidate="txAssigmentName"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>

                                </td>
                            </tr>

                            <tr>
                                <td style="height: 30px">
                                    <strong>Assignment Description <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="255" CssClass="hasDatepicker" autocomplete="off"
                                        ondrop="return false;"
                                        onpaste="return false;"></asp:TextBox>
                                      <a target="_blank" href="#" id="hrefAssignment" runat="server">
                                    <img id="imgAssignment" src="/skool365/Common/Images/viewattachment.png" runat="server" visible="false" /></a> 
                                </td>
                            </tr>

                            <tr>
                                <td style="height: 30px">
                                    <strong>Request Type</strong>
                                </td>
                                <td>
                                    <asp:RadioButton ID="rdoTypeNew" Text="New" Checked="true" runat="server" GroupName="RqType" />
                                    <asp:RadioButton ID="rdoTypeRemodel" Text="Return" Checked="false" runat="server"
                                        Visible="false" Enabled="false" GroupName="RqType" />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <strong>Submission Date<span style="color: red;">*</span></strong>
                                </td>

                                <td style="height: 30px;">
                                    <div style="position: absolute; height: 30px;">
                                        <asp:TextBox ID="txtDueDate" runat="server" Width="100px" MaxLength="10" CssClass="disable_past_dates"
                                            ReadOnly="true" autocomplete="off" CausesValidation="false" AutoPostBack="false" />
                                        <asp:ImageButton runat="server" ID="imgPopup" ImageUrl="~/Common/Images/calendar.png" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarDueDate" runat="server"
                                            TargetControlID="txtDueDate" Format="yyyy-MM-dd" PopupButtonID="imgPopup" Enabled="true"
                                            OnClientDateSelectionChanged="dateSelectionChanged" />
                                        <asp:RequiredFieldValidator ID="reqtxtDueDate" runat="server" ControlToValidate="txtDueDate"
                                            ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:Button ID="dummybtn" runat="server" Text="" Style="display: none" OnClick="dummybtn_Click" CausesValidation="false" />
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <strong>Upload document <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID='updatePnlSOVName' UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:textbox id="txtSOVName" runat="server" maxlength="255" xmlns:asp="#unknown"
                                                cssclass="hasDatepicker" width="300px" readonly="True"></asp:textbox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSOVName"
                                                ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:fileupload id="fleUpdSOVName" multiple="true" name="..."
                                                onchange="callme()" xmlns:asp="#unknown" tooltip="Upload documnet should always be Only pdf/doc/excel/image/ppt file" runat="server" />
                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID='ddlUWList' EventName='SelectedIndexChanged' />
                                            <asp:AsyncPostBackTrigger ControlID='ddlAssignments' EventName='SelectedIndexChanged' />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                </td>
                            </tr>
                         

                            <tr>
                                <td>
                                    <strong>Response <span style="color: red;">*</span></strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtResponse" runat="server" MaxLength="1000" TextMode="MultiLine" CssClass="hasDatepicker"
                                        class="Platform" autocomplete="off" Height="200px" Width="524px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtResponse" runat="server" ControlToValidate="txtResponse"
                                        ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 0px;">
                                    <span id="error" style="color: Red; display: none">* Special Characters not allowed</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Acknowledge To</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUWList" runat="server" OnSelectedIndexChanged="ddlUWList_SelectedIndexChanged"
                                        ToolTip="Please select the SOV file(s) after the change of Underwriter!" AutoPostBack="true">
                                    </asp:DropDownList>

                                </td>
                            </tr>

                            <tr>
                                <td><strong>Reason to Resubmit</strong></td>
                                <td>
                                    <div style="width: 550px; overflow: scroll; border: 1px solid #000; max-height: 250px;">
                                    <asp:GridView ID="grdRemodeReason" runat="server" AutoGenerateColumns="false" DataKeyNames="Reason_ID" OnRowDataBound="OnRowDataBound"
                                        CellPadding="20" CellSpacing="20" RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid"
                                        Width="524px">
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chckSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason" HeaderStyle-Font-Bold="true" ItemStyle-Width="300">
                                                <ItemTemplate>
                                                    <asp:Label ForeColor="Red" runat="server" Text='<%# Eval("Reason") %>' ID="LblReason"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOthers" runat="server" Text='' Width="500" MaxLength="500" Visible="false" autocomplete="off"></asp:TextBox>
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
                      
                            <tr>
                                <td colspan="2" style="height: 0px;"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <center>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                            CssClass="btnblack" OnClientClick="return displayReasonPanel() ;"
                                            CausesValidation="true" />

                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                            OnClick="btnCancel_Click" CssClass="btngray" /></center>

                                    <asp:HiddenField ID="hdId" runat="server" />
                                    <asp:HiddenField ID="hdUW" runat="server" />
                                    <asp:HiddenField ID="hdConStr" runat="server" />
                                    <asp:HiddenField ID="hdOriginalMessage" runat="server" />
                                    <asp:HiddenField ID="hdRequestID" runat="server" />
                                    <asp:HiddenField ID="hdReplyTo" runat="server" />
                                    <asp:HiddenField ID="hdReplyCC" runat="server" />
                                    <asp:HiddenField ID="hdReplyFrom" runat="server" />
                                    <asp:HiddenField ID="hdReplyDate" runat="server" />
                                    <asp:HiddenField ID="hdSubject" runat="server" />
                                    <asp:HiddenField ID="hdMessage" runat="server" />
                                    <asp:HiddenField ID="hdAlertMessage" runat="server" Value="Do you wish to submit the request for the selected account?" />
                                    <asp:HiddenField ID="hdCalendarVal" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="dummybtn" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server" ID='RemodelReasonPanel' UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divPanel1" class="web_dialog" style="display: none; max-height: 330px;">
                        <asp:Panel ID="Panel1" runat="server" Style="display: block">
                            <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: lightsteelblue; border: solid 1px maroon; color: White; height: 20px;">
                                Remodel Reason<span class="RequiredStarColor">*</span><br />
                            </asp:Panel>
                            <div style="background: lightgray; border: solid 1px blue; max-height: 400px;">
                                <p style="text-align: center;">
                                    <asp:Button ID="btnCloseRemodel" runat="server" Text="Close" OnClick="btnSubmit_Click" CausesValidation="false" />
                                </p>

                               <%-- <div style="width: 98%; overflow: scroll; border: 1px solid #000; max-height: 360px;">
                                    <asp:GridView ID="grdRemodeReason" runat="server" AutoGenerateColumns="false" DataKeyNames="Reason_ID" OnRowDataBound="OnRowDataBound"
                                        CellPadding="20" CellSpacing="20" RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid"
                                        Width="95%">
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chckSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason" ItemStyle-Width="300">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Eval("Reason") %>' ID="LblReason"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOthers" runat="server" Text='' Width="500" MaxLength="500" Visible="false" autocomplete="off"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <EmptyDataTemplate>
                                            No records found
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>--%>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select Reason/Missing Reason for [Others]"
                                    ClientValidationFunction="ValidategrdRemodeReason" ForeColor="Red"></asp:CustomValidator>
                                <br />

                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div id="UWReqList" class="subcontent" style="display: none;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin: 8px 10%">
                            <span>
                                <asp:RadioButton ID="rdoMyAccount" Text="My Account" runat="server" GroupName="Select"
                                    Checked="true" /></span> <span>
                                        <asp:RadioButton ID="rdoAllAccount" Text="All Account" runat="server" GroupName="Select"
                                            Checked="false" /></span> <span>
                                                <asp:DropDownList ID="ddlYear" runat="server" Width="73px">
                                                </asp:DropDownList>
                                            </span><span><b style="font-size: 14px; padding-left: 7px;">*Search </b>
                                                <asp:TextBox ID="txtSearch" Text="" MaxLength="100" runat="server" autocomplete="off" Width="330px">
                                                </asp:TextBox></span> <span>
                                                    <%--<asp:Button ID="btnGetDetails" runat="server" Text="Go" OnClick="btnGetDetails_Click" CssClass="btnblack"  ToolTip="Get Underwriter Requests"/>--%>
                                                    <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="false" OnClick="btnGo_Click" />
                                                    <asp:Button ID="btnClear" Width="70px" runat="server" Text="Clear" ToolTip="Reset the grid with default search" CausesValidation="false" OnClientClick=" return ClearInputSearch();" OnClick="btnGo_Click" />
                                                    <asp:Button ID="btnNewRequest" runat="server" Text="New Request" CausesValidation="false" OnClientClick="return displayEntryPanel();" OnClick="btnCancel_Click" />
                                                    <%--OnClientClick="return displayEntryPanel();"--%>
                                                </span>
                        </div>
                        <div style="width: 85%; overflow: scroll; border: 1px solid #000; max-height: 390px; margin-left :60px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,UniqueID,UnderWriter_ID"
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" OnRowDeleting="grdStyled_RowDeleting"
                                CellPadding="20" CellSpacing="20" RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid"
                                Width="95%" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdStyled_PageIndexChanging" OnRowDataBound="grdStyled_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                               
                                 <Columns>

                                    <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="10px"
                                        ItemStyle-Height="10px" SelectText="<img src='../Common/Images/edit1.png' border='0' title='Edit Request'>"></asp:CommandField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="../Common/images/icon_delete.png" OnClientClick="if (!window.confirm('Are you sure you want to delete this Request')) return false;"
                                                title='Delete Request' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mail Log">
                                        <ItemTemplate>
                                            <a href="#" onclick="showdetail(<%# Container.DataItemIndex + 1 %>,'<%#Eval("To") %>',
                                                  '<%#Eval("From") %>','<%#Eval("CC") %>','<%#Eval("Subject") %>'
                                                  ,'<%#Eval("UnderWriter_ID") %>','<%#Eval("UniqueID") %>'
                                                  ,'<%#Eval("LOG_DATE_DESCRIPTIVE") %>' )">Correspondence</a>
                                            <div style="display: none;" id="dvdtl<%# Container.DataItemIndex + 1 %>">
                                                <%# System.Web.HttpUtility.HtmlEncode(Eval("Message_Dtl")) %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="UniqueID" HeaderText="Request ID*" ItemStyle-Width="120px" />

                                    <asp:BoundField DataField="DateRecieved" HeaderText="Date Submitted" ItemStyle-Width="150px" />

                                    <asp:BoundField DataField="Type" HeaderText="Type*" ItemStyle-Width="300px" />

                                    <asp:BoundField DataField="AccountName" HeaderText="Assignment Title*" HeaderStyle-Width="700px"
                                        ItemStyle-Width="700px" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />

                                    <asp:BoundField DataField="ProcessingStatus" HeaderText="Status" ItemStyle-Width="150px" ItemStyle-BackColor="LightBlue" />

                                    <asp:BoundField DataField="NewRemodel" HeaderText="New/Response" ItemStyle-Width="200px"
                                        HeaderStyle-Wrap="true" />       
                                                              
                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="Underwriter" HeaderText="Assigned To*" ItemStyle-Width="100px" />
                               
                                    <asp:TemplateField HeaderText="Document(s)" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemTemplate>
                                            <a href="#" onclick="ShowDocumentsInLarge('<%#Eval("UniqueID") %>'  );">
                                                <img id="Img1" runat="server" src="~/Common/Images/search.gif" alt="Apply" height="21"
                                                    width="21" title="View Document"></a>
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
                    </Triggers>
                </asp:UpdatePanel>
 
            </div>
        </div>
    </div>

    <script  language="javascript">

        var mousePosition;
        var offset = [0, 0];
        var div;
        var div1;
        var isDown = false;

        div = document.getElementById('customlightbox');
        div1 = document.getElementById('customlightboxHeader');


        div1.addEventListener('mousedown', function (e) {
            isDown = true;
            offset = [
                div.offsetLeft - e.clientX,
                div.offsetTop - e.clientY
            ];
        }, true);

        document.addEventListener('mouseup', function () {
            isDown = false;
        }, true);

        document.addEventListener('mousemove', function (event) {
            event.preventDefault();
            if (isDown) {
                mousePosition = {

                    x: event.clientX,
                    y: event.clientY

                };
                div.style.left = (mousePosition.x + offset[0]) + 'px';
                div.style.top = (mousePosition.y + offset[1]) + 'px';
            }
        }, true);


        function ShowDocumentsInLarge(Account) {
            //Change locathost to live url
            var imgSrc = 'http://localhost:11510/skool365/Operation/SOVTree.aspx?Acc=' + Account;
            window.open(imgSrc, '_blank', 'status=yes,toolbar=no,height=510,width=1050,menubar=no,location=no,resizable=no');
        }
    </script>
</asp:Content>
