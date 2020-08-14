using Microsoft.AspNetCore.Mvc;

namespace BakeryPierre.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

  }
}