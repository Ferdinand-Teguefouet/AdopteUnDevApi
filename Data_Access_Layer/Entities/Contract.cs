using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
