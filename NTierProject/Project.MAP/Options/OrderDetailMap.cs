using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //DB'de table olucak class'ım için istenilen özel ayarlamaları içeren class
    public class OrderDetailMap : BaseMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            //Çoka-çok ilişki tamamlaması
            Ignore(x => x.ID); //ID'sini DB'ye yollamadık
            HasKey(x => new //verdiğimiz iki property ile composite key oluşturduk
            {
                x.OrderID,
                x.ProductID
            });

            //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
            ToTable("Sipariş Detayları");
            Property(x => x.TotalPrice).HasColumnName("Toplam Ücret");
            Property(x => x.Quantity).HasColumnName("Miktar");
        }
    }
}
