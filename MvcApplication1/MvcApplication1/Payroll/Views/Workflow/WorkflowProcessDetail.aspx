<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.WorkflowViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	WorkflowProcess
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Workflow/WorkflowProcess7.js") %>"></script>

	<script type="text/javascript">
		$(document).ready(function (e) {
			if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
				urlBackRestrict();
			}
		});
	</script>

	<style type="text/css">



	.panel-heading .accordion-toggle:after {
	/* symbol for "opening" panels */
	font-family: 'Glyphicons Halflings';  /* essential for enabling glyphicon */
	content: "\e114";    /* adjust as needed, taken from bootstrap.css */
	float: right;        /* adjust as needed */
	color: grey;         /* adjust as needed */
}
.panel-heading .accordion-toggle.collapsed:after {
	/* symbol for "collapsed" panels */
	content: "\e080";    /* adjust as needed, taken from bootstrap.css */
}
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

		.popup1 {
			border-radius: 2px;
			background: content-box;
			margin: 30px auto 0;
			padding: 6px;
			position: absolute;
			width: 100px;
			top: 39%;
			left: 50%;
			margin-left: -100px;
			z-index: 9999;
			margin-top: -40px;
		}


        .panel-default > .panel-heading {
         margin-bottom: -10px;
         color: white;
          background-color: rgba(21, 98, 118, 1);
}

	</style>




	<form method="post" id="wfProcess">
		<div class="form-horizontal" style="margin-top: 10px;">
            <div class="container-fluid">
	        <div class="row-fluid">
				<div  class="col-md-2" >
				  <div class='panel panel-default'>
                <div class='panel-heading'>
               Process
                                   <%: Html.DropDownListFor(model => model.ProcessId, Model.ProcessList , new { @class="form-control"})%>
									<%: Html.ValidationMessageFor(model => model.ProcessId)%>
      
                  </div>
                      <div id="divMenu" class='panel-body' >


                      </div>
                      </div>
				</div>

				<div class="col-lg-10" id="divWorkfolowDetail" style="display: none;">
					<div class="panel panel-default">
						<div class="panel-heading">
							<div>Workflow Process</div>
						</div>
						<div class="panel-body ">
							<div class="col-lg-2">
						<%--	<%: Html.HiddenFor(model => model.ProcessId)%>--%>
								<%: Html.HiddenFor(model => model.StateId)%>
								<%: Html.HiddenFor(model=>model.imageBase64String) %>
								<%: Html.HiddenFor(model=>model.imageName) %>
								<%: Html.HiddenFor(model=>model.AppKey) %>

								<%-- <%: Html.HiddenFor(model=>model.AttachmentId) %>--%>

								<input type="hidden" id="hfNotesText" name="hfNotesText" />
								<input type="hidden" id="hfNotesList" name="hfNotesList" />

								<%--<%: Html.DropDownListFor(model => model.NextState ,Model.NextStateList, new {@class="form-control"})%>
									<%: Html.ValidationMessageFor(model => model.NextState)%>--%>

								<select class="form-control" name="NextState" id="NextState"></select>
							</div>
							<div class="col-lg-2">
								<button type="button" id="btnworkflow" name="btnworkflow" class="btn btn-info" data-toggle="button">Action Process</button>

							</div>

                              <div id="VariablePayDiv" style="display:none;"  >
                                  <div class="form-group col-lg-6" >
                                <div class="col-lg-2" style=" white-space: nowrap;">
                                  Month
                                    </div>
                             <div  class="col-lg-3" >
                                  
									<%: Html.DropDownListFor(model => model.VarMonthId, Model.VarMonthList , new { @class="form-control "})%>
									<%: Html.ValidationMessageFor(model => model.VarMonthId)%>
                                </div>
                                      
                                <div class="col-lg-2" style=" white-space: nowrap;">
                                    Year
                                    </div>
                             <div  class="col-lg-3">
                                  	<%: Html.DropDownListFor(model => model.VarYearId, Model.VarYearList , new { @class="form-control "})%>
									<%: Html.ValidationMessageFor(model => model.VarYearId)%>
                                </div>
                           

                                 <div class="col-lg-2">
								<button type="button" id="btnVarPaySearch" name="btnVarPaySearch" class="btn btn-info" data-toggle="button">Search</button>

							  </div>

                            </div>


                            </div>
                              <div  id="ReimbursementDateDiv" style="display:none;">
                                  <div class="form-group col-lg-6" >
                                <div class="col-lg-2" style=" white-space: nowrap;">
                                    From Date 
                                    </div>
                             <div  class="col-lg-3" >
                                    <%: Html.TextBoxFor(model => model.StartDate  , new { @class = "form-control DBPicker",@readonly="readonly", placeholder = "Enter Start Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.StartDate ) %>
                                </div>
                                      
                                <div class="col-lg-2" style=" white-space: nowrap;">
                                    To Date 
                                    </div>
                             <div  class="col-lg-3">
                                    <%: Html.TextBoxFor(model => model.EndDate  , new { @class = "form-control DBPicker",@readonly="readonly", placeholder = "Enter End Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.EndDate ) %>
                                </div>
                             

                                 <div class="col-lg-2">
								<button type="button" id="btnReimbursement" name="btnReimbursement" class="btn btn-info" data-toggle="button">Search</button>

							  </div>

                            </div>


                                 

</div>
                              <div class="form-group col-lg-6"  id="divPayDate" style="display:none;">
                                <div class="col-lg-2" style=" white-space: nowrap;">
                                    Pay Date 
                                    </div>
                             <div class="col-lg-4" >
                                    <%: Html.TextBoxFor(model => model.PayDate  , new { @class = "form-control DBPicker",@readonly="readonly", placeholder = "Enter Pay Date" })%>
                                    <%: Html.ValidationMessageFor(model => model.PayDate ) %>
                                </div>

                                    <div class="col-lg-2" style=" white-space: nowrap;">
                                   Bank Account
                                    </div>
                             <div class="col-lg-4" >
                                    <%: Html.DropDownListFor(model => model.BankAccountId, Model.BankAccountList , new { @class="form-control"})%>
									<%: Html.ValidationMessageFor(model => model.BankAccountId)%>
                                </div>


                            </div>
							  <div id="bankDiv" style="display: none;">

                                 
							
                                
									<div class="col-lg-1">Bank </div>
                                    <div class="col-lg-2">
                                        <input type="hidden" id="filedate" name="filedate" />
                                   <%-- <%: Html.HiddenFor(model => model.filecreateddate)%>--%>
									<%: Html.DropDownListFor(model => model.BankId, Model.BankList , new { @class="form-control"})%>
									<%: Html.ValidationMessageFor(model => model.BankId)%>
                                        </div>
								<div class="col-lg-4">
									<button style="margin-left: 1px; margin-top: 1px; margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnBankStatement" onclick="return CheckBankValidation()" class="btn  btn-success enabling cancel" name="command" value="BankStatement">Bank Statement</button>
                                    <button style="margin-left: 1px; margin-top: 1px; margin-bottom: 2px; margin-right: 1px;" type="submit" id="btnBankinfo" onclick="return CheckBankValidationBankinfo()" class="btn  btn-success enabling cancel" name="command" value="Bankinfo">Bank Statement Info</button>
                          
                                    <button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="submit" id="btnMsWord" onclick="return CheckBankValidation()" class="btn  btn-success enabling cancel" name="command" value="MSWord">Bank Statement in Ms Word</button>
									<button style="margin-bottom: 2px; margin-right: 1px; display: none;" type="button" id="btnExcel" onclick="CheckPasswordBankValidation()" class="btn  btn-success enabling cancel" name="command" value="MSExcel">Bank Statement in Excel</button>



								</div>
                                      </div>
							<br />
							  <br />
							   <br />
							  <br />
							 <div  id="SearchDiv">

							<div class="col-lg-2">
								Search
							   <input type="text" id="txtSearch" name="txtSearch" class="form-control"/>

							</div>
								   <div class="col-lg-2">
									   <br />
							  Advance  Search
							   <input type="checkbox"  id="ChkSearch" name="ChkSearch"/>

							</div>
								   <div id="adSearch" style="display:none;">
						            <div class="col-lg-2">
								 Month
									<%: Html.DropDownListFor(model => model.MonthId, Model.MonthList , new { @class="form-control SearchTable"})%>
									<%: Html.ValidationMessageFor(model => model.MonthId)%>
								</div>

						
							         <div class="col-lg-2">
								   Year
									<%: Html.DropDownListFor(model => model.YearId, Model.YearList , new { @class="form-control SearchTable"})%>
									<%: Html.ValidationMessageFor(model => model.YearId)%>
								</div>
					                 <div class="col-lg-2">
								   Project
									<%: Html.DropDownListFor(model => model.ProjectId, Model.ProjectList , new { @class="form-control SearchTable"})%>
									<%: Html.ValidationMessageFor(model => model.ProjectId)%>
								</div>
									 <div class="col-lg-2">
								   Bank
									<%: Html.DropDownListFor(model => model.SearchBankId, Model.BankList , new { @class="form-control SearchTable"})%>
									<%: Html.ValidationMessageFor(model => model.SearchBankId)%>
								</div>
									 </div>
						  </div>    

						 
						  </div>   
						 
						</div>


				 
						<div style="overflow: auto;">

							<div id="data">
							</div>
						</div>
					</div>
				</div>

				<div class="ExcelPasswordScreeen" style="display: none; height: 300px; width: 300px; vertical-align: central;">
					<div id="ExcelPopup" class="popup1" style="height: 300px; width: 300px; vertical-align: central;">
						<div class="panel panel-primary">
							<div class="panel-heading">
								<div>Excel Password</div>
							</div>
							<div class="panel-body ">


								<span>Enter Password </span>
								<%: Html.PasswordFor(model => model.ExcelPassword , new { @class="form-control"})%>
								<%: Html.ValidationMessageFor(model => model.ExcelPassword)%>
								<br />
								<br />
								<div style="text-align: center;">
									<input type="submit" id="btnExcelPassword"  value="Ok" name="command" class="btn btn-info" />
									&nbsp;
									<input type="button" id="btncloseScreen" value="Close" class="btn btn-info" />
								</div>
							</div>
						</div>
					</div>
				</div>




            <div class="VariableSalaryScreeen" style="display: none; height: 600px; width: 300px; vertical-align: central;">
					<div id="VariableSalaryPopup" class="popup1" style="height: 600px; width: 300px; vertical-align: central;">
						<div class="panel panel-primary">
							<div class="panel-heading">
								<div>Variable Screen</div>
							</div>
							<div class="panel-body ">
                              <div class="col-lg-12">
								 Month
									<%: Html.DropDownListFor(model => model.VarMonth_Id, Model.VarMonthList , new { @class="form-control SearchTable"})%>
									<%: Html.ValidationMessageFor(model => model.VarMonth_Id)%>
								</div>

						
							         <div class="col-lg-12">
								   Year
									<%: Html.DropDownListFor(model => model.VarYear_Id, Model.VarYearList , new { @class="form-control SearchTable"})%>
									<%: Html.ValidationMessageFor(model => model.VarYear_Id)%>
								</div>
                                <br />
                                <br />
								<div class="col-lg-12" style="text-align: center;">
                                     <br />
                          
									<input type="button" id="btnMYear"  value="Ok" name="btnMYear" class="btn btn-info" />
									&nbsp;
									<input type="button" id="btnclose" value="Close" class="btn btn-info" />
  <br />
                                    <br />
                                    	<div id="VarMsgDiv" style="display: none;">
			          <label id="VarlblError"></label>
		        </div>
								</div>
							</div>
						</div>
					</div>
				</div>

</div>
			</div>
		





	</form>

	<form method="post" id="idNotesDownloadGet" action="NotesDownloadGet">

		<%: Html.HiddenFor(model=>model.AttachmentId) %>

		<div class="row">
			<div id="divAttachment" style="display: none;">
				<div class="col-lg-8">
					<div class="form-group">
						<%: Html.TextAreaFor(model => model.notesText, new { @class="form-control note", @rows="6", @cols="150"})%>
						<%: Html.ValidationMessageFor(model => model.notesText)%>
					</div>
					<div class="form-group">
						<%: Html.TextBoxFor(model => model.imageUpload, new { @class="form-control note", @type="file"})%>
						<%: Html.ValidationMessageFor(model => model.imageUpload)%>
					</div>
					<%-- <div class="form-group">
						<button type="button" id="btnAttach" name="btnAttach" class="btn btn-info">Attach</button>
					</div>--%>
					<div class="form-group">
						<button type="button" id="btnAddNotes" name="btnAddNotes" class="btn btn-info">Add Notes</button>
					</div>
				</div>

				<div class="col-lg-4">
					<div class="form-group">
						<%--<%: Html.TextAreaFor(model => model.notesList, new { @class="form-control"})%>--%>
						<img id="img" src="" class="img-responsive img-thumbnail" />
						<div id="base"></div>
					</div>
					<div class="form-group">
						<%--<button type="button" id="btnDelete" name="btnDelete" class="btn btn-danger">Delete</button>--%>
						<%--<a id="aDownload" download="">Download File</a>    --%>
					</div>
				</div>

				<div class="col-lg-12">
					<div id="downloadList"></div>
				</div>

			</div>
		</div>
	</form>

	<form method="post" id="idAttendanceDownloadGet" action="AttendanceDownloadGet">
		<div id="divOpen" style="display: none;">
		</div>
	</form>

	<div id="childDiv" style="display: none;">
		<div style="margin-left: 16px; margin-top: 2px; margin-bottom: 4px;">
		</div>
		<div class="panel col-lg-12" id="childEditPanel" style="margin-left: 20px; margin-top: 10px; display: none; width: 97%;">
		</div>
		<div class="col-lg-12">
			<div id="childData">
			</div>
		</div>
	</div>
	`
	
	
		
	<input type="hidden" id="flag" name="flag" />

	<div class="Loading" style="display: none;">
		<div id="popup" class="popup">
			<div class="content">
				<img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
			</div>
		</div>
	</div>

	<div class="navbar navbar-inverse navbar-fixed-bottom">
		<div id="MsgDiv" style="display: none;">
			<label id="lblError"></label>
		</div>
	</div>

</asp:Content>
