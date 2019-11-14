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
    public class WinkelwagenController : ControllerBase
    {
        private readonly WinkelwagenFacade facade;

        public WinkelwagenController(WinkelwagenFacade facade)
        {
            this.facade = facade;
        }

        [Route("{userId}/product/{type}/{prodId}/{count}")]
        [HttpPost]
        public WinkelwagenDTO AddProduct(string userId, int prodId, int count, string type)
        {
            return facade.AddProduct(userId, prodId, count, type);
        }

        [Route("{userId}/product/{prodId}")]
        [HttpDelete]
        public WinkelwagenDTO DeleteProduct(string userId, int prodId)
        {
            return facade.DeleteProduct(userId, prodId);
        }

        [Route("{userId}/product/{prodId}/{count}")]
        [HttpPut]
        public WinkelwagenDTO UpdateProductAantal(string userId, int prodId, int count)
        {
            return facade.UpdateProductAantal(userId, prodId, count);
        }

        [Route("{id}")]
        public WinkelwagenDTO GetBagForCustomer(string id)
        {
            return facade.GetBagForCustomer(id);
        }
    }
}
