using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlTest;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        Graphics g;
        //  Brush Brush=new 
        Pen pen = new Pen(Color.Red, 5);
        Pen pen1 ;
        public Form1()
        {
            InitializeComponent();
           
            pen1 = new Pen(this.BackColor, 5);

           // this.Paint += Form1_Paint;
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
             g = this.CreateGraphics();
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            Bitmap bufferimage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bufferimage);
            g.Clear(this.BackColor);
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            foreach (var member in this.Controls)
            {
                ControlBase controlBase = member as ControlBase;
                if (controlBase != null)
                {
                    g.DrawLine(pen1, new Point(0, 0), new Point(controlBase.Left, controlBase.Top));
                }

            }
            using (Graphics tg = e.Graphics)
            {
                tg.DrawImage(bufferimage, 0, 0);　　//把画布贴到画面上
            }
        }

    


        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                ControlBase controlBase;
                controlBase = new ControlBase(this, new Point(10, 10));
                controlBase.Name = i.ToString();
                this.Controls.Add(controlBase);
            }
           // g.DrawLine(pen, new Point(0, 0), new Point(50, 50));
        }
        private void DrawLine(ControlBase control)
        {
            if (control.Top > this.Height / 2)
            {
                g.DrawLine(pen, new Point(control.Left + control.Width / 2, control.Top), new Point(control.Left + control.Width / 2, this.Height / 2));
            }
            else
            {
                g.DrawLine(pen, new Point(control.Left + control.Width / 2, control.Top + control.Height), new Point(control.Left + control.Width / 2, this.Height / 2));

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ControlBase controlBase;
            controlBase = new ControlBase(this, new Point(100, 100));
            controlBase.Name = "sdf";
            this.Controls.Add(controlBase);
            DrawLine(controlBase);

        }
    }
}
