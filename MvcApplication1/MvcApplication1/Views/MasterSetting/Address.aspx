<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.AddressDetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<h2>Location</h2>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

    <div id="wrapper">

        <div id="page-wrapper">

            <div class="container-fluid">

                <form method="post" id="idAllFormTag">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Address <small>Detail Overview</small>
                            </h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-8">
                            <div class="panel panel-green">
                                <div class="panel-heading">
                                    Address Detail                                
                                </div>
                                <div class="panel-body ">

                                    <div class="table-responsive">
                                        <div id="data">
                                        </div>
                                    </div>
                                    <div>
                                        <ul class="pagination pagination-lg pager" id="myPager"></ul>
                                    </div>
                                </div>

                                <a href="#">
                                    <div class="panel-footer">
                                        <span class="pull-left" id="btnRefresh">Refresh</span>
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </a>
                            </div>
                        </div>

                        <div class="col-lg-4">
                            <div class="panel panel-yellow">
                                <div class="panel-heading">
                                    Address Detail                              
                                </div>
                                <div class="panel-body ">
                                    <div class="form-group">
                                        Main Heading        
                                        <input type="hidden" id="AddressId" name="AddressId" />
                                        <%: Html.TextAreaFor(model => model.MainDetail  , new {@class="form-control", placeholder="Main Heading."})%>
                                        <%: Html.ValidationMessageFor(model => model.MainDetail)%>
                                    </div>

                                    <div class="form-group">
                                        Sub Detail                                                                       
                                        <%: Html.TextAreaFor(model => model.SubDetail  , new {@class="form-control", placeholder="Sub Detail."})%>
                                        <%: Html.ValidationMessageFor(model => model.SubDetail)%>
                                    </div>
                                    <%--<div class="small-3 columns">
                                        @Html.DisplayNameFor(m => m.LoginName)<br />
                                        @Html.TextBoxFor(m => m.LoginName, new { id = "register-loginname" ,required=""})    
            <small class="error">Username is required</small>
                                    </div>--%>
                                    <div class="form-group">
                                        Contact No                                                                      
                                        <%: Html.TextBoxFor(model => model.Contact  , new {@class="form-control", placeholder="Contact."})%>
                                        <%: Html.ValidationMessageFor(model => model.Contact)%>
                                    </div>

                                    <div class="form-group">
                                        Email Id                                                                      
                                        <%: Html.TextBoxFor(model => model.EmailId  , new {@class="form-control", placeholder="Email Id."})%>
                                        <%: Html.ValidationMessageFor(model => model.EmailId)%>
                                    </div>

                                    <div class="form-group">
                                        Order No
                                        <input type="hidden" id="PriceId" name="PriceId" />
                                        <%: Html.TextBoxFor(model => model.OrderNo  , new {@class="form-control", placeholder="Order No."})%>
                                        <%: Html.ValidationMessageFor(model => model.OrderNo)%>
                                    </div>

                                    <div class="form-group">
<img src="../../Images/Spin-0.9s-17px.gif" style="display:none" id="pBar"/>
                                        <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success">  Insert</button>
                                        <button type="button" id="btnUpdate" name="btnUpdate" class="btn btn-success">Update</button>
                                    </div>

                                </div>
                                <a href="#">
                                    <div class="panel-footer">
                                        <span class="pull-left" id="btnClear">Clear</span>
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <!-- /.row -->
                </form>
            </div>
            <!-- /.container-fluid -->

        </div>
        <!-- /#page-wrapper -->
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
    <script src="<%= Url.Content("~/Scripts/ProjectScript/MasterSetting/Address.js") %>"></script>
</asp:Content>
