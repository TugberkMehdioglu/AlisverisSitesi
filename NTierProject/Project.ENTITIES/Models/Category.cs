using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "Kategori adı")]
        public string Name { get; set; }

        [MaxLength(30, ErrorMessage ="{0} en fazla {1} karakter alabilir")]
        [Display(Name ="Açıklama")]
        public string Description { get; set; }
        public Category()
        {
            Products = new List<Product>(); //FakeData ile DB kurulurken data girişi yapıcağımız için(Initilization), savechanges() demeden category'e product'larını eklemek istiyorum
        }

        //Relational Properties
        public virtual List<Product> Products { get; set; }

    }
}
