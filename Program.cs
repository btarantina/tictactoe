using System;
using System.Collections;
using com.truplay.Player;

namespace com.truplay.Games.TicTacToe {
    class Program {

        static private TicTacToePlayer? s_computer;
        static private TicTacToePlayer? s_user;

        static void Main(string[] args) {

            TicTacToeGame game = new TicTacToeGame();

            //Give the player the rules of the game
            game.DisplayInstructions();

            //create the computer player
            s_computer = new TicTacToePlayer("computer", true);
            Console.WriteLine("Computer player: " + s_computer.getName());

            //ask the player for their name and create their player object
            Console.Write("What is your name? ");
            string? playerName = Console.ReadLine();
            while (playerName == null) {
                Console.WriteLine("Please specify a name.");
                playerName = Console.ReadLine();
            }
            if (playerName != null) {
                s_user = new TicTacToePlayer(playerName, false);
            }
            Console.WriteLine("\nHello " + s_user.getName() + "!\n");

            //play the game!
            game.set_user(s_user);
            game.Play();

            //exit
            Console.WriteLine("\n Thank you for playing!");
        }

    }

}
