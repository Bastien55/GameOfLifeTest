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

            var usr = Repository.GetAll().FirstOrDefault(i =>
            {
                if (i.Name != null)
                    return i.Name.Equals(user.Name);
                return false;
            });

            if(usr == null)
                Repository.Insert(user);

            Assert.AreEqual(7, Repository.GetAll().Count);
        }

        [TestMethod()]
        public void UpdateTestDB()
        {
            var username = "Yel";
            var usr = Repository.FindByName(username);
            usr.Rule = "5A2M";

            Repository.Update(usr);

            Assert.AreEqual("5A2M", Repository.FindByName(username).Rule);
        }

        [TestMethod()]
        public void DeleteTestDB()
        {
            var usr = Repository.FindByName("Test");
            Repository.Delete(usr);

            Assert.AreEqual(6, Repository.GetAll().Count);
        }
    }
}