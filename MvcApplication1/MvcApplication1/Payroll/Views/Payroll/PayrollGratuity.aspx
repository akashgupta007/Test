<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.PayrollGratuityViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PayrollGratuity
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Payroll/PayrollGratuity1.js") %>"></script>


    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>
    <form id="PayrollGratuity" method="post" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Payroll Gratuity  </span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Payroll/PTRange.html") %>">
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
                            <div class="form-group">
                                <div class="col-lg-3">
                                    Location
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.HiddenFor(model => model.PayrollGratuityId)%>
                                    <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                </div>
                            </div>
                           <div class="form-group">
                                <div class="col-lg-3">
                                   Payroll Item
                                </div>
                               <div class="Form-control col-lg-9">
                               <div class="form-group col-lg-3" >
                                       <br />
                                            <%: Html.CheckBoxFor(model => model.Is_Basic)%>  Basic
                                            <%: Html.ValidationMessageFor(model => model.Is_Basic)%>
                                        </div>
                             <div class="form-group col-lg-3" >
                                             <br />
                                            <%: Html.CheckBoxFor(model => model.Is_Hra)%>  HRA
                                            <%: Html.ValidationMessageFor(model => model.Is_Hra)%>
                                        </div>
                               <div class="form-group col-lg-3" >
                                        <br />
                                            <%: Html.CheckBoxFor(model => model.Is_Da)%>  DA
                                            <%: Html.ValidationMessageFor(model => model.Is_Da)%>
                                        </div></div>
                            </div>
                            <div class="form-group" >
                                <div class="col-lg-3">
                                  Completed Years 
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.TextBoxFor(model => model.WorkingDays, new {@class="form-control", placeholder="Total years worked "})%>
                                   
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    Multiply Value 
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.TextBoxFor(model => model.MultiplyValue, new {@class="form-control",@value=15, placeholder="Min present days in month "})%>
                                    <%: Html.ValidationMessageFor(model => model)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    Divide Value 
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.TextBoxFor(model => model.DivideValue, new {@class="form-control",@value=26, placeholder="Max present days in month"})%>
                                    <%: Html.ValidationMessageFor(model => model)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    Start Date
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.TextBoxFor(model => model.StartDate, new {@class="form-control dtpicker dtpicker",@readonly="readonly", placeholder="Enter Start Date "})%>
                                    <%: Html.ValidationMessageFor(model => model)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    End Date
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.TextBoxFor(model => model.EndDate, new {@class="form-control dtpicker dtpicker",@readonly="readonly", placeholder="Enter End Date "})%>
                                    <%: Html.ValidationMessageFor(model => model)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success "><span class="glyphicon glyphicon-picture"></span>Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success "><span class="glyphicon glyphicon-pencil"></span> Update</button>
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
