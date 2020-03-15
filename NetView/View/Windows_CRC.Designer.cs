namespace NetView.View
{
	partial class Windows_CRC
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.BtnCalc = new System.Windows.Forms.Button();
			this.textBox_byteArray = new System.Windows.Forms.TextBox();
			this.textBox_result = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BtnCalc
			// 
			this.BtnCalc.Location = new System.Drawing.Point(496, 334);
			this.BtnCalc.Name = "BtnCalc";
			this.BtnCalc.Size = new System.Drawing.Size(84, 23);
			this.BtnCalc.TabIndex = 1;
			this.BtnCalc.Text = "Calculate";
			this.BtnCalc.UseVisualStyleBackColor = true;
			this.BtnCalc.Click += new System.EventHandler(this.BtnCalc_Click);
			// 
			// textBox_byteArray
			// 
			this.textBox_byteArray.Location = new System.Drawing.Point(15, 75);
			this.textBox_byteArray.Multiline = true;
			this.textBox_byteArray.Name = "textBox_byteArray";
			this.textBox_byteArray.Size = new System.Drawing.Size(565, 241);
			this.textBox_byteArray.TabIndex = 2;
			// 
			// textBox_result
			// 
			this.textBox_result.Location = new System.Drawing.Point(15, 336);
			this.textBox_result.Name = "textBox_result";
			this.textBox_result.Size = new System.Drawing.Size(471, 21);
			this.textBox_result.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.label1.Location = new System.Drawing.Point(15, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(512, 48);
			this.label1.TabIndex = 4;
			this.label1.Text = "Please enter the byte array as hex format. The following is an \r\nexample:12 34 56" +
    " 78 24 33 41. When you finish the data , push\r\nthe calculate button to get the r" +
    "esult";
			// 
			// Windows_CRC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 373);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_result);
			this.Controls.Add(this.textBox_byteArray);
			this.Controls.Add(this.BtnCalc);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Windows_CRC";
			this.Text = "Calculate CRC16";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button BtnCalc;
		private System.Windows.Forms.TextBox textBox_byteArray;
		private System.Windows.Forms.TextBox textBox_result;
		private System.Windows.Forms.Label label1;
	}
}