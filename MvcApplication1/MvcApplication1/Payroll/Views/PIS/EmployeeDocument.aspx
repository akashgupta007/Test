<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeDocumentviewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeDocument
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeeDocument.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>
    <style type="text/css">
        .Loading {
            width: 100%;
            display: block;
            position: absolute;
            top: 0;
            left: 0;
            height: 100%;
            z-index: 999;
            background-color: rgba(0,0,0,0.5); /*dim the background*/
        }

        .content {
            background: #fff;
            padding: 28px 26px 33px 25px;
        }

        .popup {
            border-radius: 1px;
            background: #6b6a63;
            margin: 30px auto 0;
            padding: 6px;
            position: absolute;
            width: 100px;
            top: 50%;
            left: 50%;
            margin-left: -100px;
            margin-top: -40px;
        }
    </style>


    <form method="post" id="EmployeeDocument" novalidate="novalidate" enctype="multipart/form-data">

        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employee Document   </span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Reimbursement/EmployeeReimbursement.html") %>">
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
                                    Employee Name
                                </div>
                                <div class="Form-control col-lg-9 ">

                                    <input type="hidden" class="form-control" id="ErrorMsg" name="ErrorMsg" value="<%:(ViewBag.msg ?? String.Empty)%>">
                                    <input type="hidden" class="form-control" id="Flag" name="Flag" value="<%:(ViewBag.Flag ?? String.Empty)%>">



                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList   , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>

                            <div class="form-group" id="payroll_Itemid">
                                <div class="col-lg-3 ">
                                    Document Name 
                                </div>
                                <div class="Form-control col-lg-9">

                                    <%: Html.HiddenFor(model=>model.EmployeeDocumentId)%>
                                    <%: Html.HiddenFor(model=>model.strDocumentImg) %>
                                    <%: Html.HiddenFor(model=>model.strContentType) %>
                                    <%: Html.HiddenFor(model=>model.strDocumentName) %>
                                    <%: Html.DropDownListFor(model => model.DocumentId ,Model.DocumentList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentId)%>
                                </div>

                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Upload Document
                                </div>

                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.File  , new {type="File",@class="form-control", placeholder = "Select Document"})%>

                                    <%: Html.ValidationMessageFor(model => model.File)%>
                                </div>

                            </div>
                            <div class="form-group" id="DocumentDownload">
                                <div class="col-lg-3 ">
                                    Document Download
                                </div>

                                <div class="Form-control col-lg-9 ">

                                    <div id="divDownload" style="display: none;">
                                        <button type="submit" id="btnDownload" class="btn btn-success cancel" name="Command" value="Download"><span class='glyphicon glyphicon-download'></span> Download</button>
                                    </div>

                                </div>

                            </div>

                        

                        <div class="form-group">
                            <div class="col-lg-3 ">
                                   Notes
                            </div>
                            <div class="Form-control col-lg-9 ">
                                <%: Html.TextBoxFor(model => model.Notes  , new {@class="form-control", placeholder = "Enter Notes"})%>
                                <%: Html.ValidationMessageFor(model =>model.Notes)%>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-lg-offset-4 col-lg-3">

                                <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnDocInsert" name="Command" value="Insert" onclick='return  CheckValidation()' class="btn  btn-success"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                <button type="submit" id="btndocUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" name="Command" onclick='return  CheckValidation()' value="Update" class="btn btn-success"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                <button type="button" id="btnClear" name="btnClear" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                            </div>
                        </div>

</div>
                    </div>
                </div>
            </div>

        </div>


        <div class="Loading" style="display: none;">
            <div id="popup" class="popup">
                <div class="content">
                    <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
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












