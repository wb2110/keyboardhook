using System;

namespace DataExport
{
	// Token: 0x0200000D RID: 13
	public class TableAssociationInfo
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00006D7C File Offset: 0x00004F7C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00006D94 File Offset: 0x00004F94
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00006DA0 File Offset: 0x00004FA0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public string WhereCondition
		{
			get
			{
				return this._whereCondition;
			}
			set
			{
				this._whereCondition = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00006DC4 File Offset: 0x00004FC4
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00006DDC File Offset: 0x00004FDC
		public string SqlCondition
		{
			get
			{
				return this._sqlCondition;
			}
			set
			{
				this._sqlCondition = value;
			}
		}

		// Token: 0x0400004E RID: 78
		private string _name;

		// Token: 0x0400004F RID: 79
		private string _whereCondition;

		// Token: 0x04000050 RID: 80
		private string _sqlCondition;
	}
}
