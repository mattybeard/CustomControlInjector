namespace CustomControlInjector.Plugin
{
    partial class CustomControlInjectorControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bpfList = new System.Windows.Forms.ComboBox();
            this.selectBPF = new System.Windows.Forms.Label();
            this.loadBPF = new System.Windows.Forms.Button();
            this.loadBPFDetails = new System.Windows.Forms.Button();
            this.entityComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fieldsComboBox = new System.Windows.Forms.ComboBox();
            this.customControlComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bpfFieldList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.copyWebButton = new System.Windows.Forms.Button();
            this.copyPhoneButton = new System.Windows.Forms.Button();
            this.copyTabletButton = new System.Windows.Forms.Button();
            this.copyAllButton = new System.Windows.Forms.Button();
            this.publishAllButton = new System.Windows.Forms.Button();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(871, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(86, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bpfList
            // 
            this.bpfList.Enabled = false;
            this.bpfList.FormattingEnabled = true;
            this.bpfList.Location = new System.Drawing.Point(304, 101);
            this.bpfList.Name = "bpfList";
            this.bpfList.Size = new System.Drawing.Size(344, 21);
            this.bpfList.TabIndex = 7;
            // 
            // selectBPF
            // 
            this.selectBPF.AutoSize = true;
            this.selectBPF.Location = new System.Drawing.Point(41, 104);
            this.selectBPF.Name = "selectBPF";
            this.selectBPF.Size = new System.Drawing.Size(257, 13);
            this.selectBPF.TabIndex = 9;
            this.selectBPF.Text = "Which Business Process Flow would you like to edit?";
            // 
            // loadBPF
            // 
            this.loadBPF.Location = new System.Drawing.Point(29, 51);
            this.loadBPF.Name = "loadBPF";
            this.loadBPF.Size = new System.Drawing.Size(619, 31);
            this.loadBPF.TabIndex = 6;
            this.loadBPF.Text = "Load Business Process Flows";
            this.loadBPF.UseVisualStyleBackColor = true;
            this.loadBPF.Click += new System.EventHandler(this.loadBPF_Click);
            // 
            // loadBPFDetails
            // 
            this.loadBPFDetails.Location = new System.Drawing.Point(654, 95);
            this.loadBPFDetails.Name = "loadBPFDetails";
            this.loadBPFDetails.Size = new System.Drawing.Size(88, 31);
            this.loadBPFDetails.TabIndex = 6;
            this.loadBPFDetails.Text = "Load";
            this.loadBPFDetails.UseVisualStyleBackColor = true;
            this.loadBPFDetails.Click += new System.EventHandler(this.loadBPFDetails_Click);
            // 
            // entityComboBox
            // 
            this.entityComboBox.Enabled = false;
            this.entityComboBox.FormattingEnabled = true;
            this.entityComboBox.Location = new System.Drawing.Point(304, 195);
            this.entityComboBox.Name = "entityComboBox";
            this.entityComboBox.Size = new System.Drawing.Size(344, 21);
            this.entityComboBox.TabIndex = 7;
            this.entityComboBox.SelectedIndexChanged += new System.EventHandler(this.entityComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Entity Types Available on BPF:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fields with custom controls:";
            // 
            // fieldsComboBox
            // 
            this.fieldsComboBox.Enabled = false;
            this.fieldsComboBox.FormattingEnabled = true;
            this.fieldsComboBox.Location = new System.Drawing.Point(304, 222);
            this.fieldsComboBox.Name = "fieldsComboBox";
            this.fieldsComboBox.Size = new System.Drawing.Size(344, 21);
            this.fieldsComboBox.TabIndex = 7;
            this.fieldsComboBox.SelectedIndexChanged += new System.EventHandler(this.fieldsComboBox_SelectedIndexChanged);
            // 
            // customControlComboBox
            // 
            this.customControlComboBox.Enabled = false;
            this.customControlComboBox.FormattingEnabled = true;
            this.customControlComboBox.Location = new System.Drawing.Point(304, 252);
            this.customControlComboBox.Name = "customControlComboBox";
            this.customControlComboBox.Size = new System.Drawing.Size(344, 21);
            this.customControlComboBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Custom controls:";
            // 
            // bpfFieldList
            // 
            this.bpfFieldList.Enabled = false;
            this.bpfFieldList.FormattingEnabled = true;
            this.bpfFieldList.Location = new System.Drawing.Point(304, 131);
            this.bpfFieldList.Name = "bpfFieldList";
            this.bpfFieldList.Size = new System.Drawing.Size(344, 21);
            this.bpfFieldList.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Which field on the BPF would you like to edit?";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // copyWebButton
            // 
            this.copyWebButton.Location = new System.Drawing.Point(44, 294);
            this.copyWebButton.Name = "copyWebButton";
            this.copyWebButton.Size = new System.Drawing.Size(215, 31);
            this.copyWebButton.TabIndex = 6;
            this.copyWebButton.Text = "Copy to \"Web\" layout";
            this.copyWebButton.UseVisualStyleBackColor = true;
            this.copyWebButton.Click += new System.EventHandler(this.copyWebButton_Click);
            // 
            // copyPhoneButton
            // 
            this.copyPhoneButton.Location = new System.Drawing.Point(265, 294);
            this.copyPhoneButton.Name = "copyPhoneButton";
            this.copyPhoneButton.Size = new System.Drawing.Size(215, 31);
            this.copyPhoneButton.TabIndex = 6;
            this.copyPhoneButton.Text = "Copy to \"Phone\" layout";
            this.copyPhoneButton.UseVisualStyleBackColor = true;
            this.copyPhoneButton.Click += new System.EventHandler(this.copyPhoneButton_Click);
            // 
            // copyTabletButton
            // 
            this.copyTabletButton.Location = new System.Drawing.Point(486, 294);
            this.copyTabletButton.Name = "copyTabletButton";
            this.copyTabletButton.Size = new System.Drawing.Size(215, 31);
            this.copyTabletButton.TabIndex = 6;
            this.copyTabletButton.Text = "Copy to \"Tablet\" layout";
            this.copyTabletButton.UseVisualStyleBackColor = true;
            this.copyTabletButton.Click += new System.EventHandler(this.copyTabletButton_Click);
            // 
            // copyAllButton
            // 
            this.copyAllButton.Location = new System.Drawing.Point(44, 331);
            this.copyAllButton.Name = "copyAllButton";
            this.copyAllButton.Size = new System.Drawing.Size(657, 31);
            this.copyAllButton.TabIndex = 6;
            this.copyAllButton.Text = "Copy to all layouts";
            this.copyAllButton.UseVisualStyleBackColor = true;
            this.copyAllButton.Click += new System.EventHandler(this.copyAllButton_Click);
            // 
            // publishAllButton
            // 
            this.publishAllButton.Location = new System.Drawing.Point(44, 368);
            this.publishAllButton.Name = "publishAllButton";
            this.publishAllButton.Size = new System.Drawing.Size(657, 31);
            this.publishAllButton.TabIndex = 6;
            this.publishAllButton.Text = "Publish All";
            this.publishAllButton.UseVisualStyleBackColor = true;
            this.publishAllButton.Click += new System.EventHandler(this.publishAllButton_Click);
            // 
            // CustomControlInjectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customControlComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.selectBPF);
            this.Controls.Add(this.fieldsComboBox);
            this.Controls.Add(this.entityComboBox);
            this.Controls.Add(this.bpfFieldList);
            this.Controls.Add(this.bpfList);
            this.Controls.Add(this.loadBPFDetails);
            this.Controls.Add(this.publishAllButton);
            this.Controls.Add(this.copyAllButton);
            this.Controls.Add(this.copyTabletButton);
            this.Controls.Add(this.copyPhoneButton);
            this.Controls.Add(this.copyWebButton);
            this.Controls.Add(this.loadBPF);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "CustomControlInjectorControl";
            this.Size = new System.Drawing.Size(871, 901);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ComboBox bpfList;
        private System.Windows.Forms.Label selectBPF;
        private System.Windows.Forms.Button loadBPF;
        private System.Windows.Forms.Button loadBPFDetails;
        private System.Windows.Forms.ComboBox entityComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox fieldsComboBox;
        private System.Windows.Forms.ComboBox customControlComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox bpfFieldList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button copyWebButton;
        private System.Windows.Forms.Button copyPhoneButton;
        private System.Windows.Forms.Button copyTabletButton;
        private System.Windows.Forms.Button copyAllButton;
        private System.Windows.Forms.Button publishAllButton;
    }
}
