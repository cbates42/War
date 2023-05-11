using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using War.Card;
using War.Players;
using Services;
using Services.Model;
using System.ComponentModel.DataAnnotations;


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
            Validate(player);
            if(!player.isValid)
            {
                InitializeGame();
            }

            cards = deck.deck;
            //both players get half of the deck
         player.FillHand(cards);
         cpu.FillHand(cards);
            War();
        }
        public void Validate(Player _player)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var validationContext = new ValidationContext(_player, null, null);

            _player.isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(_player, validationContext, errors, true);

            foreach (var error in errors)
            {
                foreach (var mem in error.MemberNames)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: Reason:{error.ErrorMessage}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void War()
        {
            Battle battle = new Battle(player, cpu);
            while (player.hand.Count > 0 && cpu.hand.Count > 0)
            {
                //continue looping while both players have more than 0 cards
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                bool continuePlay = battle.Play();
                Console.ReadLine();

                if (!continuePlay)
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

            //update database
            PlayerModel model = new PlayerModel();
            model.name = player.name;
            model.turns = battle.turns;

            service.InsertModel(model);
            service.APIInsertPlayer(model);

            End();
        }

        public static void End()
        {
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
