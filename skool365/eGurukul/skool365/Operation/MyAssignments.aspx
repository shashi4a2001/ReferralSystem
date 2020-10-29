<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master"
    AutoEventWireup="true" CodeFile="MyAssignments.aspx.cs" Inherits="Operation_MyAssignments" validateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <div>
        <h1>
            <asp:Literal ID="ltrlPageHeader" runat="server"></asp:Literal>
        </h1>
        <hr />
    </div>

    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>

    <div class="inner">
        <!-- Begin one column tab content window -->
        <div class="onecolumn">
            <div class="content">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" class="bordercareer">
                            <tr>
                                <th width="5%" class="thbord" style="vertical-align: middle; text-align: center; padding-left: 8px;">Status
                                </th>
                                 <th width="1%" ></th>
                                <th width="30%" class="thbord thtdborderleft" style="vertical-align: middle; text-align: left; padding-left: 8px;">Title
                                </th>
                                <th width="45%" class="thbord" style="vertical-align: middle; text-align: left; padding-left: 8px;">Summary
                                </th>
                                <th width="10%" class="thbord" style="vertical-align: middle; text-align: left; padding-left: 8px;">Posted Date
                                </th>
                                <th width="15%" class="thbord" style="vertical-align: middle; text-align: center; padding-left: 8px;">Attachment
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                              <td style="vertical-align: middle; text-align: left; padding-left: 8px;" class="tdbord thtdborderleft">
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("GRD_STATUS") %>' />
                            </td>
                            <td></td>
                            <td style="vertical-align: middle; text-align: left; padding-left: 8px;" class="tdbord thtdborderleft">
                                <%#DataBinder.Eval(Container, "DataItem.AssignmentTitle")%>
                            </td>
                            <td style="vertical-align: middle; text-align: left; padding-left: 8px;" class="tdbord">
                                <%#DataBinder.Eval(Container, "DataItem.AssignmentSummary")%>
                            </td>
                            <td style="vertical-align: middle; text-align: left; padding-left: 8px;" class="tdbord">
                                <%#DataBinder.Eval(Container,"DataItem.PostedDate")%>
                            </td>
                            <td style="vertical-align: middle; text-align: center; padding-left: 8px; padding-top: 3px;"
                                class="tdbord">
                                <a target="_blank" href='UploadedDocument/Assignment/<%#  Eval("AttachmentPath") %>'>
                                    <img id="ImgRef" src="~/Common/Images/viewattachment.png" runat="server" visible='<%# Eval("AttachmentPath").ToString().Equals("#") ? false : true %>'>
                                </a>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
