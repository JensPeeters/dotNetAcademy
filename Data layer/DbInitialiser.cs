using Data_layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_layer
{
    public class DbInitialiser
    {
        public static void Initialize(DatabaseContext context)
        {
            //Create the db if not yet exists
            context.Database.EnsureCreated();

            if (context.Trajecten.Count() == 0)
            {
                Cursus[] cursussen =
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        LangeBeschrijving ="",
                        Type = ".NET",
                        Prijs = 15.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                    new Cursus()
                    {
                        Titel = "Angular cursus",
                        Type = "Web Development",
                        LangeBeschrijving ="",
                        Prijs = 21.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    }
                };
                Traject[] trajecten =
                {
                    new Traject()
                    {
                        Cursussen = new List<Cursus>()
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        LangeBeschrijving ="",
                        Type = ".NET",
                        Prijs = 15.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                    new Cursus()
                    {
                        Titel = "Angular cursus",
                        Type = "Web Development",
                        LangeBeschrijving ="",
                        Prijs = 21.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    }
                },
                        Titel = "Complete Full Stack Traject",
                        Type = "Full Stack",
                        LangeBeschrijving ="",
                        Prijs = 50,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                     new Traject()
                    {
                        Cursussen = new List<Cursus>()
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        LangeBeschrijving ="",
                        Type = ".NET",
                        Prijs = 15.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                    new Cursus()
                    {
                        Titel = "Angular cursus",
                        Type = "Web Development",
                        LangeBeschrijving ="",
                        Prijs = 21.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    }
                },
                        Titel = "Complete Full Stack Traject",
                        Type = "Full Stack",
                        LangeBeschrijving ="",
                        Prijs = 50,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                      new Traject()
                    {
                        Cursussen = new List<Cursus>()
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        LangeBeschrijving ="",
                        Type = ".NET",
                        Prijs = 15.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    },
                    new Cursus()
                    {
                        Titel = "Angular cursus",
                        Type = "Web Development",
                        LangeBeschrijving ="",
                        Prijs = 21.45,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
                    }
                },
                        Titel = "Complete Full Stack Traject",
                        Type = "Full Stack",
                        LangeBeschrijving ="",
                        Prijs = 50,
                        Beschrijving = "Some example text some example text.",
                        FotoURLCard = "https://via.placeholder.com/450x350.png/09f/fff?text=Foto van een product"
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
