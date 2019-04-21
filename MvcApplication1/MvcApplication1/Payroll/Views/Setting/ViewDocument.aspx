<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.DocumentViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViewDocument
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/ViewDocument.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form method="post" id="AttachFile" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">

                <div class="col-lg-12">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Download Document Details</h3>--%>
                            <span  style="text-align:left;"class="panel-title" > View Document </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/ViewDocument.html") %>">
                            <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>

                        <div class="panel-body ">
                             <input type="hidden" id="DocumentObjectId" name="DocumentObjectId" class="form-control"/>
                            <input type="hidden" id="DocumentId" name="DocumentId" class="form-control"/>
                            <input type="hidden" id="flag" name="flag" />
                            <div id="data" style="overflow:auto;">
                            </div>
                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div id="childDiv" style="display: none;">
            <div class="col-lg-12">
                <div id="childData"></div>
            </div>
        </div>

    </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>


</asp:Content>
