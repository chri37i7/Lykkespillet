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
        // Has the game been won
        private static bool isDone;
        #endregion

        public static void Main()
        {
            // Register the amount of players, and their names
            RegisterPlayers();

            // Loop
            while(!isDone)
            {
                // Run the actual game
                RunTheGame();
            }
        }

        #region Run The Game
        /// <summary>
        /// Runs the actual game itself
        /// </summary>
        public static void RunTheGame()
        {
            // Int for storing the total amount of points from throws
            int total = 0;

            // Show who's turn it is
            ShowTurn(players[turnIndex]);

            // Clear the console
            Console.Clear();

            // Loop
            while(!isDone)
            {
                // Clear the console
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

                    // Save total
                    total += dice;

                    // Get the users choice
                    string choice = Console.ReadLine();

                    // Do stuff depending on the choice
                    if(choice == "1")
                    {
                        // Clear the console
                        Console.Clear();
                    }
                    else if(choice == "2")
                    {
                        // Clear the console
                        Console.Clear();

                        // Save the total
                        players[turnIndex].Points += total;

                        // Reset total
                        total = 0;

                        // Check if the game has been won
                        if(!HasBeenWon())
                        {
                            // Output the player and points
                            Console.WriteLine($"{players[turnIndex].Name} har nu {players[turnIndex].Points} points");

                            // Change turns
                            ChangeTurn();

                            // Pause console
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    // Write message
                    Console.WriteLine($"Desværre. {players[turnIndex].Name} slog {dice} og tabte sine slag");

                    // Change turns
                    ChangeTurn();

                    // Pause consone
                    Console.ReadKey();
                }
            }
        }
        #endregion

        #region Show Current Scores
        /// <summary>
        /// Shows the current scores in decending order
        /// </summary>
        public static void ShowCurrentScores()
        {
            // Sort players by points decending
            List<Player> currentPlayers = players.OrderByDescending(player => player.Points).ToList();

            // String that will contain players and their scores
            string scores = "Indhold i skattekister: ";

            // Concatenate players and their points
            foreach(Player player in currentPlayers)
            {
                scores += $"{player.Name}: {player.Points}, ";
            }

            // Output the scores
            Console.WriteLine(scores);
        }
        #endregion

        #region Show Turn
        /// <summary>
        /// Shows which players turn it is
        /// </summary>
        /// <param name="player"></param>
        public static void ShowTurn(Player player)
        {
            // Clear the console
            Console.Clear();

            // Write message
            Console.WriteLine($"Det er nu {player.Name}'s tur!\n");

            // Show the current scores
            ShowCurrentScores();

            // Pause the console
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
            // Clear the console
            Console.Clear();

            // Show message
            Console.WriteLine($"Tillykke! {winner.Name} vandt spillet med {winner.Points} points!!!");

            // Pause the console
            Console.ReadKey();
        }
        #endregion

        #region Change turn
        /// <summary>
        /// Change turn to the next player
        /// </summary>
        public static void ChangeTurn()
        {
            if(turnIndex == players.Count - 1)
            {
                turnIndex = 0;
            }
            else
            {
                turnIndex++;
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
            // Create random object
            Random random = new Random();

            // Generate a random number
            int dice = random.Next(1, 6);

            // Return the number
            return dice;
        }
        #endregion

        #region Register Players
        /// <summary>
        /// Registers a input amount of players
        /// </summary>
        public static void RegisterPlayers()
        {
            // Loop
            while(true)
            {
                // Clear the console
                Console.Clear();

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

                if(playerAmount > 0)
                {
                    // Create player objects
                    for(int i = 0; i < playerAmount; i++)
                    {
                        // Clear the console
                        Console.Clear();

                        // Create player object
                        Player player = new Player();

                        // Write message
                        Console.Write("Indtast navn: ");

                        // Assign value
                        player.Name = Console.ReadLine();
                        // Add the object to the list
                        players.Add(player);
                    }

                    // Stop the loop
                    break;
                }
                else
                {
                    // Clear the console
                    Console.Clear();

                    // Write error message
                    Console.WriteLine("Fejl! Der skal mindst være én spiller.");

                    // Pause console
                    Console.ReadKey();

                    // Restart the loop
                    continue;
                }
            }
        }
        #endregion

        #region Has Been Won
        public static bool HasBeenWon()
        {
            // Check if current player has 100 points
            if(players[turnIndex].Points >= 100)
            {
                // Break loops
                isDone = true;

                // Show winner
                ShowWinner(players[turnIndex]);

                // Return true because the game has been won
                return true;
            }
            else
            {
                // Return false because the game has not been won
                return false;
            }
        } 
        #endregion
    }
}