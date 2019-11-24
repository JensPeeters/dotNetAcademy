using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_layer;
using Business_layer.DTO;
using Data_layer.Filter;
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
        public List<TrajectDTO> GetTrajecten([FromQuery]ProductFilter filter)
        {
            return facade.GetTrajecten(filter);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<TrajectDTO> GetTraject(int id)
        {
            var traject = facade.GetTraject(id);
            if (traject == null)
                return NotFound($"Traject met id:{id} bestaat niet.");
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
                return NotFound($"Traject met id:{id} bestaat niet.");
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<TrajectDTO> UpdateTraject([FromBody]TrajectCreateUpdateDTO traject, int id)
        {
            var updatedTraject = facade.UpdateTraject(traject, id);
            if (updatedTraject == null)
                return Conflict($"Traject met id:{id} bestaat niet.");
            return Ok(updatedTraject);
        }
    }
}
