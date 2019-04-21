<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.LeaveCycleVoidViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Leave Cycle Void
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Leave/LeaveCycleVoid.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

 <%--   <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/Test.js") %>"></script>
    --%>

      <form id="LeaveCycleVoid" novalidate="novalidate">    
        <div class="form-horizontal" style="margin-top: 20px;">
            <div class="row">
                <div class="col-lg-12" style="margin-top:20px;">                                                             
                    <div id="DetailPanel"  class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Void Leave Cycle </h3>
                        </div>
                        <div class="panel-body ">                                                                       
  <div class="col-lg-12" style="margin-top:20px;">
                  <div id="data" >
                            </div>
      </div>
                             </div>
                    </div>                                                
                        </div>                                            
                    </div>
             <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                </div>
           <div class="col-lg-12">
                        <div id="childData">



                            
                    </div>

 </div>
    </form>   
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>
</asp:Content>