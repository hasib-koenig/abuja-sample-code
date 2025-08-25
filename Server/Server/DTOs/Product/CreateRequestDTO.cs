using System.ComponentModel.DataAnnotations;

namespace Server.DTOs.Product
{
    public class CreateRequestDTO
    {

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 100 letters")]
        public string Name { get; set; } = null!;

        public int Category { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]  
        public string Description { get; set; } = null!;

        [Required]
        public string Image { get; set; } = null!;

        public int Quantity { get; set; }

        public string CategoryName { get; set; } = null!;
    }

    
}
