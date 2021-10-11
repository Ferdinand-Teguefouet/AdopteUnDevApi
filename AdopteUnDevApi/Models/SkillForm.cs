using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Models
{
    public class SkillForm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Un nom est obligatoire.")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Une description est obligatoire.")]
        public string Description { get; set; }
    }
}
