namespace DataExport
{
	// Token: 0x0200000C RID: 12
	public partial class TabDataFrm : global::DevExpress.XtraEditors.XtraForm
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000619C File Offset: 0x0000439C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000061D4 File Offset: 0x000043D4
		private void InitializeComponent()
		{
			this.repositoryItemCheckEdit1 = new global::DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.splitContainer2 = new global::System.Windows.Forms.SplitContainer();
			this.selfRowLeftgridControl = new global::DevExpress.XtraGrid.GridControl();
			this.selfRowLeftGridView = new global::DevExpress.XtraGrid.Views.Grid.GridView();
			this.colDboCode = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.selectTab = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.colDboName = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.colDboId = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColCount = new global::DevExpress.XtraGrid.Columns.GridColumn();
			this.splitContainer3 = new global::System.Windows.Forms.SplitContainer();
			this.toolStrip1 = new global::System.Windows.Forms.ToolStrip();
			this.AllSelectBtn = new global::System.Windows.Forms.ToolStripButton();
			this.ContrayBtn = new global::System.Windows.Forms.ToolStripButton();
			this.AllNotSelectBtn = new global::System.Windows.Forms.ToolStripButton();
			this.exportBtn = new global::System.Windows.Forms.ToolStripButton();
			this.selfRowRightGridControl = new global::DevExpress.XtraGrid.GridControl();
			this.selfRowRightGridView = new global::DevExpress.XtraGrid.Views.Grid.GridView();
			((global::System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowLeftgridControl).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowLeftGridView).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowRightGridControl).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowRightGridView).BeginInit();
			base.SuspendLayout();
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			this.splitContainer2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new global::System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Panel1.Controls.Add(this.selfRowLeftgridControl);
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Size = new global::System.Drawing.Size(929, 495);
			this.splitContainer2.SplitterDistance = 300;
			this.splitContainer2.TabIndex = 13;
			this.selfRowLeftgridControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.selfRowLeftgridControl.Location = new global::System.Drawing.Point(0, 0);
			this.selfRowLeftgridControl.MainView = this.selfRowLeftGridView;
			this.selfRowLeftgridControl.Name = "selfRowLeftgridControl";
			this.selfRowLeftgridControl.Size = new global::System.Drawing.Size(300, 495);
			this.selfRowLeftgridControl.TabIndex = 1;
			this.selfRowLeftgridControl.ViewCollection.AddRange(new global::DevExpress.XtraGrid.Views.Base.BaseView[]
			{
				this.selfRowLeftGridView
			});
			this.selfRowLeftGridView.AutoFilterMode = global::DevExpress.XtraGrid.Views.Grid.AutoFilterModes.ClientMode;
			this.selfRowLeftGridView.Columns.AddRange(new global::DevExpress.XtraGrid.Columns.GridColumn[]
			{
				this.colDboCode,
				this.selectTab,
				this.colDboName,
				this.colDboId,
				this.gridColCount
			});
			this.selfRowLeftGridView.GridControl = this.selfRowLeftgridControl;
			this.selfRowLeftGridView.IndicatorWidth = 40;
			this.selfRowLeftGridView.Name = "selfRowLeftGridView";
			this.selfRowLeftGridView.OptionsBehavior.AutoExpandAllGroups = true;
			this.selfRowLeftGridView.OptionsDetail.EnableMasterViewMode = false;
			this.selfRowLeftGridView.OptionsView.AutoCalcPreviewLineCount = true;
			this.selfRowLeftGridView.OptionsView.EnableAppearanceEvenRow = true;
			this.selfRowLeftGridView.OptionsView.EnableAppearanceOddRow = true;
			this.selfRowLeftGridView.OptionsView.ShowAutoFilterRow = true;
			this.selfRowLeftGridView.OptionsView.ShowFooter = true;
			this.selfRowLeftGridView.OptionsView.ShowGroupPanel = false;
			this.selfRowLeftGridView.CustomDrawRowIndicator += new global::DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.selfRowLeftGridView_CustomDrawRowIndicator);
			this.selfRowLeftGridView.FocusedRowChanged += new global::DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.selfRowLeftGridView_FocusedRowChanged);
			this.colDboCode.Caption = "Dbo编号";
			this.colDboCode.FieldName = "DboCode";
			this.colDboCode.Name = "colDboCode";
			this.colDboCode.OptionsColumn.AllowEdit = false;
			this.colDboCode.Visible = true;
			this.colDboCode.VisibleIndex = 1;
			this.colDboCode.Width = 129;
			this.selectTab.Caption = "选择";
			this.selectTab.ColumnEdit = this.repositoryItemCheckEdit1;
			this.repositoryItemCheckEdit1.CheckedChanged += new global::System.EventHandler(this.RepositoryItemCheckEdit1_CheckedChanged);
			this.selectTab.FieldName = "DboSel";
			this.selectTab.Name = "selectTab";
			this.selectTab.Visible = true;
			this.selectTab.VisibleIndex = 0;
			this.selectTab.Width = 38;
			this.colDboName.Caption = "Dbo名称";
			this.colDboName.FieldName = "DboName";
			this.colDboName.Name = "colDboName";
			this.colDboName.OptionsColumn.AllowEdit = false;
			this.colDboName.Visible = true;
			this.colDboName.VisibleIndex = 2;
			this.colDboName.Width = 115;
			this.colDboId.Caption = "DboId";
			this.colDboId.FieldName = "DboId";
			this.colDboId.Name = "colDboId";
			this.gridColCount.Caption = "已选数量";
			this.gridColCount.FieldName = "SelectedCount";
			this.gridColCount.Name = "gridColCount";
			this.gridColCount.OptionsColumn.AllowEdit = false;
			this.gridColCount.Visible = true;
			this.gridColCount.VisibleIndex = 3;
			this.splitContainer3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = global::System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer3.Location = new global::System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = global::System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer3.Panel1.Controls.Add(this.toolStrip1);
			this.splitContainer3.Panel2.Controls.Add(this.selfRowRightGridControl);
			this.splitContainer3.Size = new global::System.Drawing.Size(625, 495);
			this.splitContainer3.SplitterDistance = 35;
			this.splitContainer3.TabIndex = 11;
			this.toolStrip1.Dock = global::System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AllSelectBtn,
				this.ContrayBtn,
				this.AllNotSelectBtn,
				this.exportBtn
			});
			this.toolStrip1.Location = new global::System.Drawing.Point(8, 8);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new global::System.Drawing.Size(257, 25);
			this.toolStrip1.TabIndex = 23;
			this.toolStrip1.Text = "toolStrip1";
			this.AllSelectBtn.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.AllSelectBtn.Name = "AllSelectBtn";
			this.AllSelectBtn.Size = new global::System.Drawing.Size(52, 22);
			this.AllSelectBtn.Text = "全选(&A)";
			this.AllSelectBtn.Click += new global::System.EventHandler(this.AllSelectBtn_Click);
			this.ContrayBtn.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.ContrayBtn.Name = "ContrayBtn";
			this.ContrayBtn.Size = new global::System.Drawing.Size(52, 22);
			this.ContrayBtn.Text = "反选(&C)";
			this.ContrayBtn.Click += new global::System.EventHandler(this.ContrayBtn_Click);
			this.AllNotSelectBtn.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.AllNotSelectBtn.Name = "AllNotSelectBtn";
			this.AllNotSelectBtn.Size = new global::System.Drawing.Size(66, 22);
			this.AllNotSelectBtn.Text = "全不选(&N)";
			this.AllNotSelectBtn.Click += new global::System.EventHandler(this.AllNotSelectBtn_Click);
			this.exportBtn.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.exportBtn.Name = "exportBtn";
			this.exportBtn.Size = new global::System.Drawing.Size(75, 22);
			this.exportBtn.Text = "导出数据(&E)";
			this.exportBtn.Click += new global::System.EventHandler(this.exportBtn_Click);
			this.selfRowRightGridControl.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.selfRowRightGridControl.Location = new global::System.Drawing.Point(0, 0);
			this.selfRowRightGridControl.MainView = this.selfRowRightGridView;
			this.selfRowRightGridControl.Name = "selfRowRightGridControl";
			this.selfRowRightGridControl.Size = new global::System.Drawing.Size(625, 456);
			this.selfRowRightGridControl.TabIndex = 2;
			this.selfRowRightGridControl.ViewCollection.AddRange(new global::DevExpress.XtraGrid.Views.Base.BaseView[]
			{
				this.selfRowRightGridView
			});
			this.selfRowRightGridView.AutoFilterMode = global::DevExpress.XtraGrid.Views.Grid.AutoFilterModes.ClientMode;
			this.selfRowRightGridView.GridControl = this.selfRowRightGridControl;
			this.selfRowRightGridView.Name = "selfRowRightGridView";
			this.selfRowRightGridView.OptionsDetail.EnableMasterViewMode = false;
			this.selfRowRightGridView.OptionsView.AutoCalcPreviewLineCount = true;
			this.selfRowRightGridView.OptionsView.ColumnAutoWidth = false;
			this.selfRowRightGridView.OptionsView.EnableAppearanceEvenRow = true;
			this.selfRowRightGridView.OptionsView.EnableAppearanceOddRow = true;
			this.selfRowRightGridView.OptionsView.ShowAutoFilterRow = true;
			this.selfRowRightGridView.OptionsView.ShowGroupPanel = false;
			this.selfRowRightGridView.CustomDrawRowIndicator += new global::DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.selfRowRightGridView_CustomDrawRowIndicator);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 14f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(929, 495);
			base.Controls.Add(this.splitContainer2);
			base.Name = "TabDataFrm";
			this.Text = "配置数据导出";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new global::System.EventHandler(this.TabDataFrm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.selfRowLeftgridControl).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowLeftGridView).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowRightGridControl).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.selfRowRightGridView).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400003C RID: 60
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.SplitContainer splitContainer2;

		// Token: 0x0400003E RID: 62
		private global::DevExpress.XtraGrid.GridControl selfRowLeftgridControl;

		// Token: 0x0400003F RID: 63
		private global::DevExpress.XtraGrid.Views.Grid.GridView selfRowLeftGridView;

		// Token: 0x04000040 RID: 64
		private global::DevExpress.XtraGrid.Columns.GridColumn colDboCode;

		// Token: 0x04000041 RID: 65
		private global::DevExpress.XtraGrid.Columns.GridColumn selectTab;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.SplitContainer splitContainer3;

		// Token: 0x04000043 RID: 67
		private global::DevExpress.XtraGrid.GridControl selfRowRightGridControl;

		// Token: 0x04000044 RID: 68
		private global::DevExpress.XtraGrid.Views.Grid.GridView selfRowRightGridView;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.ToolStrip toolStrip1;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.ToolStripButton AllSelectBtn;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.ToolStripButton ContrayBtn;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.ToolStripButton AllNotSelectBtn;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.ToolStripButton exportBtn;

		// Token: 0x0400004A RID: 74
		private global::DevExpress.XtraGrid.Columns.GridColumn colDboName;

		// Token: 0x0400004B RID: 75
		private global::DevExpress.XtraGrid.Columns.GridColumn colDboId;

		// Token: 0x0400004C RID: 76
		private global::DevExpress.XtraGrid.Columns.GridColumn gridColCount;

		// Token: 0x0400004D RID: 77
		private global::DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
	}
}
