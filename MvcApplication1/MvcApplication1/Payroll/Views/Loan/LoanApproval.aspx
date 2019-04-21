<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLoanViewmodel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LoanApproval
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Loan/LoanApproval1.js")%>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 


  <form id="LoanApproval" novalidate="novalidate">

    
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                          
                                                              
                            <span  style="text-align:left;"class="panel-title" > Approve Loan  </span>
                           
                              <a style="color: #E6F1F3;float:right" target="_blank"href="<%= Url.Content("~/Help/Payroll/Loan/EmployeeLoanApproval.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                
                        </div>
                      

                           <div class="panel-body ">


                           <div class="form-group">
                                <div class="col-lg-2 ">
                                Loan  Type
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       <input type="hidden" id="EmpLoanId" name="EmpLoanId" />
                                      <%: Html.DropDownListFor(model => model.Is_Approved, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Approval", Value="Approval"},
                                                                     new SelectListItem{Text="Approved", Value="Approved"},
                                                                       new SelectListItem{Text="Disapprove", Value="DisApprove"},}, "--Select--", new { @class = "form-control", @id = "Is_Approved" })%>
                                                                <%: Html.ValidationMessageFor(model => model.Is_Approved)%>                                                  

                                </div>
                            </div> 
                                 
                             
                            <div id="data" style="overflow:auto; "> 
                            </div>
                            
                        </div>

                        <div style="text-align:center;margin-bottom:5px; ">

                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif")%> " />
                        </div>
                       
                    </div>
                </div>

                



        

            </div>

         
        </div>
    </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>

</asp:Content>
