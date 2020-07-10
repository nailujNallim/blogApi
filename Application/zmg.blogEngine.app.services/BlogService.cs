﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.model.Enumerations;

namespace zmg.blogEngine.services
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
            ICollection<Post> posts = await PostRepository.GetPostsByUsername(username);

            if(posts.Equals(null))
                return new List<Post>();

            return posts;
        }

        public async Task<Guid> CreatePost(Post post)
        {
            post.Author = await UserRepository.GetWriterByUsername(post.Author.UserName);
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

        public async Task<int> SetRevisionToPost(Guid pId, int status, string editorUsername)
        {
            Post post = await PostRepository.GetPostById(pId);
            post.Status = (StatusPost)status;
            post.RevisionDate = DateTime.Now;
            post.ApprovedBy = await UserRepository.GetEditorByUsername(editorUsername);

            return (int)post.Status;
        }
    }
}
