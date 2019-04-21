﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.AssetViewModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateAsset
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/CreateAsset.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 



 <form id="CreateAsset" novalidate="novalidate">
        
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                           
                             <span  style="text-align:left;"class="panel-title" >Create Asset  </span>                           

                         <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Asset/CreateAsset.html") %>">
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
                            <h3 class="panel-title"><span id="panelHeader"></span>  </h3>
                        </div>
                        <div  class="panel-body ">


                               <div class="form-group">
                                <div class="col-lg-3 ">
                            Name 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       <input type="hidden" id="AssetId" name="AssetId" />
                                    <%: Html.TextBoxFor(model => model.AssetName , new {@class="form-control", placeholder="Enter Asset Name "})%>
                                    <%: Html.ValidationMessageFor(model => model.AssetName)%>
                                    
                                </div>
                            </div>  
                              
                           

                             <div class="form-group">
                                <div class="col-lg-3 ">
                            Description 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                       <%:Html.TextBoxFor(model => model.AssetDescription , new {@class="form-control", placeholder="Enter Asset Description "})%>
                                    <%: Html.ValidationMessageFor(model => model.AssetDescription)%>
                                    
                                </div>
                            </div>  
                              
                           


                             <div class="form-group">
                                <div class="col-lg-3 ">
                              Interest (%)  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                       <%: Html.TextBoxFor(model => model.DefaultInterestPct , new {@class="form-control", placeholder="(%) max 100%  "})%>
                                    <%: Html.ValidationMessageFor(model => model.DefaultInterestPct)%>
                                    
                                </div>
                            </div>  
                              
                           


                             <div class="form-group">
                                <div class="col-lg-3 ">
                               Expense Per Month
                                </div>
                                <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.ExpensePerMonth , new {@class="form-control", placeholder="Enter  Expense Per Month "})%>
                                    <%: Html.ValidationMessageFor(model => model.ExpensePerMonth)%>
                                    
                                </div>
                            </div> 
                            
                              
                             <div class="form-group" style="display:none;">
                                <div class="col-lg-3 ">
                            Taxable Function 
                                </div>
                                <div class="Form-control col-lg-9 ">
                                  
                                     <%: Html.DropDownListFor(model => model.TaxableFunction ,Model.Functionlist  , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.TaxableFunction)%>
                                    
                              
                                    
                                </div>
                            </div>  
                             
                              
                             <div class="form-group">
                                <div class="col-lg-3 ">
                          Additional Charges if any  
                                </div>
                                <div class="Form-control col-lg-9 ">
                                        <%: Html.TextBoxFor(model => model.AddtionalCharges , new {@class="form-control", placeholder="Enter Additional Charges "})%>
                                    <%: Html.ValidationMessageFor(model => model.AddtionalCharges)%>
                                    
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

    
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
   

</asp:Content>