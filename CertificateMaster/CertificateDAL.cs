using CostEstimation.Areas.Masters.Models.LineManagement;
using CostEstimation.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.CertificateMaster
{
    public class CertificateDAL
    {
        
        public static List<CertificateData> GetCertificate(string searchPhrase)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {

                //     string sqlQuery = @"select distinct row_number() over(order by a.ID desc) as 'RowNumber', a.[ID],a.[CertificateName],a.[Certificateshortcode],a.[Description],a.[Startdate],a.[EndDate],a.[Certificatenumber], b.[BrandName] As Brand,a.[Factorys],a.[ProductLines],
                //                         a.[ProductCategorys],a.[Item] As Item,a.[Regions],a.[Countrys],i.[Code] As Department,a.[UserNames],a.[Certificatefilename]

                //                         from CertificateMaster a left join BrandMaster b on a.Brand =b.Id 
                //                                                  left join [dbo].[FACTORYMASTER] c on a.ID =c.ID 
                //                                                   left join [dbo].[LineMaster] d on a.ID =d.Id 
                //                                                  left join [dbo].[CategoryMaster] e on a.ID =e.Id 
                //                                                     left join ItemSeries f on a.ID = f.Id 
                //                                                     left join TblRegionMst g on a.ID = g.ID 
                //                                                     left join dbo.TBLCOUNTRYMST h on a.ID =h.ID 
                //                                                     left join DepartmentMaster i on a.ID= i.Id 
                //                                                     left join dbo.TblUserMst j on a.ID =j.ID

                //where
                //                         (CONCAT(a.CertificateName,a.Certificateshortcode,a.Description,a.Certificatenumber,e.CategoryName,f.SeriesName,g.Region,h.COUNTRYNAME,i.Code) LIKE @Search) and a.IsDeleted=0 ORDER BY a.[ID] DESC";

                string sqlQuery = @"select distinct row_number() over(order by a.ID desc) as 'RowNumber', a.[ID],a.[CertificateName],a.[Certificateshortcode],a.[Description],a.[Startdate],a.[EndDate],a.[Certificatenumber], b.[BrandName] As Brand,a.[Factorys],a.[ProductLines],
                                    a.[ProductCategorys],a.[Item] As Item,a.[Regions],a.[Countrys],a.[Departments],a.[UserNames],a.[Certificatefilename]

                                    from CertificateMaster a left join BrandMaster b on a.Brand =b.Id 
                                                             left join [dbo].[FACTORYMASTER] c on a.ID =c.ID 
                                                              left join [dbo].[LineMaster] d on a.ID =d.Id 
	                                                            left join [dbo].[CategoryMaster] e on a.ID =e.Id 
                                                                left join ItemSeries f on a.ID = f.Id 
                                                                left join TblRegionMst g on a.ID = g.ID 
                                                                left join dbo.TBLCOUNTRYMST h on a.ID =h.ID 
                                                                left join DepartmentMaster i on a.ID= i.Id 
                                                                left join dbo.TblUserMst j on a.ID =j.ID
                                  
								   where
                                    (CONCAT(a.CertificateName,a.Certificateshortcode,a.Description,a.Certificatenumber,e.CategoryName,f.SeriesName,g.Region,h.COUNTRYNAME,i.Code) LIKE @Search) and a.IsDeleted=0 ORDER BY a.[ID] DESC";
                DynamicParameters param = new DynamicParameters();
                param.Add("@search", '%' + searchPhrase + '%' == null ? "" : '%' + searchPhrase + '%');

                return dbConnection.Query<CertificateData>(sqlQuery, param: param).ToList();
            }

        }
        public static int Insert(CertificateModel cetmodel)
        {
            int i = 0;
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", cetmodel.ID);
                    param.Add("@CertificateName", cetmodel.CertificateName);
                    param.Add("@Certificateshortcode", cetmodel.Certificateshortcode);
                    param.Add("@Description", cetmodel.Description);
                    param.Add("@Startdate", cetmodel.Startdate);
                    param.Add("@EndDate", cetmodel.EndDate);
                    param.Add("@Certificatenumber", cetmodel.Certificatenumber);
                    param.Add("@Brand", cetmodel.Brand);
                    param.Add("@Factory", cetmodel.Factory);
                    param.Add("@Factorys", cetmodel.Factorys);
                    param.Add("@ProductLine", cetmodel.ProductLine);
                    param.Add("@ProductLines", cetmodel.ProductLines);
                    param.Add("@ProductCategory", cetmodel.ProductCategory);
                    param.Add("@ProductCategorys", cetmodel.ProductCategorys);
                    param.Add("@ItemSeries", cetmodel.ItemSeries);
                    param.Add("@Item", cetmodel.Item);
                    param.Add("@Country", cetmodel.Country);
                    param.Add("@Countrys", cetmodel.Countrys);
                    param.Add("@Region", cetmodel.Region);
                    param.Add("@Regions", cetmodel.Regions);
                    param.Add("@Department", cetmodel.Department);
                    param.Add("@Departments", cetmodel.Departments);
                    param.Add("@UserName", cetmodel.UserName);
                    param.Add("@UserNames", cetmodel.UserNames);
                    
                    param.Add("@CreatedBy", cetmodel.CreatedBy);

                    param.Add("@ModifiedBy", cetmodel.ModifiedBy);
                    
                    param.Add("@IsDeleted", cetmodel.IsDeleted);
                    param.Add("@SaveStatus", cetmodel.SaveStatus);
                    //param.Add("@Upload", cetmodel.Upload);

                    param.Add("@EntryOrigin", cetmodel.EntryOrigin);
                    param.Add("@ApprovalStatus", cetmodel.ApprovalStatus);
                    param.Add("@RowVer", cetmodel.RowVer);

                    string sqlQuery;
                    if (!string.IsNullOrEmpty(cetmodel.Certificatefilepath))
                    {
                        param.Add("@Certificatefile", cetmodel.Certificatefile);
                        param.Add("@Certificatefilename", cetmodel.Certificatefilename);

                        sqlQuery = @"Insert into CertificateMaster (CertificateName, Certificateshortcode, Description, Startdate, EndDate, Certificatenumber,Brand,Factory,Factorys,
                                        ProductLine,ProductLines,ProductCategory,ProductCategorys,ItemSeries,Item,Region,Regions,Country,Countrys,Department,Departments,UserName,UserNames,Certificatefile,Certificatefilename,CreatedBy,ModifiedBy,IsDeleted,SaveStatus,EntryOrigin,ApprovalStatus) values (@CertificateName,@Certificateshortcode, @Description, @Startdate,@EndDate,@Certificatenumber,@Brand,@Factory,@Factorys,
                                        @ProductLine,@ProductLines,@ProductCategory,@ProductCategorys,@ItemSeries,@Item,@Region,@Regions,@Country,@Countrys,@Department,@Departments,@UserName,@UserNames,@Certificatefile,@Certificatefilename,@CreatedBy,@ModifiedBy,@IsDeleted,@SaveStatus,@EntryOrigin,@ApprovalStatus);Select ID  from BusinessEntityMaster where ID=SCOPE_IDENTITY();";


                    }
                    else
                    {
                        sqlQuery = @"Insert into CertificateMaster (CertificateName, Certificateshortcode, Description, Startdate, EndDate, Certificatenumber,Brand,Factory,Factorys,
                                        ProductLine,ProductLines,ProductCategory,ProductCategorys,ItemSeries,Item,Region,Regions,Country,Countrys,Department,Departments,UserName,UserNames,CreatedBy,ModifiedBy,IsDeleted,SaveStatus,EntryOrigin,ApprovalStatus) values (@CertificateName,@Certificateshortcode, @Description, @Startdate,@EndDate,@Certificatenumber,@Brand,@Factory,@Factorys,
                                        @ProductLine,@ProductLines,@ProductCategory,@ProductCategorys,@ItemSeries,@Item,@Region,@Regions,@Country,@Countrys,@Department,@Departments,@UserName,@UserNames,@CreatedBy,@ModifiedBy,@IsDeleted,@SaveStatus,@EntryOrigin,@ApprovalStatus);Select ID  from BusinessEntityMaster where ID=SCOPE_IDENTITY();";


                    }

                    
                    i = dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return i;
        }
        //Update
        public static int Update(CertificateModel cetmodel)
        {
            int j = 0;
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", cetmodel.ID);
                    param.Add("@CertificateName", cetmodel.CertificateName);
                    param.Add("@Certificateshortcode", cetmodel.Certificateshortcode);
                    param.Add("@Description", cetmodel.Description);
                    param.Add("@Startdate", cetmodel.Startdate);
                    param.Add("@EndDate", cetmodel.EndDate);
                    param.Add("@Certificatenumber", cetmodel.Certificatenumber);
                    param.Add("@Brand", cetmodel.Brand);
                    param.Add("@Factorys", cetmodel.Factorys);
                    param.Add("@Factory", cetmodel.Factory);
                    param.Add("@ProductLines", cetmodel.ProductLines);
                    param.Add("@ProductLine", cetmodel.ProductLine);
                    param.Add("@ProductCategory", cetmodel.ProductCategory);
                    param.Add("@ProductCategorys", cetmodel.ProductCategorys);
                    param.Add("@ItemSeries", cetmodel.ItemSeries);
                    param.Add("@Item", cetmodel.Item);
                    param.Add("@Country", cetmodel.Country);
                    param.Add("@Countrys", cetmodel.Countrys);
                    param.Add("@Region", cetmodel.Region);
                    param.Add("@Regions", cetmodel.Regions);
                    param.Add("@Department", cetmodel.Department);
                    param.Add("@Departments", cetmodel.Departments);
                    param.Add("@UserName", cetmodel.UserName);
                    param.Add("@UserNames", cetmodel.UserNames);
                    
                    //param.Add("@Upload", cetmodel.Upload);

                    param.Add("@CreatedBy", cetmodel.CreatedBy);

                    param.Add("@ModifiedBy", cetmodel.ModifiedBy);

                    param.Add("@IsDeleted", cetmodel.IsDeleted);
                    param.Add("@SaveStatus", cetmodel.SaveStatus);

                    param.Add("@EntryOrigin", cetmodel.EntryOrigin);
                    param.Add("@ApprovalStatus", cetmodel.ApprovalStatus);
                    param.Add("@RowVer", cetmodel.RowVer);
                    //if (!string.IsNullOrEmpty(cetmodel.Certificatefilepath))
                    //{
                    //    param.Add("@Certificatefile", cetmodel.Certificatefile);
                    //}
                    param.Add("@Certificatefilename", cetmodel.Certificatefilename);
                    string sqlQuery = @"Update CertificateMaster set CertificateName=@CertificateName,Certificateshortcode=@Certificateshortcode,Description=@Description, Startdate=@Startdate, EndDate=@EndDate, Certificatenumber=@Certificatenumber,Certificatefilename = @Certificatefilename,Brand=@Brand,Factory=@Factory,Factorys=@Factorys,
                                        ProductLine=@ProductLine,ProductLines=@ProductLines,ProductCategory=@ProductCategory,ProductCategorys=@ProductCategorys,Item=@Item,ItemSeries=@ItemSeries,Region=@Region,Regions=@Regions,Country=@Country,Countrys=@Countrys,Department=@Department,Departments=@Departments,UserName=@UserName,UserNames=@UserNames,CreatedBy=@CreatedBy,ModifiedBy=@ModifiedBy where ID=@ID";

                    j = dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return j;
        }

        public static CertificateData GetCode(string Certificateshortcode)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"select ID,CertificateName, Certificateshortcode, Description, Startdate, EndDate, Certificatenumber, Brand,Factory,
                                        ProductLine, ProductCategory, ItemSeries,Region,Country,Department,UserName from 
                                        CertificateMaster where  Certificateshortcode = @Certificateshortcode";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Certificateshortcode", Certificateshortcode);
                return dbConnection.QueryFirstOrDefault<CertificateData>(sqlQuery, param: param);
            }
        }

        public static void ActivateByID(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = "update  CertificateMaster set IsDeleted=0 where ID=@ID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
                dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
            }

        }

        public static void Delete(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
               
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
              
                dbConnection.ExecuteScalar<int>("spDeleteCertificate", param: param, commandType: CommandType.StoredProcedure);
            }
        }

        public static CertificateData GetById(int ID)
        {
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {
                    string sqlQuery = @"select ID,CertificateName,Certificateshortcode, Description, Startdate,EndDate,Certificatenumber,Brand,Factory,
                                        ProductLine,ProductCategory,ItemSeries,Region,Country,Department,UserName,Certificatefilename from CertificateMaster where ID=@ID";

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", ID);
                   
                    return dbConnection.QueryFirstOrDefault<CertificateData>(sqlQuery, param: param);
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        public static List<ViewData> GetData()
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"WITH CTE AS (
                SELECT DISTINCT ITMSRES.ID,ITMSRES.SERIESNAME,ITMSRES.BRANDID,BRNDMST.BRANDNAME,ITMSRES.LINEID,
                LINEMST.LINENAME,ITMSRES.CATEGORYID,CTGMST.CATEGORYNAME,ITMSRES.FACTORYID,FCTMST.FACTORYNAME
                FROM ITEMPRICEMASTER AS ITPRSMST  
                INNER JOIN ITEMSERIES ITMSRES  ON ITPRSMST.ITEMSERIESID = ITMSRES.ID
                INNER JOIN BrandMaster BRNDMST ON BRNDMST.ID = ITMSRES.BRANDID
                INNER JOIN FACTORYMASTER FCTMST ON FCTMST.ID = ITMSRES.FACTORYID
            
            
                    INNER JOIN LineMaster LINEMST ON LINEMST.ID = ITMSRES.LINEID
                    INNER JOIN CATEGORYMASTER CTGMST ON CTGMST.ID = ITMSRES.CATEGORYID
                    AND ITPRSMST.ISACTIVE = 1
                    AND ITMSRES.ISACTIVE = 1
                    )
                    Select * From CTE";
                DynamicParameters param = new DynamicParameters();
                //param.Add("@BrandId", BrandId);
                return dbConnection.Query<ViewData>(sqlQuery, param: param).ToList();
            }
        }

        public static certificatedownloadmodel getAtcmntDownload(int ID)
        {
            certificatedownloadmodel attachmentData = null;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GETDOWNLOADCERTIFICATE]";
                cmd.Parameters.AddWithValue("@ID", ID);                
                SqlDataReader dr = SqlHelper.DataReader(cmd);
                while (dr.Read())
                {
                    attachmentData = new certificatedownloadmodel()
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        Certificatefilename = Convert.ToString(dr["Certificatefilename"]),
                        Certificatefile = (byte[])dr["Certificatefile"],

                    };

                }
            }
            return attachmentData;
        }


        public static List<LineViewData> GetLineForFactoryIds(string FactoryId , string BrandId)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {

                string sqlQuery = @"select distinct a.LineId, b.LineName ,c.FactoryName,d.BrandName
                                    from [dbo].[ItemSeries] a
                                    inner join [dbo].[LineMaster] b on a.LineId = b.Id
                                    inner join FactoryMaster c on a.FactoryId = c.Id
                                    inner join BrandMaster d on a.BrandId = d.Id
                                    where a.FactoryId in(" + FactoryId + ") And a.BrandId in (" + BrandId + ") AND a.IsActive =1 ORDER BY b.LineName Asc";
                DynamicParameters param = new DynamicParameters();
                param.Add("@FactoryId", FactoryId);
                param.Add("@BrandId", BrandId);
                return dbConnection.Query<LineViewData>(sqlQuery,param:param).ToList();
            }
        }

        public static List<LineViewData> GetCategoryForFactoryIds(string LineId, string FactoryId, string BrandId)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {

                string sqlQuery = @"select distinct a.CategoryId,d.FactoryName,h.BrandName,c.CategoryName,b.LineName,b.Id as LineId
                                    from [dbo].[ItemSeries] a
                                    inner join [dbo].[LineMaster] b on a.LineId = b.Id
                                    inner join [dbo].[CategoryMaster] c on a.CategoryId = c.Id
                                    inner join FactoryMaster d on a.FactoryId = d.Id
									 inner join BrandMaster h on a.BrandId = h.Id
                                    where a.LineId in("+ LineId +") And a.FactoryId in ("+ FactoryId +") And a.BrandId in ("+ BrandId +") AND a.IsActive =1 ORDER BY c.CategoryName Asc";
                DynamicParameters param = new DynamicParameters();
                param.Add("@FactoryId", FactoryId);
                param.Add("@LineId", LineId);
                param.Add("@BrandId", BrandId);
                return dbConnection.Query<LineViewData>(sqlQuery).ToList();
            }
        }

        public static List<LineViewData> GetSeriesForLineIds(string CategoryId ,string FactoryId, string LineId)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {

                string sqlQuery = @"select distinct a.Id,a.SeriesName, a.FactoryId, a.LineId,a.CategoryId,d.CategoryName
                                   from [dbo].[ItemSeries] a
                                    
                                    inner join FACTORYMASTER b on a.FactoryId = b.ID
									inner join [dbo].[LineMaster] c on a.LineId = c.Id
									inner join [dbo].[CategoryMaster] d on a.CategoryId = d.Id 
									
									 where a.FactoryId in(" + FactoryId + ")  AND a.LineId in(" + LineId+") AND a.CategoryId in("+CategoryId+") AND a.IsActive =1 ORDER BY a.SeriesName Asc";
                DynamicParameters param = new DynamicParameters();
                param.Add("@FactoryId", FactoryId);
              //  param.Add("@BrandId", BrandId);
                param.Add("@LineId", LineId);
                param.Add("@CategoryId", CategoryId);
                return dbConnection.Query<LineViewData>(sqlQuery,param : param).ToList();
            }
        }

    }
}