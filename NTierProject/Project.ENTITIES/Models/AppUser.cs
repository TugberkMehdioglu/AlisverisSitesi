using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser : BaseEntity
    {
        [Required(ErrorMessage ="{0} girilmesi zorunludur")]
        [MaxLength(15, ErrorMessage ="{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage ="{0} en az {1} karakter alabilir")]
        [Display(Name ="Kullanıcı adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="{0} zorunludur")]
        [MaxLength(20, ErrorMessage ="{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name ="Şifre")]
        public string Password { get; set; }

        public Guid ActivationCode { get; set; } //Mail ile kullanıcıya bu kodu yollicaz
        public bool Active { get; set; } //Kod ile yollanan link'e tıklandığında bu property true olucak

        [Required(ErrorMessage ="{0} girilmesi zorunludur")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(5, ErrorMessage = "{0} en az {1} karakter alabilir")]
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public AppUser()
        {
            Role = UserRole.Member; //Bu class'tan inheritance almış olan her class'dan instance alındığı gibi bu property'sine belirttiğim değerin atanmasını sağladım
            ActivationCode = Guid.NewGuid(); //Kullanıcı üye olduğu gibi instance alınarak aktivasyon kodunun oluşmasını sağladım
        }

        //Relational Properties
        public virtual UserProfile Profile { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
