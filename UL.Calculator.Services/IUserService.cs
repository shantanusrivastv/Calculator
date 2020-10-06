using System.Threading.Tasks;
using UL.Calculator.Core.Model;

namespace UL.Calculator.Services
{
    public interface IUserService
    {
        Task<UserInfo> Authenticate(Credentials credentials);
    }
}