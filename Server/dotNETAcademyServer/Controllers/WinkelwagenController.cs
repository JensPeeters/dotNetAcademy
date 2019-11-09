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

        [Route("{userId}/product/{type}/{prodId}/{count}")]
        [HttpPost]
        public Winkelwagen AddProduct(string userId, int prodId, int count, string type)
        {
            return facade.AddProduct(userId, prodId, count, type);
        }

        [Route("{userId}/product/{prodId}")]
        [HttpDelete]
        public Winkelwagen DeleteProduct(string userId, int prodId)
        {
            return facade.DeleteProduct(userId, prodId);
        }

        [Route("{id}")]
        public Winkelwagen GetBagForCustomer(string id)
        {
            return facade.GetBagForCustomer(id);
        }
    }
}
