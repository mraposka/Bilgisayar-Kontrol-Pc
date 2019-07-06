using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Management;
using System.Collections;
using System.Runtime.InteropServices;
using System.Timers;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Update
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        static string username = Environment.UserName;
        static string winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
        static string prgName = Application.ProductName;
        static string errorLine;
        static string IP;
        static string pcadi;
        static string work;
        static string MAC;
        static char winDirShortVersion=winDir.FirstOrDefault();
        static string updated_file;
        static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static string updateFileName = "n0kayip Güncelleme Klasörü(Geçicidir)";
        private static Process[] pname;
        private static string appName = Application.ExecutablePath;
        static void Main(string[] args)
        {
            char t = '\\';
            string[] words = appName.Split(t);
            pname = Process.GetProcessesByName(words.Last());
            if (pname.Length == 0)
            {
                //Yapılacaklar
                try
                {
                    var handle = GetConsoleWindow();
                    //Gizle
                    ShowWindow(handle, SW_HIDE);

                    // Göster
                    //ShowWindow(handle, SW_SHOW);
                    IP = IPAd();
                    winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
                    winDirShortVersion = winDir.FirstOrDefault();
                    string[] file_name_array = { "BilgisayarAcKapaUygulamasi.exe", "MyIcon.ico", "n0kayip Kontrol.exe",
                    "GPS.exe","Newtonsoft.Json.dll", "Newtonsoft.Json.xml", "EkranGoruntusu.exe", "ResimleriTemizle.exe"};
                    for (int i = 0; i <= file_name_array.Length - 1; i++)
                    {
                        UpdateDosya(file_name_array[i], winDirShortVersion);
                    }
                    if(File.Exists(desktopPath + @"/" + updateFileName))
                    { Directory.Delete(desktopPath + @"/" + updateFileName); }
                    // Console.WriteLine("Done!!!");
                }
                catch (Exception Exc)
                {
                    errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                        + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                        + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                    string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
                    //Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                    System.IO.File.AppendAllText(file_path, errorLine);
                }
            }
            else
            {
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
                     + "PC: " + pcadi + Environment.NewLine + "Hata: " /* + Exc.Message +*/+ Environment.NewLine + "Program: " + prgName + Environment.NewLine
                     + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
            }
            return String.Empty;
        }
        private static void UpdateDosya(string file_name_i, char winDirShortVersion)
        {
            try
            {
                DateTime lastModified = System.IO.File.GetLastWriteTime
                  (@winDirShortVersion + @":\n0kayip\" + file_name_i);

                updated_file = DownloadFileFTP(file_name_i);

                DateTime lastModified2 = System.IO.File.GetLastWriteTime
                    (@winDirShortVersion + @":\Users\" + username + @"\AppData\Local\Temp\" + updated_file);

                if (File.Exists(@winDirShortVersion + @":\Users\" + username + @"\AppData\Local\Temp\" + updated_file)) { 

                if (lastModified2 > lastModified)
                {
                    //Startup Eskisini Silme İşlemi
                    File.Delete(@winDirShortVersion + @":\n0kayip\" + file_name_i);
                    //Startup Eskisini Silme İşlemi

                    //Kopyalama İşlemi
                    File.Copy(@winDirShortVersion + @":\Users\" + username + @"\AppData\Local\Temp\" + updated_file
                        , @winDirShortVersion + @":\n0kayip\" + file_name_i);
                    //Kopyalama İşlemi

                    //Temp Silme İşlemi
                    File.Delete(@winDirShortVersion + @":\Users\" + username + @"\AppData\Local\Temp\" + file_name_i);
                    //Temp Silme İşlemi
                    // Console.WriteLine("Updated!!!");
                }
                }
                else
                {
                    Directory.CreateDirectory(desktopPath + @"/" + updateFileName);
                    //Startup Eskisini Silme İşlemi
                    File.Delete(@winDirShortVersion + @":\n0kayip\" + file_name_i);
                    //Startup Eskisini Silme İşlemi

                    //Kopyalama İşlemi
                    File.Copy(desktopPath+@"/"+updateFileName+@"/"+updated_file, @winDirShortVersion + @":\n0kayip\" + file_name_i);
                    //Kopyalama İşlemi

                    //Temp Silme İşlemi
                    File.Delete(desktopPath + @"/" + updateFileName + @"/" + file_name_i);
                    //Temp Silme İşlemi
                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message+ Environment.NewLine + "Program: " + prgName + Environment.NewLine
                   + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
              //  Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
            }


        }

        public static string DownloadFileFTP(string updated_file_name)
        {
           try
            {
                string inputfilepath = @winDirShortVersion + @":\Users\" + username + @"\AppData\Local\Temp\" + updated_file_name;
                string ftphost = "ftp.enesvekadir.com/";
                string ftpfilepath = "/public_html/updates/" + updated_file_name;

                string ftpfullpath = "ftp://" + ftphost + ftpfilepath;

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("enesvekadir", "enes++52");
                    byte[] fileData = request.DownloadData(ftpfullpath);

                    using (FileStream file = File.Create(inputfilepath))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                    // Console.WriteLine("Download Complete");
                   
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
            return updated_file_name;
        }


    }
}
