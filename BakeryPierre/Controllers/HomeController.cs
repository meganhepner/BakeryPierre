using Microsoft.AspNetCore.Mvc;
using BakeryPierre.Models;

namespace BakeryPierre.Controllers
{
  public class HomeController : Controller
  {
    private readonly BakeryPierreContext _db;
    public HomeController(BakeryPierreContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Treats = _db.Treats;
      ViewBag.Flavors = _db.Flavors;
      return View();
    }

  }
}