<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpReimbursementEntryViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmpReimbursementEntry
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
  <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Reimbursement/EmpReimbursementEntry.js") %>"></script>


    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }


          
        });
     </script> 
    

    <%    
        AjaxOptions ajaxOptions = new AjaxOptions
        {
            UpdateTargetId = "data",
            InsertionMode = InsertionMode.Replace,
            HttpMethod = "POST",
            LoadingElementId = "loading"
         
        };  
    %>
   <%  Ajax.BeginForm("EmpReimbursementEntry", null, ajaxOptions, new { id = "EmpReimbursementEntry" });%>
    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">

                    <div class="panel-heading">
                      
                           <span  style="text-align:left;"class="panel-title" > Reimbursement Entry </span>   
                         <a style="color: #E6F1F3;float:right" target="_blank"href="<%= Url.Content("~/Help/Payroll/Biometric/BioMetricLogSummery.html")%>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>                          
                          
                    </div>

                    <div class="panel-body" style="align-items: center;">
                        <input type="hidden" id="hf1" value="EmployeeLogSummery" />
                      <div class="col-lg-12">



    <div class="col-lg-2" style="display:none;">
        Department   
        <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.DepartmentId)%>
    </div>

    <div class="col-lg-2">
        Location    
        <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.LocationId)%>
    </div>

    <div class="col-lg-2"  style="display:none;">
        Designation 
        <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.DesginationId)%>
    </div>

    <div class="col-lg-2"  style="display:none;">
        Project  
        <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.ProjectId)%>
    </div>

    <div class="col-lg-2">
        Employee Type  
        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
    </div>
    

    <div class="col-lg-1">
    </div>

</div>

<div class="col-lg-12" style="margin-top: 5px;">

     <div class="col-lg-2"  style="display:none;">
        Shift   
        <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.ShiftId)%>
    </div>
    <div class="col-lg-2">
        Employee   
        <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
       
    </div>

  
    

     <div class="col-lg-2 month"  style="display:none;">
        Month  
        <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.MonthId)%>
    </div>

    <div class="col-lg-2 month"  style="display:none;">
        Year  
        <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.Year)%>
    </div>

     <div class="col-lg-2 " >
                                    From Date
                              
                                    <%: Html.TextBoxFor(model => model.StartDate  , new { @class = "form-control DBPicker",  @readonly="readonly", placeholder = "Enter From Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.StartDate ) %>
                          
                            </div>

                              <div class="col-lg-2 " >
                                    To Date
                              
                                    <%: Html.TextBoxFor(model => model.EndDate  , new { @class = "form-control DBPicker",  @readonly="readonly", placeholder = "Enter To Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate ) %>
                          
                            </div>



    <div class="col-lg-2">
        <br />
        <button style="vertical-align: central; text-align: left;"  value="Search" id="btnSearch" name="command" class="btn  btn-success">Search</button>
      
    </div>

    <div class="col-lg-1">
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
      
  <%Html.EndForm();%>
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>



