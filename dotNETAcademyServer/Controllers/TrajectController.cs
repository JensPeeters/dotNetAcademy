using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_layer;
using Data_layer.Model;
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
        public List<Traject> GetTrajecten(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            return facade.GetTrajecten(type, titel, sortBy, direction, pageSize, page);
        }

        [Route("{id}")]
        [HttpGet]
        public Traject GetTraject(int id)
        {
            return facade.GetTraject(id);
        }

        [HttpPost]
        public ActionResult<Traject> AddTraject([FromBody] Traject traject)
        {
            var createdTraject = facade.AddTraject(traject);
            if (createdTraject == null)
                return NoContent();
            return Created("", createdTraject);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<Traject> DeleteTraject(int id)
        {
            var deletedTraject = facade.DeleteTraject(id);
            if (deletedTraject == null)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<Traject> UpdateTraject([FromBody]Traject traject)
        {
            var updatedTraject = facade.UpdateTraject(traject);
            return Created("", updatedTraject);
        }
    }
}