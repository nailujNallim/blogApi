using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.services
{
    public interface IBlogService
    {
        Task<ICollection<Post>> Posts();
        Task<ICollection<Post>> Posts(string username);
        Task<Post> Posts(Guid id);
        Task<Guid> CreatePost(string title, string content, string username);
        Task<ICollection<Post>> GestPostsPending();
        Task<int> SetRevisionToPost(Guid pId, bool approved, string username);
        Task<ICollection<Post>> GestPostsPublished();
        Task<bool> UpdatePost(Guid id, string title, string content, DateTime submitDate, string username);
    }
}
