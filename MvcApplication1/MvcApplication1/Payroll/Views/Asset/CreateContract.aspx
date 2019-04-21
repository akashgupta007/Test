<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.ContractViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Contract
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/Contract.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>


    <form id="Contract" novalidate="novalidate">

        <div class="form-horizontal">

            <div class="row">
               

                <div class="col-lg-12">
                    <div class="panel panel-primary" id="EditPanel">
                        <div class="panel-heading">
                            <h3 class="panel-title">Contract Management</h3>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-lg-2">
                                    Customer Name        <%: Html.ValidationMessageFor(model => model.CustomerId)%>                        
                                  <input type="hidden" id="ContractId" name="ContractId" />
                                    <%: Html.DropDownListFor(model => model.CustomerId ,Model.Customerlist  , new {@class="form-control", @onchange="FillDdlSiteGet(this)" })%>
                                   
                                </div>

                                <div class="col-lg-2 ">
                                    Site Address                                
                                    <select name="SiteAddressId" id="SiteAddressId" class="form-control">
                                        <option>--Select--</option>
                                    </select>

                                </div>
                                <div class="col-lg-2 ">
                                    Sales Manager       <%: Html.ValidationMessageFor(model => model.EmployeeId)%>                           
                                 <%: Html.DropDownListFor(model => model.EmployeeId ,Model.SalesManagerlist  , new {@class="form-control"})%>
                                   
                                </div>
                                <div class="col-lg-2 ">
                                    Work Order No   <%: Html.ValidationMessageFor(model => model.WorkOrderNo)%>
                                    <%:Html.TextBoxFor(model => model.WorkOrderNo , new {@class="form-control", placeholder="ABCD-001-2017"})%>
                                   
                                </div>
                                <div class="col-lg-2">
                                    Multi-Shift     <%: Html.ValidationMessageFor(model => model.MultiShift)%>
                                                  
                                                        <%: Html.RadioButtonFor(model => model.MultiShift , "True",  new { @id="rbMLYes"  })%>Yes &nbsp;
                                                        <%: Html.RadioButtonFor(model => model.MultiShift , "False", new { @id="rbMLNo" , @checked=true})%>No
                                </div>
                                <div class="col-lg-2">
                                 </div>
                            </div>

                            <div class="form-group">

                                <div class="col-lg-2">
                                    Work Order Date     <%: Html.ValidationMessageFor(model => model.WorkOrderDate)%>
                               
                                    <%: Html.TextBoxFor(model => model.WorkOrderDate , new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    
                                </div>

                                <div class="col-lg-2 ">
                                    Start Date Of Work Order                             

                                    <%: Html.TextBoxFor(model => model.StartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.StartDate)%>
                                </div>

                                <div class="col-lg-2 ">
                                    End Date Of Work Order   <%: Html.ValidationMessageFor(model => model.EndDate)%>
                                

                                    <%: Html.TextBoxFor(model => model.EndDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    
                                </div>

                                <div class="col-lg-2">
                                    Description Of Work Order                               
                                    <%:Html.TextBoxFor(model => model.Desciption , new {@class="form-control", placeholder=""})%>
                                    <%: Html.ValidationMessageFor(model => model.Desciption)%>
                                </div>
                                <div class="col-lg-2 ">
                                    Quantity             <%: Html.ValidationMessageFor(model => model.Quantity)%>                    
                                    <%:Html.TextBoxFor(model => model.Quantity , new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                                 <div class="col-lg-2">
                                 </div>
                            </div>
                            <div class="form-group">

                                <div class="col-lg-2">
                                    Billing Units(1)           <%: Html.ValidationMessageFor(model => model.BillingUnit1)%>                      
                                    <%: Html.TextBoxFor(model => model.BillingUnit1, new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                                <div class="col-lg-2">
                                    &nbsp;             
                                    <%: Html.DropDownListFor(model => model.BillingUnitId1 ,Model.BillingUnitlist1  , new {@class="form-control"})%>
                                    <%--<%: Html.ValidationMessageFor(model => model.BillingUnitId1)%>--%>
                                </div>
                                <div class="col-lg-2">
                                    Billing Units(2)      <%: Html.ValidationMessageFor(model => model.BillingUnit2)%>                            
                                    <%: Html.TextBoxFor(model => model.BillingUnit2, new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                                <div class="col-lg-2">
                                    &nbsp;     
                                    <%: Html.DropDownListFor(model => model.BillingUnitId2 ,Model.BillingUnitlist2  , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.BillingUnitId2)%>
                                </div>
                                <div class="col-lg-2 ">
                                    Total       <%: Html.ValidationMessageFor(model => model.Total)%>                          
                                    <%:Html.TextBoxFor(model => model.Total, new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                                 <div class="col-lg-2">
                                 </div>
                            </div>


                            <div class="form-group">
                                <div class="col-lg-2 ">
                                    Month Value           <%: Html.ValidationMessageFor(model => model.MonthValue)%>                         
                                    <%:Html.TextBoxFor(model => model.MonthValue, new {@class="form-control", placeholder=""})%>
                                   
                                </div>
                                <div class="col-lg-2 ">
                                    Billing Amt(1)         <%: Html.ValidationMessageFor(model => model.BillingAmt1)%>                          
                                    <%: Html.TextBoxFor(model => model.BillingAmt1, new {@class="form-control", placeholder="Enter Amount"})%>
                                   
                                </div>
                                <div class="col-lg-2 ">
                                    &nbsp;                             
                                    <label id="BillingAmtId1" for="BillingAmtId1" class="form-control"></label>
                                </div>
                                <div class="col-lg-2 ">
                                    Billing Amt(2)             <%: Html.ValidationMessageFor(model => model.BillingAmt2)%>                    
                                    <%: Html.TextBoxFor(model => model.BillingAmt2, new {@class="form-control", placeholder="Enter Amount"})%>
                                  
                                </div>
                                <div class="col-lg-2 ">
                                    &nbsp;                               
                                    <label id="BillingAmtId2" for="BillingAmtId2" class="form-control"></label>
                                </div>
                                 <div class="col-lg-2">
                                 </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-2">
                                    Overtime Calculation      <%: Html.ValidationMessageFor(model => model.Overtime)%>                          
                                    <%: Html.TextBoxFor(model => model.Overtime, new {@class="form-control",placeholder=""})%>
                                    
                                </div>
                                <div class="col-lg-2 ">
                                    &nbsp;                                 

                                    <%: Html.DropDownListFor(model => model.OvertimeId ,Model.OvertimeCalculationlist  , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.OvertimeId)%>
                                </div>
                               
                                <div  class="col-lg-2">
                                    Transportation Out Wards                              
                                    <select name="TransportationOutId" id="TransportationOutId" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="1">Yes</option>
                                        <option value="2">No</option>
                                     </select>   
                                </div>                                   
                                 <div  class="col-lg-2">     
                                         &nbsp;                           
                                    <%: Html.TextBoxFor(model => model.TransportationOutAmount, new {@class="form-control",placeholder="Amount"})%>
                                   
                                </div>
                                 <div  class="col-lg-2">
                                    Transportation In Wards                              
                                    <select name="TransportationInId" id="TransportationInId" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="1">Yes</option>
                                        <option value="2">No</option>
                                    </select>                               
                                  
                                </div>
                                <div  class="col-lg-2">  
                                        &nbsp;                              
                                    <%: Html.TextBoxFor(model => model.TransportationInAmount, new {@class="form-control",placeholder="Amount"})%>
                                  
                                </div>

                            </div>

                            <div class="form-group">
                                <div id="loadingtext" class="col-lg-2">
                                    Loading
                                </div>
                                <div class="Form-control col-lg-2 ">
                                    <select name="Loading" id="Loading" class="form-control">
                                        <option value="0">--Select--</option>
                                        <option value="1">Yes</option>
                                        <option value="2">No</option>
                                    </select>
                                </div>
                                <div class="Form-control col-lg-2 ">
                                    <%: Html.TextBoxFor(model => model.LoadingAmount, new {@class="form-control",placeholder="Loading Amount"})%>
                                   
                                </div>

                                <div class="col-lg-2 ">
                                    Contract Start Date   <%: Html.ValidationMessageFor(model => model.ContractStartDate)%>                             
                                    <%: Html.TextBoxFor(model => model.ContractStartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Start date"})%>
                              
                                      </div>
                                <div class="col-lg-2 ">
                                    Contract End Date                                
                                    <%: Html.TextBoxFor(model => model.ContractEndDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="End date"})%>
                                </div>
                                 <div class="col-lg-2">
                                 </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-2 ">
                                    Billing Period -  To   <%: Html.ValidationMessageFor(model => model.BillingPeriodTo)%>
                                     <%: Html.TextBoxFor(model => model.BillingPeriodTo, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    
                                </div>
                                <div class="Form-control col-lg-2 ">
                                    Billing Period From     <%: Html.ValidationMessageFor(model => model.BillingPeriodFrom)%>
                                     <%: Html.TextBoxFor(model => model.BillingPeriodFrom, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                   
                                </div>
                                <div class="col-lg-2 ">
                                    KPI Time-To   <%: Html.ValidationMessageFor(model => model.KPITimeTo)%>
                                    
                                    <%: Html.TextBoxFor(model => model.KPITimeTo, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                    
                                </div>
                                <div class="Form-control col-lg-2 ">
                                    KPI Time-From      <%: Html.ValidationMessageFor(model => model.KPITimeFrom)%>
                                    <%: Html.TextBoxFor(model => model.KPITimeFrom, new {@class="form-control DBPicker", @readonly="readonly", placeholder=""})%>
                                  
                                </div>
                                <div class="Form-control col-lg-2">
                                         Tax
                                       <input type="hidden" id="ComputingItemList" name="ComputingItemList" />
                                      <input type="hidden" id="ComputingItemNameList" name="ComputingItemNameList" />
                                        <select name="ComputingItemId" id="ComputingItemId" class="form-control" multiple="multiple">
                                            <option>--Select--</option>
                                        </select>
                                        <%--<%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@id="ComputingItem", @class="form-control"})%>  --%>
                                    </div>
                                 <div class="col-lg-2">
                                 </div>
                            </div>                           

                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span>Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span>Update</button>
                                    <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>

                                </div>

                            </div>


                            <%-- <div id="data" style="overflow-x: auto;">
                             </div>--%>


                            <div style="text-align: center">
                                <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                            </div>


                        </div>
                    </div>
                </div>

            </div>
            <div class="col-lg-12" style="margin-top: 5px">

                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                    <b>Show Contract List</b>
                </a>

                <div id="collapseFour" class="panel-collapse collapse">
                    <div id="data" style="overflow-x: auto;">
                    </div>
                </div>
            </div>
        </div>
    </form>

   


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
  

</asp:Content>
