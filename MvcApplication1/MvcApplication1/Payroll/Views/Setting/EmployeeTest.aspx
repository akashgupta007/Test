<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeParialViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeCenter
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/EmployeeTest1.js") %>"></script>

    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">
                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title">Employee Center</span>
                        <input type="hidden" id="hfEmployeeId" name="hfEmployeeId" />
                    </div>
                    <div class="panel-body ">
                        <div class="form-group" style="margin-left: 10px;">
                            <div class="col-md-12">
                                 <%: Html.DropDownListFor(model => model.Employee__Id ,Model.EmployeeList   , new {@class="form-control"})%>
                                 <%: Html.ValidationMessageFor(model => model.EmployeeList ) %>
                                
                            </div>
                        </div>
                        <div class="form-group" style="margin-left: 10px; display:none;" id="divEmpCenter">
                            <div class="col-md-12">
                                <button type="button" id="newEntity" name="newEntity" class="btn btn-info">Add New </button>
                                <button type="button" id="btnEmployee" name="btnEmployee" class="btn btn-info">Employee</button>
                                <button type="button" id="Department_tab" name="Department_tab" class="btn btn-info">Department</button>
                                <button type="button" id="salary_tab" name="salary_tab" class="btn btn-info">Salary</button>
                                <button type="button" id="Salary_Paid_tab" name="Salary_Paid_tab" class="btn btn-info">Salary Paid</button>

                                <button type="button" id="btnAttendanceApproval" name="btnAttendanceApproval" class="btn btn-info">Attendance Approval</button>
                                <button type="button" id="btnAttendanceEntry" name="btnAttendanceEntry" class="btn btn-info">Attendance Entry</button>
                                <button type="button" id="btnPayrollProcess" name="btnPayrollProcess" class="btn btn-info">Payroll Process</button>
                                <button type="button" id="btnSalaryPaid" name="btnSalaryPaid" class="btn btn-info">Salary Paid</button>


                                <button type="button" id="btnPayslip" name="btnPayslip" class="btn btn-info">Pay Slip</button>


                                <button type="button" id="PF_tab" name="PF_tab" class="btn btn-info">PF Detail</button>
                                <button type="button" id="ESI_tab" name="ESI_tab" class="btn btn-info">ESI Detail</button>
                            </div>
                        </div>
                        <div id="pvData" style="overflow: auto;" >



                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>
