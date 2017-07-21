using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShabakahRedir;
using Honeywell.SimulationFramework.LoggerComponent;
using System.IO;
using System.Configuration;
using System.Threading;
using ShabakahRedir;
using ShabakahEntityManager;
namespace ShabakahSimulator
{
    public partial class ShabakahSimulatorMainForm : Form
    {
        private ShabakahRedir.MockRedir MockRedir;
        private string ConfigFileName;
        private int NumberofDevicesToBeSimulated;
        private List<PanelServices> PanelMAC;
        private Logger m_logger;
        private Thread thDetReportUpdate;
        private bool blnUpdateDet = false;

        private int PanelsPerMin = 0;
        private int Arm_DisArm_Min = 0;
        Thread thArmDisArm;

        delegate void TempDelegate();
        
        public ShabakahSimulatorMainForm()
        {
            InitializeComponent();
            
            //Logger 
            bool valid = false;
            string level = ConfigurationSettings.AppSettings["LoggerLevel"];
            Level loggerLevel = GetLoggerLevel(level);
            string logFileSizeInMB = ConfigurationSettings.AppSettings["LogFileSizeInMB"];
            float logFileSize;

            valid = float.TryParse(logFileSizeInMB, out logFileSize);
            if (!valid)
            {
                logFileSize = 5;
            }
            m_logger = new Logger("SimulatorLog.txt", true, loggerLevel);
            m_logger.RollOverFileSizeInMB = logFileSize;

            //MockRedir = new ShabakahRedir.MockRedir(m_logger);
        }

