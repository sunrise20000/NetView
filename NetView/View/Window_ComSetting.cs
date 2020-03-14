using NetView.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetView.View
{
    public partial class Window_ComSetting : Form
    {
		public bool IsOkClicked { get; private set; } = false;
        public Window_ComSetting()
        {
            InitializeComponent();

            this.propertyGrid1.SelectedObject = new ComportSettingModel();
        }

        public ComportSettingModel ComSetting {
            get { return this.propertyGrid1.SelectedObject as ComportSettingModel; }
            set { this.propertyGrid1.SelectedObject = value; }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            var model = this.propertyGrid1.SelectedObject as ComportSettingModel;
            model.RefreshComport();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
			IsOkClicked = true;
			Close();
        }
    }
}
