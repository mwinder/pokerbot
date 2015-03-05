using System.Text.RegularExpressions;
using NUnit.Framework;
using PokerBot.Models;

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

            Assert.AreEqual(Card.Ace, game.Card(new Card("A")));
        }

        [Test]
        public void DeterminesMoveForAce()
        {
            var game = new Game("Opponent", 100, 300);

            game.Card(new Card("A"));

            Assert.AreEqual("BET:1000", game.Move());
        }

        [Test]
        public void DeterminesMoveForThree()
        {
            var game = new Game("Opponent", 100, 300);

            game.Card(new Card("3"));

            Assert.AreEqual("CALL", game.Move());
        }

        [Test]
        public void ReadBet()
        {
            Assert.AreEqual("10", Regex.Replace("BET:10", @"BET:(\d)", "$1"));
            Assert.AreEqual("10", Regex.Replace("BET", @"BET:(\d)", "$1"));
        }
    }
}