using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRP.Models.DTOs
{
    public class CommentDto
    {
        public string Response { get; set; }
        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public DateTime Created { get; set; }
    }
}
