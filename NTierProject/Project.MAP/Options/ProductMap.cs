using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //DB'de table olucak class'ım için istenilen özel ayarlamaları içeren class
    public class ProductMap : BaseMap<Product>
    {
        public ProductMap()
        {
            //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
            ToTable("Ürünler");
            Property(x => x.Name).HasColumnName("Adı").IsRequired().HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("Birim Fiyatı").IsRequired();
            Property(x => x.UnitInStock).HasColumnName("Stok Adedi").IsRequired();
            Property(x => x.ImagePath).HasColumnName("Resim Yolu");
        }
    }
}
