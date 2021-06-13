using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //DB'de table olucak class'ım için istenilen özel ayarlamaları içeren class
    public class OrderMap : BaseMap<Order>
    {
        public OrderMap()
        {
            //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
            ToTable("Siparişler");
            Property(x => x.Address).HasColumnName("Adres");
            Property(x => x.TotalPrice).HasColumnName("Toplam Ücret");
            Property(x => x.UserName).HasColumnName("Kullanıcı Adı");
            Property(x => x.Email).HasColumnName("Mail");
        }
    }
}
