<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpAttendanceEntryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Payroll Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/PayrollDetails1.js") %>"></script>
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 


    <form method="post" id="PayrollDetailsForm" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-12">
                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                          <span  style="text-align:left;"class="panel-title" >Employee Payroll </span>
                            <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/PayrollDetails.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>
                   
                        </div>

                        <div class="panel-body" style="align-items: center;">
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
                                    Designation 
                                                              
                                    <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DesginationId)%>
                                </div>
                          
        <div class="col-lg-1">

        </div>
    
   </div>

    <div class="col-lg-12" style="margin-top:5px;">
  
       
       
                                                
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
                                    Shift   
                               
                                
                                    <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ShiftId)%>
                                </div>
        <div class="col-lg-2 common">
                                    Company                                                              
                                    <%:  Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.CompanyId)%>
                                </div>
                                                 
        <div class="col-lg-2">
            <br />                     
           <button style="vertical-align:central; text-align:left; " type="button" value="Search" id="btnSearch"  class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
           <input type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" style="display:none;"/>
        </div>

        <div class="col-lg-1">

        </div>
   
    </div>

                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>

                        
                      
                       <input type="hidden" id="flag" name="flag" />
                        <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
                        </div>













                        <br />
                  
                        <button style="margin-bottom: 2px; margin-right: 2px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                        <button style="margin-bottom: 2px; margin-right: 2px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                    </div>
                </div>
            </div>
        </div>
      
           
            
          


        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
           
            


            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
              
            </div>
            <div class="col-lg-12">
                <div id="childData">
                </div>
               
            </div>
        </div>













    </form>
    <div>

       

    </div>



    <%--<%Html.EndForm(); %>--%>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>


