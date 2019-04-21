using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Text;
using System.Data;
namespace MvcApplication1.Controllers
{
    public class PropertyController : Controller
    {
        //
        // GET: /Property/
        BusinessLayer obj = new BusinessLayer();

        public ActionResult PropertyDetail()
        {
            PropertyDetailModel model = new PropertyDetailModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult PropertyDetailInsert(PropertyDetailModel model, HttpPostedFileBase UploadFile)
        {
            string html = null;
            try
            {
                var filname = Request.Files["ProperyImageUpload"];
                int a = obj.FunExecuteNonQuery("exec dbo.PropertyDetail_Create '" + model.UserName + "','" 
                    + model.PropertyType + "','" + model.RentBuySale
                    + "','" + model.BedroomSqFeetEtc + "','" + model.Price + "','" + model.Area + "','"
                    + model.SubArea + "','" + model.LandMark + "','" + model.PropertyDate + "','"
                    + model.PropertyHeading + "','" + model.PropertySummary + "','"
                    + model.Name + "','" + model.Contact + "','" + model.EmailId + "','"
                    + model.strbase64Image + "','" + model.ImageName + "','" + model.ImageType + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditModeWithImage(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>'"+Resources.Resource1.norecord+"'</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    html = Resources.Resource1.insertfailed;
                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = "Insert Failed !!!";
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PropertyDetailUpdate(PropertyDetailModel model, HttpPostedFileBase UploadFile)
        {
            string html = null;
            try
            {
                string filname = Request[UploadFile.FileName];
                int a = obj.FunExecuteNonQuery("exec dbo.PropertyDetail_Update '" + model.PropertyId + "','" + 
                    model.UserName + "','" + model.PropertyType + "','" + model.RentBuySale
                    + "','" + model.BedroomSqFeetEtc + "','" + model.Price + "','" + model.Area + "','"
                    + model.SubArea + "','" + model.LandMark + "','" + model.PropertyDate + "','"
                    + model.PropertyHeading + "','" + model.PropertySummary + "','"
                    + model.Name + "','" + model.Contact + "','" + model.EmailId + "','"
                    + model.strbase64Image + "','" + model.ImageName + "','" + model.ImageType + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditModeWithImage(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>'"+Resources.Resource1.norecord+"'</div>";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    html = Resources.Resource1.updatefailed;
                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = Resources.Resource1.updatefailed;
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PropertyDetailDelete(PropertyDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PropertyDetail_Delete '" + model.PropertyId + "'");
                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyDetail_Get ()");                  
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditModeWithImage(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>'"+Resources.Resource1.norecord+"'</div>";
                        return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = Resources.Resource1.deletefailed;
                    return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = Resources.Resource1.deletefailed;
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PropertyDetailGet(PropertyDetailModel model)
        {
            string html = null;
            int pagesize = 10;
            try
            {
                int total = Convert.ToInt32(obj.FunExecutescalar("select count(*) from PropertyDetail"));
                DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyDetail_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeWithImage(dt, columnHide);
                    StringBuilder totalRecord = CommonUtil.htmlTablePaging(total, pagesize);
                    //var jsonResult = Json(new { Flag = 0, Html = htmlTable.ToString(), TotalRecord = Convert.ToString(totalRecord) }, JsonRequestBehavior.AllowGet);
                    var jsonResult = Json(new { Flag = 0, Html = htmlTable.ToString(), TotalRecord = Convert.ToString(total) }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    html = "<div class='alert alert-danger'>'"+Resources.Resource1.norecord+"'</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = "Fatch Result Failed !!!";
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public ActionResult GetDetailWithPaging(PropertyDetailModel model)
        {
            string html = null;
            int PageSize = 10;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.Property_Detail_Get(" + PageSize + "," + model.PageNo + ")");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditModeWithImage(dt, columnHide);
                    //return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    var jsonResult = Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    html = "<div class='alert alert-danger'>'"+Resources.Resource1.norecord+"'</div>";
                    return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = "Fatch Result Failed !!!";
                return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }























        public ActionResult PropertyImageGalleryDetail()
        {
            PropertyImageGalleryModel model = new PropertyImageGalleryModel ();
            return View(model);
        }




    }
}
