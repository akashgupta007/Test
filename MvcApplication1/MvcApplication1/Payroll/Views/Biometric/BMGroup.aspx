<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.BiometricGroupViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BMGroup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Biometric/BMGroup1.js") %>"></script>


    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form method="post" id="BiometricGroup" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                            
                            <span  style="text-align:left;"class="panel-title" > Biometric Group   </span>   
                             <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Biometric/BMGroup.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>                         
                            
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addBMGroup" name="addBMGroup" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>
                            </div>

                            <div id="data" style="overflow:auto;">
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
                                    Group Name 
                                </div>
                                <div class="col-lg-9 ">
                                    <input type="hidden" id="GroupId" name="GroupId"/>
                                    <%: Html.TextBoxFor(model => model.GroupName  , new {@class="form-control", placeholder="Enter Group Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.GroupName)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Department
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DepartmentId)%>                    
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Designation 
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DesignationId ,Model.DesginationList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DesignationId)%>
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="col-lg-3 ">
                                    Employee Type 
                                </div>
                                <div class="col-lg-9 ">
                                   <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                   <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                                </div>
                            </div>
                         
                             <div class="form-group">
                                <div class="col-lg-3 ">
                                    Location
                                </div>
                                <div class="col-lg-9 ">
                                   <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                   <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="col-lg-3 ">
                                    Group Description
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.GroupDescription  , new {@class="form-control", placeholder="Enter Group Description"})%>
                                    <%: Html.ValidationMessageFor(model => model.GroupDescription )%>
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
   <%-- </div>--%>


</asp:Content>

