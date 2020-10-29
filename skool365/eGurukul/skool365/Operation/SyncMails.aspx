<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncMails.aspx.cs" Inherits="Operation_SyncMails"
    MasterPageFile="~/Operation/CRMOperationAreaFooterLess.Master" ValidateRequest="false" %>

 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/UC_PageHeader.ascx" TagName="UC_PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
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

        function showdetail(cnt) {
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
        .modalPopup {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>

    <div style="position: fixed; z-index: 999; height: 75%; width: 75%; top: 20px; left: 20px; background-color: #e6f3f7; padding: 5px; border-color: #024e66; border-width: 2px; border-radius: 5px; display: none;"
        id="customlightbox">
        <span style="float: right; cursor: pointer;" title="Close" onclick="hidememsg()">
            <img src="../Common/images/icon_close.gif" />&nbsp;</span>
        <div style="width: 100%; cursor: move; background-color: #d1e7f9;" id='customlightboxHeader'>
        </div>
        <hr />
        <div style="height: 100%; width: 100%; background-color: #e6f3f7;">
            <span style="color: #050a11;">Message Trails </span>
            <div id="txtdescmsg" style="height: 70%; width: 100%; overflow: auto; background-color: #ced8e0;">
            </div>
        </div>
    </div>

    <div id="Div1" class="contentwrapper">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                     <img runat="server" src="../Common/Images/waitTran.gif"
                        style="position: static; width: 60px; height:60px;"> </img>
                    <%--<asp:Image ID="Image1" ImageUrl="../Common/Images/waitTran.gif" AlternateText="Processing"
                        runat="server" Width="80px" Height="80px" />--%>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
        </div>
    </div>
 
    <div id="basicform" class="subcontent" style="display: block;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Text="" ForeColor="Red"></asp:Label>
                <div>
                    <span><b style="font-size:14px; padding-left:7px;">Select Subject Header </b>
                        <asp:DropDownList ID="ddlSubject" runat="server"  >
                        </asp:DropDownList>

                         <asp:Button ID="btnSubmit" runat="server" Text="Mail LOG" OnClick="btnSubmit_Click"
                            Visible="true"  />
                    </span>
                </div>
                <div>
                    
                    <div style="  margin-top:3px">
                        <asp:GridView ID="grdStyled" Width="100%" runat="server" AutoGenerateColumns="false"
                            BackColor="White" BorderColor="#3366CC"  BorderWidth="1px"
                            CellPadding="4">   
                             <Columns>
    

                               <asp:TemplateField HeaderText="FROM">
                            <ItemTemplate> <asp:Label ID="lbl1" Text='<%# Bind("From") %>' runat="server"> </asp:Label>  </ItemTemplate>
                             </asp:TemplateField>

                                <asp:TemplateField HeaderText="SUBJECT">
                            <ItemTemplate> <asp:Label ID="lbl1" Text='<%# Bind("Subject") %>' runat="server"> </asp:Label>  </ItemTemplate>
                             </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Date Sent">
                            <ItemTemplate> <asp:Label ID="lbl1" Text='<%# Bind("DateSent") %>' runat="server"> </asp:Label>  </ItemTemplate>
                             </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="MESSAGE">
                            <ItemTemplate> 
                            <a href="#" onclick = "showdetail(<%# Container.DataItemIndex + 1 %>)">Correspondence</a>
                            <div style="display:none;" id="dvdtl<%# Container.DataItemIndex + 1 %>"> <%# System.Web.HttpUtility.HtmlEncode(Eval("Message")) %>  </div> 
                             </ItemTemplate>
                             </asp:TemplateField>
                             </Columns>

                          <%--  <Columns>
                                <asp:BoundField DataField="From" HeaderText="From" />
                                <asp:BoundField DataField="Message" HeaderText="Message" />
                                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                <asp:BoundField DataField="DateSent" HeaderText="Sent On" />
                            </Columns>--%>

                            <EmptyDataTemplate>
                                No records found
                            </EmptyDataTemplate>

                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#003399" BackColor="White" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </div>
                </div>
                <asp:HiddenField ID="hdOriginalMessage" runat="server" />
                <asp:HiddenField ID="hdRequestID" runat="server" />
                <asp:HiddenField ID="hdReplyTo" runat="server" />  
                <asp:HiddenField ID="hdReplyFrom" runat="server" />
                <asp:HiddenField ID="hdReplyDate" runat="server" />
                <asp:HiddenField ID="hdSubject" runat="server" />
                <asp:HiddenField ID="hdMessage" runat="server" />
            </ContentTemplate>

            <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
        </asp:UpdatePanel>
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
    </script>

</asp:Content>

