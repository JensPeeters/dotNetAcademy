using Business_layer;
using Business_layer.Interfaces;
using Data_layer;
using Data_layer.Filter;
using Data_layer.Filter.ProductenFilters;
using Data_layer.Interfaces;
using Data_layer.Model;
using Data_layer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTestDotNET
{
    [TestClass]
    public class TrajectTest
    {
        [TestClass]
        public class CursusTest
        {
            TrajectFilter trajectFilter;

            IContextFilter contextFilter;
            [SetUp]
            public void SetUp()
            {
                trajectFilter = new TrajectFilter();

                contextFilter = new ContextFilter();
            }

            [DataTestMethod()]
            public void OphalenCursussen()
            {
                // Arrange - We're mocking our dbSet & dbContext
                // in-memory data

                List<Cursus> cursussen = new List<Cursus>()
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        Type = ".NET",
                        Prijs = 15.45,
                        Categorie = "Cursus",
                         IsBuyable = true,
                        Beschrijving = "Some example text some ...",
                        LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    },
                    new Cursus()
                    {
                        Titel = "dotNET cursus 2",
                        Type = ".NET",
                        Prijs = 18.45,
                         IsBuyable = true,
                        Beschrijving = "Some example text some ...",
                        Categorie = "Cursus",
                        LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    }
                };
                IQueryable<Traject> trajecten = new List<Traject>()
                {
                    new Traject()
                    {
                        Titel = "dotNET cursus",
                        Type = ".NET",
                        Prijs = 45.45,
                        Categorie = "Traject",
                         IsBuyable = true,
                        Cursussen = cursussen,
                        Beschrijving = "Some example text some ...",
                        LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    },
                    new Traject()
                    {
                        Titel = "dotNET cursus 2",
                        Type = ".NET",
                        Prijs = 48.45,
                         IsBuyable = true,
                        Cursussen = cursussen,
                        Beschrijving = "Some example text some ...",
                        Categorie = "Cursus",
                        LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    }

                }.AsQueryable();

                var mockSet = new Mock<DbSet<Traject>>();
                mockSet.As<IQueryable<Traject>>().Setup(m => m.Provider).Returns(trajecten.Provider);
                mockSet.As<IQueryable<Traject>>().Setup(m => m.Expression).Returns(trajecten.Expression);
                mockSet.As<IQueryable<Traject>>().Setup(m => m.ElementType).Returns(trajecten.ElementType);
                mockSet.As<IQueryable<Traject>>().Setup(m => m.GetEnumerator()).Returns(trajecten.GetEnumerator());

                var mockContext = new Mock<ITrajectRepository>();
                mockContext.Setup(c => c.GetTrajecten(trajectFilter)).Returns(trajecten.ToList());

                // Act
                ITrajectFacade facade = new TrajectFacade(mockContext.Object);
                var actual = facade.GetTrajecten(trajectFilter);

                // Assert
                Assert.AreEqual(2, actual.Count());
                Assert.AreEqual("Traject", actual.First().Categorie);
                Assert.AreEqual(".NET", actual.First().Type);
                Assert.AreEqual(45.45, actual.First().Prijs);
            }


            [DataTestMethod()]
            public void CreateTrajectTest()
            {
                IQueryable<Cursus> cursussen = new List<Cursus>()
                {
                    new Cursus()
                    {
                        Titel = "dotNET cursus",
                        Type = ".NET",
                        Prijs = 15.45,
                        Categorie = "Cursus",
                        IsBuyable = true,
                        Beschrijving = "Some example text some ...",
                        LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    },
                    new Cursus()
                    {
                        Titel = "dotNET cursus 2",
                        Type = ".NET",
                        Prijs = 18.45,
                         IsBuyable = true,
                        Beschrijving = "Some example text some ...",
                        Categorie = "Cursus",
                        LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                    }
                }.AsQueryable();
                var traject = new Traject()
                {
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                    Cursussen = cursussen.ToList(),
                    IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"

                };
                // Arrange - We're mocking our dbSet & dbContext
                // in-memory implementations of you context and sets
                var mockSet = new Mock<DbSet<Traject>>();
                var mockSetCursus = new Mock<DbSet<Cursus>>();
                mockSetCursus.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
                mockSetCursus.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
                mockSetCursus.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
                mockSetCursus.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());


                var mockContext = new Mock<DatabaseContext>();
                mockContext.Setup(m => m.Trajecten).Returns(mockSet.Object);
                mockContext.Setup(m => m.Cursussen).Returns(mockSetCursus.Object);

                var repo = new TrajectRepository(mockContext.Object, contextFilter);
                repo.AddTraject(traject);

                // Assert
                mockSet.Verify(m => m.Add(It.IsAny<Traject>()), Times.Once);
            }
        }
    }
}
