using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using MvcApplication1.Models;
using System.Web.Mvc;

namespace MvcApplication1.Models
{
    
    public class PropertyModel
    {
    }

    public class PropertyDetailModel
    {
        BusinessLayer obj = new BusinessLayer ();

        public Int32? PropertyId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string RentBuySale { get; set; }

        public string BedroomSqFeetEtc { get; set; }

        [Required]
        public Int32? Price { get; set; }

        [Required]
        public string Area { get; set; }

        public string SubArea { get; set; }

        public string LandMark { get; set; }

        public DateTime? PropertyDate { get; set; }

        public string PropertyHeading { get; set; }

        public string PropertySummary { get; set; }

        [Required]
        public string Name { get; set; }

        public string Contact { get; set; }

        public string EmailId { get; set; }

        public HttpPostedFileBase ProperyImageUpload { get; set; }
        
        //public string PropertyImage { get; set; }
        public string strbase64Image { get; set; }

        public string ImageName { get; set; }

        public string ImageType { get; set; }

        public Int32? PageNo { get; set; }

        public List<SelectListItem> PropertyTypeList { get; set; }
        public List<SelectListItem> CityLocationList { get; set; }

        public PropertyDetailModel()
        {
            try
            {
                PropertyTypeList = new List<SelectListItem>();
                DataTable dtPropertyType = obj.FunDataTable("SELECT * FROM PropertyTypeDetail_Get()");
                PropertyTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPropertyType.Rows)
                {
                    PropertyTypeList.Add(new SelectListItem
                    {
                        Text = dr[2].ToString(),
                        Value = dr[1].ToString()
                    });
                }

                CityLocationList = new List<SelectListItem>();
                DataTable dtLocation = obj.FunDataTable("SELECT * FROM CityDetail_Get()");
                CityLocationList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtLocation.Rows)
                {
                    CityLocationList.Add(new SelectListItem
                    {
                        Text = dr[2].ToString(),
                        Value = dr[1].ToString()
                    });
                }
            }
            catch(Exception ex)
            {

            }
        }

    }

    public class PropertyImageGalleryModel
    {
        BusinessLayer obj = new BusinessLayer();

        public Int32? GalleryId { get; set; }

        [Required]
        public Int32? PropertyType { get; set; }

        [Required]
        public Int32? PropertyId { get; set; }
        
        [Required]
        public HttpPostedFileBase ProperyGalleryUpload { get; set; }

        public string strbase64Image { get; set; }

        [Required]
        public Int32? OrderNo { get; set; }
     
       


        public List<SelectListItem> PropertyTypeList { get; set; }
        public List<SelectListItem> PropertyIdList { get; set; }

        public PropertyImageGalleryModel()
        {
            try
            {
                PropertyTypeList = new List<SelectListItem>();
                DataTable dtPropertyType = obj.FunDataTable("SELECT * FROM PropertyTypeDetail_Get()");
                PropertyTypeList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPropertyType.Rows)
                {
                    PropertyTypeList.Add(new SelectListItem
                    {
                        Text = dr[2].ToString(),
                        Value = dr[0].ToString()
                    });
                }

                PropertyIdList = new List<SelectListItem>();
                DataTable dtPropertyId = obj.FunDataTable("SELECT * FROM PropertyDetail_Get()");
                PropertyIdList.Add(new SelectListItem { Text = "--Select--", Value = "" });
                foreach (DataRow dr in dtPropertyId.Rows)
                {
                    PropertyIdList.Add(new SelectListItem
                    {
                        Text = dr[0].ToString(),
                        Value = dr[0].ToString()
                    });
                }
            }
            catch(Exception ex)
            {

            }
        }

        
    }
}
