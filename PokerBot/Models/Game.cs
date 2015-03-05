using System;
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
            return opponentMove = move;
        }

        // FOLD, CALL, BET, BET:X
        public string Move()
        {
            switch (card.Id)
            {
                case "2":
                case "3":
                case "4":
                    BetChips(1);
                    return "CALL";
                case "5":
                case "6":
                case "7":
                    BetChips(1);
                    return "BET";
                case "8":
                case "9":
                case "T":
                    BetChips(1);
                    return "BET";
                case "J":
                case "Q":
                case "K":
                    BetChips(3);
                    return "BET:3";
                case "A":
                    BetChips(5);
                    return "BET:5";
                default:
                    throw new Exception("Invalid card");
            }
        }

        private void BetChips(int chips)
        {
            Chips -= chips;
        }

        public void ReceiveChips(int chips)
        {
            Chips += chips;
        }

        public string Result()
        {
            if (Chips == StartingChips) return "DRAW";
            return Chips > StartingChips ? "WIN" : "LOSE";
        }
    }
}