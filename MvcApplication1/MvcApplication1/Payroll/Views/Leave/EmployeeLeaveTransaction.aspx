<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmployeeLeaveTransactionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Leave Transaction
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Leave/EmployeeLeaveTransaction1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

        <form method="post" id="EmployeeLeaveTransaction">
        <div class="form-horizontal" style="margin-top: 10px;">
           <%--  <div class="container ">
                   <div id="content">--%>
                       <div class="row">
                           <div class="panel panel-primary" >
                               <div class="panel-heading">
               <%--             <h3 class="panel-title"><span id="panelHeader">Employee Leave Transaction</span></h3>--%>
                                      <span  style="text-align:left;"class="panel-title">Employee Leave Transaction</span>
                                    <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Leave/EmployeeLeaveTransaction.html")%>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>                            
                        </div>
                        <div  class="panel-body">

                            <div class="col-lg-4" >
                                         <div class="col-md-3">
                                             Show History
                                          </div>
                                     <div class=" col-lg-9">
                                         <input type="checkbox" id="chkshow_History"/>
                                    </div>
                                     </div>


                         <div class="col-lg-8" style="margin-top: 2px; margin-bottom:5px; text-align: center;">
                        <div class="col-md-2">
                            Employee:  
                        </div>
                        <div class=" col-lg-4" text-align: center;">
                             <select name="EmployeeId" id="EmployeeId" class="form-control"><option>--Select--</option></select> 
                        </div>
                         <div class=" col-lg-2" >
                             <img id="loading_ddl" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                         </div>
                        
                    </div>
                             
                                <div class="col-lg-12" id="tabsdiv">

                              
                           <%-- <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                            <li id="tabP" class="active list-group-item-info"><a href="#tabPersonalInfo" data-toggle="tab">Personal Info</a></li>
                            <li id="tabC"><a href="#tabCommunicationInfo" class="list-group-item-info" data-toggle="tab">Communication Info</a></li>
                            <li id="tabG"><a href="#tabGenearlInfo" class="list-group-item-info" data-toggle="tab">General Info</a></li>
                            <li id="tabO"><a href="#tabPhotoInfo" class="list-group-item-info" data-toggle="tab">Other Info</a></li>
                            <li id="tabD"><a href="#tabDocumentInfo" class="list-group-item-info" data-toggle="tab">Photo</a></li>
                            <li id="tabParameter"><a href="#tabParameterInfo" class="list-group-item-info" data-toggle="tab">Parameters</a></li>

                        </ul>--%>

                          
                                      </div>

                           <div id="tabcontent" class="col-lg-12">

                               <%--<div>
                                     <iframe id="ExcelFrame" style="display: none"></iframe>
                               </div>--%>
                                 <%-- <div id="my-tab-content" class="tab-content">
                             
                                </div>--%>
                           </div>
                        </div>
                           </div>
                           

                            
                         
                       </div>
                   </div>
            <%-- </div>
        </div>--%>
        </form>
    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>
