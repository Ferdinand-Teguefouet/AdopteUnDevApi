using AdopteUnDevApi.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Entities.Views;
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

        public static UserLogin ToDalLogin(this LoginModel login)
        {
            return new UserLogin
            {
                Email = login.Email,
                Password = login.Password
            };
        }

        public static UserModel ToApiUser(this UserConnected uc)
        {
            return new UserModel
            {
                Id = uc.Id,
                Email = uc.Email,
                Name = uc.Name,
                Telephone = uc.Telephone,
                IsClient = uc.IsClient
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

        public static ProfilDevModel ToApiProfilDev(this ProfilDev pv)
        {
            return new ProfilDevModel
            {
                Name = pv.Name,
                Skill = pv.Skill,
                Category = pv.Category,
                Description = pv.Description
            };
        }
    }
}
