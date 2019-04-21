<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLoanDetailViewmodel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeLoanDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Loan/Loan_details.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

     <form id="EmployeeLoanDetail" method="post" novalidate="novalidate">
        <div id="page-wrapper" class="col-lg-9">
            <div class="row">
                <div class="col-lg-12" style="margin-top:20px;">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">                           
                            
                           
                      
                                <span  style="text-align:left;"class="panel-title" > Assigned Loan Information </span>                          
                             <a style="color: #E6F1F3;float:right" target="_blank"  href="<%= Url.Content("~/Help/Payroll/Loan/EmployeeLoanDetail.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
           
                        
                        </div>
                        <div class="panel-body">
                            <div class="form-group" style="text-align: center;">
                                

                                <div class="col-md-1" >Loan Type</div>
                                <div class="col-md-2">
                                   
                                    <%: Html.DropDownListFor(model => model.LoanId, Model.Loanlist,  new { @class = "form-control" ,@id = "EmployeeId"})%>
                                      
                                </div>

                               <div class="col-md-1"    >Employee Name</div>
                                <div class="col-md-2">
                                   
                                    <%: Html.DropDownListFor(model => model.EmployeeId, Model.EmployeeList,  new { @class = "form-control" ,@id = "EmployeeId"})%>
                                     
                                </div>
                                


                                <div class="col-md-1">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnSearch" name="btnSearch" class="btn  btn-success" ><span class="glyphicon glyphicon-search"></span> Search</button>
                                         <div class="content"><img  style="display: none;"  id="loading"  src="<%= Url.Content("~/Images/loading.gif") %> " /></div>
                                </div>
                                 
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12">


                        <div>                   
                               
                            <div id="data" style="overflow:auto; "> 

                             
                         
                            </div>
                      
                     <div id="allthree">
                            
                   
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnExcel" class="btn  btn-success  cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnWord" class="btn  btn-success  cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                        </div>
                         
                         
                    </div>  
  <br />
                     <div id="LoanDetails" style="overflow:auto;margin-bottom:50PX;"> 
                         <div>
                      
                             </div>
                         </div>

                    </div>
                </div>
            </div>
        </div>
         
        <div >     
                    
           
          
        </div>
        </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>
    


</asp:Content>
