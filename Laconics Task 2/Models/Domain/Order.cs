using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaconicsCrm.webapi.Models.Domain
{
    public class Order
    {
        [Key]
        public Guid orderId { get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public Guid customerId { get; set; } // dhe kjo esht id e konsumatorit 
        public DateTime? date { get; set; }
        public decimal totalAmount { get; set; }

        //navigation properties
     //   public Customer Customer { get; set; } //i tregojm entity framework core qe nje Order do te kete nje konsumator
    
        public ICollection<Product> Products { get; set; }

 
    }
}
