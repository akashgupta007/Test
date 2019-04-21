<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Models.PayrollReportViewModel>" %>




<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>

 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeCenterSalaryPaid.js") %>"></script>  

<form method="post" id="EmployeeSalaryPaid" novalidate="novalidate" >
    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">
                    <div class="panel-heading">
                        <%--<h3 class="panel-title">Employee Details</h3>--%>
                        <span style="text-align: left;" class="panel-title"><span id="panelHeader">Employee Salary Report</span></span>
                                                    <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Report/SalaryReport.html") %> ">
                                                        <b>
                                                            <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a></span>
                    </div>

                    <div class="panel-body" style="align-items: center;">
                      <%--  <%:Html.Partial("PayrollDropDownList",new PoiseERP.Areas.Payroll.Models.PayrollUtil()) %>--%>


                         <div class="col-lg-12">
  
      

    
                                         
        <div class="col-lg-2">
                                    Month  
                             
                                      <%: Html.HiddenFor(model => model.EmployeeId)%>
                                    <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.MonthId)%>
                                </div>
                                          <div class="col-lg-2">
                                    Year  
                               
                                
                                    <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.Year)%>
                                </div>


                              <div class="col-lg-2">
                                   
                                  <br />
                                  <button style="vertical-align:central; text-align:left; " type="button" value="Search" id="btnSearch" name="command" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>           
                                 <button type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"><span class="glyphicon glyphicon-export"></span> Export To Excel</button>
                                   <%-- <input type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"/>--%>

                                </div>

                             </div>


                        <div>

                              <button  style="margin-top:15px;margin-right:15px;display:none;"  type="button" id="btnSalary" class="btn  btn-success"  >Salary Summary with Late Entry Details</button>
                          
                        </div>
                    </div>

                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>

                    <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
                    </div>
                    <br />     
                  
                    <button  style="margin-bottom:2px;margin-right:1px;"  type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                    <button  style="margin-bottom:2px;margin-right:1px;"  type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>            
                                  
                </div>
            </div>
            <div class="col-lg-12">
                <div></div>
                <div id="LoadReport" style="overflow: auto; display:none;">
                    <iframe id="ReportFrame" style="border: none; width:1800px;height:1000px;"></iframe>
                </div>
            </div>
        </div>
    </div>
  
</form>
    <div class="navbar navbar-inverse navbar-fixed-bottom">
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    </div>