<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Models.PayrollReportViewModel>" %>

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeCenterEsiDetail.js") %>"></script> 
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
<form method="post" id="EmployeePFDetail" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Employee ESI Details</h3>
                    </div>

                    <div class="panel-body" style="align-items: center;">
                        <div class="col-lg-12">
                        <div class="col-lg-2">
                            <%: Html.HiddenFor(model => model.EmployeeId)%>
                            Month  
                            <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control" })%>
                            <%: Html.ValidationMessageFor(model => model.MonthId)%>
                        </div>

                        <div class="col-lg-2">
                            Year  
                            <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.Year)%>
                        </div>

                        <div class="col-lg-2">
                            <br />
                            <button style="vertical-align: central; text-align: left;" type="submit" value="Search" id="btnSearch" name="command" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
                        </div>
                    </div>
                    </div>

                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>

                    <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
                    </div>  

                    <br />
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



