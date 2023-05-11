using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War.Card
{
    public class Deck
    {


        public List<Card> deck { get; set; }
        public CardFactory factory = CardFactory.Instance();
      
        public Deck()
        {
           deck = new List<Card>();
            deck = FillDeck();
            Shuffle(deck);
        }

        public List<Card> FillDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                deck.AddRange(factory.FillSuit(suit));
            }
            return deck;
        }
        public void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
