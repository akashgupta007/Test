﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.CcaRangeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CCA Range
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Leave/CCARange.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

<form id="CCARange" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top:10px;">
          <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <%--<h3 class="panel-title">CCA Range Details  </h3>--%>
                             <span  style="text-align:left;"class="panel-title">CCA Range Defined </span>
                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Payroll/CCARange.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record  </button>
                                </div>
                            </div>                            
                         <div id="data" style="overflow:auto; "> 
                            </div>
                 
                        </div>
                        <div style="text-align:center;margin-bottom:5px; ">
                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>                       
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div  class="panel-body ">  
                              <div class="form-group"> 
                                 <div class="col-lg-3"> Pay Range 
                                 </div>                                
                                                                                                  
                                            </div>                                                                                                                                     
                               <div class="form-group"> 
                                 <div class="col-lg-3"> Minimum 
                                 </div>                                
                                     <div class="Form-control col-lg-9"> 
                                           <input type="hidden" id="Id" name="Id" />
                                         <%: Html.TextBoxFor(model => model.StartPayRange, new {@class="form-control", placeholder="Enter Min Pay Range "})%>
                                     <%: Html.ValidationMessageFor(model => model.StartPayRange)%>
                                              </div>                                                             
                                            </div>                                
                               <div class="form-group"> 
                                 <div class="col-lg-3"> Maximum
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                                  <%: Html.TextBoxFor(model => model.EndPayRange, new {@class="form-control", placeholder="Enter Max Pay Range "})%>
                                     <%: Html.ValidationMessageFor(model => model.EndPayRange)%>                                  
                                              </div>                                                             
                                            </div>  
                            <div class="form-group"> 
                                 <div class="col-lg-3"> CCA 
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                                  <%: Html.TextBoxFor(model => model.Cca, new {@class="form-control", placeholder="Enter CCA "})%>
                                     <%: Html.ValidationMessageFor(model => model.Cca)%>                            
                                              </div>                                                             
                                            </div>
                            <div class="form-group"> 
                                 <div class="col-lg-3"> Location 
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                                  <%: Html.DropDownListFor(model => model.LocationId ,Model.LocationList, new {@class="form-control"})%>                             
                                              </div>                                                             
                                            </div>                                                                                                                             
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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