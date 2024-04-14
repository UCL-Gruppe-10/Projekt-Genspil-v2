using Projekt_Genspil_v._2;

namespace ProjektGenspilTest
{
    [TestClass]
    public class UnitTest1
    {
        Game g1, g2, g3;

        [TestInitialize]
        public void Init()
        {
            g1 = new Game("Risk", "Classic", "Strategi", 2, 6, "a", 300, "Reserveret");
            g2 = new Game("Cluedo", "Den bedste version", "familie", 2, 5);
            g3 = new Game("Kalaha", "Familie", 1, 2);
        }

        [TestMethod]
        public void GameConstructorWithCopy()
        {
            Assert.AreEqual("Spil: Risk -- Genre: Strategi -- Spillere: 2 til 6", g1.GetGame());
            Assert.AreEqual("Version: Classic", g1.versionList[0].GetVersion());
            Assert.AreEqual("Stand: a -- Pris: 300 -- Noter: Reserveret", g1.versionList[0].copyList[0].GetCopy());
        }

        [TestMethod]
        public void GameConstructorWithVersion()
        {
            Assert.AreEqual("Spil: Cluedo -- Genre: familie -- Spillere: 2 til 5", g2.GetGame());
            Assert.AreEqual("Version: Den bedste version", g2.versionList[0].GetVersion());
        }


        [TestMethod]
        public void GameConstructorWithoutVersion()
        {
            Assert.AreEqual("Spil: Kalaha -- Genre: Familie -- Spillere: 1 til 2", g3.GetGame());
        }
    }
}