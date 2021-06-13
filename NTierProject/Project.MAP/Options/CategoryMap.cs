using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //DB'de table olucak class'ım için istenilen özel ayarlamaları içeren class
    public class CategoryMap : BaseMap<Category>
    {
        public CategoryMap()
        {
            //Column'ları türkçeleştirme ve gerekli yerlerde sınırlamalar koyuldu
            ToTable("Kategoriler");
            Property(x => x.Name).HasColumnName("Kategori İsmi").IsRequired().HasMaxLength(20);
            Property(x => x.Description).HasColumnName("Açıklama").HasMaxLength(30);
        }
    }
}
