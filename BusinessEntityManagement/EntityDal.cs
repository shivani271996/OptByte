using CostEstimation.DAL;
using CostEstimation.Managers.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace CostEstimation.Areas.Masters.Models.BusinessEntityManagement
{
    public class EntityDal
    {

        public static List<EntityData> GetEntity(string searchPhrase)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {


                string sqlQuery = @"select row_number() over(order by a.[ID] desc) as 'RowNumber', a.[ID],b.[Region] As Region,c.COUNTRYNAME As CountryId,d.[CurrencyCode] As CurrencyId,a.[EntityName],a.[EntityCode],
                                  a.[Description],a.[RegistrationNumber],a.[TaxNumber],a.
								  [BeneficiaryName],a.[BankName],a.[BankAddress],a.[BankSwiftCode],
                                   a.[BankAccountNo],a.[IBANNo],a.[AddressLine1],a.[AddressLine2],
								   a.[AddressLine3],a.[PostalCode],a.[CityName],
                                    a.[PhoneOffice],a.[Email],a.[Fax],a.[WebSite],a.[ApprovalStatus],a.[EntryOrigin],a.[SaveStatus]
                                    from BusinessEntityMaster a left join TblRegionMst b on a.Region= b.ID left join TBLCOUNTRYMST c on a.ID= c.ID left join TblCurrencyMst d on a.CurrencyId= d.ID 
                                    where
                                    (CONCAT(a.EntityName,b.Region,c.COUNTRYNAME,a.AddressLine1,a.BankAccountNo,a.BankAddress,a.BankSwiftCode,a.BankName,a.BeneficiaryName,a.EntityCode,d.CurrencyCode) LIKE @search) and a.IsDeleted=0 ORDER BY a.[ID] DESC";
                DynamicParameters param = new DynamicParameters();
                param.Add("@search", '%' + searchPhrase + '%' == null ? "" : '%' + searchPhrase + '%');
               // param.Add("@Year", Year);
                return dbConnection.Query<EntityData>(sqlQuery, param: param).ToList();
            }

        }
        //
        public static EntityData GetEntityById(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"select ID,EntityName,EntityCode,Description,RegistrationNumber,TaxNumber,CurrencyId,BeneficiaryName,BankName,BankAddress,BankSwiftCode,
		                           BankAccountNo,IBANNo,AddressLine1,AddressLine2,AddressLine3,ApprovalStatus,PostalCode,CityName,Region,CountryId,PhoneOffice,
		                           Email,Fax,WebSite from BusinessEntityMaster where ID = @ID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
                return dbConnection.QueryFirstOrDefault<EntityData>(sqlQuery, param: param);
            }
        }
        //
        public static EntityData GetEntityCode(string EntityCode)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"select ID,EntityName,EntityCode,Description,RegistrationNumber,TaxNumber,CurrencyId,BeneficiaryName,BankName,BankAddress,BankSwiftCode,
		                           BankAccountNo,IBANNo,AddressLine1,AddressLine2,AddressLine3,ApprovalStatus,PostalCode,CityName,Region,CountryId,PhoneOffice,
		                           Email,Fax,WebSite from BusinessEntityMaster where  EntityCode = @EntityCode";
                DynamicParameters param = new DynamicParameters();
                param.Add("@EntityCode", EntityCode);
                return dbConnection.QueryFirstOrDefault<EntityData>(sqlQuery, param: param);
            }
        }
        public static int InsertEntity(EntityModel ent)
        {
            int i = 0;
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", ent.ID);
                    param.Add("@EntityName", ent.EntityName);
                    param.Add("@EntityCode", ent.EntityCode);
                    param.Add("@Description", ent.Description);
                    param.Add("@RegistrationNumber", ent.RegistrationNumber);
                    param.Add("@TaxNumber", ent.TaxNumber);
                    param.Add("@CurrencyId", ent.CurrencyId);
                    param.Add("@BeneficiaryName", ent.BeneficiaryName);
                    param.Add("@BankName", ent.BankName);
                    param.Add("@BankAddress", ent.BankAddress);
                    param.Add("@BankSwiftCode", ent.BankSwiftCode);
                    param.Add("@BankAccountNo", ent.BankAccountNo);
                    param.Add("@IBANNo", ent.IBANNo);

                    param.Add("@AddressLine1", ent.AddressLine1);
                    param.Add("@AddressLine2", ent.AddressLine2);
                    param.Add("@AddressLine3", ent.AddressLine3);
                    param.Add("@PostalCode", ent.PostalCode);
                    param.Add("@CityName", ent.CityName);
                    param.Add("@Region", ent.Region);
                    param.Add("@CountryId", ent.CountryId);
                    param.Add("@PhoneOffice", ent.PhoneOffice);
                    param.Add("@Email", ent.Email);
                    param.Add("@Fax", ent.Fax);
                    param.Add("@WebSite", ent.WebSite);
                    param.Add("@IsActive", ent.IsActive);
                    param.Add("@CreatedBy", ent.CreatedBy);
                    param.Add("@CreatedAt", ent.CreatedAt);

                    param.Add("@ModifiedBy", ent.ModifiedBy);
                    param.Add("@ModifiedAt", ent.ModifiedAt);
                    param.Add("@IsDeleted", ent.IsDeleted);
                    param.Add("@SaveStatus", ent.SaveStatus);

                    param.Add("@EntryOrigin", ent.EntryOrigin);
                    param.Add("@ApprovalStatus", ent.ApprovalStatus);
                    param.Add("@RowVer", ent.RowVer);
                    string sqlQuery = @"Insert Into BusinessEntityMaster (EntityName,EntityCode,Description,RegistrationNumber,TaxNumber,
                                        CurrencyId,BeneficiaryName,BankName,BankAddress,BankSwiftCode,BankAccountNo,IBANNo,AddressLine1,AddressLine2,
                                        AddressLine3,PostalCode,CityName,Region,CountryId,PhoneOffice,Email,Fax,WebSite,IsActive,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,
                                        IsDeleted,SaveStatus,EntryOrigin,ApprovalStatus) values (@EntityName, @EntityCode, @Description, @RegistrationNumber, 
                                        @TaxNumber, @CurrencyId, @BeneficiaryName,@BankName,@BankAddress,@BankSwiftCode,@BankAccountNo,@IBANNo,@AddressLine1,@AddressLine2,
                                        @AddressLine3,@PostalCode,@CityName,@Region,@CountryId,@PhoneOffice,@Email,@Fax,@WebSite,@IsActive,@CreatedBy,@CreatedAt,@ModifiedBy,
                                        @ModifiedAt,0,@SaveStatus,@EntryOrigin,@ApprovalStatus)";

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
        public static int UpdateEntity(EntityModel ent)
        {
            int j = 0;
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", ent.ID);
                    param.Add("@EntityName", ent.EntityName);
                    param.Add("@EntityCode", ent.EntityCode);
                    param.Add("@Description", ent.Description);
                    param.Add("@RegistrationNumber", ent.RegistrationNumber);
                    param.Add("@TaxNumber", ent.TaxNumber);
                    param.Add("@CurrencyId", ent.CurrencyId);
                    param.Add("@BeneficiaryName", ent.BeneficiaryName);
                    param.Add("@BankName", ent.BankName);
                    param.Add("@BankAddress", ent.BankAddress);
                    param.Add("@BankSwiftCode", ent.BankSwiftCode);
                    param.Add("@BankAccountNo", ent.BankAccountNo);
                    param.Add("@IBANNo", ent.IBANNo);

                    param.Add("@AddressLine1", ent.AddressLine1);
                    param.Add("@AddressLine2", ent.AddressLine2);
                    param.Add("@AddressLine3", ent.AddressLine3);
                    param.Add("@PostalCode", ent.PostalCode);
                    param.Add("@CityName", ent.CityName);
                    param.Add("@Region", ent.Region);
                    param.Add("@CountryId", ent.CountryId);
                    param.Add("@PhoneOffice", ent.PhoneOffice);
                    param.Add("@Email", ent.Email);
                    param.Add("@Fax", ent.Fax);
                    param.Add("@WebSite", ent.WebSite);
                    param.Add("@IsActive", ent.IsActive);
                    param.Add("@CreatedBy", ent.CreatedBy);
                    param.Add("@CreatedAt", ent.CreatedAt);

                    param.Add("@ModifiedBy", ent.ModifiedBy);
                    param.Add("@ModifiedAt", ent.ModifiedAt);
                    param.Add("@IsDeleted", ent.IsDeleted);
                    param.Add("@SaveStatus", ent.SaveStatus);

                    param.Add("@EntryOrigin", ent.EntryOrigin);
                    param.Add("@ApprovalStatus", ent.ApprovalStatus);
                    param.Add("@RowVer", ent.RowVer);
                    string sqlQuery = @"Update BusinessEntityMaster set EntityName=@EntityName,EntityCode=@EntityCode,Description=@Description,RegistrationNumber=@RegistrationNumber,TaxNumber= @TaxNumber,
                                        CurrencyId=@CurrencyId,BeneficiaryName=@BeneficiaryName,BankName=@BankName,BankAddress=@BankAddress,BankSwiftCode=@BankSwiftCode,BankAccountNo=@BankAccountNo,IBANNo=@IBANNo,
                                        AddressLine1=@AddressLine1,AddressLine2=@AddressLine2,
                                        AddressLine3=@AddressLine3,PostalCode=@PostalCode,CityName=@CityName,Region=@Region,CountryId=@CountryId,PhoneOffice=@PhoneOffice,Email=@Email,Fax=@Fax,WebSite=@WebSite,
                                        IsActive=@IsActive,CreatedBy=@CreatedBy,CreatedAt=@CreatedAt,ModifiedBy=@ModifiedBy,ModifiedAt=@ModifiedAt,
                                        IsDeleted = 0,SaveStatus=@SaveStatus,EntryOrigin=@EntryOrigin,ApprovalStatus=@ApprovalStatus where ID=@ID";

                    j = dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return j;
        }
        public static EntityData GetEntityByCode(string Countrycode)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"select ID,EntityName,EntityCode,Description,RegistrationNumber,TaxNumber,CurrencyId,BeneficiaryName,
									BankName,BankAddress,BankAccountNo,IBANNo,AddressLine1,AddressLine2,AddressLine3,PostalCode,
									CityName,Region,CountryId,PhoneOffice,
									Email,Fax,WebSite,SaveStatus,EntryOrigin,ApprovalStatus from BusinessEntityMaster where CountryId = @CountryId";
                DynamicParameters param = new DynamicParameters();
               
                param.Add("@CountryId", Countrycode);
                return dbConnection.QueryFirstOrDefault<EntityData>(sqlQuery, param: param);
            }
        }
        
        public static void ActivateEntityById(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = "update  BusinessEntityMaster set IsDeleted=0 where ID=@ID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
                dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
            }

        }

        public static void Delete(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                //string sqlQuery = "Delete from BusinessEntityMaster Where ID = @ID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
                //dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
                dbConnection.ExecuteScalar<int>("spDeleteEntity", param: param, commandType: CommandType.StoredProcedure);
            }
        }
        public static List<EntityData> GetEntityForDep()
        {
           
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"SELECT a.[ID] As EntityID,a.[EntityCode] FROM BusinessEntityMaster a WHERE a.IsDeleted=0";

                return dbConnection.Query<EntityData>(sqlQuery).ToList();
            }
        }


    }
}