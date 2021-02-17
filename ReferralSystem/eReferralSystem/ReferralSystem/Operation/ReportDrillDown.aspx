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

<div>

    <div style="border: 2px solid #00c2a8;font-size: 16px;">
        <div style="float: left;width: 15.75%;background-color:#00c2a8;/* margin-bottom: -1px; */padding-bottom: -2px; font-size:14px;color:white;">Client Type Wise Summary</div>
         <div style="float:right;width: 83.24%; font-size:14px; margin-left:5px;">
             <asp:Literal ID="Literal1" runat="server"></asp:Literal>
         </div>
        <div style="clear:both;"></div>
       
    </div>  
    <div style="clear:both;"></div>
    <div>
    <div style="float: left;width: 16%;border: 2px solid #d4d4d4;padding: 3px;background-color: #64847f;">
        
    
        <asp:GridView ID="grdStyled2" runat="server"
                                CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%"
                               >
                               
                                
                                
                                
                                <EmptyDataRowStyle HorizontalAlign="Center" />

                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>

<RowStyle BorderWidth="1px" BorderStyle="Solid"></RowStyle>
                            </asp:GridView>
    </div>
                          
      <div style="float:right;width: 83.5%;overflow: auto;border: 2px solid #d4d4d4;max-height: 400px;padding: 3px;">
                            <asp:GridView ID="grdStyled" runat="server"
                                CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%"
                               OnRowCommand="grdStyled_RowCommand">
                               
                                
                                <Columns>
                                     <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton runat="server" Text="Go Next >>" CommandName="SelectData" CommandArgument='<%# Eval("ClientId") %>' />
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
    </div>
</div>
    
</asp:Content>

