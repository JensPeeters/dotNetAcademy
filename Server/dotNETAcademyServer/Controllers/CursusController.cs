using System.Collections.Generic;
using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer.Filter.ProductenFilters;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/cursus")]
    [ApiController]
    public class CursusController : ControllerBase
    {
        private readonly ICursusFacade _cursusFacade;
        public CursusController(ICursusFacade cursusFacade)
        {
            _cursusFacade = cursusFacade;
        }

        [Route("types")]
        [HttpGet]
        public List<string> GetCursusTypes()
        {
            return _cursusFacade.GetCursusTypes();
        }

        [HttpGet]
        public List<CursusDTO> GetCursussen([FromQuery]CursusFilter filter)
        {
            return _cursusFacade.GetCursussen(filter);
        }

        [Route("buyable")]
        [HttpGet]
        public List<CursusDTO> GetBuyableCursussen([FromQuery]CursusFilter filter)
        {
            return _cursusFacade.GetBuyableCursussen(filter);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<CursusDTO> GetCursus(int id)
        {
            var cursus = _cursusFacade.GetCursus(id);
            if (cursus == null)
                return NotFound($"Cursus met id:{id} bestaat niet.");
            return cursus;
        }

        [HttpPost]
        public ActionResult<CursusDTO> AddCursus([FromBody] CursusCreateUpdateDTO cursus)
        {
            var createdCursus = _cursusFacade.AddCursus(cursus);
            if (createdCursus == null)
                return Conflict("Cursus met die titel bestaat al.");
            return Created($"api/cursus/{createdCursus.ID}", createdCursus);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<CursusDTO> DeleteCursus(int id)
        {
            var deletedCursus = _cursusFacade.DeleteCursus(id);
            if (deletedCursus == null)
                return NotFound($"Cursus met id:{id} bestaat niet.");
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<CursusCreateUpdateDTO> UpdateCursus([FromBody]CursusCreateUpdateDTO cursus, int id)
        {
            if (cursus.OrderNumber <= 0)
                return BadRequest("OrderNumber mag niet kleiner of gelijk zijn aan 0.");
            
            var updatedCursus = _cursusFacade.UpdateCursus(cursus, id);
            if (updatedCursus == null)
                return Conflict($"Cursus met id:{id} bestaat niet.");
            return Ok(updatedCursus);
        }
    }
}
