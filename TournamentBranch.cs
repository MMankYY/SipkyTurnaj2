using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SipkyTurnaj1
{
    public class TournamentBranch
    {
        public List<Match> _matches { get; set; }
        public List<Player> _players { get; set; }
        public string _branchName { get; set; }


        //Zatial pre prvu branch
        List<Player> _playersSelected { get; set; }



        public TournamentBranch(List<Player> players)
        {
            _matches = new List<Match>();
            _players = players;
            _playersSelected = new List<Player>();
        }

        public void AddMatch(Match match)
        {
            _matches.Add(match);
        }

        public List<Match> CreateFirstBranch()
        {
            // Zistiť počet hráčov a určiť počet zápasov
            int pocetHracov = _players.Count;
            int pocetZapasov = NajblizsieVyssiaMocninaDvojky(pocetHracov) / 2;

            // Doplniť prázdne miesta
            while (_players.Count < pocetZapasov * 2)
            {
                _players.Add(new Player("Volny los"));
            }

            // Náhodne miešať hráčov
            Random rand = new Random();
            _players = _players.OrderBy(x => rand.Next()).ToList();

            // Vytvoriť zápasy
            for (int i = 0; i < pocetZapasov; i++)
            {
                Player player1 = NajdiNenahodnehoHraca();
                Player player2 = NajdiNenahodnehoHraca(player1.Name);                

                var zapas = new Match(player1, player2);
                _matches.Add(zapas);
            }

            //Dat za sebou zapasi tak aby sa striedali hraci a volne losy ak sa da
            var playersMatch = _matches.Where(p => p.Player1.Name != "Volny los" || p.Player2.Name != "Volny los").ToList();
            var freeMatch = _matches.Where(p => p.Player1.Name == "Volny los" || p.Player2.Name == "Volny los").ToList();

            var mixMatchs = new List<Match>();

            for (int i = 0; (i < freeMatch.Count); i++)
            {
                mixMatchs.Add(freeMatch[i]);

                if (playersMatch.Count > 0 && playersMatch[i] != null)
                {
                    mixMatchs.Add(playersMatch[i]);
                }
            }

            return mixMatchs;
        }

        private Player NajdiNenahodnehoHraca(string vynechajHraca = "")
        {
            Random rand = new Random();
            Player hrac;
            List<string> vybranyHraci = new List<string>();

            var realPlayers = _players.Where(r => r.Name != "Volny los").Count();
            var fakePlayers = _players.Where(r => r.Name == "Volny los").Count();

            if (realPlayers == fakePlayers)
            {
                hrac = _players.Where(r => r.Name != "Volny los").First();
            }
            else if (realPlayers < fakePlayers)
            {
                hrac = _players.Where(r => r.Name == "Volny los").First();
            }
            else
            {
                do
                {
                    hrac = _players[rand.Next(_players.Count)];
                }
                while (hrac.Name == vynechajHraca || hrac.Name == "Volny los" || vybranyHraci.Contains(hrac.Name));
            }

            _playersSelected.Add(hrac);
            _players.Remove(hrac);
            vybranyHraci.Add(hrac.Name);
            return hrac;
        }

        private int NajblizsieVyssiaMocninaDvojky(int cislo)
        {
            int mocninaDvojky = 1;
            while (mocninaDvojky < cislo)
            {
                mocninaDvojky *= 2;
            }
            return mocninaDvojky;
        }

    }
}
