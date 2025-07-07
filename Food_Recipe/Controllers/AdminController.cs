using Microsoft.AspNetCore.Mvc;
using Food_Recipe.Services.Interfaces;
using Food_Recipe.Domain.Models;

namespace Food_Recipe.Controllers;

public class AdminController : Controller
{
    private readonly IAdminService _admin;
    public AdminController(IAdminService admin) => _admin = admin;

    public IActionResult Users()
    {
        var users = _admin.GetAllUsers();
        return View(users);
    }

    public IActionResult Dashboard(string status = "approved")
    {
        status = (status ?? "approved").ToLower();
        if (status == "pending")
            return View("Dashboard", _admin.GetPendingRecipes());

        return View("Dashboard", _admin.GetApprovedRecipes());
    }

    [HttpGet]
    public IActionResult EditRecipe(int id)
    {
        var rec = _admin.GetPendingRecipe(id);
        if (rec == null || rec.IsApproved)
            return RedirectToAction("Dashboard", new { status = "pending" });

        return View(rec);
    }

    [HttpPost]
    public IActionResult EditRecipe(PendingUserRecipe form)
    {
        bool updated = _admin.UpdatePendingRecipe(form);
        TempData["Message"] = updated ? "Changes saved." : "Update failed.";
        return RedirectToAction("Dashboard", new { status = "pending" });
    }

    [HttpPost]
    public IActionResult Approve(int id)
    {
        _admin.ApproveRecipe(id);
        return RedirectToAction("Dashboard", new { status = "approved" });
    }

    [HttpPost]
    public IActionResult Reject(int id)
    {
        _admin.RejectRecipe(id);
        return RedirectToAction("Dashboard", new { status = "pending" });
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "User");
    }
}
