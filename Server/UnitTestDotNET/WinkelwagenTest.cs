using Business_layer;
using Data_layer;
using Data_layer.Model;
using Data_layer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestDotNET
{
    [TestClass]
    public class WinkelwagenTest
    {
        [DataTestMethod()]
        [DataRow(5, 1, 5)]
        [DataRow(23, 1, 23)]
        [DataRow(345, 1, 345)]
        [DataRow(5, 2, 10)]
        [DataRow(15, 2, 30)]
        [DataRow(5, 3, 15)]
        [DataRow(50, 3, 150)]
        [DataRow(50, 4, 200)]
        [DataRow(50, 5, 250)]
        public void TestBagOneProduct(double price, int quantity, double expectedTotal)
        {
            var bag = new Winkelwagen()
            {
                Producten = new List<WinkelwagenItem>()
            };
            bag.Producten.Add(
            new WinkelwagenItem()
            {
                Product = new Cursus()
                {
                    Titel = "dotNET",
                    Prijs = price
                },
                Aantal = quantity
            });

            var calculator = new CostCalculator();
            var cost = calculator.CalculateCost(bag);
            Assert.AreEqual(expectedTotal, cost);
        }
        

        [DataTestMethod()]
        [DataRow(5, 1, 1, 5)]
        [DataRow(23, 1, 2, 46)]
        [DataRow(345, 1, 2, 690)]
        [DataRow(5, 2, 2, 20)]
        [DataRow(15, 2, 2, 60)]
        [DataRow(5, 3, 2, 30)]
        [DataRow(50, 3, 3, 450)]
        [DataRow(50, 4, 3, 600)]
        [DataRow(50, 5, 6, 1500)]
        public void TestBagMultiProduct(double price, int quantity, int products, double expectedTotal)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseSqlite("DataSource=:memory:")
             .Options;

            // Run the test against one instance of the context
            using (var context = new DatabaseContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Klanten.Add(new Klant()
                {
                    AzureId = "TestUser",
                });

                for (int f = 1; f <= products; f++)
                {
                    context.Cursussen.Add(new Cursus()
                    {
                        ID = f,
                        Titel = "dotNET"+f,
                        Prijs = price
                    });
                }
                context.SaveChanges();

                var facade = new WinkelwagenFacade(new CostCalculator(), new WinkelwagenRepository(context));
                var winkelwagen = facade.GetBagForCustomer("TestUser");

                for (int f = 1; f <= products; f++)
                {
                    winkelwagen = facade.AddProduct("TestUser",f,quantity,"Cursus");
                }
                Assert.AreEqual(expectedTotal, winkelwagen.TotaalPrijs);

            }
        }

        /// <summary>
        /// Check if the same product is added several times, that is only appears once in the bag
        /// (with the correct quantity ofcourse)
        /// this is a check of a business rule that says that each specific product can only appear once
        /// in the shoppingbag
        /// </summary>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="expectedTotal"></param>
        [DataTestMethod()]
        [DataRow(5, 1)]
        [DataRow(23, 1)]
        [DataRow(345, 1)]
        [DataRow(5, 2)]
        [DataRow(15, 2)]
        [DataRow(5, 3)]
        [DataRow(50, 3)]
        [DataRow(50, 4)]
        [DataRow(50, 5)]
        public void TestBagOneProduct2TimesCheckIfSingleItem(double price, int quantity)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseSqlite("DataSource=:memory:")
             .Options;

            // Run the test against one instance of the context
            using (var context = new DatabaseContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Klanten.Add(new Klant()
                {
                    AzureId = "TestUser"
                });

                context.Cursussen.Add(new Cursus()
                {
                    ID = 1,
                    Titel = "C#",
                    Prijs = price
                });

                context.SaveChanges();

                var facade = new WinkelwagenFacade(new CostCalculator(), new WinkelwagenRepository(context));
                var winkelwagen = facade.GetBagForCustomer("TestUser");

                //Add the same product twice
                winkelwagen = facade.AddProduct("TestUser", 1, quantity, "Cursus");
                winkelwagen = facade.AddProduct("TestUser", 1, quantity, "Cursus");

                //Now we should still have only 1 item (= 1 product)
                Assert.AreEqual(1, winkelwagen.Producten.Count);

            }
        }

        [DataTestMethod()]
        [DataRow(5, 1, 1)]
        [DataRow(23, 1, 2)]
        [DataRow(345, 1, 2)]
        [DataRow(5, 2, 2)]
        [DataRow(15, 2, 2)]
        [DataRow(5, 3, 2)]
        [DataRow(50, 3, 3)]
        [DataRow(50, 4, 3)]
        [DataRow(50, 5, 6)]
        public void TestBagMultiProductCheckIfCorrectnumberOfItems(double price, int quantity, int products)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseSqlite("DataSource=:memory:")
             .Options;

            // Run the test against one instance of the context
            using (var context = new DatabaseContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Klanten.Add(new Klant()
                {
                    AzureId = "TestUser"
                });

                for (int f = 1; f <= products; f++)
                {
                    context.Cursussen.Add(new Cursus()
                    {
                        Titel = "C#"+f,
                        Prijs = price
                    });
                }
                context.SaveChanges();

                var facade = new WinkelwagenFacade(new CostCalculator(), new WinkelwagenRepository(context));
                var winkelwagen = facade.GetBagForCustomer("TestUser");

                //Add each product 3 times 
                for (int f = 1; f <= products; f++)
                {
                    winkelwagen = facade.AddProduct("TestUser", f, quantity, "Cursus");
                }
                for (int f = 1; f <= products; f++)
                {
                    winkelwagen = facade.AddProduct("TestUser", f, quantity, "Cursus");
                }
                for (int f = 1; f <= products; f++)
                {
                    winkelwagen = facade.AddProduct("TestUser", f, quantity, "Cursus");
                }

                //Check if there are still no more than number of items equal to the number of different products
                Assert.AreEqual(products, winkelwagen.Producten.Count);

            }
        }
    }
}
