<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PoiseERP.Areas.Payroll.Models.TdsLimitViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TDS Policy
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%= Url.Content("~/Scripts/ViewScript/Payroll/Master/TdsLimit.js") %>"></script>

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
    <form id="TdsLimit" novalidate="novalidate">

        <div class="form-horizontal" style="margin-top: 10px;">

            <div class="row">

                <div class="col-lg-12">
                    <div class="col-lg-12" style="margin-top: 10px; text-align: center;">
                        <div class="form-group col-lg-6">
                            Select Fiscal year: 
                        </div>
                        <div class="form-group col-lg-6">
                            <select id="FiscalYear" name="FiscalYear" class="form-control Parent">
                                <option value="">--Select--</option>
                            </select>
                        </div>
                    </div>
                </div>

                <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                    <li id="tabTdsLimitSetup" class="active list-group-item-info"><a href="#TdsLimitSetup" data-toggle="tab">Set TDS Limit</a></li>
                    <li id="tabTaxSlabSetup"><a href="#TaxSlabSetup" class="list-group-item-info" data-toggle="tab">Tax Slab Setup</a></li>
                </ul>

                <div>
                    <div id="my-tab-content" class="tab-content">
                        <div class="tab-pane  active " id="TdsLimitSetup" style="margin-top: 10px">
                            <div class="parent_div">
                                <div class="col-lg-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <%--<h3 class="panel-title">TDS Limit Setup</h3>--%>
                                            <span  style="text-align:left;"class="panel-title" > Employee Category  </span>
                                            <span style=" float:right; vertical-align:top" ><a style="background-color: #337ab7; color: #E6F1F3;" target="_blank" href="<%= Url.Content("~/Help/Payroll/Master/TdsPolicy.html") %>">
                                            <b><img src="<%= Url.Content("~/Images/HelpImage.PNG") %> " /></b></a></span>
                                        </div>
                                        <div class="panel-body ">
                                            <fieldset>
                                                <legend>Under Section 10 & 17 </legend>

                                                <div class="form-group">
                                                    <div class="col-md-3">HRA Exemption for Metro in %</div>
                                                    <div class="col-md-3">
                                                        <input type="hidden" id="TdsLimitId" name="TdsLimitId" />
                                                        <input type="hidden" id="FiscalYearDetail" name="FiscalYearDetail" />
                                                        <%: Html.TextBoxFor(model => model.MetroPct, new {@class="form-control parent limit" , @maxlength="3", placeholder="HRA Exemption for Metro" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MetroPct)%>
                                                    </div>
                                                    <div class="col-md-3">HRA Exemption for Non-Metro in %</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.NonMetroPct, new {@class="form-control parent limit" , @maxlength="3", placeholder="HRA Exemption for Non-Metro" })%>
                                                        <%: Html.ValidationMessageFor(model => model.NonMetroPct)%>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <div class="col-md-3">Transport/Conveyance Exemption (Sec 10(14))</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.TransExemption, new {@class="form-control parent limit", @maxlength="14", placeholder="Transport/Conveyance Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.TransExemption)%>
                                                    </div>
                                                    <div class="col-md-3">Other Exemption under Sec10(10)(gratuity,etc)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.OtherExemption, new {@class="form-control parent limit", @maxlength="14", placeholder="Other Exemption under" })%>
                                                        <%: Html.ValidationMessageFor(model => model.OtherExemption)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Medical Bills Exemption (Sec 17 (2))</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.MedBillExemption, new {@class="form-control parent limit", @maxlength="14", placeholder="Medical Bills Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedBillExemption)%>
                                                    </div>
                                                    <div class="col-md-3">Children's Education Allowance Exemption (Sec 10 (14))</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.ChildEduExemption, new {@class="form-control parent limit", @maxlength="14", placeholder="Children's Education Allowance" })%>
                                                        <%: Html.ValidationMessageFor(model => model.ChildEduExemption)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">LTA Exemption (Sec 10(5))</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.LtaExemption, new {@class="form-control parent limit", @maxlength="14", placeholder="LTA Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.LtaExemption)%>
                                                    </div>
                                                    <div class="col-md-3">Uniform Exemption Maximum Allowed</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.UniformExemption, new {@class="form-control parent limit", @maxlength="14", placeholder="Uniform Exemption" })%>
                                                        <%: Html.ValidationMessageFor(model => model.UniformExemption)%>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <fieldset>
                                                <legend>Other Income</legend>

                                                <div class="form-group">
                                                    <div class="col-md-3">House Loan Interest. (Sec 80C) </div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.HouseLoanInterest, new {@class="form-control parent limit", @maxlength="14", placeholder="Other Income" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HouseLoanInterest)%>
                                                    </div>
                                                    <div class="col-md-3">House/Property Income or Loss</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.HousePropertyIncome, new {@class="form-control parent limit", @maxlength="14", placeholder="House/Property Income or Loss" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HousePropertyIncome)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Other Income</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.OtherIncome, new {@class="form-control parent limit", @maxlength="14", placeholder="Other Income" })%>
                                                        <%: Html.ValidationMessageFor(model => model.OtherIncome)%>
                                                    </div>
                                                    <div class="col-md-3">Savings Bank interest</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.SavingbankInterest, new {@class="form-control parent limit", @maxlength="14", placeholder="Savings Bank interest" })%>
                                                        <%: Html.ValidationMessageFor(model => model.SavingbankInterest)%>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <fieldset>
                                                <legend>Under Section VI-A</legend>

                                                <div class="form-group">
                                                    <div class="col-md-3">Medical Insurance Premium/health check (sec 80D)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.MedInsurPremium, new {@class="form-control parent limit", @maxlength="14", placeholder="Medical Insurance" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedInsurPremium)%>
                                                    </div>
                                                    <div class="col-md-3">Medical Insurance Premium for Parents (Sec 80D)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.MedInsurPremiumPar, new {@class="form-control parent limit", @maxlength="14", placeholder="Medical Insurance Premium for Parents" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedInsurPremiumPar)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Medical for handicaped dependents (Sec 80 DD)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.MedHandicapDepend, new {@class="form-control parent limit", @maxlength="14", placeholder="Medical for handicaped dependents" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedHandicapDepend)%>
                                                    </div>
                                                    <div class="col-md-3">Medical for Special Diseases (Sec 80DDB)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.MedSpecDisease, new {@class="form-control parent", @maxlength="14", placeholder="Medical for Special Diseases" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MedSpecDisease)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Higher Education Loan Interest Repayment (Sec 80 E)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.HighEduLoanRepayment, new {@class="form-control parent limit", @maxlength="14", placeholder="Higher Education" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HighEduLoanRepayment)%>
                                                    </div>
                                                    <div class="col-md-3">Donation to approved fund and charities (Sec 80 G)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.DonateFundCharity, new {@class="form-control parent limit", @maxlength="14", placeholder="Donation" })%>
                                                        <%: Html.ValidationMessageFor(model => model.DonateFundCharity)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Rent Deduction (Sec 80 GG) only if HRA not Received</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.RentDeduction, new {@class="form-control parent limit", @maxlength="14", placeholder="Rent Deduction" })%>
                                                        <%: Html.ValidationMessageFor(model => model.RentDeduction)%>
                                                    </div>
                                                    <div class="col-md-3">Deduction for permanent disability (80 U)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.PermanentDisableDeduction, new {@class="form-control parent limit", @maxlength="14", placeholder="permanent disability" })%>
                                                        <%: Html.ValidationMessageFor(model => model.PermanentDisableDeduction)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Any other deduction</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.OtherDeduction, new {@class="form-control parent limit", @maxlength="14", placeholder="Any other deduction" })%>
                                                        <%: Html.ValidationMessageFor(model => model.OtherDeduction)%>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <fieldset>
                                                <legend>Under Section 80 C</legend>
                                                <div class="form-group">
                                                    <div class="col-md-3">Pension Scheme (Sec 80 C)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.PensionScheme, new {@class="form-control parent limit", @maxlength="14", placeholder="Pension Scheme" })%>
                                                        <%: Html.ValidationMessageFor(model => model.PensionScheme)%>
                                                    </div>
                                                    <div class="col-md-3">National Savings Certificate (sec 80 C)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.Nsc, new {@class="form-control parent limit", @maxlength="14", placeholder="National Savings Certificate" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Nsc)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Public provident fund (Sec 80 C)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.Ppf, new {@class="form-control parent limit", @maxlength="14", placeholder="Public provident fund" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Ppf)%>
                                                    </div>
                                                    <div class="col-md-3">Employees Provident fund & Voulntary PF (sec 80 C)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.Pf, new {@class="form-control parent limit", @maxlength="14", placeholder="Employees Provident fund" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Pf)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Children's Education Tuition Fees (sec 80 C) </div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.ChildEduFund, new {@class="form-control parent limit", @maxlength="14", placeholder="Children's Education Tuition Fees" })%>
                                                        <%: Html.ValidationMessageFor(model => model.ChildEduFund)%>
                                                    </div>
                                                    <div class="col-md-3">House Loan Principal Repayment, regn/stamp duty (Sec 80 C)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.HouseLoanPrincipalRepay, new {@class="form-control parent limit", @maxlength="14", placeholder="House Loan Principal" })%>
                                                        <%: Html.ValidationMessageFor(model => model.HouseLoanPrincipalRepay)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Insurance Premium (sec 80 C)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.InsurancePremium, new {@class="form-control parent limit", @maxlength="14", placeholder="Insurance Premium" })%>
                                                        <%: Html.ValidationMessageFor(model => model.InsurancePremium)%>
                                                    </div>
                                                    <div class="col-md-3">Mutual Fund/ULIP/FD , etc. (Sec 80 c)</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.MutualFund, new {@class="form-control parent limit", @maxlength="14", placeholder="Mutual Fund/ULIP/FD" })%>
                                                        <%: Html.ValidationMessageFor(model => model.MutualFund)%>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">Employer's contribution to NPS (sec 80CCD(2))</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.Fd, new {@class="form-control parent limit", @maxlength="14", placeholder="Employer's contribution" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Fd)%>
                                                    </div>
                                                    <div class="col-md-3">Rajiv Gandhi Equity Savings Scheme (sec 80CCG) Max Allowed</div>
                                                    <div class="col-md-3">
                                                        <%: Html.TextBoxFor(model => model.Rajivgandhisavingsscheme, new {@class="form-control parent limit", @maxlength="14", placeholder="Rajiv Gandhi Equity" })%>
                                                        <%: Html.ValidationMessageFor(model => model.Rajivgandhisavingsscheme)%>
                                                    </div>
                                                </div>
                                            </fieldset>

                                            <div class="col-lg-12" style="margin-top: 10px; text-align: center;">
                                                <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsert" name="btnInsert" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                                <button type="button" id="btnUpdate" name="btnUpdate" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                                <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnClear" name="btnClear" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-sign"></span> Clear</button>
                                                <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnDelete" name="btnDelete" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove"></span> Delete</button>
                                                <img id="LoadingImage" style="display: none;" src="<%= Url.Content("~/Images/loading.gif") %> " />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div>
                            </div>
                        </div>

                        <div class="tab-pane " id="TaxSlabSetup" style="margin-top: 10px">

                            <div id="DetailPanel" class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Tax Slab Setup</h3>
                                </div>
                                <div class="panel-body ">
                                    <div class="form-group">
                                        <div class="col-md-3">
                                            <button type="button" id="addSlab" name="addSlab" class="btn btn-info"><span class="glyphicon glyphicon-plus"></span>Slab</button>
                                        </div>
                                    </div>

                                    <div id="data" style="overflow: auto;">
                                    </div>

                                    <div>
                                        <div class="col-lg-12">
                                            <div class="panel panel-primary" id="EditPanel" style="display: none; margin-top: 20px;">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title"><span id="panelHeader"></span></h3>
                                                </div>
                                                <div class="panel-body ">
                                                    <div class="form-group">
                                                        <div class="col-md-2">Tax Slab Name</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.TaxSlabName, new {@class="form-control child setup", @readonly="readonly", placeholder="Tax Slab Name" })%>
                                                            <%: Html.ValidationMessageFor(model => model.TaxSlabName)%>
                                                        </div>
                                                        <div class="col-md-2">Min Value Male</div>
                                                        <div class="col-md-2">
                                                            <input type="hidden" id="TaxSlabId" name="TaxSlabId" />
                                                            <input type="hidden" id="TaxSlabName" name="TaxSlabName" />
                                                            <%: Html.TextBoxFor(model => model.MinvalMale, new {@class="form-control child setup", @maxlength="14", placeholder="Min Value Male" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MinvalMale)%>
                                                        </div>
                                                        <div class="col-md-2">Max Value Male</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.MaxvalMale, new {@class="form-control child setup", @maxlength="14", placeholder="Max Value Male" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MaxvalMale)%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-2">Min Value Female</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.MinvalFemale, new {@class="form-control child setup", @maxlength="14", placeholder="Min Value Female" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MinvalFemale)%>
                                                        </div>
                                                        <div class="col-md-2">Max Value Female</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.MaxvalFemale, new {@class="form-control child setup", @maxlength="14", placeholder="Max Value Female" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MaxvalFemale)%>
                                                        </div>
                                                        <div class="col-md-2">Min Value Senior Citizon</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.MinvalSeniorcitizon, new {@class="form-control child setup", @maxlength="14", placeholder="Min Value Senior Citizon" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MinvalSeniorcitizon)%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-2">Max Value Senior Citizon</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.MaxvalSeniorcitizon, new {@class="form-control child setup", @maxlength="14", placeholder="Max Value Senior Citizon" })%>
                                                            <%: Html.ValidationMessageFor(model => model.MaxvalSeniorcitizon)%>
                                                        </div>
                                                        <div class="col-md-2">Tax Rate</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.TaxRate, new {@class="form-control child setup", @maxlength="3", placeholder="Tax Rate" })%>
                                                            <%: Html.ValidationMessageFor(model => model.TaxRate)%>
                                                        </div>
                                                        <div class="col-md-2">Description</div>
                                                        <div class="col-md-2">
                                                            <%: Html.TextBoxFor(model => model.Description, new {@class="form-control child setup", placeholder="Description" })%>
                                                            <%: Html.ValidationMessageFor(model => model.Description)%>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-offset-4 col-lg-3">
                                                        <button style="margin-bottom: 2px; margin-right: 1px;" type="button" id="btnInsertSlab" name="btnInsertSlab" class="btn  btn-success enabling"><span class="glyphicon glyphicon-picture"></span> Save</button>
                                                        <button type="button" id="btnUpdateSlab" name="btnUpdateSlab" style="margin-bottom: 2px; margin-right: 1px; display: none;" class="btn btn-success enabling"><span class="glyphicon glyphicon-pencil"></span> Update</button>
                                                        <button style="margin-bottom: 1px; margin-right: 1px;" type="button" id="btnCloseSlab" name="btnCloseSlab" class="btn  btn-success enabling"><span class="glyphicon glyphicon-remove-circle"></span> Close</button>
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
