using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UL.Calculator.Core.Model;

namespace UL.Calculator.Services
{
    public interface IUserService
    {
        UserInfo Authenticate(Credentials credentials);
    }
}