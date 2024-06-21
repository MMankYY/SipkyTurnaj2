using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipkyTurnaj1
{
    public class Tournament
    {
        public List<TournamentBranch> _tournamentBranch { get; set; }
        public List<Player> _players { get; set; }

        public Tournament(List<Player> players)
        {
            _tournamentBranch = new List<TournamentBranch>();
            _players = players;
            CreateFirsBranch();
        }

        public void CreateFirsBranch()
        {
            var firstBranch = new TournamentBranch(_players);
            firstBranch._matches = firstBranch.CreateFirstBranch();
            _tournamentBranch.Add(firstBranch);
        }

        public List<Player> GetRightPlayers()
        {
            var firstBranch = _tournamentBranch.FirstOrDefault();
            List<Player> _playersRightBranche = new List<Player>();

            foreach (var match in firstBranch._matches) 
            {
                if (match.Player1.Name == "Volny los" || match.Player2.Name == "Volny los")
                {
                    if (match.Player1.Name != "Volny los")
                    {
                        _playersRightBranche.Add(match.Player1);
                    }
                    else 
                    {
                        _playersRightBranche.Add(match.Player2);
                    }
                }
            }

            return _playersRightBranche;
        }



    }
}
