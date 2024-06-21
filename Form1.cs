using SipkyTurnaj1;
using System;
using System.Windows.Forms;

namespace SipkyTurnaj2
{
    public partial class Form1 : Form
    {
        int DisUnit = 1;
        List<Player> _players = new List<Player>();
        string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "players.txt");
        Tournament _tournament;

        public Form1()
        {
            InitializeComponent();
            txt_playerName.AutoCompleteCustomSource = FileHelper.LoadNamesFromFile(_filePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void DrawBracket(List<Match> firstBranch)
        {
            int matchHeight = 50;
            int matchSpacing = 30;
            int roundSpacing = 150;
            int startX = 1000; // Starting X position for the center
            int startY = 50;
            var playerCount = firstBranch.Count() * 2;

            int winnersRounds = (int)Math.Log(playerCount, 2); // Po�et k�l pre hlavn� turnaj (Winners Bracket)

            // Vygenerovanie hlavn�ho turnaja
            for (int round = 0; round < winnersRounds; round++)
            {
                int matches = (int)Math.Pow(2, winnersRounds - round - 1);
                for (int match = 0; match < matches; match++)
                {
                    int yOffset = startY + (matchHeight + matchSpacing) * (2 * match + 1) * (int)Math.Pow(2, round);
                    DrawMatch(startX + roundSpacing * round, yOffset, round == 0, firstBranch[match]);
                }
            }


            int losersRounds = winnersRounds; // Po�et k�l pre ved�aj�� turnaj (Losers Bracket)
            for (int round = 0; round < losersRounds; round++)
            {
                int matches = (int)Math.Pow(2, losersRounds - round - 1);
                for (int match = 0; match < matches; match++)
                {
                    if (round > 0 || match >= playerCount / 2) // Skip first matches in the first round
                    {
                        int yOffset = startY + (matchHeight + matchSpacing) * (2 * match + 1) * (int)Math.Pow(2, round);

                        // Draw the original match
                        DrawMatch(startX - roundSpacing * round, yOffset, false, null);

                        // Draw the additional match next to the original
                        DrawMatch(startX - roundSpacing * round - 120, yOffset, false, null); // Adjusted X position for additional match
                    }
                }
            }

        }

        private bool IsFreeMatch(Match match)
        {
            if (match != null)
            {
                return match.Player1.Name == "Volny los" || match.Player2.Name == "Volny los";
            }

            return false;
        }

        private void DrawMatch(int x, int y, bool first, Match match)
        {
            TextBox player1 = new TextBox
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(100, 20),
                Text = first ? match.Player1.Name : string.Empty,
                ReadOnly = true,
                Enabled = !IsFreeMatch(match) && first,

            };

            TextBox player2 = new TextBox
            {
                Location = new System.Drawing.Point(x, y + 25),
                Size = new System.Drawing.Size(100, 20),
                Text = first ? match.Player2.Name : string.Empty,
                ReadOnly = true,
                Enabled = !IsFreeMatch(match) && first,
            };

            player1.Click += TextBox_Click;
            player2.Click += TextBox_Click;

            this.Controls.Add(player1);
            this.Controls.Add(player2);
        }

        private void TextBox_Click(object sender, EventArgs e)
        {
            TextBox clickedTextBox = sender as TextBox;
            if (clickedTextBox != null)
            {
                MessageBox.Show($"Vyhral hrac {clickedTextBox.Text}, presuvam na poziciu PXX, porazeneho na LYY", "Docasne", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_addPlayer_Click(object sender, EventArgs e)
        {
            AddPlayer();
        }

        private void AddPlayer()
        {
            FileHelper.AddNameToFile(txt_playerName.Text, _filePath);
            txt_playerName.AutoCompleteCustomSource.Add(txt_playerName.Text);

            if (txt_playerName.Text == string.Empty)
            {
                MessageBox.Show("Zadaj meno hr��a", "Zadaj meno hr��a", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (_players.Any(p => p.Name.ToLower() == txt_playerName.Text.ToLower()))
            {
                txt_playerName.Text = string.Empty;
                MessageBox.Show("Hr�� u� tam je", "Zadaj meno hr��a", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Player player = new Player(txt_playerName.Text);
                _players.Add(player);
                lbl_playerCount.Text = _players.Count().ToString();

                TextBox txt = new TextBox();
                txt.Text = txt_playerName.Text;
                txt.ReadOnly = true;
                this.Controls.Add(txt);

                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.Click += (s, ev) =>
                {
                    int index = this.Controls.IndexOf(txt); // Index textov�ho po�a v Controls kolekcii
                    var playerName = this.Controls[index].Text;
                    this.Controls.RemoveAt(index); // Odstr�ni� textov� pole
                    this.Controls.RemoveAt(index); // Odstr�ni� pr�slu�n� tla�idlo "Delete"

                    var playerToDeleted = _players.Where(p => p.Name.ToLower() == playerName.ToLower()).FirstOrDefault();
                    _players.Remove(playerToDeleted);
                    lbl_playerCount.Text = _players.Count().ToString();

                    // Aktualizova� poz�ciu a ve�kos� v�etk�ch zvy�n�ch textov�ch pol� a tla�idiel "Delete"
                    for (int i = index; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i] is TextBox)
                        {
                            TextBox remainingTextBox = (TextBox)this.Controls[i];
                            remainingTextBox.Top -= 25; // Posun�� textov� pole hore
                        }
                        else if (this.Controls[i] is Button)
                        {
                            Button remainingDeleteButton = (Button)this.Controls[i];
                            remainingDeleteButton.Top -= 25; // Posun�� tla�idlo "Delete" hore
                        }
                    }

                    DisUnit--; // Aktualizova� po�et textov�ch pol�
                    if (DisUnit == 0)
                    {
                        btn_addPlayer.Top = 0; // Ak neexistuje �iadne textov� pole, tla�idlo "Add Player" sa vr�ti na p�vodn� poz�ciu
                    }
                };

                this.Controls.Add(deleteButton);

                txt.Top = DisUnit * 25;
                txt.Left = 170;
                txt.ForeColor = Color.Green;
                txt.Width = 150;
                txt.Height = 50;

                deleteButton.Top = DisUnit * 25;
                deleteButton.Left = 330;
                deleteButton.Width = 60;
                deleteButton.Height = 20;

                DisUnit++; // Inkrementova� po�et textov�ch pol�
                txt_playerName.Text = string.Empty;
            }
        }

        private void btn_turnaj_Click(object sender, EventArgs e)
        {
            if (_players == null || _players.Count() < 5)
            {
                MessageBox.Show("Zadaj aspo� 5 hra�ov.", "Zadaj aspo� 5 hra�ov.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    control.Visible = false; // Skry� ovl�dac� prvok               
                }

                int playerCount = _players.Count(); // Nastavte po�et hr��ov pod�a potreby

                _tournament = new Tournament(_players);
                var firstBranch = _tournament._tournamentBranch.FirstOrDefault();

                var firstRightBranche = _tournament.GetRightPlayers();

                DrawBracket(firstBranch._matches);
            }            
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddPlayer();
                e.Handled = true;
                e.SuppressKeyPress = true; // Zabra�uje zvukov�mu sign�lu pri stla�en� Enter
            }
        }
    }
}
