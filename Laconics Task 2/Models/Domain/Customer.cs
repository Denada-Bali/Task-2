using System.ComponentModel.DataAnnotations;

namespace LaconicsCrm.webapi.Models.Domain
{
    public class Customer
    {
        [Key]
        public Guid id { get; set; } //type guid -> unique identifire ~ id 
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }

       
        // Navigation property
      // public ICollection<Order> Orders { get; set; }

    }
}
