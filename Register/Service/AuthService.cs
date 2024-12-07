//using DevOne.Security.Cryptography.BCrypt;
using Microsoft.IdentityModel.Tokens;
using Register.DTO.Request;
using Register.Entity;
using Register.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Register.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        private readonly IConfiguration _configuration;


        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        // register new user
        public async Task<string> Register(UserRequest request)
        {
            var add = new User
            {
                Name = request.Name,
                Email = request.Email,
                Role = request.Role,

                // change password into HashPassword
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            var user = await _authRepository.Adduser(add);
            var tokan = CreateToken(user);
            return tokan;
        }

        // log in 
        public async Task<string> LogIn(string email, string password)
        {
            // get user
            var user = await _authRepository.GetUserByEmail(email);

            // check user
            if (user == null) throw new Exception("User not found!");

            // check password
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) throw new Exception("Wrong Password!");
       
            return CreateToken(user);
        }


        //create token for new user

        private string CreateToken(User user) 
        {
            var claimList = new List<Claim>();

            claimList.Add(new Claim("Id", user.Id.ToString()));
            claimList.Add(new Claim("Name", user.Name));
            claimList.Add(new Claim("Email", user.Email));
            claimList.Add(new Claim("Role", user.Role.ToString()));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
               claims: claimList,
               signingCredentials: credintials
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
