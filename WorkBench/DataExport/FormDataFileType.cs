using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DataExport
{
	// Token: 0x02000007 RID: 7
	public partial class FormDataFileType : Form
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002D4A File Offset: 0x00000F4A
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002D52 File Offset: 0x00000F52
		public string FileSaveType { get; set; }

		// Token: 0x0600001B RID: 27 RVA: 0x00002D5B File Offset: 0x00000F5B
		public FormDataFileType()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002D73 File Offset: 0x00000F73
		private void radioBtnDboFiles_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002D73 File Offset: 0x00000F73
		private void radioBtnOneFile_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002D78 File Offset: 0x00000F78
		private void btnOk_Click(object sender, EventArgs e)
		{
			bool @checked = this.radioBtnDboFiles.Checked;
			if (@checked)
			{
				this.FileSaveType = "0";
			}
			bool checked2 = this.radioBtnOneFile.Checked;
			if (checked2)
			{
				this.FileSaveType = "1";
			}
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002DC4 File Offset: 0x00000FC4
		private void btnCancle_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}
	}
}
