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
    public partial class HL2002 : Form
    {
        SubBusModel busModel;
        public HL2002(SubBusModel controlBase)
        {
            InitializeComponent();
            this.FormClosing += FormModelClosing;

            this.busModel = controlBase;
            Init();
            AddEvent();
        }
        private void Init()
        {
            this.txt_Name.Text = busModel.Name;
          //  this.txt_Function.Text = busModel.Function;
            this.nmUD_PlugSequence.Value = busModel.Sequence;
        }
        private void AddEvent()
        {
            this.nmUD_PlugSequence.ValueChanged += UpdateParam;
        }
        private void RemoveEvent()
        {
            this.nmUD_PlugSequence.ValueChanged -= UpdateParam;
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            busModel.Sequence = (int)this.nmUD_PlugSequence.Value;
        }
        private void FormModelClosing(object sender, FormClosingEventArgs e)
        {
            RemoveEvent();
        }
    }
}
