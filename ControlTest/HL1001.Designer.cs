namespace ControlTest
{
    partial class HL1001
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Name = new System.Windows.Forms.Label();
            this.label_Function = new System.Windows.Forms.Label();
            this.label_PlugSequence = new System.Windows.Forms.Label();
            this.label_Type = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_Function = new System.Windows.Forms.TextBox();
            this.cmB_Type = new System.Windows.Forms.ComboBox();
            this.nmUD_PlugSequence = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_PlugSequence)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(110, 27);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(53, 18);
            this.label_Name.TabIndex = 0;
            this.label_Name.Text = "Name:";
            // 
            // label_Function
            // 
            this.label_Function.AutoSize = true;
            this.label_Function.Location = new System.Drawing.Point(74, 72);
            this.label_Function.Name = "label_Function";
            this.label_Function.Size = new System.Drawing.Size(89, 18);
            this.label_Function.TabIndex = 0;
            this.label_Function.Text = "Function:";
            // 
            // label_PlugSequence
            // 
            this.label_PlugSequence.AutoSize = true;
            this.label_PlugSequence.Location = new System.Drawing.Point(12, 116);
            this.label_PlugSequence.Name = "label_PlugSequence";
            this.label_PlugSequence.Size = new System.Drawing.Size(143, 18);
            this.label_PlugSequence.TabIndex = 0;
            this.label_PlugSequence.Text = "Plug  Sequence:";
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(110, 175);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(53, 18);
            this.label_Type.TabIndex = 0;
            this.label_Type.Text = "Type:";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(207, 24);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.ReadOnly = true;
            this.txt_Name.Size = new System.Drawing.Size(248, 28);
            this.txt_Name.TabIndex = 1;
            // 
            // txt_Function
            // 
            this.txt_Function.Location = new System.Drawing.Point(207, 69);
            this.txt_Function.Name = "txt_Function";
            this.txt_Function.ReadOnly = true;
            this.txt_Function.Size = new System.Drawing.Size(248, 28);
            this.txt_Function.TabIndex = 1;
            this.txt_Function.Text = "DI8xDC24V";
            // 
            // cmB_Type
            // 
            this.cmB_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmB_Type.FormattingEnabled = true;
            this.cmB_Type.Items.AddRange(new object[] {
            "Normal",
            "DI1 as Alarm",
            "DI1 2 as Alarm",
            "DI1 3 as Alarm",
            "DI1 4 as Alarm",
            "DI1 5 as Alarm",
            "DI16  as Alarm",
            "DI1 7 as Alarm",
            "DI1 8 as Alarm"});
            this.cmB_Type.Location = new System.Drawing.Point(207, 172);
            this.cmB_Type.Name = "cmB_Type";
            this.cmB_Type.Size = new System.Drawing.Size(248, 26);
            this.cmB_Type.TabIndex = 2;
            // 
            // nmUD_PlugSequence
            // 
            this.nmUD_PlugSequence.Location = new System.Drawing.Point(207, 114);
            this.nmUD_PlugSequence.Name = "nmUD_PlugSequence";
            this.nmUD_PlugSequence.Size = new System.Drawing.Size(248, 28);
            this.nmUD_PlugSequence.TabIndex = 3;
            // 
            // HL1001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 340);
            this.Controls.Add(this.nmUD_PlugSequence);
            this.Controls.Add(this.cmB_Type);
            this.Controls.Add(this.txt_Function);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.label_PlugSequence);
            this.Controls.Add(this.label_Function);
            this.Controls.Add(this.label_Name);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HL1001";
            this.Text = "HL1001Param";
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_PlugSequence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_Function;
        private System.Windows.Forms.Label label_PlugSequence;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_Function;
        private System.Windows.Forms.ComboBox cmB_Type;
        private System.Windows.Forms.NumericUpDown nmUD_PlugSequence;
    }
}
