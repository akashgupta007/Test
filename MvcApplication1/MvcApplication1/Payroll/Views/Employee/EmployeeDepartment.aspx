<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpProjectViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeDepartment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeDepartment1.js") %>"></script>
    
             <link href="<%= Url.Content("~/Scripts/select2.css") %>" rel="stylesheet" />
    <script src="<%= Url.Content("~/Scripts/select2.js") %>"></script>
    <style>
        
          .select2-choice{
    width: 253px;
    margin-left: -15px;
    margin-top: -5px;
}
    </style>
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>
      <style>
        
          .select2-choice{
    width: 253px;
    margin-left: -15px;
    margin-top: -5px;
}
    </style>


    <form method="post" id="EmployeeDepartment" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-12">



                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Employee Department Details</h3>--%>
                            <span style="text-align: left;" class="panel-title">Employee Department</span>
                            <a style="color: #E6F1F3; float: right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeDepartment.html") %>  ">
                                <b>
                                    <img style="width: 30px; height: 20px; margin-top: -10px; padding-top: -10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                            </a>
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">

                                <div class="col-md-3">
                                    <input type='checkbox' class='checkbox-inline' id="IsHistory" name="IsHistory" />
                                    Show History
                                </div>
                                <div class="col-md-3">
                                    <button type="button" id="addDepartment" name="addDepartment" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>

                                </div>
                                <div class="col-md-3">

                                    <%--  <button type="button" id="ExportData"  name="ExportData" class="btn btn-info">Export To Excel</button>--%>
                                </div>
                            </div>
                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><span id="panelHeader"></span></h3>
                                </div>
                                <div class="panel-body ">
                                    <div id="divDisable">
                                        <div class="form-group">
                                            <div class="col-lg-3">
                                                Employee 
                                           <input type="hidden" id="getallDepartmentList" name="attendance_last_end_date" runat="server" />
                                                <input type="hidden" id="attendance_last_end_date" name="attendance_last_end_date" />
                                                <input type="hidden" id="salary_last_end_date" name="salary_last_end_date" />
                                                <input type="hidden" id="EmpProjectId" name="EmpProjectId" />
                                                <input type="hidden" id="EmployeeId2" name="EmployeeId2" />
                                                <input type="hidden" id="LeaveDate" name="LeaveDate" />
                                                <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                                <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                            </div>
                                            <div class="col-lg-3 ">
                                                Departments
                                       <input type="hidden" id="Department_Id" name="Department_Id" />
                                                <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                                <%: Html.ValidationMessageFor(model => model.DepartmentId)%>
                                            </div>


                                            <div class="col-lg-3 ">
                                                Designation
                                        <input type="hidden" id="Designation_Id" name="Designation_Id" />
                                                <%: Html.DropDownListFor(model => model.DesignationId ,Model.DesignationList, new {@class="form-control"})%>
                                                <%: Html.ValidationMessageFor(model => model.DesignationId)%>
                                            </div>
                                            <div class="col-lg-3 ">
                                                Location                                   
                                        <input type="hidden" id="Location_Id" name="Location_Id" />
                                                <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                                <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                            </div>
                                        </div>

                                        <div class="form-group">

                                            <div class="col-lg-3 ">
                                                Project                                    
                                        <input type="hidden" id="Project_Id" name="Project_Id" />
                                                <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                                <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                                            </div>
                                            <div class="col-lg-3 ">
                                                Start Date                                   
                                        <input type="hidden" id="StartDt2" name="StartDt2" />
                                                <%: Html.TextBoxFor(model => model.StartDt  , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select Start Date"})%>
                                                <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                            </div>
                                            <div class="col-lg-3 ">
                                                End Date
                                    <input type="hidden" id="EndDt2" name="EndDt2" />
                                                <%: Html.TextBoxFor(model => model.EndDt, new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select End Date"})%>
                                                <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                            </div>
                                            <div class="col-lg-3">
                                                <input type="hidden" class="form-control" id="a" name="a">
                                                <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success enabling"><span class="glyphicon glyphicon-picture"></span>Save</button>
                                                <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                                <button type="button" id="btnClose" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-12 ">
                                                <span id="spanDepartmentId" class="field-validation-error" data-valmsg-replace="true" style="color: #AF002A">Select Either Designation, Department or Location</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="data" style="overflow-x: auto; overflow-y: hidden">
                            </div>
                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnPdf" class="btn  btn-success enabling cancel" name="command" value="Pdf"><span class="glyphicon glyphicon-export"></span>Pdf</button>
                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span>Excel</button>
                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span>Word</button>
                            <div id="dataExport" style="display: none;">
                            </div>
                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
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
