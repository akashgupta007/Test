<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.PayrollItemViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Payroll Item
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Payroll/PayrollItem.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>

    <form id="PayrollItem" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">

                            <span style="text-align: left;" class="panel-title">Payroll Item List</span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Payroll/DASetup.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record  </button>
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

                            <div id="divDisable">

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Payroll Item
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <input type="hidden" id="PayrollItemId" name="PayrollItemId" />
                                        <%: Html.TextBoxFor(model => model.PayrollItemDesc, new {@class="form-control", placeholder="Enter Item Type "})%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollItemDesc)%>
                                    </div>
                                </div>

                                <div class="form-group" hidden="hidden">
                                    <div class="col-lg-3">
                                        Payroll Type 
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <select id="PayrollTypeId" name="PayrollTypeId" class="form-control">
                                            <option value="1" selected="selected">Standard</option>
                                            <option value="2">Overtime</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Payroll Item Type 
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.PayrollItemTypeId ,Model.PayrollItemTypeList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.PayrollItemTypeId)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                       Is Override
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.RadioButtonFor(model => model.PayrollItemOverridable, "True",  new { @id="ovrYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.PayrollItemOverridable, "False", new { @id="ovrNo", @checked = "checked" }) %>No
                                                   
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3">
                                        Proportionate Pay 
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.RadioButtonFor(model => model.PropotionatePay, "True",  new { @id="prnYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.PropotionatePay, "False", new { @id="prnNo", @checked = "checked" }) %>No
                 
                                    </div>
                                </div>

                                <div class="form-group" style="display: none;">
                                    <div class="col-lg-3">
                                        Employee Type 
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                    </div>
                                </div>

                                <div class="form-group" style="display: none;">
                                    <div class="col-lg-3">
                                        Employee
                                    </div>
                                    <div class="Form-control col-lg-9">
                                        <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-lg-3">
                                    Notes
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.TextBoxFor(model => model.PayrollItemNotes, new {@class="form-control", placeholder="Enter Notes "})%>
                                    <%: Html.ValidationMessageFor(model => model.PayrollItemNotes)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3">
                                    Display Sequence
                                </div>
                                <div class="Form-control col-lg-9">

                                    <%: Html.TextBoxFor(model => model.DisplayOrder, new {@class="form-control",@maxlength =2, placeholder="Enter Display Order "})%>
                                    <%: Html.ValidationMessageFor(model => model.DisplayOrder)%>
                                </div>
                            </div>

                            <div class="form-group" >
                                <div class="col-lg-3">
                                    Additional Earning
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.CheckBoxFor(model => model.IsVariablePay)%>
                                    <%: Html.ValidationMessageFor(model => model.IsVariablePay)%>
                                </div>
                            </div>

                            <div class="form-group" id="idIsTax" style="display:none;">
                                <div class="col-lg-3">
                                    Is Tax 
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.CheckBoxFor(model => model.IsTaxApplicable)%>
                                    <%: Html.ValidationMessageFor(model => model.IsTaxApplicable)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
