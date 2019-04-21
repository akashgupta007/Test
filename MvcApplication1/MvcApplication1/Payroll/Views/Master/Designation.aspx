<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.DesignationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Designation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Master/Designation1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="Designation" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Designation Details</h3>--%>
                             <span  style="text-align:left;"class="panel-title" > Designation  </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Master/Designation.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newDesignation" name="newDesignation" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
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
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div  class="panel-body ">

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Designation 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="DesignationId" name="DesignationId" />
                                    <%: Html.TextBoxFor(model => model.DesignationDesc  , new {@class="form-control", placeholder="Enter Designation Name"})%>
                                    <%: Html.ValidationMessageFor(model => model.DesignationDesc)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Status
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.RadioButtonFor(model => model.Inactive, "True",  new { @id="InactiveYes", @checked = "checked" })%>Active &nbsp;
                                    <%: Html.RadioButtonFor(model => model.Inactive, "False", new { @id="InactiveNo" })  %>InActive  
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Approve Leave  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.RadioButtonFor(model => model.IsLeaveApproval, "True",  new { @id="IsLeaveApprovalYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.IsLeaveApproval, "False", new { @id="IsLeaveApprovalNo", @checked = "checked" })  %>No  
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                  View Employee  Attendance
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.RadioButtonFor(model => model.IsSeeOtherAttendance, "True",  new { @id="IsSeeOtherAttendanceYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.IsSeeOtherAttendance, "False", new { @id="IsSeeOtherAttendanceNo", @checked = "checked" })  %>No  
                                </div>
                            </div>
                              <div class="form-group">
                                <div class="col-lg-3 ">
                                   View Employee Information 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.RadioButtonFor(model => model.IsSeeEmpDetails, "True",  new { @id="IsSeeOtherEmployeeDetailsYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.IsSeeEmpDetails, "False", new { @id="IsSeeOtherEmployeeDetailsNo", @checked = "checked" })  %>No  
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    View Employee Payslip 
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%: Html.RadioButtonFor(model => model.IsSeeEmpPayslip, "True",  new { @id="IsSeeOtherEmployeePayslipYes" })%>Yes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <%: Html.RadioButtonFor(model => model.IsSeeEmpPayslip, "False", new { @id="IsSeeOtherEmployeePayslipNo", @checked = "checked" })  %>No  
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Pay Scale
                                </div>
                                <div class="col-lg-9">
                                    <%: Html.DropDownListFor(model => model.PayScalId ,Model.PayScalList , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.PayScalId)%>
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
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
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
