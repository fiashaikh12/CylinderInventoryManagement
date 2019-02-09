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
        public int UserId { get; set; }
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        [Display(Name ="Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Sub category")]
        public string SubCategoryName { get; set; }
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Deposit Allowed")]
        public bool IsDepositAllowed { get; set; }
        [Display(Name = "Exchange Allowed")]
        public bool IsExchangeAllowed { get; set; }
        [Display(Name = "Available Quantity")]
        public int AvailableQuantity { get; set; }
        [Display(Name = "Product Price")]
        public double Price { get; set; }
        [Display(Name = "Deposit Amount")]
        public double DepositAmount { get; set; }
        [Display(Name = "Purchase Quantity")]
        public int PurchaseQuantity { get; set; }
        [Display(Name = "HoldingStock")]
        public int HoldingStock { get; set; }
    }
}
