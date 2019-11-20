using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_layer;
using Business_layer.DTO;
using Data_layer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlantController : ControllerBase
    {
        private readonly KlantFacade facade;
        public KlantController(KlantFacade facade)
        {
            this.facade = facade;
        }

        [Route("{klantId}")]
        [HttpPost]
        public ActionResult<KlantDTO> CreateKlant(string klantId)
        {
            var createdKlant = facade.CreateKlant(klantId);
            if (createdKlant == null)
                return Conflict("Klant met die ID bestaat al.");
            return Created("", createdKlant);
        }

        [Route("{klantId}")]
        [HttpGet]
        public ActionResult<KlantDTO> GetKlant(string klantId)
        {
            var klant = facade.GetKlant(klantId);
            if (klant == null)
                return NotFound($"Klant met id:{klantId} bestaat niet.");
            return klant;
        }
    }
}