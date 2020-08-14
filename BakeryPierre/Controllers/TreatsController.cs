using Microsoft.AspNetCore.Mvc;
using BakeryPierre.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BakeryPierre.Controllers
{
  public class TreatsController : Controller
  {
    private readonly BakeryPierreContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, BakeryPierreContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index(string sortOrder, string searchString)
    {
      ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      var treats = from treat in _db.Treats select treat;
      if (!String.IsNullOrEmpty(searchString))
      {
          treats = treats.Where(treat => treat.TreatName.Contains(searchString));
      }
      switch (sortOrder)
      {
        case "name_desc":
          treats = treats.OrderByDescending(treat => treat.TreatName);
          break;
        // case "Date":
        //   engineers = engineers.OrderBy(engineer => engineer.DateofHire);
        //   break;
        // case "date_desc":
        //   engineers = engineers.OrderByDescending(engineer => engineer.DateofHire);
        //   break;
        default:
          treats = treats.OrderBy(treat => treat.TreatName);
          break;
      }
      return View(treats.ToList());
    }

    [Authorize]
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  
  }
}