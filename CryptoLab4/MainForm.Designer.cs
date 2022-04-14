
namespace CryptoLab4
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sequencePanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.findPrototypeButton = new System.Windows.Forms.Button();
            this.findCollisionButton = new System.Windows.Forms.Button();
            this.messageLengthLabel = new System.Windows.Forms.Label();
            this.hashLengthLabel = new System.Windows.Forms.Label();
            this.messageLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.hashLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ioLabel = new System.Windows.Forms.Label();
            this.ioRichTextBox = new System.Windows.Forms.RichTextBox();
            this.getHashButton = new System.Windows.Forms.Button();
            this.chartComboBox = new System.Windows.Forms.ComboBox();
            this.plotButton = new System.Windows.Forms.Button();
            this.settingsLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sequencePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageLengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashLengthNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sequencePanel
            // 
            this.sequencePanel.BackColor = System.Drawing.Color.Gainsboro;
            this.sequencePanel.Controls.Add(this.label2);
            this.sequencePanel.Controls.Add(this.findPrototypeButton);
            this.sequencePanel.Controls.Add(this.findCollisionButton);
            this.sequencePanel.Controls.Add(this.messageLengthLabel);
            this.sequencePanel.Controls.Add(this.hashLengthLabel);
            this.sequencePanel.Controls.Add(this.messageLengthNumericUpDown);
            this.sequencePanel.Controls.Add(this.hashLengthNumericUpDown);
            this.sequencePanel.Controls.Add(this.ioLabel);
            this.sequencePanel.Controls.Add(this.ioRichTextBox);
            this.sequencePanel.Controls.Add(this.getHashButton);
            this.sequencePanel.Location = new System.Drawing.Point(10, 9);
            this.sequencePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sequencePanel.Name = "sequencePanel";
            this.sequencePanel.Size = new System.Drawing.Size(331, 271);
            this.sequencePanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "bytes";
            // 
            // findPrototypeButton
            // 
            this.findPrototypeButton.Location = new System.Drawing.Point(230, 221);
            this.findPrototypeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.findPrototypeButton.Name = "findPrototypeButton";
            this.findPrototypeButton.Size = new System.Drawing.Size(98, 22);
            this.findPrototypeButton.TabIndex = 9;
            this.findPrototypeButton.Text = "Find prototype";
            this.findPrototypeButton.UseVisualStyleBackColor = true;
            this.findPrototypeButton.Click += new System.EventHandler(this.findPrototypeButton_Click);
            // 
            // findCollisionButton
            // 
            this.findCollisionButton.Location = new System.Drawing.Point(116, 221);
            this.findCollisionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.findCollisionButton.Name = "findCollisionButton";
            this.findCollisionButton.Size = new System.Drawing.Size(98, 22);
            this.findCollisionButton.TabIndex = 8;
            this.findCollisionButton.Text = "Find collision";
            this.findCollisionButton.UseVisualStyleBackColor = true;
            this.findCollisionButton.Click += new System.EventHandler(this.findCollisionButton_Click);
            // 
            // messageLengthLabel
            // 
            this.messageLengthLabel.AutoSize = true;
            this.messageLengthLabel.Location = new System.Drawing.Point(128, 247);
            this.messageLengthLabel.Name = "messageLengthLabel";
            this.messageLengthLabel.Size = new System.Drawing.Size(115, 15);
            this.messageLengthLabel.TabIndex = 7;
            this.messageLengthLabel.Text = "bits.Message length:";
            // 
            // hashLengthLabel
            // 
            this.hashLengthLabel.AutoSize = true;
            this.hashLengthLabel.Location = new System.Drawing.Point(3, 247);
            this.hashLengthLabel.Name = "hashLengthLabel";
            this.hashLengthLabel.Size = new System.Drawing.Size(74, 15);
            this.hashLengthLabel.TabIndex = 6;
            this.hashLengthLabel.Text = "Hash length:";
            // 
            // messageLengthNumericUpDown
            // 
            this.messageLengthNumericUpDown.Location = new System.Drawing.Point(249, 245);
            this.messageLengthNumericUpDown.Name = "messageLengthNumericUpDown";
            this.messageLengthNumericUpDown.Size = new System.Drawing.Size(39, 23);
            this.messageLengthNumericUpDown.TabIndex = 5;
            // 
            // hashLengthNumericUpDown
            // 
            this.hashLengthNumericUpDown.Location = new System.Drawing.Point(83, 245);
            this.hashLengthNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.hashLengthNumericUpDown.Name = "hashLengthNumericUpDown";
            this.hashLengthNumericUpDown.Size = new System.Drawing.Size(39, 23);
            this.hashLengthNumericUpDown.TabIndex = 4;
            // 
            // ioLabel
            // 
            this.ioLabel.AutoSize = true;
            this.ioLabel.Location = new System.Drawing.Point(3, 2);
            this.ioLabel.Name = "ioLabel";
            this.ioLabel.Size = new System.Drawing.Size(78, 15);
            this.ioLabel.TabIndex = 3;
            this.ioLabel.Text = "Input/Output";
            // 
            // ioRichTextBox
            // 
            this.ioRichTextBox.Location = new System.Drawing.Point(2, 19);
            this.ioRichTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ioRichTextBox.Name = "ioRichTextBox";
            this.ioRichTextBox.Size = new System.Drawing.Size(326, 198);
            this.ioRichTextBox.TabIndex = 0;
            this.ioRichTextBox.Text = "";
            // 
            // getHashButton
            // 
            this.getHashButton.Location = new System.Drawing.Point(3, 221);
            this.getHashButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getHashButton.Name = "getHashButton";
            this.getHashButton.Size = new System.Drawing.Size(98, 22);
            this.getHashButton.TabIndex = 2;
            this.getHashButton.Text = "Get hash";
            this.getHashButton.UseVisualStyleBackColor = true;
            this.getHashButton.Click += new System.EventHandler(this.getHashButton_Click);
            // 
            // chartComboBox
            // 
            this.chartComboBox.FormattingEnabled = true;
            this.chartComboBox.Items.AddRange(new object[] {
            "Avalanche effect (number of rounds)",
            "Avalanche effect (constant values)",
            "Collision search from hash length",
            "Prototype search from hash length",
            "Hashing time from message length"});
            this.chartComboBox.Location = new System.Drawing.Point(3, 245);
            this.chartComboBox.Name = "chartComboBox";
            this.chartComboBox.Size = new System.Drawing.Size(282, 23);
            this.chartComboBox.TabIndex = 6;
            // 
            // plotButton
            // 
            this.plotButton.Location = new System.Drawing.Point(291, 246);
            this.plotButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plotButton.Name = "plotButton";
            this.plotButton.Size = new System.Drawing.Size(38, 22);
            this.plotButton.TabIndex = 5;
            this.plotButton.Text = "Plot";
            this.plotButton.UseVisualStyleBackColor = true;
            this.plotButton.Click += new System.EventHandler(this.plotButton_Click);
            // 
            // settingsLabel
            // 
            this.settingsLabel.AutoSize = true;
            this.settingsLabel.Location = new System.Drawing.Point(3, 224);
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Size = new System.Drawing.Size(118, 15);
            this.settingsLabel.TabIndex = 2;
            this.settingsLabel.Text = "Select a chart to plot:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.chartComboBox);
            this.panel1.Controls.Add(this.settingsLabel);
            this.panel1.Controls.Add(this.plotButton);
            this.panel1.Location = new System.Drawing.Point(354, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 271);
            this.panel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(695, 289);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sequencePanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MD4";
            this.sequencePanel.ResumeLayout(false);
            this.sequencePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageLengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hashLengthNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel sequencePanel;
        private System.Windows.Forms.Label settingsLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox ioRichTextBox;
        private System.Windows.Forms.Button getHashButton;
        private System.Windows.Forms.ComboBox chartComboBox;
        private System.Windows.Forms.Button plotButton;
        private System.Windows.Forms.Label ioLabel;
        private System.Windows.Forms.Button findPrototypeButton;
        private System.Windows.Forms.Button findCollisionButton;
        private System.Windows.Forms.Label messageLengthLabel;
        private System.Windows.Forms.Label hashLengthLabel;
        private System.Windows.Forms.NumericUpDown messageLengthNumericUpDown;
        private System.Windows.Forms.NumericUpDown hashLengthNumericUpDown;
        private System.Windows.Forms.Label label2;
    }
}

