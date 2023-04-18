using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Domain.Entities.Autitables
{
    public class Report : AuditableEntity
    {
        public string Message { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Status Status { get; set; }
        public ReportType Type { get; set; }

        public void AddComment(string responseMessage, Guid author)
        {
            Comments.Add(new Comment
            {
                Response = responseMessage,
                Created = DateTime.Now,
                CreatedBy = author
            });
            Status = Status.Otwarte;
        }

    }
}
