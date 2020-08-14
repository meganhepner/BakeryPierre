using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using BakeryPierre.Models;
using System.Threading.Tasks;
using BakeryPierre.ViewModels;

namespace BakeryPierre.Controllers
{
  public class AdministrationController : Controller
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BakeryPierreContext _db;

    public AdministrationController(RoleManager<IdentityRole> roleManager, BakeryPierreContext db, UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _db = db;
    }

    [HttpGet]
    public IActionResult CreateRole()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
      if(ModelState.IsValid)
      {
        IdentityRole identityRole = new IdentityRole
        {
          Name = model.RoleName
        };

        IdentityResult result = await _roleManager.CreateAsync(identityRole);

        if(result.Succeeded)
        {
          return RedirectToAction("ListRoles", "Administration");
        }
        foreach(IdentityError error in result.Errors)
        {
          ModelState.AddModelError("", error.Description);
        }
      }
    return View(model);
    }

    [HttpGet]
    public IActionResult ListRoles()
    {
      var roles = _roleManager.Roles;
      return View(roles);
    }

    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {
      var role = await _roleManager.FindByIdAsync(id);
      if(role == null)
      {
        RedirectToAction("ListRoles");
      }

      var model = new EditRoleViewModel
      {
        Id = role.Id,
        RoleName = role.Name
      };
      foreach(var user in _userManager.Users)
      {
        if(await _userManager.IsInRoleAsync(user, role.Name))
        {
          model.Users.Add(user.UserName);
        }
      }
      return View(model);
    }
  }
}