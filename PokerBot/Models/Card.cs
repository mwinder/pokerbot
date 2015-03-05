using System;

namespace PokerBot
{
    public class Card
    {
        private readonly string card;

        public Card(string card)
        {
            this.card = card;
        }

        public int Weighting
        {
            get
            {
                switch (card)
                {
                    case "2":
                        return 0;
                    case "3":
                        return 1;
                    case "4":
                        return 2;
                    case "5":
                        return 3;
                    case "6":
                        return 4;
                    case "7":
                        return 5;
                    case "8":
                        return 6;
                    case "9":
                        return 7;
                    case "T":
                        return 8;
                    case "J":
                        return 9;
                    case "Q":
                        return 10;
                    case "K":
                        return 11;
                    case "A":
                        return 12;
                    default:
                        throw new Exception("Invalid card");
                }
            }
        }
    }
}