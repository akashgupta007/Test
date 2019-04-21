using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Controllers
{
    [ValidateInput(false)]
    public class MasterController : Controller
    {
        //
        // GET: /Payroll/Master/
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public ActionResult Department()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DepartmentViewModel model = new DepartmentViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult DepartmentGet()
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 7, 8, 9, 10, 11 };
                DataTable dtDepartment = obj.DepartmentGet(null, null);
                if (dtDepartment.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
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
        public ActionResult DepartmentDateValidateGet(DepartmentViewModel model)
        {
            string first_start_date = null, first_end_date = null, last_start_date = null, last_end_date = null;
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2 };
                DataTable dtDepartment = obj.DepartmentGet(null, model.DepartmentId);
                if (dtDepartment.Rows.Count > 0)
                {
                    first_start_date = Convert.ToString(dtDepartment.Rows[0]["first_start_date"]);
                    first_end_date = Convert.ToString(dtDepartment.Rows[0]["first_end_date"]);
                    last_start_date = Convert.ToString(dtDepartment.Rows[0]["last_start_date"]);
                    last_end_date = Convert.ToString(dtDepartment.Rows[0]["last_end_date"]);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
                    return Json(new { Flag = 0, first_start_date, first_end_date, last_start_date, last_end_date }, JsonRequestBehavior.AllowGet);
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
        public ActionResult ParentDepartmentGet()
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtDepartment = obj.DepartmentGet(null, null);
            foreach (DataRow dr in dtDepartment.Rows)
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
        public ActionResult DepartmentCreate(DepartmentViewModel model)
        {
            string html = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var a = 1;
                }
                else
                {
                    var a = 1;

                }
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.DepartmentExistsValidate(model.DepartmentName.Trim(), null);
                if (Exist == 0)
                {
                    model.StartDt = model.StartDt2;
                    model.EndDt = model.EndDt2;

                    int? status = obj.DepartmentCreate(model.DepartmentName, model.ParentDepartmentId, model.StartDt, model.EndDt, model.Notes);
                    int[] columnHide = new[] { 0, 2, 7, 8, 9, 10, 11 };
                    DataTable dtDepartment = obj.DepartmentGet(null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
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
        public ActionResult DepartmentUpdate(DepartmentViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.DepartmentExistsValidate(model.DepartmentName.Trim(), model.DepartmentId);
                if (Exist == 0)
                {

                    model.StartDt = model.StartDt2;
                    model.EndDt = model.EndDt2;
                    model.ParentDepartmentId = model.ParentDepartment_Id;

                    int? status = obj.DepartmentUpdate(model.DepartmentId, model.DepartmentName, model.ParentDepartmentId, model.StartDt, model.EndDt, model.Notes);
                    int[] columnHide = new[] { 0, 2, 7, 8, 9, 10, 11 };

                    DataTable dtDepartment = obj.DepartmentGet(null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);



                }
                else
                {
                    return Json(new { Flag = 1, Html = "Department Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DepartmentDelete(DepartmentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.DepartmentDelete(model.DepartmentId);
                int[] columnHide = new[] { 0, 2, 7, 8, 9, 10, 11 };
                DataTable dtDepartment = obj.DepartmentGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Designation()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DesignationViewModel model = new DesignationViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult DesignationGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 3, 8, 10 };
                DataTable dtDesignationGet = obj.DesignationGet(null);
                if (dtDesignationGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDesignationGet, columnHide);
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
        public ActionResult DesignationCreate(DesignationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.DesignationExistsValidate(model.DesignationDesc.Trim(), null);
                if (Exist == 0)
                {
                    int? status = obj.DesignationCreate(model.DesignationDesc, model.Inactive, model.ParentDesgId, model.IsLeaveApproval, model.IsSeeOtherAttendance, model.IsSeeEmpDetails, model.IsSeeEmpPayslip, model.PayScalId);

                    int[] columnHide = new[] { 0, 2, 3, 8, 10 };
                    DataTable dt = obj.DesignationGet(null);

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
        public ActionResult DesignationUpdate(DesignationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.DesignationExistsValidate(model.DesignationDesc.Trim(), model.DesignationId);
                if (Exist == 0)
                {
                    int? status = obj.DesignationUpdate(model.DesignationId, model.DesignationDesc, model.Inactive, model.ParentDesgId, model.IsLeaveApproval, model.IsSeeOtherAttendance, model.IsSeeEmpDetails, model.IsSeeEmpPayslip, model.PayScalId);
                    int[] columnHide = new[] { 0, 2, 3, 8, 10 };
                    DataTable dt = obj.DesignationGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Designation Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DesignationDelete(DesignationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.DesignationDelete(model.DesignationId);
                int[] columnHide = new[] { 0, 2, 3, 8, 10 };
                DataTable dt = obj.DesignationGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EmployeeType()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpTypeViewModel model = new EmpTypeViewModel();


                model.AttendanceSourceList = new List<SelectListItem>();
                DataTable dtAttendanceSourceList = obj.AttendanceSourceGet();
                model.AttendanceSourceList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (DataRow dr in dtAttendanceSourceList.Rows)
                {

                    model.AttendanceSourceList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });

                }
                model.WorkUnitList = new List<SelectListItem>();
                DataTable dtWorkUnitList = obj.WorkUnitGet(null);
                model.WorkUnitList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtWorkUnitList.Rows)
                {

                    model.WorkUnitList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });

                }



                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeTypeGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 3, 5 };
                DataTable dt = obj.EmpTypeGet(null);
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
        public ActionResult EmployeeCreate(EmpTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpTypeExistsValidate(model.EmpTypeDesc, null);
                if (Exist == 0)
                {
                    int? status = obj.EmpTypeCreate(model.EmpTypeDesc, model.Notes, model.AttendanceSourceId, model.WorkUnit, model.WorkUnitValue);

                    int[] columnHide = new[] { 0, 3, 5 };
                    DataTable dt = obj.EmpTypeGet(null);

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
        public ActionResult Employeeupdate(EmpTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.EmpTypeExistsValidate(model.EmpTypeDesc, model.EmpTypeId);
                if (Exist == 0)
                {
                    int? status = obj.EmpTypeUpdate(model.EmpTypeId, model.EmpTypeDesc, model.Notes, model.AttendanceSourceId, model.WorkUnit, model.WorkUnitValue);
                    int[] columnHide = new[] { 0, 3, 5 };
                    DataTable dt = obj.EmpTypeGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Employee Type  Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Employeedelete(EmpTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpTypeDelete(model.EmpTypeId);
                int[] columnHide = new[] { 0, 3, 5 };
                DataTable dt = obj.EmpTypeGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Project()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                ProjectViewModel model = new ProjectViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProjectGet()
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 6, 7, 8, 9, 10, 11 };
                DataTable dtProject = obj.ProjectGet(null, null);
                if (dtProject.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtProject, columnHide);
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
        public ActionResult ProjectDateValidateGet(ProjectViewModel model)
        {
            string first_start_date = null, first_end_date = null, last_start_date = null, last_end_date = null;
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2 };
                DataTable dtProject = obj.ProjectGet(null, model.ProjectId);
                if (dtProject.Rows.Count > 0)
                {
                    first_start_date = Convert.ToString(dtProject.Rows[0]["first_start_date"]);
                    first_end_date = Convert.ToString(dtProject.Rows[0]["first_end_date"]);
                    last_start_date = Convert.ToString(dtProject.Rows[0]["last_start_date"]);
                    last_end_date = Convert.ToString(dtProject.Rows[0]["last_end_date"]);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtProject, columnHide);
                    return Json(new { Flag = 0, first_start_date, first_end_date, last_start_date, last_end_date }, JsonRequestBehavior.AllowGet);
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
        public ActionResult ParentProjectGet(ProjectViewModel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtProject = obj.ProjectGet(true, null);
            foreach (DataRow dr in dtProject.Rows)
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
        public ActionResult ProjectCreate(ProjectViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.ProjectExistsValidate(model.ProjectName.Trim(), null);
                if (Exist == 0)
                {
                    model.StartDt = model.StartDt2;
                    model.EndDt = model.EndDt2;

                    int? status = obj.ProjectCreate(model.ProjectName, model.ParentProjectId, model.StartDt, model.EndDt, model.Active);

                    int[] columnHide = new[] { 0, 2, 6, 7, 8, 9, 10, 11 };
                    DataTable dtProject = obj.ProjectGet(null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtProject, columnHide);
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
        public ActionResult ProjectUpdate(ProjectViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.ProjectExistsValidate(model.ProjectName.Trim(), model.ProjectId);
                if (Exist == 0)
                {
                    model.StartDt = model.StartDt2;
                    model.EndDt = model.EndDt2;
                    model.ParentProjectId = model.ParentProject_Id;


                    int? status = obj.ProjectUpdate(model.ProjectId, model.ProjectName, model.ParentProjectId, model.StartDt, model.EndDt, model.Active);

                    int[] columnHide = new[] { 0, 2, 6, 7, 8, 9, 10, 11 };
                    DataTable dtProject = obj.ProjectGet(null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtProject, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Project Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ProjectDelete(ProjectViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.ProjectDelete(model.ProjectId);
                int[] columnHide = new[] { 0, 2, 6, 7, 8, 9, 10, 11 };
                DataTable dtProject = obj.ProjectGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtProject, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //employee bank 

        public ActionResult EmployeeBank()
        {
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
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeBankGet()
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dtEmployeeBank = obj.EmployeeBankGet();
                if (dtEmployeeBank.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
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
        public ActionResult EmployeeBankCreate(EmployeeBankViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmployeeBankExistsValidate(model.EmpBankName.Trim(), model.EmpBranchName, model.EmpBankCode, model.BankIfscCode, null);
                if (Exist == 0)
                {
                    int? status = obj.EmployeeBankCreate(model.EmpBankName, model.EmpBranchName, model.EmpBankCode, model.BankIfscCode, model.BankMicrCode, model.EmpBankAddress, model.BankPinCode,model.BankAccountNo,model.CustomerID);

                    int[] columnHide = new[] { 0 };
                    DataTable dtEmployeeBank = obj.EmployeeBankGet();

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
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
        public ActionResult EmployeeBankUpdate(EmployeeBankViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmployeeBankExistsValidate(model.EmpBankName.Trim(), model.EmpBranchName, model.EmpBankCode, model.BankIfscCode, model.EmpBankId);
                if (Exist == 0)
                {

                    int? status = obj.EmployeeBankUpdate(model.EmpBankId, model.EmpBankName, model.EmpBranchName, model.EmpBankCode, model.BankIfscCode, model.BankMicrCode, model.EmpBankAddress, model.BankPinCode, model.BankAccountNo,model.CustomerID);

                    int[] columnHide = new[] { 0 };
                    DataTable dtEmployeeBank = obj.EmployeeBankGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
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
        public ActionResult EmployeeBankDelete(EmployeeBankViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmployeeBankDelete(model.EmpBankId);
                int[] columnHide = new[] { 0 };
                DataTable dtEmployeeBank = obj.EmployeeBankGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //Bank file format

        public ActionResult BankFileFormat()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                BankFileformatViewModel model = new BankFileformatViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult BankFileFormatGet()
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                //DataTable dtEmployeeBank = obj.BankFileFormatGet();
                DataTable dtEmployeeBank = null;
                if (dtEmployeeBank.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
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
        public ActionResult BankFileFormatCreate(BankFileformatViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.BankFileFormatExistsValidate(model.BankId);
                if (Exist == 0)
                {
                    int? status = obj.BankFileFormatCreate(model.BankId, model.Suffix, model.Prefix, model.Date, model.MonthtypeID, model.IsSequence);

                    int[] columnHide = new[] { 0 };
                    //DataTable dtEmployeeBank = obj.BankFileFormatGet();
                    DataTable dtEmployeeBank = null;

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
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
        public ActionResult BankFileFormatUpdate(BankFileformatViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.BankFileFormatExistsValidate(model.BankId);
                if (Exist == 0)
                {

                    int? status = obj.BankFileFormatUpdate(model.BankFileFormatId, model.BankId, model.Suffix, model.Prefix, model.Date, model.MonthtypeID, model.IsSequence);

                    int[] columnHide = new[] { 0 };
              //   DataTable dtEmployeeBank = obj.BankFileFormatGet();
                    DataTable dtEmployeeBank = null;
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
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
        public ActionResult BankFileFormatDelete(BankFileformatViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.BankFileFormatDelete(model.BankFileFormatId);
                int[] columnHide = new[] { 0 };
                //DataTable dtEmployeeBank = obj.BankFileFormatGet();
                DataTable dtEmployeeBank =null;
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeBank, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        //----------------salary roup-----------------------------
        public ActionResult EmpSalaryGroup()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpSalaryGroupViewModel model = new EmpSalaryGroupViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }

        [HttpPost]
        public ActionResult EmpSalaryGroupGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 3, 4 };
                DataTable dtEmpSalaryGroup = obj.EmpSalaryGroupGet();
                if (dtEmpSalaryGroup.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtEmpSalaryGroup, columnHide);
                    return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmpSalaryGroupCreate(EmpSalaryGroupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.EmpSalaryGroupExistsValidate(model.EmpSalaryGroupName);
                if (Exist == 0)
                {
                    int? status = obj.EmpSalaryGroupCreate(model.EmpSalaryGroupName, model.IsFreeze);
                    int[] columnHide = new[] { 0, 2, 3, 4 };
                    DataTable dtEmpSalaryGroup = obj.EmpSalaryGroupGet();
                    StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtEmpSalaryGroup, columnHide);
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
        public ActionResult EmpSalaryGroupUpdate(EmpSalaryGroupViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int? status = obj.EmpSalaryGroupUpdate(model.EmpSalaryGroupId, model.EmpSalaryGroupName, model.IsFreeze);

                int[] columnHide = new[] { 0, 2, 3, 4 };
                DataTable dtEmpSalaryGroup = obj.EmpSalaryGroupGet();
                StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtEmpSalaryGroup, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmpSalaryGroupDelete(EmpSalaryGroupViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpSalaryGroupDelete(model.EmpSalaryGroupId);

                int[] columnHide = new[] { 0, 2, 3, 4 };
                DataTable dtEmpParameter = obj.EmpSalaryGroupGet();
                StringBuilder htmlTable = CommonUtil.htmlNestedTableEditMode(dtEmpParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        //-----------------Employee Salary Item Details -----------------------

        [HttpPost]
        public ActionResult GroupSalaryItemGet(int? EmpSalaryGroupId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 4, 6, 9, 11, 13, 14, 15, 16 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryGroupDetailGet(EmpSalaryGroupId);

                if (dtEmpSalaryItem.Rows.Count > 0)
                {


                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
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
        public ActionResult GroupSalaryItemCreate(EmpSalaryGroupViewModel model)
        {
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string IsCheck = string.Empty;

                IsCheck = Request["IsComputedItem"];

                if (IsCheck == "on")
                {
                    model.IsComputedItem = true;
                }
                else
                {
                    model.IsComputedItem = false;
                }



                if (model.PayrollItemId == null)
                {

                    model.PayrollItemId = model.PayrollItem_Id;
                }

                if (model.Payroll_ValueType == null)
                {
                    model.Payroll_ValueType = model.PayrollValueType;
                }






                int? Exist = obj.EmpSalaryGroupDetailExistsValidate(model.EmpSalaryGroupId, model.PayrollItemId, model.StartDt, model.EndDt, null);
                if (Exist == 0)
                {
                    int? status = obj.EmpSalaryGroupDetailCreate(model.EmpSalaryGroupId, model.PayrollItemId, model.PayPercent, model.Amount, model.IsComputedItem, model.StartDt, model.EndDt, model.FunctionId, model.Payroll_ValueType);

                    int[] columnHide = new[] { 0, 1, 2, 4, 6, 9, 11, 13, 14, 15, 16 };
                    DataTable dtEmpSalaryItem = obj.EmpSalaryGroupDetailGet(model.EmpSalaryGroupId);

                    if (dtEmpSalaryItem.Rows.Count > 0)
                    {


                        StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
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
        public ActionResult GroupSalaryItemUpdate(EmpSalaryGroupViewModel model)
        {
            string html = null;

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                string IsCheck = string.Empty;

                IsCheck = Request["IsComputedItem"];

                if (IsCheck == "on")
                {
                    model.IsComputedItem = true;
                }
                else
                {
                    model.IsComputedItem = false;
                }
                if (model.PayrollItemId == null)
                {

                    model.PayrollItemId = model.PayrollItem_Id;
                }

                if (model.Payroll_ValueType == null)
                {
                    model.Payroll_ValueType = model.PayrollValueType;
                }


                int? Exist = obj.EmpSalaryGroupDetailExistsValidate(model.EmpSalaryGroupId, model.PayrollItemId, model.StartDt, model.EndDt, model.EmpSalaryGroupDetailId);
                if (Exist == 0)
                {
                    int? status = obj.EmpSalaryGroupDetailUpdate(model.EmpSalaryGroupDetailId, model.EmpSalaryGroupId, model.PayrollItemId, model.PayPercent, model.Amount, model.IsComputedItem, model.StartDt, model.EndDt, model.FunctionId, model.Payroll_ValueType);

                    int[] columnHide = new[] { 0, 1, 2, 4, 6, 9, 11, 13, 14, 15, 16 };
                    DataTable dtEmpSalaryItem = obj.EmpSalaryGroupDetailGet(model.EmpSalaryGroupId);

                    if (dtEmpSalaryItem.Rows.Count > 0)
                    {


                        StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
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
        public ActionResult GroupSalaryItemDelete(EmpSalaryGroupViewModel model)
        {
            string html = null;
            int? t3 = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpSalaryGroupDetailDelete(model.EmpSalaryGroupDetailId);

                int[] columnHide = new[] { 0, 1, 2, 4, 6, 9, 11, 13, 14, 15, 16 };
                DataTable dtEmpSalaryItem = obj.EmpSalaryGroupDetailGet(model.EmpSalaryGroupId);

                if (dtEmpSalaryItem.Rows.Count > 0)
                {


                    StringBuilder htmlTable = CommonUtil.htmlChildTableEditMode(dtEmpSalaryItem, columnHide);
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

        public ActionResult EmpParameter()
        {
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
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }

        [HttpPost]
        public ActionResult EmpParameterGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 3 };
                DataTable dtEmpParameter = obj.EmpParameterGet();
                if (dtEmpParameter.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpParameter, columnHide);
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

        public ActionResult EmpParameterCreate(EmpParameterViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.EmpParameterExistsValidate(model.ParameterName.Trim());
                if (Exist == 0)
                {
                    int? status = obj.EmpParameterCreate(model.ParameterName, model.ParameterDescription, model.ParamDataType);
                    int[] columnHide = new[] { 0, 3 };
                    DataTable dtEmpParameter = obj.EmpParameterGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpParameter, columnHide);
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

        public ActionResult EmpParameterUpdate(EmpParameterViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int? status = obj.EmpParameterUpdate(model.ParameterId, model.ParameterName, model.ParameterDescription, model.ParamDataType);

                int[] columnHide = new[] { 0, 3 };
                DataTable dtEmpParameter = obj.EmpParameterGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]

        public ActionResult EmpParameterDelete(EmpParameterViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpParameterDelete(model.ParameterId);

                int[] columnHide = new[] { 0, 3 };
                DataTable dtEmpParameter = obj.EmpParameterGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmpParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LeaveType()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                //List<SelectListItem> LeaveRequestTypeListItems = new List<SelectListItem>();
                //LeaveRequestTypeListItems.Add(new SelectListItem { Selected = true, Text = "Origination", Value = "Origination" });
                //LeaveRequestTypeListItems.Add(new SelectListItem { Text = "Surrender", Value = "Surrender" });
                //ViewData["LeaveRequestTypeListItems"] = LeaveRequestTypeListItems;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }

        [HttpPost]
        public ActionResult LeaveTypeGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
                DataTable dtLeaveType = obj.LeaveTypeGet();
                if (dtLeaveType.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtLeaveType, columnHide);
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
                return Json(new { Flag = 0, html = html.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LeaveTypeCreate(LeaveTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exists = obj.LeaveTypeExistsValidate(model.LeaveTypeName.Trim(), null);
                if (Exists == 0)
                {
                    int? status = obj.LeaveTypeCreate(model.LeaveTypeName, model.LeaveDescription, model.DisplayOrder);
                    int[] columnHide = new[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
                    DataTable dtLeaveType = obj.LeaveTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtLeaveType, columnHide);
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
        public ActionResult LeaveTypeUpdate(LeaveTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exists = obj.LeaveTypeExistsValidate(model.LeaveTypeName.Trim(), model.LeaveTypeId);
                if (Exists == 0)
                {
                    int? status = obj.LeaveTypeUpdate(model.LeaveTypeId, model.LeaveTypeName, model.LeaveDescription, model.DisplayOrder);
                    int[] columnHide = new[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
                    DataTable dtLeaveType = obj.LeaveTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtLeaveType, columnHide);
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
        public ActionResult LeaveTypeDelete(LeaveTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.LeaveTypeDelete(model.LeaveTypeId);
                int[] columnHide = new[] { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
                DataTable dtLeaveType = obj.LeaveTypeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtLeaveType, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult TdsLimit()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                TdsLimitViewModel model = new TdsLimitViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }

        [HttpPost]
        public ActionResult TdsLimitGet(TdsLimitViewModel model)
        {
            List<TdsLimitViewModel> modellist = new List<TdsLimitViewModel>();
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataTable dtTdsLimit = obj.TdsLimitGet(model.FiscalYear);
                if (dtTdsLimit.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTdsLimit.Rows)
                    {

                        //tds_limit_id as TdsLimitId 0,
                        //fiscal_year as FiscalYear 1,
                        //metro_pct as MetroPct 2,
                        //non_metro_pct as NonMetroPct 3,
                        //hra_exemption as HraExemption 4,
                        //trans_exemption as TransExemption 5,
                        //other_exemption as OtherExemption 6,
                        //med_bill_exemption as MedBillExemption 7,
                        //child_edu_exemption as ChildEduExemption 8,
                        //lta_exemption as LtaExemption 9,
                        //vehi_main_exemption as VehiMainExemption 10,
                        //house_property_income as HousePropertyIncome 11,
                        //house_loan_interest as HouseLoanInterest 12,
                        //other_income as OtherIncome 13,
                        //med_insur_premium as MedInsurPremium 14,
                        //med_insur_premium_par as MedInsurPremiumPar 15,
                        //med_handicap_depend as MedHandicapDepend 16,
                        //med_spec_disease as MedSpecDisease 17,
                        //high_edu_loan_repayment as HighEduLoanRepayment 18,
                        //donate_fund_charity as DonateFundCharity 19,
                        //rent_deduction as RentDeduction 20,
                        //permanent_disable_deduction as PermanentDisableDeduction 21,
                        //interest_on_deposit as InterestOnDeposit 22,
                        //royalty_income_deduction as RoyaltyIncomeDeduction 23,
                        //royalty_patent_deduction as RoyaltyPatentDeduction 24,
                        //other_deduction as OtherDeduction 25,
                        //pension_scheme as PensionScheme 26,
                        //nsc as Nsc 27,
                        //ppf as Ppf 28,
                        //pf as Pf 29,
                        //infra_bond as InfraBond 30,
                        //child_edu_fund as ChildEduFund 31,
                        //house_loan_principal_repay as HouseLoanPrincipalRepay 32,
                        //insurance_premium as InsurancePremium 33,
                        //mutual_fund as MutualFund 34,
                        //fd as Fd 35,
                        //uniform_exemption as UniformExemption 36,
                        //savingbank_interest as SavingbankInterest 37,
                        //savingbank_interest_exception as SavingbankInterestException 38,
                        //rajivgandhisavingsscheme as Rajivgandhisavingsscheme 39

                        modellist.Add(new TdsLimitViewModel()
                        {
                            TdsLimitId = Convert.ToInt32(dr[0] is DBNull ? null : dr[0]),
                            MetroPct = Convert.ToInt32(dr[2] is DBNull ? null : dr[2]),
                            NonMetroPct = Convert.ToInt32(dr[3] is DBNull ? null : dr[3]),
                            TransExemption = Convert.ToDecimal(dr[5] is DBNull ? null : dr[5]),
                            OtherExemption = Convert.ToDecimal(dr[6] is DBNull ? null : dr[6]),
                            MedBillExemption = Convert.ToDecimal(dr[7] is DBNull ? null : dr[7]),
                            ChildEduExemption = Convert.ToDecimal(dr[8] is DBNull ? null : dr[8]),
                            LtaExemption = Convert.ToDecimal(dr[9] is DBNull ? null : dr[9]),
                            HousePropertyIncome = Convert.ToDecimal(dr[11] is DBNull ? null : dr[11]),
                            HouseLoanInterest = Convert.ToDecimal(dr[12] is DBNull ? null : dr[12]),
                            OtherIncome = Convert.ToDecimal(dr[13] is DBNull ? null : dr[13]),
                            MedInsurPremium = Convert.ToDecimal(dr[14] is DBNull ? null : dr[14]),
                            MedInsurPremiumPar = Convert.ToDecimal(dr[15] is DBNull ? null : dr[15]),
                            MedHandicapDepend = Convert.ToDecimal(dr[16] is DBNull ? null : dr[16]),
                            MedSpecDisease = Convert.ToDecimal(dr[17] is DBNull ? null : dr[17]),
                            HighEduLoanRepayment = Convert.ToDecimal(dr[18] is DBNull ? null : dr[18]),
                            DonateFundCharity = Convert.ToDecimal(dr[19] is DBNull ? null : dr[19]),
                            RentDeduction = Convert.ToDecimal(dr[20] is DBNull ? null : dr[20]),
                            PermanentDisableDeduction = Convert.ToDecimal(dr[21] is DBNull ? null : dr[21]),
                            OtherDeduction = Convert.ToDecimal(dr[25] is DBNull ? null : dr[25]),
                            PensionScheme = Convert.ToDecimal(dr[26] is DBNull ? null : dr[26]),
                            Nsc = Convert.ToDecimal(dr[27] is DBNull ? null : dr[27]),
                            Ppf = Convert.ToDecimal(dr[28] is DBNull ? null : dr[28]),
                            Pf = Convert.ToDecimal(dr[29] is DBNull ? null : dr[29]),
                            ChildEduFund = Convert.ToDecimal(dr[31] is DBNull ? null : dr[31]),
                            HouseLoanPrincipalRepay = Convert.ToDecimal(dr[32] is DBNull ? null : dr[32]),
                            InsurancePremium = Convert.ToDecimal(dr[33] is DBNull ? null : dr[33]),
                            MutualFund = Convert.ToDecimal(dr[34] is DBNull ? null : dr[34]),
                            Fd = Convert.ToDecimal(dr[35] is DBNull ? null : dr[35]),
                            UniformExemption = Convert.ToDecimal(dr[36] is DBNull ? null : dr[36]),
                            SavingbankInterest = Convert.ToDecimal(dr[37] is DBNull ? null : dr[37]),
                            Rajivgandhisavingsscheme = Convert.ToDecimal(dr[39] is DBNull ? null : dr[39])
                        });
                    }
                    return Json(modellist);
                }
                else
                {
                    return Json("");
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult TdsLimitCreate(TdsLimitViewModel model)
        {
            List<TdsLimitViewModel> modellist = new List<TdsLimitViewModel>();
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //int? Exists = obj.TdsLimitExistsValidate(model.FiscalYear);
                //if (Exists == 0)
                //{
                //    int? status = obj.TdsLimitCreate(model.FiscalYear, model.MetroPct, model.NonMetroPct, model.HraExemption, model.TransExemption, model.OtherExemption,
                //        model.MedBillExemption, model.ChildEduExemption, model.LtaExemption, model.VehiMainExemption, model.HousePropertyIncome, model.HouseLoanInterest,
                //        model.OtherIncome, model.MedInsurPremium, model.MedInsurPremiumPar, model.MedHandicapDepend, model.MedSpecDisease, model.HighEduLoanRepayment,
                //        model.DonateFundCharity, model.RentDeduction, model.PermanentDisableDeduction, model.InterestOnDeposit, model.RoyaltyIncomeDeduction,
                //        model.RoyaltyPatentDeduction, model.OtherDeduction, model.PensionScheme, model.Nsc, model.Ppf, model.Pf, model.InfraBond, model.ChildEduFund,
                //        model.HouseLoanPrincipalRepay, model.InsurancePremium, model.MutualFund, model.Fd, model.UniformExemption, model.SavingbankInterest,
                //        model.SavingbankInterestException, model.Rajivgandhisavingsscheme);

                //    if (status == 0)
                //    {
                //        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        html = "";
                //        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                //    }
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                //}
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult TdsLimitUpdate(TdsLimitViewModel model)
        {
            List<TdsLimitViewModel> modellist = new List<TdsLimitViewModel>();
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                //int? status = obj.TdsLimitUpdate(model.TdsLimitId, model.FiscalYear, model.MetroPct, model.NonMetroPct, model.HraExemption, model.TransExemption, model.OtherExemption,
                //    model.MedBillExemption, model.ChildEduExemption, model.LtaExemption, model.VehiMainExemption, model.HousePropertyIncome, model.HouseLoanInterest, model.OtherIncome,
                //    model.MedInsurPremium, model.MedInsurPremiumPar, model.MedHandicapDepend, model.MedSpecDisease, model.HighEduLoanRepayment, model.DonateFundCharity, model.RentDeduction,
                //    model.PermanentDisableDeduction, model.InterestOnDeposit, model.RoyaltyIncomeDeduction, model.RoyaltyPatentDeduction, model.OtherDeduction, model.PensionScheme,
                //    model.Nsc, model.Ppf, model.Pf, model.InfraBond, model.ChildEduFund, model.HouseLoanPrincipalRepay, model.InsurancePremium, model.MutualFund, model.Fd,
                //    model.UniformExemption, model.SavingbankInterest, model.SavingbankInterestException, model.Rajivgandhisavingsscheme);

                //if (status == 0)
                //{
                //    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                    html = "";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
               // }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult TdsLimitDelete(TdsLimitViewModel model)
        {
            List<TdsLimitViewModel> modellist = new List<TdsLimitViewModel>();
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.TdsLimitDelete(model.TdsLimitId);
                //DataTable dtTdsLimit = obj.TdsLimitGet(model.FiscalYear);
                //if (dtTdsLimit.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dtTdsLimit.Rows)
                //    {
                //        modellist.Add(new TdsLimitViewModel() { TdsLimitId = Convert.ToInt32(dr[0]), MetroPct = Convert.ToInt32(dr[2]), NonMetroPct = Convert.ToInt32(dr[3]), TransExemption = Convert.ToInt32(dr[5]), OtherExemption = Convert.ToInt32(dr[6]), MedBillExemption = Convert.ToInt32(dr[7]), ChildEduExemption = Convert.ToInt32(dr[8]), LtaExemption = Convert.ToInt32(dr[9]) });
                //    }
                //    return Json(modellist);
                //}
                //else
                //{
                //    return Json(new { Flag = 0 , Html=html }, JsonRequestBehavior.AllowGet);
                //}
                if (status == 0)
                {
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "";
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
        public ActionResult FiskalYearGet(TdsLimitViewModel model)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtFiskalYear = obj.FinancialYearGet(null);
            foreach (DataRow dr in dtFiskalYear.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[0].ToString(),
                    Value = dr[0].ToString()

                });
            }

            var list = SectionListItems;
            return Json(list);
        }

        //-------------- Tds Slab ---------------------------

        [HttpPost]
        public ActionResult TdsSlabGet(string ficsalyear)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dtTaxSlabGet = obj.TaxSlabGet(ficsalyear);
                if (dtTaxSlabGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtTaxSlabGet, columnHide);
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
        public ActionResult TdsSlabCreate(TdsLimitViewModel model)
        {
            string html = null;
            int countslab = 0;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? isExists = obj.TdsLimitExistsValidate(model.FiscalYear);
                if (isExists > 0)
                {

                    int? Exists = obj.TaxSlabExistsValidate(model.TaxSlabName, model.FiscalYearDetail, null);
                    if (Exists == 0)
                    {
                        model.FiscalYearDetail = model.FiscalYearDetail.Trim();
                        DataTable dtTaxGet = obj.TaxSlabGet(model.FiscalYearDetail);
                        countslab = dtTaxGet.Rows.Count;
                        countslab += 1;
                        if (countslab < 5)
                        {
                            model.TaxSlabName = "TaxSlab" + countslab;
                            int? status = obj.TaxSlabCreate(model.TaxSlabName, model.TdsLimitId, model.FiscalYearDetail, model.MinvalMale, model.MaxvalMale, model.MinvalFemale, model.MaxvalFemale, model.MinvalSeniorcitizon, model.MaxvalSeniorcitizon, model.TaxRate, model.Description);
                            int[] columnHide = new[] { 0 };
                            DataTable dtTaxSlabGet = obj.TaxSlabGet(model.FiscalYearDetail);
                            if (dtTaxSlabGet.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtTaxSlabGet, columnHide);
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
                            html = "<div class='alert alert-danger'>Only Four Record will Entered !!</div>";
                            return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "First Enter Tds Limit Setup !";
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
        public ActionResult TaxSlabUpdate(TdsLimitViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                model.FiscalYearDetail = model.FiscalYearDetail.Trim();
                int? Exists = obj.TaxSlabExistsValidate(model.TaxSlabName, model.FiscalYearDetail, model.TaxSlabId);
                if (Exists == 0)
                {
                    int? status = obj.TaxSlabUpdate(model.TaxSlabId, model.TaxSlabName, model.TdsLimitId, model.FiscalYearDetail, model.MinvalMale, model.MaxvalMale, model.MinvalFemale, model.MaxvalFemale, model.MinvalSeniorcitizon, model.MaxvalSeniorcitizon, model.TaxRate, model.Description);
                    int[] columnHide = new[] { 0 };
                    DataTable dtTaxSlabGet = obj.TaxSlabGet(model.FiscalYearDetail);
                    if (dtTaxSlabGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtTaxSlabGet, columnHide);
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
                    return Json(new { Flag = 1, Html = "Tax Slab Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult TaxSlabDelete(TdsLimitViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.TaxSlabDelete(model.TaxSlabId);
                int[] columnHide = new[] { 0 };
                DataTable dtTaxSlabGet = obj.TaxSlabGet(model.FiscalYearDetail);
                if (dtTaxSlabGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtTaxSlabGet, columnHide);
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

        public ActionResult EmployeeShift()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmployeeShiftViewModel model = new EmployeeShiftViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeShiftGet(EmployeeShiftViewModel model)
        {

            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dtShiftGet = obj.ShiftGet();
                if (dtShiftGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtShiftGet, columnHide);
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
        public ActionResult EmployeeShiftCreate(EmployeeShiftViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exists = obj.ShiftExistsValidate(model.ShiftName, null);
                if (Exists == 0)
                {
                    if (model.ShiftEndTime > model.ShiftStartTime)
                    {
                        int? status = obj.ShiftCreate(model.ShiftName, model.ShiftStartTime, model.ShiftEndTime);
                        int[] columnHide = new[] { 0 };
                        DataTable dtShiftGet = obj.ShiftGet();
                        if (dtShiftGet.Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtShiftGet, columnHide);
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
                        return Json(new { Flag = 2, Html = "Start Time should be less than End Time" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeShiftUpdate(EmployeeShiftViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                if (model.ShiftEndTime > model.ShiftStartTime)
                {
                    int? Exists = obj.ShiftExistsValidate(model.ShiftName, model.ShiftId);
                    if (Exists == 0)
                    {
                        int? status = obj.ShiftUpdate(model.ShiftId, model.ShiftName, model.ShiftStartTime, model.ShiftEndTime, null);
                        int[] columnHide = new[] { 0 };
                        DataTable dtShiftGet = obj.ShiftGet();
                        if (dtShiftGet.Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtShiftGet, columnHide);
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
                        return Json(new { Flag = 1, Html = "Shift Name Already Exists !" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Flag = 2, Html = "Start Time should be less than End Time" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeShiftDelete(EmployeeShiftViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.ShiftDelete(model.ShiftId);
                int[] columnHide = new[] { 0 };
                DataTable dtShiftGet = obj.ShiftGet();
                if (dtShiftGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtShiftGet, columnHide);
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


        // -------------- Employee Category -----------------------

        public ActionResult EmployeeCategory()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                EmpCategoryViewModel model = new EmpCategoryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeCategoryGet()
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dtEmployeeCategory = obj.EmpCategoryGet();
                if (dtEmployeeCategory.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeCategory, columnHide);
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
        public ActionResult EmployeeCategoryUpdate(EmpCategoryViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.EmpCategoryExistsValidate(model.EmpCategoryName, model.EmpCategoryId);
                if (Exist == 0)
                {
                    int? status = obj.EmpCategoryUpdate(model.EmpCategoryId, model.EmpCategoryName, model.Notes);

                    int[] columnHide = new[] { 0 };
                    DataTable dtEmployeeCategory = obj.EmpCategoryGet();
                    if (dtEmployeeCategory.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeCategory, columnHide);
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
                    return Json(new { Flag = 1, Html = "Category Name Already Exists !!" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EmployeeCategoryDelete(EmpCategoryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpCategoryDelete(model.EmpCategoryId);
                int[] columnHide = new[] { 0 };
                DataTable dtEmployeeCategory = obj.EmpCategoryGet();
                if (dtEmployeeCategory.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeCategory, columnHide);
                    return Json(new { Flag = 1, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeCategoryCreate(EmpCategoryViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpCategoryExistsValidate(model.EmpCategoryName, null);
                if (Exist == 0)
                {
                    int? status = obj.EmpCategoryCreate(model.EmpCategoryName, model.Notes);

                    int[] columnHide = new[] { 0 };
                    DataTable dtEmployeeCategory = obj.EmpCategoryGet();
                    if (dtEmployeeCategory.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeCategory, columnHide);
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
                    return Json(new { Flag = 2, Html = "Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult DepartmentExcelGet(DepartmentViewModel model)
        {
            DataTable dtresult = null;
            string html = null;
            try
            {
                if (ModelState.IsValid)
                {
                    var a = 1;
                }
                else
                {
                    var a = 1;

                }
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

                //int? Exist = obj.DepartmentExistsValidate(model.DepartmentName.Trim(), null);

                if (dtresult.Rows.Count > 0)
                {
                    string msg = "Department Alreay Exist";
                    for (int j = 0; j < dtresult.Rows.Count; j++)
                    {
                        model.StartDt = model.StartDt2 = Convert.ToDateTime(dtresult.Rows[j][2]);
                        model.EndDt = model.EndDt2;
                        model.DepartmentName = dtresult.Rows[j][0].ToString() == "" || dtresult.Rows[j][0].ToString() == null ? "" : dtresult.Rows[j][0].ToString();
                        model.Notes = dtresult.Rows[j][3].ToString() == "" || dtresult.Rows[j][3].ToString() == null ? "" : dtresult.Rows[j][3].ToString();
                        int? Exist = obj.DepartmentExistsValidate(dtresult.Rows[j][0].ToString().Trim(), null);
                        if (Exist == 0)
                        {
                            int? status = obj.DepartmentCreate(model.DepartmentName, model.ParentDepartmentId, model.StartDt, model.EndDt, model.Notes);
                        }
                        else
                        {

                            return Json(new { Flag = 2, Html = (msg + " :   <b style='color:blue'>"+model.DepartmentName+"</b>").ToString() }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    int[] columnHide = new[] { 0, 2, 7, 8, 9, 10, 11 };
                    DataTable dtDepartment = obj.DepartmentGet(null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDepartment, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString()  }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { Flag = 2, Html = "There are no Records to import" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();

                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
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
    }
}
