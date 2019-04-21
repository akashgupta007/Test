using MCLSystem;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web;
namespace PoiseERP.Areas.Payroll.Controllers
{
    public class WorkflowController : Controller
    {
        //
        // GET: /Payroll/Workflow/

        //public ActionResult WorkflowProcess()
        //{
        //    return View();
        //}

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        //download file



















        //


        [HttpPost]
        public ActionResult WorkflowProcessDetail(WorkflowViewModel model, string command, string filedate)
        {

            ActionResult dataFile = null;

            WorkflowViewModel datamodel = new WorkflowViewModel();
            filedate = filedate == null || filedate == "" ? DateTime.Now.ToShortDateString() : filedate;
            model.filecreateddate = Convert.ToDateTime(filedate);
            DataSet BankDataSet = new DataSet();
            datamodel.BankList = new List<SelectListItem>();
            DataTable dtBankList = obj.EmployeeBankGet();
            datamodel.BankList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtBankList.Rows)
            {
                datamodel.BankList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            try
            {
                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                string AppKeyList = "";
                string App_Key = "";
                int n = 0;
                if (model.WorkflowDataList != null)
                {
                    int count = model.WorkflowDataList.Count;
                    for (int x = 0; x < count; x++)
                    {
                        checkBoxName = "WorkflowDataList[" + x + "].isCheck";
                        IsCheck = Request[checkBoxName];
                        if (IsCheck == "on")
                        {
                            if (model.WorkflowDataList[x].AppKey != null)
                            {
                                App_Key = Convert.ToString(model.WorkflowDataList[x].AppKey).Trim();


                                if (!(string.IsNullOrEmpty(App_Key)))
                                {



                                    if (n == 0)
                                    {
                                        AppKeyList = Convert.ToString(model.WorkflowDataList[x].AppKey).Trim();
                                    }
                                    else
                                    {
                                        AppKeyList += "," + Convert.ToString(model.WorkflowDataList[x].AppKey).Trim();
                                    }

                                    n = n + 1;
                                }
                            }
                        }
                    }
                }

                if (model.ProcessId == 186) //------Reimbursement process
                {
                 BankDataSet = obj.ReimbursementBankFileGet(AppKeyList, model.BankId);
                }

                else   if (model.ProcessId == 187) //------variable salary process
                {
                  BankDataSet = obj.VariablePayBankFileGet(AppKeyList, model.BankId);
                }
                else  //---- Payroll processs
                {
                    if (model.ProcessId ==0 ) //------Reimbursement process
                    {
                        BankDataSet = obj.SalaryReimbursementBankFileGet(AppKeyList, model.BankId);
                    }
                    else
                    {

                        BankDataSet = obj.SalaryBankFileGet(AppKeyList, model.BankId);
                    }
                }

                string CompanyName = string.Empty;
                if (BankDataSet.Tables.Count > 0)
                {

                    if (BankDataSet.Tables[0].Rows.Count > 0)
                    {
                        GridView gv = null;

                        DataTable dt1 = obj.ListToTable(AppKeyList, "~");
                        if (dt1.Rows.Count > 0)
                        {
                            string payyear = Convert.ToString(dt1.Rows[3][1]);
                            string paymonth = Convert.ToString(dt1.Rows[2][1]);
                        }

                       

                        if (command == "BankStatement")
                        {
                           
                            if (model.BankId == 8 || model.BankId == 35)
                            {
                                
                                DataTable dt = new DataTable();
                         
                                dt = BankDataSet.Tables[0];
                         
                                if (model.BankId == 8)
                                {

                                    dataFile = DataExportNotePad(dt, "ICICI", ".txt", true, DateTime.Now);
                                }

                                else
                                {
                                    dataFile = DataExportNotePadHDFC(dt, "ARC69SAL", ".txt", true, DateTime.Now);
                                }


                             
                            }

                        }


                        if (command == "Bankinfo")
                        {

                            if (model.BankId == 8 || model.BankId == 35)
                            {

                               
                                DataTable dtinfo = new DataTable();
                              
                              

                                dtinfo = BankDataSet.Tables[2];
                                if (model.BankId == 8)
                                {

                                    dataFile = DataExportNotePad(dtinfo, "ICICI", "_info.txt", false, model.filecreateddate);
                                }

                                else 
                                {
                                    dataFile = DataExportNotePadHDFC(dtinfo, "ARC69SAL", "_info.txt", false, model.filecreateddate);
                                }



                            }

                        }



                        //--for Excel--
                        if (command == "Ok")
                        {
                            if (BankDataSet.Tables[2].Rows.Count > 0)
                            {
                                CompanyName = Convert.ToString(BankDataSet.Tables[2].Rows[0][0]);
                            }

                            if (BankDataSet.Tables[0].Rows.Count > 22)
                            {
                                DataTable dt = new DataTable();
                                dt = BankDataSet.Tables[0];
                                gv = GridViewGet(dt, CompanyName);
                            }
                            else
                            {
                                gv = GridViewExcelGet(BankDataSet.Tables[0], CompanyName);
                            }
                            dataFile = DataExportExcel(gv, model.ExcelPassword, 12);
                        }

                        if (command == "MSWord")
                        {
                            if (BankDataSet.Tables[1].Rows.Count > 0)
                            {
                                string htmlText = Convert.ToString(BankDataSet.Tables[1].Rows[0][0]);

                                dataFile = DataExportWord(htmlText, "DhanBanksalarySummary", 12);
                            }
                        }
                        return dataFile;
                    }
                }



                return View(datamodel);
            }

            catch (Exception ex)
            {

                return dataFile;

            }

            return View(datamodel);
        }

