using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostEstimation.Areas.Masters.Models.DepartmentManagement
{
    public class DepartmentMasterModel
    {
        
        public int RowNumber { get; set; }
        public int ID { get; set; }
        public string DPTCODE { get; set; }
        public string DPTNAME { get; set; }
        public string ENT { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        
        public int ModifiedBy { get; set; }
        
        public int IsDeleted { get; set; }
        public string SaveStatus { get; set; }
        public string EntryOrigin { get; set; }
        public string ApprovalStatus { get; set; }
        public byte[] RowVer { get; set; }
    }
}