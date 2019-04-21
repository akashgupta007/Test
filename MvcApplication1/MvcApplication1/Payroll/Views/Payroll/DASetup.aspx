<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.DaRangeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Da Range View Model
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Payroll/DaRate1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <style>   
</style>
    <form method="post" id="DaRange" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                         
                            <span  style="text-align:left;"class="panel-title" >DA Rate</span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/UserMaster.html") %>">
                            <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> "/></b></a></span>
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addNew" name="addNew" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>DA Rate</button>
                                </div>
                            </div>

                            <div id="data" style="overflow: auto;">
                            </div>
                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
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
                                <div class="col-lg-3 ">
                                    Local Rate
                                </div>
                                <div class="col-lg-9 ">
                                    <input type="hidden" id="DaRangeId" name="DaRangeId" />
                                    <%: Html.TextBoxFor(model => model.LocalRate  , new {@class="form-control", @maxlength="14", placeholder="Enter Local Rate"})%>
                                    <%: Html.ValidationMessageFor(model => model.LocalRate)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Non Local Rate
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.NonLocalRate   , new {@class="form-control", @maxlength="14", placeholder="Enter Non Local Rate"})%>
                                    <%: Html.ValidationMessageFor(model => model.NonLocalRate )%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Location
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.LocationId, Model.LocationList , new {@class="form-control", placeholder="Location"})%>
                                    <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Designation
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DesignationId , Model.DesignationList , new {@class="form-control", placeholder="Designation"})%>
                                    <%: Html.ValidationMessageFor(model => model.DesignationId )%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Start Date
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.StartDate , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Enter Start Date"})%>
                                    <%: Html.ValidationMessageFor(model => model.StartDate)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    End Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.EndDate , new {@class="form-control DBPicker" , @readonly="readonly", placeholder="Enter End Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                </div>
                            </div>
                         
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    <input type="hidden" class="form-control" id="a" name="a">
                                </div>
                                <div class="col-lg-9">
                                    <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button type="button" id="btnClose" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                </div>
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
