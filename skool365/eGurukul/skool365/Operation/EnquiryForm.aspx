<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMUsers.Master" AutoEventWireup="true" CodeFile="EnquiryForm.aspx.cs" Inherits="Operation_EnquiryForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="Server">
 
 
        <style type="text/css">
            div#globalWrap
            {
                width: 820px;
                margin: 0 auto;
                text-align: left;
                padding: 10px 10px 0px;
                background: #fff;
                border: #f6f6f3 1px solid;
            }

            div#searchBar
            {
                height: 25px;
                padding: 5px 5px 5px 5px;
                color: #fff;
                background: #ffe001;
                text-align: left;
                position: relative;
                width: 800px;
            }

                div#searchBar div.topic
                {
                    position: absolute;
                    top: 5px;
                    left: 10px;
                }

            div.midCtnt
            {
                width: 813px;
                margin: 0 auto;
            }

            .h3
            {
                font-size: 20px;
                color: #881313;
                margin: 0;
            }

            .text
            {
                color: black;
                padding: 5px;
                text-align: justify;
                font-size: 12px;
                background-color: #e1e0d6;
                width: 800px;
            }

            .firstrow
            {
                background-color: #dedfd2;
                height: 30px;
            }

                .firstrow span
                {
                    width: 101px;
                    height: 15px;
                    font-size: 11px;
                    padding-left: 10px;
                }

            .required
            {
                color: #AE002E;
            }

            #form .tblenquiry td.secondrol,
            #form #tblOrderForm td.secondrol
            {
                width: 30em;
            }

            #globalWrap .midCtnt #form #flaDetails td.secondrol,
            #globalWrap .midCtnt #form #flaQuestions td.secondrol
            {
                width: 70%;
            }

            #form table.tblenquiry
            {
                width: 810px;
            }


            #form .tblenquiry td.secondrol,
            #form #tblOrderForm td.secondrol
            {
                width: 60em;
            }

            table
            {
                margin: 0;
                padding: 0;
                border-top: 1px solid #dfe1e3;
                border-left: 1px solid #dfe1e3;
                width: 100%;
            }

            * html table
            {
                font-size: 1em;
            }

            th, td
            {
                margin: 0;
                padding: 0px 0px 0px 8px;
                border-right: 1px solid #dfe1e3;
                border-bottom: 1px solid #dfe1e3;
            }

            th
            {
                background: #f6f6f3;
                /*font-weight:      normal;*/
            }

            table.sitemapTable
            {
                border-top: 0px!important;
                border-left: 0px!important;
                width: auto!important;
            }

                table.sitemapTable th, table.sitemapTable td
                {
                    border-right: 0px!important;
                    border-bottom: 0px!important;
                    background: none!important;
                }


            #form
            {
                margin: 0;
                padding: 0;
            }

            input.inputLng
            {
                width: 220px;
                border: 1px solid #dfe1e3;
            }


            /* form template */
            div.formRow
            {
                width: 716px;
                widt\h: 696px;
                padding: 10px;
                background: #f6f6f3;
            }

            div.formLeftSide
            {
                width: 347px;
                width: 337px;
                padding-right: 10px;
                border-right: 1px solid #a2aab0;
                float: left;
            }

            div.formRightSide
            {
                width: 347px;
                width: 337px;
                padding-left: 10px;
                border-left: 1px solid #fff;
                float: left;
            }

            div.sideNoBdr
            {
                border-left: 0px!important;
                border-right: 0px!important;
            }

            div.formRow span.highlight
            {
                float: none!important;
                display: inline!important;
            }

            div.formRow label, div.formRow span
            {
                display: block;
                float: left;
            }

                div.formRow label.lblSht, div.formRow span.lblSht
                {
                    width: 130px;
                }

                div.formRow label.lblMed, div.formRow span.lblMed
                {
                    width: 236px;
                }

                div.formRow label.lblLng, div.formRow span.lblLng
                {
                    width: 370px;
                }

            input, select, textarea
            {
                border: 1px solid #a2aab0;
                font-size: 14px;
            }

            div.formRow input, div.formRow select, div.formRow textarea
            {
                float: left;
            }

            input.inputSht
            {
                width: 95px;
            }

            input.inputMed
            {
                width: 200px;
            }

            input.inputExtLng
            {
                width: 280px;
                border: 1px solid #a2aab0;
            }

            select.selectSht
            {
                width: 98px;
            }

            select.selectMed
            {
                width: 203px;
            }

            input.rdoBtn, input.chkBox
            {
                margin: 0px 5px 0px 0px;
                padding: 0px;
                border: 0px!important;
            }

            * html input.rdoBtn, * html input.chkBox
            {
                margin-top: -3px;
            }

            input.lMar, select.lMar, textarea.lMar
            {
                margin-left: 15px;
            }

            textarea.taMed
            {
                width: 265px;
                height: 95px;
            }

            .style2
            {
                background-color: rgb(207, 207, 207);
                height: 30px;
                width: 255px;
            }

            .style4
            {
                background-color: rgb(207, 207, 207);
                width: 255px;
                height: 81px;
            }

            .MyCalendar .ajax__calendar_container
            {
                border: 1px solid #646464;
                background-color: lemonchiffon;
                color: red;
            }

            .MyCalendar .ajax__calendar_other .ajax__calendar_day,
            .MyCalendar .ajax__calendar_other .ajax__calendar_year
            {
                color: black;
            }

            .MyCalendar .ajax__calendar_hover .ajax__calendar_day,
            .MyCalendar .ajax__calendar_hover .ajax__calendar_month,
            .MyCalendar .ajax__calendar_hover .ajax__calendar_year
            {
                color: black;
            }

            .MyCalendar .ajax__calendar_active .ajax__calendar_day,
            .MyCalendar .ajax__calendar_active .ajax__calendar_month,
            .MyCalendar .ajax__calendar_active .ajax__calendar_year
            {
                color: black;
                font-weight: bold;
            }

            .style6
            {
                background-color: rgb(207, 207, 207);
                height: 7px;
                width: 200px;
            }

            .style7
            {
                height: 7px;
            }

            .style8
            {
                background-color: rgb(207, 207, 207);
                height: 30px;
                width: 262px;
            }

            .style10
            {
                background-color: rgb(207, 207, 207);
                width: 262px;
                height: 81px;
            }

            .auto-style1
            {
                background-color: rgb(207, 207, 207);
                height: 30px;
                width: 8px;
            }

            .auto-style2
            {
                background-color: rgb(207, 207, 207);
                height: 7px;
                width: 8px;
            }

            .auto-style3
            {
                background-color: rgb(207, 207, 207);
                width: 8px;
                height: 81px;
            }

            .auto-style4
            {
                background-color: rgb(207, 207, 207);
                height: 30px;
                width: 125px;
            }

            .auto-style5
            {
                background-color: rgb(207, 207, 207);
                height: 7px;
                width: 125px;
            }

            .auto-style6
            {
                background-color: rgb(207, 207, 207);
                width: 125px;
                height: 81px;
            }
        </style>

        <script language="javascript" type="text/javascript">
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
 
 
            <div id="globalWrap">
                <div class="midCtnt">
                    <div id="searchBar">
                        <span class="h3">Make an Online Enquiry</span>
                    </div>
                    <div class="text">
                        Fill out the form below to get expert advice from one of our friendly Student Enrolment
                Advisors.<br />
                        You will receive a reply within 24 hours (Monday to Friday in India). We look forward
                to hearing from you!
                    </div>
                    <div id="form">
                        <table cellpadding="0" cellspacing="0" class="tblenquiry">
                            <tr>
                                <td class="auto-style4">
                                    <span>Name</span>
                                </td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td class="secondrol">
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="100" size="55" onkeypress="return fnCommonAcceptCharacterOnly(event)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <span>Email</span>
                                </td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td class="secondrol">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" size="55" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Invalid E-Mail Id"  ForeColor="red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="RegularExpressionValidatorColor"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <span>Telephone</span>
                                </td>
                                <td class="auto-style1">&nbsp;
                                </td>
                                <td class="secondrol">
                                    <asp:TextBox ID="txtTelephone" runat="server" MaxLength="11" size="55" onkeypress="return fnCommonAcceptNumberOnly(event)" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <span>Mobile</span>
                                </td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td class="firstrol">
                                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" size="55" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <span>DOB</span>
                                </td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td class="firstrol">
                                    <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" size="10" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <span>Course Interested</span>
                                </td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td class="firstrol">
                                    <asp:DropDownList ID="dpdCourse" runat="server" class="programselect">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                                                        <tr>
                                <td class="auto-style4">
                                    <span>Citizenship</span>
                                </td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td class="firstrol">
                                    <asp:TextBox ID="txtAddress" MaxLength ="1000" TextMode="MultiLine" runat="server" size="55px" Height="268px" Width="479px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">
                                    <span>Contact me by</span>
                                </td>
                                <td class="auto-style2">
                                    <span class="required">*</span>
                                </td>
                                <td class="style7">
                                    <asp:RadioButton ID="rdoEmail" GroupName="ContactBy" runat="server" Checked="true"
                                        Text="Email" />
                                    <asp:RadioButton ID="rdoTelephone" GroupName="ContactBy" runat="server" Text="Telephone" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style6">
                                    <span>Subscription</span>
                                </td>
                                <td class="auto-style3">
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSpecialOffers" runat="server" Checked="true" />
                                    Keep me updated news including special offers and promotions.
                            <p style="font-size: 11px;">
                                Every email has an unsubscribe option. We will never disclose your details to any
                                third parties. Please view our Privacy Policy for information on how we use your
                                data.
                            </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <span>Query</span></td>
                                <td class="auto-style1">
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuestions" MaxLength="1000" TextMode="MultiLine" runat="server"
                                        size="55px" Height="268px" Width="479px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="buttonrol" colspan="2"></td>
                                <td class="buttonrolsec" height="35" >
                                    <center><asp:Button ID="btnSubmit" class="button" Text="Submit" runat="server"/></center>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
      
        </div>
      
</asp:Content>

