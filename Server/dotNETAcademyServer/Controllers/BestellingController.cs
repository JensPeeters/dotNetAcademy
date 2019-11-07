using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business_layer;
using Data_layer.Model;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly BestellingFacade facade;
        public BestellingController(BestellingFacade facade)
        {
            this.facade = facade;
        }

        [Route("{custId}")]
        [HttpGet]
        public List<Bestelling> GetBestellingen(string custId)
        {
            return facade.GetBestellingen(custId);
        }
    }
}