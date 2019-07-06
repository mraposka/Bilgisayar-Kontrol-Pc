using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Management;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Timers;
using System.Drawing;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace BilgisayarAcKapaUygulamasi
{
    class Program
    {
        static string prgName = Application.ProductName;
        static string errorLine;
        static string IP;
        public static NotifyIcon tray = new NotifyIcon();
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        private static System.Threading.Timer sayac2;
        public static System.Threading.Timer trayIconSayac;
        private static bool d = false;
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        internal const UInt32 SC_CLOSE = 0xF060;
        internal const UInt32 MF_GRAYED = 0x00000001;
        [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
        
        static public class TopMostMessageBox
        {
            static public DialogResult Show(string message)
            {
                return Show(message, string.Empty, MessageBoxButtons.OK);
            }

            static public DialogResult Show(string message, string title)
            {
                return Show(message, title, MessageBoxButtons.OK);
            }

            static public DialogResult Show(string message, string title,
                MessageBoxButtons buttons)
            {
                Form topmostForm = new Form();
                topmostForm.Size = new System.Drawing.Size(1, 1);
                topmostForm.StartPosition = FormStartPosition.Manual;
                System.Drawing.Rectangle rect = SystemInformation.VirtualScreen;
                topmostForm.Location = new System.Drawing.Point(rect.Bottom + 10,
                rect.Right + 10);
                topmostForm.Show();
                topmostForm.Focus();
                topmostForm.BringToFront();
                topmostForm.TopMost = true;
                DialogResult result = MessageBox.Show(topmostForm, message, title,buttons);
                topmostForm.Dispose();
                return result;
            }
        }
        class Uygulama
        {
            [JsonProperty(PropertyName = "pc_id")]
            public string pc_id { get; set; }
            [JsonProperty(PropertyName = "pc_adi")]
            public string pc_adi { get; set; }
            [JsonProperty(PropertyName = "durum")]
            public string durum { get; set; }
            [JsonProperty(PropertyName = "mac_id")]
            public string mac_id { get; set; }
        }
        static bool cevapVerildi = false;
        static string MAC;
        static string pcadi;
        static string site = "littlep";
        static string work;
        static string winDir;
        static char winDirShortVersion;
        private static Process[] pname;
        private static string appName = Application.ExecutablePath;
        public static void Main(string[] args)
        {
            char t = '\\';
            string[] words = appName.Split(t);
            pname = Process.GetProcessesByName(words.Last());
            if (pname.Length == 0)
            {
                //Yapılacaklar
                try
                {
                    winDir = Convert.ToString(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)));
                    winDirShortVersion = winDir.FirstOrDefault();

                    SystemEvents.SessionEnding += SystemEvents_SessionEnding;

                    var handle = GetConsoleWindow();
                    //Gizle
                    //ShowWindow(handle, SW_HIDE);

                    // Göster
                    //ShowWindow(handle, SW_SHOW);
                    IntPtr current = Process.GetCurrentProcess().MainWindowHandle;
                    EnableMenuItem(GetSystemMenu(current, false), SC_CLOSE, MF_GRAYED);
                    MAC = Mac();
                    if (String.IsNullOrEmpty(MAC))
                    {
                        Console.WriteLine("Biglisayarınızda bir ağ bağdaştırıcısı bulunamadı.");
                    }
                    else
                    {
                        IP = IPAd();
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
                        Console.Write(pcadi);
                        ManagementObjectSearcher mos =
                        new ManagementObjectSearcher(@"root\CIMV2", @"SELECT * FROM Win32_ComputerSystem");
                        foreach (ManagementObject mo in mos.Get())
                        {
                            Console.Write(" WorkGroup= ");
                            work = mo["Workgroup"].ToString();
                            Console.WriteLine(work);
                        }
                        Console.WriteLine("Mac adresiniz= " + MAC);
                    }
                    KayitKontrolu();
                    PcKaydet();
                    IlkAcilis();
                    Console.ReadLine();
                }
                catch (Exception Exc)
                {
                    errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                       + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                       + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                    string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
                  //  Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                    System.IO.File.AppendAllText(file_path, errorLine);
                    PcKapat();
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
        public static void PcKapat()
        {
            try
            {
                string siteUrl = "http://" + site + ".xyz/pc/pckontrol/" + MAC + "/0";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(siteUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    //  Console.WriteLine("Kapatıldı");
                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }


        private static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            try
            {
                string siteUrl = "http://" + site + ".xyz/pc/pckontrol/" + MAC + "/0";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(siteUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    //  Console.WriteLine("Kapatıldı");
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
                PcKapat();
            }

        }
       
        void MyMethod()
        {
            try
            {
                Console.WriteLine("Tray Icon");
                tray.Icon = new Icon("MyIcon.ico");
                tray.Visible = true;
                tray.BalloonTipText = "Sistem";
                tray.ShowBalloonTip(5000, "Sistem Kapatılacak", "Sistem sorusuna cevap vermezseniz 30 saniye içinde otomatik kapatılacaktır.-" + SystemInformation.ComputerName, ToolTipIcon.Info);

            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }

        }
        private static void PcKaydet()
        {
            try
            {
                Console.WriteLine("PcKaydet");
                string macUrl = "http://" + site + ".xyz/pc/pcdirekkayit/" + MAC + "/" + pcadi + "/" + SystemInformation.UserName + "/" + work;
                var httpWReq = (HttpWebRequest)WebRequest.Create(macUrl);
                httpWReq.ContentType = "application/json";
                httpWReq.Method = "GET";
                var httpResp = (HttpWebResponse)httpWReq.GetResponse();
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }

        }
        private static void KayitKontrolu()
        {
            try
            {
                Console.WriteLine("KayitKontrol");
                string macUrl = "http://" + site + ".xyz/pc/pcoku/" + MAC;
                HttpWebRequest request = WebRequest.Create(macUrl) as HttpWebRequest;
                request.ContentType = "application/json";
                request.Method = "GET";
                string jsonVerisi = "";
                var httpResponse1 = (HttpWebResponse)request.GetResponse();
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader okuyucu = new StreamReader(response.GetResponseStream());
                    jsonVerisi = okuyucu.ReadToEnd();
                    Uygulama app = JsonConvert.DeserializeObject<Uygulama>(jsonVerisi);
                    try
                    {
                        var obje1 = app.mac_id;

                        if (obje1 == MAC)

                            Console.WriteLine("Kayitli");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Kayıtsız");
                        PcKaydet();

                    }

                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }

        public static void GPSAl()
        {
            try
            {
            //    Process.Start(@"GPS.exe");
                
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
            //    Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }

           

        }

        public static void EkranAl()
        {
            try
            {
                Process.Start(@"EkranGoruntusu.exe");
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }


        private static string Mac()
        {
            try
            {
                Console.WriteLine("Mac");
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
                PcKapat();
            }
            return String.Empty;
        }
        public static void IlkAcilis()
        {
            try
            {
                Console.WriteLine("IlkAcılıs");
                string siteUrl = "http://" + site + ".com/pc/pckontrol/" + MAC + "/1";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(siteUrl);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Açıldi");
                    sayac2Baslat();
                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }
        public static void TrayCallBack(Object o)
        {
            try
            {
                Console.WriteLine("Tray=Null");
                tray.Icon = null;
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }

        }
            private static void trayIconSayacM()
        {
            try
            {
                Console.WriteLine("TrayIconSayac");
                trayIconSayac = new System.Threading.Timer(TrayCallBack, null, 0, 10000);
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                   + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                   + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
            //    Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }

        private static void sayac2Baslat()
        {
            try
            {
                Console.WriteLine("Sayac2");
                sayac2 = new System.Threading.Timer(TimerCallback, null, 0, 10000);
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                     + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                     + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
              //  Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }

        }


        public static void KapanmaSorusu() {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "kapat.wav";
            try
            {
                Console.WriteLine("KapanmaSorusu");
                System.Timers.Timer t = new System.Timers.Timer(30000);
                player.Play();
                t.Elapsed += new System.Timers.ElapsedEventHandler(Saniye);
                t.Start();
                Task.Run(() =>
                {
                    Program p = new Program();
                    trayIconSayacM();
                    p.MyMethod();
                    var dialogResult = TopMostMessageBox.Show("Kapatmak istiyor musunuz?", "Sistem", MessageBoxButtons.YesNo);
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        string siteUrl = "http://" + site + ".com/pc/pckontrol/" + MAC + "/0";
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(siteUrl);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "GET";
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine("Kapaniyor");
                        }
                        player.Stop();
                        Process.Start("shutdown", "-s -f -t 00");
                    }
                    else
                    {
                        Console.WriteLine("Else");
                        player.Stop();
                        t.Stop();
                        d = false;
                        EkranAl();
                        GPSAl();
                        IlkAcilis();
                    }
                });
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                     + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                     + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }

        private static void Saniye(object sender, ElapsedEventArgs e)
        {
            try
            {
                Console.WriteLine("Saniye");
                Process.Start("shutdown", "-s -f -t 00");
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                     + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                     + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }
        }
        public static void TimerCallback(Object o)
        {
            try
            {
                Console.WriteLine("TimerCallback");
                if (d == false)
                {
                    string url = "http://" + site + ".com/pc/pcoku/" + MAC;
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request.ContentType = "application/json";
                    request.Method = "GET";
                    string jsonVerisi = "";
                    var httpResponse1 = (HttpWebResponse)request.GetResponse();
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        StreamReader okuyucu = new StreamReader(response.GetResponseStream());
                        jsonVerisi = okuyucu.ReadToEnd();
                        Uygulama app = JsonConvert.DeserializeObject<Uygulama>(jsonVerisi);
                        var obje = app.durum;
                        Console.WriteLine(obje);
                        if (obje == "0" && d == false)
                        {
                            d = true;
                            KapanmaSorusu();
                        }
                    }
                    GC.Collect();
                }
                else
                {
                }
            }
            catch (Exception Exc)
            {
                errorLine = "IP: " + IP + Environment.NewLine + "MAC: " + MAC + Environment.NewLine + "WORKGROUP: " + work + Environment.NewLine
                    + "PC: " + pcadi + Environment.NewLine + "Hata: " + Exc.Message + Environment.NewLine + "Program: " + prgName + Environment.NewLine
                    + "Tarih: " + DateTime.Now.ToString() + Environment.NewLine + "------------------------------------" + Environment.NewLine;
                string file_path = @winDirShortVersion + @":\n0kayip\Logs\error_log.txt";
             //   Process.Start(@winDirShortVersion + @":\n0kayip\GPS.exe");
                System.IO.File.AppendAllText(file_path, errorLine);
                PcKapat();
            }

        }
    }
}

