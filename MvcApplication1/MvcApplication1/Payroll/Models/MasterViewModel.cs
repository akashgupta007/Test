using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
namespace PoiseERP.Areas.Payroll.Models
{
    public class MasterViewModel
    {
    }

    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {

        private const string _defaultErrorMessage = "'{0}' must be greater than '{1}'";
        private string _basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object

            try
            {
                var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);



                //Get Value of the property  
                var startDate = (DateTime?)basePropertyInfo.GetValue(validationContext.ObjectInstance);


                var thisDate = (DateTime?)value;

                //Actual comparision  
                if (thisDate <= startDate)
                {
                    var message = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(message);
                }

                //Default return - This means there were no validation error  
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class DepartmentViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is Required.")]
        [Display(Name = "Department Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Department Name")]
        public string DepartmentName { get; set; }

        public Int32? ParentDepartmentId { get; set; }

        public Int32? ParentDepartment_Id { get; set; }

        public string ParentDepartmentName { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDt { get; set; }
        public DateTime? StartDt2 { get; set; }

        public DateTime? EndDt { get; set; }
        public DateTime? EndDt2 { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }

        public DepartmentViewModel()
        {
            try
            {
                DepartmentList = new List<SelectListItem>();
                DataTable dtDepartment = obj.DepartmentGet(true, null);
                DepartmentList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDepartment.Rows)
                {
                    DepartmentList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

            }
            catch (Exception ex)
            {

            }
        }

    }

    public class DesignationViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? DesignationId { get; set; }

        [Required(ErrorMessage = "Designation is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Designation Name")]
        public string DesignationDesc { get; set; }

        public bool? Inactive { get; set; }

        public string StatusName { get; set; }

        public Int32? ParentDesgId { get; set; }

        public bool? IsLeaveApproval { get; set; }

        public bool? IsSeeOtherAttendance { get; set; }
        public bool? IsSeeEmpDetails{ get; set; }

        public bool? IsSeeEmpPayslip { get; set; }

        public int? PayScalId { get; set; }

        public List<SelectListItem> PayScalList { get; set; }

        public DesignationViewModel()
        {

            PayScalList = new List<SelectListItem>();

            DataTable dtPayScalList = obj.PayScale6pcGet();
            PayScalList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtPayScalList.Rows)
            {
                PayScalList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }


    }

    public class ProjectViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? ProjectId { get; set; }


        [Required(ErrorMessage = "Project Name is Required.")]
        [Display(Name = "Project Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Project Name")]
        public string ProjectName { get; set; }

        public Int32? ParentProjectId { get; set; }

        public Int32? ParentProject_Id { get; set; }

        public string ParentProjectName { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDt { get; set; }
        public DateTime? StartDt2 { get; set; }

        public DateTime? EndDt { get; set; }
        public DateTime? EndDt2 { get; set; }

        public bool Active { get; set; }

        public List<SelectListItem> ProjectList { get; set; }

        public ProjectViewModel()
        {
            try
            {
                ProjectList = new List<SelectListItem>();
                DataTable dtProject = obj.ProjectGet(true, null);
                ProjectList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtProject.Rows)
                {
                    ProjectList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

            }
            catch (Exception ex)
            {

            }
        }


    }

    public class EmpTypeViewModel
    {

