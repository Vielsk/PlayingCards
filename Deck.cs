using System;
using System.Collections.Generic;

namespace PlayingCards
{
    public class Deck
    {
        private List<Card> _cards = new ();
        private Random _random = new ();

        public int Count() => _cards.Count;

        public void AddCardToTop(Card card) => _cards.Add(card);
        
        public void AddCardToBottom(Card card) => _cards.Insert(0, card);

        public void AddCardToRandomPlace(Card card) => _cards.Insert(_random.Next(0, _cards.Count), card);

        public void AddCardsToRandomPlaces(List<Card> cards) => cards.ForEach(c => AddCardToRandomPlace(c));

        public Card DrawRandomCard()
        {
            if (_cards.Count == 0) throw new InvalidOperationException("The deck is empty.");

            var index = _random.Next(0, _cards.Count);
            var card = _cards[index];

            _cards.RemoveAt(index);

            return card;
        }

        public Card DrawTopCard()
        {
            if (_cards.Count == 0) throw new InvalidOperationException("The deck is empty.");

            var lastIndex = _cards.Count - 1;
            var card = _cards[lastIndex];

            _cards.RemoveAt(lastIndex);

            return card;
        }

        public Card DrawBottomCard()
        {
            if (_cards.Count == 0) throw new InvalidOperationException("The deck is empty.");

            var card = _cards[0];

            _cards.RemoveAt(0);

            return card;
        }

        public List<Card> DrawRandomCards(int count)
        {
            if (count > _cards.Count) throw new InvalidOperationException("There are not enough cards in the deck.");

            var drawnCards = new List<Card>();

            for (var i = 0; i < count; i++) drawnCards.Add(DrawRandomCard());

            return drawnCards;
        }

        public Deck RemoveCard(Card card)
        {
            _cards.Remove(card);
            return this;
        }

        public Deck RemoveCards(List<Card> cards)
        {
            foreach (var card in cards) _cards.Remove(card);
            return this;
        }

        public void Shuffle()
        {
            for (var i = _cards.Count - 1; i > 0; i--)
            {
                var j = _random.Next(i + 1);
                var temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }
    }
}