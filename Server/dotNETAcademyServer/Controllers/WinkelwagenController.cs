using Business_layer.DTO;
using Business_layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinkelwagenController : ControllerBase
    {
        private readonly IWinkelwagenFacade _winkelwagenFacade;

        public WinkelwagenController(IWinkelwagenFacade winkelwagenFacade)
        {
            _winkelwagenFacade = winkelwagenFacade;
        }

        [Route("{userId}/product/{type}/{prodId}/{count}")]
        [HttpPost]
        public WinkelwagenDTO AddProduct(string userId, int prodId, int count, string type)
        {
            return _winkelwagenFacade.AddProduct(userId, prodId, count, type);
        }

        [Route("{userId}/product/{prodId}")]
        [HttpDelete]
        public WinkelwagenDTO DeleteProduct(string userId, int prodId)
        {
            return _winkelwagenFacade.DeleteProduct(userId, prodId);
        }

        [Route("{userId}/product/{prodId}/{count}")]
        [HttpPut]
        public WinkelwagenDTO UpdateProductAantal(string userId, int prodId, int count)
        {
            return _winkelwagenFacade.UpdateProductAantal(userId, prodId, count);
        }

        [Route("{id}")]
        public ActionResult<WinkelwagenDTO> GetBagForCustomer(string id)
        {
            try
            {
                return _winkelwagenFacade.GetBagForCustomer(id);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
