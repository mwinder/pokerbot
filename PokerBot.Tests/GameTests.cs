using NUnit.Framework;

namespace PokerBot.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void CanInitialise()
        {
            var game = new Game("Opponent", 100, 300);

            Assert.AreEqual("Opponent", game.Opponent);
            Assert.AreEqual(100, game.Chips);
            Assert.AreEqual(300, game.HandLimit);
        }

        [Test]
        public void ReceiveButton()
        {
            var game = new Game("Opponent", 100, 300);

            Assert.That(game.ReceiveButton(), Is.True);
        }

        [Test]
        public void ReceiveBlind()
        {
            var game = new Game("Opponent", 100, 300);

            Assert.That(game.PostBlind(), Is.True);
        }

        [Test]
        public void RecieveCard()
        {
            var game = new Game("Opponent", 100, 300);

            Assert.AreEqual("A", game.Card("A"));
        }

        [Test]
        public void DeterminesMoveForAce()
        {
            var game = new Game("Opponent", 100, 300);

            game.Card("A");

            Assert.AreEqual("BET:5", game.Move());
        }

        [Test]
        public void DeterminesMoveForThree()
        {
            var game = new Game("Opponent", 100, 300);

            game.Card("3");

            Assert.AreEqual("CALL", game.Move());
        }
    }
}