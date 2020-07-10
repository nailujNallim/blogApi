using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.model
{
    public interface IPostRepository
    {
        Task<Guid> CreatePost(Post post);
        Task<ICollection<Post>> GetPostsByUsername(string username);
        Task<ICollection<Post>> GetPostsPending();
        Task<Post> GetPostById(Guid pId);
    }
}
