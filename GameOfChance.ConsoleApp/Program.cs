using GameOfChance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfChance.ConsoleApp
{
    public class Program
    {
        #region Static Fields

        // Static field for storing players
        private static List<Player> players;
        // Who's turn is it?
        private static int turnIndex;

        #endregion

        public static void Main()
        {
            //players = CreatePlayers(2);

            //players[0].Name = "Christian";
            //players[1].Name = "Jens";

            RegisterPlayers();

            ShowStartMenu();

            while(true)
            {
                RunTheGame();

                if(players[turnIndex].Points >= 100)
                {
                    break;
                }
            }
        }

        #region Show Start Menu
        /// <summary>
        /// Displays a starting message
        /// </summary>
        public static void ShowStartMenu()
        {
            Console.Clear();

            Console.WriteLine("Tryk på en tast for at starte spillet...");

            Console.ReadKey();
        }
        #endregion

        #region Show Winner
        /// <summary>
        /// Displays who won the game
        /// </summary>
        /// <param name="winner"></param>
        public static void ShowWinner(Player winner)
        {
            Console.Clear();

            // Show message
            Console.WriteLine($"Tillykke! {winner.Name} vandt spillet med {winner.Points} points!!!");

            Console.ReadKey();
        }
        #endregion

        #region Run The Game
        /// <summary>
        /// The game itself
        /// </summary>
        public static void RunTheGame()
        {
            // Loop
            while(true)
            {
                Console.Clear();

                // Roll the dice?
                int dice = RollTheDice();

                // Selection!
                if(dice != 1)
                {                  
                    // Write message
                    Console.Write(
                        $"{players[turnIndex].Name} slog {dice}, hvad vil han nu?\n\n" +
                        $"1) Slå igen\n" +
                        $"2) Gem slag\n\n" +
                        $"Indtast valg: ");

                    // Get the users choice
                    string choice = Console.ReadLine();

                    // Do stuff depending on the choice
                    if(choice == "1")
                    {
                        Console.Clear();

                        // Restart the loop
                        continue;
                    }
                    else if(choice == "2")
                    {
                        Console.Clear();

                        // Save the total
                        players[turnIndex].Points += dice;

                        // Check if the game has been won
                        if(players[turnIndex].Points >= 100)
                        {
                            ShowWinner(players[turnIndex]);

                            break;
                        }
                        // Output the player and points
                        Console.WriteLine($"{players[turnIndex].Name} har nu {players[turnIndex].Points} points");

                        // Change turns
                        if(turnIndex == players.Count - 1)
                        {
                            turnIndex = 0;
                        }
                        else
                        {
                            turnIndex++;
                        }

                        // Pause console
                        Console.ReadKey();
                    }
                }
                else
                {
                    // Write message
                    Console.WriteLine($"Desværre. {players[turnIndex].Name} slog {dice} og tabte sine slag");

                    // Change turns
                    if(turnIndex == players.Count - 1)
                    {
                        turnIndex = 0;
                    }
                    else
                    {
                        turnIndex++;
                    }

                    // Pause consone
                    Console.ReadKey();

                    // Break loop to restart
                    break;
                }
            }
        }
        #endregion

        #region Roll the Dice
        /// <summary>
        /// Used for generating a number on the dice
        /// </summary>
        /// <returns>Returns a number from 1 - 6</returns>
        public static int RollTheDice()
        {
            Random random = new Random();

            int dice = random.Next(1, 6);

            return dice;
        }
        #endregion

        #region Register Players
        /// <summary>
        /// Registers a input amount of players
        /// </summary>
        public static void RegisterPlayers()
        {
            // Initialize players
            players = new List<Player>();

            // Write message
            Console.Write(
                "Hvor mange spillere er i?" +
                "\n\n" +
                "Indtast antal: ");

            // Read and parse input
            string input = Console.ReadLine();
            int.TryParse(input, out int playerAmount);

            // Create player objects
            for(int i = 0; i < playerAmount; i++)
            {
                Console.Clear();

                Player player = new Player();

                Console.Write("Indtast navn: ");

                string name = Console.ReadLine();

                player.Name = name;

                players.Add(player);
            }
        }
        #endregion

        #region Create Players
        /// <summary>
        /// Creates a specific amount of <see cref="Player"/> objects
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>A list og <see cref="Player"/> objects</returns>
        public static List<Player> CreatePlayers(int amount)
        {
            // Create list to store players
            List<Player> players = new List<Player>();

            // Create players, and add them to the list
            for(int i = 0; i < amount; i++)
            {
                Player player = new Player();

                players.Add(player);
            }

            // Return the list
            return players;
        }
        #endregion
    }
}