<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.EmpAttendanceEntryViewModel>" %>

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeCenterAttendanceApproval.js") %>"></script>
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
<%   
    AjaxOptions ajaxOptions = new AjaxOptions
    {
        UpdateTargetId = "data",
        InsertionMode = InsertionMode.Replace,
        HttpMethod = "POST",
        LoadingElementId = "loading",
    };   
%>
<% Ajax.BeginForm("AttendanceApproval", ajaxOptions); %>

<div class="form-horizontal" style="margin-top: 10px;">
    <div class="row">
        <div class="col-lg-12">
            <div id="DetailPanel" class="panel panel-primary">

                <div class="panel-heading">
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
            </div>
        </div>
    </div>
</div>
<%Html.EndForm(); %>
<div class="navbar navbar-inverse navbar-fixed-bottom">
    <div id="MsgDiv" style="display: none;">
        <label id="lblError"></label>
    </div>
</div>
