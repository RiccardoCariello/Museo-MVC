using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Museo_MVC.Models
{
    public class Ordini
    {
        
        public int Id {get; set;}

        public int AdminId { get; set;}

        public string Name { get; set;}

        
        public DateTime? Date { get; set;}

        public string Fornitore { get; set;}

        public string SouvenirName { get; set; } 

        public int Quantity { get; set;}

        public float Price { get; set;}

        public int? SouvenirId { get; set; }

        public Souvenir? Souvenir { get; set; }

        public Ordini() { }

        public Ordini (string name, string surname, int souvenirId, string souvenirName, int quantity, float price, string fornitore)
        {
            
            Name = name;
            
            Date = DateTime.Now;
            SouvenirId = souvenirId;
            SouvenirName = souvenirName;
            Quantity = quantity;
            Price = price;
            Fornitore = fornitore;
        }
    }
}
