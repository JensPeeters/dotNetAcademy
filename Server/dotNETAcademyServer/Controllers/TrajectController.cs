using System.Collections.Generic;
using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer.Filter.ProductenFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/traject")]
    [ApiController]
    public class TrajectController : ControllerBase
    {
        private readonly ITrajectFacade _trajectFacade;

        public TrajectController(ITrajectFacade trajectFacade)
        {
            _trajectFacade = trajectFacade;
        }

        [Route("types")]
        [HttpGet]
        public List<string> GetTrajectTypes()
        {
            return _trajectFacade.GetTrajectTypes();
        }

        [HttpGet]
        public List<TrajectDTO> GetTrajecten([FromQuery]TrajectFilter filter)
        {
            return _trajectFacade.GetTrajecten(filter);
        }
        [Route("buyable")]
        [HttpGet]
        public List<TrajectDTO> GetBuyableTrajecten([FromQuery]TrajectFilter filter)
        {
            return _trajectFacade.GetBuyableTrajecten(filter);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<TrajectDTO> GetTraject(int id)
        {
            var traject = _trajectFacade.GetTraject(id);
            if (traject == null)
                return NotFound($"Traject met id:{id} bestaat niet.");
            return traject;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<TrajectDTO> AddTraject([FromBody] TrajectCreateUpdateDTO traject)
        {
            var createdTraject = _trajectFacade.AddTraject(traject);
            if (createdTraject == null)
                return Conflict("Traject met die titel bestaat al.");
            return Created($"api/traject/{createdTraject.ID}", createdTraject);
        }

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public ActionResult<TrajectDTO> DeleteTraject(int id)
        {
            var deletedTraject = _trajectFacade.DeleteTraject(id);
            if (deletedTraject == null)
                return NotFound($"Traject met id:{id} bestaat niet.");
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<TrajectDTO> UpdateTraject([FromBody]TrajectCreateUpdateDTO traject, int id)
        {
            if (traject.OrderNumber <= 0)
                return BadRequest("OrderNumber mag niet kleiner of gelijk zijn aan 0.");
            var updatedTraject = _trajectFacade.UpdateTraject(traject, id);
            if (updatedTraject == null)
                return Conflict($"Traject met id:{id} bestaat niet.");
            return Ok(updatedTraject);
        }

        [Authorize]
        [HttpGet("amount/{id}")]
        public ActionResult<int> GetAmountSold(int id)
        {
            var amount = _trajectFacade.GetAmountSold(id);
            return Ok(amount);
        }
    }
}
