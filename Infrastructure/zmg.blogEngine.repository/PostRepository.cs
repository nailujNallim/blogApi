using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using System;
using zmg.blogEngine.model.Enumerations;

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
            var postsList = await Task.Run(() 
                => (from p in Repository.ToList<Post>() where p.Author.UserName.Equals(username) select p));            
            Repository.CommitTransaction();

            return postsList.ToList();
        }

        public async Task<ICollection<Post>> GetPostsPending()
        {
            Repository.BeginTransaction();
            var postsList = await Task.Run(() 
                => (from p in Repository.ToList<Post>() select p));
            Repository.CommitTransaction();

            var postsPending = new List<Post>();
            foreach(var p in postsList)
            {
                if(p.Status.Equals(StatusPost.Pending))
                {
                    postsPending.Add(p);
                }
            }

            return postsPending.ToList();
        }

        public async Task<ICollection<Post>> GetPostsPublished()
        {
            Repository.BeginTransaction();
            var postsList = await Task.Run(()
                => (from p in Repository.ToList<Post>() select p));
            Repository.CommitTransaction();

            var posts = new List<Post>();
            foreach (var p in postsList)
            {
                if (p.Status.Equals(StatusPost.Approved))
                {
                    posts.Add(p);
                }
            }

            return posts.ToList();
        }

        public async Task<Post> GetPostById(Guid pId)
        {
            Repository.BeginTransaction();

            var post= await Repository.GetById(typeof(Post), pId) as Post;
            
            Repository.CommitTransaction();
            return post;
        }

        public async Task<ICollection<Post>> GetPosts()
        {
            Repository.BeginTransaction();
            var postsList = await Task.Run(()
                => (from p in Repository.ToList<Post>() select p));
            Repository.CommitTransaction();

            return postsList.ToList();
        }
    }
}
