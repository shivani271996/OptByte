using CostEstimation.DAL;
using CostEstimation.Managers.Model;
using CostEstimation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.BusinessEntityManagement
{
    public class EntityManager
    {

        public static List<EntityModel> GetEntity(string searchPhrase)
        {
            searchPhrase = string.IsNullOrEmpty(searchPhrase) ? null : searchPhrase.Trim();
            List<EntityData> entities = EntityDal.GetEntity(searchPhrase);
            List<EntityModel> entityModel = new List<EntityModel>();
            EntityModel entity = null;
            foreach (var item in entities)
            {
                entity = new EntityModel
                {
                    RowNumber = item.RowNumber,
                    ID = item.ID,
                    EntityName = item.EntityName,
                    EntityCode = item.EntityCode,
                    Description = item.Description,
                    RegistrationNumber = item.RegistrationNumber,
                    TaxNumber = item.TaxNumber,
                    CurrencyId = item.CurrencyId,
                    BeneficiaryName = item.BeneficiaryName,
                    BankName = item.BankName,
                    BankAddress = item.BankAddress,
                    BankSwiftCode = item.BankSwiftCode,
                    BankAccountNo = item.BankAccountNo,
                    IBANNo = item.IBANNo,
                    AddressLine1 = item.AddressLine1,
                    AddressLine2 = item.AddressLine2,
                    AddressLine3 = item.AddressLine3,
                    PostalCode = item.PostalCode,
                    CityName = item.CityName,
                    Region = item.Region,
                    CountryId = item.CountryId,
                    PhoneOffice = item.PhoneOffice,
                    Email = item.Email,
                    Fax = item.Fax,
                    WebSite = item.WebSite,
                    EntryOrigin = item.EntryOrigin

                };
                entityModel.Add(entity);
            }
            return entityModel;
        }
        //GetById
        public static EntityModel GetEntityById(int ID)
        {
            EntityData item = EntityDal.GetEntityById(ID);
            return new EntityModel()
            {
                ID = item.ID,
                EntityName = item.EntityName,
                EntityCode = item.EntityCode,
                Description = item.Description,
                RegistrationNumber = item.RegistrationNumber,
                TaxNumber = item.TaxNumber,
                CurrencyId = item.CurrencyId,
                BeneficiaryName = item.BeneficiaryName,
                BankName = item.BankName,
                BankAddress = item.BankAddress,
                BankSwiftCode = item.BankSwiftCode,
                BankAccountNo = item.BankAccountNo,
                IBANNo = item.IBANNo,
                AddressLine1 = item.AddressLine1,
                AddressLine2 = item.AddressLine2,
                AddressLine3 = item.AddressLine3,
                ApprovalStatus = item.ApprovalStatus,
                PostalCode = item.PostalCode,
                CityName = item.CityName,
                Region = item.Region,
                CountryId = item.CountryId,
                PhoneOffice = item.PhoneOffice,
                Email = item.Email,
                Fax = item.Fax,
                WebSite = item.WebSite,
                EntryOrigin = item.EntryOrigin,

            };
        }
        public static EntityModel GetEntityCode(string EntityCode)
        {
            EntityData item = EntityDal.GetEntityCode(EntityCode);
            if (item != null)
            {
                return new EntityModel()
                {
                    ID = item.ID,
                    EntityName = item.EntityName,
                    EntityCode = item.EntityCode,
                    Description = item.Description,
                    RegistrationNumber = item.RegistrationNumber,
                    TaxNumber = item.TaxNumber,
                    CurrencyId = item.CurrencyId,
                    BeneficiaryName = item.BeneficiaryName,
                    BankName = item.BankName,
                    BankAddress = item.BankAddress,
                    BankSwiftCode = item.BankSwiftCode,
                    BankAccountNo = item.BankAccountNo,
                    IBANNo = item.IBANNo,
                    AddressLine1 = item.AddressLine1,
                    AddressLine2 = item.AddressLine2,
                    AddressLine3 = item.AddressLine3,
                    ApprovalStatus = item.ApprovalStatus,
                    PostalCode = item.PostalCode,
                    CityName = item.CityName,
                    Region = item.Region,
                    CountryId = item.CountryId,
                    PhoneOffice = item.PhoneOffice,
                    Email = item.Email,
                    Fax = item.Fax,
                    WebSite = item.WebSite,
                    EntryOrigin = item.EntryOrigin,
                };
            }
            else
            {
                return new EntityModel();
            }
        }
        public static int Save(EntityModel model)
        {
            int retVal;
            if (model.ID > 0  )
            {
                //call update
                retVal = EntityDal.UpdateEntity(model);
            }
            else
            {
                retVal = EntityDal.InsertEntity(model);
            }
            return retVal;
        }
        public static EntityModel GetEntityByCode(string typeCode)
        {

            EntityData item = EntityDal.GetEntityByCode(typeCode);
            if (item != null)
            {
                return new EntityModel()
                {
                    ID = item.ID,
                    EntityName = item.EntityName,
                    EntityCode = item.EntityCode,
                    Description = item.Description,
                    RegistrationNumber = item.RegistrationNumber,
                    TaxNumber = item.TaxNumber,
                    CurrencyId = item.CurrencyId,
                    BeneficiaryName = item.BeneficiaryName,
                    BankName = item.BankName,
                    BankAddress = item.BankAddress,
                    BankSwiftCode = item.BankSwiftCode,
                    BankAccountNo = item.BankAccountNo,
                    IBANNo = item.IBANNo,
                    AddressLine1 = item.AddressLine1,
                    AddressLine2 = item.AddressLine2,
                    AddressLine3 = item.AddressLine3,
                    PostalCode = item.PostalCode,
                    CityName = item.CityName,
                    Region = item.Region,
                    CountryId = item.CountryId,
                    PhoneOffice = item.PhoneOffice,
                    Email = item.Email,
                    Fax = item.Fax,
                    WebSite = item.WebSite,
                    EntryOrigin = item.EntryOrigin,
                };
            }
            else
            {
                return new EntityModel();
            }
        }
        
        public static void ActivateEntityById(int ID)
        {
            EntityDal.ActivateEntityById(ID);
        }

        public static void Delete(int ID)
        {
            EntityDal.Delete(ID);

        }
        public static List<EntityModel> GetEntityForDep()
        {
            List<EntityData> ent = EntityDal.GetEntityForDep();
            List<EntityModel> models = new List<EntityModel>();
            EntityModel entity = null;
            foreach (var item in ent)
            {
                entity = new EntityModel
                {
                    EntityID = item.EntityID,
                    EntityCode= item.EntityCode
                   
                };
                models.Add(entity);
            }
            return models;
        }

    }
}