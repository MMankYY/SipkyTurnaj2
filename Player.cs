using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipkyTurnaj1
{
    public class Player
    {
        public string Name { get; set; }
        public int MatchNumber { get; set; }
        public int Losses { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
        }
    }
}
