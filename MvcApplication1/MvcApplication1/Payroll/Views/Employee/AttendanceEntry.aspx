<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpAttendanceEntryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Attendance Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/AttendanceEntry23.js") %>"></script>
        <link href="<%= Url.Content("~/Scripts/select2.css") %>" rel="stylesheet" />
    <script src="<%= Url.Content("~/Scripts/select2.js") %>"></script> 
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
            $('.ddl').select2();
        });


        var onSuccess = function () {

            var flag = 0;
            var colTitle = $("#data_Table").length;


            if (colTitle == "0" || colTitle == undefined) {

            }
            else {
                flag = 1;

            }

            if (flag == 1) {
                $("#data_Table").dataTable();

            }

        }


    </script>
    <%   
        AjaxOptions ajaxOptions = new AjaxOptions
        {
            UpdateTargetId = "data",
            InsertionMode = InsertionMode.Replace,
            HttpMethod = "POST",
            LoadingElementId = "loading",
            OnSuccess = "onSuccess"
        };
    %>
    <% Ajax.BeginForm(ajaxOptions); %>
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
    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <%--<button type="button" id="btnAttendanceUpload" name="btnAttendanceUpload" class="btn btn-info">Attendance Upload</button>--%>
            
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">
                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title">Monthly Attendance Entry</span>

                        <a style="color: #E6F1F3; float: right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/AttendanceEntry.html") %>  ">
                            <b>
                                <img style="width: 30px; height: 20px; margin-top: -10px; padding-top: -10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                        </a>
                    </div>
                    <div id="uploadPanel" style="display: none">
                    <table class="table table-striped table-bordered table-hover table-responsive table-condensed" style="width: 80%">
                        <tr>
                            <td>
                                <button type="submit" id="btnDownloadGorss" name="command" value="DownloadAttendanceFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span>Download Salary File</button></td>
                            <td>Import Attendance</td>
                            <td>
                                <input name="PathExcel" id="PathExcel" type="file" tabindex="3" onchange="onFileSelectedtype(event)" /></td>
                            <td>
                                <button type="button" id="UploadFile" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Salary Information</button>
                            </td>
                        </tr>                        
                    </table>
                </div>
                    <div class="panel-body" style="align-items: center;">
                        <%:Html.Partial("CommonDropDownList",new PoiseERP.Areas.Payroll.Models.PayrollUtil()) %>
                    </div>
                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>
                    <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
                    </div>
                </div>
                
            </div>
            
        </div>
    </div>
    <%Html.EndForm(); %>


    <form method="post" id="idAttendanceEntryUpload">

        <%: Html.HiddenFor(model=>model.imageBase64String) %>
        <%: Html.HiddenFor(model=>model.imageName) %>

        <%: Html.HiddenFor(model=>model.hfEmployee_Id) %>
        <%: Html.HiddenFor(model=>model.hfYear_Id) %>

        <%: Html.HiddenFor(model=>model.hfMonth_Id) %>
        <%: Html.HiddenFor(model=>model.hfAttendanceSource_Id) %>

        <div id="divAttendanceEntryUpload" style="display: none;">
            <div class="row">

                <div class="col-lg-12">

                    <%--<div class="form-group">
                        <div class="col-md-3">Month</div>
                        <div class="col-md-3">
                            <%: Html.DropDownListFor(model => model.Month_Id ,Model.MonthList, new {@class="form-control" })%>
                            <%: Html.ValidationMessageFor(model => model.Month_Id)%>
                        </div>
                        <div class="col-md-3">Year</div>
                        <div class="col-md-3">
                            <%: Html.DropDownListFor(model => model.Year_Id ,Model.YearList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.Year_Id)%>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="col-md-3">Employee</div>
                        <div class="col-md-3">
                            <%: Html.DropDownListFor(model => model.Employee_Id ,Model.EmployeeList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.Employee_Id)%>
                        </div>
                        <div class="col-md-3">Attendance Source </div>
                        <div class="col-md-3">
                            <%: Html.DropDownListFor(model => model.AttendanceSource_Id ,Model.AttendanceSourceList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.AttendanceSource_Id)%>
                        </div>
                    </div>
                    <br />--%>
                    <div class="form-group">
                        <div class="col-md-3">Attendance Document</div>
                        <div class="col-md-6">
                            <%: Html.TextBoxFor(model => model.UploadAttendance, new { @class="form-control", @type="file"})%>
                            <%: Html.ValidationMessageFor(model => model.UploadAttendance)%>
                        </div>
                        <div class="col-md-3">
                            <button type="button" id="btnAttachAttendance" name="btnAttachAttendance" class="btn btn-info"><span class="glyphicon glyphicon-upload"></span>Upload</button>
                        </div>
                    </div>
                </div>

                <br />

                <div class="col-lg-12">
                    <div class="form-group">
                        <img id="img" src="" class="img-responsive img-thumbnail" />
                    </div>
                </div>


                <div class="col-lg-12">
                    <div id="divAttendanceList"></div>
                </div>

            </div>
        </div>

        <div id="childDiv" style="display: none;">
            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
            </div>
            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
            </div>
            <div class="col-lg-12">
                <div id="childData">
                </div>
            </div>
        </div>

        <input type="hidden" id="flag" name="flag" />

    </form>

    <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
            <div class="content">
                <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
            </div>
        </div>
    </div>

    <%-- <div class="navbar navbar-inverse navbar-fixed-bottom">--%>
    <div id="MsgDiv" style="display: none;">
        <label id="lblError"></label>
    </div>
    <%--   </div>--%>
</asp:Content>
