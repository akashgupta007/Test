<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.CustomerManagementViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Customer Management
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/CustomerManagement.js") %>"></script>


    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>
    <style>
        #targetUL {
            width: 280px;
            border: 1px solid silver;
            margin-top: 2px;
            list-style: none;
        }

            #targetUL li {
                margin-left: 40px;
                border-bottom: 1px solid silver;
                height: 26px;
                width: 280px;
                padding-left: 5px;
                padding-top: 8px;
                cursor: pointer;
            }

        .targetUL li {
            margin-left: 40px;
            border-bottom: 1px solid silver;
            height: 26px;
            width: 280px;
            padding-left: 5px;
            padding-top: 8px;
            cursor: pointer;
        }

       
    </style>
    <form id="CustomerManagement" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-12">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title">Create Customer Management   </span>
                        </div>
                       
                        <div class="panel-body ">
                            <fieldset>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        Name <%: Html.ValidationMessageFor(model => model.Name)%>
                                            <input type="hidden" id="CustomerId" name="CustomerId" />
                                        <%: Html.TextBoxFor(model => model.Name, new {@class="form-control", placeholder="Enter Name" })%>
                                        
                                    </div>
                                    <div class="col-md-2">
                                        Phone#      <%: Html.ValidationMessageFor(model => model.Phone)%>                                
                                            <%: Html.TextBoxFor(model => model.Phone, new {@class="form-control phone", placeholder="+00-00-00000000",Maxlength="16" })%>
                                       
                                    </div>
                                    <div class="col-md-2">
                                        Fax #  <%: Html.ValidationMessageFor(model => model.Fax)%>
                                          <%: Html.TextBoxFor(model => model.Fax, new {@class="form-control fax", placeholder="+00-00-00000000",Maxlength="16" })%>
                                        
                                    </div>
                                    <div class="col-md-2">
                                        Email  <%: Html.ValidationMessageFor(model => model.Email)%>
                                            <%: Html.TextBoxFor(model => model.Email, new {@class="form-control", placeholder="Enter Email Address" })%>
                                        
                                    </div>
                                    <div class="col-md-2">
                                        GST       <%: Html.ValidationMessageFor(model => model.Stc)%>                                 
                                            <%: Html.TextBoxFor(model => model.Stc, new {@class="form-control", placeholder="Enter GST Number" })%>
                                       
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2">
                                        Register Address      <%: Html.ValidationMessageFor(model => model.Address)%>                                 
                                            <%: Html.TextAreaFor(model => model.Address, new {@class="form-control", placeholder="Enter Address" })%>
                                       
                                    </div>
                                    <div class="col-md-2">
                                        Pin Code          <%: Html.ValidationMessageFor(model => model.PinCode)%>                             
                                            <%: Html.TextBoxFor(model => model.PinCode, new {@class="form-control num", placeholder="Enter Pin Code",Maxlength="6" })%>
                                        
                                    </div>
                                    <div class="col-md-2">
                                        PAN #            <%: Html.ValidationMessageFor(model => model.PayrolltaxPanno)%>                           
                                            <%: Html.TextBoxFor(model => model.PayrolltaxPanno, new {@class="form-control", placeholder="Enter PAN #" })%>
                                       
                                    </div>
                                    <div class="col-md-2">
                                        TAN #         <%: Html.ValidationMessageFor(model => model.Tan )%>                                 
                                            <%: Html.TextBoxFor(model => model.Tan , new {@class="form-control", placeholder="Enter TAN" })%>
                                      
                                    </div>
                                    <div class="col-md-2">
                                        Premises Code #     <%: Html.ValidationMessageFor(model => model.PremisesCode )%>                                 
                                            <%: Html.TextBoxFor(model => model.PremisesCode , new {@class="form-control", placeholder="Enter Premises Code" })%>
                                        
                                    </div>
                                </div>
                            </fieldset>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Site Address</h3>
                                </div>
                                <div class="panel-body ">
                                    <table id='data_TableInfo' style="width:1200px">
                                        <tr style="width:100%">   
                                                                                 
                                                  <th>Country  <%: Html.ValidationMessageFor(model => model.Country )%>  </th>                                          
                                            <th>State</th>
                                            <th>District</th>
                                            <th>Zone</th>
                                            <th>City</th>
                                            <th>Address</th>
                                             <%--<th style="display:none"></th>   
                                             <th style="display:none"></th>   --%>
                                        </tr>
                                        <tr class="appendableDIV" style="width:1380px">
                                           
                                            <td style="width:180px">
                                                <div class="form-group">
                                             <%: Html.HiddenFor(model => model.AddressId, new {@class="form-control", placeholder="Enter City" })%>
