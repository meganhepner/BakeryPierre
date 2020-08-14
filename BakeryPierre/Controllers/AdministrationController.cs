using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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

}