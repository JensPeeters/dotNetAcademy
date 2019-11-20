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
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminFacade facade;
        public AdminController(AdminFacade facade)
        {
            this.facade = facade;
        }

        [Route("{adminId}")]
        [HttpPost]
        public ActionResult<AdminDTO> CreateAdmin(string adminId)
        {
            var createdAdmin = facade.CreateAdmin(adminId);
            if (createdAdmin == null)
                return Conflict("Admin met die ID bestaat al.");
            return Created("", createdAdmin);
        }

        [Route("{adminId}")]
        [HttpGet]
        public ActionResult<AdminDTO> GetAdmin(string adminId)
        {
            var admin = facade.GetAdmin(adminId);
            if (admin == null)
                return NotFound($"Admin met id:{adminId} bestaat niet.");
            return admin;
        }
    }
}