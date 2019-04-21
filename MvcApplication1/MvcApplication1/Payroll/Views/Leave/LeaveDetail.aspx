<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpLeaveRequestViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Leave Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%:Scripts.Render("~/bundles/LeaveDetail")%> 
    <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/LeaveDetail.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <%--<%
    
        AjaxOptions ajaxOptions = new AjaxOptions
        {
            UpdateTargetId = "data",
            InsertionMode = InsertionMode.Replace,
            HttpMethod = "POST",
            LoadingElementId = "loading_ddl",
        };
    %>
    <% Ajax.BeginForm(ajaxOptions); %>--%>
    <form method="post" id="LeaveDetail" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="container-fluid">
             <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Leave Transaction Report</h3>--%>
                            <span style="text-align: left;" class="panel-title">Leave Detail</span>
                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Leave/LeaveDetails.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">
                            <%--   <div class="col-lg-12">  --%>
                            <%--<div class="col-md-1">Employee:</div>--%>
                            <div class="col-md-2">
                                Employee:
                                                          <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control parent"})%>
                            </div>
                            <%-- <div class="col-md-1">Leave Type</div>--%>
                            <div class="col-md-2">
                                Leave Type:
                                                         <%: Html.DropDownListFor(model => model.LeaveTypeId ,Model.LeaveTypeList, new {@class="form-control parent"})%>
                            </div>
                            <%-- <div class="col-md-1">--%><%--Start Date:</div>--%>
                            <div class="col-md-2">
                                Start Date:
                                                        <%: Html.TextBoxFor(model => model.StartDate, new {@class="form-control parent  DBPicker", @readonly="readonly", placeholder="Enter Start Date"})%>
                                <%: Html.ValidationMessageFor(model => model.StartDate)%>
                            </div>
                            <div class="col-md-2">
                                End Date:<%--</div>--%>
                                <%--     <div class="col-md-2">--%>

                                <%: Html.TextBoxFor(model => model.EndDate, new {@class="form-control parent DBPicker", @readonly="readonly", placeholder="Enter End Date"})%>
                                <%: Html.ValidationMessageFor(model => model.EndDate)%>
                            </div>
                            <%--<div class="col-md-1"></div>--%>
                            <%-- <div class="col-md-2">--%>
                            <div class="col-md-4">
                                <button style="vertical-align: bottom; text-align: left; margin-top: 20px;" id="btnSearch" type="button" class="btn  btn-success"><span class="glyphicon glyphicon-search"></span> Search</button>
                                <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnPdf" class="btn  btn-success enabling cancel" name="command" value="Pdf"><span class="glyphicon glyphicon-export"></span> Pdf</button>
                                <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                                <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>

                            </div>

                            <%-- </div>--%>
                        </div>
                        <%--     </div>--%>
                    </div>
                </div>
               <%-- <div style="text-align: center">
                    <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                </div>--%>
           <%-- </div>--%>


            <div class="col-lg-12">

                <div id="data" style="overflow: auto;">
                </div>

            </div>


        </div>
       </div>
             <div class="ExcelPasswordScreeen" style="height: 600px; width: 600px;display: none;vertical-align:central;">
					<div id="ExcelPopup" class="popup1" style="vertical-align: central;">
						<div class="panel panel-primary">
							<div class="panel-heading">
								<div>Edit Applied Leave</div>
							</div>
							<div class="panel-body">
                                <div class="form-group" style="display:none">
                                    <div class="col-md-3">
                                        Employee Name :
                                    </div>
                                    <div class="col-md-6">
                                        <input type="hidden" id="EmpLeaveRequestId" name="EmpLeaveRequestId" />

                                        <%:Html.HiddenFor(model=>model.EmployeeId) %>
                                        <%: Html.TextBoxFor(model => model.EmployeeName , new {@class="form-control1 form-control parent" , @readonly="readonly"})%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3">
                                        Leave Type :
                                    </div>
                                    <div class="col-md-6">

                                        <%: Html.DropDownListFor(model => model.EmpLeaveTypeId, Model.LeaveTypeList, new {@class="form-control1 form-control" })%>

                                        <%: Html.ValidationMessageFor(model => model.EmpLeaveTypeId)%>
                                    </div>
                                </div>
                                <div class="form-group" style="display:none" >
                                    <div class="col-md-3">
                                        Apply Date :
                                    </div>
                                    <div class="col-md-6">
                                        <%: Html.TextBoxFor(model => model.EmpLeaveTxnDate , new {@class="form-control1 form-control parent ApplyDate", @readonly="readonly", placeholder="Enter Apply Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.EmpLeaveTxnDate)%>
                                    </div></div>
                                    <div class="form-group">
                                        <div class="col-lg-3" style="display: none;">
                                            Requested Days :
                                        </div>
                                        <div class="col-md-6" style="display: none;">
                                            <%: Html.TextBoxFor(model => model.RequestedDays , new {@class="form-control1 form-control parent", placeholder="Enter Requested Days"})%>
                                            <%: Html.ValidationMessageFor(model => model.RequestedDays)%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                    <div class="col-lg-3">
                                        Is Leave For Half Days
                                    </div>
                                    <div class="col-lg-3">
                                        <input type='checkbox' class='checkbox-inline' id="isHalfDay" name="isHalfDay" />

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3">
                                        Start Date :
                                    </div>
                                    <div class="col-md-6">
                                        <%: Html.TextBoxFor(model => model.StartDt , new {@class="form-control1 form-control parent DBPicker", @readonly="readonly", placeholder="Enter Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDt)%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3">
                                        End Date :
                                    </div>
                                    <div class="col-md-6">
                                        <%: Html.TextBoxFor(model => model.EndDt , new {@class="form-control1 form-control parent DBPicker",@readonly="readonly",placeholder="Enter End Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.EndDt)%>
                                    </div>
                                </div>
                                <div class="form-group" style="display:none;">
                                    <div class="col-lg-3">
                                        Is Send For Mail
                                    </div>
                                    <div class="col-lg-3">
                                        <input type='checkbox' class='checkbox-inline' id="Is_Send_Mail" name="Is_Send_Mail" checked="checked"/>
                                    </div>
                                </div>

                                <%--<div class="form-group" id="Too" style="display:none;">
                                    <div class="col-md-3">
                                        To
                                    </div>
                                    <div class="col-md-6">
                                        <%: Html.TextBoxFor(model => model.To , new {@class="form-control1  parent", placeholder="Enter  To Email Id" })%>
                                        <%: Html.ValidationMessageFor(model => model.To)%>
                                    </div>
                                </div>

                                <div class="form-group" id="Ccc" style="display:none;">
                                    <div class="col-md-3">
                                        Cc
                                    </div>
                                    <div class="col-md-6">
                                        <%: Html.TextBoxFor(model => model.Cc , new {@class="form-control1  parent", placeholder="Enter Cc Email Id "})%>
                                        <%: Html.ValidationMessageFor(model => model.Cc)%>
                                    </div>
                                </div>--%>


                                <div class="form-group">
                                    <div class="col-md-3">
                                        Request Description :
                                    </div>
                                    <div class="col-md-9">
                                        <%: Html.TextAreaFor (model => model.LeaveDescription,10,100 , new {@class="form-control1 form-control parent", @minwidth="450px", @maxwidth="500px", placeholder="Enter Leave Description"})%>
                                        <%: Html.ValidationMessageFor(model => model.LeaveDescription)%>
                                    </div>
                                </div>




                                <div class="form-group">
                                    <div class="col-md-3">
                                        
                                    </div>
                                    <div class="col-lg-6">
                                        <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnUpdate" name="btnUpdate" class="btn  btn-success enabling">Update</button>
                                       
                                        <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling">Clear</button>
                                      
                                    </div>
                                </div>                                                             
                            </div>
							</div>
						</div>
					</div>
				</div>
  
          <div class="Loading" style="display: none;">
            <div id="popup" class="popup">
                <div class="content">
                    <img id="LoadingProgress" src="<%= Url.Content("~/Images/processingImage.gif") %> " />
                </div>
            </div>
        </div>
    </form>
    <%--<%Html.EndForm(); %>--%>
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>
</asp:Content>
