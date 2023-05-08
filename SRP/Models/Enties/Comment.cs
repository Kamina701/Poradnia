using SRP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace SRP.Models.Enties
{
    public class Comment : AuditableEntity
    {
        public Report Report { get; set; }
        public Guid ReportId { get; set; }
        public string Response { get; set; }

    }
    public enum ReportType
    {
        [Display(Name = "Propozycja")]
        Sugestion,
        [Display(Name = "Błąd w programie")]
        Bug

    }
    public enum Status
    {
        Nowe,
        Otwarte,
        Zakończone
    }
}
