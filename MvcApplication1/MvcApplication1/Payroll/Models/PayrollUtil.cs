using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Web.Mvc;
namespace PoiseERP.Areas.Payroll.Models
{
    public class PayrollUtil
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }
        public Int32? ShiftId { get; set; }
        public string EmpCode { get; set; }
        public Int32? EmployeeId { get; set; }
        public decimal? ItemAmount { get; set; }
        public Boolean isRowCheck { get; set; }
        public Int32? EmpTypeId { get; set; }
        public Int32? DepartmentId { get; set; }
        public Int32? LocationId { get; set; }
        public Int32? DesginationId { get; set; }
        [Required(ErrorMessage = "Leave Type is Required.")]
        public Int32? LeaveType_Id { get; set; }

        public Int32? PayrollTypeId { get; set; }
        [Required(ErrorMessage = "Payroll Item is Required.")]
        public Int32? PayrollItemId { get; set; }
        [Required(ErrorMessage = "Payroll Item is Required.")]
        public Int32? PayrollArrearItemId { get; set; }

        [Required(ErrorMessage = "Month is Required.")]
        public Int32? MonthId { get; set; }

        [Required(ErrorMessage = "Month is Required.")]
        public Int32? ArearMonthId { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public Int32? Year { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public Int32? ArearYear { get; set; }
        public decimal? LeaveCredit { get; set; }
        public decimal? LeaveDebit { get; set; }
        public Int32? FinancialYear { get; set; }
        public Int32? ProjectId { get; set; }
        public Int32? AttendanceSourceId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string EarningDeduction { get; set; }
        public string SalaryItemType { get; set; }
        public int? PayrollPaymentMethod { get; set; }

        public string Notes { get; set; }

        //Attendance Upload Properties



        public List<PayrollUtil> EmpDataList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> EmployeeLeftList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesginationList { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> LocationList { get; set; }

        public List<SelectListItem> LeaveTypeList { get; set; }
        //public List<SelectListItem> EmpLeaveTypeList { get; set; }
        public List<SelectListItem> PayrollArearItemList { get; set; }

        public List<SelectListItem> PayrollItemList { get; set; }

        public List<SelectListItem> PayrollTypeList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }



        public List<SelectListItem> ArearMonthList { get; set; }
        public List<SelectListItem> ArearYearList { get; set; }


        public List<SelectListItem> FinacialYearList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> AttendanceSourceList { get; set; }

        public List<SelectListItem> UserList { get; set; }
        public List<SelectListItem> GroupList { get; set; }

        public List<SelectListItem> DocumentCategoryList { get; set; }
        public List<SelectListItem> ShiftList { get; set; }


        public List<SelectListItem> PayrollPaymentMethodList { get; set; }

        public int? ListValue { get; set; }
        public string ListText { get; set; }
        public string Is_Check { get; set; }
        public List<PayrollUtil> SelectList { get; set; }

        public Int32? Quarter { get; set; }

        public int? CompanyId { get; set; }
        public int? EmpCategoryId { get; set; }
        public string PerformanceType { get; set; }

        public List<SelectListItem> EmpCategoryList { get; set; }

        public List<SelectListItem> PerformanceTypeList { get; set; }
        public string BonusType { get; set; }
        public List<SelectListItem> BonusTypeList { get; set; }
        public List<SelectListItem> QuarterList { get; set; }

        public PayrollUtil()
        {

            try
            {
                PayrollItemList = new List<SelectListItem>();
                DataTable dtPayrollItemList = obj.PayrollitemTaxitemGet();
                PayrollItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollItemList.Rows)
                {
                    PayrollItemList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                PayrollArearItemList = new List<SelectListItem>();
                DataTable dtArearPayrollItemList = obj.PayrollArearItemGet();
                PayrollArearItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtArearPayrollItemList.Rows)
                {
                    PayrollArearItemList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                EmployeeList = new List<SelectListItem>();
                DataTable dtEmployeeList = obj.EmployeeInfoGet();
                EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeeList.Rows)
                {
                    EmployeeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                CompanyList = new List<SelectListItem>();
                DataTable dtcompanylist = obj.CompanyGet(null);
                CompanyList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtcompanylist.Rows)
                {
                    CompanyList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }
                EmployeeLeftList = new List<SelectListItem>();
                DataTable dtEmployeeLeftList = obj.EmployeeInfoLeftGet();
                EmployeeLeftList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeeLeftList.Rows)
                {
                    EmployeeLeftList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                DepartmentList = new List<SelectListItem>();
                DataTable dtDepartmentList = obj.DepartmentGet(true, null);
                DepartmentList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDepartmentList.Rows)
                {
                    DepartmentList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                DesginationList = new List<SelectListItem>();
                DataTable dtDesginationList = obj.DesignationGet(true);
                DesginationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDesginationList.Rows)
                {
                    DesginationList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                EmployeeTypeList = new List<SelectListItem>();
                DataTable dtEmployeeTypeList = obj.EmpTypeGet(null);
                EmployeeTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeeTypeList.Rows)
                {
                    EmployeeTypeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                LocationList = new List<SelectListItem>();
                DataTable dtLocationList = obj.LocationGet();
                LocationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLocationList.Rows)
                {
                    LocationList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }




                LeaveTypeList = new List<SelectListItem>();
                DataTable dtLeaveTypeList = obj.LeaveTypeGet();
                LeaveTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLeaveTypeList.Rows)
                {
                    LeaveTypeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                //EmpLeaveTypeList = new List<SelectListItem>();
                //DataTable dtEmpLeaveTypeList = obj.EmpLeaveTypeGet();
                //LeaveTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                //foreach (DataRow dr in dtLeaveTypeList.Rows)
                //{
                //    LeaveTypeList.Add(new SelectListItem
                //    {
                //        Text = dr[2].ToString(),
                //        Value = dr[0].ToString()
                //    });
                //}

                ShiftList = new List<SelectListItem>();
                DataTable dtShiftList = obj.ShiftGet();
                ShiftList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtShiftList.Rows)
                {
                    ShiftList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                PayrollTypeList = new List<SelectListItem>();
                DataTable dtPayrollTypeList = obj.PayrollTypeGet();
                PayrollTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollTypeList.Rows)
                {
                    PayrollTypeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                MonthList = new List<SelectListItem>();
                DataTable dtMonthList = obj.MonthGet(null);
                MonthList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtMonthList.Rows)
                {


                    if (DateTime.Now.Month - 1 == Convert.ToInt32(dr[0]))
                    {

                        MonthList.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }

                    else
                    {


                        MonthList.Add(new SelectListItem
                        {

                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                }



                YearList = new List<SelectListItem>();
                DataTable dtYearList = obj.YearGet(5, null);
                YearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtYearList.Rows)
                {


                    if (DateTime.Now.Year == Convert.ToInt32(dr[0]))
                    {

                        YearList.Add(new SelectListItem
                        {
                            Selected = true,
                            Text = dr[0].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                    else
                    {
                        YearList.Add(new SelectListItem
                        {
                            Text = dr[0].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                }




                ArearMonthList = new List<SelectListItem>();
                DataTable dtArearMonthList = obj.MonthGet(null);
                ArearMonthList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtArearMonthList.Rows)
                {
                    ArearMonthList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }



                ArearYearList = new List<SelectListItem>();
                DataTable dtArearYearList = obj.YearGet(5, null);
                ArearYearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtArearYearList.Rows)
                {
                    ArearYearList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });
                }







                ProjectList = new List<SelectListItem>();
                DataTable dtProjectList = obj.ProjectGet(true, null);
                ProjectList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtProjectList.Rows)
                {
                    ProjectList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                AttendanceSourceList = new List<SelectListItem>();
                DataTable dtAttendanceSourceList = obj.AttendanceSourceGet();
                AttendanceSourceList.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                foreach (DataRow dr in dtAttendanceSourceList.Rows)
                {
                   // -MCL-----
                    //if (Convert.ToInt32(dr[0]) == 1)
                    //{
                    //    AttendanceSourceList.Add(new SelectListItem
                    //    {

                    //        Selected = true,
                    //        Text = dr[1].ToString(),
                    //        Value = dr[0].ToString()
                    //    });
                    //}

                    //else
                    //{
                    AttendanceSourceList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                   }
               // }

                //---------------------
                UserList = new List<SelectListItem>();
                DataTable dtUserList = obj.ParentUserGet();
                UserList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtUserList.Rows)
                {
                    UserList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                GroupList = new List<SelectListItem>();
                DataTable dtGroupList = obj.BmGroupsGet();
                GroupList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtGroupList.Rows)
                {
                    GroupList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                DocumentCategoryList = new List<SelectListItem>();
                DataTable dtDocumentCategoryList = obj.DocumentCategoryGet();
                DocumentCategoryList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDocumentCategoryList.Rows)
                {
                    DocumentCategoryList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                PayrollPaymentMethodList = new List<SelectListItem>();
                DataTable dtPayrollPaymentMethodList = obj.PayrollPaymentMethodGet();
                PayrollPaymentMethodList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollPaymentMethodList.Rows)
                {
                    PayrollPaymentMethodList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }
                //------------------



                EmpCategoryList = new List<SelectListItem>();
                DataTable DtCategory = obj.EmpCategoryGet();
                EmpCategoryList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in DtCategory.Rows)
                {
                    EmpCategoryList.Add(new SelectListItem
                    {
                        Text = Convert.ToString(dr[1]),
                        Value = Convert.ToString(dr[0])
                    });
                }

                PerformanceTypeList = new List<SelectListItem>();

                PerformanceTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });

                PerformanceTypeList.Add(new SelectListItem { Text = "Monthly", Value = "Monthly" });
                PerformanceTypeList.Add(new SelectListItem { Text = "Quarterly", Value = "Quarterly" });
                PerformanceTypeList.Add(new SelectListItem { Text = "Yearly", Value = "Yearly" });

                QuarterList = new List<SelectListItem>();

                QuarterList.Add(new SelectListItem { Text = "--Select--", Value = "" });

                QuarterList.Add(new SelectListItem { Text = "Quarter1", Value = "1" });
                QuarterList.Add(new SelectListItem { Text = "Quarter2", Value = "2" });
                QuarterList.Add(new SelectListItem { Text = "Quarter3", Value = "3" });
                QuarterList.Add(new SelectListItem { Text = "Quarter4", Value = "4" });


                BonusTypeList = new List<SelectListItem>();

                BonusTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });

                BonusTypeList.Add(new SelectListItem { Text = "Individual", Value = "Individual" });
                BonusTypeList.Add(new SelectListItem { Text = "Unit", Value = "Unit" });
                BonusTypeList.Add(new SelectListItem { Text = "Organization", Value = "Organization" });


            }
            catch (Exception Ex)
            {

            }
        }





        //------------------  CheckBox in DropDownList -------------------------
        public static StringBuilder DropDownListWithCheckBox(DataTable datatable)
        {
            try
            {
                int rowId = 0;
                string txtName = string.Empty;
                string rowData = null;

                StringBuilder html = new StringBuilder();
                int i = 0;
                html.Append("<ul class='nav nav-pills' style='width:200px;'>  <li class='dropdown' style='width:200px;'>  <a href='#' data-toggle='dropdown' class='dropdown form-control'>--Select--<b class='caret'></b></a>  <ul class='dropdown-menu'>");
                if (datatable.Rows.Count > 0)
                {
                    html.Append("<li ><input type='checkbox' onchange='checkBoxChangeDropDown()' class='CheckBoxList' id='CheckedAllCheckBox' name='CheckedAllCheckBox' />&nbsp;&nbsp;");
                    html.Append("<input type='hidden' id='hfAll' name='hfAll' value='Select All' />Select All </li>");
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                /* <ul class="nav nav-pills" >

                            <li class="dropdown">
                              <a href="#" data-toggle="dropdown" class="dropdown-toggle">--Select--<b class="caret"></b></a>
                               <ul class="dropdown-menu">
                                  <li style="width:200px;"><input type="checkbox"/>hello</li>
                                 <li style="width:200px;"><input type="checkbox"/><input type="text"  />hello</li>

                           </ul> </li>                   
                   </ul>*/

                                rowData = cell.ToString();
                                txtName = "SelectList[" + rowId + "].Is_Check";
                                if (rowId == 0)
                                {
                                    html.Append("<li ><input type='checkbox' checked='checked' class='CheckBoxList' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");
                                }
                                else
                                {
                                    html.Append("<li ><input type='checkbox'  class='CheckBoxList' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");
                                }

                                txtName = "SelectList[" + rowId + "].ListValue";
                                html.Append("<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />");

                            }
                            else
                            {
                                if (i == 1)
                                {
                                    rowData = cell.ToString();
                                    txtName = "SelectList[" + rowId + "].ListText";
                                    html.Append("&nbsp;<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />" + rowData + "</li>");

                                }
                                else
                                {

                                }
                            }
                            i += 1;
                        }
                        rowId = rowId + 1;

                    }

                }

                html.Append("</ul></li></ul>");
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





        //------------------  CheckBox in DropDownList -------------------------
        public static StringBuilder EmployeeDropDownListWithCheckBox(DataTable datatable)
        {
            try
            {
                int rowId = 0;
                string txtName = string.Empty;
                string rowData = null;

                StringBuilder html = new StringBuilder();
                int i = 0;
                html.Append("<ul class='nav nav-pills' style='width:200px;'>  <li class='dropdown' style='width:200px;'>  <a href='#' data-toggle='dropdown' class='dropdown form-control'>--Select--<b class='caret'></b></a>  <ul style='max-height: 500px; overflow: auto;' class='dropdown-menu'>");
                if (datatable.Rows.Count > 0)
                {
                    html.Append("<li ><input type='checkbox' onchange='checkBoxChangeDropDown()' class='CheckBoxList' id='CheckedAllCheckBox' name='CheckedAllCheckBox' />&nbsp;&nbsp;");
                    html.Append("<input type='hidden' id='hfAll' name='hfAll' value='Select All' />Select All </li>");
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                /* <ul class="nav nav-pills" >

                            <li class="dropdown">
                              <a href="#" data-toggle="dropdown" class="dropdown-toggle">--Select--<b class="caret"></b></a>
                               <ul class="dropdown-menu">
                                  <li style="width:200px;"><input type="checkbox"/>hello</li>
                                 <li style="width:200px;"><input type="checkbox"/><input type="text"  />hello</li>

                           </ul> </li>                   
                   </ul>*/

                                rowData = cell.ToString();
                                txtName = "SelectList[" + rowId + "].Is_Check";
                                if (rowId == 0)
                                {
                                    html.Append("<li ><input type='checkbox' class='CheckBoxList' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");
                                }
                                else
                                {
                                    html.Append("<li ><input type='checkbox'  class='CheckBoxList' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");
                                }

                                txtName = "SelectList[" + rowId + "].ListValue";
                                html.Append("<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />");

                            }
                            else
                            {
                                if (i == 1)
                                {
                                    rowData = cell.ToString();
                                    txtName = "SelectList[" + rowId + "].ListText";
                                    html.Append("&nbsp;<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />" + rowData + "</li>");

                                }
                                else
                                {

                                }
                            }
                            i += 1;
                        }
                        rowId = rowId + 1;

                    }

                }

                html.Append("</ul></li></ul>");
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }







    }
}