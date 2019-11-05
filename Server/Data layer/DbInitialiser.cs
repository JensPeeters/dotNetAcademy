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
                        Type = ".NET",
                        Prijs = 15.45,
                        Beschrijving = "Some example text some ...",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    },
                    new Cursus()
                    {
                        Titel = "dotNET cursus2",
                        Type = ".NET",
                        Prijs = 45.45,
                        Beschrijving = "Some example text some ...",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    },
                    new Cursus()
                    {
                        Titel = "React cursus",
                        Type = "Web Development",
                        Prijs = 19.35,
                        Beschrijving = "Some example text some ...",
                        FotoURLCard = "https://ionicframework.com/blog/wp-content/uploads/2019/02/react-beta.png"
                    },
                    new Cursus()
                    {
                        Titel = "Angular cursus",
                        Type = "Web Development",
                        Prijs = 21.45,
                        Beschrijving = "Some example text some ...",
                        FotoURLCard = "https://miro.medium.com/max/750/1*8hD4eYuELoWAbQLNnjQ4mA.jpeg"
                    },
                    new Cursus()
                    {
                        Titel = "ReactJS cursus",
                        Type = "Web Development",
                        Prijs = 36.50,
                        Beschrijving = "Some example text some ...",
                        FotoURLCard = "https://ionicframework.com/blog/wp-content/uploads/2019/02/react-beta.png"
                    },
                    new Cursus()
                    {
                        Titel = "Vue cursus",
                        Type = "Web Development",
                        Prijs = 10.25,
                        Beschrijving = "Some example text some ...",
                        FotoURLCard = "https://miro.medium.com/max/1200/1*-PlqbnwqjqJi_EVmrhmuDQ.jpeg"
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

                Klant[] klanten =
                {
                    new Klant()
                    {
                        AzureId = "aaa"
                    }
                };

                Winkelwagen[] winkelwagens =
                {
                    new Winkelwagen()
                    {
                        Klant = klanten[0],
                        Producten = new List<WinkelwagenItem>()
                        {
                            new WinkelwagenItem()
                            {
                                Aantal = 2,
                                Product = trajecten[0]
                            },
                            new WinkelwagenItem()
                            {
                                Aantal = 3,
                                Product = cursussen[0]
                            },
                            new WinkelwagenItem()
                            {
                                Aantal = 1,
                                Product = cursussen[1]
                            }
                        },
                        Datum = new DateTime()
                    }
                };
                foreach (Winkelwagen winkelwagen in winkelwagens)
                {
                    context.Winkelwagens.Add(winkelwagen);
                }
                foreach (Klant klant in klanten)
                {
                    context.Klanten.Add(klant);
                }
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
