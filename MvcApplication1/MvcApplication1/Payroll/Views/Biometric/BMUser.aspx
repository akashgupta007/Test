<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.BiometricUserViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    BMUser
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Biometric/BMUser.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <script src="../../../../Scripts/ui.dropdownchecklist-1.4-min.js"></script>
    <form method="post" id="BiometricUser" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                            <span  style="text-align:left;"class="panel-title" >Biometric User </span>          
                             <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Biometric/BMUser.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>                  
                          
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addBMUser" name="addBMUser" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
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
                                    Employee 
                                </div>
                                <div class="col-lg-9 ">
                                    <input type="hidden" id="BmUserId" name="BmUserId"/>
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    User 
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.ParentUserId ,Model.UserList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.ParentUserId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Group 
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.GroupId ,Model.GroupList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.GroupId)%>
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="col-lg-3 ">
                                    Notes 
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.Notes  , new {@class="form-control", placeholder="Enter Notes"})%>
                                    <%: Html.ValidationMessageFor(model => model.Notes )%>
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
