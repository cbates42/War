// Shuffle referenced from: https://stackoverflow.com/questions/273313/randomize-a-listt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace War.Players
{
    public abstract class Playable
    {
        public List<Card.Card> hand = new List<Card.Card>();
        public string name;

        public void FillHand(List<Card.Card> deck)
        {
            for (int i = 0; i < 26; i++)
            {
                hand.Add(deck[i]);
            }
            deck.RemoveAll(card => hand.Contains(card));
        }

       public Card.Card Draw()
        {
            try
            {
                var currentCard = this.hand[0];
                hand.Remove(currentCard);
                return currentCard;
            }

            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Hand is empty!");
                return null;
            }
      
              

   
        }

       
    }
}
