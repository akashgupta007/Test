<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.TimeSheetEntryViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TimeSheet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/TimeSheet.js") %>"></script> 
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
            $("#EmployeeId").select2();
        });
        var onSuccess = function () {
            var flag = 0;
            var colTitle = $("#data_Table").length;
            $("#DivInsert").hide();
            if (colTitle == "0" || colTitle == undefined) {
            }
            else {
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
  <%-- <form id="TimeSheet" method="post" novalidate="novalidate">--%>
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

                        <span style="text-align: left;" class="panel-title">Asset Time Sheet Daily Entry </span>
                        <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Biometric/BMDailyAttendanceEntry1.html") %>">
                            <b>
                                <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>
                        </span>
                    </div>
                    <div class="panel-body" style="align-items: center;">



                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>

                            <div class="col-lg-2">
                                <br />
                                <input type='checkbox' class='checkbox-inline' id="IsBulkAttendance" onchange="BulkSearch()" name="IsBulkAttendance" />
                                Is Bulk Time Sheet Entry
                            </div>


                            <div class="col-lg-2 divSearch">
                                Customer Name
                   <%: Html.DropDownListFor(model => model.CustomerId,Model.Customerlist  , new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.CustomerId)%>
                            </div>

                            <div class="col-lg-2 divSearch">
                                Site Address    
                   <%: Html.DropDownListFor(model => model.SiteAddressId ,Model.SiteAddressList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.SiteAddressId)%>
                            </div>

                            <div class="col-lg-2 divSearch">
                                Model
                                                              
                                    <%: Html.DropDownListFor(model => model.ModelId ,Model.ModelList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.ModelId)%>
                            </div>
                            <div class="col-lg-2 divSearch">
                                Make
                             
                                
                                    <%: Html.DropDownListFor(model => model.MakeId ,Model.MakeList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.MakeId)%>
                            </div>


                            <div class="col-lg-1">
                            </div>

                        </div>

                        <div class="col-lg-12" style="margin-top: 5px;">

                            <div class="col-lg-1">
                            </div>


                            <%-- <div class="col-lg-2 divSearch">
                                    EquipmentSI No
                                
                                    <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                                </div>
       
       

        <div class="col-lg-2 divSearch" >
                                   Fleet Id
                               
                                
                                    <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ShiftId)%>
                                </div>--%>
                            <div class="col-lg-2">
                                Operator Name   
                               
                                
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.SalesManagerlist, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                            </div>
                            <div class="col-lg-2 divSearch" id="DivAttendanceDate">
                                Attendance  Date
                             
                                    <%: Html.TextBoxFor(model => model.AttendanceDate  , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select  Date"})%>
                                <%: Html.ValidationMessageFor(model => model.AttendanceDate)%>
                            </div>

                            <div class="col-lg-2 divBulkSearch" style="display: none;">
                                Month  
        <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.MonthId)%>
                            </div>

                            <div class="col-lg-2 divBulkSearch" style="display: none;">
                                Year  
        <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.Year)%>
                            </div>

                            <div class="col-lg-2">
                                <br />
                                <button style="vertical-align: central; text-align: left;" type="submit" value="Search" id="btnSearch" onclick='return  CheckDateValidation()' name="command" class="btn  btn-success divSearch"><span class="glyphicon glyphicon-search"></span>Search</button>
                                <button style="vertical-align: central; text-align: left; display: none;" type="submit" value="BulkSearch" onclick='return  CheckEmpValidation()' id="btnBulkSearch" name="command" class="btn  btn-success divBulkSearch"><span class="glyphicon glyphicon-search"></span>Bulk Search</button>

                            </div>

                        </div>




                    </div>

                </div>


                <div style="text-align: center">
                    <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                </div>


                
                <div id="data" style="overflow-x: auto;">
                </div>
                </div>
            <div id="calDIV"style="display:none" >
                         
                <img class="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
       <button type="button" id="btnCalculate" name="btnCalculate" class="btn btn-Warning"  onclick="CalculateTime();"><span class="glyphicon glyphicon-align-center"></span> Calculate Work Hours </button>
                          
                </div>
                <table style="display:none"  id="totalDiv" class='col-sm-6 display table table-striped table-bordered table-hover table-responsive table-condensed' width='100%' cellspacing='0'>
                    
                        
                        <tr>
                            <th>Employee Name</th>                           
                            <th>Total Work Hours</th>
                            <th>Total BreakDown Time</th> 
                            <th>Total OT in hours</th> 
                            <th>Total OT in Rs.</th>                          
                        </tr>
                    
                    
                        <tr>
                            <td ><p id="Empname"></p></td>
                            <td > <p id="twh"></p></td>
                            <td ><p id="tbdt"></p></td>
                            <td ><p id="toth"></p></td>
                            <td ><p id="totRs"></p></td>                           
                        </tr>
                    
                </table>
            
            <div id="test" title="Attached Document"> 
                <div  id="logoid"></div> 
            </div> 


             <div class="col-lg-12"style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapsesix" >
        <b style="color:blue">Attendance Of Employee's</b>
        </a>
        </h4>
        </div>
        <div id="collapsesix" class="panel-collapse collapse">
        <div class="panel panel-primary">                                       
        <div class="panel-body ">
       <div id="dataPivot" style="overflow-x: auto;">
                </div>
        <%--<button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="Command" value="Excel"><span class="glyphicon glyphicon-export"></span>Excel</button>--%>
        <%--<button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="Command" value="Word"><span class="glyphicon glyphicon-export"></span>Word</button>--%>
        </div>
        </div>
        </div>
        </div>
                 </div>
            <div class="col-lg-12"style="margin-top:5px;display:none" id="unassigneddiv">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseunassign" >
        <b style="color:blue">UnAssigned list</b>
        </a>
        </h4>
        </div>
        <div id="collapseunassign" class="panel-collapse collapse">
        <div class="panel panel-primary">                                       
        <div class="panel-body ">
       <div id="dataUnAssigned" style="overflow-x: auto;">
                </div>
        <%--<button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="Command" value="Excel"><span class="glyphicon glyphicon-export"></span>Excel</button>--%>
        <%--<button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="Command" value="Word"><span class="glyphicon glyphicon-export"></span>Word</button>--%>
        </div>
        </div>
        </div>
        </div>
                 </div>


        </div>


    </div>


       <%--</form>--%>



  <%--  <%Html.EndForm(); %>--%>

  <%--  <div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
  <%--  </div>--%>
</asp:Content>
