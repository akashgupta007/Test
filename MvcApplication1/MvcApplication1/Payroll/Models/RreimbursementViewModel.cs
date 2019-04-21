using MCLSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace PoiseERP.Areas.Payroll.Models
{
    public class RreimbursementViewModel
    {

    }
    public class EmpReimbursementEntryViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();
        public Int32? EmpReimbursementId { get; set; }
        public Int32? EmpReimbursementEntryId { get; set; }
        public Boolean isRowCheck { get; set; }

        public Int32? EmployeeId { get; set; }

        public List<EmpReimbursementEntryViewModel> EmployeeDataList { get; set; }

        public decimal? Amount { get; set; }
        public decimal? TotalAmount { get; set; }
        public Int32? IsApprove { get; set; }

        public Int32? IsPayrollProcees { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime  StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int32? EmpTypeId { get; set; }

        public Int32? DepartmentId { get; set; }

        public Int32? LocationId { get; set; }

        public Int32? DesginationId { get; set; }

      
        [Required(ErrorMessage = "Month is Required.")]
        public Int32? MonthId { get; set; }

        [Required(ErrorMessage = "Year is Required.")]
        public Int32? Year { get; set; }

        public Int32? ProjectId { get; set; }
        public Int32? ShiftId { get; set; }

        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> EmployeeLeftList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> DesginationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public List<SelectListItem> ShiftList { get; set; }

        public EmpReimbursementEntryViewModel()
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
                DataTable dtDepartmentList = obj.DepartmentGet(true,null);
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
                DataTable dtProjectList = obj.ProjectGet(true,null);
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


    public class EmpReimbursementItemViewModel
    {
        public Int32? EmpReimbursementItemId { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        [Required(ErrorMessage = "Reimbursement Item Name is Required.")]
        public string ReimbursementItemName { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string ReimbursementItemDesc { get; set; }


    }

    public class EmpReimbursementViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public Int32? EmpReimbursementId { get; set; }

        public string ErrorMsg { get; set; }
        public string Flag { get; set; }

        [Required(ErrorMessage = "Employee Name is Required.")]
        public Int32? EmployeeId { get; set; }

        [Required(ErrorMessage = "Reimbursement Item is Required.")]
        public Int32? ReimbursementItemId { get; set; }

         [Required(ErrorMessage = "Reimbursement Date is Required.")]
        public DateTime? RequestDate { get; set; }

        [Required(ErrorMessage = "Amount is Required.")]
        [Range(1, 99999999999999, ErrorMessage = "Amount should be greater than Zero .")]

        public string Amount { get; set; }

        public bool IsDocument { get; set; }

        public bool IsPayrollItem { get; set; }

        [StringLength(500, ErrorMessage = "Max length is 500.")]
        public string ReimbursementNotes { get; set; }

       // [Required(ErrorMessage = "Project Name is Required.")]
        public Int32? ProjectId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfLeaving { get; set; }

       // [Required(ErrorMessage = "Document Image is Required.")]
        public byte[] DocumentImg { get; set; }
        public string strDocumentImg { get; set; }
        public string strContentType { get; set; }
        public string strDocumentName { get; set; }

        public string imagePath { get; set; }

      //  [Required(ErrorMessage = "From Date is Required.")]
        public DateTime? FromDate { get; set; }

      //  [Required(ErrorMessage = "To Date is Required.")]
        public DateTime? ToDate { get; set; }
       // [Required(ErrorMessage = "Document Upload is Required.")]
        public HttpPostedFileBase File { get; set; }

        public List<SelectListItem> Projectlist { get; set; }
        public List<SelectListItem> Employeelist { get; set; }

        public List<SelectListItem> ReimbursementItemList { get; set; }

        public EmpReimbursementViewModel()
        {

            Projectlist = new List<SelectListItem>();
            DataTable dt = obj.ProjectGet(true,null);
            Projectlist.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dt.Rows)
            {
                Projectlist.Add(new SelectListItem
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


            ReimbursementItemList = new List<SelectListItem>();
            DataTable dtPayrollItemList = obj.ReimbursementItemGet(null);
            ReimbursementItemList.Add(new SelectListItem { Text = "--Select--", Value = "" });
            foreach (DataRow dr in dtPayrollItemList.Rows)
            {
                ReimbursementItemList.Add(new SelectListItem
                {
                    Text = dr[1].ToString(),
                    Value = dr[0].ToString()
                });
            }

        }

    }

    public class EmpReimbursementDetailViewModel
    {
        PoisePayrollServiceModel obj = new PoisePayrollServiceModel();

        public int? EmpReimbursementId { get; set; }

        public Int32? EmployeeId { get; set; }

         [Required(ErrorMessage = "From Date is Required.")]
        public DateTime? FromDate { get; set; }
       [Required(ErrorMessage = "To Date is Required.")]
        public DateTime? ToDate { get; set; }

        public List<SelectListItem> Employeelist { get; set; }

        public EmpReimbursementDetailViewModel()
        {

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

}