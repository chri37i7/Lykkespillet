﻿using GameOfChance.Entities;
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
            // Register the amount of players, and their names
            RegisterPlayers();

            // Loop
            while(true)
            {
                // Run the actual game
                RunTheGame();

                // Check if the current player has won
                if(players[turnIndex].Points >= 100)
                {
                    // Break the current loop if the game has been won
                    break;
                }
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
            while(true)
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

                        // Restart the loop
                        continue;
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

                        // Restart
                        break;
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

        public static void ShowTurn(Player player)
        {
            Console.Clear();

            Console.WriteLine($"Det er nu {player.Name}'s tur!\n");

            ShowCurrentScores();

            Console.ReadKey();
        }

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
    }
}