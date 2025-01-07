using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Required]
        public DateTime DataEmitere { get; set; }

        [Required]
        public decimal Suma { get; set; }

        [Required]
        public int ContorId { get; set; }

        [ForeignKey("ContorId")]
        public Contor? Contor { get; set; }

        public int? PlataId { get; set; }

        [ForeignKey("PlataId")]
        public Plata? Plata { get; set; }

        [ForeignKey("TarifId")]
        public Tarif? Tarif { get; set; }
    }
}
