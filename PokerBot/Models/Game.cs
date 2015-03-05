using System;
using System.Text.RegularExpressions;
using log4net;
using PokerBot.Controllers;

namespace PokerBot.Models
{
    public class Game
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PokerController));

        public string Opponent { get; private set; }
        public int StartingChips { get; private set; }
        public int Chips { get; private set; }
        public int HandLimit { get; private set; }

        private bool button;
        private bool blind;

        private Card card;

        private string opponentCard;
        private string opponentMove;

        public Game(string opponent, int chips, int handLimit)
        {
            Opponent = opponent;
            StartingChips = chips;
            Chips = chips;
            HandLimit = handLimit;
        }

        public bool ReceiveButton()
        {
            return button = true;
        }

        public bool PostBlind()
        {
            Chips -= 1;
            return blind = true;
        }

        public Card Card(Card value)
        {
            return card = value;
        }

        public string OpponentCard(string value)
        {
            return opponentCard = value;
        }

        public string OpponentMove(string move)
        {
            const string raisePattern = @"BET:(\d)";
            if (Regex.IsMatch(move, raisePattern))
            {
                var raise = Convert.ToInt32(Regex.Replace(move, raisePattern, "$1"));
                Log.InfoFormat("Raised: " + raise);
            }

            return opponentMove = move;
        }

        public string Move()
        {
            switch (card.Id)
            {
                case "2":
                case "3":
                case "4":
                case "5":
                    return Fold();
                case "6":
                case "7":
                case "8":
                case "9":
                    return Bet();
                case "T":
                case "J":
                    return Raise(5);
                case "Q":
                    return Raise(20);
                case "K":
                    return Raise(100);
                case "A":
                    return Raise(2000);
            }
            throw new Exception("Invalid card");
        }

        private string Fold()
        {
            return "FOLD";
        }

        private string Bet()
        {
            return "BET";
        }

        private string Call()
        {
            return "CALL";
        }

        private string Raise(int chips)
        {
            return "BET:" + chips;
        }

        public void ReceiveChips(int chips)
        {
            Chips += chips;
        }
    }
}