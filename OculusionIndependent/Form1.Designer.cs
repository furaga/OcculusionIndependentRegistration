namespace OculusionIndependent
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sketchCheckBox = new System.Windows.Forms.CheckBox();
            this.bridgeCheckBox = new System.Windows.Forms.CheckBox();
            this.circleCheckBox = new System.Windows.Forms.CheckBox();
            this.salientCurveCheckBox = new System.Windows.Forms.CheckBox();
            this.sketchControl1 = new OculusionIndependent.SketchControl();
            this.TJunctionCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sketchCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.bridgeCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.circleCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.salientCurveCheckBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sketchControl1);
            this.splitContainer1.Size = new System.Drawing.Size(751, 483);
            this.splitContainer1.SplitterDistance = 155;
            this.splitContainer1.TabIndex = 1;
            // 
            // sketchCheckBox
            // 
            this.sketchCheckBox.AutoSize = true;
            this.sketchCheckBox.Checked = true;
            this.sketchCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sketchCheckBox.Location = new System.Drawing.Point(12, 12);
            this.sketchCheckBox.Name = "sketchCheckBox";
            this.sketchCheckBox.Size = new System.Drawing.Size(59, 16);
            this.sketchCheckBox.TabIndex = 4;
            this.sketchCheckBox.Text = "Sketch";
            this.sketchCheckBox.UseVisualStyleBackColor = true;
            this.sketchCheckBox.CheckedChanged += new System.EventHandler(this.sketchCheckBox_CheckedChanged);
            // 
            // bridgeCheckBox
            // 
            this.bridgeCheckBox.AutoSize = true;
            this.bridgeCheckBox.Checked = true;
            this.bridgeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bridgeCheckBox.Location = new System.Drawing.Point(12, 115);
            this.bridgeCheckBox.Name = "bridgeCheckBox";
            this.bridgeCheckBox.Size = new System.Drawing.Size(96, 16);
            this.bridgeCheckBox.TabIndex = 2;
            this.bridgeCheckBox.Text = "connect curve";
            this.bridgeCheckBox.UseVisualStyleBackColor = true;
            this.bridgeCheckBox.CheckedChanged += new System.EventHandler(this.bridgeCheckBox_CheckedChanged);
            // 
            // circleCheckBox
            // 
            this.circleCheckBox.AutoSize = true;
            this.circleCheckBox.Checked = true;
            this.circleCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.circleCheckBox.Location = new System.Drawing.Point(12, 93);
            this.circleCheckBox.Name = "circleCheckBox";
            this.circleCheckBox.Size = new System.Drawing.Size(119, 16);
            this.circleCheckBox.TabIndex = 1;
            this.circleCheckBox.Text = "extrapolated circle";
            this.circleCheckBox.UseVisualStyleBackColor = true;
            this.circleCheckBox.CheckedChanged += new System.EventHandler(this.bridgeCheckBox_CheckedChanged);
            // 
            // salientCurveCheckBox
            // 
            this.salientCurveCheckBox.AutoSize = true;
            this.salientCurveCheckBox.Checked = true;
            this.salientCurveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.salientCurveCheckBox.Location = new System.Drawing.Point(12, 71);
            this.salientCurveCheckBox.Name = "salientCurveCheckBox";
            this.salientCurveCheckBox.Size = new System.Drawing.Size(90, 16);
            this.salientCurveCheckBox.TabIndex = 0;
            this.salientCurveCheckBox.Text = "salient curve";
            this.salientCurveCheckBox.UseVisualStyleBackColor = true;
            this.salientCurveCheckBox.CheckedChanged += new System.EventHandler(this.bridgeCheckBox_CheckedChanged);
            // 
            // sketchControl1
            // 
            this.sketchControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sketchControl1.Location = new System.Drawing.Point(0, 0);
            this.sketchControl1.Name = "sketchControl1";
            this.sketchControl1.Size = new System.Drawing.Size(592, 483);
            this.sketchControl1.TabIndex = 0;
            // 
            // TJunctionCheckBox
            // 
            this.TJunctionCheckBox.AutoSize = true;
            this.TJunctionCheckBox.Checked = true;
            this.TJunctionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TJunctionCheckBox.Location = new System.Drawing.Point(12, 49);
            this.TJunctionCheckBox.Name = "TJunctionCheckBox";
            this.TJunctionCheckBox.Size = new System.Drawing.Size(77, 16);
            this.TJunctionCheckBox.TabIndex = 3;
            this.TJunctionCheckBox.Text = "T-junction";
            this.TJunctionCheckBox.UseVisualStyleBackColor = true;
            this.TJunctionCheckBox.CheckedChanged += new System.EventHandler(this.bridgeCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 483);
            this.Controls.Add(this.TJunctionCheckBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SketchControl sketchControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox bridgeCheckBox;
        private System.Windows.Forms.CheckBox circleCheckBox;
        private System.Windows.Forms.CheckBox salientCurveCheckBox;
        private System.Windows.Forms.CheckBox TJunctionCheckBox;
        private System.Windows.Forms.CheckBox sketchCheckBox;

    }
}

