<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.WorkdayTimeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    WorkDayTime
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/WorkDayTime.js") %>"></script>
<script src="<%= Url.Content("~/Scripts/bootstrap.timepicker.min.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 
    
<%--    <script src="../../../../Scripts/bootstrap.timepicker.min.js"></script>--%>

    <form id="WorkDayTime" novalidate="novalidate">

    
        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">
                <div class="col-lg-6">


                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                           <span  style="text-align:left;"class="panel-title" >Work Day Hours   </span>                           

                         <span style=" float:right; vertical-align:top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/WorkDayHours.html") %>">
                                <b> <img  src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a></span>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Records </button>
                                </div>

                            </div>
                            
                            <div id="data" style="overflow-x:auto; overflow-y:hidden">
                            </div>
                            
                        </div>

                        <div style="text-align:center;margin-bottom:5px; ">

                             <img id="LoadingImage" style="display:none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                        </div>
                       
                    </div>
                </div>








                <div class="col-lg-6">
                    <div class="panel panel-primary" id="EditPanel" style="display: none;">
                        <div class="panel-heading">
                            <h3 class="panel-title"><span id="panelHeader"></span> </h3>
                        </div>
                        <div  class="panel-body ">


                               <div class="form-group">
                                <div class="col-lg-3 ">
                                 Work Day Description
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <input type="hidden" id="WorkdayTimeId" name="WorkdayTimeId" />
                                    <%: Html.TextBoxFor(model => model.WorkDayDescription , new {@class="form-control", placeholder="Enter Work Day Description"})%>
                                    <%: Html.ValidationMessageFor(model => model.WorkDayDescription)%>
                                </div>
                            </div>  
               

           
                           


                              <div class="form-group">
                                <div class="col-lg-3 ">
                                   In Time
                                </div>
                                 <div class="Form-control col-lg-3 ">
                                    
                                    <%: Html.TextBoxFor(model => model.InTime , new {@class="form-control time_picker input-small", placeholder="00:00:00"})%>
                                    <%: Html.ValidationMessageFor(model => model.InTime)%>
                                  
                                </div>
                                  <div class="Form-control col-lg-3" style="text-align:left;">
                                   <select id="InAMPM" class="Form-control"  ><option value="AM">AM</option>
                                       <option value="PM">PM</option>
                                       </select>
                                      </div>
                            </div>
                           


                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    Out Time
                                </div>
                                <div class="Form-control col-lg-3 ">
                                  
                                    <%: Html.TextBoxFor(model => model.OutTime , new {@class="form-control time_picker input-small", placeholder="00:00:00"})%>
                                    <%: Html.ValidationMessageFor(model => model.OutTime)%>
                                </div>
                                 <div class="Form-control col-lg-3" style="text-align:left;">
                                   <select id="OutAMPM" class="Form-control"  ><option value="AM">AM</option>
                                       <option value="PM">PM</option>
                                       </select>
                                      </div>
                            </div>        
                              <div class="form-group">
                                <div class="col-lg-3 ">
                                  Work Day Hours(HH:MM:SS)
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <div class='bootstrap-timepicker'>
                                    <%: Html.TextBoxFor(model => model.WorkDayHours , new {@class="form-control time_picker input-small",@readonly="readonly", placeholder="00:00:00"})%>
                                    <%: Html.ValidationMessageFor(model => model.WorkDayHours)%>
                                    </div>
                                </div>
                            </div>                             
                            
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                  Half Day Work Hours
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    <%: Html.TextBoxFor(model => model.HalfDayWorkHours , new {@class="form-control time_picker input-small", placeholder="00:00:00"})%>
                                    <%: Html.ValidationMessageFor(model => model.HalfDayWorkHours)%>
                                </div>
                            </div>
                           
                              <%--<div class="form-group">
                                <div class="col-lg-3 ">
                                   Function
                                </div>
                                <div class="Form-control col-lg-9 ">                                  
                                     <%: Html.DropDownListFor(model => model.FunctionId ,Model.Function   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.FunctionId)%>                                   
                                </div>
                            </div> --%>  
                           
          
                            
                             <div class="form-group">
                                <div class="col-lg-3 ">
                                  Start Date
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                   <%: Html.TextBoxFor(model => model.StartDt  , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter Start Date" })%>
                                   <%: Html.ValidationMessageFor(model => model.StartDt ) %>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                End Date
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                 <%: Html.TextBoxFor(model => model.EndDt   , new { @class = "form-control DBPicker ", @readonly="readonly", placeholder = "Enter End Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.EndDt     ) %>
                                </div>
                            </div>                          
                                                  
                                                      
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                   Calendar Type
                                </div>
                                 <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.CalendarTypeId ,Model.CalendarType   , new {@class="form-control"})%>
                                    <%: Html.ValidationMessageFor(model => model.CalendarTypeId)%>
                                </div>
                            </div>
                           

                            <div class="form-group">
                                <div class="col-lg-3 ">
                                   Group Rule Name
                                </div>
                                <div class="Form-control col-lg-9 ">
                                    <%: Html.DropDownListFor(model => model.RuleId ,Model.Rule   , new {@class="form-control"})%>
                                      <%: Html.ValidationMessageFor(model => model.RuleId)%>
                                </div>
                            </div>                             
                                                        
                                  
                                                
                           
                           
                           
                            <div class="form-group">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

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
