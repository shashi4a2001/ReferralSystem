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

                 <asp:TemplateField HeaderText="NationalHead">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkNationalHead" runat="server" Checked='<%# Eval("CanMakeNationalHead") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="RegionalHead">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRegionalHead" runat="server" Checked='<%# Eval("CanMakeRegionalHead") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="StateFranchisee">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStateFranchisee" runat="server" Checked='<%# Eval("CanMakeStateFranchisee") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DistrictFranchisee">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDistrictFranchisee" runat="server" Checked='<%# Eval("CanMakeDistrictFranchisee") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="IndividualAgent">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIndividualAgent" runat="server" Checked='<%# Eval("CanMakeIndividualAgent") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

              



                <asp:TemplateField HeaderText="Individual">
                     <ItemTemplate>
                        <asp:CheckBox ID="chkIndividual" runat="server" Checked='<%# Eval("CanMakeIndividual") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

              



            </Columns>
            
<RowStyle BorderWidth="1px" BorderStyle="Solid"></RowStyle>
            
        </asp:GridView>
                          <asp:button runat="server" text="Update Data" id="UpdateData" name="UpdateData" OnClick="UpdateData_Click" />
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
                     </div>
                 </div>
        </div>
   
</asp:Content>

