<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Operation_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <div>
         <div style="position: fixed; top: 8em; z-index: 999999; right: 33px;">
                    <img src="../Common/images/shortcut/dashboardwatermark.png" alt="Access Panel" />
          </div>
        <div>
            <a href="RoleManagement.aspx">Role Management</a>
        </div>

    </div>
</asp:Content>
 