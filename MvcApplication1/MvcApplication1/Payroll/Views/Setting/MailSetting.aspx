<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.MailSetupViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MailSetting
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/MailSetting123.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

<%--    <script src="../../../../Scripts/ViewScript/Payroll/Setting/MailSetting.js"></script>--%>

    <form method="post" id="MailSetting" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">



                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                            <span  style="text-align:left;"class="panel-title" >Mail Setting</span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/MailSetting.html") %>">
                            <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> "/></b></a></span>
                        </div>

                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addMailSetup" name="addMailSetup" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>
                            </div>

                            <div id="data" style="overflow: auto;">
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

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Company
                                </div>
                                <div class="col-lg-9 ">
                                    <input type="hidden" id="MailSetupId" name="MailSetupId" />
                                    <%: Html.DropDownListFor(model => model.CompanyId ,Model.CompanyList, new {@class="form-control" })%>
                                    <%: Html.ValidationMessageFor(model => model.CompanyId)%>
                                </div>
                            </div>

                             <div class="form-group">
                                <div class="col-lg-3 ">
                                    Common for All Location
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.CheckBoxFor(model => model.IsCommonMail , new { @id="IsCommonMail", @value="True"})%>
                                </div>
                            </div>

                            <div class="form-group" id="loc">
                                <div class="col-lg-3 ">
                                    Location
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control" })%>
                                    <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Is Active
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.CheckBoxFor(model => model.IsActive , new { @id="IsActive", @checked="Checked", @value="True"})%>
                                </div>
                            </div>


                            <div class="form-group" style="display:none;">
                                <div class="col-lg-3 ">
                                    SMTP Email Account
                                </div>
                                <div class="col-lg-9">
                                    <%: Html.TextBoxFor(model => model.SmtpEmailAccount  , new {@class="form-control", placeholder="Smtp email account"})%>
                                    <%: Html.ValidationMessageFor(model => model.SmtpEmailAccount )%>
                                </div>
                            </div>



                            <div class="form-group">
                                <div class="col-lg-3 ">
                                     SMTP Server
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.SmtpServer  , new {@class="form-control", placeholder="Smtp Server"})%>
                                    <%: Html.ValidationMessageFor(model => model.SmtpServer )%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Sender Email
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.SenderEmail , new {@class="form-control ",  placeholder="Sender Email "})%>
                                    <%: Html.ValidationMessageFor(model => model.SenderEmail)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Sender Password
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.PasswordFor(model => model.SenderPassword , new {@class="form-control ", placeholder="Sender Password"})%>
                                    <%: Html.ValidationMessageFor(model => model.SenderPassword)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Port Address
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.PortAddress , new {@class="form-control ", placeholder="Port Address "})%>
                                    <%: Html.ValidationMessageFor(model => model.PortAddress)%>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <div class="col-lg-3 ">
                                    Subject
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.Subject , new {@class="form-control ", placeholder="Subject"})%>
                                    <%: Html.ValidationMessageFor(model => model.Subject)%>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <div class="col-lg-3 ">
                                    Body Content
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.BodyContent , new {@class="form-control", placeholder="Body Content"})%>
                                    <%: Html.ValidationMessageFor(model => model.BodyContent)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Use SSL
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.CheckBoxFor(model => model.UseSsl , new { @id="UseSsl", @value="True"})%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    To Mail (Employee Portal)
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextAreaFor(model => model.ToMailId , new {@class="form-control", placeholder="To Mail Id"})%>
                                    <%: Html.ValidationMessageFor(model => model.ToMailId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    CC Mail (Employee Portal)
                                </div>
                                <div class="col-lg-9 ">
                                    <%: Html.TextAreaFor(model => model.CcMailId , new {@class="form-control", placeholder="Cc Maild Id"})%>
                                    <%: Html.ValidationMessageFor(model => model.CcMailId)%>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    <input type="hidden" class="form-control" id="a" name="a">
                                </div>
                                <div class="col-lg-9">
                                    <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button type="button" id="btnTestMail" name="btnTestMail"  style="display: none;" class="btn btn-success"><span class="glyphicon glyphicon-check"></span> Test Mail</button>
                                    <button type="button" id="btnClose" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                    <img id="loadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
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