        public GridView GridViewGet(DataTable dt, string ReportHeader)
        {


            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.RowDataBound += GridView1_RowDataBound;
            GridView1.Font.Size = 8;

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridViewRow FooterRow;
            GridViewRow HeaderRow1;
            TableCell FooterCell;
            int colsan;
            int FooterIndex = 0;

            int countRow = dt.Rows.Count;
            int loopCount = countRow / 22;
            int loopRem = countRow % 22;
            int i = 0;
            int j = 0;
            string br;


            j = 22;
            for (i = 1; i <= loopCount; i++)
            {
                j = j * i;
                FooterIndex = FooterIndex + 3;
                HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "S.No";
                //HeaderCell.ForeColor = Color.RoyalBlue;
                //HeaderCell.Font.Bold = true;
                //HeaderCell.Font.Size = 12;
                //HeaderCell.Width = 6;
                //HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.Font.Name = "Arial Narrow";
                HeaderRow1.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Employee No";
                //HeaderCell.ForeColor = Color.RoyalBlue;
                //HeaderCell.Font.Bold = true;
                //HeaderCell.Font.Size = 12;
                //HeaderCell.Width = 9;
                //HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.Font.Name = "Arial Narrow";
                HeaderRow1.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Employee Name";
                //HeaderCell.ForeColor = Color.RoyalBlue;
                //HeaderCell.Font.Bold = true;
                //HeaderCell.Font.Size = 12;
                //HeaderCell.Width = 28;
                //HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.Font.Name = "Arial Narrow";
                HeaderRow1.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Project";
                //HeaderCell.ForeColor = Color.RoyalBlue;
                //HeaderCell.Font.Bold = true;
                //HeaderCell.Font.Size = 12;
                //HeaderCell.Width = 12;
                //HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.Font.Name = "Arial Narrow";
                HeaderRow1.Cells.Add(HeaderCell);





                HeaderCell = new TableCell();
                HeaderCell.Text = "Net Salary";
                //HeaderCell.Font.Bold = true;
                //HeaderCell.Font.Size = 12;
                //HeaderCell.Width = 10;
                //HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.Font.Name = "Arial Narrow";
                HeaderRow1.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Bank Account No";
                //HeaderCell.ForeColor = Color.RoyalBlue;
                //HeaderCell.Font.Bold = true;
                //HeaderCell.Font.Size = 12;
                //HeaderCell.Width = 22;
                //HeaderCell.BorderStyle = BorderStyle.Solid;
                HeaderCell.Font.Name = "Arial Narrow";
                HeaderRow1.Cells.Add(HeaderCell);




                HeaderRow1.Font.Bold = true;
                HeaderRow1.Font.Size = 12;
                HeaderRow1.Attributes.Add("class", "header");

                GridView1.Controls[0].Controls.AddAt(j + i, HeaderRow1);

                FooterRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
                FooterCell = new TableCell();





                // FooterCell.Text = "<center><div style='height:66;'><b>Maruti Computers P Limited." + "</b></div><div style=height:66;></div><br /><div style=height:33;><b>Swaminathan Muthuvelu</b></div><br /><div style=height:33;><b>Managing Director & Authorized Signatory.</b></div></center>" + str;

                FooterCell.Text = "<center><b>" + ReportHeader + ".<br /><br /><br /><br />Swaminathan Muthuvelu<br />Managing Director & Authorized Signatory.</b><br /></center><br />";
                colsan = dt.Columns.Count;
                FooterCell.ColumnSpan = colsan;
                FooterCell.BorderStyle = BorderStyle.None;
                FooterRow.Cells.Add(FooterCell);
                FooterRow.BorderStyle = BorderStyle.None;
                GridView1.Controls[0].Controls.AddAt(j + i, FooterRow);
                GridView1.FooterStyle.BorderStyle = BorderStyle.None;
            }


            FooterRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Insert);
            FooterCell = new TableCell();
            //    FooterCell.Text = "<center><div style='height:66;'><b><br />Maruti Computers P Limited." + "</b></div><div style=height:66;></div><br /><div style=height:33;><b>Swaminathan Muthuvelu</b></div><br /><div style=height:33;><b>Managing Director & Authorized Signatory.</b></div></center>";

            FooterCell.Text = "<center><b>" + ReportHeader + ".<br /><br /><br /><br />Swaminathan Muthuvelu<br />Managing Director & Authorized Signatory.</b></center><br />";
            colsan = dt.Columns.Count;
            FooterCell.ColumnSpan = colsan;
            FooterCell.BorderStyle = BorderStyle.None;
            FooterRow.Cells.Add(FooterCell);
            FooterRow.BorderStyle = BorderStyle.None;
            GridView1.Controls[0].Controls.AddAt(countRow + FooterIndex, FooterRow);

            GridView1.FooterStyle.BorderStyle = BorderStyle.None;




            //   GridView1.RowStyle.Height = 35;

            //  GridView1.Columns[].ItemStyle.HorizontalAlign = HorizontalAlign.Left;

            //  GridView1.RowStyle.HorizontalAlign = HorizontalAlign.Right;
            //  GridView1.RowStyle.VerticalAlign = VerticalAlign.Middle;
            return GridView1;


        }

        private void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        public GridView GridViewExcelGet(DataTable dt, string ReportHeader)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 8;
            GridView1.RowDataBound += GridView1_RowDataBound;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridViewRow FooterRow;
            TableCell FooterCell;
            int colsan;
            int countRow = dt.Rows.Count;
            int j = countRow + 1;

            FooterRow = new GridViewRow(0, 0, DataControlRowType.Separator, DataControlRowState.Insert);
            FooterCell = new TableCell();

            //   FooterCell.Text = "<center><div style='height:66;'><b><br />Maruti Computers P Limited." + "</b></div><div style=height:66;></div><br /><div style=height:33;><b>Swaminathan Muthuvelu</b></div><br /><div style=height:33;><b>Managing Director & Authorized Signatory.</b></div></center>" + str;
            FooterCell.Text = "<center><b>" + ReportHeader + ".<br /><br /><br /><br />Swaminathan Muthuvelu<br />Managing Director & Authorized Signatory.</b></center><br />";
            colsan = dt.Columns.Count;
            FooterCell.ColumnSpan = colsan;
            FooterRow.Cells.Add(FooterCell);
            GridView1.Controls[0].Controls.AddAt(j, FooterRow);

