<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.PayrollUtil>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeVariableSalary.js") %>"></script>

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
             LoadingElementId = "loading",

         };
    
    
     %>
    <% Ajax.BeginForm(ajaxOptions); %>
  
        <div class="form-horizontal" style="margin-top: 10px;">

            
                <div class="row">
                    <div class="col-lg-12">
                        <div id="DetailPanel" class="panel panel-primary">

                            <div class="panel-heading">
                                  <span  style="text-align:left;"class="panel-title" > Variable Salary </span>                       

                               <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeVariableSalary.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>
                            </div>

                            <div class="panel-body" style="align-items:center; ">



                  <div class="col-lg-12" style="margin-bottom:4px;"  >
                            <div class="col-lg-2" style="display:none;"> 
                            Salary Item Type    <%-- <input type='checkbox' class='checkbox-inline VariableSalary' id="isVariableSalary " onchange="VariableSalaryChange()"  name="isVariableSalary"/> --%>
                                  <%: Html.DropDownListFor(model => model.SalaryItemType, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="VariableSalary", Value="VariableSalary"},
                                                                     new SelectListItem{Text="SpecialBonus", Value="SpecialBonus"},
                                                             new SelectListItem{Text="ProfessionalTax", Value="ProfessionalTax"}, 
                                                                new SelectListItem{Text="DA Local", Value="DALocal"}, 
                                                                   new SelectListItem{Text="DA Non Local", Value="DANonLocal"}}, 
                                                             "--Select--", new {@class="form-control" })%>
                                                            <%: Html.ValidationMessageFor(model => model.SalaryItemType ) %>
                                </div>
                       <div class="col-lg-2" id="PerformanceTypeDiv"  style="display:none;">
                                   Performance Type
                                                    
                                 <%: Html.DropDownListFor(model => model.PerformanceType ,Model.PerformanceTypeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.PerformanceType)%>
                                </div>
                                         
                              <div class="col-lg-2" id="QuarterDiv" style="display:none;">
                                    Quarter  
                               
                                
                                    <%: Html.DropDownListFor(model => model.Quarter ,Model.QuarterList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.Quarter)%>
                                </div>
      
                        <div class="col-lg-2" id="BonusDiv" style="display:none;">
                                    Bonus Type  
                               
                                
                                    <%: Html.DropDownListFor(model => model.BonusType ,Model.BonusTypeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.BonusType)%>
                                </div>





                                  </div>
       <div class="col-lg-12">
   

        <div class="col-lg-2">
                                    Payroll Item 
                                                    
                                 <%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.PayrollItemId)%>
                                </div>
                                         
        <div class="col-lg-2">
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
            Department   
                   <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
            <%: Html.ValidationMessageFor(model => model.DepartmentId)%>
        </div>

        <div class="col-lg-2">
            Location    
                   <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
            <%: Html.ValidationMessageFor(model => model.LocationId)%>
        </div>

    
                          
        <div class="col-lg-1">

        </div>
    
   </div>

    <div class="col-lg-12" style="margin-top:5px;">
  
       
                <div class="col-lg-2">
                                    Employee Category 
                                                              
                                    <%: Html.DropDownListFor(model => model.EmpCategoryId ,Model.EmpCategoryList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmpCategoryId)%>
                                </div>
                                       
            <div class="col-lg-2">
                                    Designation 
                                                              
                                    <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DesginationId)%>
                                </div>         
      <%--  <div class="col-lg-2">
                                    Project  
                             
                                
                                    <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                                </div>--%>

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
                                    Shift   
                               
                                
                                    <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ShiftId)%>
                                </div>
                                                 
        <div class="col-lg-2">
            <br />                     
           <button style="vertical-align:central; text-align:left; " type="submit" value="Search" id="btnSearch" name="command" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
           <input type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"/>
        </div>

     
   
    </div>


    </div>
                         <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
         

  
                                <div id="data" style="overflow:auto;margin-left:5px;margin-right:5px; text-align:center; " >
                                </div>
                                     </div>


                    </div>

                                  
                </div>
            

            
                
            
        </div>
   <%Html.EndForm(); %>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>






</asp:Content>
