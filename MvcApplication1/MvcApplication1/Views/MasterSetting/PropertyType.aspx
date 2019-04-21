<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.PropertyTypeDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>PropertyType</h2>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

    <div id="wrapper">

        <div id="page-wrapper">

            <div class="container-fluid">

                <form method="post" id="idAllFormTag">

                    <!-- Page Heading -->

                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Dashboard <small>Statistics Overview</small>
                            </h1>
                            <%--<ol class="breadcrumb">
                            <li class="active">
                                <i class="fa fa-dashboard"></i>Dashboard
                            </li>
                        </ol>--%>
                        </div>
                    </div>
                    <!-- /.row -->
                    <!-- /.row -->

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="panel panel-green">
                                <div class="panel-heading">
                                    Property Type                                
                                </div>
                                <div class="panel-body ">
                                    <input type="text" id="txtSearch" class="form-control" />
                                    <div id="data" class="table-responsive">
                                        <%--<div id="data" >
                                        </div>--%>
                                    </div>
                                     <%--<div >
                                        <ul class="pagination pagination-lg pager" id="myPager"></ul>
                                     </div>--%>
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







                        <div class="col-lg-6">
                            <div class="panel panel-yellow">
                                <div class="panel-heading">
                                    Property Type Detail                               
                                </div>
                                <div class="panel-body ">

                                    <div class="form-group">
                                        Type
                                    
                                    
                                        <input type="hidden" id="PropertyTypeId" name="PropertyTypeId" />
                                        <%: Html.TextBoxFor(model => model.PropertyTypeName  , new {@class="form-control", placeholder="Property Name"})%>
                                        <%: Html.ValidationMessageFor(model => model.PropertyTypeName)%>
                                    </div>

                                    <%--<div class="form-group">
                                    <div class="col-lg-3 col-sm-6"">
                                       
                                    </div>
                                    <div class="col-lg-9 col-sm-6 ">
                                        
                                    </div>
                                </div>--%>

                                    <div class="form-group">
                                        Order No.
                                    
                                    
                                        <%: Html.TextBoxFor(model => model.OrderNo  , new {@class="form-control", placeholder="Order No."})%>
                                        <%: Html.ValidationMessageFor(model => model.OrderNo)%>
                                    </div>

                                    <div class="form-group">

                                        <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success">Insert</button>
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









                        <%--<div class="col-sm-6 col-md-6">
                        <div class="panel panel-yellow">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-3">
                                        <i class="fa fa-shopping-cart fa-5x"></i>
                                    </div>
                                    <div class="col-xs-9 text-right">
                                        <div class="huge">124</div>
                                        <div>New Orders!</div>
                                    </div>
                                </div>
                            </div>
                            <a href="#">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>--%>


                        <%--<div class="col-lg-3 col-md-6">
                        <div class="panel panel-red">
                            <div class="panel-heading">
                                
                            </div>
                            
                        </div>
                    </div>--%>
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

    <script src="<%= Url.Content("~/Scripts/ProjectScript/MasterSetting/PropertyType.js") %>"></script>

</asp:Content>
