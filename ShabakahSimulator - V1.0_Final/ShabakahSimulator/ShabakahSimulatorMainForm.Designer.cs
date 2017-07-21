namespace ShabakahSimulator
{
    partial class ShabakahSimulatorMainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.grpBoxConfig = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRedirStatus = new System.Windows.Forms.Label();
            this.grpBoxRedir = new System.Windows.Forms.GroupBox();
            this.btnStopRedir = new System.Windows.Forms.Button();
            this.btnStartRedir = new System.Windows.Forms.Button();
            this.grpBoxAPI = new System.Windows.Forms.GroupBox();
            this.txtArmDisArm = new System.Windows.Forms.TextBox();
            this.lblNumArmDisArmPerMin = new System.Windows.Forms.Label();
            this.txtPanelsPerMin = new System.Windows.Forms.TextBox();
            this.lblConnPerMin = new System.Windows.Forms.Label();
            this.btnStopReq = new System.Windows.Forms.Button();
            this.btnStartReq = new System.Windows.Forms.Button();
            this.grpBoxReport = new System.Windows.Forms.GroupBox();
            this.grpRedirReport = new System.Windows.Forms.GroupBox();
            this.lblRespDisarm = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblRecDisarm = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRespArm = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecArm = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBoxDisArm = new System.Windows.Forms.GroupBox();
            this.lblDSFail = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDSSuccess = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblDSSent = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.grpBoxOpenSession = new System.Windows.Forms.GroupBox();
            this.lblOSFail = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblOSSuccess = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOSSent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grpBoxArm = new System.Windows.Forms.GroupBox();
            this.lblArmFail = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblArmSuccess = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblArmSent = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabDetailedReport = new System.Windows.Forms.TabPage();
            this.dgv_DetailedReport = new System.Windows.Forms.DataGridView();
            this.ColMAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSession = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColArm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDisArm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.grpBoxConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpBoxRedir.SuspendLayout();
            this.grpBoxAPI.SuspendLayout();
            this.grpBoxReport.SuspendLayout();
            this.grpRedirReport.SuspendLayout();
            this.grpBoxDisArm.SuspendLayout();
            this.grpBoxOpenSession.SuspendLayout();
            this.grpBoxArm.SuspendLayout();
            this.tabDetailedReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DetailedReport)).BeginInit();
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(795, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // tabConfig
            // 
            this.tabConfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabConfig.Controls.Add(this.grpBoxConfig);
            this.tabConfig.Controls.Add(this.grpBoxReport);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Size = new System.Drawing.Size(787, 395);
            this.tabConfig.TabIndex = 2;
            this.tabConfig.Text = "Configuration";
            // 
            // grpBoxConfig
            // 
            this.grpBoxConfig.Controls.Add(this.groupBox1);
            this.grpBoxConfig.Controls.Add(this.grpBoxRedir);
            this.grpBoxConfig.Controls.Add(this.grpBoxAPI);
            this.grpBoxConfig.Location = new System.Drawing.Point(20, 9);
            this.grpBoxConfig.Name = "grpBoxConfig";
            this.grpBoxConfig.Size = new System.Drawing.Size(582, 183);
            this.grpBoxConfig.TabIndex = 10;
            this.grpBoxConfig.TabStop = false;
            this.grpBoxConfig.Text = "Configuration";
            this.grpBoxConfig.Enter += new System.EventHandler(this.grpBoxConfig_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.lblRedirStatus);
            this.groupBox1.Location = new System.Drawing.Point(35, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 50);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection Status";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter_1);
            // 
            // lblRedirStatus
            // 
            this.lblRedirStatus.AutoSize = true;
            this.lblRedirStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedirStatus.Location = new System.Drawing.Point(11, 16);
            this.lblRedirStatus.Name = "lblRedirStatus";
            this.lblRedirStatus.Size = new System.Drawing.Size(143, 25);
            this.lblRedirStatus.TabIndex = 0;
            this.lblRedirStatus.Text = "Disconnected";
            this.lblRedirStatus.Click += new System.EventHandler(this.lblRedirStatus_Click);
            // 
            // grpBoxRedir
            // 
            this.grpBoxRedir.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBoxRedir.Controls.Add(this.btnStopRedir);
            this.grpBoxRedir.Controls.Add(this.btnStartRedir);
            this.grpBoxRedir.Location = new System.Drawing.Point(15, 19);
            this.grpBoxRedir.Name = "grpBoxRedir";
            this.grpBoxRedir.Size = new System.Drawing.Size(196, 74);
            this.grpBoxRedir.TabIndex = 0;
            this.grpBoxRedir.TabStop = false;
            this.grpBoxRedir.Text = "Redir";
            // 
            // btnStopRedir
            // 
            this.btnStopRedir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStopRedir.Location = new System.Drawing.Point(107, 19);
            this.btnStopRedir.Name = "btnStopRedir";
            this.btnStopRedir.Size = new System.Drawing.Size(75, 36);
            this.btnStopRedir.TabIndex = 2;
            this.btnStopRedir.Text = "Stop Redir";
            this.btnStopRedir.UseVisualStyleBackColor = true;
            this.btnStopRedir.Click += new System.EventHandler(this.btnStopRedir_Click);
            // 
            // btnStartRedir
            // 
            this.btnStartRedir.Location = new System.Drawing.Point(15, 19);
            this.btnStartRedir.Name = "btnStartRedir";
            this.btnStartRedir.Size = new System.Drawing.Size(75, 36);
            this.btnStartRedir.TabIndex = 1;
            this.btnStartRedir.Text = "Start Redir";
            this.btnStartRedir.UseVisualStyleBackColor = true;
            this.btnStartRedir.Click += new System.EventHandler(this.btnStartRedir_Click);
            // 
            // grpBoxAPI
            // 
            this.grpBoxAPI.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBoxAPI.Controls.Add(this.txtArmDisArm);
            this.grpBoxAPI.Controls.Add(this.lblNumArmDisArmPerMin);
            this.grpBoxAPI.Controls.Add(this.txtPanelsPerMin);
            this.grpBoxAPI.Controls.Add(this.lblConnPerMin);
            this.grpBoxAPI.Controls.Add(this.btnStopReq);
            this.grpBoxAPI.Controls.Add(this.btnStartReq);
            this.grpBoxAPI.Location = new System.Drawing.Point(229, 19);
            this.grpBoxAPI.Name = "grpBoxAPI";
            this.grpBoxAPI.Size = new System.Drawing.Size(335, 153);
            this.grpBoxAPI.TabIndex = 3;
            this.grpBoxAPI.TabStop = false;
            this.grpBoxAPI.Text = "PanelService";
            // 
            // txtArmDisArm
            // 
            this.txtArmDisArm.Location = new System.Drawing.Point(214, 58);
            this.txtArmDisArm.Name = "txtArmDisArm";
            this.txtArmDisArm.Size = new System.Drawing.Size(100, 20);
            this.txtArmDisArm.TabIndex = 6;
            this.txtArmDisArm.Text = "0";
            this.txtArmDisArm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfIDoXs_KeyPress);
            // 
            // lblNumArmDisArmPerMin
            // 
            this.lblNumArmDisArmPerMin.AutoSize = true;
            this.lblNumArmDisArmPerMin.Location = new System.Drawing.Point(19, 61);
            this.lblNumArmDisArmPerMin.Name = "lblNumArmDisArmPerMin";
            this.lblNumArmDisArmPerMin.Size = new System.Drawing.Size(195, 13);
            this.lblNumArmDisArmPerMin.TabIndex = 5;
            this.lblNumArmDisArmPerMin.Text = "Number of Arm/DisArm Request per min";
            // 
            // txtPanelsPerMin
            // 
            this.txtPanelsPerMin.Location = new System.Drawing.Point(214, 28);
            this.txtPanelsPerMin.Name = "txtPanelsPerMin";
            this.txtPanelsPerMin.Size = new System.Drawing.Size(100, 20);
            this.txtPanelsPerMin.TabIndex = 4;
            this.txtPanelsPerMin.Text = "0";
            this.txtPanelsPerMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfIDoXs_KeyPress);
            // 
            // lblConnPerMin
            // 
            this.lblConnPerMin.AutoSize = true;
            this.lblConnPerMin.Location = new System.Drawing.Point(19, 31);
            this.lblConnPerMin.Name = "lblConnPerMin";
            this.lblConnPerMin.Size = new System.Drawing.Size(187, 13);
            this.lblConnPerMin.TabIndex = 3;
            this.lblConnPerMin.Text = "Number of Panel Connections Per Min";
            // 
            // btnStopReq
            // 
            this.btnStopReq.Location = new System.Drawing.Point(239, 104);
            this.btnStopReq.Name = "btnStopReq";
            this.btnStopReq.Size = new System.Drawing.Size(75, 36);
            this.btnStopReq.TabIndex = 2;
            this.btnStopReq.Text = "Stop";
            this.btnStopReq.UseVisualStyleBackColor = true;
            this.btnStopReq.Click += new System.EventHandler(this.btnStopReq_Click);
            // 
            // btnStartReq
            // 
            this.btnStartReq.Location = new System.Drawing.Point(149, 104);
            this.btnStartReq.Name = "btnStartReq";
            this.btnStartReq.Size = new System.Drawing.Size(75, 36);
            this.btnStartReq.TabIndex = 1;
            this.btnStartReq.Text = "Start";
            this.btnStartReq.UseVisualStyleBackColor = true;
            this.btnStartReq.Click += new System.EventHandler(this.btnStartReq_Click);
            // 
            // grpBoxReport
            // 
            this.grpBoxReport.Controls.Add(this.grpRedirReport);
            this.grpBoxReport.Controls.Add(this.grpBoxDisArm);
            this.grpBoxReport.Controls.Add(this.grpBoxOpenSession);
            this.grpBoxReport.Controls.Add(this.grpBoxArm);
            this.grpBoxReport.Location = new System.Drawing.Point(20, 205);
            this.grpBoxReport.Name = "grpBoxReport";
            this.grpBoxReport.Size = new System.Drawing.Size(582, 152);
            this.grpBoxReport.TabIndex = 9;
            this.grpBoxReport.TabStop = false;
            this.grpBoxReport.Text = "Report";
            // 
            // grpRedirReport
            // 
            this.grpRedirReport.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpRedirReport.Controls.Add(this.lblRespDisarm);
            this.grpRedirReport.Controls.Add(this.label11);
            this.grpRedirReport.Controls.Add(this.lblRecDisarm);
            this.grpRedirReport.Controls.Add(this.label4);
            this.grpRedirReport.Controls.Add(this.lblRespArm);
            this.grpRedirReport.Controls.Add(this.label3);
            this.grpRedirReport.Controls.Add(this.lblRecArm);
            this.grpRedirReport.Controls.Add(this.label1);
            this.grpRedirReport.Location = new System.Drawing.Point(15, 19);
            this.grpRedirReport.Name = "grpRedirReport";
            this.grpRedirReport.Size = new System.Drawing.Size(132, 111);
            this.grpRedirReport.TabIndex = 3;
            this.grpRedirReport.TabStop = false;
            this.grpRedirReport.Text = "MockPanel";
            this.grpRedirReport.Enter += new System.EventHandler(this.grpRedirReport_Enter);
            // 
            // lblRespDisarm
            // 
            this.lblRespDisarm.AutoSize = true;
            this.lblRespDisarm.Location = new System.Drawing.Point(81, 78);
            this.lblRespDisarm.Name = "lblRespDisarm";
            this.lblRespDisarm.Size = new System.Drawing.Size(13, 13);
            this.lblRespDisarm.TabIndex = 10;
            this.lblRespDisarm.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "RespDisarm :";
            // 
            // lblRecDisarm
            // 
            this.lblRecDisarm.AutoSize = true;
            this.lblRecDisarm.Location = new System.Drawing.Point(81, 58);
            this.lblRecDisarm.Name = "lblRecDisarm";
            this.lblRecDisarm.Size = new System.Drawing.Size(13, 13);
            this.lblRecDisarm.TabIndex = 8;
            this.lblRecDisarm.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "RecDisarm :";
            // 
            // lblRespArm
            // 
            this.lblRespArm.AutoSize = true;
            this.lblRespArm.Location = new System.Drawing.Point(80, 38);
            this.lblRespArm.Name = "lblRespArm";
            this.lblRespArm.Size = new System.Drawing.Size(13, 13);
            this.lblRespArm.TabIndex = 6;
            this.lblRespArm.Text = "0";
            this.lblRespArm.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "RespArm : ";
            // 
            // lblRecArm
            // 
            this.lblRecArm.AutoSize = true;
            this.lblRecArm.Location = new System.Drawing.Point(80, 18);
            this.lblRecArm.Name = "lblRecArm";
            this.lblRecArm.Size = new System.Drawing.Size(13, 13);
            this.lblRecArm.TabIndex = 4;
            this.lblRecArm.Text = "0";
            this.lblRecArm.Click += new System.EventHandler(this.lblRedirSent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RecArm : ";
            // 
            // grpBoxDisArm
            // 
            this.grpBoxDisArm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBoxDisArm.Controls.Add(this.lblDSFail);
            this.grpBoxDisArm.Controls.Add(this.label8);
            this.grpBoxDisArm.Controls.Add(this.lblDSSuccess);
            this.grpBoxDisArm.Controls.Add(this.label13);
            this.grpBoxDisArm.Controls.Add(this.lblDSSent);
            this.grpBoxDisArm.Controls.Add(this.label15);
            this.grpBoxDisArm.Location = new System.Drawing.Point(446, 19);
            this.grpBoxDisArm.Name = "grpBoxDisArm";
            this.grpBoxDisArm.Size = new System.Drawing.Size(121, 111);
            this.grpBoxDisArm.TabIndex = 11;
            this.grpBoxDisArm.TabStop = false;
            this.grpBoxDisArm.Text = "TC-DisArm";
            // 
            // lblDSFail
            // 
            this.lblDSFail.AutoSize = true;
            this.lblDSFail.Location = new System.Drawing.Point(68, 79);
            this.lblDSFail.Name = "lblDSFail";
            this.lblDSFail.Size = new System.Drawing.Size(13, 13);
            this.lblDSFail.TabIndex = 8;
            this.lblDSFail.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Fail :";
            // 
            // lblDSSuccess
            // 
            this.lblDSSuccess.AutoSize = true;
            this.lblDSSuccess.Location = new System.Drawing.Point(68, 54);
            this.lblDSSuccess.Name = "lblDSSuccess";
            this.lblDSSuccess.Size = new System.Drawing.Size(13, 13);
            this.lblDSSuccess.TabIndex = 6;
            this.lblDSSuccess.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Success : ";
            // 
            // lblDSSent
            // 
            this.lblDSSent.AutoSize = true;
            this.lblDSSent.Location = new System.Drawing.Point(68, 28);
            this.lblDSSent.Name = "lblDSSent";
            this.lblDSSent.Size = new System.Drawing.Size(13, 13);
            this.lblDSSent.TabIndex = 4;
            this.lblDSSent.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Sent : ";
            // 
            // grpBoxOpenSession
            // 
            this.grpBoxOpenSession.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBoxOpenSession.Controls.Add(this.lblOSFail);
            this.grpBoxOpenSession.Controls.Add(this.label5);
            this.grpBoxOpenSession.Controls.Add(this.lblOSSuccess);
            this.grpBoxOpenSession.Controls.Add(this.label7);
            this.grpBoxOpenSession.Controls.Add(this.lblOSSent);
            this.grpBoxOpenSession.Controls.Add(this.label9);
            this.grpBoxOpenSession.Location = new System.Drawing.Point(163, 19);
            this.grpBoxOpenSession.Name = "grpBoxOpenSession";
            this.grpBoxOpenSession.Size = new System.Drawing.Size(126, 111);
            this.grpBoxOpenSession.TabIndex = 9;
            this.grpBoxOpenSession.TabStop = false;
            this.grpBoxOpenSession.Text = "TC-OpenSession";
            // 
            // lblOSFail
            // 
            this.lblOSFail.AutoSize = true;
            this.lblOSFail.Location = new System.Drawing.Point(68, 79);
            this.lblOSFail.Name = "lblOSFail";
            this.lblOSFail.Size = new System.Drawing.Size(13, 13);
            this.lblOSFail.TabIndex = 8;
            this.lblOSFail.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Fail :";
            // 
            // lblOSSuccess
            // 
            this.lblOSSuccess.AutoSize = true;
            this.lblOSSuccess.Location = new System.Drawing.Point(68, 54);
            this.lblOSSuccess.Name = "lblOSSuccess";
            this.lblOSSuccess.Size = new System.Drawing.Size(13, 13);
            this.lblOSSuccess.TabIndex = 6;
            this.lblOSSuccess.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Success : ";
            // 
            // lblOSSent
            // 
            this.lblOSSent.AutoSize = true;
            this.lblOSSent.Location = new System.Drawing.Point(68, 28);
            this.lblOSSent.Name = "lblOSSent";
            this.lblOSSent.Size = new System.Drawing.Size(13, 13);
            this.lblOSSent.TabIndex = 4;
            this.lblOSSent.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Sent : ";
            // 
            // grpBoxArm
            // 
            this.grpBoxArm.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpBoxArm.Controls.Add(this.lblArmFail);
            this.grpBoxArm.Controls.Add(this.label6);
            this.grpBoxArm.Controls.Add(this.lblArmSuccess);
            this.grpBoxArm.Controls.Add(this.label10);
            this.grpBoxArm.Controls.Add(this.lblArmSent);
            this.grpBoxArm.Controls.Add(this.label12);
            this.grpBoxArm.Location = new System.Drawing.Point(306, 19);
            this.grpBoxArm.Name = "grpBoxArm";
            this.grpBoxArm.Size = new System.Drawing.Size(125, 111);
            this.grpBoxArm.TabIndex = 10;
            this.grpBoxArm.TabStop = false;
            this.grpBoxArm.Text = "TC-Arm";
            // 
            // lblArmFail
            // 
            this.lblArmFail.AutoSize = true;
            this.lblArmFail.Location = new System.Drawing.Point(68, 79);
            this.lblArmFail.Name = "lblArmFail";
            this.lblArmFail.Size = new System.Drawing.Size(13, 13);
            this.lblArmFail.TabIndex = 8;
            this.lblArmFail.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Fail :";
            // 
            // lblArmSuccess
            // 
            this.lblArmSuccess.AutoSize = true;
            this.lblArmSuccess.Location = new System.Drawing.Point(68, 54);
            this.lblArmSuccess.Name = "lblArmSuccess";
            this.lblArmSuccess.Size = new System.Drawing.Size(13, 13);
            this.lblArmSuccess.TabIndex = 6;
            this.lblArmSuccess.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Success : ";
            // 
            // lblArmSent
            // 
            this.lblArmSent.AutoSize = true;
            this.lblArmSent.Location = new System.Drawing.Point(68, 28);
            this.lblArmSent.Name = "lblArmSent";
            this.lblArmSent.Size = new System.Drawing.Size(13, 13);
            this.lblArmSent.TabIndex = 4;
            this.lblArmSent.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Sent : ";
            // 
            // tabDetailedReport
            // 
            this.tabDetailedReport.Controls.Add(this.dgv_DetailedReport);
            this.tabDetailedReport.Location = new System.Drawing.Point(4, 22);
            this.tabDetailedReport.Name = "tabDetailedReport";
            this.tabDetailedReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetailedReport.Size = new System.Drawing.Size(787, 395);
            this.tabDetailedReport.TabIndex = 1;
            this.tabDetailedReport.Text = "DetailedReport";
            this.tabDetailedReport.UseVisualStyleBackColor = true;
            // 
            // dgv_DetailedReport
            // 
            this.dgv_DetailedReport.AllowUserToAddRows = false;
            this.dgv_DetailedReport.AllowUserToDeleteRows = false;
            this.dgv_DetailedReport.AllowUserToResizeColumns = false;
            this.dgv_DetailedReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DetailedReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_DetailedReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DetailedReport.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_DetailedReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DetailedReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMAC,
            this.ColSession,
            this.ColArm,
            this.ColDisArm});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DetailedReport.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_DetailedReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DetailedReport.Location = new System.Drawing.Point(3, 3);
            this.dgv_DetailedReport.Name = "dgv_DetailedReport";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DetailedReport.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_DetailedReport.ShowCellErrors = false;
            this.dgv_DetailedReport.ShowCellToolTips = false;
            this.dgv_DetailedReport.ShowEditingIcon = false;
            this.dgv_DetailedReport.ShowRowErrors = false;
            this.dgv_DetailedReport.Size = new System.Drawing.Size(781, 389);
            this.dgv_DetailedReport.TabIndex = 0;
            // 
            // ColMAC
            // 
            this.ColMAC.HeaderText = "MAC";
            this.ColMAC.Name = "ColMAC";
            this.ColMAC.ReadOnly = true;
            // 
            // ColSession
            // 
            this.ColSession.HeaderText = "Session";
            this.ColSession.Name = "ColSession";
            this.ColSession.ReadOnly = true;
            // 
            // ColArm
            // 
            this.ColArm.HeaderText = "Arm";
            this.ColArm.Name = "ColArm";
            this.ColArm.ReadOnly = true;
            // 
            // ColDisArm
            // 
            this.ColDisArm.HeaderText = "DisArm";
            this.ColDisArm.Name = "ColDisArm";
            this.ColDisArm.ReadOnly = true;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabConfig);
            this.TabControl.Controls.Add(this.tabDetailedReport);
            this.TabControl.Location = new System.Drawing.Point(24, 27);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(795, 421);
            this.TabControl.TabIndex = 1;
            // 
            // ShabakahSimulatorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 452);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ShabakahSimulatorMainForm";
            this.Text = "Shabakah Simulator";
            this.Load += new System.EventHandler(this.ShabakahSimulatorMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabConfig.ResumeLayout(false);
            this.grpBoxConfig.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpBoxRedir.ResumeLayout(false);
            this.grpBoxAPI.ResumeLayout(false);
            this.grpBoxAPI.PerformLayout();
            this.grpBoxReport.ResumeLayout(false);
            this.grpRedirReport.ResumeLayout(false);
            this.grpRedirReport.PerformLayout();
            this.grpBoxDisArm.ResumeLayout(false);
            this.grpBoxDisArm.PerformLayout();
            this.grpBoxOpenSession.ResumeLayout(false);
            this.grpBoxOpenSession.PerformLayout();
            this.grpBoxArm.ResumeLayout(false);
            this.grpBoxArm.PerformLayout();
            this.tabDetailedReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DetailedReport)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.GroupBox grpBoxConfig;
        private System.Windows.Forms.GroupBox grpBoxRedir;
        private System.Windows.Forms.Button btnStopRedir;
        private System.Windows.Forms.Button btnStartRedir;
        private System.Windows.Forms.GroupBox grpBoxAPI;
        private System.Windows.Forms.TextBox txtArmDisArm;
        private System.Windows.Forms.Label lblNumArmDisArmPerMin;
        private System.Windows.Forms.TextBox txtPanelsPerMin;
        private System.Windows.Forms.Label lblConnPerMin;
        private System.Windows.Forms.Button btnStopReq;
        private System.Windows.Forms.Button btnStartReq;
        private System.Windows.Forms.GroupBox grpBoxReport;
        private System.Windows.Forms.GroupBox grpRedirReport;
        private System.Windows.Forms.Label lblRecDisarm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRespArm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecArm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBoxDisArm;
        private System.Windows.Forms.Label lblDSFail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDSSuccess;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblDSSent;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox grpBoxOpenSession;
        private System.Windows.Forms.Label lblOSFail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblOSSuccess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOSSent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox grpBoxArm;
        private System.Windows.Forms.Label lblArmFail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblArmSuccess;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblArmSent;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabDetailedReport;
        private System.Windows.Forms.DataGridView dgv_DetailedReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSession;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColArm;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDisArm;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblRespDisarm;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRedirStatus;
    }
}

