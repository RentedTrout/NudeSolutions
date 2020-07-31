
using System.ComponentModel.DataAnnotations;

namespace NudeSolutions.Models
{
    public class InsuranceItem
    {        
        public int ID { get; set; }

        [Required]
        public int InsuranceCategoryID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an amount.")]
        public decimal Amount { get; set; }
    }
}