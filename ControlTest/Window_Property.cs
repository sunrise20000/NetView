using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlTest
{
    public partial class Window_Property : Form
    {
        public Window_Property(string Name="")
        {
            InitializeComponent();
            this.propertyGrid.PropertySort = PropertySort.NoSort;
            WindowCaption = Name;
        }
        public object SelectedObject {
            set { this.propertyGrid.SelectedObject = value; }
        }
        public string WindowCaption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
    }
}
