<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLeaveTypeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeLeaveType
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/EmpLeaveType5.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>

    <form id="EmpLeaveType" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employee Leave Type  </span>
                            <a style="color: #E6F1F3;float:right" target="_blank"  href="<%= Url.Content("~/Help/Payroll/Leave/EMployeeLeaveType.html")%>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                         
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record  </button>
                                </div>
                            </div>
                            <div id="data" style="overflow: auto;">
                            </div>

                        </div>
                        <div style="text-align: center; margin-bottom: 5px;">
                            <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div class="panel-body ">

                            <div id="divDisable">

                                <div class="form-group"  style="display:none;">
                                    <div class="col-lg-3 ">
                                        Leave Type Request
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.DropDownListFor(model => model.LeaveRequestType, new List<SelectListItem> { 
                                     new SelectListItem{Text="Origination", Value="Origination"}}, new {@class="form-control"}) %>
                                        <%: Html.ValidationMessageFor(model => model.LeaveRequestType ) %>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Leave Type :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <input type="hidden" id="EmpLeaveTypeId" name="EmpLeaveTypeId" />
                                        <%: Html.DropDownListFor(model => model.LeaveTypeId ,Model.LeaveTypeList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.LeaveTypeId)%>
                                    </div>
                                </div>

                                <div class="form-group" style="display: none;">
                                    <div class="col-lg-3">
                                        Employee Name :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Employee Type:
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                    </div>
                                </div>

                               

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Designation :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Location :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                    </div>
                                </div>
                                 
                                  <div class="form-group">
                                    <div class="col-lg-3">
                                        Department :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <div class="col-lg-3">
                                        Project :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                    </div>
                                </div>
                                <div class="form-group" style="display:none;">
                                    <div class="col-lg-3">
                                        Pay Rate Fraction
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model => model.PayRateFraction, new {@class="form-control", PlaceHolder="Enter Pay Rate Fraction" })%>
                                        <%: Html.ValidationMessageFor(model => model.PayRateFraction)%>
                                    </div>
                                </div>



                                <div class="form-group" id="orig1" >
                                    <div class="col-lg-3">
                                       Leave Distribution Period
                                    </div>
                                    <div class="Form-control col-lg-9">
                                  

                                        <%: Html.DropDownListFor(model => model.AccrualPeriod, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Monthly", Value="1"},
                                                                     new SelectListItem{ Selected=true, Text="Yearly", Value="2"}
                                                                    }, new {@class="form-control"}) %>
                                        <%: Html.ValidationMessageFor(model => model.AccrualPeriod ) %>
                                    </div>
                                </div>
                                

                                <div class="form-group" id="orig2">
                                    <div class="col-lg-3">
                                        Accrual Period Begin
                                    </div>
                                    <div class="Form-control col-lg-9">
                                           <%: Html.DropDownListFor(model => model.AccrualPeriodBegin ,Model.MonthList, new {@class="form-control"})%>
                                                                        
                                        <%: Html.ValidationMessageFor(model => model.AccrualPeriodBegin)%>
                                    </div>
                                </div>

                                <div class="form-group" id="orig3">
                                    <div class="col-lg-3">
                                        Accrual Period Count
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.AccrualPeriodCount ,Model.MonthList, new {@class="form-control"})%>
                                          <%: Html.ValidationMessageFor(model => model.AccrualPeriodCount)%>
                                    </div>
                                </div>


                                    <div class="form-group"  style="display:none;" id="yearDiv" >
                                    <div class="col-lg-3">
                                        Year :
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.LeaveTypeYear ,Model.YearList, new {@class="form-control"})%>
                                         <%: Html.ValidationMessageFor(model => model.LeaveTypeYear ) %>
                                    </div>
                                </div>




                                <div class="form-group" id="orig4">
                                    <div class="col-lg-3">
                                        Leave Per Accrual Period
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model => model.LeavePerAccrualPeriod, new {@class="form-control", PlaceHolder="Enter Leave Per Accrual Period" })%>
                                        <%: Html.ValidationMessageFor(model => model.LeavePerAccrualPeriod)%>
                                    </div>
                                </div>

                                <div class="form-group" id="orig5">
                                    <div class="col-lg-3">
                                        Earned Leave
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.RadioButtonFor(model => model.EarnedLeave, "True",  new { @id="ErlYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.EarnedLeave, "False", new { @id="ErlNo", @checked = "checked" }) %>No

                                    </div>
                                </div>

                                <div class="form-group" id="Surr2" hidden="hidden">
                                    <div class="col-lg-3">
                                        Max Accruable Leave
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model=>model.MaxAccruableLeave, new {@class="form-control", PlaceHolder="Enter Max Accruable Leave"}) %>
                                        <%: Html.ValidationMessageFor(model => model.MaxAccruableLeave)%>
                                    </div>
                                </div>

                                <div class="form-group" id="MaxMonthDays" hidden="hidden">
                                    <div class="col-lg-3">
                                        Max Working Days in Month
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model=>model.MaxDaysMonth, new {@class="form-control", PlaceHolder="Enter days "}) %>
                                        <%: Html.ValidationMessageFor(model => model.MaxDaysMonth)%>
                                    </div>
                                </div>

                                <div class="form-group" id="MaxYearDays" hidden="hidden">
                                    <div class="col-lg-3">
                                        Max Working Days in Year
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model=>model.MaxDaysYear, new {@class="form-control", PlaceHolder="Enter days "}) %>
                                        <%: Html.ValidationMessageFor(model => model.MaxDaysYear)%>
                                    </div>
                                </div>

                                <div class="form-group" id="orig9">
                                    <div class="col-lg-3">
                                        Leave Usage  Restriction
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.RadioButtonFor(model => model.LeaveUsagerestriction, "True",  new { @id="lurYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.LeaveUsagerestriction, "False", new { @id="lurNo", @checked = "checked" })  %>No  
                                    </div>
                                </div>

                                <div class="form-group" id="orig6" hidden="hidden">
                                    <div class="col-lg-3">
                                        Leave Usage Restriction Period
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <select id="LeaveUsageRestrictionPeriod" class="form-control" name="LeaveUsageRestrictionPeriod">
                                            <option value="5" selected="selected">Monthly</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="form-group" id="orig7" hidden="hidden">
                                    <div class="col-lg-3">
                                        Min Leave Usage
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model => model.MinLeaveUsage, new {@class="form-control", PlaceHolder="Enter Min Leave Usage" })%>
                                        <%: Html.ValidationMessageFor(model => model.MinLeaveUsage)%>
                                    </div>
                                </div>

                                <div class="form-group" id="orig8" hidden="hidden">
                                    <div class="col-lg-3">
                                        Max Leave Usage
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model => model.MaxLeaveUsage, new {@class="form-control", PlaceHolder="Enter Max Leave Usage" })%>
                                        <%: Html.ValidationMessageFor(model => model.MaxLeaveUsage)%>
                                    </div>
                                </div>

                                <div class="form-group" id="Surr1" hidden="hidden">
                                    <div class="col-lg-3">
                                        Min Accruable Leave
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model => model.MinAccruableLeave, new {@class="form-control", PlaceHolder="Enter Min Accruable Leave" })%>
                                        <%: Html.ValidationMessageFor(model => model.MinAccruableLeave)%>
                                    </div>
                                </div>

                                <div class="form-group" id="Surr3" hidden="hidden">
                                    <div class="col-lg-3">
                                        Leave Encashable
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.RadioButtonFor(model => model.LeaveEncashable, "True",  new { @id="leYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.LeaveEncashable, "False", new { @id="leNo", @checked = "checked" })  %>No  
                                    </div>
                                </div>

                                <div class="form-group" id="Surr5" hidden="hidden">
                                    <div class="col-lg-3">
                                        <%--  Pool Leave From--%>
                                    </div>
                                    <div class="Form-control col-lg-9">
                                    </div>
                                </div>

                                <div class="form-group" id="Surr4" hidden="hidden">
                                    <div class="col-lg-3">
                                        Encashable Pay Rate
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%:Html.TextBoxFor(model => model.EncashablePayRate, new { @class="form-control", placeholder="Enter Encashable Pay Rate"})%>
                                        <%: Html.ValidationMessageFor(model => model.EncashablePayRate)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Leave Calculated From Date of Joining
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <input type='checkbox' class='checkbox-inline' id="IsCalculateFromDoj" name="IsCalculateFromDoj" />

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Include Holiday/WeekEnd
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <input type='checkbox' class='checkbox-inline' id="IsIncludeHoilday" name="IsIncludeHoilday" />

                                    </div>
                                </div>

                                <div class="form-group"  id="StartDateDiv" >
                                    <div class="col-lg-3 ">
                                        Start Date :
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.StartDt , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Enter Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                    </div>
                                </div>

                                <div class="form-group" id="EndDateDiv">
                                    <div class="col-lg-3 ">
                                        End Date :
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.EndDt , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Enter End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                    </div>
                                </div>

                            </div>

                        <div class="form-group">
                            <div class="col-lg-offset-4 col-lg-3">
                                <button style="margin-bottom: 2px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                <button type="button" id="btnProcessleave" name="btnProcessleave" style="margin-bottom: 2px; margin-right: 1px; margin-top: 4px; display: none;" class="btn btn-success">Process Leave Allocation</button>
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
