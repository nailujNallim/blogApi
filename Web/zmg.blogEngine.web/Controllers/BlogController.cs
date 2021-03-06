﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.model.Enumerations;
using zmg.blogEngine.services;
using zmg.blogEngine.web.Model;

namespace zmg.blogEngine.web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private IBlogService BlogService { get; set; }
        private IUserRepository UserRepository { get; set; }
        private User CurrentUser { get; set; }

        public BlogController(IBlogService blogService, IUserRepository userRepo, IHttpContextAccessor httpContextAccessor)
        {
            BlogService = blogService;
            UserRepository = userRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Blog
        public async Task<ActionResult> IndexAsync()
        {
            var username = _session.GetString("UserName");
            ViewData["User"] = username;

            if (username == null)
            {
                ViewBag.IsWriter = IsWriter(UserType.Unregistered);
                ViewBag.IsEditor = IsEditor(UserType.Unregistered);
                ViewBag.IsPublic = IsPublic(UserType.Unregistered);
                return View(await BlogService.Posts());
            }
            else
            {
                CurrentUser = await UserRepository.GetUser(username);
                ViewBag.IsWriter = IsWriter(CurrentUser.UserType);
                ViewBag.IsEditor = IsEditor(CurrentUser.UserType);
                ViewBag.IsPublic = IsPublic(CurrentUser.UserType);
                return View(await BlogService.Posts(CurrentUser.UserName));
            }
        }

        public async Task<ActionResult> Visitor()
        {
            _session.Remove("UserName");

            return RedirectToAction("Index");
        }

        private bool IsEditor(UserType userType)
        {
            return (UserType.Editor.Equals(userType) ? true : false);
        }
        private bool IsWriter(UserType userType)
        {
            return (UserType.Writer.Equals(userType) ? true : false);
        }

        private bool IsPublic(UserType userType)
        {
            return (UserType.Unregistered.Equals(userType) ? true : false);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            var username = _session.GetString("UserName");
            return View(new NewPostModel() { Username = username });
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(NewPostModel postModel)
        {
            var username = _session.GetString("UserName");

            try
            {
                if (ModelState.IsValid) 
                {
                    var postId = await BlogService.CreatePost(postModel.Title, postModel.Content, username);
                }

                return RedirectToAction("Index", "Blog");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("CreateAsync Error", ex.Message);
                return View(postModel);
            }
        }

        // GET: Blog/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var username = _session.GetString("UserName");

            var post = await BlogService.Posts(id);
            var editPost = new EditPostModel();
            
            editPost.Id = post.Id;
            editPost.Title = post.Title;
            editPost.Content = post.Content;
            editPost.SubmitDate = post.SubmitDate;
            editPost.Username = post.Author.UserName;

            return View(editPost);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, EditPostModel post)
        {
            try
            {
                var username = _session.GetString("UserName");

                var postId = await BlogService.UpdatePost(post.Id, post.Title, post.Content, post.SubmitDate, post.Username);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Edit error", ex.Message);
                return View();
            }
        }

        // GET: Blog/Delete/5
        public async Task<ActionResult> ApproveAsync(Guid id, bool approved)
        {
            var username = _session.GetString("UserName");
            _ = await BlogService.SetRevisionToPost(id, approved, username);

            return RedirectToAction("Index");
            //return View();
        }

        // POST: Blog/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Approve(int id, bool approve)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(IndexAsync));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}