using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Business_layer.DTO;
using Business_layer.Interfaces;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly IBestellingFacade _bestellingFacade;
        public BestellingController(IBestellingFacade bestellingFacade)
        {
            this._bestellingFacade = bestellingFacade;
        }

        [Route("{custId}")]
        [HttpGet]
        public List<BestellingDTO> GetBestellingenByCustomerId(string custId)
        {
            return _bestellingFacade.GetBestellingenByCustomerId(custId);
        }

        [Route("{custId}")]
        [HttpPost]
        public ActionResult<BestellingDTO> AddBestellingToCustomer(string custId)
        {
            var createdBestelling = _bestellingFacade.AddBestellingToCustomer(custId);
            if (createdBestelling == null)
                return Conflict("Bestelling met die id bestaat al.");
            return Created($"api/bestelling/{createdBestelling.Klant.AzureId}", createdBestelling);
        }
    }
}