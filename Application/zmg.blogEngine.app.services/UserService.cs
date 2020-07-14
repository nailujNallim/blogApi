using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.services;

namespace zmg.blogEngine.app.services
{
    public class UserService : IUserService
    {
        IUserRepository UserRepository { get; set; }

        public UserService(IUserRepository userRepo)
        {
            UserRepository = userRepo;
        }

        //Task<User> GetCurrentUser()
        //{
        //    return new User();
        //    //var username = HttpContext.Session.GetString("UserName");
        //    //var userType = HttpContext.Session.GetString("UserType");
        //    //Enum.TryParse(userType, out UserType userTypeEnum);
        //    //CurrentUser = await UserRepository.GetUser(username, userTypeEnum);
        //}

    }
}
