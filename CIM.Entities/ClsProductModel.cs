using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIM.Entities
{
    public class ClsProductModel
    {
        [Required(ErrorMessage = "Please select category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please select sub category")]
        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please provide quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please select deposit allowed")]
        [Display(Name = "Deposit Allowed")]
        public bool IsDepositAllowed { get; set; }
        [Required(ErrorMessage = "Please select exchange allowed")]
        [Display(Name = "Exchange Allowed")]
        public bool IsExchangeAllowed { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please provide price")]
        public double Price { get; set; }
    }
}