        public Int32? EmpTypeId { get; set; }
        public Int32? AttendanceSourceId { get; set; }
        [Required(ErrorMessage = "Employee Type is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Emp Type Name")]
        public string EmpTypeDesc { get; set; }
        [Required(ErrorMessage = "Attendance Type is Required.")]
        public string WorkUnit { get; set; }
         [Required(ErrorMessage = "Work unit is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? WorkUnitValue { get; set; }
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Notes { get; set; }
        public List<SelectListItem> AttendanceSourceList { get; set; }

        public List<SelectListItem> WorkUnitList { get; set; }
            

       


    }

    public class EmployeeBankViewModel
    {
        public Int32? EmpBankId { get; set; }

        [Required(ErrorMessage = "Bank Name is Required.")]
        [Display(Name = "Bank Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Bank Name")]
        public string EmpBankName { get; set; }

        [Required(ErrorMessage = "Branch Name is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Branch Name")]
        public string EmpBranchName { get; set; }

        [Required(ErrorMessage = "Bank Code is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([A-Za-z0-9]*)$", ErrorMessage = "Invalid Bank Code")]
        public string EmpBankCode { get; set; }

        [Required(ErrorMessage = "IFSC Code is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([A-Za-z0-9]*)$", ErrorMessage = "Invalid Bank Code")]
        public string BankIfscCode { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([A-Za-z0-9]*)$", ErrorMessage = "Invalid Bank Code")]
        public string BankMicrCode { get; set; }
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string EmpBankAddress { get; set; }

        //[ValidationAttribute(DataType.PostalCode, "Please enter valid pincode")]
        //[DataType(DataType.PostalCode, ErrorMessage="Please enter valid Pincode")]       

        //[PostalCode("Please enter valid Pincode")]
        //[DataType(DataType.PostalCode)]

        [RegularExpression(@"^\d{6}(-\d{5})?$", ErrorMessage = " PIN code must be 6 digits length")]
        [Display(Name = "BankPinCode")]
        public string BankPinCode { get; set; }


        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Bank Account")]
        public string BankAccountNo { get; set; }


        public string CustomerID { get; set; }
    }
    public class BankFileformatViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? BankFileFormatId { get; set; }

        public Int32? BankId { get; set; }

        public string Suffix { get; set; }

        public string Prefix { get; set; }

        public bool Date { get; set; }

        public Int32? MonthtypeID { get; set; }

        public bool IsSequence { get; set; }
        public List<SelectListItem> BankList { get; set; }
        public List<SelectListItem> MonthTypeList { get; set; }


        public BankFileformatViewModel()
        {
            BankList = new List<SelectListItem>();
            DataTable dtBankList = obj.EmployeeBankGet();
            BankList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtBankList.Rows)
            {
                BankList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            MonthTypeList = new List<SelectListItem>();
           
            MonthTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            MonthTypeList.Add(new SelectListItem { Text = "Numeral", Value = "1" });
            MonthTypeList.Add(new SelectListItem { Text = "Character", Value = "2" });

          
            
        }
    }
    public class EmpSalaryGroupViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpSalaryGroupId { get; set; }
        public Int32? FunctionId { get; set; }
        [Required(ErrorMessage = "Group Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Group Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string EmpSalaryGroupName { get; set; }

        public bool? IsFreeze { get; set; }
        public Int32? EmpSalaryGroupDetailId { get; set; }

        [Required(ErrorMessage = "Payroll Value Type is Required.")]
        public Int32? Payroll_ValueType { get; set; }
        public Int32? PayrollValueType { get; set; }
        [Required(ErrorMessage = "Payroll Item is Required.")]
        public Int32? PayrollItemId { get; set; }
        public Int32? PayrollItem_Id { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 100.00, ErrorMessage = "value should be 1 to 100 .")]
        public decimal? PayPercent { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Amount { get; set; }

        public bool? IsComputedItem { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDt { get; set; }

        public DateTime? EndDt { get; set; }
        public List<SelectListItem> PayrollItemList { get; set; }
        public List<SelectListItem> FunctionList { get; set; }
        public EmpSalaryGroupViewModel()
        {
            PayrollItemList = new List<SelectListItem>();
            DataTable dtPayrollItemList = obj.PayrollItemGet();
            PayrollItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtPayrollItemList.Rows)
            {
                PayrollItemList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


            FunctionList = new List<SelectListItem>();
            DataTable dtFunctionList = obj.PayrollFunctionGet();
            FunctionList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtFunctionList.Rows)
            {
                FunctionList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

        }
    }

    public class EmpParameterViewModel
    {


        public Int32? ParameterId { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [Required(ErrorMessage = "Parameter Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Parameter Name")]
        public string ParameterName { get; set; }

        public string ParameterDescription { get; set; }

        public Int32? ParamDataType { get; set; }

    }

    public class LeaveTypeViewModel
    {
        public Int32? LeaveTypeId { get; set; }

        [Required(ErrorMessage = "Leave Type Name is required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Leave Type Name")]
        public string LeaveTypeName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Description")]
        public string LeaveDescription { get; set; }
        [Range(1, 1000, ErrorMessage = "DisplayOrder should be 1 to 1000.")]
        //  [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter digits only")]
        public int? DisplayOrder { get; set; }

    }

    public class TdsLimitViewModel
    {


        public Int32? TdsLimitId { get; set; }

        public string FiscalYear { get; set; }

        [Required(ErrorMessage = "HRA Exemption for Metro in % is Required.")]
        [Range(0, 100, ErrorMessage = "Value should be 1 to 100.")]
        public Int32? MetroPct { get; set; }

        [Required(ErrorMessage = "HRA Exemption for Non-Metro in %  is Required.")]
        [Range(0, 100, ErrorMessage = "Value should be 1 to 100.")]
        public Int32? NonMetroPct { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? HraExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? TransExemption { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? OtherExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MedBillExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? ChildEduExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? LtaExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? VehiMainExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? HousePropertyIncome { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? HouseLoanInterest { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? OtherIncome { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MedInsurPremium { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MedInsurPremiumPar { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MedHandicapDepend { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MedSpecDisease { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? HighEduLoanRepayment { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? DonateFundCharity { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? RentDeduction { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? PermanentDisableDeduction { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? InterestOnDeposit { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? RoyaltyIncomeDeduction { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? RoyaltyPatentDeduction { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? OtherDeduction { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? PensionScheme { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? Nsc { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? Ppf { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? Pf { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? InfraBond { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? ChildEduFund { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? HouseLoanPrincipalRepay { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? InsurancePremium { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MutualFund { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? Fd { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? UniformExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? SavingbankInterest { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? SavingbankInterestException { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? Rajivgandhisavingsscheme { get; set; }



        //---- Tax Slab 

        public Int32? TaxSlabId { get; set; }

        [Required(ErrorMessage = "Tax Slab Name is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Tax Slab Name ")]
        public string TaxSlabName { get; set; }

        //public Int32? TdsLimitId { get; set; }

        [Required(ErrorMessage = "Fiscal Year Detail is Required.")]
        public string FiscalYearDetail { get; set; }

        [Required(ErrorMessage = "Min Value Male is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MinvalMale { get; set; }

        [Required(ErrorMessage = "Max Value Male is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MaxvalMale { get; set; }

        [Required(ErrorMessage = "Min Value Female is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MinvalFemale { get; set; }

        [Required(ErrorMessage = "Max Value Female is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MaxvalFemale { get; set; }

        [Required(ErrorMessage = "Min Value Senior Citizon is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MinvalSeniorcitizon { get; set; }

        [Required(ErrorMessage = "Max Value Senior Citizon is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? MaxvalSeniorcitizon { get; set; }

        [Required(ErrorMessage = "Tax Rate is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
    //    [Range(.1, 100.00, ErrorMessage = "value should be 1 to 100 .")]

        public decimal? TaxRate { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Description { get; set; }
    }

    public class EmployeeShiftViewModel
    {

        public Int32? ShiftId { get; set; }

        [Required(ErrorMessage = "Shift Name is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Shift Name ")]
        public string ShiftName { get; set; }

        [Required(ErrorMessage = "Start Time is Required.")]
        public TimeSpan? ShiftStartTime { get; set; }

        [Required(ErrorMessage = "End Time is Required.")]
        public TimeSpan? ShiftEndTime { get; set; }

    }

    public class EmpCategoryViewModel
    {

        public Int32? EmpCategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Category Name ")]

        public string EmpCategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

    }

}