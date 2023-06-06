using System.ComponentModel.DataAnnotations;

namespace Museo_MVC.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Souvenir> SouvenirList { get; set; }
		
		public Category()
		{

		}
		public Category(string name)
		{
			Name = name;
			SouvenirList = new List<Souvenir>();
		}
	}
}
