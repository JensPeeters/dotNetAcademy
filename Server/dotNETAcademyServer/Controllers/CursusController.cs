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
        private readonly ICursusFacade _facade;

        public CursusController(ICursusFacade facade )
        {
            this._facade = facade;
        }

        [HttpGet]
        public List<CursusDTO> GetCursussen([FromQuery]CursusFilter filter)
        {
            return _facade.GetCursussen(filter);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<CursusDTO> GetCursus(int id)
        {
            var cursus = _facade.GetCursus(id);
            if (cursus == null)
                return NotFound($"Cursus met id:{id} bestaat niet.");
            return cursus;
        }

        [HttpPost]
        public ActionResult<CursusDTO> AddCursus([FromBody] CursusCreateUpdateDTO cursus)
        {
            var createdCursus = _facade.AddCursus(cursus);
            if (createdCursus == null)
                return Conflict("Cursus met die titel bestaat al.");
            return Created($"api/cursus/{createdCursus.ID}", createdCursus);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<CursusDTO> DeleteCursus(int id)
        {
            var deletedCursus = _facade.DeleteCursus(id);
            if (deletedCursus == null)
                return NotFound($"Cursus met id:{id} bestaat niet.");
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<CursusCreateUpdateDTO> UpdateCursus([FromBody]CursusCreateUpdateDTO cursus, int id)
        {
            var updatedCursus = _facade.UpdateCursus(cursus, id);
            if (updatedCursus == null)
                return Conflict($"Cursus met id:{id} bestaat niet.");
            return Ok(updatedCursus);
        }
    }
}
