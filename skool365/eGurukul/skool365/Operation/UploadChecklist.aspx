<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master" AutoEventWireup="true" 
                CodeFile="UploadChecklist.aspx.cs" Inherits="Operation_UploadChecklist"   %> 

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">

        function HideLabel() {
            var second = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, second * 1000);
        };

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

        function ShowDocumentsInLarge(imgSrc, val) {
            document.getElementById('<% = hdVal.ClientID %>').value = val;
            if (document.getElementById('<% = hdVal.ClientID %>').value  == 1) {
                 window.open(imgSrc, 'Check List Document Viewer', 'status=yes,toolbar=no,menubar=no,location=no,resizable=yes');
            }
        }
        function ShowDocumentsInLargeRep(imgSrc) {
            if (document.getElementById('<% = hdVal.ClientID %>').value != 1) {
                window.open(imgSrc, 'Check List Document Viewer', 'status=yes,toolbar=no,menubar=no,location=no,resizable=yes');
            }
            document.getElementById('<% = hdVal.ClientID %>').value = 0;
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
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="divEntry" class="subcontent" style="display: block; top: 0px; left: 0px;">
        <div style="height: 5px;">
        </div>
        <table style="font-family: Verdana">
            <tr>
                <td style="height: 15px">
                    Enter Registration No
                </td>
                <td>
                    <asp:TextBox ID="txtRegNo" onkeypress="return fnCommonAcceptNumberOnly(event)" runat="server"
                        MaxLength="6" Width="66px" Height="25px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="regValReceiptNo" runat="server" ControlToValidate="txtRegNo"
                        CssClass="RequiredFieldValidatorColor" ErrorMessage="*" SetFocusOnError="True"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" CssClass="btnblack"
                        OnClick="btnSubmit_Click" Text="Get Details" />
                    <asp:Button ID="btnReset" runat="server" CssClass="btngray" OnClick="btnReset_Click"
                        Text="Cancel" />
                </td>
                <td style="height: 15px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Details
                </td>
                <td>
                    <table id="tblDetail" runat="server">
                        <tr>
                            <td>
                                <strong>Registration No </strong>
                            </td>
                            <td>
                                <asp:Label ID="lblRegNo" ForeColor="blue" Font-Bold="true" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Name </strong>
                            </td>
                            <td>
                                <asp:Label ID="lblName" ForeColor="blue" Font-Bold="true" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="background-color: #0000FF; font-weight: 700;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Session </strong>
                            </td>
                            <td>
                                <asp:Label ID="lblSession" runat="server" Style="color: #0000FF" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Course </strong>
                            </td>
                            <td>
                                <asp:Label ID="lblCourse" runat="server" Style="color: #0000FF" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Batch</strong>
                            </td>
                            <td>
                                <asp:Label ID="lblBatch" runat="server" Style="color: #0000FF" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Remarks </strong>
                            </td>
                            <td>
                                <asp:Label ID="lblError" runat="server" Style="color: #FF0000" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Document opted at the time of Admission </strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDocOpted" runat="server" Style="width: 510px;" ForeColor="Maroon">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr style="background-color: #0000FF; font-weight: 700;" />
                            </td>
                        </tr>
                        <tr id="rowUploadDtl" runat="server" visible="false">
                            <td class="style4">
                                <strong>Upload for</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCheckList" runat="server" Style="width: 510px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="rowupload" runat="server" visible="false">
                            <td>
                                <strong>Browse Document </strong>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUploadImage" runat="server" />
                            </td>
                        </tr>
                        <tr id="rowThumbnail" runat="server" visible="false">
                            <td valign="top">
                                 <strong>Quick Visit</strong>
                            </td>
                            <td>
                                <span>
                                    <asp:ImageButton runat="server" ID="Stag2Button" CausesValidation="false" 
                                    ImageUrl="~/Common/Images/stage_2.png" onclick="Stag2Button_Click" Visible="false" />
                                </span>
                                <span>
                                    <asp:ImageButton runat="server" ID="Stag3Button" CausesValidation="false" 
                                    ImageUrl="~/Common/Images/stage_3.png" onclick="Stag3Button_Click" />
                                </span>
                                <span>
                                    <asp:ImageButton runat="server" ID="Stag4Button" CausesValidation="false" 
                                    ImageUrl="~/Common/Images/stage_4.png" onclick="Stag4Button_Click" />
                                </span>
                                <span>
                                    <asp:ImageButton runat="server" ID="Stag5Button" CausesValidation="false" 
                                    ImageUrl="~/Common/Images/stage_5.png" onclick="Stag5Button_Click"  Visible="false" />
                                </span>
                                <span>
                                    <asp:ImageButton runat="server" ID="Stag6Button" CausesValidation="false" 
                                    ImageUrl="~/Common/Images/stage_6.png" onclick="Stag6Button_Click"  Visible="false" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span style="margin-left: 100px">
                                    <asp:Button ID="btnUploadSubmit" runat="server" CssClass="btnblack" Text="Upload and Submit"
                                        OnClick="btnUploadSubmit_Click" Visible="False" OnClientClick="if (!window.confirm('Are you sure you want to continue the process')) return false;" />
                                </span>
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdId" runat="server" Value="0" />
        <asp:HiddenField ID="hdVal" runat="server" Value="" />
        <asp:Label ForeColor="Red" Visible="True" ID="lblMessage" runat="server" Text=""
            class="lblMessageClassss"></asp:Label>
    </div>
    <hr /><h3>Student Upload Details Panel</h3>
    <div class="maingrid1">
        <asp:Repeater ID="rptrUploads" runat="server" EnableTheming="true" >
            <ItemTemplate>
           
                <div style="display: inline; border-style: double; border-color: Gray;">
                    <a rel="slide" href="#" onclick="ShowDocumentsInLargeRep('<%#Eval("REPEATER_PATH") %>' );">
                        <img style='border: 1px solid #000000' alt='<%#Eval("CHECKLIST_NAME") %>' src='<%#Eval("REPEATER_PATH") %>'  
                            height="100px" width="100px" title= '<%#Eval("CHECKLIST_NAME") %>'/> 
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </div>
    <div class="maingrid">
        <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="CHECKLIST_ID"
            Width="100%" OnRowDeleting="grdStyled_RowDeleting" OnRowCommand="grdStyled_RowCommand">
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="20px"
                    ItemStyle-Height="20px" SelectText="<img src='../Common/Images/edit.png' border='0' title='Edit Record'>"
                    Visible="False"></asp:CommandField>
                <asp:BoundField DataField="CHECKLIST_NAME" HeaderText="CheckList Name" />
                <asp:BoundField DataField="CHECKLIST_EXT" HeaderText="Supported Extensions" />
                <asp:BoundField DataField="DOCUMENT_NAME" HeaderText="Document Name" />
                <asp:BoundField DataField="UPLOADED_ON" HeaderText="Uploaed On" />
                <asp:BoundField DataField="UPLOADED_BY" HeaderText="Uploaed By" />
                <asp:TemplateField HeaderText="Delete" Visible="true">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="DeleteRecord"
                            ImageUrl="../Common/Images/delete.png" OnClientClick="if (!window.confirm('Are you sure you want to delete this record')) return false;"
                            title='Delete Record' CommandArgument='<%# Eval("CHECKLIST_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Show" ItemStyle-HorizontalAlign="Center" Visible="true">
                    <ItemTemplate>
                        <a href="#" onclick="ShowDocumentsInLarge('<%#Eval("REPEATER_PATH") %>',1 );">
                            <img id="Img1" runat="server" src="~/Common/Images/search.gif" alt="Apply" height="21"
                                width="21" title="View Document"></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FilePath">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No records found
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    -<%--  <triggers>
                <asp:AsyncPostBackTrigger  ControlID="grdUser" EventName="SelectedIndexChanged" />
      </triggers>
            <triggers>
                <asp:AsyncPostBackTrigger  ControlID="btnSubmit" EventName="Click" />
                 <asp:AsyncPostBackTrigger  ControlID="btnUploadSubmit" EventName="Click" />
                
      </triggers>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <!--contentwrapper-->
</asp:Content>
