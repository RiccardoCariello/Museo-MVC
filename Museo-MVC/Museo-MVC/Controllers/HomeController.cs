using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Museo_MVC.DataBase;
using Museo_MVC.Models;
using Museo_MVC.Models.ModelForViews;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

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
        // ACTIONS PER LA VISUALIZZAZIONE COMPLETA DEI SOUVENIR 
        [HttpGet]
        public IActionResult Souvenir()
        {
            using (MuseoContext db = new MuseoContext())
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

        //PAGINA CONFERTMA ACQUISTO
        [HttpGet]
        [Authorize(Roles = "USER")]
        public IActionResult ConfirmPurchase(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                //SouvenirListCategory modelForView = new SouvenirListCategory();
                SouvenirListAcquistis modelForView = new SouvenirListAcquistis();
                
                modelForView.Acquistis = new Acquisti();
                modelForView.Acquistis.Souvenir = db.Souvenirs.Where(souvenir=> souvenir.Id == id).FirstOrDefault();
                modelForView.SouvenirsList = db.Souvenirs.ToList();
                //modelForView.Souvenirs = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();
                
                //modelForView.SouvenirsList.Add(db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault());
                return View("ConfirmPurchase", modelForView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmPurchase(int id, SouvenirListAcquistis acquisto)
        {
            if (ModelState.IsValid)
            {
                using (MuseoContext db = new MuseoContext())
                {
                    Souvenir souvenirPerLaQuantità = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();
                    //acquisto.Acquistis.Souvenir = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();
                    if(souvenirPerLaQuantità.Quantity >= acquisto.Acquistis.Quantity)
                    {
						souvenirPerLaQuantità.Quantity = souvenirPerLaQuantità.Quantity - acquisto.Acquistis.Quantity;
						acquisto.Acquistis.SouvenirId = id;
						acquisto.Acquistis.Date = DateTime.UtcNow;
						db.Acquistis.Add(acquisto.Acquistis);
						db.SaveChanges();
						return RedirectToAction("Souvenir");
					}
					else
					{
						ModelState.AddModelError("Acquistis.Quantity", "Quantità non disponibile");
						return View("ConfirmPurchase", acquisto);
					}



				}
            }
            else
            {
				return Problem("L'Impossibile continuare l'acquisto!");
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

        // ACTIONS PER LA CREAZIONE DI UN SOUVENIR GET
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using (MuseoContext db = new MuseoContext())
            {
                List<Category> souvenirCategories = db.Categories.ToList();
                //List<Category> souvenirCategories = new List<Category>();
                SouvenirListCategory modelForView = new SouvenirListCategory();
                modelForView.Souvenirs = new Souvenir();
                modelForView.Categories = souvenirCategories;

                return View(modelForView);
            }
        }

        // ACTIONS PER LA CREAZIONE DI UN SOUVENIR POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SouvenirListCategory newSouvenir)
        {
            if (!ModelState.IsValid)
            {
                using (MuseoContext db = new MuseoContext())
                {
                    return Problem("L'oggetto non è stato creato!");
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

                    souvenirToModify.Img = modifiedSouvenir.Img;
                    souvenirToModify.Name = modifiedSouvenir.Name;
                    souvenirToModify.Description = modifiedSouvenir.Description;
                    souvenirToModify.Price = modifiedSouvenir.Price;
                    souvenirToModify.Quantity = modifiedSouvenir.Quantity;
                    db.SaveChanges();
                    return RedirectToAction("Souvenir");

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

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult ConfirmOrder(int id)
        {
            using (MuseoContext db = new MuseoContext())
            {
                SouvenirListOrders modelForView = new SouvenirListOrders();
                modelForView.Souvenirs = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

				string userName = User.Identity.Name;
				modelForView.Orders = new Ordini();
				modelForView.Orders.Name = userName;
                
                
                return View("ConfirmOrder", modelForView);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult ConfirmOrder(int id, SouvenirListOrders ordine)
        {

            using (MuseoContext db = new MuseoContext())
            {
                if (!ModelState.IsValid)
                {
                    return View("ConfirmOrder", ordine);
                }
                else
                {
                    ordine.Souvenirs = db.Souvenirs.Where(souvenir => souvenir.Id == id).FirstOrDefault();

                    Ordini newOrder = new Ordini();
                    newOrder.Price = ordine.Souvenirs.Price;
                    newOrder.SouvenirName = ordine.Souvenirs.Name;
                    newOrder.Souvenir = ordine.Souvenirs;
                    newOrder.Fornitore = ordine.Orders.Fornitore;
                    newOrder.Date = DateTime.Now;
                    newOrder.Quantity = ordine.Orders.Quantity;
                    newOrder.SouvenirId = id;
                    newOrder.Name = ordine.Orders.Name;
                    ordine.Souvenirs.Quantity = ordine.Souvenirs.Quantity + newOrder.Quantity;

                    if (newOrder != null)
                    {
                        
                        
                        db.Add(newOrder);

                        db.SaveChanges();
                        return RedirectToAction("Souvenir");

                    }
                    else
                    {
                        return NotFound();
                    }
                }






            }


        }

    }
}