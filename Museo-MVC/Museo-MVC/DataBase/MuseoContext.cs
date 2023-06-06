using Microsoft.EntityFrameworkCore;
using Museo_MVC.Models;


namespace Museo_MVC.DataBase
{
    public class MuseoContext : DbContext
    {


        public DbSet<Souvenir> Souvenirs { get; set; }

        public DbSet<Category> Categories { get; set; }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EFMuseo;Integrated Security=True");
            //optionsBuilder.UseSqlServer("Data Source=PC-DI-UMBERTO\\MSSQLSERVER15;Initial Catalog=EFMuseo;" + "Integrated Security=True;TrustServerCertificate=True");
        }









    }
}
