using MCLSystem;
//---Default Name spaces---
using System;
using System.Collections.Generic;
//---Added namespaces--
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;




namespace PoiseERP.Areas.Payroll.Models
{

    public class LeaveViewModel
    {
    }


    public class EmpLeaveTypeViewModel 
    {
        public Int32? EmpLeaveTypeId { get; set; }
        [Required(ErrorMessage = "Leave  Type is required.")]
        public Int32? LeaveTypeId { get; set; }
        //   [Required(ErrorMessage = "Year is required.")]
        public Int32? LeaveTypeYear { get; set; }

           public List<SelectListItem> YearList { get; set; }
        public Int32? EmployeeId { get; set; }
     [Required(ErrorMessage = "Start Date is required.")]
        public DateTime? StartDt { get; set; }

     //   [Required(ErrorMessage = "End Date is required.")]
        public DateTime? EndDt { get; set; }


        [Required(ErrorMessage = "Leave Request Type is required.")]
        public string LeaveRequestType { get; set; }


        [Range(0, 31, ErrorMessage = "Value should be 1 to 31 .")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Number .")]
        public Int32? MaxDaysMonth { get; set; }

        [Range(0, 365, ErrorMessage = "Value should be 1 to 365 .")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Number .")]
        public Int32? MaxDaysYear { get; set; }

        public Int32? AccrualPeriod { get; set; }

        [Required(ErrorMessage = "AccrualPeriod Begin is required.")]
        [Range(1, 12, ErrorMessage = "Value should be 1 to 12 .")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Number .")]
        public Int32? AccrualPeriodBegin { get; set; }
         [Required(ErrorMessage = "AccrualPeriod Count is required.")]
        [Range(1, 12, ErrorMessage = "Value should be 1 to 12 .")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Number .")]
        public byte? AccrualPeriodCount { get; set; }


        public Int16? LeaveUnit { get; set; }
        [Required(ErrorMessage = "Leave Per AccrualPeriod is required.")]
        [Range(1, 365, ErrorMessage = "Value should be 1 to 365 .")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Number .")]

        public Int32? LeavePerAccrualPeriod { get; set; }

        public Int32? LeavePerAccrualPeriodFn { get; set; }


        [Range(0, 365.00, ErrorMessage = "Value should be 0 to 365 .")]

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        public decimal? MinAccruableLeave { get; set; }

        [Range(0, 365.00, ErrorMessage = "Value should be 0 to 365 .")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        public decimal? MaxAccruableLeave { get; set; }

        public Int32? AccrualPoolLeaveTypeId { get; set; }

        public Int32? MaxAgeInMonths { get; set; }


      //  [Required(ErrorMessage = "Pay Rate Fraction is required.")]
        [Range(1.0, 100.00, ErrorMessage = "Value should be 1 to 100 .")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        public decimal? PayRateFraction { get; set; }

        public Int32? PayRateFn { get; set; }

        public bool? LeaveEncashable { get; set; }

        public bool? AutoEncash { get; set; }



        public decimal? EncashablePayRate { get; set; }

        public Int32? EncashablePayRateFn { get; set; }

        [Range(0, 365.00, ErrorMessage = "Value should be 0 to 365 .")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        public decimal? MinLeaveUsage { get; set; }

        [Range(0, 365.00, ErrorMessage = "Value should be 0 to 365 .")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        public decimal? MaxLeaveUsage { get; set; }

        public string LeaveUsageRestrictionPeriod { get; set; }

        public string LeavePoolForm { get; set; }

        public bool? EarnedLeave { get; set; }

        public bool? LeaveUsagerestriction { get; set; }

        public bool IsCalculateFromDoj { get; set; }

        public bool IsIncludeHoilday { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public Int32? EmpTypeId { get; set; }
        public Int32? DepartmentId { get; set; }
        public Int32? LocationId { get; set; }
        public Int32? DesginationId { get; set; }
        public Int32? ProjectId { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesginationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }
                 public List<SelectListItem> MonthList { get; set; }
        
        public  EmpLeaveTypeViewModel()
        {
            PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
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



            YearList = new List<SelectListItem>();
            DataTable dtYearList = obj.YearGet(5, null);
            YearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtYearList.Rows)
            {

/*
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
                {*/
                    YearList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });
                //}
            }



            MonthList = new List<SelectListItem>();
            DataTable dtMonthList = obj.MonthGet(null);
            MonthList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtMonthList.Rows)
            {                   
                   MonthList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
               
            }




        }



















    }

    public class EmpLeaveRequestViewModel: EmployeeLeaveTransactionViewModel1
    {
        public Int32? EmpLeaveRequestId { get; set; }
        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }
        [Required(ErrorMessage = "Apply Date is Required.")]
        public Int32? EmpLeaveTransactionId { get; set; }

        public Int32? LeaveTypeId { get; set; }
        [Required(ErrorMessage = "Leave Type is Required.")]
        public Int32? EmpLeaveTypeId { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Leave Type Name ")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string LeaveTypeName { get; set; }
        public string LeaveTransactionType { get; set; }

        public bool isHalfDay { get; set; }

     //   [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Employee Name ")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Apply Date is Required.")]
        public DateTime? EmpLeaveTxnDate { get; set; }

        public DateTime? EmpLeaveTxnDates { get; set; }

        public DateTime? ApplyDate { get; set; }

        [Required(ErrorMessage = "Leave Start Date is Required.")]
        public DateTime? StartDt { get; set; }

        public DateTime? StartDts { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Leave End Date is Required.")]
        public DateTime? EndDt { get; set; }

        public DateTime? EndDts { get; set; }

        //[Required(ErrorMessage = "Requested Days is Required.")]
        public decimal? RequestedDays { get; set; }

        public decimal? RequestedDayss { get; set; }

        public string LeaveStatus { get; set; }
        [Required(ErrorMessage = "Description is Required.")]
        //[RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\?\/\\ ]*)$", ErrorMessage = "Invalid Description")]
        public string LeaveDescription { get; set; }
        //[RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\?\/\\ ]*)$", ErrorMessage = "Invalid Description")]
        public string LeaveDescriptions { get; set; }

        public string EmpLeaveReason { get; set; }

        public string PayPeriodCycle { get; set; }

        public Int32? PayPeriod { get; set; }

        public Int32? PayYear { get; set; }

        public decimal? LeaveCredit { get; set; }
        [Range(0, 365.00, ErrorMessage = "Value should be 0 to 31 .")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
          [Required(ErrorMessage = "Loss of Pay  days  are Required.")]
        public decimal? PayDeductDays { get; set; }
        [Range(0, 365.00, ErrorMessage = "Value should be 0 to 31 .")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
          [Required(ErrorMessage = "Leave debit days are Required.")]
        public decimal? LeaveDebitDays { get; set; }
        public decimal? HoliDays { get; set; }
        public decimal? AvailAbleCreditDays { get; set; }
        public decimal? LeaveDebit { get; set; }

        public string Resonreject { get; set; }
        public Int32? PayrollId { get; set; }

        public Int32? CycleId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))+", ErrorMessage = "Enter Correct Email Address")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        public string To { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))+", ErrorMessage = "Enter Correct Email Address")]
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Cc { get; set; }


        public DateTime? EmpLeaveExpiryDate { get; set; }

        public EmpLeaveRequestViewModel()
        {
            PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

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





        }


    }

    //public class ClsDateRange
    //{
    //    public string DateRange { get; set; }
    //    public string DateRangeValue { get; set; }
    //}

    public class LeaveCycleViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public List<LeaveCycleViewModel> LeaveCyclingList { get; set; }
        public Int32? LeaveCycleId { get; set; }
        public Int32? EmployeeId { get; set; }
        public Int32? PayrollId { get; set; }
        public string LeaveCycleType { get; set; }
        public Int32? LeaveTypeId { get; set; }
        public DateTime? LeaveCycleDate { get; set; }
        public Int32? AccrualPeriod { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? StartdDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DateRange { get; set; }
        public List<SelectListItem> DateRangeList { get; set; }
        public LeaveCycleViewModel()
        {
            DateRangeList = new List<SelectListItem>();
            DataTable dtDateRangeList = obj.DateRange();
            DateRangeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtDateRangeList.Rows)
            {
                DateRangeList.Add(new SelectListItem
                {
                    Text = dr[0].ToString(),
                    Value = dr[1].ToString()
                });
            }
        }

    }

    public class EmployeeLeaveTransactionViewModel1
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public int? eltEmpLeaveTransactionId { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? eltLeaveTypeId { get; set; }
        public string eltLeaveDescription { get; set; }
        public string eltLeaveTransactionType { get; set; }
        //public int? EmployeeId { get; set; }
        public string eltEmpName { get; set; }
        public DateTime? eltEmpLeaveTransactionDate { get; set; }
        public decimal? eltLeaveCredit { get; set; }
        public decimal? eltLeaveDebit { get; set; }
        public decimal? eltLeaveBalance { get; set; }
        public string eltPayPeriodCycle { get; set; }
        public int? eltPayPeriod { get; set; }
        public int? eltPayYear { get; set; }
        public string eltEmpLeaveReason { get; set; }
        public int? eltPayrollId { get; set; }

        //[Required]
        public DateTime? eltStartDate { get; set; }

        //[Required]
        public DateTime? eltEndDate { get; set; }
        public List<SelectListItem> eltLeaveTypeList { get; set; }
        public EmployeeLeaveTransactionViewModel1()
        {
            eltLeaveTypeList = new List<SelectListItem>();
            DataTable dtLeaveTypeList1 = obj.LeaveTypeGet();
            eltLeaveTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtLeaveTypeList1.Rows)
            {
                eltLeaveTypeList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }

    }

    public class LeaveCycleVoidViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public List<LeaveCycleVoidViewModel> LeaveCyclingList { get; set; }

        public List<LeaveCycleVoidViewModel> empleavetxnList { get; set; }

        public int? LeaveCycleId { get; set; }
        public int? LeaveTypeId { get; set; }
        public string LeaveDescription { get; set; }
        public string CycleType { get; set; }
        public DateTime? LeaveCycleDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? EmpLeaveTxnId { get; set; }
        public string LeaveTransactionType { get; set; }
        public int? EmployeeId { get; set; }
        public string EmpName { get; set; }
        public DateTime? EmpLeaveTxnDate { get; set; }
        public string EmpLeaveReason { get; set; }
        public string PayPeriodCycle { get; set; }
        public string PayPeriod { get; set; }
        public string PayYear { get; set; }
        public decimal? LeaveCredit { get; set; }
        public decimal? LeaveDebit { get; set; }
        public int? PayrollId { get; set; }
        public int? CycleId { get; set; }
    }

    public class EmployeeLeaveTransactionViewModel
    {
        public int? EmpLeaveTransactionId { get; set; }
        public int? LeaveTypeId { get; set; }
        public string LeaveDescription { get; set; }
        public string LeaveTransactionType { get; set; }
        public int? EmployeeId { get; set; }
        public string EmpName { get; set; }
        public DateTime? EmpLeaveTransactionDate { get; set; }
        public decimal? LeaveCredit { get; set; }
        public decimal? LeaveDebit { get; set; }
        public decimal? LeaveBalance { get; set; }
        public string PayPeriodCycle { get; set; }
        public int? PayPeriod { get; set; }
        public int? PayYear { get; set; }
        public string EmpLeaveReason { get; set; }
        public int? PayrollId { get; set; }

    }

    public class EmployeeLeaveLedgerViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? LeaveTypeId { get; set; }

        [Required(ErrorMessage = "Employee is Required.")]
        public int? EmployeeId { get; set; }

        public int? MonthId { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public int? YearId { get; set; }

        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }

        public EmployeeLeaveLedgerViewModel()
        {
            MonthList = new List<SelectListItem>();
            DataTable dtMonthList = obj.MonthGet(null);
            MonthList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtMonthList.Rows)
            {
                MonthList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            YearList = new List<SelectListItem>();
            DataTable dtYearList = obj.YearGet(5, null);
            YearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtYearList.Rows)
            {
                YearList.Add(new SelectListItem
                {
                    Text = dr[0].ToString(),
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
        }
    }

}