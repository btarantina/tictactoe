namespace com.truplay.Player {
    
    /*
    This is the Tic-Tac-Toe player class. It contains fields/methods specific to the game.
    */
    public class TicTacToePlayer : Player {

        //We don't really need to keep track of the order of the moves for this game, so just store them in a Set.
        private HashSet<int> moves; //A set to keep track of the moves on the tic-tac-toe board. 

        //basic constructor
        public TicTacToePlayer(string name, bool isAI) : base(name, isAI) {
            moves = new HashSet<int>();
        }

        public void AddMove(int move) {
            moves.Add(move);
        }

        public bool HasMove(int move) {
            return moves.Contains(move);
        }

        public HashSet<int> GetMoves() {
            return moves;
        }
    }
}