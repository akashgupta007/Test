<%@ Page Title="" Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/payrollhr.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="content-header">
      
        <small>Employee</small>
      
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Employee</li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="box box-default">
            <div class="box-body">

                <div class="row">
                    <div class="col-md-4">

                        <div class="box box-widget widget-user-2">
                            <!-- Add the bg color to the header using any of the bg-* classes -->
                            <div class="bg-blue">
                                <label class="widget-user-username"></label>
                            </div>
                            <div class="box">
                                <div class="form-group">
                                    <label>Emp Code</label>
                                    <input type="text" style="width: 100%;" class="form-control" placeholder="Emp Code" />

                                    <label>First Name</label>
                                    <input type="text" style="width: 100%;" class="form-control" placeholder="First Name" />

                                    <label>Last Name</label>
                                    <input type="text" style="width: 100%;" class="form-control" placeholder="Last Name" />

                                    <label>Gender</label>
                                    <select class="form-control select2" style="width: 100%;">
                                        <option selected="selected">Male</option>
                                        <option>Female</option>
                                    </select>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="col-md-4">
                        <div class="box box-widget widget-user-2">
                            <!-- Add the bg color to the header using any of the bg-* classes -->
                            <div class=" bg-blue">
                                <label class="widget-user-username"></label>
                            </div>
                            <div class="box">
                                <div class="form-group">
                                    <label>Company</label>
                                    <select class="form-control select2" style="width: 100%;">
                                        <option selected="selected">Alabama</option>
                                        <option>Alaska</option>
                                        <option>California</option>
                                        <option>Delaware</option>
                                        <option>Tennessee</option>
                                        <option>Texas</option>
                                        <option>Washington</option>
                                    </select>

                                    <label>Location</label>
                                    <select class="form-control select2" style="width: 100%;">
                                        <option selected="selected">Alabama</option>
                                        <option>Alaska</option>
                                        <option>California</option>
                                        <option>Delaware</option>
                                        <option>Tennessee</option>
                                        <option>Texas</option>
                                        <option>Washington</option>
                                    </select>

                                    <label>Reporting Officer</label>
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

                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="box box-widget widget-user-2">
                            <!-- Add the bg color to the header using any of the bg-* classes -->
                            <div class=" bg-blue">
                                <label class="widget-user-username"></label>
                            </div>
                            <div class="box">
                                <div class="form-group">
                                    <label>Employee Category</label>
                                    <select class="form-control select2" style="width: 100%;">
                                        <option selected="selected">Alabama</option>
                                        <option>Alaska</option>
                                        <option>California</option>
                                        <option>Delaware</option>
                                        <option>Tennessee</option>
                                        <option>Texas</option>
                                        <option>Washington</option>
                                    </select>

                                    <label>Employee Type</label>
                                    <select class="form-control" style="width: 100%;">
                                        <option selected="selected">Alabama</option>
                                        <option>Alaska</option>
                                        <option>California</option>
                                        <option>Delaware</option>
                                        <option>Tennessee</option>
                                        <option>Texas</option>
                                        <option>Washington</option>
                                    </select>

                                    <label>Category</label>
                                    <select class="form-control" style="width: 100%;">
                                        <option selected="selected">Alabama</option>
                                        <option>Alaska</option>
                                        <option>California</option>
                                        <option>Delaware</option>
                                        <option>Tennessee</option>
                                        <option>Texas</option>
                                        <option>Washington</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <!-- /.form-group -->

                        <!-- /.form-group -->
                    </div>
                </div>
                <footer>
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <button type="button" id="btnCanel" name="btnCanel" class="btn btn-default btn-sm">Cancel</button>
                        </div>

                        <div class="col-md-6 text-right">
                            <div id="pBar" class="hide">
                                <img src="../../Images/Spin-0.9s-17px.gif" />
                            </div>

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
                        <li class="active" title="PIS"><a href="#tab_1" data-toggle="tab"><i class="fa fa-clone"></i></a></li>
                        <li title="General Info"><a href="#tab_2" data-toggle="tab"><i class="fa fa-creative-commons"></i> </a></li>
                        <li title="Communication Info"><a href="#tab_3" data-toggle="tab"><i class="fa fa-phone"></i> </a></li>
                        <li title="Family Info"><a href="#tab_4" data-toggle="tab"><i class="fa fa-info-circle"></i> </a></li>
                        <li title="Education"><a href="#tab_5" data-toggle="tab"><i class="fa fa-laptop"></i> </a></li>
                        <li title=" Employer Info"><a href="#tab_6" data-toggle="tab"><i class="fa fa-book"></i></a></li>
                        <li title="Salary"><a href="#tab_7" data-toggle="tab"><i class="fa fa-calculator"></i> </a></li>
                        <li title="Asset"><a href="#tab_8" data-toggle="tab"><i class="fa fa-money"></i> </a></li>
                        <li title="Reimbursement"><a href="#tab_9" data-toggle="tab"><i class="fa  fa-sticky-note"></i> </a></li>
                        <li title="Time"><a href="#tab_10" data-toggle="tab"><i class="fa fa-clock-o"></i> </a></li>
                        <li title="Leave"><a href="#tab_11" data-toggle="tab"><i class="fa fa-tachometer"></i> </a></li>
                        <li title="Loan"><a href="#tab_12" data-toggle="tab"><i class="fa fa-bank"></i> </a></li>
                        <li class="pull-right"><a href="#" class="text-muted"><i class="fa fa-gear"></i></a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab_1">
                           Personal Informtion
                        </div>
                        <!-- /.tab-pane -->
                        <div class="tab-pane" id="tab_2">
                            General
             
                        </div>
                        <!-- /.tab-pane -->
                        <div class="tab-pane" id="tab_3">
                            Communication
                        </div>
                        <div class="tab-pane" id="tab_4">
                            Family
             
                        </div>
                        <div class="tab-pane" id="tab_5">
                            Education
             
                        </div>
                        <div class="tab-pane" id="tab_6">
                            Employer
             
                        </div>
                        <div class="tab-pane" id="tab_7">
                           Salary
                        </div>
                        <div class="tab-pane" id="tab_8">
                            Asset
             
                        </div>
                        <div class="tab-pane" id="tab_9">
                           Reimbursement
             
                        </div>
                        <div class="tab-pane" id="tab_10">
                            Time & Attendance
                        </div>
                        <div class="tab-pane" id="tab_11">
                            Leave
             
                        </div>
                        <div class="tab-pane" id="tab_12">
                            Loan             
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
        
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
    <script>
        $("#btnInsert").click(function () {
            $("#btnInsert").hide();
            $("div.form-group").find("input,select").addClass("set-b-color");
            $("div.form-group").find("label").addClass("set-color");
            $("#pBar").removeClass("hide").addClass("show");
        });

        $("#btnCanel").click(function () {
            $("div.form-group").find("input,select").removeClass("set-b-color");
            $("div.form-group").find("label").removeClass("set-color");
            $("#btnInsert").show();
            $("#pBar").removeClass("show").addClass("hide");
        });

    </script>
</asp:Content>
