<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.DocumentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AttachFile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/AttachFile.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form method="post" id="AttachFile" name="AttachFile" novalidate="novalidate" enctype="multipart/form-data">

        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">

                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Attach File Details</h3>--%>
                            <span style="text-align: left;" class="panel-title">Attach File </span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/AttachFile.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a></span>
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addFile" name="addFile" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>
                            </div>

                            <div id="data" style="overflow: auto;">
                            </div>
                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                            <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
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
                                    File Name
                                </div>
                                <div class="col-lg-9 ">
                                    <input type="hidden" id="DocumentId" name="DocumentId" />
                                    <%: Html.TextBoxFor(model => model.DocumentName  , new {@class="form-control parent", placeholder="Enter Document Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentName)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Category
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DocumentCategoryId, Model.DocumentCategoryList  , new {@class="form-control parent"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentCategoryId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                   Created Date
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DocumentDate  , new {@class="form-control DBPicker parent", @readonly="readonly", placeholder = "Enter Document Date"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentDate)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                     Expiry Date
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DocumentExpiryDate  , new {@class="form-control DBPicker parent", @readonly="readonly", placeholder = "Enter Expire Date"})%>
                                    <%: Html.ValidationMessageFor(model => model.DocumentExpiryDate)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    For Employee Portal
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.CheckBoxFor(model => model.IsForEmpPortal , new {@id="IsForEmpPortal"})%>
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



        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
                <button type="button" name="new" onclick='ChildRecordReset()' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-plus'></span>New Record</button>
                <input type="hidden" id="flag" name="flag" />
            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">

                    <div class="form-group">
                        <div class="col-lg-4 ">
                           File Name
                            <%: Html.TextBoxFor(model => model.DocumentObjectName  , new {@class="form-control child", placeholder = "Enter Document Name"})%>
                            <%: Html.ValidationMessageFor(model => model.DocumentObjectName)%>
                        </div>
                        <div class="col-lg-4 ">

                            <input type="hidden" id="DocumentObjectId" name="DocumentObjectId" class="form-control" />
                            <input type="hidden" class="form-control" id="DocumentObjectType" name="DocumentObjectType">
                            <%--<input type="file" id="Path" class="form-control flUpload" name="Path" onchange="onFileSelected(event)" >
                            <input type="hidden" class="form-control" id="imagePath" name="imagePath">--%>
                            Upload
                            <%: Html.TextBoxFor(model => model.File  , new {@class="form-control child", type="File", placeholder = "Enter Document Name"})%>
                            <%: Html.ValidationMessageFor(model => model.File)%>
                        </div>
                        <div class="col-lg-4 ">
                            <div>
                                &nbsp;
                            </div>
                            <%--<button type="submit">Add Document</button>--%>
                            <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnChildInsert" class="btn  btn-success" name="Command" value="Insert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                            <%--<button style="margin-bottom: 2px; margin-right: 1px;" type="submit" name="btnChildInsert" onclick='InsertChildRecord()' class="btn  btn-success childInsert">Insert</button>--%>
                            <%--<button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success childUpdate child">Update</button>--%>
                            <button type="submit" id="btnChildUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success" name="Command" value="Update"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                            <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                            
                        </div>
                    </div>

                    <%--<div class="form-group">                       
                        <div style="height:200px;">
                            <div style="width:150px;height:150px;" class="col-lg-offset-4" id="logoimage">
                                <img id="limage" class="img-thumbnail" onload="LoadImage(this);" />
                            </div>
                            <div style="width:150px;height:150px;" class="col-lg-offset-4" id="logoid">

                            </div>
                        </div>
                   </div>--%>
                </div>
            </div>

            <div class="col-lg-12">
                <div id="childData"></div>
            </div>
        </div>
        <div id="Msg" style="color:green;">
            <label id="lblMsg"> <%:ViewData["Msg"] %></label>
        </div>
    </form>
   
    <%--<%  
        string valu = null;
        valu = Convert.ToString(Session["a"]);
        
        if (!string.IsNullOrEmpty(valu))
       { %>
    <div class="navbar navbar-inverse navbar-fixed-bottom">
        <div id="bagMsg" style="margin-left: 10px; background-color: green;">
            <%:ViewData["Msg"] %>
        </div>
    </div>
    <% }     
       else
       { %>
    <div class="navbar navbar-inverse navbar-fixed-bottom">
           <div id="MsgDiv1" style="display: none;">
            <label id="lblError1"></label>
        </div>
     </div>
       <%}
       %>--%>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
           <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
     <%--</div>--%>


</asp:Content>
