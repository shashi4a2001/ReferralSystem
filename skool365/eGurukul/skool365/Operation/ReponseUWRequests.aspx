<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master" AutoEventWireup="true"
    CodeFile="ReponseUWRequests.aspx.cs" Inherits="Operation_ReponseUWRequests" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <script type="text/javascript">

        function setFocusToEntryTab(id) {
            if (id == "EntryTab") {
                document.getElementById("basicform").style.display = 'block';
                document.getElementById("UserList").style.display = 'none';
            }
            if (id == "UWList") {
                document.getElementById("UserList").style.display = "block";
                document.getElementById("basicform").style.display = "none";
            }
        }

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


        function decodeHTMLEntities(text) {
            var entities = [
                ['amp', '&'],
                ['apos', '\''],
                ['#x27', '\''],
                ['#x2F', '/'],
                ['#39', '\''],
                ['#47', '/'],
                ['lt', '<'],
                ['gt', '>'],
                ['nbsp', ' '],
                ['quot', '"']
            ];

            for (var i = 0, max = entities.length; i < max; ++i)
                text = text.replace(new RegExp('&' + entities[i][0] + ';', 'g'), entities[i][1]);

            return text;
        }

        function showdetail(cnt, To, From, CC, Subject, Message, RequestID, LogCreateDate) {

            document.getElementById('<%=hdReplyTo.ClientID%>').value = To;
            document.getElementById('<%=hdReplyFrom.ClientID%>').value = From;
            document.getElementById('<%=hdReplyCC.ClientID%>').value = CC;
            document.getElementById('<%=hdReplyDate.ClientID%>').value = LogCreateDate;
            document.getElementById('<%=hdSubject.ClientID%>').value = Subject;
            document.getElementById('<%=hdMessage.ClientID%>').value = Message;
            document.getElementById('<%=hdRequestID.ClientID%>').value = RequestID;


            document.getElementById('<%=txtReply.ClientID%>').value = "";

            document.getElementById('txtdescmsg').innerHTML = "";

            var i = 'dvdtl' + cnt;
            document.getElementById('txtdescmsg').innerHTML = decodeHTMLEntities(document.getElementById(i).innerHTML);
            document.getElementById('customlightbox').style.display = 'block';

        }

        function hidememsg() {
            document.getElementById('customlightbox').style.display = 'none';
        }

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

    <div style="position: fixed; z-index: 999; height: 75%; width: 75%; top: 20px; left: 20px; background-color: #e6f3f7; padding: 5px; border-color: #024e66; border-width: 2px; border-radius: 5px; display: none;"
        id="customlightbox">
        <span style="float: right; cursor: pointer;" title="Close" onclick="hidememsg()">
            <img src="../Common/images/icon_close.gif" />&nbsp;</span>
        <div style="width: 100%; cursor: move; background-color: #d1e7f9;" id='customlightboxHeader'>
            <span style="color: #050a11;">Reply </span>
            <br />
            <asp:TextBox ID="txtReply" Text="" TextMode="MultiLine" runat="server" Width="85%"
                Height="180px" autocomplete="off"></asp:TextBox>
            <asp:Button ID="btnReply" Text="Reply" runat="server" CausesValidation="false" OnClick="btnReply_Click"></asp:Button>
        </div>
        <hr />
        <div style="height: 70%; width: 100%; background-color: #e6f3f7;">
            <span style="color: #050a11;">Message Trails </span>
            <div id="txtdescmsg" style="height: 300px; width: 900px; overflow: auto; background-color: #ced8e0;">
            </div>
        </div>
    </div>

    <%--<uc1:UC_PageHeader ID="UC_PageHeader1" runat="server" PageTitle="Raise UW Requests Query" />--%>
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
    <div>
        <ul class="hornav">
            <%--  <li class="current"><a href="#basicform" id="A1" onclick="setFocusToEntryTab('EntryTab')">
                Raise Query</a></li> --%>
            <li class=""><a href="#UserList" id="A2" onclick="setFocusToEntryTab('UWList')">UW Requests</a></li>
        </ul>
    </div>
    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="txtblnk_Remove" style="color: Red; font-size: 16px; line-height: 25px; padding-left: 10px; font-family: Arial; margin: 0 auto; padding-left: 6px;">
                        <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <strong>UW Request ID</strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblReqID" runat="server" Font-Bold="true" ForeColor="Blue" />
                                </td>
                                <td rowspan="18"></td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>Account Name</strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblAccountName" runat="server" Font-Bold="true" ForeColor="Blue" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px; width: 120px;">
                                    <strong>Underwriter</strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblUnderwriter" runat="server" Font-Bold="true" ForeColor="Blue" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>Email Id</strong>
                                </td>
                                <td>
                                    <asp:Label ID="lblEmailId" runat="server" Font-Bold="true" ForeColor="Blue" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>CC</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCC" runat="server" TextMode="SingleLine" Width="500px" autocomplete="off" />
                                    <asp:RegularExpressionValidator ID="regExptxtCC" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                        ValidationGroup="vgSubmit" ControlToValidate="txtCC" CssClass="requiredFieldValidateStyle"
                                        ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>Attachment if any</strong>
                                </td>
                                <td>
                                    <asp:fileupload id="fleUpd" multiple="true" name="..." xmlns:asp="#unknown" tooltip="Select attachment"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Query Types</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlQueryTypes" runat="server" Width="150px">
                                        <asp:ListItem Text="Inprogress" Value="Inprogress"></asp:ListItem>
                                        <asp:ListItem Text="Query Raised" Value="Query Raised"></asp:ListItem>
                                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px">
                                    <strong>Raise Query</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequest" runat="server" autocomplete="off" TextMode="MultiLine" Width="720px" Height="180px" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Raise Query" OnClick="btnSubmit_Click"
                                        CssClass="btnblack" OnClientClick="Confirm('Do you really want to Raise Query?');"
                                        CausesValidation="true" Enabled="false" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btngray" />
                                    <asp:HiddenField ID="hdId" runat="server" />
                                    <asp:HiddenField ID="hdUWId" runat="server" />

                                    <asp:HiddenField ID="hdOriginalMessage" runat="server" />
                                    <asp:HiddenField ID="hdRequestID" runat="server" />
                                    <asp:HiddenField ID="hdReplyTo" runat="server" />
                                    <asp:HiddenField ID="hdReplyCC" runat="server" />
                                    <asp:HiddenField ID="hdReplyFrom" runat="server" />
                                    <asp:HiddenField ID="hdReplyDate" runat="server" />
                                    <asp:HiddenField ID="hdSubject" runat="server" />
                                    <asp:HiddenField ID="hdMessage" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="UserList" class="subcontent" style="display: none;">
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin: 8px 32%">
                            <span>
                                <asp:RadioButton ID="rdoMyAccount" Text="My Account" runat="server" GroupName="Select"
                                    Checked="true" /></span> <span>
                                        <asp:RadioButton ID="rdoAllAccount" Text="All Account" runat="server" GroupName="Select"
                                            Checked="false" /></span> <span>
                                                <asp:DropDownList ID="ddlYear" runat="server" Width="73px">
                                                </asp:DropDownList>
                                            </span><span>
                                                <asp:Button ID="btnGo" runat="server" Text="Go" CausesValidation="false" OnClick="btnGo_Click" />
                                            </span>
                        </div>
                        <div style="width: 100%; overflow: auto; border: 2px solid #000; max-height: 400px;">
                            <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="UniqueID,UnderWriter_ID"
                                OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" CellPadding="20" CellSpacing="20"
                                RowStyle-BorderWidth="1" RowStyle-BorderStyle="Solid" Width="100%" OnRowDataBound="grdStyled_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="10px"
                                        ItemStyle-Height="10px" SelectText="<img src='../Common/Images/edit1.png' border='0' title='Raise Query'>"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Mail Log">
                                        <ItemTemplate>
                                            <a href="#" onclick="showdetail(<%# Container.DataItemIndex + 1 %>,'<%#Eval("To") %>',
                                                  '<%#Eval("From") %>','<%#Eval("CC") %>','<%#Eval("Subject") %>'
                                                  ,'<%#Eval("UnderWriter_ID") %>','<%#Eval("UniqueID") %>'
                                                  ,'<%#Eval("LOG_DATE_DESCRIPTIVE") %>' )">Correspondence</a>
                                            <div style="display: none;" id="dvdtl<%# Container.DataItemIndex + 1 %>">
                                                <%# System.Web.HttpUtility.HtmlEncode(Eval("Message_Dtl")) %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UniqueID" HeaderText="Request ID" ItemStyle-Width="120px" />
                                    <asp:BoundField DataField="DateRecieved" HeaderText="Date Recieved" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-Width="300px" />
                                    <asp:BoundField DataField="AccountName" HeaderText="Account Name" HeaderStyle-Width="500px"
                                        ItemStyle-Width="500px" HeaderStyle-Wrap="true" ItemStyle-Wrap="true" />
                                    <asp:BoundField DataField="NewRemodel" HeaderText="New/Remodel" ItemStyle-Width="200px"
                                        HeaderStyle-Wrap="true" />
                                    <asp:BoundField DataField="InternationalExposure" HeaderText="International Exposure"
                                        HeaderStyle-Wrap="true" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="NoofLocations" HeaderText="No of Locations" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" ItemStyle-Width="100px" />
                                    <asp:TemplateField HeaderText="SOV Files" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <%--  <ItemTemplate>
                                            <asp:ImageButton ID="imgbtn" CausesValidation="false" ImageUrl="~/Common/Images/search.gif"
                                                runat="server" Width="25" Height="25" OnClick="imgbtn_Click" />
                                        </ItemTemplate>--%>
                                        <ItemTemplate>
                                            <a href="#" onclick="ShowDocumentsInLarge('<%#Eval("AccountName") %>'  );">
                                                <img id="Img1" runat="server" src="~/Common/Images/search.gif" alt="Apply" height="21"
                                                    width="21" title="View Document"></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Platform" HeaderText="Platform" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="TIV" HeaderText="TIV" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="Underwriter" HeaderText="Underwriter" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="ProcessingStatus" HeaderText="Processing Status" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="Branchoffice" HeaderText="Branch Office" ItemStyle-Width="200px" />
                                    <asp:BoundField DataField="Submission" HeaderText="Submission" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="EMAILID" HeaderText="UW Email" ItemStyle-Width="100px" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No records found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <asp:Panel ID="pnlpopup" runat="server" Style="display: none; max-height: 550px; width: 500px; height: 550px; overflow: auto">
                            <asp:Panel ID="Panel4" runat="server" Style="cursor: move; background-color: blue; border: solid 1px maroon; color: white; width: 100%;">
                                <div style="cursor: move; background: white; border: solid 1px maroon; height: 100%; width: 100%;">
                                    <p>
                                        <asp:Label ID="lblAccName" runat="server" Text="" ForeColor="Black"></asp:Label>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblSOVError" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                                </div>
                            </asp:Panel>
                            <div style="background: lightblue; border: solid 1px maroon; height: 100%; width: 100%;">
                                <p style="text-align: right;">
                                    <asp:Button ID="Button2" runat="server" Text="Close" CausesValidation="false" />
                                </p>
                                <table style="margin-left: 25%">

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
                        </asp:Panel>

                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
                            PopupControlID="pnlpopup" BackgroundCssClass="modalBackground" OkControlID="OkButton"
                            OnOkScript="onOk()" DropShadow="true" PopupDragHandleControlID="Panel2" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdStyled" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script>

        var mousePosition;
        var offset = [0, 0];
        var div;
        var div1;
        var isDown = false;

        div = document.getElementById('customlightbox');
        div1 = document.getElementById('customlightboxHeader');


        div1.addEventListener('mousedown', function (e) {
            isDown = true;
            offset = [
                div.offsetLeft - e.clientX,
                div.offsetTop - e.clientY
            ];
        }, true);

        document.addEventListener('mouseup', function () {
            isDown = false;
        }, true);

        document.addEventListener('mousemove', function (event) {
            event.preventDefault();
            if (isDown) {
                mousePosition = {

                    x: event.clientX,
                    y: event.clientY

                };
                div.style.left = (mousePosition.x + offset[0]) + 'px';
                div.style.top = (mousePosition.y + offset[1]) + 'px';
            }
        }, true);


        function ShowDocumentsInLarge(Account) {
            //Change locathost to live url
            var imgSrc = 'http://localhost:50584/CMR/Operation/SOVTree.aspx?Acc=' + Account;
            window.open(imgSrc, '_blank', 'status=yes,toolbar=no,height=510,width=1050,menubar=no,location=no,resizable=no');
        }
    </script>
</asp:Content>
