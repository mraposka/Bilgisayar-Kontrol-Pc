using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net;
using System.Management;
using System.Runtime.InteropServices;

namespace n0kayip_Kontrol
{
    class Program
    {
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static int SW_SHOW = 5;
        static int SW_HIDE = 0;
        private static bool baslatildi=false;
        private static string MAC,site;
        private static Process[] pname;
        private static string appName = Application.ExecutablePath;
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            //Gizle
            ShowWindow(handle, SW_HIDE);

            // Göster
            //ShowWindow(handle, SW_SHOW);
            char t = '\\';
            string[] words = appName.Split(t);
            pname = Process.GetProcessesByName(words.Last());
            if (pname.Length == 0)
            {
                //Yapılacaklar
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
                MAC = Mac();
                site = "enesvekadir";
                Process.Start("BilgisayarAcKapaUygulamasi.exe");
                while (baslatildi == false)
                {
                    pname = Process.GetProcessesByName("BilgisayarAcKapaUygulamasi");
                    if (pname.Length == 0)
                    {
                        //0
                        string siteUrl = "http://" + site + ".com/pc/pckontrol/" + MAC + "/0";
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(siteUrl);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "GET";
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine("Kapatıldı");
                        }
                        //0

                    }
                    else
                    {
                        //1
                    }
                }
            }
            else
            {
                Application.Exit();
            }
            
        }
        public static string Mac()
        {
            ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj in manager.GetInstances())
            {
                if ((bool)obj["IPEnabled"])
                {
                    return obj["MacAddress"].ToString();
                }
            }
            return String.Empty;
        }
    }
}
