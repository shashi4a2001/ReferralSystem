<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Holidays.aspx.cs" Inherits="Operation_Holidays"
    MasterPageFile="~/Operation/CRMOperationArea2.Master" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        //Used to display Modal dailog while processing of records
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }


    </script>

    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Configure Mails Format" />  class="contentwrapper" --%>
    <div id="Div1" style="osition: absolute; left: 50%; top: 50%; margin-top: -30px; margin-left: -30px;">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <img runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height: 60px;"> </img>
                    <%--<asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>
    <div id="contentwrapper" class="contentwrapper">
        <div id="dvMessage" runat="server">
        </div>
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div style="margin-left: 2%; margin-top: 2%; height: 100%;">

                        <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>

                        <div>

                            <asp:Calendar ID="CMRCalendar" runat="server" Height="444px" Width="100%" OnDayRender="CMRCalendar_DayRender"
                                OnSelectionChanged="CMRCalendar_SelectionChanged" OnVisibleMonthChanged="CMRCalendar_VisibleMonthChanged"
                                DayNameFormat="Full" ForeColor="#000099" NextMonthText="" BorderWidth="2px" CellPadding="2" CellSpacing="2">

                                <DayHeaderStyle BackColor="#666699" ForeColor="White" />

                                <DayStyle BackColor="#CCCCCC" Font-Bold="True" Font-Italic="False" ForeColor="#000066" />

                                <SelectedDayStyle BackColor="#9999FF" Font-Bold="True" ForeColor="Maroon" />

                                <WeekendDayStyle BackColor="AliceBlue" Font-Bold="True" ForeColor="Red" />

                            </asp:Calendar>

                            <asp:Label ID="lblAction" runat="server"></asp:Label>

                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
