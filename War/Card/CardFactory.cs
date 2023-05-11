using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War.Card
{
    public class CardFactory
    {
        private static CardFactory instance { get; set;}
        public static CardFactory Instance()
        {
            if (instance == null)
            {
                instance = new CardFactory();
            }
            return instance;
        }

        public Card CreateCard(Suit suit, int value)
        {
            return new Card(suit, value);
        }

        public List<Card> FillSuit(Suit suit)
        {
            List<Card> currentSuit = new List<Card>();
            for (int i = 1; i <= 13; i++)
            {
                currentSuit.Add(CreateCard(suit, i));
          
  
            }
            return currentSuit;
        }


    }
}
