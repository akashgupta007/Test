<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpReimbursementViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeReimbursement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Reimbursement/EmployeeReimbursement.js") %>"></script>

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
    
  

  <form method="post" id="EmployeeReimbursement" novalidate="novalidate" enctype="multipart/form-data">

        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employee Reimbursement    </span>
                            <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Reimbursement/EmployeeReimbursement.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                            
                            
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                      <input type="hidden" class="form-control" id="ErrorMsg" name="ErrorMsg" value="<%:(ViewBag.msg ?? String.Empty)%>" />

                                    <input type="hidden" class="form-control" id="Flag" name="Flag" value="<%:(ViewBag.Flag ?? String.Empty)%>" >
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>

                            </div>
                            <div id="data" style="overflow: auto;">
                            </div>
                        </div>
                        
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                       
                         <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Employee Name
                                </div>
                                <div class="Form-control col-lg-9 ">

                                  
                                    <input type="hidden" class="form-control" id="FileUpdateFlag" name="FileUpdateFlag">
                                     
                                    <%: Html.HiddenFor(model=>model.DateOfJoining) %>
                                    <%: Html.HiddenFor(model=>model.DateOfLeaving) %>

                                    <%: Html.HiddenFor(model=>model.EmpReimbursementId) %>
                                    <%: Html.HiddenFor(model=>model.strDocumentImg) %>
                                    <%: Html.HiddenFor(model=>model.strContentType) %>
                                    <%: Html.HiddenFor(model=>model.strDocumentName) %>
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.Employeelist  , new {@class="form-control updateform"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                            </div>

                            <div class="form-group" id="payroll_Itemid">
                                <div class="col-lg-3 ">
                                    Reimbursement Item
                                </div>
                                <div class="Form-control col-lg-9">

                                    <%: Html.DropDownListFor(model => model.ReimbursementItemId ,Model.ReimbursementItemList  , new {@class="form-control updateform"})%>
                                    <%: Html.ValidationMessageFor(model => model.ReimbursementItemId)%>
                                </div>

                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Description
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%:Html.TextAreaFor(model => model.ReimbursementNotes , new {@class="form-control updateform", placeholder="Enter Description "})%>
                                    <%: Html.ValidationMessageFor(model => model.ReimbursementNotes)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Request Date
                                </div>
                                <div class="Form-control col-lg-9 "> 
                                    <%: Html.TextBoxFor(model => model.RequestDate  , new { @class = "form-control  updateform", placeholder = "Enter Reimbursement Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.RequestDate ) %>
                                </div>
                            </div>

                            <div class="form-group" style="display: none;">
                                <div class="col-lg-3 ">
                                    From Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.FromDate  , new { @class = "form-control DBPicker updateform",  @readonly="readonly", placeholder = "Enter From Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.FromDate ) %>
                                </div>
                            </div>

                            <div class="form-group" style="display: none;">
                                <div class="col-lg-3 ">
                                    To date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.ToDate  , new { @class = "form-control DBPicker updateform",  @readonly="readonly", placeholder = "Enter To Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.ToDate ) %>
                                </div>
                            </div>

                            <div class="form-group" style="display: none;">
                                <div class="col-lg-3 ">
                                    Project
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.ProjectId ,Model.Projectlist  , new {@class="form-control updateform"})%>
                                    <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Amount
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.Amount  , new { @class="form-control updateform", @maxlength="14" , placeholder = "Enter Amount" })%>
                                    <%: Html.ValidationMessageFor(model => model.Amount ) %>
                                </div>
                            </div>

                            <div class="form-group" style="display: none;">
                                <div>
                                    Is Document
                                </div>
                                <div class="Form-control" style="display: none;">

                                    <input type="checkbox" id="IsDocument" name="IsDocument" checked="checked" />

                                    <%: Html.ValidationMessageFor(model => model.IsDocument ) %>
                                </div>
                            </div>
                            <div class="form-group" id="divDownload">
                                <div class="col-lg-3 ">
                                    Download File
                                </div>
                                <div class="col-lg-9" id="divDownload1">

                                    <button type="submit" id="btnDownload" class="btn btn-success cancel" name="Command" value="Download">Download</button>

                                </div>
                            </div>
                        

                        <div class="form-group" id="uploadDocument">
                            <div class="col-lg-3 ">
                                Upload File
                            </div>
                            <div class="Form-control col-lg-9 ">
                            
                                <%: Html.TextBoxFor(model => model.File  , new {type="File",@class="form-control", placeholder = "Select Document"})%>

                                <%: Html.ValidationMessageFor(model => model.File)%>
                            </div>
                        </div>
                        </div>

                         <div class="form-group">
                            <div class="col-lg-offset-4 col-lg-3">
                                <%-- <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnInsert" class="btn  btn-success enabling" name="Command" onclick="CheckValidation()" value="Insert">Insert</button>
                                    <button type="submit" id="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling" name="Command" onclick="CheckValidation()"value="Update">Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling">Close</button>--%>

                                <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnInsert" class="btn  btn-success" name="Command" onclick='return  CheckValidation("insert")' value="Insert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnUpdate" class="btn btn-success" onclick='return  CheckValidation("Update")' name="Command" value="Update"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success "><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
