using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Museo_MVC.Models
{
    public class Acquisti
    {
        
        public int Id { get; set; }

        DateTime Date { get; set; }

        string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        
        [MaxLength(5)]
        public string Cap { get; set; }
        public int? SouvenirId { get; set; }

        public Souvenir? Souvenir { get; set; }


        public Acquisti() { }

        public Acquisti(string name , string surname , string email, string cap)
        {
            Date = DateTime.UtcNow;
            Name = name;
            Surname = surname;
            Email = email;
            Cap = cap;

        }
        


    }
}
