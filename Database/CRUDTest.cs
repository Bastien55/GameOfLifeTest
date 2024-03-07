using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketBackend.Repository;
using SocketBackend.Models;

namespace GameOfLifeTests.Database
{
    [TestClass()]
    public class CRUDTest
    {
        public UserRepository Repository { get; set; }

        [TestInitialize]
        public void Init()
        {
            Repository = new UserRepository(new SocketBackend.Context.GameOfLifeContext());
        }

        [TestMethod()]
        public void AddUserInDB()
        {
            User user = new User() 
            { 
                Name = "Test",
                Rule = "3A3M"
            };

            Repository.Insert(user);

            Assert.AreEqual(3, Repository.GetAll().Count);
        }

        [TestMethod()]
        public void UpdateTestDB()
        {
            var usr = Repository.FindByName("leo");
            usr.Rule = "5A2M";

            Repository.Update(usr);

            Assert.AreEqual("5A2M", Repository.FindByName("leo").Rule);
        }

        [TestMethod()]
        public void DeleteTestDB()
        {
            var usr = Repository.FindByID(2);
            Repository.Delete(usr);

            Assert.AreEqual(2, Repository.GetAll().Count);
        }
    }
}