using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_layer;
using Business_layer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/traject")]
    [ApiController]
    public class TrajectController : ControllerBase
    {
        private readonly TrajectFacade facade;

        public TrajectController(TrajectFacade facade)
        {
            this.facade = facade;
        }

        [HttpGet]
        public List<TrajectDTO> GetTrajecten(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            return facade.GetTrajecten(type, titel, sortBy, direction, pageSize, page);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<TrajectDTO> GetTraject(int id)
        {
            var traject = facade.GetTraject(id);
            if (traject == null)
                return NotFound();
            return traject;
        }

        [HttpPost]
        public ActionResult<TrajectDTO> AddTraject([FromBody] TrajectCreateUpdateDTO traject)
        {
            var createdTraject = facade.AddTraject(traject);
            if (createdTraject == null)
                return Conflict("Traject met die titel bestaat al.");
            return Created("", createdTraject);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<TrajectDTO> DeleteTraject(int id)
        {
            var deletedTraject = facade.DeleteTraject(id);
            if (deletedTraject == null)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<TrajectDTO> UpdateTraject([FromBody]TrajectCreateUpdateDTO traject, int id)
        {
            var updatedTraject = facade.UpdateTraject(traject, id);
            if (updatedTraject == null)
                return Conflict($"Traject met id:{id} bestaal al.");
            return Ok(updatedTraject);
        }
    }
}
