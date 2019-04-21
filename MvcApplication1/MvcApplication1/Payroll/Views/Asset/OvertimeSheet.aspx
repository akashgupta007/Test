<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.OvertimeViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateTax
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/overtime.js") %>"></script>
      <link href="<%= Url.Content("~/Scripts/select2.css") %>" rel="stylesheet" />
    <script src="<%= Url.Content("~/Scripts/select2.js") %>"></script>  
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
            $("#AssetId").select2();
            
        });
    </script>


    <form id="overtime" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title"></h3>
                            <span style="text-align: left;" class="panel-title">Asset Tax  </span>

                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
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
                            <%--<div class="form-group" >
                                <input type="hidden" id="otId" name="otId" />
                                <div class="col-lg-3 ">
                                    Employee
                                </div>
                                <div class="Form-control col-lg-9 ">
                                 
                                      <%: Html.DropDownListFor(model => model.EmployeeId ,Model.Employeelist  , new {@class="form-control" })%>
                                    <%: Html.ValidationMessageFor(model => model.CategoryId)%>
                                </div>
                            </div>--%>
                            <%-- <div class="form-group" >
                                <div class="col-lg-3 ">
                                    Category
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  <input type="hidden" id="otId" name="otId" />
                                      <%: Html.DropDownListFor(model => model.CategoryId ,Model.OTCategorylist  , new {@class="form-control", @onchange="FillDdlModelGet(this)" })%>
                                    <%: Html.ValidationMessageFor(model => model.CategoryId)%>
                                </div>
                            </div>--%>
                            <%-- <div class="form-group">
                                <div class="col-lg-3 ">
                                    Model
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <select name="ModelId" id="ModelId" class="form-control">
                                        <option>--Select--</option>
                                    </select>

                                </div>
                            </div> --%>
                            <%--<div class="form-group">
                                <div class="col-lg-3 ">
                                    Make
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <select name="MakeId" id="MakeId" class="form-control">
                                        <option>--Select--</option>
                                    </select>

                                </div>
                            </div>--%>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Fleet Number / Machine Number
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="otId" name="otId" />

                                    <%: Html.DropDownListFor(model => model.AssetId ,Model.MachineList  , new {@class="form-control" })%>
                                    <%: Html.ValidationMessageFor(model => model.AssetId)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Overtime rate
                                </div>
                                <div class="Form-control col-lg-9 ">

                                    <%:Html.TextBoxFor(model => model.Overtime , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Overtime)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Start Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.OtrateStartDate , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Start Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.OtrateStartDate)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                   End Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    
                                    <%: Html.TextBoxFor(model => model.OtrateEndDate , new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Start Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.OtrateEndDate)%>
                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span>Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>

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




    <div id="MsgDiv" style="display: none;">
        <label id="lblError"></label>


    </div>

</asp:Content>
