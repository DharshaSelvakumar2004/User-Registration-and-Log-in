using Microsoft.EntityFrameworkCore;
using Register.DB;
using Register.Entity;

namespace Register.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDb appDb;

        public AuthRepository(AppDb appDb)
        {
            this.appDb = appDb;
        }

        //register a new user
        public async Task<User> Adduser(User user)
        {
            var data = await appDb.Users.AddAsync(user);    
            await appDb.SaveChangesAsync();
            return user;
        }

        // get user by email (log in)

        public async Task<User> GetUserByEmail(string email)
        {
            var data = await appDb.Users.SingleOrDefaultAsync(user => user.Email == email);
            return data;
        }
    }
}
