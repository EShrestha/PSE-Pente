using System;
using System.Collections.Generic;
using System.Text;

namespace Pente.GameLogic
{

    // What each player has
    [Serializable]
    public class Player
    {
        // Details of a player
        public string name;
        public int color;
        public int numOfCaptures;
        public bool isOut;
        public bool isAi;

        // Used to make a player
        public Player(string name, int color, int numOfCaptures, bool isAi=false, bool isOut=false)
        {
            this.name = name;
            this.color = color;
            this.numOfCaptures = numOfCaptures;
            this.isAi = isAi;
            this.isOut = isOut;
        }
    }
}
