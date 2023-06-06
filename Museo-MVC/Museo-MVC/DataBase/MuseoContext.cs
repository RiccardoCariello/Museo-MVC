using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Museo_MVC.Models;


namespace Museo_MVC.DataBase
{
    public class MuseoContext : IdentityDbContext<IdentityUser>

    {


        public DbSet<Souvenir> Souvenirs { get; set; }

        public DbSet<Category> Categories { get; set; }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EFMuseo;Integrated Security=True");
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=EFMuseo;" + "Integrated Security=True;TrustServerCertificate=True");
        }









    }
}
