using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Game.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GameOfLife.Game.Logical.Tests
{
    [TestClass()]
    public class GameLogiqueTest
    {

        private GameManager manager = null;
        private Models.Rule rule = new Models.Rule("3A3S");

        [TestInitialize] 
        public void Init() 
        {
            manager = GameManager.Instance;
            manager.Logical.Area[1, 0].IsAlive = true;
            manager.Logical.Area[1, 1].IsAlive = false;
            manager.Logical.Area[1, 2].IsAlive = true;
            manager.Logical.Area[2, 1].IsAlive = true;
            manager.Logical.Area[2, 2].IsAlive = true;
            manager.Logical.Area[2, 3].IsAlive = true;
            manager.Logical.NumberGeneration = 1;
        }

        [TestMethod()]
        public void TestGiveBirthToACell()
        {
            manager.Logical.SimulateGeneration(rule);
            Assert.IsTrue(manager.Logical.Area[1, 3].IsAlive, "La cellule 1,3 doit devenir vivante");
        }

        [TestMethod()]
        public void TestKillACell()
        {
            manager.Logical.SimulateGeneration(rule);
            Assert.IsFalse(manager.Logical.Area[1, 0].IsAlive, "La cellule 1,0 doit être morte");
        }
    }
}