<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="RoleManagement.aspx.cs" Inherits="RoleManagement" %>
<%@ Register Src="~/UserControl/UC_PageLabel.ascx" TagPrefix="uc1" TagName="UC_PageLabel" %>


<asp:Content ID="Content2" ContentPlaceHolderID="PageLabelContent" Runat="Server">
    <uc1:UC_PageLabel runat="server" ID="UC_PageLabel" />
</asp:Content> 

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div class="row">
             <div class="col-md-10 col-md-offset-1">
                 <div class="card card-table">
                     <div class="card-header">
        <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%">
            <Columns>
                <asp:BoundField DataField="ClientTypeLevel" HeaderText="Level" ReadOnly="True" SortExpression="ClientTypeLevel" />
                
                <asp:TemplateField HeaderText="code">
                    <ItemTemplate>
                       <asp:Label ID="lblCode" runat="server" Text='<%# Eval("ClientTypeCode").ToString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="ClientTypeName" HeaderText="Name" ReadOnly="True" SortExpression="ClientTypeName" />
                
                <asp:TemplateField HeaderText="SuperAdmin">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSuperAdmin" runat="server" Checked='<%# Eval("CanMakeSuperAdmin") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="MasterAgent">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkMasterAgent" runat="server" Checked='<%# Eval("CanMakeMasterAgent") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SubAgent">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSubAgent" runat="server" Checked='<%# Eval("CanMakeSubAgent") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Franchisee">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkFranchisee" runat="server" Checked='<%# Eval("CanMakeFranchisee") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FranchiseeAgent">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkFranchiseeAgent" runat="server" Checked='<%# Eval("CanMakeFranchiseeAgent") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Individual">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIndividual" runat="server" Checked='<%# Eval("CanMakeIndividual") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

              



            </Columns>
            
        </asp:GridView>
                          <asp:button runat="server" text="Update Data" id="UpdateData" name="UpdateData" OnClick="UpdateData_Click" />
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
                     </div>
                 </div>
        </div>
   
</asp:Content>

