<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeBankDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeBank
</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeBankDetails.js") %>"></script>

     <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>


    <form method="post" id="EmployeeBank" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">



                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Employee Bank Details</h3>--%>
                            <span style="text-align: left;" class="panel-title"> Bank Information</span>
                            <%-- <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmplloyeeArrearProcess.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>--%>
                           <%-- <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeDepartment.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>--%>
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <input type='checkbox' class='checkbox-inline' id="IsHistory" name="IsHistory" />
                                    Show History                      
                                  </div>
                                <div class="col-md-4">
                                    <button type="button" id="addBank" name="addBank" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Bank Details</button>
                                     <%--<input type='checkbox' class='checkbox-inline' id="IsHistory" style="display:none;" name="IsHistory" />--%>
                                    <%--Show History--%>
                                </div>
                                <div class="col-md-3">

                                    <%--  <button type="button" id="ExportData"  name="ExportData" class="btn btn-info">Export To Excel</button>--%>
                                   

                                </div>
                            </div>

                            <div id="data" style="overflow-x: auto; overflow-y: hidden">
                            </div>
                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnPdf" class="btn  btn-success enabling cancel" name="command" value="Pdf"><span class="glyphicon glyphicon-export"></span> Pdf</button>
                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                            <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                            <div id="dataExport" style="display: none;">
                            </div>
                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                    </div>


                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div class="panel-body ">

                            <div id="divDisable">

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Employee
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                          <input type="hidden" id="EmployeeBankDetailsId" name="EmployeeBankDetailsId" />
                                         <%-- <input type="hidden" id="EmployeeId2" name="EmployeeId2" />--%>
                                        <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Bank
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.DropDownListFor(model => model.EmpBankId ,Model.BankList, new {@class="form-control"})%>
                                        <%: Html.ValidationMessageFor(model => model.EmpBankId)%>
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <div class="col-lg-3 ">
                                        Bank Account No
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.BankAccountNo  , new {@class="form-control", placeholder="Enter Bank Account No"})%>
                                        <%: Html.ValidationMessageFor(model => model.BankAccountNo)%>
                                    </div>
                                </div>

                                <div class="form-group" id="StartDt2">
                                    <div class="col-lg-3 ">
                                        Start Date
                                    </div>
                                    <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.StartDate  , new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select Start Date"})%>
                                        <%: Html.ValidationMessageFor(model => model.StartDate)%>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group" id="EndDt2">
                                <div class="col-lg-3 ">
                                    End Date
                                </div>
                                <div class="col-lg-9 ">
                                   <%: Html.TextBoxFor(model => model.EndDate, new {@class="form-control DBPicker ", @readonly="readonly", placeholder="Select End Date"})%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                </div>
                            </div>
                             <div class="form-group" >
                                <div class="col-lg-3 ">
                                    Description
                                </div>
                                <div class="col-lg-9 ">
                                   <%: Html.TextBoxFor(model => model.Description, new {@class="form-control ", placeholder="Enter Description"})%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    <input type="hidden" class="form-control" id="a" name="a">
                                </div>
                                <div class="col-lg-9">
                                    <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button type="button" id="btnClose" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
