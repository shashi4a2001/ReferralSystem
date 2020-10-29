<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    CodeFile="AdmissionFeeDetails.aspx.cs" Inherits="Operation_AdmissionFeeDetails" %>

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
            width: 600px;
        }
    </style>
    <script type="text/javascript">
        function HideLabel() {
            var second = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, second * 1000);
        };
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


        function SemesterFee(txt) {

            var amt1 = 0;
            var amt2 = 0;
            var amtPrv = 0;

            if (document.getElementById('<% = txtTotalSemesterFee.ClientID %>').value != "") {
                amt1 = document.getElementById('<% = txtTotalSemesterFee.ClientID %>').value;
            }

            if ((document.getElementById('<% = hdVal.ClientID %>').value != "")) {
                amtPrv = document.getElementById('<% = hdVal.ClientID %>').value;
            }

            amt2 = txt.value;
            if (txt.value == "") {
                amt2 = 0;
            }

            amt1 = parseInt(amt1) + parseInt(amt2) - parseInt(amtPrv);
            document.getElementById('<% =  txtTotalSemesterFee.ClientID %>').value = amt1;
            document.getElementById('<% = hdVal.ClientID %>').value = "";

        }


        function AdmissionFee(txt) {
            var amt1 = 0;
            var amt2 = 0;
            var amtPrv = 0;

            if (document.getElementById('<% = txtTotalAnnualFee.ClientID %>').value != "") {
                amt1 = document.getElementById('<% = txtTotalAnnualFee.ClientID %>').value;
            }

            if (document.getElementById('<% = hdVal.ClientID %>').value != "") {
                amtPrv = document.getElementById('<% = hdVal.ClientID %>').value;
            }

            amt2 = txt.value;
            if (txt.value == "") {
                amt2 = 0;
            }

            amt1 = parseInt(amt1) + parseInt(amt2) - parseInt(amtPrv);
            document.getElementById('<% = txtTotalAnnualFee.ClientID %>').value = amt1;
            document.getElementById('<% = hdVal.ClientID %>').value = "";
        }



    </script>
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

    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>

                    <table>
                        <tr>
                            <td class="style4">
                                <strong>Registration Number</strong>
                            </td>
                            <td style="Width:1000px;">
                                <asp:TextBox ID="txtRegistrationNo" Text="" Font-Bold="true" Enabled="False" runat="server"
                                    Style="width: 100px;" ForeColor="#000099"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="regtxtRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                                &nbsp;<asp:Button ID="btnShow" runat="server" CssClass="buttonCss" OnClick="btnShow_Click"
                                    Text="Show" Width="75px" /><asp:Button ID="btnCancel" runat="server" Text="Cancel" Enabled="true" CssClass="btnblack" BorderStyle="None" OnClick="btnCancel_Click" /></td>
                        </tr>
                    </table>
                    <hr />
                    <table id="tblDetail" runat="server" visible="false" style="border-width:1px;" >
                        <tr>
                            <td>Name
                            </td>
                            <td>
                                <asp:Label ID="lblStudentName" runat="server" Width="400px" Font-Bold="true" Text=""
                                    ForeColor="#000099"></asp:Label>
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
                            <td>
                                <strong>Annual Fee(Actual)</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCollegeAnnualFee" runat="server" Enabled="False" Font-Bold="true"
                                    Style="width: 100px;" Text="" ForeColor="#000099"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Student Annual Fee</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentAdmnFee" runat="server" Font-Bold="True" Style="width: 100px;"
                                    Text="" MaxLength="7"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Section Wise</strong>
                            </td>
                            <td class="style5">
                                <asp:DataList ID="dtAnnualFee" runat="server" DataKeyField="SectionID" OnItemDataBound="dtAnnualFee_ItemDataBound"
                                    BackColor="White" BorderStyle="Solid" CellPadding="1" CellSpacing="1">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" BackColor="#FFFFCC" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" />
                                    <HeaderStyle BackColor="#95c0e6" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" BorderColor="Black"
                                        BorderWidth="1px" />
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                              
                                                <td colspan="6" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;">Actual Fee
                                                </td>
                                                <td colspan="2" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;"></td>
                                                <td style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;">Student Fee
                                                </td>
                                            </tr>
                                            </tr>
                                            <tr>
                                                  <td style="width: 300px;">Section
                                                </td>
                                                <td style="width: 100px;"></td>
                                                <td colspan="6" style="width: 300px;"></td>
                                                <td style="width: 150px;"></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle BackColor="Silver" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lblAdmnSection" Width="300px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Section") %>'> </asp:Label>
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:TextBox ID="txtAdmnAmount" Width="100px" Enabled="false" MaxLength="7" runat="server"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "AnnualFee") %> '> </asp:TextBox>
                                                </td>
                                              <td colspan="6" style="width: 300px;"></td>
                                                <td style="width: 150px;">
                                                    <asp:TextBox ID="txtAdmnFeeAmount" Width="90px" MaxLength="7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FeeAmount") %> '> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Total Amount</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalAnnualFee" runat="server" Font-Bold="True" ForeColor="#000066" BackColor="#ffffcc"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Semester Fee(Actual)</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCollegeSemesterFee" runat="server" Enabled="false" Font-Bold="true"
                                    Style="width: 100px;" Text="" ForeColor="#000099"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Student Semester Fee</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentSemFee" runat="server" Font-Bold="true" Style="width: 100px;"
                                    Text="" MaxLength="7"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Section Wise</strong>
                            </td>
                            <td class="style5">
                                <asp:DataList ID="dtSemeterFee" DataKeyField="SectionID" runat="server" OnItemDataBound="dtSemeterFee_ItemDataBound"
                                    BackColor="White" BorderStyle="Solid" CellPadding="1" CellSpacing="1">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" BackColor="#FFFFCC" BorderColor="#CCCCCC"
                                        BorderStyle="Solid" BorderWidth="1px" />
                                    <HeaderStyle BackColor="#95c0e6" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" BorderColor="Black"
                                        BorderWidth="1px" />
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td colspan="6" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;">Actual Fee
                                                </td>
                                                <td colspan="2" style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;"></td>
                                                <td style="font-size: 14px; text-align: center; line-height: 23px; font-weight: bold;">Student Fee
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 300px;">Section
                                                </td>
                                                <td style="width: 100px;"></td>
                                                <td colspan="6" style="width: 300px;"></td>
                                                <td style="width: 150px;"></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle BackColor="Silver" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 300px;">
                                                    <asp:Label ID="lblSemSection" Width="300px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Section") %>'> </asp:Label>
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:TextBox ID="txtSemAmount" Enabled="false" Width="100px" MaxLength="7" runat="server"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "AnnualFee") %> '> </asp:TextBox>
                                                </td>
                                                <td colspan="6" style="width: 300px;"></td>
                                                <td style="width: 150px;">
                                                    <asp:TextBox ID="txtSemFeeAmount" Width="90px" MaxLength="7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FeeAmount") %> '> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Total Aount</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotalSemesterFee" BackColor="#ffffcc" runat="server" Font-Bold="True" ForeColor="#000066"></asp:TextBox>
                            </td>
                        </tr>
                        <%--
                        <tr>
                            <td>
                                College Hostel Fee
                            </td>
                            <td>
                                <asp:TextBox ID="txtCollegeHostelFee" runat="server" Enabled="false" 
                                    Font-Bold="true" Style="width: 100px;" Text="" ForeColor="#000099"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Student Hostel Fee
                            </td>
                            <td>
                                <asp:TextBox ID="txtStudentHostelFee" runat="server" Font-Bold="true" 
                                    Style="width: 100px;" Text="" MaxLength="7"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <%--   <tr>
                        <td>
                                Hostel Fee txtTotalSemesterFee</td>
                            <td  class="style5">
                                <asp:DataList ID="dtHostelFee" DataKeyField="SectionID" runat="server" OnItemDataBound="dtHostelFee_ItemDataBound">
                                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="Gray" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                                    <HeaderTemplate>
                                       <table>
                                            <tr >
                                                <td colspan="2" style="background:#243154;font-size:14px; text-align:center; line-height:23px; font-weight:bold; color:#ffffff;" >
                                                    College Fees
                                                </td>
                                                <td colspan="6" style="background:#243154; font-size:14px; text-align:center; line-height:23px; font-weight:bold ;color:#ffffff;">
                                                </td>
                                                <td style="background:#243154;font-size:14px; text-align:center; line-height:23px; font-weight:bold ;color:#ffffff;">
                                                    Student Fees
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px;background:#CCCCCC; color:black; " >
                                                    Section
                                                </td>
                                                <td style="width: 100px;background:#CCCCCC; color:black; ">
                                                    Amount
                                                </td>
                                                <td colspan="6" style="width: 300px;background:#CCCCCC; color:black; ">
                                                </td>
                                                <td style="width: 150px;background:#CCCCCC; color:black; ">
                                                    Amount
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemStyle BackColor="Silver" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 100px;">
                                                    <asp:Label ID="lblHostelSection" Width="50px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Section") %>'> </asp:Label>
                                                </td>
                                                <td style="width: 100px;">
                                                    <asp:TextBox ID="txtHostelAmount"  Enabled ="false" Width="50px" MaxLength="7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AnnualFee") %> '> </asp:TextBox>
                                                </td>
                                                <td colspan="6" style="width: 300px;">
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:TextBox ID="txtHostelFeeAmount" Width="50px" MaxLength="7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FeeAmount") %> '> </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td>
                            Total
                            </td>
                            <td>
                             <asp:TextBox ID="txtTotalHostelFee" runat="server" Font-Bold="True" 
                                    ForeColor="#000066"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td></td>
                            <td class="style5">

                                <center>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" BorderStyle="None" OnClick="btnSubmit_Click"
                                        CssClass="btnblack" OnClientClick="if (!window.confirm('Are you sure you want to save/modify fee details of selected student?')) return false;" />
                                </center>
                                <asp:HiddenField ID="hdVal" runat="server" Value="" />
                                <asp:HiddenField ID="hdPrvSemFee" runat="server" Value="" />

                            </td>
                           
                        </tr>
                        <%--                        <tr>
                        <td>Total Amount :</td>
                        <td>
                            <input id="txtAmount" type="text" /></td>
                        </tr>--%>
                    </table>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--contentwrapper-->
</asp:Content>
