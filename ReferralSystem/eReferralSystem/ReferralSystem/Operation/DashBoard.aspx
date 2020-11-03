<%@ Page Language="C#" MasterPageFile="~/Operation/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Operation_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MiddleContent" Runat="Server">
    <style>
        .col-md-2 {padding:7px 15px !important;}
        #form1 table tr td {
            padding: 10px !important;
        }
    </style>

         <div style="position: fixed; top: 8em; z-index: 999999; right: 33px;">
                    <img src="../Common/images/shortcut/dashboardwatermark.png" alt="Access Panel" />
             
          </div>
       
         <div class="row">
             <div class="col-md-11">
                 <div class="card card-table">
                     <div class="card-header">
            <asp:GridView ID="grdSummary" runat="server" CssClass="table table-striped"></asp:GridView>
             <div class="text-right"><a href="ReportAccountOpen.aspx">More..</a></div>
                     </div>
                     </div>
                 </div>
         </div>
    <div style="clear:both"></div>
        <div class="row">
             <div class="col-md-11">
                 <div class="card card-table">
                     <div class="card-header">
                         <div class="title">Account Detail</div>

                         <div class="row" style="font-size:13px; padding:4px 0">
                             <div class="col-md-2 strong">Mobile No.</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlMobileNo" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">LoginId</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlLoginId" runat="server"></asp:Literal></div>
                          <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                             
                             <div class="col-md-2 strong">Client Type</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlClientType" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">Client Code</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlClientCode" runat="server"></asp:Literal></div>
                            <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                              
                             <div class="col-md-2 strong">Client Name</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlClientName" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">Contact Person</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlContactPerson" runat="server"></asp:Literal></div>
                             <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                             
                             <div class="col-md-2 strong">Email Id</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlEmailId" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">Address</div>
                             <div class="col-md-4"> <asp:Literal ID="ltrlAddress" runat="server"></asp:Literal></div>
                          <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                              
                             <div class="col-md-2 strong">Landline No.</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlLandlineNo" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">Bank Name</div>
                             <div class="col-md-4"> <asp:Literal ID="ltrlBankName" runat="server"></asp:Literal></div>
                             <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                             
                             <div class="col-md-2 strong">Client Name As Per Bank</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlClientNameAsperBank" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">Account No.</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlAccountNo" runat="server"></asp:Literal></div>
                              <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                             
                             <div class="col-md-2 strong">IFSC Code</div>
                             <div class="col-md-4">  <asp:Literal ID="ltrlIFSCCode" runat="server"></asp:Literal></div>
                             <div class="col-md-2 strong">Sharing (%)</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlSharingPercnt" runat="server"></asp:Literal></div>
                              <div style="clear:both;border-bottom: 1px solid #f9f9f9;"></div>
                             
                             <div class="col-md-2 strong">Referred Referral Code</div>
                             <div class="col-md-4"><asp:Literal ID="ltrlReferredReferralCode" runat="server"></asp:Literal></div>
                             
                             
                         </div>

                         </div>
                     </div>
              </div>
            </div>
        
</asp:Content>
 