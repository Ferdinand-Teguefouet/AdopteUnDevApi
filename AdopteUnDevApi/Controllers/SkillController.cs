using AdopteUnDevApi.Models;
using AdopteUnDevApi.Tools;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IRepository<Skill> _skill;

        public SkillController(IRepository<Skill> skill)
        {
            _skill = skill;
        }

        [HttpGet]
        [Authorize("devPolicy")]
        public IActionResult GetAll()
        {
            return Ok(_skill.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize("devPolicy")]
        public IActionResult GetById(int id, string _token)
        {
            try
            {
                return Ok(_skill.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        [Authorize("devPolicy")]
        public IActionResult Insert(SkillForm sf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");
            }
            try
            {
                _skill.Insert(sf.ToDalSkill());
                return Ok("Insérer avec succès!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPut]
        [Authorize("devPolicy")]
        public IActionResult Update(SkillForm sf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");
            }
            if (sf.Id == 0)
            {
                return BadRequest("L'identifiant de l'utilisateur (Id) est obligatoire.");
            }
            try
            {
                _skill.Update(sf.ToDalSkill());
                return Ok("Utilisateur mise à jour avec succès");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize("devPolicy")]
        public IActionResult Delete(int id)
        {
            try
            {
                _skill.Delete(id);
                return Ok("Opération effectuée.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
