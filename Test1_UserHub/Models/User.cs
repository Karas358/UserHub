using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1_UserHub.Models
{
    public class User
    {
        [Required]
        public string ID { get; set; }
        [Required(ErrorMessage = "Please provide a name")]
        [StringLength(30)]  
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide a surname")]
        [StringLength(30)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        [StringLength(10)]
        public string Cellphone { get; set; }
    }
}
