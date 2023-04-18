using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS
{
    public abstract class AuthorizedRequest : IBaseRequest
    {
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
    }
}
