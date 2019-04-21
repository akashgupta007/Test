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
        <div class="box box-default">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Minimal</label>
                            <select class="form-control select2" style="width: 100%;">
                                <option selected="selected">Alabama</option>
                                <option>Alaska</option>
                                <option>California</option>
                                <option>Delaware</option>
                                <option>Tennessee</option>
                                <option>Texas</option>
                                <option>Washington</option>
                            </select>
                        </div>
                        <!-- /.form-group -->
                        
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Minimal</label>
                            <select class="form-control select2" style="width: 100%;">
                                <option selected="selected">Alabama</option>
                                <option>Alaska</option>
                                <option>California</option>
                                <option>Delaware</option>
                                <option>Tennessee</option>
                                <option>Texas</option>
                                <option>Washington</option>
                            </select>
                        </div>
                        <!-- /.form-group -->
                        
                        <!-- /.form-group -->
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Minimal</label>
                            <select class="form-control select2" style="width: 100%;">
                                <option selected="selected">Alabama</option>
                                <option>Alaska</option>
                                <option>California</option>
                                <option>Delaware</option>
                                <option>Tennessee</option>
                                <option>Texas</option>
                                <option>Washington</option>
                            </select>
                        </div>
                        <!-- /.form-group -->
                        
                        <!-- /.form-group -->
                    </div>
                </div>
                <footer>
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <button type="button" id="btnUpdate" name="btnUpdate" class="btn btn-default btn-sm">Cancel</button>
                        </div>
                        <div class="col-md-6 text-right">
                            <button type="button" id="btnInsert" name="btnInsert" class="btn  btn-success btn-sm">Save</button>

                        </div>
                    </div>
                </footer>
                <!-- /.row -->
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <!-- Custom Tabs -->
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tab_1" data-toggle="tab">Tab 1</a></li>
                        <li><a href="#tab_2" data-toggle="tab">Tab 2</a></li>
                        <li><a href="#tab_3" data-toggle="tab">Tab 3</a></li>

                        <li class="pull-right"><a href="#" class="text-muted"><i class="fa fa-gear"></i></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                            <b>How to use:</b>

                            <p>
                                Exactly like the original bootstrap tabs except you should use
                  the custom wrapper <code>.nav-tabs-custom</code> to achieve this style.
                            </p>
                            A wonderful serenity has taken possession of my entire soul,
                like these sweet mornings of spring which I enjoy with my whole heart.
                I am alone, and feel the charm of existence in this spot,
                which was created for the bliss of souls like mine. I am so happy,
                my dear friend, so absorbed in the exquisite sense of mere tranquil existence,
                that I neglect my talents. I should be incapable of drawing a single stroke
                at the present moment; and yet I feel that I never was a greater artist than now.
             
                        </div>
                        <!-- /.tab-pane -->
                        <div class="tab-pane" id="tab_2">
                            The European languages are members of the same family. Their separate existence is a myth.
                For science, music, sport, etc, Europe uses the same vocabulary. The languages only differ
                in their grammar, their pronunciation and their most common words. Everyone realizes why a
                new common language would be desirable: one could refuse to pay expensive translators. To
                achieve this, it would be necessary to have uniform grammar, pronunciation and more common
                words. If several languages coalesce, the grammar of the resulting language is more simple
                and regular than that of the individual languages.
             
                        </div>
                        <!-- /.tab-pane -->
                        <div class="tab-pane" id="tab_3">
                            Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,
                when an unknown printer took a galley of type and scrambled it to make a type specimen book.
                It has survived not only five centuries, but also the leap into electronic typesetting,
                remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset
                sheets containing Lorem Ipsum passages, and more recently with desktop publishing software
                like Aldus PageMaker including versions of Lorem Ipsum.
             
                        </div>
                        <!-- /.tab-pane -->
                    </div>
                    <!-- /.tab-content -->
                </div>
                <!-- nav-tabs-custom -->
            </div>
            <!-- /.col -->


            <!-- /.col -->
        </div>
        <%--<div class="container">

            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#home"><i class="fa fa-dashboard"></i> Home</a></li>
                <li><a data-toggle="tab" href="#menu1"><i class="fa fa-files-o"></i> Menu 1</a></li>
                <li><a data-toggle="tab" href="#menu2"><i class="fa fa-th"></i> Menu 2</a></li>
                <li><a data-toggle="tab" href="#menu3"><i class="fa fa-pie-chart"></i>Menu 3</a></li>
            </ul>

            <div class="tab-content">
                <div id="home" class="tab-pane fade in active">
                    <br />
                    <div class="row">
                        <div class="col-md-3">
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
                        </div>
                    </div>
                </div>
                <div id="menu1" class="tab-pane fade">
                     <br />
                    <div class="row">
                        <div class="col-md-3">
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
                        </div>
                    </div>
                </div>
                <div id="menu2" class="tab-pane fade">
                     <br />
                    <div class="row">
                        <div class="col-md-3">
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
                        </div>
                    </div>
                </div>
                <div id="menu3" class="tab-pane fade">
                     <br />
                    <div class="row">
                        <div class="col-md-3">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
    <script>
        $("#btnInsert").click(function () {            
            $("div.form-group").find("input,select").css("border-color", "red");
            $("div.form-group").find("label").css("color", "red");
        });        
    </script>
</asp:Content>
