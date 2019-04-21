using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MCLSystem;

using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PoiseERP.Areas.Payroll.Controllers
{
    public class BiometricController : Controller
    {
        //
        // GET: /Payroll/Biometric/
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        //----------------- Biometric Group Details -----------------------------

        public ActionResult BMGroup()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                BiometricGroupViewModel model = new BiometricGroupViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult BiometricGroupGet(BiometricGroupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showColumn = new[] { 1, 2 };
                DataTable dt = obj.BmGroupsGet();
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
        public ActionResult BiometricGroupCreate(BiometricGroupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.BmGroupsExistsValidate(model.GroupName, null);
                if (Exist == 0)
                {
                    int? status = obj.BmGroupsCreate(model.GroupName, model.GroupDescription, model.DepartmentId, model.DesignationId, model.EmpTypeId, model.LocationId);
                    int[] showColumn = new[] { 1, 2, 3, 4 };
                    DataTable dt = obj.BmGroupsGet();
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
        public ActionResult BiometricGroupUpdate(BiometricGroupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.BmGroupsExistsValidate(model.GroupName, model.GroupId);
                if (Exist == 0)
                {
                    int? status = obj.BmGroupsUpdate(model.GroupId, model.GroupName, model.GroupDescription, model.DepartmentId, model.DesignationId, model.EmpTypeId, model.LocationId);
                    int[] showColumn = new[] { 1, 2, 3, 4 };
                    DataTable dt = obj.BmGroupsGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Flag = 1, Html = "Group Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BiometricGroupDelete(BiometricGroupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.BmGroupsDelete(model.GroupId);
                int[] showColumn = new[] { 1, 2, 3, 4 };
                DataTable dt = obj.BmGroupsGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //----------------- Biometric User Details -----------------------------
        public ActionResult BMUser()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                BiometricUserViewModel model = new BiometricUserViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult BiometricUserGet(BiometricUserViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showColumn = new[] {  5, 4 };
                DataTable dt = obj.BmUserGet();
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
        public ActionResult BiometricUserCreate(BiometricUserViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.BmUserExistsValidate(model.ParentUserId, model.EmployeeId, null);
                if (Exist == 0)
                {
                    int? status = obj.BmUserCreate(model.ParentUserId, model.EmployeeId, model.GroupId, model.Notes);
                    int[] showColumn = new[] { 1, 5, 4 };
                    DataTable dt = obj.BmUserGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Record Already Exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BiometricUserUpdate(BiometricUserViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.BmUserExistsValidate(model.ParentUserId, model.EmployeeId, model.BmUserId);
                if (Exist == 0)
                {
                    int? status = obj.BmUserUpdate(model.BmUserId, model.ParentUserId, model.EmployeeId, model.GroupId, model.Notes);
                    int[] showColumn = new[] { 1, 5, 4 };
                    DataTable dt = obj.BmUserGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Record Already Exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BiometricUserDelete(BiometricUserViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.BmUserDelete(model.BmUserId);
                int[] showColumn = new[] { 1, 5, 4 };
                DataTable dt = obj.BmUserGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(showColumn, dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //  BMDailyAttendanceEntry
        // --------------- Log Summery -----------------------------------
        public ActionResult BioMetricLogSummery()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                BiometricViewModel model = new BiometricViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult BioMetricLogSummeryGet(BiometricViewModel model)
        {
            string html = null;
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
                /*

                                if (model.SelectList != null)
                                {
                                    int count = model.SelectList.Count;
                                    for (int x = 0; x < count; x++)
                                    {
                                        checkBoxName = "SelectList[" + x + "].Is_Check";
                                        IsCheck = Request[checkBoxName];
                                        if (IsCheck == "on")
                                        {
                                            if (model.SelectList[x].ListValue != null)
                                            {
                                                if (n == 0)
                                                {
                                                    EmployeeIdList = Convert.ToString(model.SelectList[x].ListValue).Trim();
                                                }
                                                else
                                                {
                                                    EmployeeIdList += "," + Convert.ToString(model.SelectList[x].ListValue).Trim();
                                                }

                                                n = n + 1;

                                            }
                                        }

                                    }
                                }
                                */


                DataTable dtLogSummery = obj.BmLogSummaryGet(model.MonthId, model.Year, Convert.ToString(model.EmployeeId), model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId);


                if (dtLogSummery.Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dtLogSummery;
                    StringBuilder htmlTable = CommonUtil.htmlTableAll(dtLogSummery);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }


                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult BioMetricLogSummery(BiometricViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (command == "Excel")
                {

                    DataTable dtLogSummery;
                    if (Session["dtEmployeeLog"] != null)
                    {
                        dtLogSummery = (DataTable)Session["dtEmployeeLog"];




                        GridView gv = GridViewGet(dtLogSummery, "BioMetric LogSummary Report");

                        ActionResult a = null;
                        a = DataExportExcel(gv, "BioMetricLogSummaryReport");
                        return a;
                    }
                }
                if (command == "Word")
                {

                    DataTable dtLogSummery;
                    if (Session["dtEmployeeLog"] != null)
                    {
                        dtLogSummery = (DataTable)Session["dtEmployeeLog"];




                        GridView gv = GridViewGet(dtLogSummery, "BioMetric Log Summary Report");

                        ActionResult a = null;
                        a = DataExportWord(gv, "BioMetricLogSummaryReport");
                        return a;
                    }
                }






                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //--------------------- Employee Log Report --------------------------

        public ActionResult BioMetricEmployeeLog()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                BiometricViewModel model = new BiometricViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult ShiftRoaster(BiometricViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumn = new[] { 0, 7, 8, 9, 10, 11,12,13 };

                DataSet dsEmployeeLog = obj.BmAttendanceRegisterGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);

                if (dsEmployeeLog.Tables[0].Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dsEmployeeLog.Tables[0];
                    StringBuilder htmlTable = CommonUtil.htmlTable(dsEmployeeLog.Tables[0], HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost]
        public ActionResult EmployeeShiftLog(BiometricViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;

                int n = 0;



                int[] HideColumn = new[] { 0,7,8,9,10,11 };

                DataSet dsEmployeeLog = obj.BmAttendanceRegisterGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);

                if (dsEmployeeLog.Tables[0].Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dsEmployeeLog.Tables[0];
                    StringBuilder htmlTable = CommonUtil.htmlTable(dsEmployeeLog.Tables[0], HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult EmployeeLog(BiometricViewModel model)
        {

            string html = null;
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

                DataTable clientDt = obj.ClientGangesDeatilsGet();

                string CompanyName = Convert.ToString(clientDt.Rows[0][0]);
                //--------------MCL payroll------------


                string IsImportAttendance = string.Empty;
                IsImportAttendance = Request["IsImportAttendance"];
                if (IsImportAttendance == "on")
                {
                    int? status = obj.BiometricToPayrollAttendanceUpdate();
                }


                IsCheck = Request["IsCalAttendance"];
                if (IsCheck == "on")
                {


                    int? k = obj.BmAttendanceCalculateUpdate("Job", model.StartDate, model.EndDate, null);
                }


                int[] HideColumn = new[] { 0, 6, 7 };


                if (CompanyName == "MCL")
                {

                    DataSet dsEmployeeLog = obj.BmLogDetailsGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, "LogDetail");

                    if (dsEmployeeLog.Tables[0].Rows.Count > 0)
                    {
                        Session["dtEmployeeLog"] = dsEmployeeLog.Tables[0];
                        StringBuilder htmlTable = CommonUtil.htmlTableAll(dsEmployeeLog.Tables[0]);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                }

                else
                {
                    DataSet dsEmployeeLog = obj.BmLogDetailsGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, "LogDetail");

                    if (dsEmployeeLog.Tables[0].Rows.Count > 0)
                    {
                        Session["dtEmployeeLog"] = dsEmployeeLog.Tables[0];
                        StringBuilder htmlTable = CommonUtil.htmlTable(dsEmployeeLog.Tables[0], HideColumn);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                }


                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult BioMetricDailyAttendanceGet(BiometricViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                string checkBoxName = string.Empty;


                string IsImportAttendance = string.Empty;
                IsImportAttendance = Request["IsImportAttendance"];
                if (IsImportAttendance == "on")
                {
                    int? status = obj.BiometricToPayrollAttendanceUpdate();
                }

                string IsCheck = string.Empty;
                IsCheck = Request["IsCalAttendance"];
                if (IsCheck == "on")
                {
                    // int? s = obj.BmAttendanceWorkhoursUpdate("Job", model.StartDate, model.EndDate, null);  //for shift 

                    int? k = obj.BmAttendanceCalculateUpdate("Job", model.StartDate, model.EndDate, null);  //for Rohtak chain client
                }


                //    int[] HideColumn = new[] { 0};// for shift
                int[] HideColumn = new[] { 0, 6, 7 };
                DataSet dsEmployeeLog = obj.BmLogDetailsGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, "DailyAttendance");

                if (dsEmployeeLog.Tables[0].Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dsEmployeeLog.Tables[0];
                    StringBuilder htmlTable = CommonUtil.htmlTable(dsEmployeeLog.Tables[0], HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult BioMetricAttendanceAbsentGet(BiometricViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                int n = 0;
                int[] HideColumn = new[] { 0,7,8,9,11 ,14};
                DataSet dsEmployeeLog = obj.BmAttendanceRegisterGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);
                 var results = from myRow in dsEmployeeLog.Tables[0].AsEnumerable()  where myRow.Field<string>("Remarks") == "Absent" select myRow;
                DataTable dtnew = results.CopyToDataTable();
                if (dtnew.Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dtnew;
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtnew, HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult BioMetricAttendancePresentGet(BiometricViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                List<string> lst = new List<string>(new string[] { "IN", "OUT"});
                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                int n = 0;
                int[] HideColumn = new[] { 0,14 };
                DataSet dsEmployeeLog = obj.BmAttendanceRegisterGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);
                //var results = (from myRow in dsEmployeeLog.Tables[0].AsEnumerable() where myRow.Field<string>("InOutDetails") == "<b>IN-</b>08:00:00, <b>OUT-</b>18:15:00  , " select myRow);
                //var item =from a in dsEmployeeLog.Tables[0].AsEnumerable() from b in lst where a.Field<string>("InOutDetails").ToUpper().Contains(b.ToUpper()) select a;
                DataTable dtnew = dsEmployeeLog.Tables[0].Select("Workday = '1.00'").CopyToDataTable();
                if (dtnew.Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dtnew;
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtnew, HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult BioMetricAttendanceRegisterGet(BiometricViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;

                int n = 0;



                int[] HideColumn = new[] { 0 };

                DataSet dsEmployeeLog = obj.BmAttendanceRegisterGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);

                if (dsEmployeeLog.Tables[0].Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dsEmployeeLog.Tables[0];
                    StringBuilder htmlTable = CommonUtil.htmlTable(dsEmployeeLog.Tables[0], HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult BioMetricEmployeeLog(BiometricViewModel model, string command)
        {
           System.Collections.Generic.List<MyObject> collection = new List<MyObject>();
            string html = null;
            var TotalTime = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataTable dtEmployeeLog = new DataTable();

                if (command == "Excel")
                {
                    if (Session["dtEmployeeLog"] != null)
                    {
                        dtEmployeeLog = (DataTable)Session["dtEmployeeLog"];
                        for (int i = 0; i < dtEmployeeLog.Rows.Count; i++)
                        {
                            if (!DBNull.Value.Equals(dtEmployeeLog.Rows[i][18]))
                            {
                                Char delimiter = ':';
                                String[] substrings = dtEmployeeLog.Rows[i][18].ToString().Split(delimiter);
                                MyObject mb = new MyObject();
                                mb.TheDuration = new TimeSpan(Convert.ToInt32(substrings[0]), Convert.ToInt32(substrings[1]), Convert.ToInt32("00"));
                                collection.Add(mb);
                            }

                        }
                         TotalTime = (DateTime.Now.Date.AddMinutes((from r in collection select r.TheDuration.TotalMinutes).Sum())).ToString("h:mm:ss");
                        int[] Column = new[] { 0, 6 };
                        DataTable dt = CommonUtil.DataTableColumnRemove(dtEmployeeLog, Column);
                        GridView gv = GridViewGet1(dt, "Employee Log Report", TotalTime);
                        ActionResult a = null;
                        a = DataExportExcel(gv, "EmployeeLogReport");
                        return a;
                    }
                }
                if (command == "Word")
                {
                    if (Session["dtEmployeeLog"] != null)
                    {
                        dtEmployeeLog = (DataTable)Session["dtEmployeeLog"];

                        int[] Column = new[] { 0, 6 };
                        DataTable dt = CommonUtil.DataTableColumnRemove(dtEmployeeLog, Column);

                        GridView gv = GridViewGet1(dt, "Employee Log Report", TotalTime);

                        ActionResult a = null;
                        a = DataExportWord(gv, "EmployeeLogReport");
                        return a;
                    }
                }






                return View();

            }
            catch (Exception ex)
            {

                return View();
            }
        }
        public GridView GridViewGet1(DataTable dt, string ReportHeader,string _time)
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


            HeaderCell2.Text = CompanyName + "<br />" + CompanyAddress + "<br />" + CompanyCity + " " + PinCode + "<br />" + ReportHeader + "<br />" + _time;
            HeaderCell2.Text = "<table><tr style='text-align:center'><td style='column-span:4'><h2>" + CompanyName + "</h2></td></tr><tr style='text-align:center'><td style='column-span:4'><h2>" + CompanyAddress + "</h2></td></tr><tr style='text-align:center'><td style='column-span:4'><h3>" + ReportHeader + "</h3></td></tr><tr style='text-align:center'><td style='column-span:4'<h2>>Month Of  : June</h2></td></tr><tr><td><b>Overtime </b>:" + _time + "</td></tr></table>";

            int colsan = dt.Columns.Count;
            HeaderCell2.ColumnSpan = colsan;
            HeaderRow.Cells.Add(HeaderCell2);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

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
            GridView1.Font.Size = 9;
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
        public ActionResult EmployeeInfoGetDetail()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable dt = obj.EmployeeInfoGet();
                StringBuilder htmlTable = PayrollUtil.EmployeeDropDownListWithCheckBox(dt);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        // --------------- Daily Attendance Entry -----------------------------------
        public ActionResult BMDailyAttendanceEntry()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                BMDailyAttendanceEntryViewModel model = new BMDailyAttendanceEntryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult BMDailyAttendanceEntry(BMDailyAttendanceEntryViewModel model, string command)
        {

            string html = null;
            string employeeCode = null;
            try
            {
                int? status = null;
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                if (command == "Search")
                {
                    Session["Search"] = "Search";
                    int[] HideColumn = new[] { 0,8,10,11 };
                    DataTable dtAttendance = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);
                    if (dtAttendance.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableBMDailyAttendanceEntry(dtAttendance, HideColumn);
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == "BulkSearch")
                {
                    Session["Search"] = "BulkSearch";

                    int[] HideColumn = new[] {0};
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


                if (command == "Insert")
                {
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;



                    if (model.EmployeeDataList != null)
                    {

                        int count = model.EmployeeDataList.Count;
                        for (int x = 0; x < count; x++)
                        {

                            checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.EmployeeDataList[x].BMUserId != null)
                                {
                                    if ((model.EmployeeDataList[x].InTime != null) || (model.EmployeeDataList[x].OutTime != null))
                                    {

                                        if (Convert.ToString(Session["Search"]) == "BulkSearch")
                                        {
                                            int? sts = obj.BiometricAttendanceUpdate(
                                                model.EmployeeDataList[x].BMUserId,
                                                model.EmployeeDataList[x].AttendanceDate, 
                                                model.EmployeeDataList[x].InTime, 
                                                model.EmployeeDataList[x].OutTime,
                                                null,
                                                model.EmployeeDataList[x].Remarks);

                                        }
                                        else
                                        {

                                            int? sts = obj.BiometricAttendanceUpdate(model.EmployeeDataList[x].BMUserId, model.AttendanceDate, model.EmployeeDataList[x].InTime, model.EmployeeDataList[x].OutTime, null, model.EmployeeDataList[x].Remarks);
                                        }
                                    }
                                    else
                                    {
                                        //if ((model.EmployeeDataList[x].InTime == null) || (model.EmployeeDataList[x].OutTime == null))
                                        //{

                                        //    if (Convert.ToString(Session["Search"]) == "BulkSearch")
                                        //    {
                                        //        int? sts = obj.BiometricAttendanceUpdate(model.EmployeeDataList[x].BMUserId, model.EmployeeDataList[x].AttendanceDate,null,null, null, model.EmployeeDataList[x].Remarks);

                                        //    }
                                        //    else
                                        //    {

                                        //        int? sts = obj.BiometricAttendanceUpdate(model.EmployeeDataList[x].BMUserId, model.AttendanceDate, null, null, null, model.EmployeeDataList[x].Remarks);
                                        //    }
                                        //}

                                        employeeCode = employeeCode + ',' + x + 1;
                                    }
                                }
                            }
                        }



                    }

                    if (Session["Search"] != null)
                    {


                        if (Convert.ToString(Session["Search"]) == "Search")
                        {


                            int[] HideColumn = new[] { 0, 8 };
                            DataTable dtAttendanceTable = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);


                            if (dtAttendanceTable.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableBMDailyAttendanceEntry(dtAttendanceTable, HideColumn);

                                htmlTable.Append("<div class='alert alert-success'>Attendance Inserted Successfully  for the Selected Record(s) !!</div>");
                                return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);

                            }

                            else
                            {
                                html = "<div class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) !!</div>";
                                return Json(html, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (Convert.ToString(Session["Search"]) == "BulkSearch")
                        {

                            int[] HideColumn = new[] { 0};
                            DataTable dtAttendance = obj.BmDailyAttendanceBulkGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.MonthId, model.Year);
                            if (dtAttendance.Rows.Count > 0)
                            {

                                StringBuilder htmlTable = CommonUtil.htmlTableBMBulkAttendanceEntry(dtAttendance, HideColumn);
                                htmlTable.Append("<div class='alert alert-success'>Attendance Inserted Successfully  for the Selected Record(s) !!</div>");
                                return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                            }
                            else
                            {

                                html = "<div class='alert alert-success'>Attendance Inserted Successfully for the Selected Record(s) !!</div>";
                                return Json(html, JsonRequestBehavior.AllowGet);
                            }





                        }



                    }











                }

                if (command == "Delete")
                {
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;



                    if (model.EmployeeDataList != null)
                    {

                        int count = model.EmployeeDataList.Count;
                        for (int x = 0; x < count; x++)
                        {

                            checkBoxName = "EmployeeDataList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.EmployeeDataList[x].BMUserId != null)
                                {

                                    if (Convert.ToString(Session["Search"]) == "BulkSearch")
                                    {
                                        int? sts = obj.BiometricAttendanceDelete(model.EmployeeDataList[x].BMUserId, model.EmployeeDataList[x].AttendanceDate);
                                    }
                                    else
                                    {
                                        int? sts = obj.BiometricAttendanceDelete(model.EmployeeDataList[x].BMUserId, model.AttendanceDate);
                                    }
                                }
                            }
                        }



                    }







                    if (Session["Search"] != null)
                    {


                        if (Convert.ToString(Session["Search"]) == "Search")
                        {



                            int[] HideColumn = new[] { 0, 8 };
                            DataTable dtAttendanceTable = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);


                            if (dtAttendanceTable.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableBMDailyAttendanceEntry(dtAttendanceTable, HideColumn);

                                htmlTable.Append("<div class='alert alert-success'>Attendance deleted Successfully  for the Selected Record(s) !!</div>");
                                return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);

                            }

                            else
                            {
                                html = "<div class='alert alert-success'>Attendance deleted Successfully for the Selected Record(s) !!</div>";
                                return Json(html, JsonRequestBehavior.AllowGet);
                            }


                        }
                        if (Convert.ToString(Session["Search"]) == "BulkSearch")
                        {

                            int[] HideColumn = new[] { 0 };
                            DataTable dtAttendance = obj.BmDailyAttendanceBulkGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.MonthId, model.Year);
                            if (dtAttendance.Rows.Count > 0)
                            {

                                StringBuilder htmlTable = CommonUtil.htmlTableBMBulkAttendanceEntry(dtAttendance, HideColumn);
                                htmlTable.Append("<div class='alert alert-success'>Attendance deleted Successfully  for the Selected Record(s) !!</div>");
                                return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                            }
                            else
                            {

                                html = "<div class='alert alert-success'>Attendance deleted Successfully for the Selected Record(s) !!</div>";
                                return Json(html, JsonRequestBehavior.AllowGet);
                            }





                        }



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




        //---------------------------Employee shift upload--------------

        public ActionResult EmployeeShift()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmployeeShiftModel model = new EmployeeShiftModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
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
        public GridView GridViewGet(DataTable dt)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            return GridView1;
        }
        public ActionResult DataExportToExcel(GridView GridView1, string FileName)
        {


            try
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
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult EmployeeShift(EmployeeShiftModel model, string command)
        {

            string msg = "0";
            ActionResult a = null;
            StringBuilder htmlTable = new StringBuilder();
            string ConStr = "";
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
                    Session["EmployeeShift"] = null;



                    DataTable dsEmployee = obj.EmployeeDetailsGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, null, null);


                    GridView gv = GridViewGet(dsEmployee);
                    a = DataExportToExcel(gv, "EmployeeShift");


                    return a;
                }


                if (command == "UploadFile")
                {
                    try
                    {
                        Session["EmployeeShift"] = null;

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

                        //DataTable dt1 = CommonUtil.DataTableColumnRemove(dt, HideColumn);




                        // dt =   READExcel(path);;

                        if (dt.Rows.Count > 0)
                        {
                            Session.Add("EmployeeShift", dt);
                        }







                        return View(model);
                    }
                    catch (Exception ex)
                    {

                        html = ex.Message.ToString();
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }


                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString() + msg + " out";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }
        public DataTable READExcel(string path)
        {

            //Instance reference for Excel Application

            Microsoft.Office.Interop.Excel.Application objXL = null;

            //Workbook refrence

            Microsoft.Office.Interop.Excel.Workbook objWB = null;

            DataTable dt = new DataTable();

            try
            {

                //Instancing Excel using COM services

                objXL = new Microsoft.Office.Interop.Excel.Application();

                //Adding WorkBook

                objWB = objXL.Workbooks.Open(path);



                //   foreach (Microsoft.Office.Interop.Excel.Worksheet objSHT in objWB.Worksheets)
                {
                    Microsoft.Office.Interop.Excel.Worksheet objSHT = objWB.Worksheets[1];
                    int rows = objSHT.UsedRange.Rows.Count;

                    int cols = objSHT.UsedRange.Columns.Count;



                    int noofrow = 1;



                    //If 1st Row Contains unique Headers for datatable include this part else remove it

                    //Start

                    for (int c = 1; c <= cols; c++)
                    {

                        string colname = objSHT.Cells[1, c].Text;

                        dt.Columns.Add(colname);

                        noofrow = 2;

                    }

                    //END



                    for (int r = noofrow; r <= rows; r++)
                    {

                        DataRow dr = dt.NewRow();

                        for (int c = 1; c <= cols; c++)
                        {

                            dr[c - 1] = objSHT.Cells[r, c].Text;

                        }

                        dt.Rows.Add(dr);

                    }





                }



                //Closing workbook

                objWB.Close();

                //Closing excel application

                objXL.Quit();

                return dt;

            }

            catch (Exception ex)
            {

                objWB.Saved = true;

                //Closing work book

                objWB.Close();

                //Closing excel application

                objXL.Quit();
                return dt;
                //Response.Write("Illegal permission");

            }

        }
        [HttpPost]
        public ActionResult FillGrid(EmployeeShiftModel model)
        {
            StringBuilder htmlTable = new StringBuilder();

            int[] HideColumn = new[] { 0, 7, 8, 9, 12, 15, 16 };
            string html = null;

            if (Session["EmployeeShift"] != null)
            {
                DataTable dt = (DataTable)Session["EmployeeShift"];
                if (dt.Rows.Count > 0)
                {
                    htmlTable = CommonUtil.htmlTableEmployeeShift(dt, obj);


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



            html = "No Records !!";
            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult EmployeeShiftUpdate(EmployeeShiftModel model)
        {

            StringBuilder htmlTable = new StringBuilder();

            string html = null;
            try
            {



                if (Session["EmployeeShift"] == null)
                {
                    html = "File does not exist !";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = (DataTable)Session["EmployeeShift"];

                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        int? employee_id = obj.EmployeeIdExitsValidate(null, Convert.ToString(dt.Rows[i][0]));
                        if ((employee_id == null) || (employee_id == 0))
                        {
                            continue;
                        }
                        else
                        {
                            var attendanceType = Convert.ToString(dt.Rows[i][3]);
                            if (attendanceType.ToUpper() == "ROSTER")
                            {
                                for (int col = 4; col < dt.Columns.Count; col++)
                                {
                                    var colShiftDate = dt.Columns[col].ColumnName;

                                    var colShiftTime = Convert.ToString(dt.Rows[i][col]).Trim();
                                    var notes = "";

                                    if (string.IsNullOrEmpty(colShiftDate))
                                    {
                                        continue;
                                    }
                                    if (string.IsNullOrEmpty(colShiftTime))
                                    {
                                        continue;
                                    }

                                    //if ((colShiftTime == "P") || (colShiftTime == "W") || (colShiftTime == "UPL") || (colShiftTime == "ML") || (colShiftTime == "LOP"))
                                    //{
                                    //    continue;
                                    //}
                                    DateTime datetime;
                                    if (DateTime.TryParseExact(colShiftDate, "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                                    {

                                    }
                                    else if (DateTime.TryParseExact(colShiftDate, "mm-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                                    {

                                    }
                                    else if (DateTime.TryParseExact(colShiftDate, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                                    {

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                    TimeSpan timespan;

                                    TimeSpan? endTime = null;

                                    if (TimeSpan.TryParse(colShiftTime, out timespan))
                                    {
                                        endTime = TimeSpan.Parse(colShiftTime);
                                    }
                                    else
                                    {
                                        if ((colShiftTime == "W") || (colShiftTime == "UPL") || (colShiftTime == "P") || (colShiftTime == "ML") || (colShiftTime == "PL") || (colShiftTime == "Resigned") || (colShiftTime == "Terminated") || (colShiftTime == "LOP") || (colShiftTime == "") || (colShiftTime == null))
                                        {
                                            notes = colShiftTime;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }


                                    DateTime shiftDate = Convert.ToDateTime(colShiftDate);

                                    int? Exist = obj.EmpShiftExistsValidate(employee_id, null, shiftDate, shiftDate, null);
                                    if (Exist == 0)
                                    {
                                        int? r1 = obj.EmpShiftCreate(employee_id, null, shiftDate, shiftDate, notes, endTime);
                                    }
                                    else
                                    {

                                        int? status = obj.EmpShiftUpdate(null, employee_id, null, shiftDate, shiftDate, notes, endTime);
                                    }


                                }







                            }
                        }


                    }


                    //  end 
                    Session["EmployeeShift"] = null;

                    return Json(new { Flag = 0, Html = "<div class='alert alert-success'>Record Updated Successfully !!</div>" }, JsonRequestBehavior.AllowGet);


                }


                html = "No Records !!";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
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


        //Inport Biometric

        public ActionResult ImportBiometric()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmployeeShiftModel model = new EmployeeShiftModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ImportBiometricUpdate(EmployeeShiftModel1 model)
        {
            string[] timevalue = null;
            StringBuilder htmlTable = new StringBuilder();
            int? Month = null;
            int? Year = null;
            string html = null;
            try
            {
                if (Session["EmployeeShift"] == null)
                {
                    html = "File does not exist !";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }

                DataTable dt = (DataTable)Session["EmployeeShift"];
                int totalcolumn = dt.Columns.Count;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        int? employee_id = obj.EmployeeIdExitsValidate(null, Convert.ToString(dt.Rows[i][0]));
                        if ((employee_id == null) || (employee_id == 0))
                        {
                            continue;
                        }
                        else
                        {


                            var checkdate = dt.Columns[4].ColumnName;
                            if (!(string.IsNullOrEmpty(checkdate)))
                            {
                                DateTime checkdateindt = Convert.ToDateTime(checkdate);
                                Month = Convert.ToDateTime(checkdate).Month;
                                Year = Convert.ToDateTime(checkdate).Year;
                                totalcolumn = obj.DaysInMonth(Month, Year);

                            }


                            for (int col = 2; col <= totalcolumn + 1; col++)
                            {


                                var colShiftDate = dt.Columns[col].ColumnName;


                                var colShiftTime = Convert.ToString(dt.Rows[i][col]).Trim();

                                if (colShiftTime.ToUpper() == "W" || colShiftTime.ToUpper() == "S" || colShiftTime.ToUpper() == "H" || colShiftTime.ToUpper() == "L")
                                {

                                    continue;
                                }

                                if (!string.IsNullOrEmpty(colShiftTime) && colShiftTime.Contains("/"))
                                {

                                    timevalue = colShiftTime.Split('/');

                                }
                                else
                                {
                                    timevalue[0] = colShiftTime;

                                }
                                string intime = timevalue[0];
                                string outime = timevalue[1];


                                var notes = "";

                                if (string.IsNullOrEmpty(colShiftDate))
                                {
                                    continue;
                                }
                                if (string.IsNullOrEmpty(colShiftTime))
                                {
                                    continue;
                                }

                                DateTime datetime;
                                if (DateTime.TryParseExact(colShiftDate, "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                                {

                                }
                                else if (DateTime.TryParseExact(colShiftDate, "mm-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                                {

                                }
                                else if (DateTime.TryParseExact(colShiftDate, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                                {

                                }
                                else
                                {
                                    continue;
                                }
                                TimeSpan timespan;

                                TimeSpan? intimeval = null;
                                TimeSpan? outtimval = null;
                                if (TimeSpan.TryParse(intime, out timespan))
                                {
                                    intimeval = TimeSpan.Parse(intime);
                                }

                                if (TimeSpan.TryParse(outime, out timespan))
                                {
                                    outtimval = TimeSpan.Parse(outime);
                                }

                                DateTime AttendanceDate = Convert.ToDateTime(colShiftDate);
                                int? sts = obj.BiometricImportAttendanceUpdate(employee_id, null, null, AttendanceDate, intimeval, outtimval, null, notes);

                            }

                        }
                    }

                    Session["EmployeeShift"] = null;

                    return Json(new { Flag = 0, Html = "<div class='alert alert-success'>Record Updated Successfully !!</div>" }, JsonRequestBehavior.AllowGet);


                }


                html = "No Records !!";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ImportBiometric(EmployeeShiftModel model, string command)
        {

            string msg = "0";
            ActionResult a = null;
            StringBuilder htmlTable = new StringBuilder();
            string ConStr = "";
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
                    try
                    {
                        Session["EmployeeShift"] = null;

                        string filePaths1 = Request.PhysicalApplicationPath + "Report\\Biometric\\ImportBiometric.xls";

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


                if (command == "UploadFile")
                {
                    try
                    {
                        //Session["EmployeeShift"] = null;

                        //string filePaths = Request.PhysicalApplicationPath + "Report\\Biometric\\ImportBiometricUpload.xls";

                        //System.IO.File.Delete(filePaths);

                        //string fname = Path.GetFileName(model.filename.FileName);




                        //DataTable DbTableColumn = new DataTable();





                        //string ext = Path.GetExtension(model.filename.FileName).ToLower();

                        //string path = model.filename.FileName;

                        //html = path;
                        //var path1 = Path.Combine(Request.PhysicalApplicationPath + "Report\\UploadFile\\",
                        //                                        System.IO.Path.GetFileName(fname));
                        //model.filename.SaveAs(path1);




                        OleDbConnection conn = null;

                        //try
                        //{

                        //    if (ext.Trim() == ".xls")
                        //    {
                        //        ConStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        //    }
                        //    else if (ext.Trim() == ".xlsx")
                        //    {
                        //        ConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //    }
                        //    conn = new OleDbConnection(ConStr);

                        //    if (conn.State == ConnectionState.Closed)
                        //    {
                        //        conn.Open();
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    html = ex.Message.ToString();
                        //    return Json(html, JsonRequestBehavior.AllowGet);

                        //}
                        DataTable dtresult = null;
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

                        //DataTable dtExcelSchema;
                        //dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();



                        //string query = "SELECT * FROM [" + SheetName + "]";
                        //conn.Close();

                        //OleDbCommand cmd = new OleDbCommand(query, conn);
                        //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        //DataTable dt = new DataTable();
                        //da.Fill(dt);

                        if (dtresult.Rows.Count > 0)
                        {
                            Session.Add("EmployeeShift", dtresult);
                        }







                        return View(model);
                    }
                    catch (Exception ex)
                    {

                        html = ex.Message.ToString();
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }


                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString() + msg + " out";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }
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
        public ActionResult ImportBiometricFillGrid(EmployeeShiftModel model)
        {
            StringBuilder htmlTable = new StringBuilder();

            int[] HideColumn = new[] { 0, 7, 8, 9, 12, 15, 16 };
            string html = null;

            if (Session["EmployeeShift"] != null)
            {
                DataTable dt = (DataTable)Session["EmployeeShift"];
                if (dt.Rows.Count > 0)
                {
                    htmlTable = CommonUtil.htmlTableImportBiometric(dt, obj);


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



            html = "No Records !!";
            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult BMSwipeReports(BiometricViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumn = new[] { 0};

                //DataSet dsEmployeeLog = obj.BmAttendanceRegisterGet(model.StartDate, model.EndDate, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, null);
                DataTable dtAttendance = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.StartDate);
                if (dtAttendance.Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dtAttendance;
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtAttendance, HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }

            //string html = null;
            //string employeeCode = null;
            //try
            //{
            //    int? status = null;
            //    if (Session["userName"] == null)
            //    {
            //        return Redirect("~/Home/Login");
            //    }               
            //        int[] HideColumn = new[] { 0, 8 };
            //        DataTable dtAttendance = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);
            //        if (dtAttendance.Rows.Count > 0)
            //        {

            //            StringBuilder htmlTable = CommonUtil.htmlTableBMDailyAttendanceEntry(dtAttendance, HideColumn);
            //            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
            //        }
            //        else
            //        {

            //            html = "<div class='alert alert-danger'>No Record !!</div>";
            //            return Json(html, JsonRequestBehavior.AllowGet);
            //        }              

            //    //if (command == "BulkSearch")
            //    //{
            //    //    Session["Search"] = "BulkSearch";

            //    //    int[] HideColumn = new[] { 0, 11 };
            //    //    DataTable dtAttendance = obj.BmDailyAttendanceBulkGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.MonthId, model.Year);
            //    //    if (dtAttendance.Rows.Count > 0)
            //    //    {

            //    //        StringBuilder htmlTable = CommonUtil.htmlTableBMBulkAttendanceEntry(dtAttendance, HideColumn);
            //    //        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
            //    //    }
            //    //    else
            //    //    {

            //    //        html = "<div class='alert alert-danger'>No Record !!</div>";
            //    //        return Json(html, JsonRequestBehavior.AllowGet);
            //    //    }
            //    //}
            //    html = "<div class='alert alert-danger'>No Record !!</div>";
            //    return Json(html, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

            //    return Json(html, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpPost]
        public ActionResult BMSwipeMonthlyReports(BiometricViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumn = new[] { 0 };
                String sDate = model.StartDate.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                int dy = Convert.ToInt32(datevalue.Day.ToString());
                int mn = Convert.ToInt32(datevalue.Month.ToString());
                int yy = Convert.ToInt32(datevalue.Year.ToString());
                DataTable dtAttendance = obj.BmDailyAttendanceBulkGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, mn, yy);
               // DataTable dtAttendance = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.StartDate);
                if (dtAttendance.Rows.Count > 0)
                {
                    Session["dtEmployeeLog"] = dtAttendance;
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtAttendance, HideColumn);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
                }

                html = "No Record !!";
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }

            //string html = null;
            //string employeeCode = null;
            //try
            //{
            //    int? status = null;
            //    if (Session["userName"] == null)
            //    {
            //        return Redirect("~/Home/Login");
            //    }               
            //        int[] HideColumn = new[] { 0, 8 };
            //        DataTable dtAttendance = obj.BmDailyAttendanceGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.AttendanceDate);
            //        if (dtAttendance.Rows.Count > 0)
            //        {

            //            StringBuilder htmlTable = CommonUtil.htmlTableBMDailyAttendanceEntry(dtAttendance, HideColumn);
            //            return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
            //        }
            //        else
            //        {

            //            html = "<div class='alert alert-danger'>No Record !!</div>";
            //            return Json(html, JsonRequestBehavior.AllowGet);
            //        }              

            //    //if (command == "BulkSearch")
            //    //{
            //    //    Session["Search"] = "BulkSearch";

            //    //    int[] HideColumn = new[] { 0, 11 };
            //    //    DataTable dtAttendance = obj.BmDailyAttendanceBulkGet(model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.MonthId, model.Year);
            //    //    if (dtAttendance.Rows.Count > 0)
            //    //    {

            //    //        StringBuilder htmlTable = CommonUtil.htmlTableBMBulkAttendanceEntry(dtAttendance, HideColumn);
            //    //        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
            //    //    }
            //    //    else
            //    //    {

            //    //        html = "<div class='alert alert-danger'>No Record !!</div>";
            //    //        return Json(html, JsonRequestBehavior.AllowGet);
            //    //    }
            //    //}
            //    html = "<div class='alert alert-danger'>No Record !!</div>";
            //    return Json(html, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

            //    return Json(html, JsonRequestBehavior.AllowGet);
            //}
        }


    }
    public class MyObject
    {
        public TimeSpan TheDuration { get; set; }
    }
}
