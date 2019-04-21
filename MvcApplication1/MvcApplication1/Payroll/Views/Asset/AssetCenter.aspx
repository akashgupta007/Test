<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.AssetParialViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeCenter
</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Asset/AssetCenter.js") %>"></script>

    <style type="text/css">
        .Loading {
            width: 100%;
            display: block;
            position: absolute;
            top: 0;
            left: 0;          
            height: 100%;
            z-index: 10;
            background-color: rgba(0,0,0,0.5); /*dim the background*/
        }

        .content {
            background: #fff;
            padding: 28px 26px 33px 25px;
        }
        .BgColor {
            background-color:#337ab7;
            color:white
        }
        .popup {
            border-radius: 1px;
            background: #6b6a63;
            margin: 30px auto 0;
            padding: 6px;
            position: absolute;
            width: 100px;
            top: 50%;
            left: 50%;
            margin-left: -100px;
            margin-top: -40px;
        }
    </style>

    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">

            
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">                 
                    <div class="panel-heading">Asset Master's
                       <%-- <span style="text-align: left;" class="panel-title" id="spnEmployeeName"></span><span style="display:none">Employee Id -</span><span style="text-align: right;display:none" class="panel-title" id="spnEmpId"></span>
                        <input type="hidden" id="hfEmployeeId" name="hfEmployeeId" />--%>
                    </div>
                    <div class="panel-body ">
                        <div class="form-group" id="divEmpCenter">
                            <div class="col-md-12">

                                <button type="button" id="btnFleet" name="btnFleet" class="btn btn-primary btn-xs">Fleet Management</button>
                                <button type="button" id="btnCustomer" name="btnCustomer" class="btn btn-primary btn-xs">Customer management</button>
                                <button type="button" id="btnAsset" name="btnAsset" class="btn btn-primary btn-xs">Asset management</button>
                                <button type="button" id="btnCategory" name="btnCategory" class="btn btn-primary btn-xs">Category</button>
                                <button type="button" id="btnModel" name="btnModel" class="btn btn-primary btn-xs">Model</button>
                                <button type="button" id="btnMake" name="btnMake" class="btn btn-primary btn-xs">Make</button>
                                <button type="button" id="btnContract" name="btnContract" class="btn btn-primary btn-xs">Contract</button>
                                <button type="button" id="btnTax" name="btnTax" class="btn btn-primary btn-xs">Tax</button>
                                <button type="button" id="btnTrasporter" name="btnTrasporter" class="btn btn-primary btn-xs">Trasporter</button>
                                <button type="button" id="btnTimeSheet" name="btnTimeSheet" class="btn btn-primary btn-xs">TimeSheet</button>
                                <button type="button" id="btnOverTime" name="btnOverTime" class="btn btn-primary btn-xs">OverTime Sheet</button>
                                <button type="button" id="btnCustomersite" name="btnCustomersite" class="btn btn-primary btn-xs">Customer Site</button>                                
                            </div>
                        </div>
                        <div id="pvData" style="overflow: auto; height: 600px;">
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="divOpen" style="display:none;">
        <div class="row">
            <div class="col-lg-8">
                <div class="form-group">
                    <textarea class="form-control" style="max-width: 450px;" rows="8"></textarea>
                </div>
                <div class="form-group">
                    <input type="file" class="form-control" id="asd" />
                    <input type="hidden" id="hfFileName" name="hfFileName" />
                </div>
                <div class="form-group">
                    <button type="button" id="btnAttach" name="btnAttach" class="btn btn-info">Attach</button>
                </div>
                <button type="button" id="btnAddNotes" name="btnAddNotes" class="btn btn-info">Add Notes</button>

            </div>

            <div class="col-lg-4">
                <div class="form-group">
                    <textarea class="form-control" rows="8" id="txtFileNames"></textarea>
                    <img id="img" src="" class="img-responsive img-circle" />
                    <div id="base"></div>
                </div>
                <div class="form-group">
                    <button type="button" id="btnDelete" name="btnDelete" class="btn btn-danger">Delete</button>
                    <a id="aDownload" download="">Download File</a>
                    <input type="hidden" id="base64" name="base64" />
                </div>
            </div>
        </div>
    </div>


    <div class="Loading" style="display: none;">
        <div id="popup" class="popup">
            <div class="content">
                <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
            </div>
        </div>
    </div>

   <%--  --%>

</asp:Content>
