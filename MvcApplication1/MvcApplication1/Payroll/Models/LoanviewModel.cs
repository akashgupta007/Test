using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;


namespace PoiseERP.Areas.Payroll.Models
{

    public class LoanviewModel
    {

    }

    public class LoanTypeViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
       // //[Required(ErrorMessage = "Month is Required")]
       // public int? MonthId { get; set; }

       //// [Required(ErrorMessage = "Year is Required.")]
       // public int? YearId { get; set; }

     

        public Int32? LoanId { get; set; }

        [Required(ErrorMessage = "Loan Name  is Required.")]

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Loan Name ")]
        public string LoanName { get; set; }

        [Required(ErrorMessage = "Loan Code  is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Description")]
        public string LoanDescription { get; set; }

        [Required(ErrorMessage = "Minimum Loan Amount  is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? MinLoanAmount { get; set; }

        [Required(ErrorMessage = "Maximum Loan Amount  is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? MaxLoanAmount { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 100.00, ErrorMessage = "value should be 1 to 100.")]
        public decimal? DefaultInterestPct { get; set; }

        [Required(ErrorMessage = "Loan Term is Required.")]

        [Range(1, 500, ErrorMessage = " Loan Term   is  not Correct")]
        public Int32? DefaultLoanTerm { get; set; }
        [Range(1, 100, ErrorMessage = "value should be 1 to 100.")]

        public Int32? HolidayMonths { get; set; }

        public bool SelectHolidayMonth { get; set; }

        public bool IsActive { get; set; }
       
        //public LoanTypeViewModel()
        //{
            
        //}
    }

    public class EmpLoanViewmodel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        //[Required(ErrorMessage = "Month is Required")]
        public int? MonthId { get; set; }

        // [Required(ErrorMessage = "Year is Required.")]
        public int? YearId { get; set; }
        public Int32? EmpLoanId { get; set; }

        [Required(ErrorMessage = "Loan Name  is Required.")]
        public Int32? LoanId { get; set; }

        [Required(ErrorMessage = "Employee Name  is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Loan Date is Required.")]
        public DateTime? LoanDate { get; set; }

        [Required(ErrorMessage = " Loan Amount is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public string ComputingItemMonthList { get; set; }
        public string ComputingItemYearList { get; set; }
        public decimal? LoanAmount { get; set; }

        [Required(ErrorMessage = "Loan Term is Required.")]
        public Int32? LoanTerm { get; set; }

        public Int32? hfLoanTerm { get; set; }

        public Int32? HolidayMonths { get; set; }

        public Int32? hfHolidayMonths { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 100.00, ErrorMessage = "value should be 0 to 100 ")]

        public decimal? InterestPct { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? AddlPrincipalPerMonth { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? ExpensePerMonth { get; set; }

        public Int32? TaxableFunction { get; set; }

        public Int32? PaymentTransactionId { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public bool IsApproved { get; set; }

        public string Is_Approved { get; set; }

        public Int32? HolidayPeriod { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> Loanlist { get; set; }

        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }

        public EmpLoanViewmodel()
        {

            EmployeeList = new List<SelectListItem>();
            DataTable dt = obj.EmployeeInfoGet();
            EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeList.Add(new SelectListItem
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
                //if (DateTime.Now.Month - 1 == Convert.ToInt32(dr[0]))
                //{
                //    MonthList.Add(new SelectListItem
                //    {
                //        Selected = true,
                //        Text = dr[1].ToString(),
                //        Value = dr[0].ToString()
                //    });
                //}
                //else
                //{
                //    MonthList.Add(new SelectListItem
                //    {

                //        Text = dr[1].ToString(),
                //        Value = dr[0].ToString()
                //    });
                //}
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
                //if (DateTime.Now.Year == Convert.ToInt32(dr[0]))
                //{

                //    YearList.Add(new SelectListItem
                //    {
                //        Selected = true,
                //        Text = dr[0].ToString(),
                //        Value = dr[0].ToString()
                //    });
                //}
                //else
                //{
                //    YearList.Add(new SelectListItem
                //    {
                //        Text = dr[0].ToString(),
                //        Value = dr[0].ToString()
                //    });
                //}
                YearList.Add(new SelectListItem
                {
                    Text = dr[0].ToString(),
                    Value = dr[0].ToString()
                });
            }
            Loanlist = new List<SelectListItem>();
            DataTable dtLoanlist = obj.LoantypeGet(true);
            Loanlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtLoanlist.Rows)
            {
                Loanlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

        }

    }

    public class EmpLoanDetailViewmodel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? LoanId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public Int32? EmployeeId { get; set; }


        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> Loanlist { get; set; }


        public EmpLoanDetailViewmodel()
        {

            EmployeeList = new List<SelectListItem>();
            DataTable dt = obj.EmployeeInfoGet();
            EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


            Loanlist = new List<SelectListItem>();
            DataTable dtLoanlist = obj.LoantypeGet(null);
            Loanlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtLoanlist.Rows)
            {
                Loanlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

        }

    }

}