<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="ReportAccountOpen.aspx.cs" Inherits="Operation_ReportAccountOpen" %>
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
                            <div style="margin: 8px 10%">
                            <span>Client Type <span style="color: red;">*</span></span>
                            <span><asp:DropDownList ID="ddlClientType" runat="server" Width="120px"></asp:DropDownList></span>

                            <span>Date From <span style="color: red;">*</span></span>
                            <span>
                                <asp:TextBox ID="txtDateFrom" Text="" runat="server" MaxLength="10" Width="100px" Enabled="true" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="regtxtDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="calttxtDateFrom" runat="server" TargetControlID="txtDateFrom"
                                    CssClass="MyCalendar" Format="dd/MM/yyyy" />
                            </span>
                            <span>Date To <span style="color: red;">*</span></span>
                            <span>
                                <asp:TextBox ID="txtDateTo" Text="" runat="server" MaxLength="10" Width="100px" Enabled="true" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="regtxtDateTo" runat="server" ControlToValidate="txtDateTo"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="caltxtDateTo" runat="server" TargetControlID="txtDateTo"
                                    CssClass="MyCalendar" Format="dd/MM/yyyy" />

                            </span>
                            
                            <span>
                                <asp:RadioButton ID="rdbDetail" runat="server" Text="Detail" Checked="true" GroupName="reporttype" />
                                <asp:RadioButton ID="rdbSummary" runat="server" Text="Summary"  GroupName="reporttype"/>
                            </span>

                            <span>
                                <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="true" OnClick="btnGo_Click" />
                            </span>
                           
                                <span><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Common/Images/export_toExcel.png" OnClick="ImageButton1_Click" /></span>
                                
                        </div>
                        <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 400px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="true"
                                CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%">
                                <EmptyDataRowStyle HorizontalAlign="Center" />

                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
</asp:Content>

