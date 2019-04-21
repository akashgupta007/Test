<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLoanDetailViewmodel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LoanRecoveryDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Loan/LoanRecoveryDetail1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 
  


    
    <form id="LoanRecoveryDetail" method="post" novalidate="novalidate">
  
        <div id="page-wrapper" class="col-lg-9">
            <div class="row">
                <div class="col-lg-12" style="margin-top:20px;">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                       
                             <span  style="text-align:left;"class="panel-title" > Loan Recovery Detail   </span>
                 
                                      
                            <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Loan/LoanRecoveryDetail.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
           
                        </div>
                        <div class="panel-body">
                            <div class="form-group" style="text-align: center;">
                                <div class="col-md-1" >Loan Type</div>
                                <div class="col-md-2">
                                   
                                    <%: Html.DropDownListFor(model => model.LoanId , Model.Loanlist , new {@class="form-control"})%>
                                  
                                </div>

                                <div class="col-md-1" > From Date</div>
                                <div class="col-md-2">
                                   
                                     <%: Html.TextBoxFor(model => model.FromDate  , new { @class = "form-control DBPicker ",  @readonly="readonly", placeholder = "Select From Date" })%>
                                  
                                </div>

                                <div class="col-md-1" > To Date</div>
                                <div class="col-md-2">         
                                    
                                
                                   <%: Html.TextBoxFor(model => model.ToDate  , new { @class = "form-control DBPicker ",  @readonly="readonly", placeholder = "Select To Date" })%>
                                   
                               
                                </div>
                                               


                                <div class="col-md-1">
                               <button style="margin-bottom: 2px; margin-right: 1px;" type="button"id="btnSearch" name="btnSearch" class="btn  btn-success enabling" ><span class="glyphicon glyphicon-search"></span> Search</button>
                                   
                                </div>
                               
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12">


                        <div></div>                     
                               
                            <div id="data" style="overflow:auto; ">                         
                        

                            </div>
                        <div id="allthree">
                             <div class="form-group" style="margin-left: 10px;  margin-bottom: 10px; margin-top:10px;">
                          <button style="margin-bottom: 2px; margin-left:10px; margin-right: 1px;" type="submit" id="btnPdf" class="btn  btn-success enabling cancel" name="command" value="Pdf"><span class="glyphicon glyphicon-export"></span> Pdf</button>
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> PdfExcel</button>
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> PdfWord</button>
                        </div>
                            </div>

                     
                    
                  
                     </div>
                </div>
            </div>
        </div>
         
        <div >     
                    
                <div class="content"><img  style="display: none;"  id="loading"  src="<%= Url.Content("~/Images/loading.gif") %> " /></div>
          
        </div>


         </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>
    



</asp:Content>
