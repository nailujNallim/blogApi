using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zmg.blogEngine.services;
using zmg.blogEngine.web.Model;

namespace zmg.blogEngine.web.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService { get; set; }

        public BlogController(IBlogService service)
        {
            BlogService = service;
        }

        // GET: Blog
        public async Task<ActionResult> IndexAsync()
        {
            var postList = await BlogService.Posts();
            return View(postList);
        }

        // GET: Blog/Details/5
        public async Task<ActionResult> DetailsAsync(Guid id)
        {
            var post = await BlogService.Posts(id);
            return View("PostDetail", post);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(NewPostModel postModel)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var postId = await BlogService.CreatePost(postModel.Title, postModel.Content, "juliamg");
                }

                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            var post = await BlogService.Posts(id);
            return View(post);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}