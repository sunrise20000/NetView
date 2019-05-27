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
    public partial class HL3002 : Form
    {
        SubBusModel busModel;
        public HL3002(SubBusModel controlBase)
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

            this.cmb_Ch1InputType.SelectedIndex = busModel.Ch1InoutOrOutputType;
            this.cmb_Ch1Accurary.SelectedIndex = busModel.Ch1Accuracy;

            this.cmb_Ch2InputType.SelectedIndex = busModel.Ch2InoutOrOutputType;
            this.cmb_Ch2Accurary.SelectedIndex = busModel.Ch2Accuracy;

            this.cmb_Ch3InputType.SelectedIndex = busModel.Ch3InoutOrOutputType;
            this.cmb_Ch3Accurary.SelectedIndex = busModel.Ch3Accuracy;

            this.cmb_Ch4InputType.SelectedIndex = busModel.Ch4InoutOrOutputType;
            this.cmb_Ch4Accurary.SelectedIndex = busModel.Ch4Accuracy;
        }
        private void AddEvent()
        {
            this.nmUD_PlugSequence.ValueChanged += UpdateParam;
            this.cmb_Ch1InputType.SelectedIndexChanged += UpdateParam;
            this.cmb_Ch2InputType.SelectedIndexChanged += UpdateParam;
            this.cmb_Ch3InputType.SelectedIndexChanged += UpdateParam;
            this.cmb_Ch4InputType.SelectedIndexChanged += UpdateParam;

            this.cmb_Ch1Accurary.SelectedIndexChanged += UpdateParam;
            this.cmb_Ch2Accurary.SelectedIndexChanged += UpdateParam;
            this.cmb_Ch3Accurary.SelectedIndexChanged += UpdateParam;
            this.cmb_Ch4Accurary.SelectedIndexChanged += UpdateParam;

        }
        private void RemoveEvent()
        {
            this.nmUD_PlugSequence.ValueChanged -= UpdateParam;
            this.cmb_Ch1InputType.SelectedIndexChanged -= UpdateParam;
            this.cmb_Ch2InputType.SelectedIndexChanged -= UpdateParam;
            this.cmb_Ch3InputType.SelectedIndexChanged -= UpdateParam;
            this.cmb_Ch4InputType.SelectedIndexChanged -= UpdateParam;

            this.cmb_Ch1Accurary.SelectedIndexChanged -= UpdateParam;
            this.cmb_Ch2Accurary.SelectedIndexChanged -= UpdateParam;
            this.cmb_Ch3Accurary.SelectedIndexChanged -= UpdateParam;
            this.cmb_Ch4Accurary.SelectedIndexChanged -= UpdateParam;
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            busModel.Sequence = (int)this.nmUD_PlugSequence.Value;
            busModel.Ch1InoutOrOutputType = (short)cmb_Ch1InputType.SelectedIndex;
            busModel.Ch2InoutOrOutputType = (short)cmb_Ch2InputType.SelectedIndex;
            busModel.Ch3InoutOrOutputType = (short)cmb_Ch3InputType.SelectedIndex;
            busModel.Ch4InoutOrOutputType = (short)cmb_Ch4InputType.SelectedIndex;

            busModel.Ch1Accuracy = (short)cmb_Ch1Accurary.SelectedIndex;
            busModel.Ch2Accuracy = (short)cmb_Ch2Accurary.SelectedIndex;
            busModel.Ch3Accuracy = (short)cmb_Ch3Accurary.SelectedIndex;
            busModel.Ch4Accuracy = (short)cmb_Ch4Accurary.SelectedIndex;
        }
        private void FormModelClosing(object sender, FormClosingEventArgs e)
        {
            RemoveEvent();
        }
    }
}
