<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.EmpSalaryViewModel>" %>

 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmpGroupSalary5.js") %>"></script>
<script>
    $(document).ready(function (e) {

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');

    });
</script>
 <form id="EmployeeGroupSalary" method="post" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 20px;">
            <div class="row">
                <div class="col-lg-12" style="margin-top: 20px;">

                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employee Group Salary Details</span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeGroupSalary.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>


                        </div>
                        <div class="panel-body ">
                            <div class="form-group" style="margin-top: 10px;">
                                <div class="col-md-2">
                                    <input type='checkbox' class='checkbox-inline' id="IsHistory" name="IsHistory" />
                                    Show History
                                    <button type="button" id="addSalary" name="addSalary" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span> Salary</button>
                                 
                                </div>

                            </div>


                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Employee Salary Details</h3>
                                </div>
                                <div class="panel-body ">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-2" id="IsCTCDiv" style="margin-left: 1px; display: none;">

                                            <br />
                                            <%: Html.RadioButtonFor(model => model.IsCTC, "CTC", new {@class="ctcgross" })%> Using CTC
                                                                <%: Html.RadioButtonFor(model => model.IsCTC,"Gross", new {@class="ctcgross" })%> Using Gross
                                                               <%-- <%: Html.RadioButtonFor(model => model.IsInternationalWorker, "False", new { @id="IsInternationalWorkerNo" , @checked=true})  %>No --%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Salary Group    
                                             <input type="hidden" id="PayrollStartDate" name="PayrollStartDate" />
                                            <input type="hidden" id="PayrollEndDate" name="PayrollEndDate" />

                                            <input type="hidden" id="EmpSalaryId" name="EmpSalaryId" />
                                            <input type="hidden" id="EmpSalaryGroup_Id" name="EmpSalaryGroup_Id" />

                                            <input type="hidden" id="hfCtc_group" name="hfCtc_group" />
                                            <input type="hidden" id="hfGross_group" name="hfGross_group" />

                                            <input type="hidden" id="rateflag" name="rateflag" />
                                            <%: Html.DropDownListFor(model => model.EmpSalaryGroupId ,Model.SalaryGroupList, new {@class="form-control Parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.EmpSalaryGroupId)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Employee Name               
                                          <input type="hidden" id="Employee_Id" name="Employee_Id" />
                                            <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control Parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                        </div>
                                        <div class="form-group col-lg-2" id="GrossDiv" style="margin-left: 1px; display: none;">
                                            Gross
                                        <%: Html.TextBoxFor(model => model.Gross_group , new {@class="form-control Parent",@maxlength="16", placeholder="Gross"})%>
                                            <%: Html.ValidationMessageFor(model => model.Gross_group)%>
                                        </div>
                                        <div class="form-group col-lg-2" id="CTCDiv" style="margin-left: 1px;">
                                            CTC
                                        <%: Html.TextBoxFor(model => model.Ctc_group , new {@class="form-control Parent",@onChange="CalculationCTC()",@maxlength="16", placeholder="CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.Ctc_group)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Special Bonus
                                        <%: Html.TextBoxFor(model => model.SpecialBonus , new {@class="form-control Parent",@onChange="CalculationCTC()",@maxlength="16", placeholder="Special Bonus"})%>
                                            <%: Html.ValidationMessageFor(model => model.SpecialBonus)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Actual CTC
                                        <%: Html.TextBoxFor(model => model.ActualCtc , new {@class="form-control Parent",@onChange="CalculationCTC()",@maxlength="16",@readonly="readonly", placeholder=" Actual CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.ActualCtc)%>
                                        </div>

                                        <div class="form-group col-lg-2" id="DivPF" style="margin-left: 1px;">
                                            <br />
                                            <%: Html.CheckBoxFor(model => model.IsPfApplicable)%>  Voluntary PF 
                                            <%: Html.ValidationMessageFor(model => model.IsPfApplicable)%>
                                        </div>



                                    </div>

                                    <div class="col-lg-8">
                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Variable Pay %
                                        <%: Html.TextBoxFor(model => model.VariablePayPercentage , new {@class="form-control Parent",@onChange="CalculationCTC()",@maxlength="6", placeholder="Pay %"})%>
                                            <%: Html.ValidationMessageFor(model => model.VariablePayPercentage)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Variable CTC
                                        <%: Html.TextBoxFor(model => model.VariableCtc , new {@class="form-control Parent",@maxlength="16",@onChange="CalculationCTC()", placeholder="Variable CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.VariableCtc)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px; display: none;">
                                            Annual Fixed CTC
                                        <%: Html.TextBoxFor(model => model.FixedCtc , new {@class="form-control Parent",@maxlength="16",@readonly="readonly", placeholder="  Fixed CTC"})%>
                                            <%: Html.ValidationMessageFor(model => model.FixedCtc)%>
                                        </div>


                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Period Cycle 
                                       <%: Html.DropDownListFor(model => model.PayPeriodCycle, new List<SelectListItem> { 
                                                new SelectListItem{Text="Monthly", Value="M", Selected=true}}, new {@class="form-control Parent" })%>
                                            <%: Html.ValidationMessageFor(model => model.PayPeriodCycle ) %>
                                        </div>


                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Start Date
                              
                                   <%: Html.TextBoxFor(model => model.StartDt  , new {@class="form-control  Parent", @readonly="readonly", placeholder="Start Date"})%>
                                            <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            End Date
                            
                                 <%: Html.TextBoxFor(model => model.EndDt, new {@class="form-control DBPicker Parent", @readonly="readonly", placeholder="End Date"})%>
                                            <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                        </div>
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Notes
                             
                                     <%: Html.TextBoxFor(model => model.Notes , new {@class="form-control Parent" , placeholder="Notes."})%>
                                            <%: Html.ValidationMessageFor(model => model.Notes)%>
                                        </div>

                                        <br />
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">

                                            <button style="margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" style="margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Update</button>
                                            <button style="margin-right: 1px;" type="button" id="btnClose" name="btnClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12" style="margin-top: 20px;">
                                <div id="data" style="overflow-x: auto;">
                                </div>

                                <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                                <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">

                <input type="hidden" id="flag" name="flag" />


            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Employee Salary Item</h3>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div id="childData">
                </div>

            </div>
        </div>
    </form>