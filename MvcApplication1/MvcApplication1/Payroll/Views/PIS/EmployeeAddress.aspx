<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpAddressViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeAddress
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/PIS/EmployeeAddress1.js") %>"></script>
      

    <form id="PisEmpAddress" method="post" novalidate="novalidate">
        
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-3">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">

                            <span style="text-align: left;" class="panel-title">Employee Address  </span>


                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/PIS/EmployeeAdderss.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>


                        </div>
                        <div class="panel-body ">


                            <div class="form-group" style="margin-left: 10px;">
                                <div class="col-md-3">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Employee Address</button>
                                </div>                                                 

                              

                            </div>


                             <div id="data" style="overflow: auto;">
                         
                        </div>



                        <div id="Download" style="display:none;">
                         <div class="form-group" style="margin-left: 10px;  margin-bottom: 10px; margin-top:10px;">
                 
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel">Excel</button>
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word">Word</button>

                        
                             
                        </div></div>
                          

                    </div>

                       


                       

                    <div style="text-align: center; margin-bottom: 5px;">

                        <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>

                </div>
             </div>


            <div class="col-lg-9">
                <div class="panel panel-primary" id="EditPanel" style="display: none;">
                    <div class="panel-heading">
                        <h3 class="panel-title"><span id="panelHeader"></span></h3>
                    </div>
                    <div class="panel-body ">


                        <div class="form-group">
                            <div class="col-lg-3 ">
                                Employee Name
                            </div>
                            <div class="Form-control col-lg-9 ">
                                <input type="hidden" id="EmpAddressId" name="EmpAddressId" />
                                <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList   , new {@class="form-control"})%>
                                <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-lg-3 ">
                                Address Proff
                            </div>
                            <div class="Form-control col-lg-5">

                               <%-- <%: Html.TextBoxFor(model => model.Address1 , new {@class="form-control", placeholder="Enter  Address1"})%>
                                <%: Html.ValidationMessageFor(model => model.Address1)%>--%>
                                <input type="file" id="PathAddress" class="form-control" name="file" onchange="onFileSelected(event,'Address')">
                                <input type="hidden" class="form-control" id="imagePathAddressproff" name="imagePathAddressproff">
                                <label id="addresslbl"></label>
                            </div>
                            <div class="col-md-4" style="display:none">                                                        
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoimageAdd">
                            <img id="limageAdd" class="img-thumbnail" onload="LoadImage(this,'Address');" />
                            </div>
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoidAdd">
                            </div>                                                
                            </div>
                        </div>
                        <div class="form-group" ">
                            <div class="col-lg-3 ">
                                Id Proff
                            </div>                          
                            <div class="Form-control col-lg-5">

                               <%-- <%: Html.TextBoxFor(model => model.Address1 , new {@class="form-control", placeholder="Enter  Address1"})%>
                                <%: Html.ValidationMessageFor(model => model.Address1)%>--%>
                                <input type="file" multiple id="PathId" onchange="onFileSelected(event,'IDPROOF');"  class="form-control"/>
                                <%--<input type="file" id="PathId" class="form-control" name="file[]" onchange="onFileSelected(event,'IDPROOF')"  multiple>--%>
                                <input type="hidden" class="form-control" id="imagePathIdproff" name="imagePathIdproff">
                                
                                <div id="Idlbl"></div>
                            </div>
                            <div class="col-md-4"  style="display:none">                                                        
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoimageIdproff">
                            <img id="limageIdproff" class="img-thumbnail" onload="LoadImage(this,'IDPROOF');" />
                            </div>
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoidIdproff">
                            </div>                                                
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 ">
                                Education
                            </div>
                             <div class="Form-control col-lg-5">
                                   <input type="file" id="PathEducationProff" class="form-control" name="file" onchange="onFileSelected(event,'Education')">
                                <input type="hidden" class="form-control" id="imagePathEducationsproff" name="imagePathEducationsproff">
                                  <label id="Educationlbl"></label>
                            </div>
                            <div class="col-md-4"  style="display:none">                                                        
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoimagEducationproff">
                            <img id="limageEducationproff" class="img-thumbnail" onload="LoadImage(this,'Education');" />
                            </div>
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoidEducationproff">
                            </div>                                                
                            </div>
                           
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 ">
                                Family Details
                            </div>  
                            <div class="Form-control col-lg-5">
                                 <input type="file" id="PathEfamilyProff" class="form-control" name="file" onchange="onFileSelected(event,'family')">
                                <input type="hidden" class="form-control" id="imagePathfamilyproff" name="imagePathfamilyproff">
                                 <label id="familylbl"></label>
                            </div>
                            <div class="col-md-4"  style="display:none">                                                        
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoimagfamilyproff">
                            <img id="limagefamilyproff" class="img-thumbnail" onload="LoadImage(this,'family');" />
                            </div>
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoidfamilyproff">
                            </div>                                                
                            </div>                       
                           
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 ">
                                Nominee Details
                            </div>       
                            <div class="Form-control col-lg-5">
                                 <input type="file" id="PathNomineeProff" class="form-control" name="file" onchange="onFileSelected(event,'Nominee')">
                                <input type="hidden" class="form-control" id="imagePathnomineesproff" name="imagePathnomineesproff">
                                 <label id="nomineelbl"></label>
                            </div>
                            <div class="col-md-4"  style="display:none">                                                        
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoimagnomineeproff">
                            <img id="limagenomineeproff" class="img-thumbnail" onload="LoadImage(this,'Nominee');" />
                            </div>
                            <div style="width: 150px; height: 50px;" class="col-lg-offset-4" id="logoidnomineeproff">
                            </div>                                                
                            </div>                    
                            
                        </div>



                        <div class="form-group">

                            <div class="col-lg-offset-4 col-lg-3">
                                <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                            </div>

                        </div>
                        <div style="text-align: center">
                            <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>


                    </div>
                </div>
            </div>

        </div>

        </div>
     
    </form>

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>

</asp:Content>
