using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Management;
using IWshRuntimeLibrary;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Install
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        static string strIP = String.Empty;
        static string prgName = Application.ProductName;
        static string MAC;
        static string IP;
        static string pcadi;
        static string work;
        static string username = Environment.UserName;
        static string winDir;
        static string errorLine;
        public static char winDirShortVersion;
        private static Process[] pname;
        private static string appName = Application.ExecutablePath;
        static string AppStartLocation = @"C:\n0kayip\SistemiBaslat.exe";
        static void Main(string[] args)
        {
            char t = '\\';
            string[] words = appName.Split(t);
            pname = Process.GetProcessesByName(words.Last());
            if (pname.Length == 0)
            {
                    Baslat();
            /*    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("SistemiBaslat", "\"" + AppStartLocation+ "\"");
                Registry Key ile başlangıç sağlanacak
                 */
                MessageBox.Show("Kurulum başarıyla tamamlandı.");
            }
            else
            {
                Application.Exit();
            }
           
        }

        private static void Baslat()
        {
            winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
            winDirShortVersion = winDir.FirstOrDefault();
             try
              {
            var handle = GetConsoleWindow();
            //Gizle
            ShowWindow(handle, SW_HIDE);
                // Göster
             //   ShowWindow(handle, SW_SHOW);
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
                    work = mo["Workgroup"].ToString();
                }
                if (Directory.Exists(@winDirShortVersion + @":\n0kayip")) { }
                else {
                    Directory.CreateDirectory(@winDirShortVersion + @":\n0kayip");
                    Directory.CreateDirectory(@winDirShortVersion + @":\n0kayip\Logs");
                    System.IO.File.Create(@winDirShortVersion + @":\n0kayip\Logs\error_log.txt");
                    System.IO.File.Create(@winDirShortVersion + @":\n0kayip\Logs\gps.txt");
                }
                
                string[] file_name_array =
                    { "BilgisayarAcKapaUygulamasi.exe", "MyIcon.ico", "Newtonsoft.Json.dll",
                "Newtonsoft.Json.xml", "EkranGoruntusu.exe", "SistemiBaslat.exe","Guncelleme.exe","ResimleriTemizle.exe","GPS.exe","n0kayip Kontrol.exe","kapat.wav" };
                for (int i = 0; i <= file_name_array.Length - 1; i++)
                {
                    DosyalariYukle(file_name_array[i], winDirShortVersion);
                    
                }
           }
            catch (Exception Exc)
           {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
              //  Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);

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
              //  Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
            }
            return String.Empty;
        }
        
        public static void DosyalariYukle(string file_name_2,char theStartup)
        {
            
            string file_name = @"\"+file_name_2;
            string source_path = @"KurulumKlasör";
            string target_path = @winDirShortVersion + @":\n0kayip\";
                System.IO.File.Copy(source_path + file_name, target_path + file_name);
        }


    }
}
