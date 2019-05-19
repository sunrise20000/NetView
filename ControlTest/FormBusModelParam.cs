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
    public partial class FormBusModelParam : Form
    {
        #region 字段state
        BusModel m_controlBase;
       
        #endregion
        public FormBusModelParam(BusModel controlBase)
        {
            InitializeComponent();
            this.m_controlBase = controlBase;
            Init();
            AddEvent();
        }
        private void Init()
        {
            txt_Name.Text = m_controlBase.Name;
            cmB_Type.Text = m_controlBase.Type;
            cmB_Param3.Text = m_controlBase.Param3;
            cmB_Param4.Text = m_controlBase.Param4;
            cmB_Param5.Text = m_controlBase.Param5;
            cmB_Param6.Text = m_controlBase.Param6;
            cmB_Param7.Text = m_controlBase.Param7;
        }
        private void AddEvent()
        {
            cmB_Type.TextChanged += UpdateParam;
            cmB_Param3.TextChanged += UpdateParam;
            cmB_Param4.TextChanged += UpdateParam;      
            cmB_Param5.TextChanged += UpdateParam;
            cmB_Param6.TextChanged += UpdateParam;
            cmB_Param7.TextChanged += UpdateParam;

        }
        private void RemoveEvent()
        {
            cmB_Type.TextChanged -= UpdateParam;
            cmB_Param3.TextChanged -= UpdateParam;
            cmB_Param4.TextChanged -= UpdateParam;
            cmB_Param5.TextChanged -= UpdateParam;
            cmB_Param6.TextChanged -= UpdateParam;
            cmB_Param7.TextChanged -= UpdateParam;
        }
        private void UpdateParam(object sender, EventArgs e)
        {
            m_controlBase.Type = cmB_Type.Text;
            m_controlBase.Param3 = cmB_Param3.Text;
            m_controlBase.Param4 = cmB_Param4.Text;
            m_controlBase.Param5 = cmB_Param5.Text;
            m_controlBase.Param6 = cmB_Param6.Text;
            m_controlBase.Param7 = cmB_Param7.Text;
        }

        private void FormSubModelParam_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveEvent();
        }
    }
}
