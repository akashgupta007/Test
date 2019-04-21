using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;





namespace PoiseERP.Areas.Payroll.Controllers
{
    public class LoanController : Controller
    {
        //
        // GET: /Payroll/Loan/
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();






        public GridView GridViewGet(DataTable dt, string ReportHeader)
        {


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 4;

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            DataTable dtCompany = obj.CurrentCompanyInformationGet("");
            string CompanyName = Convert.ToString(dtCompany.Rows[0][0]);
            string CompanyAddress = Convert.ToString(dtCompany.Rows[0][1]);
            string CompanyCity = Convert.ToString(dtCompany.Rows[0][2]);
            string PinCode = Convert.ToString(dtCompany.Rows[0][3]);


            DateTimeFormatInfo dinfo1 = new DateTimeFormatInfo();

            HeaderCell2.Text = CompanyName + "<br />" + CompanyAddress + "<br />" + CompanyCity + " " + PinCode + "<br />" + ReportHeader;
            int colsan = dt.Columns.Count;
            HeaderCell2.ColumnSpan = colsan;
            HeaderRow.Cells.Add(HeaderCell2);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            return GridView1;
        }


        public ActionResult DataExportPDF(GridView GridView1, string FileName)
        {

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            GridView1.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

            return View();
        }

        public ActionResult DataExportWord(GridView GridView1, string FileName)
        {

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".doc");

            GridView1.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult DataExportExcel(GridView GridView1, string FileName)
        {
            GridView1.Font.Size = 8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();


            return View();
        }

        #region  Loan Type code from here

        public ActionResult LoanType()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                LoanTypeViewModel model = new LoanTypeViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }





