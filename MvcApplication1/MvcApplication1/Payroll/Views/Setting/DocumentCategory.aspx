<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.DocumentCategoryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DocumentCategory
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/DocumentCategory.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form method="post" id="DocumentCategory" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Document Category Details</h3>--%>
                            <span  style="text-align:left;"class="panel-title" >Document Category </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/DocumentCategory.html") %>">
                            <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> "/></b></a></span>
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addDocument" name="addDocument" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>
                            </div>
                             
                            <div id="data" style="overflow:auto; "> 
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
                                 Type
                                </div>
                                <div class="col-lg-9 ">
                                    <input type="hidden" id="DocumentCategoryId" name="DocumentCategoryId"/>
                                    <%: Html.TextBoxFor(model => model.DocumentCategoryName  , new {@class="form-control", placeholder="Enter Category Type"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentCategoryName)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                     Description
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DocumentCategoryDescription  , new {@class="form-control", placeholder="Enter Category Description"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentCategoryDescription)%>                 
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
