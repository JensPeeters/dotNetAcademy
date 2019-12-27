using Business_layer;
using Business_layer.DTO;
using Business_layer.Interfaces;
using Business_layer.Interfaces.Mapping;
using Business_layer.Mapping;
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
        ICursusMapper cursusMapper;
        [SetUp]
        public void SetUp()
        {
            cursusFilter = new CursusFilter();
            cursusMapper = new CursusMapper();
            contextFilter = new ContextFilter();
        }

        [DataTestMethod()]
        public void OphalenCursussenTest()
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
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                },
                new Cursus()
                {
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 2
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Cursussen).Returns(mockSet.Object);

            var mockRepo = new Mock<ICursusRepository>();
            mockRepo.Setup(a => a.GetCursussen(cursusFilter)).Returns(mockContext.Object.Cursussen.ToList());

            var actual = mockRepo.Object.GetCursussen(cursusFilter);

            // Assert
            mockRepo.Verify(a => a.GetCursussen(It.IsAny<CursusFilter>()), Times.Once);
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual("Cursus", actual.First().Categorie);
            Assert.AreEqual("Some example text some ...", actual.First().Beschrijving);
            Assert.AreEqual(15.45, actual.First().Prijs);
        }

        [DataTestMethod()]
        public void OphalenBuyableCursussenTest()
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
                     IsBuyable = false,
                    Beschrijving = "Some example text some ...",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                },
                new Cursus()
                {
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 2
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Cursussen).Returns(mockSet.Object);

            var mockRepo = new Mock<ICursusRepository>();
            mockRepo.Setup(a => a.GetBuyableCursussen(cursusFilter)).Returns(mockContext.Object.Cursussen.Where(a => a.IsBuyable == true).ToList());

            var actual = mockRepo.Object.GetBuyableCursussen(cursusFilter);

            // Assert
            mockRepo.Verify(a => a.GetBuyableCursussen(It.IsAny<CursusFilter>()), Times.Once);
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Count(), 1);
            Assert.IsTrue(actual.First().IsBuyable);
            Assert.AreEqual(18.45, actual.First().Prijs);
            Assert.AreEqual("Cursus", actual.First().Categorie);
            Assert.AreEqual(".NET", actual.First().Type);
        }

        [DataTestMethod()]
        public void OphalenCursusByIdTest()
        {
            // Arrange - We're mocking our dbSet & dbContext
            // in-memory data
            IQueryable<Cursus> cursussen = new List<Cursus>()
            {
                new Cursus()
                {
                    ID = 1,
                    Titel = "PHP cursus",
                    Type = "PHP",
                    Prijs = 15.45,
                    Categorie = "Cursus",
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                },
                new Cursus()
                {
                    ID = 2,
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 2
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Cursussen).Returns(mockSet.Object);

            var mockRepo = new Mock<ICursusRepository>();
            mockRepo.Setup(a => a.GetCursusById(It.IsAny<int>())).Returns(mockContext.Object.Cursussen.Where(a => a.ID == 2).FirstOrDefault());

            var actual = mockRepo.Object.GetCursusById(2);

            // Assert
            mockRepo.Verify(a => a.GetCursusById(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(18.45, actual.Prijs);
            Assert.AreEqual("Cursus", actual.Categorie);
            Assert.AreEqual(".NET", actual.Type);
        }

        [DataTestMethod()]
        public void OphalenCursusByTitelTest()
        {
            // Arrange - We're mocking our dbSet & dbContext
            // in-memory data
            IQueryable<Cursus> cursussen = new List<Cursus>()
            {
                new Cursus()
                {
                    ID = 1,
                    Titel = "PHP cursus",
                    Type = "PHP",
                    Prijs = 15.45,
                    Categorie = "Cursus",
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                },
                new Cursus()
                {
                    ID = 2,
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 2
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Cursussen).Returns(mockSet.Object);

            var mockRepo = new Mock<ICursusRepository>();
            mockRepo.Setup(a => a.GetCursusByTitel(It.IsAny<string>())).Returns(mockContext.Object.Cursussen.Where(a => a.Titel == "dotNET cursus 2").FirstOrDefault());

            var actual = mockRepo.Object.GetCursusByTitel("dotNET cursus 2");

            // Assert
            mockRepo.Verify(a => a.GetCursusByTitel(It.IsAny<string>()), Times.Once);
            Assert.AreEqual(18.45, actual.Prijs);
            Assert.AreEqual("Cursus", actual.Categorie);
            Assert.AreEqual(".NET", actual.Type);
            Assert.AreEqual("dotNET cursus 2", actual.Titel);
        }

        [DataTestMethod()]
        public void CreateCursusTest()
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
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                },
                new Cursus()
                {
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                     IsBuyable = true,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 2
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var cursus = new Cursus()
            {
                Titel = "dotNET cursus 2",
                Type = ".NET",
                Prijs = 18.45,
                IsBuyable = true,
                Beschrijving = "Some example text some ...",
                Categorie = "Cursus",
                LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg"

            };

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Cursussen).Returns(mockSet.Object);

            var repo = new CursusRepository(mockContext.Object, contextFilter);
            repo.AddCursus(cursus);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Cursus>()), Times.Once);
        }

        [DataTestMethod()]
        public void DeleteCursusTest()
        {
            IQueryable<Cursus> cursussen = new List<Cursus>()
            {
                new Cursus()
                {
                    ID = 1,
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                    IsBuyable = false,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Cursussen).Returns(mockSet.Object);

            var mockRepo = new Mock<ICursusRepository>();
            mockRepo.Setup(m => m.DeleteCursus(1)).Returns(cursussen.First());
            var deletedCursus = mockRepo.Object.DeleteCursus(1);

            mockRepo.Verify(m => m.DeleteCursus(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(deletedCursus.IsBuyable, false);
            // Assert
        }
        [DataTestMethod()]
        public void UpdateCursusTest()
        {
            IQueryable<Cursus> cursussen = new List<Cursus>()
            {
                new Cursus()
                {
                    ID = 1,
                    Titel = "dotNET cursus 2",
                    Type = ".NET",
                    Prijs = 18.45,
                    IsBuyable = false,
                    Beschrijving = "Some example text some ...",
                    Categorie = "Cursus",
                    LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                    OrderNumber = 1
                }
            }.AsQueryable();

            var cursus = new Cursus()
            {
                ID = 1,
                Titel = "dotNET cursus 2",
                Type = ".NET",
                Prijs = 12,
                IsBuyable = false,
                Beschrijving = "Some example text some ...",
                Categorie = "Cursus",
                LangeBeschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                FotoURLCard = "https://52bec9fb483231ac1c712343-jebebgcvzf.stackpathdns.com/wp-content/uploads/2016/05/dotnet.jpg",
                OrderNumber = 2
            };

            var mockSet = new Mock<DbSet<Cursus>>();
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Provider).Returns(cursussen.Provider);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.Expression).Returns(cursussen.Expression);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.ElementType).Returns(cursussen.ElementType);
            mockSet.As<IQueryable<Cursus>>().Setup(m => m.GetEnumerator()).Returns(cursussen.GetEnumerator());

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Cursussen).Returns(mockSet.Object);

            var mockRepo = new Mock<ICursusRepository>();
            mockRepo.Setup(m => m.UpdateCursus(It.IsAny<Cursus>())).Returns(cursus);
            var updatedCursus = mockRepo.Object.UpdateCursus(cursus);
            // Assert
            mockRepo.Verify(m => m.UpdateCursus(It.IsAny<Cursus>()), Times.Once);
            Assert.AreEqual(updatedCursus.Prijs, 12);
        }
    }
}
