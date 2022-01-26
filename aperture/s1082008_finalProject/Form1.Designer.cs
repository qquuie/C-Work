namespace s1082008_finalProject
{
    partial class Form1
    {
        public class MyOpenGLControl : Tao.Platform.Windows.OpenGLControl
        {
            protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
            {
                if (keyData == System.Windows.Forms.Keys.Left ||
                    keyData == System.Windows.Forms.Keys.Right ||
                    keyData == System.Windows.Forms.Keys.Up ||
                    keyData == System.Windows.Forms.Keys.Down)
                    return true;
                return base.IsInputKey(keyData);
            }
        }

        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openGLControl1 = new s1082008_finalProject.Form1.MyOpenGLControl();
            this.txt_plot = new System.Windows.Forms.Label();
            this.txt_time = new System.Windows.Forms.Timer(this.components);
            this.btn_begin1 = new System.Windows.Forms.Button();
            this.btn_begin2 = new System.Windows.Forms.Button();
            this.txt_x = new System.Windows.Forms.Label();
            this.txt_z = new System.Windows.Forms.Label();
            this.txt_change_x = new System.Windows.Forms.Label();
            this.txt_change_z = new System.Windows.Forms.Label();
            this.txt_level = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.AccumBits = ((byte)(0));
            this.openGLControl1.AutoCheckErrors = false;
            this.openGLControl1.AutoFinish = false;
            this.openGLControl1.AutoMakeCurrent = true;
            this.openGLControl1.AutoSwapBuffers = true;
            this.openGLControl1.BackColor = System.Drawing.Color.Black;
            this.openGLControl1.ColorBits = ((byte)(32));
            this.openGLControl1.DepthBits = ((byte)(16));
            this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.openGLControl1.Location = new System.Drawing.Point(0, 0);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.Size = new System.Drawing.Size(542, 409);
            this.openGLControl1.StencilBits = ((byte)(0));
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.Load += new System.EventHandler(this.openGLControl1_Load);
            this.openGLControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_Paint);
            this.openGLControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.openGLControl1_KeyDown);
            this.openGLControl1.Resize += new System.EventHandler(this.openGLControl1_Resize);
            // 
            // txt_plot
            // 
            this.txt_plot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_plot.AutoSize = true;
            this.txt_plot.Font = new System.Drawing.Font("標楷體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_plot.Location = new System.Drawing.Point(148, 500);
            this.txt_plot.Name = "txt_plot";
            this.txt_plot.Size = new System.Drawing.Size(263, 40);
            this.txt_plot.TabIndex = 1;
            this.txt_plot.Text = "歡迎你的到來";
            this.txt_plot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_time
            // 
            this.txt_time.Enabled = true;
            this.txt_time.Interval = 3000;
            this.txt_time.Tick += new System.EventHandler(this.txt_time_Tick);
            // 
            // btn_begin1
            // 
            this.btn_begin1.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_begin1.Location = new System.Drawing.Point(107, 500);
            this.btn_begin1.Name = "btn_begin1";
            this.btn_begin1.Size = new System.Drawing.Size(130, 40);
            this.btn_begin1.TabIndex = 2;
            this.btn_begin1.Text = "毅然前往";
            this.btn_begin1.UseVisualStyleBackColor = true;
            this.btn_begin1.Click += new System.EventHandler(this.begin_Click);
            // 
            // btn_begin2
            // 
            this.btn_begin2.Font = new System.Drawing.Font("標楷體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_begin2.Location = new System.Drawing.Point(317, 500);
            this.btn_begin2.Name = "btn_begin2";
            this.btn_begin2.Size = new System.Drawing.Size(130, 40);
            this.btn_begin2.TabIndex = 3;
            this.btn_begin2.Text = "被迫前往";
            this.btn_begin2.UseVisualStyleBackColor = true;
            this.btn_begin2.Click += new System.EventHandler(this.begin_Click);
            // 
            // txt_x
            // 
            this.txt_x.AutoSize = true;
            this.txt_x.Location = new System.Drawing.Point(341, 492);
            this.txt_x.Name = "txt_x";
            this.txt_x.Size = new System.Drawing.Size(18, 15);
            this.txt_x.TabIndex = 4;
            this.txt_x.Text = "x:";
            // 
            // txt_z
            // 
            this.txt_z.AutoSize = true;
            this.txt_z.Location = new System.Drawing.Point(341, 530);
            this.txt_z.Name = "txt_z";
            this.txt_z.Size = new System.Drawing.Size(17, 15);
            this.txt_z.TabIndex = 5;
            this.txt_z.Text = "z:";
            // 
            // txt_change_x
            // 
            this.txt_change_x.AutoSize = true;
            this.txt_change_x.Location = new System.Drawing.Point(370, 492);
            this.txt_change_x.Name = "txt_change_x";
            this.txt_change_x.Size = new System.Drawing.Size(14, 15);
            this.txt_change_x.TabIndex = 6;
            this.txt_change_x.Text = "0";
            // 
            // txt_change_z
            // 
            this.txt_change_z.AutoSize = true;
            this.txt_change_z.Location = new System.Drawing.Point(370, 530);
            this.txt_change_z.Name = "txt_change_z";
            this.txt_change_z.Size = new System.Drawing.Size(14, 15);
            this.txt_change_z.TabIndex = 7;
            this.txt_change_z.Text = "0";
            // 
            // txt_level
            // 
            this.txt_level.AutoSize = true;
            this.txt_level.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_level.Location = new System.Drawing.Point(24, 484);
            this.txt_level.Name = "txt_level";
            this.txt_level.Size = new System.Drawing.Size(260, 75);
            this.txt_level.TabIndex = 8;
            this.txt_level.Text = "請透過位置開啟所有的燈\r\nps:位置(0,0)開啟中央的燈，\r\n其他燈在中央與四個角中間";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 443);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 41);
            this.button1.TabIndex = 9;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(373, 443);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 41);
            this.button2.TabIndex = 10;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(420, 443);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(41, 41);
            this.button3.TabIndex = 11;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(326, 490);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(41, 41);
            this.button4.TabIndex = 14;
            this.button4.Text = "4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(373, 492);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(41, 41);
            this.button5.TabIndex = 13;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(420, 492);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(41, 41);
            this.button6.TabIndex = 12;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(326, 543);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(41, 41);
            this.button7.TabIndex = 15;
            this.button7.Text = "7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(373, 543);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(41, 41);
            this.button8.TabIndex = 16;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(420, 543);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(41, 41);
            this.button9.TabIndex = 17;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 658);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_level);
            this.Controls.Add(this.txt_change_z);
            this.Controls.Add(this.txt_change_x);
            this.Controls.Add(this.txt_z);
            this.Controls.Add(this.txt_x);
            this.Controls.Add(this.btn_begin2);
            this.Controls.Add(this.btn_begin1);
            this.Controls.Add(this.txt_plot);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyOpenGLControl openGLControl1;
        private System.Windows.Forms.Label txt_plot;
        private System.Windows.Forms.Timer txt_time;
        private System.Windows.Forms.Button btn_begin1;
        private System.Windows.Forms.Button btn_begin2;
        private System.Windows.Forms.Label txt_x;
        private System.Windows.Forms.Label txt_z;
        private System.Windows.Forms.Label txt_change_x;
        private System.Windows.Forms.Label txt_change_z;
        private System.Windows.Forms.Label txt_level;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
    }
}

