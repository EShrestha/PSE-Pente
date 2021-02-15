using System;
using System.Collections.Generic;
using System.Text;

namespace Pente.GameLogic
{
    [Serializable]
    public class Player
    {
        public string name;
        public int color;
        public int numOfCaptures;
        public bool isOut;
        public bool isAi;

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
