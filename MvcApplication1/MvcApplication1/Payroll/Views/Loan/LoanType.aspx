<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.LoanTypeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LoanType
</asp:Content>      

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Loan/LoanType1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 



 <form id="LoanType" novalidate="novalidate">
        
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                          
                            
                             <span  style="text-align:left;"class="panel-title" > Loan Type </span>
                           
                             <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Loan/LoanType.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                        
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-2">
                                 <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Loan Type</button>
                                </div>

                            </div>
                            
                            <div id="data" style="overflow:auto; "> 
                            </div>

                            
                        </div>

                        <div style="text-align:center;margin-bottom:5px; ">

                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                       
                    </div>
                </div>

               

                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span>  </h3>
                        </div>
                        <div  class="panel-body ">


                               <div class="form-group">
                                <div class="col-lg-3 ">
                                Loan Type
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       <input type="hidden" id="LoanId" name="LoanId" />
                                    <%: Html.TextBoxFor(model => model.LoanName ,  new {@class="form-control", placeholder="Enter Loan Name"  })%>
                                    <%: Html.ValidationMessageFor(model => model.LoanName)%>
                                    
                                </div>
                            </div>  
                              
                           

                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Loan Code 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                       <%:Html.TextAreaFor(model => model.LoanDescription , new {@class="form-control", placeholder="Enter Loan Code"})%>
                                    <%: Html.ValidationMessageFor(model => model.LoanDescription)%>
                                    
                                </div>
                            </div>  
                              
                           
                           

                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Minimum Loan Amount 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       <%: Html.TextBoxFor(model => model.MinLoanAmount , new {@class="form-control", @maxlength="14", placeholder="Enter Minimum Loan Amount "})%>
                                    <%: Html.ValidationMessageFor(model => model.MinLoanAmount)%>
                                    
                                </div>
                            </div>  
                              
                           


                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Maximum Loan Amount 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.MaxLoanAmount , new {@class="form-control", @maxlength="14", placeholder="Enter  Maximum Loan Amount "})%>
                                    <%: Html.ValidationMessageFor(model => model.MaxLoanAmount)%>
                                    
                                </div>
                            </div> 
                            
                              
                             <div class="form-group">
                                <div class="col-lg-3 ">
                             Interest (%) 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.DefaultInterestPct , new {@class="form-control", placeholder="(%)"})%>
                                    <%: Html.ValidationMessageFor(model => model.DefaultInterestPct)%>
                                    
                                </div>
                            </div>  
                             
                              
                             <div class="form-group">
                                <div class="col-lg-3 ">
                            Loan Term  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.DefaultLoanTerm , new {@class="form-control",@maxlength="3",  placeholder="In Month"})%>
                                    <%: Html.ValidationMessageFor(model => model.DefaultLoanTerm)%>
                                    
                                </div>
                            </div>  
                                                        
                             

                          <div class="form-group">
                                <div class="col-lg-3 ">
                        Allow  Holiday Months
 
                                </div>
                              <%--<div class="col-lg-3">
                                    Month
                                    <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control",style = "width: 135px"})%>
                                    <%: Html.ValidationMessageFor(model => model.MonthId)%>
                                </div>
                                 <div class="col-lg-3">
                                   Year  
                               
                                
                                    <%: Html.DropDownListFor(model => model.YearId ,Model.YearList, new {@class="form-control",style = "width: 135px"})%>
                                    <%: Html.ValidationMessageFor(model => model.YearId)%>
                                </div>--%>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.HolidayMonths , new {@class="form-control",@maxlength="2", placeholder="Enter Holiday Months"})%>
                                     <%: Html.ValidationMessageFor(model => model.HolidayMonths)%>
                                    
                                </div>
                            </div>  
                              
                             <div class="form-group">
                                <div class="col-lg-3 ">
                          Allow Selecting Holiday Month(s) 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                     <%: Html.CheckBoxFor(model => model.SelectHolidayMonth , new {@id="SelectHolidayMonth"})%>                                       
                                    
                                </div>
                            </div>  

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Is Active
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.CheckBoxFor(model => model.IsActive , new { @id="IsActive", @checked="Checked", @value="True"})%>
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
