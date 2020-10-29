<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master" AutoEventWireup="true"
    CodeFile="ExcpReport.aspx.cs" Inherits="Operation_ExcpReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">

        //Confirmation Message
        function Confirm(MSG) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(MSG)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function hidememsg() {
            document.getElementById('customlightbox').style.display = 'none';
        }

        function validateSearch() {
            if (document.getElementById("<%=txtDateFrom.ClientID %>").value.trim() == '' || document.getElementById("<%=txtDateTo.ClientID %>").value.trim() == '') {
                return false;
            }
            return true;
        };

    </script>
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

    <style type="text/css">
        #wrapper {
            width: 2000px;
            margin: 0 auto;
            padding: 0;
            overflow: auto;
        }

        #leftcolumn {
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

        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>

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
        <div id="txtblnk_Remove" style="color: Red; font-size: 13px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
        <div id="UserList" class="subcontent" style="display: block;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin: 8px 10%">
                            <span>Date From <span style="color: red;">*</span></span>
                            <span>
                                <asp:TextBox ID="txtDateFrom" Text="" runat="server" MaxLength="10" Width="100px" Enabled="true" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="regtxtDateFrom" runat="server" ControlToValidate="txtDateFrom"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="calttxtDateFrom" runat="server" TargetControlID="txtDateFrom"
                                    CssClass="MyCalendar" Format="dd/MM/yyyy" />
                            </span>
                            <span>Date To <span style="color: red;">*</span></span>
                            <span>
                                <asp:TextBox ID="txtDateTo" Text="" runat="server" MaxLength="10" Width="100px" Enabled="true" autocomplete="off" />
                                <asp:RequiredFieldValidator ID="regtxtDateTo" runat="server" ControlToValidate="txtDateTo"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="caltxtDateTo" runat="server" TargetControlID="txtDateTo"
                                    CssClass="MyCalendar" Format="dd/MM/yyyy" />

                            </span>
                            <span><span>Segment </span><span>
                                <asp:DropDownList ID="ddlExceptionReport" runat="server" Width="250px">
                                    <asp:ListItem Text="LOGIN DETAILS" Value="LD"></asp:ListItem>
                                    <asp:ListItem Text="MAIL LOG DETAILS" Value="ML"></asp:ListItem>
                                    <asp:ListItem Text="EVENT LOG DETAILS" Value="EL"></asp:ListItem>
                                </asp:DropDownList>
                            </span><span>
                                <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="true" OnClick="btnGo_Click" OnClientClick="return validateSearch();" />
                            </span>
                        </div>
                        <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 400px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="true"
                                CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%">
                                <EmptyDataRowStyle HorizontalAlign="Center" />

                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
