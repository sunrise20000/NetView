namespace SubBusContrainer
{
    partial class ProductContrainer
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
            this.panel_Product = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_Product
            // 
            this.panel_Product.AllowDrop = true;
            this.panel_Product.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel_Product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Product.Location = new System.Drawing.Point(0, 0);
            this.panel_Product.Name = "panel_Product";
            this.panel_Product.Size = new System.Drawing.Size(957, 537);
            this.panel_Product.TabIndex = 0;
            this.panel_Product.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.panel_Product_ControlRemoved);
            this.panel_Product.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel_Product_DragDrop);
            this.panel_Product.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel_Product_DragEnter);
            this.panel_Product.DragOver += new System.Windows.Forms.DragEventHandler(this.panel_Product_DragOver);
            // 
            // ProductContrainer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Product);
            this.Name = "ProductContrainer";
            this.Size = new System.Drawing.Size(957, 537);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Product;
    }
}
