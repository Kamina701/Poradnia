using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Autitables
{
    public class Access : AuditableEntity
    {
        public Access() { }


        public Access(string internalUserNumber)
        {
            UserInternalId = internalUserNumber ?? throw new ArgumentNullException(nameof(internalUserNumber));

            Created = DateTime.Now;
        }

        public Access(Guid userId)
        {
            UserId = userId;

        }

        public Access(Guid userId,DateTime? @from, DateTime? to)
        {
            UserId = userId;
            From = from;
            To = to;
        }

        public Access(string internalUserNumber, DateTime? @from, DateTime? to)
        {
            UserInternalId = internalUserNumber ?? throw new ArgumentNullException(nameof(internalUserNumber));
            Created = DateTime.Now;
            From = from;
            To = to;
        }

        public bool Validate()
        {
            return !(To < DateTime.Now) && !(From > DateTime.Now);
        }

        public bool AlertEnabled()
        {
            return Alert;
        }
        //Properties
        public DateTime? To { get; set; }
        public DateTime? From { get; set; }
        public string UserInternalId { get; set; }
        public string Alias { get; set; }
        public bool Alert { get; set; }
        //Relationships
        public Guid? UserId { get; set; }





    }
}
