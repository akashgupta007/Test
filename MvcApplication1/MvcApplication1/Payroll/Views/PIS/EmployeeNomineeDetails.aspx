<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpNomineeDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeNomineeDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeeNomineeDetails1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 
 
    <form id="EmployeeNomineeDetails" method="post" novalidate="novalidate">
    
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                           
                   <span  style="text-align:left;"class="panel-title" > Employee Nominee   </span>      
                    <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/PIS/EmployeeNomineeDetail.html") %>"><b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                      
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-3">
                                 <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Employee Nominee</button>
                                </div>
                               
                            </div>
                            
                            <div id="data" style="overflow:auto; "> 
                            </div>
                            
                           <div id="Download" style="display:none;">
                         <div class="form-group" style="margin-left: 10px;  margin-bottom: 10px; margin-top:10px;">
               
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                        </div></div>
                            
                        </div>

                        <div style="text-align:center;margin-bottom:5px; ">

                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                       
                    </div>
                </div>






               

                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span> </h3>
                        </div>
                        <div  class="panel-body ">


                               <div class="form-group">
                                <div class="col-lg-3 ">
                                Employee Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="NomineeId" name="NomineeId" />
                                    <%: Html.DropDownListFor(model => model.EmployeeId  ,Model.EmployeeList   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>                     
       

      
        
          
           
           

                           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Nominee Name 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.NomineeName , new {@class="form-control", placeholder="Enter Nominee Name "  })%>
                                    <%: Html.ValidationMessageFor(model => model.NomineeName)%>
                                </div>
                            </div> 

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                Nominee Address 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                  <%: Html.TextBoxFor(model => model.NomineeAddress  , new { @class = "form-control", placeholder = "Enter Nominee Address  " })%>
                                   <%: Html.ValidationMessageFor(model => model.NomineeAddress ) %>
                                </div>
                            </div> 


                                      
           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                                 Nominee Date of Birth 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                      <%: Html.TextBoxFor(model => model.NomineeDob  , new { @class = "form-control DBPicker_order ", @readonly="readonly", placeholder = "Enter   Date Of Birth " })%>
                                   <%: Html.ValidationMessageFor(model => model.NomineeDob ) %>
                                    
                                      
                                 
                                </div>
                            </div>


       
                            
                               <div class="form-group">
                                <div class="col-lg-3 ">
                                 Nominee Relationship 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                     <%: Html.DropDownListFor(model => model.NomineeRelationId ,Model.Relationlist   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.NomineeRelationId)%>
                                </div>
                            </div> 



                            <div class="form-group">
                                <div class="col-lg-3 ">
                               Nominee Sex 
                                </div>
                                <div class="Form-control col-lg-9 ">


                                    <%: Html.DropDownListFor(model => model.NomineeSex, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Male", Value="M"},
                                                                     new SelectListItem{Text="Female", Value="F"}}, "--Select--", new {@class="form-control" })%>
                                                                <%: Html.ValidationMessageFor(model => model.NomineeSex ) %>
                                   
                                </div>
                            </div> 
                               
          
                              <div class="form-group">
                                <div class="col-lg-3 ">
                                Marital Status  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.MaritalStatus, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Single", Value="Single"},
                                                                     new SelectListItem{Text="Married", Value="Married"}}, "--Select--", new {@class="form-control" })%>
                                                                <%: Html.ValidationMessageFor(model => model.MaritalStatus ) %>

                                   
                                   
                                </div>
                            </div> 



                                <div class="form-group">
                                <div class="col-lg-3 ">
                                 Share Amount
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.ShareAmount , new {@class="form-control",  placeholder="Enter Share Amount"})%>
                                    <%: Html.ValidationMessageFor(model => model.ShareAmount)%>
                                </div>
                            </div>
             


                             <div class="form-group">
                                <div class="col-lg-3 ">
                                Is Nominee Minor 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                   <%: Html.RadioButtonFor(model => model.IsMinor, "True",  new { @id="IsFamilyDependentyes" })%>Yes &nbsp;
                                  <%: Html.RadioButtonFor(model => model.IsMinor, "False", new { @id="IsFamilyDependentno" , @checked=true})  %>No 

                                </div>
                            </div>     


                               <div class="form-group">
                                <div class="col-lg-3 ">
                               Guardian Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.GuardianName , new {@class="form-control", placeholder="Enter Guardian Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.GuardianName)%>
                                </div>
                            </div> 


                               <div class="form-group">
                                <div class="col-lg-3 ">
                              Guardian Address
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.GuardianAddress , new {@class="form-control", placeholder="Enter Guardian Address"})%>
                                    <%: Html.ValidationMessageFor(model => model.GuardianAddress)%>
                                </div>
                            </div> 


                            
                             <div class="form-group">
                                <div class="col-lg-3 ">
                             Guardian Relationship
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                      <%: Html.DropDownListFor(model => model.GuardianRelationId ,Model.Relationlist   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.GuardianRelationId)%>
                                    
                                  
                                </div>
                            </div> 
                            

        
           
           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                             Gender
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.GuardianSex, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="Male", Value="M"},
                                                                     new SelectListItem{Text="Female", Value="F"}}, "--Select--", new {@class="form-control" })%>
                                                                <%: Html.ValidationMessageFor(model => model.GuardianSex ) %>
                                     
                                    
                                </div>
                            </div> 
          


                               <div class="form-group">
                                <div class="col-lg-3 ">
                              Guardian Date Birth
                                </div>
                                <div class="Form-control col-lg-9 ">

                                     <%: Html.TextBoxFor(model => model.GuardianDob  , new { @class = "form-control DBPicker_order ", @readonly="readonly", placeholder = "Enter   Date Of Birth " })%>
                                   <%: Html.ValidationMessageFor(model => model.GuardianDob ) %>
                                   
                                  
                                </div>
                            </div> 


                             <div class="form-group">
                                <div class="col-lg-3 ">
                              Is Nominate for widow pension 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                   <%: Html.RadioButtonFor(model => model.IsNominateForWidowPension, "True",  new { @id="IsFamilyDependentyes" })%>Yes &nbsp;
                                  <%: Html.RadioButtonFor(model => model.IsNominateForWidowPension, "False", new { @id="IsFamilyDependentno" , @checked=true})  %>No 

                                </div>
                            </div>  
          



                            
                            <div class="form-group">
                                <div class="col-lg-3 ">
                               Remarks 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    
                                       
                                  <%: Html.TextBoxFor(model => model.Remarks  , new { @class = "form-control", placeholder = "Enter  Remarks " })%>
                                   <%: Html.ValidationMessageFor(model => model.Remarks ) %>
                                </div>
                            </div>       
                            
                            
                            
                              <div class="form-group">
                                <div class="col-lg-3 ">
                           Employee  Document 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                     <%: Html.TextBoxFor(model => model.EmpDocumentId  , new { @class = "form-control", @maxlength="10",  placeholder = "Enter   Employee  Document Id " })%>
                                   <%: Html.ValidationMessageFor(model => model.EmpDocumentId ) %>
                                   
                                </div>
                            </div>                                                             
                                                        
                                        
                                                  
                             

                                                       
                                      
                           
                           
                           
                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                                </div>

                            </div>
                              <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>


                        </div>
                    </div>
                </div>

            </div>


        </div>
    </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>

</asp:Content>
