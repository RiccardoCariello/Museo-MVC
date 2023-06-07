using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Museo_MVC.DataBase;
using Museo_MVC.Models;
using Museo_MVC.Models.ModelForViews;
using System.Diagnostics;

namespace Museo_MVC.Controllers
{
    //[Authorize(Roles = "ADMIN,USER")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            
                
                return View();
            

        }
        
        [HttpGet]
        public IActionResult Souvenir()
        {
            using(MuseoContext db = new MuseoContext())
            {
                List<Souvenir>? souvenirs = db.Souvenirs.ToList();
                if (souvenirs != null)
                {

                    return View("Souvenir", souvenirs);
                }
                else
                {
                    return NotFound();
                }
            }
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
                    return NotFound($"Il souvenir con id {id} non è stato trovato!");
                }
            }

        }

        //PAGINA CONFERMA ELIMINAZIONE
        public IActionResult ConfirmDelete(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                Souvenir? souvenirDelete = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                if (souvenirDelete != null)
                {
                    return View("ConfirmDelete", souvenirDelete);
                }
                else
                {
                    return NotFound($"Il souvenir con id {id} non è stato trovato!");
                }
            }

        }

        // ACTIONS PER LA CREAZIONE DI UN SOUVENIR
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using(MuseoContext db = new MuseoContext())
            {
                List<Category> souvenirCategories = db.Categories.ToList();
                //List<Category> souvenirCategories = new List<Category>();
                SouvenirListCategory modelForView = new SouvenirListCategory();
                modelForView.Souvenirs = new Souvenir();
                modelForView.Categories = souvenirCategories;

                return View(modelForView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SouvenirListCategory newSouvenir)
        {
            if (!ModelState.IsValid)
            {
                using(MuseoContext db = new MuseoContext())
                {
                    List<Category> souvenirCategories = db.Categories.ToList();
                    newSouvenir.Categories = souvenirCategories;
					return View("Create", newSouvenir);
				}
                
            }

            using (MuseoContext db = new MuseoContext())
            {
                db.Souvenirs.Add(newSouvenir.Souvenirs);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }

		// ACTIONS PER LA MODIFICA DI UN SOUVENIR
		[Authorize(Roles = "ADMIN")]
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
                Souvenir? souvenirToModify = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

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

		[Authorize(Roles = "ADMIN")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                Souvenir? souvenirToDelete = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                if (souvenirToDelete != null)
                {
                    db.Remove(souvenirToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Souvenir");

                }
                else
                {
                    return NotFound("Non ho torvato il souvenir da eliminare");

                }
            }
        }



    }











}
