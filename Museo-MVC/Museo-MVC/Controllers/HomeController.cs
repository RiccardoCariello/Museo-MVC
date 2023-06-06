using Microsoft.AspNetCore.Mvc;
using Museo_MVC.Models;
using System.Diagnostics;

namespace Museo_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // PAGINA CON SINGOLO SOUVENIR
        public IActionResult Details(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                Souvenir? souvenirDetails = db.?.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                if (souvenirDetails != null)
                {
                    return View("Details", souvenirDetails);
                }
                else
                {
                    return NotFound($"L'articolo con id {id} non è stato trovato!");
                }
            }

        }




    }











}
}