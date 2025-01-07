using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Consumator
    {
        [Key]
        public int ConsumatorId { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Numele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau AnaMaria")]
        [StringLength(30, MinimumLength = 3)]
        public string? Nume { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau AnaMaria")]
        [StringLength(30, MinimumLength = 3)]
        public string? Prenume { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Contor>? Contoare { get; set; }
    }
}
