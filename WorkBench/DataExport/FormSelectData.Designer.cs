namespace DataExport
{
	// Token: 0x02000009 RID: 9
	public partial class FormSelectData : global::System.Windows.Forms.Form
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000044DC File Offset: 0x000026DC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004514 File Offset: 0x00002714
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(962, 627);
			base.Name = "FormSelectData";
			this.Text = "配置数导出";
			base.ResumeLayout(false);
		}

		// Token: 0x04000037 RID: 55
		private global::System.ComponentModel.IContainer components = null;
	}
}
