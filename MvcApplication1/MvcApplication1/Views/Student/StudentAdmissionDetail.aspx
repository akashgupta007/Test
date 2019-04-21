<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.StudentAdmissionDetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>StudentAdmissionDetail</h2>

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

                <form method="post" id="idAllFormTag" name="idAllFormTag">
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
                                            Student Name       
                                        <input type="hidden" id="Id" name="Id" />
                                            <%--<input type="hidden" id="PageNo" name="PageNo" />
                                            <input type="hidden" id="ImageName" name="ImageName" />
                                            <input type="hidden" id="ImageType" name="ImageType" />--%>
                                            <%: Html.TextBoxFor(model => model.StudentName, new {@class="form-control", placeholder="Student Name"})%>
                                            <%: Html.ValidationMessageFor(model => model.StudentName)%>
                                        </div>

                                        <div class="col-md-4">
                                            Father Name                                                                       
                                        <%: Html.TextBoxFor(model => model.FatherName  , new {@class="form-control", placeholder="Father Name"})%>
                                            <%: Html.ValidationMessageFor(model => model.FatherName)%>
                                        </div>

                                        <div class="col-md-4">
                                            Mother Name                                                                      
                                        <%: Html.TextBoxFor(model => model.MotherName  , new {@class="form-control", placeholder="Mother Name"})%>
                                            <%: Html.ValidationMessageFor(model => model.MotherName)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Gender                                                                     
                                        <%: Html.TextBoxFor(model => model.Gender  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.Gender)%>
                                        </div>

                                        <div class="col-md-4">
                                            Contact                                                                      
                                        <%: Html.TextBoxFor(model => model.Contact , new {@class="form-control", placeholder="City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.Contact)%>
                                        </div>

                                        <div class="col-md-4">
                                            Father Contact                                                                       
                                        <%: Html.TextBoxFor(model => model.FatherContact  , new {@class="form-control", placeholder="Sub City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.FatherContact)%>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12 col-md-12">
                                        <div class="col-md-6">
                                            Emergancy Contact                                                                      
                                        <%: Html.TextBoxFor(model => model.EmgContact  , new {@class="form-control", placeholder="Landmarks."})%>
                                            <%: Html.ValidationMessageFor(model => model.EmgContact)%>
                                        </div>

                                        <div class="col-md-6">
                                            Email Id                                                                      
                                        <%: Html.TextBoxFor(model => model.EmailId  , new {@class="form-control", placeholder="Property Date."})%>
                                            <%: Html.ValidationMessageFor(model => model.EmailId)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Date of Birth                                                                      
                                        <%: Html.TextBoxFor(model => model.DOB  , new {@class="form-control", placeholder="Name."})%>
                                            <%: Html.ValidationMessageFor(model => model.DOB)%>
                                        </div>

                                        <div class="col-md-4">
                                            Present Address                                                                      
                                        <%: Html.TextBoxFor(model => model.PAddress  , new {@class="form-control", placeholder="Contact."})%>
                                            <%: Html.ValidationMessageFor(model => model.PAddress)%>
                                        </div>

                                        <div class="col-md-4">
                                            State                                                                      
                                        <%: Html.TextBoxFor(model => model.PState  , new {@class="form-control", placeholder="Email."})%>
                                            <%: Html.ValidationMessageFor(model => model.PState)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class=" col-md-12">
                                            City                                                                     
                                        <%: Html.TextBoxFor(model => model.PCity  , new {@class="form-control", placeholder="Property Heading."})%>
                                            <%: Html.ValidationMessageFor(model => model.PCity)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class=" col-md-12">
                                            Pincode                                                                      
                                        <%: Html.TextAreaFor(model => model.PPincode  , new {@class="form-control", placeholder="Property Summary."})%>
                                            <%: Html.ValidationMessageFor(model => model.PPincode)%>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Current Address                                                                     
                                        <%: Html.TextBoxFor(model => model.CAddress  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.CAddress)%>
                                        </div>

                                        <div class="col-md-4">
                                            State                                                                      
                                        <%: Html.TextBoxFor(model => model.CState , new {@class="form-control", placeholder="City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.CState)%>
                                        </div>

                                        <div class="col-md-4">
                                            City                                                                      
                                        <%: Html.TextBoxFor(model => model.CCity  , new {@class="form-control", placeholder="Sub City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.CCity)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Pincode                                                                     
                                        <%: Html.TextBoxFor(model => model.CPincode  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.CPincode)%>
                                        </div>

                                        <div class="col-md-4">
                                            Height                                                                      
                                        <%: Html.TextBoxFor(model => model.Height , new {@class="form-control", placeholder="City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.Height)%>
                                        </div>

                                        <div class="col-md-4">
                                            Weight                                                                       
                                        <%: Html.TextBoxFor(model => model.Wight  , new {@class="form-control", placeholder="Sub City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.Wight)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Any Health Issue                                                                     
                                        <%: Html.TextBoxFor(model => model.AnyHealthIssue  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.AnyHealthIssue)%>
                                        </div>

                                        <div class="col-md-4">
                                            Health Issue Description                                                                      
                                        <%: Html.TextBoxFor(model => model.HealthIssueDescription , new {@class="form-control", placeholder="City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.HealthIssueDescription)%>
                                        </div>

                                        <div class="col-md-4">
                                            Current Qualification                                                                       
                                        <%: Html.TextBoxFor(model => model.CurrentQualification  , new {@class="form-control", placeholder="Sub City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.CurrentQualification)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Class for Admission                                                                     
                                        <%: Html.TextBoxFor(model => model.ClassForAdmission  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.ClassForAdmission)%>
                                        </div>

                                        <div class="col-md-4">
                                            Admission Fee                                                                      
                                        <%: Html.TextBoxFor(model => model.AdmissionFee , new {@class="form-control", placeholder="City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.AdmissionFee)%>
                                        </div>

                                        <div class="col-md-4">
                                            Roll No.                                                                       
                                        <%: Html.TextBoxFor(model => model.RollNo  , new {@class="form-control", placeholder="Sub City / Area / Location."})%>
                                            <%: Html.ValidationMessageFor(model => model.RollNo)%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Date of Admission                                                                     
                                        <%: Html.TextBoxFor(model => model.DateOfAdmission  , new {@class="form-control", placeholder="Price."})%>
                                            <%: Html.ValidationMessageFor(model => model.DateOfAdmission)%>
                                        </div>                                  
                                    </div>



                                    <div class="form-group col-lg-12">
                                        <div class="col-md-4">
                                            Photo                                                                      
                                        <%: Html.TextBoxFor(model => model.Photo  , new {@class="form-control", @type="file", placeholder="Select Property Image."})%>
                                            <%: Html.ValidationMessageFor(model => model.Photo)%>
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
    <script src="<%= Url.Content("~/Scripts/ProjectScript/Student/StudentAdmissionDetail2.js") %>"></script>
</asp:Content>
