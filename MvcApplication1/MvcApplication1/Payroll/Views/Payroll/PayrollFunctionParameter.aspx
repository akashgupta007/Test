<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.PayrollFunctionParameterViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Payroll Function Parameter
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <script src="<%=Url.Content("~/Scripts/ViewScript/Payroll/Payroll/PayrollFunctionParameter.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <form id="PayrollFunctionParameter" novalidate="novalidate">
    <div class="form-horizontal" style="margin-top:10px;">
          <div class="row">
                <div class="col-lg-6">
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Payroll Function Parameter Details  </h3>
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <button type="button" id="newEntity" name="newEntity" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>New Record  </button>
                                </div>
                            </div>                            
                          <div id="data" style="overflow:auto; "> 
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
                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                        </div>
                        <div  class="panel-body ">                                                                                                                                     
                               <div class="form-group"> 
                                 <div class="col-lg-3"> Payroll Function List:
                                 </div>                                
                                     <div class="Form-control col-lg-9"> 
                                           <input type="hidden" id="PayrollFunctionParameterId" name="PayrollFunctionParameterId" />
                                 <%: Html.DropDownListFor(model => model.PayrollFunctionListId ,Model.PayrollFunctionListList, new {@class="form-control"})%>
                                              </div>                                                             
                                            </div>  
                            
                            <div class="form-group" > 
                                 <div class="col-lg-3">Parameter Sequence:
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                   <%: Html.TextBoxFor(model => model.ParameterSeq, new {@class="form-control",maxlength = "2", placeholder="Enter Parameter Sequence "})%>
                                             <%: Html.ValidationMessageFor(model => model.ParameterSeq)%>       
                                              </div>                                                             
                                            </div>
                                                          
                               <div class="form-group"> 
                                 <div class="col-lg-3">Parameter Name:
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                  <%: Html.TextBoxFor(model => model.ParameterName, new {@class="form-control", placeholder="Enter Parameter Name "})%>
                                          <%: Html.ValidationMessageFor(model => model.ParameterName)%>       
                                              </div>                                                             
                                            </div>
                            
                            <div class="form-group"> 
                                 <div class="col-lg-3">Parameter Datatype :
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                       <%: Html.TextBoxFor(model => model.ParameterDatatype, new {@class="form-control", placeholder="Enter Parameter Data Type "})%>                                                  
                                              </div>                                                             
                                            </div>
                            <div class="form-group"> 
                                 <div class="col-lg-3">Table Name: 
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                         <%: Html.TextBoxFor(model => model.TableName, new {@class="form-control", placeholder="Enter Table Name "})%>                
                                              </div>                                                             
                                            </div>
                            <div class="form-group"> 
                                 <div class="col-lg-3">Comments: 
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                  <%: Html.TextBoxFor(model => model.Comments, new {@class="form-control", placeholder="Enter Comments "})%>                
                                              </div>                                                             
                                            </div>
                            <div class="form-group"> 
                                 <div class="col-lg-3">Ref. Payroll Function:
                                 </div>                                
                                     <div class="Form-control col-lg-9">
                                    <%: Html.DropDownListFor(model => model.PayrollFunctionId ,Model.PayrollFunctionList, new {@class="form-control"})%>                         
                                              </div>                                                             
                                            </div>                                                                                                                                                           
                            <div class="form-group">
                                <div class="col-lg-offset-4 col-lg-3">
                                    <button  style="margin-bottom:2px;margin-right:1px;"  type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picure"></span> Save</button>
                                    <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom:2px;margin-right:1px;display:none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                    <button style="margin-bottom:1px;margin-right:1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span>Close</button>
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
