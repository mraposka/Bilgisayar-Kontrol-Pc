using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace SistemiBaslat
{

    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        static string prgName = Application.ProductName;
        static string errorLine;
        static string IP;
        static string pcadi;
        static string work;
        static string MAC;
        static bool baslatildi = false;
        static string ftpUser = "enesvekadir";
        static string ftpPass = "enes++52";
        static string ftpDizin = "ftp://ftp.enesvekadir.com/public_html/Logs/";
        static string errortxt = "/error_log.txt";
        static string gpstxt = "/gps.txt";
        static string thisPcUserName = Environment.UserName;
        static string winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
        static char winDirShortVersion = winDir.FirstOrDefault();

        private static Process[] pname;
        private static string appName = Application.ExecutablePath;
        static void Main(string[] args)
        {
            MAC = Mac();
            IP = IPAd();
            string uploadLogPath = thisPcUserName + "-" + MAC + "-" + IP;
            CreateFTPDirectory(ftpDizin + thisPcUserName + "-" + MAC + "-" + IP);
            char t = '\\';
            string[] words = appName.Split(t);
            pname = Process.GetProcessesByName(words.Last());
            if (pname.Length == 0)
            {
                //Yapılacaklar
              //  try
               // {
                    //Error_log.txt
                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(ftpUser, ftpPass);
                        client.UploadFile(ftpDizin+ uploadLogPath + errortxt, "STOR", @winDirShortVersion + @":\n0kayip\Logs\"+ errortxt);
                    }
                    //Error_log.txt
                  //  MessageBox.Show("error_log.txt yüklendi.");
                    /*gps.txt
                     İstisna...
                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(ftpUser, ftpPass);
                        client.UploadFile(ftpDizin + uploadLogPath + gpstxt, "STOR", @winDirShortVersion + @":\n0kayip\Logs\" + gpstxt);
                    }
                    */
                   // MessageBox.Show("gps.txt yüklendi");
                    //gps.txt
                    System.Threading.Thread.Sleep(5000);

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
                    ManagementObjectSearcher mos =
                       new ManagementObjectSearcher(@"root\CIMV2", @"SELECT * FROM Win32_ComputerSystem");
                    foreach (ManagementObject mo in mos.Get())
                    {
                        //Console.Write(" WorkGroup= ");
                        work = mo["Workgroup"].ToString();
                    }
                    Process.Start("Guncelleme.exe");
                    Process.Start("ResimleriTemizle.exe");
                    while (baslatildi == false)
                    {
                        pname = Process.GetProcessesByName("Guncelleme");
                        if (pname.Length == 0)
                        {
                            Process.Start("n0kayip Kontrol.exe");
                            baslatildi = true;
                        }
                        else
                        { }
                    }
              /*  }
                catch (Exception Exc)
                {
                    errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                                        + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                                        + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                    string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
                 //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                    System.IO.File.AppendAllText(file_path, errorLine);
                }*/

            }
            else
            {
                Application.Exit();
            }
            
        }
        public static bool CreateFTPDirectory(string directory)
        {

            try
            {
                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(directory));
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(ftpUser, ftpPass);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    response.Close();
                    return false;
                }
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
            //    Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
            }
               
            return String.Empty;
        }
    }
}
