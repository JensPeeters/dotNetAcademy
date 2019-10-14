using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNETAcademyServer.Model
{
    public class DbInitialiser
    {
        public static void Initialize(DatabaseContext context)
        {
            //Create the db if not yet exists
            context.Database.EnsureCreated();

            //Are there already books present ?
            if (context.Cursussen.Count() == 0)
            {
                Cursus[] cursussen =
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        Type = ".NET",
                        Prijs = 15.45,
                        Beschrijving = "Some example text some example text. John Doe is an architect and engineer. John Doe is an architect and engineer."
                        ImageCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                    new Cursus()
                    {
                        Titel = "Angular cursus",
                        Type = "Web Development",
                        Prijs = 21.45,
                        Beschrijving = "Some example text some example text. John Doe is an architect and engineer. John Doe is an architect and engineer."
                        ImageCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    }
                };
                Traject[] trajecten =
                {
                    new Traject()
                    {
                        Cursussen = cursussen,
                        Titel = "Complete Full Stack Traject",
                        Type = "Full Stack",
                        Beschrijving = "Some example text some example text. John Doe is an architect and engineer. John Doe is an architect and engineer.",
                        FotoCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    }
                };
                foreach (Cursus cursus in cursussen)
                {
                    context.Cursussen.Add(cursus);
                }
                foreach (Traject traject in trajecten)
                {
                    context.Trajecten.Add(traject);
                }
                context.SaveChanges();
            }
        }
    }
}
