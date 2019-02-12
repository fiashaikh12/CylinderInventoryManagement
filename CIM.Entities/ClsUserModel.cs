using System;
using System.ComponentModel.DataAnnotations;

namespace CIM.Entities
{
    public class ClsCustomerModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage ="Please enter full name")]
        public string Name { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        [RegularExpression("^[0-9]*$", ErrorMessage ="Only numeric value allowed")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please enter company name")]
        public string CompanyName { get; set; }
        public int TypeId { get; set; }
        public string BusinessId { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please enter gst no")]
        public string GSTNo { get; set; }
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int DeletedOn { get; set; }
        public DateTime? DeletedBy { get; set; }
        public float DepositAmount { get; set; }
    }

    public class ClsUserLoginModel
    {
        public string  Mobile { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }


    public class ClsUserHoldingStockModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int HoldingStock { get; set; }
    }
}
