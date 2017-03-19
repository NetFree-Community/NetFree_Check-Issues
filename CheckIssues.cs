using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetFree_Check_Issues
{
    public partial class CheckIssues : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }

        public CheckIssues()
        {
            InitializeComponent();
            loader.Size = layout.Size;
            loader.Location = layout.Location;
            CheckStores(MyIspIssuerName());
            refresh();
        }

        private void CheckIssues_Load(object sender, EventArgs e)
        {

        }

        public DateTime InternetTime()
        {
            DateTime datetime = DateTime.MinValue;
            for (int tries = 1; tries < 3; tries++)
            {
                try
                {
                    const string ntpServer = "time.windows.com";

                    // NTP message size - 16 bytes of the digest (RFC 2030)
                    var ntpData = new byte[48];

                    //Setting the Leap Indicator, Version Number and Mode values
                    ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                    var addresses = Dns.GetHostEntry(ntpServer).AddressList;

                    //The UDP port number assigned to NTP is 123
                    var ipEndPoint = new IPEndPoint(addresses[0], 123);
                    //NTP uses UDP
                    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    socket.Connect(ipEndPoint);

                    //Stops code hang if NTP is blocked
                    socket.ReceiveTimeout = 3000;

                    socket.Send(ntpData);
                    socket.Receive(ntpData);
                    socket.Close();

                    //Offset to get to the "Transmit Timestamp" field (time at which the reply 
                    //departed the server for the client, in 64-bit timestamp format."
                    const byte serverReplyTime = 40;

                    //Get the seconds part
                    ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

                    //Get the seconds fraction
                    ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

                    //Convert From big-endian to little-endian
                    intPart = SwapEndianness(intPart);
                    fractPart = SwapEndianness(fractPart);

                    var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

                    //**UTC** time
                    var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
                    datetime = networkDateTime;
                    return datetime;
                }
                catch { };
            }
            return datetime;
        }

        private bool TimeRight()
        {
            return InternetTime().ToLocalTime().Date == DateTime.Now.Date;
        }

        private int CheckCert(string ispIssuerName)
        {
            if (ispIssuerName == "")
                return -1;
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var Cert = store.Certificates.Find(
                X509FindType.FindByIssuerName,
                "NetFree Sign ," + ispIssuerName,
                false);

            if (Cert != null && Cert.Count > 0)
            {
                    try
                    {
                        using (var client = new WebClient())
                        {
                            using (var stream = client.OpenRead("https://mail.google.com"))
                            {
                                return 2;
                            }
                        }
                    }
                    catch
                    {
                        return 1;
                    }
                }
            return 0;
        }

        private void CheckStores(string ispIssuerName)
        {
            Boolean HasOther = false;
            List<StoreLocation> StoreLocations = new List<StoreLocation>
            {
                StoreLocation.CurrentUser,
                StoreLocation.LocalMachine
            };

            List<StoreName> StoreNames = new List<StoreName>
            {
                StoreName.AddressBook,
                StoreName.AuthRoot,
                StoreName.CertificateAuthority,
                StoreName.Disallowed,
                StoreName.My,
                StoreName.Root,
                StoreName.TrustedPeople,
                StoreName.TrustedPublisher
            };
            X509Store store;

            foreach (StoreLocation storelocation in StoreLocations)
            {
                foreach (StoreName storename in StoreNames)
                {
                    if (storelocation != StoreLocation.LocalMachine || storename != StoreName.Root)
                    {
                        store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                        try
                        {
                            store.Open(OpenFlags.ReadWrite);
                        }
                        catch
                        {
                            DialogResult Response =  MessageBox.Show("תוכנה זו דורש הרשאות מנהל", "שגיאה", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (Response == System.Windows.Forms.DialogResult.Retry)
                                CheckStores(ispIssuerName);
                            else
                                Environment.Exit(1);
                        }

                        foreach (X509Certificate2 Cert in store.Certificates)
                        {
                            if (Cert.Issuer.Contains("NetFree Sign ," + ispIssuerName))
                            {
                                HasOther = true;
                                store.Remove(Cert);
                            }
                        }
                        store.Close();
                    }
                }
            }
            if (HasOther && CheckCert(ispIssuerName) == 0)
            {
                CertInstall();
            }
        }

        private void CertInstall()
        {
            try
            {
                WebClient client = new WebClient();
                byte[] CertFile = client.DownloadData("http://netfree.link/netfree-ca.crt");
                X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                X509Certificate2 cert = new X509Certificate2();
                cert.Import(CertFile);
                store.Open(OpenFlags.ReadWrite);
                store.Add(cert);
                store.Close();
            }
            catch { };
        }

        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }

        private string MyIspIssuerName()
        {
            string ISP = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                try
                {
                    ISP = client.DownloadString("http://netfree.link/nf/user/0");
                }
                catch { };

                if (ISP.Contains("@rl-internet"))
                    ISP = "RL ISP";
                else if (ISP.Contains("@019"))
                    ISP = "019";
                else if (ISP.Contains("@URI"))
                    ISP = "NISIM-VPN";
                else if (ISP.Contains("@netfree-anywhere"))
                    ISP = "NetFreeAnywhere";
                else
                    ISP = "";
            }
            return ISP;
        }

        private int Connected()
        {
            Ping pinger = new Ping();
            for (int tries = 1; tries < 3; tries++)
            {
                try
                {
                    PingReply netfree = pinger.Send("NetFree.Link");
                    PingReply gmail = pinger.Send("mail.google.com");
                    if (netfree.Status == IPStatus.Success)
                        return 2;
                    else if (gmail.Status == IPStatus.Success)
                        return 1;
                }
                catch { };
            }
            return 0;
        }

        private void SetTime(DateTime datetime)
        {
            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = short.Parse(datetime.Year.ToString());
            st.wMonth = short.Parse(datetime.Month.ToString());
            st.wDay = short.Parse(datetime.Day.ToString());
            st.wHour = short.Parse(datetime.Hour.ToString());
            st.wMinute = short.Parse(datetime.Minute.ToString());
            st.wSecond = short.Parse(datetime.Second.ToString());

            SetSystemTime(ref st);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            fixcert.Visible = false;
            fixtime.Visible = false;
            loader.Visible = true;
            if (Connected() == 2)
            {
                string isp = MyIspIssuerName();
                Cert.Visible = true;
                ISP.Visible = true;
                TimeCorrect.Visible = true;
                Internet.Text = "אינטרנט מחובר";
                if (CheckCert(isp) == 2)
                    Cert.Text = "תעודה מותקן";
                else if (CheckCert(isp) == 1)
                {
                    Cert.Text = "תעודה לא עובד";
                    fixcert.Visible = true;
                }
                else if (CheckCert(isp) == 0)
                {
                    Cert.Text = "תעודה לא מותקן";
                    fixcert.Visible = true;
                }
                else
                    Cert.Text = "אין ספק נטפרי";

                if (isp != "")
                    ISP.Text = isp;
                else
                    ISP.Text = "לא מחובר לספק נטפרי";

                if (TimeRight())
                    TimeCorrect.Text = "תאריך נכון";
                else
                {
                    TimeCorrect.Text = "תאריך לא נכון";
                    fixtime.Visible = true;
                }
            }
            else if (Connected() == 1)
            {
                TimeCorrect.Visible = true;
                Internet.Text = "אין גישה לנטפרי";
                Cert.Visible = false;
                ISP.Visible = false;

                if (TimeRight())
                    TimeCorrect.Text = "תאריך נכון";
                else
                {
                    TimeCorrect.Text = "תאריך נכון";
                    fixtime.Visible = true;
                }
            }
            else
            {
                Internet.Text = "אינטרנט לא מחובר";
                Cert.Visible = false;
                ISP.Visible = false;
                TimeCorrect.Visible = false;
            }
            loader.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetTime(InternetTime());
            refresh();
            Process.Start("timedate.cpl").WaitForExit();
            refresh();
        }

        private void fixcert_Click(object sender, EventArgs e)
        {
            CertInstall();
            refresh();
            if (CheckCert(MyIspIssuerName()) == 1 || CheckCert(MyIspIssuerName()) == 2)
            {
                MessageBox.Show("התעודה הותקן בהצלחה", "התקנת תעודת אבטחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult answer = MessageBox.Show("התעודה לא הותקן", "התקנת תעודת אבטחה", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (answer == DialogResult.Retry)
                    fixcert_Click(sender, e);
            }
        }
    }
}
