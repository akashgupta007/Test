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
                            
                                                                       
                            <span  style="text-align:left;"class="panel-title" > Employee Loan Process  </span>                          
<a style="color: #E6F1F3;float:right" target="_blank"  href="<%= Url.Content("~/Help/Payroll/Loan/LoanProcess.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                  
                            </div>

                            <div class="panel-body" style="align-items:center; ">



                  
       <div class="col-lg-12">
   

    
                                         
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
                 <div class="col-lg-2">
                                    Employee Category 
                                                              
                                    <%: Html.DropDownListFor(model => model.EmpCategoryId ,Model.EmpCategoryList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmpCategoryId)%>
                                </div>
    
                          
        <div class="col-lg-1">

        </div>
    
   </div>

    <div class="col-lg-12" style="margin-top:5px; ">
  
       
          
                                       
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
            <button style="display:none;" type="button" id="btnExportToExcel" class="btn  btn-success" value="Export To Excel"><span class="glyphicon glyphicon-export"></span>Export To Excel</button>
          <%-- <input type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"/>--%>
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
