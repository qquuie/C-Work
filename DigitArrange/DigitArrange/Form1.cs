using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitArrange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int firstBtnLeft, firstBtnTop;
        int secondBtnLeft, secondBtnTop;
        string firstBtnText, secondBtnText;

        int clickBtnCount = 0;
        bool PressBtnBlank = false;
        Button btn1, btn2;

        
        Button[,] BtnDigit = new Button[3, 3];

        Panel PnlPlatter = new Panel();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            this.Controls.Add(PnlPlatter);

            PnlPlatter.Top = 15;
            PnlPlatter.Left = 15;
            PnlPlatter.Width = 330;
            PnlPlatter.Height = 330;
            PnlPlatter.BackColor = Color.Aqua;
            PnlPlatter.Enabled = false;

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    BtnDigit[i, j] = new Button();

                    PnlPlatter.Controls.Add(BtnDigit[i, j]);

                    BtnDigit[i, j].Name = "btn" + i.ToString() + j.ToString();
                    BtnDigit[i, j].Text = "";
                    BtnDigit[i, j].Width = 100;
                    BtnDigit[i, j].Height = 100;
                    BtnDigit[i, j].Font = new Font("Times New Roman", 24, FontStyle.Bold);
                    BtnDigit[i, j].Top = 15 + i * 100;
                    BtnDigit[i, j].Left = 15 + j * 100;
                    BtnDigit[i, j].BackColor=Color.White;

                    BtnDigit[i, j].Click += new EventHandler(btn_click);
                }
            }

        }

        void btn_click(object sender,EventArgs e)
        {
            clickBtnCount++;
            if (clickBtnCount == 1)
            {
                btn1 = (Button)sender;
                firstBtnLeft = btn1.Left;
                firstBtnTop = btn1.Top;
                firstBtnText = btn1.Text;
                if (btn1.Text == "")
                {
                    PressBtnBlank = true;
                }
            }else
            {
                btn2 = (Button)sender;
                secondBtnLeft = btn2.Left;
                secondBtnTop = btn2.Top;
                secondBtnText = btn2.Text;
                if (btn2.Text == "")
                {
                    PressBtnBlank = true;
                }

                if (PressBtnBlank)
                {
                    if (firstBtnLeft == secondBtnLeft &&
                        Math.Abs(firstBtnTop-secondBtnTop)==btn1.Height||
                        firstBtnTop==secondBtnTop&&
                        Math.Abs(firstBtnLeft - secondBtnLeft) == btn1.Width)
                    {
                        btn1.Text = secondBtnText;
                        btn2.Text = firstBtnText;
                    }
                }

                clickBtnCount = 0;
                PressBtnBlank = false;
                int i, j;

                for(i = 0; i < 3; i++)
                {
                    for(j = 0;j < 3; j++)
                    {
                        if (i == 2 && j == 2)
                        {
                            if (BtnDigit[2, 2].Text != "")
                                goto ExitDoubleFor;
                        }
                        else
                        {
                            if (BtnDigit[i, j].Text != (3 * i +j + 1).ToString())
                            {
                                goto ExitDoubleFor;
                            }
                        }
                    }
                }
                ExitDoubleFor:
                if (i == 3)
                {
                    MessageBox.Show("恭喜過關了", "遊戲結束");
                    PnlPlatter.Enabled = false;
                    btnStart.Enabled = true;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            PnlPlatter.Enabled = true;
            Random rd = new Random();
            int[] num = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int index;
            int count = 8;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (i == 2 & j == 2)
                    {
                        break;
                    }
                    index = rd.Next(count);
                    BtnDigit[i, j].Text = num[index].ToString();
                    count--;

                    num[index] = num[count];
                }
            }
            BtnDigit[2, 2].Text = "";
            btnStart.Enabled = false;
        }

    }
}
