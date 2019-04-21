using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
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
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PoiseERP.Areas.Payroll.Controllers
{
    public class AssetController : Controller
    {
        //
        // GET: /Payroll/Asset/

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();

        #region  Asset Activity Type  code from here

        public ActionResult AssetActivityType()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                AssetActivityTypeViewModel model = new AssetActivityTypeViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }





        [HttpPost]
        public ActionResult AssetActivityTypeGet(AssetActivityTypeViewModel model)
        {


            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 3, 4 };
                DataTable dt = obj.AssetActivityTypeGet();
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
        public ActionResult AssetActivityTypeCreate(AssetActivityTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.AssetActivityTypeExistsValidate(model.AssetActivityTypeDescription, null);
                if (Exist == 0)
                {
                    int? status = obj.AssetActivityTypeCreate(model.AssetActivityTypeDescription, model.AssetActivityTypeNotes, model.AssetActivityTypeFn);

                    int[] columnHide = new[] { 0, 3, 4 };
                    DataTable dt = obj.AssetActivityTypeGet();

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
        public ActionResult AssetActivityTypeupdate(AssetActivityTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.AssetActivityTypeExistsValidate(model.AssetActivityTypeDescription, model.AssetActivityTypeId);
                if (Exist == 0)
                {
                    int? status = obj.AssetActivityTypeUpdate(model.AssetActivityTypeId, model.AssetActivityTypeDescription, model.AssetActivityTypeNotes, model.AssetActivityTypeFn);

                    int[] columnHide = new[] { 0, 3, 4 };
                    DataTable dt = obj.AssetActivityTypeGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Asset Activity Type Already Exists !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult AssetActivityTypedelete(AssetActivityTypeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.AssetActivityTypeDelete(model.AssetActivityTypeId);
                int[] columnHide = new[] { 0, 3, 4 };
                DataTable dt = obj.AssetActivityTypeGet();
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



        #region  Create Asset  code from here

        public ActionResult CreateAsset()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                AssetViewModels model = new AssetViewModels();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }





        [HttpPost]
        public ActionResult CreateAssetGet(AssetViewModels model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 5, 6, 7, 8 };
                DataTable dt = obj.AssetGet();
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
        public ActionResult CreateAssetCreate(AssetViewModels model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.AssetExistsValidate(model.AssetName);
                if (Exist == 0)
                {
                    int? status = obj.AssetCreate(model.AssetName, model.AssetDescription, model.DefaultInterestPct, model.ExpensePerMonth, model.TaxableFunction, model.AddtionalCharges);

                    int[] columnHide = new[] { 0, 5, 6, 7, 8 };
                    DataTable dt = obj.AssetGet();

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
        public ActionResult CreateAssetupdate(AssetViewModels model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.AssetUpdate(model.AssetId, model.AssetName, model.AssetDescription, model.DefaultInterestPct, model.ExpensePerMonth, model.TaxableFunction, model.AddtionalCharges);


                int[] columnHide = new[] { 0, 5, 6, 7, 8 };
                DataTable dt = obj.AssetGet();
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
        public ActionResult CreateAssetdelete(AssetViewModels model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.AssetDelete(model.AssetId);
                int[] columnHide = new[] { 0, 5, 6, 7, 8 };
                DataTable dt = obj.AssetGet();
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

        #region   Create Employee Asset code from here


        public ActionResult CreateEmployeeAsset()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpAssetViewModel model = new EmpAssetViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult CreateEmployeeAssetGet(EmpAssetViewModel model)
        {


            string html = null;
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 7, 8, 11, 12 };
                DataTable dt = obj.EmpAssetGet();
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
        public ActionResult CreateEmployeeAssetCreate(EmpAssetViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpAssetExistsValidate(model.AssetId);
                if (Exist == 0)
                {
                    int? status = obj.EmpAssetCreate(model.AssetId, model.EmployeeId, model.AssetCreatedDt, model.InterestPct, model.ExpensePerMonth, model.PayrollFunctionId, model.PaymentTransactionId, model.Notes, model.AssetClosedDt);

                    int[] columnHide = new[] { 0, 1, 3, 7, 8, 11, 12 };
                    DataTable dt = obj.EmpAssetGet();

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
        public ActionResult CreateEmployeeAssetupdate(EmpAssetViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpAssetUpdate(model.EmpAssetId, model.AssetId, model.EmployeeId, model.AssetCreatedDt, model.InterestPct, model.ExpensePerMonth, model.PayrollFunctionId, model.PaymentTransactionId, model.Notes, model.AssetClosedDt);

                int[] columnHide = new[] { 0, 1, 3, 7, 8, 11, 12 };
                DataTable dt = obj.EmpAssetGet();
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
        public ActionResult CreateEmployeeAssetdelete(EmpAssetViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpAssetDelete(model.EmpAssetId);
                int[] columnHide = new[] { 0, 1, 3, 7, 8, 11, 12 };
                DataTable dt = obj.EmpAssetGet();
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

        #region  Process Asset Cycle  code from here


        public ActionResult ProcessAssetCycle()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                AssetActivityTypeViewModel model = new AssetActivityTypeViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        #endregion


        #region  Void Asset Cycle  code from here


        public ActionResult VoidAssetCycle()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                AssetActivityTypeViewModel model = new AssetActivityTypeViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }
        #endregion

        #region Code for Asset Report 


        public ActionResult AssetReportStatement()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                AssetreportViewModel model = new AssetreportViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }



        [HttpPost]
        public ActionResult AssetReportStatement(AssetreportViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = new[] { 0, 1, 3, 7, 8, 11 };
                DataTable dt = obj.AssetDetailGet(model.AssetId, model.EmployeeId, model.AssetCreatedDt);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(dt, columnHide);
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
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion

        #region  Create Asset Management code from here


        public ActionResult CreateAssetManagement(AssetManagementViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public JsonResult FillDdlModelGet(int? CategoryId)
        {

            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtModel = ObjML.ModelGet(CategoryId);
            foreach (DataRow dr in dtModel.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
            var result = SectionListItems;
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult FillDdlMakeGet(int? CategoryId)
        {
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtMake = ObjML.MakeGet(CategoryId);
            foreach (DataRow dr in dtMake.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var result = SectionListItems;
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult CreateAssetManagementGet(AssetManagementViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 5 };
                DataTable dt = ObjML.AssetManagementGet();
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
        public ActionResult AssetManagementCreate(AssetManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //int? Exist = obj.AssetExistsValidate(model.AssetName);
                //if (Exist == 0)
                //{
                //  
                int? status = ObjML.AssetManagementCreate(model.CategoryId, model.ModelId, model.MakeId, model.SerialnoEngineno, model.KpiSiNo, model.AccSiNo, model.YearOfManufacturing, model.DateOfPurchase, model.DateOfAdditionFleet, model.EquipmentNoFleetNo, model.Weight, model.Height, model.Length, model.Width, model.DateRemovalFleet, model.Reason, model.Value, model.duties, model.Taxes, model.Total, model.DepreciationAsPerCo, model.DepreciationAsPerIT);

                int[] columnHide = new[] { 0, 1, 3, 5 };
                DataTable dt = ObjML.AssetManagementGet();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult AssetManagementupdate(AssetManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.AssetManagementUpdate(model.AssetId, model.CategoryId, model.ModelId, model.MakeId, model.SerialnoEngineno, model.KpiSiNo, model.AccSiNo, model.YearOfManufacturing, model.DateOfPurchase, model.DateOfAdditionFleet, model.EquipmentNoFleetNo, model.Weight, model.Height, model.Length, model.Width, model.DateRemovalFleet, model.Reason, model.Value, model.duties, model.Taxes, model.Total, model.DepreciationAsPerCo, model.DepreciationAsPerIT);


                int[] columnHide = new[] { 0, 1, 3, 5 };
                DataTable dt = ObjML.AssetManagementGet();
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
        public ActionResult AssetManagementdelete(AssetManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.AssetManagementDelete(model.AssetId);
                int[] columnHide = new[] { 0, 1, 3, 5 };
                DataTable dt = ObjML.AssetManagementGet();
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


        #region  Create Asset Category code from here


        public ActionResult CreateCategory(AssetManagementViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult CreateCategoryGet(AssetManagementViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.CategoryGet();
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
        public ActionResult CreateCategoryCreate(AssetManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = ObjML.CategoryExistsValidate(model.CategoryName);
                if (Exist == 0)
                {

                    int? status = ObjML.AssetCategoryCreate(model.CategoryName, model.CategoryDesc);

                    int[] columnHide = new[] { 0 };
                    DataTable dt = ObjML.CategoryGet();

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
        public ActionResult CreateCategoryUpdate(AssetManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.AssetCategoryUpdate(model.CategoryId, model.CategoryName, model.CategoryDesc);

                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.CategoryGet();
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
        public ActionResult CreateCategorydelete(AssetManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.AssetCategoryDelete(model.CategoryId);
                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.CategoryGet();
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


        #region  Create Asset Model code from here


        public ActionResult CreateModel(AssetModelViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult CreateModelGet(AssetModelViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2 };
                DataTable dt = ObjML.ModelGet(null);
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
        public ActionResult CreateModelCreate(AssetModelViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = ObjML.ModelExistsValidate(model.ModelName);
                if (Exist == 0)
                {

                    int? status = ObjML.AssetModelCreate(model.CategoryId, model.ModelName, model.ModelDesc);

                    int[] columnHide = new[] { 0, 2 };
                    DataTable dt = ObjML.ModelGet(null);

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
        public ActionResult CreateModelUpdate(AssetModelViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.AssetModelUpdate(model.ModelId, model.ModelName, model.ModelDesc, model.CategoryId);

                int[] columnHide = new[] { 0, 2 };
                DataTable dt = ObjML.ModelGet(null);
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
        public ActionResult CreateModeldelete(AssetModelViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.AssetModelDelete(model.ModelId);
                int[] columnHide = new[] { 0, 2 };
                DataTable dt = ObjML.ModelGet(null);
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

        #region  Create Asset Make code from here


        public ActionResult CreateMake(AssetMakeViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult CreateMakeGet(AssetMakeViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2 };
                DataTable dt = ObjML.MakeGet(null);
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
        public ActionResult CreateMakeCreate(AssetMakeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = ObjML.MakeExistsValidate(model.MakeName);
                if (Exist == 0)
                {

                    int? status = ObjML.AssetMakeCreate(model.CategoryId, model.MakeName, model.MakeDesc);

                    int[] columnHide = new[] { 0, 2 };
                    DataTable dt = ObjML.MakeGet(null);

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
        public ActionResult CreateMakeUpdate(AssetMakeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.AssetMakeUpdate(model.MakeId, model.MakeName, model.MakeDesc, model.CategoryId);

                int[] columnHide = new[] { 0, 2 };
                DataTable dt = ObjML.MakeGet(null);
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
        public ActionResult CreateMakedelete(AssetMakeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.AssetMakeDelete(model.MakeId);
                int[] columnHide = new[] { 0, 2 };
                DataTable dt = ObjML.MakeGet(null);
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
        #region  Customer Management code from here


        public ActionResult CustomerManagement(CustomerManagementViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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

        public ActionResult CustomerManagementALLSite(int? CustomerId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                object[] _SiteList;
                object[] addressidlist;
                int[] columnHide = new[] { 0, 11, 13, 15 };
                List<int> address_id = new List<int>();
                List<CustomerManagementViewModel> emplist = new List<CustomerManagementViewModel>();
                DataTable dt = ObjML.AllCustomerSiteAddressGet(CustomerId);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        emplist.Add(new CustomerManagementViewModel()
                        {
                            Country = dt.Rows[i][1].ToString(),
                            States = dt.Rows[i][2].ToString(),
                            Districts = dt.Rows[i][3].ToString(),
                            Zone = dt.Rows[i][4].ToString(),
                            City = dt.Rows[i][7].ToString(),
                            Address1 = dt.Rows[i][6].ToString(),
                            AddressId = Convert.ToInt32(dt.Rows[i][0].ToString()),
                            CustomerId = Convert.ToInt32(dt.Rows[i][5].ToString())

                        });
                        address_id.Add(Convert.ToInt32(dt.Rows[i][0].ToString()));
                    }

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode1(dt, columnHide);
                    return Json(new { Flag = 0, _SiteList = emplist, addressidlist = address_id }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }

                //html = "<div class='alert alert-danger'>No Record !!</div>";
                //    return Json(new { Flag = 0, Html = dt }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult CustomerManagementGet(CustomerManagementViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 11, 13, 15 };
                DataTable dt = ObjML.CustomerManagementGet();
                if (dt.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode1(dt, columnHide);
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

        public ActionResult CustomerManagementCreate(CustomerManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.CustomerDetailsCreate(model.Name, model.Phone, model.Fax, model.Address, model.Email, model.PinCode, model.PayrolltaxPanno, model.Tan, model.Stc, model.PremisesCode);
                int? status1 = ObjML.CustomerAddressCreate(Convert.ToInt32(model.Country), Convert.ToInt32(model.States), Convert.ToInt32(model.District), Convert.ToString(model.Zone), model.Address1, model.City);
                // int? status = ObjML.CustomerAddressCreate(model.Name,model.Phone,model.Fax,model.Address,);

                int[] columnHide = new[] { 0, 11, 13, 15 };
                DataTable dt = ObjML.CustomerManagementGet();

                //StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = status }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CustomerManagementmultipleCreate(CustomerManagementViewModel model, string empcode)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                List<CustomerManagementViewModel> models = new JavaScriptSerializer().Deserialize<List<CustomerManagementViewModel>>(empcode);
                int? status = ObjML.CustomerDetailsCreate(models[0].Name, models[0].Phone, models[0].Fax, models[0].Address, models[0].Email, models[0].PinCode, models[0].PayrolltaxPanno, models[0].Tan, models[0].Stc, models[0].PremisesCode);
                List<CustomerManagementViewModel> model1 = new JavaScriptSerializer().Deserialize<List<CustomerManagementViewModel>>(empcode);
                for (int i = 0; i < model1.Count; i++)
                {
                    int? status1 = ObjML.CustomerAddressCreate(Convert.ToInt32(model1[i].Country), Convert.ToInt32(model1[i].States), Convert.ToInt32(model1[i].District), Convert.ToString(model1[i].Zone), model1[i].Address1, model1[i].City);
                }


                return Json(new { Flag = 0, Html = status }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]

        public ActionResult CustomerManagementdelete(int? CustomerId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.CustomerDetailsDelete(CustomerId);
                int? status1 = ObjML.CustomerAddressDelete(CustomerId);
                int[] columnHide = new[] { 0, 11, 13, 15 };
                DataTable dt = ObjML.CustomerManagementGet();
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
        public ActionResult CustomerManagementupdate(CustomerManagementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? status = ObjML.CustomerDetailsUpdate(model.CustomerId, model.Name, model.Phone, model.Fax, model.Address, model.Email, model.PinCode, model.PayrolltaxPanno, model.Tan, model.Stc, model.PremisesCode);
                //int? status1 = ObjML.cus(Convert.ToInt32(model.Country), Convert.ToInt32(model.States), Convert.ToInt32(model.City), Convert.ToString(model.Zone), model.Address1);


                int[] columnHide = new[] { 0, 11, 13, 15 };
                DataTable dt = ObjML.CustomerManagementGet();
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
        public ActionResult CustomerManagementmultipleupdate(string empcode, string tabledata)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                List<CustomerManagementViewModel> models = new JavaScriptSerializer().Deserialize<List<CustomerManagementViewModel>>(empcode);
                int? status = ObjML.CustomerDetailsUpdate(models[0].CustomerId, models[0].Name, models[0].Phone, models[0].Fax, models[0].Address, models[0].Email, models[0].PinCode, models[0].PayrolltaxPanno, models[0].Tan, models[0].Stc, models[0].PremisesCode);
                List<CustomerManagementViewModel> model1 = new JavaScriptSerializer().Deserialize<List<CustomerManagementViewModel>>(tabledata);
                int? status1 = 0;
                for (int i = 0; i < model1.Count; i++)
                {

                    status1 = ObjML.CustomerAddressUpdate(
                        Convert.ToInt32(model1[i].CustomerId),
                        Convert.ToInt32(model1[i].Country),
                        Convert.ToInt32(model1[i].States),
                        Convert.ToInt32(model1[i].District),
                        Convert.ToString(model1[i].Zone),
                        model1[i].Address1,
                        model1[i].City,
                        Convert.ToInt32(model1[i].AddressId));
                }
                int[] columnHide = new[] { 0, 11, 13, 15 };
                DataTable dt = ObjML.CustomerManagementGet();
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode1(dt, columnHide);
                    //return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    //StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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

        public ActionResult CustomerAddressGet(int? CustomerId)
        {
            DataTable ds = ObjML.CustomerAddressGet(CustomerId);
            List<SelectListItem> customeraddresslist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Rows)
            {
                customeraddresslist.Add(new SelectListItem
                {
                    Text = dr["Address"].ToString(),
                    Value = dr["AddressId"].ToString()
                });
            }
            return Json(customeraddresslist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult State_Bind(string country_name)
        {
            DataTable ds = ObjML.GetState(country_name);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Rows)
            {
                statelist.Add(new SelectListItem
                {
                    Text = dr["StateName"].ToString(),
                    Value = dr["StateId"].ToString()
                });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }



        public JsonResult District(int stateid)
        {
            DataTable city_name = ObjML.GetCity(stateid);
            List<SelectListItem> citylist = new List<SelectListItem>();
            foreach (DataRow dr in city_name.Rows)
            {
                citylist.Add(new SelectListItem
                {
                    Text = dr["DistrictName"].ToString(),
                    Value = dr["DistrictId"].ToString()
                });
            }
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region  Contract code from here


        public ActionResult CreateContract(ContractViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public JsonResult FillDdlSiteGet(int? CustomerId)
        {

            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtModel = ObjML.AllCustomerSiteGet(CustomerId);
            foreach (DataRow dr in dtModel.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[7].ToString(),
                    Value = dr[0].ToString()
                });
            }


            var result = SectionListItems;
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult ContractGet(ContractViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 2, 4, 6 };
                DataTable dt = ObjML.ContractMasterGet();
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
        public ActionResult ContractCreate(ContractViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //int? Exist = obj.AssetExistsValidate(model.AssetName);
                //if (Exist == 0)
                //{
                //  
                int? status = ObjML.ContractMasterCreate(
                    model.CustomerId, model.SiteAddressId, model.EmployeeId, model.WorkOrderNo, model.WorkOrderDate, model.StartDate, model.EndDate,
                    model.Desciption, model.Quantity, model.BillingUnit1, model.BillingUnitId1, model.BillingUnit2, model.BillingUnitId2, model.Total,
                    model.MonthValue, model.BillingAmt1, model.BillingAmtId1, model.BillingAmt2, model.BillingAmtId2, Convert.ToString(model.Overtime),
                    model.OvertimeId, Convert.ToBoolean(model.TransportationOutId), Convert.ToBoolean(model.Loading), model.ComputingItemList, model.BillingPeriodTo,
                    model.BillingPeriodFrom, model.KPITimeTo, model.KPITimeFrom, model.TransportationOutAmount, model.LoadingAmount, model.ContractStartDate,
                    model.ContractEndDate, model.MultiShift, Convert.ToBoolean(model.TransportationInId), model.TransportationInAmount);

                int[] columnHide = new[] { 0, 2, 4, 6 };
                DataTable dt = ObjML.ContractMasterGet();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult ContractUpdate(ContractViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
               
                int? status = ObjML.ContractMasterUpdate(model.ContractId, model.CustomerId, model.SiteAddressId, model.EmployeeId, model.WorkOrderNo, model.WorkOrderDate, model.StartDate,
                    model.EndDate, model.Desciption, model.Quantity, model.BillingUnit1, model.BillingUnitId1, model.BillingUnit2, model.BillingUnitId2, model.Total, model.MonthValue,
                    model.BillingAmt1, model.BillingAmtId1, model.BillingAmt2, model.BillingAmtId2, Convert.ToString(model.Overtime), model.OvertimeId, Convert.ToBoolean(model.TransportationInId),
                    Convert.ToBoolean(model.Loading), model.ComputingItemList, model.BillingPeriodTo, model.BillingPeriodFrom, model.KPITimeTo, model.KPITimeFrom, model.TransportationOutAmount,
                    model.LoadingAmount, model.ContractStartDate,
                    model.ContractEndDate, model.MultiShift, Convert.ToBoolean(model.TransportationInId), model.TransportationInAmount);


                int[] columnHide = new[] { 0, 2, 4, 6 };
                DataTable dt = ObjML.ContractMasterGet();
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
        public ActionResult ContractDelete(ContractViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.ContractMasterDelete(model.ContractId);
                int[] columnHide = new[] { 0, 2, 4, 6 };
                DataTable dt = ObjML.ContractMasterGet();
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
        public ActionResult overtime_sum()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable overtime_sume = ObjML.OvertimeSum();
                int? _value = !DBNull.Value.Equals(Convert.ToInt32(overtime_sume.Rows[0]["Sum"])) ? Convert.ToInt32(overtime_sume.Rows[0]["Sum"]) : 0;
                return Json(new { Flag = 0, Html = _value }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region coustomer filter
        public ActionResult CustomerSitelist()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                CustomerManagementViewModel model = new CustomerManagementViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult customerListWithDetailGet(CustomerManagementViewModel model)
        {
            PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
          //  PoisePayrollManLiftServiceModel ObjML = new PoisePayrollManLiftServiceModel();
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                int[] columnHide = { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

                DataTable dtcustomerListDetail = ObjML.CustomerSiteGet(model.CustomerId, model.stateId, model.districtId);
                if (dtcustomerListDetail.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlNestedTableChildPanel(columnHide, dtcustomerListDetail);
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
        public ActionResult AllCustomersiteGet(int? CustomerId)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 20 };
                DataTable dt = ObjML.AllCustomerSiteGet(CustomerId);
                if (dt.Rows.Count > 0)
                {

                    StringBuilder htmlTable = CommonUtil.htmlTableDisplayMode(dt, columnHide);
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


        #region  Create Tax code from here


        public ActionResult CreateTax(TaxViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult TaxGet(TaxViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.GetTax();
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
        public ActionResult TaxCreate(TaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //  int? Exist = ObjML.TaxExistsValidate(model.CategoryName);
                //if (Exist == 0)
                //{

                int? status = ObjML.TaxCreate(model.TaxDesc, model.taxpercent);

                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.GetTax();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult TaxUpdate(TaxViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.TaxUpdate(model.TaxId, model.TaxDesc, model.taxpercent);

                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.GetTax();
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
        public ActionResult TaxDelete(int roleId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.TaxDelete(roleId);
                int[] columnHide = new[] { 0 };
                DataTable dt = ObjML.GetTax();
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





        #region  Create Overtime code from here


        public ActionResult OvertimeSheet(OvertimeViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult OvertimeGet(OvertimeViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 5, 8 };
                DataTable dt = ObjML.GetOvertime();
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
        public ActionResult OverTimeCreate(OvertimeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //int? Exist = ObjML.TaxExistsValidate(model.CategoryName);
                //if (Exist == 0)
                //{

                int? status = ObjML.OTCreate(model.CategoryId, model.ModelId, model.MakeId, model.Overtime, model.EmployeeId, model.OtrateStartDate, model.OtrateEndDate, model.AssetId);

                int[] columnHide = new[] { 0, 1, 3, 5, 8 };
                DataTable dt = ObjML.GetOvertime();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult OverTimeUpdate(OvertimeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.OTUpdate(model.OTId, model.CategoryId, model.ModelId, model.MakeId, model.Overtime, model.EmployeeId, model.OtrateStartDate, model.OtrateEndDate, model.AssetId);

                int[] columnHide = new[] { 0, 1, 3, 5, 8 };
                DataTable dt = ObjML.GetOvertime();
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
        public ActionResult OverTimeDelete(OvertimeViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.OTDelete(model.OTId);
                int[] columnHide = new[] { 0, 1, 3, 5, 8 };
                DataTable dt = ObjML.GetOvertime();
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
        [HttpPost]
        public ActionResult EmployeeAttachmentGet(string data)
        {
            try
            {
                List<getdataClass> modelsEmployment = new JavaScriptSerializer().Deserialize<List<getdataClass>>(data);

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                byte[] byteImage = null;
                if (modelsEmployment[0].Check == 1)
                {
                    DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(modelsEmployment[0].EmployeeId, null, modelsEmployment[0].MonthId, modelsEmployment[0].Year);
                    var rowColl = dtAttendance.AsEnumerable();
                    List<int?> lst = new List<int?>(new int?[] { modelsEmployment[0].Id });
                    var dataRow = (from product in dtAttendance.AsEnumerable()
                                   where product.Field<int>("Id") == modelsEmployment[0].Id
                                   select product).First();
                    byteImage = (byte[])dataRow[26];
                }
                else {
                    DataTable dtAttendance = ObjML.TimeSheetDailyEntryGet(modelsEmployment[0].EmployeeId, modelsEmployment[0].CustomerId, modelsEmployment[0].SiteAddressId, modelsEmployment[0].MakeId, modelsEmployment[0].ModelId, modelsEmployment[0].AttendanceDate);
                    var rowColl = dtAttendance.AsEnumerable();
                    List<int?> lst = new List<int?>(new int?[] { modelsEmployment[0].Id });
                    var dataRow = (from product in dtAttendance.AsEnumerable()
                                   where product.Field<int>("Id") == modelsEmployment[0].Id
                                   select product).First();
                    byteImage = (byte[])dataRow[26];
                }
                return Json(byteImage == null ? null : Convert.ToBase64String(byteImage), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(1);
            }
        }

        #region  Create Time Sheet code from here
        [HttpPost]
        public ActionResult TimeSheetPivotView(TimeSheetEntryViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 3 };

                SqlConnection con = null;
                string conStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString();
                con = new SqlConnection(conStr);

                SqlCommand command = new SqlCommand("MLAsset.get_pivot_aatendance", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                StringBuilder htmlTable = CommonUtil.htmlTableEditModePIVOT(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public ActionResult TimeSheetCalculateTime(TimeSheetEntryViewModel model,string CTIME)
        {
            List<TimeSheetEntryViewModel> modelsEmployment = new JavaScriptSerializer().Deserialize<List<TimeSheetEntryViewModel>>(CTIME);


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] HideColumns = new[] { 0, 1 };

                DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(modelsEmployment[0].EmployeeId, null, modelsEmployment[0].MonthId, modelsEmployment[0].Year);
                  char[] splitChars = new[] { ':' };
                char[] bdhrmint = new[] { ':' };
                // DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(model.EmployeeId, null,model.MonthId, model.Year);
                // List<string> lst = new List<string>(new string[] { "Not Assigned" });

                string name;
                int hr=0,bdhr=0;
                double min=0;
                double bdmin = 0;
                object sumObject;
                decimal OTRS;
                decimal Total;
               
                sumObject = dtAttendance.Compute("Sum(EmployeeOverTimeAmt)", "");
                //OTRS= dtAttendance.Compute("Sum(MachineOT)", "");
                if (dtAttendance.Rows.Count > 0)
                {
                    name=dtAttendance.Rows[0][3].ToString();
                    for (int i = 0; i < dtAttendance.Rows.Count; i++)
                    {
                OTRS = dtAttendance.Rows[i][27].ToString()=="0.00"? Convert.ToDecimal("0.00"):Convert.ToDecimal(dtAttendance.Rows[i][27]);
                 var splitString = dtAttendance.Rows[i][11].ToString().Split(splitChars);
                  hr = hr+Convert.ToInt32(splitString[0]);
                  min = min + Convert.ToDouble(splitString[1]);
                  var bdhrmin = dtAttendance.Rows[i][17].ToString().Split(bdhrmint);
                  bdhr = bdhr + Convert.ToInt32(bdhrmin[0]);
                  bdmin = bdmin + Convert.ToDouble(bdhrmin[1]);

                    }
                    Total = Convert.ToDecimal(Convert.ToDecimal(sumObject));

                    string TWH = hr + (TimeSpan.FromMinutes(min)).Hours  + ":" + (TimeSpan.FromMinutes(min)).Minutes;
                    string TBD = bdhr + (TimeSpan.FromMinutes(bdmin)).Hours + ":" + (TimeSpan.FromMinutes(bdmin)).Minutes;

                    hr = 0;min = 0; bdhr=0; bdmin = 0;
                    return Json(new { Flag = 0,Empname= name ,OTHRS= sumObject.ToString(),OTINRS= Total.ToString(),TWH= TWH , TBD = TBD }, JsonRequestBehavior.AllowGet);
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
        public ActionResult TimeSheetunAssginedList(string UNAssignedist,bool value)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<TimeSheetEntryViewModel> modelsEmployment = new JavaScriptSerializer().Deserialize<List<TimeSheetEntryViewModel>>(UNAssignedist);
             string html = null;
            try
            {
                if (value == true)
                {
                    
                    int[] HideColumns = new[] { 0, 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };

                    DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(modelsEmployment[0].EmployeeId, null, modelsEmployment[0].MonthId, modelsEmployment[0].Year);
                    List<string> lst = new List<string>(new string[] { "Not Assigned" });
                    var item = from a in dtAttendance.AsEnumerable()
                               from b in lst
                               where a.Field<string>("Fleet No").ToUpper().Contains(b.ToUpper())
                               select a;
                    if (item.Count() > 0)
                    {
                        DataTable dt = item.Count() > 0 ? item.CopyToDataTable() : null;

                        StringBuilder htmlTable = CommonUtil.htmlTableEditModeunAssigned(dt, HideColumns);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    DataTable dtAttendance = ObjML.TimeSheetDailyEntryGet(modelsEmployment[0].EmployeeId, modelsEmployment[0].CustomerId, modelsEmployment[0].SiteAddressId, modelsEmployment[0].MakeId, modelsEmployment[0].ModelId, modelsEmployment[0].AttendanceDate);
                    int[] HideColumns = new[] { 0, 1, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };

                    //DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(modelsEmployment[0].EmployeeId, null, modelsEmployment[0].MonthId, modelsEmployment[0].Year);
                    List<string> lst = new List<string>(new string[] { "Not Assigned" });
                    var item = from a in dtAttendance.AsEnumerable()
                               from b in lst
                               where a.Field<string>("Fleet No").ToUpper().Contains(b.ToUpper())
                               select a;
                    if (item.Count() > 0)
                    {
                        DataTable dt = item.Count() > 0 ? item.CopyToDataTable() : null;

                        StringBuilder htmlTable = CommonUtil.htmlTableEditModeunAssigned(dt, HideColumns);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }

                }


            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TimeSheet()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                TimeSheetEntryViewModel model = new TimeSheetEntryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult TimeSheet(TimeSheetEntryViewModel model, string command, string[] commandName)
        {

            string html = null;
            // string employeeCode = null;
            try
            {
                int? status = null;
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                //DataSet ds = ObjML.GetPivotAatendance();
                //DataSet dsEmpSalary = ObjML.GetPivotAatendance();
                int[] Column = new[] { 7, 8 };
                SqlConnection con = null;
                string conStr = WebConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString();
                con = new SqlConnection(conStr);

                SqlCommand cmd = new SqlCommand("MLAsset.get_pivot_aatendance", con);
                
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
              //  cmd.Parameters.Add("@EmployeeId", SqlDbType.NVarChar, 20).Value = null;
                con.Open();
                DataTable dtt = new DataTable();
                dtt.Load(cmd.ExecuteReader());

                if (dtt.Rows.Count > 0)
                {
                    GridView gv = GridViewGet(dtt, "Employee Time Sheet report");

                    ActionResult a = null;
                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "TimeSheetExcel");
                        return a;
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "TimeSheetExcel", 12);
                        return a;
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "TimeSheetExcel");
                        return a;
                    }
                }

                if (command == "Search")
                {
                    Session["Search"] = "Search";
                    int[] HideColumn = new[] { 0, 1 };
                    DataTable dtAttendance = ObjML.TimeSheetDailyEntryGet(model.EmployeeId, model.CustomerId, model.SiteAddressId, model.MakeId, model.ModelId, model.AttendanceDate);
                    if (dtAttendance.Rows.Count > 0)
                    {

                        StringBuilder htmlTable = CommonUtil.htmlTableTSDailyAttendanceEntry(dtAttendance, HideColumn);

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

                    int[] HideColumn = new[] { 0, 1, 2, 3 };
                    int[] HideColumns = new[] { 0, 1};
                    DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(model.EmployeeId, model.EmpTypeId, model.MonthId, model.Year);
                    //List<string> lst = new List<string>(new string[] { "Not Assigned"});
                    //var item =from a in dtAttendance.AsEnumerable()
                    //          from b in lst
                    //          where a.Field<string>("Fleet No").ToUpper().Contains(b.ToUpper())
                    //          select a;
                    //return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    //var item2 =
                    //    from a in dtAttendance.AsEnumerable()
                    //    where lst.Any(x => a.Field<string>("Fleet No").ToUpper().Contains(x.ToUpper()))
                    //    select a;
                    if (dtAttendance.Rows.Count > 0 )
                    {
                       // DataTable UnAssignedlist = item.Count() > 0 ? item.CopyToDataTable() : null;

                        StringBuilder htmlTable = CommonUtil.htmlTableTimeSheetBulkEntry(dtAttendance, HideColumn);
                       // StringBuilder htmlTables = CommonUtil.htmlTableEditModePIVOT(UnAssignedlist, HideColumns);

                        return Json(htmlTable.ToString(),JsonRequestBehavior.AllowGet);
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
                                    if (commandName == null)
                                    {
                                        var statusName = "command[" + x + "]";
                                        string StatusValue = Request[statusName];
                                        model.AttendanceStatus = Convert.ToString(StatusValue).Trim();
                                    }
                                    else
                                    {
                                        model.AttendanceStatus = commandName[x].Trim();

                                    }
                                    //if ((model.EmployeeDataList[x].WorkInTime != null))
                                    //{

                                    if (Convert.ToString(Session["Search"]) == "BulkSearch")
                                    {
                                        int? sts = ObjML.TimeSheetBulkAttendanceUpdate(model.EmployeeDataList[x].BMUserId, model.EmployeeDataList[x].AttendanceDate, model.EmployeeDataList[x].WorkInTime, model.EmployeeDataList[x].WorkOutTime, model.EmployeeDataList[x].LunchBreak, model.EmployeeDataList[x].BreakInTime, model.EmployeeDataList[x].BreakOutTime, model.EmployeeDataList[x].MeterReading, model.AttendanceStatus);

                                    }
                                    else
                                    {

                                        int? sts = ObjML.TimeSheetBulkAttendanceUpdate(model.EmployeeDataList[x].BMUserId, model.AttendanceDate, model.EmployeeDataList[x].WorkInTime, model.EmployeeDataList[x].WorkOutTime, model.EmployeeDataList[x].LunchBreak, model.EmployeeDataList[x].BreakInTime, model.EmployeeDataList[x].BreakOutTime, model.EmployeeDataList[x].MeterReading, model.AttendanceStatus);
                                    }
                                    //}
                                    //else
                                    //{
                                    //    employeeCode = employeeCode + ',' + x + 1;
                                    //}
                                }
                            }
                        }



                    }

                    if (Session["Search"] != null)
                    {


                        if (Convert.ToString(Session["Search"]) == "Search")
                        {


                            int[] HideColumn = new[] { 0, 1 };
                            DataTable dtAttendanceTable = ObjML.TimeSheetDailyEntryGet(model.EmployeeId, model.CustomerId, model.SiteAddressId, model.MakeId, model.ModelId, model.AttendanceDate);


                            if (dtAttendanceTable.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableTSDailyAttendanceEntry(dtAttendanceTable, HideColumn);

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
                            int[] HideColumn = new[] { 0, 1, 2, 3 };
                            DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(model.EmployeeId, model.EmpTypeId, model.MonthId, model.Year);
                            if (dtAttendance.Rows.Count > 0)
                            {

                                StringBuilder htmlTable = CommonUtil.htmlTableTimeSheetBulkEntry(dtAttendance, HideColumn);
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
                                        int? sts = ObjML.TimeSheetAttendanceDelete(model.EmployeeDataList[x].BMUserId, model.EmployeeDataList[x].AttendanceDate);
                                    }
                                    else
                                    {
                                        int? sts = ObjML.TimeSheetAttendanceDelete(model.EmployeeDataList[x].BMUserId, model.AttendanceDate);
                                    }
                                }
                            }
                        }



                    }







                    if (Session["Search"] != null)
                    {


                        if (Convert.ToString(Session["Search"]) == "Search")
                        {



                            int[] HideColumn = new[] { 0, 1 };
                            DataTable dtAttendanceTable = ObjML.TimeSheetDailyEntryGet(model.EmployeeId, model.CustomerId, model.SiteAddressId, model.MakeId, model.ModelId, model.AttendanceDate);


                            if (dtAttendanceTable.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableTSDailyAttendanceEntry(dtAttendanceTable, HideColumn);

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

                            int[] HideColumn = new[] { 0, 1, 2, 3 };
                            DataTable dtAttendance = ObjML.TimeSheetBulkEntryGet(model.EmployeeId, model.EmpTypeId, model.MonthId, model.Year);
                            if (dtAttendance.Rows.Count > 0)
                            {

                                StringBuilder htmlTable = CommonUtil.htmlTableTimeSheetBulkEntry(dtAttendance, HideColumn);
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
        #endregion

        #region  Create fleet code from here
        public ActionResult EmployeeFleet()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                OperatorFleetAssignment model = new OperatorFleetAssignment();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();

                return View();
            }
        }

        [HttpPost]
        public ActionResult FillDllFleetGet()
        {

            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtasset = obj.GetAsset();
            foreach (DataRow dr in dtasset.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = SectionListItems;
            return Json(new { response1 = list }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public ActionResult FillDllContractGet()
        {

            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtasset = obj.GetContractByID(null);
            foreach (DataRow dr in dtasset.Rows)
            {
                SectionListItems.Add(new SelectListItem
                {
                    Text = dr[7].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = SectionListItems;
            return Json(new { response1 = list }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult EmployeeFleet(OperatorFleetAssignment model, string command, string[] commandName)
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
        public ActionResult EmployeeFleetGet(OperatorFleetAssignment model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 5 };
                DataTable dt = obj.AssetMachineGet();
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
        public ActionResult FillListByContractId(int? ContractId, OperatorFleetAssignment model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                DataTable dt = obj.GetContractByID(ContractId);
                List<OperatorFleetAssignment> emplist = new List<OperatorFleetAssignment>();
                if (!DBNull.Value.Equals(dt.Rows[0][7]))
                    Session["hfContractStartDate"] = Convert.ToDateTime(dt.Rows[0][7]);
                if (!DBNull.Value.Equals(dt.Rows[0][8]))
                    Session["hfContractEndDate"] = Convert.ToDateTime(dt.Rows[0][8]);
                if (!DBNull.Value.Equals(dt.Rows[0][7]))
                    ViewData["ABC"] = Convert.ToDateTime(dt.Rows[0][7]);
                if (!DBNull.Value.Equals(dt.Rows[0][7]))
                    model.hfContractStartDate = Convert.ToString(dt.Rows[0][7]);
                if (!DBNull.Value.Equals(dt.Rows[0][8]))
                    model.hfContractEndDate = Convert.ToString(dt.Rows[0][8]);

                if (dt.Rows.Count > 0)
                {
                    emplist.Add(new OperatorFleetAssignment()
                    {
                        CustomerName = dt.Rows[0][2].ToString(),
                        AddressName = dt.Rows[0][4].ToString(),
                        ContractEmployeeName = dt.Rows[0][6].ToString(),
                        CustomerId = Convert.ToInt32(dt.Rows[0][1].ToString()),
                        SiteAddressId = Convert.ToInt32(dt.Rows[0][3].ToString()),
                        ContractEmployeeId = Convert.ToInt32(dt.Rows[0][5].ToString()),
                        //hfContractStartDate = Convert.ToString(dt.Rows[0][7]),
                        //hfContractEndDate = Convert.ToString(dt.Rows[0][8])
                        hfContractStartDate = (DBNull.Value.Equals(dt.Rows[0][7])) ? null : Convert.ToString(dt.Rows[0][7]),
                        hfContractEndDate = (DBNull.Value.Equals(dt.Rows[0][8])) ? null : Convert.ToString(dt.Rows[0][8]),
                        isMulti = Convert.ToBoolean(dt.Rows[0][9])
                        // ContractStartDate = !DBNull.Value.Equals(dt.Rows[0][7]) ? null : Convert.ToString(dt.Rows[0][7]),
                        // ContractEndDate = !DBNull.Value.Equals(dt.Rows[0][8]) ? null : Convert.ToString(dt.Rows[0][8]),
                    });
                }
                int Flag = 0;
                return Json(new { Flag = 0, html = emplist }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult MachineDelete(int roleId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.AssetMachineDelete(roleId);
                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetMachineGet();
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
        public ActionResult MachineCreate(OperatorFleetAssignment model)
        {
            string html = null;
            try

            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //  int? Exist = ObjML.TaxExistsValidate(model.CategoryName);
                //if (Exist == 0)
                //{

                int? status = ObjML.AssetMachineCreate(model.AssetId, model.ContractId, model.FleetNo, model.CategoryId, model.MakeId, model.ModelId, model.CustomerId, model.EmployeeId, model.SiteAddressId,
                    model.StartDate, model.EndDate, model.ContractEmployeeId, model.ContractStartDate, model.ContractEndDate, model.TrasportationOutWardDispatchStartDate, model.TrasportationCheckNoOutWard,
                    model.TrasportationOutWardReciptdate, model.TrasportationNameOutWard, model.TrasportationInWardDispatchStartDate, model.TrasportationChecknoInWard, model.TrasportationInWardReciptdate,
                    model.TrasportationNameInWard, model.MachineEntryDate, model.AmountOutWard, model.AmountInWard, model.opeartorStartDate, model.opeartorEndDate, model.RoomRentPaid, model.ArrearRoomRentPaid,
                    model.Travels, model.Food, model.MachineIncentives, model.TotalExpenses, model.ComputingItemList1, model.ComputingItemList);

                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetMachineGet();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}()
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillDdlTaxesGet(PayPercentViewModel model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtPayrollItem = obj.GetTax();
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
        public ActionResult MachineUpdate(OperatorFleetAssignment model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.AssetMachineUpdate(model.MachineId, model.AssetId, model.ContractId, model.FleetNo, model.CategoryId, model.MakeId, model.ModelId, model.CustomerId, model.EmployeeId, model.SiteAddressId,
                      model.StartDate, model.EndDate, model.ContractEmployeeId, model.ContractStartDate, model.ContractEndDate, model.TrasportationOutWardDispatchStartDate, model.TrasportationCheckNoOutWard,
                      model.TrasportationOutWardReciptdate, model.TrasportationNameOutWard, model.TrasportationInWardDispatchStartDate, model.TrasportationChecknoInWard, model.TrasportationInWardReciptdate, model.TrasportationNameInWard);

                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetMachineGet();
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
        #region  Create Trasporter code from here


        public ActionResult Trasporter(TrasporterViewModel model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult TrasportorGet(TrasporterViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetTransporterGet();
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
        public ActionResult TrasportorCreate(TrasporterViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                //  int? Exist = ObjML.TaxExistsValidate(model.CategoryName);
                //if (Exist == 0)
                //{

                int? status = obj.AssetTransporterCreate(model.TrasporterName, model.TrasporterDescription);

                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetTransporterGet();

                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}()
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult TrasportorUpdate(TrasporterViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.AssetTransporterUpdate(model.TrasporterId, model.TrasporterName, model.TrasporterDescription);

                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetTransporterGet();
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
        public ActionResult TrasportorDelete(int roleId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.AssetTransporterDelete(roleId);
                int[] columnHide = new[] { 0 };
                DataTable dt = obj.AssetTransporterGet();
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

        public ActionResult AssetFleetPartial()
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            int? cid = null;

            OperatorFleetAssignment model = new OperatorFleetAssignment();
            return PartialView("AssetFleetPartial", model);

        }
        public ActionResult AssetCenter()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                AssetParialViewModel model = new AssetParialViewModel();
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult AssetCenterEmployeeListGet()
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

        public ActionResult AssetCustomerPartiall()
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            CustomerManagementViewModel model = new CustomerManagementViewModel();

            return PartialView("AssetCustomerPartiall", model);
        }
        public ActionResult AssetmanagementPartiall()
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            AssetManagementViewModel model = new AssetManagementViewModel();
            return PartialView("AssetManagement", model);
        }
        [HttpPost]
        public JsonResult AutoComplete(string query)
        {
            List<Autocomplete> list = new List<Autocomplete>();
            //Note : you can bind same list from database  
            DataTable dt = ObjML.CustomerManagementGet();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Autocomplete userinfo = new Autocomplete();
                userinfo.Name = dt.Rows[i]["CSAddress"].ToString();
                list.Add(userinfo);
            }
            //Searching records from list using LINQ query  
            var DistrictName = (from N in list
                                where N.Name.StartsWith(query)
                                select new { N.Name });
            return Json(DistrictName, JsonRequestBehavior.AllowGet);
            //List<Autocomplete> list = new List<Autocomplete>();

            //List<Autocomplete> listData = null;
            ////checking the query parameter sent from view. If it is null we will return null else we will return list based on query.
            //if (!string.IsNullOrEmpty(query))
            //{
            //    //Created an array of players. We can fetch this from database as well.
            //  //  string[] arrayData = new string[] { "Fabregas", "Messi", "Ronaldo", "Ronaldinho", "Goetze", "Cazorla", "Henry", "Luiz", "Reus", "Neur", "Podolski" };
            //    DataTable dt = ObjML.CustomerManagementGet();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        Autocomplete userinfo = new Autocomplete();
            //        userinfo.Name = dt.Rows[i]["CSAddress"].ToString();
            //        list.Add(userinfo);
            //    }
            //    //Using Linq to query the result from an array matching letter entered in textbox.
            //    listData = list.Where(q => q.ToLower().Contains(query.ToLower())).ToList();
            //}

            ////Returning the matched list as json data.
            //return Json(new { Data = listData });
        }


        #region  Create EmployeemachineKnown code from here


        public ActionResult EmployeemachineKnown(EmployeemachineKnown model)
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
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
        public ActionResult EMKGet(EmployeemachineKnown model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dt = ObjML.EmployeMachineKnownGet();

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
        public ActionResult EMKCreate(EmployeemachineKnown model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                DataTable Validate = ObjML.EmployeMachineKnownGet();
                var results = from myRow in Validate.AsEnumerable()
                              where myRow.Field<int>("employee_id") == model.EmployeeId
                              select myRow;
                if (results.Count() == 0)
                {
                    int? status = ObjML.EmployeMachineKnownCREATE(model.EmployeeId, model.ComputingItemList, model.ComputingItemNameList);
                    int[] columnHide = new[] { 0, 1, 4 };
                    DataTable dt = ObjML.EmployeMachineKnownGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                //DataTable _ExistValidate = results.CopyToDataTable();
                //if (_ExistValidate.Rows.Count == 0)
                //{
                //    int? status = ObjML.EmployeMachineKnownCREATE(model.EmployeeId, model.ComputingItemList, model.ComputingItemNameList);
                //    int[] columnHide = new[] { 0, 1, 4 };
                //    DataTable dt = ObjML.EmployeMachineKnownGet();
                //    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                //    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                //}
                else { return Json(new { Flag = 2, Html = "Machine Already Assigned" }, JsonRequestBehavior.AllowGet); }
                // bool exists = ObjML.EmployeMachineKnownGet().Any(t => t.EntityID == list.EntityID);
                //  int? Exist = ObjML.TaxExistsValidate(model.CategoryName);
                //if (Exist == 0)
                //{
                
                //}
                //else
                //{
                //    return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EMKupdate(EmployeemachineKnown model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = ObjML.EmployeMachineKnownUpdate(model.EMKId, model.EmployeeId, model.ComputingItemList, model.ComputingItemNameList);
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dt = ObjML.EmployeMachineKnownGet();
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
        public ActionResult EMKDelete(int roleId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = ObjML.EmployeMachineKnownDelete(roleId);
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dt = ObjML.EmployeMachineKnownGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillDdlmachines(EmployeemachineKnown model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtasset = obj.GetAsset();
            foreach (DataRow dr in dtasset.Rows)
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
        public ActionResult FillDdlEmployees(EmployeemachineKnown model)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            List<SelectListItem> SectionListItems = new List<SelectListItem>();
            DataTable dtasset = ObjML.OperatorGet();
            foreach (DataRow dr in dtasset.Rows)
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
        public ActionResult machineKnownMultpleCreate(EmployeemachineKnown model, string empcode)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                // List<CustomerManagementViewModel> models = new JavaScriptSerializer().Deserialize<List<CustomerManagementViewModel>>(empcode);
                //int? status = ObjML.CustomerDetailsCreate(models[0].Name, models[0].Phone, models[0].Fax, models[0].Address, models[0].Email, models[0].PinCode, models[0].PayrolltaxPanno, models[0].Tan, models[0].Stc, models[0].PremisesCode);
                List<CustomerManagementViewModel> model1 = new JavaScriptSerializer().Deserialize<List<CustomerManagementViewModel>>(empcode);
                //for (int i = 0; i < model1.Count; i++)
                //{
                //    int? status1 = ObjML.EmployeMachineKnownCREATE(Convert.ToInt32(model1[i].Country), Convert.ToInt32(model1[i].States), Convert.ToInt32(model1[i].District), Convert.ToString(model1[i].Zone), model1[i].Address1, model1[i].City);
                //}


                //return Json(new { Flag = 0, Html = status }, JsonRequestBehavior.AllowGet);
                //  return Json(new { Flag = 0, Html = 0 }, JsonRequestBehavior.AllowGet);


                for (int i = 0; i < model1.Count; i++)
                {
                    int? status = ObjML.EmployeMachineKnownCREATE(Convert.ToInt32(model1[i].Country), model.ComputingItemList, model.ComputingItemNameList);
                }
                int[] columnHide = new[] { 0, 1, 4 };
                DataTable dt = ObjML.EmployeMachineKnownGet();
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Opeartor_Bind(string AssetId)
        {
            List<string> lst = new List<string>(new string[] { AssetId });

            DataTable dt = ObjML.EmployeMachineKnownGet();

            var itemList =
            from a in dt.AsEnumerable()
            from b in lst
            where a.Field<string>("Machines").ToUpper().Contains(b.ToUpper())
            select a;

            // var item2 =
            //from a in dt.AsEnumerable()
            //where lst.Any(x => a.Field<string>("Machines").ToUpper().Contains(x.ToUpper()))
            //select a;
            DataTable dtOprator = ObjML.OperatorGet();
            List<SelectListItem> OperatorList = new List<SelectListItem>();


            if (itemList.Count() > 0)
            {
                DataTable dtOriginal = itemList.CopyToDataTable();
                for (int i = 0; i < dtOriginal.Rows.Count; i++)
                {

                    foreach (DataRow dr in dtOprator.Rows)
                    {
                        if (dtOriginal.Rows[i][1].ToString() == dr[0].ToString())
                        {
                            OperatorList.Add(new SelectListItem
                            {
                                Text = dr[1].ToString(),
                                Value = dr[0].ToString()
                            });
                        }

                    }
                }
                return Json(OperatorList, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(OperatorList, JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion

    }
}
