using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class MasterSettingModel
    {
    }

    public class PropertyTypeDetail
    {
        public int? PropertyTypeId { get; set; }

        [Required]
        public int? OrderNo { get; set; }

        [Required]
        public string PropertyTypeName { get; set; }

    }

    public class LocationDetailModel
    {
        public Int32? CityId { get; set; }

        [Required]
        public Int32? OrderNo { get; set; }

        [Required]
        public string CityName { get; set; }
    }

    public class PriceRangeModel
    {
        public Int32? PriceId { get; set; }

        [Required]
        public Int32? OrderNo { get; set; }

        [Required]
        public string MinAmountRange { get; set; }

        [Required]
        public string MaxAmountRange { get; set; }

        public string AmountRange { get; set; }

    }

    public class MainBannerDetailModel
    {
        public Int32? BannerId { get; set; }

        public Int32? OrderNo { get; set; }

        public byte[] BannerImage { get; set; }

        public string strbase64Image { get; set; }

        public HttpPostedFileBase MyFile { get; set; }

    }

    public class SubBannerDetailModel
    {
        public Int32? SubBannerId { get; set; }

        public Int32? OrderNo { get; set; }

        public byte[] BannerImage { get; set; }

        public string TopBottom { get; set; }

        public string strbase64Image { get; set; }

        public HttpPostedFileBase MyBanner { get; set; }

    }

    public class TextFlashModel
    {
        public Int32? TextFlashId { get; set; }

        public string TextMessage { get; set; }

    }

    public class AddressDetailModel
    {
        public Int32? AddressId { get; set; }

        public Int32? OrderNo { get; set; }

        public string MainDetail { get; set; }

        public string SubDetail { get; set; }

        public string Contact { get; set; }

        public string EmailId { get; set; }

    }

    public class AddressDetailMapModel
    {
        public Int32? AddressGoogleMapId { get; set; }

        public Int32? AddressId { get; set; }

        public string LocationName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

    }
}