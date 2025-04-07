using System;

namespace PlayingCards
{
    public static class StandardDeck
    {
        public static Deck CreateDeck52()
        {
            var deck = new Deck();

            for (int rank = CardRank.Two.Value(); rank <= CardRank.Ace.Value(); rank++)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    deck.AddCardToTop(new (rank, suit));
                }
            }

            deck.Shuffle();

            return deck;
        }

        public static Deck CreateDeck36()
        {
            var deck = new Deck();

            for (int rank = CardRank.Six.Value(); rank < CardRank.Ace.Value(); rank++)
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    deck.AddCardToTop(new (rank, suit));
                }
            }

            deck.Shuffle();

            return deck;
        }
    }
}
