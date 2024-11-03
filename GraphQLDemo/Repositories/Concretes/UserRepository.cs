using GraphQLDemo.Models;
using GraphQLDemo.Repositories.Interfaces;

namespace GraphQLDemo.Repositories.Concretes
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public Task<List<User>> AddUser(string name, string email, string role, FunctionalGroup functionalGroup)
        {
            _users.Add(new User
            {
                Email = email,
                Name = name,
                Role = role,
                Id =  new Random().Next(3, 1000),
                FunctionalGroup = new FunctionalGroup
                {
                    FullName = functionalGroup.FullName,
                    Id = functionalGroup.Id ??  new Random().Next(3, 1000),
                    ShortName = functionalGroup.ShortName
                }
            });
            return Task.FromResult(_users);
        }

        public Task<List<User>> GetUsers() => Task.FromResult(_users);

        public Task UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}