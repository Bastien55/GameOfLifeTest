using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife.Game.Logical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Game.Logical.Tests
{
    [TestClass()]
    public class CRUDTest
    {

        private GameManager manager = null;

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
        }

        [TestMethod()]
        public void TestGiveBirthToACell()
        {
            manager.Logical.SimulateGeneration();
            Assert.IsTrue(manager.Logical.Area[1, 3].IsAlive, "La cellule 1,3 doit devenir vivante");
        }

        [TestMethod()]
        public void TestKillACell()
        {
            manager.Logical.SimulateGeneration();
            Assert.IsFalse(manager.Logical.Area[1, 0].IsAlive, "La cellule 1,0 doit être morte");
        }
    }
}