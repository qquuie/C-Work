using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace letter_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Color onColor = Color.Coral; // 將onColor的顏色設為咖啡色
        Color offColor = Color.Yellow; // 將offColor的顏色設為黃色
        int seconds, count; //紀錄遊戲時間以及按鍵的次數
        Button[] Buttons = new Button[10]; //儲存按鈕的陣列(陣列第一個元素沒有用到)
        int[] btnState = new int[10];   //紀錄每個按鈕的狀態(陣列第一個元素沒有用到)
        int[,] ChangeCells = {{-1,-1,-1,-1,-1},  //第一列是無用的數值
                       {1, 2, 4, 5, -1},  //按鈕1影響按鈕1, 2, 4, 5
	                   {2, 1, 3, -1, -1}, //按鈕2影響按鈕2, 1, 3
	                   {3, 2, 5, 6, -1},  //按鈕3影響按鈕3, 2, 5, 6
	                   {4, 1, 7, -1, -1}, //按鈕4影響按鈕4, 1, 7
	                   {5, 2, 4, 6, 8},   //按鈕5影響按鈕5, 2, 4, 6, 8
	                   {6, 3, 9, -1, -1}, //按鈕6影響按鈕6, 3, 9
	                   {7, 4, 5, 8, -1},  //按鈕7影響按鈕7, 4, 5, 8
	                   {8, 7, 9, -1, -1}, //按鈕8影響按鈕8, 7, 9
	                   {9, 5, 6, 8, -1},}; //按鈕9影響按鈕9, 5, 6, 8

        private void Form1_Load(object sender, EventArgs e)
        {
            //將建立的按鈕依序存在按鈕陣列中
            Buttons[1] = button1;
            Buttons[2] = button2;
            Buttons[3] = button3;
            Buttons[4] = button4;
            Buttons[5] = button5;
            Buttons[6] = button6;
            Buttons[7] = button7;
            Buttons[8] = button8;
            Buttons[9] = button9;

            for (int i = 1; i <= 9; i++)
                Buttons[i].Enabled = false; //讓程式剛啟動時，所有的數字鈕都處於無效的狀態
            btnBegin.Enabled = true; //開始鈕可正常運作
            btnEnd.Enabled = false; //結束紐處於無效狀態
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++; //紀錄的秒數加1
            lblSecound.Text = "秒數:" +seconds.ToString() + "秒"; //顯示目前經過的秒數
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = true; //有效化開始鍵
            btnEnd.Enabled = false; //無效化結束鍵
            timer1.Enabled = false; //停止計時器
            for (int i = 1; i <= 9; i++)
            {
                Buttons[i].BackColor = Color.LightGray; //將所有按鍵的顏色設為淡灰色
                Buttons[i].Enabled = false; //無效化所有的數字鍵
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {

            seconds = count = 0; //將秒數與按鍵次數都重設為0
            lblTime.Text = "次數:" + count.ToString() + "次"; //顯示按下的次數
            lblSecound.Text = "秒數:" + seconds.ToString() + "秒"; //顯示經過的秒數
            Random rn = new Random(); //建立亂數產生器
            for (int i = 1; i <= 9; i++)
            {
                Buttons[i].Enabled = true; //將每個數字鍵設成可用狀態
                btnState[i] = rn.Next(0, 2); //隨機設定每個按鍵的狀態 (0代表關，1代表開)
                if (btnState[i] == 1)
                    Buttons[i].BackColor = onColor; //若按鍵狀態為開則按鍵顯示咖啡色
                else
                    Buttons[i].BackColor = offColor; //若按鍵狀態為開則按鍵顯示黃色
            }
            timer1.Enabled = true; //開始計時
            btnBegin.Enabled = false; //將開始鍵無效化
            btnEnd.Enabled = true; //有效化結束鍵
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button_Click(sender, e);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btnHit = (Button)sender; //取得目前按鍵的物件實體
            int Num = int.Parse(btnHit.Text);  //根據按鈕上的數字判定是第幾個按鍵
            for (int i = 0; i < 5; i++)  //檢查與此按鍵有關的其他按鍵
            {
                int X = ChangeCells[Num, i]; //從ChangeCells陣列取得按鍵編號
                if (X > 0) //若按鍵編號大於0，表示是正確的按鍵編號
                {
                    if (btnState[X] == 1) //若按鍵狀態為1
                    {
                        btnState[X] = 0;
                        Buttons[X].BackColor = offColor; //將按鍵狀態反相，並顛倒按鍵顏色
                    }
                    else
                    {
                        btnState[X] = 1;
                        Buttons[X].BackColor = onColor; //將按鍵狀態反相，並顛倒按鍵顏色
                    }
                }
            }
            count++; //按鍵次數加1
            lblTime.Text = "次數:" + count.ToString() + "次"; //顯示目前總共按了幾次按鍵
                                                           // 判別是否達到過關條件
            int sum = 0;
            for (int i = 1; i <= 9; i++)
                sum += btnState[i]; //將所有按鍵的狀態數值加起來
            if (sum == 8 && btnState[5] == 0) //若按鍵數值總和為8同時按鍵5的狀態值為0，表示已達過關條件
            {
                btnBegin.Enabled = true; //有效化開始鍵
                btnEnd.Enabled = false; //無效化結束鍵
                timer1.Enabled = false; //停止計時器
                MessageBox.Show("恭喜你過關了!", "九宮格遊戲"); //顯示過關訊息
                for (int i = 1; i <= 9; i++) Buttons[i].Enabled = false; //無效化所有的數字按鍵
            }
        }
    }
}
