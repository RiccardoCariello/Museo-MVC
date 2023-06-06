using Microsoft.EntityFrameworkCore;
using Museo_MVC.Models;


namespace Museo_MVC.DataBase
{
    public class MuseoContext : DbContext
    {


        public DbSet<Souvenir> Souvenir { get ; set };

        public DbSet<Category> Category { get; set; }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EFMuseo;Integrated Security=True");
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EFTecBlog;" + "Integrated Security=True;TrustServerCertificate=True");
        }









    }
}
