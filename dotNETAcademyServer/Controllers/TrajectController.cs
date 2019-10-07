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
    }
}