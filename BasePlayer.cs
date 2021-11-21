namespace com.truplay.Player {
    
    /*
    This is the base player class for all games. As such, it should only contain information about the player itself
    and additional fields/methods for specific games should utilize inheritence. 
    */
    public abstract class BasePlayer {
        private string Name;
        private bool IsAI;  //Is the player a computer?

        //TODO: add additional fields here... age, gender(?), last login date/time, etc. 

        public BasePlayer() {
            Name = "";
            IsAI = false;
        }

        public BasePlayer(string? name, bool ai) {
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