        <%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeViewModel>" %>



        <asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
        Employee
        </asp:Content>

        <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/Employee11.js") %>"></script>
   

        <script type="text/javascript">
        $(document).ready(function (e) {
        if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
        urlBackRestrict();
        }
        });
        </script>
        <style>
            option:before {
                content: "☐ ";
            }

            option:checked:before {
                content: "☑ ";
            }
         
            #myTablefamily tr td{
                width:200px;
                padding-left:50px;
            }
            #myTablefamily input {
                width:200px;               
            }
           #myTableEmployement tr td {
                width:200px;
                padding-left:50px;
            }
         #myTableEmployement input{
                width:200px;               
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
                top: 15%;
                left: 15%;
                width: 60%;
                height: 30%;
                padding: 16px;
                border: 16px solid white;
                background-color: white;
                z-index: 1002;
                overflow: auto;
            }

            .panel-heading span {
                margin-top: -20px;
                font-size: 15px;
            }

            .row {
                margin-top: 10px;
                padding: 0 10px;
            }

            .clickable {
                cursor: pointer;
            }

	</style>
    
        <form method="post" id="EmployeeInfo" enctype="multipart/form-data">
       
        <div class="form-horizontal">
        <div class="container-fluid ">
        <div class="row-fluid">
        <input type="radio" name="view" value="0" checked="checked" id="templatemode" onclick="checkclick();">
        <span class="glyphicon glyphicon-user"></span>User Defined Format Entry &nbsp; &nbsp;
        <input type="radio" name="view" value="1" id="gridmode" onclick="checkclick();" />
        <span class="glyphicon glyphicon-download"></span>Upload from Excel
                   
        </div>
        <br />
        <div class="col-md-12 col-sm-12 col-xs-12" id="pnlerror">
        <div class="x_panel">
        <div class="x_title">
        <h2><SPAN>Success/ Error Information</SPAN></h2>
        <ul class="nav navbar-right panel_toolbox">
        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
        </li>                       
        <li><a class="close-link"><i class="fa fa-close"></i></a>
        </li>
        </ul>
        <div class="clearfix"></div>
        </div>
        <div class="x_content">
        <div class="row">                       
        <div class="panel-group"  >                                           
                
        <div id="collapseOnee" class="panel-collapse collapse in">
        <div class="panel-body">
        <div id="spara"></div>
        <div id="epara"></div>
        <div id="wpara"></div>                
        <div id="MsgDiv" style="display: none;">
        <div id="msgp"></div> 
        <label id="lblError"></label>
        </div>
        </div>
        </div>
        </div>               
        </div>
        </div>
        </div>
        </div>
                
        <div id="content">
        <div>
        <iframe id="ExcelFrame" style="display: none"></iframe>
        <div id="dataExport" style="display: none;"></div>
        </div>
                           
                           
        <div class="col-lg-12">                              
        <div class="panel panel-primary">                                                
        <div class="panel-heading">
        <h3 class="panel-title"><span id="panelHeader"></span></h3>
        <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/Employee1.html") %>  ">
        <img style="width:30px;height:20px;margin-top:-30px;padding-top:-30px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " />
        </a>
        
        </div>                                                
        <br />
        <center>
        <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success enabling"  onclick="SaveEmployee();"><span class="glyphicon glyphicon-plus"></span> Save </button>
        <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling" onclick="UpdateEmployee();"><span class="glyphicon glyphicon-pencil"></span>Update</button>
        <button type="button"  class="btn btn-warning enabling" onclick="clearAll();">Clear All</button>
        <br />
        </center> 
        <br /> 
            <div style="text-align: center">
                            <img class="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
            </div>                                            
        <div class="container">
        <div class="panel-group" id="accordion">                                              
        <div class="col-lg-12">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
        <i class="fa fa-pencil-square-o"></i>  Personal Details
        </a>
        </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in">
        <div class="panel-body">
        <div class="form-group">                                                       
                                                                                                                
        <div class="col-md-4"> Company    <%: Html.ValidationMessageFor(model => model.CompanyId ) %>
        <input type="hidden" id="hfCompanyId" name="hfCompany_id" value="0" />
        <input type="hidden" id="hfSalaryLastDate" name="hfSalaryLastDate" />
        <input type="hidden" id="hfAttendanceLastDate" name="hfAttendanceLastDate" />
        <input type="hidden" id="EmpProjectId" name="EmpProjectId" />
        <input type="hidden" id="hfEmpSalaryLastStartDate" name="hfEmpSalaryLastStartDate" />
        <input type="hidden" id="hfDptLastEndDate" name="hfDptLastEndDate" />
        <input type="hidden" id="hfEmpSalaryLastEndDate" name="hfEmpSalaryLastEndDate" />
        <input type="hidden" id="hfDptLastStartDate" name="hfDptLastStartDate" />
        <input type="hidden" id="hfEmpCode" name="hfEmpCode" value="0" />
        <input type="hidden" id="hfEmpName" name="hfEmpName" />
        <input type="hidden" class="form-control" id="EmployeeId" name="EmployeeId">
        <%: Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList   , new {@class="form-control"})%>                                                           
        </div>                                                        
        <div class="col-md-4">Emp Code <%: Html.ValidationMessageFor(model => model.EmpCode) %>

        <%: Html.TextBoxFor(model => model.EmpCode, new { @class = "form-control", placeholder = "Enter Employee Code" })%>
                                                            
        </div>
        <div class="col-md-1">
        &nbsp;Saturation <%: Html.DropDownListFor(model => model.TitleId ,Model.TitleList , new {@class="form-control",@style="width:100px"})%>
        </div>
                                              
        <div class="col-md-3">
        Employee Name  <%: Html.ValidationMessageFor(model => model.EmpName) %>
        <%: Html.TextBoxFor(model => model.EmpName, new { @class = "form-control", placeholder = "Full Name" })%>
                                                            
        </div>
        </div>
        <div class="form-group">
       <%-- <div class="col-md-4">
        First Name
        <%: Html.TextBoxFor(model => model.FirstName, new { @class = "form-control", placeholder = "Enter First Name" })%>
        <%: Html.ValidationMessageFor(model => model.FirstName ) %>
        </div>
                                                                 
        <div class="col-md-4">
        Last Name
        <%: Html.TextBoxFor(model => model.LastName , new { @class = "form-control", placeholder = "Enter Last Name" })%>
        <%: Html.ValidationMessageFor(model => model.LastName  ) %>
        </div>--%>
             <div class="col-md-4">
       Nationality
       <%: Html.DropDownListFor(model => model.Nationality, new List<SelectListItem> { 
        new SelectListItem{Text="Indian", Value="Indian"},
        new SelectListItem{Text="Others", Value="Others"}}, "--Select--", new {@class="form-control" })%>
        </div>
                                                                 
        <div class="col-md-4">
      
        Marital Status                                                          
        <%: Html.DropDownListFor(model => model.MaritalStatus, new List<SelectListItem> { 
        new SelectListItem{Text="Unmarried", Value="4"},
        new SelectListItem{Text="Married", Value="2"},
        new SelectListItem{Text="Divorced", Value="6"},
        new SelectListItem{Text="Widow", Value="5"},
        new SelectListItem{Text="Widower", Value="1"},                                                                        
        new SelectListItem{Text="Remarried", Value="3"},               
        }, "--Select--", new {@class="form-control" })%>
        </div>
        <div class="col-md-4">
        Sex 
           <%-- <%: Html.ValidationMessageFor(model => model.Sex ) %>--%>
        <%: Html.DropDownListFor(model => model.Sex, new List<SelectListItem> { 
        new SelectListItem{Text="Male", Value="M"},
        new SelectListItem{Text="Female", Value="F"}}, "--Select--", new {@class="form-control" })%>
                                                            
        </div>
        </div>                                                    
        <div class="form-group">
             <div class="col-md-4">  
        Employee Relationship     <%: Html.ValidationMessageFor(model => model.Relationship ) %>
        <%: Html.DropDownListFor(model => model.Relationship, new List<SelectListItem> { 
        new SelectListItem{Text="Husband", Value="H"},
        new SelectListItem{Text="Father", Value="F"}}, "--Select--", new {@class="form-control" })%>
                                                           
        </div>                                            
        <div class="col-md-4">
        Father/Husband Name    <%: Html.ValidationMessageFor(model => model.FatherHusbandName   ) %>
        <%: Html.TextBoxFor(model => model.FatherHusbandName  , new { @class = "form-control", placeholder = "Enter Father's/Husband's Name" })%>
                                                            
        </div>
                                                         
        
        <div class="col-md-4">
        Mother Name
        <%: Html.TextBoxFor(model => model.MotherName   , new { @class = "form-control", placeholder = "Enter Mother Name" })%>
        <%: Html.ValidationMessageFor(model => model.MotherName    ) %>
        </div>
        </div>
        <div class="form-group">
        <div class="col-md-4">
        Date of Birth     <%: Html.ValidationMessageFor(model => model.Dob    ) %>
        <%: Html.TextBoxFor(model => model.Dob   , new { @class = "form-control DOBPicker ", @readonly="readonly", placeholder = " DOB" })%>
                                                            
        </div>
        <div class="col-md-4">
        Date of Joining     <%: Html.ValidationMessageFor(model => model.Doj     ) %>
        <input type="hidden" id="Doj2" name="Doj2" />                                                            
        <%: Html.TextBoxFor(model => model.Doj   , new { @class = "form-control Dojoining", @readonly="readonly", placeholder = " DOJ" })%>
                                                            
        </div>
            <div class="col-md-4" ">
        Highest Education
            <%: Html.DropDownListFor(model => model.Sex, new List<SelectListItem> {
            new SelectListItem{Text=" Class I", Value=" Class I"},
            new SelectListItem{Text="Class II", Value=" Class II"},
            new SelectListItem{Text="Class III", Value="Class III"},
            new SelectListItem{Text="Class IV", Value="Class IV"},
            new SelectListItem{Text="Class V", Value="Class V"},
            new SelectListItem{Text="Class VI - IX", Value="VI - IX"},
            new SelectListItem{Text="MATRIX (CLASS X)", Value="(CLASS X)"},
            new SelectListItem{Text="CLASS XI", Value="CLASS XI"},
            new SelectListItem{Text="intermediate (CLASS XII)", Value="intermediate (CLASS XII)"},
            new SelectListItem{Text="Diploma", Value="Diploma"},
             new SelectListItem{Text="ITI", Value="ITI"},
            new SelectListItem{Text="Non-ITI", Value="Non-ITI"},
            new SelectListItem{Text="Graduate", Value="Graduate"},
            new SelectListItem{Text="Post Graduate", Value="Post Graduate"},
            new SelectListItem{Text="Phd.", Value="Phd."}
            },
            "--Select--", new {@class="form-control" })%>
        <%--<%: Html.TextBoxFor(model => model.Education , new { @class = "form-control", placeholder = "Enter Education" })%>--%>
        <%--<%: Html.ValidationMessageFor(model => model.Education) %>--%>
        </div>
        <div class="col-md-4" style="display: none;"id="doresid">
        Date of Resignation
        <%: Html.TextBoxFor(model => model.Dor    , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOR" })%>
        <%: Html.ValidationMessageFor(model => model.Dor      ) %>
        </div>
        </div>      
            <div class="form-group">
        <div class="col-md-4">
       Hobbies
        <%: Html.TextBoxFor(model => model.Hobbies , new { @class = "form-control", placeholder = "Enter Hobbies" }    )%>
                                                            
        </div>
        <div class="col-md-4">
       Visible Marks
               <%: Html.TextBoxFor(model => model.VisibleMark , new { @class = "form-control", placeholder = "Enter Visible marks" }    )%>
                                                 
        </div>
            <div class="col-md-4" ">
        Nick Name
        <%: Html.TextBoxFor(model => model.FirstName, new { @class = "form-control", placeholder = "Enter Name" })%>
        <%: Html.ValidationMessageFor(model => model.FirstName ) %>
        </div>
        <div class="col-md-4" style="display: none;"id="doresid">
        Date of Resignation
        <%: Html.TextBoxFor(model => model.Dor    , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOR" })%>
        <%: Html.ValidationMessageFor(model => model.Dor      ) %>
        </div>
        </div>                                              
        <div class="form-group" style="display: none;" id="dorid"> 
        <div class="col-md-4">
        Date of Leaving
        <%: Html.TextBoxFor(model => model.Dol    , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOL" })%>
        <%: Html.ValidationMessageFor(model => model.Dol      ) %>
        </div>  
        <div class="col-md-4">
        Reason for Leaving (PF)
        <%: Html.DropDownListFor(model => model.LeavingReasonId ,Model.LeavingReasonList   , new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.LeavingReasonId ) %>
        </div>
        <div class="col-md-4">
        Reason for Leaving (ESI)
        <%: Html.DropDownListFor(model => model.ESILeavingReasonId ,Model.ESILeavingReasonList   , new {@class="form-control"})%>
        </div>
        </div>
        <div class="form-group" style="display: none;" id="roleavingid">
        <div class="col-md-4">
        Date of Relieving 
        <%: Html.TextBoxFor(model => model.RetirementDate   , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter DOR" })%>
        <%: Html.ValidationMessageFor(model => model.RetirementDate    ) %>
        </div>
        </div>
                                                        
        </div>
        </div>
        </div>
        </div>
            <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapseFour" >
        <i class="fa fa-pencil-square-o"></i>  Assigning Employee
        </a>
        </h4>
        </div>
        <div id="collapseFour" class="panel-collapse collapse">
        <div class="panel-body">                                                   
        <div class="form-group" >
        <div class="col-lg-4 ">
        Employee Category                                                    
        <%: Html.DropDownListFor(model => model.EmpCategoryId ,Model.EmpCategoryList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmpCategoryId)%>
        </div>
                                                     
        <div class="col-lg-4 ">
        Employee Type                                                  
        <input type="hidden" id="EmpType_Id" name="EmpType_Id" />
        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeCategoryList   , new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
        </div>
        <div class="col-lg-4">
        Attendance Source                                                   
        <%: Html.DropDownListFor(model => model.AttendanceSourceId ,Model.AttendanceSourceList, new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.AttendanceSourceId)%>
        </div>
        </div> 
        <div class="form-group" >
        <div class="col-lg-4 ">
        Department    <%: Html.ValidationMessageFor(model => model.DepartmentId)%>                                                
        <input type="hidden" id="Department_Id" name="Department_Id"  class="form-control"/>
        <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                                       
        </div>
                                                     
        <div class="col-lg-4 ">
        Designation    <%: Html.ValidationMessageFor(model => model.DesignationId)%>                                             
        <input type="hidden" id="Designation_Id" name="Designation_Id" class="form-control"/>
        <%: Html.DropDownListFor(model => model.DesignationId ,Model.DesignationList, new {@class="form-control"})%>
                                                        
        </div>
        <div class="col-lg-4">
        Location   <%: Html.ValidationMessageFor(model => model.LocationId)%>                                              
        <input type="hidden" id="Location_Id" name="Location_Id"  class="form-control"/>
        <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                                      
        </div>
        </div>                                            
                                                
        <div class="form-group">  
        <div class="col-lg-4 ">
                                                                 
        Project    <%: Html.ValidationMessageFor(model => model.ProjectId)%>                                                  
        <input type="hidden" id="Project_Id" name="Project_Id" />
        <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                        
        </div>
        <div class="col-lg-4 " >
        Shift                                                    
        <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList   , new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.ShiftId ) %>

        </div>
                                                   
        <div class="col-lg-4 ">
        Pay Method                                                        
        <%: Html.DropDownListFor(model => model.PayMethodId ,Model.PayMethodList   , new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.PayMethodId)%>
        </div>
                                                   
                                                    
        </div>
                                               
        <div class="form-group">
                                                     
        <div class="col-lg-4 ">
        Reporting Officer                                                      
        <%: Html.DropDownListFor(model => model.MgrId ,Model.ReportingOfficerList   , new {@class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.MgrId)%>
        </div>
        <%--<div class="col-lg-4 ">
        Attendance Required 
        <br />                                                   
        <%: Html.RadioButtonFor(model => model.IsAttendanceRequired, "True",  new { @id="rbAttendanceYes" })%>Yes &nbsp;
        <%: Html.RadioButtonFor(model => model.IsAttendanceRequired, "False", new { @id="rbAttendanceNo" , @checked=true})  %>No 
        </div>--%>
        </div>
        </div>
        </div>
        </div>
        </div>                                           
        <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
        <i class="fa fa-pencil-square-o"></i>  Legal Information
        </a>
        </h4>
        </div>
        <div id="collapseTwo" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="p2">
        <div class="form-group">         
                 
        <div class="col-lg-4 ">
        EPF No                                                   
        <%: Html.TextBoxFor(model => model.PfAccountNo , new {@class="form-control", placeholder = "EPF No."})%>
        <%: Html.ValidationMessageFor(model => model.PfAccountNo ) %>
        </div>
        
         <div class="col-lg-4 ">
        ESIC No                                                    
        <%: Html.TextBoxFor(model => model.EsiAccountNo , new {@class="form-control" , placeholder = "ESIC No."})%>
        <%: Html.ValidationMessageFor(model => model.EsiAccountNo ) %>
        </div>
           <div class="col-lg-4 ">
        PAN No.                                                    
        <%: Html.TextBoxFor(model => model.TdsAccountNo , new {@class="form-control", placeholder = "Enter PAN No.",Maxlength="10"})%>
        <%: Html.ValidationMessageFor(model => model.TdsAccountNo ) %>
        </div>    
        </div>
        <div class="form-group">
        <div class="col-md-4" >
        Aadhaar Card Number
        <%: Html.TextBoxFor(model => model.AdharNo , new {@class="form-control", placeholder = "Enter Adhaar No.",Maxlength="12",Minlength="12"})%>
        <%: Html.ValidationMessageFor(model => model.AdharNo ) %>
        </div>
        
        <div class="col-lg-4 ">
        UAN No                                                    
        <%: Html.TextBoxFor(model =>model.UANNo, new { @class = "form-control", placeholder = "Enter UAN  No",Maxlength="12"})%>
        <%: Html.ValidationMessageFor(model => model.UANNo) %>
        </div>
        <div class="col-md-4" >
        Legal Copy
         <%: Html.TextBoxFor(model => model.LegalCopy , new {@class="form-control", placeholder = "Enter Legal No.",Maxlength="10"})%>
        <%: Html.ValidationMessageFor(model => model.LegalCopy ) %>
        </div>
        </div>
        <div class="form-group">
          <div class="col-md-4">
        Upload Photo                                                   
        <input type="file" id="Path" class="form-control" name="file" onchange="onFileSelected(event)">
        <input type="hidden" class="form-control" id="imagePath" name="imagePath">
        </div>   
        <div class="col-md-4" >
        Police Verification
        <%: Html.RadioButtonFor(model => model.IsPolice, "True",  new { @id="rbIsPoliceYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.IsPolice, "False", new { @id="rbIsPoliceNo" , @checked=true})%>No 
        </div>   
       
        <div class="col-lg-4 " id="rbIsPoliceNotes" style="display:none">
            Police verification No
        <%: Html.TextBoxFor(model =>model.poilceverification, new { @class = "form-control",placeholder = "police verification No "})%>
        <%: Html.ValidationMessageFor(model => model.poilceverification) %>
        </div>

        
            
        </div>
        <div class="form-group">
       
        
        
            
        <div class="col-lg-4 " style="display:none">
        GIS No.                                                         
        <%: Html.TextBoxFor(model =>model.GisNo, new { @class = "form-control", placeholder = "Enter GIS No."})%>
        <%: Html.ValidationMessageFor(model => model.GisNo ) %>
        </div>
        <div class="col-lg-4 " style="display:none">
        Work Division                                                      
        <%: Html.TextBoxFor(model => model.WorkDivision, new { @class = "form-control", placeholder = "Enter Work Division"})%>
        <%: Html.ValidationMessageFor(model => model.WorkDivision) %>
        </div>
        <div class="col-lg-4 " style="display:none">
        Marital Status                                                          
        <%: Html.DropDownListFor(model => model.MaritalStatus, new List<SelectListItem> { 
        new SelectListItem{Text="Unmarried", Value="4"},
        new SelectListItem{Text="Married", Value="2"},
        new SelectListItem{Text="Divorced", Value="6"},
        new SelectListItem{Text="Widow", Value="5"},
        new SelectListItem{Text="Widower", Value="1"},
                                                                        
        new SelectListItem{Text="Remarried", Value="3"},
                                                                        
                                                                         
                                                                        
        }, "--Select--", new {@class="form-control" })%>
        <%: Html.ValidationMessageFor(model => model.MaritalStatus ) %>
        </div>

        </div>     
        <div class="form-group">
            <div class="col-md-4">                                                        
        <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoimage">
        <img id="limage" class="img-thumbnail" onload="LoadImage(this);" />
        </div>
        <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoid">
        </div>                                                
        </div>
           
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>                                          
        
        <div class="col-lg-12" style="display:none;margin-top:5px">

        <div class="panel panel-primary" >
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapse1" >
        <i class="fa fa-pencil-square-o"></i>  General Information
        </a>
        </h4>
        </div>
        <div id="collapse1" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body" style="display:none">
                                                   
        <div class="form-group">
                                                    
                                                     
        <div class="col-lg-4 ">
        Community
        <%: Html.TextBoxFor(model =>model.Community, new { @class = "form-control", placeholder = "Enter Community"})%>
        <%: Html.ValidationMessageFor(model => model.Community) %>

        </div>
        <div class="col-lg-4 ">
        Caste                                                      
        <%: Html.TextBoxFor(model =>model.Caste , new { @class = "form-control", placeholder = "Enter Caste"})%>
        <%: Html.ValidationMessageFor(model => model.Caste) %>
        </div>
        </div>                                                
        <div class="form-group">
        <div class="col-lg-4 ">
        Religion                                                      
        <%: Html.TextBoxFor(model =>model.Religion, new { @class = "form-control", placeholder = "Enter Religion"})%>
        <%: Html.ValidationMessageFor(model => model.Religion) %>
        </div>
        <div class="col-lg-4 ">
        Nationality  
                                                    
        <%: Html.TextBoxFor(model =>model.Nationality, new { @class = "form-control", placeholder = "Enter Nationality"})%>
        <%: Html.ValidationMessageFor(model => model.Nationality) %>
        </div>
        <div class="col-lg-4 ">
        Identification Mark                                                     
        <%: Html.TextBoxFor(model =>model.IdenMks1, new { @class = "form-control", placeholder = "Enter Identification Mark"})%>
        <%: Html.ValidationMessageFor(model => model.IdenMks1) %>
        </div>
        </div>                                               
        <div class="form-group">
        <div class="col-lg-4 ">
        Identification Mark 2                                                    
        <%: Html.TextBoxFor(model =>model.IdenMks2, new { @class = "form-control", placeholder = "Enter Identification Mark 2"})%>
        <%: Html.ValidationMessageFor(model => model.IdenMks2) %>
        </div>
        <div class="col-lg-4 ">
        Height                                                   
        <%: Html.TextBoxFor(model =>model.Height, new { @class = "form-control", placeholder = "in cm"})%>
        <%: Html.ValidationMessageFor(model => model.Height) %>
        </div>
        <div class="col-lg-4 ">
        Weight                                                    
        <%: Html.TextBoxFor(model =>model.Weight, new { @class = "form-control", placeholder = "in kg"})%>
        <%: Html.ValidationMessageFor(model => model.Weight) %>
        </div>
        </div>
                                                
        <div class="form-group">
        <div class="col-lg-4 ">
        Blood Group                                                     
        <%: Html.TextBoxFor(model =>model.BloodGroup , new { @class = "form-control", placeholder = "A +ve"})%>
        <%: Html.ValidationMessageFor(model => model.BloodGroup) %>
        </div>
                                                    
        <div class="col-lg-4 ">
        Trainee
        <%: Html.RadioButtonFor(model => model.Trainee , "True",  new { @id="rbTraineeYes"  })%>Yes &nbsp;
        <%: Html.RadioButtonFor(model => model.Trainee , "False", new { @id="rbTraineeNo" , @checked=true})%>No
        </div>
                                                   
        </div>
        <div class="form-group" style="display: none;">
        <div class="col-lg-4 ">
        Display Order                                                     
        <%: Html.TextBoxFor(model =>model.EmpOrder , new { @class = "form-control", placeholder = "Enter Order"})%>
        <%: Html.ValidationMessageFor(model => model.EmpOrder) %>
        </div>
                                                     
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        <div class="col-lg-12" style="margin-top:5px">

        <div class="panel panel-primary" >
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapse5" >
        <i class="fa fa-pencil-square-o"></i>  Bank Information
        </a>
        </h4>
        </div>
        <div id="collapse5" class="panel-collapse collapse">
        <div class="panel-body">
            <div class="form-group">
        <div class="col-lg-4">
        Bank Name                                                        
        <%: Html.DropDownListFor(model => model.EmpBankId, Model.BankList , new { @class="form-control"})%>
        <%: Html.ValidationMessageFor(model => model.EmpBankId)%>
        </div>
                                                        
        <div class="col-lg-4">
        Bank A/C No                                                       
        <%: Html.TextBoxFor(model =>model.BankAccountNo , new { @class = "form-control",@maxlength=18, placeholder = "Enter Bank Account No."})%>
        <%: Html.ValidationMessageFor(model => model.BankAccountNo) %>
        </div>
        <div class="col-lg-4">
        Bank IFSC Code                                                     
        <%: Html.TextBoxFor(model =>model.BankIfscCode , new { @class = "form-control",@maxlength=18, placeholder = "Enter IFSC Code."})%>
        <%: Html.ValidationMessageFor(model => model.BankIfscCode) %>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>        
        <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary" >
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapsepermannet" >
        <i class="fa fa-pencil-square-o"></i>  Permanent Address
        </a>
        </h4>
        </div>
        <div id="collapsepermannet" class="panel-collapse collapse">
        <div class="panel-body">
       
        <div class="form-group">
        <div class="col-lg-4 ">
        Address Line 1                                                    
        <%: Html.TextBoxFor(model =>model.Address1, new { @class = "form-control", placeholder = "Enter First Address"})%>
        <%: Html.ValidationMessageFor(model => model.Address1) %>
        </div>                                                   
        <div class="col-lg-4 ">
        Address Line 2                                                    
        <%: Html.TextBoxFor(model =>model.Address2, new { @class = "form-control", placeholder = "Enter Second Address"})%>
        <%: Html.ValidationMessageFor(model => model.Address2 ) %>
        </div>
        <div class="col-lg-4 ">
        District                                                    
        <%: Html.TextBoxFor(model =>model.District , new { @class = "form-control", placeholder = "Enter District"})%>
        <%: Html.ValidationMessageFor(model => model.District ) %>
        </div>
                                                        
        </div> 
        <div class="form-group">
        <div class="col-lg-4 ">
        City                                                    
        <%: Html.TextBoxFor(model =>model.City , new { @class = "form-control", placeholder = "Enter City"})%>
        <%: Html.ValidationMessageFor(model => model.City) %>
        </div>
                                                    
        <div class="col-lg-4 ">
        State
                                                    
        <%: Html.TextBoxFor(model =>model.Region , new { @class = "form-control", placeholder = "Ex: Uttar Pradesh "})%>
        <%: Html.ValidationMessageFor(model => model.Region ) %>
        </div>
        <div class="col-lg-4 ">
        Pin Code                                                   
        <%: Html.TextBoxFor(model =>model.Pincode , new { @class = "form-control", placeholder = "Enter Pin Code."})%>
        <%: Html.ValidationMessageFor(model => model.Pincode ) %>
        </div>
                                                        
        </div>
        <div class="form-group">    
        <div class="col-lg-4 ">
        Mobile Number
        <%: Html.TextBoxFor(model =>model.MobileNo, new { @class = "form-control", placeholder = "9999999999", maxlength="10"})%>
        <%: Html.ValidationMessageFor(model => model.MobileNo) %>
        </div>
        <div class="col-md-4">
        Email Address 
        <%: Html.TextBoxFor(model =>model.EmailAddress , new { @class = "form-control", placeholder = "Enter Email Address"})%>
        <%: Html.ValidationMessageFor(model => model.EmailAddress          ) %>
        </div>
            <div class="col-md-4">
        Emergency Contact
        <%: Html.TextBoxFor(model =>model.emergencycontact1 , new { @class = "form-control", placeholder = "Enter Email Address"})%>
        <%: Html.ValidationMessageFor(model => model.emergencycontact1          ) %>
        </div>
        </div>
        
        </div>
        </div>
        </div>
        </div>    
        <div class="col-lg-12" style="margin-top:5px"> 
        <input type="checkbox" name="sameaspermanent" value="sameaspermanent" style="margin-left:10px" />  Same as Permanent Address
                </div>         
        <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary" >
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapseres" >
        <i class="fa fa-pencil-square-o"></i>  Corrspondance Address
        </a>
        </h4>
        </div>
        <div id="collapseres" class="panel-collapse collapse">
        <div class="panel-body">
       
         <div class="form-group">
        <div class="col-lg-4 ">
        Address Line 1                                                    
        <%: Html.TextBoxFor(model =>model.AddressRes, new { @class = "form-control", placeholder = "Enter First Address"})%>
        <%: Html.ValidationMessageFor(model => model.AddressRes) %>
        </div>                                                   
        <div class="col-lg-4 ">
        Address Line 2                                                    
        <%: Html.TextBoxFor(model =>model.AddressRes2, new { @class = "form-control", placeholder = "Enter Second Address"})%>
        <%: Html.ValidationMessageFor(model => model.AddressRes2 ) %>
        </div>
        <div class="col-lg-4 ">
        District                                                    
        <%: Html.TextBoxFor(model =>model.DistrictRes , new { @class = "form-control", placeholder = "Enter District"})%>
        <%: Html.ValidationMessageFor(model => model.DistrictRes ) %>
        </div>
                                                        
        </div> 
        <div class="form-group">
        <div class="col-lg-4 ">
        City                                                    
        <%: Html.TextBoxFor(model =>model.CityRes , new { @class = "form-control", placeholder = "Enter City"})%>
        <%: Html.ValidationMessageFor(model => model.CityRes) %>
        </div>
                                                    
        <div class="col-lg-4 ">
        State
                                                    
        <%: Html.TextBoxFor(model =>model.RegionRes , new { @class = "form-control", placeholder = "Ex: Uttar Pradesh "})%>
        <%: Html.ValidationMessageFor(model => model.RegionRes ) %>
        </div>
        <div class="col-lg-4 ">
        Pin Code                                                   
        <%: Html.TextBoxFor(model =>model.PincodeRes , new { @class = "form-control", placeholder = "Enter Pin Code."})%>
        <%: Html.ValidationMessageFor(model => model.PincodeRes ) %>
        </div>
                                                        
        </div>
        <div class="form-group">    
        <div class="col-lg-4 ">
        Mobile Number
        <%: Html.TextBoxFor(model =>model.MobileNoRes, new { @class = "form-control", placeholder = "9999999999", maxlength="10"})%>
        <%: Html.ValidationMessageFor(model => model.MobileNoRes) %>
        </div>
        <div class="col-md-4">
        Email Address 
        <%: Html.TextBoxFor(model =>model.EmailAddressRes , new { @class = "form-control", placeholder = "Enter Email Address"})%>
        <%: Html.ValidationMessageFor(model => model.EmailAddressRes          ) %>
        </div>
            <div class="col-md-4">
        Emergency Contact
        <%: Html.TextBoxFor(model =>model.emergencycontact2 , new { @class = "form-control", placeholder = "Enter Email Address"})%>
        <%: Html.ValidationMessageFor(model => model.emergencycontact2          ) %>
        </div>
        </div>
        
      
        </div>
        </div>
        </div>
        </div>  
        <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapseDL">
        <i class="fa fa-pencil-square-o"></i>  Driving Licence Details
        </a>
        </h4>
        </div>
        <div id="collapseDL" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="DLDetails">
        <div class="form-group">         
                 
        <div class="col-lg-4 ">
        Licence No.                                                  
        <%: Html.TextBoxFor(model => model.drivinglicence , new {@class="form-control", placeholder = "Enter DL Number"})%>        
        </div>
        
         <div class="col-lg-4 ">
          Date of Issue     
        <%: Html.TextBoxFor(model => model.DLdateofIssue   , new { @class = "form-control DOBPicker ", @readonly="readonly", placeholder = "Issue date" })%>
        </div>
             <div class="col-md-4" >
       Validity
                <%: Html.TextBoxFor(model => model.DLdateofExpiry   , new { @class = "form-control DOBPicker ", @readonly="readonly", placeholder = "Expiry date" })%>

        </div>  
        </div>
         <div class="form-group">         
                 
        <div class="col-lg-4 ">
       Licence Type      
            <input type="hidden" id="ComputingItemList" name="ComputingItemList" />
                                    <input type="hidden" id="ComputingItemNameList" name="ComputingItemNameList" />
                                    <select name="ComputingItemId" id="ComputingItemId" class="form-control" multiple="multiple">                                           
      
    <option value="0">MCYL</option>
    <option value="1">LMV</option>
    <option value="2">HMV</option>
    <option value="3">CRANE</option>
    
</select>       
        </div>
        
         <div class="col-lg-4 ">
          Issue Authority    
        <%: Html.TextBoxFor(model => model.DLIssueAutority  , new { @class = "form-control", placeholder = "Enter Issue Autority" })%>
        </div>
            
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>  
            <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapsePhysical">
        <i class="fa fa-pencil-square-o"></i>  Physical Details
        </a>
        </h4>
        </div>
        <div id="collapsePhysical" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="PhysicalDetails">
        <div class="form-group">         
                 
        <div class="col-lg-4 ">
        Height(C.mtr/ft)                                                
        <%: Html.TextBoxFor(model => model.Height , new {@class="form-control", placeholder = "Enter Heisght "})%>        
        </div>
        
         <div class="col-lg-4 ">
          Weight (Kg.) 
        <%: Html.TextBoxFor(model => model.Weight    , new {@class="form-control", placeholder = "Enter Weight "})%>
        </div>
             <div class="col-md-4" >
       Colour Blindness
                <%: Html.TextBoxFor(model => model.ColorBlind    , new { @class = "form-control" })%>

        </div>  
        </div>
         <div class="form-group">         
                 
        <div class="col-lg-4 ">
             Wear Spectacles
                <%: Html.TextBoxFor(model => model.WearSpectacles    , new { @class = "form-control" })%>
    
        </div>
        
         <div class="col-lg-4 ">
          Suffer any Physical Disablity or sickness(T.B /B.P/ Heart etc.)    
        <%: Html.TextBoxFor(model => model.sufferphy  , new { @class = "form-control", placeholder = "Enter " })%>
        </div>
            
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>    
              <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapsePassport">
        <i class="fa fa-pencil-square-o"></i>  Passport Detailss
        </a>
        </h4>
        </div>
        <div id="collapsePassport" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="PassportDetails">
        <div class="form-group">         
                 
        <div class="col-lg-4 ">
        Passport No.                                                  
        <%: Html.TextBoxFor(model => model.passportNo , new {@class="form-control", placeholder = "Enter Passport Number"})%>        
        </div>
        
         <div class="col-lg-4 ">
          Date of Issue     
        <%: Html.TextBoxFor(model => model.PassportdateofIssue   , new { @class = "form-control DOBPicker ", @readonly="readonly", placeholder = "Issue date" })%>
        </div>
             <div class="col-md-4" >
       Validity
                <%: Html.TextBoxFor(model => model.passportdateofExpiry   , new { @class = "form-control DOBPicker ", @readonly="readonly", placeholder = "Expiry date" })%>

        </div>  
        </div>
         <div class="form-group">         
                 
        <div class="col-lg-4 ">
            Place of Issue
        <%: Html.TextBoxFor(model => model.PassportPlaceofissue , new { @class = "form-control", placeholder = "Enter Visible marks" }    )%>     
        </div>
        
         <div class="col-lg-4 ">
          Address as per Passport    
        <%: Html.TextBoxFor(model => model.PassportAddress  , new { @class = "form-control", placeholder = "Enter Issue Autority" })%>
        </div>
            
        </div>
        </div>
        </div>
        </div>
        </div>
        </div> 
            <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapselistTwoo">
        <i class="fa fa-pencil-square-o"></i>  Alcoholic Information
        </a>
        </h4>
        </div>
        <div id="collapselistTwoo" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="listTwoDetails">
        <div class="form-group">         
                 
        <div class="col-lg-4">
        Drink Alcoholic                                                
        <input type="hidden" id="ComputingItemList1" name="ComputingItemList1" />
                                    <input type="hidden" id="ComputingItemNameList1" name="ComputingItemNameList1" />
                                    <select name="ComputingItemId1" id="ComputingItemId1" class="form-control" multiple="multiple">
    <option value="1">Regularly</option>
    <option value="2">Weekly</option>
    <option value="3">Occasionally</option>
    <option value="4">No</option>
    
</select>          
        </div>
            <div class="col-lg-4">
        Chew Tobacco                                                
         <%: Html.RadioButtonFor(model => model.chewtab, "True",  new { @id="rbIschewtabYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.chewtab, "False", new { @id="rbIschewtabNo" , @checked=true})%>No 
                <br />
                Smoking                                               
          <%: Html.RadioButtonFor(model => model.smoking, "True",  new { @id="rbIssmokingYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.smoking, "False", new { @id="rbIssmokingNo" , @checked=true})%>No        
        </div>
            
        </div>
            
        </div>
        </div>
        </div>
        </div>
        </div>  
            <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapseRefreTwo">
        <i class="fa fa-pencil-square-o"></i>  List of two Reference other than Relative
        </a>
        </h4>
        </div>
        <div id="collapseRefreTwo" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="ReferTwoDetails">
        <div class="form-group">         
                 
        <div class="col-lg-6">
                   <h2>First Referenece</h2>                                
            <div class="col-lg-12">                 
                <div class="form-group">  
                 <div class="col-lg-4">
                     Name
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.RefName1 , new {@class="form-control", placeholder = "Enter Name"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Position
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refpos1 , new {@class="form-control", placeholder = "Enter position"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Company
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refcompany1 , new {@class="form-control", placeholder = "Enter Company"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Emp Code
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refempcode1 , new {@class="form-control", placeholder = "Enter employee code"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Address
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refadd1 , new {@class="form-control", placeholder = "Enter Address"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Mobile
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refmob1 , new {@class="form-control", placeholder = "Enter Mobile"})%>
                     </div>
                     </div>
                
             </div>
        </div>
            <div class="col-lg-6">
                 <h2>Second Referenece</h2>  
       <div class="col-lg-12">              
                 
                <div class="form-group">  
                 <div class="col-lg-4">
                     Name
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.RefName2 , new {@class="form-control", placeholder = "Enter Name"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Position
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refpos2 , new {@class="form-control", placeholder = "Enter position"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Company
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refcompany2 , new {@class="form-control", placeholder = "Enter Company"})%>
                     </div>
                     </div>
           <div class="form-group">  
                 <div class="col-lg-4">
                     Emp Code
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refempcode2 , new {@class="form-control", placeholder = "Enter employee code"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Address
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refadd2 , new {@class="form-control", placeholder = "Enter Address"})%>
                     </div>
                     </div>
                <div class="form-group">  
                 <div class="col-lg-4">
                     Mobile
                     </div>
                <div class="col-lg-8">
                    <%: Html.TextBoxFor(model => model.Refmob2 , new {@class="form-control", placeholder = "Enter Mobile"})%>
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
            <div class="col-lg-12" style="margin-top:5px" id="academicinfodiv">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapseAcademicQualification">
        <i class="fa fa-pencil-square-o"></i>  Academic Qualification Details
        </a>
        </h4>
        </div>
        <div id="collapseAcademicQualification" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="collapseAcademic">
        <div class="form-group">   
            <table id='data_TableInfo' style="width: 100%">
                                        <tr>
                                            <td>Examination Passed</td>
            <td>Name Of School</td>
            <td>Subjects </td>
                                            <td>Month of Passing</td>
            <td>Year of Passing</td>
            <td>Grade/%</td>
                                            <td></td>
                                        </tr>
                                        <tr class="appendableDIV">
                                            
                                            <td>
                                                <div class="form-group">
                                                     <%--<input type="hidden" id="EmpEducationId" name="EmpEducationId" />--%>
                                                      <%: Html.TextBoxFor(model => model.Education , new {@class="" , placeholder="Enter Education" })%>
                                                    <%--<%: Html.ValidationMessageFor(model => model.Education)%>--%>
                                                </div>
                                            </td>                                           
                                            
                                            <td>
                                                <div class="form-group">
                                                    <%: Html.TextBoxFor(model => model.UniversityName , new {@class="" , placeholder="Enter University" })%>
                                                    <%--<%: Html.ValidationMessageFor(model => model.UniversityName)%>--%>
                                                </div>
                                            </td>


                                            <td>
                                                <div class="form-group">

                                                  <%: Html.TextBoxFor(model => model.UniversityAddress, new {@class="" , placeholder="Enter University Address" })%>
                                                    <%--<%: Html.ValidationMessageFor(model => model.UniversityAddress)%>--%>
                                                </div>

                                            </td>
                                            <td>
                                                 <%: Html.DropDownListFor(model => model.EducationMonth ,Model.Monthlist  , new {@class=""})%>
                                      <%--<%: Html.ValidationMessageFor(model => model.EducationMonth)%>--%>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <%: Html.DropDownListFor(model => model.EducationYearE ,Model.YearList   , new {@class=""})%>
                                      <%--<%: Html.ValidationMessageFor(model => model.EducationYear)%>--%>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                   
                                                    <%: Html.TextBoxFor(model => model.EducationGrade, new {@class="" , placeholder="Enter  Grade" })%>
                                                    <%--<%: Html.ValidationMessageFor(model => model.EducationGrade)%>--%>
                                                </div>
                                            </td>
                                            
                                            <td id="dButton">
                                                <button class="btn btn-primary" type="button" onclick="addRow();"><i class="glyphicon glyphicon-plus"></i></button>
                                                <button class="btn btn-danger" type="button" onclick="removeRow(this);"><i class="glyphicon glyphicon-minus"></i></button>
                                                
                                            </td>
                                        </tr>

                                    </table>
                
                 
      
        </div>
        </div>
        </div>
        </div>                  
        </div>
                                    
        </div>
            <%--<div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseProfQualification">
        <i class="fa fa-pencil-square-o"></i>  Professional Qualification Details
        </a>
        </h4>
        </div>
        <div id="collapseProfQualification" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="collapseProfessional">
        <div class="form-group">         
                 
       <table id="myTablePro" class="order-listPro">
    <thead>
        <tr>
            <td>Examination Passed</td>
            <td>Name Of School</td>
            <td>Subjects </td>
            <td>Year of Passing</td>
            <td>Grade/%</td>
            
        </tr>
    </thead>
    <tbody>
        <tr>
            <td><input type="text" name="EmpEducationPro[]" id="EmpEducationPro"class="form-control"></td>
            <td><input type="text" name="UniversityNamePro[]" id="UniversityNamePro"class="form-control"> </td>
            <td><input type="text" name="UniversityAddressPro[]" id="UniversityAddressPro"class="form-control"></td>
            <td><input type="text" name="EducationYearPro[]" id="EducationYearPro"class="form-control">  </td>
            <td><input type="text" name="EducationGradePro[]" id="EducationGradePro"class="form-control"> </td>           
            <td><a class="deleteRow"></a></td>
        </tr>
    </tbody>
    <tfoot>
        <tr>
            <td colspan="5" style="text-align: left;"><button type="button" id="addrowPro" class="btn btn-warning"><span class="glyphicon glyphicon-plus"></span></button></td>
        </tr>
        <tr>
            <td colspan=""></span></td>
        </tr>
    </tfoot>
</table>
        </div>
        </div>
        </div>
        </div>                  
        </div>
                                    
        </div>--%>
            <div class="col-lg-12" style="margin-top:5px"id="familyinfodiv">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle"style="font-size:small" data-toggle="collapse" data-parent="#accordion" href="#collapseFM">
        <i class="fa fa-pencil-square-o"></i> Family Details
        </a>
        </h4>
        </div>
        <div id="collapseFM" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="collapsefamilymember">
        <div class="form-group">         
                 
       <table id="myTablefamily" class="order-listfamily">
    
        <tr>
            <td>Relation</td>
            <td>Name</td>
             <td>Age</td>
            <td>DOB</td>
             <td>Adhaar No</td>
        </tr>
    
        <tr>
            <td ><select name="familydetailsname[]">
    <option value="0" selected="selected">--Select--</option>
    <option value="5" >Father</option>
    <option value="6">Mother</option>
    <option value="1">Wife</option>
    <option value="2">Husband</option>
    <option value="3">Son</option>
    <option value="4">Daughter</option>
    <option value="8">Brother</option>
    <option value="7">Sister</option>
                <option value="9">Others</option>
</select></td>
            <td ><input type="text" name="FamilyMemberName[]" id="FamilyMemberName"class="form-control"> </td>
             <td ><input type="text" name="FamilyMemberAge[]" id="FamilyMemberAge"class="form-control"> </td>
            <td ><input  id="FamilyMemberDob" class="datepick" name="FamilyMemberDob[]" ></td>
            
             <td ><input type="text" name="FamilyMembeAdhaarNo[]" id="FamilyMembeAdhaarNo"class="form-control"> </td>
            <td ><a class="deleteRow"></a></td>
        </tr>
    
    <tfoot>
        <tr>
            <td colspan="5" style="text-align: left;"><button type="button" id="addrowfamily" class="btn btn-warning"><span class="glyphicon glyphicon-plus"></span></button></td>
        </tr>
        
    </tfoot>
</table>
        </div>
        </div>
        </div>
        </div>                  
        </div>
                                    
        </div>
            <div class="col-lg-12" style="margin-top:5px"id="employementinfodiv">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapseEmployement">
        <i class="fa fa-pencil-square-o"></i> Previous Employement Records
        </a>
        </h4>
        </div>
        <div id="collapseEmployement" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="collapseEmployementL">
        <div class="form-group">   
            <table id="myTableEmployement" class="order-listEmployement">
   
        <tr>
            <td>OrganizationName</td>
            <td>Organization Addess</td>
            <td>From </td>
            <td>TO</td>
            <td>Description</td>
            
        </tr>
    
        <tr>
            <td style="width:200px"><input type="text" name="CompanyName[]" id="CompanyName" class="form-control"></td>
            <td style="width:200px"><input type="text" name="CompanyAddress[]" id="CompanyAddress"class="form-control"> </td>
            <td style="width:200px"><input type="text" name="Doj[]" id="Doj"class="datepickf"></td>
            <td style="width:200px"><input type="text" name="Dol[]" id="Dol"class="datepickt">  </td>
            <td style="width:200px"><input type="text" name="ReasonOfLeaving[]" id="ReasonOfLeaving"class="form-control"> </td>           
            <td><a class="deleteRow"></a></td>
        </tr>
    
    <tfoot>
        <tr>
            <td colspan="5" style="text-align: left;"><button type="button" id="addrowEmployement" class="btn btn-warning"><span class="glyphicon glyphicon-plus"></span></button></td>
        </tr>
        
    </tfoot>
</table>      
                 
      
        </div>
        </div>
        </div>
        </div>                  
        </div>
                                    
        </div>
            <div class="col-lg-12" style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapseDS">
        <i class="fa fa-pencil-square-o"></i> Document Submission
        </a>
        </h4>
        </div>
        <div id="collapseDS" class="panel-collapse collapse">
        <div class="panel-body">
        <div class="panel-body " id="DSDetails">
        <div class="form-group">         
                 
        <div class="col-lg-6">
              <div class="form-group"> <div class="col-lg-12">
                  10th Certificate Copy
                 <%: Html.RadioButtonFor(model => model.tenC, "True",  new { @id="rbIs10CYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.tenC, "False", new { @id="rbIs10CYes" , @checked=true})%>No  
                 </div></div>      
             <div class="form-group"> <div class="col-lg-12">12th Certificate Copy
                  <%: Html.RadioButtonFor(model => model.tweleveC, "True",  new { @id="rbIstweleveCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.tweleveC, "False", new { @id="rbIstweleveCNo" , @checked=true})%>No  

                                      </div></div>
            <div class="form-group"> <div class="col-lg-12">Graduate Certificate Copy
                 <%: Html.RadioButtonFor(model => model.gradC, "True",  new { @id="rbIsgradCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.gradC, "False", new { @id="rbIsgradCNo" , @checked=true})%>No </div></div>
            <div class="form-group"> <div class="col-lg-12">Post Graduate Certificate Copy
                 <%: Html.RadioButtonFor(model => model.PGC, "True",  new { @id="rbIsPGCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.PGC, "False", new { @id="rbIsPGCNo" , @checked=true})%>No </div></div>
            <div class="form-group"> <div class="col-lg-12">Resident  Certificate Copy
                   <%: Html.RadioButtonFor(model => model.RPC, "True",  new { @id="rbIsRPCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.RPC, "False", new { @id="rbIsRPCNo" , @checked=true})%>No</div></div>
            <div class="form-group"> <div class="col-lg-12">Driving Licence Certificate Copy
                   <%: Html.RadioButtonFor(model => model.DLC, "True",  new { @id="rbIsDLCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.DLC, "False", new { @id="rbIsDLCNo" , @checked=true})%>No </div></div>
            <div class="form-group"> <div class="col-lg-12">Identity Certificate Copy
                   <%: Html.RadioButtonFor(model => model.ICC, "True",  new { @id="rbIsICCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.ICC, "False", new { @id="rbIsICCNo" , @checked=true})%>No</div></div>                               
            
        </div>
          <div class="col-lg-6">
              <div class="form-group"> <div class="col-lg-12">Passport Copy
                   <%: Html.RadioButtonFor(model => model.PassC, "True",  new { @id="rbIsPassCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.PassC, "False", new { @id="rbIsPassCNo" , @checked=true})%>No</div></div>      
             <div class="form-group"> <div class="col-lg-12">Last Company Joing
                 <%: Html.RadioButtonFor(model => model.LCJC, "True",  new { @id="rbIsLCJCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.LCJC, "False", new { @id="rbIsLCJCNo" , @checked=true})%>No</div></div>
            <div class="form-group"> <div class="col-lg-12">profssional certificate Copy
                 <%: Html.RadioButtonFor(model => model.ProfC, "True",  new { @id="rbIsProfCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.ProfC, "False", new { @id="rbIsProfCNo" , @checked=true})%>No</div></div>
            <div class="form-group"> <div class="col-lg-12">PAN Copy
                 <%: Html.RadioButtonFor(model => model.PanC, "True",  new { @id="rbIsPanCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.PanC, "False", new { @id="rbIsPanCNo" , @checked=true})%>No</div></div>
            <div class="form-group"> <div class="col-lg-12">Bank Passbok Copy
                <%: Html.RadioButtonFor(model => model.bankC, "True",  new { @id="rbIsbankCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.bankC, "False", new { @id="rbIsbankCNo" , @checked=true})%>No</div></div>
            <div class="form-group"> <div class="col-lg-12">Adhaar Copy
                <%: Html.RadioButtonFor(model => model.AdharC, "True",  new { @id="rbIsAdharCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.AdharC, "False", new { @id="rbIsAdharCNo" , @checked=true})%>No</div></div>
            <div class="form-group"> <div class="col-lg-12">Police /Pradhaan Copy
                   <%: Html.RadioButtonFor(model => model.PoliceC, "True",  new { @id="rbIsPoliceCYes" })%>Yes &nbsp;
       <%: Html.RadioButtonFor(model => model.PoliceC, "False", new { @id="rbIsPoliceCNo" , @checked=true})%>No</div></div>                               
            
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>               
        </div>
            <br />                         
        </div>
       
        </div>   
            
             <br />
        <center>
                                                   
        <%--<button type="button" id="newEmployee" name="newEmployee" class="btn btn-primary" onclick="AddEmployee(this);"><span class="glyphicon glyphicon-plus" ></span>New Employee</button>--%>
        <button type="button" id="btnInsert1" name="btnInsert" class="btn btn-success enabling"  onclick="SaveEmployee();"><span class="glyphicon glyphicon-plus"></span> Save</button>
        <button type="button" id="btnUpdate1" name="btnUpdate" style="display: none;" class="btn btn-success enabling" onclick="UpdateEmployee();"><span class="glyphicon glyphicon-pencil"></span>Update</button>
        <button type="button"  class="btn btn-warning enabling" onclick="clearAll();">Clear All</button>
        <%--<img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />--%>
        <br />
        </center>
        <br />  
            <div style="text-align: center">
                            <img class="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
            </div> 
        </div>    
        <div class=""style="margin-top:5px">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h4 class="panel-title">
        <a class="accordion-toggle" style="font-size:small"data-toggle="collapse" data-parent="#accordion" href="#collapsesix" >
        <b style="color:red">List Of Employee's
        </a>
        </h4>
        </div>
        <div id="collapsesix" class="panel-collapse collapse">
        <div class="panel panel-primary">                                       
        <div class="panel-body ">
        <div class="form-group">
        <div class="col-lg-1">
        <input type="button" id="btnLeft" name="btnLeft" class="btn btn-primary enabling" value="Ex-Employee">
        </div>
        <div class="col-lg-1">
        <input type="button" id="btnExist" name="btnExist" class="btn btn-success enabling" value="Current Employee  ">
        </div>
        <div class="col-lg-1">
        <input type="button" id="btnallEmp" name="btnallEmp" class="btn btn-warning enabling" value="Total Employee  ">
        </div>
        </div>


        <div id="data" style="overflow: auto;">
        </div>
        <br />
        <div id="dataEmpInfo" style="display: none;">
        <label>Employee Information </label>
        <div id="dataInfo" style="overflow: auto;">
        </div>
        </div>
        <br />
        <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="Command" value="Excel"><span class="glyphicon glyphicon-export"></span>Excel</button>
        <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="Command" value="Word"><span class="glyphicon glyphicon-export"></span>Word</button>
        </div>
        </div>
        </div>
        </div>
        </div>
                                                     
        </div>
       
                                                                      
        </div>    
            <div id="uploadPanel" style="display: none">
        <%--<form method="post" enctype="multipart/form-data" id="downlaodexcel"></form>--%>
       
        <table class="table table-striped table-bordered table-hover table-responsive table-condensed"style="width:80%">
        <tr>
        <td> <button type="submit" id="btnDownload" name="command" value="DownloadExcelFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span> Download Excel</button></td>
        <td> Import Excel File : </td>
        <td>  <input name="PathExcel" id="PathExcel" type="file" tabindex="3" onchange="onFileSelectedtype(event)"/></td>                            
        <td> <button type="button" id="UploadFile" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Employee Information</button> </td>
        </tr>
        </table>
      
        
        
        </div>
        <div class="tab-pane " id="tabParameterInfo" style="margin-top: 10px;display:none">
        <div class="col-lg-12">
        <div class="panel panel-primary">
        <div class="panel-heading">
        <h3 class="panel-title">Photo Information</h3>
        </div>
        <div class="panel-body ">

        <div class="form-group">
        <div class="col-lg-4">
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
                   
        </div>
        <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
        <div class="content">                           
        <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
        </div>
        </div>
        </div>
         </div>
        </form>                                             
   
                                                       
   
            </b>                                             
   
                                                       
   
        </asp:Content>
