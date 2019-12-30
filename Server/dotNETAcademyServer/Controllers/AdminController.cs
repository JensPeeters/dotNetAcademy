﻿using Business_layer.DTO;
using Business_layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminFacade _adminFacade;
        public AdminController(IAdminFacade adminFacade)
        {
            _adminFacade = adminFacade;
        }

        [Route("{adminId}")]
        [HttpPost]
        public ActionResult<AdminDTO> CreateAdmin(string adminId)
        {
            var createdAdmin = _adminFacade.CreateAdmin(adminId);
            if (createdAdmin == null)
                return Conflict("Admin met die ID bestaat al.");
            return Created("", createdAdmin);
        }

        [Route("{adminId}")]
        [HttpDelete]
        public ActionResult DeleteAdmin(string adminId)
        {
            var deletedAdmin = _adminFacade.DeleteAdmin(adminId);
            if (deletedAdmin == null)
                return NotFound("Admin bestaat niet.");
            return Ok("Admin succesvol verwijderd.");
        }

        [Route("{adminId}")]
        [HttpGet]
        public ActionResult<AdminDTO> GetAdmin(string adminId)
        {
            var admin = _adminFacade.GetAdmin(adminId);
            if (admin == null)
                return NotFound($"Admin met id:{adminId} bestaat niet.");
            return admin;
        }
    }
}