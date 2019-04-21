using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Data;
using System.Text;
namespace MvcApplication1.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        BusinessLayer obj = new BusinessLayer();

        public ActionResult StudentAdmissionDetail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentAdmissionDetailInsert(StudentAdmissionDetailModel model)
        {
            string html = null;
            try
            {
                var filname = Request.Files["Photo"];
                var studname = Request.Files["StudentName"];

                int a = obj.FunExecuteNonQuery("exec dbo.usp_StudentAdmissionDetail_Create '"
                    + model.StudentName               +"','"
                    + model.FatherName                +"','"
                    + model.MotherName                +"','"
                    + model.Gender                    +"','"
                    + model.Contact                   +"','"
                    + model.FatherContact             +"','"
                    + model.EmgContact                +"','"
                    + model.EmailId                   +"','"
                    + model.DOB                       +"','"
                    + model.PAddress                  +"','"
                    + model.PCity                     +"','"
                    + model.PState                    +"','"
                    + model.PPincode                  +"','"
                    + model.CAddress                  +"','"
                    + model.CCity                     +"','"
                    + model.CState                    +"','"
                    + model.CPincode                  +"','"
                    + model.Height                    +"','"
                    + model.Wight                     +"','"
                    + model.AnyHealthIssue            +"','"
                    + model.HealthIssueDescription    +"','"
                    + model.CurrentQualification      +"','"
                    + model.ClassForAdmission         +"','"
                    + model.AdmissionFee              +"','"
                    + model.RollNo                    +"','"
                    + model.Photo                     +"','"
                    + model.DateOfAdmission           +"'");

                if (a > 0)
                {
                    DataTable dt = obj.FunDataTable("SELECT * from dbo.fnStudentAdmissionDetail_Get ()");
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

    }
}
