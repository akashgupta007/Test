<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.PayPercentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pay Percent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Payroll/PayPercent.js") %>"></script>
    <script src="<%=Url.Content("~/Scripts/DropDownList/jquery.multi-select.js")%>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>

    <form id="PayPercent" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">

                            <span style="text-align: left;" class="panel-title">Pay Percentage Details </span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Payroll/PayPercentage.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span> New Record  </button>
                                </div>
                            </div>
                            <div id="data" style="overflow: auto;">
                            </div>

                        </div>
                        <div style="text-align: center; margin-bottom: 5px;">
                            <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div class="panel-body ">

                            <input type="hidden" id="SalaryStartDate" />
                            <input type="hidden" id="SalaryEndDate" />

                            <input type="hidden" id="PayrollStartDate" />
                            <input type="hidden" id="PayrollEndDate" />


                            <div id="divDisable">

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Payroll Item: 
                                         <input type="hidden" id="PayPercentId" name="PayPercentId" />
                                    </div>

                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollItemId)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Computation Type
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.RadioButtonFor(model => model.ComputingType, "Percentage",  new {@class="ComputingType", @id="Percentage", @checked = "checked" })%> % &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.ComputingType, "Sum", new {@class="ComputingType", @id="Sum"}) %> Sum &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                         
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Computed By 
                                    </div>
                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.ComputingBy, new List<SelectListItem> {
                                               new SelectListItem{Text="Payroll Item",Selected=true, Value="Payroll Item"} ,
                                                new SelectListItem{Text="Gross", Value="Gross"},
                                                new SelectListItem{Text="CTC", Value="CTC"} 
                                         },"--Select--", new {@class="form-control" })%>
                                        <%: Html.ValidationMessageFor(model => model.ComputingBy ) %>
                                    </div>
                                </div>

                                <div class="form-group" id="PercentageDiv">
                                    <div class="col-lg-3">
                                        Percentage: 
                                    </div>

                                    <div class="Form-control col-lg-9">
                                        <%: Html.TextBoxFor(model => model.ItemPercent , new {@class="form-control",@maxlength="7",  placeholder="Enter Prcentage"})%>
                                        <%: Html.ValidationMessageFor(model => model.ItemPercent)%>
                                    </div>
                                </div>

                                <div class="form-group" id="divComputingItem">
                                    <div class="col-lg-3">
                                        Computing Payroll Items:
                                    </div>

                                    <div class="Form-control col-lg-9">
                                        <input type="hidden" id="ComputingItemList" name="ComputingItemList" />
                                        <select name="ComputingItemId" id="ComputingItemId" class="form-control" multiple="multiple">
                                            <option>--Select--</option>
                                        </select>
                                        <%--<%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@id="ComputingItem", @class="form-control"})%>  --%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Additional Amount: 
                                    </div>

                                    <div class="Form-control col-lg-9">
                                        <%: Html.TextBoxFor(model => model.AdditionalAmount , new {@class="form-control",@maxlength="7",  placeholder="Enter  Additional Amount"})%>
                                        <%: Html.ValidationMessageFor(model => model.AdditionalAmount)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Subtract From 
                                    </div>
                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.SubtractBy, new List<SelectListItem> {
                                               new SelectListItem{Text="Payroll Item", Value="Payroll Item"} ,
                                                new SelectListItem{Text="Gross", Value="Gross"},
                                                new SelectListItem{Text="CTC", Value="CTC"} 
                                         },"--Select--", new {@class="form-control" })%>
                                        <%: Html.ValidationMessageFor(model => model.SubtractBy ) %>
                                    </div>
                                </div>

                                <div class="form-group" style="display: none;" id="DivSubtractItem">
                                    <div class="col-lg-3">
                                        Subtract Item : 
                                    </div>

                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.SubtractPayrollItemId ,Model.SubtractPayrollItemList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.SubtractPayrollItemId)%>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Location: 
                                    </div>

                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Employee Type: 
                                    </div>

                                    <div class="Form-control col-lg-9">

                                        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Start Date:
                                    
                                    </div>
                                    <div class="Form-control col-lg-9 ">

                                        <%: Html.TextBoxFor(model => model.StartDate , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Enter Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDate)%>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    End Date:
                                    
                                </div>
                                <div class="Form-control col-lg-9 ">

                                    <%: Html.TextBoxFor(model => model.EndDate , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Enter End Date"})%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon glyphicon-remove-circle"></span> Close</button>
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
