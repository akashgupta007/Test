<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpMonthlyAttendanceEntryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Monthly Attendance Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/MonthlyAttendanceEntry.js") %>"></script>
    
    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });


        var onSuccess = function () {

            var flag = 0;
            var colTitle = $("#data_Table").length;


            if (colTitle == "0" || colTitle == undefined) {

            }
            else {
                flag = 1;

            }

            if (flag == 1) {
                $("#data_Table").dataTable();

            }

        }


    </script>
    <%   
        AjaxOptions ajaxOptions = new AjaxOptions
        {
            UpdateTargetId = "data",
            InsertionMode = InsertionMode.Replace,
            HttpMethod = "POST",
            LoadingElementId = "loading",
            OnSuccess = "onSuccess" 
        };   
    %>
    <% Ajax.BeginForm(ajaxOptions); %>
   


    <style type="text/css">
        .Loading {
            width: 100%;
            display: block;
            position: absolute;
            top: 0;
            left: 0;          
            height: 100%;
            z-index: 999;
            background-color: rgba(0,0,0,0.5); /*dim the background*/
        }

        .content {
            background: #fff;
            padding: 28px 26px 33px 25px;
        }

        .popup {
            border-radius: 1px;
            background: #6b6a63;
            margin: 30px auto 0;
            padding: 6px;
            position: absolute;
            width: 100px;
            top: 50%;
            left: 50%;
            margin-left: -100px;
            margin-top: -40px;
        }
    </style>

    

    <div class="form-horizontal" style="margin-top: 10px;">
        <div class="row">
            <%--<button type="button" id="btnAttendanceUpload" name="btnAttendanceUpload" class="btn btn-info">Attendance Upload</button>--%>
            <div class="col-lg-12">
                <div id="DetailPanel" class="panel panel-primary">

                    <div class="panel-heading">
                        <span style="text-align: left;" class="panel-title">Employee Monthly Attendance Entry</span>
               
                    </div>
                    <div class="panel-body" style="align-items: center;">
                        <%:Html.Partial("CommonDropDownList",new PoiseERP.Areas.Payroll.Models.PayrollUtil()) %>
                    </div>
                    <div style="text-align: center">
                        <img id="loading" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                    </div>
                    <div id="data" style="overflow: auto; margin-left: 5px; margin-right: 5px; text-align: center;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%Html.EndForm(); %>


    

  

    <%--<div class="navbar navbar-inverse navbar-fixed-bottom">--%>
        <div id="MsgDiv" style="display: none;">
            <label id="lblError"></label>
        </div>
    <%--</div>--%>

</asp:Content>
