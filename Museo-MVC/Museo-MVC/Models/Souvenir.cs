using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Museo_MVC.Models
{
	public class Souvenir
	{
		[Key]
		public int Id { get; set; }


		public string Img { get; set; }
		[MaxLength(20)]
		public string Name { get; set; }
		public string Description { get; set; }
		public float Price { get; set; }

		public int? Quantity { get; set; }
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }

		public List<Acquisti>? AcquistiList { get; set; }

		

		public List<Ordini> OrdiniList { get; set; }
        

        public Souvenir() { }
		public Souvenir(string img, string name, string description, float price, int quantity)
		{
			Img = img;
			Name = name;
			Description = description;
			Price = price;
			AcquistiList = new List<Acquisti>();
			Quantity = quantity;

			OrdiniList = new List<Ordini>();
		}
	}
}
