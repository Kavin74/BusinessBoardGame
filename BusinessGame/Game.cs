using BusinessGame.Config;
using BusinessGame.Contracts;
using BusinessGame.Contracts.Enum;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BusinessGame
{
    public class GameProcessor
    {
        private List<Player> Players { get; set; }
        private Board Board { get; set; }
        private Die Die { get; set; }

        public GameProcessor()
        {
            Players = GetPlayers();
            Board = new Board(BoardManifest.PathSequence);
            Die = GetDie();
        }
        
        private Die GetDie()
        {
            Console.WriteLine($"No. of dice: {BoardManifest.NoOfDie}");
            return new Die(BoardManifest.NoOfDie, false);
        }

        private List<Player> GetPlayers()
        {
            Console.Write("How many players? ");
            int.TryParse(Console.ReadLine(), out int noOfPlayers);
            Console.WriteLine();

            Players = new List<Player>(noOfPlayers);
            for (int iter = 0; iter < noOfPlayers; iter++)
            {
                Console.Write($"Enter Player{iter+1} name: ");
                string name = Console.ReadLine().Trim();
                Console.WriteLine();
                Players.Add(new Player()
                {
                    Name = name,
                    HotelAssest = 0,
                    Money = BoardManifest.PlayerInitialMoney,
                    Position = 0,
                    TotalChances = 0
                });
            }
            return Players;
        }

        public void Start()
        {
            Console.WriteLine("Game Starts.............");
            try{
                for (int i = 0; i < BoardManifest.NoOfChances; i++)
                {
                    foreach (Player player in Players)
                    {
                        if (player.TotalChances < BoardManifest.NoOfChances)
                        {
                            player.TotalChances++;
                            int stepForward = Die.Roll();
                            Console.WriteLine();
                            Console.WriteLine($"Player- {player.Name.ToUpper()} takes the chance.............");
                            Console.WriteLine($"\trolled a number: {stepForward}");
                            player.Position += stepForward;
                            Property property = Board.GetBoardCell(player.Position - 1);
                            
                            switch (property.Type)
                            {
                                case PropertyType.None:                                    
                                    PrintPlayerAssest(player);
                                    break;

                                case PropertyType.Jail:
                                    player.Money -= property.Penalty;
                                    Console.WriteLine("Player imprisoned ");
                                    PrintPlayerAssest(player);
                                    break;

                                case PropertyType.Treasure:
                                    player.Money += property.Penalty;
                                    Console.WriteLine($"\tWow.. you got a treasure worth of {property.Penalty}");
                                    PrintPlayerAssest(player);
                                    break;

                                case PropertyType.Hotel:
                                    var hotel = property as Hotel;
                                    if (hotel.IsOwned && !hotel.Owner.Equals(player.Name))
                                    {
                                        player.Money -= property.Penalty;
                                        Player houseOwner = Players.FirstOrDefault(f => f.Name.Equals(hotel.Owner));
                                        houseOwner.Money += property.Penalty;
                                        Console.WriteLine($"\tPlayer landed on the property owned by {houseOwner.Name}. {hotel.Penalty} deducted from net worth ");
                                        PrintPlayerAssest(player);
                                    }
                                    else if (!hotel.IsOwned && player.Money >= hotel.Worth)
                                    {
                                        player.HotelAssest += hotel.Worth;
                                        player.Money -= hotel.Worth;
                                        hotel.Owner = player.Name;
                                        hotel.IsOwned = true;
                                        Console.WriteLine("\tPlayer owned a house");
                                        PrintPlayerAssest(player);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception thrown: "+ e.Message);
            }
            
            PrintPlayerScore();
        }
        
        private void PrintPlayerScore()
        {
            Console.WriteLine("---------Game Ends---------");
            Console.WriteLine();

            Players.OrderByDescending(o=> o.TotalWorth).ToList().ForEach(f => Console.WriteLine($"Player-{f.Name} has total worth {f.TotalWorth}"));
            Console.Read();
        }

        private void PrintPlayerAssest(Player player)
        {
            Console.WriteLine($"\tPlayer's current net worth = {player.TotalWorth}");
        }        
    }
}
