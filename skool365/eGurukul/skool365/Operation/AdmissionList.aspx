<%@ Page Title="" Language="C#"  MasterPageFile="~/Operation/CRMOperationArea2.Master" AutoEventWireup="true" CodeFile ="AdmissionList.aspx.cs" Inherits="Operation_AdmissionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" runat="server">
    <style type="text/css">
        .BorderNone
        {
            border: none !important;
        }
        .BorderNone td
        {
            border: none !important;
        }
        
        .tblHeaderTD
        {
            padding: 5px 5px 2px 5px !important;
            border-bottom: 1px solid gray;
            font-weight: bold;
            float: left;
        }
        .tblContentTD
        {
            padding: 5px 5px 2px 5px !important;
            border-bottom: 1px solid gray;
            float: left;
        }
        .tblSeparator
        {
            float: left;
            margin: 0px 8px;
        }
        .MainDivStyle
        {
            border-top: 3px solid #e1885a;
            border-bottom: 2px dotted #f6c2ae;
            border-left: 4px solid #e1885a;
            border-right: 2px dotted #f6c2ae;
            padding: 5px 5px 2px 5px;
        }
        
        .SpecialHeaderColor
        {
            color: #ba3406;
            font-size: 12px;
        }
    </style>
    
    <div id="contentwrapper" class="contentwrapper">
        <div id="basicform" class="subcontent" style="display: block;">
            <div style="padding: 20px; border: 2px solid gray">
                <span><b>Search Panel : </b></span><span>
                    <asp:TextBox ID="txtSearchString" runat="server" Width="300px" MaxLength="200"></asp:TextBox>
                </span><span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnblack" OnClick="btnSearch_Click" />
                </span><span></span><span style="color: Red; font-size: 14px;">
                    <asp:Literal ID="ltrlMessage" runat="server"></asp:Literal></span>
            </div>
        </div>
    </div>
    <div>
        <asp:GridView ID="grdRegisteredCandidates" runat="server" AutoGenerateColumns="False"
            CssClass="mGrid" DataKeyNames="RegistrationNumber" Width="100%" OnRowCommand="grdRegisteredCandidates_RowCommand"
            OnRowDataBound="grdRegisteredCandidates_RowDataBound" SkinID="0" ShowHeader="false">
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="MainDivStyle">
                            <div>
                                <span class="tblHeaderTD SpecialHeaderColor">Reg. No : </span><span class="tblContentTD SpecialHeaderColor">
                                    <b>
                                        <asp:Literal ID="lblRegistrationNumber" runat="server" Text='<%# Bind("RegistrationNumber") %>'></asp:Literal>
                                    </b></span><span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD SpecialHeaderColor">
                                        Name :</span> <span class="tblContentTD SpecialHeaderColor" style="text-transform: uppercase;">
                                            <b>
                                                <asp:Literal ID="lblStateNmae" runat="server" Text='<%# Bind("StudentName") %>'></asp:Literal></b></span>
                                <span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">DOB : </span>
                                <span class="tblContentTD">
                                    <asp:Literal ID="lblDOB" runat="server" Text='<%# Bind("DateOfBirth") %>'></asp:Literal></span>
                                <span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Course : </span>
                                <span class="tblContentTD">
                                    <asp:Literal ID="lblCourse" runat="server" Text='<%# Bind("CourseName") %>'></asp:Literal>
                                </span><span class="tblSeparator">&nbsp;</span> <span style="float: right !important">
                                    <span class="tblHeaderTD">Is Confirmed : </span><span class="tblContentTD">
                                        <asp:Literal ID="lblConfirm" runat="server" Text='<%# Eval("IsConfirmRegistration").ToString().Equals("True") ? "Confirmed" : "Pending" %>'></asp:Literal>
                                    </span></span>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div>
                                <span class="tblHeaderTD">Academic Session : </span><span class="tblContentTD">
                                    <asp:Literal ID="lblSession" runat="server" Text='<%# Bind("AcedemicYear") %>'></asp:Literal>
                                </span><span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Email :
                                </span><span class="tblContentTD">
                                    <asp:Literal ID="lblStateID" runat="server" Text='<%# Bind("Email") %>'></asp:Literal>
                                </span><span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Father :
                                </span><span class="tblContentTD">
                                    <asp:Literal ID="lblCenterAddress" runat="server" Text='<%# Bind("FatherName")  %>'></asp:Literal></span>
                                <span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Mother : </span>
                                <span class="tblContentTD">
                                    <asp:Literal ID="lblMotherName" runat="server" Text='<%# Bind("MotherName")  %>'></asp:Literal></span>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="clear: both">
                            </div>
                            <div>
                                <span class="tblHeaderTD">Entry By : </span><span class="tblContentTD">
                                    <asp:Literal ID="lblEntryBy" runat="server" Text='<%# Bind("INSERTED_BY") %>'></asp:Literal>
                                </span><span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Entry On :
                                </span><span class="tblContentTD">
                                    <asp:Literal ID="lblEntryOn" runat="server" Text='<%# Bind("INSERTED_ON") %>'></asp:Literal>
                                </span><span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Modified By
                                    : </span><span class="tblContentTD">
                                        <asp:Literal ID="lblModifyBy" runat="server" Text='<%# Bind("MODIFIED_BY")  %>'></asp:Literal></span>
                                <span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Modified On :
                                </span><span class="tblContentTD">
                                    <asp:Literal ID="lblModifyOn" runat="server" Text='<%# Bind("MODIFIED_ON")  %>'></asp:Literal></span>
                                <span class="tblSeparator">&nbsp;</span> <span class="tblHeaderTD">Study Center :
                                </span><span class="tblContentTD">
                                    <asp:Literal ID="lblStudyCenter" runat="server" Text='<%# Bind("Study_Center")  %>'></asp:Literal></span>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="margin-top: 5px;">
                                <span>
                                    <asp:ImageButton runat="server" ID="EditButton" CausesValidation="false" CommandName="EditRecord"
                                        CommandArgument='<%# Eval("RegistrationNumber") %>' ImageUrl="~/Common/Images/stage_edit.png"
                                        title='Edit Record' />
                                </span><span style="position: relative; top: -7px;"><b>Stages :</b></span> <span>
                                    <asp:ImageButton runat="server" ID="Stag2Button" CausesValidation="false" CommandName="Stage2"
                                        CommandArgument='<%# Eval("RegistrationNumber") %>' ImageUrl="~/Common/Images/stage_1.png" />
                                </span><span>
                                    <asp:ImageButton runat="server" ID="Stag3Button" CausesValidation="false" CommandName="Stage3"
                                        CommandArgument='<%# Eval("RegistrationNumber") %>' ImageUrl="~/Common/Images/stage_2.png" />
                                </span><span>
                                    <asp:ImageButton runat="server" ID="Stag4Button" CausesValidation="false" CommandName="Stage4"
                                        CommandArgument='<%# Eval("RegistrationNumber") %>' ImageUrl="~/Common/Images/stage_3.png" />
                                </span><span>
                                    <asp:ImageButton runat="server" ID="Stag5Button" CausesValidation="false" CommandName="Stage5"
                                        CommandArgument='<%# Eval("RegistrationNumber") %>' ImageUrl="~/Common/Images/stage_4.png" />
                                </span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No records found
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
