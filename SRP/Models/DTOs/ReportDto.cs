using System;
using System.Collections.Generic;
using SRP.Models.Enties;

namespace SRP.Models.DTOs
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public Status Status { get; set; }
        public ReportType Type { get; set; }
        public Guid Author { get; set; }
        public DateTime Created { get; set; }
    }
}
