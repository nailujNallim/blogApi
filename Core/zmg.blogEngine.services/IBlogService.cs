using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.services
{
    public interface IBlogService
    {
        Task<ICollection<Post>> Posts(string username);

        Task<Guid> CreatePost(Post post);
    }
}
