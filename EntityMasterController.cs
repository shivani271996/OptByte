using CostEstimation.Areas.Masters.Models.BusinessEntityManagement;
using CostEstimation.Areas.Masters.Models.CustomerManagement;
using CostEstimation.Common;
using CostEstimation.Common.Const;
using CostEstimation.FX;
using CostEstimation.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
//using System.Windows.Forms;

namespace CostEstimation.Areas.Masters.Controllers
{
    public class EntityMasterController : BaseController           
    {
        //
        // GET: /Masters/EntityMaster/
        [CetAuthorize(ConstPageName.ENTITYMASTER, Permissions.VIEW)]
        public ActionResult Index()
        {
            return View();
        }
        

        [CetAuthorize(ConstPageName.ENTITYMASTER, Permissions.VIEW)]
        public ActionResult GetEntityBrowser(DevExGridPageViewModel devExGridPageView)
        {
            var sortval = devExGridPageView.Sorts();
            string selector = sortval.Count > 0 ? sortval[0].selector : string.Empty;
            bool desc = sortval.Count > 0 ? sortval[0].desc : false;
            string searchPhrase = devExGridPageView.GetSearchPhrase();

            List<EntityModel> model = EntityManager.GetEntity(searchPhrase);

            var sortedModel = Utils.OrderBy<EntityModel>(model, selector, desc);
            
            if(devExGridPageView.take == 0) //incase of export
                devExGridPageView.take = model.Count;

            return Json(new { rows = sortedModel.Skip(devExGridPageView.skip).Take(devExGridPageView.take), totalCount = model.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CetAuthorize(ConstPageName.ENTITYMASTER, Permissions.VIEW)]
        public ActionResult GetEntityById(int ID)
        {
            EntityModel entity = ID == 0 ? new EntityModel() : EntityManager.GetEntityById(ID);
            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CetAuthorize(ConstPageName.ENTITYMASTER, Permissions.UPDATE, Permissions.ADD, Permissions.VIEW)]
        public ActionResult EntityMaster(EntityModel model)
        {
            try
            {

                model.CopyProperties(model);

                model.CreatedBy = LogonUserInfo.UserId;
                model.CreatedAt = DateTimeOffset.Now;
                model.ModifiedAt = DateTime.UtcNow;
                model.ModifiedBy = LogonUserInfo.UserId;
               
                model.SaveStatus = "Save";
                model.ApprovalStatus = "A";
                model.IsDeleted = 0;
                model.IsActive = 1;
                model.EntryOrigin = "User";
                var _ent = EntityManager.GetEntityCode(model.EntityCode);
                if (_ent != null)
                {
                    if (model.ID == 0 && !string.IsNullOrEmpty(_ent.EntityCode))
                    {
                        if (_ent.IsDeleted == 1)
                            EntityManager.ActivateEntityById(model.ID);
                        else
                            return Json(new { isExist = true }, JsonRequestBehavior.AllowGet);
                    }
                    
                }

                int newentityId = EntityManager.Save(model);
               

                string message = model.ID > 0 ? $"AddEntity : {model.ID}" : $"New Entity : {newentityId}";

                ActivityLogManager.InsertLog("EntityMaster-InsertEntity", message, Ip, LogonUserName);

                return Json(new { Success = true, newentityId });
               
            }
            //catch (Exception ex)
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UniqueConstraint"))
                    throw;

                
                ActivityLogManager.InsertLog("EntityMaster-InsertEntity", ex.ToString(), Ip, LogonUserName);
                return Json(new { Success = false, errors = ex.Message });
            }
           
        }

        [HttpPost]
        [CetAuthorize(ConstPageName.ENTITYMASTER, Permissions.DELETE)]
        public ActionResult DeleteById(int ID)
        {
            EntityManager.Delete(ID);
            ActivityLogManager.InsertLog("Delete Entity", $"Deleted Entity : {ID}", Ip, LogonUserName);
            return Json(new { Sucess = "Done" });
        }


        public ActionResult GetEntityForDep()
        {
            List<EntityModel> models = EntityManager.GetEntityForDep();
            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}
