using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Models
{
    public class ContractForm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Une description est obligatoire.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Un prix est obligatoire.")]
        public Decimal Price { get; set; }
        [Required(ErrorMessage = "Une date est obligatoire.")]
        public DateTime DeadLine { get; set; }
    }
}
