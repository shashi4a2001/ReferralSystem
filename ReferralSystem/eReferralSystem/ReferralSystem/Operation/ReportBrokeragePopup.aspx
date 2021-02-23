<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPagePopuUp.master"  AutoEventWireup="true" CodeFile="ReportBrokeragePopup.aspx.cs" Inherits="ReportBrokeragePopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageLabelContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div>
        <asp:GridView ID="grdStyled" runat="server"></asp:GridView>
    </div>
        <div>
            <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label></div>
</asp:Content>
