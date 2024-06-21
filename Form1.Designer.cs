namespace SipkyTurnaj2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btn_addPlayer = new Button();
            txt_playerName = new TextBox();
            btn_turnaj = new Button();
            lbl_playerCountTxt = new Label();
            lbl_playerCount = new Label();
            SuspendLayout();
            // 
            // btn_addPlayer
            // 
            btn_addPlayer.Location = new Point(8, 9);
            btn_addPlayer.Name = "btn_addPlayer";
            btn_addPlayer.Size = new Size(130, 36);
            btn_addPlayer.TabIndex = 0;
            btn_addPlayer.Text = "Pridaj hráča";
            btn_addPlayer.UseVisualStyleBackColor = true;
            btn_addPlayer.Click += btn_addPlayer_Click;
            // 
            // txt_playerName
            // 
            txt_playerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_playerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txt_playerName.Location = new Point(8, 51);
            txt_playerName.Name = "txt_playerName";
            txt_playerName.Size = new Size(130, 23);
            txt_playerName.TabIndex = 1;
            txt_playerName.KeyDown += Txt_KeyDown;
            // 
            // btn_turnaj
            // 
            btn_turnaj.Location = new Point(8, 142);
            btn_turnaj.Name = "btn_turnaj";
            btn_turnaj.Size = new Size(130, 36);
            btn_turnaj.TabIndex = 2;
            btn_turnaj.Text = "Vytvor turnaj";
            btn_turnaj.UseVisualStyleBackColor = true;
            btn_turnaj.Click += btn_turnaj_Click;
            // 
            // lbl_playerCountTxt
            // 
            lbl_playerCountTxt.AutoSize = true;
            lbl_playerCountTxt.Location = new Point(8, 86);
            lbl_playerCountTxt.Name = "lbl_playerCountTxt";
            lbl_playerCountTxt.Size = new Size(126, 15);
            lbl_playerCountTxt.TabIndex = 3;
            lbl_playerCountTxt.Text = "Aktuálny počet hráčov";
            // 
            // lbl_playerCount
            // 
            lbl_playerCount.AutoSize = true;
            lbl_playerCount.Location = new Point(52, 114);
            lbl_playerCount.Name = "lbl_playerCount";
            lbl_playerCount.Size = new Size(0, 15);
            lbl_playerCount.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScroll = true;
            ClientSize = new Size(468, 460);
            Controls.Add(lbl_playerCount);
            Controls.Add(lbl_playerCountTxt);
            Controls.Add(btn_turnaj);
            Controls.Add(txt_playerName);
            Controls.Add(btn_addPlayer);
            Name = "Form1";
            Text = "Turnajový pavúk";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btn_addPlayer;
        private TextBox txt_playerName;
        private Button btn_turnaj;
        private Label lbl_playerCountTxt;
        private Label lbl_playerCount;
    }
}
