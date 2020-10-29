<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admission.aspx.cs" Inherits="Operation_Admission" MasterPageFile="~/Operation/CRMOperationArea2.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <link href="Common/CSS/ApplicationForm.css" type="text/css" rel="Stylesheet" />
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

        .style5
        {
            width: 159px;
        }

        .style6
        {
            width: 261px;
        }

        .style7
        {
            width: 159px;
            height: 24px;
        }

        .style8
        {
            width: 299px;
        }

        .style9
        {
            height: 18px;
        }

        .button
        {
            border-top: 1px solid #2ca8f5;
            background: #65a9d7;
            background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
            background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
            background: -moz-linear-gradient(top, #3e779d, #65a9d7);
            background: -ms-linear-gradient(top, #3e779d, #65a9d7);
            background: #65a9d7;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: #FFFFFF;
            font-size: 14px;
            font-family: Arial, Helvetica, sans-serif;
            text-decoration: none;
            vertical-align: middle;
            font-weight: 700;
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
                background: #1b435e;
            }

        .style11
        {
            height: 38px;
        }

        .style12
        {
            width: 182px;
        }
        .auto-style1
        {
            width: 159px;
            height: 38px;
        }
        .auto-style2
        {
            width: 261px;
            height: 38px;
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
                document.getElementById("MiddleContent_txtParmAddress").value = document.getElementById("MiddleContent_txtCorrAddress").value;
                document.getElementById("MiddleContent_txtParmAddressPinCode").value = document.getElementById("MiddleContent_txtPinCode").value;
            }
            else {
                document.getElementById("MiddleContent_txtParmAddress").value = "";
                document.getElementById("MiddleContent_txtParmAddressPinCode").value = "";
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
            if (document.getElementById("MiddleContent_txtFirstName").value.trim() == "") {
                alert('Please enter the first name.');
                document.getElementById("MiddleContent_txtFirstName").focus();
                return false;
            }

            //-- Last Name validation
            if (document.getElementById("MiddleContent_dtDOB").value == "") {
                alert('Please enter Date of Birth.');
                document.getElementById("MiddleContent_dtDOB").focus();
                return false;
            }

            //-- Nationality
            if (document.getElementById("MiddleContent_txtNationality").value.trim() == "") {
                alert('Please enter nationality');
                document.getElementById("MiddleContent_txtNationality").focus();
                return false;
            }

            //-- Mobile Number
            if (document.getElementById("MiddleContent_txtMobileNumber").value.trim() == "") {
                alert('Please enter mobile number');
                document.getElementById("MiddleContent_txtMobileNumber").focus();
                return false;
            }

            //-- Email Address
            if (document.getElementById("MiddleContent_txtEmailAddress").value.trim() == "") {
                alert('Please enter Email.');
                document.getElementById("MiddleContent_txtEmailAddress").focus();
                return false;
            } else if (!validateEmail(document.getElementById("MiddleContent_txtEmailAddress").value)) {
                alert('Please enter valid mail address.');
                document.getElementById("MiddleContent_txtEmailAddress").focus();
                return false;
            }

            //-- fATHER Email Address
            if (document.getElementById("MiddleContent_txtFatherEmail").value.trim() != "") {
                if (!validateEmail(document.getElementById("MiddleContent_txtFatherEmail").value)) {
                    alert('Please enter valid father mail address.');
                    document.getElementById("MiddleContent_txtFatherEmail").focus();
                    return false;
                }
            }

            //-- mOTHER Email Address
            if (document.getElementById("MiddleContent_txtMotherEmail").value.trim() != "") {
                if (!validateEmail(document.getElementById("MiddleContent_txtMotherEmail").value)) {
                    alert('Please enter valid mother mail address.');
                    document.getElementById("MiddleContent_txtMotherEmail").focus();
                    return false;
                }
            }

            //-- Correspondence Address
            if (document.getElementById("MiddleContent_txtCorrAddress").value.trim() == "") {
                alert('Please enter correspondence address');
                document.getElementById("MiddleContent_txtCorrAddress").focus();
                return false;
            }

            //-- Permanent Address
            if (document.getElementById("MiddleContent_txtParmAddress").value.trim() == "") {
                alert('Please enter permanent address');
                document.getElementById("MiddleContent_txtParmAddress").focus();
                return false;
            }

            //-- Father Name
            if (document.getElementById("MiddleContent_txtFatherName").value.trim() == "") {
                alert('Please enter father name');
                document.getElementById("MiddleContent_txtFatherName").focus();
                return false;
            }

            //-- Mother Name
            if (document.getElementById("MiddleContent_txtMotherName").value.trim() == "") {
                alert('Please enter mother name');
                document.getElementById("MiddleContent_txtMotherName").focus();
                return false;
            }

            //-- Father's address
            if (document.getElementById("MiddleContent_txtAddress").value.trim() == "") {
                alert('Please enter father address');
                document.getElementById("MiddleContent_txtAddress").focus();
                return false;
            }

            if (document.getElementById('MiddleContent_ddlCourseApplyingFor').value.trim() == "Select" || document.getElementById('MiddleContent_txtCourseCode').value.trim() == "Auto Generate Course code") {
                alert('Please select the Acedemic Course');
                document.getElementById('MiddleContent_ddlCourseApplyingFor').focus();
                return false;
            }

            if (document.getElementById('MiddleContent_ddlFinCourseApplyingFor').value.trim() == "Select" || document.getElementById('MiddleContent_txtFinCourseCode').value.trim() == "Auto Generate Course code") {
                alert('Please select the Financial Course ');
                document.getElementById('MiddleContent_ddlFinCourseApplyingFor').focus();
                return false;
            }

            if (document.getElementById('MiddleContent_chkCouncellor').checked == true && document.getElementById('MiddleContent_chkConsultant').checked == true) {
                alert('Please select either option Councellor or  Consultant');
                document.getElementById("chkCouncellor").focus();
                return false;
            }

            if (document.getElementById('MiddleContent_chkCouncellor').checked == true && document.getElementById('MiddleContent_ddlCouncellor').value == "0") {
                alert('Please select Councellor name');
                document.getElementById("MiddleContent_ddlCouncellor").focus();
                return false;
            }

            if (document.getElementById('MiddleContent_chkConsultant').checked == true && document.getElementById('MiddleContent_txtConsultantName').value.trim() == "") {
                alert('Please enter  Consultant name');
                document.getElementById("MiddleContent_txtConsultantName").focus();
                return false;
            }

            //Proof of document
            if (document.getElementById("MiddleContent_chkDiplomaCheck").checked == false) {
                if (document.getElementById("MiddleContent_chkProffIdentityAdharCard").checked == false || document.getElementById("MiddleContent_chkTranscriptsOfQualifications").checked == false) {
                    alert('The Aadhar/12 certificate is mandatory at the time of admission.');
                    document.getElementById("MiddleContent_chkProffIdentityAdharCard").focus();
                }
            }
             
            if (document.getElementById("MiddleContent_chkDiplomaCheck").checked == true) {
                if (document.getElementById("MiddleContent_chkProffIdentityAdharCard").checked == false) {
                    alert('The Aadhar/12 certificate is mandatory at the time of admission for compartmental student.');
                    document.getElementById("MiddleContent_chkProffIdentityAdharCard").focus();
                }
            }


            //Declaration
            var chkDeclaration1 = document.getElementById("MiddleContent_chkIconfirmThatInformationIHaveGivenIsTrue");
            var chkDeclaration2 = document.getElementById("MiddleContent_chkIUnderstandThatTheDataInThisFormWillNot");
            var chkDeclaration3 = document.getElementById("MiddleContent_chkIConfirmThatIFulfillTheMinimumEligibilityCriteria");
            var chkDeclaration34 = document.getElementById("MiddleContent_chkIConfirmThatIHaveReadTheEnclosedDeclaration");

            if (chkDeclaration1.checked == false || chkDeclaration2.checked == false || chkDeclaration3.checked == false) {
                alert('Please mark the declarations. You have not checked all.');
                document.getElementById("MiddleContent_chkIconfirmThatInformationIHaveGivenIsTrue").focus();
                return false;
            }

            //Final Confirmation
            if (confirm('Do you really wish to submit the Application Form?') == true) {
                return true;
            }

            return false;
        }

        function goBack() {

            history.back();
            return false;
        }
    </script>

    <div style="background-color: #b5d0e8; padding: 0px 5px; border: #a6b0b6 1px solid; box-shadow: 0px 5px 5px #888888; }">
        <div class="H1">
            <center>
                ADMISSION FORM |<asp:Label ID="lblWorkingLocation" runat="server" Font-Bold="true"
                    Style="color: #000099" />|</center>
            <div align="right">
            </div>
        </div>

        <div class="h4">
            Personal Details
        </div>
        <div>
            <asp:Label ID="lblRegDate" Text="" runat="server" Font-Bold="true" Style="color: #000099;"
                Visible="true" />
            <table>
                <tr>
                    <td class="style4">Registration Number
                    </td>
                    <td>
                        <asp:TextBox ID="txtRegistrationNo" Font-Bold="false" Enabled="false" runat="server"
                            Style="width: 186px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">Applicant's First Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                    </td>
                    <td align="right">Middle Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtMiddleName" runat="server" Style="width: 182px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                    </td>
                    <td align="right">Last Name
                    </td>
                    <td class="style12">
                        <asp:TextBox ID="txtLastName" runat="server" Style="width: 180px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">Gender
                    </td>
                    <td class="style12">
                        <asp:DropDownList ID="ddlGender" runat="server" Style="width: 100%;">
                            <asp:ListItem Value="0">Male</asp:ListItem>
                            <asp:ListItem Value="1">Female</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">Date Of Birth
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="dtDOB" autocomplete="off" Style="width: 182px;" /><br />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtDOB"
                            CssClass="MyCalendar" Format="dd/MM/yyyy" />
                    </td>
                    <td>(DD/MM/YYYY)
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="style4">Nationality
                    </td>
                    <td>
                        <asp:TextBox ID="txtNationality" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                    </td>
                    <td align="right">Status
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEducationStatus" runat="server" Style="width: 202px;"   >
                            <asp:ListItem Value="0">Regular</asp:ListItem>
                            <asp:ListItem Value="1">Distance</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">Category
                    </td>
                    <td class="style12">
                        <asp:DropDownList ID="ddlCategory" runat="server" Style="width: 100%;">
                            <asp:ListItem Value="0">General</asp:ListItem>
                            <asp:ListItem Value="1">Tuition fee waiver scheme</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="rowCategory" runat="server" visible="false">
                    <td class="style4">Caste
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaste" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                    </td>
                    <td align="right"></td>
                    <td></td>
                    <td>Religion
                    </td>
                    <td>
                        <asp:TextBox ID="txtReligion" runat="server" Style="width: 180px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4">Mobile Number
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobileNumber" MaxLength="10" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptNumberOnly(event)"></asp:TextBox>
                    </td>
                    <td>Admission Status
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAdmissionStatus" runat="server" Style="width: 202px;">
                            <asp:ListItem Value="0">Management</asp:ListItem>
                            <asp:ListItem Value="1">Direct</asp:ListItem>
                            <asp:ListItem Value="1">Entrance</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td class="style12"></td>
                </tr>
                <tr>
                    <td class="style4">Residence Phone
                    </td>
                    <td class="style12">
                        <asp:TextBox ID="txtResidencePh" runat="server" Style="width: 186px;"  MaxLength="11"></asp:TextBox>
                    </td>
                    <td align="right">E-mail Address
                    </td>
                    <td colspan="3" >
                        <asp:TextBox ID="txtEmailAddress" runat="server" Style="width: 490px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="style4" nowrap="nowrap">Correspondence Address
                    </td>
                    <td colspan="3"  style="width:800px;">
                        <asp:TextBox Rows="2" style="width:100%;" TextMode="MultiLine" ID="txtCorrAddress" runat="server"  ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Pin Code
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPinCode"  MaxLength="6" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptNumberOnly(event)"></asp:TextBox>
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
                    <td colspan="3" style="width:800px;">
                        <asp:TextBox Rows="2" TextMode="MultiLine" ID="txtParmAddress" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <td>Pin Code
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtParmAddressPinCode"  MaxLength="6" runat="server" Style="width: 186px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Passport Number
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtPassportNumber" runat="server" Style="width: 186px;" />
                    </td>
                    <td>Issued at
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassportIssuedAt" runat="server" Style="width: 100%;" />
                    </td>
                </tr>
                <tr>
                    <td>Countries Traveled
                    </td>
                    <td colspan="3" style="width:800px;">
                        <asp:TextBox ID="txtCountriesTraveled" runat="server" Style="width: 100%;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="4" class="info"><strong>Parent Information</strong>
                    </td>
                </tr>
                <tr>
                    <td class="style5">Father's Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtFatherName" runat="server" Style="width: 248px;" onkeypress="return fnCommonAcceptCharacterOnly(event)" />
                    </td>
                    <td style="width:0px;"></td>
                    <td align="right" class="style6">Mother's Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtMotherName" runat="server" Style="width: 248px;" onkeypress="return fnCommonAcceptCharacterOnly(event)" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Father's Mobile
                    </td>
                    <td class="style11">
                        <asp:TextBox ID="txtFatherMobile"  MaxLength="10" runat="server" Style="width: 248px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                    
                    </td><td>
                    </td>
                    <td align="right" class="auto-style2">Mother's Mobile
                    </td>
                    <td class="style11">
                        <asp:TextBox ID="txtMotherMobile"  MaxLength="10" runat="server" Style="width: 248px;" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                    </td>
                </tr>
                <tr>
                    <td class="style5">Father's E-mail ID
                    </td>
                    <td>
                        <asp:TextBox ID="txtFatherEmail" runat="server" Style="width: 248px;"  />
                    </td><td>
                    </td>
                    <td align="right" class="style6">Mother's E-mail ID
                    </td>
                    <td>
                        <asp:TextBox ID="txtMotherEmail" runat="server" Style="width: 248px;" />
                    </td>
                </tr>
                <tr>
                    <td class="style7">Address
                    </td>
                    <td colspan="4" style ="width:845px;" >
                        <asp:TextBox ID="txtAddress" runat="server" Style="width: 95%;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="h4">
            Course Details
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="4">
                        <b>Academic</b>
                    </td>
                </tr>
                <tr>
                    <td>Name of the Course you are applying for
                    </td>
                    <td colspan="3">
                        <asp:DropDownList onChange="javascript:document.getElementById('MiddleContent_txtCourseCode').value = this.value;"
                            ID="ddlCourseApplyingFor" runat="server" Style="width: 100%;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Course Code
                    </td>
                    <td class="style8">
                        <asp:TextBox Text="Auto Generate Course code" Enabled="false" ID="txtCourseCode"
                            runat="server" Font-Bold="false" Style="width: 250px;" />
                    </td>
                    <td>Academic Year
                    </td>
                    <td>
                        <asp:TextBox Enabled="false" ID="txtAcademicYr" runat="server" Font-Bold="true" Style="width: 250px;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>Financial</b>
                    </td>
                </tr>
                <tr>
                    <td>Name of the Course you are applying for
                    </td>
                    <td colspan="3">
                        <asp:DropDownList onChange="javascript:document.getElementById('MiddleContent_txtFinCourseCode').value = this.value;"
                            ID="ddlFinCourseApplyingFor" runat="server" Style="width: 100%;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Course Code
                    </td>
                    <td class="style8">
                        <asp:TextBox Text="Auto Generate Course code" Enabled="false" ID="txtFinCourseCode"
                            runat="server" Font-Bold="false" Style="width: 250px;" />
                    </td>
                    <td>Academic Year
                    </td>
                    <td>
                        <asp:TextBox Enabled="false" ID="txtFinAcademicYr" runat="server" Font-Bold="true"
                            Style="width: 250px;" />
                    </td>
                </tr>
                <tr id="rowOPT" runat="server" visible="false">
                    <td>Opted Additional Subject
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlAdditinalSubject" runat="server" Style="width: 647px;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="rowOPT1" runat="server" visible="false">
                    <td>Opt Examination Center
                    </td>
                    <td colspan="3">
                        <asp:CheckBoxList ID="chkListCenters" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr id="rowOPT2" runat="server" visible="false">
                    <td>Online Registration No.
                    </td>
                    <td colspan="3" style="visibility: hidden;">
                        <asp:TextBox ID="txtOnlineRegNo" runat="server" Text="" MaxLength="10" Font-Bold="true"
                            Style="width: 250px;">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Study Center</strong>
                    </td>
                    <td colspan="3">
                        <asp:RadioButton ID="rdoCenter1" runat="server" Text="Old DLF (Sec-14)" GroupName="StudyCenter"
                            Checked="true" Style="font-weight: 700" />
                    </td>
                </tr>
                <tr>
                    <td>Are you applying as a Foreign / NRI Student?
                    </td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rdoIsApplyingAsAForeignStudent" RepeatDirection="Horizontal"
                            runat="server">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div id="dvEntrance" runat="server" visible="false">
            <table>
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
                        <asp:TextBox ID="EntTestName1" runat="server" Style="width: 250px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestDate1" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="EntTestDate1"
                            CssClass="MyCalendar" Format="dd/MM/yyyy" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestPlace1" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestRank1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>2.
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestName2" runat="server" Style="width: 250px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestDate2" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="EntTestDate2"
                            CssClass="MyCalendar" Format="dd/MM/yyyy" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestPlace2" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestRank2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>3.
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestName3" runat="server" Style="width: 250px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestDate3" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="EntTestDate3"
                            CssClass="MyCalendar" Format="dd/MM/yyyy" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestPlace3" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestRank3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>4.
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestName4" runat="server" Style="width: 250px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestDate4" runat="server" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="EntTestDate4"
                            CssClass="MyCalendar" Format="dd/MM/yyyy" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestPlace4" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="EntTestRank4" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td style="font-weight: bold">Why are you applying for a programme at this organization
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtReasonForApply" TextMode="MultiLine" Rows="3" runat="server"
                            Style="height: 70px; width: 685px;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="h4">
            Qualifications
        </div>
        <asp:CheckBox ID="chkDiplomaCheck" Text="If compartment Student, Please check." runat="server" style="color: #FF0000" />
        <br/> 
        <div class="info">
            Educational Qualification
        </div>
        <div>
            <table>
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
                        <asp:TextBox ID="EduQualificationClassX_Board" runat="server"  Style="width: 400px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassX_Marks" runat="server" Style="width: 100px;"
                            onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4" />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassX_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassX_MaxMarks','EduQualificationClassX_Marks','EduQualificationClassX_Percent');"
                            Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassX_Percent" runat="server" Style="width: 70px;"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEduQualificationClassX_Result" runat="server" Style="width: 105px; height:35px;">
                            <asp:ListItem Value="1">Pass</asp:ListItem>
                            <asp:ListItem Value="2">Compartment</asp:ListItem>
                            <asp:ListItem Value="3">Fail</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
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
                        <asp:TextBox ID="EduQualificationClassXII_Board" runat="server"   Style="width: 400px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassXII_Marks" runat="server" Style="width: 100px;"
                            onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassXII_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassXII_MaxMarks','EduQualificationClassXII_Marks','EduQualificationClassXII_Percent');"
                            Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassXII_Percent" runat="server" Style="width: 70px;"  MaxLength="6"  />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEduQualificationClassXII_Result" runat="server" Style="width: 105px;  height:35px;">
                            <asp:ListItem Value="1">Pass</asp:ListItem>
                            <asp:ListItem Value="2">Compartment</asp:ListItem>
                            <asp:ListItem Value="3">Fail</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
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
                        <asp:TextBox ID="EduQualificationClassGraduation_Board" runat="server"  Style="width: 400px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassGraduation_Marks" runat="server" Style="width: 100px;"
                            onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassGraduation_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassGraduation_MaxMarks','EduQualificationClassGraduation_Marks','EduQualificationClassGraduation_Percent');"
                            Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassGraduation_Percent" runat="server" Style="width: 70px;"  MaxLength="6"  />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEduQualificationClassGraduation_Result" runat="server" Style="width: 105px;  height:35px;">
                            <asp:ListItem Value="1">Pass</asp:ListItem>
                            <asp:ListItem Value="2">Compartment</asp:ListItem>
                            <asp:ListItem Value="3">Fail</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
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
                        <asp:TextBox ID="EduQualificationClassOther_Board" runat="server"  Style="width: 400px;" />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassOther_Marks" runat="server" Style="width: 100px;"
                            onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassOther_MaxMarks" runat="server" onblur="calculatePercentage('EduQualificationClassOther_MaxMarks','EduQualificationClassOther_Marks','EduQualificationClassOther_Percent');"
                            Style="width: 70px;" onkeypress="return fnCommonAcceptNumberOnly(event)"  MaxLength="4"  />
                    </td>
                    <td>
                        <asp:TextBox ID="EduQualificationClassOther_Percent" runat="server" Style="width: 70px;"  MaxLength="6"  />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEduQualificationClassOther_Result" runat="server" Style="width: 105px;  height:35px;">
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
                <tr>
                    <td style="font-weight: bold">Professional / Other Qualification / Awards & Honours
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtProfession_OtherQualificationAwards_Text" TextMode="MultiLine"
                            runat="server" Style="height: 70px; width: 685px;" />
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Please enter details of any work experience which is relevant to your application
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtWorkExperience" TextMode="MultiLine" runat="server" Style="height: 70px; width: 685px;" />
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Please tell us more about your hobbies and interests
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtHobbies" TextMode="MultiLine" runat="server" Style="height: 70px; width: 685px;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="h4">
            Other Details
        </div>
        <div>
            <table>
                <tr>
                    <td>Marital Staus
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoMaritalStatus" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Selected="True" Value="0">Single</asp:ListItem>
                            <%-- <asp:ListItem Value="1">Married</asp:ListItem>--%>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>Name of your Spouse
                    </td>
                    <td>
                        <asp:TextBox ID="txtNameOfSpouse" runat="server" Style="width: 407px;" />
                    </td>
                </tr>
                <tr>
                    <td>Please specify your blood group type
                    </td>
                    <td>
                        <asp:TextBox ID="txtBloodGroup" runat="server" Style="width: 407px;" />
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
                <tr>
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
        <div class="h4">
            Further Information
        </div>
        <div>
            <table>
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
                        <asp:CheckBox Text="Direct" ID="chkInforationGotFrom_Direct" runat="server" />
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
        <div class="h4">
            Counsellor/Consultant Information (Please select our representative who assisted
            you or leave it blank)
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkCouncellor" Text="Councellor" runat="server" ToolTip="check if admission throough Councellor's account" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCouncellor" runat="server" Height="35px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkConsultant" Text="Consultant" runat="server" ToolTip="check if admission throough Councellor's account" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtConsultantName" MaxLength="200" Style="width: 407px;"
                            ToolTip="check if admission throough Consultant's account" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="h4">
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
                        <asp:CheckBox Text="Adhar Card" ID="chkProffIdentityAdharCard" runat="server" style="color: #FF0000" />
                    </td>
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME1" runat="server" TargetControlID="chkProffIdentityVoterCard"
                        Key="ProofOfIdentity" />
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME2" runat="server" TargetControlID="chkProffIdentityDrivingLicense"
                        Key="ProofOfIdentity" />
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME3" runat="server" TargetControlID="chkProffIdentityPasspport"
                        Key="ProofOfIdentity" />
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME4" runat="server" TargetControlID="chkProffIdentityPanCard"
                        Key="ProofOfIdentity" />
                    <ajaxToolkit:MutuallyExclusiveCheckBoxExtender ID="ME5" runat="server" TargetControlID="chkProffIdentityAdharCard"
                        Key="ProofOfIdentity" />
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="2" class="info">Other Document
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="Transcripts of qualifications / Photocopy of 10th & 12th marksheet"
                            ID="chkTranscriptsOfQualifications" runat="server" style="color: #FF0000" />
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
        <%--    <div class="h4">Upload Image</div>

    <div>
     
    <table>
    <tr>
    <td>Upload Image</td>
    <td><asp:FileUpload ID="FileUploadImage" runat="server"/>
        <asp:Button ID="btnUploadImage" runat="server" Text="Upload" class="button" 
            onclick="btnUploadImage_Click"/><br /><asp:Label ID="lblImageError" ForeColor="Red" Font-Bold="true" Visible="false" runat="server" Text="The file size exceeds the allowed limit."></asp:Label></td>
    <td><asp:Image ImageUrl="~/Common/Images/SampleImage.jpg" style="width:150; height:150px;" ID="UserImage" runat="server" /></td>
    <td><asp:Label ID="lblAllowedImageText" runat="server" Text=""></asp:Label></td>
    </tr>

    <tr>
    <td>Upload Signature</td>
    <td><asp:FileUpload ID="FileUploadSignature" runat="server" />
        <asp:Button ID="btnUploadSignature" runat="server" Text="Upload" class="button" 
            onclick="btnUploadSignature_Click"/><br /><asp:Label ID="lblSignatureError" ForeColor="Red" Font-Bold="true" Visible="false" runat="server" Text="The file size exceeds the allowed limit."></asp:Label></td>
    <td><asp:Image ImageUrl="~/Common/Images/SampleSignature.jpg" style="width:260px; height:50px;" ID="ImageSignature" runat="server" /></td>
    <td><asp:Label ID="lblAllowedSignatureText" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr><td><br /></td></tr>
    
    </table>
    
    </div>--%>
        <div class="h4">
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
                    <td class="style11">
                        <asp:CheckBox Text="I understand that the data in this form will not be provided to any external organisation, but will be used to provide me with further <br/> information on study opportunities here."
                            ID="chkIUnderstandThatTheDataInThisFormWillNot" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="I confirm that I fulfill the minimum eligibility criteria required for the course that I am applying for"
                            ID="chkIConfirmThatIFulfillTheMinimumEligibilityCriteria" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="" ID="chkIConfirmThatIHaveReadTheEnclosedDeclaration" Checked="true"
                            Visible="false" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <%-- </table>--%>
    </div>
    <div class="h4">
        <asp:Button ID="btnSubmit" runat="server" Text="Start Admission Workflow" class="button"
            OnClick="btnSubmit_Click" OnClientClick="return validatePageData();" BorderStyle="Solid"
            BorderWidth="1px" />
        <asp:Button ID="btnCancel" runat="server" Text="Back to Main Page" class="button"
            BorderStyle="Solid" BorderWidth="1px" OnClientClick="return goBack();" />
    </div>
    <div style="display: none;" id="divOnlinePaymentOption">
        <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red" Text="This functionality will be introduced shortly...">
        </asp:Label>
    </div>
    <div>
        <asp:HiddenField ID="hdPhoto" runat="server" Value="0" />
        <asp:HiddenField ID="hdSignature" runat="server" Value="0" />
        <asp:HiddenField ID="hdAlloModCouncellor" runat="server" Value="N" />
    </div>
    <%--    </div>--%>
</asp:Content>
