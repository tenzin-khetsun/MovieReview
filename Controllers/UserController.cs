using Microsoft.AspNetCore.Mvc;
using MovieReview.Models;
using MovieReview.Services;

namespace MovieReview.Controllers{
    public class UserController : Controller{
        private readonly IUser _users;
        public UserController(IUser users){
            _users = users;
        }
        public IActionResult Login(){
            return View();
        }
        [HttpPost]
        public IActionResult Login(User request){
            if(_users.LoginMethod(request)){
                return RedirectToAction("Index", "Home");
            }
            else{
                return RedirectToAction("Login");
            }
            
        }
        public IActionResult Logout(){
            _users.LogoutMethod();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User request){
            if(ModelState.IsValid){
                _users.RegisterMethod(request);
                return RedirectToAction("Login");
            }
            else{
                return RedirectToAction("Register");
            }
            
        }
    }
}