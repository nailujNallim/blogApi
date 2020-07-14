using System.Linq;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.model.Enumerations;

namespace zmg.blogEngine.repository
{
    public class UserRepository : IUserRepository
    {
        IRepository Repository { get; set; }

        public UserRepository(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<User> GetUser(string username, UserType userType)
        {
            Repository.BeginTransaction();
            var w =  await Task.Run(() => (from w in Repository.ToList<User>()
                                         where w.UserName.Equals(username)
                                           select w).FirstOrDefault());
            if (w == null)
            {
                w = new User()
                {
                    FullName = string.Format("{0} {1}", username, userType),
                    UserName = username,
                    UserType = userType
                };
                w.Id = await Repository.Save(w);
            }
            Repository.CommitTransaction();
            return w;
        }

        public async Task<User> GetUser(string username)
        {
            Repository.BeginTransaction();
            var w = await Task.Run(() => (from w in Repository.ToList<User>()
                                          where w.UserName.Equals(username)
                                          select w).FirstOrDefault());

            Repository.CommitTransaction();
            return w;
        }
    }
}
