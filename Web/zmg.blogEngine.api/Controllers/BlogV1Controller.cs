using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using zmg.blogEngine.api.Models;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.services;

namespace zmg.blogEngine.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class BlogV1Controller : ControllerBase
    {
        private readonly IMapper _mapper;
        private IBlogService BlogService { get; set; }

        public BlogV1Controller(IMapper mapper, IBlogService service)
        {
            _mapper = mapper;
            BlogService = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBlog(NewPostDto postDto)
        {
            var post= _mapper.Map<Post>(postDto);
            Guid postId = await BlogService.CreatePost(post);
            return Ok(postId);
        }

        [HttpGet("posts/{username}")]
        public async Task<IActionResult> GetAllPosts(string username)
        {
            var postList = await BlogService.Posts(username);
            return Ok(postList);
        }
    }
}