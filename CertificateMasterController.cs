using CostEstimation.Areas.Masters.Models.BrandManagement;
using CostEstimation.Areas.Masters.Models.CertificateMaster;
using CostEstimation.Areas.Masters.Models.CustomerManagement;
using CostEstimation.Areas.Masters.Models.DepartmentManagement;
using CostEstimation.Areas.Masters.Models.FactoryPLManagement;
using CostEstimation.Areas.Masters.Models.LineManagement;
using CostEstimation.Areas.Masters.Models.RegionManagement;
using CostEstimation.Common;
using CostEstimation.Common.API.Models.EquipmentManagement;
using CostEstimation.Common.Const;
using CostEstimation.FX;
using CostEstimation.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CostEstimation.Areas.Masters.Controllers
{
    public class CertificateMasterController : BaseController
    {
        // GET: Masters/CertificateMaster
        [CetAuthorize(ConstPageName.CERTIFICATEMASTER, Permissions.VIEW)]
        public ActionResult Index()
        {
            
            return View();
        }

        [CetAuthorize(ConstPageName.CERTIFICATEMASTER, Permissions.VIEW)]
        public ActionResult GetBrowser(DevExGridPageViewModel devExGridPageView)
        {
            var sortval = devExGridPageView.Sorts();
            string selector = sortval.Count > 0 ? sortval[0].selector : string.Empty;
            bool desc = sortval.Count > 0 ? sortval[0].desc : false;
            string searchPhrase = devExGridPageView.GetSearchPhrase();

            List<CertificateModel> model = CertificateManager.GetCertificate(searchPhrase);

            var sortedModel = Utils.OrderBy<CertificateModel>(model, selector, desc);

            if (devExGridPageView.take == 0) //incase of export
                devExGridPageView.take = model.Count;

            return Json(new { rows = sortedModel.Skip(devExGridPageView.skip).Take(devExGridPageView.take), totalCount = model.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CetAuthorize(ConstPageName.CERTIFICATEMASTER, Permissions.UPDATE, Permissions.ADD, Permissions.VIEW)]
        public ActionResult SaveCertificateMaster(CertificateModel model)
        {
            try
            {
                
                model.CopyProperties(model);

                if (!string.IsNullOrEmpty(model.Certificatefilepath))
                {
                    model.Certificatefile = System.IO.File.ReadAllBytes(model.Certificatefilepath);
                    if (System.IO.File.Exists(model.Certificatefilepath))
                    {
                        System.IO.File.Delete(model.Certificatefilepath);
                    }

                }

                model.CreatedBy = LogonUserInfo.UserId;

                model.ModifiedBy = LogonUserInfo.UserId;

                model.SaveStatus = "Save";
                model.ApprovalStatus = "A";
                model.IsDeleted = 0;
                model.IsActive = 1;
                model.EntryOrigin = "User";

                var _certificate = CertificateManager.GetCode(model.Certificateshortcode);
                if (_certificate != null)
                {
                    if (model.ID == 0 && !string.IsNullOrEmpty(_certificate.Certificateshortcode))
                    {
                        if (_certificate.IsDeleted == 1)
                            CertificateManager.ActivateByID(model.ID);
                        else
                            return Json(new { isExist = true }, JsonRequestBehavior.AllowGet);
                    }

                }

                int newId = CertificateManager.Save(model);


                string message = model.ID > 0 ? $"AddCertifiacte : {model.ID}" : $"New Certificate : {newId}";

                ActivityLogManager.InsertLog("CertificateMaster-Insert", message, Ip, LogonUserName);

                return Json(new { Success = true, newId });

            }
            catch (Exception ex)
            {
               
                ActivityLogManager.InsertLog("CertificateMaster-Insert", ex.ToString(), Ip, LogonUserName);
                return Json(new { Success = false, errors = ex.Message });
            }


           
        }

        [HttpPost]
        [CetAuthorize(ConstPageName.CERTIFICATEMASTER, Permissions.DELETE)]
        public ActionResult DeleteById(int ID)
        {
            CertificateManager.Delete(ID);
            ActivityLogManager.InsertLog("Delete Certificate", $"Deleted Certificate : {ID}", Ip, LogonUserName);
            return Json(new { Sucess = "Done" });
        }


        [HttpPost]
        [CetAuthorize(ConstPageName.CERTIFICATEMASTER, Permissions.VIEW)]
        public ActionResult GetById(int ID)
        {
            CertificateModel set = ID == 0 ? new CertificateModel() : CertificateManager.GetById(ID);

            return Json(set, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData()
        {
            List<certificateViewModel> entity =  CertificateManager.GetData();

            return Json(new { seriesdata = entity}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CetAuthorize(new string[] { ConstPageName.CERTIFICATEMASTER }, Permissions.ADD)]
        public ActionResult UploadFile()
        {
            
            try
            {

                HttpPostedFileBase file = Request.Files[0];
                string fname = Utils.GetTempFileName();

               
                file.SaveAs(fname);

                // Returns message that successfully uploaded  
                return Json(new { filepath = fname, contenttype = file.ContentType });

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [CetAuthorize(ConstPageName.CERTIFICATEMASTER, Permissions.VIEW)]
        public ActionResult DownloadFile(int ID)
        {
            certificatedownloadmodel viewModel = CertificateManager.GetAttcmntForDwnld(ID);
           
            byte[] fileByte = null;
            if (viewModel != null)
            {
                fileByte = viewModel.Certificatefile;
                return File(fileByte, "multipart/form-data", viewModel.Certificatefilename);
            }
            return File(fileByte, null);
        }

        public ActionResult GetLineForFactoryIds(string FactoryIds , string BrandIds)
        {
            List<LineViewModel> models = CertificateManager.GetLineForFactoryIds(FactoryIds, BrandIds);
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoryForFactoryIds(string LineIds, string FactoryIds, string BrandIds)
        {
            List<LineViewModel> models = CertificateManager.GetCategoryForFactoryIds(LineIds, FactoryIds, BrandIds);
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSeriesForLineIds(string CategoryIds, string FactoryIds, string LineIds)
        {
            List<LineViewModel> models = CertificateManager.GetSeriesForLineIds(CategoryIds, LineIds, FactoryIds);
            return Json(models, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetSeriesForLineIds(string CategoryIds)
        //{
        //    List<LineViewModel> models = CertificateManager.GetSeriesForLineIds(CategoryIds);
        //    return Json(models, JsonRequestBehavior.AllowGet);
        //}
    }
}