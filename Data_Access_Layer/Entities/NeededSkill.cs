using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class NeededSkill
    {
        public int Id { get; set; }
        public int ContractId_fk { get; set; }
        public int SkillId_fk { get; set; }
    }
}
