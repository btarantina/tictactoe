using System;
using System.Collections;
using com.truplay.Player;

namespace com.truplay.Games.TicTacToe {
    class Program {

        static private TicTacToePlayer? s_computer;
        static private TicTacToePlayer? s_user;

        //data structure to hold the open spaces on the board
        static private int[] s_spaces = {1,2,3,4,5,6,7,8,9};
        static private HashSet<int> s_freeSpaces = new HashSet<int>(s_spaces);

        static void Main(string[] args) {
            //Give the player the rules of the game
            DisplayInstructions();

            //create the computer player
            s_computer = new TicTacToePlayer("computer", true);
            Console.WriteLine("Computer player: " + s_computer.getName());

            //create the user's player
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

            Boolean keepPlaying = true;
            int selection;

            while (keepPlaying) {
                //show the current board
                ShowBoard();

                //get input and validate that it is both an integer and an empty spot on the board
                selection = GetInput();
                s_user.AddMove(selection);

                //check for end of game scenario
                Console.WriteLine("checking for a win condition...");
                keepPlaying = CheckKeepPlaying(s_user);

                if (keepPlaying) {
                    Console.WriteLine("no win scenario yet... keep playing");
                    selection = GetComputerTurn();
                    Console.WriteLine("Computer chose: " + selection);
                    s_computer.AddMove(selection);

                    Console.WriteLine("no win scenario after computer's turn...keep playing");
                    keepPlaying = CheckKeepPlaying(s_computer);
                }
            }

            //display the final board and exit
            ShowBoard();

        }

        static void DisplayInstructions() {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("\nGet 3 in a row (vertical, horizontal or diagonal) and you win!\n");
        }

        static int GetInput() {
            bool validInput = false;
            int selection = 0;
            while ( ! validInput) {

                Console.Write("Enter your selection: ");
                string? value = Console.ReadLine();

                //Is the value a number?
                if ( ! string.IsNullOrEmpty(value)) {
                    selection = Convert.ToInt16(value);

                    if (s_freeSpaces.Contains(selection)) {
                        s_freeSpaces.Remove(selection);
                        validInput = true;
                    }

                } else {
                    Console.WriteLine("Incorrect input! Please enter a number between 1-9");
                }

            }
            return selection;

        }

        static ArrayList CreateWinConditions() {
            ArrayList winConditions = new ArrayList();

            //horizontal wins
            HashSet<int> win_1 = new HashSet<int> {1,2,3};
            HashSet<int> win_2 = new HashSet<int> {4,5,6};
            HashSet<int> win_3 = new HashSet<int> {7,8,9};
            //vertical wins 
            HashSet<int> win_4 = new HashSet<int> {1,4,7};
            HashSet<int> win_5 = new HashSet<int> {2,5,8};
            HashSet<int> win_6 = new HashSet<int> {3,6,9};
            //diagonal wins
            HashSet<int> win_7 = new HashSet<int> {1,5,9};
            HashSet<int> win_8 = new HashSet<int> {3,5,7};
            
            winConditions.Add(win_1);
            winConditions.Add(win_2);
            winConditions.Add(win_3);
            winConditions.Add(win_4);
            winConditions.Add(win_5);
            winConditions.Add(win_6);
            winConditions.Add(win_7);
            winConditions.Add(win_8);
            
            /*
            */
            return winConditions;
        }
        static bool CheckKeepPlaying(TicTacToePlayer player) {
            //check for 3 in a row, horizontal, vertical or diagonal
            ArrayList winConditions = CreateWinConditions();
            bool isSuperset;

            foreach (HashSet<int> win_condition in winConditions ) {
                isSuperset = new HashSet<int>(player.GetMoves()).IsSupersetOf(win_condition);
                if (isSuperset) {
                    Console.WriteLine("\n");
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("     {0} won!", player.getName());
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("-------------------------------------------");
                    return false;
                }
            }
            //if there is no winning condition AND there are no more moves left on the board, declare a tie
            if (s_freeSpaces.Count() == 0) {
                Console.WriteLine("It's a Tie!");
                return false;
            }

            return true;
        }

        static int GetComputerTurn() {
            //get a random free space... it would be better to exand this into an actual strategy by trying to find 
            //two side-by-side moves already taken by the user and trying to block it.
            Random random = new Random();
            var res = (from sel in s_freeSpaces
                            orderby random.Next()
                            select sel).Take(1);

            //int selection = s_freeSpaces.First();
            int s = Convert.ToInt32(res.First());
            s_freeSpaces.Remove(s);
            return s;
        }

        /*
            Name: populateValues()
            Params: none
            Return: string[]
            Description: This method populates the game board with the values by examining the moves already 
            played by each player. It follows these rules:
            - empty: number of the space
            - user: an 'X'
            - computere: an 'O'
        */
        private static string[] populateValues() {
            string[] vals = new string[9];
            for (int i=1; i<=9; i++) {
                if (s_user.HasMove(i)) {
                    vals[i-1] = "X";
                } else if (s_computer.HasMove(i)) {
                    vals[i-1] = "O";
                } else {
                    vals[i-1] = Convert.ToString(i);
                }
            }
            return vals;
        }

        /*
        /*
            Name: showBoard()
            Params: none
            Return: void
            Description: Print the 3x3 game board, showing all slots. For simplicity, users are always 'X's 
            and the computer is always 'O's. Since this is a simple 3x3 board, don't over-engineer it with loops.
        */
        private static void ShowBoard() {
            string space = "|     |     |     |";
            string line =  "-------------------";
            string[] values = populateValues();


            Console.WriteLine(line);
            Console.WriteLine(space);
            Console.WriteLine("|  " + values[0] + "  |  " + values[1] + "  |  " + values[2] + "  |");
            Console.WriteLine(space);
            Console.WriteLine(line);
            Console.WriteLine(space);
            Console.WriteLine("|  " + values[3] + "  |  " + values[4] + "  |  " + values[5] + "  |");
            Console.WriteLine(space);
            Console.WriteLine(line);
            Console.WriteLine(space);
            Console.WriteLine("|  " + values[6] + "  |  " + values[7] + "  |  " + values[8] + "  |");
            Console.WriteLine(space);
            Console.WriteLine(line);
        }

    }
}
