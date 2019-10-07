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
        public List<Cursus> GetCourses()
        {
            return context.Cursussen.ToList();
        }

       
    }
}