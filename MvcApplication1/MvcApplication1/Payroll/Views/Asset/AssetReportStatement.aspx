<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.AssetreportViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AssetReportStatement
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/AssetReportStatement.js") %>"></script>

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
  
        <div id="page-wrapper" class="col-lg-9">
            <div class="row">
                <div class="col-lg-12" style="margin-top:20px;">
                    <div class="panel panel-primary">
                        <div class="panel-heading" style="text-align: center;">
                           
                            <span  style="text-align:left;"class="panel-title" >Asset Report Statement  </span>                           

                         <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Asset/AssetReportStatement.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>

                        </div>
                        <div class="panel-body">
                            <div class="form-group" style="text-align: center;">
                                <div class="col-md-1" >Asset Name</div>
                                <div class="col-md-2">
                                   
                                    <%: Html.DropDownListFor(model => model.AssetId , Model.AssetList , new {@class="form-control"})%>
                                  
                                </div>

                                <div class="col-md-1" > Employee Name</div>
                                <div class="col-md-2">
                                   
                                    <%: Html.DropDownListFor(model => model.EmployeeId , Model.Employeelist , new {@class="form-control"})%>
                                  
                                </div>

                                <div class="col-md-1" > Asset  Created Date</div>
                                <div class="col-md-2">         
                                    
                                
                                   <%: Html.TextBoxFor(model => model.AssetCreatedDt  , new { @class = "form-control DBPicker ",  @readonly="readonly", placeholder = "Enter Asset Created Date" })%>
                                   
                               
                                </div>

                                


                                <div class="col-md-1">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="submit"id="btnSearch" name="btnSearch" class="btn  btn-success enabling" ><span class="glyphicon glyphicon-search"></span> Search</button>
                                </div>
                                 <div class="col-md-1">
                                     <button type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" ><span class="glyphicon glyphicon-export"></span> Export To Excel</button>
                                     <%--<input type="button" class="btn btn-success" id="btnExportToExcel" value="Export To Excel" />--%>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12">


                        <div></div>                     
                               
                            <div id="data" style="overflow:auto; "> 
                            </div>
                         <br />
                    
                      <div><iframe id="ExcelFrame" style="display:none"></iframe></div>
                    </div>
                </div>
            </div>
        </div>
         
        <div >     
                    
                <div class="content"><img  style="display: none;"  id="loading"  src="<%= Url.Content("~/Images/loading.gif") %> " /></div>
          
        </div>
        <%Html.EndForm(); %>
    



</asp:Content>
 