using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bomb_game
{
    public partial class Bomb_Game : Form
    {
        int source = 0;
        public static int R = 9;
        public Bomb_Game()
        {
            InitializeComponent();
        }
        List<List<Button>> buttons = new List<List<Button>>();
        int NumberOfBomb = 20;
        int[] bombs;
        //10, 60
        private void Form1_Load(object sender, EventArgs e)
        {
            tur();
        }
        void tur()
        {
            bombs = new int[NumberOfBomb];
            for (int H = 0; H < R; H++)
            {
                List<Button> B = new List<Button>();
                for (int u = 0; u < R; u++)
                {
                    B.Add(new Button());
                }
                for (int u = 0; u < R; u++)
                {
                    B[u].Size = new System.Drawing.Size(25, 25);
                    B[u].Click += Buttons_click;
                }
                buttons.Add(B);
            }
        }
        void Buttons_click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                if (((Button)sender).Tag == "B")
                {
                    for (int y = 0; y < NumberOfBomb; y++)
                    {
                        buttons[bombs[y] / R][bombs[y] % R].BackgroundImage = Bomb_game.Properties.Resources.bomb;
                        buttons[bombs[y] / R][bombs[y] % R].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    timer1.Enabled = false;
                    Application.DoEvents();
                    MessageBox.Show("BOMB!!!");
                }
                else if (((Button)sender).BackgroundImage == null)
                {
                    ((Button)sender).BackgroundImage = Bomb_game.Properties.Resources.blank;
                    source++;
                    label1.Text = "Source :" + source;
                }
                if (source == R * R - NumberOfBomb)
                {
                    MessageBox.Show("You are win!!!");
                    for (int y = 0; y < NumberOfBomb; y++)
                    {
                        buttons[bombs[y] / R][bombs[y] % R].BackgroundImage = Bomb_game.Properties.Resources.bomb;
                        buttons[bombs[y] / R][bombs[y] % R].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    timer1.Enabled = false;
                    Application.DoEvents();
                }
            }
        }
        void new_game()
        {
            source = 0;
            label1.Text= "Source :" + source;
            Random Rn = new Random();
            int I = 0, p, o;
            bombs = new int[NumberOfBomb];
            for (int i = 0; i < NumberOfBomb; i++)
            {

                I = Rn.Next(1, R * R);
                p = I / R;
                o = I % R;
                if (buttons[p][o].Tag == "B")
                {
                    i--;
                    continue;
                }
                bombs[i] = I;
                buttons[p][o].Tag = "B";
            }
        }
        void show_buttons()
        {
            int this_x = 40, this_y = 0;
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < R; j++)
                {
                    buttons[i][j].Location = new System.Drawing.Point(this_x, this_y);
                    this_x += 25;
                    panel1.Controls.Add(buttons[i][j]);
                }
                this_y += 25;
                this_x -= R * 25;
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            time_ = 240;
            panel1.Controls.Clear();
            buttons.Clear();
            tur();
            new_game();
            show_buttons();
            timer1.Enabled = true;
        }
        int time_ = 240;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            time_--;
            if (time_ >= 0)
            {
                label2.Text = time_ / 60 + ":" + time_ % 60;
                return;
            }
            for (int y = 0; y < NumberOfBomb; y++)
            {
                buttons[bombs[y] / R][bombs[y] % R].BackgroundImage = Bomb_game.Properties.Resources.bomb;
                buttons[bombs[y] / R][bombs[y] % R].BackgroundImageLayout = ImageLayout.Stretch;
            }
            timer1.Enabled = false;
            Application.DoEvents();
            MessageBox.Show("BOMB!!!");
        }
    }
}
