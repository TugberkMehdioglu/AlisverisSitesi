using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    //Bu class'ımın görevi sadece inheritance olucağı için abstract yaptım
    //Class'ımın amacı inharantance alan her class'da bulunmasını istediğim ortak özellekleri içermesi
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity //Sadece BaseEntity'den miras alan class'lar(DB'de table olucak class'larım) generic'in içine atanabilicek
    {
        public BaseMap()
        {
            Property(x => x.CreatedDate).HasColumnName("Yaratılma Tarihi"); //Column türkçeleştirme
            Property(x => x.DeletedDate).HasColumnName("Silme Tarihi"); //Column türkçeleştirme
            Property(x => x.ModifiedDate).HasColumnName("Güncellenme Tarihi"); //Column türkçeleştirme
            Property(x => x.Status).HasColumnName("Durum"); //Column türkçeleştirme
        }
    }
}
