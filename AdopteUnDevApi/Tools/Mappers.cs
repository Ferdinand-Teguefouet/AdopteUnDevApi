using AdopteUnDevApi.Models;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Tools
{
    public static class Mappers
    {
        public static User ToDalUser(this UserForm uf)
        {
            return new User
            {
                Id = uf.Id,
                Name = uf.Name,
                Email = uf.Email,
                Password = uf.Password,
                Telephone = uf.Telephone,
                IsClient = uf.IsClient
            };
        }

        public static Contract ToDalContract(this ContractForm cf)
        {
            return new Contract
            {
                Id = cf.Id,
                Description = cf.Description,
                DeadLine = cf.DeadLine,
                Price = cf.Price
            };
        }

        public static Skill ToDalSkill(this SkillForm sf)
        {
            return new Skill
            {
                Id = sf.Id,
                SkillName = sf.SkillName,
                Description = sf.Description
            };
        }
    }
}
