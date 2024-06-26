using OnlineStore.Application.Common.Interfaces.Authentication;
using OnlineStore.Application.Common.Interfaces.Persistence;
using OnlineStore.Domain.Models;

namespace OnlineStore.Application.Services.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login (string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with this email does not exist");
            }

            if (password != user.Password)
            {
                throw new Exception("Invalid password");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            AuthenticationResult result = new AuthenticationResult(
                user,
                token);

            return result;
        }

        public AuthenticationResult Register (string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exists");
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            AuthenticationResult result = new AuthenticationResult(
                user, 
                token);

            return result;
        }
    }
}