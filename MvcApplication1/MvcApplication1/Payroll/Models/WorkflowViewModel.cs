
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace PoiseERP.Areas.Payroll.Models
{
    public class WorkflowViewModel
    {
        public DateTime filecreateddate;
        public int? ProcessId { get; set; }
        public List<SelectListItem> ProcessList { get; set; }
        [Required]
        public string ExcelPassword { get; set; }
        public int? StateId { get; set; }
        public string EmpCode { get; set; }
        public int? NextState { get; set; }
        public int? BankId { get; set; }
        public int? SearchBankId { get; set; }
        public List<SelectListItem> BankList { get; set; }
//--------------------------------------------------------------------
        public int? BankAccountId { get; set; }
        public List<SelectListItem> BankAccountList { get; set; }
//-----------------------------------------------------------------------
        public DateTime? PayDate { get; set; }
        public int? MonthId { get; set; }
        public int? VarMonthId { get; set; }
        public int? VarMonth_Id { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> VarMonthList { get; set; }

        public decimal? TotalAmount { get; set; }
        public decimal? Amount { get; set; }
      
        public decimal? BalanceAmount { get; set; } 
        public int? YearId { get; set; }
        public int? VarYearId { get; set; }
        public int? VarYear_Id { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> VarYearList { get; set; }
        public int? EmpReimbursementId { get; set; }
        
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? ProjectId { get; set; }

        public List<SelectListItem> ProjectList { get; set; }

        public List<WorkflowViewModel> WorkflowDataList { get; set; }
        public List<SelectListItem> NextStateList { get; set; }
        public string AppKey { get; set; }
        public Boolean isCheck { get; set; }

        public byte[] imageByte { get; set; }
        public string imageBase64String { get; set; }

        [Required]
        public string imageUpload { get; set; }

        public string imageName { get; set; }

        [Required]
        public string notesText { get; set; }

        public string notesList { get; set; }

        public string hfNotesText { get; set; }

        public string hfNotesList { get; set; }

        public int? AttachmentId { get; set; }

    }
}