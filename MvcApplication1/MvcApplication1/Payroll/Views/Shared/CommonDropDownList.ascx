<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.PayrollUtil>" %>
 
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/CommonDropdownList.js") %>"></script>
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
<div class="col-lg-12">

  
   



    <div class="col-lg-2">
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

    <div class="col-lg-1">
    </div>

</div>

<div class="col-lg-12" style="margin-top: 5px;">

   
  


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
        Employee   
                               
                                
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
    </div>


    <div class="col-lg-2">
        Attendance Source    
                   <%: Html.DropDownListFor(model => model.AttendanceSourceId ,Model.AttendanceSourceList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.AttendanceSourceId)%>
    </div>

    <div class="col-lg-2">
        <br />
        <button style="vertical-align: central; text-align: left;" type="submit" value="Search" id="btnSearch" name="command" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
    </div>


</div>

