using AdopteUnDevApi.Tools;
using Data_Access_Layer.Entities.Views;
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
    public class DevelopperController : ControllerBase
    {
        private readonly IRepository<ProfilDev> _pvservice;
        //private readonly ITokenManager _token;

        public DevelopperController(IRepository<ProfilDev> pvservice)
        {
            _pvservice = pvservice;
        }

        [HttpGet("profil")]
        [Authorize("clientPolicy")]
        public IActionResult GetAll()
        {
            return Ok(_pvservice.GetAll().Select(d => d.ToApiProfilDev()));
        }
    }
}
