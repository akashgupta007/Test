<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.EmpAttendanceEntryViewModel>" %>

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeCenterPayrollDetail.js") %>"></script>
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
<form method="post" id="PayrollDetailsForm" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">

                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title">Employee Payroll Details</span>
                    </div>

                    <div class="panel-body" style="align-items: center;">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <%: Html.HiddenFor(model => model.EmployeeId)%>
                                Month                                                             
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
                            <button style="vertical-align: central; text-align: left;" type="submit" value="Search" id="btnSearch" name="command" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
                        </div>

                        </div>
                    </div>
                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>
                    <div id="dataExport" style="display: none;"></div>
                    <input type="hidden" id="flag" name="flag" />
                    <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
                    </div>
                    <br />
                    <button style="margin-bottom: 2px; margin-right: 2px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel">Excel</button>
                    <button style="margin-bottom: 2px; margin-right: 2px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word">Word</button>
                </div>
            </div>
        </div>
    </div>

    <div id="childDiv" style="display: none;">
        <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
        </div>
        <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
        </div>
        <div class="col-lg-12">
            <div id="childData">
            </div>
        </div>
    </div>

</form>

<div class="navbar navbar-inverse navbar-fixed-bottom">
    <div id="MsgDiv" style="display: none;">
        <label id="lblError"></label>
    </div>
</div>

