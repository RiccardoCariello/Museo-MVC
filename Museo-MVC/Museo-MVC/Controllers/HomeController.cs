using Microsoft.AspNetCore.Mvc;
using Museo_MVC.DataBase;
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
                Souvenir? souvenirDetails = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

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

        // ACTIONS PER LA CREAZIONE DI UN ARTICOLO
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Souvenir newSouvenir)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newSouvenir);
            }

            using (MuseoContext db = new MuseoContext())
            {
                db.Souvenirs.Add(newSouvenir);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }

        // ACTIONS PER LA MODIFICA DI UN ARTICOLO
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                Souvenir? souvenirToModify = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                if (souvenirToModify != null)
                {
                    return View("Update", souvenirToModify);
                }
                else
                {

                    return NotFound("Souvenir da modificare inesistente!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Souvenir modifiedSouvenir)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", modifiedSouvenir);
            }

            using (MuseoContext db = new MuseoContext())
            {
                Souvenir? articleToModify = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                if (souvenirToModify != null)
                {

                    souvenirToModify.Img = souvenirToModify.Img;
                    souvenirToModify.Name = souvenirToModify.Name;
                    souvenirToModify.Description = souvenirToModify.Description;
                    souvenirToModify.Price = souvenirToModify.Price;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound("Il souvenir da modificare non esiste!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                Souvenir? articleToDelete = db.souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                if (souvenirToDelete != null)
                {
                    db.Remove(souvenirToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound("Non ho torvato il souvenir da eliminare");

                }
            }
        }



    }











}
