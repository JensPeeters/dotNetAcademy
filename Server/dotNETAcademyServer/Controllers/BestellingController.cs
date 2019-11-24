﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business_layer;
using Business_layer.DTO;
using Business_layer.Interfaces;

namespace dotNETAcademyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellingController : ControllerBase
    {
        private readonly IBestellingFacade _facade;
        public BestellingController(IBestellingFacade facade)
        {
            this._facade = facade;
        }

        [Route("{custId}")]
        [HttpGet]
        public List<BestellingDTO> GetBestellingenByCustomerId(string custId)
        {
            return _facade.GetBestellingenByCustomerId(custId);
        }
    }
}