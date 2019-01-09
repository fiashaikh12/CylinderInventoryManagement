using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class ClsUserRegistrationModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string CompanyName { get; set; }
        public int TypeId { get; set; }
        public string BusinessId { get; set; }
        public bool IsActive { get; set; }
        public string GSTNo { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int DeletedOn { get; set; }
        public DateTime? DeletedBy { get; set; }
    }

    public class ClsUserLoginModel
    {
        public string  MobileNumber { get; set; }
        public string Password { get; set; }
    }
}
