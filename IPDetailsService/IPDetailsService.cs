using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IPDetailsService
{
    public partial class IPDetailsService : ServiceBase
    {
        Timer timer = new Timer();
        IPAddressesFileProvider fileProvider = new IPAddressesFileProvider();
      
        public IPDetailsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //some logs to track service
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = AppSettingsManager.Interval * 60000;   
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            //some logs to track service
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //create folder CSVs
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\CSVs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fileProvider.CreateCSVFile(path);
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {

                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
