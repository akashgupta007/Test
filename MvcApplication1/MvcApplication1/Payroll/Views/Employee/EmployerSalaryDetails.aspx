<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployerSalaryDetailViewModel>" %>





<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employer Salary Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployerSalaryDetail.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>
    <style type="text/css">
        .Loading {
            width: 100%;
            display: block;
            position: absolute;
            top: 0;
            left: 0;
            height: 100%;
            z-index: 999;
            background-color: rgba(0,0,0,0.5); /*dim the background*/
        }

        .content {
            background: #fff;
            padding: 28px 26px 33px 25px;
        }

        .popup {
            border-radius: 1px;
            background: #6b6a63;
            margin: 30px auto 0;
            padding: 6px;
            position: absolute;
            width: 100px;
            top: 50%;
            left: 50%;
            margin-left: -100px;
            margin-top: -40px;
        }
    </style>



    <form method="post" id="EmployerSalaryDetails">

        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Employer Salary   </span>
                       <%--     <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Reimbursement/EmployeeReimbursement.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>--%>
                            <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeReimbursement.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>

                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Add New</button>
                                </div>

                            </div>
                            <div id="data" style="overflow: auto;">
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group" style="display: none;">

                                <div class="col-lg-3 ">
                                    Company Name
                                </div>
                                <div class="Form-control col-lg-9 ">

                                    <input type="hidden" class="form-control" id="ErrorMsg" name="ErrorMsg" value="<%:(ViewBag.msg ?? String.Empty)%>">
                                    <input type="hidden" class="form-control" id="Flag" name="Flag" value="<%:(ViewBag.Flag ?? String.Empty)%>">



                                    <%: Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList   , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.CompanyId)%>
                                </div>
                            </div>

                            <div class="form-group" id="payroll_Itemid">
                                <div class="col-lg-3 ">
                                    Location Name
                                </div>
                                <div class="Form-control col-lg-9">

                                    <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList   , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.LocationId)%>

                                    <%: Html.HiddenFor(model=>model.EmployersalaryDetailid) %>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Pay Type                              
                                </div>
                                <div class="Form-control col-lg-9">

                                    <%: Html.DropDownListFor(model => model.PayType ,Model.PayTypeList   , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.PayType)%>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Amount
                                </div>

                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.Amount  , new {@class="form-control ", placeholder = "Enter Amount"})%>

                                    <%: Html.ValidationMessageFor(model => model.Amount)%>
                                </div>

                            </div>




                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Start Date 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.StartDate  , new {@class="form-control dtpicker", placeholder = "Enter Start Date" , Readonly="Readonly"})%>
                                    <%: Html.ValidationMessageFor(model =>model.StartDate)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    End Date 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.EndDate  , new {@class="form-control dtpicker", placeholder = "Enter End Date", Readonly="Readonly"})%>
                                    <%: Html.ValidationMessageFor(model =>model.EndDate)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Description 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.desciption  , new {@class="form-control", placeholder = "Enter Description"})%>
                                    <%: Html.ValidationMessageFor(model =>model.desciption)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">

                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnDocInsert" name="btnDocInsert" class="btn  btn-success"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btndocUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" name="btndocUpdate" class="btn btn-success"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button type="button" id="btnClear" name="btnClear" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>

    </form>


    <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
            <div class="content">
                <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
            </div>
        </div>
    </div>



    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>




