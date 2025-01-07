using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppContoare.Models
{
    public class Plata
    {
        [Key]
        public int PlataId { get; set; }

        [Required]
        public DateTime DataPlatii { get; set; }

        [Required]
        public decimal SumaPlatita { get; set; }

        [Required]
        public ModalitateDePlata ModalitateDePlata { get; set; }


        [Required]
        public int FacturaId { get; set; }
        [ForeignKey("FacturaId")]
        public Factura? Factura { get; set; }

    }
    public enum ModalitateDePlata
    {
        [Display(Name = "Numerar")]
        Numerar,
        [Display(Name = "Card Bancar")]
        CardBancar,
        [Display(Name = "Transfer Bancar")]
        TransferBancar
    }
}
