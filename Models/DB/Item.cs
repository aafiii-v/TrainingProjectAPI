using System.ComponentModel.DataAnnotations;

namespace TrainingProjectAPI.Models.DB
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NamaItem { get; set; }

        [Required]
        public int QTY { get; set; }

        [Required]
        public DateTime TglExpired { get; set; }

        [Required]
        [MaxLength (100)]
        public string Supplier {  get; set; }

        [MaxLength (100)]
        public string? AlamatSupplier { get; set; }
    }
}
