using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Enumerations;
using zmg.blogEngine.web.Model;

namespace zmg.blogEngine.web.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository UserRepository { get; set; }

        public HomeController(IUserRepository userRepo)
        {
            UserRepository = userRepo;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(UserLoginModel objUser)
        {
            if (ModelState.IsValid)
            {
                var ut = (objUser.Username.ToLower()) switch
                {
                    "editor" => UserType.Editor,
                    "writer" => UserType.Writer,
                    _ => UserType.Unregistered,
                };
                if (!UserType.Unregistered.Equals(ut))
                {
                    //if the user doesnt exist, it creates a new one
                    var user = await UserRepository.GetUser(objUser.Username, ut);
                    HttpContext.Session.SetString("UserName", user.UserName);
                    return RedirectToAction("Index", "Blog", objUser.Username);
                }
                ModelState.AddModelError("Username", "The user doesn´t exist");
            }
            return View(objUser);
        }
    }
}