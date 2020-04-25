using System;
using System.Windows.Forms;

namespace DataExport
{
	// Token: 0x02000004 RID: 4
	public class MessageResponseBroker
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002098 File Offset: 0x00000298
		public void ShowMessage(string message, ResponsePresentation presentation)
		{
			if (presentation == ResponsePresentation.MessageBox)
			{
				MessageBox.Show(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}
}