            return GridView1;


        }


        public GridView GridViewGetOld(DataTable dt, string ReportHeader)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 8;

            //GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell2 = new TableCell();
            //DataTable dtCompany = obj.CurrentCompanyInformationGet("");
            //string CompanyName = Convert.ToString(dtCompany.Rows[0][0]);
            //string CompanyAddress = Convert.ToString(dtCompany.Rows[0][1]);
            //string CompanyCity = Convert.ToString(dtCompany.Rows[0][2]);
            //string PinCode = Convert.ToString(dtCompany.Rows[0][3]);
            //byte[] CompanyLogo = (byte[])dtCompany.Rows[0][10];
            //string PhotoString = Convert.ToBase64String(CompanyLogo);

            //DateTimeFormatInfo dinfo1 = new DateTimeFormatInfo();

            //HeaderCell2.Text = CompanyName + "<br />" + CompanyAddress + "<br />" + CompanyCity + " " + PinCode + "<br />" + ReportHeader;
            //int colsan = dt.Columns.Count;
            //HeaderCell2.ColumnSpan = colsan;
            //HeaderRow.Cells.Add(HeaderCell2);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridViewRow FooterRow = new GridViewRow(1, 0, DataControlRowType.Footer, DataControlRowState.Insert);
            TableCell FooterCell2 = new TableCell();
            FooterCell2.Text = "Maruti Computers P Limited." + "<br /><br />Swaminathan Muthuvelu<br />Managing Director & Authorized Signatory.";
            int colsan = dt.Columns.Count;
            FooterCell2.ColumnSpan = colsan;
            FooterRow.Cells.Add(FooterCell2);
            GridView1.Controls[0].Controls.AddAt(0, FooterRow);

            return GridView1;
        }

        public GridView GridViewMSWordGet(String htmlText)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.Font.Size = 8;

            //GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell2 = new TableCell();
            //DataTable dtCompany = obj.CurrentCompanyInformationGet("");
            //string CompanyName = Convert.ToString(dtCompany.Rows[0][0]);
            //string CompanyAddress = Convert.ToString(dtCompany.Rows[0][1]);
            //string CompanyCity = Convert.ToString(dtCompany.Rows[0][2]);
            //string PinCode = Convert.ToString(dtCompany.Rows[0][3]);
            //byte[] CompanyLogo = (byte[])dtCompany.Rows[0][10];
            //string PhotoString = Convert.ToBase64String(CompanyLogo);

            //DateTimeFormatInfo dinfo1 = new DateTimeFormatInfo();

            //HeaderCell2.Text = CompanyName + "<br />" + CompanyAddress + "<br />" + CompanyCity + " " + PinCode + "<br />" + ReportHeader;
            //int colsan = dt.Columns.Count;
            //HeaderCell2.ColumnSpan = colsan;
            //HeaderRow.Cells.Add(HeaderCell2);

            //  GridView1.DataSource = dt;
            //   GridView1.DataBind();
            GridViewRow tableRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell2 = new TableCell();
            Cell2.Text = htmlText;


            tableRow.Cells.Add(Cell2);
            GridView1.Controls[0].Controls.AddAt(0, tableRow);

            return GridView1;
        }

        public ActionResult DataExportWord(string HtmlText, string FileName, int FontSize)
        {

            /*
              GridView GridView1 = new GridView();
              GridView1.AllowPaging = false;
              GridView1.BorderStyle = BorderStyle.None;
              GridView1.ShowHeader = false;
              GridView1.GridLines = GridLines.None;
              GridView1.ShowFooter = false;
              GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Footer, DataControlRowState.Insert);
              TableCell HeaderCell2 = new TableCell();
              HeaderCell2.BorderStyle = BorderStyle.None;

              DataTable dt = new DataTable();
              dt.Columns.Add("a", typeof(string));

              dt.Rows.Add("");

              HeaderCell2.Text = HtmlText;

              HeaderRow.Cells.Add(HeaderCell2);

              GridView1.DataSource = dt;
              GridView1.DataBind();
              GridView1.Controls[0].Controls.AddAt(0, HeaderRow);





              StringBuilder str=new StringBuilder();


              StringWriter sw = new StringWriter();
              HtmlTextWriter hw = new HtmlTextWriter(sw);

              Response.Clear();
              Response.Buffer = true;
              Response.Charset = "";
              Response.ContentType = "application/vnd.ms-word ";
              Response.AddHeader("content-disposition", "attachment;filename=DhanBank.doc");

              GridView1.RenderControl(hw);

              Response.Output.Write(Convert.ToString(sw));

              Response.Flush();
              Response.End();

             */




            string strBody = string.Empty;
            strBody = @"<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
            "xmlns:w='urn:schemas-microsoft-com:office:word'" +
            "xmlns='http://www.w3.org/TR/REC-html40'>";

            strBody = strBody + "<!--[if gte mso 9]>" +
            "<xml>" +
            "<w:WordDocument>" +
            "<w:View>Print</w:View>" +
            "<w:Zoom>100</w:Zoom>" +
            "</w:WordDocument>" +
            "</xml>" +
            "<![endif]-->";
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.AddHeader("Content-Disposition", "inline;filename=DhanBankStatement.doc");

            StringBuilder htmlCode = new StringBuilder();
            htmlCode.Append("<html>");
            htmlCode.Append("<head>" + strBody + " <style type=\"text/css\">body {font-family:arial Narrow;font-size:14.5;}</style></head>");
            htmlCode.Append("<body>");
            htmlCode.Append(HtmlText);
            htmlCode.Append("</body></html>");
            Response.Output.Write(Convert.ToString(htmlCode));
            Response.Flush();
            Response.End();
            return View();
        }

        public ActionResult DataExportExcel_OLD(GridView GridView1, string FileName, int FontSize)
        {
            GridView1.Font.Size = FontSize;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView dataGridView1 = new GridView();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=D:\\output1.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            int RowCount = GridView1.Rows.Count;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                // GridView1.Rows[i].Attributes.Add("class", "textmode");
                GridView1.Rows[i].Height = 33;
                GridView1.Rows[i].Cells[5].Wrap = false;
                GridView1.Rows[i].Cells[5].Style.Add("mso-number-format", TestFormat(GridView1.Rows[i].Cells[5].Text.Length));
            }
            GridView1.Rows[RowCount - 1].Cells[3].Font.Bold = true;
            GridView1.Rows[RowCount - 1].Cells[4].Font.Bold = true;
            GridView1.RenderControl(hw);
            GridView1.HeaderStyle.Height = 33;
            GridView1.FooterStyle.Height = 33;
            dataGridView1 = GridView1;
            Response.Output.Write(Convert.ToString(sw));
            Response.Flush();
            Response.End();
            //------------------------------------------------
            /*
                  Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                  // creating new WorkBook within Excel application
                  Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                  // creating new Excelsheet in workbook
                  Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                  // see the excel sheet behind the program
                  //app.Visible = true;
                  // get the reference of first sheet. By default its name is Sheet1.
                  // store its reference to worksheet
                  worksheet = workbook.Sheets["Sheet1"];
                  worksheet = workbook.ActiveSheet;
                  // changing the name of active sheet
                  worksheet.Name = "Exported from gridview";                                 
                 // storing header part in Excel
                  //for (int i = 1; i < dataGridView1.Rows[i].Cells.Count + 1; i++)
                  //{
                  //    worksheet.Cells[1, i] = dataGridView1.Rows[i].Cells.
                  //}         
                  // storing Each row and column value to excel sheet
                  for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                  {
                      for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                      {
                          worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Text.ToString();
                      }
                  }                
                   workbook.SaveAs("D:\\output1.xls", Type.Missing,"123" , Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Type.Missing, Type.Missing, Type.Missing, Type.Missing);     
                  // Exit from the application
                 // app.Quit();
                  */
            //-------------------------
            return View();
        }


        public ActionResult DataExportExcel(GridView GridView1, string  password, int FontSize)
        {
            GridView1.Font.Size = FontSize;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView dataGridView1 = new GridView();

            int RowCount = GridView1.Rows.Count;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                // GridView1.Rows[i].Attributes.Add("class", "textmode");
                GridView1.Rows[i].Height = 33;
                GridView1.Rows[i].Cells[5].Wrap = false;
                GridView1.Rows[i].Cells[5].Style.Add("mso-number-format", TestFormat(GridView1.Rows[i].Cells[5].Text.Length));

            }
            GridView1.Rows[RowCount - 1].Cells[3].Font.Bold = true;
            GridView1.Rows[RowCount - 1].Cells[4].Font.Bold = true;
            GridView1.RenderControl(hw);
            GridView1.HeaderStyle.Height = 33;
            GridView1.FooterStyle.Height = 33;

            dataGridView1 = GridView1;
            /*
            string[] filePaths = Directory.GetFiles(Request.PhysicalApplicationPath + "Report\\");
            foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);*/
    
            string strPath = Request.PhysicalApplicationPath + "Report\\BankStatement.xls";
            string strPath2 = Request.PhysicalApplicationPath + "Report\\BankStatementDetails.xls";
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
            string strPath3 = baseUrl + "/Report/BankStatementDetails.xls";

           
         

         if(System.IO.File.Exists(strPath))
         {
             FileStream fS = System.IO.File.Open(strPath, FileMode.Open, FileAccess.Read);
             fS.Close();
             System.IO.File.Delete(strPath);
         }

         if (System.IO.File.Exists(strPath2))
         {
             FileStream fS2 = System.IO.File.Open(strPath2, FileMode.Open, FileAccess.Read);
             fS2.Close();
             System.IO.File.Delete(strPath2);
         }
      
            //string strTextToWrite = TextBox1.Text;
         
            
            FileStream fStream = System.IO.File.Create(strPath);
            fStream.Close();
            StreamWriter sWriter = new StreamWriter(strPath);
            sWriter.Write(sw);
            sWriter.Close();



            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Open(strPath);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Worksheets[1];
            app.DisplayAlerts = false;
            app.StandardFont = "Arial Narrow";
            app.StandardFontSize = 12;
            workbook.SaveAs(strPath2, Type.Missing, password, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
           
            workbook.Close();
            app.Quit();

            //WorkflowViewModel datamodel = new WorkflowViewModel();

            //datamodel.BankList = new List<SelectListItem>();
            //DataTable dtBankList = obj.EmployeeBankGet();
            //datamodel.BankList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            //foreach (DataRow dr in dtBankList.Rows)
            //{
            //    datamodel.BankList.Add(new SelectListItem
            //    {
            //        Text = dr[1].ToString(),
            //        Value = dr[0].ToString()
            //    });
            //}
       

            WebClient req = new WebClient();

            byte[] fileContent = null;
            string contentType = GetContentType(strPath2);
            string filename = "BankStatementdt.xls";



            fileContent = req.DownloadData(strPath2);

           // fileContent = System.IO.File.ReadAllBytes(strPath2);


            return File(fileContent, contentType, filename);
/*
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename=BankStatement.xls");
            byte[] data = req.DownloadData(strPath2);
            Response.BinaryWrite(data);
            Response.End();

            */

           
        }
        public string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                contentType = registryKey.GetValue("Content Type").ToString();
            return contentType;
        }

        public string TestFormat(int strLength)
        {
            string strFormat = string.Empty;
            for (int i = 0; i < strLength; i++)
            {

                strFormat = strFormat + "0";

            }
            return strFormat;
        }

        public ActionResult DataExportNotePadold(GridView GridView1, string FileName)
        {
            string txt = string.Empty;


            //foreach (TableCell cell in GridView1.HeaderRow.Cells)
            //{
            //    txt += cell.Text + "\t\t";

            //}

            //txt += "\r\n";



            foreach (GridViewRow row in GridView1.Rows)
            {

                foreach (TableCell cell in row.Cells)
                {

                    txt += cell.Text + "\t\t";

                }


                txt += "\r\n";

            }

            /*
                        StringBuilder str = new StringBuilder();
                        for (int i = 0; (i <= (ds.Tables[0].Rows.Count - 1)); i++)
                        {
                            for (int j = 0; (j<= (ds.Tables[0].Columns.Count - 1)); j++)
                            {
                                str.Append(ds.Tables[0].Rows[i][j].ToString());
                            }

                            str.Append(("\r" + "\n"));
                        }


            */

            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=bankStatement.txt");

            Response.Charset = "";

            Response.ContentType = "application/text";

            Response.Output.Write(txt);

            Response.Flush();

            Response.End();

            return View();

        }

        //ICICI Notepad
        public ActionResult DataExportNotePad(DataTable dt, string FileName,string Extension,bool CommandType,DateTime? filecreateddate)
        {
        
            string txt = string.Empty;
            System.Web.HttpContext.Current.Application["currentdate1"] = DateTime.Now;
            string datetime = Convert.ToString(System.Web.HttpContext.Current.Application["currentdate1"]);
            if (Convert.ToDateTime(datetime).Date == DateTime.Now.Date && CommandType)
            {
                System.Web.HttpContext.Current.Application["fileno1"] = Convert.ToInt32(System.Web.HttpContext.Current.Application["fileno1"]) + 1;

            }
            else
            {
                System.Web.HttpContext.Current.Application["fileno1"] = Convert.ToInt32(System.Web.HttpContext.Current.Application["fileno1"]);
            }
            DateTime now = DateTime.Now;
            string Month = CommonUtil.getshortmonthname(now.Month);
            string Day = null;
            if(now.Day < 10)
            {
                Day = Convert.ToString("0" + now.Day);
            }
            else
            {

                Day = Convert.ToString(now.Day);
            }
            string year = Convert.ToString(now.Year);
            int fileno1 = Convert.ToInt32(System.Web.HttpContext.Current.Application["fileno1"]);
            string fileformat = "0" + fileno1;
            string datemm = Day + Month+year;
            string filename = FileName +"_" +datemm+"-" + fileformat;

            

            StringBuilder str = new StringBuilder();
            for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
            {
                for (int j = 0; (j <= (dt.Columns.Count - 1)); j++)
                {
                    str.Append(Convert.ToString(dt.Rows[i][j]));
                }

                str.Append(("\r" + "\n"));
            }

            if (!CommandType)
            {
                str.Append("Created Date:" + Convert.ToDateTime(filecreateddate).ToShortDateString());
                str.Append(("\r" + "\n"));
                str.Append("No Of Transactions:" + Convert.ToString(dt.Rows.Count - 6));
            }

            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=" + filename + Extension);

            Response.Charset = "";

            Response.ContentType = "application/text";

            Response.Output.Write(str);

            Response.Flush();

            Response.End();

            return View();

         

        }

        //HDFC NOTEPAD
        public ActionResult DataExportNotePadHDFC(DataTable dt, string FileName, string Extension, bool CommandType,DateTime? filecreateddate)
        {
            string txt = string.Empty;
            System.Web.HttpContext.Current.Application["currentdate"] = DateTime.Now;
            string datetime=Convert.ToString(System.Web.HttpContext.Current.Application["currentdate"]);
            if (Convert.ToDateTime(datetime).Date == DateTime.Now.Date && CommandType)
           {
           System.Web.HttpContext.Current.Application["fileno"] = Convert.ToInt32(System.Web.HttpContext.Current.Application["fileno"]) + 1;

           }
            else
            {
                System.Web.HttpContext.Current.Application["fileno1"] = Convert.ToInt32(System.Web.HttpContext.Current.Application["fileno1"]);
            }
             DateTime now = DateTime.Now;
             string Month = null;
            string Day = null;
            if (now.Month < 10)
            {
                Month = Convert.ToString("0" + now.Month);

            }
            else
            {
                Month = Convert.ToString(now.Month);

            }
            if (now.Day < 10)
            {
                 Day = Convert.ToString("0"+now.Day);
            }
            else
            {

                Day = Convert.ToString(now.Day);
            }
            int fileno = Convert.ToInt32(System.Web.HttpContext.Current.Application["fileno"]);
            string fileformat = ".00" + fileno;
            string datemm=Day+Month;
            string filename = FileName + datemm + fileformat;



            StringBuilder str = new StringBuilder();
            for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
            {
                for (int j = 0; (j <= (dt.Columns.Count - 1)); j++)
                {
                    str.Append(Convert.ToString(dt.Rows[i][j]));
                }

                str.Append(("\r" + "\n"));
            }
             if(!CommandType)
             {
            str.Append("Created Date:" + Convert.ToDateTime(filecreateddate).ToShortDateString());
            str.Append(("\r" + "\n"));
            str.Append("No Of Transactions:" + Convert.ToString(dt.Rows.Count - 6));
}

            Response.Clear();

            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=" + filename + Extension);

            Response.Charset = "";

            Response.ContentType = "application/text";

            Response.Output.Write(str);

            Response.Flush();

            Response.End();

            return View();

        }



        //
        public ActionResult WorkflowProcessDetail()
        {
            WorkflowViewModel model = new WorkflowViewModel();

            if (Session["userName"] == null)
            {
                return Redirect("~/Home/Login");
            }
            try
            {
               

                model.BankList = new List<SelectListItem>();
                DataTable dtBankList = obj.EmployeeBankGet();
                model.BankList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtBankList.Rows)
                {
                    model.BankList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }
                model.MonthList = new List<SelectListItem>();
                DataTable dtMonthList = obj.MonthGet(null);
                model.MonthList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtMonthList.Rows)
                {
                    model.MonthList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                model.YearList = new List<SelectListItem>();
                DataTable dtYearList = obj.YearGet(5, null);
                model.YearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtYearList.Rows)
                {
                    model.YearList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                //-----------------------------------------------



                model.BankAccountList = new List<SelectListItem>();
                DataTable dtBankAccountList = obj.WfBankAccountGet(Convert.ToInt32(Session["company_id"]));
                model.BankAccountList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtBankAccountList.Rows)
                {
                    model.BankAccountList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                //-----------------------

                model.VarMonthList = new List<SelectListItem>();
                DataTable dtVarMonthList = obj.MonthGet(null);
                model.VarMonthList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtVarMonthList.Rows)
                {

                    if (DateTime.Now.Month - 1 == Convert.ToInt32(dr[0]))
                    {

                        model.VarMonthList.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = dr[1].ToString(),

                            Value = dr[0].ToString()
                        });
                    }

                    else
                    {

                        model.VarMonthList.Add(new SelectListItem
                        {
                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                }





                model.VarYearList = new List<SelectListItem>();
                DataTable dtVarYearList = obj.YearGet(5, null);
                model.VarYearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtYearList.Rows)
                {

                    if (DateTime.Now.Year == Convert.ToInt32(dr[0]))
                    {

                        model.VarYearList.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = dr[0].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                    else
                    {
                        model.VarYearList.Add(new SelectListItem
                        {
                            Text = dr[0].ToString(),
                            Value = dr[0].ToString()
                        });

                    }
                }



                model.ProjectList = new List<SelectListItem>();
                DataTable dtProjectList = obj.ProjectGet(true, null);
                model.ProjectList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtProjectList.Rows)
                {
                    model.ProjectList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }



                model.ProcessList = new List<SelectListItem>();
                model.ProcessList.Add(new SelectListItem { Text = "--Select--", Value = "" });

                DataTable dtProcessList = new DataTable();

                DataSet ds = obj.LeftMenuGet(null, null, null);
                if (ds.Tables.Count > 0)
                {
                    dtProcessList = ds.Tables[0];
                    if (dtProcessList.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtProcessList.Rows)
                        {
                            model.ProcessList.Add(new SelectListItem
                            {
                                Text = dr[1].ToString(),
                                Value = dr[2].ToString()
                            });
                        }
                    }
                }

            }
            catch(Exception  ex)
            {

                return View(model);
            }
           
            return View(model);

        }


        [HttpPost]
        public ActionResult LeftMenuGetOld(int? ProcessId,int? VarMonthId,int ? VarYearId)
        {
            string html = "";
             StringBuilder htmlTableWorkflow=new  StringBuilder ();
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = { 0 };

                int[] columnHideMenuText = { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12 };

                DataSet ds = obj.LeftMenuGet(ProcessId, VarMonthId, VarYearId);
                if (ds.Tables.Count > 0)
                {
                    DataTable dtModuleName = ds.Tables[0];
                    if (dtModuleName.Rows.Count > 0)
                    {int? Process_Id = null;
                        string ModuleName = null;
                        Session.Add("dtWorkFlowProcess", dtModuleName);
                        


                        if (dtModuleName.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtModuleName.Rows.Count; i++)

                            {
                                ModuleName = Convert.ToString(dtModuleName.Rows[i][1]);
                                Process_Id = Convert.ToInt16(dtModuleName.Rows[i][2]);
                                DataSet dsMenu = obj.LeftMenuGet(Process_Id,VarMonthId, VarYearId);
                                if (dsMenu.Tables.Count > 0)
                                {
                                    DataTable dtMenuText = dsMenu.Tables[1];


                                    htmlTableWorkflow.Append(CommonUtil.htmlTableMenuText(dtMenuText, columnHideMenuText, ModuleName, Convert.ToString(i)));
                                }




                            }

                        }

                       



                        return Json(new { Flag = 0, Html = 0, strhtmlworkflow = Convert.ToString(htmlTableWorkflow) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult LeftMenuGet(int? ProcessId,int? VarMonthId,int?  VarYearId)
        {
            string html = "";
            StringBuilder htmlTableWorkflow = new StringBuilder();
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

              

                int[] columnHideMenuText = { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12 };


                DataSet dsMenu = obj.LeftMenuGet(ProcessId,null, null);
                                if (dsMenu.Tables.Count > 0)
                                {
                                    DataTable dtMenuText = dsMenu.Tables[1];

                                    if (dtMenuText.Rows.Count > 0)
                                    {
                                        htmlTableWorkflow.Append(CommonUtil.htmlTableLeftMenu(dtMenuText, columnHideMenuText));

                                        return Json(new { Flag = 0, Html = 0, strhtmlworkflow = Convert.ToString(htmlTableWorkflow) }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        html = "No Record !!";
                                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                                    }
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



        public ActionResult WorkflowProcess(WorkflowViewModel model)
        {
            string html = "";
            StringBuilder htmlTable = new StringBuilder();
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide ;

                if (model.ProcessId == 0)
                {
                    columnHide = new int[] { 0,10,11,12 };
                    DataSet DsBoth = obj.PayrollReimbursementWfDetails(null, null, null, null);
                    if (DsBoth.Tables.Count > 0)
                    {
                        if (DsBoth.Tables[0].Rows.Count > 0)
                        {
                            htmlTable = CommonUtil.htmlTableWorkFlowProcess(DsBoth.Tables[0], columnHide);
                            return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            html = "No Record !!";
                            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

                        }
                    }

                }
               
                DataSet dsWfProcess = obj.PayrollWfDetails(model.ProcessId, model.StateId,model.StartDate,model.EndDate,null,null);

                if (dsWfProcess.Tables.Count > 0)
                {
                    DataTable dtWfProcess = dsWfProcess.Tables[0];

                    if (dtWfProcess.Rows.Count > 0)

                       // htmlTableWorkFlowReimbursementProcess
                    {
                          

                       if(model.ProcessId==8)
                       {
                    columnHide = new int[] { 0, 9, 11, 12 };


                    htmlTable = CommonUtil.htmlTableWorkFlowProcess(dtWfProcess, columnHide);
                        }

                       else  if (model.ProcessId == 187)
                       {
                           columnHide = new int[] { 0, 10, 11, 12 };


                           htmlTable = CommonUtil.htmlTableWorkFlowVariableSalaryProcess(dtWfProcess, columnHide);
                       }
                  
                    else
                    {
                    if (model.StateId == 530)
                    {
                        columnHide = new int[] { 0,9,17};
                      
                    }
                    else
                    {
                        columnHide = new int[] { 0, 10, 11, 12,16,17 };
                       
                    }

                    htmlTable = CommonUtil.htmlTableWorkFlowReimbursementProcess(dtWfProcess, columnHide);

                }





                     
                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "No Record !!";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult NextStateGet(WorkflowViewModel model)
        {
            List<SelectListItem> NextStateItemGet = new List<SelectListItem>();
            DataTable dtWfNextStateGet = obj.WfNextstateGet(model.StateId);
            foreach (DataRow dr in dtWfNextStateGet.Rows)
            {
                NextStateItemGet.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            var list = NextStateItemGet;
            return Json(list);
        }

        [HttpPost]
        public ActionResult WorkflowActionProcess(WorkflowViewModel model)
        {
            string html = "";
            try
            {
                int? varSalaryExist = 0;
                string varEmpId = "";
                string SalaryStatus = null;
                string flag = null;
                int? employee_id = null;
                string EmpCode = "";
                string msg = "";
                int? ReimbursementId = null;
                string RequestDate = null;
                string EmpId = "";
                int? month = null;
                int? year = null;
                DateTime? st_date = null;
                DateTime? end_date = null;
                int z = 0;
                int y = 0;
                string checkBoxName = string.Empty;
                string IsCheck = string.Empty;
                string AppKeyList = "";
                int n = 0;
                if (model.WorkflowDataList != null)
                {
                    int count = model.WorkflowDataList.Count;
                    for (int x = 0; x < count; x++)
                    {
                        checkBoxName = "WorkflowDataList[" + x + "].isCheck";
                        IsCheck = Request[checkBoxName];
                        if (IsCheck == "on")
                        {
                            y = y + 1;
                          
                                AppKeyList = Convert.ToString(model.WorkflowDataList[x].AppKey);

                                employee_id = null;

                                month = null;
                                year = null;
                               

                                    EmpCode = model.WorkflowDataList[x].EmpCode;

                                    if (model.ProcessId == 186) //------Reimbursement process
                                    {
                                        if (!(string.IsNullOrEmpty(AppKeyList)))
                                        {

                                            DataTable dtt = obj.ListToTable(AppKeyList, "~");
                                            if (dtt.Rows.Count > 0 && dtt.Rows.Count >1)
                                            {
                                                employee_id = Convert.ToInt32(dtt.Rows[0][1]);
                                                ReimbursementId = Convert.ToInt32(dtt.Rows[2][1]);
                                            }
                                        }

                                        if (model.NextState == 518)
                                        {


                                            RequestDate = obj.EmpReimbursementStatusGet(employee_id, ReimbursementId);

                                            if (!(string.IsNullOrEmpty(RequestDate)))
                                            {
                                                if (model.PayDate < Convert.ToDateTime(RequestDate))
                                                {
                                                    msg = msg + EmpCode + ",";
                                                    html = "EmpCode " + msg + " Pay Date  Should be Greater than or Equal to Request Date !";

                                                }


                                                else
                                                {
                                                    z = z + 1;
                                                    if (model.WorkflowDataList[x].AppKey != null)
                                                    {
                                                        int? status = obj.WfActionProcess(AppKeyList, model.ProcessId, model.NextState);

                                                        if (status == 0)
                                                        {
                                                            int? s = obj.EmpReimbursementPaid(AppKeyList, model.PayDate);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        else if (model.NextState == 519)
                                        {
                                            z = z + 1;
                                            if (model.WorkflowDataList[x].AppKey != null)
                                            {
                                                int? status = obj.WfActionProcess(AppKeyList, model.ProcessId, model.NextState);
                                                if (status == 0)
                                                {
                                                    int? s = obj.EmpReimbursementPaid(AppKeyList, null);
                                                }
                                            }
                                        }


                                        else if (model.NextState == 501)
                                        {
                                            z = z + 1;
                                            int? status = obj.WfEmpReimbursementEntryCreate(employee_id, model.WorkflowDataList[x].Amount, model.WorkflowDataList[x].EmpReimbursementId, model.ProcessId, model.NextState, model.WorkflowDataList[x].AppKey);
                                        }



                                        else
                                        {
                                            z = z + 1;
                                            if (model.WorkflowDataList[x].AppKey != null)
                                            {
                                                int? status = obj.WfActionProcess(AppKeyList, model.ProcessId, model.NextState);
                                            }

                                        }


















                                    }
                                    else if(model.ProcessId == 187)
                                    {


                                        if (model.WorkflowDataList[x].AppKey != null)

                                        {

                                            z = z + 1;
                                        //    if (model.NextState == 544)
                                        //    {
                                        //        int? status = obj.EmpVariableSalaryEntryCreate(model.WorkflowDataList[x].AppKey, null, model.WorkflowDataList[x].Amount, model.VarMonthId, model.VarYearId, model.ProcessId, model.NextState);
                                            
                                        //    }
                                           // else
                                         //   {
                                                int? status = obj.WfActionProcess(AppKeyList, model.ProcessId, model.NextState);
                                            //}
                                        }

                                    }

                                    else 
                                    {




                                        if (model.WorkflowDataList[x].AppKey != null)
                                        {
                                            //-----------payroll process------
                                            DataTable dt = obj.ListToTable(AppKeyList, "~");

                                            employee_id = Convert.ToInt32(dt.Rows[0][1]);
                                            month = Convert.ToInt32(dt.Rows[2][1]);
                                            year = Convert.ToInt32(dt.Rows[3][1]);

                                            if(model.NextState==12)
                                            {
                                                
                                                if (model.PayDate != null && model.BankAccountId != null)

                                                {
                                                    int? mesage = obj.BankParameterTempTableCreate(model.PayDate, model.BankAccountId);                                                    
                                                }
                                                else if(model.PayDate ==null && model.BankAccountId != null)
                                                {
                                                    int? mesage = obj.BankParameterTempTableCreate(null, model.BankAccountId);
                                                }
                                                else if (model.PayDate != null && model.BankAccountId == null)
                                                {
                                                    int? mesage = obj.BankParameterTempTableCreate(model.PayDate,null);
                                                }
                                                else if (model.PayDate == null && model.BankAccountId == null)
                                                {
                                                    int? mesage = obj.BankParameterTempTableCreate(null, null);
                                                }

                                                int? status = obj.WfActionProcess(AppKeyList, model.ProcessId, model.NextState);

                                            }


                                            if (model.NextState == 41 || model.NextState ==14 || model.NextState==15)
                                            {

                                              varSalaryExist = obj.WfEmpVariableSalaryExistsValidate(employee_id, month, year);

                                                if(varSalaryExist==1)
                                                {
                                                    varEmpId = varEmpId + EmpCode + ",";

                                                }

                                            }
                                            DataTable dtDate = obj.PayrollStartEndDays(month, year);
                                            if (dtDate.Rows.Count > 0)
                                            {
                                                st_date = Convert.ToDateTime(dtDate.Rows[0][0]);
                                                end_date = Convert.ToDateTime(dtDate.Rows[0][1]);
                                            }
                                            DataTable dtSt = obj.EmpSalaryStatusGet(null, employee_id, null, null, null, st_date, end_date, null);
                                            if (dtSt.Rows.Count > 0)
                                            {
                                                SalaryStatus = Convert.ToString(dtSt.Rows[0][8]);
                                                flag = Convert.ToString(dtSt.Rows[0][9]);
                                            }

                                            if (flag == "0")
                                            {
                                                if (varSalaryExist == 0)
                                                {

                                                    z = z + 1;
                                                    int? status1 = obj.WfActionProcess(AppKeyList, model.ProcessId, model.NextState);
                                                if (model.NextState == 3)
                                                    {
                                                        if (status1 == 0)
                                                        {
                                                            int? status = obj.EmpVariableSalaryEntryCreate(null, employee_id, null, month, year, model.ProcessId, null);
                                                        }
                                                    }

                                                if (model.NextState == 41 || model.NextState == 14 || model.NextState == 15)
                                                {
                                                    if (status1 == 0)
                                                    {
                                                        int? status = obj.VariablePayDisapprove(model.WorkflowDataList[x].AppKey);
                                                    }
                                                }


                                                }
                                            }
                                            else
                                            {
                                                EmpId = EmpId + Convert.ToString(employee_id) + ",";

                                            }



                                        }





                                        /*
                                        if (n == 0)
                                        {
                                            AppKeyList = Convert.ToString(model.WorkflowDataList[x].AppKey).Trim();
                                        }
                                        else
                                        {
                                            AppKeyList += "," + Convert.ToString(model.WorkflowDataList[x].AppKey).Trim();
                                        }
                                     */
                                        n = n + 1;

                                    }
                            
                        }
                    }

                }
                /*
                int[] columnHide = { 0, 9, 11, 12 };

                DataSet dsWfProcess = obj.PayrollWfDetails(model.ProcessId, model.StateId);
                StringBuilder htmlTableWorkflow=new StringBuilder();

                 int[] columnHideMenuText = { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12 };

               
                       





                if (dsWfProcess.Tables.Count > 0)
                {
                    DataTable dtWfProcess = dsWfProcess.Tables[0];

                    if (dtWfProcess.Rows.Count > 0)
                    {
                        StringBuilder htmlTable = CommonUtil.htmlTableWorkFlowProcess(dtWfProcess, columnHide);
                        if (!(string.IsNullOrEmpty(EmpId)))
                        {
                            string msg = "<div class='alert alert-danger'> Employee " + EmpId + " have some issue in salary, so can not process !!</div>";
                            htmlTable.Append(msg);
                        }

                        return Json(new { Flag = 0, Html = Convert.ToString(htmlTable),htmlTableWorkflow=htmlTableWorkflow }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 0, Html = html,htmlTableWorkflow=htmlTableWorkflow }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 0, Html = html, htmlTableWorkflow = htmlTableWorkflow }, JsonRequestBehavior.AllowGet);

                }
                */

                int[] columnHideMenuText = { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12 };

                StringBuilder htmlTableWorkflow = new StringBuilder();
                DataSet dsMenu = obj.LeftMenuGet(model.ProcessId, null, null);
                if (dsMenu.Tables.Count > 0)
                {
                    DataTable dtMenuText = dsMenu.Tables[1];


                    htmlTableWorkflow.Append(CommonUtil.htmlTableLeftMenu(dtMenuText, columnHideMenuText));




                    int Flag = 0;
                    if (model.ProcessId == 186 && model.NextState == 518) //------Reimbursement process
                    {
                        if (!(string.IsNullOrEmpty(html)))
                        {
                            Flag = 5;
                        }
                        if (z == 0)
                        {
                            Flag = 6;

                        }
                    }
                    if (!(string.IsNullOrEmpty(varEmpId)))
                    {
                        html = html + ",(" + varEmpId + " Employee Variable Salary Already Processed.)";
                    }
                    if (z == 0)
                    {
                        Flag = 6;

                    }
                    return Json(new { Flag = Flag, Html = html, strhtmlworkflow = Convert.ToString(htmlTableWorkflow) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    html = "No Record !!";
                    return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                }




            }

            catch (Exception ex)
            {
                html = Convert.ToString(ex.Message);
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public ActionResult WorkFlowPayrollDetail(string App_Key)
        {
            string html = "";

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] HideColumn = new[] { 0, 1, 5, 6, 10, 14, 15, 16 };

                int? employee_id = null;

                int? month = null;
                int? year = null;

                DataTable dt = obj.ListToTable(App_Key, "~");
                if (dt.Rows.Count > 3)
                {
                    employee_id = Convert.ToInt32(dt.Rows[0][1]);
                    month = Convert.ToInt32(dt.Rows[2][1]);
                    year = Convert.ToInt32(dt.Rows[3][1]);
                    DataSet dsPayroll = obj.EmpPayrollDetailsGet(month, year, "1", employee_id, null, null, null, null, null, null, null, null);
                    if (dsPayroll.Tables.Count > 0)
                    {
                        if (dsPayroll.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTablePayrollDetailsWorkFlow(dsPayroll.Tables[0], HideColumn);
                            return Json(new { Flag = 0, Html = htmlTable.ToString() }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                        }

                    }
                }

                html = "<div class='alert alert-danger'>No Record !!</div>";
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult EmpPayrollItemDetailGet(int? PayrollId)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }
                int[] columnHide = new[] { 0 };
                DataTable dtEmpSalaryItem = obj.EmpPayrollsalaryItemDetailGet(PayrollId);
                if (dtEmpSalaryItem.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(dtEmpSalaryItem, columnHide);
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
        public ActionResult WorkFlowLogDetail(string App_Key)
        {
            string html = "";
            int? process_id = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] HideColumn = new[] { 0 };

              

                DataTable dt = obj.ListToTable(App_Key, "~");

               if(dt.Rows.Count>3)
               {
                   process_id = 8;
               }

               else
               {
                   process_id = 186;
               }
               DataTable DtLog = obj.WfWorkflowLogGet(process_id, App_Key, null, null, null);

                if (DtLog.Rows.Count > 0)
                {
                    StringBuilder htmlTable = CommonUtil.htmlTable(DtLog, HideColumn);
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
        public ActionResult WorkFlowAttachmentGet(string App_Key)
        {
            string html = "";

            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] HideColumn = new[] { 0 };

                DataTable dt = obj.ListToTable(App_Key, "~");

                if (dt.Rows.Count > 0)
                {
                    int? month = null;
                    int? year = null; 
                    int? employee_id = Convert.ToInt32(dt.Rows[0][1]);
                    if (dt.Rows.Count > 3)
                    {
                      
                       month = Convert.ToInt32(dt.Rows[2][1]);
                      year = Convert.ToInt32(dt.Rows[3][1]);
                    }
                    DataTable dtAttachmentGet = obj.WfAttendanceAttachmentGet(employee_id, month, year, App_Key);
                    Session["snAttendanceAttachmentGet"] = dtAttachmentGet;
                    if (dtAttachmentGet.Rows.Count > 0)
                    {
                        string strImageName = Convert.ToString(dtAttachmentGet.Rows[0][0]);
                        string strImageByte = Convert.ToString(dtAttachmentGet.Rows[0][1]);

                        if (!(string.IsNullOrEmpty(strImageName) && string.IsNullOrEmpty(strImageByte)))
                        {
                           

                            StringBuilder htmlTable = CommonUtil.htmlTableImageGet(dtAttachmentGet);
                            return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            html = "<div class='alert alert-danger'>Document Not Available !!</div>";
                            return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>Document Not Available !!</div>";
                        return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult NotesCreate(WorkflowViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = new[] { 0 };
                int? NotesId = null;

                model.notesText = model.hfNotesText;
                model.notesList = model.hfNotesList;

                DataTable dtFnwfAppState = obj.WfAppStateGet(null, null, model.AppKey);

                if (dtFnwfAppState.Rows.Count > 0)
                {
                    string pid = Convert.ToString(dtFnwfAppState.Rows[0]["process_id"]);
                    if (!string.IsNullOrEmpty(pid))
                        model.ProcessId = Convert.ToInt32(dtFnwfAppState.Rows[0]["process_id"]);
                    else
                        return Json(new { Flag = 1, Html = "Invalid Process Id" }, JsonRequestBehavior.AllowGet);


                    string sid = Convert.ToString(dtFnwfAppState.Rows[0]["current_state_id"]);
                    if (!string.IsNullOrEmpty(sid))
                        model.StateId = Convert.ToInt32(dtFnwfAppState.Rows[0]["current_state_id"]);
                    else
                        return Json(new { Flag = 1, Html = "Invalid State Id" }, JsonRequestBehavior.AllowGet);

                }

                DataSet dsSpIWfNotes = obj.SpiWfNotesGet(model.ProcessId, model.AppKey, model.StateId, model.notesText, null);
                if (dsSpIWfNotes.Tables.Count > 0)
                {
                    DataTable dt = dsSpIWfNotes.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string strNotesId = Convert.ToString(dt.Rows[0][0]);
                        if (!string.IsNullOrEmpty(strNotesId))
                            NotesId = Convert.ToInt32(strNotesId);
                        else
                        {
                            html = "<div class='alert alert-danger'>Invalid Notes Id !!</div>";
                            return Json(new { Flag = 1, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>No Record !!</div>";
                        return Json(new { Flag = 1, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(new { Flag = 1, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                }

                if (!string.IsNullOrEmpty(model.imageBase64String))
                {
                    string[] base64 = model.imageBase64String.Split(',');
                    model.imageByte = Convert.FromBase64String(base64[1]);
                }

                int? status = obj.WfNotesCreate(NotesId, 1, model.imageName, model.imageByte);

                if (status == 0)
                {
                    DataTable dtFnwfApp = obj.WfAppGet(model.ProcessId, model.AppKey, null);
                    if (dtFnwfApp.Rows.Count > 0)
                    {
                        string strAppNotesId = Convert.ToString(dtFnwfApp.Rows[0]["app_notes_id"]);
                        if (!string.IsNullOrEmpty(strAppNotesId))
                        {
                            int AppNotesId = Convert.ToInt32(strAppNotesId);
                            DataTable dtNotesGet = obj.WfNotesGet(model.ProcessId, AppNotesId);
                            if (dtNotesGet.Rows.Count > 0)
                            {
                                StringBuilder htmlTable = CommonUtil.htmlTableNotesGet(dtNotesGet, columnHide);
                                return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                html = "<div class='alert alert-danger'>No Record !!</div>";
                                return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            html = "<div class='alert alert-danger'>Invalid App Notes Id !!</div>";
                            return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>App Notes Id Not Exist !!</div>";
                        return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>Record Not Insert !!</div>";
                    return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public ActionResult NotesGet(WorkflowViewModel model)
        {
            string html = null;
            try
            {
                if (Session["userName"] == null)
                {
                    return Redirect("~/Home/Login");
                }

                int[] columnHide = new[] { 0 };

                model.notesText = model.hfNotesText;
                model.notesList = model.hfNotesList;

                DataTable dtFnwfAppState = obj.WfAppStateGet(null, null, model.AppKey);

                if (dtFnwfAppState.Rows.Count > 0)
                {
                    string pid = Convert.ToString(dtFnwfAppState.Rows[0]["process_id"]);
                    if (!string.IsNullOrEmpty(pid))
                        model.ProcessId = Convert.ToInt32(dtFnwfAppState.Rows[0]["process_id"]);
                    else
                        return Json(new { Flag = 1, Html = "Invalid Process Id" }, JsonRequestBehavior.AllowGet);


                    string sid = Convert.ToString(dtFnwfAppState.Rows[0]["current_state_id"]);
                    if (!string.IsNullOrEmpty(sid))
                        model.StateId = Convert.ToInt32(dtFnwfAppState.Rows[0]["current_state_id"]);
                    else
                        return Json(new { Flag = 1, Html = "Invalid State Id" }, JsonRequestBehavior.AllowGet);

                }

                DataTable dtFnwfApp = obj.WfAppGet(model.ProcessId, model.AppKey, null);
                if (dtFnwfApp.Rows.Count > 0)
                {
                    string strAppNotesId = Convert.ToString(dtFnwfApp.Rows[0]["app_notes_id"]);
                    if (!string.IsNullOrEmpty(strAppNotesId))
                    {
                        int AppNotesId = Convert.ToInt32(strAppNotesId);
                        DataTable dtNotesGet = obj.WfNotesGet(model.ProcessId, AppNotesId);
                        if (dtNotesGet.Rows.Count > 0)
                        {
                            StringBuilder htmlTable = CommonUtil.htmlTableNotesGet(dtNotesGet, columnHide);
                            return Json(new { Flag = 0, Html = Convert.ToString(htmlTable) }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            html = "<div class='alert alert-danger'>No Record !!</div>";
                            return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        html = "<div class='alert alert-danger'>Invalid App Notes Id !!</div>";
                        return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    html = "<div class='alert alert-danger'>App Notes Id Not Exist !!</div>";
                    return Json(new { Flag = 0, Html = Convert.ToString(html) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = ex.Message.ToString();
                return Json(new { Flag = 1, Html = html }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult NotesDownloadGet(WorkflowViewModel model)
        {
            byte[] fileContent = null;
            string contentType = "";
            string filename = "";
            try
            {
                DataTable dtAttachment = obj.WfNotesDownloadGet(model.AttachmentId);
                if (dtAttachment.Rows.Count > 0)
                {
                    DataRow dr = dtAttachment.Rows[0];
                    filename = dr[0].ToString();
                    fileContent = (byte[])dr[1];
                    contentType = "image/png";
                }

                return File(fileContent, contentType, filename);
            }
            catch (Exception ex)
            {
                return File(fileContent, contentType, filename);
            }
        }
        
        
        [HttpPost]
        public ActionResult AttendanceDownloadGet()
        {
            string html = "";
            byte[] fileContent = null;
            string contentType = "";
            string filename = "";
            try
            {
                DataTable dtAttendanceAttachmentGet = (DataTable)Session["snAttendanceAttachmentGet"];
                if (dtAttendanceAttachmentGet.Rows.Count > 0)
                {
                    DataRow dr = dtAttendanceAttachmentGet.Rows[0];
                    filename = dr[0].ToString();
                    fileContent = (byte[])dr[1];
                    contentType = "image/png";
                    return File(fileContent, contentType, filename);
                }
                else
                {
                    html = "<div class='alert alert-danger'>No Record !!</div>";
                    return Json(html, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                html = Convert.ToString(ex.Message);
                return Json(html, JsonRequestBehavior.AllowGet);
            }
        }
        //filename = Convert.ToString(Session["ImageName"]);
        //fileContent = (byte[])Session["ImageByte"];
        //contentType = "image/png";
        
        [HttpPost]
        public ActionResult ExcelDownloadGet()
        {
            
            byte[] fileContent = null;
            string contentType = "";
            string filename = "";
            try
            {
                string strPath2 = Request.PhysicalApplicationPath + "Report\\BankStatementDetails.xls";
                byte[] data = System.IO.File.ReadAllBytes(strPath2);
                filename = "BankStatementNew.xls";
                fileContent = data;
                contentType = "application/vnd.ms-excel";

                return File(fileContent, contentType, filename);
            }
            catch (Exception ex)
            {
                return File(fileContent, contentType, filename);
            }
        }
    }
}
