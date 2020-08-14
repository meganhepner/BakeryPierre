using Microsoft.AspNetCore.Mvc;
using BakeryPierre.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BakeryPierre.Controllers
{
  public class TreatsController : Controller
  {
    private readonly BakeryPierreContext _db;

    public TreatsController(BakeryPierreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      // return View(model);
    }

  
  }
}