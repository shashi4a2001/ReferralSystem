<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="ClientRegistration.aspx.cs" Inherits="Operation_ClientRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div>
        <table>
            <tr>
                <td>Mobile No.</td>
                <td>
                    <asp:TextBox ID="txtMobileNo" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td colspan="3">
                    <asp:Button ID="btnFetchDetail" runat="server" Text="Fetch Detail" Width="156px" /></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td>LoginId</td>
                <td> <asp:TextBox ID="txtLoginId" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td colspan="2">
                    <asp:Button ID="btnFetchDetail0" runat="server" Text="Check Availability" Width="156px" /></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td>Client Code</td>
                <td> <asp:TextBox ID="txtClientCode" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td>Client Type</td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="213px"></asp:DropDownList></td>
                <td></td>
            </tr>
            
             <tr>               
                <td>Client Name</td>
                <td> <asp:TextBox ID="txtClientName" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td>Contact Person</td>
                <td> <asp:TextBox ID="txtContactPerson" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

            </tr>
             
             <tr>              
                <td>Email Id</td>
                <td> <asp:TextBox ID="txtEmailId" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td>Address</td>
                <td rowspan="2"> <asp:TextBox ID="txtAddress" runat="server" Width="196px" Height="41px" TextMode="MultiLine"></asp:TextBox></td>
                <td></td>
                 
            </tr>
             
             <tr>
                <td>Landline No.</td>
                <td> <asp:TextBox ID="txtLandlineNo" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>  
            </tr>
             
             <tr>
                <td colspan="6"><hr /></td>
            </tr>                     

             <tr>
                <td>Bank Name</td>
                <td> <asp:TextBox ID="txtBankName" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td>Client Name As Per Bank</td>
                <td> <asp:TextBox ID="txtClientNameAsperBank" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>
            </tr>
             
            <tr>
                <td>Account No.</td>
                <td> <asp:TextBox ID="txtAccountNo" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td>IFSC Code</td>
                <td> <asp:TextBox ID="txtIFSCCode" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>
            </tr>
             <tr>
                <td colspan="6"><hr /></td>
            </tr> 
            <tr>
                <td>Sharing (%)</td>
                <td> <asp:TextBox ID="txtSharingPercnt" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>

                <td>Referred Referral Code</td>
                <td> <asp:TextBox ID="txtReferredReferralCode" runat="server" Width="200px"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                <td></td>
                <td></td>
                
                <td>
                    </td>
                <td></td>
                
            </tr>
             

        </table>
    </div>
</asp:Content>

