using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    //Bu class'ımın görevi sadece inheritance olucağı için abstract yaptım
    //Class'ımın amacı inharantance alan her class'da bulunmasını istediğim ortak özellekleri içermesi
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; } //Null geçilebilir çünkü her instance alındığında buraya veri girişi olma şartı yok
        public DateTime? DeletedDate { get; set; } //Null geçilebilir çünkü her instance alındığında buraya veri girişi olma şartı yok
        public DataStatus Status { get; set; }
        public BaseEntity()
        {
            Status = DataStatus.Inserted; //Bu class'tan inheritance almış olan her class'dan instance alındığı gibi bu property'sine belirttiğim değerin atanmasını sağladım
            CreatedDate = DateTime.Now; //Bu class'tan inheritance almış olan her class'dan instance alındığı gibi bu property'sine belirttiğim değerin atanmasını sağladım
        }
    }
}
