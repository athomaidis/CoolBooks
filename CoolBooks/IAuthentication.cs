using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBooks
{
    public interface IAuthentication
    {
        string GetUserId(System.Security.Principal.IPrincipal User);
    }
}