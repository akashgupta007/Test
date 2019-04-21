using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Models
{
    public class AssetViewModel
    {
    }
    public class AssetParialViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
       // PoisePayrollManLiftServiceModel ObjML = new PoisePayrollManLiftServiceModel();
        public Int32? Empl__Id { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }

        public AssetParialViewModel()
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
            }
            catch (Exception ex)
            { }
        }
    }
    public class AssetActivityTypeViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? AssetActivityTypeId { get; set; }

        [Required(ErrorMessage = "Asset  Activity Type is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Asset Activity ")]
        public string AssetActivityTypeDescription { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string AssetActivityTypeNotes { get; set; }

        public Int32? AssetActivityTypeFn { get; set; }

        public List<SelectListItem> Functionlist { get; set; }


        public AssetActivityTypeViewModel()
        {

            Functionlist = new List<SelectListItem>();
            DataTable dt = obj.PayrollFunctionGet();
            Functionlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                Functionlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }

    }


    public class AssetViewModels
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? AssetId { get; set; }

        [Required(ErrorMessage = "Asset Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Asset Name ")]
        public string AssetName { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Description")]
        public string AssetDescription { get; set; }

        [Required(ErrorMessage = "Interest Percent is Required.")]
        [Range(0.1, 100.00, ErrorMessage = " Interest Percent should be in 0.1 to 100")]
        public decimal? DefaultInterestPct { get; set; }

        public decimal? ExpensePerMonth { get; set; }

        public Int32? TaxableFunction { get; set; }

        public Int32? AssetDisbursementCreditAccountNo { get; set; }

        public Int32? AssetDisbursementDebitAccountNo { get; set; }

        public Int32? AssetRepaymentCreditAccountNo { get; set; }

        public Int32? AssetRepaymentDebitAccountNo { get; set; }

        public Int32? AssetInterestCreditAccountNo { get; set; }

        public Int32? AssetInterestDebitAccountNo { get; set; }

        public Int32? AssetExpenseCreditAccountNo { get; set; }

        public Int32? AssetExpenseDebitAccountNo { get; set; }

        public Int32? AssetTaxCreditAccountNo { get; set; }

        public Int32? AssetTaxDebitAccountNo { get; set; }

        public decimal? AddtionalCharges { get; set; }


        public List<SelectListItem> Functionlist { get; set; }


        public AssetViewModels()
        {

            Functionlist = new List<SelectListItem>();
            DataTable dt = obj.PayrollFunctionGet();
            Functionlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                Functionlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }

    }

    public class EmpAssetViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpAssetId { get; set; }

        [Required(ErrorMessage = "Asset Name is Required.")]
        public Int32? AssetId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Asset Created Date is Required.")]
        public DateTime? AssetCreatedDt { get; set; }

        [Required(ErrorMessage = "Interest Percent is Required.")]
        [Range(0.1, 100, ErrorMessage = " Interest Percent  should be in  0.1 to 100")]
        public decimal? InterestPct { get; set; }

        [Required(ErrorMessage = "Expense Per Months is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? ExpensePerMonth { get; set; }

        public Int32? PayrollFunctionId { get; set; }

        public Int32? PaymentTransactionId { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Asset Closed Date is Required.")]
        public DateTime? AssetClosedDt { get; set; }

        public List<SelectListItem> Functionlist { get; set; }
        public List<SelectListItem> Assetlist { get; set; }
        public List<SelectListItem> Employeelist { get; set; }


        public EmpAssetViewModel()
        {

            Functionlist = new List<SelectListItem>();
            DataTable dt = obj.PayrollFunctionGet();
            Functionlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                Functionlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            Assetlist = new List<SelectListItem>();
            DataTable dtAssetlist = obj.AssetGet();
            Assetlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtAssetlist.Rows)
            {
                Assetlist.Add(new SelectListItem
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



        }


    }


    //------------- Asset Report  Model ------------------

    public class AssetreportViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();


        public Int32? AssetId { get; set; }

        public Int32? EmployeeId { get; set; }

        public DateTime? AssetCreatedDt { get; set; }

        public List<SelectListItem> AssetList { get; set; }

        public List<SelectListItem> Employeelist { get; set; }
        public AssetreportViewModel()
        {
            try
            {


                AssetList = new List<SelectListItem>();
                DataTable dtAssetList = obj.AssetGet();
                AssetList.Add(new SelectListItem { Text = "...Select...", Value = "" });
                foreach (DataRow dr in dtAssetList.Rows)
                {
                    AssetList.Add(new SelectListItem
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


            }
            catch (Exception ex)
            {
                var a = ex.Message.ToString();
            }
        }


    }


    //-------------Asset Management Model--------------------

    public class AssetManagementViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        public Int32? AssetId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? CategoryId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? MakeId { get; set; }
        [Required(ErrorMessage = "*")]
        public string SerialnoEngineno { get; set; }
        [Required(ErrorMessage = "*")]
        public string KpiSiNo { get; set; }
        [Required(ErrorMessage = "*")]
        public string AccSiNo { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? DateOfPurchase { get; set; }
        public DateTime? DateOfAdditionFleet { get; set; }
        [Required(ErrorMessage = "*")]
        public string EquipmentNoFleetNo { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public DateTime? DateRemovalFleet { get; set; }
        public DateTime? YearOfManufacturing { get; set; }
        public string Reason { get; set; }
        public string Value { get; set; }
        public string duties { get; set; }
        public string Taxes { get; set; }
        public int? Total { get; set; }
        public int? DepreciationAsPerCo { get; set; }
        public int? DepreciationAsPerIT { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }

        public List<SelectListItem> AssetCategorylist { get; set; }


        public AssetManagementViewModel()
        {

            AssetCategorylist = new List<SelectListItem>();
            DataTable dt = ObjML.CategoryGet();
            AssetCategorylist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                AssetCategorylist.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


        }

    }


    //-------------Asset Model--------------------

    public class AssetModelViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        public Int32? ModelId { get; set; }
        [Required(ErrorMessage = "Category is Required.")]
        public Int32? CategoryId { get; set; }
        [Required(ErrorMessage = "Model is Required.")]
        public string ModelName { get; set; }
        public string ModelDesc { get; set; }

        public List<SelectListItem> AssetCategorylist { get; set; }


        public AssetModelViewModel()
        {

            AssetCategorylist = new List<SelectListItem>();
            DataTable dt = ObjML.CategoryGet();
            AssetCategorylist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                AssetCategorylist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


        }

    }

    //-------------Asset Make --------------------

    public class AssetMakeViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        public Int32? MakeId { get; set; }
        [Required(ErrorMessage = "Category is Required.")]
        public Int32? CategoryId { get; set; }
        [Required(ErrorMessage = "Make is Required.")]
        public string MakeName { get; set; }
        public string MakeDesc { get; set; }

        public List<SelectListItem> AssetCategorylist { get; set; }


        public AssetMakeViewModel()
        {

            AssetCategorylist = new List<SelectListItem>();
            DataTable dt = ObjML.CategoryGet();
            AssetCategorylist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                AssetCategorylist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


        }



    }
    public class CustomerManagementViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();


        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Customer Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public int? District { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid City Name")]
        public string City { get; set; }
        [Required(ErrorMessage = "*")]

        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid Postal Code")]
        [DataType(DataType.PostalCode)]
        public string PinCode { get; set; }
        [Required(ErrorMessage = "*")]
        public string States { get; set; }
        [Required(ErrorMessage = "*")]
        public string Country { get; set; }
        public string Districts { get; set; }

        [Required(ErrorMessage = "*")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*")]
        public string Fax { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(12, ErrorMessage = "Max length is 12.")]
        [RegularExpression(@"^([A-Z]{4}\d{5}[A-Z]{1} *)$", ErrorMessage = "Invalid Tan No.")]
        public string Tan { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(12, ErrorMessage = "Max length is 12.")]
        [RegularExpression(@"^([A-Z]{5}\d{4}[A-Z]{1} *)$", ErrorMessage = "Invalid Pan No.")]
        public string PayrolltaxPanno { get; set; }
        [Required(ErrorMessage = "*")]
        public string PayrolltaxContact { get; set; }

        public string Designation { get; set; }

        public int AddressId { get; set; }

        public string PayrollTaxArea { get; set; }

        public string PayrolltaxPhone { get; set; }

        public string PayrolltaxStdCode { get; set; }

        public string PayrolltaxDivision { get; set; }

        public string PayrolltaxFlatno { get; set; }

        public string PayrollTaxBuilding { get; set; }

        public string PayrollTaxStreet { get; set; }

        public int PayrollTaxStateId { get; set; }
        [Required(ErrorMessage = "*")]
        public string Address1 { get; set; }
        [Required(ErrorMessage = "*")]
        public string Address2 { get; set; }

        public DateTime? LockTransactionsUpto { get; set; }
        //  [A-Z]{5}\d{4}[A-Z]{1}
        //[RegularExpression(@"^([A-Z]{5}\d{4}[A-Z]{3}\d{3} *)$", ErrorMessage = "Invalid Service Tax No")]
        [Required(ErrorMessage = "*")]
        public decimal? Stc { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^([A-Za-z0-9\/]*)$", ErrorMessage = "Invalid Premises Code.")]
        public string PremisesCode { get; set; }

        [Required(ErrorMessage = "*")]

        [RegularExpression(@"^([A-Za-z0-9\/]*)$", ErrorMessage = "Invalid ESI No.")]
        public string EsiAccno { get; set; }

        [Required(ErrorMessage = "Pay Start Date is Required.")]
        public int? PayStartdt { get; set; }

        public bool IsAttendanceRequired { get; set; }

        public string Zone { get; set; }

        public int? location_id { get; set; }

        public int? CustomerId { get; set; }

        public int? stateId { get; set; }
        public int? districtId { get; set; }
        public int? cityId { get; set; }
        public int manualidorsqlid { get; set; }


        public List<SelectListItem> CustomerList { get; set; }
        public List<SelectListItem> LocationList { get; set; }

        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }

        public CustomerManagementViewModel()
        {
            PoisePayrollManliftServiceModel obj1 = new PoisePayrollManliftServiceModel();

            CountryList = new List<SelectListItem>();
            DataTable ds = obj1.GetCountry();
            CountryList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in ds.Rows)
            {


                CountryList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            CustomerList = new List<SelectListItem>();
            DataTable dsCustomerList = obj1.GetCustomer();
            CustomerList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dsCustomerList.Rows)
            {

                CustomerList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


        }

    }

    //-------------Asset Management Model--------------------
    public class Autocomplete
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class ContractViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();

        [Required(ErrorMessage = "*")]
        public bool? MultiShift { get; set; }
        public Int32? ContractId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? CustomerId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? SiteAddressId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? EmployeeId { get; set; }
        [Required(ErrorMessage = "*")]
       // [RegularExpression(@"^([a-zA-Z_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\]){5}([0-9 _\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\]){4}([0-9]){4}$", ErrorMessage = "Invalid Work Order No .")]
        public string WorkOrderNo { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? BillingPeriodTo { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? BillingPeriodFrom { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? KPITimeTo { get; set; }
       // [Required(ErrorMessage = "*")]
        public DateTime? KPITimeFrom { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? BillingUnit1 { get; set; }

        public string Desciption { get; set; }
        [Required(ErrorMessage = "*")]
        public string Quantity { get; set; }
        public Int32? BillingUnitId1 { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? TransportationInAmount { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? TransportationOutAmount { get; set; }
        public decimal? LoadingAmount { get; set; }
        public Int32? BillingUnitId2 { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? BillingUnit2 { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? Total { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? MonthValue { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? BillingAmt1 { get; set; }
        public string BillingAmtId1 { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? BillingAmt2 { get; set; }

        public string BillingAmtId2 { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? Overtime { get; set; }
        public Int32? OvertimeId { get; set; }
        [Required(ErrorMessage = "*")]
        public int? TransportationInId { get; set; }
        [Required(ErrorMessage = "*")]
        public int? TransportationOutId { get; set; }
        public int? Loading { get; set; }
        public int?[] ComputingItemId { get; set; }

        public string ComputingItemList { get; set; }
        public string ComputingItemNameList { get; set; }
        public List<SelectListItem> Taxeslist { get; set; }
        public List<SelectListItem> Customerlist { get; set; }
        public List<SelectListItem> SalesManagerlist { get; set; }
        public List<SelectListItem> BillingUnitlist1 { get; set; }
        public List<SelectListItem> BillingUnitlist2 { get; set; }
        public List<SelectListItem> OvertimeCalculationlist { get; set; }
        public ContractViewModel()
        {

            Customerlist = new List<SelectListItem>();
            DataTable dt = ObjML.GetCustomer();
            Customerlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                Customerlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            SalesManagerlist = new List<SelectListItem>();
            DataTable dtSM = ObjML.SalesManagerGet();
            SalesManagerlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtSM.Rows)
            {
                SalesManagerlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


            Taxeslist = new List<SelectListItem>();
            DataTable dtTaxeslist = ObjML.GetTax();
            Taxeslist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtTaxeslist.Rows)
            {
                Taxeslist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


            BillingUnitlist1 = new List<SelectListItem>();
            BillingUnitlist1.Add(new SelectListItem { Text = "--Select--", Value = "1" });
            BillingUnitlist1.Add(new SelectListItem { Text = "Hours", Selected = true, Value = "2" });
            BillingUnitlist1.Add(new SelectListItem { Text = "Days", Value = "3" });
            BillingUnitlist1.Add(new SelectListItem { Text = "Weeks", Value = "4" });
            BillingUnitlist1.Add(new SelectListItem { Text = "Months", Value = "5" });

            BillingUnitlist2 = new List<SelectListItem>();
            BillingUnitlist2.Add(new SelectListItem { Text = "--Select--", Value = "1" });
            BillingUnitlist2.Add(new SelectListItem { Text = "Hours", Value = "2" });
            BillingUnitlist2.Add(new SelectListItem { Text = "Days", Selected = true, Value = "3" });
            BillingUnitlist2.Add(new SelectListItem { Text = "Weeks", Value = "4" });
            BillingUnitlist2.Add(new SelectListItem { Text = "Months", Value = "5" });

            OvertimeCalculationlist = new List<SelectListItem>();
            OvertimeCalculationlist.Add(new SelectListItem { Text = "--Select--", Value = "1" });
            OvertimeCalculationlist.Add(new SelectListItem { Text = "Hours", Selected = true, Value = "2" });
            OvertimeCalculationlist.Add(new SelectListItem { Text = "Days", Value = "3" });
            OvertimeCalculationlist.Add(new SelectListItem { Text = "Weeks", Value = "4" });
            OvertimeCalculationlist.Add(new SelectListItem { Text = "Months", Value = "5" });

        }

    }

    public class TaxViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        public Int32? TaxId { get; set; }
        [Required(ErrorMessage = "tax is Required.")]
        public string TaxDesc { get; set; }
        [Required(ErrorMessage = "Tax percent is Required.")]
        public decimal? taxpercent { get; set; }

        public TaxViewModel()
        {
        }
    }
    public class OvertimeViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        public Int32? OTId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public int? ModelId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public int? MakeId { get; set; }
        [Required(ErrorMessage = "Required.")]
        public decimal? Overtime { get; set; }

        [Required(ErrorMessage = "Required.")]
        public int? AssetId { get; set; }
        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? OtrateStartDate { get; set; }

        public DateTime? OtrateEndDate { get; set; }
        public List<SelectListItem> OTCategorylist { get; set; }
        public List<SelectListItem> Employeelist { get; set; }
        public List<SelectListItem> MachineList { get; set; }

        public OvertimeViewModel()
        {
            OTCategorylist = new List<SelectListItem>();
            DataTable dt = ObjML.CategoryGet();
            OTCategorylist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                OTCategorylist.Add(new SelectListItem
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
            MachineList = new List<SelectListItem>();
            DataTable _MachineList = obj.GetAsset();
            MachineList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in _MachineList.Rows)
            {
                MachineList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }
    }


    public class getdataClass
    {
        public Int32? MonthId { get; set; }
        
       
        public Int32? Year { get; set; }
        public Int32? Id { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public Int32? CustomerId { get; set; }
        public Int32? ModelId { get; set; }
        public Int32? MakeId { get; set; }
        public Int32? SiteAddressId { get; set; }
        public Int32? EmployeeId { get; set; }
        public int Check { get; set; }
        


    }
    public class TimeSheetEntryViewModel : PayrollUtil
    {
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();

        public DateTime? AttendanceDate { get; set; }
        public Int32? Id { get; set; }
        public Int32? CustomerId { get; set; }
        public Int32? ModelId { get; set; }
        public Int32? MakeId { get; set; }
        public Int32? SiteAddressId { get; set; }
        public List<TimeSheetEntryViewModel> EmployeeDataList { get; set; }

        public Boolean isCheck { get; set; }

        public Int32? EmpDailyAttendanceId { get; set; }

        public Int32? BMUserId { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? Start_Date { get; set; }

        public TimeSpan? WorkInTime { get; set; }

        public TimeSpan? WorkOutTime { get; set; }
        public TimeSpan? BreakInTime { get; set; }

        public TimeSpan? BreakOutTime { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Remarks")]
        public string Remarks { get; set; }
        public decimal? MeterReading { get; set; }
        public TimeSpan? LunchBreak { get; set; }
        public string AttendanceStatus { get; set; }
        public byte[] UploadAttachment { get; set; }
        public List<SelectListItem> SalesManagerlist { get; set; }
        public List<SelectListItem> Customerlist { get; set; }
        public List<SelectListItem> SiteAddressList { get; set; }
        public List<SelectListItem> ModelList { get; set; }
        public List<SelectListItem> MakeList { get; set; }
        public TimeSheetEntryViewModel()
        {
            SalesManagerlist = new List<SelectListItem>();
            //DataTable dtSM = ObjML.SalesManagerGet();
            DataTable dtSM = ObjML.OperatorGet();

            SalesManagerlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtSM.Rows)
            {
                SalesManagerlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            Customerlist = new List<SelectListItem>();
            DataTable dt = ObjML.GetCustomer();
            Customerlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                Customerlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            SiteAddressList = new List<SelectListItem>();
            DataTable dtSA = ObjML.AllCustomerSiteGet(null);
            SiteAddressList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtSA.Rows)
            {
                SiteAddressList.Add(new SelectListItem
                {

                    Text = dr[7].ToString(),
                    Value = dr[0].ToString()
                });
            }


            ModelList = new List<SelectListItem>();
            DataTable dtMO = ObjML.ModelGet(null);
            ModelList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtMO.Rows)
            {
                ModelList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            MakeList = new List<SelectListItem>();
            DataTable dtMA = ObjML.MakeGet(null);
            MakeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtMA.Rows)
            {
                MakeList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

        }
    }

    public class OperatorFleetAssignment
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        [Required(ErrorMessage = "*")]
        public Int32? ContractId { get; set; }
        [Required]
        public Int32? AssetId { get; set; }
        [Required]
        public Int32? MachineId { get; set; }
        [Required]
        public Int32? TrasporterId { get; set; }
        [Required]
        public Int32? CustomerId { get; set; }
        [Required]
        public Int32? SiteAddressId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? CategoryId { get; set; }
        public string CustomerName { get; set; }
        public string AddressName { get; set; }
        public string ContractEmployeeName { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? FleetNo { get; set; }
        public Int32? MakeId { get; set; }

        public Int32? ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? TaxesIn { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? TaxesOut { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? AmountInWard { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? AmountOutWard { get; set; }
        [Required(ErrorMessage = "*")]
        public Int32? EmployeeId { get; set; }
        
        public Int32? ContractEmployeeId { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? EndDate { get; set; }
        [Required]
        public DateTime? MachineEntryDate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string hfContractStartDate { get; set; }
        public string hfContractEndDate { get; set; }
        [Required(ErrorMessage = "*")]
        public bool? isMulti { get; set; }

        public DateTime? opeartorStartDate { get; set; }
        public DateTime? opeartorEndDate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? TrasportationOutWardDispatchStartDate { get; set; }
        [Required(ErrorMessage = "*")]

        public DateTime? TrasportationInWardDispatchStartDate { get; set; }
        [Required(ErrorMessage = "*")]

        public DateTime? TrasportationInWardReciptdate { get; set; }
        [Required(ErrorMessage = "*")]
        public DateTime? TrasportationOutWardReciptdate { get; set; }
        [Required(ErrorMessage = "*")]
        public string TrasportationNameInWard { get; set; }
        [Required(ErrorMessage = "*")]
        public string TrasportationNameOutWard { get; set; }
        [Required(ErrorMessage = "*")]
        public string TrasportationChecknoInWard { get; set; }
        [Required(ErrorMessage = "*")]
        public string TrasportationCheckNoOutWard { get; set; }
       
        public decimal? RoomRentPaid { get; set; }
        public decimal? ArrearRoomRentPaid { get; set; }
        public decimal? Travels { get; set; }
        public decimal? Food { get; set; }
        public decimal? MachineIncentives { get; set; }
        public decimal? TotalExpenses { get; set; }

        public int?[] ComputingItemId { get; set; }

        public string ComputingItemList { get; set; }


        public int?[] ComputingItemId1 { get; set; }

        public string ComputingItemList1 { get; set; }
        public List<SelectListItem> Customerlist { get; set; }
        public List<SelectListItem> SalesManagerlist { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> AssetCategorylist { get; set; }
        public List<SelectListItem> AssetFleetlist { get; set; }
        public List<SelectListItem> ContractList { get; set; }
        public List<SelectListItem> TransporterInwardList { get; set; }
        public List<SelectListItem> TransporteroutwardList { get; set; }

        public List<SelectListItem> TaxeslistIn { get; set; }
        public List<SelectListItem> TaxeslistOut { get; set; }

        public OperatorFleetAssignment()
        {

            DataTable dt2 = ObjML.AssetMachineGet();
            AssetCategorylist = new List<SelectListItem>();
            DataTable dt = obj.CategoryGet();
            AssetCategorylist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                AssetCategorylist.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


            AssetFleetlist = new List<SelectListItem>();
            DataTable dts = obj.GetAsset();
            AssetFleetlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            DataTable dtThirdTable = new DataTable();
            dtThirdTable.Columns.Add("ID", Type.GetType("System.String"));
            dtThirdTable.Columns.Add("Name", Type.GetType("System.String"));
            foreach (DataRow drFirstTableRow in dts.Rows)
            {
                bool matched = false;
                foreach (DataRow drSecondTableRow in dt2.Rows)
                {
                    if (drFirstTableRow["AssetId"].ToString() == drSecondTableRow["asset_id"].ToString())
                    {
                        matched = true;
                    }
                }
                if (!matched)
                {
                    DataRow drUnMatchedRow = dtThirdTable.NewRow();
                    drUnMatchedRow["ID"] = drFirstTableRow["AssetId"];
                    drUnMatchedRow["Name"] = drFirstTableRow["AssetName"];
                    dtThirdTable.Rows.Add(drUnMatchedRow);
                }
            }
            foreach (DataRow dr in dtThirdTable.Rows)
            {
                AssetFleetlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }           

            EmployeeList = new List<SelectListItem>();
            // DataTable dtEmployeeList = obj.EmployeeInfoGet();
            DataTable dtEmployeeList = ObjML.OperatorGet();
            EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtEmployeeList.Rows)
            {
                EmployeeList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

            Customerlist = new List<SelectListItem>();
            DataTable dtt = obj.GetCustomer();
            Customerlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtt.Rows)
            {
                Customerlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
            SalesManagerlist = new List<SelectListItem>();
            DataTable dtSM = obj.SalesManagerGet();
            SalesManagerlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtSM.Rows)
            {
                SalesManagerlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
            
            ContractList = new List<SelectListItem>();
            DataTable dt1 = obj.GetContractByID(null);
            ContractList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            DataTable dtThirdTables = new DataTable();
            dtThirdTables.Columns.Add("ID", Type.GetType("System.String"));
            dtThirdTables.Columns.Add("Name", Type.GetType("System.String"));
            foreach (DataRow drFirstTableRow in dt1.Rows)
            {
                bool matched = false;
                foreach (DataRow drSecondTableRow in dt2.Rows)
                {
                    if (drFirstTableRow["ContractId"].ToString() == drSecondTableRow["ContractId"].ToString())
                    {
                        matched = true;
                    }
                }
                if (!matched)
                {
                    DataRow drUnMatchedRow = dtThirdTables.NewRow();
                    drUnMatchedRow["ID"] = drFirstTableRow["ContractId"];
                    drUnMatchedRow["Name"] = drFirstTableRow["Contractstart"];
                    dtThirdTables.Rows.Add(drUnMatchedRow);
                }
            }
            foreach (DataRow dr in dtThirdTables.Rows)
            {
                ContractList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
            TransporterInwardList = new List<SelectListItem>();
            DataTable TinWard = obj.AssetTransporterGet();
            TransporterInwardList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in TinWard.Rows)
            {
                TransporterInwardList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
            TransporteroutwardList = new List<SelectListItem>();
            DataTable TOutWard = obj.AssetTransporterGet();
            TransporteroutwardList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in TOutWard.Rows)
            {
                TransporteroutwardList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
            //TaxeslistIn = new List<SelectListItem>();
            //DataTable dtTaxeslist = ObjML.GetTax();
            //TaxeslistIn.Add(new SelectListItem { Text = "--Select--", Value = "" });
            //foreach (DataRow dr in dtTaxeslist.Rows)
            //{
            //    TaxeslistIn.Add(new SelectListItem
            //    {

            //        Text = dr[1].ToString(),
            //        Value = dr[0].ToString()
            //    });
            //}
            //TaxeslistOut = new List<SelectListItem>();
            //DataTable dtTaxeslisto = ObjML.GetTax();
            //TaxeslistOut.Add(new SelectListItem { Text = "--Select--", Value = "" });
            //foreach (DataRow dr in dtTaxeslisto.Rows)
            //{
            //    TaxeslistOut.Add(new SelectListItem
            //    {

            //        Text = dr[1].ToString(),
            //        Value = dr[0].ToString()
            //    });
            //}
        }





    }
    public class TrasporterViewModel
    {
        public Int32? TrasporterId { get; set; }
        public string TrasporterName { get; set; }
        public string TrasporterDescription { get; set; }

    }
    public class EmployeemachineKnown
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();       
        public Int32? EMKId { get; set; }
        [Required]
        public Int32? EmployeeId { get; set; }
        [Required]
        public Int32? TaxesIn { get; set; }
        [Required(ErrorMessage ="*")]
        public Int32? AssetId { get; set; }
        public int?[] ComputingItemId { get; set; }
        public string ComputingItemList { get; set; }
        public string ComputingItemNameList { get; set; }
        public List<SelectListItem> AssetFleetlist { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }       
        public EmployeemachineKnown()
        {


            //AssetFleetlist = new List<SelectListItem>();
            //DataTable dt = obj.GetAsset();
            //AssetFleetlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            //foreach (DataRow dr in dt.Rows)
            //{
            //    AssetFleetlist.Add(new SelectListItem
            //    {
            //        Text = dr[1].ToString(),
            //        Value = dr[0].ToString()
            //    });
            //}
            EmployeeList = new List<SelectListItem>();
            DataTable dtEmployeeList = ObjML.OperatorGet();
            EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtEmployeeList.Rows)
            {
                EmployeeList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }
    }
}






