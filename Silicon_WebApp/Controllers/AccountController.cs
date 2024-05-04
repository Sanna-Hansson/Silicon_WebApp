using Microsoft.AspNetCore.Mvc;
using Silicon_WebApp.ViewModels;

namespace Silicon_WebApp.Controllers;

public class AccountController : Controller
{
    public IActionResult Details()
    {

        var viewModel = new AccountDetailsViewModel();

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult UpdateBasicInfo()
    {
        TempData["StatusMessage"] = "Unable to save the information.";
        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public IActionResult UpdateAddressInfo()
    {
        TempData["StatusMessage"] = "Unable to save the information.";
        return RedirectToAction("Details", "Account");
    }
}
