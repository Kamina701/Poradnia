using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Autitables
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public Guid? CreatedBy { get; set; }

    }
}
