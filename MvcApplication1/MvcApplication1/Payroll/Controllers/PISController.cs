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
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PoiseERP.Areas.Payroll.Controllers
{
    public class PISController : Controller
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel objML = new PoisePayrollManliftServiceModel();
        // GET: /Payroll/PIS/

        

        public GridView GridViewGet(DataTable dt, string ReportHeader)
        {


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 4;

            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            DataTable dtCompany = obj.CurrentCompanyInformationGet("");
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

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.Font.Size = 10;

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
            GridView1.Font.Size = 10;
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



        //------------Employee Document details----------- 

        [HttpGet]
        public ActionResult EmployeeDocument()
        {
            try
            {

                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");

                }
                EmployeeDocumentviewModel model = new EmployeeDocumentviewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }




        }
        [HttpPost]
        public ActionResult EmployeeDocumentGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3, 5,7,8 };
                DataTable dtEmployeeDocumentGet = obj.EmployeeDocumentGet(null);
                if (dtEmployeeDocumentGet.Rows.Count > 0)
                {
                    
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dtEmployeeDocumentGet, columnHide);
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
        public ActionResult EmployeeDocument(EmployeeDocumentviewModel model, string Command)
        {


            string contenttype=null;
            string fileName=null;
            byte[] uploadFile = null;

            if (Command == "Insert")
            {

                try
                {
                    int? Exist = obj.EmployeeDocumentExistsValidate(model.DocumentId); //(model.DocumentId, model.DocumentObjectName);
                    if (Exist == 0)
                    {
                        if (model.File != null)
                        {
                            uploadFile = new byte[model.File.InputStream.Length];
                            model.File.InputStream.Read(uploadFile, 0, uploadFile.Length);

                            contenttype = model.File.ContentType;
                            fileName = Path.GetFileName(model.File.FileName);
                        }

                        int? status = obj.EmployeeDocumentCreate(model.EmployeeId, model.DocumentId, uploadFile, model.Notes, contenttype,fileName);  //DocumentObjectCreate(model.DocumentId, model.DocumentObjectName, contenttype, uploadFile, fileName);

                        ViewBag.Flag = "1";
                        ViewBag.msg = "Inserted Successfully !";
                        return View(model);
                    }
                    else
                    {

                        ViewBag.Flag = "2";
                        ViewBag.msg = "Record Already Exists !";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Flag = "2";
                    ViewBag.msg = Convert.ToString(ex.Message);
                    return View(model);
                }

            }
        
            else if (Command == "Update")
            {
                try
                {
                    if (model.File != null)
                    {
                        uploadFile = new byte[model.File.InputStream.Length];
                        model.File.InputStream.Read(uploadFile, 0, uploadFile.Length);

                        contenttype = model.File.ContentType;
                        fileName = Path.GetFileName(model.File.FileName);

                    }
                    else
                    {
                        model.DocumentImg = Encoding.ASCII.GetBytes(model.strDocumentImg);
                        uploadFile = model.DocumentImg;
                        contenttype = model.strContentType;
                        fileName = model.strDocumentName;


                    }
                    int? status = obj.EmployeeDocumentUpdate(model.EmployeeDocumentId, model.EmployeeId, model.DocumentId, uploadFile, model.Notes, contenttype, fileName);   //DocumentObjectUpdate(model.DocumentObjectId, model.DocumentId, model.DocumentObjectName, contenttype, uploadFile, fileName);
                    
                        ViewBag.Flag = "1";
                        ViewBag.msg = "Updated Successfully";
                        return View(model);
                    
                }
                catch (Exception ex)
                {
                    ViewBag.Flag = "2";
                    ViewBag.msg = Convert.ToString(ex.Message);
                    return View(model);
                }

            }


            else if (Command == "Download")
            {
              
                try
                {


                    int[] columnHide = new[] { 0, 1, 3, 5 };
                    //correction needs to be done there 
                    DataTable dtEmployeeDocumentDownloadGet = obj.EmployeeDocumentDownloadGet(model.EmployeeDocumentId);
                    if (dtEmployeeDocumentDownloadGet.Rows.Count > 0)
                    {
                        DataRow dr = dtEmployeeDocumentDownloadGet.Rows[0];
                        uploadFile = (byte[])dr[0];
                        contenttype = dr[1].ToString();
                        fileName = dr[2].ToString();
                    }


                    return File(uploadFile, contenttype, fileName);

                }
                catch (Exception ex)
                {
                    ViewBag.Flag = "2";
                    ViewBag.msg = Convert.ToString(ex.Message);

                    return File(uploadFile, contenttype, fileName);
                }
            }




            return File(uploadFile, contenttype, fileName);


        }
      
        [HttpPost]
        public ActionResult EmployeeDocumentdelete(EmployeeDocumentviewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmployeeDocumentDelete(model.EmployeeDocumentId);
                int[] ColumnHide = new[] { 0, 1, 3, 5 };
                DataTable dt = obj.EmployeeDocumentGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        //---------Employee Address details----------- 
        public ActionResult EmployeeAddress()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");

                }
                EmpAddressViewModel model = new EmpAddressViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }




        [HttpPost]
        public ActionResult EmployeeAddress(EmpAddressViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtEmployeeAddress"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeAddress"], "Employee Address");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeAddress");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeAddresst");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeAddresst");
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




        [HttpPost]
        public ActionResult EmployeeAddressGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 2, 6 };

                DataTable dt = obj.EmpAddressGet(null);
                if (dt.Rows.Count > 0)
                {
                    //  DataTable EmployeeAddress = CommonUtil.DataTableColumnRemove(dt, columnHide);

                    Session.Add("dtEmployeeAddress", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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






        [HttpPost]
        public ActionResult EmployeeAddressCreate(EmpAddressViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpAddressExistsValidate(model.EmployeeId, model.AddressTypeId, null);
                if (Exist == 0)
                {
                    int? status = obj.EmpAddressCreate(model.EmployeeId, model.AddressTypeId, model.Address1, model.Address2, model.City, model.State, Convert.ToString(model.Postcode), model.Country, Convert.ToString(model.ContactPhone), Convert.ToString(model.Mobile), model.Notes);
                    int[] columnHide = new[] { 0, 1, 2 };
                    DataTable dt = obj.EmpAddressGet(null);

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
        public ActionResult EmployeeAddressupdate(EmpAddressViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? Exist = obj.EmpAddressExistsValidate(model.EmployeeId, model.AddressTypeId, model.EmpAddressId);
                if (Exist == 0)
                {

                    int? status = obj.EmpAddressUpdate(model.EmpAddressId, model.EmployeeId, model.AddressTypeId, model.Address1, model.Address2, model.City, model.State, Convert.ToString(model.Postcode), model.Country, Convert.ToString(model.ContactPhone), Convert.ToString(model.Mobile), model.Notes);
                    int[] columnHide = new[] { 0, 1, 2 };
                    DataTable dt = obj.EmpAddressGet(null);
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
        public ActionResult EmployeeAddressdelete(EmpAddressViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpAddressDelete(model.EmpAddressId);
                int[] columnHide = new[] { 0, 1, 2 };

                DataTable dt = obj.EmpAddressGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        //---------Employee Identity details----------- ---------------------------------------------

        public ActionResult EmployeeIdentityProof()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpIdentityProofViewModel model = new EmpIdentityProofViewModel();
                return View(model);

            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }



        [HttpPost]
        public ActionResult EmployeeIdentityProof(EmpIdentityProofViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeeIdroof"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeIdroof"], "Employee Id Proof");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeIdProof");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeIdProof");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeIdProof");
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
        [HttpPost]
        public ActionResult EmployeeIdentityProofGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1 };


                DataTable dt = obj.EmpIdentityProofGet(null);
                if (dt.Rows.Count > 0)
                {

                    Session.Add("dtEmployeeIdroof", dt);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeeIdentityProofCreate(EmpIdentityProofViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpIdentityProofExistsValidate(model.EmployeeId, model.VoterId, model.PanNo, model.DrivingLicNo, model.PassportNo, model.OtherIdProof, model.EmpIdentityProofId);
                if (Exist == 0)
                {
                    int? status = obj.EmpIdentityProofCreate(model.EmployeeId, model.VoterId, model.PanNo, model.DrivingLicNo, model.PassportNo, model.PassportValidDt, model.OtherIdProof, model.Notes);
                    int[] columnHide = new[] { 0, 1 };
                    DataTable dt = obj.EmpIdentityProofGet(null);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Exist == 4)
                    {
                        return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Exist == 5)
                    {
                        return Json(new { Flag = 5, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Exist == 6)
                    {
                        return Json(new { Flag = 6, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Exist == 7)
                    {
                        return Json(new { Flag = 7, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Exist == 8)
                    {
                        return Json(new { Flag = 8, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

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
        public ActionResult EmployeeIdentityProofupdate(EmpIdentityProofViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.EmpIdentityProofExistsValidate(model.EmployeeId, model.VoterId, model.PanNo, model.DrivingLicNo, Convert.ToString(model.PassportNo), model.OtherIdProof, model.EmpIdentityProofId);
                if (Exist == 0)
                {
                    int? status = obj.EmpIdentityProofUpdate(model.EmpIdentityProofId, model.EmployeeId, model.VoterId, model.PanNo, model.DrivingLicNo, Convert.ToString(model.PassportNo), model.PassportValidDt, model.OtherIdProof, model.Notes);
                    int[] columnHide = new[] { 0, 1 };
                    DataTable dt = obj.EmpIdentityProofGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Exist == 4)
                    {
                        return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Exist == 5)
                    {
                        return Json(new { Flag = 5, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Exist == 6)
                    {
                        return Json(new { Flag = 6, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

                    if (Exist == 7)
                    {
                        return Json(new { Flag = 7, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Exist == 8)
                    {
                        return Json(new { Flag = 8, Html = "" }, JsonRequestBehavior.AllowGet);
                    }

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
        public ActionResult EmployeeIdentityProofdelete(EmpIdentityProofViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpIdentityProofDelete(model.EmpIdentityProofId);
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpIdentityProofGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        //---------Employee Education details-------------------------------------------------------------------------------------------------------------

        public ActionResult EmployeeEducation()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpEducationViewModel model = new EmpEducationViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }


        [HttpPost]
        public ActionResult EmployeeEducation(EmpEducationViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEducation"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEducation"], "Employee Education");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeEducation");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeEducation");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeEducation");
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


        [HttpPost]
        public ActionResult EmployeeEducationGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpEducationGet(null);
                if (dt.Rows.Count > 0)
                {

                    Session.Add("dtEducation", dt);

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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

        

        [HttpPost]
        public ActionResult EmployeeEducationCreate(EmpEducationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpEducationExistsValidate(model.EmployeeId, model.Education);
                if (Exist == 0)
                {

                    int? status = obj.EmpEducationCreate(model.EmployeeId, model.Education, model.UniversityName, model.UniversityAddress, model.EducationMonth, model.EducationYear, model.EducationGrade, model.EmpDocumentId);
                    int[] columnHide = new[] { 0, 1 };
                    DataTable dt = obj.EmpEducationGet(null);

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
        public ActionResult EmployeeEducationupdate(EmpEducationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpEducationUpdate(model.EmpEducationId, model.EmployeeId, model.Education, model.UniversityName, model.UniversityAddress, model.EducationMonth, model.EducationYear, model.EducationGrade, model.EmpDocumentId);
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpEducationGet(null);
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
        public ActionResult EmployeeEducationdelete(EmpEducationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpEducationDelete(model.EmpEducationId);
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpEducationGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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








        //---------Employee Family details-----------



        public ActionResult EmployeeFamilyDetails()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpFamilyDetailsViewModel model = new EmpFamilyDetailsViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }




        [HttpPost]
        public ActionResult EmployeeFamilyDetails(EmpFamilyDetailsViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeeFamilyDetails"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeFamilyDetails"], "Employee Family Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeFamilyDetails");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeFamilyDetails");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeFamilyDetails");
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

        [HttpPost]
        public ActionResult EmployeeFamilyDetailsGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = objML.EmpFamilyDetailsGet(null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeeFamilyDetails", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeeFamilyDetailsCreate(EmpFamilyDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpFamilyDetailsExistsValidate(model.EmployeeId, model.FamilyMemberName);
                if (Exist == 0)
                {
                    int? status = objML.EmpFamilyDetailsCreate(model.EmployeeId, model.FamilyMemberName, model.FamilyMemberDob, model.FamilyRelationId, model.IsFamilyDependent, model.FamilyMemberAge,model.FamilyMembeAdhaarNo);
                    int[] columnHide = new[] { 0, 1, 5 };
                    DataTable dt = obj.EmpFamilyDetailsGet(null);

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
        public ActionResult EmployeeFamilyDetailsupdate(EmpFamilyDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = objML.EmpFamilyDetailsUpdate(model.EmpFamilyDetailsId, model.EmployeeId, model.FamilyMemberName, model.FamilyMemberDob, model.FamilyRelationId, model.IsFamilyDependent, model.FamilyMemberAge, model.FamilyMembeAdhaarNo);
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpFamilyDetailsGet(null);
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
        public ActionResult EmployeeFamilyDetailsdelete(EmpFamilyDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpFamilyDetailsDelete(model.EmpFamilyDetailsId);
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpFamilyDetailsGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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












        //---------Employee nominee details-----------



        public ActionResult EmployeeNomineeDetails()
        {
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpNomineeDetailsViewModel model = new EmpNomineeDetailsViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }


        [HttpPost]
        public ActionResult EmployeeNomineeDetails(EmpNomineeDetailsViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeeNomineeDetails"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeNomineeDetails"], "Employee Nominee Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeNomineeDetails");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeNomineeDetails");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeNomineeDetails");
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

        [HttpPost]
        public ActionResult EmployeeNomineeDetailsGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 6, 14, 18, 19, 20 };
                DataTable dt = obj.EmpNomineeDetailsGet();
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeeNomineeDetails", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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




        [HttpPost]
        public ActionResult EmployeeNomineeDetailsCreate(EmpNomineeDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.EmpNomineeDetailsExistsValidate(model.NomineeId, model.EmployeeId, model.NomineeName, model.EmpDocumentId);
                if (Exist == 0)
                {

                    int? status = obj.EmpNomineeDetailsCreate(model.EmployeeId, model.NomineeName, model.NomineeAddress, model.NomineeDob, model.NomineeRelationId, model.NomineeSex, model.MaritalStatus, model.ShareAmount, model.IsMinor, model.GuardianName, model.GuardianAddress, model.GuardianRelationId, model.GuardianSex, model.GuardianDob, model.IsNominateForWidowPension, model.Remarks, model.EmpDocumentId);
                    int[] columnHide = new[] { 0, 1, 6, 14, 18, 19, 20 };
                    DataTable dt = obj.EmpNomineeDetailsGet();

                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (Exist == 4)
                    {
                        return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult EmployeeNomineeDetailsupdate(EmpNomineeDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? Exist = obj.EmpNomineeDetailsExistsValidate(model.NomineeId, model.EmployeeId, model.NomineeName, model.EmpDocumentId);
                if (Exist == 0)
                {
                    int? status = obj.EmpNomineeDetailsUpdate(model.NomineeId,
                    model.EmployeeId,
                    model.NomineeName,
                    model.NomineeAddress,
                    model.NomineeDob,
                    model.NomineeRelationId,
                    model.NomineeSex,
                    model.MaritalStatus,
                    model.ShareAmount,
                    model.IsMinor,
                    model.GuardianName,
                    model.GuardianAddress,
                    model.GuardianRelationId,
                    model.GuardianSex,
                    model.GuardianDob,
                    model.IsNominateForWidowPension,
                    model.Remarks,
                    model.EmpDocumentId);

                    int[] columnHide = new[] { 0, 1, 6, 14, 18, 19, 20 };
                    DataTable dt = obj.EmpNomineeDetailsGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (Exist == 4)
                    {
                        return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult EmployeeNomineeDetailsdelete(EmpNomineeDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpNomineeDetailsDelete(model.NomineeId);
                int[] columnHide = new[] { 0, 1, 6, 14, 18, 19, 20 };
                DataTable dt = obj.EmpNomineeDetailsGet();
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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




        //  --------------Previous Employer details---------------



        public ActionResult EmployeePreviousEmployerDetails()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpPreviousEmployerDetailsViewModel model = new EmpPreviousEmployerDetailsViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }


        }


        [HttpPost]
        public ActionResult EmployeePreviousEmployerDetails(EmpPreviousEmployerDetailsViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeePreviousEmployerDetails"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeePreviousEmployerDetails"], "Employee Previous Employer Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeePreviousEmployerDetails");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeePreviousEmployerDetails");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeePreviousEmployerDetails");
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

        [HttpPost]
        public ActionResult EmployeePreviousEmployerDetailsGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 13 };
                DataTable dt = obj.EmpPreviousEmployerDetailsGet();
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeePreviousEmployerDetails", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        [HttpPost]
        public ActionResult EmployeePreviousEmployerDetailsCreate(EmpPreviousEmployerDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpPreviousEmployerDetailsExistsValidate(model.EmployeeId, model.CompanyName);
                if (Exist == 0)
                {
                    int? status = obj.EmpPreviousEmployerDetailsCreate(model.EmployeeId, model.CompanyName, model.CompanyAddress, model.PfAccountno, model.EsiAccountno, model.EpfOfficeName, model.EpfOfficeAddress,
                        model.Doj, model.Dol, model.ReasonOfLeaving, null, model.ExperienceMonths, model.IsLastCompany);
                    int[] columnHide = new[] { 0, 1, 13 };
                    DataTable dt = obj.EmpPreviousEmployerDetailsGet();

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
        public ActionResult EmployeePreviousEmployerDetailsupdate(EmpPreviousEmployerDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpPreviousEmployerDetailsUpdate(model.EmpPreviousEmployerDetailsId,
                    model.EmployeeId,
                    model.CompanyName,
                    model.CompanyAddress,
                    model.PfAccountno,
                    model.EsiAccountno,
                    model.EpfOfficeName,
                    model.EpfOfficeAddress,
                    model.Doj, model.Dol,
                    model.ReasonOfLeaving,
                     null,
                    model.ExperienceMonths,
                    model.IsLastCompany);

                int[] columnHide = new[] { 0, 1, 13 };
                DataTable dt = obj.EmpPreviousEmployerDetailsGet();
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
        public ActionResult EmployeePreviousEmployerDetailsdelete(EmpPreviousEmployerDetailsViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpPreviousEmployerDetailsDelete(model.EmpPreviousEmployerDetailsId);
                int[] columnHide = new[] { 0, 1, 13 };
                DataTable dt = obj.EmpPreviousEmployerDetailsGet();
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        //  --------------Previous Promotiom details---------------



        public ActionResult EmployeePromotion()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpPromotionDemotionViewModel model = new EmpPromotionDemotionViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult EmployeePromotion(EmpPromotionDemotionViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeePromotionDetails"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeePromotionDetails"], "Employee Promotion Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeePromotionDetails");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeePromotionDetails");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeePromotionDetails");
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

        [HttpPost]
        public ActionResult EmployeePromotionDetailsGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpPromotionDemotionGet("P", null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeePromotionDetails", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeePromotionDetailsCreate(EmpPromotionDemotionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int flag = CommonUtil.CompareDate(model.PromoDemoOrderDate, model.EmployeeId);

                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? Exist = obj.EmpPromotionDemotionExistsValidate(model.EmployeeId, model.PromoDemoOrderNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpPromotionDemotionCreate(model.EmployeeId, model.PromoDemoOrderNo, model.PromoDemoOrderDate, model.DesignationId, model.PromoDemoFromDate, "P");
                        int[] columnHide = new[] { 0, 1, 5 };
                        DataTable dt = obj.EmpPromotionDemotionGet("P", null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeePromotionDetailsupdate(EmpPromotionDemotionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.PromoDemoOrderDate, model.EmployeeId);

                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int? status = obj.EmpPromotionDemotionUpdate(model.EmpPromoDemoId, model.EmployeeId, model.PromoDemoOrderNo, model.PromoDemoOrderDate, model.DesignationId, model.PromoDemoFromDate, "P");

                    int[] columnHide = new[] { 0, 1, 5 };
                    DataTable dt = obj.EmpPromotionDemotionGet("P", null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeePromotionDetailssdelete(EmpPromotionDemotionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpPromotionDemotionDelete(model.EmpPromoDemoId);
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpPromotionDemotionGet("P", null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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

        //  --------------Previous Demotion details---------------



        public ActionResult EmployeeDemotion()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpPromotionDemotionViewModel model = new EmpPromotionDemotionViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }



        [HttpPost]
        public ActionResult EmployeeDemotion(EmpPromotionDemotionViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeeDemotionDetails"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeDemotionDetails"], "Employee Demotion Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeDemotionDetails");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeDemotionDetails");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeDemotionDetails");
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
        [HttpPost]
        public ActionResult EmployeeDemotionDetailsGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpPromotionDemotionGet("D", null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeeDemotionDetails", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        [HttpPost]
        public ActionResult EmployeeDemotionDetailsCreate(EmpPromotionDemotionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.PromoDemoOrderDate, model.EmployeeId);

                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? Exist = obj.EmpPromotionDemotionExistsValidate(model.EmployeeId, model.PromoDemoOrderNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpPromotionDemotionCreate(model.EmployeeId, model.PromoDemoOrderNo, model.PromoDemoOrderDate, model.DesignationId, model.PromoDemoFromDate, "D");
                        int[] columnHide = new[] { 0, 1, 5 };
                        DataTable dt = obj.EmpPromotionDemotionGet("D", null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeDemotionDetailsupdate(EmpPromotionDemotionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int flag = CommonUtil.CompareDate(model.PromoDemoOrderDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? status = obj.EmpPromotionDemotionUpdate(model.EmpPromoDemoId,
                        model.EmployeeId,
                        model.PromoDemoOrderNo,
                        model.PromoDemoOrderDate,
                        model.DesignationId,
                        model.PromoDemoFromDate,
                        "D");

                    int[] columnHide = new[] { 0, 1, 5 };
                    DataTable dt = obj.EmpPromotionDemotionGet("D", null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeDemotionDetailssdelete(EmpPromotionDemotionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpPromotionDemotionDelete(model.EmpPromoDemoId);
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpPromotionDemotionGet("D", null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        //  ---------------------EmployeeTraining-----------------------------------


        public ActionResult EmployeeTraining()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpTrainingViewModel model = new EmpTrainingViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeTraining(EmpTrainingViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeeTraining"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeTraining"], "Employee Traning Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeTraining");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeTraining");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeTraining");
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

        [HttpPost]
        public ActionResult EmployeeTrainingGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpTrainingGet(null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeeTraining", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        [HttpPost]
        public ActionResult EmployeeTrainingCreate(EmpTrainingViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.TrainingGoDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? Exist = obj.EmpTrainingExistsValidate(model.EmpTrainingId, model.EmployeeId, model.TrainingName, model.TrainingGoNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpTrainingCreate(model.EmployeeId, model.TrainingName, model.TrainingGoNo, model.TrainingGoDate, model.TrainingFromDate, model.TrainingToDate, model.TrainingInstName, model.TrainingInstAddress);
                        int[] columnHide = new[] { 0, 1 };
                        DataTable dt = obj.EmpTrainingGet(null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (Exist == 4)
                        {
                            return Json(new { Flag = 5, Html = "" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeTrainingupdate(EmpTrainingViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.TrainingGoDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int? Exist = obj.EmpTrainingExistsValidate(model.EmpTrainingId, model.EmployeeId, model.TrainingName, model.TrainingGoNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpTrainingUpdate(model.EmpTrainingId, model.EmployeeId, model.TrainingName, model.TrainingGoNo, model.TrainingGoDate, model.TrainingFromDate, model.TrainingToDate, model.TrainingInstName, model.TrainingInstAddress);

                        int[] columnHide = new[] { 0, 1 };
                        DataTable dt = obj.EmpTrainingGet(null);
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (Exist == 4)
                        {
                            return Json(new { Flag = 5, Html = "" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeTrainingdelete(EmpTrainingViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpTrainingDelete(model.EmpTrainingId);
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpTrainingGet(null);
                if (dt.Rows.Count > 0)
                // if(status != null)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        //  --------------------------------EmployeeTransfer............................
        public ActionResult EmployeeTransfer()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpTransferViewModel model = new EmpTransferViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeTransfer(EmpTransferViewModel model, string command)
        {
            string html = null;
            try
            {

                if (Session["dtEmployeeTransfer"] != null)
                {

                    GridView gv = GridViewGet((DataTable)Session["dtEmployeeTransfer"], "Employee Transfer Details");

                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeTransfer");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeTransfer");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeTraining");
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

        [HttpPost]
        public ActionResult EmployeeTransferGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 8 };
                DataTable dt = obj.EmpTransferGet(null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtEmployeeTransfer", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeeTransferCreate(EmpTransferViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int flag = CommonUtil.CompareDate(model.TransferOrderDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int? Exist = obj.EmpTransferExistsValidate(model.EmployeeId, model.TransferOrderNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpTransferCreate(model.EmployeeId, model.TransferOrderNo, model.TransferOrderDate, model.TransferFrom, model.TransferTo, model.TransferDor, null, model.TransferJoinDate);
                        int[] columnHide = new[] { 0, 1, 8 };
                        DataTable dt = obj.EmpTransferGet(null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeTransferupdate(EmpTransferViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.TransferOrderDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int? status = obj.EmpTransferUpdate(model.EmpTransferId, model.EmployeeId, model.TransferOrderNo, model.TransferOrderDate, model.TransferFrom, model.TransferTo, model.TransferDor, null, model.TransferJoinDate);


                    int[] columnHide = new[] { 0, 1, 8 };
                    DataTable dt = obj.EmpTransferGet(null);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public ActionResult EmployeeTransferdelete(EmpTransferViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int? status = obj.EmpTransferDelete(model.EmpTransferId);
                int[] columnHide = new[] { 0, 1, 8 };
                DataTable dt = obj.EmpTransferGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        //...................................Employee Deputation .........................

        public ActionResult EmployeeDeputation()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpDeputationViewModel model = new EmpDeputationViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

    
        [HttpPost]
        public ActionResult EmployeeDeputation(EmpDeputationViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtDeputation"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtDeputation"], "Employee Deputaion");


                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeDeputaion");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeDeputaion");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeDeputaion");
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


        [HttpPost]
        public ActionResult EmployeeDeputationGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 10, 18 };
                DataTable dt = obj.EmpDeputationGet();
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtDeputation", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult EmployeeDeputationCreate(EmpDeputationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }


                int? Exist = obj.EmpDeputationExistsValidate(model.EmloyeeId, model.DeputationOrderNo);
                if (Exist == 0)
                {
                    int flag = CommonUtil.CompareDate(model.DeputationOrderDate, model.EmloyeeId);

                    if (flag == 1)
                    {
                        return Json(new { Flag = 1, Html = "Order Date Should be Greater than Joining Date !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!(model.DeputationPeriodFromDate < model.DeputationPeriodToDate))
                    {
                        return Json(new { Flag = 1, Html = "Period From Date Should be Less than Period To Date !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!(model.DeputationRelDate < model.DeputationPeriodFromDate))
                    {
                        return Json(new { Flag = 1, Html = "Relieve Date Should be Less than Period From Date !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!(model.DeputationRejoinDate > model.DeputationPeriodToDate))
                    {
                        return Json(new { Flag = 1, Html = "Rejoin Date Should be Greater than Period To Date !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!(model.DeputationBrjoinDate > model.DeputationPeriodFromDate) && (model.DeputationBrjoinDate < model.DeputationPeriodToDate))
                    {
                        return Json(new { Flag = 1, Html = "Borrowing Join Date Should be between Period From Date and Period To Date  !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!(model.DeputationBrelDate > model.DeputationPeriodFromDate) && (model.DeputationBrelDate < model.DeputationPeriodToDate))
                    {
                        return Json(new { Flag = 1, Html = "Borrowing Relieve Date Should be between Period From Date and Period To Date  !" }, JsonRequestBehavior.AllowGet);
                    }

                    if (!((model.DeputationOrderDate < model.DeputationRelDate) && (model.DeputationOrderDate < model.DeputationRejoinDate) && (model.DeputationOrderDate < model.DeputationBrjoinDate) && (model.DeputationOrderDate < model.DeputationBrelDate) && (model.DeputationOrderDate < model.DeputationPeriodFromDate) && (model.DeputationOrderDate < model.DeputationPeriodToDate)))
                    {
                        return Json(new { Flag = 1, Html = "Order Date Should be Less than All Dates !" }, JsonRequestBehavior.AllowGet);
                    }

                    int? status = obj.EmpDeputationCreate(model.EmloyeeId, model.DeputationOrderNo, model.DeputationOrderDate,
                        model.DeputationType, model.DeputationPeriodFromDate, model.DeputationPeriodToDate, model.DeputationParentOffice,
                        model.DeputationParentStation, model.DeputationParentDesignationId, model.DeputationRelDate, model.DeputationRelSession,
                        model.DeputationRejoinDate, model.DeputationRejoinSession, model.DeputationBorrOffice,
                        model.DeputationBorrStation, model.DeputationBorrDesignationId, model.DeputationBrjoinDate,
                        model.DeputationBrjoinSession, model.DeputationBrelDate, model.DeputationBrelSession);

                    int[] columnHide = new[] { 0, 1, 10, 18 };
                    DataTable dt = obj.EmpDeputationGet();

                    if (dt.Rows.Count > 0)
                    {
                        Session.Add("dtDeputation", dt);
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 2, Html = html }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Flag = 1, Html = "Record Already Exist !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult EmployeeDeputationupdate(EmpDeputationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.DeputationOrderDate, model.EmloyeeId);

                if (flag == 1)
                {
                    return Json(new { Flag = 1, Html = "Order Date Should be Greater than Joining Date !" }, JsonRequestBehavior.AllowGet);
                }

                if (!(model.DeputationPeriodFromDate < model.DeputationPeriodToDate))
                {
                    return Json(new { Flag = 1, Html = "Period From Date Should be Less than Period To Date !" }, JsonRequestBehavior.AllowGet);
                }

                if (!(model.DeputationRelDate < model.DeputationPeriodFromDate))
                {
                    return Json(new { Flag = 1, Html = "Relieve Date Should be Less than Period From Date !" }, JsonRequestBehavior.AllowGet);
                }

                if (!(model.DeputationRejoinDate > model.DeputationPeriodToDate))
                {
                    return Json(new { Flag = 1, Html = "Rejoin Date Should be Greater than Period To Date !" }, JsonRequestBehavior.AllowGet);
                }

                if (!(model.DeputationBrjoinDate > model.DeputationPeriodFromDate) && (model.DeputationBrjoinDate < model.DeputationPeriodToDate))
                {
                    return Json(new { Flag = 1, Html = "Borrowing Join Date Should be between Period From Date and Period To Date  !" }, JsonRequestBehavior.AllowGet);
                }

                if (!(model.DeputationBrelDate > model.DeputationPeriodFromDate) && (model.DeputationBrelDate < model.DeputationPeriodToDate))
                {
                    return Json(new { Flag = 1, Html = "Borrowing Relieve Date Should be between Period From Date and Period To Date  !" }, JsonRequestBehavior.AllowGet);
                }

                if (!((model.DeputationOrderDate < model.DeputationRelDate) && (model.DeputationOrderDate < model.DeputationRejoinDate) && (model.DeputationOrderDate < model.DeputationBrjoinDate) && (model.DeputationOrderDate < model.DeputationBrelDate) && (model.DeputationOrderDate < model.DeputationPeriodFromDate) && (model.DeputationOrderDate < model.DeputationPeriodToDate)))
                {
                    return Json(new { Flag = 1, Html = "Order Date Should be Less than All Dates !" }, JsonRequestBehavior.AllowGet);
                }

                int? status = obj.EmpDeputationUpdate(model.EmpDeputationId, model.EmloyeeId, model.DeputationOrderNo, model.DeputationOrderDate, model.DeputationType, model.DeputationPeriodFromDate,
                                    model.DeputationPeriodToDate,
                                     model.DeputationParentOffice,
                                     model.DeputationParentStation,
                                     model.DeputationParentDesignationId, model.DeputationRelDate, model.DeputationRelSession, model.DeputationRejoinDate, model.DeputationRejoinSession,
                                     model.DeputationBorrOffice, model.DeputationBorrStation, model.DeputationBorrDesignationId, model.DeputationBrjoinDate, model.DeputationBrjoinSession,
                                     model.DeputationBrelDate, model.DeputationBrelSession);

                int[] columnHide = new[] { 0, 1, 10, 18 };
                DataTable dt = obj.EmpDeputationGet();

                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtDeputation", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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
        public ActionResult EmployeeDeputationdelete(EmpDeputationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpDeputationDelete(model.EmpDeputationId);

                int[] columnHide = new[] { 0, 1, 10, 18 };
                DataTable dt = obj.EmpDeputationGet();

                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtDeputation", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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

        //-------------------------------Employee Suspension--------------------------------

        public ActionResult EmployeeSuspension()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpSuspensionViewModel model = new EmpSuspensionViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeSuspension(EmpSuspensionViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtSuspension"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtSuspension"], "Employee Suspention");


                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeSuspention");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeSuspention");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeSuspention");
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
        [HttpPost]
        public ActionResult EmployeeSuspensionGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpSuspensionGet(null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtSuspension", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeeSuspensionCreate(EmpSuspensionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.SuspensionOrderDate, model.EmployeeId);

                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? Exist = obj.EmpSuspensionExistsValidate(model.EmployeeId, model.SuspensionOrdno, null);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpSuspensionCreate(model.EmployeeId, model.SuspensionOrdno, model.SuspensionOrderDate, model.SuspensionDate, model.SuspensionToDate, model.SubsistenceRate);
                        int[] columnHide = new[] { 0, 1 };
                        DataTable dt = obj.EmpSuspensionGet(null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeSuspensionupdate(EmpSuspensionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.SuspensionOrderDate, model.EmployeeId);

                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? Exist = obj.EmpSuspensionExistsValidate(model.EmployeeId, model.SuspensionOrdno, model.EmpSuspensionId);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpSuspensionUpdate(model.EmpSuspensionId, model.EmployeeId, model.SuspensionOrdno, model.SuspensionOrderDate, model.SuspensionDate, model.SuspensionToDate, model.SubsistenceRate);
                        int[] columnHide = new[] { 0, 1 };
                        DataTable dt = obj.EmpSuspensionGet(null);
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeSuspensiondelete(EmpSuspensionViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpSuspensionDelete(model.EmpSuspensionId);
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpSuspensionGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        // ------------------------------------------------Employee Punishment----------------

        public ActionResult EmployeePunishment()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpPunishmentViewModel model = new EmpPunishmentViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult EmployeePunishment(EmpPunishmentViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtPunishment"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtPunishment"], "Employee Punishment");


                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeePunishment");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeePunishment");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeePunishment");
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

        [HttpPost]
        public ActionResult EmployeePunishmentGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 5 };
                DataTable dt = obj.EmpPunishmentGet();
                if (dt.Rows.Count > 0)
                {

                    Session.Add("dtPunishment", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeePunishmentCreate(EmpPunishmentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.PunishmentOrderDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int? Exist = obj.EmpPunishmentExistsValidate(model.EmployeeId, model.PunishmentOrderNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpPunishmentCreate(model.EmployeeId, model.PunishmentOrderNo, model.PunishmentOrderDate, model.PunishmentTypeId, model.PunishmentAuthority, model.PunishmentDetails);
                        int[] columnHide = new[] { 0, 1, 5, 10, 18 };
                        DataTable dt = obj.EmpPunishmentGet();

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeePunishmentupdate(EmpPunishmentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.PunishmentOrderDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int? status = obj.EmpPunishmentUpdate(model.EmpPunishmentId, model.EmployeeId, model.PunishmentOrderNo, model.PunishmentOrderDate, model.PunishmentTypeId, model.PunishmentAuthority, model.PunishmentDetails);
                    int[] columnHide = new[] { 0, 1, 5, 10, 18 };
                    DataTable dt = obj.EmpPunishmentGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeePunishmentdelete(EmpPunishmentViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpPunishmentDelete(model.EmpPunishmentId);
                int[] columnHide = new[] { 0, 1, 5, 10, 18 };
                DataTable dt = obj.EmpPunishmentGet();
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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






        //-----------------------------------------------------------Employee Probation------------



        public ActionResult EmployeeProbation()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpProbationViewModel model = new EmpProbationViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }


        [HttpPost]
        public ActionResult EmployeeProbation(EmpProbationViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtProbation"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtProbation"], "Employee Probation");


                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeProbation");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeProbation");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeProbation");
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

        [HttpPost]
        public ActionResult EmployeeProbationGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 7, 9 };
                DataTable dt = obj.EmpProbationGet(null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtProbation", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeeProbationCreate(EmpProbationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.ProbationOrderdate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    int? Exist = obj.EmpProbationExistsValidate(model.EmpProbationId, model.ProbationOrderNo);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpProbationCreate(model.EmployeeId, model.ProbationOrderNo, model.ProbationOrderdate, model.ProbationStartDate, model.ProbationCompletionDate, model.DesignationId, model.DepartmentId, model.Remarks);
                        int[] columnHide = new[] { 0, 1, 7, 9 };
                        DataTable dt = obj.EmpProbationGet(null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeProbationupdate(EmpProbationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int flag = CommonUtil.CompareDate(model.ProbationOrderdate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int? Exist = obj.EmpProbationExistsValidate(model.EmpProbationId, model.ProbationOrderNo);
                    if (Exist == 0)
                    {

                        int? status = obj.EmpProbationUpdate(model.EmpProbationId, model.EmployeeId, model.ProbationOrderNo, model.ProbationOrderdate, model.ProbationStartDate, model.ProbationCompletionDate, model.DesignationId, model.DepartmentId, model.Remarks);
                        int[] columnHide = new[] { 0, 1, 7, 9 };
                        DataTable dt = obj.EmpProbationGet(null);
                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeProbationdelete(EmpProbationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpProbationDelete(model.EmpProbationId);
                int[] columnHide = new[] { 0, 1, 7, 9 };
                DataTable dt = obj.EmpProbationGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        //.........................................Employee Department Test............................................................


        public ActionResult EmployeeDeptTest()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpDeptTestViewModel model = new EmpDeptTestViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeDeptTest(EmpDeptTestViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtDeptTest"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtDeptTest"], "Employee Department Test");


                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeDeptTest");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeDeptTest");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeDeptTest");
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

        [HttpPost]
        public ActionResult EmployeeDeptTestGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1, 3 };
                DataTable dt = obj.EmpDeptTestGet(null);
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtDeptTest", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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




        [HttpPost]
        public ActionResult EmployeeDeptTestCreate(EmpDeptTestViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.PassedDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int? Exist = obj.EmpDeptTestExistsValidate(model.EmpDeptTestId, model.EmployeeId, model.DeptTestId, model.RegisterNumber, model.GazetteNumber);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpDeptTestCreate(model.EmployeeId, model.DeptTestId, model.PassedDate, model.Authority, model.RegisterNumber, model.GazetteNumber, model.ResultDate);
                        int[] columnHide = new[] { 0, 1, 3 };
                        DataTable dt = obj.EmpDeptTestGet(null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        if (Exist == 4)
                        { return Json(new { Flag = 5, Html = "" }, JsonRequestBehavior.AllowGet); }

                        if (Exist == 5)
                        { return Json(new { Flag = 6, Html = "" }, JsonRequestBehavior.AllowGet); }

                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeDeptTestupdate(EmpDeptTestViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }



                int flag = CommonUtil.CompareDate(model.PassedDate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int? Exist = obj.EmpDeptTestExistsValidate(model.EmpDeptTestId, model.EmployeeId, model.DeptTestId, model.RegisterNumber, model.GazetteNumber);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpDeptTestUpdate(model.EmpDeptTestId, model.EmployeeId, model.DeptTestId, model.PassedDate, model.Authority, model.RegisterNumber, model.GazetteNumber, model.ResultDate);
                        int[] columnHide = new[] { 0, 1, 3 };
                        DataTable dt = obj.EmpDeptTestGet(null);

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        if (Exist == 4)
                        { return Json(new { Flag = 5, Html = "" }, JsonRequestBehavior.AllowGet); }

                        if (Exist == 5)
                        { return Json(new { Flag = 6, Html = "" }, JsonRequestBehavior.AllowGet); }

                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeDeptTestdelete(EmpDeptTestViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpDeptTestDelete(model.EmpDeptTestId);
                int[] columnHide = new[] { 0, 1, 3 };
                DataTable dt = obj.EmpDeptTestGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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


        //   ...............................EmployeeSRVerification......................................................


        public ActionResult EmployeeSRVerification()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpSrVerificationViewModel model = new EmpSrVerificationViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EmployeeSRVerification(EmpSrVerificationViewModel model, string command)
        {
            string html = null;
            try
            {
                if (Session["dtSRVerification"] != null)
                {


                    GridView gv = GridViewGet((DataTable)Session["dtSRVerification"], "Employee SR Verification");


                    ActionResult a = null;

                    if (command == "Pdf")
                    {
                        a = DataExportPDF(gv, "EmployeeSRVerification");
                    }
                    if (command == "Excel")
                    {
                        a = DataExportExcel(gv, "EmployeeSRVerification");
                    }
                    if (command == "Word")
                    {
                        a = DataExportWord(gv, "EmployeeSRVerification");
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


        [HttpPost]
        public ActionResult EmployeeSRVerificationGet()
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpSrVerificationGet();
                if (dt.Rows.Count > 0)
                {
                    Session.Add("dtSRVerification", dt);
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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



        [HttpPost]
        public ActionResult EmployeeSRVerificationCreate(EmpSrVerificationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int flag = CommonUtil.CompareDate(model.SrVerifyOndate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int? Exist = obj.EmpSrVerificationExistsValidate(model.EmployeeId);
                    if (Exist == 0)
                    {
                        int? status = obj.EmpSrVerificationCreate(model.EmployeeId, model.SrVerifyOndate, model.SrVerifyFromDate, model.SrVerifyToDate, model.IsVerified, model.SrVerifyBy, model.Remarks);
                        int[] columnHide = new[] { 0, 1 };
                        DataTable dt = obj.EmpSrVerificationGet();

                        StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                        return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Flag = 2, Html = "" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult EmployeeSRVerificationupdate(EmpSrVerificationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int flag = CommonUtil.CompareDate(model.SrVerifyOndate, model.EmployeeId);
                if (flag == 2)
                {
                    return Json(new { Flag = 3, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else if (flag == 1)
                {
                    return Json(new { Flag = 4, Html = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    int? status = obj.EmpSrVerificationUpdate(model.EmpSrVerificationId, model.EmployeeId, model.SrVerifyOndate, model.SrVerifyFromDate, model.SrVerifyToDate, model.IsVerified, model.SrVerifyBy, model.Remarks);
                    int[] columnHide = new[] { 0, 1 };
                    DataTable dt = obj.EmpSrVerificationGet();
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmployeeSRVerificationdelete(EmpSrVerificationViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpSrVerificationDelete(model.EmpSrVerificationId);
                int[] columnHide = new[] { 0, 1 };
                DataTable dt = obj.EmpSrVerificationGet();
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, columnHide);
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









    }
}
