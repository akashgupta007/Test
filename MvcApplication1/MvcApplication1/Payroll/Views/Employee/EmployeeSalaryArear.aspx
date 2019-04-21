

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpSalaryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Salary Arrear
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeSalaryArear1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 
    <%--   <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/Test.js") %>"></script>
    --%>
    <form id="EmployeeSalaryArear" novalidate="novalidate">


        <div class="form-horizontal" style="margin-top: 20px;">

            <div class="row">
                <div class="col-lg-12" style="margin-top: 20px;">

                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                           <span  style="text-align:left;"class="panel-title" >Employee Salary Arrear </span>
                            
                            <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmplloyeeArrearProcess.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>
                        </div>
                        <div class="panel-body ">


                            <div class="form-group" style="margin-top: 10px;">
                                <div class="col-md-2">
                                 
                                      <button type="button" id="ExportData" name="ExportData" style="display:none;" class="btn btn-info"><span class="glyphicon glyphicon-export"></span> Export To Excel</button>
                                            <div><iframe id="ExcelFrame" style="display:none"></iframe></div>
                                    <div id="dataExport" style="display:none;" ></div>
                                </div>

                            </div>


                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Employee Salary Details</h3>
                                </div>
                                <div class="panel-body ">
                                    <div class="col-lg-12">

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Employee Name               
                                       <input type="hidden" id="EmpSalaryId" name="EmpSalaryId" />
                                               <input type="hidden" id="EmpId" name="EmpId" />
                                            <input type="hidden" id="rateflag" name="rateflag" />
                                            <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control Parent" })%>
                                            <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                        </div>



                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Gross
                                        <%: Html.TextBoxFor(model => model.Gross , new {@class="form-control Parent",@maxlength="18", @readonly="readonly", placeholder="Enter Gross"})%>
                                            <%: Html.ValidationMessageFor(model => model.Gross)%>
                                        </div>




                                          <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Increment Amount
                                        <%: Html.TextBoxFor(model => model.ArearAmount , new {@class="form-control Parent", placeholder="Enter Amount"})%>
                                            <%: Html.ValidationMessageFor(model => model.ArearAmount)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Start Date
                              
                                   <%: Html.TextBoxFor(model => model.StartDt  , new {@class="form-control DBPicker Parent", @readonly="readonly", placeholder="Select Start Date"})%>
                                            <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            End Date
                            
                                 <%: Html.TextBoxFor(model => model.EndDt, new {@class="form-control DBPicker Parent", @readonly="readonly", placeholder="Select End Date"})%>
                                            <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                        </div>
                                     
                                         <div class="form-group col-lg-2" style="margin-left: 1px;">
                                        Notes
                            
                                 <%: Html.TextBoxFor(model => model.Notes, new {@class="form-control  Parent",@maxlength=100, placeholder="Max length is 100."})%>
                                            <%: Html.ValidationMessageFor(model => model.Notes)%>
                                        </div>
                                    </div>




                                    <div class="col-lg-12">

                                        <div class="col-lg-offset-4 col-lg-3">
                                              <button type="button" id="btnInsert" name="btnInsert" style="margin-bottom: 2px; margin-right: 1px; " class="btn btn-success "><span class="glyphicon glyphicon-plus"></span> Increment</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success "><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                            <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClose" name="btnClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-cirlce"></span> Close</button>
                                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                        </div>

                                    </div>
                                </div>
                            </div>



                            <div class="col-lg-12" style="margin-top: 20px;">
                                <div id="data" style="overflow-x: auto;">
                                </div>
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
                        <div class="panel-body ">
                            <div class="col-lg-6 ">
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Payroll Item
                                    </div>
                                    <div class="col-lg-9 ">
                                        <input type="hidden" id="EmpSalaryItemId" name="EmpSalaryItemId" />
                                        <input type="hidden" id="RateValue" name="RateValue" />
                                        <%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@class="form-control child"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollItemId)%>
                                    </div>
                                </div>

                             

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Start Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.StartDtItem  , new {@class="form-control salaryItemPicker child", @readonly="readonly",@maxlength=10 ,@id="StartDtItem", placeholder="Select Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDtItem)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                         Arear Effective Start Date  
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.PayStartDt  , new {@class="form-control salaryItemPicker child", @readonly="readonly" ,@maxlength=10 , placeholder="Select Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayStartDt)%>
                                    </div>
                                </div>


                                 <div class="form-group">
                                    <div class="col-lg-3 ">
                                       Difference Amount
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.ItemArearAmount , new {@class="form-control child", @readonly="readonly",@maxlength="18", placeholder="Enter Amount." })%>
                                        <%: Html.ValidationMessageFor(model => model.ItemArearAmount)%>

                                     
                                    </div>
                                </div>



                            </div>


                            <div class="col-lg-6 ">


                             


                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                      Amount
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.Rate , new {@class="form-control child" , placeholder="Enter Rate." })%>
                                        <%: Html.ValidationMessageFor(model => model.Rate)%>

                                     
                                    </div>
                                </div>

                              

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        End Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.EndDtItem, new {@class="form-control salaryItemPicker child", @readonly="readonly" ,@maxlength=10, placeholder="Select End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.EndDtItem)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Arear Effective End Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.PayEndDt, new {@class="form-control salaryItemPicker child", @readonly="readonly" ,@maxlength=10, placeholder="Select End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayEndDt)%>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                     Notes
                                    </div>
                                    <div class="col-lg-9 ">
                                       <%: Html.TextBoxFor(model => model.NotesItem, new {@class="form-control  Parent",@maxlength=100, placeholder="Max length is 100."})%>
                                            <%: Html.ValidationMessageFor(model => model.NotesItem)%>
                                    </div>
                                </div>
                                  

                            </div>












                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-4">
                                      <button type="button" name="btnChildInsert" onclick="InsertChildRecord()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success childInsert">Increement</button>
                                    <button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; " class="btn btn-success childUpdate">Update</button>
                                    <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success">Close</button>

                                    <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />

                                </div>
                            </div>

                        </div>
                    </div>



                </div>
            </div>

            <div class="col-lg-12">
                <div id="childData">
                </div>
                <div style="text-align:center;display:none;">
                    Total = &nbsp;<label id="lbltotal"></label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remaning Amount =
                    <label id="lblRemaning"></label>
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
