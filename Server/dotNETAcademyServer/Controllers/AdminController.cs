using Business_layer.DTO;
using Business_layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminFacade _facade;
        public AdminController(IAdminFacade facade)
        {
            this._facade = facade;
        }

        [Route("{adminId}")]
        [HttpPost]
        public ActionResult<AdminDTO> CreateAdmin(string adminId)
        {
            var createdAdmin = _facade.CreateAdmin(adminId);
            if (createdAdmin == null)
                return Conflict("Admin met die ID bestaat al.");
            return Created("", createdAdmin);
        }

        [Route("{adminId}")]
        [HttpGet]
        public ActionResult<AdminDTO> GetAdmin(string adminId)
        {
            var admin = _facade.GetAdmin(adminId);
            if (admin == null)
                return NotFound($"Admin met id:{adminId} bestaat niet.");
            return admin;
        }
    }
}