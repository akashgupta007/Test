<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpReimbursementItemViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
ReimbursementItem
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Reimbursement/EmployeeReimbursementItem1.js") %>"></script>
 

   
    <form method="post" id="ReimbursementItem">

        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title"> Employee Reimbursement Item  </span>

                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record</button>
                                </div>

                            </div>
                            <div id="data" style="overflow: auto;">
                            </div>
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
                                     Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="EmpReimbursementItemId" name="EmpReimbursementItemId" />
                                  <%: Html.TextBoxFor(model => model.ReimbursementItemName  , new { @class = "form-control  updateform", placeholder = "Enter Name" })%>
                                    <%: Html.ValidationMessageFor(model => model.ReimbursementItemName ) %>
                                </div>
                            </div>

                           
                             <div class="form-group">
                                <div class="col-lg-3 ">
                             Description
                                </div>
                                <div class="Form-control col-lg-9">
                                    <%:Html.TextAreaFor(model => model.ReimbursementItemDesc , new {@class="form-control updateform", placeholder="Enter Description "})%>
                                    <%: Html.ValidationMessageFor(model => model.ReimbursementItemDesc)%>                                    
                                   </div>
                                  
                            </div>


                      

                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                  
                                     <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnInsert" class="btn  btn-success enabling" name="Command"  value="Insert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="submit" id="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling " name="Command" value="Update"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling "><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
        <div id="MsgDiv" style="color:green;">
            <label id="lblError"><%:ViewData["Msg"] %></label>
        </div>
    <%--</div>--%>
</asp:Content>
