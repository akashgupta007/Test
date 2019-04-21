﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpDeptTestViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeDeptTest
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeeDepartmentTest1.js") %>"></script>

   
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="EmployeeDepartmentTest" method="post" novalidate="novalidate">

    
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                             <span  style="text-align:left;"class="panel-title" > Employee Department Test  </span>                          

                         <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/PIS/EmployeeDepartmentTest.html") %>"><b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                         
                          
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-4">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Employee Department Test</button>
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
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div  class="panel-body ">


                             <div class="form-group">
                                <div class="col-lg-3 ">
                                    Employee Name  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="EmpDeptTestId" name="EmpDeptTestId" />
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,  Model.EmployeeList   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>                             
                            
                            
                                             
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                Dept. Test Name 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DeptTestId  ,  Model.DeptTestlist   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.DeptTestId)%>
                                </div>
                            </div>
                           

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                 Reg. Number 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                  <%: Html.TextBoxFor(model => model.RegisterNumber , new {@class="form-control",@minlength="8", @maxlength="15", placeholder="Enter   Reg. Number  "})%>
                                    <%: Html.ValidationMessageFor(model => model.RegisterNumber)%>
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="col-lg-3 ">
                              Date of Passing 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                       <%: Html.TextBoxFor(model => model.PassedDate  , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter  Date of Passing  " })%>
                                   <%: Html.ValidationMessageFor(model => model.PassedDate ) %>
                                       
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                               Result Date 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                   <%: Html.TextBoxFor(model => model.ResultDate  , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Result Date " })%>
                                   <%: Html.ValidationMessageFor(model => model.ResultDate ) %>
                                </div>
                            </div>
                        
                         
                             
                            <div class="form-group">
                                <div class="col-lg-3 ">
                             Authority 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                     <%: Html.TextBoxFor(model => model.Authority  , new { @class = "form-control  ", placeholder = "Enter   Authority " })%>
                                   <%: Html.ValidationMessageFor(model => model.Authority ) %>
                                  
                                </div>
                            </div>
                              

                            

                           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                            Gazette Number 
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                     <%: Html.TextBoxFor(model => model.GazetteNumber  , new { @class = "form-control  ", @minlength="8", @maxlength="15", placeholder = "Enter Gazette Number " })%>
                                   <%: Html.ValidationMessageFor(model => model.GazetteNumber ) %>
                                  
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

