using OnlineStore.Application.Common.Interfaces.Persistence;
using OnlineStore.Domain.Models;

namespace OnlineStore.Infrastructure.Persistence
{
    public class UserRepository: IUserRepository
    {
        private static readonly List<User> _users = new();
        public void Add(User user)
        {
            _users.Add(user);
        }
        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault<User>(u => u.Email == email);
        }
    }
}