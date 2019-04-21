<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLeaveRequestViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Leave Approval
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/EmployeeLeaveApproval1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="EmployeeLeaveApproval" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top:5px;">
          <div class="row">                 
             <div class="col-lg-12">
                    <div class="panel panel-primary"   id="leaves" >
                        <div class="panel-heading">
                              <%-- <h3 class="panel-title">Employee Leave Approval</h3>--%>
                              <span  style="text-align:left;"class="panel-title">Employee Leave Approval</span>
                             <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Leave/EmployeeLeaveApproval.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a> 
                           
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12" style="margin-top: 2px; margin-bottom:5px; text-align: center;">
                        <div class="col-md-1">
                            Leave Status: 
                        </div>
                        <div class=" col-lg-2" text-align: center;">
                            <select id="LeaveStatus" name="LeaveStatus" class="form-control Parent">
                                <option value="">--Select--</option>
                                <option value="1" selected="selected">--For Approval--</option>
                                <option value="2">--Approved--</option>
                              <%--  <option value="3">--Rejected--</option>--%>
                            </select>
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
                            <h3 class="panel-title"><span id="panelHeader">Edit Employee Leave Request Detail</span></h3>
                        </div>
                        <div  class="panel-body " >                                                                                                                                                                
                            <div class="form-group" > 
                        <div class="col-md-3"">Employee Name :
                                 </div>                                
                                 <div class="col-md-3"">
                                                      <input type="hidden" id="EmpLeaveRequestId" name="EmpLeaveRequestId" />
                                       <input type="hidden" id="EmployeeId" name="EmployeeId" />
                                  <%-- <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control parent", @disabled = "disabled"})%>   --%>  
                                     <%: Html.TextBoxFor(model => model.EmployeeName , new {@class="form-control parent ", @disabled="disabled"})%>
                                              </div>                                                             
                                            <%--</div>
                                <div class="form-group"> --%>
                                  <div class="col-md-3"">Leave Type :
                                 </div>                                
                                <div class="col-md-3"">
                                          
                                 <%--<%: Html.DropDownListFor(model => model.LeaveTypeId ,Model.LeaveTypeList, new {@class="form-control parent"})%>--%>
                                     <input type="hidden" id="LeaveTypeId" name="LeaveTypeId" />
                                          <%: Html.TextBoxFor(model => model.LeaveTypeName , new {@class="form-control parent ", @disabled="disabled"})%>
                                              </div>                                                             
                                            </div>                   
                               <div class="form-group"> 
                        <div class="col-md-3"">Applied Date :
                                 </div>                                
                                   <div class="col-md-3"">
                                        <input type="hidden" id="EmpLeaveTxnDate" name="EmpLeaveTxnDate" />
                                        <%: Html.TextBoxFor(model => model.EmpLeaveTxnDates , new {@class="form-control parent DBPicker", @readonly="readonly",@disabled="disabled", placeholder="Enter Apply Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.EmpLeaveTxnDate)%>
                                              </div>                                                             
                                           <%-- </div>
                            
                            <div class="form-group"> --%>
                                 <div class="col-lg-3">Requested Days :
                                 </div>                                
                              <div class="col-md-3"">
                                   <input type="hidden" id="RequestedDays" name="RequestedDays" />
                                    <%: Html.TextBoxFor(model => model.RequestedDayss , new {@class="form-control parent", placeholder="Enter Requested Days", @disabled="disabled"})%> 
                                           <%: Html.ValidationMessageFor(model => model.RequestedDayss)%>                                        
                                              </div>                                                             
                                            </div>                            
                           <div class="form-group">
                                 <div class="col-md-3"">
                                    Start Date :
                                </div>
                               <div class="col-md-3"">  
                                   <input type="hidden" id="StartDt" name="StartDt" />                               
                                    <%: Html.TextBoxFor(model => model.StartDts , new {@class="form-control parent DBPicker", @readonly="readonly",@disabled="disabled", placeholder="Enter Start Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                </div>
                           <%-- </div>
                            <div class="form-group">--%>
                                <div class="col-md-3"">
                                    End Date :
                                </div>
                                 <div class="col-md-3"">   
                                     <input type="hidden" id="EndDt" name="EndDt" />                            
                                    <%: Html.TextBoxFor(model => model.EndDts , new {@class="form-control parent DBPicker", @readonly="readonly",@disabled="disabled", placeholder="Enter End Date"})%>
                                      <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                </div>
                            </div>           
                            <div class="form-group"> 
                                  <div class="col-md-3">Request Description :
                                 </div>                                
                                      <div class="col-md-3">
                                          <input type="hidden" id="LeaveDescription" name="LeaveDescription" />
                              <%: Html.TextBoxFor(model => model.LeaveDescriptions , new {@class="form-control parent",@disabled="disabled"})%>
                                                       
                                              </div>  
                               
                                <div id="HoliDaysDiv" style="display:none;">
                                  <div class="col-md-3">Holi days (In Applied Leaves) :
                                 </div>                                
                                      <div class="col-md-3">
                                       
                                    <%: Html.TextBoxFor(model => model.HoliDays , new {@class="form-control parent" , @disabled="disabled"})%>
                                              <%: Html.ValidationMessageFor(model => model.HoliDays)%>              
                                              </div>                                                             
                                         </div>                                                          
                                            </div>      
                            
                              <div class="form-group" id="LeaveDebitDaysDiv" style="display:none;"> 
                                  <div class="col-md-3"">Leave Debit Days:
                                 </div>                                
                                      <div class="col-md-3"">
                                       
                                    <%: Html.TextBoxFor(model => model.LeaveDebitDays , new {@class="form-control parent"})%>
                                              <%: Html.ValidationMessageFor(model => model.LeaveDebitDays)%>              
                                              </div>                                                             
                                           
                                  <div class="col-md-3 "  >Loss of pay days :
                                 </div>                                
                                      <div class="col-md-3"">
                                       
                                    <%: Html.TextBoxFor(model => model.PayDeductDays , new {@class="form-control parent"})%>
                                              <%: Html.ValidationMessageFor(model => model.PayDeductDays)%>              
                                              </div>                                                             
                                            </div>  
                             
                              <div class="form-group" id="AvailAbleCreditDiv" style="display:none;"> 
                                  <div class="col-md-3"">AvailAble Credit Days
                                 </div>                                
                                      <div class="col-md-3"">
                                       
                                    <%: Html.TextBoxFor(model => model.AvailAbleCreditDays , new {@class="form-control parent", @disabled="disabled"})%>
                                              <%: Html.ValidationMessageFor(model => model.AvailAbleCreditDays)%>              
                                              </div>                                                             
                                          <div class="col-md-3"">Reason for Approve / Reject
                                 </div>                                
                                      <div class="col-md-3"">
                                       
                                    <%: Html.TextBoxFor(model => model.Resonreject , new {@class="form-control", @placeholder="Enter Reason"})%>
                                              <%--<%: Html.ValidationMessageFor(model => model.Resonreject)%>--%>              
                                              </div>  
                                                                                          
                                            </div>  
                            
                                                                                                                                                                  
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
