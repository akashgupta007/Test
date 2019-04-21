<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpSalaryViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeSalary6.js") %>"></script>
    
      <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
              
            }
           
        });
    </script>
    <style>
        .panel childrow td {
            column-span: all;
        }
     
.black_overlay {
  display: none;
  position: absolute;
  top: 0%;
  left: 0%;
  width: 100%;
  height: 100%;
  background-color: black;
  z-index: 1001;
  -moz-opacity: 0.8;
  opacity: .80;
  filter: alpha(opacity=80);
}
.white_content {
  display: none;
  position: absolute;
  top: 25%;
  left: 50%;
  width:100%;
  height: 100%;
  /*padding: 16px;*/
  /*border: 16px solid orange;
  background-color: white;*/
  z-index: 1002;
  overflow: auto;
}
        table td {
            border:none;
        }
        #popup1 {
            display: none;
            position: fixed;
            width: 250px;
            height: 150px;
            top: 50%;
            left: 50%;
            margin-left: -155px;
            margin-top: -50px;
            border: 5px solid black;
            background-color: #DEDFDE;
            padding: 30px;
            z-index:99999; 
            font-family: Verdana;
            font-size: 10pt;
            border-radius: 10px;
            -webkit-border-radius: 20px;
            -moz-border-radius: 20px;
            font-weight: bold;
        }

        #content1 {
            height: auto;
            width: 250px;
            margin: 60px auto;
        }

        #popupclose {
            margin: 0px 0 0 80px;
            width: 50px;
        }
         select#PayrollItemId {
            max-width: 130px;
            
        }

        select#Payroll_ValueType{
            max-width: 130px;
        }
        .popup .close:hover {
  color: #06D85F;
}
.popup .content {
  max-height: 30%;
  overflow: auto;
}
        input#StartDtItem{
            max-width: 130px;
        }

        input#EndDtItem {
            max-width: 130px;
        }

        input#Rate {
            max-width: 130px;
        }
        input#NotesItem {
            max-width: 130px;
        }

    </style>
    <form id="EmployeeSalary" method="post">
        <div class="container-fluid ">
         <div class="row-fluid">
                    <input type="radio" name="view" value="0" checked="checked" id="templatemode" onclick="checkclick();">
                    <span class="glyphicon glyphicon-user"></span>User Defined Format Entry &nbsp; &nbsp;
                        <input type="radio" name="view" value="1" id="gridmode" onclick="checkclick();" />
                    <span class="glyphicon glyphicon-download"></span>Upload from Excel
                   
                </div>
         <div id="paneldata">
        <div class="form-horizontal" style="margin-top: 20px;">
            <div class="row">
                <div class="col-lg-12" style="margin-top: 20px;">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employee Salary</span>
                              <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeSalary.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                           
                        </div>
                        <div class="panel-body ">
                            <div class="form-group" style="margin-top: 10px;">
                                <div class="col-md-2">

                                    <input type='checkbox' class='checkbox-inline' id="IsHistory" name="IsHistory" />
                                    Show History
                                
                                    <button type="button" id="addSalary" name="addSalary" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Salary</button>
                                    <button type="button" id="ExportData" style="display: none;" name="ExportData" class="btn btn-info"><span class="glyphicon glyphicon-export"></span>Excel</button>
                                </div>

                            </div>


                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Employee Salary </h3>
                                </div>
                                <div class="panel-body ">
                                     <div class="col-lg-12"> 
                                    <div class="col-lg-2"id="IsCTCDiv">                                    
                                           
                                            <%: Html.RadioButtonFor(model => model.IsCTC, "CTC", new {@class="ctcgross" })%> Using CTC                                             
                                            <%: Html.RadioButtonFor(model => model.IsCTC,"Gross", new {@class="ctcgross" })%> Using Gross
                                           
                                    </div>
                                         <div class="col-lg-10" id="daApplicable">                                        
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                        <input type="checkbox" />Is DA Applicable 
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">    
                                        <input type="text" class="form-control" placeholder="Enter DA rate" title="enter rate value" id="datextbox"/>                                        
                                        </div> 
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">    
                                        <input type="text" class="form-control" placeholder="Enter Gross exclude DA" title="Enter Gross exclude DA" id="grossExDA"/>                                        
                                        </div>                                 
                                         <div class="form-group col-lg-2" style="margin-left: 1px;"> 
                                             <input type="Button" value="Calculate for Gross" id="addda" class="btn btn-success" />  
                                         </div>                                     
                                                                        
                                    </div>
                                     </div>
                                    
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Employee Name  
                                             <input type="hidden" id="Employee_Id" name="Employee_Id" />
                                            <input type="hidden" id="EmpSalaryId" name="EmpSalaryId" />
                                            <input type="hidden" id="SalaryStartDate" name="SalaryStartDate" />
                                            <input type="hidden" id="SalaryEndDate" name="SalaryEndDate" />

                                            <input type="hidden" id="PayrollStartDate" name="PayrollStartDate" />
                                            <input type="hidden" id="PayrollEndDate" name="PayrollEndDate" />
                                            <input type="hidden" id="PayrollItemStartDate" name="PayrollItemStartDate" />
                                            <input type="hidden" id="PayrollItemEndDate" name="PayrollItemEndDate" />

                                            <input type="hidden" id="hfGross" name="hfGross" />
                                            <input type="hidden" id="hfCtc" name="hfCtc" />


                                            <input type="hidden" id="rateflag" name="rateflag" />
                                            <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control Parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                        </div>
                                        <div class="form-group col-lg-2" id="GrossDiv" style="margin-left: 1px;">
                                            Gross
                                        <%: Html.TextBoxFor(model => model.Gross , new {@class="form-control Parent", placeholder="Gross",@id="grossvalue"})%>
                                            <%: Html.ValidationMessageFor(model => model.Gross)%>
                                        </div>
                                        <div class="form-group col-lg-2" id="CTCDiv" style="margin-left: 1px; display: none;">
                                            CTC
                                        <%: Html.TextBoxFor(model => model.Ctc , new {@class="form-control Parent",@onChange="CalculationCTC()", placeholder="CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.Ctc)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Special Bonus
                                        <%: Html.TextBoxFor(model => model.SpecialBonus , new {@class="form-control Parent",@onChange="CalculationCTC()", placeholder="Special Bonus"})%>
                                            <%: Html.ValidationMessageFor(model => model.SpecialBonus)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Actual CTC
                                        <%: Html.TextBoxFor(model => model.ActualCtc , new {@class="form-control Parent",@onChange="CalculationCTC()",@readonly="readonly", placeholder=" Actual CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.ActualCtc)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Start Date
                              
                                   <%: Html.TextBoxFor(model => model.StartDt  , new {@class="form-control Parent", @readonly="readonly", placeholder="Start Date"})%>
                                            <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            End Date
                            
                                 <%: Html.TextBoxFor(model => model.EndDt, new {@class="form-control DBPicker Parent", @readonly="readonly", placeholder="End Date"})%>
                                            <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Notes
                             
                                     <%: Html.TextBoxFor(model => model.Notes , new {@class="form-control Parent",@maxlength=100, placeholder="Max length is 100."})%>
                                            <%: Html.ValidationMessageFor(model => model.Notes)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-bottom:2px; margin-left: 1px;">
                                            <button  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span>Save</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                            <button  type="button" id="btnClose" name="btnClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>
                                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                        </div>

                                    </div>
                                    <div class="col-lg-12">

                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Variable Pay %
                                        <%: Html.TextBoxFor(model => model.VariablePayPercentage , new {@class="form-control Parent",@onChange="CalculationCTC()", placeholder="Pay %"})%>
                                            <%: Html.ValidationMessageFor(model => model.VariablePayPercentage)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Variable CTC
                                        <%: Html.TextBoxFor(model => model.VariableCtc , new {@class="form-control Parent",@onChange="CalculationCTC()", placeholder="Variable CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.VariableCtc)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Fixed CTC
                                        <%: Html.TextBoxFor(model => model.FixedCtc , new {@class="form-control Parent",@readonly="readonly", placeholder="  Fixed CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.FixedCtc)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Period Cycle 
                                       <%: Html.DropDownListFor(model => model.PayPeriodCycle, new List<SelectListItem> { 
                                                new SelectListItem{Text="Monthly", Value="M", Selected=true}}, new {@class="form-control Parent" })%>
                                            <%: Html.ValidationMessageFor(model => model.PayPeriodCycle ) %>
                                        </div>





                                    </div>



                                   
                                </div>
                            </div>



                            <div class="col-lg-12" style="margin-top: 20px;">
                                <div id="data" style="overflow-x: auto;">
                                </div>



                                <button style="margin-bottom: 2px; margin-top: 5px; margin-right: 1px;" type="submit" id="btnExcel" class="btn  btn-success  cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span>Excel</button>
                                <button style="margin-bottom: 2px; margin-top: 5px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success  cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span>Word</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="childDiv" style="display: none;">
            <input type="hidden" id="flag" name="flag" />
            <div class="row">
                <div class="col-lg-12">
                    <div id="childEditPanel" style="display: none; width: 100%">
                        <div style="margin-top: 10px;">

                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Employee Salary Item</h3>
                                </div>
                                <div class="panel-body ">
                                    <table id='data_TableInfo' style="width: 100%">
                                        <tr>
                                            
                                            <th>Payroll Item</th>
                                            <th>Payroll Value Type  </th>
                                            <th><span id="spanPayrollValueType"></span></th>
                                            <th>Start Date </th>
                                            <th>End Date</th>
                                            <th style="display: none">Arear Start Date </th>
                                            <th style="display: none">Arear End Date</th>
                                            <th>Notes</th>
                                            <th></th>
                                        </tr>
                                        <tr class="appendableDIV">
                                            
                                            <td>
                                                <div class="form-group">
                                                    <input type="hidden" id="EmpSalaryItemId" name="EmpSalaryItemId" />
                                                    <input type="hidden" id="PayPercentageStartDate" name="PayPercentageStartDate" />
                                                    <input type="hidden" id="PayPercentageEndDate" name="PayPercentageEndDate" />
                                                    <input type="hidden" id="PayrollValueType" name="PayrollValueType" />
                                                    <input type="hidden" id="RateValue" name="RateValue" />
                                                    <%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@class="form-control child",@onchange="PayrollItemChange(this)"})%>
                                                    <%: Html.ValidationMessageFor(model => model.PayrollItemId)%>
                                                </div>
                                            </td>
                                            
                                            <%--<td id="TDRate" style="display:none">
                                                <div class="form-group"> 
                                                     <%: Html.TextBoxFor(model => model.DArate , new {@class="form-control child" ,@id="DARate" ,@onblur="fillDA(this)" ,@maxlength="5", placeholder="Enter DA." })%>

                                                </div>

                                            </td>--%>
                                            <td>
                                                <div class="form-group">
                                                    <%: Html.DropDownListFor(model => model.Payroll_ValueType, new List<SelectListItem> { 
                                        new SelectListItem{Text="Static Value",Selected=true, Value="1"},
                                        new SelectListItem{Text="Computed Value", Value="2"}     },"--Select--", new {@class="form-control child",@onchange="PayrollValueTypeChange(this)"})%>
                                                    <%: Html.ValidationMessageFor(model => model.Payroll_ValueType ) %>
                                                </div>
                                            </td>


                                            <td>
                                                <div class="form-group">

                                                    <%: Html.TextBoxFor(model => model.Rate , new {@class="form-control child" ,@id="Rate"  ,@maxlength="18", placeholder="Enter value." , @style="display:none"})%>
                                                    <%: Html.ValidationMessageFor(model => model.Rate)%>
                                                </div>

                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <%: Html.TextBoxFor(model => model.StartDtItem  , new {@class="form-control child" , @readonly="readonly", placeholder="Select Start Date"})%>
                                                    <%: Html.ValidationMessageFor(model => model.StartDtItem)%>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <%: Html.TextBoxFor(model => model.EndDtItem, new {@class="form-control child" , @readonly="readonly", placeholder="Select End Date"})%>
                                                    <%: Html.ValidationMessageFor(model => model.EndDtItem)%>
                                                </div>
                                            </td>
                                            <td style="display: none">
                                                <div class="form-group" style="display: none;">
                                                    <%: Html.TextBoxFor(model => model.PayStartDt  , new {@class="form-control child" , @readonly="readonly"  ,@maxlength="10", placeholder="Select Start Date"})%>
                                                    <%: Html.ValidationMessageFor(model => model.PayStartDt)%>
                                                </div>
                                            </td>
                                            <td style="display: none">
                                                <div class="form-group" style="display: none;">
                                                    <%: Html.TextBoxFor(model => model.PayEndDt, new {@class="form-control child"  , @readonly="readonly" , @maxlength="10", placeholder="Select End Date"})%>
                                                    <%: Html.ValidationMessageFor(model => model.PayEndDt)%>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group" >
                                                    <%: Html.TextBoxFor(model => model.NotesItem , new {@class="form-control child" ,@maxlength=100, placeholder="Max length is 100."})%>
                                                    <%: Html.ValidationMessageFor(model => model.NotesItem)%>
                                                </div>
                                            </td>
                                            <td id="dButton">
                                                <button class="btn btn-primary" type="button" onclick="addRow();"><i class="glyphicon glyphicon-plus"></i></button>
                                                <button class="btn btn-danger" type="button" onclick="removeRow(this);"><i class="glyphicon glyphicon-minus"></i></button>
                                                <%--  <button class="btn btn-danger" type="button" onclick="$(this).closest('tr').remove()"><i class="glyphicon glyphicon-minus"></i></button>--%>
                                                
                                            </td>
                                        </tr>

                                    </table>
                                    <table>
                                        <tr>
                                            <center>
                                            <td colspan="9">
                                                <div class="form-group">
                                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" name="btnChildInsert" onclick='InsertChildRecord()' class="btn  btn-success childInsert"><span class="glyphicon glyphicon-picture"></span>Save</button>
                                                    <button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success childUpdate"><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                                    <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>
                                                    <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                                </div>

                                            </td>
                                            </center>
                                        </tr>
                                    </table>
                                    <div class="col-lg-12">
                                        <div id="childData">
                                        </div>
                                        <div style="text-align: center; display: none;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
      
             </div>
            <div id="uploadPanel" style="display: none">
        <%--<form method="post" enctype="multipart/form-data" id="downlaodexcel"></form>--%>
        <center>
                    <table class="table table-striped table-bordered table-hover table-responsive table-condensed"style="width:80%">
                        <tr>
                            <td><button type="submit" id="btnDownloadGorss" name="command" value="DownloadSalaryFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span> Download Salary File</button></td>
                            <td> Import Gross / CTC </td>
                            <td><input name="PathExcel" id="PathExcel" type="file" tabindex="3" onchange="onFileSelectedtype(event)"/></td>                            
                            <td> <button type="button" id="UploadFile" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Salary</button> </td>
                        </tr>
                        <tr>
                            <td><button type="submit"  name="command" value="DownloadSalaryItems" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span> Download Salary Items file</button></td>
                            <td> Import Salary BreakUp's </td>
                            <td><input name="PathExcelCTC" id="PathExcelCTC" type="file" tabindex="3" onchange="onFileSelectedtypeCTC(event)"/></td>                            
                            <td> <button type="button" id="UploadFileCTC" name="command" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Salary Items</button> </td>
                        </tr>
                        <tr>
                            <td>
                                <button type="submit" id="btnatt" name="command" value="DownloadAttendanceFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span>Download Attendance File</button></td>
                            <td>Import Attendance</td>
                            <td>
                                <input name="PathExcel" id="PathExcel1" type="file" tabindex="3" onchange="onFileSelectedtype1(event)" /></td>
                            <td>
                                <button type="button" id="UploadFile1" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Attendance</button>
                            </td>
                        </tr>  
                        <tr>
                            <td>
                                <button type="submit" id="btnvariableSalary" name="command" value="DownloadvariableFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span>Download variable File</button></td>
                            <td>Import Variable Salary</td>
                            <td>
                                <input name="PathExcel" id="PathExcelvariable" type="file" tabindex="3" onchange="onFileSelectedtype2(event)" /></td>
                            <td>
                                <button type="button" id="UploadFilevariable" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Variable salary</button>
                            </td>
                        </tr>     
                    </table>
      
        </center>
        
    </div>
        </div>
        <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
        <div class="content">                           
        <img id="LoadingProgress" src="<%= Url.Content("~/Images/pleasewait.gif") %> " />
        </div>
        </div>
        </div>
    </form>        
  <div id="light" class="white_content" style="display:none">     
      <img  src="<%= Url.Content("~/Images/loader.gif") %> " />       
  </div>
  <div id="fade" class="black_overlay" style="display:none">

  </div>
  <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">

            <label id="lblError"></label>
        </div>
    <%--</div>--%>
</asp:Content>
