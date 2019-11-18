using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserFacade facade;
        public UserController(UserFacade facade)
        {
            this.facade = facade;
        }

        [Route("{userType}/{userId}")]
        [HttpPost]
        public ActionResult<UserDTO> CreateUser(string userType, string userId)
        {
            return facade.CreateUser(userType, userId);
        }

        [Route("isklant/{userId}")]
        [HttpGet]
        public ActionResult<Klant> IsKlant(string userId)
        {
            var klant = context.Klanten.Where(a => a.AzureId == userId)
                                              .FirstOrDefault();
            if (klant != null)
            {
                return klant;
            }
            return NotFound();
        }

        [Route("isadmin/{userId}")]
        [HttpGet]
        public ActionResult<Admin> IsAdmin(string userId)
        {
            var admin = context.Admins.Where(a => a.AzureId == userId)
                                            .FirstOrDefault();
            if (admin != null)
            {
                return admin;
            }
            return NotFound();
        }
    }
}