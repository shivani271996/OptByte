using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.BusinessEntityManagement
{
    public class EntityModel
    {
        public int EntityID { get; set; }
        public int ID { get; set; }
        public string EntityName { get; set; }
        public string EntityCode { get; set; }
        public string Description { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxNumber { get; set; }
        public int RowNumber { get; set; }
       
        public string CurrencyId { get; set; }
        
        public string BeneficiaryName { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankSwiftCode { get; set; }
        public string BankAccountNo { get; set; }
        public string IBANNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public string Region { get; set; }
       
        public string CountryId { get; set; }
        
        public string PhoneOffice { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int IsDeleted { get; set;}
        public string SaveStatus { get; set; }
        public string EntryOrigin { get; set; }
        public string ApprovalStatus { get; set; }
        public byte[] RowVer { get; set; }
    }
}