using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Core.Entities.Concrete;
using System.Reflection;
using Entities.Concrete;

namespace DataAccess.Concrete.Context
{
    public class EfCoreDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//hocanın kodunda startupcsten alıyordu-  ama benim yapıma uymadı-
            //configuraiton managerle appsettingsten gerekenleri cekip burada if else ile secim sagladım
            ConfigurationManager configurationManager = new();//nuget 6.01 versiyonu geerekli
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebAPI"));
            configurationManager.AddJsonFile("appsettings.json");


            var dbType = configurationManager.GetConnectionString("DbType");
            if (dbType == "SQL")
            {
                optionsBuilder.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));
            }
            else if (dbType == "PostgreSQL")
            {
                optionsBuilder.UseNpgsql(configurationManager.GetConnectionString("PostgreSqlConnection"));
            }


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //sağdaki veritabanındaki tablo adı

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public  DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }


        public DbSet<User> Users { get; set; }
       
        
        





    }

}
