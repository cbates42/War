using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War.Card
{
    public enum Suit 
    {
    Hearts, Diamonds, Clubs, Spades
    }

    public class Card
    {
        public Suit suit { get; set; }
        public int value { get; set; }

        public Card(Suit suit, int value) 
        {
            this.suit = suit;
            this.value = value;
        }

        public string PrintName()
        {
            return $"{value} of {suit}";
        }

    }
}
