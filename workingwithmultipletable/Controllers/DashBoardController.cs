using Microsoft.AspNetCore.Mvc;

namespace workingwithmultipletable.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
