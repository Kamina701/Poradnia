using System.ComponentModel.DataAnnotations;
using System;

namespace SRP.Models.DTOs
{
    public class AddCommentDto
    {
        public Guid ReportId { get; set; }
        [Required(ErrorMessage = "Uzupełnij treść komentarza.")]
        //[BindProperty(Name = "Message")]
        public string Message { get; set; }
    }
}
