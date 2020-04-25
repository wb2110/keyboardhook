namespace DataExport
{
	// Token: 0x02000007 RID: 7
	public partial class FormDataFileType : global::System.Windows.Forms.Form
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002DD0 File Offset: 0x00000FD0
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E08 File Offset: 0x00001008
		private void InitializeComponent()
		{
			this.radioBtnDboFiles = new global::System.Windows.Forms.RadioButton();
			this.radioBtnOneFile = new global::System.Windows.Forms.RadioButton();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.btnOk = new global::System.Windows.Forms.Button();
			this.btnCancle = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.radioBtnDboFiles.AutoSize = true;
			this.radioBtnDboFiles.Checked = true;
			this.radioBtnDboFiles.Location = new global::System.Drawing.Point(25, 27);
			this.radioBtnDboFiles.Name = "radioBtnDboFiles";
			this.radioBtnDboFiles.Size = new global::System.Drawing.Size(173, 16);
			this.radioBtnDboFiles.TabIndex = 0;
			this.radioBtnDboFiles.TabStop = true;
			this.radioBtnDboFiles.Text = "按照Dbo保存为多份数据文件";
			this.radioBtnDboFiles.UseVisualStyleBackColor = true;
			this.radioBtnDboFiles.CheckedChanged += new global::System.EventHandler(this.radioBtnDboFiles_CheckedChanged);
			this.radioBtnOneFile.AutoSize = true;
			this.radioBtnOneFile.Location = new global::System.Drawing.Point(26, 80);
			this.radioBtnOneFile.Name = "radioBtnOneFile";
			this.radioBtnOneFile.Size = new global::System.Drawing.Size(131, 16);
			this.radioBtnOneFile.TabIndex = 1;
			this.radioBtnOneFile.Text = "保存为一份数据文件";
			this.radioBtnOneFile.UseVisualStyleBackColor = true;
			this.radioBtnOneFile.CheckedChanged += new global::System.EventHandler(this.radioBtnOneFile_CheckedChanged);
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Blue;
			this.label1.Location = new global::System.Drawing.Point(37, 49);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(275, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "将选择的数据，按照Dbo的名称导出为多份data文件";
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.Blue;
			this.label2.Location = new global::System.Drawing.Point(35, 103);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(197, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "将选择的数据，导出为一份data文件";
			this.btnOk.Location = new global::System.Drawing.Point(97, 146);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new global::System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "确定";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new global::System.EventHandler(this.btnOk_Click);
			this.btnCancle.Location = new global::System.Drawing.Point(204, 146);
			this.btnCancle.Name = "btnCancle";
			this.btnCancle.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancle.TabIndex = 5;
			this.btnCancle.Text = "取消";
			this.btnCancle.UseVisualStyleBackColor = true;
			this.btnCancle.Click += new global::System.EventHandler(this.btnCancle_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(386, 181);
			base.ControlBox = false;
			base.Controls.Add(this.btnCancle);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.radioBtnOneFile);
			base.Controls.Add(this.radioBtnDboFiles);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "FormDataFileType";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "选择文件保存方式";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000014 RID: 20
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.RadioButton radioBtnDboFiles;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.RadioButton radioBtnOneFile;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Button btnOk;

		// Token: 0x0400001A RID: 26
		private global::System.Windows.Forms.Button btnCancle;
	}
}
