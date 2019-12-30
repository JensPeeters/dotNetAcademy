using Data_layer;
using Data_layer.Interfaces;
using Data_layer.Model;
using Data_layer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestDotNET
{
    [TestClass]
    public class BestellingTest
    {
        [DataTestMethod()]
        public void OphalenBestellingenByCustomerIdTest()
        {
            // Arrange - We're mocking our dbSet & dbContext
            // in-memory data
            IQueryable<Bestelling> bestellingen = new List<Bestelling>()
            {
                new Bestelling()
                {
                    Klant = new Klant()
                    {
                        AzureId = "TestUser"
                    },
                    Id = 1,
                    TotaalPrijs = 60,
                    Producten = new List<BestellingItem>()
                    {
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 1,
                            Product = new Cursus()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 15,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        },
                        new BestellingItem()
                        {
                            Aantal = 1,
                            Id = 2,
                            Product = new Traject()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 30,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        }
                    }
                },
                new Bestelling()
                {
                    Klant = new Klant()
                    {
                        AzureId = "TestUser2"
                    },
                    Id = 2,
                    TotaalPrijs = 90,
                    Producten = new List<BestellingItem>()
                    {
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 1,
                            Product = new Cursus()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 15,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        },
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 2,
                            Product = new Traject()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 30,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        }
                    }
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Bestelling>>();
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.Provider).Returns(bestellingen.Provider);
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.Expression).Returns(bestellingen.Expression);
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.ElementType).Returns(bestellingen.ElementType);
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.GetEnumerator()).Returns(bestellingen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Bestellingen).Returns(mockSet.Object);

            var mockRepo = new Mock<IBestellingRepository>();
            mockRepo.Setup(a => a.GetBestellingenByCustomerId("TestUser")).Returns(mockContext.Object.Bestellingen.Where(a => a.Klant.AzureId == "TestUser").ToList());

            var actual = mockRepo.Object.GetBestellingenByCustomerId("TestUser");

            // Assert
            mockRepo.Verify(a => a.GetBestellingenByCustomerId(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual("TestUser", actual.First().Klant.AzureId);
            Assert.AreEqual(2, actual.First().Producten.First().Aantal);
            Assert.AreEqual(60, actual.First().TotaalPrijs);
        }

        [DataTestMethod()]
        public void OphalenBestellingenByIdTest()
        {
            // Arrange - We're mocking our dbSet & dbContext
            // in-memory data
            IQueryable<Bestelling> bestellingen = new List<Bestelling>()
            {
                new Bestelling()
                {
                    Klant = new Klant()
                    {
                        AzureId = "TestUser"
                    },
                    Id = 1,
                    TotaalPrijs = 60,
                    Producten = new List<BestellingItem>()
                    {
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 1,
                            Product = new Cursus()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 15,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        },
                        new BestellingItem()
                        {
                            Aantal = 1,
                            Id = 2,
                            Product = new Traject()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 30,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        }
                    }
                },
                new Bestelling()
                {
                    Klant = new Klant()
                    {
                        AzureId = "TestUser2"
                    },
                    Id = 2,
                    TotaalPrijs = 90,
                    Producten = new List<BestellingItem>()
                    {
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 1,
                            Product = new Cursus()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 15,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        },
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 2,
                            Product = new Traject()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 30,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        }
                    }
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Bestelling>>();
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.Provider).Returns(bestellingen.Provider);
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.Expression).Returns(bestellingen.Expression);
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.ElementType).Returns(bestellingen.ElementType);
            mockSet.As<IQueryable<Bestelling>>().Setup(m => m.GetEnumerator()).Returns(bestellingen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Bestellingen).Returns(mockSet.Object);

            var mockRepo = new Mock<IBestellingRepository>();
            mockRepo.Setup(a => a.GetBestellingById(2)).Returns(mockContext.Object.Bestellingen.Where(a => a.Id == 2).FirstOrDefault());

            var actual = mockRepo.Object.GetBestellingById(2);

            // Assert
            mockRepo.Verify(a => a.GetBestellingById(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(2, actual.Id);
            Assert.AreNotEqual("TestUser", actual.Klant.AzureId);
            Assert.AreEqual("TestUser2", actual.Klant.AzureId);
            Assert.AreEqual(2, actual.Producten.First().Aantal);
            Assert.AreEqual(90, actual.TotaalPrijs);
        }

        [DataTestMethod()]
        public void CreateBestellingTest()
        {
            var bestelling = new Bestelling()
            {
                Klant = new Klant()
                {
                    AzureId = "TestUser"
                },
                Id = 1,
                TotaalPrijs = 60,
                Producten = new List<BestellingItem>()
                    {
                        new BestellingItem()
                        {
                            Aantal = 2,
                            Id = 1,
                            Product = new Cursus()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 15,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        },
                        new BestellingItem()
                        {
                            Aantal = 1,
                            Id = 2,
                            Product = new Traject()
                            {
                                Titel = "dotNET cursus",
                                Type = ".NET",
                                Prijs = 30,
                                Categorie = "Cursus",
                                IsBuyable = true,
                                Beschrijving = "Some example text some ...",
                                LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                            }
                        }
                    }

            };

            var mockSet = new Mock<DbSet<Bestelling>>();

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Bestellingen).Returns(mockSet.Object);

            var repo = new BestellingRepository(mockContext.Object);
            var createdBestelling = repo.AddBestellingToCustomer(bestelling);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Bestelling>()), Times.Once);
            Assert.AreEqual(bestelling.TotaalPrijs, createdBestelling.TotaalPrijs);
            Assert.IsNotNull(createdBestelling);
            Assert.AreEqual(bestelling.Producten.Count(), createdBestelling.Producten.Count());
            Assert.AreEqual(bestelling.Producten, createdBestelling.Producten);
            Assert.AreEqual(bestelling.Klant.AzureId, createdBestelling.Klant.AzureId);

        }

    }
}
