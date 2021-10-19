using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Une adresse mail est obligatoire.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Un mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
