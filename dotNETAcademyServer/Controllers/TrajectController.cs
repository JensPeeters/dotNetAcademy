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
        public List<Traject> GetTrajecten(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            IQueryable<Traject> query = context.Trajecten;
            if (!string.IsNullOrEmpty(type))
                query = query.Where(b => b.Type == type);

            if (!string.IsNullOrEmpty(titel))
                query = query.Where(b => b.Titel == titel);

            if (string.IsNullOrEmpty(sortBy))
                sortBy = "id";

            switch (sortBy.ToLower())
            {
                case "id":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.TrajectId);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.TrajectId);
                    break;
                case "prijs":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Prijs);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.Prijs);
                    break;
                case "titel":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Titel);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.Titel);
                    break;
                case "type":
                    if (direction == "asc")
                        query = query.OrderBy(b => b.Type);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.Type);
                    break;
                default:
                    if (direction == "asc")
                        query = query.OrderBy(b => b.TrajectId);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.TrajectId);
                    break;
            }
            if (pageSize > 16)
                pageSize = 16;

            query = query.Skip(page * pageSize);
            query = query.Take(pageSize);

            return query.Include(a => a.Cursussen).ToList();
        }
        [Route("{id}")]
        [HttpGet]
        public ActionResult<Traject> GetTraject(int id)
        {
            return context.Trajecten.FirstOrDefault(a => a.TrajectId == id);
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

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<Traject> DeleteThroughURL(int id)
        {
            var traject = context.Trajecten.Include(a => a.Cursussen).FirstOrDefault(a => a.TrajectId == id);

            if (traject == null)
                return NotFound();
            context.Trajecten.Remove(traject);
            context.SaveChanges();
            return NoContent();
        }
    }
}