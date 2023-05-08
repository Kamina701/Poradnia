using SRP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SRP.Models.Enties

{
    public class Report : AuditableEntity
    {
        public string Message { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Status Status { get; set; }
        public ReportType Type { get; set; }
    }
}
