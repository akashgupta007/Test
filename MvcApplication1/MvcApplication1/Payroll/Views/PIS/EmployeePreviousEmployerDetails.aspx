<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpPreviousEmployerDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeePreviousEmployerDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeePreviousEmployerDetails1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="EmployeePreviousEmployerDetails" method="post" novalidate="novalidate">
    
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                           
                            
                             <span  style="text-align:left;"class="panel-title" > Employee's Previous Employer   </span>      
                      <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/PIS/EmployeePreviousEmployerDetails.html") %>"><b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                      
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-5">
                                 <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Previous Employer Details </button>
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
                                    <input type="hidden" id="EmpPreviousEmployerDetailsId" name="EmpPreviousEmployerDetailsId" />
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>  

                          
                   
                 
                           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Company Name 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.CompanyName , new {@class="form-control", @maxlength="50", placeholder="Enter Company Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.CompanyName)%>
                                </div>
                            </div> 


                            <div class="form-group">
                                <div class="col-lg-3 ">
                               Company Address
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.CompanyAddress , new {@class="form-control",@maxlength="100", placeholder="Enter Company Address"})%>
                                    <%: Html.ValidationMessageFor(model => model.CompanyAddress)%>
                                </div>
                            </div> 


                            <div class="form-group">
                                <div class="col-lg-3 ">
                             PF Account No. 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.PfAccountno , new {@class="form-control",@maxlength="15", placeholder="Enter Pf Account no"})%>
                                    <%: Html.ValidationMessageFor(model => model.PfAccountno)%>
                                </div>
                            </div> 


                             <div class="form-group">
                                <div class="col-lg-3 ">
                             ESI  Account No. 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.EsiAccountno , new {@class="form-control", @maxlength="15",placeholder="Enter Esi Account no"})%>
                                    <%: Html.ValidationMessageFor(model => model.EsiAccountno)%>
                                </div>
                            </div> 
                            
                   
                    


                             <div class="form-group">
                                <div class="col-lg-3 ">
                               EPF Office Name  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.EpfOfficeName , new {@class="form-control",@maxlength="50", placeholder="Enter Epf Office Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.EpfOfficeName)%>
                                </div>
                            </div> 



                             <div class="form-group">
                                <div class="col-lg-3 ">
                               EPF Office Address 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.EpfOfficeAddress , new {@class="form-control",@maxlength="100", placeholder="Enter Epf Office Address"})%>
                                    <%: Html.ValidationMessageFor(model => model.EpfOfficeAddress)%>
                                </div>
                            </div> 

                
                            
                              <div class="form-group">
                                <div class="col-lg-3 ">
                            Date of Joining 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    
                                  <%: Html.TextBoxFor(model => model.Doj  , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter    Date of Joining " })%>
                                   <%: Html.ValidationMessageFor(model => model.Doj ) %>
                                </div>
                            </div> 



                             <div class="form-group">
                                <div class="col-lg-3 ">
                              Date Of Relieving
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    
                                  <%: Html.TextBoxFor(model => model.Dol  , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter   Date Of Leaving " })%>
                                   <%: Html.ValidationMessageFor(model => model.Dol ) %>
                                </div>
                            </div> 
                  


                             <div class="form-group">
                                <div class="col-lg-3 ">
                              Reason Of Relieving 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.ReasonOfLeaving , new {@class="form-control", placeholder="Enter Reason Of Leaving "})%>
                                    <%: Html.ValidationMessageFor(model => model.ReasonOfLeaving)%>
                                </div>
                            </div> 


                           <%--   <div class="form-group">
                                <div class="col-lg-3 ">
                             Employee Document 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                   
                                    <%: Html.TextBoxFor(model => model.EmpDocument , new {@class="form-control",@maxlength="20", placeholder="Enter  Employee Document  "})%>
                                    <%: Html.ValidationMessageFor(model => model.EmpDocument)%>
                                </div>
                            </div> --%>



                             
                           

                           

                             <div class="form-group">
                                <div class="col-lg-3 ">
                                Experience In Month
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    
                                  
                                    <%: Html.TextBoxFor(model => model.ExperienceMonths , new {@class="form-control",@maxlength="3", placeholder="Enter Experience in Months"})%>
                                    <%: Html.ValidationMessageFor(model => model.ExperienceMonths)%>
                                </div>
                            </div> 


                             <div class="form-group">
                                <div class="col-lg-3 ">
                              Is Last Company  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       
                                <%: Html.CheckBoxFor(model => model.IsLastCompany , new {@id="IsLastCompany"})%>
                                   
                                  
                            
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
