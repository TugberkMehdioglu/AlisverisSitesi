using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
    public class UserProfileMap : BaseMap<UserProfile>
    {
        public UserProfileMap()
        {
            Ignore(x => x.FullName); //FullName property'si DB'de olmicak

            //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
            ToTable("Profiller");
            Property(x => x.FirstName).HasColumnName("İsim").IsRequired().HasMaxLength(20);
            Property(x => x.LastName).HasColumnName("Soyisim").IsRequired().HasMaxLength(20);
            Property(x => x.Country).HasColumnName("Ülke").IsRequired().HasMaxLength(20);
            Property(x => x.Region).HasColumnName("İlçe").IsRequired().HasMaxLength(20);
            Property(x => x.City).HasColumnName("Şehir").IsRequired().HasMaxLength(20);
            Property(x => x.Address).HasColumnName("Adres").IsRequired().HasMaxLength(50);
        }
    }
}
