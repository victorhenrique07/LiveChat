using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Api.Integration.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(HttpStatusCode status, string message) : base(message)
        {
        }
    }
}
