using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppContoare.Models
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

        public DateTime? DataSfarsit { get; set; }

        public ICollection<Factura>? Facturi { get; set; }
    }
}
