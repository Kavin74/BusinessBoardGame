using BusinessGame.Config;
using BusinessGame.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessGame.Worker
{
    public class GamePlayProcessor
    {
        public GamePlayProcessor()
        {
            
        }
        public List<Player> Players;
        public void Run(int players)
        {
            Players = new List<Player>(players);
            for (int iter = 0; iter < players; iter++)
            {
                Players.Add(new Player()
                {
                    HotelAssest = 0,
                    TotalWorth = BoardManifest.InitialMoney,
                    Position = 0
                });
            }

           
            //Context context = new Context()
            //{
                
            //    Players = Players,
            //};

            while (true)
            {
                Console.WriteLine("Feed Dice Value, enter (0) to quit :");
                int diceValue = int.Parse(Console.ReadLine());
                if(diceValue == 0 && (diceValue <2 || diceValue >12))
                {
                    return;
                }
                else
                {
                    BoardManifest.Build();
                    
                }
            }
        }
    }
}
