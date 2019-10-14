using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNETAcademyServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/traject")]
    [ApiController]
    public class TrajectController : ControllerBase
    {
        DatabaseContext context;

        public TrajectController(DatabaseContext ctx)
        {
            this.context = ctx;
        }

        [HttpGet]
        public List<Traject> GetTrajects()
        {
            return context.Trajecten.Include(b => b.Cursussen).ToList();
        }

        [HttpPost]
        public ActionResult<Traject> AddTraject([FromBody] Traject traject)
        {
            var tempTraject = context.Trajecten.FirstOrDefault(o => o.Titel == traject.Titel);
            if (tempTraject != null)
                return NoContent();
            context.Trajecten.Add(traject);
            context.SaveChanges();
            return Created("", traject);
        }
    }
}