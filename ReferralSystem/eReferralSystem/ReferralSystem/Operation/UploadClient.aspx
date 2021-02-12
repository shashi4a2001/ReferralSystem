<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="UploadClient.aspx.cs" Inherits="Operation_UploadClient" %>
<%@ Register Src="~/UserControl/UC_PageLabel.ascx" TagPrefix="uc1" TagName="UC_PageLabel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageLabelContent" Runat="Server">
    <uc1:UC_PageLabel runat="server" ID="UC_PageLabel" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" Runat="Server">
         <div>Upload Client <span style="color: red;">*</span></div>
         <div>
             <asp:FileUpload ID="FileUpload1" runat="server" Width="499px" /></div>
          
          <div style="margin-top:5px;">
                <asp:Button ID="btnGo" runat="server" Text="Show Records In Grid" CausesValidation="true" OnClick="btnGo_Click" />
              &nbsp;&nbsp;
              <asp:Button ID="btnUpload" runat="server" Text="Upload Records" CausesValidation="true" OnClick="btnUpload_Click" />
               &nbsp;&nbsp;
              <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Common/Images/export_toExcel.png" OnClick="ImageButton1_Click" />
              &nbsp;&nbsp;
              <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
          </div>
          <div>
             <table>
                 <tr>
                     <td><span style="color:#00c2a8;font-weight:bold;">Uploaded Summary || </span></td>
                     <td>New :</td><td><asp:Label ID="lblNewUploadedRecord" runat="server" Text="" Font-Bold="True" ForeColor="#00c2a8"></asp:Label></td>
                    <td>Duplicate :</td><td><asp:Label ID="lblDuplicateRecord" runat="server" Text="" Font-Bold="True" ForeColor="#00c2a8"></asp:Label></td>
                    <td>Error :</td><td><asp:Label ID="lblErrorRecord" runat="server" Text="" Font-Bold="True" ForeColor="red"></asp:Label> (Including Incorrect ClientTypeCode/CreatedDate)</td>

                 </tr>

             </table>
          </div>
          <div style="width: 100%; overflow: auto; border: 2px solid #d4d4d4; max-height: 350px;padding:3px">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="true"
                                CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%">
                                <EmptyDataRowStyle HorizontalAlign="Center" />

                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
     </div>
</asp:Content>

