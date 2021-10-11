using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Models
{
    public class UserForm
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Le champ nom est obligatoire.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Une adresse mail est obligatoire.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Un mot de passe est obligatoire.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Un numéro de téléphone est obligatoire.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "True: si c'est un client / False: si c'est un utilisateur.")]
        public bool IsClient { get; set; }
    }
}
