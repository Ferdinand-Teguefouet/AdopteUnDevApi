using AdopteUnDevApi.Models;
using AdopteUnDevApi.Tools;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interface;
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
    public class ContractController : ControllerBase
    {
        private readonly IRepository<Contract> _contract;

        public ContractController(IRepository<Contract> contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_contract.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_contract.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        public IActionResult Insert(ContractForm cf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");
            }
            try
            {
                _contract.Insert(cf.ToDalContract());
                return Ok("Insérer avec succès!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpPut]
        public IActionResult Update(ContractForm cf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");
            }
            if (cf.Id == 0)
            {
                return BadRequest("L'identifiant de l'utilisateur (Id) est obligatoire.");
            }
            try
            {
                _contract.Update(cf.ToDalContract());
                return Ok("Utilisateur mise à jour avec succès");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _contract.Delete(id);
                return Ok("Opération effectuée.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
