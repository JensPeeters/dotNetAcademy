using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_layer;
using Data_layer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinkelwagenController : ControllerBase
    {
        private readonly WinkelwagenFacade facade;

        public WinkelwagenController(WinkelwagenFacade facade)
        {
            this.facade = facade;
        }

        [Route("{bagId}/product/{type}/{prodId}/{count}")]
        [HttpPost]
        public Winkelwagen AddProduct(int bagId, int prodId, int count, string type)
        {
            return facade.AddProduct(bagId, prodId, count, type);
        }

        [Route("{id}")]
        public Winkelwagen GetBagForCustomer(string id)
        {
            return facade.GetBagForCustomer(id);
        }
    }
}
