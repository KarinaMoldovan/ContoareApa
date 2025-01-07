using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppContoare.Models
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
