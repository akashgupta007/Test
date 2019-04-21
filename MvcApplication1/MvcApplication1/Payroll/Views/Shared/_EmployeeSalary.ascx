<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.EmpSalaryViewModel>" %>




    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeCenterEmployeeSalary.js") %>"></script>
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
    <form id="EmployeeSalary" method="post" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 20px;">

            <div class="row">
                <div class="col-lg-12" style="margin-top: 20px;">

                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employee Salary Details</span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeSalary.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">


                            <div class="form-group" style="margin-top: 10px;">
                                <div class="col-md-4">
                                    <input type='checkbox' class='checkbox-inline' id="IsHistory" name="IsHistory" />
                                    Show History                                
                                    <button type="button" id="addSalary" name="addSalary" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Add Salary</button>
                                    <button type="button" id="ExportData" style="display: none;" name="ExportData" class="btn btn-info">Export To Excel</button>
                                    </div>
                            </div>
                             <div id="EditPanel" style="display: none;">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-6" id="IsCTCDiv" style="margin-left: 1px;">
                                            
                                            <br />
                                            <%: Html.RadioButtonFor(model => model.IsCTC, "CTC", new {@class="ctcgross" })%> Using CTC
                                                                <%: Html.RadioButtonFor(model => model.IsCTC,"Gross", new {@class="ctcgross", @checked="true" })%> Using Gross
                                                               <%-- <%: Html.RadioButtonFor(model => model.IsInternationalWorker, "False", new { @id="IsInternationalWorkerNo" , @checked=true})  %>No --%>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">

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

                                            <%:Html.HiddenFor(model=>model.EmployeeId) %>
                                            <input type="hidden" id="rateflag" name="rateflag" />

                                        <%--<div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Employee Name  
                                            

                                            <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control Parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                        </div>--%>

                                        <div class="form-group col-lg-2" id="GrossDiv" style="margin-left: 1px;">
                                            Gross
                                        <%: Html.TextBoxFor(model => model.Gross , new {@class="form-control Parent", placeholder="Gross"})%>
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
                                    <div class="col-lg-12">

                                        <div class="col-lg-offset-4 col-lg-3">
                                            <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                            <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClose" name="btnClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                        </div>

                                    </div>
                                </div>                            
                            <div class="col-lg-12" style="margin-top: 20px;">
                                <div id="data" style="overflow-x: auto;">
                                </div>
                                <button style="margin-bottom: 2px; margin-top: 5px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success  cancel" name="command" value="Excel">Excel</button>
                                <button style="margin-bottom: 2px; margin-top: 5px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success  cancel" name="command" value="Word">Word</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="childDiv" style="display: none;">
            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
                <button type="button" name="new" onclick='ChildRecordReset()' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-plus'></span><span class="glyphicon glyphicon-plus"></span>New Record</button>
                <input type="hidden" id="flag" name="flag" />
            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Employee Salary Item</h3>
                        </div>
                        <div class="panel-body ">
                            <div class="col-lg-6 ">




                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Payroll Item
                                    </div>
                                    <div class="col-lg-9 ">
                                        <input type="hidden" id="EmpSalaryItemId" name="EmpSalaryItemId" />
                                        <input type="hidden" id="PayPercentageStartDate" name="PayPercentageStartDate" />
                                        <input type="hidden" id="PayPercentageEndDate" name="PayPercentageEndDate" />
                                        <input type="hidden" id="PayrollValueType" name="PayrollValueType" />
                                        <input type="hidden" id="RateValue" name="RateValue" />
                                        <%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@class="form-control child", @onchange="PayrollItemChange()"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollItemId)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Payroll Value Type  
                                    </div>
                                    <div class="col-lg-9 ">


                                        <%: Html.DropDownListFor(model => model.Payroll_ValueType, new List<SelectListItem> { 
                                                new SelectListItem{Text="Static Value",Selected=true, Value="1"},
                                                new SelectListItem{Text="Computed Value", Value="2"}     },"--Select--", new {@class="form-control child",@onchange="PayrollValueTypeChange()" })%>
                                        <%: Html.ValidationMessageFor(model => model.Payroll_ValueType ) %>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        <span id="spanPayrollValueType"></span>
                                    </div>
                                    <div class="col-lg-9 ">

                                        <%: Html.TextBoxFor(model => model.Rate , new {@class="form-control child" ,@id="Rate"  ,@maxlength="18", placeholder="Enter value." , @style="display:none"})%>
                                        <%: Html.ValidationMessageFor(model => model.Rate)%>

                                        <%--<%: Html.DropDownListFor(model => model.PayrollFunctionId,Model.PayrollFunctionList, new {@class="form-control child", @id="PayrollFunctionId" ,@style="display:none" })%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollFunctionId ) %>--%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Start Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.StartDtItem  , new {@class="form-control child" , @readonly="readonly", placeholder="Select Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDtItem)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        End Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.EndDtItem, new {@class="form-control child" , @readonly="readonly", placeholder="Select End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.EndDtItem)%>
                                    </div>
                                </div>
                                <div class="form-group" style="display: none;">
                                    <div class="col-lg-3 ">
                                        Arear Start Date  
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.PayStartDt  , new {@class="form-control child" , @readonly="readonly"  ,@maxlength="10", placeholder="Select Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayStartDt)%>
                                    </div>
                                </div>
                                <div class="form-group" style="display: none;">
                                    <div class="col-lg-3 ">
                                        Arear End Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.PayEndDt, new {@class="form-control child"  , @readonly="readonly" , @maxlength="10", placeholder="Select End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayEndDt)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Notes  
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.NotesItem , new {@class="form-control child" ,@maxlength=100, placeholder="Max length is 100."})%>
                                        <%: Html.ValidationMessageFor(model => model.NotesItem)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-9" style="text-align: left;">

                                        <button style="margin-bottom: 2px; margin-right: 1px;" type="button" name="btnChildInsert" onclick='InsertChildRecord()' class="btn  btn-success childInsert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                        <button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success childUpdate"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                        <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                                        <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div id="childData">
                </div>
                <div style="text-align: center; display: none;">
                </div>
            </div>
        </div>
    </form>
    <div class="navbar navbar-inverse navbar-fixed-bottom">
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    </div>
