using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;


namespace PoiseERP.Areas.Payroll.Models
{
    public class PISViewModel
    {
    }


    public class EmployeeDocumentviewModel
    {
     PoisePayrollServiceModel obj = new PoisePayrollServiceModel();   
      public Int32? DocumentId { get; set; }
      public Int32? EmployeeId { get; set; }
      public Int32? EmployeeDocumentId { get; set; }
      public string DocumentObjectName { get; set; }
      public string ErrorMsg { get; set; }
      public string Flag { get; set; }
     
      public HttpPostedFileBase File { get; set; }

      [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]
       public string Notes { get; set; }


      public byte[] DocumentImg { get; set; }
      public string strDocumentImg { get; set; }
      public string strContentType { get; set; }
      public string strDocumentName { get; set; }


      public List<SelectListItem> EmployeeList { get; set; }
      public List<SelectListItem> DocumentList { get; set; }

      public EmployeeDocumentviewModel()
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



           DocumentList = new List<SelectListItem>();

           DataTable dt1 = obj.DocumentCategoryGet();
           DocumentList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt1.Rows)
            {
                DocumentList.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
         }
    }


    public class EmpAddressViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpAddressId { get; set; }
        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Address Type is Required.")]
        public Int32? AddressTypeId { get; set; }

        [Required(ErrorMessage = "Address1 is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Address1")]       
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        public string Address1 { get; set; }


        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Address2")]       
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "City is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid City Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Required.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid State Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public Int64? Postcode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Invalid Country")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string Country { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid number")]
        public Int64? ContactPhone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid number")]
        public Int64? Mobile { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]       
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Notes { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> AddressTypelist { get; set; }


        public EmpAddressViewModel()
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

            AddressTypelist = new List<SelectListItem>();
            DataTable dtAddressTypelist = obj.AddressTypeGet();
            AddressTypelist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtAddressTypelist.Rows)
            {
                AddressTypelist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }
    }

    public class EmpIdentityProofViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpIdentityProofId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Voter Id")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Voter Id must  Should be 10 characters!")]
        public string VoterId { get; set; }

        [RegularExpression("^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$", ErrorMessage = "Invalid PAN No.")]
        public string PanNo { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Driving Lic No")]

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Driving Lic No must  Should be 12 characters!")]
        public string DrivingLicNo { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Passport No")]

        [StringLength(8, MinimumLength = 8, ErrorMessage = "Passport No  must  Should be 8 characters!")]
        public string PassportNo { get; set; }

        public DateTime? PassportValidDt { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Adhar Card No")]

        [StringLength(12, MinimumLength = 10, ErrorMessage = "Adhar Card No must Should be 12 characters!")]
        public string OtherIdProof { get; set; }
         [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Notes")]       
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Notes { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }

        public EmpIdentityProofViewModel()
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
        }
    }

    public class EmpEducationViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpEducationId { get; set; }

        

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        //[Required(ErrorMessage = "Employee Education is Required.")]
        [RegularExpression(@"^[a-zA-Z.]*$", ErrorMessage = "Invalid Education ")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string Education { get; set; }

        //[Required(ErrorMessage = "University Name is Required.")]
        [RegularExpression(@"^[a-zA-Z.]*$", ErrorMessage = "Invalid University Name")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]      
        public string UniversityName { get; set; }

        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\(\)\[\].\& ]*)$", ErrorMessage = "Invalid University Address")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string UniversityAddress { get; set; }

        //[Required(ErrorMessage = "Month of Passing is Required.")]
        public Int32? EducationYearE { get; set; }
        public Int32? EducationMonth { get; set; }

        //[Required(ErrorMessage = "Year Of Passing is Required.")]
        //[Range(1980, 2015, ErrorMessage = "Age should be minimum 1980 and not more than 2016")]
        public Int32? EducationYear { get; set; }
        [StringLength(3, ErrorMessage = "Max length is 3.")]
        public string EducationGrade { get; set; }

        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Invalid Document Id")]
        [Range(1, 99999999, ErrorMessage = "Document Id should be greater than Zero .")]
        public Int32? EmpDocumentId { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> Monthlist { get; set; }

        public List<SelectListItem> YearList { get; set; }

        public EmpEducationViewModel()
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


            Monthlist = new List<SelectListItem>();
            DataTable dtMonthlist = obj.MonthGet(null);
            Monthlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtMonthlist.Rows)
            {
                Monthlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }



            YearList = new List<SelectListItem>();
            DataTable dtYearList = obj.YearGet(25, 3);
            YearList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtYearList.Rows)
            {
                YearList.Add(new SelectListItem
                {
                    Text = dr[0].ToString(),
                    Value = dr[0].ToString()
                });
            }



        }



    }

    public class EmpFamilyDetailsViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpFamilyDetailsId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Family Member name is Required.")]
        [StringLength(30, ErrorMessage = "Max length is 30 .")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid Family Member Name ")]
        public string FamilyMemberName { get; set; }
        [StringLength(12, ErrorMessage = "Max length is 12.")]

        [RegularExpression(@"^([\d{12}]*)$", ErrorMessage = "Invalid Adhar No.")]
        public string FamilyMembeAdhaarNo { get; set; }


        public DateTime? FamilyMemberDob { get; set; }

        [Required(ErrorMessage = "Relationship is Required.")]
        public Int32? FamilyRelationId { get; set; }

        [Required(ErrorMessage = "Dependent is Required.")]
        public bool IsFamilyDependent { get; set; }

        [Required(ErrorMessage = "Age is Required.")]
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Age format is wrong")]

        [Range(1, 100, ErrorMessage = "Age should be minimum 1 and not more than 100")]

        public Int32? FamilyMemberAge { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }

        public List<SelectListItem> Relationlist { get; set; }

        public EmpFamilyDetailsViewModel()
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
        }
    }

    public class EmpNomineeDetailsViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? NomineeId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]

        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Nominee  Name is Required.")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid Nominee Name ")]
        public string NomineeName { get; set; }

         [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Nominee Address")]
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string NomineeAddress { get; set; }

        public DateTime? NomineeDob { get; set; }

        [Required(ErrorMessage = "Nominee Relation is Required.")]
        public Int32? NomineeRelationId { get; set; }
        
        [Required(ErrorMessage = "Nominee Sex is Required.")]
        public string NomineeSex { get; set; }
      
        public string MaritalStatus { get; set; }

        [RegularExpression(@"^([0-9.]*)$", ErrorMessage = "Invalid Share Amount")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Share Amount be Less than 999999999999999.99 .")]
        public decimal? ShareAmount { get; set; }

        public bool? IsMinor { get; set; }
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid Guardian Name")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string GuardianName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Guardian Address")]
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string GuardianAddress { get; set; }


        public Int32? GuardianRelationId { get; set; }


        
        public string GuardianSex { get; set; }

         public DateTime? GuardianDob { get; set; }
         public bool? IsNominateForWidowPension { get; set; }

         [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Remarks")]
         [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Remarks { get; set; }
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Invalid Document Id")]
        [Range(1, 9999999999, ErrorMessage = "Document Id should be greater than Zero .")]

        public Int32? EmpDocumentId { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> Relationlist { get; set; }

        public EmpNomineeDetailsViewModel()
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
        }
    }

    public class EmpPreviousEmployerDetailsViewModel
    {


        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? EmpPreviousEmployerDetailsId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]

        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Company Name is Required.")]
        [RegularExpression(@"^[a-zA-Z.]*$", ErrorMessage = "Invalid Company Name")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Address is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Company Address")]        
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string CompanyAddress { get; set; }


        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Pf Accountno")]
        public string PfAccountno { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Esi Account no")]
        public string EsiAccountno { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Epf Office Name")]    
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string EpfOfficeName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Epf Office Address")]    
        [StringLength(50, ErrorMessage = "Max length is 50 ")]
        public string EpfOfficeAddress { get; set; }

        [Required(ErrorMessage = "Date of Joining is Required.")]
        public DateTime? Doj { get; set; }

        [Required(ErrorMessage = "Date of Leaving Required.")]
        public DateTime? Dol { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Reason Of Leaving")]       
        [StringLength(100, ErrorMessage = "Max Length is 100 .")]
        public string ReasonOfLeaving { get; set; }

        //public byte[]  EmpDocument { get; set; }
      
        [RegularExpression(@"^([0-9]*)$", ErrorMessage = "Invalid Experience Months")]
        [Range(1, 999, ErrorMessage = "Experience Months  should be greater than Zero .")]
        public Int32? ExperienceMonths { get; set; }
        public bool IsLastCompany { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }

        public EmpPreviousEmployerDetailsViewModel()
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
        }
    }

    public class EmpPromotionDemotionViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpPromoDemoId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Order is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid  Order No")]
        public string PromoDemoOrderNo { get; set; }

        [Required(ErrorMessage = "Order Date  is Required.")]
        public DateTime? PromoDemoOrderDate { get; set; }

        [Required(ErrorMessage = "Designation is Required.")]
        public Int32? DesignationId { get; set; }

        [Required(ErrorMessage = "From Date  is Required.")]
        public DateTime? PromoDemoFromDate { get; set; }

        public string PromoDemoType { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }

        public List<SelectListItem> Designationlist { get; set; }

        public EmpPromotionDemotionViewModel()
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

            Designationlist = new List<SelectListItem>();
            DataTable dtDesignationlist = obj.DesignationGet(true);
            Designationlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtDesignationlist.Rows)
            {
                Designationlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }
    }

    public class EmpTrainingViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpTrainingId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Training Name is Required.")]
        [RegularExpression(@"^([a-zA-Z]){1}([A-Za-z0-9\@\/\(\)\[\].\& ]*)$", ErrorMessage = "Invalid Training Name")]             
        [StringLength(30, ErrorMessage = "Max length is 30 .")]
        public string TrainingName { get; set; }

        [Required(ErrorMessage = "Training Go On is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Training Go No")]
        [StringLength(20, ErrorMessage = "Max length is 20 .")]
        public string TrainingGoNo { get; set; }


        [Required(ErrorMessage = "Training Go Date Required.")]
        public DateTime? TrainingGoDate { get; set; }


        [Required(ErrorMessage = "Training From Date is Required.")]
        public DateTime? TrainingFromDate { get; set; }

        [Required(ErrorMessage = "Training To Date is Required.")]
        public DateTime? TrainingToDate { get; set; }

        [Required(ErrorMessage = "Training Institute Name  is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Training Institute Name")]      
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string TrainingInstName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid  Training Institute Address")]      
        [StringLength(200, ErrorMessage = "Max length is 200 .")]
         public string TrainingInstAddress { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }

        public EmpTrainingViewModel()
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
        }
    }

    public class EmpTransferViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? EmpTransferId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Transfer Order No is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Order No")]
        public string TransferOrderNo { get; set; }

        [Required(ErrorMessage = "Transfer Order Date is Required.")]
        public DateTime? TransferOrderDate { get; set; }

        [Required(ErrorMessage = "Transfer From is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Transfer From")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string TransferFrom { get; set; }

        [Required(ErrorMessage = "Transfer To  is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Transfer To")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string TransferTo { get; set; }

        [Required(ErrorMessage = "Relieve Date  is Required.")]
        public DateTime? TransferDor { get; set; }

        public string TransferRelieveSession { get; set; }

        [Required(ErrorMessage = "Transfer Join On is Required.")]
        public DateTime? TransferJoinDate { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }

        public EmpTransferViewModel()
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

        }
    }

    public class EmpDeputationViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpDeputationId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmloyeeId { get; set; }

        [Required(ErrorMessage = "Order No is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Order No")]
        public string DeputationOrderNo { get; set; }

        [Required(ErrorMessage = "Order Date is Required.")]
        public DateTime? DeputationOrderDate { get; set; }

        public string DeputationType { get; set; }


        [Required(ErrorMessage = "Period From Date is Required.")]
        public DateTime? DeputationPeriodFromDate { get; set; }

        [Required(ErrorMessage = "Period To Date  is Required.")]
        public DateTime? DeputationPeriodToDate { get; set; }

        [Required(ErrorMessage = "Parent Office is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Parent Office")]
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string DeputationParentOffice { get; set; }

        [Required(ErrorMessage = "Parent Station is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Parent Station")]
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string DeputationParentStation { get; set; }

        [Required(ErrorMessage = "Parent Designation is Required.")]
        public Int32? DeputationParentDesignationId { get; set; }

        [Required(ErrorMessage = "Date is Required.")]
        public DateTime? DeputationRelDate { get; set; }

        public string DeputationRelSession { get; set; }

        [Required(ErrorMessage = "Date is Required.")]
        public DateTime? DeputationRejoinDate { get; set; }

        public string DeputationRejoinSession { get; set; }



        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Borrowing Office")]
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string DeputationBorrOffice { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Borrowing Station")]
        [StringLength(100, ErrorMessage = "Max length is 100 .")]
        public string DeputationBorrStation { get; set; }

        public Int32? DeputationBorrDesignationId { get; set; }

        [Required(ErrorMessage = "Date is Required.")]
        public DateTime? DeputationBrjoinDate { get; set; }

        public string DeputationBrjoinSession { get; set; }

        [Required(ErrorMessage = "Date is Required.")]
        public DateTime? DeputationBrelDate { get; set; }

        public string DeputationBrelSession { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }

        public List<SelectListItem> Designationlist { get; set; }


        public EmpDeputationViewModel()
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



            Designationlist = new List<SelectListItem>();
            DataTable dtDesignationlist = obj.DesignationGet(true);
            Designationlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtDesignationlist.Rows)
            {
                Designationlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }
        }
    }

    public class EmpSuspensionViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpSuspensionId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Suspension Order no is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Suspension Order No")]
        public string SuspensionOrdno { get; set; }

        [Required(ErrorMessage = " Order Date is Required.")]
        public DateTime? SuspensionOrderDate { get; set; }

        [Required(ErrorMessage = "Suspension Date is Required.")]
        public DateTime? SuspensionDate { get; set; }

        [Required(ErrorMessage = "Suspension To Date is Required.")]
        public DateTime? SuspensionToDate { get; set; }

        [Range(1, 100, ErrorMessage = "Subsistence Rate Should be greater than Zero And Less then or equal 100 %")]
        public Int32? SubsistenceRate { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }


        public EmpSuspensionViewModel()
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

        }

    }

    public class EmpPunishmentViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpPunishmentId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Order No")]
        [Required(ErrorMessage = "Order No  is Required.")]
        public string PunishmentOrderNo { get; set; }

        [Required(ErrorMessage = "Punishment Order Date is Required.")]
        public DateTime? PunishmentOrderDate { get; set; }

        [Required(ErrorMessage = "Punishment Typeis Required.")]
        public Int32? PunishmentTypeId { get; set; }

        [Required(ErrorMessage = "Punishment Authority is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Authority ")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]      
        public string PunishmentAuthority { get; set; }

        
        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Punishment Details")]
        [StringLength(100, ErrorMessage = "Max length is 100.")]
        public string PunishmentDetails { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> PunishmentTypelist { get; set; }

        public EmpPunishmentViewModel()
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

            PunishmentTypelist = new List<SelectListItem>();
            DataTable dtPunishmentTypelist = obj.PunishmentsTypeGet();
            PunishmentTypelist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtPunishmentTypelist.Rows)
            {
                PunishmentTypelist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


        }

    }

    public class EmpProbationViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpProbationId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Probation Order No is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Order No")]
        public string ProbationOrderNo { get; set; }

        [Required(ErrorMessage = " Order Date  is Required.")]
        public DateTime? ProbationOrderdate { get; set; }

        [Required(ErrorMessage = "Start Date  Name is Required.")]
        public DateTime? ProbationStartDate { get; set; }

        [Required(ErrorMessage = " Completion Date  is Required.")]
        public DateTime? ProbationCompletionDate { get; set; }

        [Required(ErrorMessage = " Designation is Required.")]
        public Int32? DesignationId { get; set; }

        public Int32? DepartmentId { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Remarks")] 
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Remarks { get; set; }


        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> Designationlist { get; set; }
        public List<SelectListItem> Departmenlist { get; set; }


        public EmpProbationViewModel()
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



            Designationlist = new List<SelectListItem>();
            DataTable dtDesignationlist = obj.DesignationGet(true);
            Designationlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtDesignationlist.Rows)
            {
                Designationlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }


            Departmenlist = new List<SelectListItem>();
            DataTable dtDepartmenlist = obj.DepartmentGet(true,null);
            Departmenlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtDepartmenlist.Rows)
            {
                Departmenlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

        }

    }

    public class EmpSrVerificationViewModel
    {

        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpSrVerificationId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "SR Verify On Date  is Required.")]
        public DateTime? SrVerifyOndate { get; set; }

        [Required(ErrorMessage = "SR Verify From Date  is Required.")]
        public DateTime? SrVerifyFromDate { get; set; }

        [Required(ErrorMessage = "SR Verify To Date  is Required.")]
        public DateTime? SrVerifyToDate { get; set; }

        [Required(ErrorMessage = "SR Verify  is Required.")]
        public bool IsVerified { get; set; }
        [RegularExpression(@"^[a-zA-Z. ]*$", ErrorMessage = "Invalid SrVerify By ")]
        [StringLength(50, ErrorMessage = "Max length is 50.")]
        public string SrVerifyBy { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9#]){1}([A-Za-z0-9#_\+\-\(\)\[\]\{\}\*\@\!\%\.\,\'\?\/\\ ]*)$", ErrorMessage = "Invalid Remarks")]  
        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string Remarks { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }


        public EmpSrVerificationViewModel()
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




        }


    }

    public class EmpDeptTestViewModel
    {
        public Int32? EmpDeptTestId { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = " Dept. Test Name  is Required.")]
        public Int32? DeptTestId { get; set; }

        
        [Required(ErrorMessage = "Date of Passing  is Required.")]       
        public DateTime? PassedDate { get; set; }


        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Authority ")]
        [StringLength(50, ErrorMessage = "Max length is 50 .")]
        public string Authority { get; set; }

        [Required(ErrorMessage = "Reg. Number is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Reg Number")]
        public string RegisterNumber { get; set; }

        [Required(ErrorMessage = "  Gazette Number is Required.")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Invalid Gazette Number")]
        public string GazetteNumber { get; set; }

        public DateTime? ResultDate { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> DeptTestlist { get; set; }


        public EmpDeptTestViewModel()
        {
            PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
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




            DeptTestlist = new List<SelectListItem>();
            DataTable dtDeptTestlist = obj.DeptTestsGet();
            DeptTestlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtDeptTestlist.Rows)
            {
                DeptTestlist.Add(new SelectListItem
                {

                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }




        }
    }

}