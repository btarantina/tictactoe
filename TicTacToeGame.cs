namespace com.truplay.Games;

using System.Collections;
public class TicTacToeGame : BaseGame {

    //data structure to hold the open spaces on the board
    static private int[] s_spaces = {1,2,3,4,5,6,7,8,9};
    static private HashSet<int> s_freeSpaces = new HashSet<int>(s_spaces);

    /*
        Name: DisplayInstructions()
        Params: none
        Return: void
        Description: Prints the game rules to the user.
    */
    public override void DisplayInstructions() {
        Console.WriteLine("Welcome to Tic-Tac-Toe!");
        Console.WriteLine("\nGet 3 in a row (vertical, horizontal or diagonal) and you win!\n");
        Console.WriteLine("You will be playing against a computer opponent. You will be prompted for a number that correcsponds to a location on the gameboard.");
    }
    
    /*
        Name: ShowBoard()
        Params: none
        Return: void
        Description: Print the 3x3 game board, showing all slots. For simplicity, users are always 'X's 
        and the computer is always 'O's. Since this is a simple 3x3 board, don't over-engineer it with loops.
    */
    public override void ShowBoard() {
        string space = "|     |     |     |";
        string line =  "-------------------";
        string[] values = PopulateValues();


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

    /*
        Name: Play()
        Params: player1 - the first player in the game
                player2 - the second player in the game
        Return: void
        Description: The "main" method of the game. It contols all gameplay, directing player turns and declaring a winner.
    */

    static Player.TicTacToePlayer user;
    static Player.TicTacToePlayer computer;

    public void set_user(Player.TicTacToePlayer player) {
        user = player;
    }

    static List<Player.TicTacToePlayer> players = new List<Player.TicTacToePlayer>();

    //public override void Play<T>(List<T> players ) 
    public override void Play()
    {
        //create the computer opponent
        computer = new Player.TicTacToePlayer("computer", true);

        if (user == null) {
            //we cannot play without a user!
            throw new Exception("Missing the human player!!");
        }

        players.Add(user);
        players.Add(computer);

        //play the game! Keep looping until the game is over.
        Boolean keepPlaying = true;

        while (keepPlaying) {

            foreach (Player.TicTacToePlayer player in players) {

                if (keepPlaying) {
                    //Step 1: show the current board
                    ShowBoard();

                    //Step 2: get and play the Player's move 
                    TakeATurn(player);

                    //Step 3: check for end of game scenario
                    Console.WriteLine("checking for a win condition...");
                    keepPlaying = CheckKeepPlaying(player);

                }
            }

        }

        //display the final board
        ShowBoard();

    }

    /*
        Name: CreateWinConditions()
        Params: none
        Return: ArrayList of all possible winning combinations
        Description: Constructs a list of sets, where each set is a winning combination on the game board.
        Since this is tic-tac-toe, winning conditions are three in a row, horizontally, vertically or diagonally.
    */
    private void TakeATurn(Player.TicTacToePlayer player)
    {
        int selection = 0;

        if ( player.getIsAI()) { //computer opponent
            selection = GetComputerTurn();
            Console.WriteLine("Computer chose: " + selection);

        } else { //human player

            bool validInput = false;
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
                    //TODO: update this to list all of the free spaces 
                    Console.WriteLine("Incorrect input! Please enter a number between 1-9");
                }

            }

        }

        //now that we have the selection, add the move
        player.AddMove(selection);

    }
   
    
    /*
        Name: CreateWinConditions()
        Params: none
        Return: ArrayList of all possible winning combinations
        Description: Constructs a list of sets, where each set is a winning combination on the game board.
        Since this is tic-tac-toe, winning conditions are three in a row, horizontally, vertically or diagonally.
    */
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
        
        return winConditions;
    }

    /*
        Name: CheckKeepPlaying()
        Params: none
        Return: bool -- TRUE|FALSE whether or not the game should continue
        Description: Evaluates certain game conditions and determines if play should continue.
        conditions:
            - No more free spaces
            - a player has a winning combination
    */
    static bool CheckKeepPlaying(Player.TicTacToePlayer player) {
        //check for 3 in a row, horizontal, vertical or diagonal
        ArrayList winConditions = CreateWinConditions();
        bool isSuperset;

        foreach (HashSet<int> win_condition in winConditions ) {
            //we only need to see if the player's set of moves has a winning combination in it
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
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("It's a Tie!");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
            return false;
        }

        return true;
    }

    /*
        Name: GetComputerTurn()
        Params: none
        Return: int -- the location on the board that the computer selected.
        Description: Returns a random free space entry from the list of available spaces.
    */
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
        Name: PopulateValues()
        Params: none
        Return: string[]
        Description: This helper method to populate the game board with the values by examining the moves already 
        played by each player. It follows these rules:
        - empty: number of the space
        - user: an 'X'
        - computere: an 'O'
    */
    private static string[] PopulateValues() {
        string[] vals = new string[9];

        Player.TicTacToePlayer user = players.ElementAt(0);
        Player.TicTacToePlayer computer = players.ElementAt(1);

        for (int i=1; i<=9; i++) {
            if (user.HasMove(i)) {
                vals[i-1] = "X";
            } else if (computer.HasMove(i)) {
                vals[i-1] = "O";
            } else {
                vals[i-1] = Convert.ToString(i);
            }
        }
        return vals;
    }




}