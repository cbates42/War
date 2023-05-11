// Shuffle referenced from: https://stackoverflow.com/questions/273313/randomize-a-listt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Services.Logger;


namespace War.Players
{
  
    public abstract class Playable
    {
        public List<Card.Card> hand = new List<Card.Card>();
        [Required(ErrorMessage = "Name cannot be null.")]
        [StringLength(15, ErrorMessage = "Name cannot be longer than 15 chars.")]
        public string name;

        public DbLogger logger = new DbLogger();


        public void FillHand(List<Card.Card> deck)
        {
            //get half of the deck
            for (int i = 0; i < 26; i++)
            {
                hand.Add(deck[i]);
            }
            deck.RemoveAll(card => hand.Contains(card));
        }

       public Card.Card Draw()
        {
            //in case hand gets to 0 during war
            try
            {
                logger.LogWarning("Possibility of OutOfRangeException");
                var currentCard = this.hand[0];
                hand.Remove(currentCard);
                return currentCard;
            }

            catch(ArgumentOutOfRangeException ex)
            {
                logger.LogError("Error occured.", ex);
                Console.WriteLine("Hand is empty!");
                Game.End();
                return null;
            }
      
   
        }

       
    }
}
