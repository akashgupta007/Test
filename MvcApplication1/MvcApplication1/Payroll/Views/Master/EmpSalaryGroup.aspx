<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpSalaryGroupViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Salary Group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Master/EmpSalaryGroup2.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 




    <form id="EmpSalaryGroup" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-12">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                 
                                          <span  style="text-align:left;"class="panel-title" > Salary Group Details  </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Master/SalaryGroups.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Salary Group</button>
                                </div>

                            </div>

                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div  class="panel-body ">

                               <div class="form-group col-lg-2" style="margin-left: 1px;">
                                    Group Name
                              </div>
                                 <div class="form-group col-lg-2" style="margin-left: 1px;">
                                    <input type="hidden" id="EmpSalaryGroupId" name="EmpSalaryGroupId" />
                                           <input type="hidden" id="PayrollStartDate" name="PayrollStartDate" />  
                                             <input type="hidden" id="PayrollEndDate" name="PayrollEndDate" />       
                                           <input type="hidden" id="SalaryStartDate" name="SalaryStartDate" />  
                                             <input type="hidden" id="SalaryEndDate" name="SalaryEndDate" />       
                                    <%: Html.TextBoxFor(model => model.EmpSalaryGroupName , new {@class="form-control parent", placeholder="Enter Group Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmpSalaryGroupName)%>
                                </div>
                           
                            

                            <div class="form-group">

                                 <div class="form-group col-lg-2" style="margin-left: 1px;">
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

                           <div id="data" style="overflow:auto; "> 
                            </div>

                            
                        </div>

                        <div style="text-align:center;margin-bottom:5px; ">

                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>

                       
                    </div>
                </div>

                
                    
        

            </div>


        </div>


        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
                <button type="button" name="new" onclick='ChildRecordReset()' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-plus'></span>New Record</button>
                <input type="hidden" id="flag" name="flag" />


            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Group Salary Item</h3>
                        </div>
                        <div class="panel-body ">
                            <div class="col-lg-6 ">
                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Payroll Item
                                    </div>
                                    <div class="col-lg-9 ">
                                        <input type="hidden" id="EmpSalaryGroupDetailId" name="EmpSalaryGroupDetailId" />
                                        <input type="hidden" id="PayrollItem_Id" name="PayrollItem_Id" />
                                        <%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@class="form-control child"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollItemId)%>
                                    </div>
                                </div>
                                   <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Payroll Value Type  
                                    </div>
                                    <div class="col-lg-9 ">

                                            <input type="hidden" id="PayrollValueType" name="PayrollValueType" />
                                        <%: Html.DropDownListFor(model => model.Payroll_ValueType, new List<SelectListItem> { 
                                                new SelectListItem{Text="Static Value", Value="1"},
                                                new SelectListItem{Text="Computed Value", Value="2"} ,
                                        new SelectListItem{Text="Payroll Function", Value="3"}  },"--Select--", new {@class="form-control child",@onchange="PayrollValueTypeChange()" })%>
                                        <%: Html.ValidationMessageFor(model => model.Payroll_ValueType ) %>
                                    </div>
                                </div>
                                <div class="form-group" id="paypercentageDiv" style="display:none;">
                                    <div class="col-lg-3 ">
                                       Pay Percentage 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.PayPercent, new {@class="form-control child" ,@maxlength=6, placeholder="Enter Pay Percentage"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayPercent)%>
                                    </div>
                                </div>
                                   <div class="form-group" id="AmountDiv" style="display:none;" >
                                    <div class="col-lg-3 ">
                                    Amount
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.Amount, new {@class="form-control child" ,@maxlength=16 , placeholder="Enter Amount"})%>
                                        <%: Html.ValidationMessageFor(model => model.Amount)%>
                                    </div>
                                </div>
                                   <div class="form-group" id="PayrollFunctionDiv"  style="display:none;">
                                    <div class="col-lg-3 ">
                                  Payroll Function
                                    </div>
                                    <div class="col-lg-9 ">
                                    
                                       
                                        <%: Html.DropDownListFor(model => model.FunctionId ,Model.FunctionList, new {@class="form-control child"})%>
                                        <%: Html.ValidationMessageFor(model => model.FunctionId)%>
                                    </div>
                                </div>   


                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Start Date 
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.StartDt  , new {@class="form-control  child", @readonly="readonly", placeholder="Select Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                    </div>
                                </div>

                                <div  class="form-group">
                                    <div class="col-lg-3 ">
                                        End Date  
                                    </div>
                                    <div class="col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.EndDt  , new {@class="form-control  child", @readonly="readonly", placeholder="Select End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                 <div class="col-lg-3">
                             
                                    </div>
                                <div class="col-lg-9"  style="text-align:left;">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" name="btnChildInsert" onclick='InsertChildRecord()' class="btn  btn-success childInsert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success childUpdate"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>

                                    <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />

                                </div>
                            </div>

                            </div>













                          

                        </div>
                    </div>



                </div>
            </div>

            <div class="col-lg-12">
                <div id="childData">
                </div>
                <div style="text-align: center; display: none;">
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
