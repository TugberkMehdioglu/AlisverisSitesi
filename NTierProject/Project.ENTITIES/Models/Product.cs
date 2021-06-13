using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "Kullanıcı adı")]
        public string Name { get; set; }

        [Range(1, double.MaxValue, ErrorMessage ="{0} belli bir değer aralığında olmalıdır")]
        [Required(ErrorMessage ="{0} girilmesi zorunludur")]
        [Display(Name ="Birim fiyatı")]
        public decimal UnitPrice { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "{0} belli bir değer aralığında olmalıdır")]
        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [Display(Name = "Stok")]
        public short UnitInStock { get; set; }
        public string ImagePath { get; set; } //Her ürünün resmi olucak
        public int CategoryID { get; set; }

        //Relational Properties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> Details { get; set; }


    }
}
