<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="ReportDrillDown.aspx.cs" Inherits="Operation_ReportDrillDown" %>
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
                            
                        <div style="width: 100%; overflow: auto; border: 2px solid #d4d4d4; max-height: 400px;padding:3px">
                            <asp:GridView ID="grdStyled" runat="server"
                                CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%"
                               OnRowCommand="grdStyled_RowCommand">
                               
                                
                                <Columns>
                                     <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton runat="server" Text="Select" CommandName="SelectData" CommandArgument='<%# Eval("ClientId") %>' />
            </ItemTemplate>
        </asp:TemplateField>
                                </Columns>
                               
                                
                                <EmptyDataRowStyle HorizontalAlign="Center" />

                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>

<RowStyle BorderWidth="1px" BorderStyle="Solid"></RowStyle>
                            </asp:GridView>
                        </div>
</asp:Content>

