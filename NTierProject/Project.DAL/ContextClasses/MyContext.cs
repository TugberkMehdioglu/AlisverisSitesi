using Project.DAL.StrategyPattern;
using Project.ENTITIES.Models;
using Project.MAP.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.ContextClasses
{
    public class MyContext : DbContext //DB oluşturma ayarlamaları için buradan inheritance aldık
    {
        public MyContext() : base("MyConnection") //web.Config'deki xml tag'imizi yakalamak için name'ini verdik
        {
            Database.SetInitializer(new MyInit()); //Strategy Pattern'deki fakeData'larımızın son ayarlamasını yaptık
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //MAP katmanındaki ayarlamaları eklemek için override ettik
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
        }

        //Class'larımızı DB'de table olarak kurduk
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }

    }
}
