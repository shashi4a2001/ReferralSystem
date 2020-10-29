<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrdDispCnfg.aspx.cs" Inherits="Operation_GrdDispCnfg"
    MasterPageFile="~/Operation/CRMOperationArea.Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server" >
    <script type="text/javascript">

        function IsNumeric(val) {

            if (!(event.keyCode > 47 && event.keyCode < 58)) {
                alert("Enter Numerics Only")
                event.srcElement.focus();
                return false;
            }
        }

        function Confirm(Msg) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(Msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            while (confirm_value.lastChild) {
                confirm_value.removeChild(confirm_value.lastChild);
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        $(function () {
            blinkeffect('#txtblnk');
        })
        function blinkeffect(selector) {
            $(selector).fadeOut('slow', function () {
                $(this).fadeIn('slow', function () {
                    blinkeffect(this);
                });
            });
        }
    </script>

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
    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Grid Display Privileges" />--%>
    <div id="contentwrapper" class="contentwrapper">
        <div id="dvMessage" runat="server">
        </div>
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
                <ContentTemplate>
                    <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px;
                        font-family: Arial; margin: 0 auto; padding-left: 6px;">
                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div>
                        <div class="stdform stdform2">
                            <p>
                                <label>
                                    Role Name :
                                </label>
                                <span class="field">
                                    <asp:DropDownList ID="ddlUserRole" runat="server"  AutoPostBack="True"
                                        class="chzn-select" OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </div> 
                       <%-- <div class="maingrid" style="margin-top: 20px;">--%>
                        <table>
                        <tr>
                        <td>
                         <asp:TreeView ID="trvMenu" Height="100px" runat="server" ShowCheckBoxes="Leaf" OnTreeNodeCheckChanged="trvMenu_TreeNodeCheckChanged">
                                <ParentNodeStyle Font-Bold="True" ForeColor="#333300" />
                                <RootNodeStyle Font-Bold="True" ForeColor="#CC3300" />
                            </asp:TreeView>
                           
                           </td>
                        </tr>

                           <tr>
                        <td> <asp:Button ID="btnSubmit" runat="server" Text="Assign Access Right" OnClick="btnSubmit_Click"
                                Visible="true" /></td>
                        </tr>


                        </table>
                           
                        
                      
                <triggers>
                <asp:AsyncPostBackTrigger  ControlID="trvMenu" EventName="TreeNodeCheckChanged"/></triggers>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     
  
</asp:Content>
 