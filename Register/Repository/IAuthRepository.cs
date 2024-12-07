using Register.Entity;

namespace Register.Repository
{
    public interface IAuthRepository
    {
        Task<User> Adduser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
