<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="ReportDrillDown.aspx.cs" Inherits="Operation_ReportDrillDown" EnableEventValidation = "false" %>
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
    <script>
        function ShowSubReport(a, b)
        {
            var h = 0;
            var w = 0;

            if (b == 101)
            {
                h = 300;
                w = 950;
            
            }
            if (b == 102) {
                h = 300;
                w = 630;

            }
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open('ReportBrokeragePopup.aspx?p1=' + a + '&p2=' + b,
                    "mywindow", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            
        }
    </script>
<div>

    <div style="border: 2px solid #00c2a8;font-size: 16px;">
        <div style="float: left;width: 15.75%;background-color:#00c2a8;/* margin-bottom: -1px; */padding-bottom: -2px; font-size:14px;color:white; line-height: 38px;padding-left:15px;">Client Type Wise Summary</div>
         <div style="float:right;width: 83.24%; font-size:14px; margin-left:5px; line-height: 26px;">
             <asp:Literal ID="Literal1" runat="server"></asp:Literal>

             <span style="margin-left:100px;">
             <asp:label ID="Label1" runat="server" Text="Client Name :" Font-Bold="True" ></asp:label>
             <asp:literal ID="ltrlClientName" runat="server" ></asp:literal>
                
                <span style="margin-left:20px;">&nbsp;</span>
             <asp:label ID="Label2" runat="server" Text="Referral Amount :" Font-Bold="True" ></asp:label>
             <asp:literal ID="ltrlClientReferralAmount" runat="server"></asp:literal>

                 <span style="margin-left:20px;">&nbsp;</span>
             <asp:label ID="Label3" runat="server" Text="Referral Revenue :" Font-Bold="True" ></asp:label>
             <asp:literal ID="ltrlClientReferralRevenue" runat="server"></asp:literal>
             </span>

             <span style="float:right; "><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Common/Images/export_toExcel.png" OnClick="ImageButton1_Click" /></span>
            
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
            OnRowCommand="grdStyled_RowCommand" AutoGenerateColumns="False">
                                
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="Go Next >>" CommandName="SelectData" CommandArgument='<%# Eval("ClientId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ClientTypeCode" HeaderText="ClientTypeCode" />
                    <asp:BoundField DataField="ClientTypeName" HeaderText="ClientTypeName" />
                    <asp:BoundField DataField="ClientName" HeaderText="ClientName" />

                    <asp:TemplateField HeaderText="ReferralAmount" ItemStyle-Font-Underline="true">
                        <ItemTemplate>
                            <a href="#" onclick="javascript:ShowSubReport('<%#Eval("ClientId")%>',101)"><%#Eval("ReferralAmount")%></a>
                         </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ReferralRevenue" ItemStyle-Font-Underline="true">
                        <ItemTemplate>
                            <a href="#" onclick="javascript:ShowSubReport('<%#Eval("ClientId")%>',102)"><%#Eval("ReferralRevenue")%></a>
                         </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="ContactPerson" HeaderText="ContactPerson" />
                    <asp:BoundField DataField="EmailId" HeaderText="EmailId" />
                    <asp:BoundField DataField="ClientId" HeaderText="ClientId" />

                    

                     


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

