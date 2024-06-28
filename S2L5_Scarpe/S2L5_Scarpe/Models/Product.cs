using System.ComponentModel.DataAnnotations;

namespace S2L5_Scarpe.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile CoverImage { get; set; }
        public IFormFile AddImage1 { get; set; }
        public IFormFile AddImage2 { get; set; }
    }
}
