using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Controllers
{
    public class PayrollController : Controller
    {
        //
        // GET: /Payroll/Payroll/
        //----------------Payroll Item Type-----------------//
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public ActionResult PayrollItemType()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollItemTypeViewModel model = new PayrollItemTypeViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollItemTypeGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 3 };
                DataTable dtPayrollItemType = obj.PayrollItemTypeGet();
                if (dtPayrollItemType.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemType, columnHide);
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
        public ActionResult PayrollItemTypeCreate(PayrollItemTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollItemTypeExistsValidate(model.PayrollItemTypeDesc, null);
                if (Exist == 0)
                {
                    int? status = obj.PayrollItemTypeCreate(model.PayrollItemTypeDesc, model.AddDeduct, model.PayrollTemplateId);
                    int[] columnHide = { 0, 3 };
                    DataTable dtPayrollItemType = obj.PayrollItemTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemType, columnHide);
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
        public ActionResult PayrollItemTypeUpdate(PayrollItemTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    RedirectToAction("Login", "Home");
                }
                int? Exist = obj.PayrollItemTypeExistsValidate(model.PayrollItemTypeDesc, model.PayrollItemTypeId);
                if (Exist == 0)
                {
                    int? status = obj.PayrollItemTypeUpdate(model.PayrollItemTypeId, model.PayrollItemTypeDesc, model.AddDeduct, model.PayrollTemplateId);
                    int[] columnHide = { 0, 3 };
                    DataTable dtPayrollItemType = obj.PayrollItemTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemType, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Payroll Item Type Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayrollItemTypeDelete(PayrollItemTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    RedirectToAction("Login", "Home");
                }
                int? status = obj.PayrollItemTypeDelete(model.PayrollItemTypeId);
                int[] collumnHide = { 0, 3 };
                DataTable dtPayrollItemType = obj.PayrollItemTypeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemType, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //----------------Payroll Item-----------------//
        public ActionResult PayrollItem()
        {
            if (Session["userName"] == null)
            {
                RedirectToAction("Login", "home");
            }
            PayrollItemViewModel model = new PayrollItemViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PayrollItemGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 2,3, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22 };
                DataTable dtPayrollItem = obj.PayrollItemGet();
                if (dtPayrollItem.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItem, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'> No Record !!</div>";
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
        public ActionResult PayrollItemCreate(PayrollItemViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    RedirectToAction("Login", "Home");
                }
                int? Exist = obj.PayrollItemExistsValidate(model.PayrollItemDesc, null);
                if (Exist == 0)
                {
                    int? status = obj.PayrollItemCreate(model.PayrollItemDesc, Convert.ToInt32(model.PayrollTypeId), model.PayrollItemTypeId, model.PayrollItemOverridable, model.PropotionatePay, model.AccountCode, model.IncomeTaxSection, model.CreditAccountNo, model.DebitAccountNo, model.PayrollItemNotes, model.PayrollTemplateId, model.DisplayOrder, model.EmpTypeId, model.EmployeeId, model.Entity, model.IsVariablePay, model.IsTaxApplicable);
                    int[] columnHide = { 0, 2, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22 };
                    DataTable dtPayrollItem = obj.PayrollItemGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItem, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
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
        public ActionResult PayrollItemUpdate(PayrollItemViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollItemExistsValidate(model.PayrollItemDesc, model.PayrollItemId);
                if (Exist == 0)
                {
                    int? status = obj.PayrollItemUpdate(model.PayrollItemId, model.PayrollItemDesc, model.PayrollTypeId, model.PayrollItemTypeId, model.PayrollItemOverridable, model.PropotionatePay, model.AccountCode, model.IncomeTaxSection, model.CreditAccountNo, model.DebitAccountNo, model.PayrollItemNotes, model.PayrollTemplateId, model.DisplayOrder, model.EmpTypeId, model.EmployeeId, model.Entity, model.IsVariablePay, model.IsTaxApplicable);
                    int[] columnHide = { 0, 2, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22 };
                    DataTable dtPayrollItem = obj.PayrollItemGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItem, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Payroll Item Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayrollItemDelete(PayrollItemViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    RedirectToAction("Login", "Home");
                }
                int? status = obj.PayrollItemDelete(model.PayrollItemId);
                int[] columnHide = { 0, 2, 4, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22 };
                DataTable dtPayrollItem = obj.PayrollItemGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItem, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //----------------Payroll Function-----------------//
        public ActionResult PayrollFunction()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollFunctionViewModel model = new PayrollFunctionViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollFunctionGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 3 };
                DataTable dtPayrollFunction = obj.PayrollFunctionGet();
                if (dtPayrollFunction.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunction, columnHide);
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
        public ActionResult PayrollFunctionCreate(PayrollFunctionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollFunctionExistsValidate(model.PayrollFunctionName);
                if (Exist == 0)
                {
                    int? status = obj.PayrollFunctionCreate(model.PayrollFunctionName, model.PayrollFunctionDesc, model.PayrollTemplateId);
                    int[] columnHide = { 0, 3 };
                    DataTable dtPayrollFunction = obj.PayrollFunctionGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunction, columnHide);
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
        public ActionResult PayrollFunctionUpdate(PayrollFunctionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollFunctionUpdate(model.PayrollFunctionId, model.PayrollFunctionName, model.PayrollFunctionDesc, model.PayrollTemplateId);
                int[] columnHide = { 0, 3 };
                DataTable dtPayrollFunction = obj.PayrollFunctionGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunction, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayrollFunctionDelete(PayrollFunctionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollFunctionDelete(model.PayrollFunctionId);
                int[] collumnHide = { 0, 3 };
                DataTable dtPayrollFunction = obj.PayrollFunctionGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunction, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //----------------Payroll Tax Type-----------------//
        public ActionResult PayrollTaxType()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollTaxTypeViewModel model = new PayrollTaxTypeViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollTaxTypeGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 3, 4 };
                DataTable dtPayrollTaxType = obj.PayrollTaxTypeGet();
                if (dtPayrollTaxType.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTaxType, columnHide);
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
        public ActionResult PayrollTaxTypeCreate(PayrollTaxTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollTaxTypeExistsValidate(model.PayrollTaxTypeDesc);
                if (Exist == 0)
                {
                    int? status = obj.PayrollTaxTypeCreate(model.PayrollTaxTypeName, model.PayrollTaxTypeDesc, model.PayrollTemplateId, model.IsCalculationAuto);
                    int[] columnHide = { 0, 3, 4 };
                    DataTable dtPayrollTaxType = obj.PayrollTaxTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTaxType, columnHide);
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
        public ActionResult PayrollTaxTypeUpdate(PayrollTaxTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollTaxTypeUpdate(model.PayrollTaxTypeId, model.PayrollTaxTypeName, model.PayrollTaxTypeDesc, model.PayrollTemplateId, model.IsCalculationAuto);
                int[] columnHide = { 0, 3, 4 };
                DataTable dtPayrollTaxType = obj.PayrollTaxTypeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTaxType, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayrollTaxTypeDelete(PayrollTaxTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollTaxTypeDelete(model.PayrollTaxTypeId);
                int[] columnHide = { 0, 3, 4 };
                DataTable dtPayrollTaxType = obj.PayrollTaxTypeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTaxType, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //----------------Payroll Tax -----------------//
        public ActionResult PayrollTax()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollTaxViewModel model = new PayrollTaxViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollTaxGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 2, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                DataTable dtPayrollTax = obj.PayrollTaxGet();
                if (dtPayrollTax.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTax, columnHide);
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
        public ActionResult PayrollTaxCreate(PayrollTaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollTaxExistsValidate(model.PayrollTaxDescription);
                if (Exist == 0)
                {
                    int? status = obj.PayrollTaxCreate(model.PayrollTaxDescription, model.PayrollTaxTypeId, model.TaxableEntity, model.PayrollFunctionId, model.PayrollTaxOverridable, model.PayrollTaxTracking, model.PayrollTaxPayableTo, model.PayrollTaxIdentifier, model.PayrollTaxableFnId, model.CreditAccountNo, model.DebitAccountNo, model.TaxParameterXsd, model.PayrollTemplateId, model.EmpTypeId, model.EmployeeId);
                    int[] columnHide = { 0, 2, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                    DataTable dtPayrollTax = obj.PayrollTaxGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTax, columnHide);
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
        public ActionResult PayrollTaxUpdate(PayrollTaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollTaxUpdate(model.PayrollTaxId, model.PayrollTaxDescription, model.PayrollTaxTypeId, model.TaxableEntity, model.PayrollFunctionId, model.PayrollTaxOverridable, model.PayrollTaxTracking, model.PayrollTaxPayableTo, model.PayrollTaxIdentifier, model.PayrollTaxableFnId, model.CreditAccountNo, model.DebitAccountNo, model.TaxParameterXsd, model.PayrollTemplateId, model.EmpTypeId, model.EmployeeId);
                int[] columnHide = { 0, 2, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                DataTable dtPayrollTax = obj.PayrollTaxGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTax, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayrollTaxDelete(PayrollTaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollTaxDelete(model.PayrollTaxId);
                int[] columnHide = { 0, 2, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                DataTable dtPayrollTax = obj.PayrollTaxGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollTax, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        //--------------------------------------------------//

        //------------------Payroll Bonus-------------------//

        public ActionResult PayrollBonus()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollBonusViewModel model = new PayrollBonusViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollBonusGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 7 };
                DataTable dtpayrollBonusget = obj.PayrollBonusGet(null, null, null, null);
                if (dtpayrollBonusget.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtpayrollBonusget, columnHide);
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
        public ActionResult PayrollBonusCreate(PayrollBonusViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollBonusExistsValidate(null);
                if (Exist == 0)
                {
                    int? status = obj.PayrollBonusCreate(model.MaxWages, model.MinWages, model.BonusPercent, model.BonusAmount, model.StartDate, model.EndDate, model.LocationId);
                    int[] columnHide = { 0, 7 };
                    DataTable dtpayrollBonusget = obj.PayrollBonusGet(null, null, null, null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtpayrollBonusget, columnHide);
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
        public ActionResult PayrollBonusUpdate(PayrollBonusViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.PayrollBonusUpdate(model.PayrollBonusId, model.MaxWages, model.MinWages, model.BonusPercent, model.BonusAmount, model.StartDate, model.EndDate, model.LocationId);
                int[] columnHide = { 0, 7 };
                DataTable dtpayrollBonusget = obj.PayrollBonusGet(null, null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtpayrollBonusget, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollBonusDelete(PayrollBonusViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollBonusDelete(model.PayrollBonusId);
                int[] collumnHide = { 0, 7 };
                DataTable dtpayrollBonusget = obj.PayrollBonusGet(null, null, null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtpayrollBonusget, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //--------------------------------------------------//

        //------------------Payroll Gratuity-------------------//

        public ActionResult PayrollGratuity()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollGratuityViewModel model = new PayrollGratuityViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollGratuityGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 8, 9, 10 };
                DataTable dtPayrollGratuityGet = obj.PayrollGratuityGet(null, null);
                if (dtPayrollGratuityGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollGratuityGet, columnHide);
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
        public ActionResult PayrollGratuityCreate(PayrollGratuityViewModel model)
        {
            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.PayrollGratuityExistsValidate(null);
                if (Exist == 0)
                {
                    int? status = obj.PayrollGratuityCreate(model.Is_Basic, model.Is_Hra, model.Is_Da, model.WorkingDays, model.MultiplyValue, model.DivideValue, model.StartDate, model.EndDate, model.LocationId);
                    int[] columnHide = { 0, 8, 9, 10 };

                    DataTable dtPayrollGratuityGet = obj.PayrollGratuityGet(null, null);
                    if (dtPayrollGratuityGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollGratuityGet, columnHide);
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
        public ActionResult PayrollGratuityUpdate(PayrollGratuityViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.PayrollGratuityUpdate(model.PayrollGratuityId, model.Is_Basic, model.Is_Hra, model.Is_Da, model.WorkingDays, model.MultiplyValue, model.DivideValue, model.StartDate, model.EndDate, model.LocationId);
                int[] columnHide = { 0, 8, 9, 10 };


                DataTable dtPayrollGratuityGet = obj.PayrollGratuityGet(null, null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollGratuityGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollGratuityDelete(PayrollGratuityViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollGratuityDelete(model.PayrollGratuityId);
                int[] columnHide = { 0, 8, 9, 10 };

                DataTable dtPayrollGratuityGet = obj.PayrollGratuityGet(null, null);


                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollGratuityGet, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }

        //--------------------------------------------------//

        //----------------Payroll Item Tax-----------------//
        public ActionResult PayrollItemTax()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollItemTaxViewModel model = new PayrollItemTaxViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayrollItemTaxGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 1, 2, 5 };
                DataTable dtPayrollItemTax = obj.PayrollItemTaxGet();
                if (dtPayrollItemTax.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemTax, columnHide);
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
        public ActionResult PayrollItemTaxCreate(PayrollItemTaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollItemTaxExistsValidate(model.PayrollItemId);
                if (Exist == 0)
                {
                    int? status = obj.PayrollItemTaxCreate(model.PayrollItemId, model.PayrollTaxId, model.PayrollTemplateId);
                    int[] columnHide = { 0, 1, 2, 5 };
                    DataTable dtPayrollItemTax = obj.PayrollItemTaxGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemTax, columnHide);
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
        public ActionResult PayrollItemTaxUpdate(PayrollItemTaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollItemTaxUpdate(model.PayrollItemTaxId, model.PayrollItemId, model.PayrollTaxId, model.PayrollTemplateId);
                int[] columnHide = { 0, 1, 2, 5 };
                DataTable dtPayrollItemTax = obj.PayrollItemTaxGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemTax, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayrollItemTaxDelete(PayrollItemTaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollItemTaxDelete(model.PayrollItemTaxId);
                int[] columnHide = { 0, 1, 2, 5 };
                DataTable dtPayrollItemTax = obj.PayrollItemTaxGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollItemTax, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //-----------Master mapping view-------------

        public ActionResult MasterMapping()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            MasterMappingViewModel model = new MasterMappingViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MasterMappingKeyGet(MasterMappingViewModel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtMasterMappingKeyGet = obj.MasterMappingKeyGet(model.MasterTable);
            foreach (DataRow dr in dtMasterMappingKeyGet.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr["MasterKeyName"].ToString(),
                    Value = dr["MasterKey"].ToString()
                });
            }

            var list = SectionListItems;
            return Json(list);
        }

        [HttpPost]
        public ActionResult MasterMappingGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 3 };
                DataTable dtMasterMapping = obj.MasterMappingGet();
                if (dtMasterMapping.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dtMasterMapping, columnHide);
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
        public ActionResult MasterMappingUpdate(MasterMappingViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? exist = obj.MasterMappingExistsValidate(model.MasterKey);
                if (exist == 0)
                {
                    int? status = obj.MasterMappingUpdate(model.MasterMappingId, model.MasterKey);
                    int[] columnHide = { 0, 3 };
                    DataTable dtMasterMapping = obj.MasterMappingGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dtMasterMapping, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Payroll Item Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-----------Payroll Payement Method View-------------

        public ActionResult PayrollPaymentMethod()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayrollPaymentMethodViewModel model = new PayrollPaymentMethodViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult PayrollPaymentMethodGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0,2, 4, 5 };
                DataTable dtPayrollPaymentMethod = obj.PayrollPaymentMethodGet();
                if (dtPayrollPaymentMethod.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollPaymentMethod, columnHide);
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
        public ActionResult PayrollPaymentMethodCreate(PayrollPaymentMethodViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollPaymentMethodExistsValidate(model.PaymentMethodName, null);
                if (Exist == 0)
                {
                    int? status = obj.PayrollPaymentMethodCreate(model.PaymentMethodName, model.DefaultMethod, model.Notes, model.PayrollTemplateId, model.TxnPerBatch);
                    int[] columnHide = { 0, 4, 5 };
                    DataTable dtPayrollPaymentMethod = obj.PayrollPaymentMethodGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollPaymentMethod, columnHide);
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
        public ActionResult PayrollPaymentMethodUpdate(PayrollPaymentMethodViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollPaymentMethodExistsValidate(model.PaymentMethodName, model.PaymentMethodId);
                if (Exist == 0)
                {

                    int? status = obj.PayrollPaymentMethodUpdate(model.PaymentMethodId, model.PaymentMethodName, model.DefaultMethod, model.Notes, model.PayrollTemplateId, model.TxnPerBatch);
                    int[] columnHide = { 0, 4, 5 };
                    DataTable dtPayrollPaymentMethod = obj.PayrollPaymentMethodGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollPaymentMethod, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Payment Method Already Exists !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollPaymentMethodDelete(PayrollPaymentMethodViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollPaymentMethodDelete(model.PaymentMethodId);
                int[] columnHide = { 0, 4, 5 };
                DataTable dtPayrollPaymentMethod = obj.PayrollPaymentMethodGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollPaymentMethod, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-----------Payroll Function List View-------------

        public ActionResult PayrollFunctionList()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            PayrollFunctionListViewModel model = new PayrollFunctionListViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PayrollFunctionListGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 2, 4, 8 };
                DataTable dtPayrollFunctionList = obj.PayrollFunctionListGet();
                if (dtPayrollFunctionList.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dtPayrollFunctionList, columnHide);
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
        public ActionResult PayrollFunctionListCreate(PayrollFunctionListViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollFunctionListExistsValidate(model.PayrollFunctionListName);
                if (Exist == 0)
                {
                    int? status = obj.PayrollFunctionListCreate(model.PayrollFunctionListName, model.PayrollFunctionId, model.FunctionTypeId, model.StartDt, model.EndDt, model.PayrollTemplateId);
                    int[] columnHide = { 0, 2, 4, 8 };
                    DataTable dtPayrollFunctionList = obj.PayrollFunctionListGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunctionList, columnHide);
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
        public ActionResult PayrollFunctionListUpdate(PayrollFunctionListViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollFunctionListUpdate(model.PayrollFunctionListId, model.PayrollFunctionListName, model.PayrollFunctionId, model.FunctionTypeId, model.StartDt, model.EndDt, model.PayrollTemplateId);
                int[] columnHide = { 0, 2, 4, 8 };
                DataTable dtPayrollFunctionList = obj.PayrollFunctionListGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditModeHideDelete(dtPayrollFunctionList, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollFunctionListDelete(PayrollFunctionListViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollFunctionListDelete(model.PayrollFunctionListId);
                int[] columnHide = { 0, 2, 4, 8 };
                DataTable dtPayrollFunctionList = obj.PayrollFunctionListGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunctionList, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //-----------Payroll Function Parameter View-------------//

        public ActionResult PayrollFunctionParameter()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }


            PayrollFunctionParameterViewModel model = new PayrollFunctionParameterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PayrollFunctionParameterGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 1, 8, 10 };
                DataTable dtPayrollFunctionParametert = obj.PayrollFunctionParameterGet();
                if (dtPayrollFunctionParametert.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunctionParametert, columnHide);
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
        public ActionResult PayrollFunctionParameterCreate(PayrollFunctionParameterViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayrollFunctionParameterExistsValidate(model.PayrollFunctionListId, model.ParameterSeq);
                if (Exist == 0)
                {
                    int? status = obj.PayrollFunctionParameterCreate(model.PayrollFunctionListId, model.ParameterSeq, model.ParameterName, model.ParameterDatatype, model.TableName, model.Comments, model.PayrollFunctionId, model.PayrollTemplateId);
                    int[] columnHide = { 0, 1, 8, 10 };
                    DataTable dtPayrollFunctionParameter = obj.PayrollFunctionParameterGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunctionParameter, columnHide);
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
        public ActionResult PayrollFunctionParameterUpdate(PayrollFunctionParameterViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollFunctionParameterUpdate(model.PayrollFunctionParameterId, model.PayrollFunctionListId, model.ParameterSeq, model.ParameterName, model.ParameterDatatype, model.TableName, model.Comments, model.PayrollFunctionId, model.PayrollTemplateId);
                int[] columnHide = { 0, 1, 8, 10 };
                DataTable dtPayrollFunctionParameter = obj.PayrollFunctionParameterGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunctionParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayrollFunctionParameterDelete(PayrollFunctionParameterViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayrollFunctionParameterDelete(model.PayrollFunctionParameterId);
                int[] columnHide = { 0, 1, 8, 10 };
                DataTable dtPayrollFunctionParameter = obj.PayrollFunctionParameterGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayrollFunctionParameter, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        //----------------Pay Percent-----------------//
        public ActionResult PayPercent()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }


            PayPercentViewModel model = new PayPercentViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PayPercentGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 1, 2, 5, 10, 12, 14, 20, 21, 22, 23 };
                DataTable dtPayPercent = obj.PayPercentGet();
                if (dtPayPercent.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayPercent, columnHide);
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
        public ActionResult PayPercentCreate(PayPercentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayPercentExistsValidate(model.PayrollItemId, model.EmpTypeId, model.LocationId, model.StartDate, model.EndDate, null);
                if (Exist == 0)
                {
                    if (model.ComputingType == "Sum")
                    {
                        model.ComputingBy = "Payroll Item";
                    }
                    model.ItemTypeId = 1;
                    int? status = obj.PayPercentCreate(model.PayrollItemId, model.EmpTypeId, model.StartDate, model.EndDate, model.ItemPercent, model.LocationId, model.ItemTypeId, model.ComputingItemList, model.ComputingType, model.SubtractPayrollItemId, model.ComputingBy, model.SubtractBy, model.AdditionalAmount);
                    int[] columnHide = { 0, 1, 2, 5, 10, 12, 14, 20, 21, 22, 23 };
                    DataTable dtPayPercent = obj.PayPercentGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayPercent, columnHide);
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
        public ActionResult PayPercentUpdate(PayPercentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.PayPercentExistsValidate(model.PayrollItemId, model.EmpTypeId, model.LocationId, model.StartDate, model.EndDate, model.PayPercentId);
                if (Exist == 0)
                {
                    if (model.ComputingType == "Sum")
                    {
                        model.ComputingBy = "Payroll Item";
                    }
                    model.ItemTypeId = 1;
                    int? status = obj.PayPercentUpdate(model.PayPercentId, model.PayrollItemId, model.EmpTypeId, model.StartDate, model.EndDate, model.ItemPercent, model.LocationId, model.ItemTypeId, model.ComputingItemList, model.ComputingType, model.SubtractPayrollItemId, model.ComputingBy, model.SubtractBy, model.AdditionalAmount);
                    int[] columnHide = { 0, 1, 2, 5, 10, 12, 14, 20, 21, 22, 23 };
                    DataTable dtPayPercent = obj.PayPercentGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayPercent, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Percentage Already Exists for payroll item !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PayPercentDelete(PayPercentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayPercentDelete(model.PayPercentId);
                int[] columnHide = { 0, 1, 2, 5, 10, 12, 14, 20, 21, 22, 23 };
                DataTable dtPayPercent = obj.PayPercentGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayPercent, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult FillDdlPayrollGet(PayPercentViewModel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtPayrollItem = obj.PayrollItemGet();
            foreach (DataRow dr in dtPayrollItem.Rows)
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
        public ActionResult FillDdlTaxGet(PayPercentViewModel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtPayrollTax = obj.PayrollTaxGet();
            foreach (DataRow dr in dtPayrollTax.Rows)
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



        //-----HRA Range---------------//
        public ActionResult HRARange()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            HraRangeViewModel model = new HraRangeViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult HRARangeGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0,2,3, 5 };
                DataTable dtHRARange = obj.HraRangeGet();
                if (dtHRARange.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtHRARange, columnHide);
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
        public ActionResult HRARangeCreate(HraRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.HraRangeExistsValidate(model.StartPayRange, model.EndPayRange, model.LocationId);
                if (Exist == 0)
                {
                    int? status = obj.HraRangeCreate(model.StartPayRange, model.EndPayRange, model.Hra, model.LocationId);
                    int[] columnHide = { 0, 4 };
                    DataTable dtHRARange = obj.HraRangeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtHRARange, columnHide);
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
        public ActionResult HRARangeUpdate(HraRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //int? Exist = obj.HraRangeExistsValidate(model.StartPayRange, model.EndPayRange, model.LocationId);
                //if (Exist == 0)
                //{
                int? status = obj.HraRangeUpdate(model.Id, model.StartPayRange, model.EndPayRange, model.Hra, model.LocationId);
                int[] columnHide = { 0, 4 };
                DataTable dtHRARange = obj.HraRangeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtHRARange, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //  return Json(new { Flag = 1, Html = "HRA Range already exist !!" }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult HRARangeDelete(HraRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.HraRangeDelete(model.Id);
                int[] collumnHide = { 0, 4 };
                DataTable dtHRARange = obj.HraRangeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtHRARange, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        //----------------CCA Range-----------------//

        public ActionResult CCARange()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            CcaRangeViewModel model = new CcaRangeViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult CCARangeGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 2,3,5 };
                DataTable dtCcaRange = obj.CcaRangeGet();
                if (dtCcaRange.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtCcaRange, columnHide);
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
        public ActionResult CCARangeCreate(CcaRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.CcaRangeExistsValidate(model.StartPayRange, model.EndPayRange, model.LocationId);
                if (Exist == 0)
                {
                    int? status = obj.CcaRangeCreate(model.StartPayRange, model.EndPayRange, model.Cca, model.LocationId);
                    int[] columnHide = { 0, 4 };
                    DataTable dtCcaRange = obj.CcaRangeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtCcaRange, columnHide);
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
        public ActionResult CCARangeUpdate(CcaRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //int? Exist = obj.CcaRangeExistsValidate(model.StartPayRange, model.EndPayRange, model.LocationId);
                //if (Exist == 0)
                //{
                int? status = obj.CcaRangeUpdate(model.Id, model.StartPayRange, model.EndPayRange, model.Cca, model.LocationId);
                int[] columnHide = { 0, 4 };
                DataTable dtCcaRange = obj.CcaRangeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtCcaRange, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 1, Html = "CCA Range already exist !!" }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult CCARangeDelete(CcaRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.CcaRangeDelete(model.Id);
                int[] collumnHide = { 0, 4 };
                DataTable dtCcaRange = obj.CcaRangeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtCcaRange, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        //----------------Pay Scale-----------------//

        public ActionResult PayScale()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PayScale6pcViewModel model = new PayScale6pcViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PayScaleGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 2, 3 };
                DataTable dtPayScale = obj.PayScale6pcGet();
                if (dtPayScale.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayScale, columnHide);
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
        public ActionResult PayScaleCreate(PayScale6pcViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PayScale6pcExistsValidate(model.PayScale, model.DesignationId, model.StartScale, model.EndScale, model.GradePay, null);
                if (Exist == 0)
                {
                    int? status = obj.PayScale6pcCreate(model.PayScale, model.DesignationId, model.StartScale, model.EndScale, model.GradePay, model.PayBand);
                    int[] columnHide = { 0, 2, 3 };
                    DataTable dtPayScale = obj.PayScale6pcGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayScale, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Pay Scale Already Exists !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult PayScaleUpdate(PayScale6pcViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //int? Exist = obj.PayScale6pcExistsValidate(model.PayScale,model.DesignationId,model.StartScale,model.EndScale,model.GradePay,model.PayId);
                //if (Exist == 0)
                //{
                int? status = obj.PayScale6pcUpdate(model.PayId, model.PayScale, model.DesignationId, model.StartScale, model.EndScale, model.GradePay, model.PayBand);
                int[] columnHide = { 0, 2, 3 };
                DataTable dtPayScale = obj.PayScale6pcGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayScale, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 1, Html = "Pay scale Already Exist !!" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PayScaleDelete(PayScale6pcViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PayScale6pcDelete(model.PayId);
                int[] collumnHide = { 0, 2, 3 };
                DataTable dtPayScale = obj.PayScale6pcGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPayScale, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        //----------------PT Range-----------------//

        public ActionResult PTRange()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            PTRangeViewModel model = new PTRangeViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult PTRangeGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0,2,3 };
                DataTable dtPTRange = obj.PtRangeGet();
                if (dtPTRange.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPTRange, columnHide);
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
        public ActionResult PTRangeCreate(PTRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.PtRangeExistsValidate(model.StartRange, model.EndRange, model.ProfessionTax, null);
                if (Exist == 0)
                {
                    int? status = obj.PtRangeCreate(model.StartRange, model.EndRange, model.ProfessionTax);
                    int[] columnHide = { 0 };
                    DataTable dtPTRange = obj.PtRangeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPTRange, columnHide);
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
        public ActionResult PTRangeUpdate(PTRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //  int? Exist = obj.PtRangeExistsValidate(model.StartRange, model.EndRange, model.ProfessionTax,model.PTRangeId);
                //if (Exist == 0)
                //{
                int? status = obj.PtRangeUpdate(model.PTRangeId, model.StartRange, model.EndRange, model.ProfessionTax);
                int[] columnHide = { 0 };
                DataTable dtPTRange = obj.PtRangeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPTRange, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 1, Html = "PT range Already Exists !" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult PTRangeDelete(PTRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.PtRangeDelete(model.PTRangeId);
                int[] collumnHide = { 0 };
                DataTable dtPTRange = obj.PtRangeGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPTRange, collumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }









        //---------------Grade Splitup-----------------//

        public ActionResult GradeSplitup()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            GradeSplitupViewModel model = new GradeSplitupViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult GradeSplitupGet()
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 1 };
                DataTable dtGrade = obj.GradeSplitupGet(null, null);
                if (dtGrade.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtGrade, columnHide);
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

        [HttpPost]
        public ActionResult GradeSplitupCreate(GradeSplitupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.GradeSplitupExistsValidate(model.EmpCategoryId, model.GradeName, model.Individual, model.Unit, model.Organization, model.StartDate, model.EndDate, null);
                if (Exist == 0)
                {
                    int? status = obj.GradeSplitupCreate(model.EmpCategoryId, model.GradeName, model.Individual, model.Unit, model.Organization, model.StartDate, model.EndDate);
                    int[] columnHide = { 0, 1 };
                    DataTable dtGrade = obj.GradeSplitupGet(null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtGrade, columnHide);
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
        public ActionResult GradeSplitupUpdate(GradeSplitupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.GradeSplitupExistsValidate(model.EmpCategoryId, model.GradeName, model.Individual, model.Unit, model.Organization, model.StartDate, model.EndDate, model.GradeId);
                if (Exist == 0)
                {
                    int? status = obj.GradeSplitupUpdate(model.GradeId, model.EmpCategoryId, model.GradeName, model.Individual, model.Unit, model.Organization, model.StartDate, model.EndDate);
                    int[] columnHide = { 0, 1 };
                    DataTable dtGrade = obj.GradeSplitupGet(null, null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtGrade, columnHide);
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
        public ActionResult GradeSplitupDelete(GradeSplitupViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.GradeSplitupDelete(model.GradeId);
                int[] columnHide = { 0, 1 };
                DataTable dtGrade = obj.GradeSplitupGet(null, null);
                if (dtGrade.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtGrade, columnHide);
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




        //---------------Grade Performance-----------------//

        public ActionResult GradePerformance()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            GradePerformanceViewModel model = new GradePerformanceViewModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult GradePerformanceGet(GradePerformanceViewModel model)
        {
            string html = "";
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = { 0, 1 };
                DataTable dtPerformaceGet = obj.GradePerformanceGet(null);
                if (dtPerformaceGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPerformaceGet, columnHide);
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
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public ActionResult GradePerformanceCreate(GradePerformanceViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.GradePerformanceExistsValidate(model.EmpCategoryId, model.IndividualRate, model.IndividualPercentage, model.UnitRate, model.UnitPercentage, model.OrganizationRate, model.OrganizationPercentage, model.StartDate, model.EndDate, null);
                if (Exist == 0)
                {
                    int? status = obj.GradePerformanceCreate(model.EmpCategoryId, model.IndividualRate, model.IndividualPercentage, model.UnitRate, model.UnitPercentage, model.OrganizationRate, model.OrganizationPercentage, model.StartDate, model.EndDate);
                    int[] columnHide = { 0, 1 };
                    DataTable dtPerformaceGet = obj.GradePerformanceGet(null);
                    if (dtPerformaceGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPerformaceGet, columnHide);
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
        public ActionResult GradePerformanceUpdate(GradePerformanceViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.GradePerformanceExistsValidate(model.EmpCategoryId, model.IndividualRate, model.IndividualPercentage, model.UnitRate, model.UnitPercentage, model.OrganizationRate, model.OrganizationPercentage, model.StartDate, model.EndDate, model.GradePerformanceId);
                if (Exist == 0)
                {

                    int? status = obj.GradePerformanceUpdate(model.GradePerformanceId, model.EmpCategoryId, model.IndividualRate, model.IndividualPercentage, model.UnitRate, model.UnitPercentage, model.OrganizationRate, model.OrganizationPercentage, model.StartDate, model.EndDate);
                    int[] columnHide = { 0, 1 };
                    DataTable dtPerformaceGet = obj.GradePerformanceGet(null);
                    if (dtPerformaceGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPerformaceGet, columnHide);
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
        public ActionResult GradePerformanceDelete(GradePerformanceViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.GradePerformanceDelete(model.GradePerformanceId);
                int[] columnHide = { 0, 1 };
                DataTable dtPerformaceGet = obj.GradePerformanceGet(null);
                if (dtPerformaceGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtPerformaceGet, columnHide);
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





        //---------------Employee Performance-----------------//

        public ActionResult EmployeePerformance()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            EmployeePerformanceViewModel model = new EmployeePerformanceViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult EmployeePerformance(EmployeePerformanceViewModel model, string command)
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


                    // DataTable dtEmployee = obj.EmployeeLeaveDebitProcessGet(model.Year, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.ProjectId, model.EmpTypeId, model.ShiftId, model.LeaveType_Id);
                    DataTable dtEmployee = obj.EmployeePerformanceGet(model.PerformanceType, model.GradeName, model.Quarter, model.Month, model.Year, model.EmpCategoryId, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.EmpTypeId, model.ProjectId, model.ShiftId);

                    int[] HideColumn = { 0 };
                    if (dtEmployee.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableEmployeePerformance(dtEmployee, HideColumn, model.PerformanceType);
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
                                    model.EmployeeId = model.EmpDataList[x].EmployeeId;
                                    model.IndividualRate = model.EmpDataList[x].IndividualRate;
                                    model.UnitRate = model.EmpDataList[x].UnitRate;
                                    model.OrganizationRate = model.EmpDataList[x].OrganizationRate;


                                    //  status = obj.EmployeeLeaveDebitProcessUpdate(model.EmpDataList[x].EmployeeId, model.LeaveType_Id, model.Year, model.LeaveDebit);
                                    status = obj.EmployeePerformanceCreate(model.EmployeeId, model.GradeName, model.IndividualRate, model.UnitRate, model.OrganizationRate, model.PerformanceType, model.Quarter, model.Month, model.Year, null, null);


                                }
                            }

                        }


                    }



                    DataTable dtEmployee = obj.EmployeePerformanceGet(model.PerformanceType, model.GradeName, model.Quarter, model.Month, model.Year, model.EmpCategoryId, model.EmployeeId, model.DepartmentId, model.DesginationId, model.LocationId, model.EmpTypeId, model.ProjectId, model.ShiftId);
                    int[] HideColumn = { 0 };
                    if (dtEmployee.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableEmployeePerformance(dtEmployee, HideColumn, model.PerformanceType);
                        htmlTable.Append("<div class='alert alert-success'>Performnace Updated Successfully for the Selected Record(s) !!</div>");
                        return Json(htmlTable.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-success'>Performnace Updated Successfully for the Selected Record(s) !!</div>";
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





        //---------------DA Setup-----------------//

        public ActionResult DASetup()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            DaRangeViewModel model = new DaRangeViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult DaRangeGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 3, 4 };
                DataTable dtDaRangeGet = obj.DaRangeGet();
                if (dtDaRangeGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDaRangeGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Records !!</div>";
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
        public ActionResult DaRangeCreate(DaRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.DaRangeExistsValidate(model.LocationId, model.DesignationId, null);
                if (Exist == 0)
                {
                    int? status = obj.DaRangeCreate(model.LocalRate, model.NonLocalRate, model.LocationId, model.DesignationId, model.StartDate, model.EndDate);
                    int[] columnHide = new[] { 0, 3, 4 };
                    DataTable dtDaRangeGet = obj.DaRangeGet();
                    if (dtDaRangeGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDaRangeGet, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Records !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult DaRangeUpdate(DaRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.DaRangeExistsValidate(model.LocationId, model.DesignationId, model.DaRangeId);
                if (Exist == 0)
                {
                    int? status = obj.DaRangeUpdate(model.DaRangeId, model.LocalRate, model.NonLocalRate, model.LocationId, model.DesignationId, model.StartDate, model.EndDate);
                    int[] columnHide = new[] { 0, 3, 4 };
                    DataTable dtDaRangeGet = obj.DaRangeGet();
                    if (dtDaRangeGet.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDaRangeGet, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Records !!</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }


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
        public ActionResult DaRangeDelete(DaRangeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.DaRangeDelete(model.DaRangeId);
                int[] columnHide = new[] { 0, 3, 4 };
                DataTable dtDaRangeGet = obj.DaRangeGet();
                if (dtDaRangeGet.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtDaRangeGet, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Records !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
