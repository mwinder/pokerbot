using System;
using System.Text.RegularExpressions;
using PokerBot.Models;

namespace PokerBot
{
    public class Game
    {
        public string Opponent { get; private set; }
        public int StartingChips { get; private set; }
        public int Chips { get; private set; }
        public int HandLimit { get; private set; }

        private bool button;
        private bool blind;

        private Card card;

        private string opponentCard;
        private string opponentMove;

        private int currentBet = 2;

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
                currentBet += raise;
            }

            return opponentMove = move;
        }

        private int remainingBets = 20;

        // FOLD, CALL, BET, BET:X
        public string Move()
        {
            switch (card.Id)
            {
                case "2":
                case "3":
                    return Fold();
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    return Bet();
                case "9":
                case "T":
                    return Raise(25);
                case "J":
                case "Q":
                    return Raise(50);
                case "K":
                    return Raise(250);
                case "A":
                    return Raise(1000);
            }
            throw new Exception("Invalid card");
        }

        private string Fold()
        {
            return "FOLD";
        }

        private string Bet()
        {
            Chips -= currentBet;
            return "BET";
        }

        private string Call()
        {
            Chips -= currentBet;
            return "CALL";
        }

        private string Raise(int chips)
        {
            currentBet += chips;

            Chips -= currentBet;
            return "BET:" + chips;
        }

        public void ReceiveChips(int chips)
        {
            Chips += chips;
        }
    }
}