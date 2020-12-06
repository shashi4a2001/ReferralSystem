<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage2.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Operation_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <style>
        #content{padding:0 !important;}
        input[type='submit'] {
            padding: 7px 16px !important;
        }
    </style>
    <div class="subTitle">Forgot Password..</div>

    <div style="padding:30px 0 0 20px;">
        <table>
            <tr>
                <td>User Id <span style="color:red;">*</span></td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtUserId" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                </td>
                <td></td>

                <td colspan="3">
                    <asp:Button ID="btnClick" runat="server" Text="Go" Width="70px" OnClick="btnClick_Click" /></td>
            </tr>
         </table>
     </div>
     <div><asp:Label ID="lblmsg" runat="server"></asp:Label></div>
</asp:Content>

