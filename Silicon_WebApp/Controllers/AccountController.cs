﻿using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_WebApp.ViewModels;
using System.Security.Claims;

namespace Silicon_WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, ApplicationContext context) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ApplicationContext _context = context;

    public IActionResult Details()
    {

        var viewModel = new AccountDetailsViewModel();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailsViewModel model)
    {
        if (TryValidateModel(model.Basic!))
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FirstName = user.FirstName;
                user.LastName = user.LastName;
                user.Email = user.Email;
                user.PhoneNumber = user.PhoneNumber;
                user.UserName = user.Email;
                user.Bio = user.Bio;

              var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    TempData["StatusMessage"] = " Succesfully updated basic information.";
                }
                else
                {
                    TempData["StatusMessage"] = "Unable to save basic information.";
                }
            }
        }
        else
        { 
            TempData["StatusMessage"] = "Unable to save basic information.";
        }
       
        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AccountDetailsViewModel model)
    {
        if (TryValidateModel(model.Address!))
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var user = await _context.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

          if (user != null)
           {
             if(user.Address!= null)
                {
                    user.Address.AddressLine_1 = model.Address!.AddressLine_1;
                    user.Address.AddressLine_2 = model.Address!.AddressLine_2;
                    user.Address.PostalCode = model.Address!.PostalCode;
                    user.Address.City = model.Address!.City;
                }
                else
                {
                    user.Address = new AddressEntity
                    {
                        AddressLine_1 = model.Address!.AddressLine_1,
                        AddressLine_2 = model.Address!.AddressLine_2,
                        PostalCode = model.Address!.PostalCode,
                        City = model.Address!.City

                    };
                }
                
             _context.Update(user);
                await _context.SaveChangesAsync();

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["StatusMessage"] = " Succesfully updated address information.";
                }
                else
                {
                    TempData["StatusMessage"] = "Unable to save address information.";
                }
           }
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save address information.";
        }

        return RedirectToAction("Details", "Account");
    }
}
