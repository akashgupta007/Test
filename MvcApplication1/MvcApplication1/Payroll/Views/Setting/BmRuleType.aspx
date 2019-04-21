<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.BmRuleViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BmRuleType
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/BmRuleType.js") %>"></script>    
    <script src="<%= Url.Content("~/Scripts/bootstrap.timepicker.min.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="BmRuleType" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 20px;">

            <div class="row">
                <div class="col-lg-12" style="margin-top: 20px;">

                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                        <span  style="text-align:left;"class="panel-title" > Deduction Rules  </span>                           
                         <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/BmRuleType1.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group" style="margin-top: 10px;">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>
                            </div>

                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                                <div class="panel-heading">
                                    <h3 class="panel-title" id="paneltitle"></h3>
                                </div>
                                <div class="panel-body ">
                                    <div class="col-lg-12">
                                        <input type="hidden" id="RuleId" name="RuleId" />
                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Group Rule Name
                                            <%: Html.TextBoxFor(model => model.RuleName , new {@class="form-control parent", placeholder="Enter Group Rule Name"})%>
                                            <%: Html.ValidationMessageFor(model => model.RuleName)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Rule Decription 
                                            <%: Html.TextBoxFor(model => model.RuleDescription , new {@class="form-control parent", placeholder="Enter Rule Decription"})%>
                                            <%: Html.ValidationMessageFor(model => model.RuleDescription)%>
                                        </div>

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                           <div>&nbsp;</div>
                                            <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                            <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClose" name="btnClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                        </div>

                                    </div>
                                </div>
                            </div>

                          
                            <div id="data" style="overflow:auto;">
                            </div>
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
                            <h3 class="panel-title" id="childpnlbar"></h3>
                        </div>
                        <div class="panel-body ">
                            <div class="col-lg-12 ">

                                <div class="form-group">
                                    <div class="col-md-2">Rule Description</div>
                                    <div class="col-md-4">
                                        <input type="hidden" id="RuleDetailId" name="RuleDetailId" />
                                        <%: Html.TextBoxFor(model => model.Description , new {@class="form-control child" , placeholder="Enter Description."})%>
                                        <%: Html.ValidationMessageFor(model => model.Description)%>
                                    </div>
                                    <div class="col-md-2">Start Time</div>
                                    <div class="col-md-4">
                                        <%: Html.TextBoxFor(model => model.MinTimeLateEntryAllowance , new {@class="form-control child time_picker input-small" ,@maxlength="8", placeholder="00:00:00"})%>
                                        <%: Html.ValidationMessageFor(model => model.MinTimeLateEntryAllowance)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2">End Time</div>
                                    <div class="col-md-4">      
                                        <%: Html.TextBoxFor(model => model.MaxTimeLateEntryAllowance  , new {@class="form-control child time_picker input-small",@maxlength="8", placeholder="00:00:00"})%>
                                        <%: Html.ValidationMessageFor(model => model.MaxTimeLateEntryAllowance)%>
                                    </div>
                                    <div class="col-md-2">Allowed Days in Month </div>
                                    <div class="col-md-4">
                                        <%: Html.TextBoxFor(model => model.AllowedLateEntryDays  , new {@class="form-control child",@maxlength="2",  placeholder="Enter Days in Month"})%>
                                        <%: Html.ValidationMessageFor(model => model.AllowedLateEntryDays)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <%--<div class="col-md-2">Deduction Type</div>
                                    <div class="col-md-4">      
                                        <%: Html.TextBoxFor(model => model.DeductionType  , new {@class="form-control child",@maxlength="2",  placeholder="Enter Deduction Type"})%>
                                        <%: Html.ValidationMessageFor(model => model.DeductionType)%>
                                    </div>--%>
                                    <div class="col-md-2">Deduction Value</div>
                                    <div class="col-md-4">
                                        <%: Html.TextBoxFor(model => model.DeductionValue  , new {@class="form-control child",@maxlength="9", placeholder="Enter Deduction Value"})%>
                                        <%: Html.ValidationMessageFor(model => model.DeductionValue)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2">IsActive</div>
                                    <div class="col-md-4">      
                                          <%: Html.DropDownListFor(model => model.IsActive, new List<SelectListItem> { 
                                                    new SelectListItem{Text="True", Value="True"},
                                                    new SelectListItem{Text="False", Value="False"}}, "--Select--", new {@class="form-control" , @id="IsActive"})%>
                                        <%: Html.ValidationMessageFor(model => model.IsActive ) %>       
                                    </div>
                                    <div class="col-md-2">    </div>
                                    <div class="col-md-4">
                                        <button style="margin-bottom: 2px; margin-right: 1px;" type="button" name="btnChildInsert" onclick='InsertChildRecord()' class="btn  btn-success childInsert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                        <button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success childUpdate"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                        <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
            </div>
        </div>

    </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>
