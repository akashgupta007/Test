<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.CustomerManagementViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    customerListWithDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
  <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/CustomerSitelist.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form method="post" id="CustomerSitelist" novalidate="novalidate">
        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div class="col-lg-12">
                    <div id="DetailPanel" class="panel panel-primary">

                        <div class="panel-heading">
                            <%--<h3 class="panel-title">Employee Details</h3>--%>
                            <span style="text-align: left;" class="panel-title"><span id="panelHeader">customer Details</span></span>
                                                  
                        </div>

                      <div class="panel-body" style="align-items: center;">

                            <div class="col-lg-12">



                                <div class="col-lg-2">
                                    Customer   
                                     <%: Html.DropDownListFor(model => model.CustomerId ,Model.CustomerList, new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.CustomerId)%>
                                </div>

                                <div class="col-lg-2">
                                    Country   
                                <%: Html.DropDownListFor(model => model.Country, Model.CountryList  , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.Country)%>
                                </div>

                                <div class="col-lg-2">
                                   State
                                  <select id="stateId" name="stateId" class="form-control"></select>
                                    <%: Html.ValidationMessageFor(model => model.States)%>
                                </div>

                                <div class="col-lg-2 ">
                                    City
                                     <select id="cityId" name="cityId" class="form-control"></select>
                                    <%: Html.ValidationMessageFor(model => model.cityId)%>
                                </div>
                               

                                 <div class="col-lg-2">
                                    <br />
                                    <button type="button" style="vertical-align: central; text-align: left;" id="btnSearch" class="btn  btn-success"><span class='glyphicon glyphicon-search'></span> Search</button>

                                </div>
                             
                            </div>

                        
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>

                          <div class="col-lg-12">
                             <div id="data" style="overflow: auto; "></div>
                        </div>

</div>
                      
                    </div>
                        </div>
                </div>
            </div>
        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
                <button type="button" name="new" onclick='ChildRecordReset()'  style="display:none" class='btn btn-info btn-xs'><span class='glyphicon glyphicon-plus'></span>New Record</button>
                <input type="hidden" id="flag" name="flag" />


            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Employee Salary Item</h3>
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
