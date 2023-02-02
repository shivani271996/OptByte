using CostEstimation.Areas.Masters.Models.CustomerManagement;
using CostEstimation.Areas.Masters.Models.DepartmentManagement;
using CostEstimation.Common;
using CostEstimation.Common.Const;
using CostEstimation.FX;
using CostEstimation.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CostEstimation.Areas.Masters.Controllers
{
    public class DepartmentMasterController : BaseController
    {

        // GET: /Masters/DepartmentMaster/
        [CetAuthorize(ConstPageName.DEPARTMENTMASTER, Permissions.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [CetAuthorize(ConstPageName.DEPARTMENTMASTER, Permissions.VIEW)]
        public ActionResult GetDepBrowser(DevExGridPageViewModel devExGridPageView)
        {
            var sortval = devExGridPageView.Sorts();
            string selector = sortval.Count > 0 ? sortval[0].selector : string.Empty;
            bool desc = sortval.Count > 0 ? sortval[0].desc : false;
            string searchPhrase = devExGridPageView.GetSearchPhrase();

            List<DepartmentMasterModel> model = DepartmentMasterManager.GetDep(searchPhrase);
            //Utils
            var sortedModel = Utils.OrderBy<DepartmentMasterModel>(model, selector, desc);

            if (devExGridPageView.take == 0) 
                devExGridPageView.take = model.Count;

            return Json(new { rows = sortedModel.Skip(devExGridPageView.skip).Take(devExGridPageView.take), totalCount = model.Count }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [CetAuthorize(ConstPageName.DEPARTMENTMASTER, Permissions.UPDATE, Permissions.ADD, Permissions.VIEW)]
        public ActionResult DepartmentMaster(DepartmentMasterModel model)
        {
            try
            {

                model.CopyProperties(model);

                model.CreatedBy = LogonUserInfo.UserId;
              
                model.ModifiedBy = LogonUserInfo.UserId;

                model.SaveStatus = "Save";
                model.ApprovalStatus = "A";
                model.IsDeleted = 0;
                model.IsActive = 1;
                model.EntryOrigin = "User";

                var _dep = DepartmentMasterManager.GetDepByCode(model.DPTCODE);
                if (_dep != null)
                {
                    if (model.ID == 0 && !string.IsNullOrEmpty(_dep.DPTCODE))
                    {
                        if (_dep.IsDeleted == 1)
                            DepartmentMasterManager.ActivateDepById(model.ID);
                        else
                            return Json(new { isExist = true }, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                int newdepId = DepartmentMasterManager.Save(model);


                string message = model.ID > 0 ? $"AddDepartment : {model.ID}" : $"New Department : {newdepId}";

                ActivityLogManager.InsertLog("DepartmentMaster-InsertDep", message, Ip, LogonUserName);

                return Json(new { Success = true, newdepId });

            }
            catch (Exception ex)
            {
                ActivityLogManager.InsertLog("DepartmentMaster-InsertDep", ex.ToString(), Ip, LogonUserName);
                return Json(new { Success = false, errors = ex.Message });
            }

        }

        [HttpPost]
        [CetAuthorize(ConstPageName.DEPARTMENTMASTER, Permissions.VIEW)]
        public ActionResult GetDepartmentById(int ID)
        {
            DepartmentMasterModel dep = ID == 0 ? new DepartmentMasterModel() : DepartmentMasterManager.GetDepartmentById(ID);
            return Json(dep, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CetAuthorize(ConstPageName.DEPARTMENTMASTER, Permissions.DELETE)]
        public ActionResult DeleteById(int ID)
        {
            DepartmentMasterManager.Delete(ID);
            ActivityLogManager.InsertLog("Delete Department", $"Deleted Department : {ID}", Ip, LogonUserName);
            return Json(new { Sucess = "Done" });
        }
    }
}
