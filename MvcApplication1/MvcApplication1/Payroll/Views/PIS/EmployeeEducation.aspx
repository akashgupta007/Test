﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpEducationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeEducation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeeEducation.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

 <form id="PisEmpEducation" method="post" novalidate="novalidate">

    

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">

                   

                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                          
                             <span  style="text-align:left;"class="panel-title" > Employee Education  </span>                   

                         <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/PIS/EmployeeEducation.html") %>"><b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-3">
                                 <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Employee Education </button>
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
                                    <input type="hidden" id="EmpEducationId" name="EmpEducationId" />
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList  , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>  


                             <div class="form-group">
                                <div class="col-lg-3 ">
                                  Employee Education 
                                </div>
                                <div class="Form-control col-lg-9">
                                   
                                    <%: Html.TextBoxFor(model => model.Education , new {@class="form-control", placeholder="Enter   Employee Education "})%>
                                    <%: Html.ValidationMessageFor(model => model.Education)%>
                                </div>
                            </div>                             
                            


               
                              <div class="form-group">
                                <div class="col-lg-3 ">
                                  University 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    
                                    <%: Html.TextBoxFor(model => model.UniversityName , new {@class="form-control", placeholder="Enter University Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.UniversityName)%>
                                </div>
                            </div>
                           


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                   University Address 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                    <%: Html.TextBoxFor(model => model.UniversityAddress , new {@class="form-control", placeholder="Enter   University Address  "})%>
                                    <%: Html.ValidationMessageFor(model => model.UniversityAddress)%>
                                </div>
                            </div>        


                           
                              <div class="form-group">
                                <div class="col-lg-3 ">
                                  Month of Passing 
                                </div>
                                <div class="Form-control col-lg-9 ">                                  
                                   
                                 <%: Html.DropDownListFor(model => model.EducationMonth ,Model.Monthlist  , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EducationMonth)%>
                                </div>
                            </div>   
                            
                            
                             <div class="form-group">
                                <div class="col-lg-3 ">
                                 Year of Passing 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                       <%: Html.DropDownListFor(model => model.EducationYear ,Model.YearList   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EducationYear)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                               Grade/Division/Percentage 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                  <%: Html.TextBoxFor(model => model.EducationGrade   , new { @class = "form-control " , placeholder = "Enter  Grade/Division/Percentage  " })%>
                                    <%: Html.ValidationMessageFor(model => model.EducationGrade     ) %>
                                </div>
                            </div>                          
                                    
                            
                            <div class="form-group">
                                <div class="col-lg-3 ">
                               Employee Document 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                 <%: Html.TextBoxFor(model => model.EmpDocumentId   , new { @class = "form-control " ,@maxlength="8", placeholder = "Employee Document Id " })%>
                                    <%: Html.ValidationMessageFor(model => model.EmpDocumentId     ) %>
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
