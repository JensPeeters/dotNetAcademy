using Business_layer.DTO;
using Data_layer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_layer
{
    public class UserFacade
    {
        private readonly KlantRepository repository;

        public UserFacade(KlantRepository repository)
        {
            this.repository = repository;
        }

        public CursusDTO AddCursus(CursusCreateUpdateDTO cursus)
        {
            var existingCursus = repository.GetCursusByTitel(cursus.Titel);
            if (existingCursus != null)
                return null;
            var newCursus = ConvertCreateUpdateDTOToCursus(cursus);
            var createdCursus = repository.AddCursus(newCursus);
            return ConvertCursusToDTO(createdCursus);
        }

        public UserDTO CreateUser(string userType, string userId)
        {
            if (userType == "klant")
            {
                var klant = repository.GetKlantByID(userId);
                if (klant != null)
                    return null;
                var klant = new KlantDTO()
                {
                    AzureId = userId;
            }
            context.Klanten.Add(klant);
                    context.SaveChanges();
                    return Created("", klant);
                }
                else
                {
                    return NotFound();
                }
            }
            else if (userType == "admin")
            {
                var admin = context.Admins.FirstOrDefault(a => a.AzureId == userId);
                if (admin == null)
                {
                    admin = new Admin()
                    {
                        GebruikersID = userId,
                        RestaurantId = 0
                    };
                    context.Admins.Add(admin);
                    context.SaveChanges();
                    return Created("", admin);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
