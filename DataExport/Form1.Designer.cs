namespace DataExport
{
	// Token: 0x02000005 RID: 5
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002788 File Offset: 0x00000988
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027C0 File Offset: 0x000009C0
		private void InitializeComponent()
		{
			this.btnExport = new global::System.Windows.Forms.Button();
			this.checkDbo = new global::System.Windows.Forms.CheckBox();
			this.gridControl1 = new global::DevExpress.XtraGrid.GridControl();
			this.gridView1 = new global::DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColDboId = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColDboCode = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColDboName = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColDboSel = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit1 = new global::DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			((global::System.ComponentModel.ISupportInitialize)this.gridControl1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.gridView1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).BeginInit();
			base.SuspendLayout();
			this.btnExport.Location = new global::System.Drawing.Point(578, 547);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new global::System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 0;
			this.btnExport.Text = "导出数据";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new global::System.EventHandler(this.button1_Click);
			this.checkDbo.AutoSize = true;
			this.checkDbo.Location = new global::System.Drawing.Point(27, 552);
			this.checkDbo.Name = "checkDbo";
			this.checkDbo.Size = new global::System.Drawing.Size(48, 16);
			this.checkDbo.TabIndex = 2;
			this.checkDbo.Text = "全选";
			this.checkDbo.UseVisualStyleBackColor = true;
			this.checkDbo.CheckedChanged += new global::System.EventHandler(this.checkDbo_CheckedChanged);
			this.gridControl1.Location = new global::System.Drawing.Point(-3, 1);
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.RepositoryItems.AddRange(new global::DevExpress.XtraEditors.Repository.RepositoryItem[]
			{
				this.repositoryItemCheckEdit1
			});
			this.gridControl1.Size = new global::System.Drawing.Size(680, 531);
			this.gridControl1.TabIndex = 3;
			this.gridControl1.ViewCollection.AddRange(new global::DevExpress.XtraGrid.Views.Base.BaseView[]
			{
				this.gridView1
			});
			this.gridView1.AutoFilterMode = global::DevExpress.XtraGrid.Views.Grid.AutoFilterModes.ClientMode;
			this.gridView1.Columns.AddRange(new global::DevExpress.XtraGrid.Columns.GridColumn[]
			{
				this.gridColDboId,
				this.gridColDboCode,
				this.gridColDboName,
				this.gridColDboSel
			});
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.IndicatorWidth = 40;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsView.ShowAutoFilterRow = true;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			this.gridView1.CustomDrawRowIndicator += new global::DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
			this.gridColDboId.Caption = "DboId";
			this.gridColDboId.FieldName = "DboId";
			this.gridColDboId.Name = "gridColDboId";
			this.gridColDboCode.Caption = "Dbo编号";
			this.gridColDboCode.FieldName = "DboCode";
			this.gridColDboCode.Name = "gridColDboCode";
			this.gridColDboCode.OptionsColumn.AllowEdit = false;
			this.gridColDboCode.Visible = true;
			this.gridColDboCode.VisibleIndex = 1;
			this.gridColDboCode.Width = 274;
			this.gridColDboName.Caption = "Dbo名称";
			this.gridColDboName.FieldName = "DboName";
			this.gridColDboName.Name = "gridColDboName";
			this.gridColDboName.OptionsColumn.AllowEdit = false;
			this.gridColDboName.Visible = true;
			this.gridColDboName.VisibleIndex = 2;
			this.gridColDboName.Width = 322;
			this.gridColDboSel.Caption = "选择";
			this.gridColDboSel.ColumnEdit = this.repositoryItemCheckEdit1;
			this.gridColDboSel.FieldName = "DboSel";
			this.gridColDboSel.Name = "gridColDboSel";
			this.gridColDboSel.Visible = true;
			this.gridColDboSel.VisibleIndex = 0;
			this.gridColDboSel.Width = 42;
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(680, 579);
			base.Controls.Add(this.gridControl1);
			base.Controls.Add(this.checkDbo);
			base.Controls.Add(this.btnExport);
			base.Name = "Form1";
			this.Text = "配置数据导出";
			base.Load += new global::System.EventHandler(this.Form1_Load);
			((global::System.ComponentModel.ISupportInitialize)this.gridControl1).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.gridView1).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000007 RID: 7
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.Button btnExport;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.CheckBox checkDbo;

		// Token: 0x0400000A RID: 10
		private global::DevExpress.XtraGrid.GridControl gridControl1;

		// Token: 0x0400000B RID: 11
		private global::DevExpress.XtraGrid.Views.Grid.GridView gridView1;

		// Token: 0x0400000C RID: 12
		private global::DevExpress.XtraGrid.Columns.GridColumn gridColDboId;

		// Token: 0x0400000D RID: 13
		private global::DevExpress.XtraGrid.Columns.GridColumn gridColDboCode;

		// Token: 0x0400000E RID: 14
		private global::DevExpress.XtraGrid.Columns.GridColumn gridColDboName;

		// Token: 0x0400000F RID: 15
		private global::DevExpress.XtraGrid.Columns.GridColumn gridColDboSel;

		// Token: 0x04000010 RID: 16
		private global::DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
	}
}
