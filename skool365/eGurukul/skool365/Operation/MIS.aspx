<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MIS.aspx.cs" Inherits="Operation_MIS" MasterPageFile="~/Operation/CRMOperationAreaFooterLess.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">

    <script type="text/javascript">
        function HideLabel() {
            var seconds = 2;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        }

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

    <div id="Div1" class="contentwrapper">
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
        <div id="UserList" class="subcontent" style="display: block;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <table cellpadding="2" cellspacing="2" border="1" width="80%" style="border-color: deepskyblue; margin-left: 10%">
                            <tr>
                                <td colspan="3">Select Underwriter : 
                                    <asp:DropDownList ID="ddlUWList" AutoPostBack="true" runat="server" Width="630px" OnSelectedIndexChanged="ddlUWList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%-- CHART 1 (TOP) OVERALL SUMMARY--%>
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Chart ID="TATChart" runat="server" BackColor="#D3DFF0" Palette="BrightPastel" ImageType="Png" ImageUrl="TempImages/ChartPic_#SEQ(300,3)" Width="1102px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="26, 59, 105">
                                                <Titles>
                                                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Weekly TAT" Alignment="TopLeft" ForeColor="26, 59, 105"></asp:Title>
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Enabled="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                        <Position Y="21" Height="22" Width="18" X="73"></Position>
                                                    </asp:Legend>
                                                </Legends>
                                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                <Series>
                                                    <asp:Series Name="Sales" BorderColor="180, 26, 59, 105"></asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisY>
                                                        <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisX>
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                            <p class="dscr">
                                                Hold the mouse cursor over a&nbsp;column to&nbsp;preview a weekly TAT chart, and&nbsp;click on a data&nbsp;point to&nbsp;retrieve a&nbsp;detailed 
				chart of TAT in that week.
                                            </p>
                                            <div id="fadingTooltip" style="z-index: 999; visibility: hidden; position: absolute; background-color: lemonchiffon; border-right: darkgray 1px outset; border-top: darkgray 1px outset; font-size: 12pt; border-left: darkgray 1px outset; width: auto; color: black; border-bottom: darkgray 1px outset; height: auto; margin: 3px 3px 3px 3px; padding: 3px 3px 3px 3px; borderbottomwidth: 3px 3px 3px 3px;">
                                            </div>

                                            <script type="text/javascript">
                                                var fadingTooltip;
                                                var wnd_height, wnd_width;
                                                var tooltip_height, tooltip_width;
                                                var tooltip_shown = false;
                                                var transparency = 100;
                                                var timer_id = 1;
                                                var tooltiptext;

                                                // override events
                                                window.onload = winload;
                                                window.onresize = winonresize;
                                                document.onmousemove = doconmousemove;

                                                // override events
                                                function winload() { WindowLoading(); WindowLoadingBrUW(); }
                                                function winonresize() { UpdateWindowSize(); UpdateWindowSizeBrUW(); }
                                                function doconmousemove() { AdjustToolTipPosition(); AdjustToolTipPositionBrUW(); }

                                                function DisplayTooltip(tooltip_text) {

                                                    fadingTooltip.innerHTML = tooltip_text;

                                                    tooltip_shown = (tooltip_text != "") ? true : false;
                                                    if (tooltip_text != "") {
                                                        // Get tooltip window height
                                                        tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;
                                                        transparency = 0;
                                                        ToolTipFading();
                                                    }
                                                    else {
                                                        clearTimeout(timer_id);
                                                        fadingTooltip.style.visibility = "hidden";
                                                    }
                                                }

                                                function AdjustToolTipPosition(e) {
                                                    if (tooltip_shown) {
                                                        // Depending on IE/Firefox, find out what object to use to find mouse position
                                                        var ev;
                                                        if (e)
                                                            ev = e;
                                                        else
                                                            ev = event;

                                                        fadingTooltip.style.visibility = "visible";
                                                        offset_y = (ev.clientY + tooltip_height - document.body.scrollTop + 30 >= wnd_height) ? - 15 - tooltip_height : 20;
                                                        fadingTooltip.style.left = Math.min(wnd_width - tooltip_width - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
                                                        fadingTooltip.style.top = ev.clientY + offset_y + document.body.scrollTop + 'px';
                                                    }
                                                }
                                                function WindowLoading() {
                                                    fadingTooltip = document.getElementById('fadingTooltip');
                                                    // Get tooltip  window width				
                                                    tooltip_width = (fadingTooltip.style.pixelWidth) ? fadingTooltip.style.pixelWidth : fadingTooltip.offsetWidth;
                                                    // Get tooltip window height
                                                    tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;
                                                    UpdateWindowSize();
                                                }

                                                function ToolTipFading() {
                                                    if (transparency <= 100) {
                                                        fadingTooltip.style.filter = "alpha(opacity=" + transparency + ")";
                                                        fadingTooltip.style.opacity = transparency / 100;
                                                        transparency += 5;
                                                        timer_id = setTimeout('ToolTipFading()', 35);
                                                    }
                                                }

                                                function UpdateWindowSize() {
                                                    wnd_height = document.body.clientHeight;
                                                    wnd_width = document.body.clientWidth;
                                                }
                                            </script>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <%-- CHART 2/3 (MIDDLE) --%>
                            <tr style="align-content: center">
                                <td class="tdchart">

                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Chart ID="OverallSummaryChart" runat="server" Palette="BrightPastel"
                                                ImageType="Png" ImageUrl="TempImages/ChartPic_#SEQ(300,3)"
                                                BackColor="#F3DFC1" Width="362px" Height="296px" borderlinestyle="Solid"
                                                backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="181, 64, 1"
                                                OnClick="OverallSummaryChart_Click">
                                                <Titles>
                                                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Title" Alignment="TopLeft" ForeColor="26, 59, 105" Name="Title1"></asp:Title>
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" TitleFont="Microsoft Sans Serif, 8pt, style=Bold"></asp:Legend>
                                                </Legends>
                                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                <Series>
                                                    <asp:Series Name="Series1" ChartType="Pie" BorderColor="180, 26, 59, 105" ShadowOffset="4"></asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                        <AxisY LineColor="64, 64, 64, 64">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisY>
                                                        <AxisX LineColor="64, 64, 64, 64">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisX>
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="OverallSummaryChart" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Chart ID="SummaryByProcessingChart" runat="server" Palette="BrightPastel"
                                                ImageType="Png" ImageUrl="TempImages/ChartPic_#SEQ(300,3)"
                                                BackColor="#F3DFC1" Width="362px" Height="296px" borderlinestyle="Solid"
                                                backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="181, 64, 1"
                                                OnClick="SummaryByProcessingChart_Click">
                                                <Titles>
                                                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Title" Alignment="TopLeft" ForeColor="26, 59, 105" Name="Title1"></asp:Title>
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" TitleFont="Microsoft Sans Serif, 8pt, style=Bold"></asp:Legend>
                                                </Legends>
                                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                <Series>
                                                    <asp:Series Name="Series1" ChartType="Pie" BorderColor="180, 26, 59, 105" ShadowOffset="4"></asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                        <AxisY LineColor="64, 64, 64, 64">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisY>
                                                        <AxisX LineColor="64, 64, 64, 64">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisX>
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="SummaryByProcessingChart" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>

                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Chart ID="SummaryByTATChart" runat="server" Palette="BrightPastel"
                                                ImageType="Png" ImageUrl="TempImages/ChartPic_#SEQ(300,3)"
                                                BackColor="#F3DFC1" Width="362px" Height="296px" borderlinestyle="Solid"
                                                backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="181, 64, 1"
                                                OnClick="SummaryByTATChart_Click">
                                                <Titles>
                                                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Title" Alignment="TopLeft" ForeColor="26, 59, 105" Name="Title1"></asp:Title>
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" TitleFont="Microsoft Sans Serif, 8pt, style=Bold"></asp:Legend>
                                                </Legends>
                                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                <Series>
                                                    <asp:Series Name="Series1" ChartType="Pie" BorderColor="180, 26, 59, 105" ShadowOffset="4"></asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                        <AxisY LineColor="64, 64, 64, 64">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisY>
                                                        <AxisX LineColor="64, 64, 64, 64">
                                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                        </AxisX>
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="SummaryByTATChart" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <%-- CHART 4/5 (BOTTOM) --%>
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Chart ID="BranchUWChart" runat="server" BackColor="#D3DFF0" Palette="BrightPastel" ImageType="Png" ImageUrl="TempImages/ChartPic_#SEQ(300,3)" Width="1102px" Height="296px" borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="26, 59, 105">
                                                            <Titles>
                                                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Region Underwriter" Alignment="TopLeft" ForeColor="26, 59, 105"></asp:Title>
                                                            </Titles>
                                                            <Legends>
                                                                <asp:Legend Enabled="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                                                    <Position Y="21" Height="22" Width="18" X="73"></Position>
                                                                </asp:Legend>
                                                            </Legends>
                                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                                            <Series>
                                                                <asp:Series Name="Sales" BorderColor="180, 26, 59, 105"></asp:Series>
                                                            </Series>
                                                            <ChartAreas>
                                                                <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                    <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                                    </AxisY>
                                                                    <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                                    </AxisX>
                                                                </asp:ChartArea>
                                                            </ChartAreas>
                                                        </asp:Chart>
                                                        <p class="dscr">
                                                            Hold the mouse cursor over a&nbsp;column to&nbsp;preview a Region Underwriter chart, and&nbsp;click on a data&nbsp;point to&nbsp;retrieve a&nbsp;detailed 
				chart of Underwriter in that Region.
                                                        </p>
                                                        <div id="fadingTooltipBrUW" style="z-index: 999; visibility: hidden; position: absolute; background-color: lemonchiffon; border-right: darkgray 1px outset; border-top: darkgray 1px outset; font-size: 12pt; border-left: darkgray 1px outset; width: auto; color: black; border-bottom: darkgray 1px outset; height: auto; margin: 3px 3px 3px 3px; padding: 3px 3px 3px 3px; borderbottomwidth: 3px 3px 3px 3px; top: 300px;">
                                                        </div>
                                                        <script type="text/javascript">
                                                            var fadingTooltipBrUW;
                                                            var wnd_heightBrUW, wnd_widthBrUW;
                                                            var tooltip_heightBrUW, tooltip_widthBrUW;
                                                            var tooltip_shownBrUW = false;
                                                            var transparencyBrUW = 100;
                                                            var timer_idBrUW = 1;
                                                            var tooltiptextBrUW;

                                                            // override events
                                                            //window.onload = WindowLoadingBrUW;
                                                            //window.onresize = UpdateWindowSizeBrUW;
                                                            //document.onmousemove = AdjustToolTipPositionBrUW;

                                                            function DisplayTooltipBrUW(tooltip_textBrUW) {

                                                                fadingTooltipBrUW.innerHTML = tooltip_textBrUW;
                                                                tooltip_shownBrUW = (tooltip_textBrUW != "") ? true : false;
                                                                if (tooltip_textBrUW != "") {
                                                                    // Get tooltip window height
                                                                    tooltip_heightBrUW = (fadingTooltipBrUW.style.pixelHeight) ? fadingTooltipBrUW.style.pixelHeight : fadingTooltipBrUW.offsetHeight;
                                                                    transparencyBrUW = 0;
                                                                    ToolTipFadingBrUW();
                                                                }
                                                                else {
                                                                    clearTimeout(timer_idBrUW);
                                                                    fadingTooltipBrUW.style.visibility = "hidden";
                                                                }
                                                            }

                                                            function AdjustToolTipPositionBrUW(e) {
                                                                if (tooltip_shownBrUW) {
                                                                    // Depending on IE/Firefox, find out what object to use to find mouse position
                                                                    var ev;
                                                                    if (e)
                                                                        ev = e;
                                                                    else
                                                                        ev = event;

                                                                    fadingTooltipBrUW.style.visibility = "visible";
                                                                    offset_y = (ev.clientY + tooltip_heightBrUW - document.body.scrollTop + 30 >= wnd_heightBrUW) ? - 15 - tooltip_heightBrUW : 20;
                                                                    fadingTooltipBrUW.style.left = Math.min(wnd_widthBrUW - tooltip_widthBrUW - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
                                                                    fadingTooltipBrUW.style.top = ev.clientY + offset_y + document.body.scrollTop + 'px';
                                                                }
                                                            }
                                                            function WindowLoadingBrUW() {
                                                                fadingTooltipBrUW = document.getElementById('fadingTooltipBrUW');
                                                                // Get tooltip  window width				
                                                                tooltip_widthBrUW = (fadingTooltipBrUW.style.pixelWidth) ? fadingTooltipBrUW.style.pixelWidth : fadingTooltipBrUW.offsetWidth;
                                                                // Get tooltip window height
                                                                tooltip_heightBrUW = (fadingTooltipBrUW.style.pixelHeight) ? fadingTooltipBrUW.style.pixelHeight : fadingTooltipBrUW.offsetHeight;
                                                                UpdateWindowSizeBrUW();
                                                            }

                                                            function ToolTipFadingBrUW() {

                                                                if (transparencyBrUW <= 100) {
                                                                    fadingTooltipBrUW.style.filter = "alpha(opacity=" + transparencyBrUW + ")";
                                                                    fadingTooltipBrUW.style.opacity = transparencyBrUW / 100;
                                                                    transparencyBrUW += 5;
                                                                    timer_idBrUW = setTimeout('ToolTipFadingBrUW()', 35);
                                                                }
                                                            }

                                                            function UpdateWindowSizeBrUW() {
                                                                wnd_heightBrUW = document.body.clientHeight;
                                                                wnd_widthBrUW = document.body.clientWidth;
                                                            }
                                                        </script>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlUWList" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
