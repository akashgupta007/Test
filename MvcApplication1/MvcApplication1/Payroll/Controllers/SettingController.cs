using CrystalDecisions.Shared;
using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoiseERP.Models;
using PoisePayroll.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PoiseERP.Areas.Payroll.Controllers
{
    public class SettingController : Controller
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        // GET: /Payroll/Setting/


        public ActionResult Payslip_partial(int EmployeeId)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            Session["EmpCenter_Employee__Id"] = EmployeeId;
            PayrollReportViewModel model = new PayrollReportViewModel();
            return PartialView("Payslip_partial", model);


        }



        public ActionResult PaySlip()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                PayrollReportViewModel model = new PayrollReportViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult PaySlip(PayrollReportViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                int m = 0;

                if (command == "Search")
                {
                    int[] HideColumn = new[] { 0, 10, 11, 12, 13 };
                    DataTable dtPayrollSalaryDetailGet = obj.EmpPayrollSalaryDetailsGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId);

                    if (dtPayrollSalaryDetailGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTablePaySlip(dtPayrollSalaryDetailGet, HideColumn);
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                html = "<div class='alert alert-danger'>No Record !!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult PaySlipMail(PayrollReportViewModel model)
        {


            string html = null;

            string empCode = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;

                int n = 0;

                string MailSettings = System.Configuration.ConfigurationManager.AppSettings["MailSettings"];


                //if (MailSettings.ToUpper() == "ON")
                //{

                string Name = null;
                string Gender = null;


                //string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                //string SenderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
                //string Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                //string PortAddress = System.Configuration.ConfigurationManager.AppSettings["PortAddress"];
                //string UseSSL = System.Configuration.ConfigurationManager.AppSettings["UseSSL"];

                string SmtpServer = null;
                string SenderEmail = null;
                string Password = null;
                string PortAddress = null;
                string UseSSL = null;
                string Priority = null;





                if (model.EmployeeDataList != null)
                {
                    int count = model.EmployeeDataList.Count;
                    for (int x = 0; x < count; x++)
                    {
                        checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                        IsCheck = Request[checkBoxName];
                        if (IsCheck == "on")
                        {
                            if (model.EmployeeDataList[x].EmployeeId != null)
                            {
                                StringBuilder sb = new StringBuilder(null);

                                DataTable dtMailSettings = obj.MailSetupGet(null, null, model.EmployeeDataList[x].EmployeeId, "SendMail");
                                if (dtMailSettings.Rows.Count > 0)
                                {
                                    SmtpServer = Convert.ToString(dtMailSettings.Rows[0][4]);
                                    SenderEmail = Convert.ToString(dtMailSettings.Rows[0][5]);
                                    Password = Convert.ToString(dtMailSettings.Rows[0][6]);
                                    PortAddress = Convert.ToString(dtMailSettings.Rows[0][7]);
                                    UseSSL = Convert.ToString(dtMailSettings.Rows[0][10]);
                                    Priority = "MailPriority.High";

                                    if (string.IsNullOrEmpty(SmtpServer) && string.IsNullOrEmpty(SenderEmail) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(PortAddress) && string.IsNullOrEmpty(UseSSL))
                                    {
                                        return Json(new { Flag = 1, Html = "Check Mail Setting !!" }, JsonRequestBehavior.AllowGet);
                                    }

                                    DataSet ds = obj.PaySlipGet(1, model.MonthId, model.Year, Convert.ToString(model.EmployeeDataList[x].EmployeeId));

                                    Name = Convert.ToString(ds.Tables[0].Rows[0][3]);
                                    Gender = Convert.ToString(ds.Tables[0].Rows[0][13]);

                                    string mailToAddress = Convert.ToString(ds.Tables[0].Rows[0][41]);
                                    if (string.IsNullOrEmpty(mailToAddress))
                                    {

                                        if (string.IsNullOrEmpty(empCode))
                                        {
                                            empCode = Convert.ToString(ds.Tables[0].Rows[0][2]);
                                        }

                                        else
                                        {
                                            empCode = ',' + Convert.ToString(ds.Tables[0].Rows[0][2]);
                                        }
                                        continue;
                                    }

                                    DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
                                    string monthName = dinfo.GetMonthName(Convert.ToInt16(model.MonthId));

                                    string ReportHeader = "PaySlip for the Month-" + monthName + "-" + model.Year;


                                    if (Gender == "M")
                                    {
                                        Gender = "Mr.";
                                        sb.Append("Dear " + Gender + " " + "<b>" + Name + "</b>" + ",");
                                    }
                                    if (Gender == "F")
                                    {
                                        Gender = "Ms.";
                                        sb.Append("Dear " + Gender + " " + "<b>" + Name + "</b>" + ",");
                                    }
                                    sb.Append("<br />");
                                    sb.Append("<br />");
                                    sb.Append("Please  find  the attached  " + "<b>" + ReportHeader + "</b>" + ".");
                                    sb.Append("<br />");
                                    sb.Append("In case of any discrepancy please get back to HR within next two days.");
                                    sb.Append("<br />");
                                    sb.Append("<br />");
                                    sb.Append("<br />");
                                    sb.Append("<br />");
                                    sb.Append("Regards,");
                                    sb.Append("<br />");
                                    sb.Append("HR Department");

                                    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt;
                                    rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                                    //---------------Chttp://localhost:53223/Report/Payroll/Salary/PaySlip.aspx.csommon Pay slip---------------
                                    rpt.Load(Server.MapPath("~/Report/Payroll/Salary/PaySlipMCL.rpt"));

                                    //---------------Mathura Pay slip---------------
                                    //rpt.Load(Server.MapPath("~/Report/Payroll/Salary/PaySlipMathura.rpt"));
                                    // rpt.Load(Server.MapPath("~/Report/Payroll/Salary/PaySlipCastle.rpt"));

                                    ds.Tables[0].TableName = "PaySlip";
                                    ds.Tables[1].TableName = "CompanyAddress";
                                    rpt.SetDataSource(ds);


                                    MailMessage mail = new MailMessage();

                                    mail.From = new MailAddress(SenderEmail);
                                    mail.To.Add(mailToAddress);
                                    mail.Attachments.Add(new Attachment(rpt.ExportToStream(ExportFormatType.PortableDocFormat), "Payslip.pdf"));
                                    mail.Subject = ReportHeader;
                                    mail.Body = Convert.ToString(sb);
                                    mail.IsBodyHtml = true;
                                    mail.Priority = MailPriority.High;

                                    SmtpClient smtp = new SmtpClient();

                                    smtp.Host = SmtpServer;
                                    if (UseSSL.ToUpper() == "TRUE")
                                    {
                                        smtp.EnableSsl = true;
                                    }
                                    else
                                    {
                                        smtp.EnableSsl = false;
                                    }

                                    smtp.Credentials = new System.Net.NetworkCredential(SenderEmail, Password);
                                    smtp.Port = Convert.ToInt32(PortAddress);
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Send(mail);
                                }
                                else
                                {
                                    return Json(new { Flag = 0, Html = " No Record Exist !! " }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                    }
                }

                string empcodemsg = null;
                if (string.IsNullOrEmpty(empCode))
                {
                    empcodemsg = null;
                }
                else
                {
                    empcodemsg = empCode + " have no Email Id !! && ";
                }

                return Json(new { Flag = 0, Html = empcodemsg + " Mail sent Successfully for other selected Employees !! " }, JsonRequestBehavior.AllowGet);


                //}
                //else
                //{
                //    return Json(new { Flag = 1, Html = "Mail Setting is OFF !!" }, JsonRequestBehavior.AllowGet);

                //}





            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }








        public ActionResult SalaryPaidStatementPartial(int EmployeeId)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            Session["EmpCenter_Employee__Id"] = EmployeeId;
            PayrollReportViewModel model = new PayrollReportViewModel();
            return PartialView("SalaryPaidStatementPartial", model);


        }


        [HttpPost]
        public ActionResult SalaryReportGet(PayrollReportViewModel model)
        {
            string html = null;
            string ReportHeader = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                int[] HideColumn = new[] { 9, 11, 12, 19, 20, 21 };
                DataSet dtPayroll = obj.EmpSalaryPaidGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.EmpCategoryId);
                if (dtPayroll.Tables.Count > 0)
                {

                    if (dtPayroll.Tables[0].Rows.Count > 0)
                    {


                        DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
                        string monthName = dinfo.GetMonthName(Convert.ToInt16(model.MonthId));
                        ReportHeader = "Salary for the Month-" + monthName + "-" + model.Year;


                        StringBuilder htmlTable = CommonUtil.htmlTableWithCompanyHeader(dtPayroll.Tables[0], HideColumn, ReportHeader);
                        DataTable dt = CommonUtil.DataTableColumnRemove(dtPayroll.Tables[0], HideColumn);
                        Session.Add("dtSalaryReportGet", dt);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult SalaryReportLateEntryDetailsGet(PayrollReportViewModel model)
        {
            string html = null;
            string ReportHeader = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                int[] HideColumn = new[] { 3, 4 };
                DataTable dtPayroll = obj.EmpPayrollSalaryLateEntryDetailsGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);


                if (dtPayroll.Rows.Count > 0)
                {


                    DateTimeFormatInfo dinfo = new DateTimeFormatInfo();
                    string monthName = dinfo.GetMonthName(Convert.ToInt16(model.MonthId));
                    ReportHeader = "Salary for the Month-" + monthName + "-" + model.Year;


                    StringBuilder htmlTable = CommonUtil.htmlTableWithCompanyHeader(dtPayroll, HideColumn, ReportHeader);
                    DataTable dt = CommonUtil.DataTableColumnRemove(dtPayroll, HideColumn);
                    Session.Add("dtSalaryReportGet", dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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




















        public ActionResult CalenderDayType()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                CalenderDayTypeViewModel model = new CalenderDayTypeViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult CalenderDayTypeGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 4 };
                DataTable dt = obj.DayTypeGet();
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



        [HttpPost]
        public ActionResult CalenderDayTypeCreate(CalenderDayTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.DayTypeExistsValidate(model.DayType, null);
                if (Exist == 0)
                {
                    int? status = obj.DayTypeCreate(model.DayType, model.Overrides, model.Notes, model.AmountType, model.Value);
                    int[] columnHide = new[] { 0, 4 };
                    DataTable dt = obj.DayTypeGet();

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
        public ActionResult CalenderDayTypeeupdate(CalenderDayTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.DayTypeExistsValidate(model.DayType, model.DayTypeId);
                if (Exist == 0)
                {
                    int? status = obj.DayTypeUpdate(model.DayTypeId, model.DayType, model.Overrides, model.Notes, model.AmountType, model.Value);
                    int[] columnHide = new[] { 0, 4 };
                    DataTable dt = obj.DayTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Day Type Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult CalenderDayTypeedelete(CalenderDayTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.DayTypeDelete(model.DayTypeId);
                int[] columnHide = new[] { 0, 4 };
                DataTable dt = obj.DayTypeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //----------------Company Calendar Type ------------

        public ActionResult CompanyCalendarType()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                CompanyCalendarViewModel model = new CompanyCalendarViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult CompanyCalendarTypeGet(int CalendarYear, string DayorDateType)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 3, 4 };
                DataTable dtCalendar = obj.CompanyCalendarTypeGet(CalendarYear, DayorDateType);
                if (dtCalendar.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtCalendar, columnHide);
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
        public ActionResult CompanyCalendarTypeCreate(CompanyCalendarViewModel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.CompanyCalendarTypeExistsValidate(model.CalendarTypeDesc, model.DayTypeId, model.EmpTypeId, model.LocationId, model.CalendarYear, model.ShiftId, null);
                if (Exist == 0)
                {
                    int? status = obj.CompanyCalendarTypeCreate(model.DayTypeId, model.CalendarTypeDesc, model.EmpTypeId, model.LocationId, model.CalendarYear, model.Notes, model.IsWorkDay, model.ShiftId);
                    int[] columnHide = new[] { 0, 1, 2, 3, 4 };
                    DataTable dtCalendar = obj.CompanyCalendarTypeGet(model.CalendarYear, model.DayorDateType);
                    if (dtCalendar.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtCalendar, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }

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
        public ActionResult CompanyCalendarTypeUpdate(CompanyCalendarViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.CompanyCalendarTypeExistsValidate(model.CalendarTypeDesc, model.DayTypeId, model.EmpTypeId, model.LocationId, model.CalendarYear, model.ShiftId, model.CalendarTypeId);
                if (Exist == 0)
                {
                    int? status = obj.CompanyCalendarTypeUpdate(model.CalendarTypeId, model.CalendarTypeDesc, model.DayTypeId, model.EmpTypeId, model.LocationId, model.CalendarYear, model.Notes, model.IsWorkDay, model.ShiftId);
                    int[] columnHide = new[] { 0, 1, 2, 3, 4 };
                    DataTable dtCalendar = obj.CompanyCalendarTypeGet(model.CalendarYear, model.DayorDateType);
                    if (dtCalendar.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtCalendar, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }


                }
                else
                {
                    return Json(new { Flag = 1, Html = "Calendar Type Already Exists !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult CompanyCalendarTypeDelete(CompanyCalendarViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.CompanyCalendarTypeDelete(model.CalendarTypeId);
                int[] columnHide = new[] { 0, 1, 2, 3, 4 };
                DataTable dtCalendar = obj.CompanyCalendarTypeGet(model.CalendarYear, model.DayorDateType);

                if (dtCalendar.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtCalendar, columnHide);
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


        //----------------Company Calendar Day ------------




        [HttpPost]
        public ActionResult CompanyCalendarDaysGet(int CalendarTypeId, string DayorDateType)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = null;
                if (DayorDateType == "Day")
                {
                    columnHide = new[] { 0, 1, 3 };
                }

                if (DayorDateType == "Date")
                {
                    columnHide = new[] { 0, 1, 2 };
                }
                DataTable dtDay = obj.CompanyCalendarDaysGet(CalendarTypeId);
                if (dtDay.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtDay, columnHide);
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
        public ActionResult CompanyCalendarDaysCreate(CompanyCalendarViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = null;
                int[] columnHide = null;
                if (model.DayorDateType == "Day")
                {
                    columnHide = new[] { 0, 1, 3 };
                }

                if (model.DayorDateType == "Date")
                {
                    columnHide = new[] { 0, 1, 2 };
                }

                Exist = obj.CompanyCalendarDaysExistsValidate(model.CalendarTypeId, model.WorkDay, model.WorkDate, model.DayorDateType);
                if (Exist == 0)
                {
                    int? status = obj.CompanyCalendarDaysCreate(model.CalendarTypeId, model.WorkDay, model.WorkDate, model.WorkDayNotes);

                    DataTable dtDay = obj.CompanyCalendarDaysGet(model.CalendarTypeId);

                    if (dtDay.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtDay, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult CompanyCalendarDaysUpdate(CompanyCalendarViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int? status = obj.CompanyCalendarDaysUpdate(model.CalendarDaysId, model.CalendarTypeId, model.WorkDay, model.WorkDate, model.WorkDayNotes);

                int[] columnHide = null;
                if (model.DayorDateType == "Day")
                {
                    columnHide = new[] { 0, 1, 3 };
                }

                if (model.DayorDateType == "Date")
                {
                    columnHide = new[] { 0, 1, 2 };
                }
                DataTable dtDay = obj.CompanyCalendarDaysGet(model.CalendarTypeId);

                if (dtDay.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtDay, columnHide);
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
        public ActionResult CompanyCalendarDaysDelete(CompanyCalendarViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int? status = obj.CompanyCalendarDaysDelete(model.CalendarDaysId);

                int[] columnHide = null;
                if (model.DayorDateType == "Day")
                {
                    columnHide = new[] { 0, 1, 3 };
                }

                if (model.DayorDateType == "Date")
                {
                    columnHide = new[] { 0, 1, 2 };
                }

                DataTable dtDay = obj.CompanyCalendarDaysGet(model.CalendarTypeId);

                if (dtDay.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtDay, columnHide);
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








        //------------Salary Deduction Rule-----------------

        public ActionResult BmRuleType()
        {
            try
            {
                BmRuleViewModel model = new BmRuleViewModel();


                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult BmRuleTypeGet(BmRuleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dt = obj.BmRuleTypeGet();
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dt, columnHide);
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
        public ActionResult BmRuleTypeCreate(BmRuleViewModel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.BmRuleTypeExistsValidate(model.RuleName, null);
                if (Exist == 0)
                {
                    int? status = obj.BmRuleTypeCreate(model.RuleName, model.RuleDescription);
                    int[] columnHide = new[] { 0 };
                    DataTable dt = obj.BmRuleTypeGet();
                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult BmRuleTypeUpdate(BmRuleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.BmRuleTypeExistsValidate(model.RuleName, model.RuleId);
                if (Exist == 0)
                {
                    int? status = obj.BmRuleTypeUpdate(model.RuleId, model.RuleName, model.RuleDescription);
                    int[] columnHide = new[] { 0 };
                    DataTable dt = obj.BmRuleTypeGet();
                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Rule Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BmRuleTypeDelete(BmRuleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.BmRuleTypeDelete(model.RuleId);
                int[] columnHide = new[] { 0 };
                DataTable dt = obj.BmRuleTypeGet();

                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dt, columnHide);
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
        public ActionResult BMRuleDetailGet(int? RuleId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0,  7 };
                DataTable dtRuleDetailGet = obj.BmRuleDetailGet(RuleId);
                if (dtRuleDetailGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtRuleDetailGet, columnHide);
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
        public ActionResult BMRuleDetailCreate(BmRuleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (ModelState.IsValid)
                {

                    int? Exist = obj.BmRuleDetailExistsValidate(model.Description, null);
                    if (Exist == 0)
                    {
                        if (model.MinTimeLateEntryAllowance > model.MaxTimeLateEntryAllowance)
                        {
                            html = "Start Time Should be less than End Time";
                            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            int? status = obj.BmRuleDetailCreate(model.Description, model.MinTimeLateEntryAllowance, model.MaxTimeLateEntryAllowance, model.DeductionType, model.DeductionValue, model.AllowedLateEntryDays, model.RuleId, model.IsActive);

                            int[] columnHide = new[] { 0, 7 };
                            DataTable dtRuleDetailGet = obj.BmRuleDetailGet(model.RuleId);
                            if (dtRuleDetailGet.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtRuleDetailGet, columnHide);
                                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                html = "<div class='alert alert-danger'>No Record !!</div>";
                                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "Rule Description Already Exists" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Invalid Data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BMRuleDetailUpdate(BmRuleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                if (ModelState.IsValid)
                {
                    if (model.MinTimeLateEntryAllowance > model.MaxTimeLateEntryAllowance)
                    {
                        html = "Start Time Should be less than End Time";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        int? Exist = obj.BmRuleDetailExistsValidate(model.Description, model.RuleDetailId);
                        if (Exist == 0)
                        {

                            int? status = obj.BmRuleDetailUpdate(model.RuleDetailId, model.Description, model.MinTimeLateEntryAllowance,
                                                                 model.MaxTimeLateEntryAllowance, model.DeductionType, model.DeductionValue,
                                                                 model.AllowedLateEntryDays, model.RuleId, model.IsActive);

                            int[] columnHide = new[] { 0,7 };
                            DataTable dtRuleDetailGet = obj.BmRuleDetailGet(model.RuleId);
                            if (dtRuleDetailGet.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtRuleDetailGet, columnHide);
                                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                html = "<div class='alert alert-danger'>No Record !!</div>";
                                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { Flag = 2, Html = "Rule Description Already Exists" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Invalid Data" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BMRuleDetailDelete(BmRuleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.BmRuleDetailDelete(model.RuleDetailId);
                int[] columnHide = new[] { 0,  7 };
                DataTable dtRuleDetailGet = obj.BmRuleDetailGet(model.RuleId);
                if (dtRuleDetailGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtRuleDetailGet, columnHide);
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


        //------------------Work day hours time setting----------------




        public ActionResult WorkDayTime()
        {
            try
            {
                WorkdayTimeViewModel model = new WorkdayTimeViewModel();


                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }






        [HttpPost]
        public ActionResult WorkDayTimeGet(WorkdayTimeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 5, 6, 9, 11 };
                DataTable dt = obj.WorkdayTimeGet();
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


        [HttpPost]
        public ActionResult WorkDayTimeCreate(WorkdayTimeViewModel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.WorkdayTimeExistsValidate(model.WorkDayDescription, model.CalendarTypeId, model.RuleId, model.StartDt, model.EndDt, null);
                if (Exist == 0)
                {

                    int? status = obj.WorkdayTimeCreate(model.WorkDayDescription, model.WorkDayHours, model.InTime, model.OutTime, model.FunctionId, model.StartDt, model.EndDt, model.CalendarTypeId, model.RuleId, model.HalfDayWorkHours);
                    int[] columnHide = new[] { 0, 5, 6, 9, 11 };
                    DataTable dt = obj.WorkdayTimeGet();

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
        public ActionResult WorkDayTimeupdate(WorkdayTimeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.WorkdayTimeExistsValidate(model.WorkDayDescription, model.CalendarTypeId, model.RuleId, model.StartDt, model.EndDt, model.WorkdayTimeId);

                if (Exist == 0)
                {
                    int? status = obj.WorkdayTimeUpdate(model.WorkdayTimeId, model.WorkDayDescription, model.WorkDayHours, model.InTime, model.OutTime, model.FunctionId, model.StartDt, model.EndDt, model.CalendarTypeId, model.RuleId, model.HalfDayWorkHours);
                    int[] columnHide = new[] { 0, 5, 6, 9, 11 };
                    DataTable dt = obj.WorkdayTimeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Day Time Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult WorkDayTimedelete(WorkdayTimeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.WorkdayTimeDelete(model.WorkdayTimeId);
                int[] columnHide = new[] { 0, 5, 6, 9, 11 };
                DataTable dt = obj.WorkdayTimeGet();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //-------------------- Document Category ------------------------------
        public ActionResult DocumentCategory()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DocumentCategoryViewModel model = new DocumentCategoryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult DocumentCategoryGet(DocumentCategoryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showColumn = new[] { 1, 2 };
                DataTable dt = obj.DocumentCategoryGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DocumentCategoryCreate(DocumentCategoryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.DocumentCategoryExistsValidate(model.DocumentCategoryName, null);
                if (Exist == 0)
                {
                    int? status = obj.DocumentCategoryCreate(model.DocumentCategoryName, model.DocumentCategoryDescription);
                    int[] showColumn = new[] { 1, 2 };
                    DataTable dt = obj.DocumentCategoryGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
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
        public ActionResult DocumentCategoryUpdate(DocumentCategoryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.DocumentCategoryExistsValidate(model.DocumentCategoryName, model.DocumentCategoryId);
                if (Exist == 0)
                {
                    int? status = obj.DocumentCategoryUpdate(model.DocumentCategoryId, model.DocumentCategoryName, model.DocumentCategoryDescription);
                    int[] showColumn = new[] { 1, 2 };
                    DataTable dt = obj.DocumentCategoryGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "DocumentCategory Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DocumentCategoryDelete(DocumentCategoryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.DocumentCategoryDelete(model.DocumentCategoryId);
                int[] showColumn = new[] { 1, 2 };
                DataTable dt = obj.DocumentCategoryGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-------------------------- Attach Document File -----------------------------------------

        public ActionResult AttachFile()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DocumentViewModel model = new DocumentViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult AttachFileGet(DocumentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 5, 6, 7 };
                DataTable dtFileGet = obj.DocumentGet();
                StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtFileGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AttachFileCreate(DocumentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.DocumentExistsValidate(model.DocumentName, null);
                if (Exist == 0)
                {
                    int? status = obj.DocumentCreate(model.DocumentName, model.DocumentCategoryId, model.DocumentDate, model.DocumentExpiryDate, model.IsForEmpPortal, model.UserType, model.UserId);
                    int[] columnHide = new[] { 0, 2, 5, 6, 7 };
                    DataTable dtFileGet = obj.DocumentGet();
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtFileGet, columnHide);
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
        public ActionResult AttachFileUpdate(DocumentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.DocumentUpdate(model.DocumentId, model.DocumentName, model.DocumentCategoryId, model.DocumentDate, model.DocumentExpiryDate, model.IsForEmpPortal, model.UserType, model.UserId);
                int[] columnHide = new[] { 0, 2, 5, 6, 7 };
                DataTable dtFileGet = obj.DocumentGet();
                StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtFileGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AttachFileDelete(DocumentViewModel model)
        {
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.DocumentDelete(model.DocumentId);
                int[] columnHide = new[] { 0, 2, 5, 6, 7 };
                DataTable dtFileGet = obj.DocumentGet();
                StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtFileGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //---- Attach File Document --------------------

        [HttpPost]
        public ActionResult AttachFileDocumentGet(int DocumentObjId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 4 };
                DataTable dtAttachFileDocumentGet = obj.DocumentObjectGet(DocumentObjId);
                if (dtAttachFileDocumentGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtAttachFileDocumentGet, columnHide);
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
        public ActionResult AttachFile(DocumentViewModel model, string Command)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            string contenttype;
            string fileName;

            if (Command == "Insert")
            {
                try
                {
                    int? Exist = obj.DocumentObjectExistsValidate(model.DocumentId, model.DocumentObjectName);
                    if (Exist == 0)
                    {
                        byte[] uploadFile = new byte[model.File.InputStream.Length];
                        model.File.InputStream.Read(uploadFile, 0, uploadFile.Length);

                        contenttype = model.File.ContentType;
                        fileName = Path.GetFileName(model.File.FileName);

                        int? status = obj.DocumentObjectCreate(model.DocumentId, model.DocumentObjectName, contenttype, uploadFile, fileName);
                        ViewData["Msg"] = "Insert File Successfully";
                        return View(model);
                    }
                    else
                    {
                        ViewData["Msg"] = "File Already Exists !";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ViewData["Msg"] = ex.Message.ToString();
                    return View(model);
                }
            }
            else if (Command == "Update")
            {
                try
                {
                    byte[] uploadFile = new byte[model.File.InputStream.Length];
                    model.File.InputStream.Read(uploadFile, 0, uploadFile.Length);

                    contenttype = model.File.ContentType;
                    fileName = Path.GetFileName(model.File.FileName);

                    int? status = obj.DocumentObjectUpdate(model.DocumentObjectId, model.DocumentId, model.DocumentObjectName, contenttype, uploadFile, fileName);
                    ViewData["Msg"] = "Update File Successfully";
                    return View(model);

                }
                catch (Exception ex)
                {
                    ViewData["Msg"] = ex.Message.ToString();
                    return View(model);
                }
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult AttachFileDocumentUpdate(DocumentViewModel model)
        //{
        //    string html = null;
        //    try
        //    {
        //        if (Session["userName"] == null)
        //        {
        //            return Redirect("~/Home/Login");
        //        }

        //        string flag = model.UpdateDocumentData(model);
        //        int[] columnHide = new[] { 0, 1, 3, 4 };
        //        DataTable dtAttachFileDocumentGet = obj.DocumentObjectGet(model.DocumentId);
        //        if (dtAttachFileDocumentGet.Rows.Count > 0)
        //        {
        //            StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtAttachFileDocumentGet, columnHide);
        //            return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            html = "<div class='alert alert-danger'>No Record !!</div>";
        //            return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        html = ex.Message.ToString();
        //        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult AttachFileDocumentDelete(DocumentViewModel model)
        {
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.DocumentObjectDelete(model.DocumentObjectId);

                int[] columnHide = new[] { 0, 1, 3, 4 };
                DataTable dtAttachFileDocumentGet = obj.DocumentObjectGet(model.DocumentId);
                if (dtAttachFileDocumentGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtAttachFileDocumentGet, columnHide);
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



        //------ View Document -----------------

        public ActionResult ViewDocument()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DocumentViewModel model = new DocumentViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ViewDocument(DocumentViewModel model)
        {
            try
            {
                byte[] fileContent = null;
                string contentType = "";
                string filename = "";
                try
                {
                    DataTable dtDocumentDownload = obj.DocumentObjectDownloadGet(model.DocumentObjectId);
                    if (dtDocumentDownload.Rows.Count > 0)
                    {
                        DataRow dr = dtDocumentDownload.Rows[0];
                        fileContent = (byte[])dr[2];
                        contentType = dr[1].ToString();
                        filename = dr[3].ToString();
                    }

                    return File(fileContent, contentType, filename);
                }
                catch (Exception ex)
                {
                    return File(fileContent, contentType, filename);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ViewDocumentGet(DocumentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 5, 6, 7 };
                DataTable dtFileGet = obj.DocumentGet();
                StringBuilder htmlTable = CommonUtil.htmlViewDocumentNestedTableEditMode(dtFileGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ViewDocumentDownloadGet(int DocumentObjId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 4 };
                DataTable dtViewFileDocumentGet = obj.DocumentObjectGet(DocumentObjId);
                if (dtViewFileDocumentGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableDownloadEditMode(dtViewFileDocumentGet, columnHide);
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

        public ActionResult ImportExportExcel()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                Session["snAttendanceEntry"] = null;
                Session["snEmployeeMain"] = null;
                Session["snEmployeeSalary"] = null;
                ImportExportExcelViewModel model = new ImportExportExcelViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult AttendanceEntryGet(EmpAttendanceEntryViewModel model)
        {
            string PayrollrollTypeIdList = "1";
            int? totalDay = null;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] HideColumn = new[] { 0, 1, 7, 8, 9, 12, 15, 16 };
                DataSet dtAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.AttendanceSourceId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                if (dtAttendance.Tables.Count > 0)
                {
                    if (dtAttendance.Tables[0].Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTable(dtAttendance.Tables[0], HideColumn);
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                        htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "No Records !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {

                    html = "No Records !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public GridView GridViewGet(DataTable dt)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            return GridView1;
        }

        public ActionResult DataExportExcel(GridView GridView1, string FileName)
        {
            GridView1.Font.Size = 9;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".xlsx");
            Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".xls");


            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
            return View();
        }


        [HttpPost]
        public ActionResult ImportExportExcel(ImportExportExcelViewModel model, string command)
        {
            ActionResult a = null;
            StringBuilder htmlTable = new StringBuilder();
            string PayrollrollTypeIdList = "1";
            string html = null;
            string consString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                if (command == "DownloadExcelFormat")
                {
                    Session["snAttendanceEntry"] = null;
                    Session["snEmployeeMain"] = null;
                    Session["snEmployeeSalary"] = null;
                    if (model.ExcelDropDown == "Attendance Entry")
                    {
                        int[] HideColumn = { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15, 16, 18 };
                        DataSet dsAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, null, model.LocationId, model.ProjectId, model.EmpTypeId, null);
                        DataTable dtAttendance = dsAttendance.Tables[0];
                        DataTable dt = CommonUtil.DataTableColumnRemove(dtAttendance, HideColumn);
                        GridView gv = GridViewGet(dt);
                        a = DataExportExcel(gv, "AttendanceEntry");
                    }
                    if (model.ExcelDropDown == "Employee Salary")
                    {
                        int[] HideColumn = new[] { 0, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 17, 18, 19, 20, 21, 22 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        DataTable dt = CommonUtil.DataTableColumnRemove(dtEmpSalary, HideColumn);
                        GridView gv = GridViewGet(dt);
                        a = DataExportExcel(gv, "EmployeeSalary");
                    }
                    if (model.ExcelDropDown == "Employee Detail")
                    {
                        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                        //SqlDataAdapter da = new SqlDataAdapter("SELECT * from Common.employee", con);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);
                        DataTable dt = obj.EmployeeDownloadDataGet();
                        Session.Add("snEmployeeMain", dt);
                        GridView gv = GridViewGet(dt);
                        a = DataExportExcel(gv, "EmployeeDetail");
                    }
                    return a;
                }


                if (command == "UploadFile")
                {
                    try
                    {
                        Session["snAttendanceEntry"] = null;

                        string[] filePaths = Directory.GetFiles(Request.PhysicalApplicationPath + "Report\\UploadFile\\");
                        foreach (string filePath in filePaths)
                            System.IO.File.Delete(filePath);

                        string fname = Path.GetFileName(model.filename.FileName);

                        DataTable DbTableColumn = new DataTable();

                        string ext = Path.GetExtension(model.filename.FileName).ToLower();

                        string path = model.filename.FileName;

                        html = path;
                        var path1 = Path.Combine(Request.PhysicalApplicationPath + "Report\\UploadFile\\",
                                                                System.IO.Path.GetFileName(fname));
                        model.filename.SaveAs(path1);

                        string ConStr = "";

                        if (ext.Trim() == ".xls")
                        {
                            ConStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        else if (ext.Trim() == ".xlsx")
                        {
                            ConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        OleDbConnection conn = new OleDbConnection(ConStr);
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        DataTable dtExcelSchema;
                        dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                        string query = "SELECT * FROM [" + SheetName + "]";
                        conn.Close();

                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            if (model.ExcelDropDown == "Attendance Entry")
                            {
                                Session.Add("snAttendanceEntry", dt);
                            }

                            if (model.ExcelDropDown == "Employee Salary")
                            {
                                Session.Add("snEmployeeSalary", dt);
                            }
                            if (model.ExcelDropDown == "Employee Detail")
                            {
                                Session.Add("snEmployee", dt);
                            }
                        }

                        return View(model);
                    }
                    catch (Exception ex)
                    {
                        return View(model);
                    }
                }

                html = "No Records !!";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult FillAttendanceEntry(ImportExportExcelViewModel model)
        {

            StringBuilder htmlTable = new StringBuilder();
            int? totalDay = null;
            int[] HideColumn = new[] { 0, 7, 8, 9, 12, 15, 16 };
            string html = null;
            if (model.ExcelDropDown == "Attendance Entry")
            {
                if (Session["snAttendanceEntry"] != null)
                {
                    DataTable dt = (DataTable)Session["snAttendanceEntry"];
                    if (dt.Rows.Count > 0)
                    {
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);

                        htmlTable = CommonUtil.htmlTableAttendanceEntry(dt, obj, totalDay);
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                        htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                        htmlTable.Append("<div class='alert alert-success'>Excel Import in Grid Successfully !!</div>");

                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "No Records !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "No Records !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model.ExcelDropDown == "Employee Salary")
            {
                if (Session["snEmployeeSalary"] != null)
                {
                    DataTable dt = (DataTable)Session["snEmployeeSalary"];
                    if (dt.Rows.Count > 0)
                    {
                        htmlTable = CommonUtil.htmlTabledSalaryEntry(dt, obj);
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                        htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                        htmlTable.Append("<div class='alert alert-success'>Excel Import in Grid Successfully !!</div>");
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Records !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "No Records !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model.ExcelDropDown == "Employee Detail")
            {
                if (Session["snEmployee"] != null)
                {
                    DataTable dt = (DataTable)Session["snEmployee"];
                    if (dt.Rows.Count > 0)
                    {
                        //htmlTable = CommonUtil.htmlTableAll(dt);
                        htmlTable = CommonUtil.htmlTabledEmployeeEntry(dt, obj);
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                        htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                        htmlTable.Append("<div class='alert alert-success'>Excel Import in Grid Successfully !!</div>");
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Records !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "No Records !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            html = "No Records !!";
            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult AttendanceEntryEnter(ImportExportExcelViewModel model)
        {
            int? status = null;
            string PayrollrollTypeIdList = "1";
            StringBuilder htmlTable = new StringBuilder();
            int? totalDay = null;
            decimal? totalWorkDay = null;
            string html = null;

            string checkBoxName = string.Empty;
            string IsCheck = string.Empty;

            if (model.ExcelDropDown == "Attendance Entry")
            {

                if (Session["snAttendanceEntry"] == null)
                {
                    html = "File does not exist !";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }

                DataTable dtAttendance = new DataTable();
                dtAttendance.Columns.Add("emp_attendance_entry_id", typeof(int));
                dtAttendance.Columns.Add("paytype_id", typeof(string));
                dtAttendance.Columns.Add("employee_id", typeof(int));
                dtAttendance.Columns.Add("pay_period", typeof(int));
                dtAttendance.Columns.Add("pay_year", typeof(int));
                dtAttendance.Columns.Add("work_unit", typeof(string));
                dtAttendance.Columns.Add("days_worked", typeof(decimal));
                dtAttendance.Columns.Add("days_overtime", typeof(decimal));
                dtAttendance.Columns.Add("notes", typeof(string));
                dtAttendance.Columns.Add("attendance_method", typeof(int));
                dtAttendance.Columns.Add("local_day", typeof(decimal));
                dtAttendance.Columns.Add("non_local_day", typeof(decimal));

                DataTable dt = (DataTable)Session["snAttendanceEntry"];

                if (dt.Rows.Count > 0)
                {

                    int[] HideColumn = { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15, 16, 18 };
                    DataSet dsAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, null, model.LocationId, model.ProjectId, model.EmpTypeId, null);
                    bool result = ValidateDataTableColumns(dt, dsAttendance.Tables[0]);

                    if (result)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (IsInteger(Convert.ToString(row[0])))
                            {
                                try
                                {
                                    int? employee_id = obj.EmployeeIdExitsValidate(Convert.ToInt32(row[0]), Convert.ToString(row[1]));

                                    string workedday = Convert.ToString(row[3]);
                                    string overtime = Convert.ToString(row[4]);

                                    if (IsDecimal(workedday))
                                    {
                                        totalWorkDay = Convert.ToDecimal(row[3]);
                                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);

                                        if ((employee_id != null && employee_id != 0) && (totalWorkDay <= totalDay) && IsDecimal(overtime))
                                        {
                                            dtAttendance.Rows.Add(null, PayrollrollTypeIdList, Convert.ToInt32(employee_id), model.MonthId, model.Year, null, Convert.ToDecimal(row[3]), Convert.ToDecimal(row[4]), Convert.ToString(row[5]), null, null, null);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    html = ex.ToString();
                                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        html = "Column Does Not Matched !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }

                string conStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString();
                SqlConnection con = new SqlConnection(conStr);
                con.Open();

                SqlCommand cmd = new SqlCommand("HRM.emp_attendance_entry_Create", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();

                prm = cmd.Parameters.AddWithValue("@EmpAttendanceEntry", dtAttendance);
                prm.SqlDbType = SqlDbType.Structured;
                prm.TypeName = "HRM.EmpAttendanceEntry";
                status = cmd.ExecuteNonQuery();
                con.Close();

                DataSet dtEmpAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, null, model.LocationId, model.ProjectId, model.EmpTypeId, null);
                if (dtEmpAttendance.Tables.Count > 0)
                {
                    htmlTable = new StringBuilder();
                    if (dtEmpAttendance.Tables[0].Rows.Count > 0)
                    {

                        htmlTable = CommonUtil.htmlTableAll(dtEmpAttendance.Tables[0]);

                    }
                    totalDay = obj.DaysInMonth(model.MonthId, model.Year);

                    Session["snAttendanceEntry"] = null;
                    Session["snEmployeeMain"] = null;
                    Session["snEmployeeSalary"] = null;

                    htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                    htmlTable.Append("<div class='alert alert-success'>Attendance Inserted Successfully!!</div>");
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Records !!>";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }

            if (model.ExcelDropDown == "Employee Salary")
            {

                if (Session["snEmployeeSalary"] == null)
                {
                    html = "File does not exist !";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = (DataTable)Session["snEmployeeSalary"];

                if (dt.Rows.Count > 0)
                {
                    DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                    bool result = ValidateDataTableColumns(dt, dtEmpSalary);

                    if (result)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (IsInteger(Convert.ToString(row[0])))
                            {
                                try
                                {
                                    int? employee_id = obj.EmployeeIdExitsValidate(Convert.ToInt32(row[0]), Convert.ToString(row[1]));
                                    if (employee_id != null)
                                    {
                                        DateTime? salaryDate = null;
                                        decimal? gross = null;
                                        decimal? ctc = null;
                                        string strSalaryDate = Convert.ToString(row[3]);
                                        if (!string.IsNullOrEmpty(strSalaryDate))
                                        {
                                            salaryDate = Convert.ToDateTime(strSalaryDate);
                                        }

                                        string strGross = Convert.ToString(row[5]);
                                        if (!string.IsNullOrEmpty(strGross))
                                        {
                                            gross = Convert.ToDecimal(strGross);
                                        }

                                        string strctc = Convert.ToString(row[4]);
                                        if (!string.IsNullOrEmpty(strctc))
                                        {
                                            ctc = Convert.ToDecimal(strctc);
                                        }

                                        if (!string.IsNullOrEmpty(Convert.ToString(gross)))
                                        {
                                            ctc = null;
                                        }

                                        int? Exist = obj.EmpSalaryExistsValidate(employee_id, salaryDate, null, null);
                                        if (Exist == 0)
                                        {
                                            int? r1 = obj.EmpSalaryCreate(employee_id, "M", salaryDate, null, gross, ctc, null, null, null, null, null, null, null, null, null, null, null, null);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    html = ex.ToString();
                                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        int[] HideColumn = new[] { 0, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 17 };
                        dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        htmlTable = CommonUtil.htmlTable(dtEmpSalary, HideColumn);

                        Session["snAttendanceEntry"] = null;
                        Session["snEmployeeMain"] = null;
                        Session["snEmployeeSalary"] = null;

                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


                    }
                    else
                    {
                        html = "Column Does Not Matched !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }

                }
                html = "No Records !!";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }


            //------------------Employee Details---------
            if (model.ExcelDropDown == "Employee Detail")
            {
                if (Session["snEmployee"] == null)
                {
                    html = "File does not exist !";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = (DataTable)Session["snEmployee"];

                if (dt.Rows.Count > 0)
                {
                    DataTable dtEmployee = obj.EmployeeDownloadDataGet();
                    bool result = ValidateDataTableColumns(dt, dtEmployee);

                    if (result)
                    {
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    int? employee_id = obj.EmployeeIdExitsValidate(null, Convert.ToString(row["emp_code"]));
                                    if (employee_id == 0 || employee_id == null)
                                    {
                                        //string ColValue1 = null;
                                        //string ColValue2 = null;

                                        string name = Convert.ToString(row[1]);
                                        string firstname = Convert.ToString(row[2]);
                                        string lastname = Convert.ToString(row[3]);
                                        string sex = Convert.ToString(row[4]);

                                        string dob = Convert.ToString(row[5]);
                                        if (!(string.IsNullOrEmpty(dob)))
                                        {
                                            DateTime datedob = Convert.ToDateTime(row[5]);
                                            dob = datedob.ToShortDateString();
                                        }
                                        else
                                        {
                                            dob = null;
                                        }

                                        string doj = Convert.ToString(row[6]);
                                        if (!(string.IsNullOrEmpty(doj)))
                                        {
                                            DateTime datedoj = Convert.ToDateTime(row[6]);
                                            doj = datedoj.ToShortDateString();
                                        }
                                        else
                                        {
                                            doj = null;
                                        }



                                        string fhname = Convert.ToString(row[7]);
                                        string mothername = Convert.ToString(row[8]);
                                        string pfno = Convert.ToString(row[9]);
                                        string esino = Convert.ToString(row[10]);




                                        //ColValue1 = Convert.ToString(row[5]);
                                        //ColValue2 = Convert.ToString(row[6]);

                                        if ((!(String.IsNullOrWhiteSpace(dob)) && !(String.IsNullOrWhiteSpace(doj))) && IsStringWithSpace(name) && IsStringValue(firstname) && IsStringValue(lastname) && (sex.Length == 1) && IsCharValue(sex) && IsDate(dob) && IsDate(doj) && IsStringWithBlank(fhname) && IsStringWithBlank(mothername) && IsAlphaNumeric(pfno) && IsAlphaNumeric(esino))
                                        {
                                            if (CompareDate(Convert.ToDateTime(row[5]), Convert.ToDateTime(row[6])))
                                            {
                                                int? i = obj.EmployeeCreate(Convert.ToString(row[0]), Convert.ToString(row[1]), Convert.ToString(row[2]),
                                                Convert.ToString(row[3]), Convert.ToString(row[4]), Convert.ToDateTime(row[5]),
                                                Convert.ToDateTime(row[6]), null, null, null, null, null,
                                                null, null, null, null, null, null, null, null, null, null, false, null, null, null, null, null, null, null, null, null, null,
                                                null, null, Convert.ToInt32(model.EmpTypeId), null, null, null, null, null, Convert.ToString(row[7]), Convert.ToString(row[8]),
                                                null, null, null, null, null, null, null, null, null, null, null,
                                                null, null, null, null, null, null, null, null, null, null, null, Convert.ToInt32(model.CompanyId),
                                                Convert.ToString(row[9]), Convert.ToString(row[10]), null, null, null, null, null, null, null,null,null);
                                            }

                                        }
                                    }
                                }

                                DataTable dtEmployeeRecord = obj.EmployeeGet(null);
                                if (dtEmployeeRecord.Rows.Count > 0)
                                {
                                    htmlTable = new StringBuilder();
                                    htmlTable = CommonUtil.htmlTableAll(dtEmployeeRecord);

                                    Session["snAttendanceEntry"] = null;
                                    Session["snEmployeeMain"] = null;
                                    Session["snEmployeeSalary"] = null;
                                    htmlTable.Append("<div class='alert alert-success'>Employee Inserted Successfully!!</div>");
                                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    html = "No Records !!";
                                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            html = ex.ToString();
                            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        html = "Column Does Not Matched !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }

                }
                html = "No Records !!";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
            html = "No Records !!";
            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
        }

        Regex regex = new Regex(@"^[0-9]+$");
        private bool IsInteger(string str)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool CompareDate(DateTime dob, DateTime doj)
        {
            try
            {
                if (dob > doj)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsDate(string str)
        {
            Regex ddmmyyyy = new Regex(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$");
            try
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!ddmmyyyy.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsStringValue(string str)
        {
            Regex regex = new Regex(@"^\w+$");
            try
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsStringWithSpace(string str)
        {
            Regex regex = new Regex("^[a-zA-Z ]*$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool IsStringWithBlank(string str)
        {
            Regex regex = new Regex("^[a-zA-Z ]*$");
            try
            {
                if (String.IsNullOrEmpty(str))
                {
                    return true;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsAlphaNumeric(string str)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            try
            {
                if (String.IsNullOrEmpty(str))
                {
                    return true;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsCharValue(string str)
        {
            Regex regex = new Regex(@"^[mMfF]$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private bool IsDecimal(string str)
        {
            //Regex regex = new Regex(@"^[0-9]+$");
            Regex regex = new Regex(@"^[0-9]\d{0,9}(\.\d{1,3})?%?$");
            try
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }





        private bool ValidateDataTableColumns(DataTable dt1, DataTable dt2)
        {
            try
            {
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataColumn dc in dt1.Columns)
                    {
                        string cname = dc.ColumnName;
                        if (!(dt2.Columns.Contains(cname)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ActionResult MailSetting()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                string msg = "msg";

                MailSetupViewModel model = new MailSetupViewModel();

                //bool b = SendMailTab("dksaai", "192.139.147.104", "Ganges Test", "sujeet@maruticomputers.com", "poise123#", "25", "true", "true", "High",
                //    "durgesh@maruticomputers.com", "", "", "Testing Ganges Mail", "Testing Ganges Mail Body", "", ref msg, ref msg);


                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        //public static bool SendMailTab(string EmailAccount, string SmtpServer, string SenderName, string SenderEmail, string Password, string PortAddrs, string CopyMailinServer, string SentItemsFolderNameinServer, string EmailPrior, string ToAddress, string EmailCCIds, string EmailBCCIds, string Subject, string Body, string FileAttachment, ref string ErrorMessage, ref string MessageID)
        //{
        //    try
        //    {
        //        GangesEmail.GangesEmail objMail = new GangesEmail.GangesEmail();
        //        GangesEmail.MailServerSettings MailSettings = new GangesEmail.MailServerSettings();
        //        GangesEmail.EmailBodySettings MailBodySettings = new GangesEmail.EmailBodySettings();
        //        MailSettings.smtp_server = SmtpServer;  //"10.2.0.80";
        //        MailSettings.smtp_email_account = EmailAccount;
        //        MailSettings.email_sender_name = SenderName;
        //        MailSettings.sender_email = SenderEmail;

        //        MailSettings.password = Password;
        //        MailSettings.imap_port = int.Parse(PortAddrs);
        //        MailSettings.copy_mail_in_server_sent_items = bool.Parse(CopyMailinServer);
        //        MailSettings.sent_items_folder_name_in_server = SentItemsFolderNameinServer;
        //        MailSettings.email_priority = EmailPrior;

        //        MailBodySettings.BodyMessage = Body;
        //        MailBodySettings.ImportBodyFromFile = false;
        //        MailBodySettings.BodyType = Quiksoft.EasyMail.SMTP.BodyPartFormat.Plain;
        //        MailBodySettings.CharsetEncoding = System.Text.Encoding.UTF8;

        //        return objMail.SendEmail(MailSettings, ToAddress, EmailCCIds, EmailBCCIds, Subject, MailBodySettings, FileAttachment, false, ref ErrorMessage, ref MessageID);
        //    }
        //    catch (Exception mailex)
        //    {
        //        throw mailex;
        //    }
        //}











        [HttpPost]
        public ActionResult MailSettingGet(MailSetupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = { 0, 6, 8, 9, 11, 12 };

                DataTable dtMailSettingGet = obj.MailSetupGet(null, null, null, null);
                if (dtMailSettingGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtMailSettingGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult MailSettingCreate(MailSetupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.MailSetupExistsValidate(model.CompanyId, model.LocationId, model.IsCommonMail);
                if (Exist == 0)
                {
                    int? i = obj.MailSetupCreate(model.CompanyId, model.LocationId, model.IsActive, model.SmtpServer, model.SenderEmail, model.SenderPassword, model.PortAddress, model.Subject, model.BodyContent, model.UseSsl, model.IsCommonMail, model.ToMailId, model.CcMailId);
                    int[] columnHide = { 0, 6, 8, 9, 11, 12 };

                    DataTable dtMailSettingGet = obj.MailSetupGet(null, null, null, null);
                    if (dtMailSettingGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtMailSettingGet, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "Already Exist !!";
                    return Json(new { Flag = 2, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult MailSettingTestMail(MailSetupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;

                string MailSettings = System.Configuration.ConfigurationManager.AppSettings["MailSettings"];

                string SmtpServer = null;
                string SenderEmail = null;
                string Password = null;
                string PortAddress = null;
                string UseSSL = null;
                string Priority = null;


                StringBuilder sb = new StringBuilder(null);

                DataTable dtMailSettings = obj.MailSetupTestMailGet(model.MailSetupId);
                if (dtMailSettings.Rows.Count > 0)
                {
                    SmtpServer = Convert.ToString(dtMailSettings.Rows[0][4]);
                    SenderEmail = Convert.ToString(dtMailSettings.Rows[0][5]);
                    Password = Convert.ToString(dtMailSettings.Rows[0][6]);
                    PortAddress = Convert.ToString(dtMailSettings.Rows[0][7]);
                    UseSSL = Convert.ToString(dtMailSettings.Rows[0][10]);

                    if (string.IsNullOrEmpty(SmtpServer) && string.IsNullOrEmpty(SenderEmail) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(PortAddress) && string.IsNullOrEmpty(UseSSL))
                    {
                        return Json(new { Flag = 1, Html = "Check Mail Setting !!" }, JsonRequestBehavior.AllowGet);
                    }

                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("Mail Testing Successfully  - " + SenderEmail);
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("<br />");
                    sb.Append("Regards,");
                    sb.Append("<br />");
                    sb.Append("HR Department");

                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress(SenderEmail);
                    mail.To.Add(SenderEmail);
                   
                    mail.Subject = "Test Mail";
                    mail.Body = Convert.ToString(sb);
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    SmtpClient smtp = new SmtpClient();
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Host = SmtpServer;
                    if (UseSSL.ToUpper() == "TRUE")
                    {
                        smtp.EnableSsl = true;
                    }
                    else
                    {
                        smtp.EnableSsl = false;
                    }

                    smtp.Credentials = new System.Net.NetworkCredential(SenderEmail, Password);
                    smtp.Port = Convert.ToInt32(PortAddress);
                    smtp.UseDefaultCredentials = false;
                    smtp.Send(mail);

                    return Json(new { Flag = 0, Html = " Test Mail Successfully !! " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = " No Record Exist !! " }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Flag = 1, Html = "Error in Test Mail" }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult MailSettingUpdate(MailSetupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                //int? Exist = obj.MailSetupExistsValidate(model.CompanyId, model.LocationId, model.IsCommonMail);
                //if (Exist == 0)
                //{
                int? i = obj.MailSetupUpdate(model.MailSetupId, model.CompanyId, model.LocationId, model.IsActive, model.SmtpServer, model.SenderEmail, model.SenderPassword, model.PortAddress, model.Subject, model.BodyContent, model.UseSsl, model.IsCommonMail, model.ToMailId, model.CcMailId);

                int[] columnHide = { 0, 6, 8, 9, 11, 12 };

                DataTable dtMailSettingGet = obj.MailSetupGet(null, null, null, null);
                if (dtMailSettingGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtMailSettingGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }
                //}
                //else
                //{
                //    html = "Already Exist !!";
                //    return Json(new { Flag = 2, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult MailSettingDelete(MailSetupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? i = obj.MailSetupDelete(model.MailSetupId);

                int[] columnHide = { 0, 6, 8, 9, 11, 12 };

                DataTable dtMailSettingGet = obj.MailSetupGet(null, null, null, null);
                if (dtMailSettingGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtMailSettingGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult EmployeeTest()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmployeeParialViewModel model = new EmployeeParialViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        public ActionResult AttendanceEnterPartial()
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            return PartialView("_AttendanceEnter", model);
        }



        public ActionResult AttendanceApprovalPartial()
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            return PartialView("_AttendanceApproval", model);

        }

        public ActionResult PayrollProcessPartial()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            return PartialView("_PayrollProcess", model);
        }
    }

}
