using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Management;

namespace ResimleriTemizle
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        static Process[] pname;
        static string prgName = Application.ProductName;
        static string errorLine;
        static string IP;
        static string[] parcalar;
        static string pcadi;
        static string MAC;
        static string work;
        static string winDir;
        static char winDirShortVersion;
        static void Main(string[] args)
        {
            winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
            winDirShortVersion = winDir.FirstOrDefault();
            try
            {
                var handle = GetConsoleWindow();
                //Gizle
                ShowWindow(handle, SW_HIDE);

                // Göster
                //ShowWindow(handle, SW_SHOW);
                pcadi = SystemInformation.ComputerName;
                pcadi = pcadi.Replace("Ç", "C");
                pcadi = pcadi.Replace("Ü", "U");
                pcadi = pcadi.Replace("İ", "I");
                pcadi = pcadi.Replace("Ö", "O");
                pcadi = pcadi.Replace("Ş", "S");
                pcadi = pcadi.Replace(" ", "_");
                pcadi = pcadi.Replace("ç", "c");
                pcadi = pcadi.Replace("ü", "u");
                pcadi = pcadi.Replace("ı", "i");
                pcadi = pcadi.Replace("ö", "o");
                pcadi = pcadi.Replace("ş", "s");
                MAC = Mac();
                IP = IPAd();
                ManagementObjectSearcher mos =
                   new ManagementObjectSearcher(@"root\CIMV2", @"SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject mo in mos.Get())
                {
                    //Console.Write(" WorkGroup= ");
                    work = mo["Workgroup"].ToString();
                }
                string winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
                char winDirShort = winDir.FirstOrDefault();
                string path = @winDirShort + @":\Users\" + Environment.UserName + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] rgFiles = di.GetFiles();
                foreach (FileInfo fi in rgFiles)
                {
                    parcalar = fi.Name.Split('.');
                    if (parcalar.Last().ToString() == "jpg")
                    {
                        path = path + "/" + fi.Name;
                        path = path.Replace(@"\", @"/");
                        File.Delete(path);
                    }
                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                     + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                     + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
            //    Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                Application.Exit();
            }

        }
        public static string IPAd()
        {
            string externalIP = "";
            externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
            externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();
            return externalIP;
        }
        private static string Mac()
        {
            try
            {
                //Console.WriteLine("Mac");
                ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject obj in manager.GetInstances())
                {
                    if ((bool)obj["IPEnabled"])
                    {
                        return obj["MacAddress"].ToString();
                    }
                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
           //     Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                Application.Exit();
            }
            return String.Empty;
        }
    }
}
