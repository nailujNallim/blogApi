using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.model.Enumerations;
using zmg.blogEngine.services;

namespace zmg.blogEngine.app.services
{
    public class BlogService : IBlogService
    {
        private IPostRepository PostRepository { get; set; }
        private IUserRepository UserRepository { get; set; }

        public BlogService(IPostRepository postRepo, IUserRepository userRepo)
        {
            PostRepository = postRepo;
            UserRepository = userRepo;
        }

        public async Task<ICollection<Post>> Posts(string username)
        {
            var user = await UserRepository.GetUser(username);

            if(UserType.Writer.Equals(user.UserType))
                return  await PostRepository.GetPostsByUsername(username);
            else
                return await PostRepository.GetPostsPending();
        }

        public async Task<Guid> CreatePost(string title, string content, string username)
        {
            var post = new Post();
            post.Title = title;
            post.Content = content;
            post.Author = await UserRepository.GetUser(username, UserType.Writer);
            post.SubmitDate = DateTime.Now;
            post.Status = StatusPost.Pending;

            Guid postId = await PostRepository.CreatePost(post);

            return postId;
        }

        public async Task<ICollection<Post>> GestPostsPending()
        {
            ICollection<Post> posts = await PostRepository.GetPostsPending();

            if (posts.Equals(null))
                return new List<Post>();

            return posts;
        }

        public async Task<ICollection<Post>> GestPostsPublished()
        {
            ICollection<Post> posts = await PostRepository.GetPostsPublished();

            if (posts.Equals(null))
                return new List<Post>();

            return posts;
        }

        public async Task<int> SetRevisionToPost(Guid pId, bool approved, string editorUsername)
        {
            Post post = await PostRepository.GetPostById(pId);
            post.Status = approved ?  StatusPost.Approved : StatusPost.Rejected;
            post.RevisionDate = DateTime.Now;
            post.ApprovedBy = await UserRepository.GetUser(editorUsername, UserType.Writer);

            return (int)post.Status;
        }

        public async Task<ICollection<Post>> Posts()
        {
            ICollection<Post> posts = await PostRepository.GetPostsPublished();

            if (posts.Equals(null))
                return new List<Post>();

            return posts;
        }

        public async Task<Post> Posts(Guid id)
        {
            return await PostRepository.GetPostById(id);
        }

        public async Task<bool> UpdatePost(Guid id, string title, string content, DateTime submitDate, string username)
        {
            var post = new Post();
            post.Id = id;
            post.Title = title;
            post.Content = content;
            post.Author = await UserRepository.GetUser(username);
            post.SubmitDate = submitDate;
            post.Status = StatusPost.Pending;

            return await PostRepository.SaveOrUpdate(post);
        }
    }
}
