<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.LeaveCycleViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Process Leave Cycle
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/ProcessLeaveCycle.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

     <%
    
    AjaxOptions ajaxOptions = new AjaxOptions
    {
        UpdateTargetId = "data",
        InsertionMode = InsertionMode.Replace,
        HttpMethod = "POST",
        LoadingElementId = "loading_ddl",
    };
        %>
       <% Ajax.BeginForm(ajaxOptions); %>
    <div class="form-horizontal" style="margin-top:10px;">
          <div class="row">
        <div class="col-lg-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <h3 class="panel-title">Process Leave Cycle</h3>
                                        </div>
                                        <div class="panel-body ">                                          
                                                   <%-- <div class="col-md-1">Date Range:</div>
                                                    <div class="col-md-2">
                                                          <%: Html.DropDownListFor(model => model.DateRange ,Model.DateRangeList, new {@class="form-control parent"})%>                                                           
                                                    </div>--%>
                                                    <div class="col-md-1">Start Date:</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.StartdDate, new {@class="form-control DBPicker parent", placeholder="Enter Start Date" })%>
                                                        <%: Html.ValidationMessageFor(model => model.StartdDate)%>
                                                    </div>
                                                    <div class="col-md-1">End Date:</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.EndDate, new {@class="form-control DBPicker parent", placeholder="Enter End Date" })%>
                                                        <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                                    </div>
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-2">
                                                          <button style="vertical-align:central; text-align:left; " id="btnGo" type="submit" value="Go" id="btnSearch" name="command" class="btn  btn-success"><span class='glyphicon glyphicon-arrow-right'></span> Go</button>
                                           
                                                    </div>
     
                                        </div>
                                    </div>                                
                                 <div style="text-align: center">
                            <img id="loading_ddl" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                </div>


              <div class="col-lg-12">

                              <div id="data" style="overflow:auto; "> 


                            </div>

                </div>

              <div class="col-lg-12">

                              <div id="data1" style="overflow:auto; "> 


                            </div>

                </div>
            </div> 
    </div>
       <%Html.EndForm(); %>
     <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>
</asp:Content>