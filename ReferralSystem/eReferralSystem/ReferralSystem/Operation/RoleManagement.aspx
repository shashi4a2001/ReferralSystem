<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="RoleManagement.aspx.cs" Inherits="RoleManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div>
        Role management
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
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
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
    <div>
        <asp:button runat="server" text="Update Data" id="UpdateData" name="UpdateData" OnClick="UpdateData_Click" />
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
</asp:Content>

