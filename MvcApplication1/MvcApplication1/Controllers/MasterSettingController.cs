using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;

namespace MvcApplication1.Controllers
{
    public class MasterSettingController : Controller
    {
        //
        // GET: /MasterSetting/
        BusinessLayer obj = new BusinessLayer();

        public ActionResult PropertyType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PropertyTypeInsert(PropertyTypeDetail model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PropertyTypeDetail_Create '" + model.OrderNo + "','" + model.PropertyTypeName + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult PropertyTypeUpdate(PropertyTypeDetail model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PropertyTypeDetail_Update '" + model.PropertyTypeId + "','" + model.OrderNo + "','" + model.PropertyTypeName + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult PropertyTypeDelete(PropertyTypeDetail model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PropertyTypeDetail_Delete '" + model.PropertyTypeId + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult PropertyTypeGet(PropertyTypeDetail model)
        {
            string html = null;
            try
            {             
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PropertyTypeDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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



        public ActionResult Location()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LocationDetailInsert(LocationDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.CityDetail_Create '" + model.OrderNo + "','" + model.CityName + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.CityDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult LocationDetailUpdate(LocationDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.CityDetail_Update '" + model.CityId + "','" + model.OrderNo + "','" + model.CityName + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.CityDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult LocationDetailDelete(LocationDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.CityDetail_Delete '" + model.CityId + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.CityDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult LocationDetailGet(LocationDetailModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.CityDetail_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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


        public ActionResult PriceRange()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PriceDetailInsert(PriceRangeModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PriceDetail_Create '" + model.OrderNo + "','" + model.MinAmountRange + "','" + model.MaxAmountRange + "','" + model.AmountRange + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PriceDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult PriceDetailUpdate(PriceRangeModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PriceDetail_Update '" + model.PriceId + "','" + model.OrderNo + "','" + model.MinAmountRange + "','" + model.MaxAmountRange + "','" + model.AmountRange + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PriceDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult PriceDetailDelete(PriceRangeModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.PriceDetail_Delete '" + model.PriceId + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.PriceDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult PriceDetailGet(PriceRangeModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.PriceDetail_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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


        public ActionResult MainBanner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MainBannerInsert(MainBannerDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.BannerDetail_Create '" + model.OrderNo + "','" + model.BannerImage + "','" + model.strbase64Image + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImage(dt, columnHide);
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
        public ActionResult MainBannerUpdate(MainBannerDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.BannerDetail_Update '" + model.BannerId + "','" + model.OrderNo + "','" + model.BannerImage + "','" + model.strbase64Image + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImage(dt, columnHide);
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
        public ActionResult MainBannerGet(MainBannerDetailModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetail_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableImage(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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
        public ActionResult MainBannerDelete(MainBannerDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.BannerDetail_Delete '" + model.BannerId + "'");
                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImage(dt, columnHide);
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
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



        public ActionResult SubBanner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubBannerInsert(SubBannerDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.BannerDetailSub_Create '" + model.OrderNo + "','" + model.BannerImage + "','" +
                    model.strbase64Image + "','" + model.TopBottom + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetailSub_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
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
        public ActionResult SubBannerUpdate(SubBannerDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.BannerDetailSub_Update '" + model.SubBannerId + "','" + model.OrderNo + "','" +
                    model.BannerImage + "','" + model.strbase64Image + "','" + model.TopBottom + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetailSub_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
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
        public ActionResult SubBannerGet(SubBannerDetailModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetailSub_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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
        public ActionResult SubBannerDelete(SubBannerDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.BannerDetailSub_Delete '" + model.SubBannerId + "'");
                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.BannerDetailSub_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
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


        public ActionResult TextFlash()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TextFlashNewsInsert(TextFlashModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.TextFlashNews_Create '" + model.TextMessage + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.TextFlashNews_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
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
        public ActionResult TextFlashNewsUpdate(TextFlashModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.TextFlashNews_Update '" + model.TextFlashId + "','" + model.TextMessage + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.TextFlashNews_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
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
        public ActionResult TextFlashNewsGet(TextFlashModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.TextFlashNews_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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
        public ActionResult TextFlashNewsDelete(TextFlashModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.TextFlashNews_Delete '" + model.TextFlashId + "'");
                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.TextFlashNews_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableImageSub(dt, columnHide);
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
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


        public ActionResult Address()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressDetailInsert(AddressDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.AddressDetail_Create '" + model.OrderNo + "','" + model.MainDetail + "','" + model.SubDetail + "','" + model.Contact
                    + "','" + model.EmailId + "'");

                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult AddressDetailUpdate(AddressDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.AddressDetail_Update '" + model.AddressId + "','" + model.OrderNo + "','" + model.MainDetail + "','" + model.SubDetail
                    + "','" + model.Contact + "','" + model.EmailId + "'");

                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult AddressDetailDelete(AddressDetailModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.AddressDetail_Delete '" + model.AddressId + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetail_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult AddressDetailGet(AddressDetailModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetail_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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


        public ActionResult AddressMap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressDetailMapInsert(AddressDetailMapModel model)
        {
            string html = null;
            try
            {
                model.AddressId = 3;
                int a = obj.FunExecuteNonQuery("exec dbo.AddressDetailMap_Create '" + model.AddressId + "','" + model.LocationName
                    + "','" + model.Latitude + "','" + model.Longitude + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetailMap_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult AddressDetailMapUpdate(AddressDetailMapModel model)
        {
            string html = null;
            try
            {
                model.AddressId = 3;
                int a = obj.FunExecuteNonQuery("exec dbo.AddressDetailMap_Update '" + model.AddressGoogleMapId
                    + "','" + model.AddressId + "','" + model.LocationName
                    + "','" + model.Latitude + "','" + model.Longitude + "'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetailMap_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult AddressDetailMapDelete(AddressDetailMapModel model)
        {
            string html = null;
            try
            {
                int a = obj.FunExecuteNonQuery("exec dbo.AddressDetailMap_Delete '" + model.AddressGoogleMapId + "'");
                if (a > 0)
                {
                    //return RedirectToAction("PropertyTypeGet");
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetailMap_Get ()");
                    if (dt.Rows.Count > 0)
                    {
                        int[] columnHide = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult AddressDetailMapGet(AddressDetailMapModel model)
        {
            string html = null;
            try
            {
                DataTable dt = obj.FunDataTable("SELECT * from dbo.AddressDetailMap_Get ()");
                if (dt.Rows.Count > 0)
                {
                    int[] columnHide = { 0 };
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
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

        public ActionResult uitest()
        {
            return View();
        }

    }
}
