using System.ComponentModel.DataAnnotations;
using Server.Models;

namespace Server.Models
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "Name must be between 3 to 100 letters")]
        public string Name { get; set; } = null!;

        

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Image { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int Quantity { get; set; }

        public int CategoryId {get; set; }

        public Category ? Category { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = new User();
        
    }
    
}


