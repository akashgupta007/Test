<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeParialViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeCenter
</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeCenter.js") %>"></script>

    

    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">

            <div class="col-lg-3">

                <div id="DetailPanel1" class="panel panel-primary">
                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title">Employee List</span>
                        <%--<input type="hidden" id="hfEmployeeId1" name="hfEmployeeId1" />--%>
                    </div>
                    <div class="panel-body ">
                        <div class="form-group" style="margin-left: 10px;">
                            <div class="col-md-12">
                                <%: Html.HiddenFor(model => model.Employee__Id)%>
                                <div id="divEmployeeDetail" style="overflow: auto; height: 600px;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-9">

                <div id="DetailPanel" class="panel panel-primary">
                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title" id="spnEmployeeName"></span><span style="display:none">Employee Id -</span><span style="text-align: right;display:none" class="panel-title" id="spnEmpId"></span>
                        <input type="hidden" id="hfEmployeeId" name="hfEmployeeId" />
                    </div>
                    <div class="panel-body ">
                        <div class="form-group" id="divEmpCenter">
                            <div class="col-md-12">

                                <button type="button" id="btnEmployee" name="btnEmployee" class="btn btn-primary btn-xs">Employee</button>
                                <button type="button" id="btnDepartment" name="btnDepartment" class="btn btn-primary btn-xs">Department</button>
                                <button type="button" id="btnSalary" name="btnSalary" class="btn btn-primary btn-xs">Salary</button>
                                <button type="button" id="btnSalaryGroup" name="btnSalaryGroup" class="btn btn-primary btn-xs">Group Salary</button>
                                <button type="button" id="btnAttendanceEntry" name="btnAttendanceEntry" class="btn btn-primary btn-xs">Attendance Entry</button>
                                <button type="button" id="btnAttendanceApproval" name="btnAttendanceApproval" class="btn btn-primary btn-xs">Attendance Approval</button>
                                <button type="button" id="btnPayrollProcess" name="btnPayrollProcess" class="btn btn-primary btn-xs">Payroll Process</button>
                                <button type="button" id="btnPayrollDetail" name="btnPayrollDetail" class="btn btn-primary btn-xs">Payroll Detail</button>
                                <button type="button" id="btnPFDetail" name="btnPFDetail" class="btn btn-primary btn-xs">PF Detail</button>
                                <button type="button" id="btnEsiDetail" name="btnEsiDetail" class="btn btn-primary btn-xs">ESI Detail</button>
                                <button type="button" id="btnPayslipDetail" name="btnPayslipDetail" class="btn btn-primary btn-xs">Payslip</button>
                                <button type="button" id="btnSalaryPaid" name="btnSalaryPaid" class="btn btn-primary btn-xs">Salary Paid</button>
                                <%--<button type="button" id="btnModalPopup" name="btnModalPopup" class="btn btn-danger btn-xs">Show Modal Popup</button>--%>
                            </div>
                        </div>
                        <div id="pvData" style="overflow: auto; height: 600px;">
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="divOpen" style="display:none;">
        <div class="row">
            <div class="col-lg-8">
                <div class="form-group">
                    <textarea class="form-control" style="max-width: 450px;" rows="8"></textarea>
                </div>
                <div class="form-group">
                    <input type="file" class="form-control" id="asd" />
                    <input type="hidden" id="hfFileName" name="hfFileName" />
                </div>
                <div class="form-group">
                    <button type="button" id="btnAttach" name="btnAttach" class="btn btn-info">Attach</button>
                </div>
                <button type="button" id="btnAddNotes" name="btnAddNotes" class="btn btn-info">Add Notes</button>

            </div>

            <div class="col-lg-4">
                <div class="form-group">
                    <textarea class="form-control" rows="8" id="txtFileNames"></textarea>
                    <img id="img" src="" class="img-responsive img-circle" />
                    <div id="base"></div>
                </div>
                <div class="form-group">
                    <button type="button" id="btnDelete" name="btnDelete" class="btn btn-danger">Delete</button>
                    <a id="aDownload" download="">Download File</a>
                    <input type="hidden" id="base64" name="base64" />
                </div>
            </div>
        </div>
    </div>


    <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
            <div class="content">                           
                <img id="LoadingProgress" src="<%= Url.Content("~/Images/pleasewait.gif") %> " />
            </div>
        </div>
    </div>

   <%--  --%>

</asp:Content>
