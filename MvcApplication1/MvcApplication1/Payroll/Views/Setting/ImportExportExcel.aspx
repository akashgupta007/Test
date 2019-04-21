<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.ImportExportExcelViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ImportExportExcel
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Setting/ImportExportExcel1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>

    <form method="post" id="ImportExportExcel" novalidate="novalidate" enctype="multipart/form-data">
        <div class="row">

            <div id="DetailPanel" class="panel panel-primary">

                <div class="panel-heading">
                    <%--<h3 class="panel-title">Employee Attendance Entry</h3>--%>
                    <span style="text-align: left;" class="panel-title">Import/ Export </span>
                    <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/ImportExportExcel.html") %>">
                        <b>
                            <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a></span>
                </div>

                <div class="panel-body" style="align-items: center;">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <div class="col-md-8">
                                <input type="radio" id="rbdownload" name="rbuploaddownload" value="f1" />Download Excel File &nbsp;&nbsp;&nbsp;
                                <input type="radio" id="rbupload" name="rbuploaddownload" value="f2" />Upload Excel File
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12">
                        <div class="col-lg-2">
                            Select Page 
                            <%: Html.DropDownListFor(model => model.ExcelDropDown, new List<SelectListItem> { 
                                    new SelectListItem{Text="Attendance Entry", Value="Attendance Entry", Selected = true},
                                    new SelectListItem{Text="Employee Salary", Value="Employee Salary"},
                                    //new SelectListItem{Text="Employee Shift", Value="Employee Shift"},
                                    new SelectListItem{Text="Employee Detail", Value="Employee Detail"}}, "--Select--", new {@class="form-control" })%>
                            <%: Html.ValidationMessageFor(model => model.ExcelDropDown ) %>
                        </div>
                    </div>

                    <div id="divFilter">

                        <div class="col-lg-12">

                            <div class="col-lg-2">
                                Month  
            <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control" })%>
                                <%: Html.ValidationMessageFor(model => model.MonthId)%>
                            </div>

                            <div class="col-lg-2">
                                Year  
            <%: Html.DropDownListFor(model => model.Year ,Model.YearList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.Year)%>
                            </div>

                            <div class="col-lg-2">
                                Department   
            <%: Html.DropDownListFor(model => model.DepartmentId ,Model.DepartmentList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.DepartmentId)%>
                            </div>
                            <div class="col-lg-2">
                                Location    
            <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.LocationId)%>
                            </div>


                            <div class="col-lg-1">
                            </div>

                        </div>


                        <div class="col-lg-12" style="margin-top: 5px;">

                            <div class="col-lg-2">
                                Designation 
            <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.DesginationId)%>
                            </div>

                            <div class="col-lg-2">
                                Project  
            <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                            </div>


                            <div class="col-lg-2">
                                Employee   
            <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                            </div>


                            <div class="col-lg-2">
                                Attendance Source    
            <%: Html.DropDownListFor(model => model.AttendanceSourceId ,Model.AttendanceSourceList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.AttendanceSourceId)%>
                            </div>

                            <div class="col-lg-2">
                                <br />

                            </div>
                        </div>




                    </div>

                    <div id="divFilter2">
                        <div class="col-lg-12" style="margin-top: 5px;">
                            <div class="col-lg-2">
                                Select Company  
                                <%: Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.CompanyId)%>
                            </div>

                            <div class="col-lg-2">
                                Employee Type  
                                <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12" style="margin-top: 5px;">
                        <div id="divDownload">
                            <div class="col-lg-2">
                                <button type="submit" id="btnDownload" name="command" value="DownloadExcelFormat" class="btn  btn-success"><span class='glyphicon glyphicon-download'></span> Download Excel File</button>
                                <button style="vertical-align: central; text-align: left;" type="button" value="Search" id="btnSearch" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
                            </div>
                        </div>

                        <div id="divUpload">
                            <div class="col-lg-2">
                                <%: Html.TextBoxFor(model => model.filename  , new {@class="form-control parent", @type="file" , placeholder="Enter Document Name"})%>
                                <%: Html.ValidationMessageFor(model => model.filename)%>
                            </div>
                            <div class="col-lg-2">
                                <button type="submit" id="UploadFile" onclick="return checkValidation()" name="command" value="UploadFile" class="btn  btn-success"><span class="glyphicon glyphicon-upload"></span>Upload Excel File</button>
                            </div>

                            <div class="col-lg-2">
                                <button type="button" id="btnUpdate" style="display: none;" class="btn  btn-success"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                            </div>
                        </div>


                    </div>


                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>

                    <div class="col-lg-12" style="margin-top: 5px;">
                        <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
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
