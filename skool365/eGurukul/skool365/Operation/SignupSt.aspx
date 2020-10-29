<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationAreaForOlympiad.Master" AutoEventWireup="true" CodeFile="SignupSt.aspx.cs" Inherits="Operation_SignupSt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script src="../Common/JS/Calendar.js" type="text/javascript"></script>
    <link href="../Common/CSS/Admin.css" rel="stylesheet" type="text/css" />
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
            background: #65a9d7;
            background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
            background: -webkit-linear-gradient(top, #3e779d, #65a9d7);
            background: -moz-linear-gradient(top, #3e779d, #65a9d7);
            background: -ms-linear-gradient(top, #3e779d, #65a9d7);
            background: -o-linear-gradient(top, #3e779d, #65a9d7);
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: #3d37ed;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
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
            font-size: 25pt;
            font-family: "Arial Rounded MT Bold";
        }

        .style30
        {
            font-size: 20pt;
            font-family: "Arial";
            color: #000040;
        }
    </style>
    <script type="text/javascript">

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

            var objddlLocation = document.getElementById("ddlState")
            objddlLocation.selectedIndex = 0;
            return true;
        };

        function setFocusToEntryTab(id) {
            if (document.getElementById('<%=hdCalledFromPortal.ClientID %>').value != "Y") {
                document.getElementById("lstListtab").style = "visibility:hidden;";
                document.getElementById("lstEntrytab").style = "visibility:hidden;";
                //if (id == "EntryTab") {
                //    document.getElementById("basicform").style.display = 'block';
                //    document.getElementById("UWReqList").style.display = 'none';
                //    document.getElementById("lstListtab").className = 'current';
                //    document.getElementById("lstEntrytab").className = '';
                //}
                //if (id == "UWList") {
                //    document.getElementById("UWReqList").style.display = "block";
                //    document.getElementById("basicform").style.display = "none";
                //    document.getElementById("lstListtab").className = '';
                //    document.getElementById("lstEntrytab").className = 'current';
                //}
            }
            else {

                document.getElementById("lstEntrytab").style = "visibility:hidden;";
                document.getElementById("basicform").style.display = "none";
                document.getElementById("lstEntrytab").className = '';

                document.getElementById("lstListtab").style = "visibility:visible;";
                document.getElementById("UWReqList").style.display = 'block';
                document.getElementById("UWReqList").className = '';
                document.getElementById("lstListtab").className = '';
            }
        }


        function DisplayEntryListOnly() {
            setFocusToEntryTab('UWReqList');
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

        function hidememsg() {
            document.getElementById('customlightbox').style.display = 'none';
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

        .auto-style1
        {
            width: 4px;
        }

        .auto-style3
        {
            width: 180px;
            height: 47px;
        }

        .auto-style4
        {
            height: 47px;
        }

        .auto-style5
        {
            height: 49px;
        }
    </style>
    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Submit a Request" />--%><div id="Div1" class="contentwrapper">
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
    <div class="style22">
        <strong>
            <span class="style24"><span class="auto-style1">REGISTRATION-CUM-ADMISSION FORM</span></span> <span class="style23"></span>

        </strong>
        <ul class="hornav">
            <li class="" id="lstEntrytab" style="visibility: hidden;"><a href="#" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">New Request</a></li>
            <li class="current" id="lstListtab" style="visibility: hidden;"><a href="#" id="UWList" onclick="setFocusToEntryTab('UWList')">Account Signup List</a></li>
        </ul>
        <asp:UpdatePanel runat="server" ID='updatepnlddlUWList' UpdateMode="Conditional">
        </asp:UpdatePanel>
    </div>

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

                    <div style="height: 450px;">
                        <table width="60%" style="margin-left: 250px;" cellspacing="8px">

                            <tr>
                                <td class="auto-style3">Name of Applicant<span style="color: red;">*</span></td>
                                <td class="auto-style4" colspan="3">
                                    <asp:TextBox ID="txtApplicantName" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtApplicantName" runat="server" ControlToValidate="txtApplicantName" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="4"><strong>Address for Correspondence of the applicant :</strong></td>
                            </tr>
                            <tr style="background-color: lightyellow;">
                                <td class="auto-style5">Address<strong><span style="color: red;"> 
                                    *</span></strong></td>

                                <td colspan="3" class="auto-style5">
                                    <asp:TextBox ID="txtAddress" runat="server" autocomplete="off" TextMode="MultiLine" class="Platform" CssClass="hasDatepicker" MaxLength="1000" Width="481px" Height="157px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtAddress" runat="server" ControlToValidate="txtAddress" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>

                            </tr>



                            <tr style="background-color: lightyellow;">
                                <td>Pin Code<strong>&nbsp;<span style="color: red;"> 
                                    *</span></strong></td>

                                <td>
                                    <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="10" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtPinCode" runat="server" ControlToValidate="txtPinCode" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>

                                <td>State<strong>&nbsp;<span style="color: red;"> 
                                    *</span></strong></td>

                                <td>
                                    <asp:DropDownList ID="ddlState" runat="server" Width="200px">
                                        <asp:ListItem Text="New Delhi" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Bihar" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Haryana" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Punjab" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Himachal Pradesh" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>


                            </tr>



                            <tr style="background-color: lightyellow;">
                                <td><strong>Occupation</strong></td>

                                <td colspan="3">
                                    <asp:TextBox ID="txtOccupation" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="100"></asp:TextBox>
                                </td>

                            </tr>

                            <tr style="background-color: lightyellow;">
                                <td>Mobile No<strong>&nbsp;<span style="color: red;">*</span></strong> </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMobile" runat="server" autocomplete="off"
                                        class="Platform" CssClass="hasDatepicker" MaxLength="10" onkeypress="return fnCommonAcceptNumberOnly(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtMobile" runat="server" ControlToValidate="txtMobile" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="background-color: lightyellow;">
                                <td>Email ID<strong>&nbsp;<span style="color: red;">*</span></strong> </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtEmail" runat="server" ControlToValidate="txtEmail" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ForeColor="Red"
                                        Text="Invalid Email Id" ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*" runat="server" />

                                </td>
                            </tr>



                            <tr>
                                <td colspan="4"><strong>Family Information of the applicant :</strong></td>
                            </tr>
                            <tr style="background-color: lightyellow;">
                                <td>Father’s Name<strong>&nbsp;<span style="color: red;"> 
                                    *</span></strong></td>

                                <td colspan="3">
                                    <asp:TextBox ID="txtFatherName" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtFatherName" runat="server" ControlToValidate="txtFatherName" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>

                            </tr>

                            <tr style="background-color: lightyellow;">
                                <td><strong>Mother’s Name </strong>&nbsp;<span style="color: red;"> 
                                    *</span></strong></td>

                                <td colspan="3">
                                    <asp:TextBox ID="txtMotherName" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtMotherName" runat="server" ControlToValidate="txtMotherName" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>

                            </tr>


                            <tr>
                                <td>Date of Birth<strong>&nbsp;<span style="color: red;"> 
                                    *</span></strong></td>

                                <td colspan="3">
                                    <asp:TextBox ID="txtDob" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtDob" runat="server" ControlToValidate="txtDob" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender ID="CalendarDueDate" runat="server"
                                        TargetControlID="txtDob" Format="dd/MM/yyyy" PopupButtonID="imgPopup" Enabled="true" />
                                </td>

                            </tr>

                            <tr>
                                <td style="width: 180px">Nationality<span style="color: red;">*</span></td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtNationality" runat="server" autocomplete="off" class="Platform" CssClass="hasDatepicker" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ValtxtNationality" runat="server" ControlToValidate="txtNationality" CssClass="RequiredFieldValidatorColor" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>

                            </tr>


                            <tr>
                                <td style="height: 50px">Are you married or single ? </td>
                                <td colspan="3">
                                    <asp:RadioButton ID="rdoUnMarried" Text="Unmarried" runat="server" GroupName="MartialStatus" Checked="true" />
                                    <asp:RadioButton ID="rdoMarried" Text="Married" runat="server" GroupName="MartialStatus" Checked="false" />
                                </td>

                            </tr>


                            <tr>
                                <td colspan="4"><strong>Course you have applied for
                                </strong></td>
                            </tr>
                            <tr>
                                <td colspan="4"><strong>1st choice</strong>
                                    <asp:DropDownList ID="ddlCourse" runat="server" Width="200px">
                                        <asp:ListItem Text="New Delhi" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Bihar" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Haryana" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Punjab" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Himachal Pradesh" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>



                            <tr>
                                <td></td>
                                <td>
                                    <center>
                                        <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" CssClass="btnblack"
                                            OnClick="btnSubmit_Click" OnClientClick="if (!window.confirm('Are you sure you want to save/update this record')) return false;"
                                            Text="Submit Request" />
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                            CssClass="btngray" OnClick="btnCancel_Click" Text="Cancel" />
                                    </center>

                                    <asp:HiddenField ID="hdId" runat="server" />
                                    <asp:HiddenField ID="hdUW" runat="server" />
                                    <asp:HiddenField ID="hdCalledFromPortal" runat="server" />
                                    <asp:HiddenField ID="hdMessage" runat="server" />
                                    <asp:HiddenField ID="hdAlertMessage" runat="server" Value="Do you wish to submit the request for the selected account?" />
                                </td>
                            </tr>
                            </caption>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>


        </div>
        <div id="UWReqList" class="subcontent" style="display: none;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin: 8px 10%">
                            <span>
                                <asp:RadioButton ID="rdoPending" Text="Pending" runat="server" GroupName="Select"
                                    Checked="true" /></span> <span>
                                        <asp:RadioButton ID="rdoAll" Text="All Account" runat="server" GroupName="Select"
                                            Checked="false" /></span> <span></span><span><b style="font-size: 14px; padding-left: 7px;">*Search </b>
                                                <asp:TextBox ID="txtSearch" Text="" MaxLength="100" runat="server" autocomplete="off" Width="330px">
                                                </asp:TextBox></span> <span>

                                                    <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="false" OnClick="btnGo_Click" />
                                                    <asp:Button ID="btnClear" Width="70px" runat="server" Text="Clear" ToolTip="Reset the grid with default search" CausesValidation="false" OnClientClick=" return ClearInputSearch();" OnClick="btnGo_Click" />

                                                </span>
                        </div>
                        <div style="width: 98%; overflow: scroll; border: 1px solid #000; max-height: 390px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="SIGNUP_ID"
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" OnRowDeleting="grdStyled_RowDeleting"
                                CellPadding="20" CellSpacing="20" RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid"
                                Width="95%" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdStyled_PageIndexChanging" OnRowDataBound="grdStyled_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <Columns>
                                   <%-- <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="10px"
                                        ItemStyle-Height="10px" SelectText="<img src='../Common/Images/edit1.png' border='0' title='Edit Request'>"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Delete Request">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                                ImageUrl="../Common/images/icon_delete.png" OnClientClick="if (!window.confirm('Are you sure you want to delete this Request')) return false;"
                                                title='Delete Request' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

</asp:Content>
