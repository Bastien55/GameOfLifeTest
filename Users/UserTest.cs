using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLife.Service;
using GameOfLife.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketBackend.Enumeration;
using SocketBackend.Models;
using SocketBackend.Repository;

namespace GameOfLifeTests.Users
{
    [TestClass]
    public class UserTest
    {
        private UserRepository Repository { get; set; }

        [TestInitialize]
        public void Init()
        {
            Task.Run(async () => await SocketService.Instance.Client.ConnectAsync()).Wait(10);
            Repository = new UserRepository(new SocketBackend.Context.GameOfLifeContext());
        }

        [TestMethod]
        public void InscriptionUserTest()
        {
            UserSession.Instance.CurrentUser = new User()
            {
                Name = "InscriptionUser",
                Rule = "1A3S"
            };

            ViewModelLocator.Instance.UserVM.ConnectionCommand.Execute(TypeMessage.NEW_USER);

            Thread.Sleep(1000);
            var user = Repository.FindByName(UserSession.Instance.CurrentUser.Name);

            Assert.IsNotNull(user, "Utilisateur Inexistant");
        }

        [TestMethod]
        public void MajNameAndRuleTest()
        {
            Assert.IsNotNull(UserSession.Instance.CurrentUser, "Attention current user est null");

            UserSession.Instance.IsLoggedIn = true;
            var oldRule = UserSession.Instance.CurrentUser.Rule;

            ViewModelLocator.Instance.UserVM.Name = "UpdateUser";
            ViewModelLocator.Instance.UserVM.Rule = "5A5S";
            var newUser = Repository.FindByName("UpdateUser");

            Thread.Sleep(1000);

            Assert.IsNotNull(newUser);
            Assert.AreNotEqual(oldRule, newUser.Rule, "Problème avec la mise à jour");
        }
    }
}
