<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.MainBannerDetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

    <div id="wrapper">

        <div id="page-wrapper">

            <div class="container-fluid">

                <form method="post" id="idAllFormTag">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Banner  <small>Setting Detail</small>
                            </h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-8">
                            <div class="panel panel-green">
                                <div class="panel-heading">
                                    Banner Detail                               
                                </div>
                                <div class="panel-body ">

                                    <div class="table-responsive">
                                        <div id="data" >
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        Banner              
                                        <input type="hidden" id="BannerId" name="BannerId"/>                                                        
                                        <%: Html.TextBoxFor(model => model.MyFile  , new {@class="form-control", @type="file", placeholder="Banner Id."})%>
                                        <%: Html.ValidationMessageFor(model => model.MyFile)%>                                       
                                    </div>

                                    <div class="form-group">
                                        Order No.                                                                       
                                        <%: Html.TextBoxFor(model => model.OrderNo  , new {@class="form-control", placeholder="Order No."})%>
                                        <%: Html.ValidationMessageFor(model => model.OrderNo)%>
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
                                    Banner Detail                              
                                </div>
                                <div class="panel-body ">                                  
                                                                      
                                    <div class="form-group" style="overflow:scroll">
                                        <img id="img" src="" class="img-thumbnail img-responsive" alt="" width="304" height="236" />
                                        <div id="base"></div>
                                        <input type="hidden" id="strbase64Image" name="strbase64Image" />
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
    <script src="<%= Url.Content("~/Scripts/ProjectScript/MasterSetting/MainBanner2.js") %>"></script>
</asp:Content>
