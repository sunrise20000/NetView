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
    public partial class HL1001 : Form
    {
        SubBusModel busModel;
        public HL1001(SubBusModel controlBase) 
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
           // this.txt_Function.Text = busModel.Function;
            this.nmUD_PlugSequence.Value = busModel.Sequence;
            this.cmB_Type.SelectedIndex = busModel.Type;
        }
        private void AddEvent()
        {
            this.nmUD_PlugSequence.ValueChanged += UpdateParam;
            this.cmB_Type.TextChanged += UpdateParam;
        }
        private void RemoveEvent()
        {
            this.nmUD_PlugSequence.ValueChanged -= UpdateParam;
            this.cmB_Type.TextChanged -= UpdateParam;
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            busModel.Sequence = (int)this.nmUD_PlugSequence.Value;
            busModel.Type = (short)this.cmB_Type.SelectedIndex;
        }
        private void FormModelClosing(object sender, FormClosingEventArgs e)
        {
            RemoveEvent();
        }
    }
}
