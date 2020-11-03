<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Operation_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div>
         <div style="position: fixed; top: 8em; z-index: 999999; right: 33px;">
                    <img src="../Common/images/shortcut/dashboardwatermark.png" alt="Access Panel" />
             
          </div>
       
         <div>
            <asp:GridView ID="grdSummary" runat="server"></asp:GridView>
             <div><a href="ReportAccountOpen.aspx">More..</a></div>
         </div>

        <div>
            <div>Account Detail</div>
            <div>

                    <table>
            <tr>
                <td>Mobile No.</td>
                <td>
                    <asp:Literal ID="ltrlMobileNo" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td colspan="3">
                    </td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td>LoginId</td>
                <td> 
                    <asp:Literal ID="ltrlLoginId" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td colspan="2">
                     
                    
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td>Client Type</td>
                <td>
                    <asp:Literal ID="ltrlClientType" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td>Client Code</td>
                <td> 
                    <asp:Literal ID="ltrlClientCode" runat="server"></asp:Literal>
                </td>
                <td></td>
            </tr>
            
             <tr>               
                <td>Client Name</td>
                <td> 
                    <asp:Literal ID="ltrlClientName" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td>Contact Person</td>
                <td> 
                    <asp:Literal ID="ltrlContactPerson" runat="server"></asp:Literal>
                </td>
                <td></td>

            </tr>
             
             <tr>              
                <td>Email Id</td>
                <td> 
                    <asp:Literal ID="ltrlEmailId" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td>Address</td>
                <td rowspan="2"> 
                    <asp:Literal ID="ltrlAddress" runat="server"></asp:Literal>
                </td>
                <td></td>
                 
            </tr>
             
             <tr>
                <td>Landline No.</td>
                <td> 
                    <asp:Literal ID="ltrlLandlineNo" runat="server"></asp:Literal>
                </td>
                <td></td>  
            </tr>
             
             <tr>
                <td colspan="6"><hr /></td>
            </tr>                     

             <tr>
                <td>Bank Name</td>
                <td> 
                    <asp:Literal ID="ltrlBankName" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td>Client Name As Per Bank</td>
                <td> 
                    <asp:Literal ID="ltrlClientNameAsperBank" runat="server"></asp:Literal>

                </td>
                <td></td>
            </tr>
             
            <tr>
                <td>Account No.</td>
                <td> 
                    <asp:Literal ID="ltrlAccountNo" runat="server"></asp:Literal>

                </td>
                <td></td>

                <td>IFSC Code</td>
                <td> 
                    <asp:Literal ID="ltrlIFSCCode" runat="server"></asp:Literal>

                </td>
                <td></td>
            </tr>
             <tr>
                <td colspan="6"><hr /></td>
            </tr> 
            <tr>
                <td>Sharing (%)</td>
                <td> 
                    <asp:Literal ID="ltrlSharingPercnt" runat="server"></asp:Literal>
                </td>
                <td></td>

                <td>Referred Referral Code</td>
                <td> 
                    <asp:Literal ID="ltrlReferredReferralCode" runat="server"></asp:Literal>

                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6"><hr /></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5" style="vertical-align:bottom;">
                 
                </td>
                
            </tr>
             

        </table>
            </div>
        </div>
    </div>
</asp:Content>
 