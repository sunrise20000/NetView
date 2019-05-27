namespace ControlTest
{
    partial class HL2001
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
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_Function = new System.Windows.Forms.TextBox();
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
            this.label_PlugSequence.Location = new System.Drawing.Point(20, 126);
            this.label_PlugSequence.Name = "label_PlugSequence";
            this.label_PlugSequence.Size = new System.Drawing.Size(143, 18);
            this.label_PlugSequence.TabIndex = 0;
            this.label_PlugSequence.Text = "Plug  Sequence:";
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
            this.txt_Function.Text = "DO8xDC24V 0.5A";
            // 
            // nmUD_PlugSequence
            // 
            this.nmUD_PlugSequence.Location = new System.Drawing.Point(207, 124);
            this.nmUD_PlugSequence.Name = "nmUD_PlugSequence";
            this.nmUD_PlugSequence.Size = new System.Drawing.Size(248, 28);
            this.nmUD_PlugSequence.TabIndex = 2;
            // 
            // HL2001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 340);
            this.Controls.Add(this.nmUD_PlugSequence);
            this.Controls.Add(this.txt_Function);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.label_PlugSequence);
            this.Controls.Add(this.label_Function);
            this.Controls.Add(this.label_Name);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HL2001";
            this.Text = "HL2001Param";
            ((System.ComponentModel.ISupportInitialize)(this.nmUD_PlugSequence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_Function;
        private System.Windows.Forms.Label label_PlugSequence;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_Function;
        private System.Windows.Forms.NumericUpDown nmUD_PlugSequence;
    }
}
