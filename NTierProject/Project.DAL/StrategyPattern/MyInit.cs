using Bogus;
using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.ContextClasses;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    //Bu class için FakeData'yı sağlamak amacıyla Bogus küttüphanesi kullandım
    public class MyInit : CreateDatabaseIfNotExists<MyContext> //DB modelleri her değişime uğradığında DB'yi yeniden kurduk (test için) (sonradan değiştirildi!!)
    {
        protected override void Seed(MyContext context)
        {
            AppUser au = new AppUser()
            {
                UserName = "Mehdi",
                Password = DantexCryptex.Crypt("123"), //Şifreleri DB'de kriptolanmış bir şekilde tutuyoruz
                Email = "tugberkmehdioglu@gmail.com",
                Role = UserRole.Admin
            };

            context.AppUsers.Add(au);

            for (int i = 0; i < 10; i++) //10 tane kullanıcı oluşturduk
            {
                AppUser ap = new AppUser
                {
                    UserName = $"Random{i}",
                    Password = "123", //Şifreler bogus'tan geldiği için bu kullanıcıların şifrelerini kriptolamıyoruz
                    Email = $"blabla{i}@gmail.com",                    
                };
                context.AppUsers.Add(ap);
            }

            for (int i = 1; i < 12; i++) //İlk oluşturduğumuz admin'in profili olmicak, o yüzden profile'ların ID'lerini 2'den itibaren vermeye başlicaz
            {
                UserProfile up = new UserProfile
                {
                    ID = i,
                    FirstName = "Random",
                    LastName = "Random",
                    Country = "Random",
                    Region = "Random", //Region bulamadım
                    City = "Random",
                    Address = "Random"
                };
                context.Profiles.Add(up);
            }
            context.SaveChanges();


            //10 adet kategori oluşturduk ve her bir kategorinin 20 adet ürünü oldu
            for (int i = 0; i < 10; i++)
            {
                Category c = new Category
                {
                    Name = $"Random{i}", //1 adet kategori oluştur, ilkini al
                    Description = $"Random{i}"
                };

                for (int k = 0; k < 20; k++)
                {
                    Product p = new Product
                    {
                        Name = $"Random{k}",
                        UnitPrice = Convert.ToDecimal($"100{k}"),
                        UnitInStock = 50,
                        ImagePath = new Images("tr").Fashion() //Kütüphane eski olduğundan resimler çok geç yüklenebilir ya da yüklenemeyebilir
                    };
                    c.Products.Add(p);
                }
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }
    }
}
