using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.Entities
{
    public class ClsProductDetailModel
    {
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name ="Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Sub category")]
        public string SubCategoryName { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Deposit Allowed")]
        public bool IsDepositAllowed { get; set; }
        [Display(Name = "Exchange Allowed")]
        public bool IsExchangeAllowed { get; set; }
        //[Display(Name = "Product Status")]
        //public string ProductStatus { get; set; }
        public int Price { get; set; }
    }
}
