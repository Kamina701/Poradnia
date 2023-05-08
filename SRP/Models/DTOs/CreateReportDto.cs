using SRP.Models.Enties;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SRP.Models.DTOs
{
    public class CreateReportDto
    {
        [Required(ErrorMessage = "Treść zgłoszenia nie może być pusta.")]
        [Display(Name = "Treść")]

        public string Message { get; set; }

        [Display(Name = "Wybierz typ zgłoszenia")]
        [Required(ErrorMessage = "Określ typ zgłoszenia.")]
        public ReportType? Type { get; set; }
    }
}
