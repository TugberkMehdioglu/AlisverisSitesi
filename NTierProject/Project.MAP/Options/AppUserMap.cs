using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //DB'de table olucak class'ım için istenilen özel ayarlamaları içeren class
    public class AppUserMap : BaseMap<AppUser>
    {
        public AppUserMap()
        {
            HasOptional(x => x.Profile).WithRequired(x => x.AppUser); //Bire-bir ilişki tamamlaması


            //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
            ToTable("Kullanıcılar");
            Property(x => x.UserName).HasColumnName("Kullanıcı Adı").IsRequired().HasMaxLength(15);
            Property(x => x.Password).HasColumnName("Şifre").IsRequired().HasMaxLength(20);
            
            Property(x => x.Active).HasColumnName("Aktif");
            Property(x => x.Email).HasColumnName("Mail Adresi").HasMaxLength(40).IsRequired();
            Property(x => x.Role).HasColumnName("Rolü");
        }
    }
}
