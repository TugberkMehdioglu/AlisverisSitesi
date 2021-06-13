using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class UserProfile : BaseEntity
    {
        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } } //DB'ye gitmicek

        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "Ülke")]
        public string Country { get; set; }

        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "İlçe")]
        public string Region { get; set; }

        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(20, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "Şehir")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} girilmesi zorunludur")]
        [MaxLength(50, ErrorMessage = "{0} en fazla {1} karakter alabilir")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter alabilir")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        //Relational Properties
        public virtual AppUser AppUser { get; set; }



    }
}
