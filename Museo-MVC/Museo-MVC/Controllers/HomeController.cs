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
                Souvenir? souvenirDetails = db.Articles.Where(souvenir => souvenir.Id == id).FirstOrDefault();

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
        public IActionResult Create(Article newArticle)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newArticle);
            }

            using (MuseoContext db = new MuseoContext())
            {
                db.Articles.Add(newArticle);
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
                Article? articleToModify = db.Articles.Where(article => article.Id == id).FirstOrDefault();

                if (articleToModify != null)
                {
                    return View("Update", articleToModify);
                }
                else
                {

                    return NotFound("Articolo da modifcare inesistente!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Article modifiedArticle)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", modifiedArticle);
            }

            using (MuseoContext db = new MuseoContext())
            {
                Article? articleToModify = db.Articles.Where(article => article.Id == id).FirstOrDefault();

                if (articleToModify != null)
                {

                    articleToModify.Title = modifiedArticle.Title;
                    articleToModify.Description = modifiedArticle.Description;
                    articleToModify.Image = modifiedArticle.Image;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound("L'articolo da modificare non esiste!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                Article? articleToDelete = db.Articles.Where(article => article.Id == id).FirstOrDefault();

                if (articleToDelete != null)
                {
                    db.Remove(articleToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound("Non ho torvato l'articolo da eliminare");

                }
            }
        }



    }











}
}