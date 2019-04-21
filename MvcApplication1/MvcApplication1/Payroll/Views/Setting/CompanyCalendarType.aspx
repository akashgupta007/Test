<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.CompanyCalendarViewModel>" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CompanyCalendarType
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Setting/CompanyCalendar1.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
    </script>



    <form id="CompanyCalender" novalidate="novalidate">


        <div class="form-horizontal" style="margin-top: 20px;">

            <div class="row">
                <div class="col-lg-12" style="margin-top: 20px;">

                    <div class="panel panel-primary">

                        <div class="panel-body" style="margin-top: 20px; text-align: center;">


                            <div class="form-group" style="text-align: center;">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <div class="col-lg-3" style="text-align: center;">
                                        Year
                                    </div>
                                    <div class="col-lg-6" style="text-align: center;">

                                        <%: Html.DropDownListFor(model => model.CalendarYear, Model.YearList, new {@class="form-control parent child",@id="CalendarYear"})%>


                                        <%: Html.ValidationMessageFor(model => model.CalendarYear)%>
                                    </div>
                                    <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                </div>
                            </div>

                            <div class="form-group" style="text-align: center;">

                                <div class="col-lg-offset-4 col-lg-3">
                                    <div class="col-lg-3" style="text-align: center; white-space: nowrap;">
                                        Calendar Type
                                    </div>
                                    <div class="col-lg-6" style="text-align: center;">
                                        &nbsp;&nbsp;     <%: Html.RadioButtonFor(model => model.DayorDateType, "Day",  new {@class="DayorDateType", @checked = "checked" })%>Day &nbsp;
                                    <%: Html.RadioButtonFor(model => model.DayorDateType, "Date", new { @class="DayorDateType"})  %>Date  
                              
                                    </div>

                                </div>
                            </div>

                            <div class="col-lg-6" style="text-align: center; margin-bottom: 5px;">
                            </div>

                        </div>



                    </div>

                    <div id="DetailPanel" style="display: none;" class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="text-align: left;" class="panel-title"> Calendar Type    </span>

                            <span style="float: right; vertical-align: top"><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Settings/CompanyCalendarType.html") %>">
                                <b>
                                    <img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a>   </span>
                        </div>
                        <div class="panel-body ">

                            <div class="form-group" style="margin-top: 10px;">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record  </button>
                                </div>

                            </div>


                            <div class="panel panel-primary" id="EditPanel" style="display: none;">
                                <div class="panel-heading">
                                    <h3 class="panel-title"></h3>
                                </div>
                                <div class="panel-body ">
                                    <div class="col-lg-12">

                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Day Type                        
                               
                                      <input type="hidden" id="CalendarTypeId" name="CalendarTypeId" />
                                            <%: Html.DropDownListFor(model => model.DayTypeId ,Model.DayTypeList,  new {@class="form-control parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.DayTypeId)%>
                                        </div>



                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Calender Type Decription

                                         <%: Html.TextBoxFor(model => model.CalendarTypeDesc, new {@class="form-control parent", placeholder="Enter CalendarType Decription "})%>
                                            <%: Html.ValidationMessageFor(model => model.CalendarTypeDesc)%>
                                        </div>




                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Shift
                              
                                     <%: Html.DropDownListFor(model => model.ShiftId ,Model.ShiftList   , new {@class="form-control parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.ShiftId)%>
                                        </div>




                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Employee Type
                             
                                    <%: Html.DropDownListFor(model => model.EmpTypeId ,Model.EmpTypeList   , new {@class="form-control parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.EmpTypeId)%>
                                        </div>



                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Location
                              
                                    <%: Html.DropDownListFor(model => model.LocationId ,Model.Location   , new {@class="form-control parent"})%>
                                            <%: Html.ValidationMessageFor(model => model.LocationId)%>
                                        </div>








                                        <div class="form-group col-lg-2" style="margin-left: 1px;">
                                            Notes
                            
                                    <%: Html.TextBoxFor(model => model.Notes , new {@class="form-control parent", placeholder="Enter Value"})%>
                                            <%: Html.ValidationMessageFor(model => model.Notes)%>
                                        </div>
                                        <div class="form-group col-lg-1" id="divIsWorkDay" style="margin-left: 1px; display: none;">
                                            Is Work Day
                                <br />
                                            <%: Html.CheckBoxFor(model => model.IsWorkDay , new {@id="IsWorkDay",@class="parent"})%>
                                        </div>
                                    </div>




                                    <div class="col-lg-12">

                                        <div class="col-lg-offset-4 col-lg-3">
                                            <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                            <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                            <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                                        </div>

                                    </div>
                                </div>
                            </div>


                        </div>

                        <div id="data">
                        </div>
                    </div>








                </div>



            </div>
        </div>


















        <div id="childDiv" style="display: none;">

            <div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
                <button type="button" name="new" onclick='ChildRecordReset()' class='btn btn-info btn-xs'><span class='glyphicon glyphicon-plus'></span>New Record</button>
                <input type="hidden" id="flag" name="flag" />


            </div>

            <div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
                <div style="margin-top: 10px;">
                    <div id="Day" style="display: none;">
                        <div class="form-group col-lg-1">
                            Work Day
                        </div>
                        <div class="form-group col-lg-2">
                            <input type="hidden" id="CalendarDaysId" name="CalendarDaysId" />
                            <%: Html.DropDownListFor(model => model.WorkDay, new List<SelectListItem> { 
                                                                        new SelectListItem{Text="--Select--", Value=""},
                                                                        new SelectListItem{Text="Sunday", Value="Sunday"},
                                                                        new SelectListItem{Text="Monday", Value="Monday"},
                                                                        new SelectListItem{Text="Tuesday", Value="Tuesday"},
                                                                        new SelectListItem{Text="Wednesday", Value="Wednesday"},
                                                                        new SelectListItem{Text="Thursday", Value="Thursday"},
                                                                        new SelectListItem{Text="Friday", Value="Friday"},
                                                                        new SelectListItem{Text="Saturday", Value="Saturday"}} , new {@class="form-control",@id="WorkDay"})%>


                            <%: Html.ValidationMessageFor(model => model.WorkDay)%>
                        </div>
                    </div>
                    <div id="Date" style="display: none;">
                        <div class="form-group col-lg-1" style="margin-left: 1px; margin-right: 1px;">
                            Work Date
                        </div>
                        <div class="form-group col-lg-2">


                            <%: Html.TextBoxFor(model => model.WorkDate , new {@class="form-control",@id="WorkDate" ,@maxlength=10 , placeholder="Enter  Work Date"})%>
                            <%: Html.ValidationMessageFor(model => model.WorkDate)%>
                        </div>


                    </div>
                    <div class="form-group col-lg-1" style="margin-left: 1px; margin-right: 1px;">
                        Notes
                    </div>
                    <div class="form-group col-lg-2">
                        <%: Html.TextBoxFor(model => model.WorkDayNotes , new {@class="form-control child", placeholder="Enter Notes"})%>
                        <%: Html.ValidationMessageFor(model => model.WorkDayNotes)%>
                    </div>
                    <div class="form-group col-lg-2" style="margin-left: 1px; margin-right: 1px;">
                        <button style="margin-bottom: 2px; margin-right: 1px;" type="button" name="btnChildInsert" onclick='InsertChildRecord()' class="btn  btn-success childInsert"><span class="glyphicon glyphicon-picture"></span> Save</button>
                        <button type="button" name="btnChildUpdate" onclick="UpdateChildRecord()" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success childUpdate"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                        <button type="button" name="btnChildClose" onclick="childPanalClose()" style="margin-bottom: 2px; margin-right: 1px;" class="btn btn-success"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>

                        <img id="ChildLoading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />



                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div id="childData"></div>

            </div>

        </div>





    </form>





    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>


        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>


        </div>
    <%--</div>--%>







</asp:Content>
