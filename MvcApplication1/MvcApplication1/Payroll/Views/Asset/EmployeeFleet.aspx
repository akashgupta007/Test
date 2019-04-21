<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.OperatorFleetAssignment>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Fleet
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/OperatorFleetAssignment.js") %>"></script>
    <%-- <link href="<%= Url.Content("~/Scripts/select2.css") %>" rel="stylesheet" />
    <script src="<%= Url.Content("~/Scripts/select2.js") %>"></script> --%> 
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
            //$("#FleetNo").select2();
        });
    </script>
    <style>
         select#EmployeeId {
            max-width: 200px;
        }
         input#opeartorStartDate {
            max-width: 150px;
        }
          input#opeartorEndDate {
                       max-width: 150px;

        }
           input#RoomRentPaid {
            max-width: 150px;
        }
            input#ArrearRoomRentPaid {
           max-width: 150px;
        }
             input#Travels {
           max-width: 150px;
        }
              input#Food {
          max-width: 150px;
        }
               input#MachineIncentives {
           max-width: 150px;
        }
                input#TotalExpenses {
           max-width: 150px;
            
        }
                 
    </style>

    <form id="operaoroFleet" novalidate="novalidate">

        <div class="form-horizontal">

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-primary" id="EditPanel">
                        <div class="panel-heading">
                            <h3 class="panel-title">Fleet Assignment</h3>
                        </div>
                        <div class="panel-body ">
                           <%-- <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record  </button>--%>
                            <div class="form-group">                               
                                <div class="col-lg-2">
                                    <input type="hidden" id="AssetId" name="AssetId" />
                                    <input type="hidden" id="MachineId" name="MachineId" />
                                    <input type="hidden" id="CustomerId" name="CustomerId" />
                                    <input type="hidden" id="SiteAddressId" name="SiteAddressId" />
                                    <input type="hidden" id="ContractEmployeeId" name="ContractEmployeeId" />
                                    <input type="hidden" id="hfContractStratDate" name="hfContractStratDate" />
                                    <input type="hidden" id="hfContractEndDate" name="hfContractEndDate" />

                                    Fleet No  <%: Html.ValidationMessageFor(model => model.FleetNo)%>
                                    <%: Html.DropDownListFor(model => model.FleetNo ,Model.AssetFleetlist, new {@class="form-control", @onchange="FillListByFleetNo(this)"})%>
                                    
                                </div>
                                <div class="col-lg-2">
                                    Contract  <%: Html.ValidationMessageFor(model => model.ContractId)%>
                                    <%: Html.DropDownListFor(model => model.ContractId ,Model.ContractList,new {@class="form-control", @onchange="FillListByContractId(this)" } )%>
                                </div>
                                <div class="col-lg-2">
                                    Customer
                                     <input type="text" class="form-control" id="CustomerName" disabled="disabled"/>
                                </div>
                                <div class="col-lg-2">
                                    SiteAddress
                                    <input type="text" class="form-control" id="AddressName" disabled="disabled"/>
                                </div>
                                <div class="col-lg-2">
                                    Sales Manager
                                     <input type="text" class="form-control" id="ContractEmployeeName" disabled="disabled"/>
                                </div>
                                 <div class="col-lg-2">
                                    Contract  Start Date 
                                 <%: Html.TextBoxFor(model => model.ContractStartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Date"})%>
                                </div>
                                </div>                         
                                <fieldset>
                                <legend>Trasportation outward</legend>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                       Date Of Dispatch   <%: Html.ValidationMessageFor(model => model.TrasportationOutWardDispatchStartDate)%>
                                         
                                    <%: Html.TextBoxFor(model => model.TrasportationOutWardDispatchStartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Dispatch"})%>
                                    </div>
                                    <div class="col-lg-2">
                                        Challan No  <%: Html.ValidationMessageFor(model => model.TrasportationCheckNoOutWard)%>
                                    <%: Html.TextBoxFor(model => model.TrasportationCheckNoOutWard ,new {@class="form-control", placeholder="Enter Challan No" } )%>
                                    </div>
                                    <div class="col-lg-2">
                                        Recipt date <%: Html.ValidationMessageFor(model => model.TrasportationOutWardReciptdate)%>
                                   <%: Html.TextBoxFor(model => model.TrasportationOutWardReciptdate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Recipt"})%>
                                    </div>
                                    <div class="col-lg-2">
                                        Trasporter Name  <%: Html.ValidationMessageFor(model => model.TrasportationNameOutWard)%>
                                     <%: Html.DropDownListFor(model => model.TrasportationNameOutWard ,Model.TransporteroutwardList,new {@class="form-control"} )%>
                                        <%-- <%: Html.TextBoxFor(model => model.TrasportationNameOutWard ,new {@class="form-control" } )%> --%>
                                    </div>
                                    <div class="col-lg-2">
                                        Amount   <%: Html.ValidationMessageFor(model => model.AmountInWard)%>
                                    <%: Html.TextBoxFor(model => model.AmountInWard, new {@class="form-control" , placeholder="Enter Amount"})%>

                                    </div>
                                    
                                     <div class="Form-control col-lg-2">
                                         Tax
                                        <input type="hidden" id="ComputingItemList" name="ComputingItemList" />
                                        <select name="ComputingItemId" id="ComputingItemId" class="form-control" multiple="multiple">
                                            <option>--Select--</option>
                                        </select>
                                        <%--<%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@id="ComputingItem", @class="form-control"})%>  --%>
                                    </div>
                                </div>
                            </fieldset>
                                <fieldset style="display:none" id="EmpListDiv">
                                <legend>Operator Information</legend>
                            <div class="form-group">
                                <table id='data_TableInfo' style="width: 100%">
                                        <tr>

                                            <th>Operator</th>
                                            <th>Start </th>
                                            <th>End</th>
                                            <th>Room</th>
                                            <th>Arrear Room</th>
                                            <th>Travel</th>
                                            <th>Food</th>
                                            <th>Machine</th>
                                            <th>Total</th>
                                            <th></th>
                                        </tr>
                                        <tr class="appendableDIV">
                                            <td>
                                                <div class="form-group">
                                                    <select id="EmployeeId" name="EmployeeId" class="form-control"></select>

                                                     </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                <%: Html.TextBoxFor(model => model.opeartorStartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Start Date"})%>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                 <%: Html.TextBoxFor(model => model.opeartorEndDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select End Date"})%>

                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                 <%: Html.TextBoxFor(model => model.RoomRentPaid, new {@class="form-control"})%>
                                                </div>
                                            </td>

                                            <td>
                                                <div class="form-group">
                                              <%: Html.TextBoxFor(model => model.ArrearRoomRentPaid, new {@class="form-control" })%>                                  

                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                <%: Html.TextBoxFor(model => model.Travels, new {@class="form-control"})%>                                  

                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                 <%: Html.TextBoxFor(model => model.Food, new {@class="form-control"})%> 
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                <%: Html.TextBoxFor(model => model.MachineIncentives, new {@class="form-control"})%>  
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                <%: Html.TextBoxFor(model => model.TotalExpenses, new {@class="form-control",@onclick="AddValues(this)",@onmousehover="AddValues(this)",@onkeyup="AddValues(this)"})%>                                  

                                                </div>
                                            </td>
                                            <td id="dButton">
                                                <button class="btn btn-primary" type="button" onclick="addRow();"><i class="glyphicon glyphicon-plus"></i></button>
                                                <button class="btn btn-danger" type="button" onclick="removeRow(this);"><i class="glyphicon glyphicon-minus"></i></button>
                                                <%--  <button class="btn btn-danger" type="button" onclick="$(this).closest('tr').remove()"><i class="glyphicon glyphicon-minus"></i></button>--%>
                                                
                                            </td>
                                        </tr>
                                   

                                    </table>
                                 <label id="lblError1"></label>
                              
                            </div>
                             </fieldset>
                            <fieldset>
                                <legend>Trasportation Inward</legend>
                                <div class="form-group">
                                    <div class="col-lg-2">
                                        Date Of Dispatch  
                                        <%--<%: Html.ValidationMessageFor(model => model.TrasportationInWardDispatchStartDate)%>--%>
                                    <%: Html.TextBoxFor(model => model.TrasportationInWardDispatchStartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Dispatch"})%>
                                  
                                          </div>
                                    <div class="col-lg-2">
                                        Challan No  
                                        <%--<%: Html.ValidationMessageFor(model => model.TrasportationChecknoInWard)%>--%>
                                    <%: Html.TextBoxFor(model => model.TrasportationChecknoInWard ,new {@class="form-control" , placeholder="Enter Challan No"} )%>
                                  
                                          </div>
                                    <div class="col-lg-2">
                                        Recipt date
                                         <%--<%: Html.ValidationMessageFor(model => model.TrasportationInWardReciptdate)%>--%>
                                   <%: Html.TextBoxFor(model => model.TrasportationInWardReciptdate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Recipt Date"})%>
                                   
                                         </div>
                                    <div class="col-lg-2">
                                        Trasporter Name
                                         <%--<%: Html.ValidationMessageFor(model => model.TrasportationNameInWard)%>--%>

                                      <%: Html.DropDownListFor(model => model.TrasportationNameInWard ,Model.TransporterInwardList,new {@class="form-control"} )%>
                                        <%--<%: Html.TextBoxFor(model => model.TrasportationNameInWard ,new {@class="form-control" } )%>--%>
                                    </div>
                                    <div class="col-lg-2">
                                        Amount
                                         <%--<%: Html.ValidationMessageFor(model => model.AmountOutWard)%>--%>

                                    <%: Html.TextBoxFor(model => model.AmountOutWard, new {@class="form-control"  , placeholder="Enter Amount"})%>
                                    </div>
                                     <div class="Form-control col-lg-2">
                                         Tax
                                        <input type="hidden" id="ComputingItemList1" name="ComputingItemList1" />
                                        <select name="ComputingItemId1" id="ComputingItemId1" class="form-control" multiple="multiple">
                                            <option>--Select--</option>
                                        </select>
                                        <%--<%: Html.DropDownListFor(model => model.PayrollItemId ,Model.PayrollItemList, new {@id="ComputingItem", @class="form-control"})%>  --%>
                                    </div>
                                    <%--<div class="col-lg-2">
                                        Tax
                                     <%: Html.DropDownListFor(model => model.TaxesOut ,Model.TaxeslistOut, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.TaxesOut)%>
                                    </div>--%>
                                </div>
                            </fieldset>

                            <div class="form-group">
                                <div class="col-lg-2">
                                    Machine  Start Date 
                                 <%: Html.TextBoxFor(model => model.StartDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Machine Start Date"})%>
                                </div>
                                <div class="col-lg-2">
                                    Machine End Date 
                                 <%: Html.TextBoxFor(model => model.EndDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Machine End Date"})%>
                                </div>

                                <div class="col-lg-2">
                                    Machine Entry Date 
                                 <%: Html.TextBoxFor(model => model.MachineEntryDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Machine Entry Date"})%>
                                </div>
                                
                                <div class="col-lg-2">
                                    Contract End Date 
                                 <%: Html.TextBoxFor(model => model.ContractEndDate, new {@class="form-control DBPicker", @readonly="readonly", placeholder="Select Date"})%>
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

                            <div style="text-align: center">
                                <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                            </div>

                           
                        </div>
                    </div>



                    <div style="text-align: center; margin-bottom: 5px;">

                        <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>


                </div>
                <div class="col-lg-12" style="margin-top: 5px">

                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                    <b>Show All Fleets</b>
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
