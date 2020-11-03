<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="ClientRegistration.aspx.cs" Inherits="Operation_ClientRegistration" %>
<%@ Register Src="~/UserControl/UC_PageLabel.ascx" TagPrefix="uc1" TagName="UC_PageLabel" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PageLabelContent" Runat="Server">
    <uc1:UC_PageLabel runat="server" ID="UC_PageLabel" />
</asp:Content> 

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div>
        <table>
            <tr>
                <td>Mobile No. <span style="color:red;">*</span></td>
                <td>
                    <asp:TextBox ID="txtMobileNo" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtMobileNo" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>

                <td colspan="3">
                    <asp:Button ID="btnFetchDetail" runat="server" Text="Fetch Detail" Width="156px" /></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td>LoginId <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtLoginId" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtLoginId" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>

                <td colspan="2">
                    <asp:Button ID="btnFetchDetail0" runat="server" Text="Check Availability" Width="156px" CausesValidation="False" OnClick="btnFetchDetail0_Click" />
                    &nbsp;
                    <asp:Label ID="lblmsgCheckAvailability" runat="server"></asp:Label>
                    
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td>Client Type <span style="color:red;">*</span></td>
                <td>
                    <asp:DropDownList ID="ddlClientType" runat="server" Width="213px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="ddlClientType" ForeColor="#FF3300" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
                <td></td>

                <td>Client Code <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtClientCode" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtClientCode" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            
             <tr>               
                <td>Client Name <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtClientName" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtClientName" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>

                <td>Contact Person <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtContactPerson" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtContactPerson" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>

            </tr>
             
             <tr>              
                <td>Email Id <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtEmailId" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtEmailId" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailId"
                    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    Display = "Dynamic" ErrorMessage = "Invalid email id"/>
                </td>
                <td></td>

                <td>Address <span style="color:red;">*</span></td>
                <td rowspan="2"> 
                    <asp:TextBox ID="txtAddress" runat="server" Width="196px" Height="41px" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtAddress" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>
                 
            </tr>
             
             <tr>
                <td>Landline No. <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtLandlineNo" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtLandlineNo" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>  
            </tr>
             
             <tr>
                <td colspan="6"><hr /></td>
            </tr>                     

             <tr>
                <td>Bank Name <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtBankName" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtBankName" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>

                <td>Client Name As Per Bank <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtClientNameAsperBank" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtClientNameAsperBank" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>

                </td>
                <td></td>
            </tr>
             
            <tr>
                <td>Account No. <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtAccountNo" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtAccountNo" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>

                </td>
                <td></td>

                <td>IFSC Code <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtIFSCCode" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtIFSCCode" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>

                </td>
                <td></td>
            </tr>
             <tr>
                <td colspan="6"><hr /></td>
            </tr> 
            <tr>
                <td>Sharing (%) <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtSharingPercnt" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtSharingPercnt" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                    ErrorMessage="Enter valid number" ForeColor="#FF3300" Display = "Dynamic"
                    ControlToValidate="txtSharingPercnt" />
                </td>
                <td></td>

                <td>Referred Referral Code <span style="color:red;">*</span></td>
                <td> 
                    <asp:TextBox ID="txtReferredReferralCode" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtReferredReferralCode" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>

                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5" style="vertical-align:bottom;"><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                 
                </td>
                
            </tr>
             

        </table>
    </div>
    <div><asp:Label ID="lblmsg" runat="server"></asp:Label></div>
</asp:Content>

