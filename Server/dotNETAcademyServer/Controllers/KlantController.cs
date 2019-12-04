using Business_layer.DTO;
using Business_layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlantController : ControllerBase
    {
        private readonly IKlantFacade _facade;
        public KlantController(IKlantFacade facade)
        {
            this._facade = facade;
        }

        [Route("{klantId}")]
        [HttpPost]
        public ActionResult<KlantDTO> CreateKlant(string klantId)
        {
            var createdKlant = _facade.CreateKlant(klantId);
            if (createdKlant == null)
                return Conflict("Klant met die ID bestaat al.");
            return Created("", createdKlant);
        }

        [Route("{klantId}")]
        [HttpDelete]
        public ActionResult DeleteKlant(string klantId)
        {
            var deletedKlant = _facade.DeleteKlant(klantId);
            if (deletedKlant == null)
                return NotFound("Klant bestaat niet.");
            return Ok("Klant succesvol verwijderd.");
        }

        [Route("{klantId}")]
        [HttpGet]
        public ActionResult<KlantDTO> GetKlant(string klantId)
        {
            var klant = _facade.GetKlant(klantId);
            if (klant == null)
                return NotFound($"Klant met id:{klantId} bestaat niet.");
            return klant;
        }
    }
}