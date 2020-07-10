using System.Linq;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.repository
{
    public class UserRepository : IUserRepository
    {
        IRepository Repository { get; set; }

        public UserRepository(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<Writer> GetWriterByUsername(string username)
        {
            Repository.BeginTransaction();
            var w =  await Task.Run(() => (from w in Repository.ToList<Writer>()
                                         where w.UserName.Equals(username)
                                        select w).FirstOrDefault());
            if (w.Equals(null))
            {
                w = new Writer()
                {
                    FullName = "new writer",
                    UserName = username
                };
                w.Id = await Repository.Save(w);
            }
            Repository.CommitTransaction();
            return w;
        }


    }
}
