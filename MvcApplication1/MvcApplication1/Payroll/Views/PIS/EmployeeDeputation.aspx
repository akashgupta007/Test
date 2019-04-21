<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpDeputationViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeDeputation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeeDeputation.js") %>"></script>


    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="EmployeeDeputation" method="post" novalidate="novalidate">


        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">



                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">

                            <span style="text-align: left;" class="panel-title">Employee Deputation  </span>

                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/PIS/EmployeeDeputation.html") %>"><b>
                                <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>

                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Employee Deputation</button>
                                </div>
                            </div>

                            <div id="data" style="overflow: auto">
                            </div>

                            <br />   
                        
                            <button style="margin-bottom: 2px; margin-right: 1px; display:none;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                            <button style="margin-bottom: 2px; margin-right: 1px; display:none;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>
                                
                            

                        </div>

                        <div style="text-align: center; margin-bottom: 5px;">
                            <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>

                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div class="panel-body ">


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Employee Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="EmpDeputationId" name="EmpDeputationId" />
                                    <%: Html.DropDownListFor(model => model.EmloyeeId ,Model.EmployeeList  , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.EmloyeeId)%>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Order No 
                                </div>
                                <div class="Form-control col-lg-9 ">

                                    <%: Html.TextBoxFor(model => model.DeputationOrderNo , new {@class="form-control", @maxlength="20", placeholder="Enter  Order No "})%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationOrderNo)%>
                                </div>
                            </div>




                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Order Date
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationOrderDate , new { @class = "form-control  DBPicker", @readonly="readonly", placeholder = "Enter Order Date " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationOrderDate ) %>
                                </div>
                            </div>



                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Type 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.RadioButtonFor(model => model.DeputationType, "FN",  new { @id="IsDeputationTypefn" })%>FN &nbsp;
                                  <%: Html.RadioButtonFor(model => model.DeputationType, "AN", new { @id="IsDeputationTypeAn" , @checked=true})  %>AN
                                      
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Period From 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationPeriodFromDate , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Period From Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationPeriodFromDate ) %>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Period To 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationPeriodToDate , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Period To Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationPeriodToDate ) %>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Parent Office  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationParentOffice   , new { @class = "form-control " , @maxlength="50", placeholder = "Enter Parent Office " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationParentOffice     ) %>
                                </div>
                            </div>



                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Parent Station 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationParentStation   , new { @class = "form-control " , @maxlength="50", placeholder = "Parent Station  " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationParentStation     ) %>
                                </div>
                            </div>



                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Parent Designation
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DeputationParentDesignationId ,Model.Designationlist  , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationParentDesignationId)%>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Relieve Date 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationRelDate , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Relieve Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationRelDate ) %>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Relieve Session 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.RadioButtonFor(model => model.DeputationRelSession, "FN",  new { @id="IsDeputationRelSessionfn" })%>FN &nbsp;
                                  <%: Html.RadioButtonFor(model => model.DeputationRelSession, "AN", new { @id="IsDeputationRelSessionAn" , @checked=true})  %>AN
                                      
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Rejoin Date 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationRejoinDate , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Rejoin Date  " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationRejoinDate ) %>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Rejoin Session
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.RadioButtonFor(model => model.DeputationRejoinSession, "FN",  new { @id="IsDeputationRejoinSessionfn" })%>FN &nbsp;
                                  <%: Html.RadioButtonFor(model => model.DeputationRejoinSession, "AN", new { @id="IsDeputationRejoinSessionAn" , @checked=true})  %>AN
                                      
                                </div>
                            </div>






                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing Office
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationBorrOffice   , new { @class = "form-control " , @maxlength="50", placeholder = "Enter Borrowing Office" })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationBorrOffice     ) %>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing Station
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationBorrStation   , new { @class = "form-control " , @maxlength="50",placeholder = "Enter Borrowing Station " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationBorrStation     ) %>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing Designation 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.DeputationBorrDesignationId ,Model.Designationlist  , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationBorrDesignationId)%>
                                </div>
                            </div>



                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing joindate  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.DeputationBrjoinDate , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter  Borrowing join date  " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationBrjoinDate ) %>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing JoinSession
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.RadioButtonFor(model => model.DeputationBrjoinSession, "FN",  new { @id="DeputationBrjoinSessionFn" })%>FN &nbsp;
                                  <%: Html.RadioButtonFor(model => model.DeputationBrjoinSession, "AN", new { @id="DeputationBrjoinSessionAN" , @checked=true})  %>AN
                                      
                                
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing Relieve Date
                  
                                </div>
                                <div class="Form-control col-lg-9 ">

                                    <%: Html.TextBoxFor(model => model.DeputationBrelDate , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Borrowing Reldate " })%>
                                    <%: Html.ValidationMessageFor(model => model.DeputationBrelDate ) %>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Borrowing Relieve Session
                                </div>

                                <div class="Form-control col-lg-9 ">
                                    <%--<%: Html.RadioButtonFor(model => model.DeputationBrelSession, "True",  new { @id="IsDeputationRelieveSessionfn" })%>FN &nbsp;
                                  <%: Html.RadioButtonFor(model => model.DeputationBrelSession, "False", new { @id="IsDeputationRelieveSessionAn" , @checked=true})  %>AN--%>

                                    <%: Html.RadioButtonFor(model => model.DeputationBrelSession, "FN",  new { @id="IsDeputationRelieveSessionfn" })%>FN &nbsp;
                                    <%: Html.RadioButtonFor(model => model.DeputationBrelSession, "AN", new { @id="IsDeputationRelieveSessionAn",  @checked = "true" })  %>AN  
                                      
                                 
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
