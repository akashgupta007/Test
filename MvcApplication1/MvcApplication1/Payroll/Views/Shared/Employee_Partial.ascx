<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PoiseERP.Areas.Payroll.Models.EmployeeViewModel>" %>

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeePartial.js") %>"></script>

<script type="text/javascript">
    $(document).ready(function (e) {
        if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
            urlBackRestrict();
        }

        $('.btn').addClass('btn-xs');
        $('.form-control').addClass('input-xs');
    });
</script>

<style type="text/css">
    .Loding {
        width: 100%;
        display: block;
    }

    .content {
        background: #fff;
        padding: 28px 26px 33px 25px;
    }

    .popup {
        border-radius: 7px;
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
</style>


<form method="post" id="EmployeeInfo">

        <div class="form-horizontal" style="margin-top: 10px;">


            <div class="container ">
                <div id="content">



                    <div class="row">
                        <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                            <li id="tabP" class="active list-group-item-info"><a href="#tabPersonalInfo" data-toggle="tab">Personal Info</a></li>
                            <li id="tabC"><a href="#tabCommunicationInfo" class="list-group-item-info" data-toggle="tab">Communication Info</a></li>
                            <li id="tabG"><a href="#tabGenearlInfo" class="list-group-item-info" data-toggle="tab">General Info</a></li>
                            <li id="tabO"><a href="#tabPhotoInfo" class="list-group-item-info" data-toggle="tab">Other Info</a></li>
                            <li id="tabD"><a href="#tabDocumentInfo" class="list-group-item-info" data-toggle="tab">Photo</a></li>
                            <li id="tabParameter"><a href="#tabParameterInfo" class="list-group-item-info" data-toggle="tab">Parameters</a></li>
                        </ul>
                        <div>
                            <div>
                                <iframe id="ExcelFrame" style="display: none"></iframe>
                                <div id="dataExport" style="display: none;"></div>
                            </div>

                            <div id="my-tab-content" class="tab-content">

                                <div class="form-group" style="margin-top: 10px; margin-left: 8px;">
                                    <div class="col-lg-6">
                                        <button type="button" id="newEmployee" name="newEmployee" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span>New Employee</button>
                                        <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                        <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>

                                        <%--<button type="button" id="ExportData" name="ExportData" class="btn btn-info">Export To Excel</button>--%>

                                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                    </div>
                                </div>



                                <div class="tab-pane  active " id="tabPersonalInfo" style="margin-top: 10px">

                                    <div>
                                        <div class="col-lg-6">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <%--<h3 class="panel-title"><span id="panelHeader"></span></h3>--%>
                                                    <span style="text-align: left;" class="panel-title"><span id="panelHeader"></span></span>
                                                    <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/Employee1.html") %> ">
                                                        <b>
                                                            <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a></span>
                                                </div>
                                                <div class="panel-body ">

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Company
                                                        </div>
                                                        <div class="col-md-6">
                                                            <input type="hidden" id="hfCompanyId" name="hfCompany_id" value="0" />
                                                            <input type="hidden" id="hfSalaryLastDate" name="hfSalaryLastDate" />
                                                            <input type="hidden" id="hfAttendanceLastDate" name="hfAttendanceLastDate" />

                                                            <input type="hidden" id="hfEmpSalaryLastStartDate" name="hfEmpSalaryLastStartDate" />
                                                            <input type="hidden" id="hfDptLastEndDate" name="hfDptLastEndDate" />

                                                            <input type="hidden" id="hfEmpSalaryLastEndDate" name="hfEmpSalaryLastEndDate" />
                                                            <input type="hidden" id="hfDptLastStartDate" name="hfDptLastStartDate" />

                                                            <input type="hidden" id="hfEmpCode" name="hfEmpCode" value="0" />
                                                            <input type="hidden" id="hfEmpName" name="hfEmpName"  />

                                                            <%--<%: Html.HiddenFor(model=>model.hfEmpName) %>--%>

                                                            <%: Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList   , new {@class="form-control"})%>
                                                            <%: Html.ValidationMessageFor(model => model.CompanyId ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Emp Code
                                                        </div>
                                                        <div class="col-md-6">
                                                            <input type="hidden" class="form-control" id="EmployeeId" name="EmployeeId">
                                                            <%: Html.TextBoxFor(model => model.EmpCode, new { @class = "form-control", placeholder = "Enter Employee Code" })%>
                                                            <%: Html.ValidationMessageFor(model => model.EmpCode) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Employee Name
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.EmpName, new { @class = "form-control", placeholder = "Enter Employee Name" })%>
                                                            <%: Html.ValidationMessageFor(model => model.EmpName) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            First Name
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.FirstName, new { @class = "form-control", placeholder = "Enter First Name" })%>
                                                            <%: Html.ValidationMessageFor(model => model.FirstName ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Last Name
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.LastName , new { @class = "form-control", placeholder = "Enter Last Name" })%>
                                                            <%: Html.ValidationMessageFor(model => model.LastName  ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" style="height: 25px;">
                                                        <div class="col-lg-4 ">
                                                            Gender
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.DropDownListFor(model => model.Sex, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Male", Value="M"},
                                                                     new SelectListItem{Text="Female", Value="F"}}, "--Select--", new {@class="form-control" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Sex ) %>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                           Father/Husband Name
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.FatherHusbandName  , new { @class = "form-control", placeholder = "Enter Father's/Husband's Name" })%>
                                                            <%: Html.ValidationMessageFor(model => model.FatherHusbandName   ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Employee Relationship
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.DropDownListFor(model => model.Relationship, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Husband", Value="H"},
                                                                     new SelectListItem{Text="Father", Value="F"}}, "--Select--", new {@class="form-control" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Relationship ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Mother Name
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.MotherName   , new { @class = "form-control", placeholder = "Enter Mother Name" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MotherName    ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Date of Birth
                                                        </div>
                                                        <div class="col-md-6">


                                                            <%: Html.TextBoxFor(model => model.Dob   , new { @class = "form-control DOBPicker ", @readonly="readonly", placeholder = "Enter DOB" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Dob    ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Date of Joining
                                                        </div>
                                                        <div class="col-md-6">
                                                            <input type="hidden" id="Doj2" name="Doj2" />
                                                            <%: Html.TextBoxFor(model => model.Doj   , new { @class = "form-control Dojoining", @readonly="readonly", placeholder = "Enter DOJ" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Doj     ) %>
                                                        </div>
                                                    </div>


                                                    <div class="form-group" style="display: none;" id="dorid">
                                                        <div class="col-lg-4 ">
                                                            Date of Resingation Notice
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.Dor    , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOR" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Dor      ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group" style="display: none;" id="dolid">
                                                        <div class="col-lg-4 ">
                                                            Date of Leaving
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.Dol    , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOL" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Dol      ) %>
                                                        </div>
                                                    </div>

                                                   <div class="form-group" style="display: none;" id="roleavingid">
                                                        <div class="col-lg-4 ">
                                                            Reason for Leaving (PF)
                                                        </div>
                                                        <div class="col-md-6">
                                                               <%: Html.DropDownListFor(model => model.LeavingReasonId ,Model.LeavingReasonList   , new {@class="form-control"})%>
                                                            <%: Html.ValidationMessageFor(model => model.LeavingReasonId ) %>
                                                        </div>
                                                    </div>
                                                      <div class="form-group"  style="display: none;" id="esileavingDiv">
                                                        <div class="col-lg-4 ">
                                                            Reason for Leaving (ESI)
                                                        </div>
                                                        <div class="col-md-6">
                                                               <%: Html.DropDownListFor(model => model.ESILeavingReasonId ,Model.ESILeavingReasonList   , new {@class="form-control"})%>
                                                            <%: Html.ValidationMessageFor(model => model.ESILeavingReasonId ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Date of Retirement 
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.RetirementDate   , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOR" })%>
                                                            <%: Html.ValidationMessageFor(model => model.RetirementDate    ) %>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Company
                                                    </div>
                                                    <div class="col-md-8">
                                                        <%: Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList   , new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.CompanyId ) %>
                                                    </div>
                                                </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="tab-pane " id="tabCommunicationInfo" style="margin-top: 10px">

                                    <div>
                                        <div class="col-lg-6">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">Communication Info</h3>
                                                </div>
                                                <div class="panel-body ">
                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Salutation
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.DropDownListFor(model => model.TitleId ,Model.TitleList , new {@class="form-control"})%>
                                                            <%: Html.ValidationMessageFor(model => model.TitleId)%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Education
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.Education , new { @class = "form-control", placeholder = "Enter Education" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Education) %>
                                                        </div>
                                                    </div>



                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            International Worker
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.RadioButtonFor(model => model.IsInternationalWorker, "True",  new { @id="IsInternationalWorkerYes" })%>Yes &nbsp;
                                                                <%: Html.RadioButtonFor(model => model.IsInternationalWorker, "False", new { @id="IsInternationalWorkerNo" , @checked=true})  %>No 
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Mobile Number
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model =>model.MobileNo, new { @class = "form-control", placeholder = "Enter Mobile No."})%>
                                                            <%: Html.ValidationMessageFor(model => model.MobileNo) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Email Address 
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model =>model.EmailAddress , new { @class = "form-control", placeholder = "Enter Email Address"})%>
                                                            <%: Html.ValidationMessageFor(model => model.EmailAddress          ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Bank Name
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.DropDownListFor(model => model.EmpBankId, Model.BankList , new { @class="form-control"})%>
                                                            <%: Html.ValidationMessageFor(model => model.EmpBankId)%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Bank Acc. No
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model =>model.BankAccountNo , new { @class = "form-control",@maxlength=18, placeholder = "Enter Bank Account No."})%>
                                                            <%: Html.ValidationMessageFor(model => model.BankAccountNo) %>
                                                        </div>
                                                    </div>

                                                    <%--<div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            IFSC Code
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model =>model.BankIfscCode , new { @class = "form-control", placeholder = "Enter Bank IFSC Code"})%>
                                                            <%: Html.ValidationMessageFor(model => model.BankIfscCode ) %>
                                                        </div>
                                                    </div>--%>

                                                    <div class="form-group" style="display: none;">
                                                        <div class="col-lg-4 ">
                                                            GIS No. 
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model =>model.GisNo, new { @class = "form-control", placeholder = "Enter GIS No."})%>
                                                            <%: Html.ValidationMessageFor(model => model.GisNo ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Marital Status  
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.DropDownListFor(model => model.MaritalStatus, new List<SelectListItem> { 
                                                                         new SelectListItem{Text="WIDOWER", Value="1"},
                                                                         new SelectListItem{Text="MARRIED", Value="2"},
                                                                         new SelectListItem{Text="REMARRIED", Value="3"},
                                                                         new SelectListItem{Text="UNMARRIED", Value="4"},
                                                                         new SelectListItem{Text="WIDOW", Value="5"},
                                                                         new SelectListItem{Text="DIVORCEE", Value="6"}
                                                                      }, "--Select--", new {@class="form-control" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MaritalStatus ) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Work Division
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model => model.WorkDivision, new { @class = "form-control", placeholder = "Enter Work Division"})%>
                                                            <%: Html.ValidationMessageFor(model => model.WorkDivision) %>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-lg-4 ">
                                                            Notes   
                                                        </div>
                                                        <div class="col-md-6">
                                                            <%: Html.TextBoxFor(model =>model.Notes, new { @class = "form-control", placeholder = "Enter Notes"})%>
                                                            <%: Html.ValidationMessageFor(model => model.Notes) %>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="tab-pane " id="tabGenearlInfo" style="margin-top: 10px">
                                    <div class="col-lg-6">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">General Information</h3>
                                            </div>
                                            <div class="panel-body ">

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Trainee   
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.RadioButtonFor(model => model.Trainee , "True",  new { @id="rbTraineeYes"  })%>Yes &nbsp;
                                                        <%: Html.RadioButtonFor(model => model.Trainee , "False", new { @id="rbTraineeNo" , @checked=true})%>No
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Community  
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Community, new { @class = "form-control", placeholder = "Enter Community"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Community) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Caste   
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Caste , new { @class = "form-control", placeholder = "Enter Caste"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Caste) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Religion  
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Religion, new { @class = "form-control", placeholder = "Enter Religion"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Religion) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Nationality   
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Nationality, new { @class = "form-control", placeholder = "Enter Nationality"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Nationality) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Identfication Mark1  
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.IdenMks1, new { @class = "form-control", placeholder = "Enter First Identification Mark"})%>
                                                        <%: Html.ValidationMessageFor(model => model.IdenMks1) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Identfication Mark2 
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.IdenMks2, new { @class = "form-control", placeholder = "Enter Second Identification Mark"})%>
                                                        <%: Html.ValidationMessageFor(model => model.IdenMks2) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Height(in cm)  
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Height, new { @class = "form-control", placeholder = "Enter Height (in cm)"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Height) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Weight(in kg)
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Weight, new { @class = "form-control", placeholder = "Enter Weight (in kg)"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Weight) %>
                                                    </div>
                                                </div>

                                                <div class="form-group" style="display: none;">
                                                    <div class="col-lg-4 ">
                                                        Display Order 
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.EmpOrder , new { @class = "form-control", placeholder = "Enter Order"})%>
                                                        <%: Html.ValidationMessageFor(model => model.EmpOrder) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Blood Group 
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.BloodGroup , new { @class = "form-control", placeholder = "Enter Blood Group"})%>
                                                        <%: Html.ValidationMessageFor(model => model.BloodGroup) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Address1 
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Address1, new { @class = "form-control", placeholder = "Enter First Address"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Address1) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Address2 
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Address2, new { @class = "form-control", placeholder = "Enter Second Address"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Address2 ) %>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="tab-pane " id="tabPhotoInfo" style="margin-top: 10px">
                                    <div class="col-lg-6">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Photo Info</h3>
                                            </div>
                                            <div class="panel-body ">

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        City
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.City , new { @class = "form-control", placeholder = "Enter City"})%>
                                                        <%: Html.ValidationMessageFor(model => model.City) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        District
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.District , new { @class = "form-control", placeholder = "Enter District"})%>
                                                        <%: Html.ValidationMessageFor(model => model.District ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        State
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Region , new { @class = "form-control", placeholder = "Enter State"})%>
                                                        <%: Html.ValidationMessageFor(model => model.Region ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Pin Code
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.Pincode , new { @class = "form-control", placeholder = "Enter Pin Code."})%>
                                                        <%: Html.ValidationMessageFor(model => model.Pincode ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Attendance Required    
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.RadioButtonFor(model => model.IsAttendanceRequired, "True",  new { @id="rbAttendanceYes" })%>Yes &nbsp;
                                                        <%: Html.RadioButtonFor(model => model.IsAttendanceRequired, "False", new { @id="rbAttendanceNo" , @checked=true})  %>No 
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Reporting Officer     
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.DropDownListFor(model => model.MgrId ,Model.ReportingOfficerList   , new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.MgrId)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Pay Method    
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.DropDownListFor(model => model.PayMethodId ,Model.PayMethodList   , new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.PayMethodId)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Reservation Category     
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.DropDownListFor(model => model.ReservationCategory, new List<SelectListItem> { 
                                                                         new SelectListItem{Text="General", Value="1"},
                                                                         new SelectListItem{Text="BC", Value="2"},
                                                                         new SelectListItem{Text="SC", Value="3"},
                                                                         new SelectListItem{Text="ST", Value="4"},
                                                                         new SelectListItem{Text="Ex-Service Man", Value="5"}
                                                                      }, "--Select--", new {@class="form-control" })%>
                                                        <%: Html.ValidationMessageFor(model => model.ReservationCategory ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Handicap    
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.RadioButtonFor(model => model.IsHandicap, "True",  new { @id="rbIsHandicapYes" })%>Yes &nbsp;
                                                        <%: Html.RadioButtonFor(model => model.IsHandicap, "False", new { @id="rbIsHandicapNo" , @checked=true})%>No 
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Percentage  
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.HandicapPercent, new { @class = "form-control", @maxlength=5, placeholder = "Percentage."})%>
                                                        <%: Html.ValidationMessageFor(model => model.HandicapPercent ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Handicap Notes  
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.HandicapNotes, new { @class = "form-control",@maxlength=200, placeholder = "Notes."})%>
                                                        <%: Html.ValidationMessageFor(model => model.HandicapNotes) %>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <div class="tab-pane " id="tabDocumentInfo" style="margin-top: 10px">
                                    <div class="col-lg-6">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Photo Info</h3>
                                            </div>
                                            <div class="panel-body ">

                                                <div class="form-group" style="display: none;">
                                                    <div class="col-lg-4 ">
                                                        Shift
                                                    </div>
                                                    <div class="col-md-8">
                                                        <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList   , new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.ShiftId ) %>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Employee Type
                                                    </div>
                                                    <div class="col-md-6">
                                                        <input type="hidden" id="EmpType_Id" name="EmpType_Id" />
                                                        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeCategoryList   , new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Pf Account No
                                                    </div>
                                                    <div class="col-md-8">
                                                        <%: Html.TextBoxFor(model => model.PfAccountNo , new {@class="form-control", placeholder = "PF Account No."})%>
                                                        <%: Html.ValidationMessageFor(model => model.PfAccountNo ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Esi Account No
                                                    </div>
                                                    <div class="col-md-8">
                                                        <%: Html.TextBoxFor(model => model.EsiAccountNo , new {@class="form-control" , placeholder = "ESI Account No."})%>
                                                        <%: Html.ValidationMessageFor(model => model.EsiAccountNo ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Tds Account No
                                                    </div>
                                                    <div class="col-md-8">
                                                        <%: Html.TextBoxFor(model => model.TdsAccountNo , new {@class="form-control", placeholder = "Tds Account No."})%>
                                                        <%: Html.ValidationMessageFor(model => model.TdsAccountNo ) %>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Employee Category   
                                                    </div>
                                                    <div class="col-md-8">
                                                        <%: Html.DropDownListFor(model => model.EmpCategoryId ,Model.EmpCategoryList, new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.EmpCategoryId)%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-lg-4">
                                                        Attendance Source
                                                    </div>
                                                    <div class="Form-control col-lg-8 ">
                                                        <%: Html.DropDownListFor(model => model.AttendanceSourceId ,Model.AttendanceSourceList, new {@class="form-control"})%>

                                                        <%: Html.ValidationMessageFor(model => model.AttendanceSourceId)%>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        Upload Photo
                                                    </div>
                                                    <div class="col-md-8">
                                                        <input type="file" id="Path" class="form-control" name="file" onchange="onFileSelected(event)">
                                                        <input type="hidden" class="form-control" id="imagePath" name="imagePath">
                                                    </div>
                                                </div>

                                                <div style="height: 200px;">
                                                    <div style="width: 150px; height: 150px;" class="col-lg-offset-4" id="logoimage">
                                                        <img id="limage" class="img-thumbnail" onload="LoadImage(this);" />
                                                    </div>
                                                    <div style="width: 150px; height: 150px;" class="col-lg-offset-4" id="logoid">
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <div class="tab-pane " id="tabParameterInfo" style="margin-top: 10px;">
                                    <div class="col-lg-6">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Photo Info</h3>
                                            </div>
                                            <div class="panel-body ">

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Parameter ID
                                                    </div>
                                                    <div class="col-md-6">
                                                        <input type="hidden" class="form-control" id="EmpParameterId" name="EmpParameterId">

                                                        <%: Html.DropDownListFor(model => model.ParameterId ,Model.EmpParameterDetailList   , new {@class="form-control"})%>
                                                        <%: Html.ValidationMessageFor(model => model.ParameterId ) %>
                                                        <span class="field-validation-valid" style="color: #AF002A" data-valmsg-for="ParameterId" data-valmsg-replace="true" id="spanParameterId">Parameter Id is Required.</span>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                        Parameter Value
                                                    </div>
                                                    <div class="col-md-6">
                                                        <%: Html.TextBoxFor(model =>model.ParameterValue, new { @class = "form-control",@maxlength=30, placeholder = "Enter Parameter Value"})%>
                                                        <%: Html.ValidationMessageFor(model => model.ParameterValue) %>
                                                        <span class="field-validation-valid" style="color: #AF002A" data-valmsg-for="ParameterValue" data-valmsg-replace="true" id="spanParameterValue">Parameter Value is Required.</span>

                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-4 ">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <input type="button" id="btnParameterInsert" name="btnParameterInsert" class="form-control btn-success" value="Insert">
                                                        <input type="button" id="btnParameterUpdate" name="btnParameterUpdate" class="form-control btn-success" value="Update">
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-12">
                                                        <div class="panel panel-primary">
                                                            <div class="panel-heading">
                                                                <h3 class="panel-title">Employee Paramerter Details</h3>
                                                            </div>
                                                            <div class="panel-body ">
                                                                <div class="form-group">
                                                                </div>

                                                                <div id="dataParameter">
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>


                                <div class="col-lg-6">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Employee Info Details</h3>
                                        </div>
                                        <div class="panel-body ">
                                            <div class="form-group">
                                                <div class="col-lg-3">
                                                    <input type="button" id="btnLeft" name="btnLeft" class="btn btn-success" value="Left Employee">
                                                </div>
                                                <div class="col-lg-3">
                                                    <input type="button" id="btnExist" name="btnExist" class="btn btn-success" value="Employee Info ">
                                                </div>
                                            </div>


                                            <div id="data" style="overflow: auto;">
                                            </div>
                                            <br />
                                            <div id="dataEmpInfo" style="display: none;">
                                                <label>Employee Details</label>
                                                <div id="dataInfo" style="overflow: auto;">
                                                </div>
                                            </div>
                                            <br />
                                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>
        <div class="Loding" style="display: none;">
            <div id="popup" class="popup">
                <div class="content">
                    <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
                </div>
            </div>
        </div>
    </form>





















<div class="navbar navbar-inverse navbar-fixed-bottom">
    <div id="MsgDiv" style="display: none;">
        <label id="lblError"></label>
    </div>
</div>




