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
    public partial class BusModel : ControlBase
    {
        string m_type;//better to change to enum type
        string m_param3;
        string m_param4;
        string m_param5;
        string m_param6;
        string m_param7;
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
        public BusModel(Point location, int Width=150, int Height=150):base(location,Width,Height)
        {
            InitializeComponent();
        }
        public override void ShowProperty()
        {
            FormBusModelParam form = new FormBusModelParam(this);
            form.ShowDialog();
        }
    }
}
