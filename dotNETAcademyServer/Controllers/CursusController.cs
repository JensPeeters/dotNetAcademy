using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNETAcademyServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/cursus")]
    [ApiController]
    public class CursusController : ControllerBase
    {
        DatabaseContext context;

        public CursusController(DatabaseContext ctx)
        {
            this.context = ctx;
        }

        [HttpGet]
        public List<Cursus> GetCourses(string type, string titel,
                                                 string sortBy, string direction = "asc",
                                                 int pageSize = 16, int page = 0)
        {
            IQueryable<Cursus> query = context.Cursussen;
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
                        query = query.OrderBy(b => b.CursusID);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.CursusID);
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
                        query = query.OrderBy(b => b.CursusID);
                    else if (direction == "desc")
                        query = query.OrderByDescending(b => b.CursusID);
                    break;
            }
            if (pageSize > 16)
                pageSize = 16;

            query = query.Skip(page * pageSize);
            query = query.Take(pageSize);

            return query.ToList();
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<Cursus> GetCursus(int id)
        {
            return context.Cursussen.FirstOrDefault(a => a.CursusID == id);
        }


        [HttpPost]
        public ActionResult<Cursus> AddCursus([FromBody] Cursus cursus)
        {
            var tempCursus = context.Cursussen.FirstOrDefault(o => o.Titel == cursus.Titel);
            if (tempCursus != null)
                return NoContent();
            context.Cursussen.Add(cursus);
            context.SaveChanges();
            return Created("", cursus);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult<Cursus> DeleteThroughURL(int id)
        {
            var cursus = context.Cursussen.Find(id);
            if (cursus == null)
                return NotFound();

            context.Cursussen.Remove(cursus);
            context.SaveChanges();
            return NoContent();
        }
    }
}