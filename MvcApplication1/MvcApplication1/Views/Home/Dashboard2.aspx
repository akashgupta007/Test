<%@ Page Title="" Language="C#" MasterPageFile="../../Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="content-header">
        <h1>Dashboard
        <small>Version 2.0</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Dashboard</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="row">
            <div class="col-md-3">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user-2">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class=" bg-blue">
                        <label class="widget-user-username">Employee</label>
                    </div>
                    <div class="box-footer no-padding">
                        <ul class="nav nav-stacked">
                           <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Department <span class="pull-right badge bg-blue">31</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Designation <span class="pull-right badge bg-aqua">5</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Employee Type<span class="pull-right badge bg-green">12</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Project <span class="pull-right badge bg-red">842</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Shift <span class="pull-right badge bg-blue">31</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Employee Category <span class="pull-right badge bg-aqua">5</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Location<span class="pull-right badge bg-green">12</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Wages <span class="pull-right badge bg-red">842</span></a></li>
                        </ul>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
            <div class="col-md-3">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user-2">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class=" bg-blue">
                        <label class="widget-user-username">Leave</label>
                    </div>
                    <div class="box-footer no-padding">
                        <ul class="nav nav-stacked">
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Leave Type <span class="pull-right badge bg-blue">31</span></a></li>
                        </ul>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
            <div class="col-md-3">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user-2">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class=" bg-blue">
                        <label class="widget-user-username">Policy</label>
                    </div>
                    <div class="box-footer no-padding">
                        <ul class="nav nav-stacked">
                    <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">TDS policy <span class="pull-right badge bg-blue">31</span></a></li>

                        </ul>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
            <div class="col-md-3">
                <!-- Widget: user widget style 1 -->
                <div class="box box-widget widget-user-2">
                    <!-- Add the bg color to the header using any of the bg-* classes -->
                    <div class=" bg-blue">
                        <label class="widget-user-username">Bank</label>
                    </div>
                    <div class="box-footer no-padding">
                        <ul class="nav nav-stacked">
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">File Format <span class="pull-right badge bg-blue">31</span></a></li>
                            <li><a href="<%= Url.Content("~/MasterSetting/Address") %>">Company bank master <span class="pull-right badge bg-aqua">5</span></a></li>
                            </ul>
                    </div>
                </div>
                <!-- /.widget-user -->
            </div>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
