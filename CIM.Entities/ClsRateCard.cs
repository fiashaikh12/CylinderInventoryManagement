using System.ComponentModel.DataAnnotations;

namespace CIM.Entities
{
    public class ClsRateCard
    {
        [Required(ErrorMessage = "Please enter user")]
        public string Username { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please select category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter rate card")]
        public double RateCard { get; set; }
    }
}
