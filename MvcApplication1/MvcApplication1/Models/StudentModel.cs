using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class StudentModel
    {
    }

    [Serializable]
    public class StudentAdmissionDetailModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string FatherContact { get; set; }
        public string EmgContact { get; set; }
        public string EmailId { get; set; }
        public string DOB { get; set; }
        public string PAddress { get; set; }
        public string PCity { get; set; }
        public string PState { get; set; }
        public string PPincode { get; set; }
        public string CAddress { get; set; }
        public string CCity { get; set; }
        public string CState { get; set; }
        public string CPincode { get; set; }
        public string Height { get; set; }
        public string Wight { get; set; }
        public string AnyHealthIssue { get; set; }
        public string HealthIssueDescription { get; set; }
        public string CurrentQualification { get; set; }
        public string ClassForAdmission { get; set; }
        public int AdmissionFee { get; set; }
        public string RollNo { get; set; }
        public string Photo { get; set; }
        public string DateOfAdmission { get; set; }
    }
}