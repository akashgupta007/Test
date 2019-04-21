using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PoiseERP.Areas.Payroll.Controllers
{
    public class ReimbursementController : Controller
    {
        //
        // GET: /Payroll/Reimbursement/


        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        #region Code For ReimbursementItem
       
        public ActionResult ReimbursementItem()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");

                }
                EmpReimbursementItemViewModel model = new EmpReimbursementItemViewModel();
                return View(model);

            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();

            }


        }



        public ActionResult ReimbursementItemGet()
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable dt = obj.ReimbursementItemGet(null);

              

                int[] ColumnHide = new[] {0};
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmltable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                    return Json(new { Flag = 0, Html = htmltable.ToString() }, JsonRequestBehavior.AllowGet);

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
        public ActionResult ReimbursementItem(EmpReimbursementItemViewModel model,string Command)
        {

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
           
            if (Command == "Insert")
            {
                try
                {
                   
                      int? Exist = obj.ReimbursementItemExistsValidate(model.ReimbursementItemName);


                        if (Exist == 0)
                        {

                            int? status = obj.ReimbursementItemCreate(model.ReimbursementItemName,model.ReimbursementItemDesc);

                            ViewData["Msg"] =  "Record Insert  Successfully";
                            return View(model);
                        }
                        else
                        {
                            ViewData["Msg"] = "Record Already Exists !";
                            return View(model);
                        }
                    
                }
                catch (Exception ex)
                {
                    ViewData["Msg"] = ex.Message.ToString();
                    return View(model);
                }
            }
            else if (Command == "Update")
            {

                try
                {

                    int? status = obj.ReimbursementItemUpdate(model.EmpReimbursementItemId, model.ReimbursementItemName, model.ReimbursementItemDesc);
                        int[] ColumnHide = new[] { 0};
                        ViewData["Msg"] = " Record Update Successfully";
                        return View(model);
                }

                
                catch (Exception ex)
                {
                    ViewData["Msg"] = ex.Message.ToString();
                    return View(model);
                }
            }
            return View(model);


        }



        [HttpPost]
        public ActionResult ReimbursementItemDelete(EmpReimbursementItemViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.ReimbursementItemDelete(model.EmpReimbursementItemId);
                int[] ColumnHide = new[] {0};
                DataTable dt = obj.ReimbursementItemGet(null);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html= "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html}, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return Json(new { Flag = 0, Html = html }, JsonRequestBehavior.AllowGet);

            }
        }

            #endregion




            #region Code For Employee Reimbursement
        public ActionResult EmployeeReimbursement()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");

                }
              
                EmpReimbursementViewModel model = new EmpReimbursementViewModel();
               
                return View(model);

            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();

            }


        }



        //public ActionResult ReimbursementDownloadGet(int? EmpReimbursementId)
        //{
        //    EmpReimbursementViewModel model = new EmpReimbursementViewModel();
        //    try
        //    {
        //        byte[] filleDocument = null;
        //        string contentType = "";
        //        string filename = "";
        //        try
        //        {
        //            DataTable dtDocumentDownload = obj.EmpReimbursementDetailDownloadGet(EmpReimbursementId);
        //            if (dtDocumentDownload.Rows.Count > 0)
        //            {
        //                DataRow dr = dtDocumentDownload.Rows[0];
        //                filleDocument = (byte[])dr[0];
        //                contentType = dr[1].ToString();
        //                filename = dr[2].ToString();
        //            }

        //            return File(filleDocument, contentType, filename);
        //        }
        //        catch (Exception ex)
        //        {
        //            return File(filleDocument, contentType, filename);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(model);
        //    }
        //}

        public ActionResult EmployeeReimbursementGet(EmpReimbursementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                DataTable dt = obj.EmpReimbursementGet(null);

                // byte[] byteImage = null;

                int[] ColumnHide = new[] { 0, 1, 2, 4, 7,8, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                if (dt.Rows.Count > 0)
                {
                    StringBuilder htmltable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                    return Json(new { Flag = 0, Html = htmltable.ToString() }, JsonRequestBehavior.AllowGet);

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
        public ActionResult EmployeeDojValidate(int? employeeid)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }

            DataTable dt = obj.EmployeeDojDolGet(employeeid);

            string doj = Convert.ToString(dt.Rows[0]["DOJ"]);
            string dol = Convert.ToString(dt.Rows[0]["DOL"]);
            return Json(new {doj=doj,dol=dol });

        }
        [HttpPost]
        public ActionResult EmployeeReimbursement(EmpReimbursementViewModel model, string Command)
        {
            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            } 
            string contenttype=null;
            string fileName=null;
            byte[] uploadFile = null;
            int flag = CommonUtil.CompareDate(model.RequestDate, model.EmployeeId);
            if (flag == 2)
            {
                   ViewBag.Flag = "2";
                ViewBag.msg = "Request Date Should be Less than or Equal to Employee Date of Leaving ! !";

                return View(model);
            }
            else if (flag == 1)
            {
                ViewBag.Flag = "2";
                ViewBag.msg = "Request Date  Should be Greater than or Equal to Employee Date of Joining !";

                return View(model);
            }




            if (Command == "Insert")
            {
                try
                {

                   
                        int? Exist = obj.EmpReimbursementExistsValidate(model.EmployeeId, model.FromDate, model.ToDate, model.ProjectId,model.RequestDate,null);
                        if (Exist == 0)
                        {
                            if(model.File != null)
                            {
                                uploadFile = new byte[model.File.InputStream.Length];
                                model.File.InputStream.Read(uploadFile, 0, uploadFile.Length);

                                contenttype = model.File.ContentType;
                                fileName = Path.GetFileName(model.File.FileName);
                            }
                  
                            int? status = obj.EmpReimbursementCreate(model.EmployeeId,model.RequestDate, model.Amount, model.ReimbursementNotes, 
                                model.ProjectId, uploadFile, model.FromDate, model.ToDate, contenttype, fileName, model.ReimbursementItemId, model.IsDocument);

                               ViewBag.Flag = "0";
                               ViewBag.msg = "Inserted Successfully !";
                            //  EmpReimbursementViewModel modell = new EmpReimbursementViewModel();
                            //  modell.Flag = "1";
                            //  modell.ErrorMsg = "Updated Successfully !";
                            return View(model);
                           



                        }
                        else
                        {




                            ViewBag.Flag = "1";
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
                                 
                                DataTable dtDocumentDownload = obj.EmpReimbursementDetailDownloadGet(model.EmpReimbursementId);
                                if (dtDocumentDownload.Rows.Count > 0)
                                {
                                    DataRow dr = dtDocumentDownload.Rows[0];
                                    if (dr[0] != null)
                                    {
                                        uploadFile = (byte[])dr[0];
                                    }
                                    contenttype =  Convert.ToString(dr[1]);
                                    fileName = Convert.ToString(dr[2]);
                                }
                                
                               

                            }
                        
                         int? Exist = obj.EmpReimbursementExistsValidate(model.EmployeeId, model.FromDate, model.ToDate, model.ProjectId,model.RequestDate,model.EmpReimbursementId);
                         if (Exist == 0)
                         {

                             int? status = obj.EmpReimbursementUpdate(model.EmpReimbursementId, model.EmployeeId, model.RequestDate, model.Amount, model.ReimbursementNotes, model.ProjectId, uploadFile, model.FromDate, model.ToDate, contenttype, fileName, model.ReimbursementItemId, model.IsDocument);
                            


                             ViewBag.Flag = "1";
                             ViewBag.msg = "Updated Successfully !";
                         
                            // modell.Flag = "1";
                          //   modell.ErrorMsg = "Updated Successfully !";
                             return View(model);

                        
                        }
                        else
                        {
                          //  ViewBag.Flag = "2";
                           // ViewBag.msg = "Record Already Exists !";
                            ViewBag.Flag = "2";
                            ViewBag.msg = "Record Already Exists !";
                        
                            return View(model);
                        }
                }
                catch (Exception ex)
                {
                    model.Flag = "2";
                    model.ErrorMsg = ex.Message.ToString();
                    return View(model);
                }
            }
            else if (Command == "Download")
            {
                try
                {
                    byte[] filleDocument = null;
                    string contentType = "";
                    string filename = "";
                    try
                    {
                        DataTable dtDocumentDownload = obj.EmpReimbursementDetailDownloadGet(model.EmpReimbursementId);
                        if (dtDocumentDownload.Rows.Count > 0)
                        {
                            DataRow dr = dtDocumentDownload.Rows[0];
                            filleDocument = (byte[])dr[0];
                            contentType = dr[1].ToString();
                            filename = dr[2].ToString();
                        }

                        return File(filleDocument, contentType, filename);
                    }
                    catch (Exception ex)
                    {
                        return File(filleDocument, contentType, filename);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Flag = "2";
                    ViewBag.msg = Convert.ToString(ex.Message);
                   
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeReimbursementdelete(EmpReimbursementViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int? status = obj.EmpReimbursementDelete(model.EmpReimbursementId);
                int[] ColumnHide = new[] { 0, 1, 2, 4, 7, 8, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                DataTable dt = obj.EmpReimbursementGet(null);
                StringBuilder htmlTable = CommonUtil.htmlTableEditMode(dt, ColumnHide);
                return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult ReimbursementDetail()
        {

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                EmpReimbursementDetailViewModel model = new EmpReimbursementDetailViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message.ToString();
                return View();
            }

        }


        //Download file / Document
        //[HttpPost]
        //public ActionResult ReimbursementDetail(EmpReimbursementDetailViewModel model)
        //{
        //    try
        //    {
        //        byte[] filleDocument = null;
        //        string contentType = "";
        //        string filename = "";
        //        try
        //        {
        //            DataTable dtDocumentDownload = obj.EmpReimbursementDetailDownloadGet(model.EmpReimbursementId);
        //            if (dtDocumentDownload.Rows.Count > 0)
        //            {
        //                DataRow dr = dtDocumentDownload.Rows[0];
        //                filleDocument = (byte[])dr[0];
        //                contentType = dr[1].ToString();
        //                filename = dr[2].ToString();
        //            }

        //            return File(filleDocument, contentType, filename);
        //        }
        //        catch (Exception ex)
        //        {
        //            return File(filleDocument, contentType, filename);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        public ActionResult ReimbursementDetail(EmpReimbursementDetailViewModel model, string command)
        {
            string html = null;
            try
            {
                if (command == null)
                {

                    try
                    {
                        byte[] filleDocument = null;
                        string contentType = "";
                        string filename = "";
                        try
                        {
                            DataTable dtDocumentDownload = obj.EmpReimbursementDetailDownloadGet(model.EmpReimbursementId);
                            if (dtDocumentDownload.Rows.Count > 0)
                            {
                                DataRow dr = dtDocumentDownload.Rows[0];
                                filleDocument = (byte[])dr[0];
                                contentType = dr[1].ToString();
                                filename = dr[2].ToString();
                            }
                           
                            return File(filleDocument, contentType, filename);
                        }
                        catch (Exception ex)
                        {
                            return File(filleDocument, contentType, filename);
                        }
                    }
                    catch (Exception ex)
                    {
                        return View();
                    }
                }
                else
                {
                    if (Session["snReimbursementDetail"] != null)
                    {
                        GridView gv = GridViewGet((DataTable)Session["snReimbursementDetail"], "Reimbursement Detail");

                        ActionResult a = null;

                        if (command == "Pdf")
                        {
                            a = DataExportPDF(gv, "ReimbursementDetail", "N", 4);
                        }
                        if (command == "Excel")
                        {
                            a = DataExportExcel(gv, "ReimbursementDetail", 12);
                        }
                        if (command == "Word")
                        {
                            a = DataExportWord(gv, "ReimbursementDetail", 12);
                        }
                        return a;
                    }
                    else
                    {
                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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


        [HttpPost]
        public ActionResult ReimbursementDetailGet(EmpReimbursementDetailViewModel model)
        {


            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = new[] { 0 };
                DataTable dt = obj.EmpReimbursementDetailGet(model.EmployeeId, model.FromDate, model.ToDate);
                if (dt.Rows.Count > 0)
                {


                    StringBuilder htmlTable = CommonUtil.htmlChildTableDownloadEditMode(dt, columnHide);

                    DataTable dtReimburement = CommonUtil.DataTableColumnRemove(dt, columnHide);
                    Session.Add("snReimbursementDetail", dtReimburement);

                    return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult EmpReimbursementEntry()
        {

           
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                
                EmpReimbursementEntryViewModel model = new EmpReimbursementEntryViewModel();
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }

            return View();



        }


        [HttpPost]
        public ActionResult EmpReimbursementEntry(EmpReimbursementEntryViewModel modell,string command)
        {

            string html = null;
            int? status = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
                if (command == "Search")
                {

                   
                    DataTable dtRemb = obj.EmpReimbursementEntryGet(modell.MonthId, modell.Year, modell.EmployeeId, modell.DepartmentId, modell.DesginationId, modell.LocationId, modell.ProjectId, modell.EmpTypeId, modell.ShiftId,modell.StartDate,modell.EndDate);

                    if (dtRemb.Rows.Count > 0)
                    {
                        int[] HideColumn = { 0,1 };
                        StringBuilder htmlTable = CommonUtil.htmlTableReimbursementEntry(dtRemb, HideColumn);
                            return Json(Convert.ToString(htmlTable), JsonRequestBehavior.AllowGet);
                        
                       

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


                    if (modell.EmployeeDataList != null)
                    {
                        int count = modell.EmployeeDataList.Count;

                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "EmployeeDataList[" + x + "].isRowCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (modell.EmployeeDataList[x].EmployeeId != null)
                                {
                                    modell.Amount = modell.EmployeeDataList[x].Amount;
                                    if ((modell.Amount != null) || (modell.Amount != 0))
                                    {
                                        status = obj.EmpReimbursementEntryCreate(modell.EmployeeDataList[x].EmployeeId, modell.MonthId, modell.Year, modell.Amount,null, modell.EmployeeDataList[x].EmpReimbursementId);
                                    }
                                }
                            }

                        }





                    }



                    DataTable dtRemb = obj.EmpReimbursementEntryGet(modell.MonthId, modell.Year, modell.EmployeeId, modell.DepartmentId, modell.DesginationId, modell.LocationId, modell.ProjectId, modell.EmpTypeId, modell.ShiftId,modell.StartDate,modell.EndDate);

                    if (dtRemb.Rows.Count > 0)
                    {
                        int[] HideColumn = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableReimbursementEntry(dtRemb, HideColumn);
                        return Json(Convert.ToString(htmlTable), JsonRequestBehavior.AllowGet);



                    }

                    else
                    {

                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(html, JsonRequestBehavior.AllowGet);
                    }

                }


                return Json(html, JsonRequestBehavior.AllowGet);


                
            }
            catch (Exception ex)
            {
                html = "<div class='alert alert-danger'>"+ex.Message+"!!</div>";
                return Json(html, JsonRequestBehavior.AllowGet);
            }

           



        }

        [HttpPost]
        public ActionResult EmpReimbursementEntryCreate(EmpReimbursementEntryViewModel modell)
        {

            string html = null;
            int? status = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
               

               
                    string checkBoxName = string.Empty;
                    string IsCheck = string.Empty;

                    int n = 0;


                    if (modell.EmployeeDataList != null)
                    {
                        int count = modell.EmployeeDataList.Count;

                        for (int x = 0; x < count; x++)
                        {
                            checkBoxName = "EmployeeDataList[" + x + "].isRowCheck";
                            IsCheck = Request[checkBoxName];
                            if (IsCheck == "on")
                            {
                                if (modell.EmployeeDataList[x].EmployeeId != null)
                                {
                                    modell.Amount = modell.EmployeeDataList[x].Amount;
                                    if ((modell.Amount != null) || (modell.Amount != 0))
                                    {
                                        status = obj.EmpReimbursementEntryCreate(modell.EmployeeDataList[x].EmployeeId, modell.MonthId, modell.Year, modell.Amount, null, modell.EmployeeDataList[x].EmpReimbursementId);
                                    }
                                }
                            }

                        }





                 



                    DataTable dtRemb = obj.EmpReimbursementEntryGet(modell.MonthId, modell.Year, modell.EmployeeId, modell.DepartmentId, modell.DesginationId, modell.LocationId, modell.ProjectId, modell.EmpTypeId, modell.ShiftId, modell.StartDate, modell.EndDate);

                    if (dtRemb.Rows.Count > 0)
                    {
                        int[] HideColumn = { 0 };
                        StringBuilder htmlTable = CommonUtil.htmlTableReimbursementEntry(dtRemb, HideColumn);


                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);




                    }

                    else
                    {

                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }

                }


                return Json(html, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                html =  ex.Message ;
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }





        }



    }
}



