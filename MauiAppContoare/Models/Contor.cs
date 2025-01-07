using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppContoare.Models
{
    public class Contor
    {
        [Key]
        public int ContorId { get; set; }

        [Required]
        [StringLength(50)]
        public string NumarSerie { get; set; }

        [Required]
        public decimal ValoareActuala { get; set; }

        [Required]
        public int ConsumatorId { get; set; }

        [ForeignKey("ConsumatorId")]
        public Consumator? Consumator { get; set; }

        public ICollection<Factura>? Facturi { get; set; }
    }
}
