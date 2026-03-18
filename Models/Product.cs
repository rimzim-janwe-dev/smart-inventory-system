using System.ComponentModel.DataAnnotations;

namespace SmartInventory.API.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Threshold { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}