namespace PlayingCards
{
    public enum CardSuit
    {
        Clubs, Diamonds, Hearts, Spades  
    }

    public enum CardRank
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6,
        Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11,
        Queen = 12, King = 13, Ace = 14
    }

    public enum CardColor
    {
        Red, Black
    }

    public class Card
    {
        public CardSuit Suit;
        public CardRank Rank;

        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public Card(int rank, CardSuit suit)
        {
            Rank = (CardRank)rank;
            Suit = suit;
        }

        public override string ToString()
        {
            string suitEmoji = Suit switch
            {
                CardSuit.Clubs => "♣️",
                CardSuit.Diamonds => "♦️",
                CardSuit.Hearts => "♥️",
                CardSuit.Spades => "♠️",
                _ => ""
            };

            string rankStr = Rank switch
            {
                CardRank.Two   => "2",
                CardRank.Three => "3",
                CardRank.Four  => "4",
                CardRank.Five  => "5",
                CardRank.Six   => "6",
                CardRank.Seven => "7",
                CardRank.Eight => "8",
                CardRank.Nine  => "9",
                CardRank.Ten   => "10",
                CardRank.Jack  => "J",
                CardRank.Queen => "Q",
                CardRank.King  => "K",
                CardRank.Ace   => "A",
                _ => ""
            };

            return $"{rankStr}{suitEmoji}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Card other) return Rank == other.Rank && Suit == other.Suit;
            return false;
        }

        public override int GetHashCode()
        {
            int hashRank = Rank.GetHashCode();
            int hashSuit = Suit.GetHashCode();
            return (hashRank * 397) ^ hashSuit;
        }

        public static bool operator ==(Card left, Card right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(Card left, Card right)
        {
            return !(left == right);
        }
    }

    public static class CardExtensions
    {
        public static CardColor Color(this CardSuit cardSuit)
        {
            switch (cardSuit)
            {
                case CardSuit.Clubs: return CardColor.Black;
                case CardSuit.Diamonds: return CardColor.Red;
                case CardSuit.Hearts: return CardColor.Red;
                case CardSuit.Spades: return CardColor.Black;
                default: return CardColor.Red;
            }
        }

        public static int Value(this CardRank cardRank)
        {
            return (int)cardRank;
        }
    }
}