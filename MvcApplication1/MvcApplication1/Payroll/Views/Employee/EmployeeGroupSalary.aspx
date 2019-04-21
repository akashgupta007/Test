﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpSalaryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Group Salary
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmpGroupSalary5.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>
    <%--<style type="text/css">
        .Loading {
            width: 100%;
            display: block;
            position: absolute;
            top: 0;
            left: 0;
            height: 100%;
            z-index: 999;
            background-color: rgba(0,0,0,0.5); /*dim the background*/
        }

        .content {
            background: #fff;
            padding: 28px 26px 33px 25px;
        }

        .popup {
            border-radius: 1px;
            background: #6b6a63;
            margin: 30px auto 0;
            padding: 6px;
            position: absolute;
            width: 100px;
            top: 50%;
            left: 50%;
            margin-left: -100px;
            margin-top: -40px;
        }

       .popup1 { 
    border-radius: 19px; 
    background-color: rgba(0,0,0,0.5); 
    padding: 201px; 
    padding-bottom: 300px; 
    padding-left: 200px; 
    padding-top: 200px; 
    padding-right: 200px; 
    position: absolute; 
    width: 101%; 
    top: 32%; 
    left: 8%; 
    margin-left: -164px; 
    margin-top: -225px; 
    border-top-left-radius: 19px; 
    border-top-right-radius: 19px; 
    border-bottom-right-radius: 19px; 
    border-bottom-left-radius: 19px; 
}

    </style>--%>
    <div class="form-horizontal" style="margin-top: 20px;">
        <div class="row">
            <div class="form-group">
                <div class="col-lg-6">
                    <div class="col-lg-3" style="display:none;">
                        <input type="checkbox" id="IsVariableSalary"  name="IsVariableSalary" />

                        Variable Salary
                                          
                    </div>
                </div>
            </div>
        </div>
    </div>
    <form id="EmployeeGroupSalary" method="post" novalidate="novalidate">
        <div id="groupsalary">
            <div class="form-horizontal" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-lg-12" style="margin-top: 20px;">

                        <div id="DetailPanel" class="panel panel-primary">
                            <div class="panel-heading">
                                <span style="text-align: left;" class="panel-title">Employee Group Salary </span>
                                 <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeGroupSalary.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>  
                               


                            </div>
                            <div class="panel-body ">
                                <div class="form-group" style="margin-top: 10px;">
                                    <div class="col-md-2">

                                        <button type="button" id="addSalary" name="addSalary" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Salary</button>
                                        <input type='checkbox' class='checkbox-inline' id="IsHistory" name="IsHistory" />
                                        Show History
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

                                            <div class="form-group col-lg-2" id="DivPF" style="margin-left: 1px;white-space:nowrap;">
                                                <br />
                                                <%: Html.CheckBoxFor(model => model.IsPfApplicable)%>  Voluntary PF 
                                            <%: Html.ValidationMessageFor(model => model.IsPfApplicable)%>
                                            </div>
                                     <div class="form-group col-lg-1" id="VarSalaryDiv" style="margin-left: 1px;display:none;white-space:nowrap;">
                                                <br />
                                              <input type="checkbox" id="IsVariableSalaryChk" name="IsVariableSalaryChk"  />

                                                  Variable Salary
                                            </div>


                                        </div>

                                        <div class="col-lg-12">
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


                                            <div class="form-group col-lg-2" style="margin-left: 1px; display: none">
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
                                                <button type="button" id="btnUpdate" name="btnUpdate" style="margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
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
                                    <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span>  Word</button>
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
        </div>
    </form>



    <form id="EmployeeVariableSalary1" method="post" novalidate="novalidate">
        <div id="Varsalary"  >
            <div class="form-horizontal" style="margin-top: 10px;">
                <div class="row">
                    <div class="col-lg-6">
                        <div id="VarDetailPanel" class="panel panel-primary">
                            <div class="panel-heading">
                                <span style="text-align: left;" class="panel-title">Employee Variable Salary Details  </span>
                                <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Reimbursement/EmployeeReimbursement.html") %>">
                                    <b>
                                        <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                            </div>
                            <div class="panel-body ">
                                <div class="form-group">
                                    <div class="col-lg-6">
                                        <div class="col-lg-3 ">
                                            <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                        </div>
                                        <div class="col-lg-9">

                                            <input type='checkbox' class='checkbox-inline' id="VarIsHistory" name="VarIsHistory" />&nbsp;Show History
                                          
                                            
                                        </div>
                                    </div>
                                </div>
                                <div id="datavar" >
                                </div>
                            </div>

                        </div>
                    </div>
                    
                    
             
                    <div class="col-lg-6 " >
                        
                        <div class="panel panel-primary "  id="EditPanel1" >
                            <div class="panel-heading">
                                <h3 class="panel-title"><span id="panelHeader"></span></h3>
                            </div>
                            <div class="panel-body ">

                                <div class="form-group" id="VarEmployeeDiv">

                                    <div class="col-lg-3 ">
                                        Employee Name
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                           <input type="hidden" id="VarSalaryEndDate" name="VarSalaryEndDate" />
                                        <input type="hidden" id="EmpVariableSalaryId" name="EmpVariableSalaryId" />

                                        <%: Html.DropDownListFor(model => model.VarEmployeeId ,Model.EmployeeList, new {@class="form-control Parent"})%>
                                        <%: Html.ValidationMessageFor(model => model.VarEmployeeId)%>
                                    </div>
                                </div>

                                <div class="form-group" id="payroll_Itemid" style="display: none;">
                                    <div class="col-lg-3 ">
                                        Period Cycle 
                                    </div>
                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.VarPayPeriod_Cycle, new List<SelectListItem> { 
                                                new SelectListItem { Text = "--Select--", Value = "" },
                                                new SelectListItem{Text="Monthly", Value="Monthly", Selected=true},
                                                new SelectListItem {Text="Quartely", Value="Quartely"},
                                                   new SelectListItem {Text="Half Yearly", Value="Half Yearly"},
                                                     new SelectListItem {Text="Yearly", Value="Yearly"}}, new {@class="form-control Parent" })%>

                                        <%: Html.ValidationMessageFor(model => model.VarPayPeriod_Cycle ) %>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Payroll Item                              
                                    </div>
                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.VarPayrollItem_Id ,Model.VarPayrollItemList , new {@class="form-control Parent"})%>
                                        <%: Html.ValidationMessageFor(model => model.VarPayrollItem_Id)%>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Amount
                                    </div>

                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.VarAmount , new {@class="form-control Parent"})%>
                                        <%: Html.ValidationMessageFor(model => model.VarAmount)%>
                                    </div>

                                </div>




                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Start Date 
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.VarStartDt  , new {@class="form-control Parent", @readonly="readonly", placeholder="Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.VarStartDt)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        End Date 
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.VarEndDt, new {@class="form-control DBPicker Parent", @readonly="readonly", placeholder="End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.VarEndDt)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Notes 
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.VarNotes , new {@class="form-control Parent" , placeholder="Notes."})%>
                                        <%: Html.ValidationMessageFor(model => model.VarNotes)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Attendance Required 
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.CheckBoxFor(model => model.IsAttendanceApplicable)%>
                                        <%: Html.ValidationMessageFor(model => model.IsAttendanceApplicable)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-offset-4 col-lg-3">

                                        <button style="margin-right: 1px;" type="button" id="btnVarInsert" name="btnVarInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                        <button type="button" id="btnVarUpdate" name="btnVarUpdate" style="margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                        <button style="margin-right: 1px;" type="button" id="btnVarClose" name="btnVarClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                                    </div>
                                </div>

                            </div>
                        </div>
                   
                             </div>
               
            </div>
                 </div>
        </div>
    </form>

    <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
            <div class="content">                           
                <img id="LoadingProgress" src="<%= Url.Content("~/Images/pleasewait.gif") %> " />
            </div>
        </div>
    </div>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>
</asp:Content>
