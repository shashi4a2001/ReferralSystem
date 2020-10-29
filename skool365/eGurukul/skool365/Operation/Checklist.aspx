<%@ Page Title="" Language="C#" MasterPageFile="~/Operation/CRMOperationArea2.Master" AutoEventWireup="true"
         CodeFile="Checklist.aspx.cs" Inherits="Operation_Checklist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 24px;
        }

        .style4
        {
            width: 150px;
            height: 10px;
        }
    </style>
 
    <script type="text/javascript">
        function setFocusToEntryTab(id) {
            //alert(document.getElementById(id));
            if (id == "EntryTab") {
                document.getElementById("basicform").style.display = 'block';
                document.getElementById("UserList").style.display = 'none';
            }
            if (id == "ListUser") {
                document.getElementById("UserList").style.display = "block";
                document.getElementById("basicform").style.display = "none";

            }
            //alert(id)
            //document.getElementById('EntryTab').click();

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

        function IsNumeric(val) {

            if (!(event.keyCode > 47 && event.keyCode < 58)) {
                alert("Enter Numerics Only")
                event.srcElement.focus();
                return false;
            }
        }
    </script>
    <style type="text/css">
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
    </style>
    <script src="../Common/JS/CommonFunction.js"   type ="text/javascript"></script>
 
    <div>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:Image ID="Image1" ImageUrl="~/Common/Images/waitTran.gif" AlternateText="Processing"
                    runat="server" Width="60px" Height="60px" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    </div>

    <div>
        <%--  <span class="pagedesc">Manage Users of the software</span>--%>
        <ul class="hornav">
            <li class="current"><a href="#basicform" id="EntryTab" onclick="setFocusToEntryTab('EntryTab')">Checklist Entry</a></li>
            <li class=""><a href="#UserList" id="ListUser" onclick="setFocusToEntryTab('ListUser')">Checklist Details</a></li>
        </ul>
    </div>
    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <div style="height: 5px;">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>

                    <table style="font-family: Verdana">
                        <tr>
                            <td class="style4">CheckList Name<span class="RequiredStarColor">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtChecklistName" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChecklistName"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style4">File Size<span class="RequiredStarColor">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileSize" runat="server" Width="500px" MaxLength="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFileSize"
                                    ErrorMessage="*" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                    ForeColor="red" ToolTip="in KB not greater than 1024 KB"></asp:RequiredFieldValidator>
                                <span class="Mandatory">Size (in KB not greater than 1024 KB)</span>
                            </td>
                        </tr>

                        <tr>
                            <td class="style4">
                                <span class="RequiredStarColor">*</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkReqForAdmn" runat="server" Text="Check if required as supporting document of Admission Procedure"></asp:CheckBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="style4">Extentions<span class="RequiredStarColor">*</span>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="ChecklistExtensions" runat="server" RepeatLayout="OrderedList"
                                    Width="432px">
                                    <asp:ListItem>doc</asp:ListItem>
                                    <asp:ListItem>docx</asp:ListItem>
                                    <asp:ListItem>jpg</asp:ListItem>
                                    <asp:ListItem>png</asp:ListItem>
                                    <asp:ListItem>pdf</asp:ListItem>
                                    <asp:ListItem>tiff</asp:ListItem>
                                    <asp:ListItem>bmp</asp:ListItem>
                                    <asp:ListItem>gif</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                    <p style="display: none;">
                        <label>
                            Status<span class="RequiredStarColor">*</span></label>
                        <span class="field">
                            <asp:DropDownList ID="dpdStatus" runat="server" Width="242px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Selected="True">Active</asp:ListItem>
                                <asp:ListItem>InActive</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dpdStatus"
                                ErrorMessage="Required" SetFocusOnError="True" CssClass="RequiredFieldValidatorColor"
                                InitialValue="--Select--" Enabled="false"></asp:RequiredFieldValidator>
                        </span>
                    </p>
                    <p>
                        <label>
                        </label>
                        <span style="margin-left: 200px">
                            <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btnblack" />
                            &nbsp;<asp:Button ID="btnReset" runat="server" Text="Cancel" OnClick="btnReset_Click"
                                CausesValidation="False" CssClass="btngray" />
                            <asp:HiddenField ID="hdId" runat="server" Value="0" />
                            <br />
                            <br />

                        </span>
                    </p>
                    <div id="dvMessage" runat="server">
                    </div>
                    <triggers>
                <asp:AsyncPostBackTrigger  ControlID="btnSubmit" EventName="Click" />
      </triggers>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="UserList" class="subcontent" style="display: none;">
            <div style="height: 5px;">
            </div>
            <div class="maingrid">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdStyled" runat="server" AutoGenerateColumns="False" DataKeyNames="CHECKLIST_ID"
                            OnSelectedIndexChanged="grdStyled_SelectedIndexChanged" OnRowDeleting="grdStyled_RowDeleting"
                            Width="80%" GridLines="None" OnRowDataBound="grdStyled_RowDataBound">
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ItemStyle-CssClass="customLnk" ItemStyle-Width="20px"
                                    ItemStyle-Height="20px" SelectText="<img src='../Common/Images/edit.png' border='0' title='Edit Record'>"></asp:CommandField>
                                <asp:BoundField DataField="CHECKLIST_NAME" HeaderText="CheckList Name" />
                                <asp:BoundField DataField="CHECKLIST_EXT" HeaderText="CheckList Supporting Extensions" />
                                <asp:BoundField DataField="FILE_SIZE" HeaderText="File Size (In KB)" />
                                <asp:BoundField DataField="REQ_FOR_ADMN" HeaderText="Required in Admission" />
                                <asp:BoundField DataField="ADDED_ON" HeaderText="Created On" />
                                <asp:BoundField DataField="ADDED_BY" HeaderText="Created By" />
                                <asp:BoundField DataField="MODIFIED_ON" HeaderText="Modified On" />
                                <asp:BoundField DataField="MODIFIED_By" HeaderText="Modified By" />
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="DeleteButton" CausesValidation="false" CommandName="Delete"
                                            ImageUrl="../Common/Images/delete.png" OnClientClick="if (!window.confirm('Are you sure you want to delete this record')) return false;"
                                            title='Delete Record' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgAudit" class="customLnk" Style="width: 15px; height: 15px;"
                                            src='Common/Images/search.gif' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No records found
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <triggers>
                <asp:AsyncPostBackTrigger  ControlID="grdUser" EventName="SelectedIndexChanged" />
      </triggers>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!--contentwrapper-->
</asp:Content>
