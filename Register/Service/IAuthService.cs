using Register.DTO.Request;

namespace Register.Service
{
    public interface IAuthService
    {
        Task<string> Register(UserRequest request);
        Task<string> LogIn(string email, string password);
    }
}
