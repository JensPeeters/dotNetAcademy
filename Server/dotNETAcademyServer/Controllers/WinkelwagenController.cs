using Business_layer.DTO;
using Business_layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinkelwagenController : ControllerBase
    {
        private readonly IWinkelwagenFacade _facade;

        public WinkelwagenController(IWinkelwagenFacade facade)
        {
            this._facade = facade;
        }

        [Route("{userId}/product/{type}/{prodId}/{count}")]
        [HttpPost]
        public WinkelwagenDTO AddProduct(string userId, int prodId, int count, string type)
        {
            return _facade.AddProduct(userId, prodId, count, type);
        }

        [Route("{userId}/product/{prodId}")]
        [HttpDelete]
        public WinkelwagenDTO DeleteProduct(string userId, int prodId)
        {
            return _facade.DeleteProduct(userId, prodId);
        }

        [Route("{userId}/product/{prodId}/{count}")]
        [HttpPut]
        public WinkelwagenDTO UpdateProductAantal(string userId, int prodId, int count)
        {
            return _facade.UpdateProductAantal(userId, prodId, count);
        }

        [Route("{id}")]
        public WinkelwagenDTO GetBagForCustomer(string id)
        {
            return _facade.GetBagForCustomer(id);
        }
    }
}
