using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.DepartmentManagement
{
    public class DepartmentMasterManager
    {
        public static List<DepartmentMasterModel> GetDep(string searchPhrase)
        {
            searchPhrase = string.IsNullOrEmpty(searchPhrase) ? null : searchPhrase.Trim();
            List<DepartmentMasterData> dpt = DepartmentMasterDal.GetDep(searchPhrase);
            List<DepartmentMasterModel> dptModel = new List<DepartmentMasterModel>();
            DepartmentMasterModel dep = null;
            foreach (var item in dpt)
            {
                dep = new DepartmentMasterModel
                {
                    RowNumber = item.RowNumber,
                    ID = item.ID,
                    DPTCODE = item.DPTCODE,
                    DPTNAME = item.DPTNAME,
                    ENT = item.ENT

                };
                dptModel.Add(dep);
            }
            return dptModel;
        }

        public static int Save(DepartmentMasterModel model)
        {
            int retVal;
            if (model.ID > 0)
            {
                
                retVal = DepartmentMasterDal.UpdateDep(model);
            }
            else
            {
                retVal = DepartmentMasterDal.InsertDep(model);
            }
            return retVal;
        }
        public static void ActivateDepById(int ID)
        {
            DepartmentMasterDal.ActivateDepById(ID);
        }

        public static DepartmentMasterModel GetDepByCode(string DPTCODE)
        {
            DepartmentMasterData dep = DepartmentMasterDal.GetDepByCode(DPTCODE);
            if (dep != null)
            {
                return new DepartmentMasterModel()
                {
                   
                    ID = dep.ID,
                    DPTCODE = dep.DPTCODE,
                    DPTNAME = dep.DPTNAME,
                    ENT = dep.ENT
                };
            }
            else
            {
                return new DepartmentMasterModel();
            }
        }

        public static DepartmentMasterModel GetDepartmentById(int ID)
        {
            DepartmentMasterData item = DepartmentMasterDal.GetDepartmentById(ID);
            return new DepartmentMasterModel()
            {
                ID=item.ID,
                DPTCODE = item.DPTCODE,
                DPTNAME = item.DPTNAME,
                ENT = item.ENT

            };
        }
        public static void Delete(int ID)
        {
            DepartmentMasterDal.Delete(ID);

        }

    }
}