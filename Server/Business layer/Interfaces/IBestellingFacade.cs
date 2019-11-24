using Business_layer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer.Interfaces
{
    public interface IBestellingFacade
    {
        List<BestellingDTO> GetBestellingenByCustomerId(string custId);
    }
}
