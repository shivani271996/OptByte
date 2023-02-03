using CostEstimation.Areas.Masters.Models.LineManagement;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.CertificateMaster
{
    public class CertificateManager
    {
        public static List<CertificateModel> GetCertificate(string searchPhrase)
        {
            searchPhrase = string.IsNullOrEmpty(searchPhrase) ? null : searchPhrase.Trim();
            List<CertificateData> c = CertificateDAL.GetCertificate(searchPhrase);
            List<CertificateModel> model = new List<CertificateModel>();
            CertificateModel certificate = null;
            foreach (var item in c)
            {
                certificate = new CertificateModel
                {
                    RowNumber = item.RowNumber,
                    ID = item.ID,
                    CertificateName = item.CertificateName,
                    Certificateshortcode = item.Certificateshortcode,
                    Description = item.Description,
                    Startdate = item.Startdate,
                    Factorys = item.Factorys,
                    EndDate = item.EndDate,
                    

                    Certificatenumber = item.Certificatenumber,

                    Brand = item.Brand,
                    Factory = item.Factory,
                    ProductLine = item.ProductLine,
                    ProductLines = item.ProductLines,

                    ProductCategory = item.ProductCategory,
                    ProductCategorys = item.ProductCategorys,
                    ItemSeries = item.ItemSeries,
                    Item = item.Item,
                    Country = item.Country,
                    Countrys = item.Countrys,
                    Region = item.Region,
                    Regions = item.Regions,
                    Department = item.Department,
                    Departments = item.Departments,
                    UserName = item.UserName,
                    UserNames = item.UserNames,
                    Certificatefilename = item.Certificatefilename,
                    
                };
                model.Add(certificate);
            }
            return model;
        }

        public static int Save(CertificateModel model)
        {
            int retVal;
            if (model.ID > 0)
            {

                retVal = CertificateDAL.Update(model);
            }
            else
            {
                retVal = CertificateDAL.Insert(model);
            }
            return retVal;
        }


        public static CertificateModel GetCode(string Certificateshortcode)
        {
            CertificateData item = CertificateDAL.GetCode(Certificateshortcode);
            if (item != null)
            {
                return new CertificateModel()
                {
                    ID = item.ID,
                    CertificateName = item.CertificateName,
                    Certificateshortcode = item.Certificateshortcode,
                    Description = item.Description,
                    Startdate = item.Startdate,

                    EndDate = item.EndDate,

                    Certificatenumber = item.Certificatenumber,

                    Brand = item.Brand,
                    Factory = item.Factory,
                    ProductLine = item.ProductLine,

                    ProductCategory = item.ProductCategory,
                    ItemSeries = item.ItemSeries,
                    Country = item.Country,
                    Region = item.Region,
                    Department = item.Department,
                    UserName = item.UserName,
                    
                   
                };
            }
            else
            {
                return new CertificateModel();
            }
        }

        public static void ActivateByID(int ID)
        {
            CertificateDAL.ActivateByID(ID);
        }

        public static void Delete(int ID)
        {
            CertificateDAL.Delete(ID);

        }
        
        public static CertificateModel GetById(int ID)
        {
            CertificateData item = CertificateDAL.GetById(ID);
            return new CertificateModel()
            {
                ID = item.ID,
                CertificateName = item.CertificateName,
                Certificateshortcode = item.Certificateshortcode,
                Description = item.Description,
                Startdate = item.Startdate,
                Certificatefilename = item.Certificatefilename,
                EndDate = item.EndDate,

                Certificatenumber = item.Certificatenumber,
               // Upload = item.Upload,
                Brand = item.Brand,
                Factory = item.Factory,
                ProductLine = item.ProductLine,

                ProductCategory = item.ProductCategory,
                ItemSeries = item.ItemSeries,
                Country = item.Country,
                Region = item.Region,
                Department = item.Department,
                UserName = item.UserName,
                
            };
        }

        public static List<certificateViewModel> GetData()
        {
            List<ViewData> items = CertificateDAL.GetData();
            List<certificateViewModel> model = new List<certificateViewModel>();
            foreach (var item in items)
            {
                certificateViewModel modelItem = new certificateViewModel()
                {


                    ID = item.ID,
                    BRANDID = item.BRANDID,
                    FACTORYID = item.FACTORYID,
                    BRANDNAME = item.BRANDNAME,
                    FACTORYNAME = item.FACTORYNAME,
                    LINEID = item.LINEID,
                    LINENAME = item.LINENAME,
                    SERIESNAME = item.SERIESNAME,
                    CATEGORYID = item.CATEGORYID,
                    CATEGORYNAME = item.CATEGORYNAME
                };
                model.Add(modelItem);
            }
            return model;
            
        }

        public static certificatedownloadmodel GetAttcmntForDwnld(int ID)
        {
            certificatedownloadmodel modelData = CertificateDAL.getAtcmntDownload(ID);
            return modelData;
        }

        
        public static List<LineViewModel> GetLineForFactoryIds(string FactoryId, string BrandId)
        {
            List<LineViewModel> countryModels = new List<LineViewModel>();
            var str1 = FactoryId.Replace("\\", "").Replace("\"", "");
            var str2 = BrandId.Replace("\\", "").Replace("\"", "");
            if (str1 == "NaN")
                str1 = "";
            if (str2 == "NaN")
                str2 = "";
            if (str1 != "")
            {
                if (str2 != "")
                {
                    List<LineViewData> countries = CertificateDAL.GetLineForFactoryIds(str1, str2);
                    LineViewModel country = null;
                    foreach (var item in countries)
                    {
                        country = new LineViewModel
                        {
                            LineId = item.LineId,
                            BrandId = item.BrandId,
                            BrandName = item.BrandName,
                            FactoryId = item.FactoryId,
                            LineName = item.LineName,
                            FactoryName = item.FactoryName,
                        };
                        countryModels.Add(country);
                    }
                }
            }
            return countryModels;
        }

        public static List<LineViewModel> GetCategoryForFactoryIds(string LineId, string FactoryId, string BrandId)
        {

            List<LineViewModel> countryModels = new List<LineViewModel>();
            var str1 = LineId.Replace("\\", "").Replace("\"", "");
            var str2 = FactoryId.Replace("\\", "").Replace("\"", "");
            var str3 = BrandId.Replace("\\", "").Replace("\"", "");
            if (str1 == "NaN")
                str1 = "";
            if (str2 == "NaN")
                str2 = "";
            if (str3 == "NaN")
                str3 = "";
            if (str1 != "")
            {
                if (str2 != "")
                {
                    if (str3 != "")
                    {
                        List<LineViewData> countries = CertificateDAL.GetCategoryForFactoryIds(str1, str2, str3);
                        LineViewModel country = null;
                        foreach (var item in countries)
                        {
                            country = new LineViewModel
                            {
                                LineId = item.LineId,
                                FactoryId = item.FactoryId,
                                BrandId = item.BrandId,
                                FactoryName = item.FactoryName,
                                BrandName = item.BrandName,
                                CategoryId = item.CategoryId,
                                LineName = item.LineName,
                                CategoryName = item.CategoryName,
                            };
                            countryModels.Add(country);
                        }
                    }
                }
            }
            return countryModels;
            
        }
        //public static List<LineViewModel> GetSeriesForLineIds(string CategoryId, string FactoryId, string LineId)
        //{
        //    List<LineViewModel> countryModels = new List<LineViewModel>();

        //    var str1 = LineId.Replace("\\", "").Replace("\"", "");
        //    var str2 = FactoryId.Replace("\\", "").Replace("\"", "");
        //    var str3 = CategoryId.Replace("\\", "").Replace("\"", "");

        //    if (str1 == "NaN" || str2 =="NaN" || str3 =="NaN")
        //        str1 = "";
        //    if (str1 != "")

        //    { 

        //        List<LineViewData> countries = CertificateDAL.GetSeriesForLineIds(str1,str2,str3);
        //        LineViewModel country = null;
        //        foreach (var item in countries)
        //        {
        //            country = new LineViewModel
        //            {
        //                Id = item.Id,
        //                CategoryId = item.CategoryId,
        //                CategoryName = item.CategoryName,
        //                LineName = item.LineName,
        //                SeriesName = item.SeriesName,
        //                LineId = item.LineId,
        //                FactoryName = item.FactoryName,
        //                FactoryId= item.FactoryId
        //            };
        //            countryModels.Add(country);
        //        }
        //    }

        //    return countryModels;

        //}


        public static List<LineViewModel> GetSeriesForLineIds(string CategoryId, string LineId,string FactoryId)
        {
            List<LineViewModel> countryModels = new List<LineViewModel>();

            var str1 = CategoryId.Replace("\\", "").Replace("\"", "");
            var str2 = FactoryId.Replace("\\", "").Replace("\"", "");
            var str3 = LineId.Replace("\\", "").Replace("\"", "");

            if (str1 == "NaN")
                str1 = "";
            if (str2 == "NaN")
                str2 = "";
            if (str3 == "NaN")
                str3 = "";

            if (str1 != "")

            {
                if (str2 != "")

                {
                    if (str3 != "")

                    {
                       

                            List<LineViewData> countries = CertificateDAL.GetSeriesForLineIds(str1, str2, str3);
                            LineViewModel country = null;
                            foreach (var item in countries)
                            {
                                country = new LineViewModel
                                {
                                    Id = item.Id,
                                    CategoryId = item.CategoryId,
                                    CategoryName = item.CategoryName,
                                    LineName = item.LineName,
                                    SeriesName = item.SeriesName,
                                    LineId = item.LineId,
                                    //Brand = item.BrandId,
                                  //  BrandName = item.BrandName,
                                    FactoryName = item.FactoryName,
                                    FactoryId = item.FactoryId
                                };
                                countryModels.Add(country);
                            }
                        }
                    
                }
            }

            return countryModels;

        }

    }
}