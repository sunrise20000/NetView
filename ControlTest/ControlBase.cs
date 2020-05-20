﻿using System;
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
        private bool m_leftMouseDownFlag;//鼠标左键按下标志
        private Point m_lastPoint;//控件上一时刻鼠标位置 
        protected Window_Property WinPropertySetting =null;
        public event EventHandler OnModleDelete;
        #endregion
        #region 属性定义
		/// <summary>
		/// 模块名称
		/// </summary>
        public new string Name
        {
            get;set;
        }
        
		/// <summary>
		/// 显示名称
		/// </summary>
        protected virtual string DisplayName
		{
			get;set;
		}
        public int GlobalIndex { get; set; }
        public int LocalIndex { get; set; }

        public bool IsAllowMove { get; set; } = true;
        #endregion
        #region 事件定义
        public event EventHandler<ControlMoveEventArgs> ControlMoveEvent;
        #endregion
        public ControlBase(Point location,int Width, int Height, Color clr)
        {
            InitializeComponent();
            this.label1.Dock = DockStyle.Fill;
            this.ContextMenuStrip = contextMenuStrip1;          
            this.Location = location;
            this.Width = Width;
            this.Height = Height;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = clr;
            label1.BackColor = clr;
            
        }
        public ControlBase()
        { }
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
                OnControlMove(new ControlMoveEventArgs("LeftMouseDown"));
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
            //OnControlMove(new ControlMoveEventArgs("LeftMouseDown"));
            OnControlMove(new ControlMoveEventArgs("Delete"));
            OnModleDelete?.Invoke(this, e);
        }  

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowProperty();
        }
        public virtual void ShowProperty()
        {
            
        }
		public virtual void UpdateGUI()
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
