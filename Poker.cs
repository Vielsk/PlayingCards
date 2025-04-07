using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayingCards
{
	public enum PokerHandType
	{
		HighCard, OnePair, TwoPairs, ThreeOfKind, Straight,
		Flush, FullHouse, FourOfKind, StraightFlush
	}

	public class PokerHandResult
	{
		public PokerHandType HandType;
		public List<Card> Cards;

		public PokerHandResult(PokerHandType handType, List<Card> cards)
		{
			HandType = handType;
			Cards = cards;
		}

		public override bool Equals(object obj)
		{
			if (obj is PokerHandResult other) return HandType == other.HandType && Cards.SequenceEqual(other.Cards);
			return false;
		}

		public override int GetHashCode()
		{
			int hash = HandType.GetHashCode();
			foreach (var card in Cards) hash = (hash * 397) ^ card.GetHashCode();
			return hash;
		}

		public static bool operator ==(PokerHandResult left, PokerHandResult right)
		{
			if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
			return left.Equals(right);
		}

		public static bool operator !=(PokerHandResult left, PokerHandResult right)
		{
			return !(left == right);
		}

		public override string ToString()
		{
			return $"{HandType} [{string.Join(",", Cards)}]";
		}
	}

	public static class PokerHandFinder
	{
		public static PokerHandResult FindBestHand(List<Card> cards)
		{
			if (cards == null || cards.Count == 0) return new PokerHandResult(PokerHandType.HighCard, new List<Card>());

			var rankGroups = cards.GroupBy(card => card.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
			var suitGroups = cards.GroupBy(card => card.Suit).ToList();

			bool IsStraight(List<Card> cards, out List<Card> straightCards)
			{
				straightCards = new List<Card>();

				var orderedRanks = cards.Select(c => c.Rank).Distinct().OrderByDescending(r => (int)r).ToList();

				if (orderedRanks.Contains(CardRank.Ace)) orderedRanks.Add(CardRank.Ace - 13);

				for (var i = 0; i <= orderedRanks.Count - 5; i++)
				{
					if ((int)orderedRanks[i] - (int)orderedRanks[i + 4] == 4)
					{
						var straightRanks = orderedRanks.Skip(i).Take(5).Select(r => r < CardRank.Two ? CardRank.Ace : r).ToHashSet();

						straightCards = cards.Where(c => straightRanks.Contains(c.Rank)).Distinct().ToList();

						return true;
					}
				}
				return false;
			}

			var flushExists = suitGroups.Any(g => g.Count() >= 5);
			var flushCards = flushExists ? suitGroups.First(g => g.Count() >= 5).ToList() : new List<Card>();

			if (flushExists && IsStraight(flushCards, out var sfCards)) return new (PokerHandType.StraightFlush, sfCards);

			if (rankGroups[0].Count() == 4) return new (PokerHandType.FourOfKind, rankGroups[0].ToList());

			if (rankGroups[0].Count() == 3 && rankGroups.Count > 1 && rankGroups[1].Count() >= 2)
			{
				return new (PokerHandType.FullHouse, rankGroups[0].Concat(rankGroups[1].Take(2)).ToList());
			}

			if (flushExists) return new (PokerHandType.Flush, flushCards.Take(5).ToList());

			if (IsStraight(cards, out var straightCards)) return new (PokerHandType.Straight, straightCards);

			if (rankGroups[0].Count() == 3) return new (PokerHandType.ThreeOfKind, rankGroups[0].ToList());

			if (rankGroups[0].Count() == 2 && rankGroups.Count > 1 && rankGroups[1].Count() == 2)
			{
				return new (PokerHandType.TwoPairs, rankGroups[0].Concat(rankGroups[1]).ToList());
			}

			if (rankGroups[0].Count() == 2) return new (PokerHandType.OnePair, rankGroups[0].ToList());

			return new (PokerHandType.HighCard, rankGroups.SelectMany(g => g).Take(1).ToList());
		}
	}
}
