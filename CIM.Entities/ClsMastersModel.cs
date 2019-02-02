using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.Entities
{
    public class ClsBusinessMasterModel
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessLocation { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    public class ClsCategoryMasterModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public int UserId { get; set; }
        public string flag { get; set; }
        public int IsActive { get; set; }
    }
    public class ClsSubCategoryMasterModel: ClsCommonModel
    {
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Please select category"),Display(Name ="Category Id")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please provide sub category"),Display(Name = "Sub Category")]
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    public class ClsUserTypeMasterModel
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
