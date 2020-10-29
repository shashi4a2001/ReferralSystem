<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    CodeFile="Invoice.aspx.cs" Inherits="Operation_Invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <link href="Common/CSS/ApplicationForm.css" type="text/css" rel="Stylesheet" />
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
            width: 800px;
        }
    </style>

    <script type="text/javascript">

        function IsNumeric(val) {
            if (!(event.keyCode > 47 && event.keyCode < 58)) {
                alert("Enter Numerics Only")
                event.srcElement.focus();
                return false;
            }
        }

        function PrvAmount(txt) {
            if ((document.getElementById('<% = hdVal.ClientID %>').value == "")) {
                document.getElementById('<% = hdVal.ClientID %>').value = txt.value;;
            }
        }

        function AdmissionFee(txt) {
            var amt1 = 0;
            var amt2 = 0;
            var amtPrv = 0;

            if (document.getElementById('<% = txtTotalPayingAmount.ClientID %>').value != "") {
                amt1 = document.getElementById('<% = txtTotalPayingAmount.ClientID %>').value;
            }

            if (document.getElementById('<% = hdVal.ClientID %>').value != "") {
                amtPrv = document.getElementById('<% = hdVal.ClientID %>').value;
            }
            amt2 = txt.value;
            if (txt.value == "") {
                amt2 = 0;
            }

            amt1 = parseInt(amt1) + parseInt(amt2) - parseInt(amtPrv);
            document.getElementById('<% = txtTotalPayingAmount.ClientID %>').value = amt1;
            document.getElementById('<% = txtAmount.ClientID %>').value = amt1;
            document.getElementById('<% = hdVal.ClientID %>').value = "";
        }

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

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Payment Section</td>
                            <td>
                                <asp:RadioButton ID="rdoAdmission" runat="server" Text="Admission Fee" GroupName="PaymentSection" Checked="true" />
                                <asp:RadioButton ID="rdoSemester" runat="server" Text="Course Fee" GroupName="PaymentSection" Checked="false" />
                                <asp:RadioButton ID="rdoMisc" runat="server" Text="Miscellaneous Fee" GroupName="PaymentSection" Checked="false" />
                            </td>

                        </tr>
                        <tr>
                            <td class="style4">Registration Number
                            </td>
                            <td>
                                <asp:TextBox ID="txtRegistrationNo" Text="" Font-Bold="true" runat="server" Style="width: 50px;"
                                    ForeColor="#000099" Width="98px"></asp:TextBox>
                                <asp:Button ID="btnShow" runat="server" CssClass="buttonCss" OnClick="btnShow_Click"
                                    Text="Show" Width="75px" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCss" BorderStyle="None"
                                    OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>Name
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Width="279px" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>Academic Year / Cource
                            </td>
                            <td>
                                <asp:Label ID="lblAcedemicYear" Width="1000px" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Financial Year / Cource
                            </td>
                            <td>
                                <asp:Label ID="lblFinancialYear" Width="1000px" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><b>Total Fee (</b><asp:Label ID="lblTotalFee" runat="server" Enabled="False" Font-Bold="true" ForeColor="Black" Text=""></asp:Label>
                                ) <b>Total Received (</b>
                                <asp:Label ID="lblTotalReceived" runat="server" Enabled="False" Font-Bold="true" ForeColor="DarkGreen" Text=""></asp:Label>
                                ) <b>Total Dues(</b>
                                <asp:Label ID="lblTotalDues" runat="server" Enabled="False" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                                ) </td>
                        </tr>

                             <tr>
                            <td>
                                Installment Destails
                            </td>
                            <td class="style5">
                                <asp:DataList ID="dtSemesterFee" runat="server" OnItemDataBound="dtSemesterFee_ItemDataBound"
                                    BackColor="White" BorderStyle="Solid" CellPadding="1" CellSpacing="1">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" BackColor="#FFFFCC" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" />
                                    <HeaderStyle BackColor="#FF9933" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" BorderColor="Black"
                                        BorderWidth="1px" />
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td colspan="4" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;
                                                    color: #ffffff; width: 300px;">
                                                    Student Fees
                                                </td>
                                                <td colspan="2" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;
                                                    color: #ffffff; width: 100px;">
                                                </td>
                                                <td colspan="4" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;
                                                    color: #ffffff; width: 300px;">
                                                    Payments
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50px; color: black;"   >
                                                    Section
                                                </td>
                                                <td style="width: 150px; color: black;" colspan="2">
                                                    Installment Date
                                                </td>
                                                <td style="width: 100px; color: black;">
                                                    Amount
                                                </td>
                                                <td colspan="2" style="width: 100px; color: black;">
                                                </td>
                                                <td style="width: 150px; color: black;" colspan="2">
                                                    Total Paid Amount
                                                </td>
                                                <td style="width: 150px; color: black;" colspan="2">
                                                    Due Amount
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle BackColor="Silver" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 50px;"  >
                                                    <asp:Label ID="lblInsNo" Width="50px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsNo") %>'> </asp:Label>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="lblInsDate" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DueDate") %>'> </asp:Label>
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:Label ID="lblInstallmentAmount" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InstallmentAmount") %> '> </asp:Label>
                                                </td>
                                                <td colspan="2" style="width: 100px;">
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="lblPaidAmount" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PaidAmount") %> '> </asp:Label>
                                                </td>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="lblDueAmount" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DueAmount") %> '> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>

                        <tr>
                            <td>Description
                            </td>
                            <td class="style5">
                                <asp:DataList ID="dtAnnualFee" runat="server" DataKeyField="SectionID" OnItemDataBound="dtAnnualFee_ItemDataBound"
                                    BackColor="White" BorderStyle="Solid" CellPadding="1" CellSpacing="1">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" BackColor="#FFFFCC" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" />
                                    <HeaderStyle BackColor="#FF9933" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" BorderColor="Black"
                                        BorderWidth="1px" />
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td colspan="6" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold; color: #ffffff; width: 400px;">Student Fees
                                                </td>
                                                <td colspan="2" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold; color: #ffffff; width: 100px;"></td>
                                                <td colspan="3" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold; color: #ffffff; width: 450px;">Payments
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="width: 300px; color: black;">Section
                                                </td>
                                                <td colspan="2" style="width: 100px; color: black;">Amount
                                                </td>
                                                <td colspan="2" style="width: 100px; color: black;"></td>
                                                <td style="width: 150px; color: black;">Paid Amount
                                                </td>
                                                <td style="width: 150px; color: black;">Due Amount
                                                </td>
                                                <td style="width: 150px; color: black;">Paying Amount
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle BackColor="Silver" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                          
                                                <td style="width: 300px;" colspan="4">
                                                    <asp:Label ID="lblAdmnSection"   runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Section") %>'> </asp:Label>
                                                      <asp:Label ID="lblGST_Applicable" Width="0%" ForeColor="Red" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GST_Applicable") %>'> </asp:Label>
                                                </td>
                                                <td style="width: 100px;" colspan="2">
                                                    <asp:TextBox ID="txtAdmnAmount" Width="100px" Enabled="false" MaxLength="10" runat="server"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "AnnualFee") %> '> </asp:TextBox>
                                                </td>
                                                <td colspan="2" style="width: 100px;"></td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="lblPaidAmount" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PaidAmt") %> '> </asp:Label>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="lblDueAmount" Width="150px" runat="server" Text=""> </asp:Label>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:TextBox ID="txtPayingAmount" Width="150px" MaxLength="10" runat="server" Text=""> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>


                                <asp:DataList ID="dtPayingList" runat="server" DataKeyField="SectionID" OnItemDataBound="dtPayingList_ItemDataBound"
                                    BackColor="White" BorderStyle="Solid" CellPadding="1" CellSpacing="1" >
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" BackColor="#FFFFCC" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" />
                                    <HeaderStyle BackColor="#FF9933" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" BorderColor="Black"
                                        BorderWidth="1px" />
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td colspan="4" style="width: 300px; color: black;"></td>

                                                <td style="font-size: 14px; text-align: left; line-height: 23px; font-weight: bold; color: #ffffff; width: 450px;">Payments
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="width: 300px; color: black;">Section
                                                </td>

                                                <td style="width: 150px; color: black;">Paying Amount
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle BackColor="Silver" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 300px;" colspan="4">
                                                    <asp:Label ID="lblAdmnSection" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Section") %>'> </asp:Label>
                                                     <asp:Label ID="lblGST_Applicable" Width="0%" ForeColor="Red" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GST_Applicable") %>'> </asp:Label>
                                                </td>

                                                <td style="width: 150px;">
                                                    <asp:TextBox ID="txtPayingAmount" Width="150px" MaxLength="10" runat="server" Text=""> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td>Total
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalPayingAmount" runat="server" Font-Bold="True" ForeColor="#000066"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="background: #243154; font-size: 14px; text-align: center; line-height: 23px; font-weight: bold; color: #ffffff;"
                        id="dvPaymentOption" runat="server" visible="false">
                        Payment Option
                    </div>
                    <div id="divOfflinePaymentOption" runat="server" visible="false">
                        <table>
                            <tr>
                                <td colspan="4">
                                    <asp:RadioButtonList ID="rdoPaymentOption" RepeatDirection="Horizontal" runat="server"
                                        Enabled="true" OnSelectedIndexChanged="rdoPaymentOption_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0" Selected="True">By Cash</asp:ListItem>
                                        <asp:ListItem Value="1">Cheque/DD</asp:ListItem>
                                        <asp:ListItem Value="2">NEFT</asp:ListItem>
                                        <%--  <asp:ListItem Value="3">Online Payment</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">Draft/NEFT No.
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDraftNumber" runat="server" Style="width: 252px;" Enabled="False" />
                                </td>
                                <td align="right">Bank Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankName" runat="server" Style="width: 252px;" onkeypress="return fnCommonAcceptCharacterOnly(event)"
                                        Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td>Amount
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Style="width: 252px;" Enabled="False" />
                                </td>
                                <td align="right" class="style10">Draft/NEFT Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDraftDate" runat="server" Style="width: 252px;" Enabled="False" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtDraftDate"
                                        CssClass="MyCalendar" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                            <tr>
                                <td>Remarks
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtRemarks" runat="server" Style="width: 630px; height: 50px" MaxLength="300"
                                        TextMode="MultiLine" Enabled="true" />
                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td colspan="3" style="text-align: right;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save & Print Invoice" CssClass="buttonCss" BorderStyle="None"
                                        OnClick="btnSubmit_Click" Enabled="False" OnClientClick="if (!window.confirm('Are you sure you want to generate the receipt?')) return false;" />
                                    <asp:HiddenField ID="hdVal" runat="server" Value="" />
                                    <asp:HiddenField ID="hdReceiptNo" runat="server" Value="" />
                                </td>
                            </tr>


                        </table>
                    </div>

                    <div style="background-color: Menu; padding: 0px 15px; border: #a6b0b6 1px solid; box-shadow: 0px 5px 5px #888888; }">
                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--contentwrapper-->
</asp:Content>
