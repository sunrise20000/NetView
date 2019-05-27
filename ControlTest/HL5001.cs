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
    public partial class HL5001 : Form
    {
        SubBusModel busModel;
        public HL5001(SubBusModel controlBase)
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

            this.nmUD_CounterLimitH.Value = busModel.CounterLimitH;
            this.nmUD_CounterLimitL.Value = busModel.CounterLimitL;

            this.nmUD_ResPara1.Value = busModel.ResPara1;
            this.nmUD_ResPara2.Value = busModel.ResPara2;
            this.nmUD_ResPara3.Value = busModel.ResPara3;
            this.nmUD_ResPara4.Value = busModel.ResPara4;
            this.nmUD_ResPara5.Value = busModel.ResPara5;
            this.nmUD_ResPara5.Value = busModel.ResPara6;
        }
        private void AddEvent()
        {
            this.nmUD_PlugSequence.ValueChanged += UpdateParam;

            this.nmUD_CounterLimitH.ValueChanged += UpdateParam;
            this.nmUD_CounterLimitL.ValueChanged += UpdateParam;

            this.nmUD_ResPara1.ValueChanged += UpdateParam;
            this.nmUD_ResPara2.ValueChanged += UpdateParam;
            this.nmUD_ResPara3.ValueChanged += UpdateParam;
            this.nmUD_ResPara4.ValueChanged += UpdateParam;
            this.nmUD_ResPara5.ValueChanged += UpdateParam;
            this.nmUD_ResPara6.ValueChanged += UpdateParam;



        }
        private void RemoveEvent()
        {
            this.nmUD_PlugSequence.ValueChanged -= UpdateParam;

            this.nmUD_ResPara1.ValueChanged -= UpdateParam;
            this.nmUD_ResPara2.ValueChanged -= UpdateParam;
            this.nmUD_ResPara3.ValueChanged -= UpdateParam;
            this.nmUD_ResPara4.ValueChanged -= UpdateParam;
            this.nmUD_ResPara5.ValueChanged -= UpdateParam;
            this.nmUD_ResPara6.ValueChanged -= UpdateParam;
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            busModel.Sequence = (int)this.nmUD_PlugSequence.Value;

            busModel.CounterLimitH = (short)this.nmUD_CounterLimitH.Value;
            busModel.CounterLimitL = (short)this.nmUD_CounterLimitL.Value;

            busModel.ResPara1 = (short)this.nmUD_ResPara1.Value;
            busModel.ResPara2 = (short)this.nmUD_ResPara2.Value;
            busModel.ResPara3 = (short)this.nmUD_ResPara3.Value;
            busModel.ResPara4 = (short)this.nmUD_ResPara4.Value;
            busModel.ResPara5 = (short)this.nmUD_ResPara5.Value;
            busModel.ResPara6 = (short)this.nmUD_ResPara6.Value;


        }
        private void FormModelClosing(object sender, FormClosingEventArgs e)
        {
            RemoveEvent();
        }
    }
}
