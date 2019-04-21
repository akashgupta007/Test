<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLoanViewmodel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeLoan
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Loan/EmployeeLoan.js") %>"></script>

  

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

  
 <form id="EmployeeLoan" novalidate="novalidate">

    
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            
                                                            
                            <span  style="text-align:left;"class="panel-title" >Create Employee Loan  </span>                          
 <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Loan/EmployeeLoan.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                        
                        </div>
                        <div class="panel-body ">


                            <div class="form-group">
                                <div class="col-md-2">
                                 <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Employee Loan</button>
                                </div>

                            </div>
                                 
                             
                            <div id="data" style="overflow:auto; "> 
                            </div>
                            
                        </div>

                        <div style="text-align:center;margin-bottom:5px; ">

                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                       
                    </div>
                </div>

                


                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span> </h3>
                        </div>
                        <div  class="panel-body "> 


                            <div id="LoanDetail"  style="overflow:auto;"  " >
                                     </div>

                              <div class="form-group">
                                <div class="col-lg-3 ">
                               Loan Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="EmpLoanId" name="EmpLoanId" />
                                    <%: Html.DropDownListFor(model => model.LoanId ,Model.Loanlist , new {@class="form-control", @id="LoanId"})%>
                                      <%: Html.ValidationMessageFor(model => model.LoanId)%>
                                </div>
                            </div> 


                               <div class="form-group">
                                <div class="col-lg-3 ">
                                Employee Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList  , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>  

                           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Loan Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                  <%: Html.TextBoxFor(model => model.LoanDate  , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Loan Date " })%>
                                   <%: Html.ValidationMessageFor(model => model.LoanDate ) %>
                                </div>
                            </div>                             
                           
                                   


                              <div class="form-group">
                                <div class="col-lg-3 ">
                               Loan Amount
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    
                                    <%: Html.TextBoxFor(model => model.LoanAmount , new {@class="form-control", @maxlength="14", placeholder="Enter  Loan Amount"})%>
                                    <%: Html.ValidationMessageFor(model => model.LoanAmount)%>
                                    <%: Html.Label("lblmessge", new {style="display:none"})%>
                                </div>
                            </div>
                           
                              

     

      

        


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                   Loan Term
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  <input type="hidden" id="hfLoanTerm"/>
                                    <%: Html.TextBoxFor(model => model.LoanTerm , new {@class="form-control",@readonly="readonly", placeholder="Enter   Loan Term "})%>
                                    <%: Html.ValidationMessageFor(model => model.LoanTerm)%>
                                </div>
                            </div>        


                           
                              <div class="form-group">
                                <div class="col-lg-3 ">
                                 Holiday Months
                                </div>
                                  <div class="col-lg-3">
                                   Year   
                                     <input type="hidden" id="ComputingItemYearList" name="ComputingItemYearList" />
                                        <select name="ComputingItemId1" id="ComputingItemYearId" class="form-control" multiple="multiple">
                                            <option>--Select--</option>
                                        </select>                             
                              
                                </div>
                                   <div class="col-lg-3" style="display:none" id="monthddl">
                                    Month
                                       <input type="hidden" id="ComputingItemMonthList" name="ComputingItemMonthList" />
                                        <select name="ComputingItemId" id="ComputingItemMonthId" class="form-control" multiple="multiple">
                                            <option>--Select--</option>
                                        </select>                                       
                                </div>
                                 
                                
                            </div>   
                                   
                     

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                Add Principal Per Month
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                 <%: Html.TextBoxFor(model => model.AddlPrincipalPerMonth   , new { @class = "form-control ", placeholder = "Enter  Add Principal Per Month " })%>
                                    <%: Html.ValidationMessageFor(model => model.AddlPrincipalPerMonth     ) %>
                                </div>
                            </div>                          
                                     <div class="form-group">
                                <div class="col-lg-3 ">
                              Expense Per Month
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.ExpensePerMonth   , new { @class = "form-control ", placeholder = "Enter Expense Per Month" })%>
                                    <%: Html.ValidationMessageFor(model => model.ExpensePerMonth     ) %>
                                </div>
                            </div>             
                                                      
                                                  

                              <div class="form-group">
                                <div class="col-lg-3 ">
                               Notes
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.Notes   , new { @class = "form-control ", placeholder = "Enter Notes" })%>
                                    <%: Html.ValidationMessageFor(model => model.Notes     ) %>
                                </div>
                            </div>
                           
                           
                            
                              <div class="form-group">
                                <div class="col-lg-3 ">
                               Holiday Period
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.HolidayPeriod   , new { @class = "form-control ", placeholder = "Enter Holiday Period" })%>
                                    <%: Html.ValidationMessageFor(model => model.HolidayPeriod     ) %>
                                </div>
                            </div>
                           
                                      
                           
                           
                           
                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                                </div>

                            </div>
                              <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>


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
