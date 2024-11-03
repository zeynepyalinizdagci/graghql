using GraphQLDemo.Models;
using GraphQLDemo.Repositories.Interfaces;

public class UserMutation{
    public readonly IUserRepository _userRepository;

    public UserMutation(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public Task<List<User>> AddUser(string name, string email, string role, FunctionalGroup functionalGroup)
        {
            return _userRepository.AddUser(name, email, role, functionalGroup);
        }
}

public class FunctionalGroup
{
    public int? Id { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
}