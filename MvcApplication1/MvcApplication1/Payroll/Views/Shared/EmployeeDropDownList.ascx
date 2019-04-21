<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.PayrollUtil>" %>

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/EmployeeDropDownList1.js") %>"></script>
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
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
        <%--<%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmployeeId)%>--%>
        <div  id="EmployeeId" ></div>  
    </div>

  
    <div class="col-lg-2 date">
        Start Date  
        <%: Html.TextBoxFor(model => model.StartDate , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select Start Date" })%>
        <%: Html.ValidationMessageFor(model => model.StartDate)%>
    </div>

    <div class="col-lg-2 date">
        End Date 
        <%: Html.TextBoxFor(model => model.EndDate , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select End Date"})%>
        <%: Html.ValidationMessageFor(model => model.EndDate)%>
    </div>

     <div class="col-lg-2 month">
        Month  
        <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.MonthId)%>
    </div>

    <div class="col-lg-2 month">
        Year  
        <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.Year)%>
    </div>

    <div class="col-lg-2">
        <br />
        <button style="vertical-align: central; text-align: left;"  value="Search" id="btnSearch" name="command" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
       <button  type ="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"><span class="glyphicon glyphicon-export"></span> Export To Excel</button>
         <%--<input type ="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"/>--%>
    </div>

    <div class="col-lg-1">
    </div>

</div>



