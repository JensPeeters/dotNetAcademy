using Business_layer.DTO;
using Business_layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlantController : ControllerBase
    {
        private readonly IKlantFacade _klantFacade;
        public KlantController(IKlantFacade klantFacade)
        {
            _klantFacade = klantFacade;
        }

        [Route("{klantId}")]
        [HttpPost]
        public ActionResult<KlantDTO> CreateKlant(string klantId)
        {
            var createdKlant = _klantFacade.CreateKlant(klantId);
            if (createdKlant == null)
                return Conflict("Klant met die ID bestaat al.");
            return Created("", createdKlant);
        }

        [Route("{klantId}")]
        [HttpDelete]
        public ActionResult DeleteKlant(string klantId)
        {
            var deletedKlant = _klantFacade.DeleteKlant(klantId);
            if (deletedKlant == null)
                return NotFound("Klant bestaat niet.");
            return Ok("Klant succesvol verwijderd.");
        }

        [Route("toadmin/{klantId}")]
        [HttpPut]
        public ActionResult MakeKlantAdmin(string klantId)
        {
            var changedKlantAdmin = _klantFacade.MakeKlantAdmin(klantId);
            if (changedKlantAdmin == null)
                return NotFound("Klant bestaat niet.");
            return Ok("Klant succesvol aangepast naar admin.");
        }

        [Route("{klantId}")]
        [HttpGet]
        public ActionResult<KlantDTO> GetKlant(string klantId)
        {
            var klant = _klantFacade.GetKlant(klantId);
            if (klant == null)
                return NotFound($"Klant met id:{klantId} bestaat niet.");
            return klant;
        }
    }
}