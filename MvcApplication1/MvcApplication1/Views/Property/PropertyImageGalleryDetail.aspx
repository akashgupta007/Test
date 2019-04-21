<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.PropertyImageGalleryModel>" %>

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
                            <h1 class="page-header">Property Gallery <small>Details Overview</small>
                            </h1>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-lg-12">
                            <div class="panel panel-green">
                                <div class="panel-heading">
                                    Property Gallery                                
                                </div>
                                <div class="panel-body ">

                                    <div class="table-responsive">
                                        <div id="data">
                                        </div>
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

                        <div class="col-lg-12">
                            <div class="panel panel-yellow">
                                <div class="panel-heading">
                                    Property Gallery                              
                                </div>
                                <div class="panel-body ">

                                    <div class="form-group">
                                        <div class="col-md-6 col-lg-3">
                                            Property Type        
                                        <input type="hidden" id="GalleryId" name="GalleryId" />
                                            <%: Html.DropDownListFor(model => model.PropertyType, Model.PropertyTypeList , new {@class="form-control", placeholder="Property Type."})%>
                                            <%: Html.ValidationMessageFor(model => model.PropertyType)%>
                                        </div>

                                        <div class="col-md-6 col-lg-3">
                                            Property ID                                                                       
                                        <%: Html.DropDownListFor(model => model.PropertyId, Model.PropertyIdList, new {@class="form-control", placeholder="Property Id."})%>
                                            <%: Html.ValidationMessageFor(model => model.PropertyIdList)%>
                                        </div>

                                        <div class="col-md-6 col-lg-1">
                                            Order                                                                     
                                        <%: Html.TextBoxFor(model => model.OrderNo  , new {@class="form-control", placeholder="Order No."})%>
                                            <%: Html.ValidationMessageFor(model => model.OrderNo)%>
                                        </div>

                                        <div class="col-md-6 col-lg-3">
                                            Price                                                                     
                                        <%: Html.TextBoxFor(model => model.ProperyGalleryUpload  , new {@class="form-control", @type="file", placeholder="Select File / Image."})%>
                                            <%: Html.ValidationMessageFor(model => model.ProperyGalleryUpload)%>
                                        </div>

                                        
                                        <div class="col-md-6 col-lg-2" style="margin-top:18px;">
                                           <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success">Insert</button>
                                           <button type="button" id="btnUpdate" name="btnUpdate" class="btn btn-success">Update</button>                                        
                                        </div>

                                    </div>
                                   
                                    <div class="form-group">
                                        <div class="col-lg-12" style="margin-top:18px;">
                                        <img id="img" src="" class="img-thumbnail img-responsive" alt="" />
                                        <input type="hidden" id="strbase64Image" name="strbase64Image" />
                                        </div>
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
    <script src="<%= Url.Content("~/Scripts/ProjectScript/Property/PropertyImageGalleryDetail.js") %>"></script>
</asp:Content>
