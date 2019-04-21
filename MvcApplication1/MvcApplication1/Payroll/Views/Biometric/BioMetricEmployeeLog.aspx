<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.BiometricViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BioMetric Log Report
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Biometric/BioMetricEmployeeLog.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>


    <%--   <%    
        AjaxOptions ajaxOptions = new AjaxOptions
        {
            UpdateTargetId = "data",
            InsertionMode = InsertionMode.Replace,
            HttpMethod = "POST",
            LoadingElementId = "loading",
            OnSuccess = "OnSuccess",
            OnFailure = "OnFailure"
        };  
    %>
    <% Ajax.BeginForm(ajaxOptions); %>--%>
    <form id="BioMetricEmployeeLogId" method="post">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-xs-12">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Biometric Log Details  </span>

                            <a style="color: #E6F1F3; float: right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Biometric/BioMetricEmployeeLog1.html")  %>  ">
                                <b>
                                    <img style="width: 30px; height: 20px; margin-top: -10px; padding-top: -10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                            </a>

                        </div>

                        <div class="panel-body" style="align-items: center;">

                            <div class="col-lg-12">
                                <div class="col-lg-2">
                                    Department   
        <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DepartmentId)%>
                                </div>

                                <div class="col-lg-2">
                                    Location    
        <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                </div>

                                <div class="col-lg-2">
                                    Designation 
        <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DesginationId)%>
                                </div>

                                <div class="col-lg-2">
                                    Project  
        <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                                </div>

                                <div class="col-lg-2">
                                    Employee Type  
        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                                </div>
                                <div class="col-lg-2">
                                    Shift   
        <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ShiftId)%>
                                </div>

                                <div class="col-lg-1">
                                </div>

                            </div>

                            <div class="col-lg-12" style="margin-top: 5px;">

                                <div class="col-lg-2">
                                    Employee   
        <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                    <%--<div  id="EmployeeId" ></div>--%>
                                </div>


                                <div class="col-lg-2 date">
                                    Start Date  
        <%: Html.TextBoxFor(model => model.StartDate , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Start Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.StartDate)%>
                                </div>

                                <div class="col-lg-2 date">
                                    End Date 
        <%: Html.TextBoxFor(model => model.EndDate , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select End Date"})%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                </div>
                                <%-- <div class="col-lg-1" >
            <br />
                           <div class="form-group"> 
                                
                              <input type='checkbox' class='checkbox-inline' id="IsImportAttendance"  name="IsImportAttendance"/>Import Attendance
                                 
                                 </div>  
        </div>


      <div class="col-lg-1">
            <br />
                           <div class="form-group"> 
                                
                         
                                   <input type='checkbox' class='checkbox-inline' id="IsCalAttendance"  name="IsCalAttendance"/>Calculate Attendance
                                 </div>  
        </div>--%>
                            </div>
                            <br />
                            <div class="col-lg-12">
                                 <br />
                                <div class="col-lg-2">
                                    <button type="button" value="Search" id="btnSearch" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span>Search</button>
                                </div>
                               
                                <div class="col-lg-2">
                                    <button type="button" value="Search" id="btnDailyAttendance" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Daily Attendance</button>
                                </div>
                              
                                <div class="col-lg-2">
                                    <button type="button" value="Attendance Register" id="btnShiftAttendance" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Attendance Register</button>
                                    <%--<input type="button"  style="vertical-align: central; text-align: left; display:none;"  value="Attendance Register" id="btnShiftAttendance" class="btn  btn-success" />--%>
                                </div>
                               

                                <div class="col-lg-2">
                                    <button type="button" value="Absent Report" id="btnAbsent" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Absent Report</button>
                                </div>
                               
                                <div class="col-lg-2">
                                    <button type="button" value="UnAutorized absent" id="btnUnautorizedAbsent" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Unautorized Absent Report</button>
                                </div>

                                
                                <div class="col-lg-2">
                                    <button type="button" value="Present" id="btnPresent" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Present Report</button>
                                </div>
                            </div>
                              <br />
                            <div class="col-lg-12">
                              <br />
                                <div class="col-lg-2">
                                    <button type="button" value="Shift Report" id="btnShift" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Shift Report</button>

                                </div>
                                <div class="col-lg-2">

                                    <button type="button" value="Shift Roster Report" id="btnShiftRoaster" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Shift Roster Report</button>

                                </div>
                               
                                <div class="col-lg-2">

                                    <button type="button" value="Swipe Report" id="BMSwipeReports" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Swipe/Daily Biometric Report</button>

                                </div>
                            
                                <div class="col-lg-2">

                                    <button type="button" value="Swipe Monthly Report" id="BMSwipeMonthlyReports" class="btn  btn-success"><span class="glyphicon glyphicon-ok-circle"></span>Swipe/Monthly Biometric Report</button>

                                </div>
                            </div>
                        </div>

                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>

                    <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center; overflow: initial">
                    </div>
                    <br />

                    <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span>Excel</button>
                    <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span>Word</button>




                </div>

            </div>
        </div>
        </div>
    </form>
    <%-- <%Html.EndForm(); %>--%>
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
    <div id="MsgDiv" style="display: none;">
        <label id="lblError"></label>
    </div>
    <%-- </div>--%>
</asp:Content>

