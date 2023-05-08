using System;

namespace SRP.Models
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
