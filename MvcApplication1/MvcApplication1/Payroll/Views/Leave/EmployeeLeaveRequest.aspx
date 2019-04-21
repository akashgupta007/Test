<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLeaveRequestViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Leave Request
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/EmployeeLeaveRequest2.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="EmpLeaveRequst" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top:10px;">
          <div class="row">
                
                <div class="col-lg-6 col-md-offset-3">
                    <div class="panel panel-primary" id="EditPanel" >
                        <div class="panel-heading">
                    <%--        <h3 class="panel-title"><span id="panelHeader"></span></h3>--%>
                                     <span  style="text-align:left;"class="panel-title">Apply Employee Leave</span>
                            <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Leave/ApplyEmployeeLeave.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                           
                        </div>
                        <div  class="panel-body">                                                                                                                                                                
                            <div class="form-group" > 
                        <div class="col-md-3">Employee Name :
                                 </div>                                
                                 <div class="col-md-6">
                                                      <input type="hidden" id="EmpLeaveRequestId" name="EmpLeaveRequestId" />
                                   <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control parent"})%>  
                                           <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                              </div>                                                             
                                    </div>
                             <div class="form-group" > 
                                  <div class="col-md-3">Leave Type :
                                 </div>                                
                                <div class="col-md-6">
                                          
                               <%: Html.DropDownListFor(model => model.EmpLeaveTypeId, new List<SelectListItem> { 
                                                                     new SelectListItem{Text="--Select--", Value="" }        }, new {@class="form-control" })%>
                                  
                                       <%: Html.ValidationMessageFor(model => model.EmpLeaveTypeId)%>
                                              </div>                                                             
                                            </div>                   
                               <div class="form-group"> 
                            <div class="col-md-3">Apply Date :
                                 </div>                                
                                   <div class="col-md-6">

                                     
                                     <%: Html.TextBoxFor(model => model.EmpLeaveTxnDate , new {@class="form-control parent ApplyDate", @readonly="readonly", placeholder="Enter Apply Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmpLeaveTxnDate)%>
                                              </div>                                                             
                                     </div>  
                              <div class="form-group"> 
                                 <div class="col-lg-3" ">Is Leave For Half Day 
                                 </div>    
                                     <div class="col-lg-6" ><input type='checkbox' class='checkbox-inline' id="isHalfDay"  name="isHalfDay"/> 
                                 </div>                               
                             <%-- <div class="col-md-3" style="display:none;">
                                    <%: Html.TextBoxFor(model => model.RequestedDays , new {@class="form-control parent", placeholder="Enter Requested Days"})%> 
                                           <%: Html.ValidationMessageFor(model => model.RequestedDays)%>                                        
                                              </div>   --%>                                                          
                                            </div>                            
                           <div class="form-group">
                                 <div class="col-md-3">
                                    Start Date :
                                </div>
                               <div class="col-md-6">                                 
                                    <%: Html.TextBoxFor(model => model.StartDt , new {@class="form-control parent DBPicker", @readonly="readonly", placeholder="Enter Start Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                </div>
                             </div>                            
                           <div class="form-group">
                                <div class="col-md-3">
                                    End Date :
                                </div>
                                 <div class="col-md-6">                               
                                    <%: Html.TextBoxFor(model => model.EndDt , new {@class="form-control parent DBPicker", @readonly="readonly", placeholder="Enter End Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                </div>
                            </div>           
                            <div class="form-group"> 
                                  <div class="col-md-3">Request Description :
                                 </div>                                
                                      <div class="col-md-6">
                              <%: Html.TextBoxFor(model => model.LeaveDescription , new {@class="form-control parent", placeholder="Enter Leave Description"})%>
                                    <%: Html.ValidationMessageFor(model => model.LeaveDescription)%>                             
                                              </div>                                                             
                                            </div>                                                                                                                                            
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-ok"></span> Apply</button>
                            
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-sign"></span> Clear</button>
                                </div>
                            </div>
                              <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                        </div>
                    </div>
                </div>


                <div class="col-lg-12">
                    <div class="panel panel-primary"   id="leaveBalance" style="display: none;">
                        <div class="panel-heading">
                               <h3 class="panel-title">Leave Balance</h3>
                        </div>
                        <div class="panel-body">
                              <div id="data" style="overflow:auto; "> 

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
