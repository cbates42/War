using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War.Card;
using War.Players;
using Services;
using Services.Model;


namespace War
{
    internal class Game
    {
        Deck deck = new Deck();
        Player player = new Player();
        CPU cpu = new CPU();
        public List<Card.Card> cards;
        public Service service = new Service();

        public Game() 
        {
           
            InitializeGame();
        }

        public void InitializeGame()
        {
            Console.WriteLine("What is your name?");
            player.name = Console.ReadLine();
            cards = deck.deck;
         player.FillHand(cards);
         cpu.FillHand(cards);
            War();
        }

        public void War()
        {
            Battle battle = new Battle(player, cpu);
            while (player.hand.Count > 0 && cpu.hand.Count > 0)
            {
                bool continuePlay = battle.Play();

                if(!continuePlay)
                {
                    Console.WriteLine("Game over.");
                    break;
                }    
             }

            if (player.hand.Count > 0)
            {
                cpu.hand.Clear();
                Console.WriteLine("You win!");
            }
            if(cpu.hand.Count > 0)
            {
                player.hand.Clear();
                Console.WriteLine("You lost.");
            }

            PlayerModel model = new PlayerModel();
            model.name = player.name;
            model.turns = battle.turns;

            service.InsertModel(model);
            service.APIInsertPlayer(model);

            Console.WriteLine("Press any key to play again!");
            Console.ReadKey();
            Console.Clear();
            new Game();
        }
       }

 

        

    /*    public void Test()
        {
            foreach(Card.Card card in cards)
            {
                Console.WriteLine(card.PrintName());
            }
            Console.WriteLine("player");
            foreach (Card.Card card in player.hand)
            {
               
                Console.WriteLine(card.PrintName());
            }
            Console.WriteLine("cpu");
            foreach (Card.Card card in cpu.hand)
            {
          
                Console.WriteLine(card.PrintName());
            }

            Console.ReadLine();
        }*/


    }
