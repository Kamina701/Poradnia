using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class NotificationDTO
    {
        public DateTime? DateTime { get; set; }
        public string Description { get; set; }
        public string DestinationURL { get; set; }
    }
}
