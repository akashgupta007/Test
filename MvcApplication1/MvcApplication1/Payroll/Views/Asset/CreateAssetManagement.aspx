<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.AssetManagementViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateAssetMangement
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/CreateAssetManagement.js") %>"></script>
    
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>


    <form id="CreateAssetMangement" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row"> 
                <div class="col-lg-12">
                    <div class="panel panel-primary" id="EditPanel">
                        <div class="panel-heading">
                            <h3 class="panel-title">Asset</h3>
                        </div>
                        <div class="panel-body ">
                            <div class="form-group" >
                                <div class="col-lg-2">
                                    Category    <%: Html.ValidationMessageFor(model => model.CategoryId)%>                            
                                  <input type="hidden" id="AssetId" name="AssetId" />
                                      <%: Html.DropDownListFor(model => model.CategoryId ,Model.AssetCategorylist  , new {@class="form-control", @onchange="FillDdlModelGet(this)" })%>
                                   
                                </div>
                                 <div class="col-lg-2">
                                 Model                                
                                    <select name="ModelId" id="ModelId" class="form-control">   </select>
                                </div>
                                <div class="col-lg-2">
                                    Make                               
                                    <select name="MakeId" id="MakeId" class="form-control">
                                    </select>
                                </div>
                                <div class="col-lg-2">
                                    Serial Number/Engine Number    <%: Html.ValidationMessageFor(model => model.SerialnoEngineno)%>                            
                                    <%:Html.TextBoxFor(model => model.SerialnoEngineno , new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                                <div class="col-lg-2">
                                    KPI SI No    <%: Html.ValidationMessageFor(model => model.KpiSiNo)%>                            
                                    <%:Html.TextBoxFor(model => model.KpiSiNo , new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                            </div>                           
                            
                            <div class="form-group">
                                <div class="col-lg-2">
                                    Acc SI No    <%: Html.ValidationMessageFor(model => model.AccSiNo)%>                             
                                    <%:Html.TextBoxFor(model => model.AccSiNo , new {@class="form-control", placeholder=""})%>
                                   
                                </div>                            
                                <div class="col-lg-2">
                                    Year of manufacturing     <%: Html.ValidationMessageFor(model => model.YearOfManufacturing)%>                            
                                      <%: Html.TextBoxFor(model => model.YearOfManufacturing , new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                  
                               </div>                            
                                <div class="col-lg-2 ">
                                    Date of Purchase      <%: Html.ValidationMessageFor(model => model.DateOfPurchase)%>                         
                                     <%: Html.TextBoxFor(model => model.DateOfPurchase , new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    
                                </div>                           
                                <div class="col-lg-2">
                                    Date of Addition to Fleet   <%: Html.ValidationMessageFor(model => model.DateOfAdditionFleet)%>                           

                                     <%: Html.TextBoxFor(model => model.DateOfAdditionFleet, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    
                                </div>                            
                                <div class="col-lg-2">
                                    Equipment Number/Fleet Number    <%: Html.ValidationMessageFor(model => model.EquipmentNoFleetNo)%>                            

                                    <%:Html.TextBoxFor(model => model.EquipmentNoFleetNo , new {@class="form-control", placeholder=""})%>
                                    
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-2">
                                    Weight                               
                                    <%:Html.TextBoxFor(model => model.Weight , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Weight)%>
                                </div>                            
                                <div class="col-lg-2">
                                    Height                               

                                    <%:Html.TextBoxFor(model => model.Height , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Height)%>
                                </div>                           
                                <div class="col-lg-2">
                                    Length                               

                                    <%:Html.TextBoxFor(model => model.Length , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Length)%>
                                </div>                            
                                <div class="col-lg-2">
                                    Width                               

                                    <%:Html.TextBoxFor(model => model.Width , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Width)%>
                                </div>                           
                                <div class="col-lg-2">
                                    Date of Removal to Fleet                              

                                     <%: Html.TextBoxFor(model => model.DateRemovalFleet , new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.DateRemovalFleet)%>
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="col-lg-2 ">
                                   Reason                              

                                    <%:Html.TextBoxFor(model => model.Reason , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Reason)%>
                                </div>                            
                                <div class="col-lg-2">
                                  Value                              

                                    <%:Html.TextBoxFor(model => model.Value, new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Value)%>
                                </div>                           
                                <div class="col-lg-2">
                                 Duties                              

                                    <%:Html.TextBoxFor(model => model.duties, new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.duties)%>
                                </div>                           
                                <div class="col-lg-2">
                                  Taxes                              

                                    <%:Html.TextBoxFor(model => model.Taxes, new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Taxes)%>
                                </div>                           
                                <div class="col-lg-3 ">
                                   Total                               
                                    <%: Html.TextBoxFor(model => model.Total , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Total)%>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-2">
                                   Depreciation As per Co Till the Date of Removal of Fleet                                
                                    <%: Html.TextBoxFor(model => model.DepreciationAsPerCo , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.DepreciationAsPerCo)%>
                                </div>                            
                                <div class="col-lg-2">
                                  Depreciation As per IT Till the Date of Removal of Fleet                                      
                                    <%: Html.TextBoxFor(model => model.DepreciationAsPerIT , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.DepreciationAsPerIT)%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span>Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>
                                </div>
                            </div>
                            <div style="text-align: center">
                                <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12" style="margin-top: 5px">
                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                    <b>Show All Assets</b>
                </a>
                <div id="collapseFour" class="panel-collapse collapse">
                    <div id="data" style="overflow-x: auto;">
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
