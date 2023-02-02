using CostEstimation.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.DepartmentManagement
{
    public class DepartmentMasterDal
    {

        public static List<DepartmentMasterData> GetDep(string searchPhrase)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {

                string sqlQuery = @"select row_number() over(order by a.ID desc) as 'RowNumber', a.ID,a.DPTCODE,a.DPTNAME,b.EntityCode As ENT
                                    from Department_Master a left join BusinessEntityMaster b on a.ENT= b.ID 
                                    where
                                    (CONCAT(a.DPTCODE,DPTNAME,b.EntityCode) LIKE @search) and a.IsDeleted=0 ORDER BY a.ID DESC";
                DynamicParameters param = new DynamicParameters();
                param.Add("@search", '%' + searchPhrase + '%' == null ? "" : '%' + searchPhrase + '%');
               
                return dbConnection.Query<DepartmentMasterData>(sqlQuery, param: param).ToList();
            }

        }


        public static int InsertDep(DepartmentMasterModel dep)
        {
            int i = 0;
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", dep.ID);
                    param.Add("@DPTNAME", dep.DPTNAME);
                    param.Add("@DPTCODE", dep.DPTCODE);
                    param.Add("@ENT", dep.ENT);

                    param.Add("@CreatedBy", dep.CreatedBy);

                    param.Add("@ModifiedBy", dep.ModifiedBy);
                    param.Add("@IsActive", dep.IsActive);

                    param.Add("@IsDeleted", dep.IsDeleted);
                    param.Add("@SaveStatus", dep.SaveStatus);

                    param.Add("@EntryOrigin", dep.EntryOrigin);
                    param.Add("@ApprovalStatus", dep.ApprovalStatus);
                    param.Add("@RowVer", dep.RowVer);
                    string sqlQuery = @"Insert into Department_Master (DPTCODE,DPTNAME,ENT,CreatedBy,ModifiedBy,IsActive,IsDeleted,SaveStatus,EntryOrigin,ApprovalStatus) values (@DPTCODE, @DPTNAME,@ENT,@IsActive,@CreatedBy,@ModifiedBy,0,@SaveStatus,@EntryOrigin,@ApprovalStatus)";

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
        public static int UpdateDep(DepartmentMasterModel dep)
        {
            int j = 0;
            try
            {
                using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@ID", dep.ID);
                    param.Add("@DPTNAME", dep.DPTNAME);
                    param.Add("@DPTCODE", dep.DPTCODE);
                    param.Add("@ENT", dep.ENT);

                    param.Add("@CreatedBy", dep.CreatedBy);

                    param.Add("@ModifiedBy", dep.ModifiedBy);
                    param.Add("@IsActive", dep.IsActive);

                    param.Add("@IsDeleted", dep.IsDeleted);
                    param.Add("@SaveStatus", dep.SaveStatus);

                    param.Add("@EntryOrigin", dep.EntryOrigin);
                    param.Add("@ApprovalStatus", dep.ApprovalStatus);
                    param.Add("@RowVer", dep.RowVer);
                    string sqlQuery = @"Update Department_Master set DPTCODE=@DPTCODE,DPTNAME=@DPTNAME,ENT=@ENT,IsActive=@IsActive,CreatedBy=@CreatedBy,ModifiedBy=@ModifiedBy,IsDeleted = 0,SaveStatus=@SaveStatus,EntryOrigin=@EntryOrigin,ApprovalStatus=@ApprovalStatus where ID=@ID";

                    j = dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return j;
        }
        public static DepartmentMasterData GetDepartmentById(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"select ID, DPTCODE, DPTNAME,ENT from Department_Master where ID = @ID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
                return dbConnection.QueryFirstOrDefault<DepartmentMasterData>(sqlQuery, param: param);
            }
        }
        public static DepartmentMasterData GetDepByCode(string DPTCODE)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = @"select ID, DPTCODE, DPTNAME,ENT from Department_Master where DPTCODE = @DPTCODE";
                DynamicParameters param = new DynamicParameters();
                param.Add("@DPTCODE", DPTCODE);
                return dbConnection.QueryFirstOrDefault<DepartmentMasterData>(sqlQuery, param: param);
            }
        }
        public static void ActivateDepById(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                string sqlQuery = "update Department_Master set IsDeleted=0 where ID = @ID";
                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);
                dbConnection.ExecuteScalar<int>(sqlQuery, param: param);
            }

        }
        public static void Delete(int ID)
        {
            using (IDbConnection dbConnection = new DbConnectionFactory().GetDbConnection())
            {
                //string sqlQuery = "update Department_Master set IsDeleted=0 where ID=@ID";
                //DynamicParameters param = new DynamicParameters();
                //param.Add("@ID", ID);
                //dbConnection.ExecuteScalar<int>(sqlQuery, param: param);

                DynamicParameters param = new DynamicParameters();
                param.Add("@ID", ID);

                dbConnection.ExecuteScalar<int>("spDeleteDepartment", param: param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}