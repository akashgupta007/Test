<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.TextFlashModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

    <div id="wrapper">

        <div id="page-wrapper">

            <div class="container-fluid">

                <form method="post" id="idAllFormTag">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Dashboard <small>Text News Flash Overview</small>
                            </h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-6">
                            <div class="panel panel-green">
                                <div class="panel-heading">
                                    Text Flash News                                
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

                        <div class="col-lg-6">
                            <div class="panel panel-yellow">
                                <div class="panel-heading">
                                    Text Flash News                               
                                </div>
                                <div class="panel-body ">

                                    <div class="form-group">
                                        News
                                        <input type="hidden" id="TextFlashId" name="TextFlashId" />
                                        <%: Html.TextAreaFor(model => model.TextMessage  , new {@class="form-control", placeholder="Enter News Msg Etc."})%>
                                        <%: Html.ValidationMessageFor(model => model.TextMessage)%>
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
    <script src="<%= Url.Content("~/Scripts/ProjectScript/MasterSetting/TextFlashNews.js") %>"></script>
</asp:Content>
