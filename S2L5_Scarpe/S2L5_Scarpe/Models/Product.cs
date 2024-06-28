using System.ComponentModel.DataAnnotations;

namespace S2L5_Scarpe.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Prezzo")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Descrizione")]
        [Required]
        public string Description { get; set; }

        public IFormFile CoverImage { get; set; }
        public IFormFile AddImage1 { get; set; }
        public IFormFile AddImage2 { get; set; }
    }
}
