

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpAttendanceEntryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Payroll Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeSalaryTransfer.js") %>"></script>

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
        LoadingElementId = "loading",
        
    };
    
    
     %>
    <% Ajax.BeginForm(ajaxOptions); %>

        <div class="form-horizontal" style="margin-top: 10px;">

            
                <div class="row">
                    <div class="col-lg-12">
                        <div id="DetailPanel" class="panel panel-primary">

                            <div class="panel-heading">
                               <span  style="text-align:left;"class="panel-title" >Employee Salary Transfer Details</span>
                                <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeSalaryTransfer.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>
                          
                            </div>
                         
                            <div class="panel-body" style="align-items:center; ">
                                
                                
                             <%:Html.Partial("PayrollDropDownList",new PoiseERP.Areas.Payroll.Models.PayrollUtil()) %>
                              
                            </div>
                         <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
         

  
                                <div id="data" style="overflow:auto;margin-left:5px;margin-right:5px; text-align:center; " >
                                </div>
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


