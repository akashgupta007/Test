<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpDailyAttendanceViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Daily Attendance Entry
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/DailyAttendanceEntry1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });

        //$(window).load(function () {
        //    $("#data_Table").dataTable();
        //});

      var onSuccess = function () {
            
             var flag = 0;
          var colTitle = $("#data_Table").length;
          $("#DivInsert").hide();
        
          if (colTitle == "0" || colTitle == undefined)
          {

          }
          else
          {        
              flag = 1;

          }

          if (flag == 1) {
              $("#data_Table").dataTable();
              $("#DivInsert").show();
          }
          else {
              $("#DivInsert").hide();
          }
       }

    </script>


    <%
    
        AjaxOptions ajaxOptions = new AjaxOptions
        {
            UpdateTargetId = "data",
            InsertionMode = InsertionMode.Replace,
            HttpMethod = "POST",
            LoadingElementId = "loading",
           OnSuccess="onSuccess"     
        };
    
    
    %>
    <% Ajax.BeginForm(ajaxOptions); %>

    <div class="form-horizontal" style="margin-top: 10px;" >

        <div class="container-fluid ">
                <div class="row-fluid">
                    <input type="radio" name="view" value="0" checked="checked" id="templatemode" onclick="checkclick();">
                    <span class="glyphicon glyphicon-user"></span>User Defined Format Entry &nbsp; &nbsp;
                        <input type="radio" name="view" value="1" id="gridmode" onclick="checkclick();" />
                    <span class="glyphicon glyphicon-download"></span>Upload from Excel
                   
                </div>
        <div class="row" id="content">
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">

                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title">Employee Daily Attendance Entry</span>
                       
                        <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/DailyAttendanceEntry.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>
                    </div>
                    <div class="panel-body" style="align-items: center;">



                        <div class="col-lg-12">

                            <div class="col-lg-1">
                                <input type='checkbox' class='checkbox-inline' id="IsBulkAttendance" onchange="BulkSearch()"  name="IsBulkAttendance"/> 
                                 Is Bulk Attendance Entry
                            </div>


                            <div class="col-lg-2 divSearch">
                                Department   
                   <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.DepartmentId)%>
                            </div>

                            <div class="col-lg-2 divSearch">
                                Location    
                   <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.LocationId)%>
                            </div>

                            <div class="col-lg-2 divSearch">
                                Designation 
                                                              
                                    <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.DesginationId)%>
                            </div>
                            <div class="col-lg-2 divSearch">
                                Project  
                             
                                
                                    <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                            </div>

                            <div class="col-lg-1">
                            </div>

                        </div>

                        <div class="col-lg-12" style="margin-top: 5px;">

                            <div class="col-lg-1">
                            </div>



                            <div class="col-lg-2 divSearch">
                                Employee Type  
                             
                                
                                    <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                            </div>

                            <div class="col-lg-2 divBulkSearch">
                                Employee   
                               
                                
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                            </div>

                            <div class="col-lg-2 divSearch">
                                Shift   
                               
                                
                                    <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.ShiftId)%>
                            </div>
                            <div class="col-lg-2 divSearch">
                                Attendance  Date
                             
                                    <%: Html.TextBoxFor(model => model.AttendanceDate  , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select  Date"})%>
                                <%: Html.ValidationMessageFor(model => model.AttendanceDate)%>
                            </div>
                            <div class="col-lg-2 divBulkSearch"  style="display:none;">
        Month  
        <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.MonthId)%>
    </div>

    <div class="col-lg-2 divBulkSearch"  style="display:none;">
        Year  
        <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.Year)%>
    </div>
                            <div class="col-lg-2">
                                <br />
                                <button style="vertical-align: central; text-align: left;" type="submit" value="Search" id="btnSearch" name="commandName" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>

                            </div>

                        </div>



                        <div class="col-lg-1">
                        </div>

                    </div>

                </div>


                <div style="text-align: center">
                    <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                </div>



                
                <div>
                <div id="data" style="overflow-x: auto;">
                </div>
<br />
                <div id="DivInsert" style="text-align:center;display:none;"><button type='submit' id='insert' value='Insert'  name='commandName' onclick='return  CheckValidation()' class='btn btn-success '><span class="glyphicon glyphicon-picture"></span> Save</button></div>
           
                </div>    
                     </div>


        </div>
    </div>
        <div id="uploadPanel" style="display: none">
        <%--<form method="post" enctype="multipart/form-data" id="downlaodexcel"></form>--%>
        <center>
                    <table class="table table-striped table-bordered table-hover table-responsive table-condensed"style="width:80%">
                        <tr>
                            <td> <button type="submit" id="btnDownload" name="command" value="DownloadExcelFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span> Download Excel</button></td>
                            <td> Import Excel File : </td>
                            <td>  <input name="PathExcel" id="PathExcel" type="file" tabindex="3" onchange="onFileSelectedtype(event)"/></td>                            
                            <td> <button type="button" id="UploadFile" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Attendance</button> </td>
                        </tr>
                    </table>
      
        </center>
        
    </div>
    </div>




    <%Html.EndForm(); %>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>
