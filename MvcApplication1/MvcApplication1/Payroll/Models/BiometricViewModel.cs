using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Models
{
    public class BiometricViewModel : PayrollUtil
    {

    }

    public class BiometricUserViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? BmUserId { get; set; }

        [Required(ErrorMessage = "Employee is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "User Name is Required.")]
        public string ParentUserId { get; set; }

        public Int32? GroupId { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Notes { get; set; }


        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public List<SelectListItem> GroupList { get; set; }

        public BiometricUserViewModel()
        {

            try
            {
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
            }
            catch (Exception Ex)
            {

            }
        }

    }

    public class BiometricGroupViewModel : PayrollUtil
    {

        public Int32? GroupId { get; set; }

        [Required(ErrorMessage = "Group Name  is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Group Name ")]
        public string GroupName { get; set; }

        public Int32? DesignationId { get; set; }

        [Required(ErrorMessage = "Group Description  is Required.")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Description")]
        public string GroupDescription { get; set; }

    }

    public class BMDailyAttendanceEntryViewModel : PayrollUtil
    {

        public DateTime? AttendanceDate { get; set; }

        public List<BMDailyAttendanceEntryViewModel> EmployeeDataList { get; set; }

        public Boolean isCheck { get; set; }

        public Int32? EmpDailyAttendanceId { get; set; }

        public Int32? BMUserId { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? Start_Date { get; set; }
        public decimal? Overtime { get; set; }

        public TimeSpan? InTime { get; set; }

        public TimeSpan? OutTime { get; set; }
            [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Remarks")]
        public string Remarks { get; set; }
        public string Status { get; set; }

    }

    public class BiometricLogSummeryViewModel : PayrollUtil
    {

    }

    public class EmployeeShiftModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public List<ImportExportExcelViewModel> EmployeeDataList { get; set; }

        public HttpPostedFileBase filename { get; set; }

        public Int32? EmployeeId { get; set; }

        public Int32? EmpTypeId { get; set; }

        public Int32? DepartmentId { get; set; }

        public Int32? LocationId { get; set; }

        public Int32? DesginationId { get; set; }

        public string Workunit { get; set; }

        [Required(ErrorMessage = "Month is Required.")]
        public Int32? MonthId { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public Int32? Year { get; set; }

        public Int32? ProjectId { get; set; }

        public Int32? AttendanceSourceId { get; set; }

        public decimal? WorkqtyActual { get; set; }

        public decimal? OvertimeActual { get; set; }

        public decimal? LocalDay { get; set; }

        public decimal? NonLocalDay { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public string ExcelDropDown { get; set; }

        public List<PayrollUtil> EmpDataList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> EmployeeLeftList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesginationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> AttendanceSourceList { get; set; }

        public EmployeeShiftModel()
        {
            try
            {
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
                DataTable dtDepartmentList = obj.DepartmentGet(true,null);
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

                ProjectList = new List<SelectListItem>();
                DataTable dtProjectList = obj.ProjectGet(true,null);
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

                    if (Convert.ToInt32(dr[0]) == 2)
                    {
                        AttendanceSourceList.Add(new SelectListItem
                        {

                            Selected = true,
                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }

                    else
                    {
                        AttendanceSourceList.Add(new SelectListItem
                        {
                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }



    }

    public class EmployeeShiftModel1
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public List<ImportExportExcelViewModel> EmployeeDataList { get; set; }

        public HttpPostedFileBase filename { get; set; }

        public Int32? EmployeeId { get; set; }

        public Int32? EmpTypeId { get; set; }

        public Int32? DepartmentId { get; set; }

        public Int32? LocationId { get; set; }

        public Int32? DesginationId { get; set; }

        public string Workunit { get; set; }

        [Required(ErrorMessage = "Month is Required.")]
        public Int32? MonthId { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public Int32? Year { get; set; }

        public Int32? ProjectId { get; set; }

        public Int32? AttendanceSourceId { get; set; }

        public decimal? WorkqtyActual { get; set; }

        public decimal? OvertimeActual { get; set; }

        public decimal? LocalDay { get; set; }

        public decimal? NonLocalDay { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public string ExcelDropDown { get; set; }

        public List<PayrollUtil> EmpDataList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> EmployeeLeftList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesginationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> AttendanceSourceList { get; set; }

        public EmployeeShiftModel1()
        {
            try
            {
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

                    if (Convert.ToInt32(dr[0]) == 2)
                    {
                        AttendanceSourceList.Add(new SelectListItem
                        {

                            Selected = true,
                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }

                    else
                    {
                        AttendanceSourceList.Add(new SelectListItem
                        {
                            Text = dr[1].ToString(),
                            Value = dr[0].ToString()
                        });
                    }
                }
            }
            catch (Exception Ex)
            {

            }
        }



    }
}