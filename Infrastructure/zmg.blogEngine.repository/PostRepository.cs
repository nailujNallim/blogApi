using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using System;

namespace zmg.blogEngine.repository
{
    public class PostRepository : IPostRepository
    {
        IRepository Repository { get; set; }

        public PostRepository(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<Guid> CreatePost(Post post)
        {
            try
            {
                Repository.BeginTransaction();
                Guid id = await Repository.Save(post);
                Repository.CommitTransaction();
                return id;
            }
            catch (Exception e)
            {
                //Repository.RollbackTransaction();
                throw new Exception("Create Post Error: " + e.Message);
            }
        }

        public async Task<ICollection<Post>> GetPostsByUsername(string username)
        {
            Repository.BeginTransaction();
            var postsList = await Task.Run(() => (from p in Repository.ToList<Post>() select p));            
            Repository.CommitTransaction();

            return postsList.ToList();
        }
    }
}
