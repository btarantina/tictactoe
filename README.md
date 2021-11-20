# tictactoe
A simple C# game


Overall Design:

Create a very basic game engine that can be expanded for games. Nearly all games follow a general turn-taking methodology, so we have a top-level class for games and a specific one for this two-player game (where one opponent is the computer);

There are two player classes: 
- Player -- a high level parent class that stores personal information about the player that is not directly part of a specific game. (e.g. name, age, etc)
- TicTacToePlayer -- extends the Player class with fields/methods specific to the tic-tac-toe gameplay

The overall gameplay:
1. Welcome the player and display the instructions.
2. Create the players.
3. Start the turn-based game:
   1. Player makes a move
   2. Evaluate for a win/tie
   3. computer opponent make a move
   4. Evaluate for a win/tie
   5. continue 1-4 until the game ends


TODO:
1. Create unit tests for code coverage and validating that the methods respond correctly with erroneous values/inputs.