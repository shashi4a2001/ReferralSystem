<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationAreaFooterLess.Master" AutoEventWireup="true" 
            CodeFile="Registration.aspx.cs" Inherits="Operation_Registration" enableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <link href="../Common/CSS/Admin.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">

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
                document.getElementById("lstListtab").className = 'current';
                document.getElementById("lstEntrytab").className = '';
                document.getElementById('<%=txtFirstName.ClientID %>').focus(); 
            }
            if (id == "ListUser") {
                document.getElementById("UserList").style.display = "block";
                document.getElementById("basicform").style.display = "none";
                document.getElementById("lstListtab").className = '';
                document.getElementById("lstEntrytab").className = 'current';
                document.getElementById('<%=txtSearch.ClientID %>').focus(); 
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

        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function HideLabelMsg() {
            document.getElementById("<%=lblMessage.ClientID %>").value = "";
            document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        };
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


        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }

        .H1
        {
            font-size: 17PX;
            font-weight: bold;
            color: #541110;
            text-align: center;
            line-height: 28PX;
        }
    </style>
    <style type="text/css">
        .style1
        {
            height: 24px;
        }

        .style2
        {
            height: 24px;
            width: 516px;
        }

        .style3
        {
            width: 516px;
        }

        .style4
        {
            width: 158px;
        }

        .style8
        {
            width: 299px;
        }

        .style9
        {
            height: 18px;
        }

        .style10
        {
            width: 92px;
        }

        .button
        {
            border-top: 1px solid #2ca8f5;
            background-color: rgb(0, 51, 0);
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: rgb(0, 51, 0);
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
            background: rgb(0, 51, 0);
        }

            .button:hover
            {
                border-top-color: #28597a;
                background: #28597a;
                color: #e82e63;
            }

            .button:active
            {
                border-top-color: #1b435e;
                background: rgb(0, 51, 0);
            }

        .style11
        {
            height: 38px;
        }

        .style12
        {
            width: 182px;
        }

        .style13
        {
            color: #0000FF;
            text-decoration: underline;
        }

        .style14
        {
            font-weight: bold;
        }

        .style15
        {
            width: 157px;
        }

        .style16
        {
            width: 250px;
            text-align: right;
        }

        .style22
        {
            text-align: center;
        }

        .style23
        {
            font-size: large;
        }

        .style24
        {
            color: #FF0000;
        }

        .style25
        {
            font-size: 40pt;
            font-family: "Arial Rounded MT Bold";
        }

        .style27
        {
            font-size: medium;
        }

        .style28
        {
            color: #000066;
        }

        .style29
        {
            font-family: Arial;
        }

        .auto-style1
        {
            font-size: 30pt;
            font-family: "Arial Rounded MT Bold";
        }
        .auto-style2
        {
            width: 157px;
            height: 34px;
        }
        .auto-style3
        {
            height: 34px;
        }
        .auto-style4
        {
            width: 250px;
            text-align: right;
            height: 34px;
        }
    </style>
    <script type="text/javascript">

        /////////////////////////////////////////////////
        ///Function Name: fnCommonAcceptCharacterOnly
        ///Written By   : Shashi
        ///Return Type  : True or False
        ///               If Any Character Except (a-z and A-Z and Enter and Back Space 
        ///               Then Return False Else Return True)                    
        ///Task         : Accept Character Only Also Accept Back Space and Enter Key
        /////////////////////////////////////////////////
        //---------------------------------------------
        //Method to calling to this function
        //onkeypress="return fnCommonAcceptCharacterOnly(event)"
        //---------------------------------------------

        function fnCommonAcceptCharacterOnly(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            // alert(charCode);
            if (charCode == 32)
                return true;
            if (charCode == 13)
                return true;

            if ((charCode >= 65 && charCode <= 90))
                return true;

            if ((charCode >= 97 && charCode <= 122))
                return true;

            return false;
        }

        //-----------------------------------------------------

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

        function fillParmanentAddress(checkedValue) {
            if (checkedValue) {
                document.getElementById('<%=txtParmAddress.ClientID %>').value = document.getElementById('<%=txtCorrAddress.ClientID %>').value;
                document.getElementById('<%=txtParmAddressPinCode.ClientID %>').value = document.getElementById('<%=txtPinCode.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtParmAddress.ClientID %>').value = "";
                document.getElementById('<%=txtParmAddressPinCode.ClientID %>').value = "";
            }
        }

        function calculatePercentage(MaxMarksTxtBoxID, ObtMarksTxtBoxId, PercentageTxtBoxId) {
            var maxMarks = document.getElementById(MaxMarksTxtBoxID).value;
            var obtMarks = document.getElementById(ObtMarksTxtBoxId).value;
            document.getElementById(PercentageTxtBoxId).value = (obtMarks * 100) / maxMarks;
        }
        function validateEmail(val) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(val);
        }

        function validatePageData() {

            //-- First Name validation
            if (document.getElementById('<%=txtFirstName.ClientID %>').value == "") {
                alert('Please enter the first name.');
                document.getElementById('<%=txtFirstName.ClientID %>').focus();
                return false;
            }
            //-- Last Name validation
            if (document.getElementById('<%=dtDOB.ClientID %>').value == "") {
                alert('Please enter Date of Birth.');
                document.getElementById('<%=dtDOB.ClientID %>').focus();
                return false;
            }
            //Date format
           // var reDate = /(?:0[1-9]|[12][0-9]|3[01])\/(?:0[1-9]|1[0-2])\/(?:19|20\d{2})/;
          //  if (reDate.test(document.getElementById('<%=dtDOB.ClientID %>').value)) {
            //    alert("Valid");
           // }
           // alert("InValid");
            //-- Nationality
            if (document.getElementById('<%=txtNationality.ClientID %>').value == "") {
                alert('Please enter nationality');
                document.getElementById('<%=txtNationality.ClientID %>').focus();
                return false;
            }
            //-- Caste
            if (document.getElementById('<%=txtCaste.ClientID %>').value == "") {
                alert('Please enter caste');
                document.getElementById('<%=txtCaste.ClientID %>').focus();
                return false;
            }
            //-- Religion
            if (document.getElementById('<%=txtReligion.ClientID %>').value == "") {
                alert('Please enter religion');
                document.getElementById('<%=txtReligion.ClientID %>').focus();
                return false;
            }
            //-- Mobile Number
            if (document.getElementById('<%=txtMobileNumber.ClientID %>').value == "") {
                alert('Please enter mobile number');
                document.getElementById('<%=txtMobileNumber.ClientID %>').focus();
                return false;
            }
            //-- Email Address
            if (document.getElementById('<%=txtEmailAddress.ClientID %>').value == "") {
                alert('Please enter Email.');
                document.getElementById('<%=txtEmailAddress.ClientID %>').focus();
                return false;
            } else if (!validateEmail(document.getElementById('<%=txtEmailAddress.ClientID %>').value)) {
                alert('Please enter a valid E-Mail address.');
            document.getElementById('<%=txtEmailAddress.ClientID %>').focus();
                return false;
            }
            //-- Correspondesce Address
            if (document.getElementById('<%=txtCorrAddress.ClientID %>').value == "") {
                alert('Please enter Correspondence Address');
                document.getElementById('<%=txtCorrAddress.ClientID %>').focus();
                return false;
            }
            //-- Correspondesce Pin
            if (document.getElementById('<%=txtPinCode.ClientID %>').value == "") {
                alert('Please enter Correspondence Pin Code');
                document.getElementById('<%=txtPinCode.ClientID %>').focus();
              return false;
            }
            //-- Father Name
            if (document.getElementById('<%=txtFatherName.ClientID %>').value == "") {
                    alert('Please enter father name');
                    document.getElementById('<%=txtFatherName.ClientID %>').focus();
                    return false;
            }
            //-- Mother Name
            if (document.getElementById('<%=txtMotherName.ClientID %>').value == "") {
                    alert('Please enter mother name');
                    document.getElementById('<%=txtMotherName.ClientID %>').focus();
                    return false;
           }
            //-- Father's address
            if (document.getElementById('<%=txtAddress.ClientID %>').value == "") {
                alert('Please enter father address');
                document.getElementById('<%=txtAddress.ClientID %>').focus();
                return false;
            }
            //-- Father Email Address
            if (document.getElementById('<%=txtFatherEmail.ClientID %>').value != "") {
                if (!validateEmail(document.getElementById('<%=txtFatherEmail.ClientID %>').value)) {
                    alert('Please enter a valid Father E-Mail address.');
                    document.getElementById('<%=txtFatherEmail.ClientID %>').focus();
                    return false;
                }
            }
            //-- Mother Email Address
            if (document.getElementById('<%=txtMotherEmail.ClientID %>').value != "") {
                if (!validateEmail(document.getElementById('<%=txtMotherEmail.ClientID %>').value)) {
                    alert('Please enter a valid Mother E-Mail address.');
                    document.getElementById('<%=txtMotherEmail.ClientID %>').focus();
                    return false;
                }
            }

            //For Entrance Date Selection
            var rb = document.getElementById('<%=rdoOLExamDates.ClientID%>');
            var radio = rb.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    counter++;
                }
            }
            if (counter == 0) {
                alert("Please select the Olympiad Examination date.");
                document.getElementById('<%=rdoOLExamDates.ClientID %>').focus();
                return false;
            }

            //For Center List (Max 3 centers can be selected)
            var ListBox = document.getElementById('<%=lstCenters.ClientID %>');
            var length = ListBox.length;
            var i = 0;
            var SelectedItemCount = 0;

            for (i = 0; i < length; i++) {

                if (ListBox.options[i].selected) {
                    SelectedItemCount = SelectedItemCount + 1;
                }

                if (SelectedItemCount > 3) {
                    alert('More than 3 centers can not be possible.');
                    document.getElementById('<%=lstCenters.ClientID %>').focus();
                    return false;
                }
            }

            if (SelectedItemCount == 0) {
                alert('Please select the center.');
                document.getElementById('<%=lstCenters.ClientID %>').focus();
                return false;
            }

            //School Address
            if (document.getElementById('<%=lblSchoolAddress.ClientID %>').value == "") {
                alert('Please select the School Name');
                document.getElementById('<%=lblSchoolAddress.ClientID %>').focus();
                return false;
            }
            
            //Declaration
            var chkDeclaration1 = document.getElementById('<%=chkIconfirmThatInformationIHaveGivenIsTrue.ClientID %>');
            var chkDeclaration2 = document.getElementById('<%=chkIUnderstandThatTheDataInThisFormWillNot.ClientID %>');
             var chkDeclaration3 = document.getElementById('<%=chkIConfirmThatIFulfillTheMinimumEligibilityCriteria.ClientID %>');

            if (chkDeclaration1.checked == false || chkDeclaration2.checked == false || chkDeclaration3.checked == false) {
                alert('Please mark the declarations. You have not checked all.');
                document.getElementById('<%=chkIconfirmThatInformationIHaveGivenIsTrue.ClientID %>').focus();
                return false;
            }

            //Final Confirmation
            if (confirm('Do you really wish to submit the Aero Olympiad Application Form?') == true) {
                return true;
            }

            return false;
        }
    </script>
  
   
      <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <img runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height: 60px;"> </img>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>
    <div>
        <ul class="hornav">
            <li class="" id="lstEntrytab"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">New Registration</a></li>
            <li class="current" id="lstListtab"><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">Registration List</a></li>
        </ul>
    </div>
  
    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="aspNetHidden">
                        <div class="style22">
                            <strong>
                                <br />
                                <span class="style24"><span class="auto-style1">NATIONAL AERO OLYMPIAD</span></span> <span class="style23">
                                    <br />
                                    <span class="style29"><span class="style28">Organised & Conducted By Laxmi Narain Verma Memorial
                        Society,
                        <br />
                                    </span></span></span><span class="style29"><span class="style28"><span class="style27">Registered Under Societies Registration Act ,
                        <br />
                                        Delhi Administration</span></span></span><br />
                            </strong>
                        </div>
                      
                        <div class="aspNetHidden">
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="background-color: rgb(180, 255, 204); padding: 0px 15px; border: #a6b0b6 1px solid; box-shadow: 0px 5px 5px #888888; }">
                        <div class="H1">
                               <asp:Label ID="lblHeader" Text="REGISTRATION FORM (Session 2018-19)" runat="server"
                                    ForeColor="Blue" Style="color:blue; font-size: x-large;" />  <%--   <div align="right">
                                        <a href="SearchApplication.aspx">Search & Track your application status</a>
                                </div>--%>
                            </div>
                            <br />
 
                            <div class="h4" style="background-color: rgb(0, 51, 0)">
                                Personal Details
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td class="style4" nowrap="nowrap">Registration Number
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRegistrationNo" Text="Pending" Font-Bold="true" Enabled="false"
                                                runat="server" Style="width: 186px;"></asp:TextBox>
                                        </td>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkOnlineExamOpted" Text="Opted For Online Examination" Font-Bold="true"
                                                runat="server" ForeColor="Blue" Visible="false" Checked="false"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">Applicant's First Name<span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFirstName" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper" AutoCompleteType="None"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator runat="server" ID="NReq" SetFocusOnError = True 
            ControlToValidate="txtFirstName"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />First Name is required." />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE"
            TargetControlID="NReq"
            HighlightCssClass="validatorCalloutHighlight" />--%>
                                        </td>
                                        <td align="right">Middle Name
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMiddleName" runat="server" Style="width: 182px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper" AutoCompleteType="None"></asp:TextBox>
                                        </td>
                                        <td align="right">Last Name
                                        </td>
                                        <td class="style12">
                                            <asp:TextBox ID="txtLastName" runat="server" Style="width: 180px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper"  AutoCompleteType="None"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">Gender
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGender" runat="server" Style="width: 203px;">
                                                <asp:ListItem Value="0">Male</asp:ListItem>
                                                <asp:ListItem Value="1">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">Date Of Birth<span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="dtDOB" Style="width: 182px;"  MaxLength="10" Width="100px" Enabled="true" autocomplete="off" /><br />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtDOB"
                                                CssClass="MyCalendar"  Format="dd/MM/yyyy"/>
                                        </td>
                                        <td>(DD/MM/YYYY)
                                        </td>
                                        <td class="style12"></td>
                                    </tr>
                                    <tr>
                                        <td class="style4">Nationality<span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNationality" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper"></asp:TextBox>
                                        </td>
                                        <td align="right">Status
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEducationStatus" runat="server" Style="width: 200px;" Enabled="false">
                                                <asp:ListItem Value="0">Regular</asp:ListItem>
                                                <asp:ListItem Value="1">Distance</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td class="style12"></td>
                                    </tr>
                                    <tr>
                                        <td class="style4">Caste<span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaste" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper"></asp:TextBox>
                                        </td>
                                        <td align="right">Category
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server" Style="width: 200px;">
                                                <asp:ListItem Value="0">General</asp:ListItem>
                                                <asp:ListItem Value="1">Obc</asp:ListItem>
                                                <asp:ListItem Value="2">SC/ST</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">Religion<span style="color: red;">*</span>
                                        </td>
                                        <td class="style12">
                                            <asp:TextBox ID="txtReligion" runat="server" Style="width: 180px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">Mobile Number<span style="color: red;">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMobileNumber" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptNumberOnly(event)"></asp:TextBox>
                                        </td>
                                        <td align="right">Res. Phone
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtResidencePh" runat="server" Style="width: 186px;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <%--Admission Status--%>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAdmissionStatus" runat="server" Style="width: 200px;" Visible="false">
                                                <asp:ListItem Value="0">Management</asp:ListItem>
                                                <asp:ListItem Value="1">Direct</asp:ListItem>
                                                <asp:ListItem Value="1">Entrance</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">E-mail Address<span style="color: red;">*</span>
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtEmailAddress" runat="server" Width="780px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td class="style4" nowrap="nowrap">Correspondence Address<span style="color: red;">*</span>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox Rows="2" TextMode="MultiLine" ID="txtCorrAddress" runat="server" Width="780px"
                                                CssClass="textareaWithoutMargin"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">Pin Code<span style="color: red;">*</span>
                                        </td>
                                        <td class="style2"> 
                                            <asp:TextBox ID="txtPinCode" MaxLength="6" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptNumberOnly(event)"></asp:TextBox>
                                        </td>
                                        <td class="style1"></td>
                                        <td class="style1"></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td colspan="4" align="left">
                                            <asp:CheckBox OnClick="javascript:fillParmanentAddress(this.checked);" ID="chkFillParmanentAddress"
                                                Text="Parmanent address is same as correspondance address" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Permanent Address
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox Rows="2" TextMode="MultiLine" ID="txtParmAddress" runat="server" Width="780px"
                                                CssClass="textareaWithoutMargin" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pin Code
                                        </td>
                                        <td class="style3">
                                            <asp:TextBox ID="txtParmAddressPinCode" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr id="rwPassport" runat="server" visible="false">
                                        <td>Passport Number
                                        </td>
                                        <td class="style3">
                                            <asp:TextBox ID="txtPassportNumber" runat="server" Style="width: 186px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>Issued at
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassportIssuedAt" runat="server" Width="206px" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                    <tr id="rwTravel" runat="server" visible="false">
                                        <td>Countries Traveled
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCountriesTraveled" runat="server" Style="width: 781px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0)" id="div2" runat="server" visible="false">
                                Parent Information
                            </div>
                            <div>
                                <table>

                                    <tr>
                                        <td class="auto-style2">Father's Name<span style="color: red;">*</span>
                                        </td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="txtFatherName" runat="server" Style="width: 258px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td align="right" class="auto-style4">Mother's Name<span style="color: red;">*</span>
                                        </td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="txtMotherName" runat="server" Style="width: 259px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                                CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15">Father's Mobile
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFatherMobile" runat="server" Style="width: 258px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td align="right" class="auto-style4">Mother's Mobile
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMotherMobile" runat="server" Style="width: 259px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15">Father's E-mail ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFatherEmail" runat="server" Style="width: 258px;" />
                                        </td>
                                        <td align="right" class="style16">Mother's E-mail ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMotherEmail" runat="server" Style="width: 259px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Address
                            <br />
                                            (Work Address)
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAddress" runat="server" Width="792px" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0)">
                                Olympiad Subject Details
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td>Name of the Test you are applying for
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList onChange="javascript:document.getElementById('txtCourseCode').value = this.value;"
                                                ID="ddlCourseApplyingFor" runat="server" Style="width: 663px;">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="rwSubjcCode" runat="server" visible="false">
                                        <td>Subject Code
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox Text="Auto Generate Course code" Enabled="false" ID="txtCourseCode"
                                                runat="server" Font-Bold="true" Style="width: 250px;" />
                                        </td>
                                        <td>Session
                                        </td>
                                        <td>
                                            <asp:TextBox Enabled="false" ID="txtAcademicYr" runat="server" Font-Bold="true" Style="width: 250px;" />
                                        </td>
                                    </tr>
                                    <tr id="rwStudyCenter" runat="server" visible="false">
                                        <td>Opted Study Center
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButton ID="rdoSOA" runat="server" Text="School Of Aeronautics (Delhi)"
                                                GroupName="Study" Checked="true" CssClass="style14" />
                                            <asp:RadioButton ID="rdoSOA_N" runat="server" Text="School Of Aeronautics (Neemrana)"
                                                GroupName="Study" CssClass="style14" />
                                            <asp:RadioButton ID="rdoSOA_F" runat="server" Text="School Of Fashion Designing"
                                                GroupName="Study" CssClass="style14" />
                                        </td>
                                    </tr>
                                    <tr id="rwCenter" runat="server" visible="true">
                                        <td>Opted Examination Center for Level 2 Examination
                                        </td>
                                        <td colspan="3">
                                            <asp:ListBox ID="lstCenters" runat="server" SelectionMode="Multiple" Height="100px"/>  
                                        </td>
                                    </tr>
                                    <tr id="rowOnline" runat="server" visible="true">
                                        <td>
                                            <span class="style13"><em><strong>Olympiad Held On</strong></em></span>
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButtonList ID="rdoOLExamDates" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                                Style="color: #0000FF">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="rowForeign" runat="server" visible="true">
                                        <td>Are you applying as Student?
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButtonList ID="rdoIsApplyingAsAForeignStudent" RepeatDirection="Horizontal"
                                                runat="server">
                                                <asp:ListItem Value="1"  Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div>
                                <table id="rwEntrance" runat="server" visible="false">
                                    <tr>
                                        <td colspan="5" class="info">Entrance Tests Taken (AIEEE, RPET and other....)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Sr.No.
                                        </td>
                                        <td style="font-weight: bold">Name of the Test
                                        </td>
                                        <td style="font-weight: bold">Date of test
                                        </td>
                                        <td style="font-weight: bold">Place
                                        </td>
                                        <td style="font-weight: bold">Rank
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>1.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestName1" runat="server" Style="width: 350px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestDate1" runat="server" Style="width: 100px;" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="EntTestDate1"
                                                CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestPlace1" runat="server" Style="width: 288px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestRank1" runat="server" Style="width: 100px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestName2" runat="server" Style="width: 350px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestDate2" runat="server" Style="width: 100px;" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="EntTestDate2"
                                                CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestPlace2" runat="server" Style="width: 288px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestRank2" runat="server" Style="width: 100px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>3.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestName3" runat="server" Style="width: 350px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestDate3" runat="server" Style="width: 100px;" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="EntTestDate3"
                                                CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestPlace3" runat="server" Style="width: 288px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestRank3" runat="server" Style="width: 100px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>4.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestName4" runat="server" Style="width: 350px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestDate4" runat="server" Style="width: 100px;" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="EntTestDate4"
                                                CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestPlace4" runat="server" Style="width: 288px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EntTestRank4" runat="server" Style="width: 100px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvAplyingReason" runat="server" visible="false">
                                <table>
                                    <tr>
                                        <td style="font-weight: bold">Why are you applying for Aero Olympiad
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtReasonForApply" TextMode="MultiLine" Rows="3" runat="server"
                                                Style="height: 70px; width: 950px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0) ">
                                Qualifications
                            </div>

                            <div>
                                <table>
                                    <tr>
                                        <td colspan="9" class="info">Educational Qualification(Please enter if applicable)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" style="font-weight: bold">Exam
                                        </td>
                                        <td class="style9" style="font-weight: bold">Roll No.
                                        </td>
                                        <td class="style9" style="font-weight: bold">Year
                                        </td>
                                        <td class="style9" style="font-weight: bold">Stream
                                        </td>
                                        <td class="style9" style="font-weight: bold">Board
                                        </td>
                                        <td class="style9" style="font-weight: bold">Obt. Marks
                                        </td>
                                        <td class="style9" style="font-weight: bold">Max. Marks
                                        </td>
                                        <td class="style9" style="font-weight: bold">Percent
                                        </td>
                                        <td class="style9" style="font-weight: bold">Result
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Class - X
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_RollNo" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_Year" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_Stream" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_Board" runat="server" Style="width: 150px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_Marks" runat="server" Style="width: 100px;"
                                                onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassX_MaxMarks','EduQualificationClassX_Marks','EduQualificationClassX_Percent');"
                                                Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassX_Percent" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEduQualificationClassX_Result" runat="server" Style="width: 80px; height: 36px;">
                                                <asp:ListItem Value="0" Selected="True" Text=""></asp:ListItem>
                                                <asp:ListItem Value="1">Pass</asp:ListItem>
                                                <asp:ListItem Value="2">Compartment</asp:ListItem>
                                                <asp:ListItem Value="3">Fail</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="rowQualification2" runat="server" visible="true">
                                        <td>Class - XII
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_RollNo" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_Year" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_Stream" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_Board" runat="server" Style="width: 150px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_Marks" runat="server" Style="width: 100px;"
                                                onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassXII_MaxMarks','EduQualificationClassXII_Marks','EduQualificationClassXII_Percent');"
                                                Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassXII_Percent" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEduQualificationClassXII_Result" runat="server" Style="width: 80px; height: 36px;">
                                                <asp:ListItem Value="0" Selected="True" Text=""></asp:ListItem>
                                                <asp:ListItem Value="1">Pass</asp:ListItem>
                                                <asp:ListItem Value="2">Compartment</asp:ListItem>
                                                <asp:ListItem Value="3">Fail</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="rowQualification3" runat="server" visible="false">
                                        <td>Graduation
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_RollNo" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_Year" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_Stream" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_Board" runat="server" Style="width: 150px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_Marks" runat="server" Style="width: 100px;"
                                                onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassGraduation_MaxMarks','EduQualificationClassGraduation_Marks','EduQualificationClassGraduation_Percent');"
                                                Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassGraduation_Percent" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEduQualificationClassGraduation_Result" runat="server" Style="width: 80px;">
                                                <asp:ListItem Value="0" Selected="True" Text=""></asp:ListItem>
                                                <asp:ListItem Value="1">Pass</asp:ListItem>
                                                <asp:ListItem Value="2">Compartment</asp:ListItem>
                                                <asp:ListItem Value="3">Fail</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="rowQualification4" runat="server" visible="false">
                                        <td>Other
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_RollNo" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_Year" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_Stream" runat="server" Style="width: 100px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_Board" runat="server" Style="width: 150px;" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_Marks" runat="server" Style="width: 100px;"
                                                onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassOther_MaxMarks','EduQualificationClassOther_Marks','EduQualificationClassOther_Percent');"
                                                Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EduQualificationClassOther_Percent" runat="server" Style="width: 70px;" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEduQualificationClassOther_Result" runat="server" Style="width: 80px;">
                                                <asp:ListItem Value="0" Selected="True" Text=""></asp:ListItem>
                                                <asp:ListItem Value="1">Pass</asp:ListItem>
                                                <asp:ListItem Value="2">Compartment</asp:ListItem>
                                                <asp:ListItem Value="3">Fail</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table>
                                    <tr id="rowProfessional" runat="server" visible="false">
                                        <td style="font-weight: bold">Professional / Other Qualification / Awards & Honours
                                        </td>
                                    </tr>
                                    <tr id="rowProfessionalDetail" runat="server" visible="false">
                                        <td>
                                            <asp:TextBox ID="txtProfession_OtherQualificationAwards_Text" TextMode="MultiLine"
                                                runat="server" Style="height: 70px; width: 950px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">School/College Name & Address( Where you are studying as offnow)<span style="color: red;">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">


                                              <asp:UpdatePanel runat="server" ID='UpdatePanel3' UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                  <asp:DropDownList ID="ddlSchool" runat="server" Style="width: 967px;" 
                                                    OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AutoPostBack="true" OnTextChanged="ddlSchool_TextChanged">
                                            </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>

                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSchoolAddress" runat="server" Font-Size="Small" Font-Bold="True" ForeColor="Blue"  />
                                           <%-- <asp:TextBox ID="txtSchoolAddress" TextMode="MultiLine" runat="server" Style="height: 90px; width: 950px;"
                                                CssClass="TextBoxTextUpper" ReadOnly="True" BackColor="#FFFFFB" Font-Size="Small" ForeColor="Black" />--%>
                                        </td>
                                    </tr>
                                    <tr id="rowHobbies" runat="server" visible="false">
                                        <td style="font-weight: bold">Please tell us more about your hobbies and interests
                                        </td>
                                    </tr>
                                    <tr id="rowHobbiesDetails" runat="server" visible="false">
                                        <td style="color: #FFFFCC">
                                            <asp:TextBox ID="txtHobbies" TextMode="MultiLine" runat="server" Style="height: 70px; width: 950px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0) " id="divOtherHeader" runat="server" visible="false">
                                Other Details
                            </div>
                            <div id="divOtherDetails" runat="server" visible="false">
                                <table>
                                    <tr>
                                        <td>Marital Staus
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoMaritalStatus" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Selected="True" Value="0">Single</asp:ListItem>
                                                <asp:ListItem Value="1">Married</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="rwMarital" runat="server" visible="false">
                                        <td>Name of your Spouse
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNameOfSpouse" runat="server" Style="width: 407px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Please specify your blood group type
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBloodGroup" runat="server" Style="width: 407px;" CssClass="TextBoxTextUpper" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Do you have a disability
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoIsPhysicalDisable" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="rwbMedical" runat="server" visible="false">
                                        <td>Do you have any medical / health problem?
                                        </td>
                                        <td>if yes please provide further details to help us to meet your needs
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>If yes, please specify
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDetailsPhysicalDisable_Text" Style="width: 407px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0) ">
                                Further Information
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="7" class="info">How you came to know about National Aero Olympiad
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="Newspaper" ID="chkInforationGotFrom_Newspaper" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Hoarding" ID="chkInforationGotFrom_Hoarding" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Internet" ID="chkInforationGotFrom_Internet" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Representative" ID="chkInforationGotFrom_Representative" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Friends" ID="chkInforationGotFrom_Friends" runat="server" />
                                        </td>
                                        <td>(Please specify)
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtInforationGotFrom_OtherText" Style="width: 137px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color:rgb(0, 51, 0)">
                                Supporting Document's Checklist
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="5" class="info">Proof Of Identity
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="Voter Card" ID="chkProffIdentityVoterCard" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Driving License" ID="chkProffIdentityDrivingLicense" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Passpport" ID="chkProffIdentityPasspport" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Pan Card" ID="chkProffIdentityPanCard" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Adhar Card" ID="chkProffIdentityAdharCard" runat="server" />
                                        </td>
                                        <%--<ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME1" runat="server" TargetControlID="chkProffIdentityVoterCard"
                                            Key="ProofOfIdentity" />
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME2" runat="server" TargetControlID="chkProffIdentityDrivingLicense"
                                            Key="ProofOfIdentity" />
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME3" runat="server" TargetControlID="chkProffIdentityPasspport"
                                            Key="ProofOfIdentity" />
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME4" runat="server" TargetControlID="chkProffIdentityPanCard"
                                            Key="ProofOfIdentity" />
                                        <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME5" runat="server" TargetControlID="chkProffIdentityAdharCard"
                                            Key="ProofOfIdentity" />--%>
                                    </tr>
                                </table>
                                <table id="tblOthDocument" runat="server" visible="false">
                                    <tr>
                                        <td colspan="2" class="info">Other Document
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="Transcripts of qualifications / Photocopy of 10th & 12th marksheet"
                                                ID="chkTranscriptsOfQualifications" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Caste Certificate for SC/ST/OBC" ID="chkCasteCertificateForSCSTOBC"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="10 passport size photographs" ID="chkPassportSizePhotograph"
                                                runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Experience Certificate & Support Documents" ID="chkExperienceCertificateSupportDocuments"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="Address Proof" ID="chkAddressProof" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="For TFWS Income Certificate" ID="chkForTFWSIncomeCertificate"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="Domicile Certificate" ID="chkDomicileCertificate" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox Text="Medical Certificate" ID="chkMedicalCertificate" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0)">
                                Upload Image
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td style="width: 15%;">Upload Image
                                        </td>
                                        <td style="width: 50%;">
                                            <asp:UpdatePanel runat="server" ID='updateUploadImage'>
                                                <ContentTemplate>
                                                    <asp:fileupload id="FileUploadImage" runat="server" xmlns:asp="#unknown" />
                                                    <asp:Button ID="btnUploadImage" runat="server" Text="Upload" OnClick="btnUploadImage_Click"
                                                       ForeColor="black" /><br />
                                                    <asp:Label ID="lblImageError" ForeColor="Red" Font-Bold="true" Visible="false" runat="server"
                                                        Text="The file size exceeds the allowed limit."></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUploadImage" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td colspan="2">
                                            <asp:Image ImageUrl="~/Common/Images/SampleImage.jpg" Style="width: 100px; height: 100px;"
                                                ID="UserImage" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Upload Signature
                                        </td>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID='updateUploadSignature'>
                                                <ContentTemplate>
                                                    <asp:fileupload id="FileUploadSignature" runat="server" xmlns:asp="#unknown" />
                                                    <asp:Button ID="btnUploadSignature" runat="server" Text="Upload" class="btnblack"
                                                        BackColor="#b4ffcc" ForeColor="black" OnClick="btnUploadSignature_Click" /><br />
                                                    <asp:Label ID="lblSignatureError" ForeColor="Red" Font-Bold="true" Visible="false"
                                                        runat="server" Text="The file size exceeds the allowed limit."></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUploadSignature" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td colspan="2"> 
                                            <asp:Image ImageUrl="~/Common/Images/SampleSignature.jpg" Style="width: 260px; height: 50px;"
                                                ID="ImageSignature" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblAllowedSignatureText" runat="server" Text="" Font-Italic="true"></asp:Label>
                                            <asp:Label ID="lblAllowedImageText" runat="server" Font-Italic="true" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(160, 230, 190) ">
                            </div>

                            <div id="divOfflinePaymentOption">
                                <table id="tblPayment" runat="server" visible="false">
                                    <tr>
                                        <td colspan="4">
                                            <asp:RadioButtonList onChange="javascript:alert('asdas');" ID="RadioButtonList1"
                                                RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Selected="True" Value="1">Offline Payment</asp:ListItem>
                                                <asp:ListItem Value="0">Online Payment</asp:ListItem>
                                                <asp:ListItem Value="2">Pay at Olympiad Center</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Draft No.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDraftNumber" runat="server" Style="width: 252px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                        </td>
                                        <td align="right" class="style10">Bank Name
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBankName" runat="server" Style="width: 252px;" onkeypress="return fnCommonAcceptCharacterOnly(event)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Amount
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" Style="width: 252px;" />
                                        </td>
                                        <td align="right" class="style10">Draft Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDraftDate" runat="server" Style="width: 252px;" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtDraftDate"
                                                CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Current Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentDate" runat="server" Style="width: 252px;" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtCurrentDate"
                                                CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                        </td>
                                        <td align="right" class="style10">Place
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlace" runat="server" Style="width: 252px;" onkeypress="return fnCommonAcceptCharacterOnly(event)" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="h4" style="background-color: rgb(0, 51, 0) ">
                                Declaration
                            </div>
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="I confirm that information I have given is true, complete and accurate, and no information has been omitted"
                                                ID="chkIconfirmThatInformationIHaveGivenIsTrue" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="I understand that the data in this form will not be provided to any external organisation."
                                                ID="chkIUnderstandThatTheDataInThisFormWillNot" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="I confirm that I fulfill the minimum eligibility criteria required for the Olympiad, i.e I am class X Pass."
                                                ID="chkIConfirmThatIFulfillTheMinimumEligibilityCriteria" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox Text="" ID="chkIConfirmThatIHaveReadTheEnclosedDeclaration" Checked="true"
                                                runat="server" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="text-align: center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validatePageData();"
                                    class="btnblack" BackColor="#b4ffcc" ForeColor="black" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btnblack" BackColor="#b4ffcc"
                                    ForeColor="black" OnClick="btnCancel_Click" />
                            </div>
                            <div style="display: none;" id="divOnlinePaymentOption">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red" Text="This functionality will be introduced shortly..."></asp:Label>
                            </div>
                            <asp:HiddenField ID="hdRegistrationNo" Value="" runat="server" />
                            <asp:HiddenField ID="hdID" Value="" runat="server" />
                            <asp:HiddenField ID="hdPhoto" Value="" runat="server" />
                            <asp:HiddenField ID="hdSignature" Value="" runat="server" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                <%-- <asp:PostBackTrigger ControlID="btnUploadImage"  />
                 <asp:PostBackTrigger ControlID="FileUploadSignature"  />--%>
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

                               <div style="width: 98%; overflow: scroll; border: 1px solid #000; max-height: 390px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,SCHOOL_ID"
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
                                    <asp:BoundField DataField="RegistrationNumber" HeaderText="Enrollment No*" ItemStyle-Width="120px" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name*" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="MiddleName" HeaderText="Middle Name*" ItemStyle-Width="50px" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name*" ItemStyle-Width="50px"/>
                                    <asp:BoundField DataField="Gender" HeaderText="Gender*" HeaderStyle-Width="500px" />  
                                    <asp:BoundField DataField="DOB" HeaderText="DOB*" ItemStyle-Width="150px"/>
                                    <asp:BoundField DataField="Email" HeaderText="Email(Candidate)*" ItemStyle-Width="200px" HeaderStyle-Wrap="true" />
                                    <asp:BoundField DataField="MobileNumber" HeaderText="MobileNo (Candidate)*" HeaderStyle-Wrap="true" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="FatherName" HeaderText="Father Name*" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="MotherName" HeaderText="Mother Name*" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="OptedStudyCenter" HeaderText="School Name*" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="EnrolledOn" HeaderText="Enrollment On" ItemStyle-Width="100px" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
   
    </asp:Content>
