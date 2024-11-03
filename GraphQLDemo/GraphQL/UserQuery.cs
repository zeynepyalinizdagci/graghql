using GraphQLDemo.Models;
using GraphQLDemo.Repositories.Concretes;
using GraphQLDemo.Repositories.Interfaces;

namespace GraphQLDemo.GraphQL
{
    public class UserQuery
    {
        private readonly IUserRepository _userRepository;
        public UserQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [UseSorting]
        public async Task<IEnumerable<User>> GetUsers() => await _userRepository.GetUsers();
    }
}
