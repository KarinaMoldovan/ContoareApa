using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Tarif
    {
        [Key]
        public int TarifId { get; set; }

        [Required]
        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]

        public decimal PretPeMetruCub { get; set; }

        [Required]
        public DateTime DataInceput { get; set; }

        public DateTime? DataSfarsit { get; set; } // Optional, poate fi null pentru tariful curent

        public ICollection<Factura>? Facturi { get; set; }
    }
}
