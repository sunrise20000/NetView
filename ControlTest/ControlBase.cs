using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlTest
{
    public partial class ControlBase: UserControl
    {
        #region 私有成员变量
        private string  m_name;//名称
        string m_type;//better to change to enum type
        string m_param3;
        string m_param4;
        string m_param5;
        string m_param6;
        string m_param7;
        private bool m_leftMouseDownFlag;//鼠标左键按下标志
        private Point m_lastPoint;//控件上一时刻鼠标位置 
    
    
        #endregion
        #region 属性定义
        public new  string Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                this.label1.Text = m_name;
            }
        }
        public string Type
        {
            get { return m_type; }
            set { m_type = value; }
        }
        public string Param3
        {
            get { return m_param3; }
            set { m_param3 = value; }
        }
        public string Param4
        {
            get { return m_param4; }
            set { m_param4 = value; }
        }
        public string Param5
        {
            get { return m_param5; }
            set { m_param5 = value; }
        }
        public string Param6
        {
            get { return m_param6; }
            set { m_param6 = value; }
        }
        public string Param7
        {
            get { return m_param7; }
            set { m_param7 = value; }
        }
        #endregion
        #region 事件定义
        public event EventHandler<ControlMoveEventArgs> ControlMoveEvent;
        #endregion
        public ControlBase(Control parent,Point location)
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
            this.Parent = parent;
            this.Location = location;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            Init();
            
        }

     

        private void Init()
        {
            using (Graphics g = this.Parent.CreateGraphics())
            {
               // DrawLine(g);

            }

        }
        private void DrawLine(Graphics g)
        {
            Pen _pen = new Pen(Color.Gray,3);
            if (this.Top > Parent.Height / 2)
            {
                g.DrawLine(_pen, new Point(this.Left + this.Width / 2, this.Top), new Point(this.Left + this.Width / 2, Parent.Height / 2));
                g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(this.Left + this.Width / 2 - 5, Parent.Height / 2 - 5, 10, 10));
            }
            else
            {
                g.DrawLine(_pen, new Point(this.Left + this.Width / 2, this.Top + this.Height), new Point(this.Left + this.Width / 2, Parent.Height / 2));
                g.FillEllipse(new System.Drawing.SolidBrush(Color.Gray), new Rectangle(this.Left + this.Width / 2 - 5, Parent.Height / 2 - 5, 10, 10));

            }
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.BringToFront();
                m_lastPoint = e.Location;
                m_leftMouseDownFlag = true;
                OnControlMove(new ControlMoveEventArgs("LeftMouseDown"));
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_leftMouseDownFlag)
            {
                this.Left += e.Location.X - m_lastPoint.X;
                this.Top += e.Location.Y - m_lastPoint.Y;                       
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_leftMouseDownFlag = false;
                OnControlMove(new ControlMoveEventArgs("LeftMouseUp"));
                
            }
        }
        private void OnControlMove(ControlMoveEventArgs e)
        {

            if (ControlMoveEvent != null)
            {
                ControlMoveEvent(this, e);
            }
        }

        private void toolStripMenuItem_Delete_Click(object sender, EventArgs e)
        {
            OnControlMove(new ControlMoveEventArgs("LeftMouseDown"));
            this.Dispose();
        }  

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowProperty();
        }
        public void ShowProperty()
        {
            FormSubModelParam form = new FormSubModelParam(this);
            form.ShowDialog();
        }

        private void toolStripMenuItem_Property_Click(object sender, EventArgs e)
        {
            ShowProperty();
        }
    }
    public class ControlMoveEventArgs:EventArgs
    {
        public string info;
        public ControlMoveEventArgs(string info)
        {
            this.info = info;
        }

    }

}
