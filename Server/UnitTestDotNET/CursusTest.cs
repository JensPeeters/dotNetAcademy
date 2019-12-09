using Business_layer;
using Business_layer.DTO;
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
    public class CursusTest
    {
        CursusFilter cursusFilter;
        IContextFilter contextFilter;
        [SetUp]
        public void SetUp()
        {
            cursusFilter = new CursusFilter();
            contextFilter = new ContextFilter();
        }

        [DataTestMethod()]
        public void OphalenCursussen()
        {
            // Arrange - We're mocking our dbSet & dbContext
            // in-memory data
            IQueryable<Cursus> cursussen = new List<Cursus>()
            {
                new Cursus()
                {
                    Titel = "dotNET cursus",
                    Type = ".NET",
                    Prijs = 15.45,
                    Categorie = "Cursus",
                    Beschrijving = "Some example text some ...",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                },
                new Cursus()
                {
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"
                }

            }.AsQueryable();
 
            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<ICursusRepository>();
            mockContext.Setup(c => c.GetCursussen(cursusFilter)).Returns(cursussen.ToList());

            // Act
            ICursusFacade facade = new CursusFacade(mockContext.Object);
            var actual = facade.GetCursussen(cursusFilter);

            // Assert
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual("Cursus", actual.First().Categorie);
            Assert.AreEqual("Some example text some ...", actual.First().Beschrijving);
            Assert.AreEqual(15.45, actual.First().Prijs);
        }

        [DataTestMethod()]
        public void CreateCursusTest()
        {
            var cursus = new Cursus()
            {
                Titel = "dotNET cursus 2",
                Type = ".NET",
                Prijs = 18.45,
                Beschrijving = "Some example text some ...",
                Categorie = "Cursus",
                LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"

            };
            // Arrange - We're mocking our dbSet & dbContext
            // in-memory implementations of you context and sets
            var mockSet = new Mock<DbSet<Cursus>>();

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Cursussen).Returns(mockSet.Object);

            var repo = new CursusRepository(mockContext.Object, contextFilter);
            repo.AddCursus(cursus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Cursus>()), Times.Once);
        }
    }
}
