namespace com.truplay.Player {
    
    /*
    This is the base player class for all games. As such, it should only contain information about the player itself
    and additional fields/methods for for specific games should utilize inheritence. 
    */
    public class Player {
        private string Name;
        private bool IsAI;

        //add additional fields here... age, gender(?), last login date/time, etc. 

        public Player() {
            Name = "";
            IsAI = false;
        }

        public Player(string? name, bool ai) {
            if (name == null) {
                Name = "";
            } else {
                Name = name;
            }
            IsAI = ai;
        }

        //getters
        public string getName() {
            return Name;
        }

        public bool getIsAI() {
            return IsAI;
        }
    }
}