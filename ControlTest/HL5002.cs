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
    public partial class HL5002 : Form
    {
        SubBusModel busModel;
        public HL5002(SubBusModel controlBase)
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
          
            this.nmUD_PlugSequence.Value = busModel.Sequence;

            this.cmb_Resolution.SelectedIndex = busModel.Resolution;
            this.cmb_Revolution.SelectedIndex = busModel.Revolution;

            this.nmUD_PreSetValue.Value = busModel.ResPara1;
            this.nmUD_ResPara1.Value = busModel.ResPara1;
            this.nmUD_ResPara1.Value = busModel.ResPara2;
            this.nmUD_ResPara3.Value = busModel.ResPara3;
            this.nmUD_ResPara4.Value = busModel.ResPara4;
            this.nmUD_ResPara5.Value = busModel.ResPara5;
        }
        private void AddEvent()
        {
            this.nmUD_PlugSequence.ValueChanged += UpdateParam;

            this.cmb_Resolution.SelectedIndexChanged += UpdateParam;
            this.cmb_Revolution.SelectedIndexChanged += UpdateParam;

            this.nmUD_PreSetValue.ValueChanged += UpdateParam;
            this.nmUD_ResPara1.ValueChanged += UpdateParam;
            this.nmUD_ResPara3.ValueChanged += UpdateParam;
            this.nmUD_ResPara4.ValueChanged += UpdateParam;
            this.nmUD_ResPara5.ValueChanged += UpdateParam;
           



        }
        private void RemoveEvent()
        {
            this.nmUD_PlugSequence.ValueChanged -= UpdateParam;

            this.cmb_Resolution.SelectedIndexChanged -= UpdateParam;
            this.cmb_Revolution.SelectedIndexChanged -= UpdateParam;
            this.nmUD_PreSetValue.ValueChanged -= UpdateParam;
            this.nmUD_ResPara1.ValueChanged -= UpdateParam;
            this.nmUD_ResPara3.ValueChanged -= UpdateParam;
            this.nmUD_ResPara4.ValueChanged -= UpdateParam;
            this.nmUD_ResPara5.ValueChanged -= UpdateParam;
            
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            busModel.Sequence = (int)this.nmUD_PlugSequence.Value;

            busModel.Resolution = (short)this.cmb_Resolution.SelectedIndex;
            busModel.Revolution = (short)this.cmb_Resolution.SelectedIndex;

            busModel.ResPara1 = (short)this.nmUD_PreSetValue.Value;
            busModel.ResPara2 = (short)this.nmUD_ResPara1.Value;
            busModel.ResPara3 = (short)this.nmUD_ResPara3.Value;
            busModel.ResPara4 = (short)this.nmUD_ResPara4.Value;
            busModel.ResPara5 = (short)this.nmUD_ResPara5.Value;          


        }
        private void FormModelClosing(object sender, FormClosingEventArgs e)
        {
            RemoveEvent();
        }
    }
}
