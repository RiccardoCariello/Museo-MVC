using MessagePack;

namespace Museo_MVC.Models
{
    public class Ordini
    {
        [Key]
        public int Id {get; set;}

        public int AdminId { get; set;}

        public string Name { get; set;}

        
        public DateTime? Date { get; set;}

        public int SouvenirId { get; set;}

        public string SouvenirName { get; set; } 

        public int Quantity { get; set;}

        public float Price { get; set;}

        public Ordini() { }

        public Ordini (string name, string surname, int souvenirId, string souvenirName, int quantity, float price)
        {
            
            Name = name;
            Surname = surname;
            Date = DateTime.Now;
            SouvenirId = souvenirId;
            SouvenirName = souvenirName;
            Quantity = quantity;
            Price = price;
        }
    }
}
