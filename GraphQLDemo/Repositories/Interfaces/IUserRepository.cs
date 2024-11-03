using GraphQLDemo.Models;

namespace GraphQLDemo.Repositories.Interfaces {
    public interface IUserRepository{
        public Task<List<User>> GetUsers();
        public Task UpdateUser();
        public Task<List<User>>  AddUser(string name, string email,string role, FunctionalGroup functionalGroup);
    }

}