        private Level GetLoggerLevel(string level)
        {
            int levelValue;
            bool valid = Int32.TryParse(level, out levelValue);

            if (!valid)
                return Level.MEDIUM;

            if (levelValue == (int)Level.TRACE)
            {
                return Level.TRACE;
            }

            if (levelValue >= (int)Level.MEDIUM)
            {
                return Level.MEDIUM;
            }

            if (levelValue >= (int)Level.LOW)
            {
                return Level.LOW;
            }

            if (levelValue >= (int)Level.HIGH)
            {
                return Level.HIGH;
            }

            return Level.CRITICAL;
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - configureToolStripMenuItem_Click - Start");

                //if (btnStopRedir.Enabled && btnStartRedir.Enabled)
                //{
                //    MessageBox.Show("Stop Redir Server", "ShabakahSimulator");
                //}

                ConfigurePanels configDevicesForm = new ConfigurePanels();
                if (configDevicesForm.ShowDialog() == DialogResult.OK)
                {
                    if (thDetReportUpdate != null)
                    {
                        thDetReportUpdate.Abort();
                        thDetReportUpdate = null;

                        blnUpdateDet = false;
                    }

                    ConfigFileName = configDevicesForm.m_ConfigFileName;
                    NumberofDevicesToBeSimulated = configDevicesForm.m_NumberofDevices;

                    if (ConfigureDevicesCSV())
                    {
                        TabControl.Enabled = true;

                        //In DetailedReport tab
                        dgv_DetailedReport.Enabled = true;

                        //In Config tab
                        btnStartRedir.Enabled = true;
                        btnStopRedir.Enabled = true;

                        btnStartReq.Enabled = false;
                        btnStopRedir.Enabled = false;

                        txtPanelsPerMin.Enabled = true;
                        txtArmDisArm.Enabled = true;

                        grpBoxReport.Enabled = true;

                        //InitializeDeviceCounters();
                        PopulateDetailedReportsTable();

                        blnUpdateDet = true;
                        thDetReportUpdate = new Thread(new ThreadStart(UpdateDetailReportTable));
                        thDetReportUpdate.Start();
                    }
                }

                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - configureToolStripMenuItem_Click - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "ShabakahSimulatorMainForm - configureToolStripMenuItem_Click - Exception - " + ex.Message);
            }
        }

        private void UpdateDetailReportTable()
        {
            try
            {
                int RowNum = 0;

                while (blnUpdateDet)
                {
                    //Update Total Report details
                    TempDelegate td1 = delegate()
                    {
                        lblRedirStatus.Text = MockRedir.RedirStatus.ToString();

                        lblRecArm.Text = MockPanel.RecArm.ToString();
                        lblRespArm.Text = MockPanel.RespArm.ToString();
                        lblRecDisarm.Text = MockPanel.RecDisarm.ToString();
                        lblRespDisarm.Text = MockPanel.RespDisarm.ToString();

                        lblOSSent.Text = PanelServices.totSessionSent.ToString();
                        lblOSSuccess.Text = PanelServices.totSessionSuccess.ToString();
                        lblOSFail.Text = PanelServices.totSessionFailed.ToString();

                        lblArmSent.Text = PanelServices.totArmSent.ToString();
                        lblArmSuccess.Text = PanelServices.totArmSuccess.ToString();
                        lblArmFail.Text = PanelServices.totArmFailed.ToString();

                        lblDSSent.Text = PanelServices.totDisArmSent.ToString();
                        lblDSSuccess.Text = PanelServices.totDisArmSuccess.ToString();
                        lblDSFail.Text = PanelServices.totDisArmFailed.ToString();
                    };
                    this.BeginInvoke(td1);

                    RowNum = 0;
                    foreach (var panel in PanelMAC)
                    {

                        TempDelegate td = delegate()
                        {
                            if (RowNum < dgv_DetailedReport.Rows.Count)
                            {
                                dgv_DetailedReport.Rows[RowNum].Cells[1].Value = "Session Sent: " + panel.SessionSent + Environment.NewLine + " Session Success: " + panel.SessionSuccess + Environment.NewLine + " Session Failed: " + panel.SessionFailed;
                                dgv_DetailedReport.Rows[RowNum].Cells[2].Value = "Arm Sent: " + panel.ArmSent + Environment.NewLine + " Arm Success: " + panel.ArmSuccess + Environment.NewLine + " Arm Fail : " + panel.ArmFailed;
                                dgv_DetailedReport.Rows[RowNum].Cells[3].Value = "DisArm Send: " + panel.DisArmSent + Environment.NewLine + " DisArm Success: " + panel.DisArmSuccess + Environment.NewLine + " DisArm Fail: " + panel.DisArmFailed;
                            }
                            RowNum++;
                        };
                        this.BeginInvoke(td);

                    }

                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "ShabakahSimulatorMainForm - UpdateDetailReportTable - Exception - " + ex.Message);
            }
        }

        /// <summary>
        /// Used to fill the table with device ids and default row data for all devices read from DB. --shaiju
        /// </summary>
        private void PopulateDetailedReportsTable()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - PopulateDetailedReportsTable - Start");
                dgv_DetailedReport.Rows.Clear();
                int rowIndex = 0;
                foreach (var ShabPanel in PanelMAC)
                {
                    dgv_DetailedReport.Rows.Add();
                    dgv_DetailedReport.Rows[rowIndex].Height = 70;
                    dgv_DetailedReport.Rows[rowIndex++].Cells[0].Value = ShabPanel.MAC;
                }
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - PopulateDetailedReportsTable - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "ShabakahSimulatorMainForm - PopulateDetailedReportsTable - Exception - " + ex.Message);
            }

        }

        private bool ConfigureDevicesCSV()
        {
            bool blnRes = false;
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - ConfigureDevicesCSV - Start");
                PanelMAC = new List<PanelServices>();

                using (var streamReader = new StreamReader(ConfigFileName))
                {
                    int lineNumber = 0;
                    string lineRead = string.Empty;

                    while (!string.IsNullOrEmpty(lineRead = streamReader.ReadLine()))
                    {
                        if (lineNumber == 0)
                        {
                            lineNumber++;
                            continue;
                        }

                        string[] data = lineRead.Split(',');
                        string MAC = data[0];
                        
                        PanelServices PanelServ = new PanelServices(MAC,m_logger);

                        PanelMAC.Add(PanelServ);
                        lineNumber++;

                        if (lineNumber > NumberofDevicesToBeSimulated)
                        {
                            blnRes = true;
                            break;
                        }

                    }

                    //MockRedir.ConfigPanelMAC(PanelMAC);
                }
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - ConfigureDevicesCSV - End");
            }
            catch (Exception ex)
            {
                blnRes = false;
                m_logger.LogMessage(Level.CRITICAL, "ShabakahSimulatorMainForm - ConfigureDevicesCSV - Exception - " + ex.Message);
            }
            return blnRes;
        }

        private void sTARTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startRedir();
        }

        private void startRedir()
        {
            m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - startRedir - Start");
            MockRedir.StartServer();
            btnStopRedir.Enabled = true;

            btnStartReq.Enabled = true;
            btnStopReq.Enabled = true;

            txtArmDisArm.Enabled = true;
            txtPanelsPerMin.Enabled = true;

            btnStartRedir.Enabled = false;
            m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - startRedir - End");
        }

        private void stopRedir()
        {
            try
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - stopRedir - Start");
                btnStopRedir.Enabled = false;
                
                StopRequest();
                MockRedir.StopServer();
                MockRedir = null;
                btnStartRedir.Enabled = true;
                
                btnStartReq.Enabled = false;
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - stopRedir - End");
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - stopRedir - Exception - " + ex.Message);
            }
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopRedir();
        }

        private void btnStopRedir_Click(object sender, EventArgs e)
        {
            stopRedir();
        }

        private void btnStartRedir_Click(object sender, EventArgs e)
        {
            //Initialize MockRedir
            MockRedir = new ShabakahRedir.MockRedir(m_logger);
            MockRedir.ConfigPanelMAC(PanelMAC);
            //InitializeRedirCounters();
            startRedir();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CloseSimulator();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblRedirSent_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnStartReq_Click(object sender, EventArgs e)
        {
            Int32.TryParse(txtPanelsPerMin.Text, out PanelsPerMin);
            Int32.TryParse(txtArmDisArm.Text, out Arm_DisArm_Min);

            if (PanelsPerMin > NumberofDevicesToBeSimulated)
            {
                MessageBox.Show("Entered Panel is more than configured Panel.");
            }
            else
            {
                btnStartReq.Enabled = false;
                btnStopReq.Enabled = true;

                MockRedir.InitializeCounter();
                
                StartArmDisArmRequest();
            }
        }

        private void StartArmDisArmRequest()
        {
            try
            {
                //ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(() => MockRedir.SendRequest(PanelsPerMin,Arm_DisArm_Min)));
                thArmDisArm = new Thread(() => MockRedir.SendRequest(PanelsPerMin, Arm_DisArm_Min));
                thArmDisArm.Start();
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.TRACE, "ShabakahSimulatorMainForm - StartArmDisArmRequest - Exception - " + ex.Message);
            }
        }

        private void btnStopReq_Click(object sender, EventArgs e)
        {
            StopRequest();
        }

        private void StopRequest()
        {
            try
            {
                btnStopReq.Enabled = false;
                MockRedir.StopRequest();

                if (thArmDisArm != null)
                {
                    thArmDisArm.Abort();
                }
                btnStartReq.Enabled = true;
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "ShabakahSimulatorMainForm - StopRequest - Exception - " + ex.Message);
            }
        }

        private void txtNoOfIDoXs_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = (int)e.KeyChar;

            if (!IsNumberInRange(KeyCode, 48, 57) && KeyCode != 8)
            {
                e.Handled = true;
                Console.Beep();
            }
        }
        private bool IsNumberInRange(int Val, int Min, int Max)
        {
            try
            {
                return (Val >= Min && Val <= Max);
            }
            catch (Exception ex)
            {
                //  m_Logger.LogMessage(Level.CRITICAL, "frmReadersConfiguration:IsNumberInRange:Exception:" + ex.Message);
                return false;
            }
        }

        private void CloseSimulator()
        {
            try
            {
                stopRedir();
                Application.Exit();
            }
            catch (Exception ex)
            {
                m_logger.LogMessage(Level.CRITICAL, "ShabakahSimulatorMainForm - CloseSimulator");
            }
        }


        private void ShabakahSimulatorMainForm_Load(object sender, EventArgs e)
        {
         /*   TabControl.Enabled = false;
            
            //In DetailedReport tab
            dgv_DetailedReport.Enabled = false;

            //In Config tab
            btnStartRedir.Enabled = false;
            btnStopRedir.Enabled = false;

            btnStartReq.Enabled = false;
            btnStopReq.Enabled = false;

            txtPanelsPerMin.Enabled = false;
            txtArmDisArm.Enabled = false;

            grpBoxReport.Enabled = false;*/
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseSimulator();
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void grpBoxConfig_Enter(object sender, EventArgs e)
        {

        }

        private void lblRedirStatus_Click(object sender, EventArgs e)
        {

        }

        private void grpRedirReport_Enter(object sender, EventArgs e)
        {

        }
    }
}
