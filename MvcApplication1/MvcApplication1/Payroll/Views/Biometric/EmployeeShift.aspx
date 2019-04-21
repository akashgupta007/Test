<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeShiftModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

    Employee Shift

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Biometric/EmployeeShift1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form method="post" id="EmployeeShift" novalidate="novalidate" enctype="multipart/form-data">
        <div class="row">

            <div id="DetailPanel" class="panel panel-primary">

                <div class="panel-heading">
                    <h3 class="panel-title">Employee Shift</h3>
                </div>

                <div class="panel-body" style="align-items: center;">

                  

                    <div id="divFilter"  style="display:none;" >
                        <div class="col-lg-12" >


                      

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
                        <div class="col-lg-2">
                            Designation 
            <%: Html.DropDownListFor(model => model.DesginationId ,Model.DesginationList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.DesginationId)%>
                        </div>

                        <div class="col-lg-1">
                        </div>

                    </div>

                    <div class="col-lg-12" style="margin-top: 5px;">
                        <div class="col-lg-2">
                            Project  
            <%: Html.DropDownListFor(model => model.ProjectId ,Model.ProjectList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.ProjectId)%>
                        </div>
                        <div class="col-lg-2">
                            Employee Type  
            <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmployeeTypeList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                        </div>

                        <div class="col-lg-2">
                            Employee   
            <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                            <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                        </div>


                   

                        <div class="col-lg-2">
                            <br />
                            <button style="vertical-align: central; text-align: left;" type="button" value="Search" id="btnSearch" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
                            
                        </div>
                    </div>
                    </div>

                    

                    <div class="col-lg-12" style="margin-top: 5px;">
                           <div class="col-lg-2">
                        <button type="submit" id="btnDownload" style="display:none;" name="command" value="DownloadExcelFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span> Download Excel</button>
                       </div>

                        <div class="col-lg-2">
                            <%: Html.TextBoxFor(model => model.filename  , new {@class="form-control parent", @type="file" , placeholder="Enter Document Name"})%>
                            <%: Html.ValidationMessageFor(model => model.filename)%>

                      

                        </div>
                        <div class="col-lg-2">
                            <button type="submit" id="UploadFile" onclick="return checkValidation()" name="command" value="UploadFile" class="btn  btn-success"><span class="glyphicon glyphicon-upload"></span> Upload File</button>
                        </div>
                     
                         
                         <div class="col-lg-2">
                            <button type="button" id="btnCancel"   class="btn  btn-success"><span class="glyphicon glyphicon-remove"></span> Cancel</button>
                        </div> 
                      <div class="col-lg-2">
                            <button type="button" id="btnUpdate"    style="display:none;" class="btn  btn-success"><span class="glyphicon glyphicon-pencil"></span>Update</button>
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
