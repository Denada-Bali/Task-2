using System.ComponentModel.DataAnnotations;

namespace LaconicsCrm.webapi.Models.Domain
{
    public class Product
    {
        [Key]
        public Guid productId { get; set; }

        [Required]
        public string productName { get; set; }
        public decimal price { get; set; }

       public Guid orderId { get; set; }
        // Navigation property for many-to-many relationship
 

    }
}
