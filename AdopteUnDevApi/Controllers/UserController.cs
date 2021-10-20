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
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _user;
        private readonly ILogin _login;
        private readonly ITokenManager _token;

        public UserController(IRepository<User> user, ILogin login, ITokenManager token)
        {
            _user = user;
            _login = login;
            _token = token;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_login.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {                
                return Ok(_login.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]  LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");
            }
            UserConnected uc;
            try
            {
                uc = _login.GetExistedUser(login.ToDalLogin());
                UserModel connectedUser = _token.GenerateJWT(uc.ToApiUser());
                return Ok(connectedUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }            
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Insert([FromBody] UserForm u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");                
            }
            try
            {
                _user.Insert(u.ToDalUser());
                return Ok("Insérer avec succès!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpPut]
        public IActionResult Update(UserForm u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Vérifier les champs du formulaire.");
            }
            if (u.Id == 0)
            {
                return BadRequest("L'identifiant de l'utilisateur (Id) est obligatoire.");
            }
            try
            {
                _user.Update(u.ToDalUser());
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
                _user.Delete(id);
                return Ok("Opération effectuée.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
