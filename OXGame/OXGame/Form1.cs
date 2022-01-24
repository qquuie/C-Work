using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OXGame
{
    public partial class Form1 : Form
    {
        int whoPlayer;
        int clickCount = 0;
        Button btn;

        Button[,] btnOX = new Button[3, 3];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    btnOX[i, j] = new Button();
                    this.Controls.Add(btnOX[i,j]);
                    btnOX[i, j].Name = "btn" + i.ToString() + j.ToString();
                    btnOX[i, j].Text = "";
                    btnOX[i, j].Width = 100;
                    btnOX[i, j].Height = 100;
                    btnOX[i, j].Font = new Font("Times New Roman", 24, FontStyle.Bold);
                    btnOX[i, j].Top = 15 + i * 100;
                    btnOX[i, j].Left = 15 + j * 100;
                    btnOX[i, j].Enabled = false;
                    btnOX[i, j].Click += new EventHandler(btn_click);
                }
            }
        }

        void btn_click(object sender,EventArgs e)
        {
            btn = (Button)sender;
            clickCount++;
            if (btn.Enabled)
            {
                btn.Enabled = false;
                if (clickCount % 2 == 1)
                {
                    btn.Text = "O";
                }
                else
                {
                    btn.Text = "X";
                }
                if (clickCount >= 5)
                {
                    int row = Int32.Parse(btn.Name.ToString().Substring(3, 1));
                    int col = Int32.Parse(btn.Name.ToString().Substring(3, 1));
                    checkBingo(row, col, clickCount);
                }
                whoPlayer++;
                whoPlayer %= 2;
                this.Text = "第 " + (whoPlayer + 1) + " 位玩家案OX位置";
            }           
        }

        void checkBingo(int row, int col, int clickCount)
        {
            int j = 0;
            for (j = 0; j < 2; j++)
            {
                if (btnOX[row, j].Text != btnOX[row, j + 1].Text)
                {
                    break;
                }
            }

            int i = 0;
            for (i = 0; i < 2; i++)
            {
                if (btnOX[i, col].Text != btnOX[i + 1, col].Text)
                {
                    break;
                }
            }

            int k = 0;
            if (row == col)
            {
                for (k = 0; k < 2; k++)
                {
                    if (btnOX[k, k].Text != btnOX[k + 1, k + 1].Text)
                    {
                        break;
                    }
                }
            }

            int p = 0;
            if ((row + col) == 2)
            {
                for (p = 0; p < 2; p++)
                {
                    if (btnOX[p, 2 - p].Text != btnOX[p + 1, 1 - p].Text)
                    {
                        break;
                    }
                }
            }

            if (j == 2 || i == 2 || k == 2 || p == 2 || clickCount == 9)
            {
                if (clickCount == 9)
                {
                    btnOX[row, col].Text = "";
                }
                if (j == 2 || i == 2 || k == 2 || p == 2)
                {
                    if (clickCount % 2 == 1)
                    {
                        MessageBox.Show("第一位玩家(O):獲勝.", "OX遊戲");
                    }
                    else
                    {
                        MessageBox.Show("第二位玩家(X):獲勝.", "OX遊戲");
                    }
                }
                else
                {
                    MessageBox.Show("可惜，沒輸沒贏.", "OX遊戲");
                }

                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        btnOX[i, j].Enabled = false;
                    }
                    btnStart.Enabled = true;
                }
            }
            else if (clickCount == 8)
            {
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        if (btnOX[i, j].Enabled)
                        {
                            goto ExitDoubleFor;
                        }
                    }
                }
            ExitDoubleFor:
                row = i;
                col = j;
                btnOX[i, j].Text = "O";
                checkBingo(row, col, 9);
            }
        }

        void btnStart_click(object sender, EventArgs e)
        {
            whoPlayer = 0;
            this.Text = "第 " + (whoPlayer + 1) + " 位玩家案OX位置";

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    btnOX[i,j].Enabled = true;
                    btnOX[i, j].Text = "";                
                }
                btnStart.Enabled = false;
                clickCount = 0;
            }
        }
    }
}
