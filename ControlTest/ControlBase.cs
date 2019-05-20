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
       
        private bool m_leftMouseDownFlag;//鼠标左键按下标志
        private Point m_lastPoint;//控件上一时刻鼠标位置 

        public event EventHandler OnSubBusModleDelete;
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
        public bool IsAllowMove { get; set; } = true;
        #endregion
        #region 事件定义
        public event EventHandler<ControlMoveEventArgs> ControlMoveEvent;
        #endregion
        public ControlBase(Point location)
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
           
            this.Location = location;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;         
            
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
            if (IsAllowMove && m_leftMouseDownFlag)
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
            OnSubBusModleDelete?.Invoke(this, e);
            this.Dispose();
        }  

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowProperty();
        }
        public virtual  void ShowProperty()
        {
            
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
