using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order : BaseEntity
    {
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int AppUserID { get; set; } //Kullanıcısı belli olmayan sipariş olmasını istemiyorum o yüzden requerid

        //----- AppUser'dan bu property'leri çektik çünkü sipariş kısmından kullanıcının bu bilgilerini daha rahat görmek istedik
        public string UserName { get; set; }
        public string Email { get; set; }
        //-----

        //Relational Properties
        public virtual AppUser AppUser { get; set; }
        public virtual List<OrderDetail> Details { get; set; }


    }
}
