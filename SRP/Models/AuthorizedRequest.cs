using MediatR;
using System;

namespace SRP.Models
{
    public class AuthorizedRequest : IBaseRequest
    {
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
    }
}
