using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War.Players;
using Services;
using Services.Model;

namespace War
{
    public class Battle
    {

        private Player player;
        private CPU cpu;
        private List<Card.Card> _pot = new List<Card.Card>();
        public int turns = 0;
        public Service service = new Service();

        public Battle(Player player, CPU cpu)
        {
            this.player = player;
            this.cpu = cpu;
        }

        public int Compare(int card1, int card2)
        {
            return card1.CompareTo(card2);
        }

        public bool Play()
        {
            turns++;
            Console.WriteLine($"Turn: {turns}");
            Console.WriteLine($"You have: {player.hand.Count} cards. CPU has: {cpu.hand.Count} cards.");
            // Each player draws a card
            Card.Card playerCard = player.Draw();
            Card.Card cpuCard = cpu.Draw();

            Console.WriteLine($"Player draws {playerCard.PrintName()}");
            Console.WriteLine($"CPU draws {cpuCard.PrintName()}");

            // Add cards to the pot
            _pot.Add(playerCard);
            _pot.Add(cpuCard);

            // Compare cards
            int result = Compare(playerCard.value, cpuCard.value);

            if (result > 0)
            {
                // Player wins
                Console.WriteLine("Player wins!");
                player.hand.AddRange(_pot);
                _pot.Clear();

                CardModel model = new CardModel();
                model.cardVal = playerCard.value;
                model.Suit = playerCard.suit.ToString();
                model.turnNum = turns;

                service.InsertWinCard(model);
                service.APIInsertCard(model);


                return true;
            }
            else if (result < 0)
            {
                // CPU wins
                Console.WriteLine("CPU wins!");
                cpu.hand.AddRange(_pot);
                _pot.Clear();

                CardModel model = new CardModel();
                model.cardVal = cpuCard.value;
                model.Suit = cpuCard.suit.ToString();
                model.turnNum = turns;

                service.InsertWinCard(model);
                service.APIInsertCard(model);
                return true;
            }
            else
            {
                // War!
                Console.WriteLine("War!");

                if (player.hand.Count < 4 || cpu.hand.Count < 4)
                {
                    // Not enough cards for war
                    Console.WriteLine("Not enough cards for war!");

                    if(player.hand.Count < cpu.hand.Count)
                    {
                        player.hand.Clear();
                    }
                 
                    return false;
                }

                // Add cards to the pot
                _pot.AddRange(player.hand.Take(4));
                _pot.AddRange(cpu.hand.Take(4));

                // Remove cards from each player's hand
                player.hand.RemoveRange(0, 4);
                cpu.hand.RemoveRange(0, 4);

                // Play another battle
                return Play();
            }

            
        }
    }
}
