
using System.Collections.Generic;
using Business_layer;
using Data_layer.Model;
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
        public List<Cursus> GetCursussen(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            return facade.GetCursussen(type, titel, sortBy, direction, pageSize, page);
        }

        [Route("{id}")]
        [HttpGet]
        public Cursus GetCursus(int id)
        {
            return facade.GetCursus(id);
        }

        [HttpPost]
        public ActionResult<Cursus> AddCursus([FromBody] Cursus cursus)
        {
            var createdCursus = facade.AddCursus(cursus);
            if (createdCursus == null)
                return NoContent();
            return Created("", createdCursus);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<Cursus> DeleteCursus(int id)
        {
            var deletedCursus = facade.DeleteCursus(id);
            if (deletedCursus == null)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<Cursus> UpdateCursus([FromBody]Cursus cursus)
        {
            var updatedCursus = facade.UpdateCursus(cursus);
            return Created("", updatedCursus);
        }
    }
}
