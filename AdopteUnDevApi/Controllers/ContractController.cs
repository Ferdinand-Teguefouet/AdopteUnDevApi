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
    public class ContractController : ControllerBase
    {
        private readonly IRepository<Contract> _contract;

        public ContractController(IRepository<Contract> contract)
        {
            _contract = contract;
        }

        [HttpGet]
        //[Authorize("clientPolicy")]
        [Authorize("devPolicy")]
        public IActionResult GetAll()
        {
            return Ok(_contract.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize("clientPolicy")]
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
        [Authorize("clientPolicy")]
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
        [Authorize("clientPolicy")]
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
        [Authorize("clientPolicy")]
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
