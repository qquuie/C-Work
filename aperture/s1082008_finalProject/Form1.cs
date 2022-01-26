using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.DevIl;
using Tao.FreeGlut;

namespace s1082008_finalProject
{
    public partial class Form1 : Form
    {

        Camera cam = new Camera();
        uint[] texName = new uint[6]; //建立儲存紋理編號的陣列

        const double DEGREE_TO_RAD = 0.01745329; // 3.1415926/180
        bool level1 = false, level1_pass = false;
        bool level2_1 = false, level2_1_pass = false;
        bool level2_2 = false, level2_2_pass = false;
        bool level2_3 = false, level2_3_pass = false;

        string[] plot = new string[5] { "歡迎你的到來", "你現在處於\n一個時間夾縫", "必解開謎底\n才能逃脫", "否則將被\n困一輩子", "開始吧，\n祝你好運，人類" };
        int plot_num = 0;

        bool begin = false;

        float width = 50f;//房間寬度
        bool L_enable_1 = false, L_enable_2 = false, L_enable_3 = false, L_enable_4 = false;//控制燈光暗

        public Form1()
        {
            InitializeComponent();
            this.openGLControl1.InitializeContexts();
            Glut.glutInit();

            Il.ilInit();
            Ilu.iluInit();
        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {
            cam.SetViewVolume(45, openGLControl1.Size.Width, openGLControl1.Size.Height, 0.1, 100.0);
            MyInit();
        }


        private void MyInit()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);//清除畫布
            Gl.glClearDepth(1.0);//判別物體遠近
            Gl.glEnable(Gl.GL_DEPTH_TEST);//深度測試

            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);//開啟第0個
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_LIGHT2);
            Gl.glEnable(Gl.GL_LIGHT3);
            Gl.glEnable(Gl.GL_LIGHT4);

            float[] global_ambient = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };

            float[] light0_ambient = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            float[] light0_diffuse = new float[] { 0.25f, 0.25f, 0.25f, 1.0f };
            float[] light0_specular = new float[] { 0.9f, 0.9f, 0.9f, 1.0f };

            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER, Gl.GL_TRUE);
            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, global_ambient);

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, light0_ambient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, light0_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, light0_specular);

            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, light0_ambient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, light0_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPECULAR, light0_specular);

            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_AMBIENT, light0_ambient);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_DIFFUSE, light0_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_SPECULAR, light0_specular);

            Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_AMBIENT, light0_ambient);
            Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_DIFFUSE, light0_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_SPECULAR, light0_specular);

            Gl.glLightfv(Gl.GL_LIGHT4, Gl.GL_AMBIENT, light0_ambient);
            Gl.glLightfv(Gl.GL_LIGHT4, Gl.GL_DIFFUSE, light0_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT4, Gl.GL_SPECULAR, light0_specular);

            Gl.glEnable(Gl.GL_NORMALIZE);//縮放時會導致法向量一起做縮放，所以須加這行
            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE, Gl.GL_TRUE);

            Gl.glGenTextures(6, texName); //產生紋理物件
            //"@加絕對路徑"
            LoadTexture("door1.jpg", texName[0]);
            LoadTexture("door2.jpg", texName[1]);
            LoadTexture("door3.jpg", texName[2]);

            //設定紋理環境參數
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);//設定環境參數//與當時環境光影變數相同

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            cam.SetPosition(0, width / 2, 0);
            cam.SetDirection(-0.5, 0, 0);

            btn_begin1.Visible = false;
            btn_begin2.Visible = false;
            txt_level.Visible = false;
            txt_x.Visible = txt_z.Visible = txt_change_x.Visible = txt_change_z.Visible = false;
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (e.Control) cam.HSlide(-1);
                    else if (e.Alt) cam.Roll(1.0);
                    else cam.Pan(5.0);
                    break;
                case Keys.Right:
                    if (e.Control) cam.HSlide(1);
                    else if (e.Alt) cam.Roll(-1.0);
                    else cam.Pan(-5.0);
                    break;
                case Keys.Up:
                    if (e.Control) cam.VSlide(2);
                    else cam.Tilt(2.0);
                    break;
                case Keys.Down:
                    if (e.Control) cam.VSlide(-2);
                    else cam.Tilt(-2.0);
                    break;
                case Keys.PageUp:
                    cam.Slide(0.5);
                    break;
                case Keys.PageDown:
                    cam.Slide(-0.5);
                    break;
                case Keys.E:
                    if (level2_1)
                    {
                        second++;
                        if (second > 60)
                        {
                            miniute++;
                            second %= 60;
                        }
                    }
                    if (level2_3)
                    {
                        cubeZ++;
                    }                    
                    break;
                case Keys.Q:
                    if (level2_1)
                    {
                        hour++;
                        if (hour > 12)
                        {
                            hour %= 12;
                        }
                    }
                    if (level2_3)
                    {
                        cubeX++;
                    }
                    break;
                case Keys.A:
                    if (level2_1)
                    {
                        hour--;
                        if (hour < 1)
                        {
                            hour += 12;
                        }
                    }
                    if (level2_3)
                    {
                        cubeX--;
                    }
                    break;
                case Keys.W:
                    if (level2_1)
                    {
                        miniute++;
                        if (miniute > 60)
                        {
                            hour++;
                            miniute %= 60;
                        }
                    }
                    if (level2_3)
                    {
                        cubeY++;
                    }
                    break;
                case Keys.S:
                    if (level2_1)
                    {
                        miniute--;
                        if (miniute < 1)
                        {
                            hour--;
                            miniute += 60;
                        }
                    }
                    if (level2_3)
                    {
                        cubeY--;
                    }
                    break;
                case Keys.D:
                    if (level2_1)
                    {
                        second--;
                        if (second < 1)
                        {
                            miniute--;
                            second += 60;
                        }
                    }
                    if (level2_3)
                    {
                        cubeZ--;
                    }
                    break;
                case Keys.R:
                    if (level2_1_pass|| level2_2_pass)
                    {
                        begin = true;
                        level1_pass = true;
                        level2_1= level2_2 = false;
                        for (int i = 1; i <= 9; i++)
                        {
                            Buttons[i].Visible = false; //隨機設定每個按鍵的狀態 (0代表關，1代表開)
                        }
                    }            
                    break;
                case Keys.T:
                    if ((level_enter_2_1 && !level2_1_pass) || (level_enter_2_2 && !level2_2_pass) || (level_enter_2_3 && !level2_3_pass))
                    {
                        begin = false;
                        level1_pass = false;
                        if (level_enter_2_1)
                        {
                            level2_1 = true;
                            guess_h = rn.Next(0, 12);
                            guess_m = rn.Next(0, 60);
                            guess_s = rn.Next(0, 60);
                            txt_level.Text = "請控制指針，到系統指定的數字，\nQ & A :時鐘\nW & S :分鐘，\nE & D :秒針";
                        }
                        if (level_enter_2_2)
                        {
                            level2_2 = true;
                            for (int i = 1; i <= 9; i++)
                            {
                                Buttons[i].Visible = true; //隨機設定每個按鍵的狀態 (0代表關，1代表開)
                            }
                            txt_level.Text = "請透過按鍵使僅有中間為線狀";
                        }

                        if (level_enter_2_3)
                        {
                            level2_3 = true;
                            cubeX = cubeY = cubeZ = 0;
                            guessX = rn.Next(-30, 31);
                            guessY = rn.Next(-25, 25);
                            guessZ = rn.Next(-35, 35);
                            Debug.WriteLine("274 ",guessX + " " + guessY + " " + guessZ);
                            txt_level.Text = "請控制方塊，到達系統指定的數字，\nQ & A :X軸\nW & S :Y軸\nE & D :Z軸";
                        }
                    }
                    break;
                case Keys.Z:
                    begin = true;
                    txt_plot.Visible = btn_begin1.Visible = btn_begin2.Visible = false;
                    txt_time.Enabled = false;
                    level1 = true;
                    txt_level.Visible = true;
                    txt_x.Visible = txt_z.Visible = txt_change_x.Visible = txt_change_z.Visible = true;
                    break;
                case Keys.X:
                    L_enable_1 = L_enable_2 = L_enable_3 = L_enable_4 = true;
                    break;
                case Keys.C:
                    begin = false;
                    level1_pass = false;
                    level2_1 = true;
                    txt_level.Text = "請控制指針，道系統指定的數字，\nQ & A :時鐘\nW & S :分鐘，\nE & D :秒針";
                    guess_h = rn.Next(0, 13);
                    guess_m = rn.Next(0, 60);
                    guess_s = rn.Next(0, 60);
                    break;
                case Keys.V:
                    hour = (int)guess_h;
                    miniute = (int)guess_m;
                    second = (int)guess_s;
                    break;
                case Keys.B:
                    begin = false;
                    level1_pass = false;
                    level2_2 = true;
                    txt_level.Text = "請透過按鍵使僅有中間為線狀";
                    for (int i = 1; i <= 9; i++)
                    {
                        Buttons[i].Visible = true; //無效化所有的數字鍵
                    }
                    break;
                case Keys.N:
                    btnState[1] = btnState[2] = btnState[3] = btnState[4] = btnState[6] = btnState[7] = btnState[8] = btnState[9] = 1;
                    btnState[5] = 0;
                    txt_level.Text = "恭喜你答對了，按R回去";
                    level2_2_pass = true;
                    for (int i = 1; i <= 9; i++)
                    {
                        Buttons[i].Visible = false; //無效化所有的數字鍵
                    }
                    break;
                default:
                    break;

            }
            if (level1)
            {
                if ((cam.px > 8 && cam.px < 12) && (cam.pz > 8 && cam.pz < 12))
                {
                    L_enable_1 = true;
                }
                if ((cam.px > 8 && cam.px < 12) && (cam.pz < -8 && cam.pz > -12))
                {
                    L_enable_2 = true;
                }
                if ((cam.px < -8 && cam.px > -12) && (cam.pz > 8 && cam.pz < 12))
                {
                    L_enable_3 = true;
                }
                if ((cam.px < -8 && cam.px > -12) && (cam.pz < -8 && cam.pz > -12))
                {
                    L_enable_4 = true;
                }
                txt_change_x.Text = cam.px.ToString();
                txt_change_z.Text = cam.pz.ToString();
                if (L_enable_1 && L_enable_2 && L_enable_3 && L_enable_4)
                {
                    txt_level.Text = "恭喜你通過，你可以前往其他房間執行下一個任務";
                    level1_pass = true;
                    txt_x.Visible = txt_z.Visible = txt_change_x.Visible = txt_change_z.Visible = false;
                    level1 = false;
                }
            }
            if (level2_3)
            {
                if ((Math.Abs(cubeX - guessX) + Math.Abs(cubeY - guessY) + Math.Abs(cubeZ - guessZ)) < 10)
                {
                    txt_level.Text = "恭喜你答對了，按R回去";
                    level2_3_pass = true;
                }
            }
            if (level1_pass)
            {
                level_enter_2_1 = level_enter_2_2 = level_enter_2_3 = false;
                if ((cam.px < -8 && cam.px > -12) && (cam.pz < 2 && cam.pz > -2))
                {
                    txt_level.Text = "是否進入時間領域，按T進入";
                    level_enter_2_1 = true;
                }
                else if ((cam.px < 4 && cam.px > -4) && (cam.pz < 12 && cam.pz > 8))
                {
                    txt_level.Text = "是否進入虛實領域，按T進入";
                    level_enter_2_2 = true;
                }
                else if ((cam.px < 4 && cam.px > -4) && (cam.pz < -8 && cam.pz > -12))
                {
                    txt_level.Text = "是否進入空間領域，按T進入";
                    level_enter_2_3 = true;
                }
                else
                {
                    txt_level.Text = "恭喜你通過，你可以前往其他房間執行下一個任務";
                }
            }
            if (level2_3_pass && level2_2_pass && level2_1_pass)
            {
                level2_3 = false;
                txt_level.Text = "恭喜你通關了";
                begin = false;
            }
            this.openGLControl1.Refresh();
        }

        bool level_enter_2_1, level_enter_2_2, level_enter_2_3;

        private void txt_time_Tick(object sender, EventArgs e)
        {
            plot_num++;
            if (plot_num == 5)
            {
                txt_time.Enabled = false;
                txt_plot.Visible = false;
                btn_begin1.Visible = true;
                btn_begin2.Visible = true;
            }
            else
            {
                txt_plot.Text = plot[plot_num];
            }
        }


        private void openGLControl1_Paint(object sender, PaintEventArgs e)
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);//清除畫布

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            cam.LookAt();

            if (begin)
            {
                light();
                room();
                if (level1_pass)
                {
                    door1();
                    door2();
                    door3();
                }
            }

            if (level2_1)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(-13, width / 2, 0.0);
                Gl.glRotated(90.0, 1.0, 0.0, 0.0);
                clock();
                Gl.glPopMatrix();
            }

            if (level2_2)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(0, 0, 10.0);
                Gl.glRotated(-90.0, 0, -1, 0);//調到另一個方向
                level2_2c();
                Gl.glPopMatrix();
            }


            if (level2_3)
            {
                Gl.glPushMatrix();
                Gl.glTranslated(cubeX, cubeY, cubeZ);
                Debug.WriteLine("452 " + cubeX + " " + cubeY + " " + cubeZ);
                level_2_3_cube();
                Gl.glPopMatrix();
            }
        }

        int cubeX, cubeY, cubeZ;
        int guessX, guessY, guessZ;
        void level_2_3_cube()
        {
            float r = (float)(Math.Abs(cubeX - guessX) + Math.Abs(cubeY - guessY) + Math.Abs(cubeZ - guessZ)) / 60 * 255;
            float g = (float)(1-(Math.Abs(cubeX - guessX) + Math.Abs(cubeY - guessY) + Math.Abs(cubeZ - guessZ)) / 60) * 255;
            Debug.WriteLine("466 " + r + " " + g);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glColor3ub((byte)r, (byte)g, 100);
            Gl.glPushMatrix();
            Gl.glRotated(-90.0, 0, -1, 0);//調到另一個方向
            Gl.glTranslated(60, width / 2, 0.0);
            Glut.glutSolidCube(10);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_COLOR_MATERIAL);
        }

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

        void level2_2c()
        {
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2 + 5, 5.0);
            if (btnState[1] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2 + 5, 0.0);
            if (btnState[2] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2 + 5, -5.0);
            if (btnState[3] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2, 5.0);
            if (btnState[4] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2, 0.0);
            if (btnState[5] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2, -5.0);
            if (btnState[6] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2 - 5, 5.0);
            if (btnState[7] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2 - 5, 0.0);
            if (btnState[8] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
            Gl.glPushMatrix();
            Gl.glTranslated(-13, width / 2 - 5, -5.0);
            if (btnState[9] == 1)
            {
                Glut.glutSolidSphere(1.5, 20, 20);
            }
            else
            {
                Glut.glutWireSphere(1.5, 20, 20);
            }
            Gl.glPopMatrix();
        }

        void door1()
        {
            Gl.glColor3ub(255, 255, 255); //將背景設為白底，以免背景顏色影響海報的顏色
            Gl.glEnable(Gl.GL_TEXTURE_2D); //開啟紋理映射功能
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texName[0]); //連結紋理物件

            Gl.glPushMatrix();
            Gl.glTranslated(-width / 2 + 0.5, width / 4, 0.0);
            Gl.glScaled(0.2, width / 2, width / 4);
            Gl.glRotated(90.0, 1, 0, 0);
            Gl.glRotated(-90.0, 0, 0, 1);
            Gl.glTranslated(-0.5, 0.0, -0.5);
            plane(100);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D); //關閉紋理映射功能
        }

        void door2()
        {
            Gl.glColor3ub(255, 255, 255); //將背景設為白底，以免背景顏色影響海報的顏色
            Gl.glEnable(Gl.GL_TEXTURE_2D); //開啟紋理映射功能
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texName[1]); //連結紋理物件

            Gl.glPushMatrix();
            Gl.glTranslated(0, width / 4, width / 2 - 0.5);
            Gl.glRotated(-90.0, 0, -1, 0);//調到另一個方向
            Gl.glRotated(90.0, 1, 0, 0);
            Gl.glRotated(-90.0, 0, 0, 1);
            Gl.glScaled(width / 4, 0.2, width / 2);
            Gl.glTranslated(-0.5, 0.0, -0.5);

            plane(100);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D); //關閉紋理映射功能
        }

        void door3()
        {
            Gl.glColor3ub(255, 255, 255); //將背景設為白底，以免背景顏色影響海報的顏色
            Gl.glEnable(Gl.GL_TEXTURE_2D); //開啟紋理映射功能
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texName[2]); //連結紋理物件

            Gl.glPushMatrix();
            Gl.glTranslated(0, width / 4, -width / 2 + 0.5);
            Gl.glRotated(-90.0, 0, -1, 0);//調到另一個方向
            Gl.glRotated(90.0, 1, 0, 0);
            Gl.glRotated(-90.0, 0, 0, 1);
            Gl.glScaled(width / 4, 0.2, width / 2);
            Gl.glTranslated(-0.5, 0.0, -0.5);

            plane(100);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D); //關閉紋理映射功能
        }

        void light()
        {
            float[] light0_position = new float[] { 0.0f, 0.0f, 0.0f, 1.0f };
            float[] light0_direction = new float[] { 0.0f, -1.0f, 0.0f };

            Gl.glPushMatrix();
            Gl.glTranslated(0, width - 2, 0);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0_position);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, light0_direction);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(width / 4, width - 1, width / 4);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, light0_position);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_SPOT_DIRECTION, light0_direction);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(width / 4, width - 1, -width / 4);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_POSITION, light0_position);
            Gl.glLightfv(Gl.GL_LIGHT2, Gl.GL_SPOT_DIRECTION, light0_direction);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-width / 4, width - 1, width / 4);
            Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_POSITION, light0_position);
            Gl.glLightfv(Gl.GL_LIGHT3, Gl.GL_SPOT_DIRECTION, light0_direction);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslated(-width / 4, width - 1, -width / 4);
            Gl.glLightfv(Gl.GL_LIGHT4, Gl.GL_POSITION, light0_position);
            Gl.glLightfv(Gl.GL_LIGHT4, Gl.GL_SPOT_DIRECTION, light0_direction);
            Gl.glPopMatrix();

            Gl.glDisable(Gl.GL_LIGHT1);
            Gl.glDisable(Gl.GL_LIGHT2);
            Gl.glDisable(Gl.GL_LIGHT3);
            Gl.glDisable(Gl.GL_LIGHT4);
            if (L_enable_1 == true)
            {
                Gl.glEnable(Gl.GL_LIGHT1);
            }
            if (L_enable_2 == true)
            {
                Gl.glEnable(Gl.GL_LIGHT2);
            }
            if (L_enable_3 == true)
            {
                Gl.glEnable(Gl.GL_LIGHT3);
            }
            if (L_enable_4 == true)
            {
                Gl.glEnable(Gl.GL_LIGHT4);
            }
        }

        private void begin_Click(object sender, EventArgs e)
        {
            begin = true;
            this.openGLControl1.Refresh();
            btn_begin1.Visible = false;
            btn_begin2.Visible = false;
            level1 = true;
            txt_level.Visible = true;
            txt_x.Visible = txt_z.Visible = txt_change_x.Visible = txt_change_z.Visible = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            {
                btnState[i] = rn.Next(0, 2); //隨機設定每個按鍵的狀態 (0代表關，1代表開)
            }

            for (int i = 1; i <= 9; i++)
            {
                Buttons[i].Visible = false; //隨機設定每個按鍵的狀態 (0代表關，1代表開)
            }

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
                    }
                    else
                    {
                        btnState[X] = 1;
                    }
                }
            }
            int sum = 0;
            for (int i = 1; i <= 9; i++)
                sum += btnState[i]; //將所有按鍵的狀態數值加起來
            if (sum == 8 && btnState[5] == 0) //若按鍵數值總和為8同時按鍵5的狀態值為0，表示已達過關條件
            {
                txt_level.Text = "恭喜你答對了，按R回去";
                level2_2_pass = true;     
                for (int i = 1; i <= 9; i++)
                {
                    Buttons[i].Enabled = false; //無效化所有的數字鍵
                }
            }
            this.openGLControl1.Refresh();

        }



        void room()
        {
            //底下
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glColor3ub(200, 200, 255);
            Gl.glPushMatrix();
            Gl.glScaled(width, 1.0, width);
            Gl.glTranslated(-0.5, 0.0, -0.5);//移置中心
            plane(100);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_COLOR_MATERIAL);

            Gl.glPushMatrix();
            Gl.glTranslated(0.0, width, 0);
            //Gl.glRotated(180, 1.0, 0.0, 0.0);
            Gl.glScaled(width, 1.0, width);
            Gl.glTranslated(-0.5, 0.0, -0.5);//移置中心
            plane(100);
            Gl.glPopMatrix();

            //牆
            Gl.glColor3ub(200, 200, 255);
            Gl.glPushMatrix();
            Gl.glTranslated(0.0, width / 2, -width / 2);
            Gl.glRotated(180, 0.0, 1.0, 0.0);
            Gl.glRotated(90, -1.0, 0.0, 0.0);
            Gl.glScaled(width, 1.0, width);
            Gl.glTranslated(-0.5, 0.0, -0.5);
            plane(100);
            Gl.glPopMatrix();

            Gl.glColor3ub(200, 200, 255);
            Gl.glPushMatrix();
            Gl.glTranslated(0.0, width / 2, width / 2);
            Gl.glRotated(90, -1.0, 0.0, 0.0);
            Gl.glScaled(width, 1.0, width);
            Gl.glTranslated(-0.5, 0.0, -0.5);
            plane(100);
            Gl.glPopMatrix();

            Gl.glColor3ub(200, 200, 255);
            Gl.glPushMatrix();
            Gl.glTranslated(-width / 2, width / 2, 0);
            Gl.glRotated(90, 0.0, 0.0, -1.0);
            Gl.glScaled(width, 1.0, width);
            Gl.glTranslated(-0.5, 0.0, -0.5);
            plane(100);
            Gl.glPopMatrix();

            Gl.glColor3ub(200, 200, 255);
            Gl.glPushMatrix();
            Gl.glTranslated(width / 2, width / 2, 0);
            Gl.glRotated(180, 0.0, 1.0, 0.0);
            Gl.glRotated(90, 0.0, 0.0, -1.0);
            Gl.glScaled(width, 1.0, width);
            Gl.glTranslated(-0.5, 0.0, -0.5);
            plane(100);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_COLOR_MATERIAL);
        }

        void plane(int Slices)
        {
            double dx = 1.0 / Slices;
            double dz = 1.0 / Slices;

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(0.0, 1.0, 0.0);
            for (double x = 0; x < 1.0; x += dx)
            {
                for (double z = 0; z < 1.0; z += dz)
                {
                    Gl.glTexCoord2d(x, z);
                    Gl.glVertex3d(x, 0.0, z);
                    Gl.glTexCoord2d(x, z + dz);
                    Gl.glVertex3d(x, 0.0, z + dz);
                    Gl.glTexCoord2d(x + dx, z + dz);
                    Gl.glVertex3d(x + dx, 0.0, z + dz);
                    Gl.glTexCoord2d(x + dx, z);
                    Gl.glVertex3d(x + dx, 0.0, z);
                }
            }
            Gl.glEnd();
        }

        int hour = DateTime.Now.Hour;
        int miniute = DateTime.Now.Minute;
        int second = DateTime.Now.Second;

        Random rn = new Random();
        float guess_h, guess_m, guess_s;
        void clock()
        {
            //時鐘外圓
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glColor3ub(180, 180, 250);
            Gl.glPushMatrix();
            Gl.glRotated(90.0, 0.0, 1.0, 0.0);
            Glut.glutSolidTorus(0.08, 1.5, 100, 100);
            Gl.glPopMatrix();

            float dish, dism, diss;
            if (hour > guess_h)
            {
                dish = hour - guess_h;
                if (dish > 6)
                {
                    dish = guess_s + 12 - hour;
                }
            }
            else
            {
                dish = guess_h - hour;
                if (dish > 6)
                {
                    dish = hour + 12 - guess_h;
                }
            }

            if (miniute > guess_m)
            {
                dism = miniute - guess_m;
                if (dism > 30)
                {
                    dism = guess_m + 60 - miniute;
                }
            }
            else
            {
                dism = guess_m - miniute;
                if (dism > 30)
                {
                    dism = miniute + 60 - guess_m;
                }
            }

            if (second > guess_s)
            {
                diss = second - guess_s;
                if (diss > 30)
                {
                    diss = guess_s + 60 - second;
                }
            }
            else
            {
                diss = guess_s - second;
                if (diss > 30)
                {
                    diss = second + 65 - guess_s;
                }
            }

            Debug.WriteLine("931 "+dish.ToString() + " " + dism.ToString() + " " + diss.ToString());

            if (dish + dism + diss <5)
            {
                txt_level.Text = "恭喜你答對了，按R回去";
                level2_1_pass = true;
            }

            float r = (dish + dism + diss) / 66 * 255;
            float g = (1 - (dish + dism + diss) / 66) * 255;

            //鐘面
            Gl.glColor3ub((byte)r, (byte)g, 0);
            Gl.glPushMatrix();
            Gl.glScaled(0.02, 1.0, 1.0);
            Glut.glutSolidSphere(1.5, 16, 16);
            Gl.glPopMatrix();

            //秒針
            Gl.glColor3ub(0, 0, 0);
            Gl.glPushMatrix();
            Gl.glRotated(second * 6, -1.0, 0.0, 0.0);
            Gl.glTranslated(0.1, 0.6, 0.0);
            Gl.glScaled(0.04, 1.2, 0.02);
            Glut.glutSolidCube(1);
            Gl.glPopMatrix();

            //分針
            Gl.glColor3ub(0, 0, 0);
            Gl.glPushMatrix();
            Gl.glRotated(miniute * 6, -1.0, 0.0, 0.0);
            Gl.glTranslated(0.1, 0.6, 0.0);
            Gl.glScaled(0.04, 1.2, 0.05);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();

            //時針
            Gl.glColor3ub(0, 0, 0);
            Gl.glPushMatrix();
            Gl.glRotated(hour * 30 + miniute / 2, -1.0, 0.0, 0.0);
            Gl.glTranslated(0.1, 0.4, 0.0);
            Gl.glScaled(0.04, 0.8, 0.05);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();

            for (int i = 0; i < 12; i++)
            {
                scale(i, 0.15f, 30);
            }
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 != 0)
                {
                    scale(i, 0.08f, 6);
                }
            }

        }

        void scale(int time, float len, int space)
        {
            Gl.glColor3ub(200, 250, 250);
            Gl.glPushMatrix();
            Gl.glRotated(space * time, -1.0, 0.0, 0.0);
            Gl.glTranslated(0.08, 1.3, 0.0);
            Gl.glScaled(0.04, len, 0.05);
            Glut.glutSolidCube(1.0);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_COLOR_MATERIAL);
        }

        private void openGLControl1_Resize(object sender, EventArgs e)
        {
            cam.SetViewVolume(45, openGLControl1.Size.Width, openGLControl1.Size.Height, 0.1, 20.0);
        }

        private void LoadTexture(string filename, uint texture)//檔案，跟存到哪個紋理物件裡面
        {
            if (Il.ilLoadImage(filename)) //載入影像檔
            {
                int BitsPerPixel = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL); //取得儲存每個像素的位元數
                int Depth = Il.ilGetInteger(Il.IL_IMAGE_DEPTH);
                Ilu.iluScale(512, 512, Depth);
                Ilu.iluFlipImage(); //顛倒影像
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture); //連結紋理物件
                                                             //設定紋理參數
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                //建立紋理物件
                if (BitsPerPixel == 24) Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, 512, 512, 0,
                 Il.ilGetInteger(Il.IL_IMAGE_FORMAT), Il.ilGetInteger(Il.IL_IMAGE_TYPE), Il.ilGetData());
                if (BitsPerPixel == 32) Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, 512, 512, 0,
                 Il.ilGetInteger(Il.IL_IMAGE_FORMAT), Il.ilGetInteger(Il.IL_IMAGE_TYPE), Il.ilGetData());
                //Gl.glGenerateMipmap(Gl.GL_TEXTURE_2D);
            }
            else
            {   // 若檔案無法開啟，顯示錯誤訊息
                string message = "Cannot open file " + filename + ".";
                MessageBox.Show(message, "Image file open error!!!", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

        }
    }
}
