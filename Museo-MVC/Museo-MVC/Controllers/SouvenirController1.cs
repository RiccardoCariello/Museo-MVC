using Microsoft.AspNetCore.Mvc;
using Museo_MVC.DataBase;
using Museo_MVC.Models;

namespace Museo_MVC.Controllers
{
	public class SouvenirController1 : Controller
	{
		public IActionResult Index()
		{
			return View();
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

		// ACTIONS PER LA CREAZIONE DI UN SOUVENIR
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

		// ACTIONS PER LA MODIFICA DI UN SOUVENIR
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
