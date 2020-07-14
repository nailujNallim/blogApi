using System;
using System.Threading.Tasks;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.model.Enumerations;

namespace zmg.blogEngine.model
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username, UserType userType);
        Task<User> GetUser(string username);
    }
}
