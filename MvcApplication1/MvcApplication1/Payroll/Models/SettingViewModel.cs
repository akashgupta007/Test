using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Models
{

    public class SettingViewModel
    {

    }

    public class CalenderDayTypeViewModel
    {

        public Int32? DayTypeId { get; set; }

        [Required(ErrorMessage = "Day Type is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid  Day Type Name ")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string DayType { get; set; }

        public bool Overrides { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public decimal? AmountType { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 1000.00, ErrorMessage = "value should be 1 to 1000 .")]
        public decimal? Value { get; set; }

    }

    public class CompanyCalendarViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? ShiftId { get; set; }
        public Int32? CalendarTypeId { get; set; }

        [Required(ErrorMessage = "Day Type is Required.")]

        public Int32? DayTypeId { get; set; }

        public string DayorDateType { get; set; }

        public Int32? EmpTypeId { get; set; }

        public Int32? LocationId { get; set; }
        [Required(ErrorMessage = "Year is Required.")]
        public Int32? CalendarYear { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Notes { get; set; }

        public bool IsWorkDay { get; set; }

        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Decription  is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid  Decription")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string CalendarTypeDesc { get; set; }

        public Int32? CalendarDaysId { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string WorkDayNotes { get; set; }

        [Required(ErrorMessage = "Day is Required.")]
        public string WorkDay { get; set; }

        [Required(ErrorMessage = "Date is Required.")]
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? WorkDate { get; set; }

        public bool IsDay { get; set; }
        public bool IsDaTe { get; set; }
        public List<SelectListItem> ShiftList { get; set; }

        public List<SelectListItem> DayTypeList { get; set; }
        public List<SelectListItem> Employeelist { get; set; }
        public List<SelectListItem> EmpTypeList { get; set; }
        public List<SelectListItem> Location { get; set; }
        public List<SelectListItem> YearList { get; set; }

        public CompanyCalendarViewModel()
        {
            try
            {
                DayTypeList = new List<SelectListItem>();
                // DataTable dtMonthGet = obj.MonthGet(null);
                DataTable dtDayType = obj.DayTypeGet();
                DayTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDayType.Rows)
                {
                    DayTypeList.Add(new SelectListItem
                    {

                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                Employeelist = new List<SelectListItem>();
                DataTable dtEmployeelist = obj.EmployeeInfoGet();
                Employeelist.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeelist.Rows)
                {
                    Employeelist.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                EmpTypeList = new List<SelectListItem>();
                DataTable dtEmpTypeList = obj.EmpTypeGet(null);
                EmpTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmpTypeList.Rows)
                {
                    EmpTypeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                Location = new List<SelectListItem>();
                DataTable dtLocation = obj.LocationGet();
                Location.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLocation.Rows)
                {
                    Location.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }
                YearList = new List<SelectListItem>();
                DataTable dtYearList = obj.YearGet(3, 1);
                // FinancialYearGet(5);
                YearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtYearList.Rows)
                {
                    YearList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });
                }
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



            }
            catch (Exception Ex)
            {

            }
        }


    }

    public class BmRuleViewModel
    {
        //(0[1-9]|1[0-2]):([0-5][0-9]:[0-5][0-9]|(59|44|29):60)

        public Int32? RuleId { get; set; }

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Rule Name")]

        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string RuleName { get; set; }

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Rule Description")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string RuleDescription { get; set; }

        public Int32? RuleDetailId { get; set; }

        [Required(ErrorMessage = "Description is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Description")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string Description { get; set; }

        //Start Date for Child Grid
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-3]):([0-5][0-9]:[0-5][0-9]*)$", ErrorMessage = "Invalid Time Format")]
        [Required(ErrorMessage = "Start Time is Required.")]
        public TimeSpan? MinTimeLateEntryAllowance { get; set; }

        //End Date for Child Grid
        [Required(ErrorMessage = "End Time is Required.")]
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-3]):([0-5][0-9]:[0-5][0-9]*)$", ErrorMessage = "Invalid Time Format")]
        public TimeSpan? MaxTimeLateEntryAllowance { get; set; }

        public Int32? DeductionType { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? DeductionValue { get; set; }

        //Allow Days in Month
        [Range(1, 31, ErrorMessage = "Enter 1 - 31.")]
        public Int32? AllowedLateEntryDays { get; set; }

        public bool IsActive { get; set; }

    }

    public class WorkdayTimeViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? WorkdayTimeId { get; set; }

        [Required(ErrorMessage = "Work Day Description is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Work Day Description")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string WorkDayDescription { get; set; }

        [Required(ErrorMessage = "Work Day Ours  is Required.")]
        public TimeSpan? WorkDayHours { get; set; }

        [Required(ErrorMessage = "In Time is Required.")]
        public TimeSpan? InTime { get; set; }

        [Required(ErrorMessage = "Out Time is Required.")]
        public TimeSpan? OutTime { get; set; }

        public TimeSpan? HalfDayWorkHours { get; set; }

        public Int32? FunctionId { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDt { get; set; }

        public DateTime? EndDt { get; set; }

        [Required(ErrorMessage = "Calendar Type is Required.")]
        public Int32? CalendarTypeId { get; set; }

        public Int32? RuleId { get; set; }

        public List<SelectListItem> Function { get; set; }
        public List<SelectListItem> CalendarType { get; set; }
        public List<SelectListItem> Rule { get; set; }

        public WorkdayTimeViewModel()
        {
            try
            {
                Function = new List<SelectListItem>();
                // DataTable dt = obj
                Function.Add(new SelectListItem { Text = "--Select--", Value = "" });
                //foreach (DataRow dr in dt.Rows)
                //{
                //    Function.Add(new SelectListItem
                //    {
                //        Text = dr[1].ToString(),
                //        Value = dr[0].ToString()
                //    });
                //}

                CalendarType = new List<SelectListItem>();

                int year = DateTime.Now.Year;

                DataTable dtCalendarTypelist = obj.CompanyCalendarTypeGet(year, "Day");
                CalendarType.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtCalendarTypelist.Rows)
                {
                    CalendarType.Add(new SelectListItem
                    {
                        Text = dr[5].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                Rule = new List<SelectListItem>();
                DataTable dtrulelist = obj.BmRuleTypeGet();
                Rule.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtrulelist.Rows)
                {
                    Rule.Add(new SelectListItem
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

    public class DocumentCategoryViewModel
    {
        public Int32? DocumentCategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Category Name")]
        public string DocumentCategoryName { get; set; }

        public string DocumentCategoryDescription { get; set; }
    }

    public class DocumentViewModel : PayrollUtil
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? DocumentId { get; set; }

        //[Required(ErrorMessage = "Document Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Document Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string DocumentName { get; set; }

        public Int32? DocumentCategoryId { get; set; }

        //[Required(ErrorMessage = "Document Date is Required.")]
        public DateTime? DocumentDate { get; set; }

        //[Required(ErrorMessage = "Document Expire Date is Required.")]
        public DateTime? DocumentExpiryDate { get; set; }

        public bool IsForEmpPortal { get; set; }

        public Int32? UserType { get; set; }

        public Int32? UserId { get; set; }

        public Int32? DocumentObjectId { get; set; }

        //public Int32? DocumentIdItem { get; set; }
        [Required(ErrorMessage = "Document Name is Required.")]
        public string DocumentObjectName { get; set; }

        public string DocumentObjectType { get; set; }

        public byte[] DocumentObject { get; set; }
        public string imagePath { get; set; }

        [Required(ErrorMessage = "Upload File is Required.")]
        public string Path { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }

    }

    public class ImportExportExcelViewModel
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
        public string Notes { get; set; }

        public string ExcelDropDown { get; set; }

        public int? CompanyId { get; set; }

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
        public List<SelectListItem> CompanyList { get; set; }

        public ImportExportExcelViewModel()
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

                CompanyList = new List<SelectListItem>();
                DataTable dtCompanyList = obj.CompanyGet(null);
                CompanyList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtCompanyList.Rows)
                {

                    CompanyList.Add(new SelectListItem
                    {
                        Selected = true,
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

    public class MailSetupViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public int? MailSetupId { get; set; }

        [Required(ErrorMessage = "Company is Required.")]
        public int? CompanyId { get; set; }

        public int? LocationId { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Server is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string SmtpServer { get; set; }

        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string SmtpEmailAccount { get; set; }


        [Required(ErrorMessage = "Mail is Required.")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        //[RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Enter Correct Email Address")]
        public string SenderEmail { get; set; }

        [StringLength(100, ErrorMessage = "Max length is 100.")]
        [Required(ErrorMessage = "Password is Required.")]
        public string SenderPassword { get; set; }

        [Required(ErrorMessage = "Port Address is Required.")]
        public int? PortAddress { get; set; }

        [StringLength(100, ErrorMessage = "Max length is 100.")]
        public string Subject { get; set; }

        public string BodyContent { get; set; }

        public bool UseSsl { get; set; }

        public bool IsCommonMail { get; set; }

        public string ToMailId { get; set; }

        public string CcMailId { get; set; }

        public List<SelectListItem> CompanyList { get; set; }
        public List<SelectListItem> LocationList { get; set; }

        public MailSetupViewModel()
        {
            try
            {

                CompanyList = new List<SelectListItem>();
                DataTable dtCompanyList = obj.CompanyGet(null);
                CompanyList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtCompanyList.Rows)
                {
                    CompanyList.Add(new SelectListItem
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

            }
            catch (Exception ex)
            { }
        }


    }
}