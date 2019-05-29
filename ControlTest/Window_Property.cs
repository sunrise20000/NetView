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
        public Window_Property()
        {
            InitializeComponent();
            this.propertyGrid.PropertySort = PropertySort.NoSort;
        }
        public object SelectedObject {
            set { this.propertyGrid.SelectedObject = value; }
        }
    }
}
