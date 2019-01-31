using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.Entities
{
    public class ClsCustomerPurchase
    {
        public int UserId { get; set; }
        public bool IsDepositGiven { get; set; }
        public bool IsDepositReturn { get; set; }
        public double DepositAmount { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public int Quantity { get; set; }
        public int BusinessId { get; set; }
    }
}
