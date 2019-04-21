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
using System.Net.Mail;

namespace PoiseERP.Areas.Payroll.Controllers
{
    public class LeaveController : Controller
    {
        //
        // GET: /Payroll/Leave/
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel objML = new PoisePayrollManliftServiceModel();
        public ActionResult EmployeeleaveType()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpLeaveTypeViewModel model = new EmpLeaveTypeViewModel();

            return View(model);
        }
        public List<CollectionListData> ListEmpShift = new List<CollectionListData>();
        [HttpPost]
        public ActionResult EmployeeInfoGet(int? employeeId)
        {
            string[] htmllistsuccess;
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
                return Json(new { Flag = 1, Html = html, htmllist = ListEmpShift }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EmpLeaveTypeGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //   emp_leave_type_id 1 ,ELT.leave_type_id 2,leave_request_type 3,ELT.employee_id 4,ELT.emp_type_id 5, D.designation_id 6,ELT.location_id 7,ELT.project_id 8,

                //accrual_period  15,accrual_period_begin 16,accrual_period_count 17,leave_unit 18,leave_per_accrual_period  19   
                //,accrual_pool_leave_type_id 20,max_age_in_months 21,pay_rate_fraction 22,leave_encashable 23 ,min_leave_usage 24,max_leave_usage 25,leave_usage_restriction_period 26,
                //is_earned_leave 27,max_days_month 28, max_days_year 29,auto_encash,encashable_pay_rate 30,start_dt 31,ELT.end_dt 32,is_calculate_from_doj 33 
                int[] collumnHide = { 0, 1, 2, 3, 4, 5, 6, 7, 9, 15, 19, 20, 21, 22, 23, 25, 29, 30, 35, 36, 37, 38, 39 };

                DataTable dtEmpLeaveType = obj.EmpLeaveTypeGet(null);
                if (dtEmpLeaveType.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpLeaveType, collumnHide);
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
        public ActionResult EmpLeaveTypeCreate(EmpLeaveTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                string IsCheck = string.Empty;
                IsCheck = Request["IsCalculateFromDoj"];
                if (IsCheck == "on")
                {
                    model.IsCalculateFromDoj = true;
                }


                string IsIncludeHoilday = Request["IsIncludeHoilday"];
                if (IsIncludeHoilday == "on")
                {
                    model.IsIncludeHoilday = true;
                }
                model.PayRateFraction = 1;

                int? Exist = obj.EmpLeaveTypeExistsValidate(model.LeaveTypeId, model.EmployeeId, model.EmpTypeId, model.DesginationId, model.LocationId, model.ProjectId, model.DepartmentId, model.LeaveRequestType, null, model.LeaveTypeYear, model.AccrualPeriodBegin, model.AccrualPeriodCount,model.StartDt,model.EndDt);
                if (Exist == 10)
                {
                    return Json(new { Flag = 1, Html = "Department dates are not exists in  Leave Type  Period !" }, JsonRequestBehavior.AllowGet);
                }
                if (Exist == 11)
                {
                    return Json(new { Flag = 1, Html = "Project dates are not exists in  Leave Type  Period !" }, JsonRequestBehavior.AllowGet);
                }

                if (Exist == 0)
                {
                    model.LeaveUnit = 1;
                    int? status = obj.EmpLeaveTypeCreate(model.LeaveTypeId, model.EmployeeId, model.EmpTypeId, model.DesginationId, model.LocationId, model.ProjectId, model.AccrualPeriod, model.AccrualPeriodBegin, model.AccrualPeriodCount, model.LeaveUnit, model.LeavePerAccrualPeriod, model.AccrualPoolLeaveTypeId, model.MaxAgeInMonths, model.PayRateFraction, model.LeaveEncashable, model.MinLeaveUsage, model.MaxLeaveUsage, model.LeaveUsageRestrictionPeriod, model.EarnedLeave, model.MaxDaysMonth, model.MaxDaysYear, model.AutoEncash, model.EncashablePayRate, model.StartDt, model.EndDt, model.LeavePoolForm, model.LeaveRequestType, model.IsCalculateFromDoj, model.IsIncludeHoilday, model.DepartmentId, model.LeaveTypeYear);
                    int[] collumnHide = { 0, 1, 3, 4, 5, 6, 7, 9, 15, 19, 20, 21, 22, 23, 25, 29, 30, 35, 36, 37, 38, 39 };
                    DataTable dtEmpLeaveType = obj.EmpLeaveTypeGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpLeaveType, collumnHide);
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
        public ActionResult EmpLeaveTypeUpdate(EmpLeaveTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string IsCheck = Request["IsCalculateFromDoj"];
                if (IsCheck == "on")
                {
                    model.IsCalculateFromDoj = true;
                }
                model.PayRateFraction = 1;
                string IsIncludeHoilday = Request["IsIncludeHoilday"];
                if (IsIncludeHoilday == "on")
                {
                    model.IsIncludeHoilday = true;
                }
                int? Exist = obj.EmpLeaveTypeExistsValidate(model.LeaveTypeId, model.EmployeeId, model.EmpTypeId, model.DesginationId, model.LocationId, model.ProjectId, model.DepartmentId, model.LeaveRequestType, model.EmpLeaveTypeId, model.LeaveTypeYear, model.AccrualPeriodBegin, model.AccrualPeriodCount,model.StartDt,model.EndDt);

                if (Exist == 10)
                {
                    return Json(new { Flag = 1, Html = "Department dates are not exists in  Leave Type  Period !" }, JsonRequestBehavior.AllowGet);
                }
                if (Exist == 11)
                {
                    return Json(new { Flag = 1, Html = "Project dates are not exists in  Leave Type  Period !" }, JsonRequestBehavior.AllowGet);
                }


                if (Exist == 0)
                {
                    model.LeaveUnit = 1;
                    int? status = obj.EmpLeaveTypeUpdate(model.EmpLeaveTypeId, model.LeaveTypeId, model.EmployeeId, model.EmpTypeId, model.DesginationId, model.LocationId, model.ProjectId, model.AccrualPeriod, model.AccrualPeriodBegin, model.AccrualPeriodCount, model.LeaveUnit, model.LeavePerAccrualPeriod, model.AccrualPoolLeaveTypeId, model.MaxAgeInMonths, model.PayRateFraction, model.LeaveEncashable, model.MinLeaveUsage, model.MaxLeaveUsage, model.LeaveUsageRestrictionPeriod, model.EarnedLeave, model.MaxDaysMonth, model.MaxDaysYear, model.AutoEncash, model.EncashablePayRate, model.StartDt, model.EndDt, model.LeavePoolForm, model.LeaveRequestType, model.IsCalculateFromDoj, model.IsIncludeHoilday, model.DepartmentId, model.LeaveTypeYear);
                    int[] collumnHide = { 0, 1, 3, 4, 5, 6, 7, 9, 15, 19, 20, 21, 22, 23, 25, 29, 30, 35, 36, 37, 38, 39 };
                    DataTable dtEmpLeaveType = obj.EmpLeaveTypeGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpLeaveType, collumnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Leave Type Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmpLeaveTypeDelete(EmpLeaveTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpLeaveTypeDelete(model.EmpLeaveTypeId);
                int[] collumnHide = { 0, 1, 3, 4, 5, 6, 7, 9, 15, 19, 20, 21, 22, 23, 25, 29, 30, 35, 36, 37, 38, 39 };
                DataTable dtEmpLeaveType = obj.EmpLeaveTypeGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpLeaveType, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmployeeLeaveRequest()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpLeaveRequestViewModel model = new EmpLeaveRequestViewModel();
            int? MonthId= DateTime.Now.Month;
                 int? Year= DateTime.Now.Year;


             int? status = obj.MonhtlyLeaveProcessBegin(null, null, null, null, null, MonthId, Year);

            return View(model);
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestCreateCheck(EmpLeaveRequestViewModel model)
        {
            string html = null;
            string IsCheck = string.Empty;

            double? reqdays = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int flag = CommonUtil.CompareDate(model.StartDt, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Status = 11, Html = "Leave Start Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Status = 11, Html = "Leave  Start Date  Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                }
                int flagDt = CommonUtil.CompareDate(model.EndDt, model.EmployeeId);
                if (flagDt == 2)
                {
                    return Json(new { Status = 11, Html = "Leave End Date Should be Less than or Equal to Employee Date of Leaving !" }, JsonRequestBehavior.AllowGet);
                }
               

                int flagEdate = CommonUtil.CompareDate(model.EndDt, model.EmployeeId);

                if (flagEdate == 1)
                {
                    return Json(new { Status = 11, Html = "Leave End Date  Should be Grater than or Equal to Employee Date of Joining !" }, JsonRequestBehavior.AllowGet);
                }


                DataTable dtSalary = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Salary");
                if (dtSalary.Rows.Count > 0)
                {
                    string lastDate = Convert.ToString(dtSalary.Rows[0][3]);

                    DateTime? lastEndDate = null;


                    if (!(string.IsNullOrEmpty(lastDate)))
                    {
                        lastEndDate = Convert.ToDateTime(lastDate);
                    }
                    if (model.StartDt < lastEndDate)
                    {

                        return Json(new { Status = 11, Html = "Payroll is already processed for these dates !" }, JsonRequestBehavior.AllowGet);
                    }


                }
                DataTable dtAt = obj.EmployeeDepartmentSalaryAttendanceDatesGet(model.EmployeeId, "Attendance");

                if (dtAt.Rows.Count > 0)
                {
                    string lastDate = Convert.ToString(dtAt.Rows[0][3]);

                    DateTime? lastEndDate = null;


                    if (!(string.IsNullOrEmpty(lastDate)))
                    {
                        lastEndDate = Convert.ToDateTime(lastDate);
                    }
                    if (model.StartDt < lastEndDate)
                    {

                        return Json(new { Status = 11, Html = "Attendance  is already processed for these dates !" }, JsonRequestBehavior.AllowGet);
                    }


                }

                DataTable dtLeave = obj.EmpLeaveCountIdGet(model.EmployeeId, model.EmpLeaveTypeId, model.StartDt, model.EndDt, "LeaveApply");

                if (dtLeave.Rows.Count == 0)
                {

                    return Json(new { Status = 11, Html = "Leave applied dates are not in Leave set up dates !" }, JsonRequestBehavior.AllowGet);

                }
                if (dtLeave.Rows.Count > 1)
                {

                    return Json(new { Status = 11, Html = "Leave applied dates should be in one Leave set up dates !" }, JsonRequestBehavior.AllowGet);

                }

                TimeSpan tspan = (TimeSpan)(model.EndDt - model.StartDt);

                IsCheck = Request["isHalfDay"];
                if (IsCheck == "on")
                {
                    reqdays = .5;
                }
                else
                {
                    reqdays = tspan.Days + 1;
                }

                int? s = obj.EmpLeaveUsageGet(model.EmployeeId, model.EmpLeaveTypeId, Convert.ToDecimal(reqdays), model.StartDt, model.EndDt);
                if (s == 0 || s == null)
                {

                    return Json(new { Status = s, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else if (s == 1)
                {
                    html = "Leave Start date can not be greater than leave End date.";
                    return Json(new { Status = s, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else if (s == 2)
                {
                    html = "Already Leave Applied For these Dates";
                    return Json(new { Status = s, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else if (s == 4)
                {
                    html = "Requested leave is less than minimum leave usage"; // here need to show minimum leave usage
                    return Json(new { Status = s, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else if (s == 5)
                {
                    html = "Requested leave is greater than maximum leave usage"; //// here need to show maximum leave usage
                    return Json(new { Status = s, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else if (s == 7)
                {
                    html = "Requested leave is greater than Maximum leave "; //// here need to show maximum leave usage
                    return Json(new { Status = 7, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else if (s == 10)
                {
                    html = "Requested leave Dates are Holidays/Weekend "; //// here need to show maximum leave usage
                    return Json(new { Status = 10, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = s }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Status = 11, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestCreate(EmpLeaveRequestViewModel model)
        {
            string IsCheck = string.Empty;
            IsCheck = Request["isHalfDay"];
            if (IsCheck == "on")
            {
                model.isHalfDay = true;
            }

            TimeSpan tspan = (TimeSpan)(model.EndDt - model.StartDt);

            IsCheck = Request["isHalfDay"];
            if (IsCheck == "on")
            {
                model.RequestedDays = Convert.ToDecimal(.5);
            }
            else
            {
                model.RequestedDays = tspan.Days + 1;
            }





            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status;
                int? s = obj.EmpLeaveRequestCreate(model.EmployeeId, model.EmpLeaveTxnDate, model.EmpLeaveTypeId, "Origination", model.StartDt, model.EndDt, model.RequestedDays, null, model.LeaveDescription, DateTime.Now, Session["userName"].ToString(), null, null, null, null, model.isHalfDay, out status);
                if (status == 0 || status == null || status == 3 || status == 7)
                {

                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
                if (status == 2)
                {

                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Flag = 6, Html = "Insert Failed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmpployeeLeavebalanceGet(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] collumnHide = { 0, 1, 3, 5, 9, 10 };

                DataTable dtEmpLeaveType = obj.EmpLeaveBalanceGet(model.EmployeeId, model.EmpLeaveTypeId);
                if (dtEmpLeaveType.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtEmpLeaveType, collumnHide);
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
        public ActionResult EmpployeeLeaveProcess(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.LeaveProcessBeginEnd("Begin", model.StartDt, model.EndDt, Convert.ToString(model.LeaveTypeId), model.EmpLeaveTypeId);


                return Json(new { Flag = 0, Html = "" }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }




        public ActionResult EmployeeLeaveApproval()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            EmpLeaveRequestViewModel model = new EmpLeaveRequestViewModel();
            int? MonthId = DateTime.Now.Month;
            int? Year = DateTime.Now.Year;

            //-- Not for Mathura Client--
          //  int? status = obj.MonhtlyLeaveProcessBegin(null, null, null, null, null, MonthId, Year);
            return View(model);
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestGet(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                int[] collumnHide = null;
                int? status = Convert.ToInt32(model.LeaveStatus);
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                collumnHide = new int[]{ 0, 1, 4, 6, 10, 11, 13, 15, 17, 18 ,19,20,21};
                if (status == 1)
                {
                    collumnHide = new int[] { 0, 1, 4, 6, 10, 11, 13, 14, 15, 16, 17, 18 };
                }
                DataSet dsEmpLeaveType = obj.EmpLeaveRequestGet(status, null);
                // DataTable dt = dsEmpLeaveType.Tables[0];
                if (dsEmpLeaveType.Tables[0].Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dsEmpLeaveType.Tables[0], collumnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestApprove(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] collumnHide = null;

                int? Flag = obj.EmpLeaveApprovedExistsValidate(model.EmployeeId, model.EmpLeaveRequestId, model.StartDt, model.EndDt);
                if (Flag == 0)
                {
                    int? spstatus = objML.EmpLeaveRequestApprove(model.EmpLeaveRequestId, model.EmployeeId, model.EmpLeaveTxnDate, model.LeaveTypeId, "Origination", model.StartDt, model.EndDt, model.RequestedDays, model.LeaveDebitDays, model.PayDeductDays, model.LeaveDescription, DateTime.Now, Session["userName"].ToString(), null, model.EmpLeaveTransactionId,model.Resonreject);
                }
                  collumnHide = new int []  { 0, 1, 4, 6, 10, 11, 13, 14, 15, 16, 17, 18 ,19,20,21};


                int? status = Convert.ToInt32(model.LeaveStatus);
                if (status == 1)
                {
                    collumnHide = new int[] { 0, 1, 4, 6, 10, 11, 13, 14, 15, 16, 17, 18 };
                }

                DataSet dsEmpLeaveType = obj.EmpLeaveRequestGet(status, null);
                if (dsEmpLeaveType.Tables[0].Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dsEmpLeaveType.Tables[0], collumnHide);
                    if (Flag == 0)
                    {
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                      
                        return Json(new { Flag =2, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    if (Flag == 0)
                    {
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);

                    }
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestDisApprove(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                 int? Flag = obj.EmpLeaveDisApprovedExistsValidate(model.EmployeeId, model.EmpLeaveRequestId, model.StartDt, model.EndDt);
                 if (Flag == 0)
                 {
                     int? spstatus = obj.EmpLeaveRequestDisapprove(model.EmpLeaveRequestId, model.EmployeeId, model.EmpLeaveTxnDate, model.LeaveTypeId, "Origination", model.StartDt, model.EndDt, model.RequestedDays, null, model.LeaveDescription, DateTime.Now, Session["userName"].ToString(), null, model.EmpLeaveTransactionId);
                 }
                    int[] collumnHide = { 0, 1, 4, 6, 10, 11, 13, 14, 15, 16, 17, 18 };
                int? status = Convert.ToInt32(model.LeaveStatus);
                DataSet dsEmpLeaveType = obj.EmpLeaveRequestGet(status, null);
                if (dsEmpLeaveType.Tables[0].Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dsEmpLeaveType.Tables[0], collumnHide);
                    if (Flag == 0)
                    {
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                      
                        return Json(new { Flag = 2, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    //return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestReject(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? spstatus = objML.EmpLeaveRequestReject(model.EmpLeaveRequestId, model.EmployeeId, model.EmpLeaveTxnDate, model.LeaveTypeId, "Origination", model.StartDt, model.EndDt, model.RequestedDays, null, model.LeaveDescription, DateTime.Now, Session["userName"].ToString(), null, model.EmpLeaveTransactionId,model.Resonreject);
                int[] collumnHide = { 0, 1, 4, 6, 10, 11, 13, 14, 15, 16, 17, 18 };
                int? status = Convert.ToInt32(model.LeaveStatus);
                DataSet dsEmpLeaveType = obj.EmpLeaveRequestGet(status, null);
                if (dsEmpLeaveType.Tables[0].Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dsEmpLeaveType.Tables[0], collumnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    //return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeLeaveTypeGet(EmpLeaveRequestViewModel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> LeaveListItems = new List<SelectListItem>();
            // DataTable DtLeaveType = obj.EmployeeLeaveTypeGet(model.EmployeeId);
            DataTable DtLeaveType = obj.EmployeeLeaveTypeAsignGet(model.EmployeeId);
            foreach (DataRow dr in DtLeaveType.Rows)
            {
                LeaveListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = LeaveListItems;
            return Json(list);
        }

        public ActionResult ProcessLeaveCycle()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            LeaveCycleViewModel model = new LeaveCycleViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GetfiscalDateRange(LeaveCycleViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // List<SelectListItem> SectionListItems = new List<SelectListItem>();
                DataTable dtfiscalDateRange = obj.FiscalDateRangeGet(model.DateRange);
                foreach (DataRow dr in dtfiscalDateRange.Rows)
                {
                    //SectionListItems.Add(new SelectListItem
                    //{
                    //    Text = dr["MasterKeyName"].ToString(),
                    //    Value = dr["MasterKey"].ToString()
                    //});
                    model.StartdDate = Convert.ToDateTime(dr[0] is DBNull ? null : dr[0]);
                    model.EndDate = Convert.ToDateTime(dr[1] is DBNull ? null : dr[1]);

                }
                var data = dtfiscalDateRange;

                //var list = SectionListItems;
                //return Json(list);

                //    if (dtfiscalDateRange.Rows.Count > 0)
                //    {

                //        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        //html = "<div class='alert alert-danger'>No Record !!</div>";
                //        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                //    }
                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult ProcessLeaveCycleGet(LeaveCycleViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] collumnHide = { 0 };
                DataTable dtProcessLeaveCycle = obj.ProcessLeaveCycleGet();
                if (dtProcessLeaveCycle.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableProcessLeaveCycle(dtProcessLeaveCycle, collumnHide);
                    return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                }
                else
                {

                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(html, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult ProcessLeaveCycle(LeaveCycleViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = null;
                if (command == "Go")
                {
                    int[] collumnHide = { 0, 4 };
                    DataTable dtProcessLeaveCycle = obj.ProcessLeaveCycleGet();
                    //DataTable dtLeaveCycle = obj.LeaveCycleGet(model.StartdDate, model.EndDate, null);
                    if (dtProcessLeaveCycle.Rows.Count > 0)
                    {
                        StringBuilder htmlTable1 = CommonUtil.htmlTableProcessLeaveCycle(dtProcessLeaveCycle, collumnHide);
                        StringBuilder htmlTable2 = null;// CommonUtil.htmlTableLeaveCycle(dtLeaveCycle, collumnHide);
                        return Json(htmlTable1.ToString() + Convert.ToString(htmlTable2), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == "BeginProcess")
                {
                    int count = model.LeaveCyclingList.Count;
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string CycleIdList = "";
                    int n = 0;


                    if (model.LeaveCyclingList != null)
                    {


                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "LeaveCyclingList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.LeaveCyclingList[x].LeaveTypeId != null)
                                {
                                    if (n == 0)
                                    {
                                        CycleIdList = Convert.ToString(model.LeaveCyclingList[x].LeaveTypeId).Trim();
                                    }
                                    else
                                    {
                                        CycleIdList += "," + Convert.ToString(model.LeaveCyclingList[x].LeaveTypeId).Trim();
                                    }

                                    n = n + 1;

                                }
                            }

                        }


                        if (!(string.IsNullOrEmpty(CycleIdList)))
                        {

                            status = obj.LeaveProcessBeginEnd("Begin", model.StartdDate, model.EndDate, CycleIdList, null);
                            //status = obj.LeaveProcessBegin(CycleIdList, model.StartdDate, model.EndDate, model.StartdDate, model.EndDate, model.EmployeeId, model.PayrollId);
                        }
                    }

                    int[] collumnHide = { 0, 2, 4 };
                    DataTable dtProcessLeaveCycle = obj.ProcessLeaveCycleGet();
                    DataTable dtLeaveCycle = obj.LeaveCycleGet(model.StartdDate, model.EndDate, CycleIdList);

                    if (dtProcessLeaveCycle.Rows.Count > 0)
                    {
                        StringBuilder htmlTable1 = CommonUtil.htmlTableProcessLeaveCycle(dtProcessLeaveCycle, collumnHide);
                        StringBuilder htmlTable2 = CommonUtil.htmlTableLeaveCycle(dtLeaveCycle, collumnHide);

                        if (status == 0)
                        {
                            htmlTable1.Append("<div class='alert alert-success'>Leave Cycle Begin Processed Successfully for the Selected Record(s) !!</div>");
                        }
                        return Json(htmlTable1.ToString() + htmlTable2.ToString() + html, JsonRequestBehavior.AllowGet);
                        // return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        html = "<div class='alert alert-danger'>Leave Cycle Begin Processed Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == "EndProcess")
                {
                    int count = model.LeaveCyclingList.Count;
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    string CycleIdList = string.Empty;
                    int n = 0;
                    //int? recordInserted = null;
                    if (model.LeaveCyclingList != null)
                    {
                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "LeaveCyclingList[" + x + "].isCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (model.LeaveCyclingList[x].LeaveTypeId != null)
                                {
                                    if (n == 0)
                                    {
                                        CycleIdList = Convert.ToString(model.LeaveCyclingList[x].LeaveTypeId).Trim();
                                    }
                                    else
                                    {
                                        CycleIdList += "," + Convert.ToString(model.LeaveCyclingList[x].LeaveTypeId).Trim();
                                    }
                                    n = n + 1;
                                }
                            }
                        }

                        if (!(string.IsNullOrEmpty(CycleIdList)))
                        {
                            status = obj.LeaveProcessBeginEnd("End", model.StartdDate, model.EndDate, CycleIdList, null);

                            //status = obj.LeaveProcessEnd(CycleIdList, model.StartdDate, model.EndDate, model.StartdDate, model.EndDate, model.EmployeeId, model.PayrollId);
                        }

                    }

                    int[] collumnHide = { 0, 2, 4 };
                    DataTable dtProcessLeaveCycle = obj.ProcessLeaveCycleGet();
                    DataTable dtLeaveCycle = obj.LeaveCycleGet(model.StartdDate, model.EndDate, CycleIdList);

                    if (dtProcessLeaveCycle.Rows.Count > 0)
                    {

                        StringBuilder htmlTable1 = CommonUtil.htmlTableProcessLeaveCycle(dtProcessLeaveCycle, collumnHide);
                        StringBuilder htmlTable2 = CommonUtil.htmlTableLeaveCycle(dtLeaveCycle, collumnHide);

                        if (status == 0)
                        {
                            htmlTable1.Append("<div class='alert alert-success'>Leave Cycle End Processed  Successfully for the Selected Record(s) !!</div>");
                        }
                        return Json(htmlTable1.ToString() + htmlTable2.ToString() + html, JsonRequestBehavior.AllowGet);
                        // return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        html = "<div class='alert alert-danger'>Leave Cycle End Processed Successfully for the Selected Record(s) !!</div>";
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
        public ActionResult ProcessLeaveCycleBegin(LeaveCycleViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = null;
                int count = model.LeaveCyclingList.Count;
                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                string CycleIdList = "";
                int n = 0;


                if (model.LeaveCyclingList != null)
                {


                    for (int x = 0; x < count; x++)
                    {
                        checkBoxName = "LeaveCyclingList[" + x + "].isCheck";
                        IsCheck = Request[checkBoxName];
                        if (IsCheck == "on")
                        {
                            if (model.LeaveCyclingList[x].LeaveTypeId != null)
                            {
                                if (n == 0)
                                {
                                    CycleIdList = Convert.ToString(model.LeaveCyclingList[x].LeaveTypeId).Trim();
                                }
                                else
                                {
                                    CycleIdList += "," + Convert.ToString(model.LeaveCyclingList[x].LeaveTypeId).Trim();
                                }

                                n = n + 1;

                            }
                        }

                    }


                    if (!(string.IsNullOrEmpty(CycleIdList)))
                    {

                        status = obj.LeaveProcessBeginEnd("Begin", model.StartdDate, model.EndDate, CycleIdList, null);
                        //status = obj.LeaveProcessBegin(CycleIdList, model.StartdDate, model.EndDate, model.StartdDate, model.EndDate, model.EmployeeId, model.PayrollId);
                    }


                }

                int[] collumnHide = { 0, 4 };
                DataTable dtProcessLeaveCycle = obj.LeaveCycleGet(model.StartdDate, model.EndDate, CycleIdList);
                if (dtProcessLeaveCycle.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtProcessLeaveCycle, collumnHide);
                    if (status == 0)
                    {
                        htmlTable.Append("<div class='alert alert-success'>Leave Cycle Begin Processed Successfully for the Selected Record(s) !!</div>");
                    }
                    return Json(new { Flag = 0, Html = htmlTable.ToString() + html }, JsonRequestBehavior.AllowGet);
                    // return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    html = "<div class='alert alert-danger'>Leave Cycle Begin Processed Successfully for the Selected Record(s) !!</div>";
                    return Json(html, JsonRequestBehavior.AllowGet);
                    //html = "<div class='alert alert-danger'>No Recod !!</div>";
                    //return Json(html, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

                return Json(html, JsonRequestBehavior.AllowGet);
            }

        }


        //---------------------------Leave Cycle Void---------------------------//
        public ActionResult LeaveCycleVoid()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                LeaveCycleVoidViewModel model = new LeaveCycleVoidViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult LeaveCycleVoidGet(LeaveCycleVoidViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] showcolumn = new[] { 0, 1 };
                DataSet dsLeaveCyclesToVoid = obj.LeaveCyclesToVoidGet();
                StringBuilder htmlTable = CommonUtil.LeaveCycleVoidHtmlNestedTableEditMode(showcolumn, dsLeaveCyclesToVoid.Tables[0]);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-----------------Leave Cycle Void Transaction Details -----------------------//

        [HttpPost]
        public ActionResult LeaveCycleTxnGet(int? LeaveCycleId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 7, 8, 9, 10, 11, 12, 13 };
                DataSet dsLeaveCyclesToVoid = obj.LeaveCyclesToVoidGet();
                StringBuilder htmlTable = CommonUtil.htmlChildTable(dsLeaveCyclesToVoid.Tables[1], columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LeaveCycleVoidPost(LeaveCycleVoidViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = null;

                //if (command == "Void")
                //{
                int count = model.LeaveCyclingList.Count;
                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                string CycleIdList = "";
                int n = 0;
                if (model.LeaveCyclingList != null)
                {


                    for (int x = 0; x < count; x++)
                    {
                        checkBoxName = "LeaveCyclingList[" + x + "].isCheck";
                        IsCheck = Request[checkBoxName];
                        if (IsCheck == "on")
                        {
                            if (model.LeaveCyclingList[x].LeaveCycleId != null)
                            {
                                if (n == 0)
                                {
                                    CycleIdList = Convert.ToString(model.LeaveCyclingList[x].LeaveCycleId).Trim();
                                }
                                else
                                {
                                    CycleIdList += "," + Convert.ToString(model.LeaveCyclingList[x].LeaveCycleId).Trim();
                                }
                                n = n + 1;
                            }
                        }
                    }
                    //if (!(string.IsNullOrEmpty(CycleIdList)))
                    //{
                    //    status = obj.LeaveCyclesVoid(CycleIdList, "");
                    //    //status = obj.LeaveProcessBegin(CycleIdList, model.StartdDate, model.EndDate, model.StartdDate, model.EndDate, model.EmployeeId, model.PayrollId);
                    //}
                }
                int countm = model.empleavetxnList.Count;
                string checkBoxNamme = string.Empty;
                string IsCheckm = string.Empty;
                string TxnIdList = "";
                int m = 0;
                if (model.empleavetxnList != null)
                {
                    for (int x = 0; x < countm; x++)
                    {
                        checkBoxNamme = "empleavetxnList[" + x + "].isCheck";
                        IsCheckm = Request[checkBoxNamme];
                        if (IsCheckm.Contains("on"))
                        {
                            if (model.empleavetxnList[x].EmpLeaveTxnId != null)
                            {
                                if (m == 0)
                                {
                                    TxnIdList = Convert.ToString(model.empleavetxnList[x].EmpLeaveTxnId).Trim();
                                }
                                else
                                {
                                    TxnIdList += "," + Convert.ToString(model.empleavetxnList[x].EmpLeaveTxnId).Trim();
                                }
                                m = m + 1;
                            }
                        }
                    }
                    if (!(string.IsNullOrEmpty(CycleIdList)))
                    {

                        status = obj.LeaveCyclesVoid(CycleIdList, TxnIdList);
                        //status = obj.LeaveProcessBegin(CycleIdList, model.StartdDate, model.EndDate, model.StartdDate, model.EndDate, model.EmployeeId, model.PayrollId);
                    }
                }

                int[] HideColumn = new[] { 0, 1 };
                DataSet dsLeaveCyclesToVoid = obj.LeaveCyclesToVoidGet();
                if (dsLeaveCyclesToVoid.Tables[0].Rows.Count > 0)
                {
                    StringBuilder htmlTable1 = CommonUtil.LeaveCycleVoidHtmlNestedTableEditMode(HideColumn, dsLeaveCyclesToVoid.Tables[0]);
                    //int[] columnHided = new[] { 0, 1, 3, 7, 8, 9, 10, 11, 12, 13 };
                    //StringBuilder htmlTable2 = CommonUtil.htmlChildTable(dsLeaveCyclesToVoid.Tables[1], columnHided);
                    if (status == 0)
                    {
                        htmlTable1.Append("<div class='alert alert-success'>Leave Cycles Void Successfully for the Selected Record(s) !!</div>");
                    }
                    //return Json(htmlTable1.ToString() + htmlTable2.ToString() + html, JsonRequestBehavior.AllowGet);
                    return Json(new { Flag = 0, Html = htmlTable1.ToString() + html }, JsonRequestBehavior.AllowGet);
                    // return Json(htmlTable.ToString() + html, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    html = "<div class='alert alert-danger'>Leave Cycles Void Successfully for the Selected Record(s) !!</div>";
                    //return Json(new { Flag = 0, Html = htmlTable1.ToString() + html }, JsonRequestBehavior.AllowGet);
                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }
                //}



                //html = "<div class='alert alert-danger'>No Recod !!</div>";
                //return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = "<div class='alert alert-danger'>" + ex.Message.ToString() + "!!</div>";

                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-----------------Employee Leave Transaction -----------------------//
        public ActionResult EmployeeLeaveTransaction()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmployeeLeaveTransactionViewModel model = new EmployeeLeaveTransactionViewModel();


                int? MonthId = DateTime.Now.Month;
                int? Year = DateTime.Now.Year;

              
            int? status = obj.MonhtlyLeaveProcessBegin(null, null, null, null, null, MonthId, Year);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeDDlGet(EmployeeLeaveTransactionViewModel model)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtEmployeeInfo = obj.EmployeeInfoGet();
            foreach (DataRow dr in dtEmployeeInfo.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = SectionListItems;
            return Json(list);
        }

        [HttpPost]
        public ActionResult EmployeeLeaveTransactionPost(EmployeeLeaveTransactionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                StringBuilder htmlTabs = new StringBuilder();
                StringBuilder htmlTabContent = new StringBuilder();
                int i = 0;
                //  DataTable dtLeaveTypeGet = obj.EmployeeLeaveTypeGet(model.EmployeeId);
                DataTable dtLeaveTypeGet = obj.EmployeeLeaveTypeAsignGet(model.EmployeeId);
                if (dtLeaveTypeGet.Rows.Count > 0)
                {
                    htmlTabs.Append("<ul id='tabs' class='nav nav-tabs' data-tabs='tabs'>");

                    htmlTabContent.Append("<div id='my-tab-content' class='tab-content' >");
                    foreach (DataRow row in dtLeaveTypeGet.Rows)
                    {


                        if (i == 0)
                        {
                            htmlTabs.Append("<li id='tab" + row["Leave Type"] + "' class='active list-group-item-info'><a href='#" + row["Leave Type"] + "' data-toggle='tab'>" + row["Leave Type"] + "</a></li>");
                            htmlTabContent.Append("<div class='tab-pane active' id='" + row["Leave Type"] + "' style='margin-top: 10px'>");
                            htmlTabContent.Append("<div class='datatable' id='data" + row["Leave Type"] + "' style='overflow: auto;'>");
                        }
                        else
                        {
                            htmlTabs.Append("<li id='tab" + row["Leave Type"] + "' class='list-group-item-info'><a href='#" + row["Leave Type"] + "' data-toggle='tab'>" + row["Leave Type"] + "</a></li>");
                            htmlTabContent.Append("<div class='tab-pane' id='" + row["Leave Type"] + "' style='margin-top: 10px'>");
                            htmlTabContent.Append("<div id='data" + row["Leave Type"] + "' style='overflow: auto;'>");
                        }


                        DataTable dtTxn = obj.EmpLeaveTransactionGet(Convert.ToInt32(row["leave_type_id"]), model.EmployeeId);
                        if (dtTxn.Rows.Count > 0)
                        {
                            int[] CollumnHide = new[] {6};
                            StringBuilder Htmltable = CommonUtil.htmlTable(dtTxn, CollumnHide);
                            htmlTabContent.Append(Htmltable + "</div>");
                            htmlTabContent.Append("</div>");
                        }
                        else
                        {
                            htmlTabContent.Append("<div class='alert alert-danger'>No Record !!</div>");
                            htmlTabContent.Append("</div>");
                            htmlTabContent.Append("</div>");
                        }
                        i = i + 1;

                    }
                    htmlTabContent.Append("</div>");
                    htmlTabs.Append("</ul>");
                }
                else
                {
                    html = "Leave Type not created!!";
                }
                return Json(new { Flag = 0, HtmlTabs = htmlTabs.ToString(), HtmlTabContent = htmlTabContent.ToString(), Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        
        [HttpPost]
        public ActionResult LefEemployeeDetails(EmployeeLeaveTransactionViewModel model)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtEmployeeLeftInfo = obj.EmployeeInfoLeftGet();
            foreach (DataRow dr in dtEmployeeLeftInfo.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = SectionListItems;
            return Json(list);
        }

        [HttpGet]
        public ActionResult EmployeeSurrenderLeaveRequest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EmployeeSurrenderLeaveApproval()
        {
            return View();
        }


        //----------------- Leave Detail(Type) -----------------------//
        public ActionResult LeaveDetail()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpLeaveRequestViewModel model = new EmpLeaveRequestViewModel();
                int? MonthId = DateTime.Now.Month;
                int? Year = DateTime.Now.Year;
               
            int? status = obj.MonhtlyLeaveProcessBegin(null, null, null, null, null, MonthId, Year);
                
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult LeaveDetailGet(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                int[] collumnHide = { 0, 3, 4, 13, 14, 15, 16 };

                DataTable dsEmpLeaveType = obj.EmployeeLeaveDetailGet(model.EmployeeId, model.LeaveTypeId, model.StartDate, model.EndDate);
                if (dsEmpLeaveType.Rows.Count > 0)
                {
                    Session.Add("snLeaveReportGet", dsEmpLeaveType);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeLeaveUpdate(dsEmpLeaveType, collumnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    //return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
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
        public ActionResult EmpployeeLeaveRequestUpdate(EmpLeaveRequestViewModel model)
        {
            int? status;
           

            int? Countworkday = obj.EmpLeaveHolidayDatesCountGet(model.EmployeeId, model.StartDt, model.EndDt, "Work Day");

            int? id = Convert.ToInt32(Session["EmployeeId"]);
            Session["EmployeeId"] = Convert.ToInt32(model.EmployeeId);
            if (Session["EmployeeId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //string IsSendMailCheck = string.Empty;
            //IsSendMailCheck = Request["Is_Send_Mail"];

            string IsCheck = string.Empty;
            IsCheck = Request["isHalfDay"];
            if (IsCheck == "on")
            {
                model.isHalfDay = true;
            }

            TimeSpan tspan = (TimeSpan)(model.EndDt - model.StartDt);

            IsCheck = Request["isHalfDay"];
            if (IsCheck == "on")
            {
                model.RequestedDays = Convert.ToDecimal(.5);
            }
            else
            {


                model.RequestedDays = Countworkday;
            }


            //string SmtpServer = null;
            //string PortAddress = null;
            //string UseSSL = null;
            //string EmailAddress = null;
            //string Password = null;
            //string To = null;
            //string Cc = null;

            string html = null;
            try
            {

                //if (Convert.ToString(Session["Employee_Email"]) != null)
                //{
                    //StringBuilder sb = new StringBuilder(null);
                    //DataTable dtMailSettings = obj.MailSetupGet(null, null, Convert.ToInt32(model.EmployeeId), "SendMail");

                    //if (dtMailSettings.Rows.Count > 0)
                    //{
                    //    SmtpServer = Convert.ToString(dtMailSettings.Rows[0][4]);
                    //    EmailAddress = Convert.ToString(dtMailSettings.Rows[0][5]);
                    //    To = Convert.ToString(dtMailSettings.Rows[0][14]);
                    //    Cc = Convert.ToString(dtMailSettings.Rows[0][15]);
                    //    EmailAddress = Convert.ToString(dtMailSettings.Rows[0][15]);
                    //    //EmailAddress = Convert.ToString(Session["Employee_Email"]);
                    //    Password = Convert.ToString(dtMailSettings.Rows[0][6]);
                    //    PortAddress = Convert.ToString(dtMailSettings.Rows[0][7]);
                    //    UseSSL = Convert.ToString(dtMailSettings.Rows[0][10]);
                        //string ReportHeader;
                        //if (IsCheck == "on")
                        //{
                        //    ReportHeader = "Leave Request Changes For Half Day";
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + " Dear Sir, " + "</b>" + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + "Leave Day: " + "Half Day " + "on " + "</b>");

                        //    sb.Append("<b>" + Convert.ToDateTime(model.StartDt).ToShortDateString() + "</b>" + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + model.LeaveDescription + "." + "</span>");

                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "Thanks & Regards," + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + model.EmployeeName + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "EMP No #" + Convert.ToString(Session["EmpCode"]) + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "Location-" + Convert.ToString(Session["LocationName"]) + "</span>");
                        //    sb.Append("<br />");

                        //}
                        //else
                        //{
                        //    ReportHeader = "Leave Request For " + Convert.ToString(Countworkday) + " Days";

                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + " Dear Sir, " + "</b>" + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + "Leave Request Changes " + "Leave Days: " + Convert.ToString(Countworkday) + " Days (" + "</b>" + "</span>");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + " From " + Convert.ToDateTime(model.StartDt).ToShortDateString() + " to " + Convert.ToDateTime(model.EndDt).ToShortDateString() + ")" + "</b>" + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + model.LeaveDescription + "." + "</span>");

                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "Thanks & Regards," + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + model.EmployeeName + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "EMP No #" + Convert.ToString(Session["EmpCode"]) + "</span>");
                        //    sb.Append("<br />");
                        //    sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "Location-" + Convert.ToString(Session["LocationName"]) + "</span>");
                        //    sb.Append("<br />");

                        //}



                        //MailMessage mail = new MailMessage();

                        //mail.From = new MailAddress(Convert.ToString(EmailAddress));
                        //mail.To.Add(Convert.ToString(model.To));
                        //mail.CC.Add(Convert.ToString(model.Cc));
                        //mail.Subject = ReportHeader;
                        //mail.Body = Convert.ToString(sb);
                        //mail.IsBodyHtml = true;
                        //mail.Priority = MailPriority.High;
                        //SmtpClient smtp = new SmtpClient();
                        //smtp.Host = SmtpServer;

                        //if (UseSSL.ToUpper() == "TRUE")
                        //{
                        //    smtp.EnableSsl = true;
                        //}
                        //else
                        //{
                        //    smtp.EnableSsl = false;
                        //}
                        //smtp.Credentials = new System.Net.NetworkCredential(EmailAddress, Password);
                        //smtp.Port = Convert.ToInt32(PortAddress);
                        //smtp.UseDefaultCredentials = false;
                        //smtp.Timeout = 30000;
                        //smtp.Send(mail);

                        int? s = obj.EmpLeaveRequestUpdate(model.EmpLeaveRequestId, model.EmployeeId, model.EmpLeaveTxnDate, model.EmpLeaveTypeId, "Origination", model.StartDt, model.EndDt, model.RequestedDays, null, model.LeaveDescription, null, null, model.isHalfDay, out status);
                        if (status == 0)
                        {


                            int[] collumnHide = { 0, 3, 4, 13, 14, 15, 16 };
                            DataTable dsEmpLeaveType = obj.EmployeeLeaveDetailGet(Convert.ToInt32(Session["EmployeeId"]), model.eltLeaveTypeId, model.StartDate, model.EndDate);
                            if (dsEmpLeaveType.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableEditModeLeaveUpdate(dsEmpLeaveType, collumnHide);

                                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (status == 1)
                        {
                            html = "Update Error";
                            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                        }

                    //}
                    //else
                    //{
                    //    return Json(new { Flag = 3, Html = "Employee Email id  Is not available !!" }, JsonRequestBehavior.AllowGet);
                    //}
                //}

            }



            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EmpployeeLeaveRequestCancel(EmpLeaveRequestViewModel model)
        {
            string html = "";
            try
            {
                if (Session["EmployeeId"] == null)
                {
                    html = "<div class='alert alert-danger'>Session Expired !!</div>";

                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }

                int? spstatus = obj.EmpLeaveRequestCancel(model.EmpLeaveRequestId, model.EmployeeId, model.EmpLeaveTxnDate, model.LeaveTypeId, "Origination", model.StartDt, model.EndDt, model.RequestedDays, null, model.LeaveDescription, DateTime.Now, Convert.ToString(Session["EmployeeName"]), null, model.EmpLeaveTransactionId);

                if (spstatus == 0)
                {
                    //string ReportHeader;
                    int? Countworkday = obj.EmpLeaveHolidayDatesCountGet(model.EmployeeId, model.StartDt, model.EndDt, "Work Day");

                    int? id = Convert.ToInt32(Session["EmployeeId"]);
                    Session["EmployeeId"] = Convert.ToInt32(model.EmployeeId);
                    //string SmtpServer = null;
                    //string PortAddress = null;
                    //string UseSSL = null;
                    //string EmailAddress = null;
                    //string Password = null;
                    //if (Convert.ToString(Session["Employee_Email"]) != null)
                    //{

                    //    StringBuilder sb = new StringBuilder(null);
                    //    DataTable dtMailSettings = obj.MailSetupGet(null, null, Convert.ToInt32(model.EmployeeId), "SendMail");

                    //    if (dtMailSettings.Rows.Count > 0)
                    //    {
                    //        model.To = Convert.ToString(dtMailSettings.Rows[0][14]);
                    //        model.Cc = Convert.ToString(dtMailSettings.Rows[0][15]);
                    //        //model.To = Convert.ToString(Session["To_Email_Id"]);
                    //        //model.Cc = Convert.ToString(Session["Cc_Email_Id"]);
                    //        model.LeaveDescription = "The bleow reuqested leave was cancelled by me.";
                    //        SmtpServer = Convert.ToString(dtMailSettings.Rows[0][4]);
                    //        EmailAddress = Convert.ToString(dtMailSettings.Rows[0][5]);
                    //       // EmailAddress = Convert.ToString(Session["Employee_Email"]);
                    //        Password = Convert.ToString(dtMailSettings.Rows[0][6]);
                    //        PortAddress = Convert.ToString(dtMailSettings.Rows[0][7]);
                    //        UseSSL = Convert.ToString(dtMailSettings.Rows[0][10]);

                    //        ReportHeader = "Cancellation of leave request";
                    //        sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + " Dear Sir, " + "</b>" + "</span>");
                    //        sb.Append("<br />");
                    //        sb.Append("<br />");
                    //        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "<b>" + "Cancellation of leave request" + "</b>");
                    //        sb.Append("<br />");
                    //        sb.Append("<br />");
                    //        sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + model.LeaveDescription + "." + "</span>");
                    //        sb.Append("<br />");
                    //        sb.Append("Leave Start Date: " + Convert.ToDateTime(model.StartDt).ToShortDateString() + " Leave End Date: " + Convert.ToDateTime(model.EndDt).ToShortDateString() + " Requested Days: " + Convert.ToString(model.RequestedDays));
                    //        sb.Append("<br />");
                    //        sb.Append("<br />");
                    //        sb.Append("<br />");
                    //        sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "Thanks & Regards," + "</span>");
                    //        sb.Append("<br />");
                    //        sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + model.EmployeeName + "</span>");
                    //        sb.Append("<br />");
                    //        sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "EMP No #" + Convert.ToString(Session["EmpCode"]) + "</span>");
                    //        sb.Append("<br />");
                    //        sb.Append("<span style='font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px'>" + "Location-" + Convert.ToString(Session["LocationName"]) + "</span>");
                    //        sb.Append("<br />");



                    //        MailMessage mail = new MailMessage();

                    //        mail.From = new MailAddress(Convert.ToString(EmailAddress));
                    //        mail.To.Add(Convert.ToString(model.To));
                    //        mail.CC.Add(Convert.ToString(model.Cc));
                    //        mail.Subject = ReportHeader;
                    //        mail.Body = Convert.ToString(sb);
                    //        mail.IsBodyHtml = true;
                    //        mail.Priority = MailPriority.High;
                    //        SmtpClient smtp = new SmtpClient();
                    //        smtp.Host = SmtpServer;

                    //        if (UseSSL.ToUpper() == "TRUE")
                    //        {
                    //            smtp.EnableSsl = true;
                    //        }
                    //        else
                    //        {
                    //            smtp.EnableSsl = false;
                    //        }
                    //        smtp.Credentials = new System.Net.NetworkCredential(EmailAddress, Password);
                    //        smtp.Port = Convert.ToInt32(PortAddress);
                    //        smtp.UseDefaultCredentials = false;
                    //        smtp.Timeout = 30000;
                    //        smtp.Send(mail);




                    //    }
                    //}
                }


                int? status = Convert.ToInt32(model.LeaveStatus);
                int[] collumnHide = { 0, 3, 4, 13, 14, 15, 16 };
                DataTable dsEmpLeaveType = obj.EmployeeLeaveDetailGet(Convert.ToInt32(Session["EmployeeId"]), model.eltLeaveTypeId, model.StartDate, model.EndDate);
                if (dsEmpLeaveType.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeLeaveUpdate(dsEmpLeaveType, collumnHide);

                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult LeaveDetail(EmpLeaveRequestViewModel model, string command)
        {

            string html = null;
            try
            {
                if (Session["snLeaveReportGet"] != null)
                {
                    GridView gv = GridViewGet((DataTable)Session["snLeaveReportGet"], "Leave Report");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "LeaveReport", "N", 4);
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "LeaveReport", 10);
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "LeaveReport", 12);
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

        public GridView GridViewGet(DataTable dt, string ReportHeader)
        {


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            DataTable dtCompany = obj.CurrentCompanyInformationGet(null);
            string CompanyName = Convert.ToString(dtCompany.Rows[0][0]);
            string CompanyAddress = Convert.ToString(dtCompany.Rows[0][1]);
            string CompanyCity = Convert.ToString(dtCompany.Rows[0][2]);
            string PinCode = Convert.ToString(dtCompany.Rows[0][3]);
            byte[] CompanyLogo = (byte[])dtCompany.Rows[0][10];
            string PhotoString = Convert.ToBase64String(CompanyLogo);

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

        //----------------- Leave Process -----------------------//
        public ActionResult LeaveProcess()
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
        [HttpPost]
        public ActionResult LeaveProcess(PayrollUtil model, string command)
        {
            string html = "";
            int? status = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (command == "Search")
                {

                    DataTable dtleaveProcess = obj.EmployeeLeaveProcessGet(model.Year, model.ArearYear, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveProcess(dtleaveProcess, HideColumn);
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
                                    model.LeaveCredit = model.EmpDataList[x].LeaveCredit;
                                    if ((model.LeaveCredit != null) || (model.LeaveCredit != 0))
                                    {

                                        // status = obj.(model.EmpDataList[x].EmployeeId, model.MonthId, model.Year, model.PayrollItemId, model.ItemAmount);
                                        status = obj.EmployeeLeaveProcessUpdate(model.EmpDataList[x].EmployeeId, model.LeaveType_Id, model.Year, model.ArearYear, model.LeaveCredit);
                                    }
                                }
                            }

                        }





                    }



                    DataTable dtleaveProcess = obj.EmployeeLeaveProcessGet(model.Year, model.ArearYear, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveProcess(dtleaveProcess, HideColumn);
                        htmlTable.Append("<div class='alert alert-success'>Leave Updated Successfully for the Selected Record(s) !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-success'>Leave Updated Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }

                }




                html = "<div class='alert alert-danger'>No Record !!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }
        //----------------- Leave Process Debit-----------------------//
        public ActionResult LeaveProcessDebit()
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
        [HttpPost]
        public ActionResult LeaveProcessDebit(PayrollUtil model, string command)
        {
            string html = "";
            int? status = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (command == "Search")
                {

                    DataTable dtleaveProcess = obj.EmployeeLeaveDebitProcessGet(model.MonthId,model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveDebitProcess(dtleaveProcess, HideColumn);
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
                                    model.LeaveDebit = model.EmpDataList[x].LeaveDebit;
                                   // if (model.LeaveDebit > 0)
                                    {

                                        // status = obj.(model.EmpDataList[x].EmployeeId, model.MonthId, model.Year, model.PayrollItemId, model.ItemAmount);
                                        status = obj.EmployeeLeaveDebitProcessUpdate(model.EmpDataList[x].EmployeeId, model.LeaveType_Id,model.MonthId, model.Year, model.LeaveDebit,model.EmpDataList[x].LeaveReason);
                                    }
                                }
                            }

                        }





                    }



                    DataTable dtleaveProcess = obj.EmployeeLeaveDebitProcessGet(model.MonthId,model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveDebitProcess(dtleaveProcess, HideColumn);
                        htmlTable.Append("<div class='alert alert-success'>Leave Updated Successfully for the Selected Record(s) !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-success'>Leave Updated Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }

                }




                html = "<div class='alert alert-danger'>No Record !!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LeaveCredit()
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
        [HttpPost]
        public ActionResult LeaveCredit(PayrollUtil model, string command)
        {
            string html = "";
            int? status = null;
            int n = 0;
            int x = 0;
            int y = 0;
            string emplist = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (command == "Search")
                {
                    DataTable dtleaveProcess = obj.EmployeeLeaveCreditGet(model.MonthId, model.ArearYear, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0, 7 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveCredit(dtleaveProcess, HideColumn);
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
                   
                    if (model.EmpDataList != null)
                    {
                        int count = model.EmpDataList.Count;

                        for ( x = 0; x < count; x++)
                        {
                            checkBoxName = "EmpDataList[" + x + "].isRowCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {

                                if (model.EmpDataList[x].EmployeeId != null)
                                {
                                    y = y + 1;
                                    model.LeaveCredit = model.EmpDataList[x].LeaveCredit;
                                    if ((model.LeaveCredit ==null ||model.LeaveCredit == 0) && model.EmpDataList[x].LeaveStatus == null)
                                    {
                                        n = n + 1;
                                       
                                    }
                                    else
                                     {

                                         emplist=emplist+(x+1)+",";
                                        status = obj.EmployeeLeaveCreditUpdate(model.EmpDataList[x].EmployeeId, model.LeaveType_Id, model.MonthId, model.ArearYear, model.LeaveCredit, model.EmpDataList[x].Notes);
                                    }
                                }
                            }
                        }
                    }
                    DataTable dtleaveProcess = obj.EmployeeLeaveCreditGet(model.MonthId, model.ArearYear, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0, 7 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveCredit(dtleaveProcess, HideColumn);

                        if (y == n)
                        {
                            htmlTable.Append("<div class='alert alert-success'>Leave Credit records is not available for updation the Selected  Record(s) !!</div>");

                        }
                        else
                        {
                            htmlTable.Append("<div class='alert alert-success'>Leave Updated Successfully for the Selected Sno. " + emplist + " Record(s) !!</div>");
                        }

                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-success'>Leave Updated Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }

                if (command == "Delete")
                {
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;
                    n = 0;
                    if (model.EmpDataList != null)
                    {
                        int count = model.EmpDataList.Count;

                        for ( x = 0; x < count; x++)
                        {
                            checkBoxName = "EmpDataList[" + x + "].isRowCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {


                                if (model.EmpDataList[x].EmployeeId != null)
                                {
                                    y = y + 1;
                                    model.LeaveCredit = model.EmpDataList[x].LeaveCredit;



                                    if ((model.LeaveCredit == null || model.LeaveCredit == 0) && model.EmpDataList[x].LeaveStatus == null)
                                    {
                                        n = n + 1;
                                    }
                                    else
                                     
                                    {
                                        emplist = emplist + x + ",";
                                        status = obj.EmployeeLeaveCreditDELETE(model.EmpDataList[x].EmployeeId, model.LeaveType_Id, model.MonthId, model.ArearYear);
                                    }
                                }
                            }
                        }
                    }
                    DataTable dtleaveProcess = obj.EmployeeLeaveCreditGet(model.MonthId, model.ArearYear, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    int[] HideColumn = { 0, 7 };
                    if (dtleaveProcess.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableLeaveCredit(dtleaveProcess, HideColumn);
                        if (y == n)
                        {
                            htmlTable.Append("<div class='alert alert-success'>Leave Credit records is not available for Delete for the Selected Record(s) !!</div>");
                        }
                        else
                        {
                            htmlTable.Append("<div class='alert alert-success'>Leave Deleted Successfully for the Selected Sno." + emplist + "  Record(s) !!</div>");
                        }
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-success'>Leave Deleted Successfully for the Selected Record(s) !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }














                html = "<div class='alert alert-danger'>No Record !!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LeaveLedger()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmployeeLeaveLedgerViewModel model = new EmployeeLeaveLedgerViewModel();
                
                int? MonthId = DateTime.Now.Month;
                int? Year = DateTime.Now.Year;

                
             int? status = obj.MonhtlyLeaveProcessBegin(null, null, null, null, null, MonthId, Year);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        [HttpPost]
        public ActionResult LeaveLedgerDetailGet(EmployeeLeaveLedgerViewModel model)
        {
            string html = "";
            try
            {
                Session["LeaveLedgerDetailGet"] = null;
                Session["LeaveHeader"] = null;
                int[] collumnHide = { 0, 1, 2, 3, 4 };
                DataTable dtleave = obj.EmpLeaveLedgerGet(model.EmployeeId, model.LeaveTypeId, model.MonthId, model.YearId);
                if (dtleave.Rows.Count > 0)
                {

                    string ReportHeader = "EMP CODE|" + Convert.ToString(dtleave.Rows[0][1]) + "|EMP NAME|" + Convert.ToString(dtleave.Rows[0][2]) + "|DEPT|" + Convert.ToString(dtleave.Rows[0][3]) + "|DESIGNATION|" + Convert.ToString(dtleave.Rows[0][4]);

                    Session.Add("LeaveHeader", ReportHeader);

                    StringBuilder htmlTable = CommonUtil.htmlTableWithCompanyHeader(dtleave, collumnHide, "<br /><center>LEAVE ROCORD<center><br />" + ReportHeader);
                    DataTable dt = CommonUtil.DataTableColumnRemove(dtleave, collumnHide);
                    Session.Add("LeaveLedgerDetailGet", dtleave);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    //return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
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
        public ActionResult LeaveLedgerRegisterGet(EmployeeLeaveLedgerViewModel model)
        {
            string html = "";
            try
            {
                Session["LeaveLedgerDetailGet"] = null;
                Session["LeaveHeader"] = null;
                int[] collumnHide = { };
                DataSet dtleave = obj.EmpLeaveRegisterGet(model.MonthId, model.YearId, model.EmployeeId, null, null, null, null, null, null, null);
                if (dtleave.Tables.Count > 0)
                {


                    if (dtleave.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableAll(dtleave.Tables[0]);

                        Session.Add("LeaveLedgerDetailGet", dtleave.Tables[0]);
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    //return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
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
        public ActionResult LeaveLedgerSummaryGet(EmployeeLeaveLedgerViewModel model)
        {
            string html = "";
            try
            {
                Session["LeaveLedgerDetailGet"] = null;
                Session["LeaveHeader"] = null;
                int[] collumnHide = { 0, 1, 2, 3 };


                DataTable dtleave = obj.EmpEarnLeaveLedgerGet(model.EmployeeId, model.LeaveTypeId, model.MonthId, model.YearId);
                if (dtleave.Rows.Count > 0)
                {


                    string ReportHeader = "EMP CODE|" + Convert.ToString(dtleave.Rows[0][0]) + "|EMP NAME|" + Convert.ToString(dtleave.Rows[0][1]) + "|DEPT|" + Convert.ToString(dtleave.Rows[0][2]) + "|DESIGNATION|" + Convert.ToString(dtleave.Rows[0][3]);

                    Session.Add("LeaveHeader", ReportHeader);

                    StringBuilder htmlTable = CommonUtil.htmlTableWithCompanyHeader(dtleave, collumnHide, "<br /><center>LEAVE ROCORD<center><br />" + ReportHeader);
                    DataTable dt = CommonUtil.DataTableColumnRemove(dtleave, collumnHide);
                    Session.Add("LeaveLedgerDetailGet", dtleave);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    //return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
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
        public ActionResult LeaveLedger(EmployeeLeaveLedgerViewModel model, string command)
        {

            string html = null;
            try
            {
                if (Session["LeaveLedgerDetailGet"] != null)
                {
                    GridView gv = GridViewHeaderGet((DataTable)Session["LeaveLedgerDetailGet"], Convert.ToString(Session["LeaveHeader"]));

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "LeaveReport", "N", 4);
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "LeaveReport", 10);
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "LeaveReport", 12);
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

        public GridView GridViewHeaderGet(DataTable dt, string ReportHeader)
        {


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            DataTable dtCompany = obj.CurrentCompanyInformationGet(null);
            string CompanyName = Convert.ToString(dtCompany.Rows[0][0]);
            string CompanyAddress = Convert.ToString(dtCompany.Rows[0][1]);
            string CompanyCity = Convert.ToString(dtCompany.Rows[0][2]);
            string PinCode = Convert.ToString(dtCompany.Rows[0][3]);



            DateTimeFormatInfo dinfo1 = new DateTimeFormatInfo();

            HeaderCell2.Text = CompanyName + "<br />" + CompanyAddress + "<br />" + CompanyCity + " " + PinCode + "<br /><center>LEAVE ROCORD<center><br />" + ReportHeader;
            int colsan = dt.Columns.Count;
            HeaderCell2.ColumnSpan = colsan;
            HeaderRow.Cells.Add(HeaderCell2);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Controls[0].Controls.AddAt(0, HeaderRow);

            return GridView1;
        }
    }
}
