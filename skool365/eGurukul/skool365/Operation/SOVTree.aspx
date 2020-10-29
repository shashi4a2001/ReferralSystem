<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SOVTree.aspx.cs" Inherits="Operation_SOVTree" MasterPageFile="~/Operation/CRMLogOutOperationArea.Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
    <head>
        <!-- Website Title -->
        <title>SOV Tree</title>
        <link href="../Common/CSS/Admin.css" type="text/css" rel="Stylesheet" />
        <style type="text/css">
            .style1 {
                color: #FF0000;
            }

            .style2 {
                color: #000099;
            }
        </style>
        <script type="text/javascript">
            function HideLabel() {
                var seconds = 2;
                setTimeout(function () {
                    document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
            };
        </script>
    </head>
    <body>
        <div class="content_wrapper">
            <div id="content">
                <div>
                    <table style="margin-left: 25%">
                        <tr>
                            <asp:Label ID="lblMessage" Text="" runat="server" />
                        </tr>
                        <tr>
                            <asp:TreeView ID="trSOV" runat="server" ImageSet="XPFileExplorer" NodeIndent="15">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                            </asp:TreeView>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </body>
</asp:Content>