        [HttpPost]
        public ActionResult LoanTypeGet(LoanTypeViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 9 };
                DataTable dt = obj.LoanGet(model.LoanId, null);
                if (dt.Rows.Count > 0)
                {
                    if (model.LoanId == null)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTable(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult LoanTypeCreate(LoanTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.LoanExistsValidate(model.LoanName, model.LoanDescription, model.MinLoanAmount, model.MaxLoanAmount, model.DefaultInterestPct, model.DefaultLoanTerm, null);
                if (Exist == 5)
                {
                    return Json(new { Flag = 1, Html = "Loan Details Already Exists !!" }, JsonRequestBehavior.AllowGet);

                }


                if (Exist == 0)
                {
                    int? status = obj.LoanCreate(model.LoanName, model.LoanDescription, model.MinLoanAmount, model.MaxLoanAmount, model.DefaultInterestPct, model.DefaultLoanTerm, model.HolidayMonths, model.SelectHolidayMonth, model.IsActive);


                    int[] columnHide = new[] { 0, 9 };
                    DataTable dt = obj.LoanGet(null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult LoanTypeupdate(LoanTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.LoanExistsValidate(model.LoanName, model.LoanDescription, model.MinLoanAmount, model.MaxLoanAmount, model.DefaultInterestPct, model.DefaultLoanTerm, model.LoanId);
                if (Exist == 5)
                {
                    return Json(new { Flag = 1, Html = "Loan Details Already Exists !!" }, JsonRequestBehavior.AllowGet);

                }
                if (Exist == 0)
                {

                    int? status = obj.LoanUpdate(model.LoanId, model.LoanName, model.LoanDescription, model.MinLoanAmount, model.MaxLoanAmount, model.DefaultInterestPct, model.DefaultLoanTerm, model.HolidayMonths, model.SelectHolidayMonth, model.IsActive);


                    int[] columnHide = new[] { 0, 9 };
                    DataTable dt = obj.LoanGet(null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Loan Code Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = "Loan Already in Use !";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        public List<CollectionListDataL> empLoanList = new List<CollectionListDataL>();

        [HttpPost]
        public ActionResult LoanTypedelete(LoanTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.LoanDelete(model.LoanId);
                int[] columnHide = new[] { 0, 9 };
                DataTable dt = obj.LoanGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Employee  Loan  code from here

        public ActionResult EmployeeLoan()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpLoanViewmodel model = new EmpLoanViewmodel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult FillDdlMonthGet(EmpLoanViewmodel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }           

            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtmONTHItem = obj.MonthGet(null);
            foreach (DataRow dr in dtmONTHItem.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = SectionListItems;
            return Json(new { response1 = list, response2 = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FillDdlyEARGet(EmpLoanViewmodel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtyearItem = obj.YearGet(1, 4);
            foreach (DataRow dr in dtyearItem.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[0].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = SectionListItems;
            return Json(new { response1 = list, response2 = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EmployeeLoanGet(EmpLoanViewmodel model)
        {

            string[] htmllistsuccess;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16,18 };

                DataTable dt = obj.EmpLoanGet(null);

                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        empLoanList.Add(new CollectionListDataL
                        {
                            Text = dt.Rows[j][4].ToString(),
                            Value = dt.Rows[j][3].ToString()
                        });

                    }
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() , htmllistsuccess = empLoanList }, JsonRequestBehavior.AllowGet);
                }
                //if (dt.Rows.Count > 0)
                //{
                //    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                //    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult EmployeeLoanCreate(EmpLoanViewmodel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int flag = CommonUtil.CompareDate(model.LoanDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                int? Exist = obj.EmpLoanExistsValidate(model.LoanId, model.EmployeeId, model.LoanDate);
                if (Exist == 0)
                {
                    int? status = obj.EmpLoanCreate(model.LoanId, model.EmployeeId, model.LoanDate, model.LoanAmount, model.LoanTerm, model.HolidayMonths, model.InterestPct, model.AddlPrincipalPerMonth, model.ExpensePerMonth, model.TaxableFunction, model.PaymentTransactionId, model.Notes, model.IsApproved, model.HolidayPeriod,model.ComputingItemMonthList);

                    int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16 };
                    DataTable dt = obj.EmpLoanGet(null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeLoanupdate(EmpLoanViewmodel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.LoanDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                int? status = obj.EmpLoanUpdate(model.EmpLoanId, model.LoanId, model.EmployeeId, model.LoanDate, model.LoanAmount, model.LoanTerm, model.HolidayMonths, model.InterestPct, model.AddlPrincipalPerMonth, model.ExpensePerMonth, model.TaxableFunction, model.PaymentTransactionId, model.Notes, model.IsApproved, model.HolidayPeriod,null);

                int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16 };
                DataTable dt = obj.EmpLoanGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeLoandelete(EmpLoanViewmodel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpLoanDelete(model.EmpLoanId);
                int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16 };
                DataTable dt = obj.EmpLoanGet(null);


                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }



                
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region Loan Approval code from here


        public ActionResult LoanApproval()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpLoanViewmodel model = new EmpLoanViewmodel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult LoanApprovalGet(EmpLoanViewmodel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16 };
                StringBuilder htmlTable = null;

                DataTable dt = null;
                if (model.Is_Approved == "Approved")
                {
                    dt = obj.EmpLoanGet(true);

                }
                else if (model.Is_Approved == "Approval")
                {
                    dt = obj.EmpLoanGet(null);

                }

                else
                {
                    dt = obj.EmpLoanGet(false);
                }


                if (dt.Rows.Count > 0 && model.Is_Approved == "Approval")
                {

                    htmlTable = CommonUtil.htmlTableEditModeloanApproval(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else if (dt.Rows.Count > 0 && model.Is_Approved == "Approved")
                {

                    htmlTable = CommonUtil.htmlTable(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else if (dt.Rows.Count > 0 && model.Is_Approved == "DisApprove")
                {

                    htmlTable = CommonUtil.htmlTable(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult EmployeeLoanApprove(EmpLoanViewmodel model)
        {
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpLoanApprove(model.EmpLoanId);


                int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16 };
                DataTable dt = obj.EmpLoanGet(true);

                if (dt.Rows.Count > 0 && model.Is_Approved == "Approved")
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeloanApproval(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Flag = 0, Html = "<div class='alert alert-danger'>No Record !!</div>" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }



        }

        [HttpPost]
        public ActionResult EmployeeLoanDisApprove(EmpLoanViewmodel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpLoanDisapprove(model.EmpLoanId);


                int[] columnHide = new[] { 0, 1, 3, 10, 13, 14, 16 };


                DataTable dt = obj.EmpLoanGet(true);

                if (dt.Rows.Count > 0 && model.Is_Approved == "Approved")
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeloanApproval(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Flag = 0, Html = "<div class='alert alert-danger'>No Record !!</div>" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }



        }


        #endregion

        #region Employee Loan Detail code

        public ActionResult EmployeeLoanDetail()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpLoanDetailViewmodel model = new EmpLoanDetailViewmodel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        [HttpPost]
        public ActionResult EmployeeLoanDetail(EmpLoanDetailViewmodel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtEmployeeLoanDetail"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeLoanDetail"], "Employee Loan Detail");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeLoanDetail");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeLoanDetail");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeLoanDetail");
                    }
                    return a;
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }







        [HttpPost]
        public ActionResult EmployeeLoanDetailGet(EmpLoanDetailViewmodel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = new[] { 0, 1, 3, 13, 14, 15, 16, 17 };
                DataTable dt = obj.EmpLoanDetailGet(model.LoanId, model.EmployeeId);
               GEmpId=model.EmployeeId;
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableLoanDetails(dt, columnHide);
                    Session.Add("dtEmployeeLoanDetail", dt);

                    return Json(new { Flag = 0, html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { Flag = 1, html = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }

        }

        int ?GEmpId=null;
        [HttpPost]
        public ActionResult EmployeeLoanEMIDetailGet(int? EmpLoanId)
        {
            StringBuilder htmlTableLoanInfo = new StringBuilder();
            StringBuilder htmlTableLoanRepayment = new StringBuilder();
            StringBuilder htmlTableLoanActivity = new StringBuilder();
            StringBuilder htmlTableAmortizationSchedule = new StringBuilder();
            StringBuilder htmlTableayoffStatement = new StringBuilder();

            StringBuilder htmlTabs = new StringBuilder();
            StringBuilder htmlTabContent = new StringBuilder();
            int Flag = 0; ;

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataSet DsLoan = obj.EmpLoanEmiGet(EmpLoanId);

                if (DsLoan.Tables.Count > 0)
                {

                    htmlTabs.Append("<ul id='tabs' class='nav nav-tabs' data-tabs='tabs'><li id='tabLoan' class='active list-group-item-info'><a href='#tabLoanInfo' data-toggle='tab'>Loan Information</a></li>");
                    htmlTabs.Append("<li  id='tabPayment'><a href='#TabLoanRepayment' class='list-group-item-info' data-toggle='tab'>Loan Repayment</a></li><li id='tabActivity'><a href='#TabLoanActivity' class='list-group-item-info' data-toggle='tab'>Loan Activity</a></li>");
                    htmlTabs.Append("<li  id='tabAmortization'><a href='#tabAmortizationSchedule' class='list-group-item-info' data-toggle='tab'>Amortization Schedule </a></li>");
                    htmlTabs.Append("<li id='tabPayoff'><a href='#TabPayoffStatement' class='list-group-item-info' data-toggle='tab'>Payoff Statement</a></li></ul>");

                    if (DsLoan.Tables[0].Rows.Count > 0)
                    {
                        htmlTableLoanInfo = CommonUtil.htmlTableAll(DsLoan.Tables[0]);
                    }

                    if (DsLoan.Tables[1].Rows.Count > 0)
                    {
                        DataTable dt = obj.EmpLoanDetailGet(null, GEmpId);
                        for (int i = 0; i <dt.Rows.Count; i++)
                        {                           
                            for (int j = 0; j < DsLoan.Tables[1].Rows.Count; j++)
                            {
                                string d = DsLoan.Tables[1].Rows[j][0].ToString();
                                Char delimiter = '/';
                                String[] substrings = d.Split(delimiter);
                                if (dt.Rows[i][18].ToString().Substring(0, 2) == substrings[1].ToString())
                                {

                                }
                            }                           
                        }
                        htmlTableLoanRepayment = CommonUtil.htmlTableAll(DsLoan.Tables[1]);
                    }
                    if (DsLoan.Tables[2].Rows.Count > 0)
                    {
                        htmlTableLoanActivity = CommonUtil.htmlTableAll(DsLoan.Tables[2]);
                    }
                    if (DsLoan.Tables[3].Rows.Count > 0)
                    {
                        htmlTableAmortizationSchedule = CommonUtil.htmlTableAll(DsLoan.Tables[3]);
                    }
                    if (DsLoan.Tables[5].Rows.Count > 0)
                    {
                        htmlTableayoffStatement = CommonUtil.htmlTableAll(DsLoan.Tables[5]);
                    }

                    htmlTabs.Append("<div id='my-tab-content' class='tab-content'><div class='tab-pane  active ' id='tabLoanInfo' style='margin-top: 10px'>" + Convert.ToString(htmlTableLoanInfo) + "</div>");

                    htmlTabs.Append("<div class='tab-pane' id='TabLoanRepayment' style='margin-top: 10px;overflow:auto; max-height:300px;margin-bottom:20PX;'>" + Convert.ToString(htmlTableLoanRepayment) + "</div>");

                    htmlTabs.Append("<div class='tab-pane' id='TabLoanActivity' style='margin-top: 10px;overflow:auto; max-height:300px;margin-bottom:20PX;'>" + Convert.ToString(htmlTableLoanActivity) + "</div>");

                    htmlTabs.Append("<div class='tab-pane' id='tabAmortizationSchedule' style='margin-top: 10px;overflow:auto; max-height:300px;margin-bottom:20PX;'>" + Convert.ToString(htmlTableAmortizationSchedule) + "</div>");

                    htmlTabs.Append("<div class='tab-pane' id='TabPayoffStatement' style='margin-top: 10px'>" + Convert.ToString(htmlTableayoffStatement) + " </div></div>");


                    return Json(new { Flag = 0, html = htmlTabs.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 0, html = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }

        }




        #endregion

        #region  Loan  Recovery Detail code
        public ActionResult LoanRecoveryDetail()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpLoanDetailViewmodel model = new EmpLoanDetailViewmodel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult LoanRecoveryDetail(EmpLoanDetailViewmodel model, string command)
        {
            string html = null;
            try
            {
                if (Session["DtEmployeeRecovery"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["DtEmployeeRecovery"], " Employee Loan Recovery Detail");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeLoanRecoveryDeatil");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeLoanRecoveryDeatil");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeLoanRecoveryDeatil");
                    }
                    return a;
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult LoanRecoveryDetailGet(EmpLoanDetailViewmodel model)
        {


            string html = null;

            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = new[] { 0, 1, 3, 13, 14, 15, 16, 17 };
                DataTable dt = obj.EmpLoanRecoveryDetailGet(model.LoanId, model.FromDate, model.ToDate);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("DtEmployeeRecovery", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTable(dt, columnHide);
                    return Json(new { Flag = 0, html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(dt, columnHide);
                    return Json(new { Flag = 1, html = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion



        public ActionResult LoanProcess()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                PayrollUtil model = new PayrollUtil();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        //----Employee Laon Process ---------

        [HttpPost]
        public ActionResult LoanProcess(PayrollUtil model, string command)
        {
            string html = null;
            int? status = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                if (command == "Search")
                {

                    string IsCheck = string.Empty;







                    DataTable dtPayroll = obj.EmpPayrollLoanAmountGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId);
                    int[] HideColumn = { 0, 6 };
                    if (dtPayroll.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollLoanProcess(dtPayroll, HideColumn);
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }


                if (command == "Update")
                {

                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;

                    int n = 0;


                    if (model.EmpDataList != null)
                    {
                        int count = model.EmpDataList.Count;

                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "EmpDataList[" + x + "].isRowCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.EmpDataList[x].EmployeeId != null)
                                {
                                    model.ItemAmount = model.EmpDataList[x].ItemAmount;
                                    if ((model.ItemAmount != null) || (model.ItemAmount != 0))
                                    {
                                        status = obj.EmpPayrollItemAmountUpdate(model.EmpDataList[x].EmployeeId, model.MonthId, model.Year, model.PayrollItemId, model.ItemAmount, "Loan");
                                    }
                                }
                            }

                        }





                    }












                    DataTable dtPayroll = obj.EmpPayrollLoanAmountGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId);
                    int[] HideColumn = { 0, 6 };
                    if (dtPayroll.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollLoanProcess(dtPayroll, HideColumn);
                        htmlTable.Append("<div class='alert alert-success'>Loan Process Updated Successfully for the Selected Record(s) !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {


                        html = "<div class='alert alert-success'>Loan Process  Updated Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }





















                }



                html = "<div class='alert alert-danger'>No Record !!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }















    }
    public class CollectionListDataL
    {

        public string Text { get; set; }
        public string Value { get; set; }
    }
}


