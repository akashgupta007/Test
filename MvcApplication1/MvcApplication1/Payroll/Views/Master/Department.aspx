<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.DepartmentViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Department
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Master/Department1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="Department" novalidate="novalidate">

                <div class="row">
                    <input type="radio" name="view" value="0" checked="checked" id="templatemode" onclick="checkclick();">
                    <span class="glyphicon glyphicon-user"></span>User Defined Format Entry &nbsp; &nbsp;
                        <input type="radio" name="view" value="1" id="gridmode" onclick="checkclick();" />
                    <span class="glyphicon glyphicon-download"></span>Upload from Excel
                   
                </div>
        
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                           <%-- <h3 class="panel-title">Department Details</h3>--%>
                              <span  style="text-align:left;"class="panel-title" > Department   </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Master/Department.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                         
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span> New Record</button>
                                </div>
                            </div>
                        <div id="data" style="overflow:auto; "> 
                            </div>
                          
                        </div>
                        <div style="text-align:center;margin-bottom:5px; ">
                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>                     
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"> </span></h3>
                        </div>
                        <div  class="panel-body ">
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Department Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="DepartmentId" name="DepartmentId" />
                                    <input type="hidden" id="first_start_date" name="first_start_date" />
                                    <input type="hidden" id="first_end_date" name="first_end_date" />
                                    <input type="hidden" id="last_start_date" name="last_start_date" />
                                    <input type="hidden" id="last_end_date" name="last_end_date" />
                                    <input type="hidden" id="ParentDepartment_Id" name="ParentDepartment_Id" />

                                    <input type="hidden" id="LeaveEndDate" name="LeaveEndDate" />

                                    <%: Html.TextBoxFor(model => model.DepartmentName , new {@class="form-control", placeholder="Enter Department Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.DepartmentName)%>
                                </div>
                            </div>                            
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Parent Department Name
                                </div>
                                <div class="Form-control col-lg-9 ">                                   
                        <%--          <%: Html.DropDownListFor(model => model.ParentDepartmentId ,Model.DepartmentList, new {@class="form-control"})%>--%>
                                     <select name="ParentDepartmentId" id="ParentDepartmentId" class="form-control"><option>--Select--</option></select>     
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Start Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="StartDt2" name="StartDt2" />                                  
                                    <%: Html.TextBoxFor(model => model.StartDt , new {@class="form-control DBPicker", placeholder="Enter Start Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    End Date
                                </div>
                                <div class="Form-control col-lg-9 ">   
                                        <input type="hidden" id="EndDt2" name="EndDt2" />
                                    <%: Html.TextBoxFor(model => model.EndDt , new {@class="form-control DBPicker", placeholder="Enter End Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Notes
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.Notes , new {@class="form-control", placeholder="Enter Notes"})%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                </div>
                            </div>
                              <div style="text-align: center">
                            
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
        <div id="uploadPanel" style="display: none"> 
                <table class="table table-striped table-bordered table-hover table-responsive table-condensed"style="width:80%">
                <tr>
                <td> <button type="submit" id="btnDownload" name="command" value="DownloadExcelFormat" class="btn  btn-success"><span class="glyphicon glyphicon-download"></span> Download Excel</button></td>
                <td> Import Excel File : </td>
                <td>  <input name="PathExcel" id="PathExcel" type="file" tabindex="3" onchange="onFileSelectedtype(event)"/></td>                            
                <td> <button type="submit" id="UploadFile" name="command" value="UploadFile" class="btn  btn-success" style="display: none"><span class="glyphicon glyphicon-upload"></span>Upload Employee Information</button> </td>
                </tr>
                </table>   
          
                </div>
         <div id="MsgDiv1" >
            <label id="lblError1"></label>
        </div>

       
    </form>
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>

</asp:Content>
