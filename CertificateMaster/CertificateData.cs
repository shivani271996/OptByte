using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.CertificateMaster
{
    public class CertificateData
    {
        public int ID { get; set; }
        public string CertificateName { get; set; }
        public string Certificateshortcode { get; set; }
        public string Description { get; set; }
        public string Startdate { get; set; }
        public string EndDate { get; set; }
        public string Certificatenumber { get; set; }
        public string Brand { get; set; }
        public string Factory { get; set; }
        public string Factorys { get; set; }
        public string ProductLine { get; set; }
        public string ProductLines { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategorys { get; set; }
        public string ItemSeries { get; set; }
        public string Item { get; set; }

        public string Country { get; set; }
        public string Countrys { get; set; }
        public string Region { get; set; }
        public string Regions { get; set; }
        public string Department { get; set; }
        public string Departments { get; set; }
        public string UserNames { get; set; }
        public string UserName { get; set; }
        public string Certificatefilename { get; set; }
        public string Certificatefilepath { get; set; }
        public byte[] Certificatefile { get; set; }
        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int IsDeleted { get; set; }
        public string SaveStatus { get; set; }
        public string EntryOrigin { get; set; }
        public string ApprovalStatus { get; set; }
        public int IsActive { get; set; }

        public byte[] RowVer { get; set; }

        public int RowNumber { get; set; }
    }

    public class setRegion
    {

        public string RegionId { get; set; }
        public string CountryId { get; set; }
       
    }

    public class ViewData
    {

        public int ID { get; set; }
        public string SERIESNAME { get; set; }
        public int BRANDID { get; set; }
        public string BRANDNAME { get; set; }
        public string LINENAME { get; set; }
        public int LINEID { get; set; }
        public int CATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
        public int FACTORYID { get; set; }
        public string FACTORYNAME { get; set; }


    }

    public class certificatedownloaddata
    {
        public string Certificatefilename { get; set; }
        public int ID { get; set; }
        public string Certificatefilepath { get; set; }
        public byte[] Certificatefile { get; set; }
    }
}