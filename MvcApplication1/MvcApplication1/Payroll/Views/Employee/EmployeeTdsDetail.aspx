<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.EmpTdsDetailViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EmployeeTdsDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Employee/EmployeeTdsDetail.js") %>"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            if ((sessionStorage.length == 0) || (sessionStorage == undefined)) {
                urlBackRestrict();
            }
        });
     </script> 

    <style type="text/css">
        .Loding {
            width: 100%;
            display: block;
        }

        .content {
            background: #fff;
            padding: 28px 26px 33px 25px;
        }

        .popup {
            border-radius: 7px;
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
    <form method="post" id="EmpTds" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">
            <div class="row">
                <div>
                    <div id="DetailPanel" class="panel panel-primary">
                        <div class="panel-heading">
                            <span  style="text-align:left;"class="panel-title" > Employee Tds   </span>
                             <a style="color: #E6F1F3;float:right" target="_blank" href="<%= Url.Content("~/Help/Payroll/Employee/EmployeeTdsDetail.html") %>  ">
                                      <b><img style="width:30px;height:20px;margin-top:-10px;padding-top:-10px" src="<%= Url.Content("~/Images/Help-icon.PNG") %> " /></b>
                                     </a>
                           
                        </div>
                        <div class="panel-body ">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <button type="button" id="addTds" name="addTds" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Add Tds</button>
                                </div>
                            </div>

                            <div id="data" style="overflow: auto;">
                            </div>
                          <button  style="margin-bottom:2px;margin-right:1px; display:none;"  type="submit" id="btnExcel" class="btn  btn-success enabling cancel" name="command" value="Excel"><span class="glyphicon glyphicon-export"></span> Excel</button>
                          <button  style="margin-bottom:2px;margin-right:1px; display:none;"  type="submit" id="btnWord" class="btn  btn-success enabling cancel" name="command" value="Word"><span class="glyphicon glyphicon-export"></span> Word</button>

                            <div>
                                <div class="col-lg-12">
                                    <div class="panel panel-primary" id="EditPanel" style="display: none; margin-top: 20px;">
                                        <div class="panel-heading">
                                            <h3 class="panel-title"><span id="panelHeader"></span></h3>
                                        </div>
                                        <div class="panel-body ">

                                            <fieldset>
                                                <legend>Select</legend>

                                                <div class="form-group">
                                                    <div class="col-md-2"> Employee </div>
                                                    <div class="col-md-2">
                                                        <input type="hidden" id="EmpTdsDetailId" name="EmpTdsDetailId" />
                                                        <input type="hidden" id="FiscalYearDetail" name="FiscalYearDetail" />
                                                        <%: Html.DropDownListFor(model => model.EmployeeId ,Model.EmployeeList, new {@class="form-control parent"})%>
                                                        <%: Html.ValidationMessageFor(model => model.EmployeeId)%>
                                                    </div>
                                                    <div class="col-md-2"> Financial Year</div>
                                                    <div class="col-md-2">
                                                        <%: Html.DropDownListFor(model => model.FiscalYear, Model.FiscalYearList  , new {@class="form-control parent"})%>
                                                        <%: Html.ValidationMessageFor(model => model.FiscalYear)%>
                                                    </div>
                                                    <div class="col-md-2">Is Metro</div>
                                                    <div class="col-md-2">
                                                        <%: Html.RadioButtonFor(model => model.IsMetro, "True",  new { @id="IsMetroYes" })%>Yes &nbsp;
                                                        <%: Html.RadioButtonFor(model => model.IsMetro, "False", new { @id="IsMetroNo" , @checked=true})  %>No 
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <fieldset>
                                                <legend>Under Section 10 & 17 </legend>

                                                <div class="form-group">
                                                    <div class="col-md-4">Rent Paid (Sec 10 (13A)) </div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.HraExemption, new {@class="form-control parent", placeholder="Rent Paid (Sec 10 (13A))" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HraExemption)%>
                                                    </div>
                                                    <div class="col-md-4">Transport/Conveyance Exemption (Sec 10(14))</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.TransExemption, new {@class="form-control parent", placeholder="Transport/Conveyance Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.TransExemption)%>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <div class="col-md-4">Other Exemption under Sec10(10)(gratuity,etc)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.OtherExemption, new {@class="form-control parent", placeholder="Other Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.OtherExemption)%>
                                                    </div>
                                                    <div class="col-md-4">Medical Bills Exemption (Sec 17 (2))</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.MedBillExemption, new {@class="form-control parent", placeholder="Medical Bills Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedBillExemption)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Children's Education Allowance Exemption (Sec 10 (14))</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.ChildEduExemption, new {@class="form-control parent", placeholder="Children's Education Allowance Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.ChildEduExemption)%>
                                                    </div>
                                                    <div class="col-md-4">LTA Exemption (Sec 10(5))</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.LtaExemption, new {@class="form-control parent", placeholder="LTA Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.LtaExemption)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Uniform expenses (Sec 10(14))</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.UniformExemption, new {@class="form-control parent", placeholder="Uniform expenses" })%>
                                                        <%: Html.ValidationMessageFor(model => model.UniformExemption)%>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <fieldset>
                                                <legend>Other Income</legend>

                                                <div class="form-group">
                                                    <div class="col-md-4">House Loan Interest. (Sec 80C) </div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.HouseLoanInterest, new {@class="form-control parent", placeholder="House Loan Interest" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HouseLoanInterest)%>
                                                    </div>
                                                    <div class="col-md-4">House/Property Income or Loss</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.HousePropertyIncome, new {@class="form-control parent", placeholder="House/Property Income or Loss" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HousePropertyIncome)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Other Income</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.OtherIncome, new {@class="form-control parent", placeholder="Other Income" })%>
                                                        <%: Html.ValidationMessageFor(model => model.OtherIncome)%>
                                                    </div>
                                                    <div class="col-md-4">Savings Bank interest</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.SavingbankInterest, new {@class="form-control parent", placeholder="Savings Bank interest" })%>
                                                        <%: Html.ValidationMessageFor(model => model.SavingbankInterest)%>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <fieldset>
                                                <legend>Under Section VI-A</legend>

                                                <div class="form-group">
                                                    <div class="col-md-4">Medical Insurance Premium/health check (sec 80D)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.MedInsurPremium, new {@class="form-control parent", placeholder="Medical Insurance Premium" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedInsurPremium)%>
                                                    </div>
                                                    <div class="col-md-4">Medical Insurance Premium for Parents (Sec 80D)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.MedInsurPremiumPar, new {@class="form-control parent", placeholder="Medical Insurance Premium for Parents" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedInsurPremiumPar)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Medical for handicaped dependents (Sec 80 DD)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.MedHandicapDepend, new {@class="form-control parent", placeholder="Medical for handicaped" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedHandicapDepend)%>
                                                    </div>
                                                    <div class="col-md-4">Medical for Special Diseases (Sec 80DDB)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.MedSpecDisease, new {@class="form-control parent", placeholder="Medical for Special Diseases" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedSpecDisease)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Higher Education Loan Interest Repayment (Sec 80 E)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.HighEduLoanRepayment, new {@class="form-control parent", placeholder="Higher Education Loan Interest" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HighEduLoanRepayment)%>
                                                    </div>
                                                    <div class="col-md-4">Donation to approved fund and charities (Sec 80 G)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.DonateFundCharity, new {@class="form-control parent", placeholder="Donation to approved fund" })%>
                                                        <%: Html.ValidationMessageFor(model => model.DonateFundCharity)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Rent Deduction (Sec 80 GG) only if HRA not Received</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.RentDeduction, new {@class="form-control parent", placeholder="Rent Deduction" })%>
                                                        <%: Html.ValidationMessageFor(model => model.RentDeduction)%>
                                                    </div>
                                                    <div class="col-md-4">Deduction for permanent disability (80 U)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.PermanentDisableDeduction, new {@class="form-control parent", placeholder="Deduction for permanent disability" })%>
                                                        <%: Html.ValidationMessageFor(model => model.PermanentDisableDeduction)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Any other deduction</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.OtherDeduction, new {@class="form-control parent", placeholder="Any other deduction" })%>
                                                        <%: Html.ValidationMessageFor(model => model.OtherDeduction)%>
                                                    </div>
                                                    <div class="col-md-4">Savings Bank interest exemption (sec 80TTA) </div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.SavingbankInterestException, new {@class="form-control parent", placeholder="Savings Bank interest exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.SavingbankInterestException)%>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <fieldset>
                                                <legend>Under Section 80 C</legend>
                                                <div class="form-group">
                                                    <div class="col-md-4">Pension Scheme (Sec 80 C)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.PensionScheme, new {@class="form-control parent", placeholder="Pension Scheme" })%>
                                                        <%: Html.ValidationMessageFor(model => model.PensionScheme)%>
                                                    </div>
                                                    <div class="col-md-4">National Savings Certificate (sec 80 C)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.Nsc, new {@class="form-control parent", placeholder="National Savings Certificate" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Nsc)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Public provident fund (Sec 80 C)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.Ppf, new {@class="form-control parent", placeholder="Public provident fund" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Ppf)%>
                                                    </div>
                                                    <div class="col-md-4">Employees Provident fund & Voulntary PF (sec 80 C)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.Pf, new {@class="form-control parent", placeholder="Employees Provident fund" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Pf)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Children's Education Tuition Fees (sec 80 C) </div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.ChildEduFund, new {@class="form-control parent", placeholder="Children's Education Tuition Fees" })%>
                                                        <%: Html.ValidationMessageFor(model => model.ChildEduFund)%>
                                                    </div>
                                                    <div class="col-md-4">House Loan Principal Repayment, regn/stamp duty (Sec 80 C)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.HouseLoanPrincipalRepay, new {@class="form-control parent", placeholder="House Loan Principal Repayment" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HouseLoanPrincipalRepay)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Insurance Premium (sec 80 C)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.InsurancePremium, new {@class="form-control parent", placeholder="Insurance Premium" })%>
                                                        <%: Html.ValidationMessageFor(model => model.InsurancePremium)%>
                                                    </div>
                                                    <div class="col-md-4">Mutual Fund/ULIP/FD , etc. (Sec 80 c)</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.MutualFund, new {@class="form-control parent", placeholder="Mutual Fund/ULIP/FD" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MutualFund)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-4">Employer's contribution to NPS (sec 80CCD(2))</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.Fd, new {@class="form-control parent", placeholder="Employer's contribution" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Fd)%>
                                                    </div>
                                                    <div class="col-md-4">Rajiv Gandhi Equity Savings Scheme (sec 80CCG) Max Allowed</div>
                                                    <div class="col-md-2">
                                                        <%: Html.TextBoxFor(model => model.Rajivgandhisavingsscheme, new {@class="form-control parent", placeholder="Rajiv Gandhi Equity Savings Scheme" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Rajivgandhisavingsscheme)%>
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <div class="col-lg-offset-4 col-lg-3">
                                                <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                                <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                                <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClose" name="btnClose" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="Loding" style="display: none;">
            <div id="popup" class="popup">
                <div class="content">
                    <img id="LoadingProgress" src="<%= Url.Content("~/Images/loading.gif") %> " />
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
