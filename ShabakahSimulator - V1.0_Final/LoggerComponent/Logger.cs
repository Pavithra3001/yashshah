/***************************************************************************************
								COPYRIGHT (c) 2007
									HONEYWELL INC.,
 								ALL RIGHTS RESERVED
 
This software is a copyrighted work and/or information protected as a trade secret. 
Legal rights of Honeywell Inc. in this software is distinct from ownership of any medium 
in which the software is embodied. Copyright or trade secret notices included must be 
reproduced in any copies authorized by Honeywell Inc. The information in this software 
is subject to change without notice and should not be considered as a commitment by 
Honeywell Inc.

File Name					: Logger.cs
Project Title				: FIS Simulator
Author(s)					: Karthika Gopal

*****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Honeywell.SimulationFramework.LoggerComponent
{
    public enum Level
    {
        TRACE = 5,
        LOW = 4,
        MEDIUM = 3,
        HIGH = 2,
        CRITICAL = 1
    };

    public class Logger
    {

        public string m_fileName;
        public Level m_currentLogLevel;
        public float m_rollOverSizeInMegaBytes;
        public int m_rollOverNumFiles;
        public StreamWriter m_writer;
        public object m_useFile;
        public string FileNameWithoutExtension = "abc";

        public static int m_CurrentFileIndex = 1;

        public Logger(string fileName)
        {
            Debug.Assert(fileName != null, "Filename to Logger is null");
            m_fileName = fileName;
            m_currentLogLevel = Level.TRACE;
            OpenLogFileForWriting();
        }

        public Logger(string fileName, bool appendTimeStampToFileName)
        {
            Debug.Assert(fileName != null, "Filename to Logger is null");

            if (appendTimeStampToFileName)
                fileName = AppendTimeStampToFileName(fileName);

            m_fileName = fileName;
            m_currentLogLevel = Level.TRACE;

            OpenLogFileForWriting();
        }

        public Logger(string fileName, bool appendTimeStampToFileName, Level logLevel)
        {
            Debug.Assert(fileName != null, "Filename to Logger is null");

            if (!(Directory.Exists("Logs")))
                Directory.CreateDirectory("Logs");
            m_fileName = FileNameWithoutExtension + m_CurrentFileIndex + ".txt";
            m_fileName = "Logs//" + m_fileName;
            m_currentLogLevel = logLevel;

            OpenLogFileForWriting();
        }

        public string Filename
        {
            get
            {
                lock (m_useFile)
                {
                    return m_fileName;
                }
            }
        }
        public Level LogLevel
        {
            get
            {
                return m_currentLogLevel;
            }
            set
            {
                m_currentLogLevel = value;
            }
        }
        public float RollOverFileSizeInMB
        {
            get
            {
                return m_rollOverSizeInMegaBytes;
            }
            set
            {
                m_rollOverSizeInMegaBytes = value;
            }
        }
        public int RollOverNumFiles
        {
            get
            {
                return m_rollOverNumFiles;
            }
            set
            {
                m_rollOverNumFiles = value;
            }
        }

        public void LogMessage(Level messageLevel, string messageToLog)
        {
            try
            {
                if (messageLevel <= m_currentLogLevel)
                {
                    Debug.Assert(m_writer != null, "Log file is not opened for writing.");
                    lock (m_useFile)
                    {
                        CheckIfFileSizeReachedRollOverSize();
                        m_writer.WriteLine(messageLevel.ToString() + " : " + GetTimeStamp() + " : " + messageToLog);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Closes the stream
        /// </summary>
        public void Close()
        {
            m_writer.Close();
        }

        /// <summary>
        /// Flushes the stream
        /// </summary>
        public void Flush()
        {
            m_writer.Flush();
        }

        public void OpenLogFileForWriting()
        {
            m_writer = new StreamWriter(m_fileName);
            m_writer.AutoFlush = true;
            Debug.Assert(m_writer != null, "Could not open file " + m_fileName);
            m_useFile = new object();
        }

        /// <summary>
        /// Get time stamp in DD_MM_YY_HH_MM_SS format
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            DateTime now = DateTime.Now;
            string timeStamp = now.Date.Day.ToString()
                + "_"
                + now.Month.ToString()
                + "_"
                + now.Year.ToString()
                + "_"
                + now.Hour.ToString()
                + "_"
                + now.Minute.ToString()
                + "_"
                + now.Second.ToString();
            return timeStamp;
        }


        public void RollOverFile(object args)
        {
            lock (m_useFile)
            {
                m_CurrentFileIndex++;
                if (m_CurrentFileIndex < m_rollOverNumFiles)
                {
                    m_CurrentFileIndex = 1;
                }

                m_writer.Flush();
                m_writer.Close();
                //File.Delete(m_fileName); 

                m_fileName = "Logs//" + FileNameWithoutExtension + m_CurrentFileIndex + ".txt"; ;
                m_writer = new StreamWriter(m_fileName, false);
                m_writer.AutoFlush = true;
                Debug.Assert(m_writer != null, "Logger handle is null");

                m_writer.WriteLine("CRITICAL: Rolled over file");
            }
        }


        public void CheckIfFileSizeReachedRollOverSize()
        {
            long currentSize = m_writer.BaseStream.Length;

            // Divide length by 1000000 so that we get an approximate length in MB
            currentSize = currentSize / 1000000;

            if (currentSize > m_rollOverSizeInMegaBytes)
            {
                RollOverFile(null);
            }
        }


        /// <summary>
        /// Appends the current time stamp to the file name and returns
        /// the modified file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string AppendTimeStampToFileName(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileNameWithTimeStamp = fileNameWithoutExtension + "_" + GetTimeStamp();
            string fileNameWithTimeStampAndExtension = fileNameWithTimeStamp + extension;
            return fileNameWithTimeStampAndExtension;
        }

        public string RemoveTimeStamp(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            int index = fileName.IndexOf('_');

            if (index < 0)
            {
                return fileName;
            }

            string newFileName = fileName.Substring(0, index) + extension;

            return newFileName;

        }


        public void DeleteOldLogs(int daysLimit)
        {
            DateTime now = DateTime.Today;

            try
            {
                string[] logFiles = Directory.GetFiles(Environment.CurrentDirectory + "\\Logs", "abc*.txt",
                        SearchOption.TopDirectoryOnly);

                foreach (string logFile in logFiles)
                {
                    DateTime fileCreationTime = File.GetCreationTime(logFile);

                    TimeSpan difference = now.Subtract(fileCreationTime);

                    if ((difference.TotalDays >= daysLimit || daysLimit == -1) && Environment.CurrentDirectory + "\\Logs" + "\\" + m_fileName != logFile)
                    {
                        LogMessage(Level.TRACE, "Deleting file " + logFile);
                        //  File.Delete(logFile);
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage(Level.HIGH, "Exception in deleting log file: " + ex.Message);
            }
        }
    }
}