<%: Html.DropDownListFor(model => model.Country, Model.CountryList  , new {@class="form-control",@onchange="CountryChange(this)"})%>

                                                </div>
                                            </td>
                                            <td style="width:180px">
                                                <div class="form-group">
                                                    <select id="States" name="States" class="form-control" onchange="StatesChange(this)"></select>
                                                </div>
                                            </td>
                                            <td style="width:180px">
                                                <div class="form-group">
                                                    <select id="District" name="District" class="form-control"></select>
                                                   
                                                </div>

                                            </td>
                                            <td style="width:180px">
                                                <div class="form-group">
                                                    <%: Html.DropDownListFor(model => model.Zone, new List<SelectListItem> { 
                                                                         new SelectListItem{Text="East", Value="1"},
                                                                          new SelectListItem{Text="West", Value="2"},
                                                                           new SelectListItem{Text="North", Value="3"},
                                                                           new SelectListItem{Text="South", Value="4"},
                                                 }, "--Select Zone--", new {@class="form-control" })%>                                                   
                                                </div>
                                            </td>

                                            <td style="width:180px">
                                                <div class="form-group">
                                                    <%: Html.TextBoxFor(model => model.City, new {@class="form-control", placeholder="Enter City" })%>
                                            
                                                </div>
                                            </td>
                                            <td style="width:180px">
                                                <div class="form-group">
                                                    <%: Html.TextBoxFor(model => model.Address1 , new {@class="form-control", placeholder="Enter address" })%>
                                                   
                                                </div>
                                            </td>
                                            <td style="width:90px;display:none" >
                                                <div class="form-group">
                                             <%: Html.TextBoxFor(model => model.AddressId, new {@class="form-control",@readonly="readonly" })%>
                                                </div>
                                            </td>
                                            <td style="width:90px;display:none">
                                                <div class="form-group">
                                             <%: Html.TextBoxFor(model => model.CustomerId, new {@class="form-control",@readonly="readonly" })%>
                                                </div>
                                            </td>
                                            <td id="dButton">
                                                <button class="btn btn-primary" type="button" onclick="addRow();"><i class="glyphicon glyphicon-plus"></i></button>
                                                <button class="btn btn-danger" type="button" onclick="removeRow(this);"><i class="glyphicon glyphicon-minus"></i></button>
                                                <%--  <button class="btn btn-danger" type="button" onclick="$(this).closest('tr').remove()"><i class="glyphicon glyphicon-minus"></i></button>--%>
                                                
                                            </td>
                                              
                                        </tr>

                                    </table>
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
                    <%--<div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Customer  </button>
                                </div>
                            </div>
                            <div id="data" style="overflow: auto;">
                            </div>
                        </div>--%>
                    <div style="text-align: center; margin-bottom: 5px;">
                        <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>
                </div>
                <div class="col-lg-12" style="margin-top: 5px">
                  
                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                                    <b>Show Customer</b>
                                </a>
                           
                       
                        <div id="collapseFour" class="panel-collapse collapse">
                             <div id="data" style="overflow-x: auto;">
                             </div>
                        </div>
                  
                </div>
            </div>

        </div>


        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
                <button type="button" name="new" onclick='ChildRecordReset()' style="display: none" class='btn btn-info btn-xs'><span class='glyphicon glyphicon-plus'></span>New Record</button>
                <input type="hidden" id="flag" name="flag" />


            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Address List</h3>
                        </div>
                        <div class="panel-body ">
                            <div class="col-lg-6 ">
                            </div>

                        </div>
                    </div>



                </div>
            </div>

            <div class="col-lg-12">
                <div id="childData">
                </div>
                <div style="text-align: center; display: none;">
                </div>
            </div>

        </div>
    </form>

  


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
   


</asp:Content>
