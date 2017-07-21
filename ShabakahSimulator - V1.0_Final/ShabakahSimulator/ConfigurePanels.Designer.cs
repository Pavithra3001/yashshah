namespace ShabakahSimulator
{
    partial class ConfigurePanels
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConfigureDevices = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.txtNoOfDevices = new System.Windows.Forms.TextBox();
            this.lblNumOfConfigDevices = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConfigureDevices
            // 
            this.btnConfigureDevices.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfigureDevices.Location = new System.Drawing.Point(446, 44);
            this.btnConfigureDevices.Name = "btnConfigureDevices";
            this.btnConfigureDevices.Size = new System.Drawing.Size(74, 25);
            this.btnConfigureDevices.TabIndex = 18;
            this.btnConfigureDevices.Text = "OK";
            this.btnConfigureDevices.UseVisualStyleBackColor = true;
            this.btnConfigureDevices.Click += new System.EventHandler(this.btnConfigureDevices_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(446, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(71, 24);
            this.btnBrowse.TabIndex = 16;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Location = new System.Drawing.Point(119, 18);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.Size = new System.Drawing.Size(321, 20);
            this.txtConfigFile.TabIndex = 15;
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(12, 21);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(91, 13);
            this.lblConfigFile.TabIndex = 20;
            this.lblConfigFile.Text = "Configuration File:";
            // 
            // txtNoOfDevices
            // 
            this.txtNoOfDevices.Location = new System.Drawing.Point(119, 51);
            this.txtNoOfDevices.Name = "txtNoOfDevices";
            this.txtNoOfDevices.Size = new System.Drawing.Size(77, 20);
            this.txtNoOfDevices.TabIndex = 17;
            this.txtNoOfDevices.Text = "10";
            // 
            // lblNumOfConfigDevices
            // 
            this.lblNumOfConfigDevices.AutoSize = true;
            this.lblNumOfConfigDevices.Location = new System.Drawing.Point(12, 53);
            this.lblNumOfConfigDevices.Name = "lblNumOfConfigDevices";
            this.lblNumOfConfigDevices.Size = new System.Drawing.Size(101, 13);
            this.lblNumOfConfigDevices.TabIndex = 19;
            this.lblNumOfConfigDevices.Text = "Number of Devices:\r\n";
            // 
            // ConfigurePanels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 91);
            this.Controls.Add(this.btnConfigureDevices);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtConfigFile);
            this.Controls.Add(this.lblConfigFile);
            this.Controls.Add(this.txtNoOfDevices);
            this.Controls.Add(this.lblNumOfConfigDevices);
            this.Name = "ConfigurePanels";
            this.Text = "ConfigurePanels";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfigureDevices;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.Label lblConfigFile;
        private System.Windows.Forms.TextBox txtNoOfDevices;
        private System.Windows.Forms.Label lblNumOfConfigDevices;
    }
}