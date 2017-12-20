using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Entities
{
    public interface IIdentityVerification
    {
        bool Verify();
        string ErrorMessage { get; }
    }
}
