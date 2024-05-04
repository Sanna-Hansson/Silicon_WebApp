using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_WebApp.ViewModels;

namespace Silicon_WebApp.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ApplicationContext context) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly ApplicationContext _context = context;

    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            if(!await _context.Users.AnyAsync(x=>x.Email == model.Email))

            {
                var userEntity = new UserEntity
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName

                };


                if ((await _userManager.CreateAsync(userEntity, model.Password)).Succeeded)
                {

                    if ((await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false)).Succeeded)
                        return LocalRedirect("/");
                    else
                     {
                        return LocalRedirect("/signin");

                     }
                }
                else
                {
                    ViewData["StatusMessage"] = "Somthing went wrong. You can try again later or contact cutomer service.";
                }
            }
            else
            {
                ViewData["StatusMessage"] = "A User with the same Email already exists";
            }


        }

        return View();


    }
    [Route("/signin")]
    public IActionResult SignIn(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl ?? "/";

        return View();
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            if ((await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsPressistent, false)).Succeeded)
                return LocalRedirect(returnUrl);
        }

        ViewData["ReturnUrl"] = returnUrl;
        ViewData["StatusMessage"] = "Incorrect Password Or Email";
        return View(model);
    }

    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Default");
    }


}
