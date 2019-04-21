<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.PropertyDetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<h2>Location</h2>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <link href="../../bootstrap/js/Paging/Project.css" rel="stylesheet" />

    <style type="text/css">
        /*#LoadingDiv {
            background-color: black;
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            opacity: 0.2; 
            z-index: 10;
        }*/

        /*#LoadingDiv {
            position: absolute;
            width: 592px;
            height: 512px; 
            left: 50%;
            top: 50%;
            margin-left: -296px; 
            margin-top: -256px;
        }*/
    </style>

    <div id="wrapper">

        <div id="page-wrapper">

            <div class="container-fluid">

                <form method="post" id="idAllFormTag" enctype = "multipart/form-data">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Property <small>Details Overview</small>
                            </h1>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-lg-12">
                            <div class="panel panel-green">
                                <div class="panel-heading">
                                    Property Detail                                
                                </div>
                                <div class="panel-body ">

                                    <div class="table-responsive">
                                        <div id="data">
                                        </div>
                                        <div id="divPaging">
                                        </div>
                                    </div>
                                </div>

                                <a href="#">
                                    <div class="panel-footer">
                                        <input type="hidden" id="TotalRecordInDb" name="TotalRecordInDb" />s
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
                                    Property Detail                              
                                </div>
                                <div class="panel-body ">

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Property Type        
                                        <input type="hidden" id="PropertyId" name="PropertyId" />
                                            <input type="hidden" id="PageNo" name="PageNo" />
                                            <input type="hidden" id="ImageName" name="ImageName" />
                                            <input type="hidden" id="ImageType" name="ImageType" />
                                            <%: Html.DropDownListFor(model => model.PropertyType, Model.PropertyTypeList , new {@class="form-control", placeholder="Property Type."})%>
                                            <%: Html.ValidationMessageFor(model => model.PropertyType)%>
                                        </div>

                                        <div class="col-md-4">
                                            Rent / Buy / Sale                                                                       
                                        <%: Html.TextBoxFor(model => model.RentBuySale  , new {@class="form-control", placeholder="Rent / Buy / Sale."})%>
                                            <%: Html.ValidationMessageFor(model => model.RentBuySale)%>
                                        </div>

                                        <div class="col-md-4">
                                            Room/ Bhk/ Flat/ Etc                                                                      
                                        <%: Html.TextBoxFor(model => model.BedroomSqFeetEtc  , new {@class="form-control", placeholder="Bedroom Sq Feet Etc."})%>
                                            <%: Html.ValidationMessageFor(model => model.BedroomSqFeetEtc)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Price                                                                     
                                        <%: Html.TextBoxFor(model => model.Price  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.Price)%>
                                        </div>

                                        <div class="col-md-4">
                                            City / Area / Location                                                                      
                                        <%: Html.DropDownListFor(model => model.Area, Model.CityLocationList , new {@class="form-control", placeholder="City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.Area)%>
                                        </div>

                                        <div class="col-md-4">
                                            Sub Area / City / Location                                                                       
                                        <%: Html.TextBoxFor(model => model.SubArea  , new {@class="form-control", placeholder="Sub City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.SubArea)%>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12 col-md-12">
                                        <div class="col-md-6">
                                            Landmark                                                                      
                                        <%: Html.TextBoxFor(model => model.LandMark  , new {@class="form-control", placeholder="Landmarks."})%>
                                            <%: Html.ValidationMessageFor(model => model.LandMark)%>
                                        </div>

                                        <div class="col-md-6">
                                            Property Date                                                                       
                                        <%: Html.TextBoxFor(model => model.PropertyDate  , new {@class="form-control", placeholder="Property Date."})%>
                                            <%: Html.ValidationMessageFor(model => model.PropertyDate)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Name                                                                      
                                        <%: Html.TextBoxFor(model => model.Name  , new {@class="form-control", placeholder="Name."})%>
                                            <%: Html.ValidationMessageFor(model => model.Name)%>
                                        </div>

                                        <div class="col-md-4">
                                            Contact                                                                      
                                        <%: Html.TextBoxFor(model => model.Contact  , new {@class="form-control", placeholder="Contact."})%>
                                            <%: Html.ValidationMessageFor(model => model.Contact)%>
                                        </div>

                                        <div class="col-md-4">
                                            Email                                                                      
                                        <%: Html.TextBoxFor(model => model.EmailId  , new {@class="form-control", placeholder="Email."})%>
                                            <%: Html.ValidationMessageFor(model => model.EmailId)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class=" col-md-12">
                                            Property Heading                                                                      
                                        <%: Html.TextBoxFor(model => model.PropertyHeading  , new {@class="form-control", placeholder="Property Heading."})%>
                                            <%: Html.ValidationMessageFor(model => model.PropertyHeading)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class=" col-md-12">
                                            Property Summary                                                                      
                                        <%: Html.TextAreaFor(model => model.PropertySummary  , new {@class="form-control", placeholder="Property Summary."})%>
                                            <%: Html.ValidationMessageFor(model => model.PropertySummary)%>
                                        </div>
                                    </div>





                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Property Image                                                                      
                                        <%: Html.TextBoxFor(model => model.ProperyImageUpload  , new {@class="form-control", @type="file", placeholder="Select Property Image."})%>
                                            <%: Html.ValidationMessageFor(model => model.ProperyImageUpload)%>
                                        </div>

                                        <div class="col-md-4">
                                            <img id="img" src="" class="img-thumbnail img-responsive" alt="" width="304" height="100" />
                                            <%--<div id="base"></div>--%>
                                            <input type="hidden" id="strbase64Image" name="strbase64Image" />
                                        </div>

                                        <div class="col-md-4">
                                            <button type="button" id="btnInsert" name="btnInsert" class="btn btn-success">Insert</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" class="btn btn-success">Update</button>
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

                    <div id="LoadingDiv" style="display: none;">
                        <img src="../../Images/loading.gif" alt="" />
                    </div>
                    <div class="LoadingDiv" style="display: none;">
                        <div id="popup" class="popup">
                            <div class="content">
                                <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
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
    <script src="<%= Url.Content("~/Scripts/ProjectScript/Property/PropertyDetail23.js") %>"></script>
</asp:Content>
