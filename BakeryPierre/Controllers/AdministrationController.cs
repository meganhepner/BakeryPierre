using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using BakeryPierre.Models;
using System.Threading.Tasks;
using BakeryPierre.ViewModels;

public class AdministrationController : Controller
{
  private readonly RoleManager<IdentityRole> roleManager;
  public AdministrationController(RoleManager<IdentityRole> roleManager)
  {
    this.roleManager = roleManager;
  }

  [HttpGet]
  public IActionResult CreateRole()
  {
    return View();
  }

}