<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMLogOutOperationArea.Master"
    AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="Operation_AccessDenied" ValidateRequest="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
  
    <style type="text/css">
        #wrapper {
            width: 2000px;
            margin: 0 auto;
            padding: 0;
            overflow: auto;
            margin: 8px 32% #leftcolumn;

        {
            width: 200px;
            background-color: red;
            padding: 0;
            margin: 0;
            display: block;
            border: 1px solid white;
            position: fixed;
        }

        #rightcolumn {
            width: 600px;
            background-color: yellow;
            display: block;
            float: left;
            border: 1px solid white;
        }
 
 
    </style>
    <style type="text/css">
        h2 {
            margin-left: 5px;
        }

        .clearfix:after {
            content: ".";
            display: block;
            clear: both;
            visibility: hidden;
            line-height: 0;
            height: 0;
        }

        .clearfix {
            display: inline-block;
        }

        .box {
            float: left;
            width: 80px;
            margin: 10px;
            padding: 10px;
            border: 1px solid #ccc;
        }
    </style>
    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>


    <div id="contentwrapper" class="contentwrapper">
        <center style="height: 100px;">
            <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                <asp:Label ID="lblMessage" Text="Sorry! You are not authorised to access the requested page." runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="Red" />
            </div>
        </center>
        <br />
        <br />
        <br />
        <br />
         <br />
        <br />
    </div>

</asp:Content>


