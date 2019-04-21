using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MCLSystem;
using Newtonsoft.Json;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PoiseERP.Areas.Payroll.Controllers
{
    [ValidateInput(false)]
    public class EmployeeController : Controller
    {
        //
        // GET: /Payroll/Employee/
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        [HttpPost]
        public ActionResult EmployeeSalaryItemAmountGets(int? EmpSalaryId, int? PayrollItemId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? flag = obj.PayrollItemComputingtypeGet(PayrollItemId);
                if (flag == 1)
                {
                    decimal? amount = obj.EmpSalaryItemAmountGet(EmpSalaryId, PayrollItemId);
                    return Json(new { Flag = 0, Html = amount }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeSalaryItemGets(int? EmpSalaryId)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 5, 8, 9, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(EmpSalaryId);
                if (dtEmpSalaryItem.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                    DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(EmpSalaryId, null, null, null, null, null, null, null);
                    htmlTable.Append("<div>");
                    int[] colHide = new[] { 6, 9 };
                    htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                    htmlTable.Append("</div>");
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
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
        public ActionResult EmployeeSalaryGets(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                int[] showcolumn = new[] { 2, 3, 5, 6, 10, 16, 17 };
                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeSalaryHistoryGet(string History)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                DataTable dtEmpSalary = new DataTable();

                int[] showcolumn = new[] { 2, 3, 5, 6, 10, 16, 17 };
                if (History == "True" || History == "true")
                {
                    int[] columHide = new[] { 1, 4, 9, 10 };
                    //  dtEmpSalary = obj.EmpSalaryGet(null, null, null, "History");
                    DataSet ds = ObjML.EmpsalarygetHistory(null, null, null);
                    dtEmpSalary = ds.Tables[0];
                    Session["dtEmpSalary"] = ds.Tables[0];
                    if (dtEmpSalary.Rows.Count > 0)
                    {
                        //  StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanelHistory(columHide, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                    if (dtEmpSalary.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        //      StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanelHistory(showcolumn, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult PayrollItemComputingTypeGets(int? PayrollItemId, int? EmpSalaryId)
        {
            string html = null;
            string flag = null;
            string PayStartDate = null;
            string PayEndDate = null;

            string SalaryStartDate = null;
            string SalaryEndDate = null;
            string PayrollStartDate = null;
            string PayrollEndDate = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                // int? flag = obj.PayrollItemComputingtypeGet(PayrollItemId);
                DataTable dt = obj.PayrollItemComputingtypeDatesGet(PayrollItemId, EmpSalaryId);

                if (dt.Rows.Count > 0)
                {
                    flag = Convert.ToString(dt.Rows[0][0]);
                    PayStartDate = Convert.ToString(dt.Rows[0][1]);
                    PayEndDate = Convert.ToString(dt.Rows[0][2]);
                    SalaryStartDate = Convert.ToString(dt.Rows[0][3]);
                    SalaryEndDate = Convert.ToString(dt.Rows[0][4]);


                }

                if (flag == "1")
                {

                    return Json(new { Flag = 1, Html = "", PayStartDate = PayStartDate, PayEndDate = PayEndDate, SalaryStartDate = SalaryStartDate, SalaryEndDate = SalaryEndDate }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 0, Html = "Payroll Item is Static Item !!", PayStartDate = PayStartDate, PayEndDate = PayEndDate, SalaryStartDate = SalaryStartDate, SalaryEndDate = SalaryEndDate }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EmployeeCenter()
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


        [HttpPost]
        public ActionResult EmployeeCenterGet(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = { 0, 1 };
                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);

                StringBuilder htmlTable = CommonUtil.htmlTableEmployeeCenter(dtEmployeeGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ImportExcelFileToDB(ImportExportExcelViewModel model)
        {

            StringBuilder htmlTable = new StringBuilder();
            int? totalDay = null;
            int[] HideColumn = new[] { 0, 7, 8, 9, 12, 15, 16 };
            string html = null;
            htmlTable.Append("<div class='alert alert-success'>Excel Import  Successfully !!</div>");
            return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);



        }



        public ActionResult EmployeePartial(int EmployeeId)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            int? cid = null;
            Session["EmpCenter_Employee__Id"] = EmployeeId;
            EmployeeViewModel model = new EmployeeViewModel(cid);
            return PartialView("Employee_Partial", model);


        }

        public ActionResult DepartmentPartial(int EmployeeId)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            Session["EmpCenter_Employee__Id"] = EmployeeId;
            EmpProjectViewModel model = new EmpProjectViewModel();
            return PartialView("EmployeeDepartment_Partial", model);
        }


        [HttpPost]
        public ActionResult EmployeeCenterEmployeeDepartmentGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                int? EmployeeId = Convert.ToInt32(Session["EmpCenter_Employee__Id"]);
                DataTable dt = obj.EmpProjectGet(null, EmployeeId);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmployeeSalaryPartial(int EmployeeId)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            Session["EmpCenter_Employee__Id"] = EmployeeId;
            EmpSalaryViewModel model = new EmpSalaryViewModel();
            model.EmployeeId = EmployeeId;
            return PartialView("_EmployeeSalary", model);
        }

        public ActionResult EmployeeGroupSalaryPartial(int EmployeeId)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            Session["EmpCenter_Employee__Id"] = EmployeeId;
            EmpSalaryViewModel model = new EmpSalaryViewModel();
            model.EmployeeId = EmployeeId;
            return PartialView("_EmployeeGroupSalary", model);
        }


        [HttpPost]
        public ActionResult EmployeeCenterEmployeeSalaryGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };

                int? EmployeeId = Convert.ToInt32(Session["EmpCenter_Employee__Id"]);
                DataTable dtEmpSalary = obj.EmpSalaryGet(EmployeeId, null, null, null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AttendanceEnterPartial(int EmployeeId)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            model.EmployeeId = EmployeeId;
            return PartialView("_AttendanceEnter", model);
        }




        public ActionResult AttendanceApprovalPartial(int EmployeeId)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            model.EmployeeId = EmployeeId;
            return PartialView("_AttendanceApproval", model);

        }

        public ActionResult PayrollProcessPartial(int EmployeeId)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            model.EmployeeId = EmployeeId;
            return PartialView("_PayrollProcess", model);
        }


        public ActionResult PayrollDetailPartial(int EmployeeId)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
            model.EmployeeId = EmployeeId;
            return PartialView("_PayrollDetail", model);
        }







        public ActionResult Employee()
        {
            try
            {
                int? cid = null;
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (Session["company_id"] == null)
                {
                    cid = 0;
                }
                else
                {
                    cid = Convert.ToInt32(Session["company_id"]);
                }

                EmployeeViewModel model = new EmployeeViewModel(cid);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult CompanyEmployeeCodeGet(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                string empcode = obj.CompanyCodeGet(model.hfCompany_id);

                if (!(string.IsNullOrEmpty(empcode)))
                    return Json(new { Flag = 0, Html = empcode }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { Flag = 1, Html = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeTotalGet(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //----------rohtak chain company---------

                //  int[] columnHide = { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80 };


                // DataTable dtEmployeeGet = obj.EmployeeGet(null);

                int[] columnHide = { 0 };
                DataTable leftEmployee = obj.EmployeeLeftDetailsGet(null, null, null, null, null, null, null, null);
                DataTable CueentEmployee = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                leftEmployee.Merge(CueentEmployee);

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(leftEmployee, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeGet(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //----------rohtak chain company---------

                //  int[] columnHide = { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80 };


                //DataTable dtEmployeeGet = obj.EmployeeGet(null);

                int[] columnHide = { 0 };

                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult EmployeeCenterEmployeeListGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataTable dtEmployeeList = obj.EmployeeInfoGet();
                StringBuilder htmlTable = CommonUtil.htmlTableECEmployeeDetail(dtEmployeeList);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeCenterEmployeeGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Emp_id = Convert.ToInt32(Session["EmpCenter_Employee__Id"]);
                int[] columnHide = { 0 };
                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(Emp_id, null, null, null, null, null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //------------Employer salary details----------- 


        [HttpGet]
        public ActionResult EmployerSalaryDetails()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");

                }

                EmployerSalaryDetailViewModel model = new EmployerSalaryDetailViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        public ActionResult EmployerSalaryDetailGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 8 };
                DataTable dtEmployerSalaryDetailGet = obj.EmployerSalaryDetailGet(null);
                if (dtEmployerSalaryDetailGet.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployerSalaryDetailGet, columnHide);
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
        public ActionResult EmployerSalaryDetailCreate(EmployerSalaryDetailViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmployerSalaryDetailCreate(model.LocationId, model.CompanyId, model.PayType, model.StartDate, model.EndDate, model.Amount, model.MonthId, model.YearId, model.desciption);
                int[] ColumnHide = new[] { 0, 1, 2, 8 };
                DataTable dt = obj.EmployerSalaryDetailGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost]
        public ActionResult EmployerSalaryDetailUpdate(EmployerSalaryDetailViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmployerSalaryDetailUpdate(model.EmployersalaryDetailid, model.LocationId, model.CompanyId, model.PayType, model.StartDate, model.EndDate, model.Amount, model.MonthId, model.YearId, model.desciption);   //DocumentObjectUpdate(model.DocumentObjectId, model.DocumentId, model.DocumentObjectName, contenttype, uploadFile, fileName);
                int[] ColumnHide = new[] { 0, 1, 2, 8 };
                DataTable dt = obj.EmployerSalaryDetailGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        public ActionResult EmployerSaalaryDetailDelete(EmployerSalaryDetailViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmployerSalaryDetailDelete(model.EmployersalaryDetailid);
                int[] ColumnHide = new[] { 0, 1, 2, 8 };
                DataTable dt = obj.EmployerSalaryDetailGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        //-----------------Employer salary details END------------------------------

        [HttpPost]
        public ActionResult EmployeeInfoGet(int? employeeId)
        {
            string[] htmllist;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 94, 96, 102, 103, 104, 105, 106 };

                // ListEmpInfo
                DataTable dtEmployeeGet = obj.EmployeeGet(employeeId);

                Session["EmployeeId"] = dtEmployeeGet.Columns["employee_id"];
                if (dtEmployeeGet.Rows.Count > 0)
                {
                    for (int j = 0; j < dtEmployeeGet.Rows.Count; j++)
                    {
                        ListEmpShift.Add(new CollectionListData
                        {
                            Text = dtEmployeeGet.Rows[j][7].ToString(),
                            Value = dtEmployeeGet.Rows[j][8].ToString()
                        });

                    }
                    StringBuilder htmlTable = CommonUtil.htmlTableWithoutEditMode(dtEmployeeGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString(), htmllistsuccess = ListEmpShift }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableWithoutEditMode(dtEmployeeGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html, htmllist = ListEmpInfo }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Employee(EmployeeViewModel model, string Command)
        {
            string html = string.Empty;

            if (Command == "DownloadExcelFormat")
            {
                try
                {


                    string filePaths1 = Request.PhysicalApplicationPath + "Report\\Employee\\AgainEmployee.xlsx";

                    string extension = new FileInfo(filePaths1).Extension;

                    if (extension != null || extension != string.Empty)
                    {
                        switch (extension)
                        {
                            case ".xls":
                                return File(filePaths1, "application/vnd.ms-excel");
                            case ".xlsx":
                                return File(filePaths1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        }
                    }
                }
                catch (Exception ex)
                {

                    html = ex.Message.ToString();
                    return Json(html, JsonRequestBehavior.AllowGet);
                }


            }
            ActionResult a = null;
            DataTable dtEmployee = obj.EmployeeGet(null);
            int[] Column = new[] { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80 };
            DataTable dt = CommonUtil.DataTableColumnRemove(dtEmployee, Column);

            GridView gv = GridViewGet(dt, "Employee Report");

            if (Command == "Pdf")
            {
                a = DataExportPDF(gv, "Employee");
            }
            else if (Command == "Excel")
            {
                a = DataExportExcel(gv, "Employee", 12);
            }
            else if (Command == "Word")
            {
                a = DataExportWord(gv, "Employee");
            }



            return View(model);
        }
        public GridView GridViewCreate(DataTable dt, string ReportHeader)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 4;
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            DateTimeFormatInfo dinfo1 = new DateTimeFormatInfo();
            int colsan = dt.Columns.Count;
            HeaderCell2.ColumnSpan = colsan;
            HeaderRow.Cells.Add(HeaderCell2);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            // GridView1.Controls[0].Controls.AddAt(0, HeaderRow);
            return GridView1;
        }
        public GridView GridViewGet(DataTable dt, string ReportHeader)
        {


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 4;
            //GridView1.GridLines = GridLines.Both;
            //GridView1.BorderColor = bor

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
            GridView1.Font.Size = 4;
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
            GridView1.Font.Size = 9;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".doc");

            GridView1.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult DataExportExcel(GridView GridView1, string FileName, int FontSize)
        {
            GridView1.Font.Size = FontSize;
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

        [HttpPost]
        public ActionResult EmployeeExportGet(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80 };

                DataTable dtEmployeeGet = obj.EmployeeGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableExport(dtEmployeeGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeLeftGet(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //----------rohtak chain company---------
                //int[] columnHide = { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80 };

                //DataTable dtEmployeeGet = obj.EmployeeLeftGet();

                int[] columnHide = { 0 };
                DataTable dtEmployeeGet = obj.EmployeeLeftDetailsGet(null, null, null, null, null, null, null, null);

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);

                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeCreateNew(EmployeeViewModel model,string EducationInfo,string familyInfo,string Empoymentinfo)
        {
            int? employeeid;
            string html = null;
            string id = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                string FileName = Request.PhysicalApplicationPath + "bin\\Poise.dll";                
                int? noOfEmployeesLicensed = 1000;
                int? noOfEmployee = obj.EmployeeExistsValidate(null, null);
                if (noOfEmployee > noOfEmployeesLicensed)
                {
                    return Json(new { Flag = 2, Html = "No of Employees Exceed the License!" }, JsonRequestBehavior.AllowGet);
                }

                int? isExist = obj.EmployeeExistsValidate(model.EmpCode, null);
                if (isExist == 0)
                {
                    DataTable dtEmployeeGetOld = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                    string flag = model.EmployeeCreate(model);
                    int[] columnHide = { };
                    DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);

                    var diffName = dtEmployeeGet.AsEnumerable().Select(r => r.Field<int>("employee_id")).Except(dtEmployeeGetOld.AsEnumerable().Select(r => r.Field<int>("employee_id")));
                    if (diffName.Any())
                    {
                        DataTable _result = (from row in dtEmployeeGet.AsEnumerable()
                                             join name in diffName
                                             on row.Field<int>("employee_id") equals name
                                             select row).CopyToDataTable();
                        model.EmployeeId = Convert.ToInt32(_result.Rows[0][0]);
                        employeeid = model.EmployeeId;
                        int? status = obj.EmpProjectCreate(model.EmployeeId, model.ProjectId, model.DepartmentId, model.DesignationId, model.LocationId, model.Doj, model.Dol);
                        int flags = CommonUtil.CompareDate(model.Doj, model.EmployeeId);
                        if (flags == 2)
                        {
                            return Json(new { Flag = 1, Html = "Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                        }
                        else if (flags == 1)
                        {
                            return Json(new { Flag = 1, Html = "Start Date Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                        }


                        int? Exist = obj.EmpShiftExistsValidate(model.EmployeeId, model.ShiftId, model.Doj, model.Dol, null);
                        if (Exist == 0)
                        {
                            int? statuss = obj.EmpShiftCreate(model.EmployeeId, model.ShiftId, model.Doj, model.Dol, model.Notes, null);
                        }
                        int? Exists = obj.EmployeeBankDetailsExistsValidate(null, model.EmployeeId, model.EmpBankId, model.Doj, model.Dol);
                        if (Exists == 0)
                        {
                            if (model.EmpBankId != null)
                            {
                                int? statuss = obj.EmployeeBankDetailsCreate(model.EmployeeId, model.EmpBankId, model.BankAccountNo, null, model.Doj, model.Dol);
                            }
                        }



                    }
                    else {
                        flag = "4";
                    }
                    return Json(new { Flag = flag, Html = htmlTable.ToString(),id= model.EmployeeId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeEducationCreateBulk(EmpEducationViewModel model, string EducationInfo, string familyInfo, string eMPLOYEMENTINFO)
        {
            List<EmpEducationViewModel> models = new JavaScriptSerializer().Deserialize<List<EmpEducationViewModel>>(EducationInfo);
            List<EmpFamilyDetailsViewModel> modelsS = new JavaScriptSerializer().Deserialize<List<EmpFamilyDetailsViewModel>>(familyInfo);
            List<EmpPreviousEmployerDetailsViewModel> modelsEmployment = new JavaScriptSerializer().Deserialize<List<EmpPreviousEmployerDetailsViewModel>>(eMPLOYEMENTINFO);

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                    for (int i = 0; i < models.Count; i++)
                    {
                        if (models[i].Education != "" || models[i].UniversityName != "" || models[i].UniversityAddress != "")
                        {
                            int? EmpEducation = obj.EmpEducationCreate(models[i].EmployeeId, models[i].Education, models[i].UniversityName, models[i].UniversityAddress, models[i].EducationMonth, models[i].EducationYearE, models[i].EducationGrade, models[i].EmpDocumentId);

                        }

                    }
                for (int i = 0; i < modelsS.Count; i++)
                {
                    if (modelsS[i].FamilyRelationId != 0)
                    {
                        PoisePayrollManliftServiceModel objML = new PoisePayrollManliftServiceModel();
                        int? status = objML.EmpFamilyDetailsCreate(modelsS[i].EmployeeId, modelsS[i].FamilyMemberName, modelsS[i].FamilyMemberDob, modelsS[i].FamilyRelationId, modelsS[i].IsFamilyDependent, modelsS[i].FamilyMemberAge, modelsS[i].FamilyMembeAdhaarNo);

                    }

                }
                for (int i = 0; i < modelsEmployment.Count; i++)
                {
                    if (modelsEmployment[i].CompanyName != "")
                    {
                        int? statussss = obj.EmpPreviousEmployerDetailsCreate(modelsEmployment[i].EmployeeId, modelsEmployment[i].CompanyName, modelsEmployment[i].CompanyAddress, null,null,null,null,
                      modelsEmployment[i].Doj, modelsEmployment[i].Dol, modelsEmployment[i].ReasonOfLeaving, null,null,null);

                    }

                }
               
                return Json(new { Flag = 0 }, JsonRequestBehavior.AllowGet);
               
                
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeUpdateNew(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? isExist = obj.EmployeeExistsValidate(model.EmpCode, model.EmployeeId);
                if (string.IsNullOrEmpty(model.EmpName))
                {
                    model.EmpName = model.hfEmpName;

                }

                if (string.IsNullOrEmpty(model.EmpName) || model.EmpName == "0")
                {
                    return Json(new { Flag = 1, Html = "Employee Name  Is Required !" }, JsonRequestBehavior.AllowGet);
                }

                if (isExist == 0)
                {


                    int[] columnHide = { };
                    DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);

                    int flagDep = CommonUtil.CompareDate(model.Doj, model.EmployeeId);
                    if (flagDep == 2)
                    {
                        return Json(new { Flag = 1, Html = "Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (flagDep == 1)
                    {
                        return Json(new { Flag = 1, Html = "Start Date Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.DepartmentId != null)
                    {
                        DataTable dtDptStartDate = obj.DepartmentGet(null, model.DepartmentId);
                        string strDptStDate = Convert.ToString(dtDptStartDate.Rows[0]["Start Date"]);
                        if (!(string.IsNullOrEmpty(strDptStDate)))
                        {
                            if (Convert.ToDateTime(strDptStDate) > model.Doj)
                            {
                                return Json(new { Flag = 2, Html = "Start Date Should be Greater than Department Start Date " }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { Flag = 2, Html = "Department Start Date Can not be null" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.ProjectId != null)
                    {
                        DataTable dtDptStartDate = obj.ProjectGet(null, model.ProjectId);
                        string strDptStDate = Convert.ToString(dtDptStartDate.Rows[0]["Start Date"]);
                        if (!(string.IsNullOrEmpty(strDptStDate)))
                        {

                            if (Convert.ToDateTime(strDptStDate) > model.Doj)
                            {
                                return Json(new { Flag = 2, Html = "Start Date Should be Greater than Project Start Date " }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { Flag = 2, Html = "Project Start Date Can not be null" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DataTable dt = obj.EmpProjectGet(null, null);
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {

                        if (Convert.ToInt32(dt.Rows[d][1]) == model.EmployeeId)
                        {
                            model.EmpProjectId = Convert.ToInt32(dt.Rows[d][0]);
                            int? status = obj.EmpProjectUpdate(model.EmpProjectId, model.EmployeeId, model.ProjectId, model.DepartmentId, model.DesignationId, model.LocationId, model.Doj2, model.Dol);
                            break;
                        }

                    }
                    if (model.EmpBankId != null)
                    {
                        DataTable dts = obj.EmployeeBankDetailsGet(null, null, null);
                        for (int _bdetails = 0; _bdetails < dts.Rows.Count; _bdetails++)
                        {
                            if (Convert.ToInt32(dts.Rows[_bdetails][1]) == model.EmployeeId)
                            {
                                model.EmpbankDetailsId = Convert.ToInt32(dts.Rows[_bdetails][0]);
                                int? Exist = obj.EmployeeBankDetailsExistsValidate(model.EmpbankDetailsId, model.EmployeeId, model.EmpBankId, model.Doj, model.Dol);
                                if (Exist == 0)
                                {
                                    int? status = obj.EmployeeBankDetailsUpdate(model.EmpbankDetailsId, model.EmployeeId, model.EmpBankId, model.BankAccountNo, null, model.Doj, model.Dol);

                                }
                                break;
                            }
                        }
                        // int? status = obj.EmployeeBankDetailsUpdate(null, model.EmployeeId, model.EmpBankId, model.BankAccountNo, null, model.Doj, model.Dol);
                       
                    }
                    string flag = model.EmployeeUpdate(model);
                    return Json(new { Flag = flag, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { Flag = 1, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeCreate(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                string FileName = Request.PhysicalApplicationPath + "bin\\Poise.dll";
                int? noOfEmployeesLicensed = CommonUtil.noofemployeecheck(FileName);
                int? noOfEmployee = obj.EmployeeExistsValidate(null, null);
                if (noOfEmployee > noOfEmployeesLicensed)
                {
                    return Json(new { Flag = 2, Html = "No of Employees Exceed the License!" }, JsonRequestBehavior.AllowGet);
                }

                int? isExist = obj.EmployeeExistsValidate(model.EmpCode, null);
                if (isExist == 0)
                {
                    string flag = model.EmployeeCreate(model);
                    int[] columnHide = { };
                    DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);
                    return Json(new { Flag = flag, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        [HttpPost]
        public ActionResult downlaodexcel(EmployeeViewModel model, string commands)
        {
            string command = "DownloadExcelFormat";
            string msg = "0";
            string ConStr = "";
            string html = null;

            try
            {
                if (command == "DownloadExcelFormat")
                {
                    try
                    {


                        string filePaths1 = Request.PhysicalApplicationPath + "Report\\Employee\\ExcelFormat.xls";

                        string extension = new FileInfo(filePaths1).Extension;

                        if (extension != null || extension != string.Empty)
                        {
                            switch (extension)
                            {
                                case ".xls":
                                    return File(filePaths1, "application/vnd.ms-excel");
                                case ".xlsx":
                                    return File(filePaths1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        html = ex.Message.ToString();
                        return Json(new
                        {
                            Flag = 1,
                            Html = html
                        }, JsonRequestBehavior.AllowGet);
                    }
                }


                return Json(new { Flag = "0" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new
                {
                    Flag = 1,
                    Html = html
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult EmployeeSalaryExcelGet(EmpSalaryViewModel model)
        {
            StringBuilder htmlTable = new StringBuilder();
            DataTable dtresult = null;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string targetFolder = string.Empty;
                string targetPath = string.Empty;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    targetFolder = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "UploadFiles");
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    targetPath = Path.Combine(targetFolder, fileName);
                    file.SaveAs(targetPath);
                    dtresult = importExcelFile(targetPath);
                    System.IO.File.Delete(fileName);

                }

                // DataTable dtemplist1 = obj.EmployeeInfoGet();
                DataTable dtemplist = obj.EmployeeGet(null);
                List<EmpSalaryViewModel> Salarylist = new List<EmpSalaryViewModel>();

                if (dtresult.Rows.Count > 0)
                {
                    for (int j = 0; j < dtresult.Rows.Count; j++)
                    {
                        string IsCTC = "";
                        if (Convert.ToInt32(dtresult.Rows[j][2]) == 0)
                        {
                            IsCTC = "CTC";
                        }
                        else
                        {
                            IsCTC = "Gross";
                        }

                        for (int Cinfo = 0; Cinfo < dtemplist.Rows.Count; Cinfo++)
                        {
                            //emp code matching
                            if (dtresult.Rows[j][0].ToString().ToLower().Trim() == dtemplist.Rows[Cinfo][1].ToString().ToLower().Trim())
                            {
                                model.EmployeeId = Convert.ToInt32(dtemplist.Rows[Cinfo][0]);
                                model.Employee_Id = Convert.ToInt32(dtemplist.Rows[Cinfo][0]);
                                model.StartDt = Convert.ToDateTime(dtemplist.Rows[Cinfo][7]);
                                int? Exist = obj.EmpSalaryExistsValidate(model.EmployeeId, model.StartDt, model.EndDt, null);
                                if (Exist == 0)
                                {
                                    if (IsCTC == "Gross")
                                    {

                                        model.Gross = Convert.ToInt32(dtresult.Rows[j][2]);
                                        model.Ctc = null;
                                        model.IsPfApplicable = false;
                                        model.IsEsiApplicable = false;
                                        model.PayPeriodCycle = "M";
                                        int? status = obj.EmpSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);

                                        if (dtEmpSalary.Rows.Count > 0)
                                        {
                                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                                            break;
                                        }
                                        //return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        model.IsCTC = false;
                                        model.Ctc = Convert.ToInt32(dtresult.Rows[j][3]);
                                        model.Gross = null;
                                        model.IsPfApplicable = false;
                                        model.IsEsiApplicable = false;
                                        model.PayPeriodCycle = "M";
                                        int? status = obj.EmpSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                                        if (dtEmpSalary.Rows.Count > 0)
                                        {
                                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                                            break;
                                        }
                                    }
                                    // return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    //   flag = model.EmployeeCreateExcel(Salarylist);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new
                {
                    Flag = 1,
                    Html = html
                }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult EmployeeSalaryExcelForBreakups(EmpSalaryViewModel model)
        {
            DataTable dtresult = null;
            string html = null;
            string htmlList = null;
            string globalHTMl = string.Empty;
            int? Balance = null;
            int? add_deduct = null;
            List<string> List = new List<string>();
            var list = new List<Tuple<string, string>>();
            StringBuilder htmlTable;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string targetFolder = string.Empty;
                string targetPath = string.Empty;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    targetFolder = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "UploadFiles");
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    targetPath = Path.Combine(targetFolder, fileName);
                    file.SaveAs(targetPath);
                    dtresult = importExcelFile(targetPath);
                    System.IO.File.Delete(fileName);
                }

                for (int i = 0; i < dtresult.Rows.Count; i++)
                {

                    model.EmployeeId = obj.EmployeeIdGet(Convert.ToInt32(dtresult.Rows[i][0]));
                    int flag = CommonUtil.CompareDate(Convert.ToDateTime(dtresult.Rows[i][4]), model.EmployeeId);
                    if (flag == 2)
                    {
                        return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                    //else if (flag == 1)
                    //{
                    //    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                    //}
                    model.StartDtItem = Convert.ToDateTime(dtresult.Rows[i][4]);
                    //  model.EndDtItem = Convert.ToDateTime(dtresult.Rows[i][5]);
                    DataTable dt = obj.EmpSalaryDatesGet(Convert.ToInt32(dtresult.Rows[i][0]));
                    DateTime? startdate = Convert.ToDateTime(dt.Rows[0][0]);
                    DateTime? enddate = null;
                    string strEndDate = Convert.ToString(dt.Rows[0][1]);
                    if (!(string.IsNullOrEmpty(strEndDate)))
                    {
                        enddate = Convert.ToDateTime(dt.Rows[0][1]);
                    }

                    if (enddate == null)
                    {
                        if (!(model.StartDtItem >= startdate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if (!(model.StartDtItem >= startdate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                        }
                        if (!(model.StartDtItem <= enddate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Less or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.EndDtItem != null)
                        {
                            if (!(model.EndDtItem >= enddate))
                            {
                                return Json(new { Flag = 5, Html = "Salary Item End Date Should be Greater or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    DataTable dtSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Salary");
                    if (dtSalary.Rows.Count != 0)
                    {
                        string strSalaryDate = Convert.ToString(dtSalary.Rows[0][0]);
                        if (!string.IsNullOrEmpty(strSalaryDate))
                        {
                            return Json(new { Flag = 1, Html = "Salary Already Used In Payroll !" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    model.EmpSalaryId = Convert.ToInt32(dtresult.Rows[i][0]);
                    List<int> pitem = new List<int>();
                    //model.EndDtItem= Convert.ToDateTime(dtresult.Rows[i][5]);
                    model.EmployeeId = Convert.ToInt32(dtresult.Rows[i][1]);
                    int len = dtresult.Columns.Count;
                    for (int j = 0; j <= dtresult.Columns.Count; j++)
                    {
                        if (j > 7 && j != len)
                        {
                            if (dtresult.Columns[j].ColumnName != null)
                            {
                                string[] words = dtresult.Columns[j].ColumnName.Split('_');
                                pitem.Add(Convert.ToInt32(words[1]));
                                model.PayrollValueType = 1;
                                model.Payroll_ValueType = 1;
                                model.PayrollItemId = Convert.ToInt32(words[1]);
                                int? Exist = obj.EmpSalaryItemExistsValidate(model.EmpSalaryId, model.PayrollItemId, model.StartDtItem, model.EndDtItem);
                                if (Exist == 0)
                                {

                                    DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, model.PayrollItemId, null, null, null, null, "Insert");
                                    if (dtEmpSalaryStatus.Rows.Count > 0)
                                    {
                                        string StrAdd_deduct = Convert.ToString(dtEmpSalaryStatus.Rows[0]["item_type"]);
                                        if (!string.IsNullOrEmpty(StrAdd_deduct))
                                        {
                                            add_deduct = Convert.ToInt32(StrAdd_deduct);
                                        }
                                        if (model.Rate == null)
                                        {
                                            model.Rate = Convert.ToInt32(dtresult.Rows[i][j]) == 0 ? 0 : Convert.ToInt32(dtresult.Rows[i][j]);
                                            if (model.Rate == 0)
                                            {
                                                model.Rate = null;
                                                continue;
                                            }
                                        }
                                        if (add_deduct > 0)
                                        {
                                            Balance = Convert.ToInt32(dtEmpSalaryStatus.Rows[0]["Balance"]);
                                            if (Balance <= 0)
                                                return Json(new { Flag = 1, Html = "Already Actual Monthly Gross Equal To Payroll Item Amount" }, JsonRequestBehavior.AllowGet);
                                            if (model.Rate > Balance)
                                                return Json(new { Flag = 1, Html = "Payroll Item Amount Should not Exceed Monthly Gross" }, JsonRequestBehavior.AllowGet);
                                        }
                                    }

                                    int? status = obj.EmpSalaryItemCreate(model.EmpSalaryId, model.PayrollItemId, model.PayrollValueType,
                                    model.Rate, model.PayrollFunctionId, model.PayrollItemValue, model.StartDtItem, model.EndDtItem,
                                    model.PayStartDt, model.PayEndDt, model.NotesItem, model.EmployeeId, model.EmpSalaryGroupId, model.PayrollTypeId = 1,
                                    model.IsOverridable);

                                    model.Rate = null;
                                    int[] columnHide = new[] { 0, 1, 2, 5, 8, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                                    DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(model.EmpSalaryId);
                                    htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                                    dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, null, null, null, null, null, null);
                                    htmlTable.Append("<div>");

                                    int[] colHide = new[] { 6 };
                                    htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                                    htmlTable.Append("</div>");
                                    globalHTMl = htmlTable.ToString();
                                }
                                else
                                {
                                    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                                }
                                // model.PayrollItemId = Convert.ToInt32(words[1]);
                            }
                        }
                    }
                    // model.PayrollItemId = null;


                }
                return Json(new { Flag = 0, Html = globalHTMl.ToString(), htmlList = List }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        public ActionResult ImportvariableSalary(PayrollUtil model)
        {
            StringBuilder htmlTable = new StringBuilder();
            DataTable dtresult = null;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string targetFolder = string.Empty;
                string targetPath = string.Empty;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    targetFolder = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "UploadFiles");
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    targetPath = Path.Combine(targetFolder, fileName);
                    file.SaveAs(targetPath);
                    dtresult = importExcelFile(targetPath);
                    System.IO.File.Delete(fileName);

                }
                List<int> pitem = new List<int>();
                //model.EndDtItem= Convert.ToDateTime(dtresult.Rows[i][5]);

                if (dtresult.Rows.Count > 0)
                {
                    for (int j = 0; j < dtresult.Rows.Count; j++)
                    {
                        if (dtresult.Rows[j][0] != null)
                        {
                            model.EmployeeId = Convert.ToInt32(dtresult.Rows[j][0]);
                            model.MonthId = Convert.ToInt32(dtresult.Rows[j][3]) == 0 ? DateTime.Now.Month - 1 : Convert.ToInt32(dtresult.Rows[j][3]);
                            model.Year = Convert.ToInt32(dtresult.Rows[j][4]) == 0 ? DateTime.Now.Year : Convert.ToInt32(dtresult.Rows[j][4]);
                            int len = dtresult.Columns.Count;
                            for (int i = 0; i <= dtresult.Columns.Count; i++)
                            {
                                if (i > 4 && i != len)
                                {
                                    if (dtresult.Columns[i].ColumnName != null)
                                    {
                                        string[] words = dtresult.Columns[i].ColumnName.Split('_');
                                        pitem.Add(Convert.ToInt32(words[1]));
                                        model.PayrollItemId = Convert.ToInt32(words[1]);                                      
                                        model.ItemAmount = Convert.ToInt32(dtresult.Rows[j][i]) == 0 ? 0 : Convert.ToInt32(dtresult.Rows[j][i]);
                                        if (model.ItemAmount != 0)
                                        {
                                            int? status = obj.EmpPayrollItemAmountUpdate(model.EmployeeId, model.MonthId, model.Year, model.PayrollItemId, model.ItemAmount, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new
                {
                    Flag = 1,
                    Html = html
                }, JsonRequestBehavior.AllowGet);
            }


        }
        public List<SelectListItem> _successlist { get; set; }
        public List<SelectListItem> _errorlist { get; set; }
        public List<SelectListItem> _WarningList { get; set; }



        [HttpPost]
        public ActionResult EmployeeExcelGet(EmployeeViewModel model)
        {
            DataTable dtresult = null;
            string html = null;
            string[] htmllistsuccess;
            string[] htmllisterror;
            string[] htmllistwarning;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int? isExist = obj.EmployeeExistsValidate(model.EmpCode, null);
                // if (isExist == 0)
                // {
                string targetFolder = string.Empty;
                string targetPath = string.Empty;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    targetFolder = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "UploadFiles");
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    targetPath = Path.Combine(targetFolder, fileName);
                    file.SaveAs(targetPath);
                    dtresult = importExcelFile(targetPath);
                    System.IO.File.Delete(fileName);

                }
                string flag = string.Empty;
                DataTable dtShiftList = obj.ShiftGet();
                DataTable dtdepartment = obj.DepartmentGet(null, null);
                DataTable dtDesignationGet = obj.DesignationGet(null);
                DataTable dty = obj.EmpProjectGet(null, null);
                DataTable dtCompanyList = obj.CompanyGet(null);

                DataTable dtLocationList = obj.LocationGet();
                DataTable dtReportingOfficerList = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                DataTable dtEmployeeTypeList = obj.EmpTypeGet(null);
                DataTable dtBankList = obj.EmployeeBankGet();
                DataTable dtEmployeeCategory = obj.EmpCategoryGet();
                DataTable dtPayMethodList = obj.PaymentMethodGet();
                _successlist = new List<SelectListItem>();
                _errorlist = new List<SelectListItem>();
                _WarningList = new List<SelectListItem>();
                List<EmployeeViewModel> emplist = new List<EmployeeViewModel>();
                int? DepartmentId = null;
                int? CompanyId = 0;
                int? LocationId = null;
                int? MgrId = null;
                int? EmpCategoryId = null;
                int? EmpBankId = null;
                int? EmpTypeId = null;
                int? PayMethodId = null;
                int? DesignationId = null;
                int? tdata = null;
                int? ShiftId = null;
                if (dtresult.Rows.Count > 0)
                {
                    for (int j = 0; j < dtresult.Rows.Count; j++)
                    {
                        //  for (int dep = 0; dep < dtdepartment.Rows.Count; dep++)
                        // {
                        if (dtresult.Rows[j][16].ToString().ToUpper().Trim() != "")
                        {
                            bool exists = dtdepartment.Select().ToList().Exists(row => row["Department Name"].ToString().ToUpper().Trim() == dtresult.Rows[j][16].ToString().ToUpper().Trim());
                            if (exists == false)
                            {
                                model.Doj = Convert.ToDateTime(dtresult.Rows[j][5]);
                                model.Doj2 = Convert.ToDateTime(dtresult.Rows[j][5]);
                                int? status = obj.DepartmentCreate(dtresult.Rows[j][16].ToString(), null, model.Doj, null, model.Notes);
                            }

                        }
                        // }

                        //  for (int dep = 0; dep < dtDesignationGet.Rows.Count; dep++)
                        //  {
                        if (dtresult.Rows[j][17].ToString().ToLower().Trim() != "")
                        {
                            bool exists = dtDesignationGet.Select().ToList().Exists(row => row["Designation"].ToString().ToUpper().Trim() == dtresult.Rows[j][17].ToString().ToUpper().Trim());
                            if (exists == false)
                            {
                                int? status = obj.DesignationCreate(dtresult.Rows[j][17].ToString(), true, null, null, null, null, null, null);

                            }

                        }

                        // }
                        //Create into department

                        //if (Exist == 0)
                        //{
                        //    model.StartDt = model.StartDt2;
                        //    model.EndDt = model.EndDt2;

                        //    int? status = obj.DepartmentCreate(model.DepartmentName, model.ParentDepartmentId, model.StartDt, model.EndDt, model.Notes);
                        //    int[] columnHide = new[] { 0, 2, 7, 8, 9, 10, 11 };
                        //    DataTable dtDepartment = obj.DepartmentGet(null, null);

                        //    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
                        //    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                        //}

                        //create into designation

                        //int? Exist = obj.DesignationExistsValidate(model.DesignationDesc.Trim(), null);
                        //if (Exist == 0)
                        //{
                        //    int? status = obj.DesignationCreate(model.DesignationDesc, model.Inactive, model.ParentDesgId, model.IsLeaveApproval, model.IsSeeOtherAttendance, model.IsSeeEmpDetails, model.IsSeeEmpPayslip, model.PayScalId);

                        //    int[] columnHide = new[] { 0, 2, 3, 8, 10 };
                        //    DataTable dt = obj.DesignationGet(null);

                        //    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        //    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                        //}
                        //else
                        //{
                        //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                        //}


                        //case for Company

                        for (int Cinfo = 0; Cinfo < dtCompanyList.Rows.Count; Cinfo++)
                        {
                            if (dtresult.Rows[j][2].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][2].ToString().ToLower().Trim() == dtCompanyList.Rows[Cinfo][1].ToString().ToLower().Trim())
                                {
                                    CompanyId = Convert.ToInt32(dtCompanyList.Rows[Cinfo][0]);
                                    break;
                                }
                                else
                                {
                                    CompanyId = 0;
                                }
                            }
                            else
                            {
                                _errorlist.Add(new SelectListItem
                                {
                                    Selected = true,
                                    Text = "Error while insert " + dtresult.Rows[j][1].ToString() + "due to empty company name",
                                    Value = j.ToString()
                                });
                                CompanyId = 0;
                                break;

                            }

                        }
                        //case for Department

                        for (int dep = 0; dep < dtdepartment.Rows.Count; dep++)
                        {
                            if (dtresult.Rows[j][16].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][16].ToString().ToLower().Trim() == dtdepartment.Rows[dep][1].ToString().ToLower().Trim())
                                {
                                    DepartmentId = Convert.ToInt32(dtdepartment.Rows[dep][0]);
                                    break;
                                }
                                else
                                {
                                    DepartmentId = null;
                                    continue;
                                }

                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Department " + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                DepartmentId = Convert.ToInt32(tdata);
                            }

                        }


                        //Case for Designation

                        for (int des = 0; des < dtDesignationGet.Rows.Count; des++)
                        {
                            if (dtresult.Rows[j][17].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][17].ToString().ToLower().Trim() == dtDesignationGet.Rows[des][1].ToString().ToLower().Trim())
                                {
                                    DesignationId = Convert.ToInt32(dtDesignationGet.Rows[des][0]);
                                    break;
                                }
                                else { DesignationId = null; }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Designation for " + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                DesignationId = Convert.ToInt32(tdata);
                            }

                        }

                        // case for Location

                        for (int loc = 0; loc < dtLocationList.Rows.Count; loc++)
                        {
                            if (dtresult.Rows[j][24].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][24].ToString().ToLower().Trim() == dtLocationList.Rows[loc][1].ToString().ToLower().Trim())
                                {
                                    LocationId = Convert.ToInt32(dtLocationList.Rows[loc][0]);
                                    break;
                                }
                                else { LocationId = null; }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Location for " + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                DesignationId = Convert.ToInt32(tdata);
                            }

                        }

                        // Case forReport Officer

                        for (int roff = 0; roff < dtReportingOfficerList.Rows.Count; roff++)
                        {
                            if (dtresult.Rows[j][18].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][18].ToString().ToLower().Trim() == dtReportingOfficerList.Rows[roff][2].ToString().ToLower().Trim())
                                {
                                    MgrId = Convert.ToInt32(dtReportingOfficerList.Rows[roff][0]);
                                    break;
                                }
                                else { MgrId = null; }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Reporting Officer for" + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                MgrId = Convert.ToInt32(tdata);
                            }

                        }

                        // Case for Employee Type
                        for (int EmpType = 0; EmpType < dtEmployeeTypeList.Rows.Count; EmpType++)
                        {
                            if (dtresult.Rows[j][20].ToString().ToLower().Trim() != "")
                            {


                                if (dtresult.Rows[j][20].ToString().ToLower().Trim() == dtEmployeeTypeList.Rows[EmpType][1].ToString().ToLower().Trim())
                                {
                                    EmpTypeId = Convert.ToInt32(dtEmployeeTypeList.Rows[EmpType][0]);
                                    break;
                                }
                                else
                                {
                                    EmpTypeId = null;
                                }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Employee Type for "  + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                EmpTypeId = Convert.ToInt32(tdata);
                            }

                        }

                        //Case for BankList

                        for (int Blist = 0; Blist < dtBankList.Rows.Count; Blist++)
                        {
                            if (dtresult.Rows[j][22].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][22].ToString().ToLower().Trim() == dtBankList.Rows[Blist][1].ToString().ToLower().Trim())
                                {
                                    EmpBankId = Convert.ToInt32(dtBankList.Rows[Blist][0]);
                                    break;
                                }
                                else { EmpBankId = null; continue; }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Bank name for "  + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                EmpBankId = Convert.ToInt32(tdata);
                            }

                        }

                        // Case for EmployeeCategory

                        for (int Empcat = 0; Empcat < dtEmployeeCategory.Rows.Count; Empcat++)
                        {
                            if (dtresult.Rows[j][19].ToString().ToLower().Trim() != "")
                            {

                                if (dtresult.Rows[j][19].ToString().ToLower().Trim() == dtEmployeeCategory.Rows[Empcat][1].ToString().ToLower().Trim())
                                {
                                    EmpCategoryId = Convert.ToInt32(dtEmployeeCategory.Rows[Empcat][0]);
                                    break;
                                }
                                else { EmpCategoryId = null; }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Employee category for " + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                EmpCategoryId = null;
                                //EmpCategoryId = Convert.ToInt32("019801");
                            }

                        }

                        // case for PayMethod

                        for (int paym = 0; paym < dtPayMethodList.Rows.Count; paym++)

                        {
                            if (dtresult.Rows[j][21].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][21].ToString().ToLower().Trim() == dtPayMethodList.Rows[paym][1].ToString().ToLower().Trim())
                                {
                                    PayMethodId = Convert.ToInt32(dtPayMethodList.Rows[paym][0]);
                                    break;
                                }
                                else { PayMethodId = null; }
                            }
                            else
                            {
                                _WarningList.Add(new SelectListItem
                                {

                                    Text = "added empty Pay Method for " + dtresult.Rows[j][1].ToString(),
                                    Value = j.ToString()
                                });
                                // PayMethodId = null;

                                PayMethodId = Convert.ToInt32(tdata);

                            }

                        }
                        // case for shift
                        for (int shiftinfo = 0; shiftinfo < dtShiftList.Rows.Count; shiftinfo++)
                        {
                            if (dtresult.Rows[j][25].ToString().ToLower().Trim() != "")
                            {
                                if (dtresult.Rows[j][25].ToString().ToLower().Trim() == dtShiftList.Rows[shiftinfo][1].ToString().ToLower().Trim())
                                {
                                    ShiftId = Convert.ToInt32(dtShiftList.Rows[shiftinfo][0]);
                                    break;
                                }
                                else
                                {
                                    ShiftId = 0;
                                }
                            }
                            else
                            {
                                _errorlist.Add(new SelectListItem
                                {
                                    Selected = true,
                                    Text = "Error while insert " + dtresult.Rows[j][25].ToString() + "due to empty company name",
                                    Value = j.ToString()
                                });
                                ShiftId = 0;
                                break;

                            }

                        }
                        if (CompanyId == 0)
                        {
                            _errorlist.Add(new SelectListItem
                            {
                                Selected = true,
                                Text = "Company(" + dtresult.Rows[j][21].ToString() + "0" + " does not match master Company Name",
                                Value = j.ToString()
                            });

                        }
                        if (DepartmentId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {

                                Text = "Department(" + dtresult.Rows[j][21].ToString() + "(" + " does not match master Department",
                                Value = j.ToString()
                            });

                        }
                        if (DesignationId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {
                                Text = "Designation(" + dtresult.Rows[j][21].ToString() + ")" + " does not match master Designation",
                                Value = j.ToString()
                            });

                        }

                        if (LocationId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {

                                Text = "location(" + dtresult.Rows[j][21].ToString() + ")" + " does not match master Location",
                                Value = j.ToString()
                            });

                        }
                        if (MgrId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {

                                Text = "Reporting Officer(" + dtresult.Rows[j][21].ToString() + ")" + " does not match master Reporting officer",
                                Value = j.ToString()
                            });

                        }

                        if (EmpTypeId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {
                                Text = "Employee Type(" + dtresult.Rows[j][21].ToString() + ")" + " does not match master Employee Type",
                                Value = j.ToString()
                            });

                        }
                        if (EmpCategoryId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {

                                Text = "Employe category(" + dtresult.Rows[j][21].ToString() + ")" + " does not match master Employe Category",
                                Value = j.ToString()
                            });

                        }

                        if (PayMethodId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {

                                Text = "Pay Method(" + dtresult.Rows[j][21].ToString() + ")" + " does not match master Pay Method",
                                Value = j.ToString()
                            });

                        }
                        if (ShiftId == null)
                        {
                            _WarningList.Add(new SelectListItem
                            {

                                Text = "Shift(" + dtresult.Rows[j][25].ToString() + ")" + " does not match master Shifts",
                                Value = j.ToString()
                            });

                        }
                        // if (CompanyId != null && PayMethodId != null && EmpCategoryId!= null && EmpTypeId != null && MgrId != null && LocationId != null && DesignationId != null && DepartmentId!= null)
                        if (CompanyId != 0)
                        {
                            if (dtresult.Rows[j][7].ToString() != "" && dtresult.Rows[j][8].ToString() != "" && dtresult.Rows[j][3].ToString() != "")
                            {
                                emplist.Add(new EmployeeViewModel()
                                {
                                    EmpCode = dtresult.Rows[j][0].ToString() == "" || dtresult.Rows[j][0].ToString() == null ? "" : dtresult.Rows[j][0].ToString().ToUpper(),
                                    EmpName = dtresult.Rows[j][1].ToString() == "" || dtresult.Rows[j][1].ToString() == null ? "" : dtresult.Rows[j][1].ToString(),
                                    CompanyId = CompanyId,
                                    Sex = dtresult.Rows[j][3].ToString() == "" || dtresult.Rows[j][3].ToString() == null ? "" : dtresult.Rows[j][3].ToString().ToUpper(),
                                    Dob = CheckDBNull(dtresult.Rows[j][4]),
                                    Doj = CheckDBNull(dtresult.Rows[j][5]),
                                    EmailAddress = dtresult.Rows[j][6].ToString() == "" || dtresult.Rows[j][6].ToString() == null ? "" : dtresult.Rows[j][6].ToString(),
                                    FatherHusbandName = dtresult.Rows[j][7].ToString() == "" || dtresult.Rows[j][7].ToString() == null ? "" : dtresult.Rows[j][7].ToString(),
                                    Relationship = dtresult.Rows[j][8].ToString() == "" || dtresult.Rows[j][8].ToString() == null ? "F" : dtresult.Rows[j][8].ToString().ToUpper(),
                                    Address1 = dtresult.Rows[j][9].ToString() == "" || dtresult.Rows[j][9].ToString() == null ? "" : dtresult.Rows[j][9].ToString(),
                                    Address2 = dtresult.Rows[j][10].ToString() == "" || dtresult.Rows[j][10].ToString() == null ? "" : dtresult.Rows[j][10].ToString(),
                                    MobileNo = dtresult.Rows[j][11].ToString() == "" || dtresult.Rows[j][11].ToString() == null ? 1234567890 : Convert.ToInt32(dtresult.Rows[j][11]),
                                    UANNo = dtresult.Rows[j][12].ToString() == "" || dtresult.Rows[j][12].ToString() == null ? null : Convert.ToString(dtresult.Rows[j][12]),
                                    TdsAccountNo = dtresult.Rows[j][13].ToString() == "" || dtresult.Rows[j][13].ToString() == null ? null : dtresult.Rows[j][13].ToString(),
                                    EsiAccountNo = dtresult.Rows[j][14].ToString() == "" || dtresult.Rows[j][14].ToString() == null ? null : dtresult.Rows[j][14].ToString(),
                                    PfAccountNo = dtresult.Rows[j][15].ToString() == "" || dtresult.Rows[j][15].ToString() == null ? null : dtresult.Rows[j][15].ToString(),
                                    //Designation
                                    DesignationId = DesignationId,
                                    //Department
                                    DepartmentId = DepartmentId,
                                    //ReportingOfficer Officer ||Manager
                                    MgrId = MgrId,
                                    //EmployeeType
                                    EmpTypeId = EmpTypeId,
                                    //EmployeCategory
                                    EmpCategoryId = EmpCategoryId,
                                    //Paymethod
                                    PayMethodId = PayMethodId,
                                    //bankName
                                    EmpBankId = EmpBankId,
                                    //Bank Account
                                    BankAccountNo = dtresult.Rows[j][23].ToString() == "" || dtresult.Rows[j][23].ToString() == null ? "" : dtresult.Rows[j][23].ToString(),
                                    //Location
                                    LocationId = LocationId,
                                    //Shift
                                    ShiftId = ShiftId,
                                    Dol = CheckDBNull(dtresult.Rows[j][26])
                                });
                                _successlist.Add(new SelectListItem
                                {
                                    Text = dtresult.Rows[j][1].ToString() + "  Added Succesfully !!",
                                    Value = j.ToString()
                                });
                            }
                        }
                    }

                    flag = model.EmployeeCreateExcel(emplist);

                    return Json(new { Flag = flag, htmllistsuccess = _successlist, htmllisterror = _errorlist, htmllistwarning = _WarningList }, JsonRequestBehavior.AllowGet);

                    //return Json(new { Flag = flag, htmllistsuccess = _successlist, htmllisterror = _errorlist, htmllistwarning = _WarningList, Html = Url.Action("ScreenMenu") }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Flag = 2, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new
                {
                    Flag = 1,
                    Html = html
                }, JsonRequestBehavior.AllowGet);
            }


        }
        public DateTime CheckDBNull(object dateTime)
        {
            if (dateTime == DBNull.Value)          
              
                return DateTime.MinValue;
            else
                return (DateTime)dateTime;
        }
        public List<SelectListItem> CompanyList { get; set; }
        public DataTable importExcelFile(string inputpath)
        {
            string conStr = "";
            switch (".xlsx")
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, inputpath, "yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            cmdExcel.Connection = connExcel;
            connExcel.Open();
            DataTable exldta = new DataTable();
            try
            {

                DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                da.SelectCommand = cmdExcel;
                da.Fill(ds);
                exldta = ds.Tables[0];
                connExcel.Close();
                return exldta;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                cmdExcel.Dispose();
                connExcel.Dispose();
            }

        }
        [HttpPost]
        public ActionResult EmployeePhotoGet(int employeeid)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable dt = obj.EmployeePhotoGet(employeeid);

                byte[] byteImage = null;
                if (dt.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dt.Rows[0]["emp_photo"].ToString()))
                    {
                        byteImage = null;
                    }
                    else
                    {
                        byteImage = (byte[])dt.Rows[0]["emp_photo"];
                    }
                }
                // return Json(byteImage == null ? null : str, JsonRequestBehavior.AllowGet);
                return Json(byteImage == null ? null : Convert.ToBase64String(byteImage), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(1);
            }
        }

        [HttpPost]
        public ActionResult EmployeeUpdate(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? isExist = obj.EmployeeExistsValidate(model.EmpCode, model.EmployeeId);
                if (string.IsNullOrEmpty(model.EmpName))
                {
                    model.EmpName = model.hfEmpName;

                }

                if (string.IsNullOrEmpty(model.EmpName) || model.EmpName == "0")
                {
                    return Json(new { Flag = 1, Html = "Employee Name  Is Required !" }, JsonRequestBehavior.AllowGet);
                }

                if (isExist == 0)
                {
                    string flag = model.EmployeeUpdate(model);
                    //----------rohtak chain company---------
                    //int[] columnHide = { 0, 9, 12, 15, 16, 17, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 41, 42, 52, 53, 54, 55, 64, 65, 67, 68, 69, 70, 75, 76, 77, 78, 79, 80 };

                    //DataTable dtEmployeeGet = obj.EmployeeGet(null);

                    int[] columnHide = { };
                    DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);
                    return Json(new { Flag = flag, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { Flag = 1, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDelete(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable dtEmpSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Emp_Salary");

                if (dtEmpSalary.Rows.Count > 0)
                {
                    return Json(new { Flag = 1, Html = "Employee Id Use in Employee Salary !!" }, JsonRequestBehavior.AllowGet);
                }

                DataTable dtSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Salary");

                if (dtSalary.Rows.Count > 0)
                {
                    return Json(new { Flag = 1, Html = "Employee Id Use in payroll !!" }, JsonRequestBehavior.AllowGet);
                }
                DataTable dtAttendance = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Attendance");
                if (dtAttendance.Rows.Count > 0)
                {
                    return Json(new { Flag = 1, Html = "Employee Id Use in Attendance Entry !!" }, JsonRequestBehavior.AllowGet);
                }
                string[] deletefromProject;
                string[] deletefrombankDetails;
                string[] deletefromEmployee;

                List<string> _dlproj = new List<string>();
                List<string> _dlbankdetails = new List<string>();
                List<string> _dlemp = new List<string>();
                DataTable dt = obj.EmpProjectGet(null, null);
                for (int d = 0; d < dt.Rows.Count; d++)
                {
                    if (Convert.ToInt32(dt.Rows[d][1]) == model.EmployeeId)
                    {
                        model.EmpProjectId = Convert.ToInt32(dt.Rows[d][0]);
                        int? DepStatus = obj.EmpProjectDelete(model.EmpProjectId);
                        _dlproj.Add("Delete Project,Department,Desination,Location Successfully!");
                        break;
                    }
                }
                DataTable dts = obj.EmployeeBankDetailsGet(null, null, null);
                for (int _bdetails = 0; _bdetails < dts.Rows.Count; _bdetails++)
                {
                    if (Convert.ToInt32(dts.Rows[_bdetails][1]) == model.EmployeeId)
                    {
                        model.EmpbankDetailsId = Convert.ToInt32(dts.Rows[_bdetails][0]);
                        int? bankDetails = obj.EmployeeBankDetailsDelete(model.EmpbankDetailsId);
                        _dlbankdetails.Add("Delete Bank Details Successfully!");
                        break;
                    }
                }
                int? status = obj.EmployeeDelete(model.EmployeeId);
                if (status == 0)
                {
                    _dlemp.Add("Delete Employee Information Successfully!");
                }
                int[] columnHide = { };
                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString(), deletefromProject = _dlproj, deletefromEmployee = _dlemp, deletefrombankDetails = _dlbankdetails }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Flag = 1, Html = "Employee Id Use in Leave/Biometric/Pis Modules !!" }, JsonRequestBehavior.AllowGet);
            }
        }




        //--------Employee Parameter Detail---------------

        [HttpPost]
        public ActionResult EmployeeParameterDetailGet(int? employeeid)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dtParameter = obj.EmpParameterDetailGet(employeeid);
                if (dtParameter.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtParameter, columnHide);
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
        public ActionResult EmployeeParameterDetailCreate(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.EmpParameterDetailExistsValidate(model.ParameterId, model.EmployeeId);
                if (Exist == 0)
                {
                    int? status = obj.EmpParameterDetailCreate(model.ParameterId, model.ParameterValue, model.EmployeeId);
                    int[] columnHide = new[] { 0, 1, 4 };
                    DataTable dtParameter = obj.EmpParameterDetailGet(model.EmployeeId);
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtParameter, columnHide);
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
        public ActionResult EmployeeParameterDetailUpdate(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpParameterDetailUpdate(model.EmpParameterId, model.ParameterId, model.ParameterValue, model.EmployeeId);
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dtParameter = obj.EmpParameterDetailGet(model.EmployeeId);
                StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeParameterDetailDelete(EmployeeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpParameterDetailDelete(model.EmpParameterId);
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dtParameter = obj.EmpParameterDetailGet(model.EmployeeId);
                StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-------------------Employee Bank Details---------------------------

        public ActionResult EmployeeBankDetails()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmployeeBankDetailsViewModel model = new EmployeeBankDetailsViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        public ActionResult EmployeeBankDetailsGet(EmployeeBankDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 3, 4, 5, 6, 7, 8, 9 };
                DataTable dt = obj.EmployeeBankDetailsGet(null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeBankDetailsCreate(EmployeeBankDetailsViewModel model)
        {
            //DateTime StartDt;
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmployeeBankDetailsExistsValidate(null, model.EmployeeId, model.EmpBankId, model.StartDate, model.EndDate);
                if (Exist == 0)
                {
                    int? status = obj.EmployeeBankDetailsCreate(model.EmployeeId, model.EmpBankId, model.BankAccountNo, model.Description, model.StartDate, model.EndDate);
                    int[] showcolumn = new[] { 3, 4, 5, 6, 7, 8, 9 };
                    DataTable dt = obj.EmployeeBankDetailsGet(null, null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else if (Exist == 2)
                {
                    return Json(new { Flag = 2, Html = "Check start Datae and End Date !" }, JsonRequestBehavior.AllowGet);
                }
                else 
                {
                    return Json(new { Flag = 2, Html = "Already Exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeBankDetailsUpdate(EmployeeBankDetailsViewModel model)
        {
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmployeeBankDetailsExistsValidate(model.EmployeeBankDetailsId, model.EmployeeId, model.EmpBankId, model.StartDate, model.EndDate);
                if (Exist == 0)
                {
                    int? status = obj.EmployeeBankDetailsUpdate(model.EmployeeBankDetailsId, model.EmployeeId, model.EmpBankId, model.BankAccountNo, model.Description, model.StartDate, model.EndDate);
                    int[] showcolumn = new[] { 3, 4, 5, 6, 7, 8, 9 };
                    DataTable dt = obj.EmployeeBankDetailsGet(null, null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Already Exist !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeBankDetailsDelete(EmployeeBankDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmployeeBankDetailsDelete(model.EmployeeBankDetailsId);
                int[] showcolumn = new[] { 3, 4, 5, 6, 7, 8, 9 };
                DataTable dt = obj.EmployeeBankDetailsGet(null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeBankDetails(EmployeeBankDetailsViewModel model, string command)
        {
            string IsHistory = string.IsNullOrEmpty(Request["IsHistory"]) ? "off" : "on";

            if (Session["EmpBankHistory"] != null && IsHistory == "on")
            {
                DataTable dtEmpSalary1 = ObjML.EmployeeBankDetailsGetHistory(null, null, null);
                //int[] Column = new[] { 3, 4, 5, 6 };
                int[] showcolumn = new[] { 0, 1, 2 };// 3, 4, 5, 6, 7, 8, 9 };
                DataTable dt1 = CommonUtil.DataTableColumnRemove(dtEmpSalary1, showcolumn);

                GridView gv1 = GridViewGet(dt1, "Employee Bank Report");

                ActionResult a1 = null;

                if (command == "Pdf")
                {
                    a1 = DataExportPDF(gv1, "Employee Bank");
                }
                if (command == "Excel")
                {
                    a1 = DataExportExcel(gv1, "Employee Bank", 12);
                }
                if (command == "Word")
                {
                    a1 = DataExportWord(gv1, "Employee Bank");
                }
                return a1;
            }
            else {

                DataTable dtEmployeeBank = obj.EmployeeBankDetailsGet(null, null, null);
                //int[] Column = new[] { 3, 4, 5, 6 };
                int[] Column = new[] { 0, 1, 2 };
                DataTable dt = CommonUtil.DataTableColumnRemove(dtEmployeeBank, Column);

                GridView gv = GridViewGet(dt, "Employee Bank Report");

                ActionResult a = null;

                if (command == "Pdf")
                {
                    a = DataExportPDF(gv, "Employee Bank");
                }
                if (command == "Excel")
                {
                    a = DataExportExcel(gv, "Employee Bank", 12);
                }
                if (command == "Word")
                {
                    a = DataExportWord(gv, "Employee Bank");
                }
                return a;
            }
        }

        [HttpPost]
        public ActionResult EmployeeBankDetailsExport(EmployeeBankDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 3, 4, 5, 6 };
                DataTable dt = obj.EmployeeBankDetailsGet(null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableExport(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmpBankDetailsHistoryGet(string History)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                DataTable dtEmpSalary = new DataTable();

                int[] showcolumn = new[] { 3, 4, 5, 6, 7, 8, 9 };
                if (History == "True" || History == "true")
                {
                    dtEmpSalary = ObjML.EmployeeBankDetailsGetHistory(null, null, null);
                    Session["EmpBankHistory"] = dtEmpSalary;

                    if (dtEmpSalary.Rows.Count > 0)
                    {
                        //DataTable dt1 = CommonUtil.DataTableColumnRemove(dtEmpSalary, showcolumn);
                        //Session.Add("dtEmpBankDetailsHistoryGet", dt1);
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEmpBankdetailHistoryGet(showcolumn, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    dtEmpSalary = obj.EmployeeBankDetailsGet(null, null, null);
                    if (dtEmpSalary.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-------------------- Employee Department --------------------------------


        [HttpPost]
        public ActionResult EmployeeDepartmentHistoryGet(string History)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                DataTable dtEmpdeptget = new DataTable();

                int[] showcolumn = new[] { 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                if (History == "True" || History == "true")
                {
                    dtEmpdeptget = ObjML.EmpProjectGetHistory(null, null);
                    Session["EmpDeptGetHistory"] = dtEmpdeptget;

                    if (dtEmpdeptget.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEmpDepartmentGetHistory(showcolumn, dtEmpdeptget);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    dtEmpdeptget = obj.EmpProjectGet(null, null);
                    if (dtEmpdeptget.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dtEmpdeptget);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        // public List<CollectionListData> _successlistDep { get; set; }
        public ActionResult EmployeeDepartment()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpProjectViewModel model = new EmpProjectViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        public List<CollectionListData> listd = new List<CollectionListData>();
        public List<CollectionListData> listempsal = new List<CollectionListData>();
        public List<CollectionListData> EmpGrpSalary = new List<CollectionListData>();
        public List<CollectionListData> ListEmpShift = new List<CollectionListData>();
        public List<CollectionListData> ListEmpInfo = new List<CollectionListData>();
        [HttpPost]
        public ActionResult EmployeeDepartmentGet(EmpProjectViewModel model)
        {
            string[] htmllistsuccess;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                DataTable dt = obj.EmpProjectGet(null, null);

                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        listd.Add(new CollectionListData
                        {
                            Text = dt.Rows[j][8].ToString(),
                            Value = dt.Rows[j][1].ToString()
                        });

                    }
                }
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString(), htmllistsuccess = listd }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeDepartment(EmpProjectViewModel model, string command)
        {
            string IsHistory = string.IsNullOrEmpty(Request["IsHistory"]) ? "off" : "on";

            if (Session["EmpDeptGetHistory"] != null && IsHistory == "on")
            {
                DataTable dtEmpdeptget1 = ObjML.EmpProjectGetHistory(null, null);
                //int[] Column = new[] { 3, 4, 5, 6 };
                int[] showcolumn = new[] { 0, 1, 2, 3, 4, 5 };// 3, 4, 5, 6, 7, 8, 9 };
                DataTable dt1 = CommonUtil.DataTableColumnRemove(dtEmpdeptget1, showcolumn);

                GridView gv1 = GridViewGet(dt1, "Employee Department Report");

                ActionResult a1 = null;

                if (command == "Pdf")
                {
                    a1 = DataExportPDF(gv1, "Employee Department");
                }
                if (command == "Excel")
                {
                    a1 = DataExportExcel(gv1, "Employee Department", 12);
                }
                if (command == "Word")
                {
                    a1 = DataExportWord(gv1, "Employee Department");
                }
                return a1;
            }
            else {

                DataTable dtEmployeeDepartment = obj.EmpProjectGet(null, null);
                //int[] Column = new[] { 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                int[] Column = new[] { 0, 1, 2, 3, 4, 5, 6 };
                DataTable dt = CommonUtil.DataTableColumnRemove(dtEmployeeDepartment, Column);

                GridView gv = GridViewGet(dt, "Employee Department Report");

                ActionResult a = null;

                if (command == "Pdf")
                {
                    a = DataExportPDF(gv, "Employee Department");
                }
                if (command == "Excel")
                {
                    a = DataExportExcel(gv, "Employee Department", 12);
                }
                if (command == "Word")
                {
                    a = DataExportWord(gv, "Employee Department");
                }
                return a;
            }
        }


        [HttpPost]
        public ActionResult EmployeeDepartmentExport(EmpProjectViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                DataTable dt = obj.EmpProjectGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableExport(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult DateValidateGet(EmpProjectViewModel model)
        {
            string attendance_last_end_date = null;
            string salary_last_end_date = null;
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable dtAttendanceDateGet = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId2, "Attendance");
                DataTable dtSalaryDateGet = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId2, "Salary");

                if (dtSalaryDateGet.Rows.Count > 0)
                {
                    attendance_last_end_date = Convert.ToString(dtAttendanceDateGet.Rows[0]["last_end_date"]);
                    salary_last_end_date = Convert.ToString(dtSalaryDateGet.Rows[0]["last_end_date"]);
                    return Json(new { Flag = 0, attendance_last_end_date, salary_last_end_date }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
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
        public ActionResult EmployeeDepartmentCreate(EmpProjectViewModel model)
        {
            string html = null;
            try
            {
                DateTime StartDate;

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //model.EmployeeId = model.Employee_Id;
                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                }

                if (model.DepartmentId != null)
                {
                    DataTable dtDptStartDate = obj.DepartmentGet(null, model.DepartmentId);
                    string strDptStDate = Convert.ToString(dtDptStartDate.Rows[0]["Start Date"]);
                    if (!(string.IsNullOrEmpty(strDptStDate)))
                    {
                        StartDate = Convert.ToDateTime(strDptStDate);
                        if (StartDate > model.StartDt)
                        {
                            return Json(new { Flag = 2, Html = "Start Date Should be Greater than Department Start Date " }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "Department Start Date Can not be null" }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.ProjectId != null)
                {
                    DataTable dtDptStartDate = obj.ProjectGet(null, model.ProjectId);
                    string strDptStDate = Convert.ToString(dtDptStartDate.Rows[0]["Start Date"]);
                    if (!(string.IsNullOrEmpty(strDptStDate)))
                    {
                        StartDate = Convert.ToDateTime(strDptStDate);
                        if (StartDate > model.StartDt)
                        {
                            return Json(new { Flag = 2, Html = "Start Date Should be Greater than Project Start Date " }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "Project Start Date Can not be null" }, JsonRequestBehavior.AllowGet);
                    }
                }

                int? Exist = obj.EmpProjectExistsValidate(model.EmployeeId, model.StartDt, model.EndDt, null);
                if (Exist == 0)
                {
                    int? status = obj.EmpProjectCreate(model.EmployeeId, model.ProjectId, model.DepartmentId, model.DesignationId, model.LocationId, model.StartDt, model.EndDt);
                    int[] showcolumn = new[] { 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                    DataTable dt = obj.EmpProjectGet(null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Already Exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDepartmentUpdate(EmpProjectViewModel model)
        {
            string html = null;
            DateTime StartDate;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                model.EmployeeId = model.EmployeeId2;
                model.StartDt = model.StartDt2;
                model.EndDt = model.EndDt2;

                /*
                model.DepartmentId = model.Department_Id;
                model.Designation_Id = model.Designation_Id;
                model.LocationId = model.Location_Id;
                model.ProjectId = model.Project_Id;
                */
                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                }

                if (model.DepartmentId != null)
                {
                    DataTable dtDptStartDate = obj.DepartmentGet(null, model.DepartmentId);
                    string strDptStDate = Convert.ToString(dtDptStartDate.Rows[0]["Start Date"]);
                    if (!(string.IsNullOrEmpty(strDptStDate)))
                    {
                        StartDate = Convert.ToDateTime(strDptStDate);
                        if (StartDate > model.StartDt)
                        {
                            return Json(new { Flag = 2, Html = "Start Date Should be Greater than Department Start Date " }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "Department Start Date Can not be null" }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.ProjectId != null)
                {
                    DataTable dtDptStartDate = obj.ProjectGet(null, model.ProjectId);
                    string strDptStDate = Convert.ToString(dtDptStartDate.Rows[0]["Start Date"]);
                    if (!(string.IsNullOrEmpty(strDptStDate)))
                    {
                        StartDate = Convert.ToDateTime(strDptStDate);
                        if (StartDate > model.StartDt)
                        {
                            return Json(new { Flag = 2, Html = "Start Date Should be Greater than Project Start Date " }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "Project Start Date Can not be null" }, JsonRequestBehavior.AllowGet);
                    }
                }



                int? status = obj.EmpProjectUpdate(model.EmpProjectId, model.EmployeeId, model.ProjectId, model.DepartmentId, model.DesignationId, model.LocationId, model.StartDt, model.EndDt);
                int[] showcolumn = new[] { 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                DataTable dt = obj.EmpProjectGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDepartmentDelete(EmpProjectViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? employee_id = null;

                DataTable dtPr = obj.EmpProjectGet(model.EmpProjectId, null);

                employee_id = Convert.ToInt32(dtPr.Rows[0][1]);
                DataTable dtSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(employee_id, "Salary");

                if (dtSalary.Rows.Count > 0)
                {
                    return Json(new { Flag = 1, Html = "Employee Id Use in payroll !!" }, JsonRequestBehavior.AllowGet);
                }
                DataTable dtAttendance = obj.EmployeeDepartmentSalaryAttendanceDatesGet(employee_id, "Attendance");
                if (dtAttendance.Rows.Count > 0)
                {
                    return Json(new { Flag = 1, Html = "Employee Id Use in Attendance Entry !!" }, JsonRequestBehavior.AllowGet);
                }


                int? status = obj.EmpProjectDelete(model.EmpProjectId);
                int[] showcolumn = new[] { 6, 7, 8, 9, 10, 12, 13, 14, 15 };
                DataTable dt = obj.EmpProjectGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Flag = 1, Html = "Employee Id Use in Leave !!" }, JsonRequestBehavior.AllowGet);
            }
        }


        //-------------------- Employee Salary --------------------------------//
        public ActionResult EmployeeSalary()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpSalaryViewModel model = new EmpSalaryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult EmployeeSalary(EmpSalaryViewModel model, string command, FormCollection frm)
        {
            string html = string.Empty;
            DataTable dt = new DataTable();
            GridView gv = new GridView();
            DataSet dsEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
            int[] Column = new[] { 7, 8 };
            string IsHistory = string.IsNullOrEmpty(Request["IsHistory"].ToString()) ? "off" : "on";
            if (dsEmpSalary.Tables.Count > 0 && IsHistory == "on")
            {
                int[] Columnhide = new[] { 1, 4, 9, 10 };
                DataTable dt1 = (DataTable)Session["dtEmpSalary"];
                dt = CommonUtil.DataTableColumnRemove(dt1, Columnhide);
                gv = GridViewGet(dt, "Employee Salary Report");
            }
            else if (dsEmpSalary.Tables.Count > 0)
            {
                dt = CommonUtil.DataTableColumnRemove(dsEmpSalary.Tables[0], Column);
                gv = GridViewGet(dt, "Employee Salary Report");
            }
            else
            {
                dt = null;
                gv = GridViewGet(dt, "Employee Salary Report");
            }

            ActionResult View = null;
            if (command == "DownloadSalaryFormat")
            {
                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                int[] Columns = new[] { 0 };
                DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmployeeGet, Columns);
                DataTable dtD = new DataTable();
                DataColumn dc;
                dc = new DataColumn("Gross");
                dtD.Columns.Add(dc);
                dc = new DataColumn("CTC");
                dtD.Columns.Add(dc);
                dtt.Merge(dtD);
                GridView g = GridViewCreate(dtt, "Employee Salary Report");
                // GridView gvsal = GridViewGet(dtt, "Employee Salary Report");
                View = DataExportExcel(g, "Salary Format", 12);
            }
            if (command == "DownloadAttendanceFormat")
            {
                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                //for (int i = 0; i <= dtEmployeeGet.Rows.Count; i++)
                //{
                int[] Columns = { 20 };
                DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmployeeGet, Columns);
                DataTable dtD = new DataTable();
                DataColumn dc;
                dc = new DataColumn("Pay Period Cycle");
                dc.DefaultValue = "Monthly";
                dtD.Columns.Add(dc);

                dc = new DataColumn("Pay Month");
                dc.DefaultValue = 05;
                dtD.Columns.Add(dc);

                dc = new DataColumn("Pay Year");
                dc.DefaultValue = DateTime.Now.Year;
                dtD.Columns.Add(dc);

                dc = new DataColumn("Attendance Type");
                dc.DefaultValue = 1;
                dtD.Columns.Add(dc);

                dc = new DataColumn("Work Unit");
                dc.DefaultValue = "Day";
                dtD.Columns.Add(dc);

                dc = new DataColumn("Paid Days");
                dtD.Columns.Add(dc);
                dc = new DataColumn("Overtime Amount");
                dtD.Columns.Add(dc);

                dc = new DataColumn("Entry Date");
                dc.DefaultValue = DateTime.Now;
                dtD.Columns.Add(dc);

                dtD.Rows.Add(DateTime.Now);
                dc = new DataColumn("User Name");
                dc.DefaultValue = Session["userName"];
                dtD.Columns.Add(dc);

                dc = new DataColumn("Payroll type Id");
                dc.DefaultValue = 1;
                dtD.Columns.Add(dc);

                dc = new DataColumn("Entry Lock");
                dc.DefaultValue = 0;
                dtD.Columns.Add(dc);

                dc = new DataColumn("Attendance Source");
                dc.DefaultValue = 0;
                dtD.Columns.Add(dc);

                dtt.Merge(dtD);
                GridView g = GridViewCreate(dtt, "Employee Attendance");
                // GridView gvsal = GridViewGet(dtt, "Employee Salary Report");
                View = DataExportExcel(g, "Attendance Format", 12);
                //   }

            }
            if (command == "DownloadSalaryItems")
            {
                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                DataTable dtPayrollItem = obj.PayrollItemGet();
                DataTable table = new DataTable();
                int len = model.PayrollItemList.Count;
                var uSc = "_";
                if (model.PayrollItemList != null)
                {
                    for (int j = 0; j <= model.PayrollItemList.Count; j++)
                    {
                        if (j != 0 && j != len)
                        {
                            table.Columns.Add(model.PayrollItemList[j].Text + uSc + model.PayrollItemList[j].Value, typeof(string));
                        }
                    }
                }
                dtEmpSalary.Merge(table);
                int[] Columns = new[] { 4, 7, 8, 9, 10, 11, 12, 13, 14, 15, 17, 18, 20, 21, 22, 23 };
                DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmpSalary, Columns);
                GridView gvsal = GridViewCreate(dtt, "Employee Salary Report");
                View = DataExportExcel(gvsal, "SalaryItems Format", 12);

            }
            if (command == "DownloadvariableFormat")
            {
                DataTable dttT = obj.PayrollItemGet();
                var item1 = from myRow in dttT.AsEnumerable()
                            where myRow.Field<Boolean>("Is Overridable") == true
                            select myRow;
                DataTable dtPayrollItem = item1.CopyToDataTable();
                DataTable table = new DataTable();
                //  int len = model.PayrollItemList.Count;
                int len = dtPayrollItem.Rows.Count;
                var uSc = "_";
                if (dtPayrollItem.Rows.Count > 0)
                {
                    for (int j = 0; j <= dtPayrollItem.Rows.Count; j++)
                    {
                        if (j != 0 && j != len)
                        {
                            table.Columns.Add(dtPayrollItem.Rows[j][1] + uSc + dtPayrollItem.Rows[j][0], typeof(string));
                        }
                    }
                }
                DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                int[] Columns = { 40 };
                DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmployeeGet, Columns);
                DataTable dtD = new DataTable();
                DataColumn dc;
                dc = new DataColumn("Pay Month");
                dc.DefaultValue = DateTime.Now.Month;
                dtD.Columns.Add(dc);
                dc = new DataColumn("Pay Year");
                dc.DefaultValue = DateTime.Now.Year;
                dtD.Columns.Add(dc);
                dtt.Merge(dtD);
                dtt.Merge(table);
                GridView gvsal = GridViewCreate(dtt, "Employee VARIABLE Report");
                View = DataExportExcel(gvsal, "VARIABLE Format", 12);
            }
            if (command == "Pdf")
            {
                View = DataExportPDF(gv, "EmployeeSalary");
            }
            if (command == "Excel")
            {
                View = DataExportExcel(gv, "EmployeeSalary", 12);
            }
            if (command == "Word")
            {
                View = DataExportWord(gv, "EmployeeSalary");
            }
            return View;
        }

        //[HttpPost]
        //public ActionResult EmployeeSalary(EmpSalaryViewModel model, string command, FormCollection frm)
        //{
        //    string html = string.Empty;
        //    DataTable dt = new DataTable();
        //    GridView gv = new GridView();
        //    DataSet dsEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
        //    int[] Column = new[] { 7, 8 };
        //    if (dsEmpSalary.Tables.Count > 0)
        //    {
        //        dt = CommonUtil.DataTableColumnRemove(dsEmpSalary.Tables[0], Column);
        //        gv = GridViewGet(dt, "Employee Salary Report");
        //    }
        //    else
        //    {
        //        dt = null;
        //        gv = GridViewGet(dt, "Employee Salary Report");
        //    }

        //    ActionResult View = null;
        //    if (command == "DownloadSalaryFormat")
        //    {
        //        DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
        //        int[] Columns = new[] { 0 };
        //        DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmployeeGet, Columns);
        //        DataTable dtD = new DataTable();
        //        DataColumn dc;
        //        dc = new DataColumn("Gross");
        //        dtD.Columns.Add(dc);
        //        dc = new DataColumn("CTC");
        //        dtD.Columns.Add(dc);
        //        dtt.Merge(dtD);
        //        GridView g = GridViewCreate(dtt, "Employee Salary Report");
        //        // GridView gvsal = GridViewGet(dtt, "Employee Salary Report");
        //        View = DataExportExcel(g, "Salary Format", 12);
        //    }
        //    if (command == "DownloadAttendanceFormat")
        //    {
        //        DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
        //        //for (int i = 0; i <= dtEmployeeGet.Rows.Count; i++)
        //        //{
        //        int[] Columns = { 20 };
        //        DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmployeeGet, Columns);
        //        DataTable dtD = new DataTable();
        //        DataColumn dc;
        //        dc = new DataColumn("Pay Period Cycle");
        //        dc.DefaultValue = "Monthly";
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Pay Month");
        //        dc.DefaultValue = 05;
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Pay Year");
        //        dc.DefaultValue = DateTime.Now.Year;
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Attendance Type");
        //        dc.DefaultValue = 1;
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Work Unit");
        //        dc.DefaultValue = "Day";
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Paid Days");
        //        dtD.Columns.Add(dc);
        //        dc = new DataColumn("Overtime Amount");
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Entry Date");
        //        dc.DefaultValue = DateTime.Now;
        //        dtD.Columns.Add(dc);

        //        dtD.Rows.Add(DateTime.Now);
        //        dc = new DataColumn("User Name");
        //        dc.DefaultValue = Session["userName"];
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Payroll type Id");
        //        dc.DefaultValue = 1;
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Entry Lock");
        //        dc.DefaultValue = 0;
        //        dtD.Columns.Add(dc);

        //        dc = new DataColumn("Attendance Source");
        //        dc.DefaultValue = 0;
        //        dtD.Columns.Add(dc);

        //        dtt.Merge(dtD);
        //        GridView g = GridViewCreate(dtt, "Employee Attendance");
        //        // GridView gvsal = GridViewGet(dtt, "Employee Salary Report");
        //        View = DataExportExcel(g, "Attendance Format", 12);
        //        //   }

        //    }
        //    if (command == "DownloadSalaryItems")
        //    {
        //        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
        //        DataTable dtPayrollItem = obj.PayrollItemGet();
        //        DataTable table = new DataTable();
        //        int len = model.PayrollItemList.Count;
        //        var uSc = "_";
        //        if (model.PayrollItemList != null)
        //        {
        //            for (int j = 0; j <= model.PayrollItemList.Count; j++)
        //            {
        //                if (j != 0 && j != len)
        //                {
        //                    table.Columns.Add(model.PayrollItemList[j].Text + uSc + model.PayrollItemList[j].Value, typeof(string));
        //                }
        //            }
        //        }
        //        dtEmpSalary.Merge(table);
        //        int[] Columns = new[] { 4, 7, 8, 9, 10, 11, 12, 13, 14, 15, 17, 18, 20, 21, 22, 23 };
        //        DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmpSalary, Columns);
        //        GridView gvsal = GridViewCreate(dtt, "Employee Salary Report");
        //        View = DataExportExcel(gvsal, "SalaryItems Format", 12);

        //    }
        //    if (command == "DownloadvariableFormat")
        //    {
        //        DataTable dttT = obj.PayrollItemGet();
        //        var item1 = from myRow in dttT.AsEnumerable()
        //                    where myRow.Field<Boolean>("Is Overridable") == true
        //                    select myRow;
        //        DataTable dtPayrollItem = item1.CopyToDataTable();
        //        DataTable table = new DataTable();
        //        //  int len = model.PayrollItemList.Count;
        //        int len = dtPayrollItem.Rows.Count;
        //        var uSc = "_";
        //        if (dtPayrollItem.Rows.Count > 0)
        //        {
        //            for (int j = 0; j <= dtPayrollItem.Rows.Count; j++)
        //            {
        //                if (j != 0 && j != len)
        //                {
        //                    table.Columns.Add(dtPayrollItem.Rows[j][1] + uSc + dtPayrollItem.Rows[j][0], typeof(string));
        //                }
        //            }
        //        }
        //        DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
        //        int[] Columns = { 40 };
        //        DataTable dtt = CommonUtil.DataTableColumnRemove(dtEmployeeGet, Columns);
        //        DataTable dtD = new DataTable();
        //        DataColumn dc;
        //        dc = new DataColumn("Pay Month");
        //        dc.DefaultValue = DateTime.Now.Month;
        //        dtD.Columns.Add(dc);
        //        dc = new DataColumn("Pay Year");
        //        dc.DefaultValue = DateTime.Now.Year;
        //        dtD.Columns.Add(dc);
        //        dtt.Merge(dtD);
        //        dtt.Merge(table);
        //        GridView gvsal = GridViewCreate(dtt, "Employee VARIABLE Report");
        //        View = DataExportExcel(gvsal, "VARIABLE Format", 12);
        //    }
        //    if (command == "Pdf")
        //    {
        //        View = DataExportPDF(gv, "EmployeeSalary");
        //    }
        //    if (command == "Excel")
        //    {
        //        View = DataExportExcel(gv, "EmployeeSalary", 12);
        //    }
        //    if (command == "Word")
        //    {
        //        View = DataExportWord(gv, "EmployeeSalary");
        //    }
        //    return View;
        //}

    
        [HttpPost]
        public ActionResult EmployeeSalaryGet(EmpSalaryViewModel model)
        {
            string[] htmllistsuccess;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                int[] showcolumn = new[] { 2, 3, 5, 6, 10, 16, 17 };
                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);

                if (dtEmpSalary.Rows.Count > 0)
                {
                    for (int j = 0; j < dtEmpSalary.Rows.Count; j++)
                    {
                        listempsal.Add(new CollectionListData
                        {
                            Text = dtEmpSalary.Rows[j][3].ToString(),
                            Value = dtEmpSalary.Rows[j][1].ToString()
                        });

                    }
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString(), htmllistsuccess = listempsal }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDOjGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                int[] showcolumn = new[] { 2, 3, 5, 6, 10, 16, 17 };
                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeSalaryExportGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] hideColumn = new[] { 7, 8 };
                DataSet dtEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
                if (dtEmpSalary.Tables.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableExport(dtEmpSalary.Tables[0], hideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeSalaryCreate(EmpSalaryViewModel model, string IsCTC)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                model.EmployeeId = model.Employee_Id;
                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                int? Exist = obj.EmpSalaryExistsValidate(model.EmployeeId, model.StartDt, model.EndDt, null);
                if (Exist == 0)
                {
                    if (IsCTC == "Gross")
                    {

                        model.IsCTC = false;
                        model.Gross = model.Gross;
                        model.Ctc = null;

                        int? status = obj.EmpSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        }
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


                    }

                    else if (IsCTC == "GrossCTC")
                    {

                        model.IsCTC = false;
                        model.Gross = model.Gross;


                        int? status = obj.EmpSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        }
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


                    }

                    else
                    {
                        model.IsCTC = true;
                        model.Ctc = model.Ctc;
                        model.Gross = null;


                        int? status = obj.EmpSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        }
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeSalaryUpdate(EmpSalaryViewModel model, string IsCTC)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                model.EmployeeId = model.Employee_Id;
                model.Ctc = model.hfCtc;
                model.Gross = model.hfGross;

                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }


                int? Exist = obj.EmpSalaryExistsValidate(model.EmployeeId, model.StartDt, model.EndDt, model.EmpSalaryId);
                if (Exist == 0)
                {

                    if (IsCTC == "Gross")
                    {

                        model.IsCTC = false;
                        model.Gross = model.Gross;
                        model.Ctc = null;

                        int? status = obj.EmpSalaryUpdate(model.EmpSalaryId, model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        }
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                    }

                    else if (IsCTC == "GrossCTC")
                    {

                        model.IsCTC = false;
                        model.Gross = model.Gross;


                        int? status = obj.EmpSalaryUpdate(model.EmpSalaryId, model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        }
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        model.IsCTC = true;
                        model.Ctc = model.Ctc;
                        model.Gross = null;

                        int? status = obj.EmpSalaryUpdate(model.EmpSalaryId, model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };
                        int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                        }
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);



                    }


                }
                else
                {
                    return Json(new { Flag = 1, Html = "Employee Salary Already Exists !!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeSalaryDelete(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpSalaryDelete(model.EmpSalaryId);
                //  int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17};
                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 16, 17 };
                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, null, null);

                if (dtEmpSalary.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //-----------------Employee Salary Item Details -----------------------

        [HttpPost]
        public ActionResult EmployeeSalaryItemGet(int? EmpSalaryId)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 5, 8, 9, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(EmpSalaryId);
                if (dtEmpSalaryItem.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                    DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(EmpSalaryId, null, null, null, null, null, null, null);
                    htmlTable.Append("<div>");
                    int[] colHide = new[] { 6, 9 };
                    htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                    htmlTable.Append("</div>");
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
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
        public ActionResult EmployeeSalaryItemAmountGet(int? EmpSalaryId, int? PayrollItemId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? flag = obj.PayrollItemComputingtypeGet(PayrollItemId);
                if (flag == 1)
                {
                    decimal? amount = obj.EmpSalaryItemAmountGet(EmpSalaryId, PayrollItemId);
                    return Json(new { Flag = 0, Html = amount }, JsonRequestBehavior.AllowGet);
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
        public ActionResult PayrollItemComputingTypeGet(int? PayrollItemId, int? EmpSalaryId)
        {
            string html = null;
            string flag = null;
            string PayStartDate = null;
            string PayEndDate = null;

            string SalaryStartDate = null;
            string SalaryEndDate = null;
            string PayrollStartDate = null;
            string PayrollEndDate = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                // int? flag = obj.PayrollItemComputingtypeGet(PayrollItemId);
                DataTable dt = obj.PayrollItemComputingtypeDatesGet(PayrollItemId, EmpSalaryId);

                if (dt.Rows.Count > 0)
                {
                    flag = Convert.ToString(dt.Rows[0][0]);
                    PayStartDate = Convert.ToString(dt.Rows[0][1]);
                    PayEndDate = Convert.ToString(dt.Rows[0][2]);
                    SalaryStartDate = Convert.ToString(dt.Rows[0][3]);
                    SalaryEndDate = Convert.ToString(dt.Rows[0][4]);


                }

                if (flag == "1")
                {

                    return Json(new { Flag = 1, Html = "", PayStartDate = PayStartDate, PayEndDate = PayEndDate, SalaryStartDate = SalaryStartDate, SalaryEndDate = SalaryEndDate }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 0, Html = "Payroll Item is Static Item !!", PayStartDate = PayStartDate, PayEndDate = PayEndDate, SalaryStartDate = SalaryStartDate, SalaryEndDate = SalaryEndDate }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeSalaryItemCreateSingle(EmpSalaryViewModel model)
        {
            string html = null;
            int? Balance = null;
            int? add_deduct = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                if (model.Rate == null)
                {
                    model.Rate = model.RateValue;
                }

                model.EmployeeId = obj.EmployeeIdGet(model.EmpSalaryId);

                int flag = CommonUtil.CompareDate(model.StartDtItem, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = obj.EmpSalaryDatesGet(model.EmpSalaryId);

                DateTime? startdate = Convert.ToDateTime(dt.Rows[0][0]);
                DateTime? enddate = null;
                string strEndDate = Convert.ToString(dt.Rows[0][1]);
                if (!(string.IsNullOrEmpty(strEndDate)))
                {
                    enddate = Convert.ToDateTime(dt.Rows[0][1]);
                }

                if (enddate == null)
                {
                    if (!(model.StartDtItem >= startdate))
                    {
                        return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (!(model.StartDtItem >= startdate))
                    {
                        return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!(model.StartDtItem <= enddate))
                    {
                        return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Less or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.EndDtItem != null)
                    {
                        if (!(model.EndDtItem >= enddate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item End Date Should be Greater or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                DataTable dtSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Salary");
                if (dtSalary.Rows.Count > 0)
                {
                    string strSalaryDate = Convert.ToString(dtSalary.Rows[0][0]);
                    if (!string.IsNullOrEmpty(strSalaryDate))
                    {
                        return Json(new { Flag = 1, Html = "Salary Already Used In Payroll !" }, JsonRequestBehavior.AllowGet);
                    }
                }


                int? Exist = obj.EmpSalaryItemExistsValidate(model.EmpSalaryId, model.PayrollItemId, model.StartDtItem, model.EndDtItem);
                if (Exist == 0)
                {

                    DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, model.PayrollItemId, null, null, null, null, "Insert");

                    if (dtEmpSalaryStatus.Rows.Count > 0)
                    {
                        string StrAdd_deduct = Convert.ToString(dtEmpSalaryStatus.Rows[0]["item_type"]);
                        if (!string.IsNullOrEmpty(StrAdd_deduct))
                        {
                            add_deduct = Convert.ToInt32(StrAdd_deduct);
                        }

                        if (add_deduct > 0)
                        {
                            Balance = Convert.ToInt32(dtEmpSalaryStatus.Rows[0]["Balance"]);
                            if (Balance <= 0)
                                return Json(new { Flag = 1, Html = "Already Actual Monthly Gross Equal To Payroll Item Amount" }, JsonRequestBehavior.AllowGet);




                            if (model.Rate > Balance)
                                return Json(new { Flag = 1, Html = "Payroll Item Amount Should not Exceed Monthly Gross" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    int? status = obj.EmpSalaryItemCreate(model.EmpSalaryId, model.PayrollItemId, model.PayrollValueType,
                    model.Rate, model.PayrollFunctionId, model.PayrollItemValue, model.StartDtItem, model.EndDtItem,
                    model.PayStartDt, model.PayEndDt, model.NotesItem, model.EmployeeId, model.EmpSalaryGroupId, model.PayrollTypeId = 1,
                    model.IsOverridable);

                    int[] columnHide = new[] { 0, 1, 2, 5, 8, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                    DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(model.EmpSalaryId);
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                    dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, null, null, null, null, null, null);
                    htmlTable.Append("<div>");
                    int[] colHide = new[] { 6 };
                    htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                    htmlTable.Append("</div>");
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
        public ActionResult EmployeeSalaryItemCreate(EmpSalaryViewModel models, string empcode)
        {
            string html = null;
            string htmlList = null;
            string globalHTMl = string.Empty;
            int? Balance = null;
            int? add_deduct = null;

            List<string> List = new List<string>();
            var list = new List<Tuple<string, string>>();
            //list.Add(Tuple.Create(1, "Andy"));
            //list.Add(Tuple.Create(1, "John"));
            //list.Add(Tuple.Create(3, "Sally"));

            List<EmpSalaryViewModel> model = new JavaScriptSerializer().Deserialize<List<EmpSalaryViewModel>>(empcode);
            StringBuilder htmlTable;
            try
            {

                for (int i = 0; i < model.Count; i++)
                {
                    if (Session["userName"] == null)
                    {
                        return Redirect("~/Home/Login");
                    }
                    if (model[i].Rate == null)
                    {
                        model[i].Rate = model[i].RateValue;
                    }
                    model[i].EmployeeId = obj.EmployeeIdGet(model[i].EmpSalaryId);
                    int flag = CommonUtil.CompareDate(model[i].StartDtItem, model[i].EmployeeId);
                    if (flag == 2)
                    {
                        return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (flag == 1)
                    {
                        return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                    DataTable dt = obj.EmpSalaryDatesGet(model[i].EmpSalaryId);
                    DateTime? startdate = Convert.ToDateTime(dt.Rows[0][0]);
                    DateTime? enddate = null;
                    string strEndDate = Convert.ToString(dt.Rows[0][1]);
                    if (!(string.IsNullOrEmpty(strEndDate)))
                    {
                        enddate = Convert.ToDateTime(dt.Rows[0][1]);
                    }

                    if (enddate == null)
                    {
                        if (!(model[i].StartDtItem >= startdate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if (!(model[i].StartDtItem >= startdate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                        }
                        if (!(model[i].StartDtItem <= enddate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Less or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                        }
                        if (model[i].EndDtItem != null)
                        {
                            if (!(model[i].EndDtItem >= enddate))
                            {
                                return Json(new { Flag = 5, Html = "Salary Item End Date Should be Greater or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    DataTable dtSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model[i].EmployeeId, "Salary");
                    if (dtSalary.Rows.Count != 0)
                    {
                        string strSalaryDate = Convert.ToString(dtSalary.Rows[0][0]);
                        if (!string.IsNullOrEmpty(strSalaryDate))
                        {
                            return Json(new { Flag = 1, Html = "Salary Already Used In Payroll !" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    int? Exist = obj.EmpSalaryItemExistsValidate(model[i].EmpSalaryId, model[i].PayrollItemId, model[i].StartDtItem, model[i].EndDtItem);
                    if (Exist == 0)
                    {

                        DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model[i].EmpSalaryId, model[i].EmployeeId, model[i].PayrollItemId, null, null, null, null, "Insert");

                        if (dtEmpSalaryStatus.Rows.Count > 0)
                        {
                            string StrAdd_deduct = Convert.ToString(dtEmpSalaryStatus.Rows[0]["item_type"]);
                            if (!string.IsNullOrEmpty(StrAdd_deduct))
                            {
                                add_deduct = Convert.ToInt32(StrAdd_deduct);
                            }

                            if (add_deduct > 0)
                            {
                                Balance = Convert.ToInt32(dtEmpSalaryStatus.Rows[0]["Balance"]);
                                if (Balance <= 0)
                                    return Json(new { Flag = 1, Html = "Already Actual Monthly Gross Equal To Payroll Item Amount" }, JsonRequestBehavior.AllowGet);




                                if (model[i].Rate > Balance)
                                    return Json(new { Flag = 1, Html = "Payroll Item Amount Should not Exceed Monthly Gross" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        int? status = obj.EmpSalaryItemCreate(model[i].EmpSalaryId, model[i].PayrollItemId, model[i].PayrollValueType,
                        model[i].Rate, model[i].PayrollFunctionId, model[i].PayrollItemValue, model[i].StartDtItem, model[i].EndDtItem,
                        model[i].PayStartDt, model[i].PayEndDt, model[i].NotesItem, model[i].EmployeeId, model[i].EmpSalaryGroupId, model[i].PayrollTypeId = 1,
                        model[i].IsOverridable);

                        int[] columnHide = new[] { 0, 1, 2, 5, 8, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                        DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(model[i].EmpSalaryId);
                        htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                        dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model[i].EmpSalaryId, model[i].EmployeeId, null, null, null, null, null, null);
                        htmlTable.Append("<div>");

                        int[] colHide = new[] { 6 };
                        htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                        htmlTable.Append("</div>");
                        globalHTMl = htmlTable.ToString();

                        for (int j = 0; j <= models.PayrollItemList.Count; j++)
                        {
                            if (models.PayrollItemList[j].Value != "")
                            {
                                if (model[i].PayrollItemId == Convert.ToInt32(models.PayrollItemList[j].Value))
                                {
                                    list.Add(Tuple.Create(models.PayrollItemList[j].Text, "Payroll Salary Item Successfully"));
                                    List.Add(models.PayrollItemList[j].Text);
                                    break;
                                }
                            }
                        }
                        // return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Flag = 0, Html = globalHTMl.ToString(), htmlList = List }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }




        [HttpPost]
        public ActionResult EmployeeSalaryItemUpdate(EmpSalaryViewModel model)
        {
            string html = null;
            int? Balance = null;
            int? add_deduct = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                if (model.Rate == null)
                {
                    model.Rate = model.RateValue;
                }

                model.EmployeeId = obj.EmployeeIdGet(model.EmpSalaryId);

                int flag = CommonUtil.CompareDate(model.StartDtItem, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = obj.EmpSalaryDatesGet(model.EmpSalaryId);

                DateTime? startdate = Convert.ToDateTime(dt.Rows[0][0]);
                DateTime? enddate = null;
                string strEndDate = Convert.ToString(dt.Rows[0][1]);
                if (!(string.IsNullOrEmpty(strEndDate)))
                {
                    enddate = Convert.ToDateTime(dt.Rows[0][1]);
                }

                if (enddate == null)
                {
                    if (!(model.StartDtItem >= startdate))
                    {
                        return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (!(model.StartDtItem >= startdate))
                    {
                        return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Greater or Equal to Salary Start Date" }, JsonRequestBehavior.AllowGet);
                    }
                    if (!(model.StartDtItem <= enddate))
                    {
                        return Json(new { Flag = 5, Html = "Salary Item Start Date Should be Less or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.EndDtItem != null)
                    {
                        if (!(model.EndDtItem <= enddate))
                        {
                            return Json(new { Flag = 5, Html = "Salary Item End Date Should be Greater or Equal to Salary End Date" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, null, model.EmpSalaryItemId, null, null, null, "update");

                if (dtEmpSalaryStatus.Rows.Count > 0)
                {
                    add_deduct = Convert.ToInt32(dtEmpSalaryStatus.Rows[0]["item_type"]);
                    if (add_deduct > 0)
                    {
                        Balance = Convert.ToInt32(dtEmpSalaryStatus.Rows[0]["Balance"]);
                        if (model.Rate > Balance)
                            return Json(new { Flag = 1, Html = "Payroll Item Amount Should not Exceed Monthly Gross" }, JsonRequestBehavior.AllowGet);
                    }
                }






                int? status = obj.EmpSalaryItemUpdate(model.EmpSalaryItemId, model.EmpSalaryId, model.PayrollItemId, model.PayrollValueType,
                    model.Rate, model.PayrollFunctionId, model.PayrollItemValue, model.StartDtItem, model.EndDtItem,
                    model.PayStartDt, model.PayEndDt, model.NotesItem, model.EmployeeId, model.EmpSalaryGroupId,
                    model.PayrollTypeId = 1, model.IsOverridable);

                int[] columnHide = new[] { 0, 1, 2, 5, 8, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(model.EmpSalaryId);
                StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, null, null, null, null, null, null);
                htmlTable.Append("<div>");
                int[] colHide = new[] { 6 };
                htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                htmlTable.Append("</div>");
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeSalaryItemDelete(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpSalaryItemDelete(model.EmpSalaryItemId);

                model.EmployeeId = obj.EmployeeIdGet(model.EmpSalaryId);

                DataTable dtEmpSalaryStatus = obj.EmpSalaryStatusGet(model.EmpSalaryId, model.EmployeeId, null, null, null, null, null, null);

                int[] columnHide = new[] { 0, 1, 2, 5, 8, 10, 13, 14, 16, 17, 18, 19, 20, 21 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(model.EmpSalaryId);
                if (dtEmpSalaryItem.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
                    htmlTable.Append("<div>");
                    int[] colHide = new[] { 6 };
                    htmlTable.Append(CommonUtil.htmlTable(dtEmpSalaryStatus, colHide));
                    htmlTable.Append("</div>");
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //-------------------- Employee Salary Arear --------------------------------


        public ActionResult EmployeeSalaryArear()
        {
            try
            {


                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpSalaryViewModel model = new EmpSalaryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeSalaryArearGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 1, 2, 3, 4, 5, 6, 7, 9 };
                DataTable dtEmpSalary = obj.EmpSalaryArearGet(null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutDelete(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeSalaryArearExportGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] hideColumn = new[] { 7, 8 };
                DataSet dtEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
                if (dtEmpSalary.Tables.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableExport(dtEmpSalary.Tables[0], hideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeSalaryArearCreate(EmpSalaryViewModel model)
        {
            string html = null;

            int? flag = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpSalaryArearCreate(model.EmpSalaryId, model.EmpId, model.StartDt, model.EndDt, model.Gross, model.Notes, model.ArearAmount, out flag);


                if (flag == 0)
                {
                    int[] showcolumn = new[] { 1, 2, 3, 4, 5, 6, 7, 9 };
                    DataTable dtEmpSalary = obj.EmpSalaryArearGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutDelete(showcolumn, dtEmpSalary);
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
        public ActionResult EmployeeSalaryArearUpdate(EmpSalaryViewModel model)
        {
            string html = null;

            int? flag = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpSalaryArearUpdate(model.EmpSalaryId, model.EmpId, model.StartDt, model.EndDt, model.Gross, model.Notes, model.ArearAmount, out flag);


                if (flag == 0)
                {
                    int[] showcolumn = new[] { 1, 2, 3, 4, 5, 6, 7, 9 };
                    DataTable dtEmpSalary = obj.EmpSalaryArearGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutDelete(showcolumn, dtEmpSalary);
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


        //-----------------Employee Salary  Item Arear Details -----------------------

        [HttpPost]
        public ActionResult EmployeeSalaryItemArearGet(int? EmpSalaryId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 7 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryItemArearGet(EmpSalaryId);

                if (dtEmpSalaryItem.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditModeWithoutDelete(dtEmpSalaryItem, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html, remainingAmount = "0", totalAmount = "0" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeSalaryItemArearCreate(EmpSalaryViewModel model)
        {
            string html = null;
            int? flag = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? status = obj.EmpSalaryItemArearCreate(model.EmpSalaryItemId, model.EmpSalaryId, model.PayrollItemId, model.StartDtItem, model.EndDtItem, model.PayStartDt, model.PayEndDt, model.NotesItem, model.EmployeeId, model.ItemArearAmount, model.Rate, out flag);


                if (flag == 0)
                {


                    int[] columnHide = new[] { 0, 1, 2, 7 };
                    DataTable dtEmpSalaryItem = obj.EmpSalaryItemArearGet(model.EmpSalaryId);


                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditModeWithoutDelete(dtEmpSalaryItem, columnHide);
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
        public ActionResult EmployeeSalaryItemArearUpdate(EmpSalaryViewModel model)
        {
            string html = null;
            int? flag = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? status = obj.EmpSalaryItemArearUpdate(model.EmpSalaryItemId, model.EmpSalaryId, model.PayrollItemId, model.StartDtItem, model.EndDtItem, model.PayStartDt, model.PayEndDt, model.NotesItem, model.EmployeeId, model.ItemArearAmount, model.Rate, out flag);
                int[] columnHide = new[] { 0, 1, 2, 7 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryItemArearGet(model.EmpSalaryId);


                StringBuilder htmlTable = CommonUtil.htmlChildTableEditModeWithoutDelete(dtEmpSalaryItem, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);





            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        //------------------------Employee Daily Attendance Entry-----------------


        public ActionResult Daily()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpDailyAttendanceViewModel model = new EmpDailyAttendanceViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
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

        [HttpPost]
        public ActionResult DailyAttendanceEntry(EmpDailyAttendanceViewModel model, string commandName, string[] command)
        {
            ActionResult a = null;
            string html = null;
            try
            {
                int? status = null;
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                if (commandName == "BulkSearch")
                {
                    Session["Search"] = "BulkSearch";

                    int[] HideColumn = new[] { 0, 11 };
                    DataTable dtAttendance = obj.BmDailyAttendanceBulkGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.MonthId, model.Year);
                    if (dtAttendance.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableBMBulkAttendanceEntry(dtAttendance, HideColumn);
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }
                if (commandName == "DownloadExcelFormat")
                {
                    DataTable dtAttendance = obj.EmpDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);
                    //DataTable ddt = new DataTable();
                    //SqlDataAdapter sda = new SqlDataAdapter("SELECT Top 1 * from SearchDetails", consString);
                    //sda.Fill(dtAttendance);
                    GridView gv = GridViewGet(dtAttendance);
                    a = DataExportExcel(gv, "ExcelFormat", 12);
                    return a;
                }

                if (commandName == "Search")
                {
                    int[] HideColumn = new[] { 0, 1, 7, 14 };
                    //int[] HideColumn = new[] { 0, 1, 7, 8, 9, 14 };
                    DataTable dtAttendance = obj.EmpDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);
                    if (dtAttendance.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableDailyAttendanceEntry(dtAttendance, HideColumn);
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }


                if (commandName == "Insert")
                {
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    int count = model.EmployeeDataList.Count;

                    DataTable dtAttendance = new DataTable();
                    dtAttendance.Columns.Add("emp_daily_attendance_id", typeof(int));
                    dtAttendance.Columns.Add("employee_id", typeof(int));
                    dtAttendance.Columns.Add("attendance_date", typeof(DateTime));
                    dtAttendance.Columns.Add("in_time", typeof(TimeSpan));
                    dtAttendance.Columns.Add("out_time", typeof(TimeSpan));
                    dtAttendance.Columns.Add("attendance_status", typeof(string));
                    dtAttendance.Columns.Add("worked_units", typeof(decimal));
                    dtAttendance.Columns.Add("overtime_hours", typeof(decimal));
                    dtAttendance.Columns.Add("remarks", typeof(string));

                    if (model.EmployeeDataList != null)
                    {


                        for (int x = 0; x < count; x++)
                        {

                            checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.EmployeeDataList[x].EmployeeId != null)
                                {
                                    if (command == null)
                                    {
                                        var statusName = "command[" + x + "]";
                                        string StatusValue = Request[statusName];
                                        model.AttendanceStatus = Convert.ToString(StatusValue).Trim();
                                    }
                                    else
                                    {
                                        model.AttendanceStatus = command[x].Trim();

                                    }




                                    if (model.AttendanceStatus == "A")
                                    {
                                        model.EmployeeDataList[x].WorkedUnits = 0;
                                        model.EmployeeDataList[x].OvertimeHours = 0;
                                    }

                                    if (model.AttendanceStatus == "L")
                                    {
                                        model.EmployeeDataList[x].WorkedUnits = 0;
                                        model.EmployeeDataList[x].OvertimeHours = 0;
                                    }

                                    if (model.AttendanceStatus == "P")
                                    {
                                        if ((model.EmployeeDataList[x].WorkedUnits == 0) || (model.EmployeeDataList[x].WorkedUnits == null))
                                        {
                                            model.EmployeeDataList[x].WorkedUnits = 1;
                                        }


                                    }

                                    dtAttendance.Rows.Add(model.EmployeeDataList[x].EmpDailyAttendanceId, model.EmployeeDataList[x].EmployeeId, model.AttendanceDate, model.EmployeeDataList[x].InTime, model.EmployeeDataList[x].OutTime, model.AttendanceStatus, model.EmployeeDataList[x].WorkedUnits, model.EmployeeDataList[x].OvertimeHours, model.EmployeeDataList[x].Remarks);

                                }
                            }
                        }
                        SqlConnection con = null;
                        try
                        {
                            string conStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString();
                            con = new SqlConnection(conStr);
                            con.Open();

                            SqlCommand cmd = new SqlCommand("HRM.emp_daily_attendance_create", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm = new SqlParameter();

                            prm = cmd.Parameters.AddWithValue("@EmpDailyAttendanceEntry", dtAttendance);
                            prm.SqlDbType = SqlDbType.Structured;
                            prm.TypeName = "HRM.EmpDailyAttendanceEntry";
                            status = cmd.ExecuteNonQuery();
                            con.Close();
                        }

                        catch (Exception ex)
                        {
                            html = "<div id='divmsg' class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    int[] HideColumn = new[] { 0, 7, 8, 9 };
                    DataTable dtAttendanceTable = obj.EmpDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);


                    if (dtAttendanceTable.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableDailyAttendanceEntry(dtAttendanceTable, HideColumn);
                        if (status > 0)
                        {
                            htmlTable.Append("<div id='divmsg' class='alert alert-success'>Attendance Inserted Successfully  for the Selected Record(s) !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            htmlTable.Append("<div id='divmsg' class='alert alert-danger'>Insert Failed !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        html = "<div id='divmsg' class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = "<div id='divmsg' class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDailyAttendanceExcel(EmpDailyAttendanceViewModel model)
        {
            StringBuilder htmlTable = new StringBuilder();
            DataTable dtresult = null;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string targetFolder = string.Empty;
                string targetPath = string.Empty;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    targetFolder = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "UploadFiles");
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    targetPath = Path.Combine(targetFolder, fileName);
                    file.SaveAs(targetPath);
                    dtresult = importExcelFile(targetPath);
                    System.IO.File.Delete(fileName);

                }

                // DataTable dtemplist1 = obj.EmployeeInfoGet();
                DataTable dtemplist = obj.EmployeeGet(null);
                List<EmpSalaryViewModel> Salarylist = new List<EmpSalaryViewModel>();

                if (dtresult.Rows.Count > 0)
                {
                    for (int j = 0; j < dtresult.Rows.Count; j++)
                    {


                    }
                    //   flag = model.EmployeeCreateExcel(Salarylist);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Employee Code Already Exist!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new
                {
                    Flag = 1,
                    Html = html
                }, JsonRequestBehavior.AllowGet);
            }


        }

        //EmployeeGroupSalary

        public ActionResult EmployeeGroupSalary()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpSalaryViewModel model = new EmpSalaryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        [HttpPost]
        public ActionResult EmployeeGroupSalaryHistoryGet(string History)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataTable dtEmpSalary = new DataTable();

                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 7, 10, 16, 17, 18 };
                if (History == "True" || History == "true")
                {
                    dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", "History");
                }
                else
                {
                    dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);

                }
                if (dtEmpSalary.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeGroupSalaryGet(EmpSalaryViewModel model)
        {
            string[] htmllistsuccess;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int[] showcolumn = new[] { 2, 3, 5, 6, 7, 10, 16, 17, 18 };




                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    for (int j = 0; j < dtEmpSalary.Rows.Count; j++)
                    {
                        EmpGrpSalary.Add(new CollectionListData
                        {
                            Text = dtEmpSalary.Rows[j][3].ToString(),
                            Value = dtEmpSalary.Rows[j][1].ToString()
                        });

                    }

                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString(), htmllistsuccess = EmpGrpSalary }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeGroupSalaryPartialGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 7, 10, 16, 17, 18 };

                int? EmployeeId = Convert.ToInt32(Session["EmpCenter_Employee__Id"]);

                DataTable dtEmpSalary = obj.EmpSalaryGet(EmployeeId, null, "GroupSalary", null);
                if (dtEmpSalary.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeGroupSalary(EmpSalaryViewModel model, string command)
        {
            DataSet dsEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
            int[] Column = new[] { 7, 8 };
            DataTable dt = CommonUtil.DataTableColumnRemove(dsEmpSalary.Tables[0], Column);

            GridView gv = GridViewGet(dt, "Employee Group Salary Report");

            ActionResult a = null;

            if (command == "Pdf")
            {
                a = DataExportPDF(gv, "EmployeeGroupSalary");
            }
            if (command == "Excel")
            {
                a = DataExportExcel(gv, "EmployeeGroupSalary", 12);
            }
            if (command == "Word")
            {
                a = DataExportWord(gv, "EmployeeGroupSalary");
            }
            return a;
        }

        [HttpPost]
        public ActionResult EmployeeGroupSalaryExportGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] hideColumn = new[] { 7, 8 };
                DataSet dtEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
                if (dtEmpSalary.Tables.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableExport(dtEmpSalary.Tables[0], hideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeGroupSalaryCreate(EmpSalaryViewModel model, string IsCTC)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 7, 10, 16, 17, 18 };

                if (model.EmpSalaryGroupId == null)
                {

                    model.EmpSalaryGroupId = model.EmpSalaryGroup_Id;
                }

                int? Exist = obj.EmpSalaryExistsValidate(model.EmployeeId, model.StartDt, model.EndDt, null);
                if (Exist == 0)
                {
                    if (IsCTC == "Gross")
                    {

                        model.IsCTC = false;
                        model.Gross = model.Gross_group;
                        model.Ctc = null;
                        int? status = obj.EmpGroupSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);

                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(showcolumn, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


                    }
                    else
                    {
                        model.IsCTC = true;
                        model.Ctc = model.Ctc_group;
                        model.Gross = null;
                        int? status = obj.EmpSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);

                        // int? status = obj.EmpGroupSalaryCreate(model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);




                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                            return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Flag = 0, Html = "" }, JsonRequestBehavior.AllowGet);


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
        public ActionResult EmployeeGroupSalaryUpdate(EmpSalaryViewModel model, string IsCTC)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 7, 10, 16, 17, 18 };
                model.EmployeeId = model.Employee_Id;
                model.Gross_group = model.hfGross_group;
                model.Ctc_group = model.hfCtc_group;

                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }


                int? Exist = obj.EmpSalaryExistsValidate(model.EmployeeId, model.StartDt, model.EndDt, model.EmpSalaryId);


                if (model.EmpSalaryGroupId == null)
                {

                    model.EmpSalaryGroupId = model.EmpSalaryGroup_Id;
                }
                if (Exist == 0)
                {



                    if (IsCTC == "Gross")
                    {

                        model.IsCTC = false;
                        model.Ctc = null;
                        model.Gross = model.Gross_group;
                        int? status = obj.EmpGroupSalaryUpdate(model.EmpSalaryId, model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);

                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                            return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Flag = 0, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        model.IsCTC = true;
                        model.Gross = null;
                        model.Ctc = model.Ctc_group;
                        int? status = obj.EmpSalaryUpdate(model.EmpSalaryId, model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);
                        //   int? status = obj.EmpGroupSalaryUpdate(model.EmpSalaryId, model.EmployeeId, model.PayPeriodCycle, model.StartDt, model.EndDt, model.Gross, model.Ctc, model.PayrollEntryMethod, model.PayrollFunctionId, model.Notes, model.EmpSalaryGroupId, model.IsPfApplicable, model.IsEsiApplicable, model.VariablePayPercentage, model.VariableCtc, model.ActualCtc, model.SpecialBonus, model.FixedCtc, model.IsCTC);


                        DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);
                        if (dtEmpSalary.Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                            return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Flag = 0, Html = "" }, JsonRequestBehavior.AllowGet);


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
        public ActionResult EmployeeGroupSalaryDelete(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] showcolumn = new[] { 1, 2, 3, 5, 6, 7, 10, 16, 17, 18 };
                int? status = obj.EmpSalaryDelete(model.EmpSalaryId);

                DataTable dtEmpSalary = obj.EmpSalaryGet(null, null, "GroupSalary", null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeWithoutChildPanel(showcolumn, dtEmpSalary);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmployeeGroupSalaryItemGet(int? EmpSalaryId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] columnHide = new[] { 0, 1, 2, 5, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19 };
                //   DataTable dtEmpSalaryItem = obj.EmpSalaryItemGet(EmpSalaryId);

                DataSet dsEmpSalaryItem = new DataSet();
                dsEmpSalaryItem = obj.EmpGroupSalaryItemDetailsGet(EmpSalaryId);

                if (dsEmpSalaryItem.Tables.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableAll(dsEmpSalaryItem.Tables[0]);
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
        //------variable salary-------------------------------------------------------------//

        [HttpPost]
        public ActionResult EmployeeVariableSalaryHistoryGet(string History)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataTable dtEmpSalary = new DataTable();

                int[] hidecolumn = new[] { 0, 1, 3, 5, 11 };
                if (History == "True" || History == "true")
                {
                    dtEmpSalary = obj.EmpVariableSalaryGet(null, "History");
                }
                else
                {
                    dtEmpSalary = obj.EmpVariableSalaryGet(null, null);

                }
                if (dtEmpSalary.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeSameForm(dtEmpSalary, hidecolumn);
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
        public ActionResult EmployeeVariableSalaryGet(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int[] hidecolumn = new[] { 0, 1, 3, 5, 11 };




                DataTable dtEmpSalary = obj.EmpVariableSalaryGet(null, null);
                if (dtEmpSalary.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeSameForm(dtEmpSalary, hidecolumn);
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
        public ActionResult EmployeeVariableSalaryCreate(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.VarStartDt, model.VarEmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }

                int[] hidecolumn = new[] { 0, 1, 3, 5, 11 };


                int? Exist = obj.EmpVariableSalaryExistsValidate(model.EmpVariableSalaryId, model.VarEmployeeId, model.VarPayrollItem_Id, model.VarStartDt, model.VarEndDt);
                if (Exist == 0)
                {
                    int? status = obj.EmpVariableSalaryCreate(model.VarEmployeeId, model.VarPayrollItem_Id, model.VarPayPeriod_Cycle, model.VarAmount, model.VarStartDt, model.VarEndDt, model.VarNotes, model.IsAttendanceApplicable);

                    DataTable dtEmpSalary = obj.EmpVariableSalaryGet(null, null);
                    if (dtEmpSalary.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableEditModeSameForm(dtEmpSalary, hidecolumn);
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
        public ActionResult EmployeeVariablesalaryUpdate(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] hidecolumn = new[] { 0, 1, 3, 5, 11 };

                int flag = CommonUtil.CompareDate(model.VarStartDt, model.VarEmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }


                int? Exist = obj.EmpVariableSalaryExistsValidate(model.EmpVariableSalaryId, model.VarEmployeeId, model.VarPayrollItem_Id, model.VarStartDt, model.VarEndDt);



                if (Exist == 0)
                {
                    int? status = obj.EmpVariableSalaryUpdate(model.EmpVariableSalaryId, model.VarEmployeeId, model.VarPayrollItem_Id, model.VarPayPeriod_Cycle, model.VarAmount, model.VarStartDt, model.VarEndDt, model.VarNotes, model.IsAttendanceApplicable);

                    DataTable dtEmpSalary = obj.EmpVariableSalaryGet(null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeSameForm(dtEmpSalary, hidecolumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Flag = 1, Html = "Record Already Exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeVariableSalaryDelete(EmpSalaryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] hidecolumn = new[] { 0, 1, 3, 5, 11 };
                int? status = obj.EmpVariableSalaryDelete(model.EmpVariableSalaryId);

                DataTable dtEmpSalary = obj.EmpVariableSalaryGet(null, null);
                if (dtEmpSalary.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeSameForm(dtEmpSalary, hidecolumn);
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
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //----------Employee Attendance Entry--------------------
        [HttpPost]
        public ActionResult EmployeeAttendanceUpload(EmpAttendanceEntryViewModel model)
        {
            StringBuilder htmlTable = new StringBuilder();
            DataTable dtresult = null;
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string targetFolder = string.Empty;
                string targetPath = string.Empty;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    targetFolder = System.IO.Path.Combine(System.Web.HttpContext.Current.Request.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "UploadFiles");
                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    targetPath = Path.Combine(targetFolder, fileName);
                    file.SaveAs(targetPath);
                    dtresult = importExcelFile(targetPath);
                    System.IO.File.Delete(fileName);

                }
                if (dtresult.Rows.Count > 0)
                {
                    for (int j = 0; j < dtresult.Rows.Count; j++)
                    {
                        if (dtresult.Rows[j][0] != null)
                        {
                            model.EmployeeId = Convert.ToInt32(dtresult.Rows[0]);
                            model.PayPeriodCycle = dtresult.Rows[1].ToString();
                            model.PayPeriod = Convert.ToInt32(dtresult.Rows[2]);
                            model.PayYear = Convert.ToInt32(dtresult.Rows[3]);
                            model.WorkqtyActual = Convert.ToInt32(dtresult.Rows[6]);
                            model.OvertimeActual = Convert.ToDecimal(dtresult.Rows[7]);

                            int? status = ObjML.EmpAttendanceExcelCreate(model.EmployeeId,
                                model.PayPeriodCycle, model.PayPeriod, model.PayYear, 1,
                                "Day", model.WorkqtyActual, model.OvertimeActual, DateTime.Now,
                                Session["userName"].ToString(),
                                1,
                                false,
                                model.AttendanceSource = 0);
                        }
                    }
                }
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new
                {
                    Flag = 1,
                    Html = html
                }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult AttendanceEntry()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]


        public ActionResult AttendanceEntry(EmpAttendanceEntryViewModel model, string command)
        {

            string PayrollrollTypeIdList = "1";
            int? totalDay = null;
            string html = null;
            string workUnit = null;
            try
            {
                int? status = null;
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumn = null;

                if (command == "Search")
                {

                    DataSet dtAttendance = new DataSet();


                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 1, 9, 12, 17, 18, 21 };

                    }
                    else
                    {
                        //  HideColumn = new[] { 0, 7, 8, 9, 12, 17, 18, 21 };
                        HideColumn = new[] { 0, 1, 7, 9, 12, 14, 15, 17, 18, 21 };
                    }


                    //    HideColumn = new[] { 0, 1, 7, 8, 9, 12, 21 };  // for local day //hide for other client only for HYD client
                    dtAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.AttendanceSourceId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtAttendance.Tables.Count > 0)
                    {
                        if (dtAttendance.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableAttendanceEntry(dtAttendance.Tables[0], HideColumn, model.AttendanceSourceId, obj, workUnit);
                            totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                            htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }


                }


                if (command == "Insert")
                {
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string msg = "";

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

                                    int valiDay = obj.EmpValidWorkDayGet(model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year);


                                    if (valiDay >= model.EmployeeDataList[x].WorkqtyActual)
                                    {




                                        dtAttendance.Rows.Add(model.EmployeeDataList[x].EmpAttendanceEntryId, PayrollrollTypeIdList, model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year, model.EmployeeDataList[x].WorkedUnit, model.EmployeeDataList[x].WorkqtyActual, model.EmployeeDataList[x].OvertimeActual, model.EmployeeDataList[x].Notes, model.AttendanceSourceId, model.EmployeeDataList[x].LocalDay, model.EmployeeDataList[x].NonLocalDay);

                                        // dtAttendance.Rows.Add(model.EmployeeDataList[x].EmpAttendanceEntryId, PayrollrollTypeIdList, model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year, "'Day'", model.EmployeeDataList[x].WorkqtyActual, model.EmployeeDataList[x].OvertimeActual, model.EmployeeDataList[x].Notes, model.AttendanceSourceId, model.EmployeeDataList[x].LocalDay, model.EmployeeDataList[x].NonLocalDay);





                                    }
                                    else
                                    {
                                        msg += Convert.ToString(model.EmployeeDataList[x].EmployeeId) + ",";
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg = "<span class='alert alert-danger'>and Employee id  - " + msg + " has invalid worked day / over time <span>";
                        }

                        SqlConnection con = null;
                        try
                        {



                            string conStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString();
                            con = new SqlConnection(conStr);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("HRM.emp_attendance_entry_Create", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm = new SqlParameter();
                            prm = cmd.Parameters.AddWithValue("@EmpAttendanceEntry", dtAttendance);
                            prm.SqlDbType = SqlDbType.Structured;
                            prm.TypeName = "HRM.EmpAttendanceEntry";
                            status = cmd.ExecuteNonQuery();
                            con.Close();
                        }

                        catch (Exception ex)
                        {
                            html = "<div id='divmsg' class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                        finally
                        {

                            con.Close();
                        }
                    }

                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 9, 12, 17, 18, 21 };

                    }
                    else
                    {
                        //  HideColumn = new[] { 0, 7, 8, 9, 12, 17, 18, 21 };
                        HideColumn = new[] { 0, 7, 9, 12, 14, 15, 17, 18, 21 };
                    }

                    // int[] HideColumn = new[] { 0, 1, 7, 8, 9, 12, 19 };  //for local day  //hide for other client only for HYD client
                    DataSet dtEmpAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.AttendanceSourceId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtEmpAttendance.Tables.Count > 0)
                    {
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpAttendance.Tables[0].Rows.Count > 0)
                        {

                            //htmlTable = CommonUtil.htmlTableAttendanceEntry(dtEmpAttendance.Tables[0], HideColumn);
                            htmlTable = CommonUtil.htmlTableAttendanceEntry(dtEmpAttendance.Tables[0], HideColumn, model.AttendanceSourceId, obj, workUnit);

                        }
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                        htmlTable.Append("<div hidden='hidden'><input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                        htmlTable.Append("<div class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) " + msg + " !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        html = "<div class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) " + msg + " !!</div>";
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

        //----------Employee Monthly Attendance Entry--------------------
        public ActionResult MonthlyAttendanceEntry()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpMonthlyAttendanceEntryViewModel model = new EmpMonthlyAttendanceEntryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        [HttpPost]
        public ActionResult MonthlyAttendanceEntry(EmpMonthlyAttendanceEntryViewModel model, string command)
        {

            string PayrollrollTypeIdList = "1";
            int? totalDay = null;
            string html = null;
            string workUnit = null;
            try
            {
                int? status = null;
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumn = null;
                if (command == "Search")
                {

                    DataSet dtAttendance = new DataSet();


                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 1, 9, 12, 17, 18, 21 };

                    }
                    else
                    {
                        //  HideColumn = new[] { 0, 7, 8, 9, 12, 17, 18, 21 };
                        HideColumn = new[] { 0, 1, 7, 9, 12, 14, 15, 17, 18, 21 };
                    }


                    //    HideColumn = new[] { 0, 1, 7, 8, 9, 12, 21 };  // for local day //hide for other client only for HYD client
                    dtAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.AttendanceSourceId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtAttendance.Tables.Count > 0)
                    {
                        if (dtAttendance.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableAttendanceEntry(dtAttendance.Tables[0], HideColumn, model.AttendanceSourceId, obj, workUnit);
                            totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                            htmlTable.Append("<div hidden='hidden'> <input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        html = "<div id='divmsg' class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }



                }


                if (command == "Insert")
                {
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string msg = "";

                    DataTable dtAttendance = new DataTable();
                    dtAttendance.Columns.Add("emp_Monthly_attendance_entry_id", typeof(int));
                    dtAttendance.Columns.Add("paytype_id", typeof(string));
                    dtAttendance.Columns.Add("employee_id", typeof(int));
                    dtAttendance.Columns.Add("pay_period", typeof(int));
                    dtAttendance.Columns.Add("pay_year", typeof(int));
                    dtAttendance.Columns.Add("work_unit", typeof(string));
                    dtAttendance.Columns.Add("present_days", typeof(decimal));
                    dtAttendance.Columns.Add("notes", typeof(string));
                    dtAttendance.Columns.Add("attendance_method", typeof(int));
                    dtAttendance.Columns.Add("half_days", typeof(decimal));
                    dtAttendance.Columns.Add("leave_days", typeof(decimal));
                    dtAttendance.Columns.Add("late_days", typeof(decimal));
                    dtAttendance.Columns.Add("permission_days", typeof(decimal));

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

                                    int valiDay = obj.EmpValidWorkDayGet(model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year);


                                    if (valiDay >= model.EmployeeDataList[x].PresentDays)
                                    {




                                        dtAttendance.Rows.Add(model.EmployeeDataList[x].EmpMonthlyAttendanceEntryId, PayrollrollTypeIdList, model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year, model.EmployeeDataList[x].WorkedUnit, model.EmployeeDataList[x].PresentDays, model.EmployeeDataList[x].OvertimeActual, model.EmployeeDataList[x].Notes, model.AttendanceSourceId, model.EmployeeDataList[x].HalfDay, model.EmployeeDataList[x].LeaveDay);

                                        // dtAttendance.Rows.Add(model.EmployeeDataList[x].EmpAttendanceEntryId, PayrollrollTypeIdList, model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year, "'Day'", model.EmployeeDataList[x].WorkqtyActual, model.EmployeeDataList[x].OvertimeActual, model.EmployeeDataList[x].Notes, model.AttendanceSourceId, model.EmployeeDataList[x].LocalDay, model.EmployeeDataList[x].NonLocalDay);





                                    }
                                    else
                                    {
                                        msg += Convert.ToString(model.EmployeeDataList[x].EmployeeId) + ",";
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg = "<span class='alert alert-danger'>and Employee id  - " + msg + " has invalid worked day / over time <span>";
                        }

                        SqlConnection con = null;
                        try
                        {

                            string conStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString();
                            con = new SqlConnection(conStr);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("HRM.emp_attendance_entry_Create", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter prm = new SqlParameter();
                            prm = cmd.Parameters.AddWithValue("@EmpMonthlyAttendanceEntry", dtAttendance);
                            prm.SqlDbType = SqlDbType.Structured;
                            prm.TypeName = "HRM.EmpMonthlyAttendanceEntry";
                            status = cmd.ExecuteNonQuery();
                            con.Close();
                        }

                        catch (Exception ex)
                        {
                            html = "<div id='divmsg' class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                        finally
                        {

                            con.Close();
                        }
                    }

                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 9, 12, 17, 18, 21 };

                    }
                    else
                    {
                        //  HideColumn = new[] { 0, 7, 8, 9, 12, 17, 18, 21 };
                        HideColumn = new[] { 0, 7, 9, 12, 14, 15, 17, 18, 21 };
                    }

                    // int[] HideColumn = new[] { 0, 1, 7, 8, 9, 12, 19 };  //for local day  //hide for other client only for HYD client
                    DataSet dtEmpAttendance = obj.EmpAttendanceEntryGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.AttendanceSourceId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtEmpAttendance.Tables.Count > 0)
                    {
                        StringBuilder htmlTable = new StringBuilder();
                        if (dtEmpAttendance.Tables[0].Rows.Count > 0)
                        {

                            //htmlTable = CommonUtil.htmlTableAttendanceEntry(dtEmpAttendance.Tables[0], HideColumn);
                            htmlTable = CommonUtil.htmlTableAttendanceEntry(dtEmpAttendance.Tables[0], HideColumn, model.AttendanceSourceId, obj, workUnit);

                        }
                        totalDay = obj.DaysInMonth(model.MonthId, model.Year);
                        htmlTable.Append("<div hidden='hidden'><input type='hidden' id='totalDay' value='" + totalDay + "'  /></div>");
                        htmlTable.Append("<div class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) " + msg + " !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        html = "<div class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) " + msg + " !!</div>";
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




        //----------Employee Attendance Register--------------------


        public ActionResult AttendanceRegister()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpDailyAttendanceViewModel model = new EmpDailyAttendanceViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult AttendanceRegister(EmpDailyAttendanceViewModel model, string command)
        {

            DataSet dtEmployee = obj.EmpDailyAttendanceRegisterGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.Start_Date, model.End_Date);


            GridView gv = GridViewGet(dtEmployee.Tables[0], "Attendance Details");

            ActionResult a = null;

            if (command == "Pdf")
            {
                a = DataExportPDF(gv, "Attendance Register Details");
            }
            if (command == "Excel")
            {
                a = DataExportExcel(gv, "Attendance Register Details", 12);
            }
            if (command == "Word")
            {
                a = DataExportWord(gv, "Attendance Register Details");
            }
            return a;
        }

        [HttpPost]
        public ActionResult AttendanceRegisterGet(EmpDailyAttendanceViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataSet dtEmployee = obj.EmpDailyAttendanceRegisterGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.Start_Date, model.End_Date);


                if (dtEmployee.Tables.Count > 0)
                {
                    if (dtEmployee.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableAll(dtEmployee.Tables[0]);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "No Record !!";
                        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //------------------------------
        [HttpPost]
        public ActionResult PayrollTypeGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 2, 3, 4, 5, 6 };
                DataTable dt = obj.PayrollTypeGet();
                StringBuilder htmlTable = PayrollUtil.DropDownListWithCheckBox(dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //--------------- Employee Tds Details --------------------

        public ActionResult EmployeeTdsDetail()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpTdsDetailViewModel model = new EmpTdsDetailViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeTdsGet(EmpTdsDetailViewModel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 4, 5, 6, 7 };
                //DataTable dtEmployeeTdsGet = obj.EmpTdsDetailGet(null);
                DataTable dtEmployeeTdsGet = null;
                if (dtEmployeeTdsGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeTdsGet, columnHide);
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
        public ActionResult EmployeeTdsDetail(EmpTdsDetailViewModel model, string command)
        {
            //   DataTable dtEmpShift = obj.EmpTdsDetailGet(null);
            DataTable dtEmpShift = null;
            int[] Column = new[] { 0, 1, 2, 4, 5, 6, 7 };
            DataTable dt = CommonUtil.DataTableColumnRemove(dtEmpShift, Column);

            GridView gv = GridViewGet(dt, "Employee TDS Report");

            ActionResult a = null;

            if (command == "Pdf")
            {
                a = DataExportPDF(gv, "Employee TDS");
            }
            if (command == "Excel")
            {
                a = DataExportExcel(gv, "Employee TDS", 12);
            }
            if (command == "Word")
            {
                a = DataExportWord(gv, "Employee TDS");
            }
            return a;
        }

        [HttpPost]
        public ActionResult EmployeeTdsCreate(EmpTdsDetailViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                //  int? Exists = obj.EmpTdsDetailExistsValidate(model.EmployeeId);
                int? Exists = 0;
                if (Exists == 0)
                {
                    //int? status = obj.EmpTdsDetailCreate(model.EmployeeId, model.FiscalYear, model.IsMetro, model.HraExemption, model.TransExemption,
                    //    model.OtherExemption, model.MedBillExemption, model.ChildEduExemption, model.LtaExemption, model.VehiMainExemption, model.HousePropertyIncome, model.HouseLoanInterest,
                    //    model.OtherIncome, model.MedInsurPremium, model.MedInsurPremiumPar, model.MedHandicapDepend, model.MedSpecDisease, model.HighEduLoanRepayment, model.DonateFundCharity,
                    //    model.RentDeduction, model.PermanentDisableDeduction, model.OtherDeduction, model.PensionScheme, model.Nsc, model.Ppf, model.InfraBond, model.ChildEduFund, model.MutualFund,
                    //    model.Fd, model.InterestOnDeposit, model.RoyaltyIncomeDeduction, model.RoyaltyPatentDeduction, model.UniformExemption, model.Pf, model.HouseLoanPrincipalRepay,
                    //    model.InsurancePremium, model.SavingbankInterest, model.SavingbankInterestException, model.Rajivgandhisavingsscheme);

                    int[] columnHide = new[] { 0, 4, 5, 6, 7 };
                    //  DataTable dtEmployeeTdsGet = obj.EmpTdsDetailGet(null);
                    DataTable dtEmployeeTdsGet = null;

                    if (dtEmployeeTdsGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeTdsGet, columnHide);
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
        public ActionResult EmployeeTdsUpdate(EmpTdsDetailViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                //int? status = obj.EmpTdsDetailUpdate(model.EmpTdsDetailId, model.EmployeeId, model.FiscalYear, model.IsMetro, model.HraExemption, model.TransExemption,
                //    model.OtherExemption, model.MedBillExemption, model.ChildEduExemption, model.LtaExemption, model.VehiMainExemption, model.HousePropertyIncome, model.HouseLoanInterest,
                //    model.OtherIncome, model.MedInsurPremium, model.MedInsurPremiumPar, model.MedHandicapDepend, model.MedSpecDisease, model.HighEduLoanRepayment, model.DonateFundCharity,
                //    model.RentDeduction, model.PermanentDisableDeduction, model.OtherDeduction, model.PensionScheme, model.Nsc, model.Ppf, model.InfraBond, model.ChildEduFund, model.MutualFund,
                //    model.Fd, model.InterestOnDeposit, model.RoyaltyIncomeDeduction, model.RoyaltyPatentDeduction, model.UniformExemption, model.Pf, model.HouseLoanPrincipalRepay,
                //    model.InsurancePremium, model.SavingbankInterest, model.SavingbankInterestException, model.Rajivgandhisavingsscheme);

                //int[] columnHide = new[] { 0, 4, 5, 6, 7 };
                //DataTable dtEmployeeTdsGet = obj.EmpTdsDetailGet(null);
                //if (dtEmployeeTdsGet.Rows.Count > 0)
                //{
                //    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeTdsGet, columnHide);
                //    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    html = "<div class='alert alert-danger'>No Record !!</div>";
                //    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                //}
                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeTdsDelete(EmpTdsDetailViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpTdsDetailDelete(model.EmpTdsDetailId);

                int[] columnHide = new[] { 0, 4, 5, 6, 7 };
                //DataTable dtEmployeeTdsGet = obj.EmpTdsDetailGet(null);
                DataTable dtEmployeeTdsGet = null;
                if (dtEmployeeTdsGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeTdsGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        //----Attendance Approval---------
        public ActionResult AttendanceApproval()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                return View();
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult AttendanceApproval(EmpAttendanceEntryViewModel model, string command)
        {
            string PayrollrollTypeIdList = "1";
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int? status = null;
                if (command == "Search")
                {
                    int[] HideColumn = null;
                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {

                        //  int[] HideColumn = new[] { 0, 6, 7, 10, 11, 12, 13, 14, 15 };
                        HideColumn = new[] { 0, 1, 6, 13, 14, 15, 16, 17 };
                    }
                    else
                    {
                        HideColumn = new[] { 0, 1, 6, 7, 9, 10, 13, 14, 15, 16, 17 };

                    }
                    DataSet dtAttendance = obj.EmpAttendanceApprovalGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);

                    if (dtAttendance.Tables.Count > 0)
                    {
                        if (dtAttendance.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableAttendanceApproval(dtAttendance.Tables[0], HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }

                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == "Insert")
                {

                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string EmployeeIdList = "";
                    int n = 0;
                    int? recordInserted = null;

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
                                    if (n == 0)
                                    {
                                        EmployeeIdList = Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                    }
                                    else
                                    {
                                        EmployeeIdList += "," + Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                    }

                                    n = n + 1;

                                }
                            }

                        }


                        if (!(string.IsNullOrEmpty(EmployeeIdList)))
                        {

                            status = obj.EmpAttendanceEntryApproval(model.MonthId, model.Year, EmployeeIdList, PayrollrollTypeIdList, out recordInserted);
                        }






                    }








                    //------------------------work Flow--------------------

                    /*

                                        string AppKey = string.Empty;
                                        string EmpId = string.Empty;
                                        string monthId = string.Empty;
                                        string YearId = string.Empty;
                                        string isGanges = string.Empty;
                                        int? processId = null;
                                        int? Stateid = null;

                                        DataTable DtClient = obj.ClientGangesDeatilsGet();

                                        if (DtClient.Rows.Count > 0)
                                        {


                                            isGanges = Convert.ToString(DtClient.Rows[0][2]);

                                            if (isGanges == "1")
                                            {


                                                if (!((string.IsNullOrEmpty(Convert.ToString(DtClient.Rows[0][3]))) && (string.IsNullOrEmpty(Convert.ToString(DtClient.Rows[0][5])))))
                                                {
                                                    processId = Convert.ToInt32(DtClient.Rows[0][3]);
                                                    Stateid = Convert.ToInt32(DtClient.Rows[0][5]);

                                                    if (model.EmployeeDataList != null)
                                                    {
                                                        int countEmp = model.EmployeeDataList.Count;

                                                        for (int x = 0; x < countEmp; x++)
                                                        {
                                                            checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                                                            IsCheck = Request[checkBoxName];
                                                            if (IsCheck == "on")
                                                            {
                                                                if (model.EmployeeDataList[x].EmployeeId != null)
                                                                {
                                                                    AppKey = string.Empty;
                                                                    EmpId = Convert.ToString(model.EmployeeDataList[x].EmployeeId);

                                                                    // 183~Monthly~1~2016~1~1
                                                                    monthId = Convert.ToString(model.MonthId);
                                                                    YearId = Convert.ToString(model.Year);

                                                                    AppKey = EmpId + "~Monthly~" + monthId + "~" + YearId + "~1~1";
                                                                    try
                                                                    {
                                                                    int? sts = obj.WfActionProcess(AppKey, processId, Stateid);

                                                                    }
                                                                    catch(Exception Ex)
                                                                    {
                                                                        continue;
                                                                    }

                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                            }

                                        }




                    */

























                    int[] HideColumn = null;
                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {

                        //  int[] HideColumn = new[] { 0, 6, 7, 10, 11, 12, 13, 14, 15 };
                        HideColumn = new[] { 0, 6, 13, 14, 15, 16, 17 };
                    }
                    else
                    {
                        HideColumn = new[] { 0, 6, 7, 9, 10, 13, 14, 15, 16, 17 };

                    }
                    DataSet dtEmpAttendance = obj.EmpAttendanceApprovalGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtEmpAttendance.Tables.Count > 0)
                    {
                        if (dtEmpAttendance.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableAttendanceApproval(dtEmpAttendance.Tables[0], HideColumn);
                            if (status == 0)
                            {
                                htmlTable.Append("<div class='alert alert-success'>Attendance Approved Successfully for the Selected Record(s) !!</div>");
                            }
                            return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            html = "<div class='alert alert-success'>Attendance Approved Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }

                    else
                    {
                        html = "<div class='alert alert-success'>Attendance Approved Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }





                if (command == "Unlock")
                {

                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string EmployeeIdList = "";
                    int n = 0;
                    int? recordInserted = null;

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
                                    if (n == 0)
                                    {
                                        EmployeeIdList = Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                    }
                                    else
                                    {
                                        EmployeeIdList += "," + Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                    }

                                    n = n + 1;

                                }
                            }

                        }


                        if (!(string.IsNullOrEmpty(EmployeeIdList)))
                        {

                            status = obj.EmpAttendanceApprovalUnlock(model.MonthId, model.Year, EmployeeIdList, PayrollrollTypeIdList, out recordInserted);
                        }









                        //------------------------work Flow--------------------


                        /*

                                                string AppKey = string.Empty;
                                                string EmpId = string.Empty;
                                                string monthId = string.Empty;
                                                string YearId = string.Empty;
                                                string isGanges = string.Empty;
                                                int? processId = null;
                                                int? Stateid = null;

                                                DataTable DtClient = obj.ClientGangesDeatilsGet();

                                                if (DtClient.Rows.Count > 0)
                                                {


                                                    isGanges = Convert.ToString(DtClient.Rows[0][2]);

                                                    if (isGanges == "1")
                                                    {


                                                        if (!((string.IsNullOrEmpty(Convert.ToString(DtClient.Rows[0][3]))) && (string.IsNullOrEmpty(Convert.ToString(DtClient.Rows[0][6])))))
                                                        {
                                                            processId = Convert.ToInt32(DtClient.Rows[0][3]);
                                                            Stateid = Convert.ToInt32(DtClient.Rows[0][6]);

                                                            if (model.EmployeeDataList != null)
                                                            {
                                                                int countEmp = model.EmployeeDataList.Count;

                                                                for (int x = 0; x < countEmp; x++)
                                                                {
                                                                    checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                                                                    IsCheck = Request[checkBoxName];
                                                                    if (IsCheck == "on")
                                                                    {
                                                                        if (model.EmployeeDataList[x].EmployeeId != null)
                                                                        {
                                                                            EmpId = Convert.ToString(model.EmployeeDataList[x].EmployeeId);

                                                                            // 183~Monthly~1~2016~1~1
                                                                            monthId = Convert.ToString(model.MonthId);
                                                                            YearId = Convert.ToString(model.Year);

                                                                            AppKey = EmpId + "~Monthly~" + monthId + "~" + YearId + "~1~1";
                                                                            int? sts = obj.WfActionProcess(AppKey, processId, Stateid);

                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }

                                                }



                        */















                    }

                    int[] HideColumn = null;
                    DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
                    if (dtWorkUnit.Rows.Count > 0)
                    {

                        //  int[] HideColumn = new[] { 0, 6, 7, 10, 11, 12, 13, 14, 15 };
                        HideColumn = new[] { 0, 6, 13, 14, 15, 16, 17 };
                    }
                    else
                    {
                        HideColumn = new[] { 0, 6, 7, 9, 10, 13, 14, 15, 16, 17 };

                    }
                    DataSet dtEmpAttendance = obj.EmpAttendanceApprovalGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtEmpAttendance.Tables.Count > 0)
                    {

                        if (dtEmpAttendance.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableAttendanceApproval(dtEmpAttendance.Tables[0], HideColumn);
                            if (status == 0)
                            {
                                htmlTable.Append("<div class='alert alert-success'>Attendance Unlock Successfully for the Selected Record(s) !!</div>");
                            }
                            return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            html = "<div class='alert alert-success'>Attendance Unlock Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }

                }

                else
                {
                    html = "<div class='alert alert-success'>Attendance Unlock Successfully for the Selected Record(s) !!</div>";
                    return Json(html, JsonRequestBehavior.AllowGet);
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

        //----Payroll Process---------
        public ActionResult PayrollProcess()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                return View();
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult PayrollProcess(EmpAttendanceEntryViewModel model, string command)
        {

            int? varSalaryExist = 0;
            string varEmpId = "";
            string PayrollrollTypeIdList = "1";
            string html = null;
            string htmlmsg = null;
            string EmpCodeList = "";
            int[] HideColumn = null;
            DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = null;









                if (command == "Search")
                {



                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 1, 2, 13, 14 };
                    }
                    else
                    {

                        HideColumn = new[] { 0, 1, 2, 7, 9, 10, 13, 14 };

                    }

                    DataSet dtPayroll = obj.EmpPayrollProcessGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);

                    if (dtPayroll.Tables.Count > 0)
                    {
                        if (dtPayroll.Tables[0].Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollProcess(dtPayroll.Tables[0], HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }


                }

                if (command == "Insert")
                {

                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string EmployeeIdList = "";
                    int n = 0;

                    string FileName = Request.PhysicalApplicationPath + "bin\\Poise.dll";

                    DataTable dtDate = obj.PayrollStartEndDays(model.MonthId, model.Year);
                    DateTime? payrollStartDate = Convert.ToDateTime(dtDate.Rows[0][1]);
                    bool Echk = CommonUtil.expirationcheck(FileName, payrollStartDate);
                    if ((Echk == false))
                    {
                        html = "<div class='alert alert-danger'>Sorry,Your License was Expired !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }



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
                                    if (n == 0)
                                    {
                                        EmployeeIdList = Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                    }
                                    else
                                    {
                                        EmployeeIdList += "," + Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                    }

                                    n = n + 1;

                                }
                            }

                            else
                            {
                                if (model.EmployeeDataList[x].EmployeeId != null)
                                {

                                    if ((string.IsNullOrEmpty(EmpCodeList)))
                                    {
                                        EmpCodeList = model.EmployeeDataList[x].EmpCode;
                                    }
                                    else
                                    {
                                        EmpCodeList += "," + model.EmployeeDataList[x].EmpCode;
                                    }
                                }

                            }

                        }


                        if (!(string.IsNullOrEmpty(EmployeeIdList)))
                        {

                            status = obj.EmpPayrollProcessGenrate(model.MonthId, model.Year, null, null, EmployeeIdList, PayrollrollTypeIdList, "Payroll", null);
                        }


                    }
                    if (!(string.IsNullOrEmpty(EmpCodeList)))
                    {
                        htmlmsg = "Payroll can not process for Emp Code " + EmpCodeList + " Employees";
                    }


                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 1, 2, 13, 14 };
                    }
                    else
                    {

                        HideColumn = new[] { 0, 1, 2, 7, 9, 10, 13, 14 };

                    }
                    DataSet dtPayroll = obj.EmpPayrollProcessGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);


                    if (dtPayroll.Tables.Count > 0)
                    {
                        if (dtPayroll.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollProcess(dtPayroll.Tables[0], HideColumn);

                            htmlTable.Append("<div class='alert alert-success'>Payroll Generated Successfully for the Selected Record(s) !! " + htmlmsg + "</div>");

                            return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                        }


                        else
                        {
                            html = "<div class='alert alert-success'>Payroll Generated Successfully for the Selected Record(s) !! " + htmlmsg + "</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }





                    }

                    else
                    {
                        html = "<div class='alert alert-success'>Payroll Generated Successfully for the Selected Record(s) !!" + htmlmsg + "</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }





                if (command == "DisApprove")
                {

                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string EmployeeIdList = "";

                    int n = 0;


                    if (model.EmployeeDataList != null)
                    {
                        int count = model.EmployeeDataList.Count;

                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {

                                varSalaryExist = obj.WfEmpVariableSalaryExistsValidate(model.EmployeeDataList[x].EmployeeId, model.MonthId, model.Year);

                                if (varSalaryExist == 1)
                                {



                                    if ((string.IsNullOrEmpty(varEmpId)))
                                    {
                                        varEmpId = model.EmployeeDataList[x].EmpCode + ",";
                                    }
                                    else
                                    {
                                        varEmpId = varEmpId + model.EmployeeDataList[x].EmpCode + ",";
                                    }
                                }


                                if (varSalaryExist == 0)
                                {
                                    if (model.EmployeeDataList[x].EmployeeId != null)
                                    {
                                        if (n == 0)
                                        {
                                            EmployeeIdList = Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                        }
                                        else
                                        {
                                            EmployeeIdList += "," + Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                        }

                                        n = n + 1;

                                    }
                                }
                            }




                        }


                        if (!(string.IsNullOrEmpty(EmployeeIdList)))
                        {



                            status = obj.EmpAttendanceDisapproval(model.MonthId, model.Year, EmployeeIdList, PayrollrollTypeIdList);

                        }


                    }

                    if (dtWorkUnit.Rows.Count > 0)
                    {
                        HideColumn = new[] { 0, 1, 2, 13, 14 };
                    }
                    else
                    {

                        HideColumn = new[] { 0, 1, 2, 7, 9, 10, 13, 14 };

                    }

                    if (!(string.IsNullOrEmpty(varEmpId)))
                    {
                        varEmpId = varEmpId + " employee have Variable Salary Approved !!";
                    }
                    DataSet dtPayroll = obj.EmpPayrollProcessGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                    if (dtPayroll.Tables[0].Rows.Count > 0)
                    {
                        if (dtPayroll.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableAttendanceApproval(dtPayroll.Tables[0], HideColumn);
                            if (status == 0)
                            {
                                htmlTable.Append("<div class='alert alert-success'>Attendance Unlock Successfully for the Selected Record(s) !!</div>");
                            }

                            return Json(htmlTable.ToString() + html + " " + "<div class='alert alert-danger'>" + varEmpId + "</div>", JsonRequestBehavior.AllowGet);

                        }


                        else
                        {
                            html = "<div class='alert alert-success'>Attendance Unlock Successfully for the Selected Record(s) !!+  " + varEmpId + "</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        html = "<div class='alert alert-success'>Attendance Unlock Successfully for the Selected Record(s) !! " + varEmpId + "</div>";
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

        //----Payroll Details---------
        public ActionResult PayrollDetails()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpAttendanceEntryViewModel model = new EmpAttendanceEntryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult PayrollDetailsGet(EmpAttendanceEntryViewModel model)
        {
            DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
             int[] HideColumn = null;
            string PayrollrollTypeIdList = "1";
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                if (dtWorkUnit.Rows.Count > 0)
                {

                    HideColumn = new[] { 0, 1, 5, 6, 16, 17 };
                }
                else
                {

                    HideColumn = new[] { 0, 1, 5, 6, 9, 11, 14, 16, 17 };
                }
              DataSet dtPayroll=ObjML.EmpPayrollDetailsGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId, model.CompanyId);
            //    DataSet dtPayroll = obj.EmpPayrollDetailsGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId);
                if (dtPayroll.Tables.Count > 0)
                {
                    if (dtPayroll.Tables[0].Rows.Count > 0)
                    {

                        DataSet dsPayroll = obj.EmpPayrollSalaryUnpaidGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);


                        int[] HideCol = new[] { 9, 10, 11 };
                        if (dsPayroll.Tables.Count > 0)
                        {
                            DataTable dt = CommonUtil.DataTableColumnRemove(dsPayroll.Tables[0], HideCol);
                            Session.Add("snPayrollDetailGet", dt);
                        }
                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollDetails(dtPayroll.Tables[0], HideColumn);
                        // StringBuilder htmlTable = CommonUtil.htmlTable(dtPayroll.Tables[0], HideColumn);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = Convert.ToString(ex.Message);
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollDetailsDelete(EmpAttendanceEntryViewModel model)
        {
            DataTable dtWorkUnit = obj.EmpTypeWorkunitGet(null, null);
            int[] HideColumn = null;
            string PayrollrollTypeIdList = "1";
            string html = null;
            int? status = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                string EmployeeIdList = "";
                int n = 0;
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

                                status = obj.EmpPayrollDetailsDelete(Convert.ToString(model.EmployeeDataList[x].EmployeeId), model.MonthId, model.Year, PayrollrollTypeIdList.ToString());
                                //if (n == 0)
                                //{
                                //    EmployeeIdList = Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                //}
                                //else
                                //{
                                //    EmployeeIdList += "," + Convert.ToString(model.EmployeeDataList[x].EmployeeId).Trim();
                                //}
                                //n = n + 1;
                            }
                        }
                    }
                    //if (!(string.IsNullOrEmpty(EmployeeIdList)))
                    //{
                    //    status = obj.EmpPayrollDetailsDelete(EmployeeIdList, model.MonthId, model.Year, PayrollrollTypeIdList.ToString());
                    //}
                }
                if (dtWorkUnit.Rows.Count > 0)
                {

                    HideColumn = new[] { 0, 1, 5, 6, 16, 17 };
                }
                else
                {

                    HideColumn = new[] { 0, 1, 5, 6, 9, 11, 14, 17 };
                }
                DataSet dtPayroll = ObjML.EmpPayrollDetailsGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId,model.CompanyId);

               // DataSet dtPayroll = obj.EmpPayrollDetailsGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId);
                if (dtPayroll.Tables.Count > 0)
                {
                    Session["dtPayrollDetails"] = dtPayroll.Tables[0];
                    if (dtPayroll.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollDetails(dtPayroll.Tables[0], HideColumn);
                        if (status == 0)
                        {
                            htmlTable.Append("<div class='alert alert-success'>Payroll Deleted Successfully  for the Selected Record(s) !!</div>");
                        }

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
                    html = "<div class='alert alert-success'>Payroll Deleted Successfully for the Selected Record(s) !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollDetails(EmpAttendanceEntryViewModel model, string command)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }





                if (Session["snPayrollDetailGet"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["snPayrollDetailGet"], "Payroll Detail");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "Payroll Detail", "Y", 4);
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "Payroll Detail", 12);
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "Payroll Detail", 12);
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
                html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";
                return Json(html.ToString(), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult DataExportPDF(GridView GridView1, string FileName, string IsGridLine, int FontSize)
        {

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            if (IsGridLine == "N")
                GridView1.GridLines = GridLines.None;

            GridView1.Font.Size = FontSize;

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

        public ActionResult DataExportWord(GridView GridView1, string FileName, int FontSize)
        {
            GridView1.Font.Size = FontSize;
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

        [HttpPost]
        public ActionResult EmpPayrollItemDetailGet(int? PayrollId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dtEmpSalaryItem = obj.EmpPayrollsalaryItemDetailGet(PayrollId);
                if (dtEmpSalaryItem.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtEmpSalaryItem, columnHide);
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
        public ActionResult PayrollDetailsExport(EmpAttendanceEntryViewModel model)
        {
            // string PayrollrollTypeIdList = "";
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }





                DataSet dtPayroll = obj.EmpPayrollSalaryUnpaidGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);
                if (dtPayroll.Tables.Count > 0)
                {

                    if (dtPayroll.Tables[0].Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableAll(dtPayroll.Tables[0]);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Record";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

                    }
                }






                html = "No Record";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //----Employee Arrear Process ---------
        public ActionResult EmplloyeeArrearProcess()
        {
            string html = null;
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
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }


        //----Employee Arrear Process ---------

        [HttpPost]
        public ActionResult EmplloyeeArrearProcess(PayrollUtil model, string command)
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

                    DataTable dtPayroll = obj.EmpPayrollArearAmountGet(model.ArearMonthId, model.ArearYear, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollArrearItemId, null);
                    int[] HideColumn = { 0, 1 };
                    if (dtPayroll.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollArrearProcess(dtPayroll, HideColumn);
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
                                        // status = obj.EmpPayrollArearAmountUpdate(model.EmpDataList[x].EmployeeId, model.MonthId, model.Year, model.PayrollItemId, model.ItemAmount);
                                        status = obj.EmpPayrollArrearAmountUpdate(model.EmpDataList[x].EmployeeId, model.ArearMonthId, model.ArearYear, model.MonthId, model.Year, model.PayrollArrearItemId, model.ItemAmount, null);
                                    }
                                }
                            }

                        }





                    }




                    DataTable dtPayroll = obj.EmpPayrollArearAmountGet(model.ArearMonthId, model.ArearYear, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollArrearItemId, null);



                    int[] HideColumn = { 0, 1 };
                    if (dtPayroll.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollArrearProcess(dtPayroll, HideColumn);
                        htmlTable.Append("<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>";
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

        //----Employee Variable Salary ---------
        public ActionResult EmployeeVariableSalary()
        {
            string html = null;
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
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }


        //----Employee Variable Salary ---------

        [HttpPost]
        public ActionResult EmployeeVariableSalary(PayrollUtil model, string command)
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
                    // IsCheck = Request["isVariableSalary"];

                    if (model.SalaryItemType == "VariableSalary")
                    {

                        DataTable dtPayroll = obj.EmpPayrollPerformanceAmountGet(model.PerformanceType, model.Quarter, model.EmpCategoryId, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId, model.BonusType);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }



                    }
                    else if (model.SalaryItemType == "SpecialBonus")
                    {
                        model.BonusType = "SpecialBonus";


                        DataTable dtPayroll = obj.EmpPayrollPerformanceAmountGet(model.PerformanceType, model.Quarter, model.EmpCategoryId, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId, model.BonusType);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else if (model.SalaryItemType == "ProfessionalTax")
                    {
                        model.BonusType = "ProfessionalTax";


                        DataTable dtPayroll = obj.EmpPayrollPerformanceAmountGet(model.PerformanceType, model.Quarter, model.EmpCategoryId, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId, model.BonusType);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }

                    else if ((model.SalaryItemType == "DALocal") || (model.SalaryItemType == "DANonLocal"))
                    {


                        DataTable dtPayroll = obj.EmpPayrollDaAmountGet(model.SalaryItemType, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.EmpCategoryId, model.PayrollItemId);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }




                    else
                    {



                        DataTable dtPayroll = obj.EmpPayrollItemAmountGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
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
                                        status = obj.EmpPayrollItemAmountUpdate(model.EmpDataList[x].EmployeeId, model.MonthId, model.Year, model.PayrollItemId, model.ItemAmount, null);
                                    }
                                }
                            }

                        }





                    }


                    if (model.SalaryItemType == "VariableSalary")
                    {

                        DataTable dtPayroll = obj.EmpPayrollPerformanceAmountGet(model.PerformanceType, model.Quarter, model.EmpCategoryId, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId, model.BonusType);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            htmlTable.Append("<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }



                    }
                    else if (model.SalaryItemType == "SpecialBonus")
                    {
                        model.BonusType = "SpecialBonus";


                        DataTable dtPayroll = obj.EmpPayrollPerformanceAmountGet(model.PerformanceType, model.Quarter, model.EmpCategoryId, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId, model.BonusType);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            htmlTable.Append("<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else if (model.SalaryItemType == "ProfessionalTax")
                    {
                        model.BonusType = "ProfessionalTax";


                        DataTable dtPayroll = obj.EmpPayrollPerformanceAmountGet(model.PerformanceType, model.Quarter, model.EmpCategoryId, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId, model.BonusType);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            htmlTable.Append("<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }

                    else if ((model.SalaryItemType == "DALocal") || (model.SalaryItemType == "DANonLocal"))
                    {


                        DataTable dtPayroll = obj.EmpPayrollDaAmountGet(model.SalaryItemType, model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.EmpCategoryId, model.PayrollItemId);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            htmlTable.Append("<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            html = "<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }

                    }




                    else
                    {



                        DataTable dtPayroll = obj.EmpPayrollItemAmountGet(model.MonthId, model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.PayrollItemId);
                        int[] HideColumn = { 0, 6 };
                        if (dtPayroll.Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollVariableSalary(dtPayroll, HideColumn);
                            htmlTable.Append("<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>");
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {


                            html = "<div class='alert alert-success'>Salary Upadted Successfully for the Selected Record(s) !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
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




        //----Payroll Salary Transfer---------
        public ActionResult EmployeeSalaryTransfer()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                return View();
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult EmployeeSalaryTransfer(EmpAttendanceEntryViewModel model, string command)
        {
            string PayrollrollTypeIdList = "1";
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = null;



                if (command == "Search")
                {



                    int[] HideColumn = new[] { 0, 1, 5, 6, 10, 14, 15, 16 };

                    DataSet dtPayroll = ObjML.EmpPayrollDetailsGet(model.MonthId, model.Year, "1", model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId,model.CompanyId);

                   // DataSet dtPayroll = obj.EmpPayrollDetailsGet(model.MonthId, model.Year, "1", model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId);
                    if (dtPayroll.Tables.Count > 0)
                    {
                        if (dtPayroll.Tables[0].Rows.Count > 0)
                        {

                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollSalayTransferDetails(dtPayroll.Tables[0], HideColumn);
                            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(html, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == "Transfer")
                {

                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string PayrollIdList = "";
                    int n = 0;


                    if (model.EmployeeDataList != null)
                    {
                        int count = model.EmployeeDataList.Count;

                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.EmployeeDataList[x].PayrollId != null)
                                {
                                    if (n == 0)
                                    {
                                        PayrollIdList = Convert.ToString(model.EmployeeDataList[x].PayrollId).Trim();
                                    }
                                    else
                                    {
                                        PayrollIdList += "," + Convert.ToString(model.EmployeeDataList[x].PayrollId).Trim();
                                    }

                                    n = n + 1;

                                }
                            }

                        }


                        if (!(string.IsNullOrEmpty(PayrollIdList)))
                        {
                            status = obj.EmpPayrollSalaryLockUpdate(PayrollIdList);
                        }


                    }

                    int[] HideColumn = new[] { 0, 1, 5, 6, 14, 15, 16 };
                    DataSet dtPayroll = ObjML.EmpPayrollDetailsGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId,model.CompanyId);

                  //  DataSet dtPayroll = obj.EmpPayrollDetailsGet(model.MonthId, model.Year, PayrollrollTypeIdList, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null, model.EmpCategoryId);
                    if (dtPayroll.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTablePayrollSalayTransferDetails(dtPayroll.Tables[0], HideColumn);
                        if (status == 0)
                        {
                            htmlTable.Append("<div class='alert alert-success'>Salary  Transfer Successfully  for the Selected Record(s) !!</div>");
                        }
                        return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        html = "<div class='alert alert-success'>Salary  Transfer Successfully for the Selected Record(s) !!</div>";
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









        public ActionResult EmpShift()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpShiftViewModel model = new EmpShiftViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmpShiftGet(EmpShiftViewModel model)
        {
            string[] htmllistsuccess;
            string html = null;
            StringBuilder htmlTable = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = { 0, 1, 2 };
                DataTable dtEmpShiftGet = obj.EmpShiftGet(model.Employee_Id, model.Start_Date, model.End_Date);

                if (dtEmpShiftGet.Rows.Count > 0)
                {
                    for (int j = 0; j < dtEmpShiftGet.Rows.Count; j++)
                    {
                        ListEmpShift.Add(new CollectionListData
                        {
                            Text = dtEmpShiftGet.Rows[j][3].ToString(),
                            Value = dtEmpShiftGet.Rows[j][1].ToString()
                        });

                    }
                    htmlTable = CommonUtil.htmlTableEditMode(dtEmpShiftGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString(), htmllistsuccess = ListEmpShift }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult empshift(EmpShiftViewModel model, string command)
        {
            string IsHistory = string.IsNullOrEmpty(Request["IsHistory"]) ? "off" : "on";

            if (Session["EmpShiftHistory"] != null && IsHistory == "on")
            {
                DataTable dtEmpSalary1 = ObjML.EmpShiftGetHistory(null, null, null);
                //int[] Column = new[] { 3, 4, 5, 6 };
                int[] showcolumn = new[] { 0, 1,2 };// 3, 4, 5, 6, 7, 8, 9 };
                DataTable dt1 = CommonUtil.DataTableColumnRemove(dtEmpSalary1, showcolumn);

                GridView gv1 = GridViewGet(dt1, "Employee Shift Report");

                ActionResult a1 = null;

                if (command == "Pdf")
                {
                    a1 = DataExportPDF(gv1, "Employee Shift");
                }
                if (command == "Excel")
                {
                    a1 = DataExportExcel(gv1, "Employee Shift", 12);
                }
                if (command == "Word")
                {
                    a1 = DataExportWord(gv1, "Employee Shift");
                }
                return a1;
            }
            else {


                DataTable dtEmpShift = obj.EmpShiftGet(model.Employee_Id, model.Start_Date, model.End_Date);
                int[] Column = new[] { 0, 1, 2 };
                DataTable dt = CommonUtil.DataTableColumnRemove(dtEmpShift, Column);

                GridView gv = GridViewGet(dt, "Employee Shift Report");

                ActionResult a = null;

                if (command == "Pdf")
                {
                    a = DataExportPDF(gv, "Employee Shift");
                }
                if (command == "Excel")
                {
                    a = DataExportExcel(gv, "Employee Shift", 12);
                }
                if (command == "Word")
                {
                    a = DataExportWord(gv, "Employee Shift");
                }
                return a;
            }
        }

        [HttpPost]
        public ActionResult EmpShiftHistoryGet(string History)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // int[] showcolumn = new[] { 1, 2, 3, 5, 6, 10, 11, 12, 13, 14, 15, 16, 17 };

                DataTable dtEmpSalary = new DataTable();

                int[] showcolumn = new[] { 3, 4, 5, 6, 7, 8, 9, 10, 16, 17 };
                if (History == "True" || History == "true")
                {
                    dtEmpSalary = ObjML.EmpShiftGetHistory(null, null, null);
                    Session["EmpShiftHistory"] = dtEmpSalary;

                    if (dtEmpSalary.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlNestedTableEditModeEmpshifthistory(showcolumn, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    dtEmpSalary = obj.EmpShiftGet(null, null, null);
                    if (dtEmpSalary.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showcolumn, dtEmpSalary);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Flag = 1, Html = "No Record" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmpShiftCreate(EmpShiftViewModel model)
        {
            string html = null;
            StringBuilder htmlTable = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.Start_Date, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                }


                int? Exist = obj.EmpShiftExistsValidate(model.EmployeeId, model.ShiftId, model.StartDate, model.EndDate, null);
                if (Exist == 0)
                {
                    int? status = obj.EmpShiftCreate(model.EmployeeId, model.ShiftId, model.StartDate, model.EndDate, model.Notes, null);
                    int[] columnHide = { 0, 1, 2 };
                    DataTable dtEmpShiftGet = obj.EmpShiftGet(model.Employee_Id, model.Start_Date, model.End_Date);
                    if (dtEmpShiftGet.Rows.Count > 0)
                    {
                        htmlTable = CommonUtil.htmlTableEditMode(dtEmpShiftGet, columnHide);
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 1, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
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

        public ActionResult EmpShiftUpdate(EmpShiftViewModel model)
        {
            string html = null;
            StringBuilder htmlTable = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.Start_Date, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 1, Html = "Start Date Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                }


                int? Exist = obj.EmpShiftExistsValidate(model.EmployeeId, model.ShiftId, model.StartDate, model.EndDate, model.EmpShiftId);
                if (Exist == 0)
                {

                    int? status = obj.EmpShiftUpdate(model.EmpShiftId, model.EmployeeId, model.ShiftId, model.StartDate, model.EndDate, model.Notes, null);
                    int[] columnHide = { 0, 1, 2 };
                    DataTable dtEmpShiftGet = obj.EmpShiftGet(model.Employee_Id, model.Start_Date, model.End_Date);
                    if (dtEmpShiftGet.Rows.Count > 0)
                    {
                        htmlTable = CommonUtil.htmlTableEditMode(dtEmpShiftGet, columnHide);
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 1, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 2, Html = "Employee Already Exist !" }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmpShiftDelete(EmpShiftViewModel model)
        {
            string html = null;
            StringBuilder htmlTable = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpShiftDelete(model.EmpShiftId);
                int[] columnHide = { 0, 1, 2 };
                DataTable dtEmpShiftGet = obj.EmpShiftGet(model.Employee_Id, model.Start_Date, model.End_Date);
                if (dtEmpShiftGet.Rows.Count > 0)
                {
                    htmlTable = CommonUtil.htmlTableEditMode(dtEmpShiftGet, columnHide);
                    return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult AttendanceEntryDocumentUpload(EmpAttendanceEntryViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumn = new[] { 0 };

                model.Employee_Id = model.hfEmployee_Id;
                model.Year_Id = model.hfYear_Id;
                model.Month_Id = model.hfMonth_Id;
                model.AttendanceSource_Id = model.hfAttendanceSource_Id;

                if (!string.IsNullOrEmpty(model.imageBase64String))
                {
                    string[] base64 = model.imageBase64String.Split(',');
                    model.imageByte = Convert.FromBase64String(base64[1]);
                }
                int? status = obj.AttendanceEntryDocumentUpload(model.Employee_Id, model.Month_Id, model.Year_Id, model.imageName, model.imageByte);
                if (status == 0)
                {
                    return Json(new { Flag = 0, Html = "Upload Attendance Successfully !" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }





        public ActionResult EmployeeSalaryNew()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                ViewModel model = new ViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult EmployeeSalaryNew(ViewModel model, string command, FormCollection frm)
        {
            //if(command== "Insert")
            //{
            //    int count = model.EmployeeDataList.Count;

            //}

            DataSet dsEmpSalary = obj.EmpSalaryDetailsGet(null, null, null, null, null, null, null);
            int[] Column = new[] { 7, 8 };
            DataTable dt = CommonUtil.DataTableColumnRemove(dsEmpSalary.Tables[0], Column);

            GridView gv = GridViewGet(dt, "Employee Salary Report");

            ActionResult a = null;

            if (command == "Pdf")
            {
                a = DataExportPDF(gv, "EmployeeSalary");
            }
            if (command == "Excel")
            {
                a = DataExportExcel(gv, "EmployeeSalary", 12);
            }
            if (command == "Word")
            {
                a = DataExportWord(gv, "EmployeeSalary");
            }
            return a;
        }
    }
    public class Users
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
    public class Emp
    {
        public int EmpId { get; set; }

    }
    public class CollectionListData
    {

        public string Text { get; set; }
        public string Value { get; set; }
    }

}


