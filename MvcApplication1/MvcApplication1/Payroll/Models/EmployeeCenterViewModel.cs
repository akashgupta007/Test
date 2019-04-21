using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

using Newtonsoft.Json;
using PoiseERP.Areas.Payroll.Models;
using PoisePayroll.Models;

using System.Collections;

using System.Configuration;

using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PoiseERP.Areas.Payroll.Models
{

    public class EmployeeCenterViewModel
    {

    }
    public class ViewModel
    {        
        public IList<EmpSalaryViewModel> EmpSalListModel { get; set; }
    }
    public class EmployeeParialViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();

        public Int32? Employee__Id { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }

        public EmployeeParialViewModel()
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

    //-----------------Employee Bank Details Model-------------

    public class EmployeeBankDetailsViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
      
        public Int32? EmployeeBankDetailsId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }
        [Required(ErrorMessage = "Bank Account no is Required.")]
        public string BankAccountNo { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "Bank Name is Required.")]
        public Int32? EmpBankId { get; set; }
       
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> BankList { get; set; }
       
        public EmployeeBankDetailsViewModel()
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
            }

            catch (Exception Ex)
            {

            }
        }

    }
    public class EmployeeViewModel: EmpEducationViewModel
    //{
    //public class EmployeeViewModel: EmpSalaryViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        PoisePayrollManliftServiceModel ObjML = new PoisePayrollManliftServiceModel();
        public Int32? EmployeeDocumentId { get; set; }
        public int?[] ComputingItemId { get; set; }
        public string ComputingItemList { get; set; }
        public string ComputingItemNameList { get; set; }
        public int?[] ComputingItemId1 { get; set; }
        public string ComputingItemList1 { get; set; }
        public string ComputingItemNameList1 { get; set; }
        public Int32? DocumentId { get; set; }
        //[Required(ErrorMessage = "Document Name is Required.")]
        public string DocumentObjectName { get; set; }
        public Int32? EmpbankDetailsId { get; set; }

        public int LeavingReasonId { get; set; }
        public int ESILeavingReasonId { get; set; }
        public Int32? AttendanceSourceId { get; set; }
        public Int32? hfCompany_id { get; set; }
        public Int32? EmployeeId { get; set; }

        [StringLength(20, ErrorMessage = "Max length is 20 .")]
        [Required(ErrorMessage = "*")]
       // [RegularExpression(@"^([A-Za-z0-9\@\/\(\)\[\]]*)$", ErrorMessage = "*")]
        public string EmpCode { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50 .")]

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Employee Name is Required*")]
        public string EmpName { get; set; }

        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Required")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Required")]
        public string Sex { get; set; }
        public bool? chewtab { get; set; }
        public bool? smoking { get; set; }
        public bool? tenC { get; set; }
        public bool? gradC { get; set; }
        public bool? tweleveC { get; set; }
        public bool? PGC { get; set; }
        public bool? DLC { get; set; }
        public bool? RPC { get; set; }
        public bool? ICC { get; set; }
        public bool? PassC { get; set; }
        public bool? LCJC { get; set; }
        public bool? ProfC { get; set; }
        public bool? PanC { get; set; }
        public bool? bankC { get; set; }
        public bool? AdharC { get; set; }
        public bool? PoliceC { get; set; }
       

        [Required(ErrorMessage = "*")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "*")]
        public DateTime? Doj { get; set; }

        public DateTime? Doj2 { get; set; }

        public DateTime? Dol { get; set; }

        public DateTime? RetirementDate { get; set; }

        public Int32? TitleId { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        // [Required(ErrorMessage = "Education is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Education Name")]
        public string Education { get; set; }
        [StringLength(70, ErrorMessage = "Max length is 70 .")]
        // [Required(ErrorMessage = "Email is Required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Enter Correct Email Address")]
        public string EmailAddress { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Enter Correct Email Address")]
        public string EmailAddressRes { get; set; }

        public Int32? EmpBankId { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Bank Account")]
        public string BankAccountNo { get; set; }

        public Int32? MajorProjectNo { get; set; }
        public string passportNo { get; set; }
        public DateTime? PassportdateofIssue { get; set; }
        public DateTime? passportdateofExpiry { get; set; }
        public string PassportPlaceofissue { get; set; }
        public string PassportAddress { get; set; }
        public string LegalCopy { get; set; }
        public string drivinglicence { get; set; }
        public string poilceverification { get; set; }
        public bool? Trainee { get; set; }

        public string DdStation { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500 .")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Indentity Alias")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string Alias1 { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Indentity Alias")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]

        public string Alias2 { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Indentity Alias")]
        public string Alias3 { get; set; }

        public string AliasLike { get; set; }
        public DateTime? DLdateofIssue { get; set; }
        public DateTime? DLdateofExpiry { get; set; }
        public string DLIssueAutority { get; set; }
        public string DLType { get; set; }

        public string LoginId { get; set; }

        public bool? IsAttendanceRequired { get; set; }
        [StringLength(20, ErrorMessage = "Max length is 20 .")]
        public string Password { get; set; }

        public Int32? PayMethodId { get; set; }

        public Int32? MgrId { get; set; }
        [StringLength(30, ErrorMessage = "Max length is 30 .")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid Community")]
        public string Community { get; set; }
        [StringLength(30, ErrorMessage = "Max length is 30 .")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid Caste")]
        public string Caste { get; set; }
        [StringLength(30, ErrorMessage = "Max length is 30 .")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid Religion")]
        public string Religion { get; set; }
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Indentity Marks")]
        public string IdenMks1 { get; set; }
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Indentity Marks")]
        public string IdenMks2 { get; set; }
        [Range(30, 300, ErrorMessage = "Enter 30 to 300")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Number")]
        public Int32? Height { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]

        [Range(0.1, 200.00, ErrorMessage = "Enter 0.1 to 200")]
        public decimal? Weight { get; set; }
        [StringLength(20, ErrorMessage = "Max length is 20.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z\+\- ]*)$", ErrorMessage = "Invalid BloodGroup")]
        public string BloodGroup { get; set; }
        public string ColorBlind { get; set; }
        public string WearSpectacles { get; set; }
        public string sufferphy { get; set; }




        public DateTime? Dor { get; set; }

        public Int32? EmpTypeId { get; set; }

        public Int32? EmpType_Id { get; set; }

        public byte[] EmpPhoto { get; set; }
        public string imagePath { get; set; }
      //  [StringLength(200, ErrorMessage = "Max length is 200.")]
       
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid Nationality")]
        public string Nationality { get; set; }

        //  [Required(ErrorMessage = "Marital Status is Required.")]
        public string MaritalStatus { get; set; }
        public string VisibleMark { get; set; }

        public Int32? EmpOrder { get; set; }
         [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid Father/Husband Name")]
        public string FatherHusbandName { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid Mother Name")]
        public string MotherName { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string ReservationCategory { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0.1, 100.00, ErrorMessage = "Enter 0.1 to 100 %")]
        public decimal? HandicapPercent { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        public string HandicapNotes { get; set; }

        public string GisNo { get; set; }

        public bool? IsHandicap { get; set; }

        public byte[] ResumeCopy { get; set; }

        public string ResumeContentType { get; set; }

        public string ResumeName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Relationship { get; set; }
        [StringLength(200, ErrorMessage = "Max length is 200.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Address")]
        public string Address1 { get; set; }
        [StringLength(200, ErrorMessage = "Max length is 200.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Address")]
        public string Address2 { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        //   [Required(ErrorMessage = "City is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid City Name")]
        public string City { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid District Name")]
        public string District { get; set; }

        //  [Required(ErrorMessage = "State is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid State Name")]
        public string Region { get; set; }
        [Range(100000, 999999, ErrorMessage = "Enter 100000 to 999999")]

        //  [Required(ErrorMessage = "Pincode is Required.")]
        [DataType(DataType.PostalCode)]
        public Int64? Pincode { get; set; }


        // [Required(ErrorMessage = "Mobile is Required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid number")]
        public Int64? MobileNo { get; set; }

        public bool? IsInternationalWorker { get; set; }

        public Int32? EmpBankFlag { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z.\& ]*)$", ErrorMessage = "Invalid Bank Name")]
        public string EmpBankName { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9]*)$", ErrorMessage = "Invalid IFC Code")]
        public string BankIfscCode { get; set; }

        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid Middle Name")]
        public string MiddleName { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        //  [Required(ErrorMessage = "State is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid State")]
        public string State { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string EmployeeCategory { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        [RegularExpression(@"^[a-zA-Z0-9. ]*$", ErrorMessage = "Invalid Work Division")]
        public string WorkDivision { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string ReportingOfficer { get; set; }
        
        public Int32? ProjectId { get; set; }       
        public Int32? Project_Id { get; set; }

        [Required(ErrorMessage = "*")]
        public Int32? DepartmentId { get; set; }
       
        public Int32? Department_Id { get; set; }

        [Required(ErrorMessage = "*")]
        public Int32? DesignationId { get; set; }

        [Required(ErrorMessage = "*")]
        public Int32? Designation_Id { get; set; }
     
        [Required(ErrorMessage = "*")]
        public Int32? LocationId { get; set; }
        public Int32? EmpProjectId { get; set; }
        public Int32? hfEmpProjectid { get; set; }

        public Int32? Location_Id { get; set; }
        public string DisplayOrder { get; set; }

        public int EmpParameterId { get; set; }

        public int? ParameterId { get; set; }

        public string ParameterValue { get; set; }

        public int? ShiftId { get; set; }

        [Required(ErrorMessage = "*")]
        public Int32? CompanyId { get; set; }

        [StringLength(30, ErrorMessage = "Max length is 30.")]
        [RegularExpression(@"^([A-Za-z0-9\/]*)$", ErrorMessage = "Invalid PF Account No.")]
        public string PfAccountNo { get; set; }
        public string RefName1 { get; set; }
        public string RefName2 { get; set; }
        public string Refpos1 { get; set; }
        public string Refpos2 { get; set; }
        public string Refcompany1 { get; set; }
        public string Refcompany2 { get; set; }
        public string Refadd1 { get; set; }
        public string Refadd2 { get; set; }
        public string Refmob1 { get; set; }
        public string Refempcode1 { get; set; }
        public string Refempcode2 { get; set; }
        public string Refmob2 { get; set; }
        public string emergencycontact1 { get; set; }
        public string emergencycontact2 { get; set; }


        [StringLength(30, ErrorMessage = "Max length is 30.")]
        [RegularExpression(@"^([A-Za-z0-9\/]*)$", ErrorMessage = "Invalid ESI Account No.")]
        public string EsiAccountNo { get; set; }

        
        [RegularExpression(@"^([A-Z]{5}\d{4}[A-Z]{1} *)$", ErrorMessage = "Invalid Pan No.")]
        public string TdsAccountNo { get; set; }

        [StringLength(12, ErrorMessage = "Max length is 12.")]
      
        [RegularExpression(@"^([\d{12}]*)$", ErrorMessage = "Invalid UAN No.")]
        public string UANNo { get; set; }

        public string hfEmpName { get; set; }
        public string AddressRes { get; set; }
        [StringLength(200, ErrorMessage = "Max length is 200.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Address")]
        public string AddressRes2 { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        //   [Required(ErrorMessage = "City is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid City Name")]
        public string CityRes { get; set; }
        [StringLength(50, ErrorMessage = "Max length is 50.")]

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid District Name")]
        public string DistrictRes { get; set; }

        //  [Required(ErrorMessage = "State is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid State Name")]
        public string RegionRes { get; set; }
        [Range(100000, 999999, ErrorMessage = "Enter 100000 to 999999")]

        //  [Required(ErrorMessage = "Pincode is Required.")]
        [DataType(DataType.PostalCode)]
        public Int64? PincodeRes { get; set; }
        public bool? IsPolice { get; set; }
        [StringLength(12, ErrorMessage = "Max length is 12.")]

        [RegularExpression(@"^([\d{12}]*)$", ErrorMessage = "Invalid Adhar No.")]
        public string AdharNo { get; set; }
       
        // [Required(ErrorMessage = "Mobile is Required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid number")]
        public Int64? MobileNoRes { get; set; }
        public int? EmpCategoryId { get; set; }
        public bool? IsOnProbation { get; set; }
        public DateTime? ProbationStartDate { get; set; }
        public DateTime? ProbationEndDate { get; set; }
        public string Hobbies { get; set; }

        public List<SelectListItem> AttendanceSourceList { get; set; }
        public List<SelectListItem> TitleList { get; set; }
        public List<SelectListItem> BankList { get; set; }
        public List<SelectListItem> EmployeeCategoryList { get; set; }
        public List<SelectListItem> ReportingOfficerList { get; set; }
        public List<SelectListItem> PayMethodList { get; set; }
        public List<SelectListItem> ShiftList { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public List<SelectListItem> EmpParameterDetailList { get; set; }
        public List<SelectListItem> EmpCategoryList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesignationList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> ProjectList
        {
            get; set;
        }
        public List<SelectListItem> Relationlist { get; set; }


        public List<SelectListItem> LeavingReasonList { get; set; }
            public List<SelectListItem> ESILeavingReasonList { get; set; }

            public EmployeeViewModel()
        {
            try
            {

            }
            catch (Exception ex)
            { }
        }

        public EmployeeViewModel(int? cid)
        {


            try
            {
                Relationlist = new List<SelectListItem>();
                DataTable dtRelation = obj.EmpFamilyRelationGet();
                Relationlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtRelation.Rows)
                {
                    Relationlist.Add(new SelectListItem
                    {

                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }
                TitleList = new List<SelectListItem>();
                DataTable dt = obj.TitleGet();
                TitleList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dt.Rows)
                {
                    TitleList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

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

                EmployeeCategoryList = new List<SelectListItem>();
                DataTable dtEmpCategoryList = obj.EmpTypeGet(null);
                EmployeeCategoryList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmpCategoryList.Rows)
                {
                    EmployeeCategoryList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }



                LeavingReasonList = new List<SelectListItem>();
                DataTable dtLeavingReasonList = obj.LeavingReasonGet("PF");
                LeavingReasonList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLeavingReasonList.Rows)
                {
                    LeavingReasonList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                ESILeavingReasonList = new List<SelectListItem>();
                DataTable dtESILeavingReasonList = obj.LeavingReasonGet("ESI");
                ESILeavingReasonList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtESILeavingReasonList.Rows)
                {
                    ESILeavingReasonList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }


                ReportingOfficerList = new List<SelectListItem>();
                DataTable dtReportingOfficerList = obj.EmployeeInfoGet();
                ReportingOfficerList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtReportingOfficerList.Rows)
                {
                    ReportingOfficerList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                PayMethodList = new List<SelectListItem>();
                DataTable dtPayMethodList = obj.PaymentMethodGet();
                PayMethodList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayMethodList.Rows)
                {
                    PayMethodList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                EmpParameterDetailList = new List<SelectListItem>();
                DataTable dtEmpParameterDetailList = obj.EmpParameterGet();
                EmpParameterDetailList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmpParameterDetailList.Rows)
                {
                    EmpParameterDetailList.Add(new SelectListItem
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
                if (dtCompanyList.Rows.Count == 1)
                {
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
                }
                else
                {
                    CompanyList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                    foreach (DataRow dr in dtCompanyList.Rows)
                    {
                        if (cid == Convert.ToInt32(dr[0]))
                        {
                            CompanyList.Add(new SelectListItem
                            {
                                Selected = true,
                                Text = dr[1].ToString(),
                                Value = dr[0].ToString()
                            });
                        }
                        else
                        {
                            CompanyList.Add(new SelectListItem
                            {
                                Text = dr[1].ToString(),
                                Value = dr[0].ToString()
                            });
                        }
                    }
                }
                EmpCategoryList = new List<SelectListItem>();
                DataTable dtEmployeeCategory = obj.EmpCategoryGet();
                EmpCategoryList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeeCategory.Rows)
                {
                    EmpCategoryList.Add(new SelectListItem
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

                    AttendanceSourceList.Add(new SelectListItem
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
        public string EmployeeCreateExcel(List<EmployeeViewModel> model)
        {
            string flag = "0";
            try
            {
               for(int i=0;i< model.Count; i++)
                {
                byte[] byteImage = null;
                if (!(string.IsNullOrEmpty(model[i].imagePath)))
                {
                    byteImage = Convert.FromBase64String(model[i].imagePath);
                }

                if (byteImage == null)
                {
                        model[i].EmpPhoto = null;
                }
                else
                {
                        model[i].EmpPhoto = byteImage;
                }
                    model[i].LoginId = model[i].EmpCode;
                    model[i].Password = model[i].EmpCode;
                if (model[i].CompanyId == null)
                {

                        model[i].CompanyId = model[i].hfCompany_id;
                }
                   
                        DataTable dtEmployeeGetOld = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);
                    
int? status = ObjML.EmployeeCreate(model[i].EmpCode, model[i].EmpName, model[i].FirstName, model[i].LastName, model[i].Sex, model[i].Dob == DateTime.MinValue ? null : model[i].Dob,
                    model[i].Doj == DateTime.MinValue ? null : model[i].Doj, model[i].Dol==DateTime.MinValue?null: model[i].Dol, model[i].TitleId, model[i].Education, model[i].EmailAddress, model[i].EmpBankId,
                    model[i].BankAccountNo, model[i].MajorProjectNo, model[i].Trainee, model[i].WorkDivision, model[i].Notes, model[i].Alias1, model[i].Alias2,
                    model[i].Alias3, model[i].AliasLike, model[i].LoginId, model[i].IsAttendanceRequired, model[i].Password, model[i].PayMethodId,
                    model[i].MgrId == 0? null : model[i].MgrId, model[i].Community, model[i].Caste, model[i].Religion, model[i].IdenMks1, model[i].IdenMks2, model[i].Height,
                    model[i].Weight, model[i].BloodGroup, model[i].Dor, model[i].EmpTypeId, model[i].EmpPhoto, model[i].LeavingReasonId,
                    model[i].Nationality, model[i].MaritalStatus, model[i].EmpOrder, model[i].FatherHusbandName, model[i].MotherName,
                    model[i].ReservationCategory, Convert.ToString(model[i].HandicapPercent), model[i].HandicapNotes, model[i].GisNo, model[i].IsHandicap, model[i].ResumeCopy,
                    model[i].ResumeContentType, model[i].ResumeName, model[i].Relationship, model[i].Address1, model[i].Address2, model[i].City,
                    model[i].District, model[i].Region, model[i].Pincode, model[i].MobileNo, model[i].IsInternationalWorker, model[i].EmpBankFlag,
                    model[i].EmpBankName, model[i].BankIfscCode, model[i].MiddleName, model[i].ShiftId, model[i].CompanyId, model[i].PfAccountNo,
                    model[i].EsiAccountNo, model[i].TdsAccountNo, model[i].IsOnProbation, model[i].ProbationStartDate, model[i].ProbationEndDate, model[i].EmpCategoryId,
                    model[i].RetirementDate, model[i].AttendanceSourceId, model[i].ESILeavingReasonId,
                    Convert.ToString(model[i].UANNo), null, null, null, model[i].AdharNo, model[i].passportNo, model[i].LegalCopy, model[i].drivinglicence, model[i].IsPolice, model[i].poilceverification, model[i].AddressRes, model[i].AddressRes2,
                     model[i].CityRes, model[i].DistrictRes, model[i].RegionRes, model[i].PincodeRes, model[i].MobileNoRes, model[i].EmailAddressRes,
                     model[i].emergencycontact1, model[i].emergencycontact2, model[i].DLdateofIssue, model[i].DLdateofExpiry, model[i].ComputingItemList, model[i].DLIssueAutority, model[i].ColorBlind, model[i].WearSpectacles, model[i].sufferphy,
                     model[i].PassportdateofIssue, model[i].passportdateofExpiry, model[i].PassportPlaceofissue, model[i].PassportAddress, model[i].ComputingItemList1,
                     model[i].chewtab, model[i].smoking, model[i].RefName1, model[i].Refpos1, model[i].Refcompany1, model[i].Refadd1, model[i].Refmob1, model[i].RefName2, model[i].Refpos2, model[i].Refcompany2, model[i].Refadd2, model[i].Refmob2,
                     model[i].Refempcode1, model[i].Refempcode2,
                     model[i].tenC, model[i].tweleveC, model[i].gradC, model[i].PGC, model[i].RPC, model[i].DLC, model[i].ICC, model[i].PassC,
  model[i].LCJC, model[i].ProfC, model[i].PanC, model[i].bankC, model[i].AdharC, model[i].PoliceC);

                    
 
                     DataTable dtEmployeeGet = obj.EmployeeDetailsGet(null, null, null, null, null, null, null, null);                  

                    var diffName = dtEmployeeGet.AsEnumerable().Select(r => r.Field<int>("employee_id")).Except(dtEmployeeGetOld.AsEnumerable().Select(r => r.Field<int>("employee_id")));
                    if (diffName.Any())
                    {
                        DataTable _result = (from row in dtEmployeeGet.AsEnumerable()
                                             join name in diffName
                                             on row.Field<int>("employee_id") equals name
                                             select row).CopyToDataTable();
                        model[i].EmployeeId = Convert.ToInt32(_result.Rows[0][0]);

                        int? status1 = obj.EmpProjectCreate(model[i].EmployeeId, model[i].ProjectId, model[i].DepartmentId, model[i].DesignationId, model[i].LocationId, model[i].Doj == DateTime.MinValue ? null : model[i].Doj, model[i].Dol == DateTime.MinValue ? null : model[i].Dol);

                    }
                    int? Exist = obj.EmpShiftExistsValidate(model[i].EmployeeId, model[i].ShiftId, model[i].Doj == DateTime.MinValue ? null : model[i].Doj, model[i].Dol == DateTime.MinValue ? null : model[i].Dol, null);
                    if (Exist == 0)
                    {
                        int? checkshift = obj.EmpShiftCreate(model[i].EmployeeId, model[i].ShiftId, model[i].Doj == DateTime.MinValue ? null : model[i].Doj, model[i].Dol == DateTime.MinValue ? null : model[i].Dol, null, null);
                        
                    }
                    //int? status1 = obj.EmpProjectCreate(model[i].EmployeeId, model[i].ProjectId, model[i].DepartmentId, model[i].DesignationId, model[i].LocationId, model[i].Doj, model[i].Dol);

                }


                return flag;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public string EmployeeCreate(EmployeeViewModel model)
        {
            string flag = "0";
            try
            {
                byte[] byteImage = null;
                if (!(string.IsNullOrEmpty(model.imagePath)))
                {
                    byteImage = Convert.FromBase64String(model.imagePath);
                }

                if (byteImage == null)
                {
                    model.EmpPhoto = null;
                }
                else
                {
                    model.EmpPhoto = byteImage;
                }
                model.LoginId = model.EmpCode;
                model.Password = model.EmpCode;
                if (model.CompanyId == null)
                {

                    model.CompanyId = model.hfCompany_id;
                }

              
                int? status = ObjML.EmployeeCreate(model.EmpCode, model.EmpName, model.FirstName, model.LastName, model.Sex, model.Dob,
                    model.Doj, model.Dol, model.TitleId, model.Education, model.EmailAddress, model.EmpBankId,
                    model.BankAccountNo, model.MajorProjectNo, model.Trainee, model.WorkDivision, model.Notes, model.Alias1, model.Alias2,
                    model.Alias3, model.AliasLike, model.LoginId, model.IsAttendanceRequired, model.Password, model.PayMethodId,
                    model.MgrId, model.Community, model.Caste, model.Religion, model.IdenMks1, model.IdenMks2, model.Height,
                    model.Weight, model.BloodGroup, model.Dor, model.EmpTypeId, model.EmpPhoto, model.LeavingReasonId,
                    model.Nationality, model.MaritalStatus, model.EmpOrder, model.FatherHusbandName, model.MotherName,
                    model.ReservationCategory, Convert.ToString(model.HandicapPercent), model.HandicapNotes, model.GisNo, model.IsHandicap, model.ResumeCopy,
                    model.ResumeContentType, model.ResumeName, model.Relationship, model.Address1, model.Address2, model.City,
                    model.District, model.Region, model.Pincode, model.MobileNo, model.IsInternationalWorker, model.EmpBankFlag,
                    model.EmpBankName, model.BankIfscCode, model.MiddleName, model.ShiftId, model.CompanyId, model.PfAccountNo,
                    model.EsiAccountNo, model.TdsAccountNo, model.IsOnProbation, model.ProbationStartDate, model.ProbationEndDate, model.EmpCategoryId, model.RetirementDate, model.AttendanceSourceId,model.ESILeavingReasonId,Convert.ToString(model.UANNo)
                    ,null,null,null,model.AdharNo,model.passportNo,model.LegalCopy,model.drivinglicence,model.IsPolice,model.poilceverification,model.AddressRes,model.AddressRes2,
                    model.CityRes,model.DistrictRes,model.RegionRes,model.PincodeRes,model.MobileNoRes,model.EmailAddressRes,
                    model.emergencycontact1,model.emergencycontact2,model.DLdateofIssue,model.DLdateofExpiry,model.ComputingItemList,model.DLIssueAutority,model.ColorBlind,model.WearSpectacles,model.sufferphy,
                    model.PassportdateofIssue,model.passportdateofExpiry,model.PassportPlaceofissue,model.PassportAddress,model.ComputingItemList1,
                    model.chewtab,model.smoking,model.RefName1,model.Refpos1,model.Refcompany1,model.Refadd1,model.Refmob1, model.RefName2, model.Refpos2, model.Refcompany2, model.Refadd2, model.Refmob2,
                    model.Refempcode1, model.Refempcode2,
                    model.tenC , model.tweleveC , model.gradC , model.PGC , model.RPC , model.DLC , model.ICC , model.PassC ,
 model.LCJC , model.ProfC , model.PanC , model.bankC , model.AdharC , model.PoliceC );

                return flag;

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string EmployeeUpdate(EmployeeViewModel model)
        {
            string flag = "0";
            try
            {
                byte[] byteImage = null;
                if (!(string.IsNullOrEmpty(model.imagePath)))
                {
                    byteImage = Convert.FromBase64String(model.imagePath);
                }
                else
                {
                    DataTable dt = obj.EmployeePhotoGet(model.EmployeeId);
                    if (dt.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[0]["emp_photo"].ToString()))
                        {
                            byteImage = null;
                        }
                        else
                        {
                            byteImage = (byte[])dt.Rows[0]["emp_photo"];
                        }
                    }
                }

                model.EmpPhoto = byteImage;

                model.LoginId = model.EmpCode;
                model.Password = model.EmpCode;
                model.EmpTypeId = model.EmpType_Id;

                

                if (model.CompanyId == null)
                {

                    model.CompanyId = model.hfCompany_id;
                }
               
               
    //int? status = obj.EmployeeUpdate(
    //model.EmployeeId, model.EmpCode, model.EmpName, model.FirstName, model.LastName, model.Sex, model.Dob,
    //model.Doj2, model.Dol, model.TitleId, model.Education, model.EmailAddress, model.EmpBankId,
    //model.BankAccountNo, model.MajorProjectNo, model.Trainee, model.WorkDivision, model.Notes, model.Alias1, 
    //model.Alias2,model.Alias3, model.AliasLike, model.LoginId, model.IsAttendanceRequired, model.Password,
    //model.PayMethodId,model.MgrId, model.Community, model.Caste, model.Religion, model.IdenMks1,
    //model.IdenMks2, model.Height,model.Weight, model.BloodGroup, model.Dor, model.EmpTypeId, 
    //model.EmpPhoto, model.LeavingReasonId,model.Nationality, model.MaritalStatus, model.EmpOrder, 
    //model.FatherHusbandName, model.MotherName,model.ReservationCategory, Convert.ToString(model.HandicapPercent),
    //model.HandicapNotes, model.GisNo, model.IsHandicap, model.ResumeCopy,model.ResumeContentType, model.ResumeName,
    //model.Relationship, model.Address1, model.Address2, model.City,model.District, model.Region, model.Pincode,
    //model.MobileNo, model.IsInternationalWorker, model.EmpBankFlag,model.EmpBankName, model.BankIfscCode,
    //model.MiddleName, model.ShiftId, model.CompanyId, model.PfAccountNo, model.EsiAccountNo, model.TdsAccountNo,
    //model.IsOnProbation, model.ProbationStartDate, model.ProbationEndDate, model.EmpCategoryId, model.RetirementDate, 
    //model.AttendanceSourceId,model.ESILeavingReasonId,Convert.ToString(model.UANNo));
   
        int? statuss = ObjML.EmployeeUpdate(model.EmployeeId,
        model.EmpCode, model.EmpName, model.FirstName, model.LastName,model.Sex, model.Dob, model.Doj, model.Dol, model.TitleId, model.Education, 
        model.EmailAddress, model.EmpBankId,model.BankAccountNo, model.MajorProjectNo, model.Trainee, model.WorkDivision, model.Notes, model.Alias1, model.Alias2, model.Alias3,
        model.AliasLike, model.LoginId, model.IsAttendanceRequired, model.Password,model.PayMethodId, model.MgrId, model.Community, model.Caste, model.Religion, model.IdenMks1,
        model.IdenMks2, model.Height, model.Weight, model.BloodGroup, model.Dor, model.EmpTypeId,model.EmpPhoto, model.LeavingReasonId, model.Nationality, model.MaritalStatus, 
        model.EmpOrder,model.FatherHusbandName, model.MotherName, model.ReservationCategory, Convert.ToString(model.HandicapPercent),model.HandicapNotes, model.GisNo, model.IsHandicap, model.ResumeCopy, model.ResumeContentType,
        model.ResumeName,model.Relationship, model.Address1, model.Address2, model.City, model.District, model.Region, model.Pincode,
        model.MobileNo, model.IsInternationalWorker, model.EmpBankFlag, model.EmpBankName, model.BankIfscCode,
        model.MiddleName, model.ShiftId, model.CompanyId, model.PfAccountNo, model.EsiAccountNo, model.TdsAccountNo,
        model.IsOnProbation, model.ProbationStartDate, model.ProbationEndDate, model.EmpCategoryId, model.RetirementDate,
        model.AttendanceSourceId, model.ESILeavingReasonId, Convert.ToString(model.UANNo),model.AdharNo, model.passportNo, 
        model.LegalCopy, model.drivinglicence, model.IsPolice, model.poilceverification, model.AddressRes, model.AddressRes2,
        model.CityRes, model.DistrictRes, model.RegionRes, model.PincodeRes, model.MobileNoRes, model.EmailAddressRes,
        model.emergencycontact1, model.emergencycontact2, model.DLdateofIssue, model.DLdateofExpiry, model.ComputingItemList, model.DLIssueAutority, model.ColorBlind, model.WearSpectacles, model.sufferphy,
                    model.PassportdateofIssue, model.passportdateofExpiry, model.PassportPlaceofissue, model.PassportAddress, model.ComputingItemList1,
                    model.chewtab, model.smoking, model.RefName1, model.Refpos1, model.Refcompany1, model.Refadd1, model.Refmob1, model.RefName2, model.Refpos2, model.Refcompany2, model.Refadd2, model.Refmob2, model.Refempcode1, model.Refempcode2,
                    model.tenC, model.tweleveC, model.gradC, model.PGC, model.RPC, model.DLC, model.ICC, model.PassC,
 model.LCJC, model.ProfC, model.PanC, model.bankC, model.AdharC, model.PoliceC);
        return flag;

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }

    //--------for employee Department-----------
    public class EmpProjectViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpProjectId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        public Int32? EmployeeId2 { get; set; }

        public Int32? ProjectId { get; set; }

        public Int32? Project_Id { get; set; }

        public Int32? DepartmentId { get; set; }

        public Int32? Department_Id { get; set; }

        public Int32? DesignationId { get; set; }

        public Int32? Designation_Id { get; set; }

        public Int32? LocationId { get; set; }

        public Int32? Location_Id { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDt { get; set; }

        public DateTime? EndDt { get; set; }

        public DateTime? StartDt2 { get; set; }

        public DateTime? EndDt2 { get; set; }

        public DateTime? attendance_last_end_date { get; set; }

        public DateTime? salary_last_end_date { get; set; }



        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesignationList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }

        public EmpProjectViewModel()
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
            catch (Exception Ex)
            {

            }
        }



    }
    
    //-------- For Emp Salary ------------------
    public class EmpSalaryViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public IEnumerable<EmpSalaryViewModel> EmployeeDataList { get; set; }
        public int DArate { get; set; }
        public Int32? EmpSalaryId { get; set; }
        public Int32? EmpId { get; set; }
        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }
        public Int32? Employee_Id { get; set; }
        public string PayPeriodCycle { get; set; }


        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDt { get; set; }

        public DateTime? EndDt { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        [Required(ErrorMessage = "Gross is Required.")]
        public decimal? Gross { get; set; }

        public decimal? hfGross { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        [Required(ErrorMessage = "Gross is Required.")]
        public decimal? Gross_group { get; set; }

        public decimal? hfGross_group { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        //  [Required(ErrorMessage = "CTC is Required.")]
        [Required(ErrorMessage = "CTC is Required.")]
        public decimal? Ctc { get; set; }

        public decimal? hfCtc { get; set; }

        [Required(ErrorMessage = "CTC is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Ctc_group { get; set; }

        public decimal? hfCtc_group { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]



        public decimal? ActualCtc { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? FixedCtc { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]

        public decimal? SpecialBonus { get; set; }


        [Required(ErrorMessage = "Amount is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? ArearAmount { get; set; }


        [Required(ErrorMessage = "Amount is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? ItemArearAmount { get; set; }

        public Int32? PayrollEntryMethod { get; set; }

        public Int32? PayrollFunctionId { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Salary Group is Required.")]
        public Int32? EmpSalaryGroupId { get; set; }
        public Int32? EmpSalaryGroup_Id { get; set; }
        public bool IsPfApplicable { get; set; }

        public bool? IsEsiApplicable { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? VariablePayPercentage { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? VariableCtc { get; set; }
        //variable salary models start---------
        public Int32? EmpVariableSalaryId { get; set; }

       [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? VarEmployeeId { get; set; }
        [Required(ErrorMessage = "Payroll Item  is Required.")]
        public Int32? VarPayrollItem_Id { get; set; }
        
        public string VarPayPeriod_Cycle { get; set; }

        [Required(ErrorMessage = "Amount is Required.")]
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? VarAmount { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? VarStartDt { get; set; }

        public DateTime? VarEndDt { get; set; }

        public string VarNotes { get; set; }

      

     
        public bool IsAttendanceApplicable { get; set; }

        //----------------end----------
        public List<SelectListItem> SalaryGroupList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> PayrollItemList { get; set; }
        public List<SelectListItem> VarPayrollItemList { get; set; }
        public List<SelectListItem> PayrollFunctionList { get; set; }

        public EmpSalaryViewModel()
        {

            try
            {


                SalaryGroupList = new List<SelectListItem>();
                DataTable dtSalaryGroup = obj.EmpSalaryGroupGet();
                SalaryGroupList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtSalaryGroup.Rows)
                {
                    SalaryGroupList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }



                EmployeeList = new List<SelectListItem>();
                DataTable dtEmployeeList = obj.EmployeeInfoGet();
                // DataTable dtEmployeeList = obj.SalaryEmployeeInfoGet();
                EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeeList.Rows)
                {
                    EmployeeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                PayrollItemList = new List<SelectListItem>();
                DataTable dtPayrollItemList = obj.PayrollSalaryItemGet();
                PayrollItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPayrollItemList.Rows)
                {
                    PayrollItemList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                VarPayrollItemList = new List<SelectListItem>();
                DataTable dtVarPayrollItemList = obj.PayrollVariableItemGet();
                VarPayrollItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtVarPayrollItemList.Rows)
                {
                    VarPayrollItemList.Add(new SelectListItem
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
            }
            catch (Exception Ex)
            {


            }
        }
      

        //-- Employee Salary Item Model -----------------

        public Int32? EmpSalaryItemId { get; set; }

        public Int32? EmpSalaryIdItem { get; set; }

        [Required(ErrorMessage = "Payroll Item is Required.")]
        public Int32? PayrollItemId { get; set; }
        public Int32? PayrollSalaryItemId { get; set; }
        [Required(ErrorMessage = "Payroll Value is Required.")]
        public Int32? Payroll_ValueType { get; set; }
        public Int32? PayrollValueType { get; set; }
        public decimal? RateValue { get; set; }
        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(1, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Rate { get; set; }

        public Int32? PayrollFunctionIdItem { get; set; }

        public decimal? PayrollItemValue { get; set; }
        [Required(ErrorMessage = "Start Date is Required.")]
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? StartDtItem { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? EndDtItem { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? PayStartDt { get; set; }

        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? PayEndDt { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string NotesItem { get; set; }

        public Int32? EmployeeIdSalaryItem { get; set; }

        public Int32? EmpSalaryGroupIdItem { get; set; }

        public Int32? PayrollTypeId { get; set; }

        public bool? IsOverridable { get; set; }


        public bool IsCTC { get; set; }



    }
      //-- Employer Salary Detail view Model -----------------
    public class EmployerSalaryDetailViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        [Required(ErrorMessage = "Location is Required.")]
        public Int32? LocationId { get; set; }
        public Int32? CompanyId { get; set; }
        [Required(ErrorMessage = "PayType is Required.")]
        public string PayType { get; set; }
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? StartDate { get; set; }
        [RegularExpression(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$", ErrorMessage = "Date Format Should be dd/mm/yyyy")]
        public DateTime? EndDate { get; set; }
        public Int32? EmployersalaryDetailid { get; set; }
        [Required(ErrorMessage = "Amount is Required.")]
        public decimal? Amount { get; set; }
        public Int32? MonthId { get; set; }
        public Int32? YearId { get; set; }
        public string desciption { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public List<SelectListItem> PayTypeList { get; set; }

        public EmployerSalaryDetailViewModel()
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

            PayTypeList = new List<SelectListItem>();
            PayTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            PayTypeList.Add(new SelectListItem { Text = "Monthly", Value = "1" });
            PayTypeList.Add(new SelectListItem { Text = "Quarterly", Value = "2" });
            PayTypeList.Add(new SelectListItem { Text = "Half Yearly", Value = "3" });
            PayTypeList.Add(new SelectListItem { Text = "Yearly", Value = "4" });
            

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
        }
    }
    public class EmpTdsDetailViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpTdsDetailId { get; set; }

        [Required(ErrorMessage = "Employee is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public string FiscalYear { get; set; }

        public bool? IsMetro { get; set; }

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
        public decimal? InfraBond { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? ChildEduFund { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? MutualFund { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Fd { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? InterestOnDeposit { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.99, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? RoyaltyIncomeDeduction { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? RoyaltyPatentDeduction { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? UniformExemption { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Pf { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? HouseLoanPrincipalRepay { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? InsurancePremium { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? SavingbankInterest { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? SavingbankInterestException { get; set; }

        [RegularExpression(@"^(\d+(\.\d{0,2})?)$", ErrorMessage = "Allow Only Two Digit After Decimal")]
        [Range(0, 9999999999999999.00, ErrorMessage = "value should be 1 to 16 digit no.")]
        public decimal? Rajivgandhisavingsscheme { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> FiscalYearList { get; set; }

        public EmpTdsDetailViewModel()
        {
            try
            {
                EmployeeList = new List<SelectListItem>();
                DataTable dtEmployeeInfoGet = obj.EmployeeInfoGet();
                EmployeeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtEmployeeInfoGet.Rows)
                {
                    EmployeeList.Add(new SelectListItem
                    {
                        Text = dr[1].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                FiscalYearList = new List<SelectListItem>();
                DataTable dtFiscalYearGet = obj.FinancialYearGet(null);
                FiscalYearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtFiscalYearGet.Rows)
                {
                    FiscalYearList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });
                }
            }
            catch (Exception Ex)
            {

            }
        }

    }

    //-----------employee daily attendance------

    public class EmpDailyAttendanceViewModel : PayrollUtil
    {

        public List<EmpDailyAttendanceViewModel> EmployeeDataList { get; set; }


        public Boolean isCheck { get; set; }
        public Int32? EmpDailyAttendanceId { get; set; }



        public DateTime? AttendanceDate { get; set; }
        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? Start_Date { get; set; }
        [Required(ErrorMessage = "End Date is Required.")]
        public DateTime? End_Date { get; set; }
        public TimeSpan? InTime { get; set; }

        public TimeSpan? OutTime { get; set; }

        public TimeSpan? WorkedHours { get; set; }

        public decimal? WorkDay { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Remarkd")]

        public string Remarks { get; set; }

        public Int32? DurationUnits { get; set; }

        public decimal? WorkedUnits { get; set; }

        public decimal? PayableUnits { get; set; }

        public string AttendanceStatus { get; set; }

        public decimal? OvertimeHours { get; set; }

    }

    //-----------attendance entry------------

    public class EmpAttendanceEntryViewModel : PayrollUtil
    {
        public Int32? EmpAttendanceEntryId { get; set; }

        public List<EmpAttendanceEntryViewModel> EmployeeDataList { get; set; }

        public string PayPeriodCycle { get; set; }
        public Boolean isCheck { get; set; }
        public Int32? PayPeriod { get; set; }

        public Int32? PayYear { get; set; }

        public Int32? AttendanceType { get; set; }
        public Int32? PayrollId { get; set; }

        public string Workunit { get; set; }

        public decimal? WorkqtyEstimated { get; set; }

        //[Required(ErrorMessage = "Worked Days is Required.")]
        //[Range(.5, 31, ErrorMessage = "Worked Days should be greater than Zero.")]
        public decimal? WorkqtyActual { get; set; }
        public decimal? WorkedUnit { get; set; }
        public decimal? WorkedUnitValue { get; set; }
        public decimal? LocalDay { get; set; }
        public decimal? NonLocalDay { get; set; }

        public decimal? OvertimeEstimated { get; set; }

        public decimal? OvertimeActual { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public string AttachmentFilename { get; set; }

        public byte[] AttachmentObject { get; set; }

        public DateTime? EntryDate { get; set; }

        public string EntryUser { get; set; }

        public Int32? EmpAttendanceId { get; set; }

        public bool? EntryLock { get; set; }

        public Int32? AttendanceSource { get; set; }





        // Upload Attendance Properties
        public Int32? Employee_Id { get; set; }
        public Int32? Year_Id { get; set; }
        public Int32? Month_Id { get; set; }
        public Int32? AttendanceSource_Id { get; set; }
        public Int32? UploadAttendance { get; set; }
        public string imageBase64String { get; set; }
        public string imageName { get; set; }
        public byte[] imageByte { get; set; }

        public Int32? hfEmployee_Id { get; set; }
        public Int32? hfYear_Id { get; set; }
        public Int32? hfMonth_Id { get; set; }
        public Int32? hfAttendanceSource_Id { get; set; }
        public Int32? hfUploadAttendance { get; set; }
        public string hfimageBase64String { get; set; }
        public string hfimageName { get; set; }
        public byte[] hfimageByte { get; set; }



    }


    //-----------Monthly attendance entry------------


    public class EmpMonthlyAttendanceEntryViewModel : PayrollUtil
    {
        public Int32? EmpMonthlyAttendanceEntryId { get; set; }

        public List<EmpMonthlyAttendanceEntryViewModel> EmployeeDataList { get; set; }

        public string PayPeriodCycle { get; set; }
        public Boolean isCheck { get; set; }
        public Int32? PayPeriod { get; set; }

        public Int32? PayYear { get; set; }

        public Int32? AttendanceType { get; set; }
        public Int32? PayrollId { get; set; }

        public string Workunit { get; set; }

        public decimal? WorkqtyEstimated { get; set; }

        //[Required(ErrorMessage = "Worked Days is Required.")]
        //[Range(.5, 31, ErrorMessage = "Worked Days should be greater than Zero.")]
        public decimal? WorkqtyActual { get; set; }
        public decimal? WorkedUnit { get; set; }
        public decimal? WorkedUnitValue { get; set; }
        public decimal? HalfDay { get; set; }
        public decimal? LeaveDay { get; set; }
        public decimal? PresentDays { get; set; }
        public decimal? OvertimeEstimated { get; set; }

        public decimal? OvertimeActual { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public string AttachmentFilename { get; set; }

        public byte[] AttachmentObject { get; set; }

        public DateTime? EntryDate { get; set; }

        public string EntryUser { get; set; }

        public Int32? EmpAttendanceId { get; set; }

        public bool? EntryLock { get; set; }

        public Int32? AttendanceSource { get; set; }





   

    }



    //-----------------------------
    public class EmpShiftViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpShiftId { get; set; }

        [Required(ErrorMessage = "Employee is Required.")]
        public int? EmployeeId { get; set; }

        public int? Employee_Id { get; set; }

        // [Required(ErrorMessage = "Shift is Required.")]
        public int? ShiftId { get; set; }

        [Required(ErrorMessage = "Start Date is Required.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> ShiftList { get; set; }

        public EmpShiftViewModel()
        {
            EmployeeList = new List<SelectListItem>();
            ShiftList = new List<SelectListItem>();

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
    }
}