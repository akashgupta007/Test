<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeLeaveLedgerViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LeaveLedger
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/LeaveLedger1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script>
     

    <form id="LeaveLedger" novalidate="novalidate" method="post">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-12">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                       
                               <span  style="text-align:left;"class="panel-title">Employee Leave Ledger Details </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Leave/LeaveLedger.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>

                        <div class="panel-body" style="align-items: center;">
                            <div class="form-group">
                                
                                <div class="col-lg-2">
                                     Leave Type 
                                    <%: Html.DropDownListFor(model => model.LeaveTypeId ,Model.LeaveTypeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.LeaveTypeId)%>
                                </div>
                             
                                <div class="col-lg-2">
                                     Employee  
                                    <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                </div>
                               
                                <div class="col-lg-2">
                                     Month
                                    <%: Html.DropDownListFor(model => model.MonthId ,Model.MonthList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.MonthId)%>
                                </div>
                               
                                <div class="col-lg-2">
                                       Year 
                                    <%: Html.DropDownListFor(model => model.YearId ,Model.YearList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.YearId)%>
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <button type="button" id="btnSearch" name="btnSearch" value="Leave Detail" class="btn  btn-success" ><span class='glyphicon glyphicon-zoom-in'></span> Leave Detail</button>
                              
                                    <button type="button" id="btnSummary" name="btnSummary" value="Leave Summary" class="btn  btn-success" ><span class='glyphicon glyphicon-zoom-out'></span> Leave Summary</button>
                                 
                                    <button type="button" id="btnLeaveReg" style="display:none;" name="btnLeaveReg" value="Leave Register" class="btn  btn-success" ><span class='glyphicon glyphicon-zoom-out'></span> Leave Register</button>
                                
                                    <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                </div>
                                <div class="col-lg-2">
                                     <br />
                                         <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class='glyphicon glyphicon-export'></span> Excel</button>
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class='glyphicon glyphicon-export'></span> Word</button>                      
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div id="data" style="overflow: auto;">
                                </div>
                                <div>
                                    <iframe id="ExcelFrame" style="display: none"></iframe>
                                </div>
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
