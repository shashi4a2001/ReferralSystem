<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Operation/CRMOperationArea2.Master" CodeFile="InstallmentDtl.aspx.cs" Inherits="Operation_InstallmentDtl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <style type="text/css">
        .style4
        {
            width: 158px;
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

        .style5
        {
            width: 350px;
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

        .auto-style1
        {
            height: 20px;
        }
    </style>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function HideLabel() {
            var second = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, second * 1000);
        };


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


        function IsNumeric() {
            if (!(event.keyCode > 47 && event.keyCode < 58)) {
                alert("Enter Numerics Only")
                event.srcElement.focus();
                return false;
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

        function SemesterInstallment() {

            var semesterfee = 0;
            var gv = document.getElementById("<%=grdInstallment.ClientID %>");
            var gvRowCount = gv.rows.length;
            var txtAmount;
            var i = 1;

            for (i; i <= gvRowCount - 1; i++) {
                if (gv.rows[i].cells[2].childNodes[1].value != "") {
                    semesterfee = parseInt(semesterfee) + parseInt(gv.rows[i].cells[2].childNodes[1].value);
                }
            }

            //total Admission Fee
            document.getElementById('<% = txtInstallmentTotal.ClientID %>').value = semesterfee;
            document.getElementById('<% = hdValInstallment.ClientID %>').value = semesterfee;
            return true;

        }

    </script>

    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <img id="Img1" runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height: 60px;"> </img>
                    <%--<asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block; top: 0px; left: 0px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>

                    <table>
                        <tr>
                            <td class="style4">Registration Number
                            </td>
                            <td>
                                <asp:TextBox ID="txtRegistrationNo" Text="" Font-Bold="true" Enabled="true" runat="server"
                                    Style="width: 50px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="regtxtRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnGetDetails" runat="server" CssClass="buttonCss" OnClick="btnGetDetails_Click"
                                    Text="Show Details" Width="128px" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btnblack" BorderStyle="None"
                                    OnClick="btnCancel_Click" Visible="true" CausesValidation="false" />

                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table id="tblDetail" runat="server" visible="false" style="border-width: 1px; border-color: #3399FF;" border="1" cellpadding="1" cellspacing="1">
                        <tr>
                            <td>Student Name
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStudentName" Width="1000px" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Academic Year / Cource
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblAcedemicYear" Width="1000px" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Financial Year / Cource
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblFinancialYear" Width="1000px" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Actual Semester Fee
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblCollegeSemesterFee" runat="server" Font-Bold="True" Style="width: 50px;"
                                    ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Defined Semester Fee
                            </td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="lblStudentSemFee" runat="server" Font-Bold="True" Style="width: 50px;"
                                    ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No Of Installment(s)
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtInstallment" runat="server" Font-Bold="true" Style="width: 50px;"
                                    Text="" MaxLength="2"></asp:TextBox><asp:RequiredFieldValidator ID="regValtxtInstallment" runat="server" ControlToValidate="txtInstallment"
                                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnShow" runat="server" Text="Populate" Width="90px" CssClass="btnblack"
                                    OnClick="btnShow_Click" ToolTip="Click to get Installment Plan" />

                            </td>
                        </tr>
                        <tr>
                            <td>Installment Detail
                            </td>
                            <td>:</td>
                            <td class="style5">

                                <asp:GridView ID="grdInstallment" runat="server" AutoGenerateColumns="False" Width="300px"
                                    BackColor="White" BorderColor="LightBlue" BorderStyle="Solid" BorderWidth="2px"
                                    CellPadding="4" OnRowCreated="grdInstallment_RowCreated" OnRowDataBound="grdInstallment_RowDataBound"
                                    DataKeyNames="" GridLines="Both">
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Installment No" Visible="true" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstNo" Width="100px" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "InstallmentNo") %> '></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Due Date" Visible="true" ItemStyle-Width="85px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDate" Width="90px" MaxLength="10" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "DueDate") %>  '></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" EnabledOnClient="true"
                                                    TargetControlID="txtDate" CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Installment Amount" Visible="true" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSemAmount" Font-Bold="true" BackColor="LightYellow"  Width="150px" MaxLength="7" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "FeeAmount") %>  '></asp:TextBox>
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
 
                            </td>
                        </tr>
                        <tr id="rowTotal" runat="server" visible="false">
                            <td class="captionWidth"><strong>Total Aount</strong>
                            </td>
                            <td>:</td>
                            <td class="style18">
                                <asp:TextBox ID="txtInstallmentTotal" Text="" Width="100px" runat="server" Font-Bold="True" Enabled="false"
                                    ForeColor="#000066"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td class="style5">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save" BorderStyle="None" OnClick="btnSubmit_Click"
                                    CssClass="btnblack" OnClientClick="if (!window.confirm('Are you sure you want to save/modify fee installment details of selected student?')) return false;" />

                                <asp:HiddenField ID="hdVal" runat="server" Value="" />
                                <asp:HiddenField ID="hdReceiptIssued" runat="server" Value="" />
                                <asp:HiddenField ID="hdValInstallment" runat="server" Value="" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnGetDetails" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnShow" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--contentwrapper-->
</asp:Content>
