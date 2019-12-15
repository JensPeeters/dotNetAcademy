using Business_layer.DTO;
using Business_layer.Interfaces;
using Data_layer.Interfaces;
using Data_layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
namespace Business_layer
{
    public class KlantFacade : IKlantFacade
    {
        private readonly IKlantRepository _repositoryKlant;

        public KlantFacade(IKlantRepository repositoryKlant)
        {
            _repositoryKlant = repositoryKlant;
        }

        private static Klant ConvertCreateUpdateDTOToKlant(string klantId)
        {
            return new Klant()
            {
                AzureId = klantId
            };
        }
        private static KlantDTO ConvertKlantToDTO(Klant klant)
        {
            return new KlantDTO()
            {
                AzureId = klant.AzureId,
                Bestellingen = klant.Bestellingen,
                Winkelwagens = klant.Winkelwagens
            };
        }

        public KlantDTO CreateKlant(string klantId)
        {
            var klant = _repositoryKlant.GetKlantByID(klantId);
            if (klant != null)
                return null;
            var newKlant = ConvertCreateUpdateDTOToKlant(klantId);
            var createdKlant = _repositoryKlant.CreateKlant(newKlant);
            try
            {
                _repositoryKlant.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertKlantToDTO(createdKlant);
        }

        public KlantDTO DeleteKlant(string klantId)
        {
            var deletedKlant = _repositoryKlant.DeleteKlant(klantId);
            if (deletedKlant == null)
                return null;
            try
            {
                _repositoryKlant.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ConvertKlantToDTO(deletedKlant);
        }

        public KlantDTO GetKlant(string klantId)
        {
            var klant = _repositoryKlant.GetKlantByID(klantId);
            if (klant == null)
                return null;
            return ConvertKlantToDTO(klant);
        }
    }
}
