
using System.Collections.Generic;
using Business_layer;
using Business_layer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/cursus")]
    [ApiController]
    public class CursusController : ControllerBase
    {
        private readonly CursusFacade facade;

        public CursusController(CursusFacade facade)
        {
            this.facade = facade;
        }

        [HttpGet]
        public List<CursusDTO> GetCursussen(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            return facade.GetCursussen(type, titel, sortBy, direction, pageSize, page);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<CursusDTO> GetCursus(int id)
        {
            var cursus = facade.GetCursus(id);
            if (cursus == null)
                return NotFound($"Cursus met id:{id} bestaat niet.");
            return cursus;
        }

        [HttpPost]
        public ActionResult<CursusCreateUpdateDTO> AddCursus([FromBody] CursusCreateUpdateDTO cursus)
        {
            var createdCursus = facade.AddCursus(cursus);
            if (createdCursus == null)
                return Conflict("Cursus met die titel bestaat al.");
            return Created("", createdCursus);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<CursusDTO> DeleteCursus(int id)
        {
            var deletedCursus = facade.DeleteCursus(id);
            if (deletedCursus == null)
                return NotFound($"Cursus met id:{id} bestaat niet.");
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<CursusCreateUpdateDTO> UpdateCursus([FromBody]CursusCreateUpdateDTO cursus, int id)
        {
            var updatedCursus = facade.UpdateCursus(cursus, id);
            if (updatedCursus == null)
                return Conflict($"Cursus met id:{id} bestaal al.");
            return Ok(updatedCursus);
        }
    }
}
