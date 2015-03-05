using System;

namespace PokerBot.Models
{
    public class Card
    {
        public static readonly Card Two = new Card("2");
        public static readonly Card Three = new Card("3");
        public static readonly Card Four = new Card("4");
        public static readonly Card Five = new Card("5");
        public static readonly Card Six = new Card("6");
        public static readonly Card Seven = new Card("7");
        public static readonly Card Eight = new Card("8");
        public static readonly Card Nine = new Card("9");
        public static readonly Card Ten = new Card("T");
        public static readonly Card Jack = new Card("J");
        public static readonly Card Queen = new Card("Q");
        public static readonly Card King = new Card("K");
        public static readonly Card Ace = new Card("A");

        public readonly string Id;

        public Card(string card)
        {
            Id = card;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var other = obj as Card;
            if (other == null) return false;

            return Id == other.Id;
        }

        public int Weighting
        {
            get
            {
                switch (Id)
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