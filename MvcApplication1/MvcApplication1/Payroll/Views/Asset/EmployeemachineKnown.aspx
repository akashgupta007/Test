<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeemachineKnown>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Fleet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/EmployeemachineKnown.js") %>"></script>
     <link href="<%= Url.Content("~/Scripts/select2.css") %>" rel="stylesheet" />
    <script src="<%= Url.Content("~/Scripts/select2.js") %>"></script> 
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            } $("#EmployeeId").select2();
        });
    </script>
    

    <form id="machineemployeknown" novalidate="novalidate">

        <div class="form-horizontal">

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary" id="EditPanel">
                        <div class="panel-heading">
                            <h3 class="panel-title">Employee Machine Information</h3>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-lg-3">
                                    <input type="hidden" id="EMKId" name="EMKId" />
                                    Operator

                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>

                                    <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                                <div class="col-lg-3">
                                    Machine
                                        <input type="hidden" id="ComputingItemList" name="ComputingItemList" />
                                    <input type="hidden" id="ComputingItemNameList" name="ComputingItemNameList" />
                                    <select name="ComputingItemId" id="ComputingItemId" class="form-control" multiple="multiple">
                                        <option>--Select--</option>
                                    </select>
                                </div>

                                
                                <%--<div class="col-lg-3">
                                    <input type="hidden" id="EMKId" name="EMKId" />
                                   Machine No  <%: Html.ValidationMessageFor(model => model.AssetId)%>
                                    <%: Html.DropDownListFor(model => model.AssetId ,Model.AssetFleetlist, new {@class="form-control"})%>
                                   

                                    
                                </div>
                                <div class="col-lg-3">
                                    Employee's
                                        <input type="hidden" id="ComputingItemList" name="ComputingItemList" />
                                    <input type="hidden" id="ComputingItemNameList" name="ComputingItemNameList" />
                                    <select name="ComputingItemId" id="ComputingItemId" class="form-control" multiple="multiple">
                                        <option>--Select--</option>
                                    </select>
                                </div>--%>
                                <div class="col-lg-3">
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



                    <div style="text-align: center; margin-bottom: 5px;">

                        <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>


                </div>
                <div class="col-lg-12" style="margin-top: 5px">

                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                        <b>Show All</b>
                    </a>

                    <div id="collapseFour" class="panel-collapse collapse">
                        <div id="data" style="overflow-x: auto;">
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
