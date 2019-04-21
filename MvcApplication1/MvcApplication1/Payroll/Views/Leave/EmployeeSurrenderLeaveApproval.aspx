<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLeaveRequestViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Surrender Leave Approval
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/EmployeeLeaveApproval.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="EmployeeSurrenderLeaveApproval" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top:5px;">
          <div class="row">                 
             <div class="col-lg-12">
                    <div class="panel panel-primary"   id="leaves" >
                        <div class="panel-heading">
                               <h3 class="panel-title">Employee Surrender Leave Approval</h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12" style="margin-top: 2px; margin-bottom:5px; text-align: center;">
                        <div class="col-md-1">
                            Employee:  
                        </div>
                        <div class=" col-lg-2" text-align: center;"> <select name="EmployeeId" id="EmployeeId" class="form-control"><option>--Select--</option></select> 
                        </div>
                         <div class=" col-lg-2" >
                             <img id="loading_ddl" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                         </div>
                        
                    </div>
                            <div class="col-lg-12">
                                <div id="data" style="overflow:auto; "> 

                            </div>
                            </div>                              
                        </div>
                    </div>
                </div>
             <div class="col-lg-12">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader">Edit Employee Surrender Leave Approval</span></h3>
                        </div>
                        <div  class="panel-body " >                                                                                                                                                                                                                                                                                                                              
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnApprove" name="btnApprove" class="btn  btn-success enabling"><span class="glyphicon glyphicon-ok"></span> Approve</button>
                                     <button type="button" id="btnReject" name="btnReject" style="margin-bottom:2px;margin-right:1px;" class="btn btn-success enabling"><span class="glyphicon glyphicon-remove"></span> Reject</button>
                                    <button type="button" id="btnDisApprove" name="btnDisApprove" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-remove"></span> Disapprove</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-sign"></span> Clear</button>
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
