<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="CalculateBrokerage.aspx.cs" Inherits="Operation_CalculateBrokerage" %>

<%@ Register Src="~/UserControl/UC_PageLabel.ascx" TagPrefix="uc1" TagName="UC_PageLabel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageLabelContent" Runat="Server">
     <style type="text/css">

        .MyCalendar .ajax__calendar_container {
            border: 1px solid #646464;
            background-color: lemonchiffon;
            color: red;
        }

        .MyCalendar .ajax__calendar_other .ajax__calendar_day, .MyCalendar .ajax__calendar_other .ajax__calendar_year {
            color: black;
        }

        .MyCalendar .ajax__calendar_hover .ajax__calendar_day, .MyCalendar .ajax__calendar_hover .ajax__calendar_month, .MyCalendar .ajax__calendar_hover .ajax__calendar_year {
            color: black;
        }

        .MyCalendar .ajax__calendar_active .ajax__calendar_day, .MyCalendar .ajax__calendar_active .ajax__calendar_month, .MyCalendar .ajax__calendar_active .ajax__calendar_year {
            color: black;
            font-weight: bold;
        }

        
    </style>
    <uc1:UC_PageLabel runat="server" ID="UC_PageLabel" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" Runat="Server">
      <style>
    hr{    margin-top: 6px !important;
    margin-bottom: 6px !important;
    }
</style>


     <div class="row">
             <div class="col-md-10 col-md-offset-1">
                 <div class="card card-table">
                     <div class="card-header">
                          <table style="width:100%">
                            <tr>
                                <td>Sharing Type <span style="color:red;">*</span></td>
                                <td>

                                    <asp:DropDownList ID="ddlRevenueType" runat="server" Width="200px" OnSelectedIndexChanged="ddlRevenueType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="Select">--Select--</asp:ListItem>
                                        <asp:ListItem Value="101">Referral Amount</asp:ListItem>
                                        <asp:ListItem Value="102">Revenue Amount</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="ddlRevenueType" ForeColor="#FF3300" InitialValue="Select"></asp:RequiredFieldValidator>

                                </td>

                                <td></td>
                                <td> 
                                    
                                </td>
                                <td></td>
                            </tr>

                              <tr>
                                  <td>Share To <span style="color:red;">*</span></td>
                                  <td>
                                       <asp:DropDownList ID="ddlShareTo" runat="server" Width="200px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="ddlShareTo" ForeColor="#FF3300" InitialValue="0"></asp:RequiredFieldValidator>

                                  </td>
                                  <td>Amount to Share (Per Client) <span style="color:red;">*</span></td>
                                  <td>
                                      <asp:TextBox ID="txtAmount" runat="server" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required.." BorderStyle="Dashed" ControlToValidate="txtAmount" ForeColor="#FF3300" Display = "Dynamic"></asp:RequiredFieldValidator>
                                  </td>
                                  <td></td>
                              </tr>
                              <tr>

                                   <td>Date From <span style="color: red;">*</span></td>
                            <td>
                                <asp:TextBox ID="txtDateFrom" Text="" runat="server" MaxLength="10" Width="200px" Enabled="true" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="regtxtDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="calttxtDateFrom" runat="server" TargetControlID="txtDateFrom"
                                    CssClass="MyCalendar" Format="dd/MM/yyyy" />
                            </td>
                            <td>Date To <span style="color: red;">*</span></td>
                            <td>
                                <asp:TextBox ID="txtDateTo" Text="" runat="server" MaxLength="10" Width="200px" Enabled="true" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="regtxtDateTo" runat="server" ControlToValidate="txtDateTo"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="caltxtDateTo" runat="server" TargetControlID="txtDateTo"
                                    CssClass="MyCalendar" Format="dd/MM/yyyy" />

                            </td>
                            <td>
                                 <asp:Button ID="btnGo" runat="server" Text="Process This Amount" CausesValidation="true" OnClick="btnGo_Click" />
                            </td>
                          </tr>
                          <tr><td colspan="5"> <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td></tr>
                           
                              <tr>
                                  <td colspan="5">
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
                                  </td>

                              </tr>   
                          </table>
                     </div>
                 </div>
             </div>
    </div>
         
         
          
        
  
</asp:Content>

