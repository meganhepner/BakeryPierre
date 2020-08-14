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
    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.Flavors)
          .ThenInclude(join => join.Flavor)
          .Include(treat => treat.User)
          .FirstOrDefault(treat => treat.TreatId == id);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ViewBag.IsCurrentUser = userId != null ? userId == thisTreat.User.Id : false;
      return View(thisTreat);
    }

       [Authorize]
    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTreat = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).FirstOrDefault(treats => treats.TreatId == id);
      if (thisTreat == null)
      {
        return RedirectToAction("Details", new {id = id});
      }
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  
  }
}