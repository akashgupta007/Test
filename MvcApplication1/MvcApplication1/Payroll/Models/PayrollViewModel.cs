using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Models
{
    public class PayrollViewModel
    {
       
    }

    public class PayrollItemTypeViewModel
    {
        public Int32? PayrollItemTypeId { get; set; }

        [Required(ErrorMessage = "Payroll Item Type is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\] ]*)$", ErrorMessage = "Invalid Payroll Item Type")]


        public string PayrollItemTypeDesc { get; set; }

        public Int32? AddDeduct { get; set; }

        public Int32? PayrollTemplateId { get; set; }
    }

    public class PayrollItemViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollItemId { get; set; }
        [Required(ErrorMessage = "Payroll Item is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\] ]*)$", ErrorMessage = "Invalid Payroll Item Name")]

        public string PayrollItemDesc { get; set; }

        public Int32? PayrollTypeId { get; set; }

        [Required(ErrorMessage = "Payroll Item Type is Required.")]
        public Int32? PayrollItemTypeId { get; set; }

        public bool? PayrollItemOverridable { get; set; }

        public bool? PropotionatePay { get; set; }

        public string AccountCode { get; set; }

        public Int32? IncomeTaxSection { get; set; }

        public Int32? CreditAccountNo { get; set; }

        public Int32? DebitAccountNo { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string PayrollItemNotes { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "DisplayOrder should be 1 to 1000.")]
        public Int32? DisplayOrder { get; set; }

        public bool IsVariablePay { get; set; }

        public bool IsTaxApplicable { get; set; }

        public Int32? EmpTypeId { get; set; }

        public Int32? EmployeeId { get; set; }

        public string Entity { get; set; }

        public List<SelectListItem> PayrollItemTypeList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public PayrollItemViewModel()
        {
            try
            {
                PayrollItemTypeList = new List<SelectListItem>();
                DataTable dtPayrollItemTypeList = obj.PayrollItemTypeGet();
                PayrollItemTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollItemTypeList.Rows)
                {
                    PayrollItemTypeList.Add(new SelectListItem
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
                EmployeeList = new List<SelectListItem>();
                DataTable dtEmplyeeList = obj.EmployeeGet(null);
                EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmplyeeList.Rows)
                {
                    EmployeeList.Add(new SelectListItem
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

    public class PayrollTaxTypeViewModel
    {


        public Int32? PayrollTaxTypeId { get; set; }

        [Required(ErrorMessage = "Payroll Tax Type Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Payroll Tax Type Name")]
        public string PayrollTaxTypeName { get; set; }

        public string PayrollTaxTypeDesc { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        public bool? IsCalculationAuto { get; set; }

    }

    public class PayrollItemTaxViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollItemTaxId { get; set; }

        public Int32? PayrollItemId { get; set; }

        public Int32? PayrollTaxId { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        public List<SelectListItem> PayrollItemList { get; set; }
        public List<SelectListItem> PayrollTaxList { get; set; }

        public PayrollItemTaxViewModel()
        {
            try
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
                PayrollTaxList = new List<SelectListItem>();
                DataTable dtPayrollTaxList = obj.PayrollTaxGet();
                PayrollTaxList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollTaxList.Rows)
                {
                    PayrollTaxList.Add(new SelectListItem
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
    //----------------Payroll Bonus---------------------//

    public class PayrollBonusViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollBonusId { get; set; }
        public decimal? MaxWages { get; set; }
        public decimal? MinWages { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? BonusAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int32? LocationId { get; set; }
        public List<SelectListItem> LocationList{ get; set; }
        public PayrollBonusViewModel()
        {
          
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

    }

    //--------------------------------------------------//
    //----------------Payroll Gratuity---------------------//

    public class PayrollGratuityViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollGratuityId { get; set; }

        public bool IsPfApplicable { get; set; }
        public bool Is_Basic { get; set; }
        
        public bool Is_Hra { get; set; }

        public bool Is_Da { get; set; }

        [Required(ErrorMessage="Location Is Required")]
        public Int32? LocationId { get; set; }

        [Required(ErrorMessage = "Working Days Is Required")]
        public Int32? WorkingDays { get; set; }

        [Required(ErrorMessage = "Multiply Value Is Required")]
        public decimal? MultiplyValue { get; set; }

        [Required(ErrorMessage = "Divide Value Is Required")]
        public decimal? DivideValue { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SelectListItem> LocationList { get; set; }


        public PayrollGratuityViewModel()
        {
          
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

    }

    //--------------------------------------------------//
    public class DaRangeViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? DaRangeId { get; set; }

        [Required(ErrorMessage = "Local Rate is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]

        [Range(1, 9999999999999999.00, ErrorMessage = "Value should be 1 to 16 digit no.")]
        public decimal? LocalRate { get; set; }

        [Required(ErrorMessage = "Non Local Rate is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]

        [Range(1, 9999999999999999.00, ErrorMessage = "Value should be 1 to 16 digit no.")]
        public decimal? NonLocalRate { get; set; }

        [Required(ErrorMessage = "Location is Required.")]
        public Int32? LocationId { get; set; }

        [Required(ErrorMessage = "Designation is Required.")]
        public Int32? DesignationId { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SelectListItem> LocationList { get; set; }

        public List<SelectListItem> DesignationList { get; set; }

        public DaRangeViewModel()
        {
            try
            {
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

                DesignationList = new List<SelectListItem>();
                DataTable dtDesignationList = obj.DesignationGet(true);
                DesignationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDesignationList.Rows)
                {
                    DesignationList.Add(new SelectListItem
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

    public class PayrollTaxViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollTaxId { get; set; }

        [Required(ErrorMessage = "Payroll Tax is Required.")]
        public string PayrollTaxDescription { get; set; }

        public Int32? PayrollTaxTypeId { get; set; }

        public string TaxableEntity { get; set; }

        public Int32? PayrollFunctionId { get; set; }

        public bool? PayrollTaxOverridable { get; set; }

        public string PayrollTaxTracking { get; set; }

        public string PayrollTaxPayableTo { get; set; }

        public string PayrollTaxIdentifier { get; set; }

        public Int32? PayrollTaxableFnId { get; set; }

        public Int32? CreditAccountNo { get; set; }

        public Int32? DebitAccountNo { get; set; }

        public string TaxParameterXsd { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        public Int32? EmpTypeId { get; set; }

        public Int32? EmployeeId { get; set; }

        public List<SelectListItem> PayrollTaxTypeList { get; set; }
        public List<SelectListItem> PayrollFunctionList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }

        public PayrollTaxViewModel()
        {
            try
            {
                PayrollTaxTypeList = new List<SelectListItem>();
                DataTable dtPayrollTaxTypeList = obj.PayrollTaxTypeGet();
                PayrollTaxTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollTaxTypeList.Rows)
                {
                    PayrollTaxTypeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                PayrollFunctionList = new List<SelectListItem>();
                DataTable dtPayrollFunctionList = obj.PayrollFunctionGet();
                PayrollFunctionList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollFunctionList.Rows)
                {
                    PayrollFunctionList.Add(new SelectListItem
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
                EmployeeList = new List<SelectListItem>();
                DataTable dtEmplyeeList = obj.EmployeeGet(null);
                EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmplyeeList.Rows)
                {
                    EmployeeList.Add(new SelectListItem
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

    public class PayrollPaymentMethodViewModel
    {
        public Int32? PaymentMethodId { get; set; }

        [Required(ErrorMessage = "Method Name is required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid  Payment Method Name")]

        public string PaymentMethodName { get; set; }

        public bool? DefaultMethod { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Notes { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        public bool? TxnPerBatch { get; set; }
    }

    public class PayrollFunctionViewModel
    {
        public Int32? PayrollFunctionId { get; set; }
        [Required(ErrorMessage = "Function Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9_\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Payroll Function Name")]
        public string PayrollFunctionName { get; set; }

        public string PayrollFunctionDesc { get; set; }

        public Int32? PayrollTemplateId { get; set; }
    }

    public class MasterMappingViewModel
    {
        public Int32? MasterMappingId { get; set; }

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        public string MasterTable { get; set; }

        public Int32? MasterKey { get; set; }

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Master Key Name")]
        public string MasterKeyName { get; set; }

        public Int32? PayrollItemId { get; set; }

        public Int32? PayrollTaxId { get; set; }
    }

    public class PayrollFunctionListViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollFunctionListId { get; set; }

        [Required(ErrorMessage = "Function List Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9_\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Payroll Function List Name")]
        public string PayrollFunctionListName { get; set; }

        [Required(ErrorMessage = "Payroll Function is Required.")]
        public Int32? PayrollFunctionId { get; set; }

        [Required(ErrorMessage = "Function Type is Required.")]
        public Int32? FunctionTypeId { get; set; }

        public DateTime? StartDt { get; set; }

        public DateTime? EndDt { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        public List<SelectListItem> PayrollFunctionList { get; set; }
        public List<SelectListItem> FunctinTypeList { get; set; }

        public PayrollFunctionListViewModel()
        {
            PayrollFunctionList = new List<SelectListItem>();
            DataTable dtPayrollFunctionList = obj.PayrollFunctionGet();
            PayrollFunctionList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            foreach (DataRow dr in dtPayrollFunctionList.Rows)
            {
                PayrollFunctionList.Add(new SelectListItem
                {
                    Value = dr[0].ToString(),
                    Text = dr[1].ToString()
                });
            }
            FunctinTypeList = new List<SelectListItem>();
            FunctinTypeList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            FunctinTypeList.Add(new SelectListItem { Value = "1", Text = "Table valued function returning table" });
            FunctinTypeList.Add(new SelectListItem { Value = "2", Text = "Scalar valued funtion returning a scalar value" });
            FunctinTypeList.Add(new SelectListItem { Value = "3", Text = "Stored Procedure" });
        }

    }

    public class PayrollFunctionParameterViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayrollFunctionParameterId { get; set; }

        public Int32? PayrollFunctionListId { get; set; }


        [Required(ErrorMessage = "Parameter Sequence is Required.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter digits only")]
        public Int16? ParameterSeq { get; set; }

        [Required(ErrorMessage = "Parameter Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Parameter Name")]
        public string ParameterName { get; set; }

        public string ParameterDatatype { get; set; }

        public string TableName { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Comments { get; set; }

        public Int32? PayrollFunctionId { get; set; }

        public Int32? PayrollTemplateId { get; set; }

        public List<SelectListItem> PayrollFunctionListList { get; set; }


        public List<SelectListItem> PayrollFunctionList { get; set; }



        public PayrollFunctionParameterViewModel()
        {
            PayrollFunctionListList = new List<SelectListItem>();
            DataTable dtPayrollFunctionListList = obj.PayrollFunctionListGet();
            PayrollFunctionListList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            foreach (DataRow dr in dtPayrollFunctionListList.Rows)
            {
                PayrollFunctionListList.Add(new SelectListItem
                {
                    Value = dr[0].ToString(),
                    Text = dr[1].ToString()
                });
            }


            PayrollFunctionList = new List<SelectListItem>();
            DataTable dtPayrollFunctionList = obj.PayrollFunctionGet();
            PayrollFunctionList.Add(new SelectListItem { Value = "", Text = "--Select--" });
            foreach (DataRow dr in dtPayrollFunctionList.Rows)
            {
                PayrollFunctionList.Add(new SelectListItem
                {
                    Value = dr[0].ToString(),
                    Text = dr[1].ToString()
                });
            }
        }
    }

    public class PayPercentViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? PayPercentId { get; set; }
        [Required(ErrorMessage = "Payroll item is Required.")]
        public Int32? PayrollItemId { get; set; }
        [Required(ErrorMessage = "Computing By is Required.")]
        public string ComputingBy { get; set; }
        public string SubtractBy { get; set; }
        public Int32? SubtractPayrollItemId { get; set; }

        public Int32? EmpTypeId { get; set; }
        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        //[RegularExpression(@"^(\d+(\.\d{0,3})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0.001, 100.00, ErrorMessage = "Enter 0.001 to 100 %")]
        public decimal? ItemPercent { get; set; }

        public Int32? LocationId { get; set; }

        public Int32? ItemTypeId { get; set; }

        public int?[] ComputingItemId { get; set; }

        public string ComputingItemList { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? AdditionalAmount { get; set; }

        public string ComputingType { get; set; }
        public List<SelectListItem> SubtractPayrollItemList { get; set; }

        public List<SelectListItem> PayrollItemList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }

        public PayPercentViewModel()
        {
            try
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


                SubtractPayrollItemList = new List<SelectListItem>();
                DataTable dtSubPayrollItemList = obj.PayrollItemGet();
                SubtractPayrollItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtSubPayrollItemList.Rows)
                {
                    SubtractPayrollItemList.Add(new SelectListItem
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
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class CcaRangeViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? Id { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? StartPayRange { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? EndPayRange { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Cca { get; set; }

        [Required]
        public int? LocationId { get; set; }


        public List<SelectListItem> LocationList { get; set; }

        public CcaRangeViewModel()
        {

            try
            {
                LocationList = new List<SelectListItem>();
                DataTable dtLocationListt = obj.LocationGet();
                LocationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLocationListt.Rows)
                {
                    LocationList.Add(new SelectListItem
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

    public class HraRangeViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? Id { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? StartPayRange { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? EndPayRange { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Hra { get; set; }

        [Required]
        public int? LocationId { get; set; }

        public List<SelectListItem> LocationList { get; set; }

        public HraRangeViewModel()
        {
            try
            {
                LocationList = new List<SelectListItem>();
                DataTable dtLocationListt = obj.LocationGet();
                LocationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLocationListt.Rows)
                {
                    LocationList.Add(new SelectListItem
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

    public class PayScale6pcViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? PayId { get; set; }

        [Required]
        public string PayScale { get; set; }

        [Required]
        public Int32? DesignationId { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? StartScale { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? EndScale { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? GradePay { get; set; }

        [Required]
        public string PayBand { get; set; }



        public List<SelectListItem> DesignationList { get; set; }

        public PayScale6pcViewModel()
        {
            try
            {
                DesignationList = new List<SelectListItem>();
                DataTable dtDesignationList = obj.DesignationGet(true);
                DesignationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtDesignationList.Rows)
                {
                    DesignationList.Add(new SelectListItem
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

    public class PTRangeViewModel
    {

        public Int32? PTRangeId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*\.?[0-9]+$", ErrorMessage = "Enter non negative whole or decimal number only")]
        public decimal? StartRange { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*\.?[0-9]+$", ErrorMessage = "Enter non negative whole or decimal number only")]
        public decimal? EndRange { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*\.?[0-9]+$", ErrorMessage = "Enter non negative whole or decimal number only")]
        public decimal? ProfessionTax { get; set; }

    }

    public class GradePerformanceViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? GradePerformanceId { get; set; }

        [Required]
        public Int32? EmpCategoryId { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? IndividualRate { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? IndividualPercentage { get; set; }
        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? UnitRate { get; set; }
        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? UnitPercentage { get; set; }
        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? OrganizationRate { get; set; }
        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? OrganizationPercentage { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<SelectListItem> EmpCategoryList { get; set; }

        public GradePerformanceViewModel()
        {
            try
            {
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
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class GradeSplitupViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? GradeId { get; set; }

        [Required]
        public Int32? EmpCategoryId { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Grade Name")]
        public string GradeName { get; set; }


        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Individual { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Unit { get; set; }

        [Required]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Organization { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SelectListItem> EmpCategoryList { get; set; }
        public List<SelectListItem> GradeNameList { get; set; }

        public GradeSplitupViewModel()
        {
            try
            {
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



                GradeNameList = new List<SelectListItem>();

                GradeNameList.Add(new SelectListItem { Text = "--Select--", Value = "" });

                GradeNameList.Add(new SelectListItem { Text = "P1", Value = "P1" });
                GradeNameList.Add(new SelectListItem { Text = "P2", Value = "P2" });
                GradeNameList.Add(new SelectListItem { Text = "P3", Value = "P3" });
                GradeNameList.Add(new SelectListItem { Text = "P4", Value = "P4" });
                GradeNameList.Add(new SelectListItem { Text = "P5", Value = "P5" });
                GradeNameList.Add(new SelectListItem { Text = "P6", Value = "P6" });
                GradeNameList.Add(new SelectListItem { Text = "P7", Value = "P7" });
                GradeNameList.Add(new SelectListItem { Text = "P8", Value = "P8" });
                GradeNameList.Add(new SelectListItem { Text = "P9", Value = "P9" });
                GradeNameList.Add(new SelectListItem { Text = "P10", Value = "P10" });




            }
            catch (Exception ex)
            {

            }
        }



    }

    public class EmployeePerformanceViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Boolean isRowCheck { get; set; }

        public Int32? EmployeePerformanceId { get; set; }

        public Int32? EmployeeId { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Grade Name")]
        public string GradeName { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? IndividualRate { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? UnitRate { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? OrganizationRate { get; set; }

        [Required]
        public string PerformanceType { get; set; }

        public Int32? Quarter { get; set; }

        public Int32? Month { get; set; }

        public Int32? Year { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Int32? ShiftId { get; set; }

        public Int32? EmpTypeId { get; set; }
        public Int32? DepartmentId { get; set; }
        public Int32? LocationId { get; set; }
        public Int32? DesginationId { get; set; }

        public Int32? MonthId { get; set; }
        public Int32? ProjectId { get; set; }

        public int? EmpCategoryId { get; set; }

        public List<SelectListItem> EmpCategoryList { get; set; }

        public List<SelectListItem> PerformanceTypeList { get; set; }

        public List<SelectListItem> ShiftList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> GradeNameList { get; set; }

        public List<EmployeePerformanceViewModel> EmpDataList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }

        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesginationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> LocationList { get; set; }

        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> QuarterList { get; set; }

        public EmployeePerformanceViewModel()
        {
            try
            {
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


                GradeNameList = new List<SelectListItem>();

                GradeNameList.Add(new SelectListItem { Text = "--Select--", Value = "" });

                GradeNameList.Add(new SelectListItem { Text = "P1", Value = "P1" });
                GradeNameList.Add(new SelectListItem { Text = "P2", Value = "P2" });
                GradeNameList.Add(new SelectListItem { Text = "P3", Value = "P3" });
                GradeNameList.Add(new SelectListItem { Text = "P4", Value = "P4" });
                GradeNameList.Add(new SelectListItem { Text = "P5", Value = "P5" });
                GradeNameList.Add(new SelectListItem { Text = "P6", Value = "P6" });
                GradeNameList.Add(new SelectListItem { Text = "P7", Value = "P7" });
                GradeNameList.Add(new SelectListItem { Text = "P8", Value = "P8" });
                GradeNameList.Add(new SelectListItem { Text = "P9", Value = "P9" });
                GradeNameList.Add(new SelectListItem { Text = "P10", Value = "P10" });


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












            }
            catch (Exception ex)
            {

            }
        }


    }


}