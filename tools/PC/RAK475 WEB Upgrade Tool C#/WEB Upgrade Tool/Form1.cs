using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Management;
using RAK420_Config_Tool;
using CodeProject.Dialog;
using System.Configuration;

namespace WEB_Upgrade_Tool
{  
    public partial class Form1 : Form
    {
        //WIFI扫描和配置变量
        UdpClient myUdpclient = null;
        string scancmd = "@LT_WIFI_DEVICE@";
        string scancmd_ack = "@LT_WIFI_CONFIRM@";
        int searchdesport = 55555;
        int searchsrcport = 55556;
        private Thread UDPThread = null;
        int selecteds = 0;
        IPEndPoint myUDPCIpe = null;
        private Thread UDPThread_LTSP = null;
        bool myUdpclientOPEN = false;
        bool UDPThread_LTSP_Enable = false;
        private int line_count = 0;
        private byte timerretry_count = 0;
        string[] Module_MAC_List = new string[100];//声明一个临时数组存储当前的MAC地址列表
        bool Search_Timeout = false;//扫描是否超时标志量
        RAK420 RAK420_INFO = new RAK420();//声明RAK420类中RAK420_INFO信息结构体
        byte[] empty = new byte[0];//定义一个空字节数组

        byte[] KeyName = new byte[100];//关键字
        byte[] Val = new byte[100];//配置数据信息
        bool reset_time = false;//判断是否收到复位成功
        bool facreset_time = false;//判断是否收到恢复出厂设置成功

        private bool ch_en = true;//false表示中文状态；true表示英文状态
        string BoardCastIP = "";       
        FileStream file_bin = null;

        string Get_ip = "GET /info.cgi HTTP/1.1\r\nHost: ";
        int Get_port = 80;
        string Get_length = "\r\nConnection: Keep-Alive";
        string Get_admin = "\r\nAuthorization: Basic ";

        string Post_ip = "POST /upgrade.cgi HTTP/1.1\r\nHost: ";
        string Post_length = "\r\nContent-Type: multipart/form-data; boundary=---------------------------7e04923e029e\r\nConnection: Keep-Alive\r\nContent-Length: ";
        string Post_admin = "\r\nAuthorization: Basic ";
        string Post_end = "---------------------------7e04923e029e\r\nContent-Disposition: form-data; name=\"fw_file\"; filename=\"ota_all.bin\"\r\nContent-Type: application/octet-stream\r\n\r\n";
        private Thread Thread_TCP = null;

        TcpClient Tcp_socket = null;
        NetworkStream Tcp_stream = null;

        TcpClient Tcp_socket1 = null;
        NetworkStream Tcp_stream1 = null;

        TcpClient Tcp_socket2 = null;
        NetworkStream Tcp_stream2 = null;

        TcpClient Tcp_socket3 = null;
        NetworkStream Tcp_stream3 = null;

        TcpClient Tcp_socket4 = null;
        NetworkStream Tcp_stream4 = null;

        TcpClient Tcp_socket5 = null;
        NetworkStream Tcp_stream5 = null;

        TcpClient Tcp_socket6 = null;
        NetworkStream Tcp_stream6 = null;

        TcpClient Tcp_socket7 = null;
        NetworkStream Tcp_stream7 = null;

        TcpClient Tcp_socket8 = null;
        NetworkStream Tcp_stream8 = null;

        TcpClient Tcp_socket9 = null;
        NetworkStream Tcp_stream9 = null;

        int file_len = 0;
        int file_len1 = 0;
        int file_len2 = 0;
        int file_len3 = 0;
        int file_len4 = 0;
        int file_len5 = 0;
        int file_len6 = 0;
        int file_len7 = 0;
        int file_len8 = 0;
        int file_len9 = 0;

        bool rak477getversion = true;

        int socket_line = 0;
        int socket_line1 = 0;
        int socket_line2 = 0;
        int socket_line3 = 0;
        int socket_line4 = 0;
        int socket_line5 = 0;
        int socket_line6 = 0;
        int socket_line7 = 0;
        int socket_line8 = 0;
        int socket_line9 = 0;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer4 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer5 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer6 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer7 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer8 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer9 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerout = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerload = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Tick += new System.EventHandler(timer_Upgrade);

            timer1.Enabled = false;
            timer1.Interval = 10;
            timer1.Tick += new System.EventHandler(timer1_Upgrade);

            timer2.Enabled = false;
            timer2.Interval = 10;
            timer2.Tick += new System.EventHandler(timer2_Upgrade);

            timer3.Enabled = false;
            timer3.Interval = 10;
            timer3.Tick += new System.EventHandler(timer3_Upgrade);

            timer4.Enabled = false;
            timer4.Interval = 10;
            timer4.Tick += new System.EventHandler(timer4_Upgrade);

            timer5.Enabled = false;
            timer5.Interval = 10;
            timer5.Tick += new System.EventHandler(timer5_Upgrade);

            timer6.Enabled = false;
            timer6.Interval = 10;
            timer6.Tick += new System.EventHandler(timer6_Upgrade);

            timer7.Enabled = false;
            timer7.Interval = 10;
            timer7.Tick += new System.EventHandler(timer7_Upgrade);

            timer8.Enabled = false;
            timer8.Interval = 10;
            timer8.Tick += new System.EventHandler(timer8_Upgrade);

            timer9.Enabled = false;
            timer9.Interval = 10;
            timer9.Tick += new System.EventHandler(timer9_Upgrade);

            timerout.Enabled = false;
            timerout.Interval = 500;
            timerout.Tick += new System.EventHandler(timerout_Upgrade);

            timerload.Enabled = false;
            timerload.Interval = 1000;
            timerload.Tick += new System.EventHandler(timerload_Upgrade);
        }
        /*********************************************************************************************************
         ** 功能说明：查询本机子网掩码和网关地址，计算出广播地址并返回
         ********************************************************************************************************/
        public string GetSubnetAndGateway()
        {
            string strIP, strSubnet, strGateway, strDNS;
            strIP = "0.0.0.0";
            strSubnet = "0.0.0.0";
            strGateway = "0.0.0.0";
            strDNS = "0.0.0.0";
            BoardCastIP = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    try
                    {
                        if (Convert.ToBoolean(nic["IPEnabled"]) == true)
                        {

                            if ((nic["IPAddress"] as String[]).Length > 0 && strIP == "0.0.0.0")
                            {
                                strIP = (nic["IPAddress"] as String[])[0];
                            }
                            if ((nic["IPSubnet"] as String[]).Length > 0 && strSubnet == "0.0.0.0")
                            {
                                strSubnet = (nic["IPSubnet"] as String[])[0];
                            }
                            if ((nic["DefaultIPGateway"] as String[]).Length > 0 && strGateway == "0.0.0.0")
                            {
                                strGateway = (nic["DefaultIPGateway"] as String[])[0];
                            }
                            if ((nic["DNSServerSearchOrder"] as String[]).Length > 0 && strDNS == "0.0.0.0")
                            {
                                strDNS = (nic["DNSServerSearchOrder"] as String[])[0];
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            string[] Subnet = strSubnet.Split(new Char[] { '.' });
            string[] GateWay = strGateway.Split(new Char[] { '.' });
            if ((Subnet.Length != 4) || (GateWay).Length != 4)
            {
                return BoardCastIP;
            }
            int x1 = (~(Convert.ToByte(Subnet[0])) | (Convert.ToByte(GateWay[0]))) & 0x000000FF;
            int x2 = (~(Convert.ToByte(Subnet[1])) | (Convert.ToByte(GateWay[1]))) & 0x000000FF;
            int x3 = (~(Convert.ToByte(Subnet[2])) | (Convert.ToByte(GateWay[2]))) & 0x000000FF;
            int x4 = (~(Convert.ToByte(Subnet[3])) | (Convert.ToByte(GateWay[3]))) & 0x000000FF;
            BoardCastIP = x1.ToString() + "." + x2.ToString() + "." + x3.ToString() + "." + x4.ToString();
            //return "IP地址 " + strIP + "\n" + "子网掩码 " + strSubnet + "\n" + "默认网关 " + strGateway + "\n" + "DNS服务器 " + strDNS;
            return BoardCastIP;
        }



        /*********************************************************************************************************
         ** 功能说明：查询本机IP地址
         ********************************************************************************************************/
        int errorcode = 0;
        System.Net.IPAddress GetHost_IPAddresses()   //errorcode 定义为  返回实际的错误值,如果非0 表示错误,如果为0 则表示无错误
        {
            System.Net.IPAddress[] addressListUDP = Dns.GetHostAddresses(Dns.GetHostName());//会返回所有地址，包括IPv4和IPv6
            //IPAddress ipAddrUDP = null;
            System.Net.IPAddress[] AddressList_IP = { null, null, null, null, null, null, null, null, null, null };//会返回,IPv4地址
            int n = 0;
#if  DEBUG
            Console.Write("IP(IPV4&IPV6):" + addressListUDP.Length.ToString() + "\r\n");
            for (int i = 0; i < addressListUDP.Length; i++)
            {
                Console.Write(i.ToString() + "-->AddressFamily:" + addressListUDP[i].AddressFamily + "\r\n");
                Console.Write("IP Address:" + addressListUDP[i].ToString() + "\r\n");

            }
#endif
            for (int i = 0; i < addressListUDP.Length; i++)
            {

                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (addressListUDP[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    AddressList_IP[n] = addressListUDP[i];
                    n++;
                }
            }
#if  DEBUG
            Console.Write("IP Count:" + n.ToString() + "\r\n");
#endif
            if (n > 0)
            {
                errorcode = 0;
                // ipAddrUDP = AddressList_IP[1];
                return AddressList_IP[n - 1];

            }
            else
            {
                errorcode = -1;
                return null;
            }
        }

        int search_count = 0;//扫描次数
        /*********************************************************************************************************
         ** 功能说明：发送扫描指令
         ********************************************************************************************************/
        void send_search_cmd()
        {
            /*            //由本机地址变换为广播地址
                        string aa = GetHost_IPAddresses().ToString();
                        if (errorcode != 0)
                        {
                            MsgBox.Show("Please check network connecting.");
                            return;
                        }
                        errorcode = 0;
                    #if  DEBUG
                        Console.Write("Get host IP address.\r\n");
                        Console.Write("IP:" + aa + "\r\n");
                    #endif

                        string[] tmp = aa.Split('.');
                        tmp[3] = "255";
                        aa = tmp[0] + "." + tmp[1] + "." + tmp[2] + "." + tmp[3];
            */
            BoardCastIP = GetSubnetAndGateway();
            if (BoardCastIP == "")
                return;
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(BoardCastIP), searchdesport);


            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(scancmd);
            myUdpclient.Send(bytes, bytes.Length, iep);//发送扫描信息
        }


        /*********************************************************************************************************
         ** 功能说明：扫描模块
         ********************************************************************************************************/
        bool search = false;//是否在search过程中
        private void buttonScan_Click(object sender, EventArgs e)
        {
            rak477getversion = true;
            try
            {
                search = true;
                search_count = 0;
                buttonScan.Enabled = false;
                dataGVscan.Enabled = false;
                dataGVscan.Rows.Clear();//清空列表
                Search_Timeout = false;//清扫描超时

                for (int m = 0; m < line_count; m++)//清空MAC字符串数组
                {
                    Module_MAC_List[m] = null;
                }
                line_count = 0;//列表行数清零

                if (myUdpclientOPEN == true)//已经开启单播接收线程
                {
                    UDPThread_LTSP.Abort();//关闭单播接收线程                    
                }

                if (myUdpclient == null)
                    myUdpclient = new UdpClient(searchsrcport);
                send_search_cmd();//发送扫描信息
                search_count++;
                UDPThread = new Thread(new ThreadStart(Search_Thread));
                UDPThread.IsBackground = true;//关闭窗口时自动关闭线程
                timersearch.Start();
                UDPThread.Start();
            }
            catch (ArgumentNullException ae)
            {
                //显示异常信息给客户。
               MessageBox.Show(ae.Message);
            }
        }
        /*********************************************************************************************************
         ** 功能说明：扫描线程
         ********************************************************************************************************/
        void Search_Thread()
        {
            IPEndPoint ipe = new IPEndPoint(GetHost_IPAddresses(), searchsrcport);

            if (errorcode != 0)
            {
                if (ch_en)
                    MsgBox.Show("Please check network connecting.");
                else
                    MsgBox.Show("请确认网络是否连接.");
                return;
            }
            errorcode = 0;
            while (true)
            {
                if ((myUdpclient != null) && (Search_Timeout == false))
                {
                    if (myUdpclient.Available > 0)
                    {
                        byte[] bytes = myUdpclient.Receive(ref ipe);//接收扫描到的数据信息
                        if (bytes != null)
                        {
                            this.Invoke((EventHandler)(delegate
                            {
                                bool MAC_NEW = true;
                                //清空以前的扫描数据
                                Array.Clear(RAK420_INFO.GroupName, 0, 16);
                                Array.Clear(RAK420_INFO.NickName, 0, 16);
                                Array.Clear(RAK420_INFO.IP, 0, 4);
                                RAK420_INFO.MAC = "";


                                if (bytes.Length == 43)
                                {
                                    string Module_MAC_List_temp = null;
                                    for (int j = 0; j < 6; j++)
                                    {
                                        if (bytes[j + 36] >= 16)
                                            Module_MAC_List_temp += bytes[j + 36].ToString("X");
                                        else
                                            Module_MAC_List_temp += "0" + bytes[j + 36].ToString("X");
                                    }

                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (bytes[i] != 0x00)
                                            RAK420_INFO.NickName[i] = bytes[i];
                                        else
                                            break;

                                    }
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (bytes[i + 16] != 0x00)
                                            RAK420_INFO.GroupName[i] = bytes[i + 16];
                                        else
                                            break;
                                    }
                                    for (int i = 0; i < 4; i++)
                                    {
                                        RAK420_INFO.IP[i] = bytes[i + 32];
                                    }
                                    RAK420_INFO.MAC = Module_MAC_List_temp;
                                    /*                                   for (int i = 0; i < 6; i++)
                                                                        {
                                                                            if (bytes[i + 36] >= 16)
                                                                                RAK420_INFO.MAC += bytes[i + 36].ToString("X");
                                                                            else
                                                                                RAK420_INFO.MAC += "0" + bytes[i + 36].ToString("X");
                                        
                                                                        }
                                    */
                                    /*                             
                                      try
                                      {
                                          //获取模块名称
                                          Array.Copy(bytes, 0, RAK420_INFO.NickName, 0, 16);
                                          //获取组名称
                                          Array.Copy(bytes, 16, RAK420_INFO.GroupName, 0, 16);
                                          //获取IP地址
                                          Array.Copy(bytes, 32, RAK420_INFO.IP, 0, 4);
                                          //获取MAC地址
                                          Array.Copy(bytes, 36, RAK420_INFO.MAC, 0, 6);
                                      }
                                      catch (Exception ee)
                                      {
                                          MsgBox.Show(ee.ToString());
                                      }

                                      for (int i = 0; i < 6; i++)
                                      {
                                          //记录MAC地址
                                          Module_MAC_List_temp += RAK420_INFO.MAC[i].ToString("X");                                  
                                      }
                                   */
                                    //获取信号强度
                                    RAK420_INFO.RSSI = bytes[42];

                                    //判断MAC地址是否相同    
                                    for (int m = 0; m < line_count; m++)
                                    {
                                        if (Module_MAC_List_temp == Module_MAC_List[m])
                                        {
                                            MAC_NEW = false;//相同的话，表示不是新的MAC，无需新增一行列表
                                            break;
                                        }
                                    }
                                    if (MAC_NEW == true)//不相同的话，表示是新的MAC，需新增一行列表
                                    {
                                        //记录新增一行列表的MAC
                                        Module_MAC_List[line_count] = RAK420_INFO.MAC;

                                        //新增一行列表
                                        if ((line_count >= 0))
                                        {
                                            dataGVscan.Rows.Add();
                                        }
                                        string ipstring = ipe.Address.ToString();

                                        //填充列表
                                        this.dataGVscan.Rows[line_count].Cells[0].Value = line_count+1;//序号
                                        this.dataGVscan.Rows[line_count].Cells[1].Value =
                                            System.Text.Encoding.ASCII.GetString(RAK420_INFO.NickName);//模块名称
                                        this.dataGVscan.Rows[line_count].Cells[2].Value =
                                            System.Text.Encoding.ASCII.GetString(RAK420_INFO.GroupName);//组名称
                                        this.dataGVscan.Rows[line_count].Cells[3].Value = ipstring;//IP地址
                                        for (int m = 0; m < 5; m++)
                                            RAK420_INFO.MAC = RAK420_INFO.MAC.Insert(2 * (5 - m), ":");
                                        this.dataGVscan.Rows[line_count].Cells[4].Value = RAK420_INFO.MAC;//MAC地址
                                        this.dataGVscan.Rows[line_count].Cells[5].Value = "-" +(256-RAK420_INFO.RSSI).ToString();//信号强度
                                        this.dataGVscan.Rows[line_count].Cells[7].Value = "准备升级";//信号强度
                                        if (this.dataGVscan.RowCount<=11)
                                            this.dataGVscan.Rows[line_count].Cells[8].Value = true;
                                        line_count++;//下一行
                                    }
                                }
                            }));
                        }
                    }
                }
            }
        }

        /*********************************************************************************************************
        ** 功能说明：判断扫描是否超时
        ********************************************************************************************************/
        int ver_num = 0;
        private void timersearch_Tick(object sender, EventArgs e)
        {
            if (search_count < 5)
                send_search_cmd();//发送扫描信息            
            if (search_count == 7)
            {
                timersearch.Stop();
                search = false;
                myUdpclientOPEN = false;
                Search_Timeout = true;
                UDPThread.Abort();
                this.Invoke((EventHandler)(delegate
                {
                    buttonScan.Enabled = true;
                    dataGVscan.Enabled = true;
                }));
                getversion = true;
                //获取模块版本号
                if (Thread_TCP == null)//未开启接收线程
                {
                    Thread_TCP = new Thread(new ThreadStart(Thread_TCP_Thread));//接收数据线程
                    Thread_TCP.IsBackground = true;
                    Thread_TCP.Start();//开启接收线程
                }
                Socket_Connect();
/*
                string admin_data = "user_name=" + textBoxadmin.Text + "&user_password=" + textBoxpsk.Text;//整个认证信息--字符串
                byte[] admin = new byte[admin_data.Length];
                ver_num = 0;
                admin = ASCIIEncoding.ASCII.GetBytes(admin_data); //整个认证信息--字节数组
                if (dataGVscan.Rows[ver_num].Cells[3].Value != null)
                {
                    string ip = dataGVscan.Rows[ver_num].Cells[3].Value.ToString();//获取目标IP地址
                    LTSP_CMD(0xF, admin, IPAddress.Parse(ip)); //发送认证信息 
                    confirm = true;
                    timerout.Enabled = true;//开始计时
                }
*/
            }
            search_count++;
        }
        /*********************************************************************************************************
        ** 功能说明：异或函数和异或校验函数
        ********************************************************************************************************/
        private byte Xor_Sum(byte[] Xor_SumArray, int Xor_SumLen)
        {
            int xor_sum = 0;
            int i = 0;
            for (i = 0; i < Xor_SumLen - 1; i++)
            {
                xor_sum ^= Xor_SumArray[i];
            }
            return ((byte)xor_sum);
        }

        //异或校验函数
        private bool Xor(byte[] XorArray, int XorLen)
        {
            bool xor_successful = false;
            int xor_sum = 0;
            try
            {
                for (int i = 0; i < XorLen - 1; i++)
                {
                    xor_sum ^= XorArray[i];
                }
                if (xor_sum == XorArray[XorLen - 1]) //异或成功
                    xor_successful = true;
                else                             //异或失败
                    xor_successful = false;
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
                xor_successful = false;
            }
            return xor_successful;//返回校验结果
        }
        /*********************************************************************************************************
        ** 功能说明：单播与模块进行数据交互
        ********************************************************************************************************/
        public void LTSP_CMD(byte CMD, byte[] data, IPAddress Destip)
        {
            byte[] sendata = new byte[6 + data.Length + 1];
            //初始化命令
            sendata[0] = CMD;
            sendata[1] = 0x00;
            //数据长度
            sendata[2] = (byte)(data.Length & 0xFF);
            sendata[3] = (byte)((data.Length >> 8) & 0xFF);
            //响应码
            sendata[4] = 0x00;
            sendata[5] = 0x00;
            //数据内容
            for (int i = 0; i < data.Length; i++)
            {
                sendata[6 + i] = data[i];
            }
            //求校验
            sendata[6 + data.Length] = Xor_Sum(sendata, sendata.Length);
            //if (myUdpclient == null)
            //    myUdpclient = new UdpClient(searchsrcport);
            myUDPCIpe = new IPEndPoint(Destip, searchdesport);
            myUdpclient.Send(sendata, sendata.Length, myUDPCIpe);

            UDPThread_LTSP = new Thread(new ThreadStart(LTSPCMD_Thread));//UDP单播接收数据线程
            UDPThread_LTSP.IsBackground = true;

            if (myUdpclientOPEN == false)//未开启单播接收线程
            {
                UDPThread_LTSP.Start();//开启单播接收线程
                myUdpclientOPEN = true;
            }
        }
        /*********************************************************************************************************
       ** 功能说明：UDP单播接收数据线程
       *********************************************************************************************************/
        bool confirm = false;
        bool getversion = false;
        void LTSPCMD_Thread()
        {
            IPEndPoint ipe = new IPEndPoint(GetHost_IPAddresses(), searchsrcport);
            if (errorcode != 0)
            {
                if (ch_en)
                    MsgBox.Show("Please check network connecting.");
                else
                    MsgBox.Show("请确认网络是否连接.");
                return;
            }
            errorcode = 0;
            while (search == false)//非扫描过程中
            {
                if (myUdpclient != null)
                {
                    if (myUdpclient.Available > 0)
                    {
                        byte[] buf = myUdpclient.Receive(ref ipe);
                        if (buf != null)
                        {
                            this.Invoke((EventHandler)(delegate
                            {
                                if (Xor(buf, buf.Length) == true)//接收数据校验成功
                                {
                                    if (buf[0] == 0xF)//接收到模块给的认证回复：认证成功
                                    {
                                        confirm = false;
                                        timerout.Enabled = false;//开始计时                                       
                                        if (buf[4] == 0xFE)//模块认证失败
                                        {
                                            this.dataGVscan.Rows[ver_num].Cells[6].Value = "认证失败";
                                            ver_num++;
                                            if (ver_num < this.dataGVscan.RowCount - 1)
                                            {
                                                string admin_data = "user_name=" + textBoxadmin.Text + "&user_password=" + textBoxpsk.Text;//整个认证信息--字符串
                                                byte[] admin = new byte[admin_data.Length];
                                                admin = ASCIIEncoding.ASCII.GetBytes(admin_data); //整个认证信息--字节数组
                                                string ip = dataGVscan.Rows[ver_num].Cells[3].Value.ToString();//获取目标IP地址
                                                LTSP_CMD(0xF, admin, IPAddress.Parse(ip)); //发送认证信息 
                                                confirm = true;
                                                timerout.Enabled = true;//开始计时
                                            }
                                        }
                                        else
                                        {
                                            if (this.dataGVscan.Rows[ver_num].Cells[3].Value.ToString() != null)
                                            {
                                                LTSP_CMD(0x06, empty, IPAddress.Parse(this.dataGVscan.Rows[ver_num].Cells[3].Value.ToString())); //获取模块版本号
                                                getversion = true;
                                                timerout.Enabled = true;//开始计时
                                            }
                                        }
                                    }
                                    if (buf[0] == 0x06)//获取到模块的版本信息
                                    {
                                        getversion = false;
                                        timerout.Enabled = false;//开始计时 
                                        byte[] version = new byte[buf[3] * 256 + buf[2] - 15];//版本号数据长度
                                        for (int i = 0; i < version.Length; i++)
                                        {
                                            version[i] = buf[21 + i];//获取版本号数据
                                        }
                                        this.dataGVscan.Rows[ver_num].Cells[6].Value = System.Text.Encoding.ASCII.GetString(version);//版本号填入TextBox中显示
                                        ver_num++;
                                        if (ver_num < this.dataGVscan.RowCount - 1)
                                        {                                           
                                            string admin_data = "user_name=" + textBoxadmin.Text + "&user_password=" + textBoxpsk.Text;//整个认证信息--字符串
                                            byte[] admin = new byte[admin_data.Length];
                                            admin = ASCIIEncoding.ASCII.GetBytes(admin_data); //整个认证信息--字节数组
                                            string ip = dataGVscan.Rows[ver_num].Cells[3].Value.ToString();//获取目标IP地址
                                            LTSP_CMD(0xF, admin, IPAddress.Parse(ip)); //发送认证信息 
                                            confirm = true;
                                            timerout.Enabled = true;//开始计时
                                        }
                                    }
                                } 
                                  
                            }));
                        }
                    }
                }
            }
        }


        void Time_Count(bool enable_count,int c,int line)
        {
            if (enable_count)
            {
                this.dataGVscan.Rows[line].Cells[7].Value = "加载中...  剩余" + c + "s";
                c--;
                if (c == 0)
                {
                    this.dataGVscan.Rows[line].Cells[7].Value = "加载成功，升级完毕";
                    enable_count = false;
                }
            }
        }

        bool Is_Over(bool enable_count, int c)
        {
            bool ret = false;
            if (enable_count == false)
            {
                ret = true;
            }
            else if (enable_count && (c == 0))
            {
                ret = true;
            }
            return ret;
        }

        private void timerout_Upgrade(object sender, EventArgs e)
        {
/*            if (confirm)
            {
                string admin_data = "user_name=" + textBoxadmin.Text + "&user_password=" + textBoxpsk.Text;//整个认证信息--字符串
                byte[] admin = new byte[admin_data.Length];
                if (dataGVscan.Rows[ver_num].Cells[3].Value == null)
                {
                    string ip = dataGVscan.Rows[ver_num].Cells[3].Value.ToString();//获取目标IP地址
                    LTSP_CMD(0xF, admin, IPAddress.Parse(ip)); //发送认证信息
                }
            }
            if (getversion)
            {
                LTSP_CMD(0x06, empty, IPAddress.Parse(this.dataGVscan.Rows[ver_num].Cells[3].Value.ToString())); //获取模块版本号
            }
*/
        }
        //
        int count=60;
        int count1 = 60;
        int count2 = 60;
        int count3 = 60;
        int count4 = 60;
        int count5 = 60;
        int count6 = 60;
        int count7 = 60;
        int count8 = 60;
        int count9 = 60;
        bool Is_count = false;
        bool Is_count1 = false;
        bool Is_count2 = false;
        bool Is_count3 = false;
        bool Is_count4 = false;
        bool Is_count5 = false;
        bool Is_count6 = false;
        bool Is_count7 = false;
        bool Is_count8 = false;
        bool Is_count9 = false;

        private void timerload_Upgrade(object sender, EventArgs e)
        {
/*            Time_Count(Is_count, count, socket_line);
            Time_Count(Is_count1, count1, socket_line1);
            Time_Count(Is_count2, count2, socket_line2);
            Time_Count(Is_count3, count3, socket_line3);
            Time_Count(Is_count4, count4, socket_line4);
            Time_Count(Is_count5, count5, socket_line5);
            Time_Count(Is_count6, count6, socket_line6);
            Time_Count(Is_count7, count7, socket_line7);
            Time_Count(Is_count8, count8, socket_line8);
            Time_Count(Is_count9, count9, socket_line9);
*/
            if (Is_count)
            {
                this.dataGVscan.Rows[socket_line].Cells[7].Value = "加载中...  剩余" + count + "s";
                count--;
                if (count == 0)
                {
                    this.dataGVscan.Rows[socket_line].Cells[7].Value = "加载成功，升级完毕";
                    Is_count = false;
                }
            }
            if (Is_count1)
            {
                this.dataGVscan.Rows[socket_line1].Cells[7].Value = "加载中...  剩余" + count1 + "s";
                count1--;
                if (count1 == 0)
                {
                    this.dataGVscan.Rows[socket_line1].Cells[7].Value = "加载成功，升级完毕";
                    Is_count1 = false;
                }
            }
            if (Is_count2)
            {
                this.dataGVscan.Rows[socket_line2].Cells[7].Value = "加载中...  剩余" + count2 + "s";
                count2--;
                if (count2 == 0)
                {
                    this.dataGVscan.Rows[socket_line2].Cells[7].Value = "加载成功，升级完毕";
                    Is_count2 = false;
                }
            }
            if (Is_count3)
            {
                this.dataGVscan.Rows[socket_line3].Cells[7].Value = "加载中...  剩余" + count3 + "s";
                count3--;
                if (count3 == 0)
                {
                    this.dataGVscan.Rows[socket_line3].Cells[7].Value = "加载成功，升级完毕";
                    Is_count3 = false;
                }
            }
            if (Is_count4)
            {
                this.dataGVscan.Rows[socket_line4].Cells[7].Value = "加载中...  剩余" + count4 + "s";
                count4--;
                if (count4 == 0)
                {
                    this.dataGVscan.Rows[socket_line4].Cells[7].Value = "加载成功，升级完毕";
                    Is_count4 = false;
                }
            }
            if (Is_count5)
            {
                this.dataGVscan.Rows[socket_line5].Cells[7].Value = "加载中...  剩余" + count5 + "s";
                count5--;
                if (count5 == 0)
                {
                    this.dataGVscan.Rows[socket_line5].Cells[7].Value = "加载成功，升级完毕";
                    Is_count5 = false;
                }
            }
            if (Is_count6)
            {
                this.dataGVscan.Rows[socket_line6].Cells[7].Value = "加载中...  剩余" + count6 + "s";
                count6--;
                if (count6 == 0)
                {
                    this.dataGVscan.Rows[socket_line6].Cells[7].Value = "加载成功，升级完毕";
                    Is_count6 = false;
                }
            }
            if (Is_count7)
            {
                this.dataGVscan.Rows[socket_line7].Cells[7].Value = "加载中...  剩余" + count7 + "s";
                count7--;
                if (count7 == 0)
                {
                    this.dataGVscan.Rows[socket_line7].Cells[7].Value = "加载成功，升级完毕";
                    Is_count7 = false;
                }
            }
            if (Is_count8)
            {
                this.dataGVscan.Rows[socket_line8].Cells[7].Value = "加载中...  剩余" + count8 + "s";
                count8--;
                if (count8 == 0)
                {
                    this.dataGVscan.Rows[socket_line8].Cells[7].Value = "加载成功，升级完毕";
                    Is_count8 = false;
                }
            }
            if (Is_count9)
            {
                this.dataGVscan.Rows[socket_line9].Cells[7].Value = "加载中...  剩余" + count9 + "s";
                count9--;
                if (count9 == 0)
                {
                    this.dataGVscan.Rows[socket_line9].Cells[7].Value = "加载成功，升级完毕";
                    Is_count9 = false;
                }
            }

            if ((Is_Over(Is_count, count))
                && (Is_Over(Is_count1, count1))
                && (Is_Over(Is_count2, count2))
                && (Is_Over(Is_count3, count3))
                && (Is_Over(Is_count4, count4))
                && (Is_Over(Is_count5, count5))
                && (Is_Over(Is_count6, count6))
                && (Is_Over(Is_count7, count7))
                && (Is_Over(Is_count8, count8))
                && (Is_Over(Is_count9, count9))
                ) 
            {
                buttonupgrade.Enabled = true;
                timerload.Enabled = false;
            }
            
/*
            int s = count / 10;
            int g = count % 10;
            switch (s)
            { 
                case 0:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit0;
                    break;
                case 1:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit1;
                    break;
                case 2:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit2;
                    break;
                case 3:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit3;
                    break;
                case 4:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit4;
                    break;
                case 5:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit5;
                    break;
                case 6:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit6;
                    break;
                case 7:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit7;
                    break;
                case 8:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit8;
                    break;
                case 9:
                    pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit9;
                    break;
            }
            switch (g)
            {
                case 0:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit0;
                    break;
                case 1:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit1;
                    break;
                case 2:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit2;
                    break;
                case 3:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit3;
                    break;
                case 4:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit4;
                    break;
                case 5:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit5;
                    break;
                case 6:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit6;
                    break;
                case 7:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit7;
                    break;
                case 8:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit8;
                    break;
                case 9:
                    pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit9;
                    break;
            }
            count--;
            if (count == 0)
            {
                pictureBox1.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit4;
                pictureBox2.Image = global::WEB_Upgrade_Tool.Properties.Resources.digit0;
                buttonupgrade.Enabled = true;
                label4.Visible = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                groupBox2.Visible = true;
                timerload.Enabled = false;
            }
*/
        }
        
        /*********************************************************************************************************
        ** 功能说明：选择固件路径
        ********************************************************************************************************/
        private void buttonimport_Click(object sender, EventArgs e)
        {         
            string str = ConfigurationManager.AppSettings["Directory"];

            OpenFileDialog op = new OpenFileDialog();//弹出浏览框
            if (str == "")
                op.InitialDirectory = System.Environment.CurrentDirectory;//打开当前路径
            else
                op.InitialDirectory = str;//打开上一次路径
            op.RestoreDirectory = false;//还原当前路径
            op.Filter = "BIN文件(*.bin)|*.bin";//还原当前路径
            DialogResult result = op.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = op.FileName;//获取文件路径
                textBoximport.Text = filename;
                if (textBoximport.Text != filename)
                {
                    if (file_bin != null)
                    {
                        file_bin.Close();
                        file_bin = null;
                    }
                }
                int index = filename.LastIndexOf("V");
                if (index != -1)
                {
                    textBoxVersion.Text = filename.Substring(index+1).Replace(".bin","");
                }
                if (filename != null)
                {
                    //保存这次的路径
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings.Clear();
                    configuration.AppSettings.Settings.Add("Directory", filename);
                    configuration.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

        void Socket_Connect(string ip, int port, int line)
        {
            if ((line == 0) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket = new TcpClient(ip, port);
                    Tcp_stream = Tcp_socket.GetStream();
                    Tcp_stream.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 1) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket1 = new TcpClient(ip, port);
                    Tcp_stream1 = Tcp_socket1.GetStream();
                    Tcp_stream1.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 2) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket2 = new TcpClient(ip, port);
                    Tcp_stream2 = Tcp_socket2.GetStream();
                    Tcp_stream2.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 3) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket3 = new TcpClient(ip, port);
                    Tcp_stream3 = Tcp_socket3.GetStream();
                    Tcp_stream3.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 4) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket4 = new TcpClient(ip, port);
                    Tcp_stream4 = Tcp_socket4.GetStream();
                    Tcp_stream4.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 5) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket5 = new TcpClient(ip, port);
                    Tcp_stream5 = Tcp_socket5.GetStream();
                    Tcp_stream5.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 6) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket6 = new TcpClient(ip, port);
                    Tcp_stream6 = Tcp_socket6.GetStream();
                    Tcp_stream6.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 7) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket7 = new TcpClient(ip, port);
                    Tcp_stream7 = Tcp_socket7.GetStream();
                    Tcp_stream7.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 8) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket8 = new TcpClient(ip, port);
                    Tcp_stream8 = Tcp_socket8.GetStream();
                    Tcp_stream8.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            else if ((line == 9) && (dataGVscan.Rows[line].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
            {
                try
                {
                    Tcp_socket9 = new TcpClient(ip, port);
                    Tcp_stream9 = Tcp_socket9.GetStream();
                    Tcp_stream9.Write(Post_head, 0, Post_head.Length);
                }
                catch (Exception)
                {
                    dataGVscan.Rows[line].Cells[7].Value = "未连接上模块";
                }
            }
            
        }

        /*********************************************************************************************************
        ** 功能说明：开始升级
        ********************************************************************************************************/
        byte[] Post_head = null;
        byte[] Post_endbyte = null;
        byte[] file_byte = null;
        int file_size = 0;
        string basic = "";
        private void buttonupgrade_Click(object sender, EventArgs e)
        {
            socket_line = 0;
            socket_line1 = 0;
            socket_line2 = 0;
            socket_line3 = 0;
            socket_line4 = 0;
            socket_line5 = 0;
            socket_line6 = 0;
            socket_line7 = 0;
            socket_line8 = 0;
            socket_line9 = 0;

            count = 10;
            count1 = 10;
            count2 = 10;
            count3 = 10;
            count4 = 10;
            count5 = 10;
            count6 = 10;
            count7 = 10;
            count8 = 10;
            count9 = 10;

            Is_count = false;
            Is_count1 = false;
            Is_count2 = false;
            Is_count3 = false;
            Is_count4 = false;
            Is_count5 = false;
            Is_count6 = false;
            Is_count7 = false;
            Is_count8 = false;
            Is_count9 = false;

            rak477getversion = false;

            if (textBoximport.Text == "")
            {
                if (ch_en)
                    MsgBox.Show("Please choose the upgrade firmware first.");
                else
                    MsgBox.Show("请先选择升级所需的文件！");
            }
            else
            {
                if (Thread_TCP == null)//未开启接收线程
                {
                    Thread_TCP = new Thread(new ThreadStart(Thread_TCP_Thread));//接收数据线程
                    Thread_TCP.IsBackground = true;
                    Thread_TCP.Start();//开启接收线程
                }

                if (file_bin != null)
                {
                    file_bin.Close();
                    file_bin = null;
                }
                
                if (file_bin == null)
                {
                    file_bin = new FileStream(textBoximport.Text, FileMode.Open);
                    file_size = (int)file_bin.Length;
                    file_byte = new byte[file_size + Post_end.Length];
                    byte[] file_temp = new byte[file_size];
                    file_bin.Read(file_temp, 0, file_size);
                    Post_endbyte = System.Text.Encoding.ASCII.GetBytes(Post_end);
                    Array.Copy(Post_endbyte, 0, file_byte, 0, Post_end.Length);
                    Array.Copy(file_temp, 0, file_byte, Post_end.Length, file_size);
                    file_size += Post_end.Length;
                }
                if (file_bin == null)
                {
                    if (ch_en)
                        MsgBox.Show("Please open the upgrade firmware first.");
                    else
                        MsgBox.Show("请先打开升级所需的文件！");
                }
                else
                {
                    Socket_Connect();
                }    
            }
           
        }

        void Socket_Connect()
        {
            for (int i = 0; i < dataGVscan.RowCount - 1; i++)
            {
                if (dataGVscan.Rows[i].Cells[3].Value.ToString() == "192.168.7.1")
                {
                    Get_port = 80;
                }
                else
                {
                    Get_port = 1352;
                }
                //if (dataGVscan.Rows[i].Cells[6].Value != null)//获取到版本号的，判断是否和升级固件一致
                {
                    //                            if (dataGVscan.Rows[i].Cells[6].Value.ToString() == textBoxVersion.Text.ToString())
                    //                            {
                    //                                dataGVscan.Rows[i].Cells[7].Value = "已是该固件，无需升级";
                    //                            }
                    //                            else
                    {
                        byte[] barray;
                        barray = Encoding.Default.GetBytes(textBoxadmin.Text + ":" + textBoxpsk.Text);
                        basic = Convert.ToBase64String(barray);
                        if (Post_head!=null)
                            Array.Clear(Post_head, 0, Post_head.Length);
                        
                        if (rak477getversion)
                        {
                            string send_data = Get_ip + dataGVscan.Rows[i].Cells[3].Value.ToString() + Get_length + Get_admin + basic;
                            Post_head = System.Text.Encoding.ASCII.GetBytes(send_data + "\r\n\r\n");
                        }
                        else
                        {
                            string send_data = Post_ip + dataGVscan.Rows[i].Cells[3].Value.ToString() + Post_length + file_size + Post_admin + basic;
                            Post_head = System.Text.Encoding.ASCII.GetBytes(send_data + "\r\n\r\n");
                        }
                        
                        
                        if ((Tcp_socket == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line = i;
                            file_len = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream = Tcp_socket.GetStream();
                                    Tcp_stream.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion==false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer.Enabled = true;
                        }
                        else if ((Tcp_socket1 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line1 = i;
                            file_len1 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket1 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream1 = Tcp_socket1.GetStream();
                                    Tcp_stream1.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer1.Enabled = true;
                        }
                        else if ((Tcp_socket2 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line2 = i;
                            file_len2 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket2 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream2 = Tcp_socket2.GetStream();
                                    Tcp_stream2.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer2.Enabled = true;
                        }
                        else if ((Tcp_socket3 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line3 = i;
                            file_len3 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket3 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream3 = Tcp_socket3.GetStream();
                                    Tcp_stream3.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer3.Enabled = true;
                        }
                        else if ((Tcp_socket4 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line4 = i;
                            file_len4 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket4 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream4 = Tcp_socket4.GetStream();
                                    Tcp_stream4.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer4.Enabled = true;
                        }
                        else if ((Tcp_socket5 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line5 = i;
                            file_len5 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket5 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream5 = Tcp_socket5.GetStream();
                                    Tcp_stream5.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer5.Enabled = true;
                        }
                        else if ((Tcp_socket6 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line6 = i;
                            file_len6 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket6 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream6 = Tcp_socket6.GetStream();
                                    Tcp_stream6.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer6.Enabled = true;
                        }
                        else if ((Tcp_socket7 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line7 = i;
                            file_len7 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket7 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream7 = Tcp_socket7.GetStream();
                                    Tcp_stream7.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer7.Enabled = true;
                        }
                        else if ((Tcp_socket8 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line8 = i;
                            file_len8 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket8 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream8 = Tcp_socket8.GetStream();
                                    Tcp_stream8.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer8.Enabled = true;
                        }
                        else if ((Tcp_socket9 == null) && (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True"))
                        {
                            socket_line9 = i;
                            file_len9 = file_size;
                            //if ((dataGVscan.Rows[i].Cells[6].Value.ToString() != textBoxVersion.Text.ToString()))
                            {
                                try
                                {
                                    Tcp_socket9 = new TcpClient(dataGVscan.Rows[i].Cells[3].Value.ToString(), Get_port);
                                    Tcp_stream9 = Tcp_socket9.GetStream();
                                    Tcp_stream9.Write(Post_head, 0, Post_head.Length);
                                }
                                catch (Exception)
                                {
                                    if (rak477getversion == false)
                                        dataGVscan.Rows[i].Cells[7].Value = "未连接上模块";
                                }
                            }
                            if (rak477getversion == false)
                                timer9.Enabled = true;
                        }
                        if (rak477getversion == false)
                            buttonupgrade.Enabled = false;
                    }
                }

            }//end for
        }
        /*********************************************************************************************************
        ** 功能说明：升级发送数据
        ********************************************************************************************************/
        private void timer_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket != null)
            {
                 try
                 {
                     if (file_len > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream.Write(data, 0, data.Length);
                   
                            Tcp_stream.Write(file_byte, file_size - file_len, 512);
                            file_len -= 512;
                            this.dataGVscan.Rows[socket_line].Cells[7].Style.BackColor = Color.LightSkyBlue;
                            this.dataGVscan.Rows[socket_line].Cells[7].Value = "升级中...  " + (int)((file_size - file_len) * 100 / file_size) + "%";                 
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream.Write(s_data, 0, s_data.Length);
                        Tcp_stream.Write(file_byte, file_size - file_len, file_len);
                        file_len -= file_len;
                        this.dataGVscan.Rows[socket_line].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line].Cells[7].Value = "升级中...  " + (int)((file_size - file_len) * 100 / file_size) + "%";
                        timer.Enabled = false;
                    }  
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line].Cells[7].Value = "模块异常";
                }
            }
        }
        private void timer1_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket1 != null)
            {
                try
                {
                    if (file_len1 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream1.Write(data, 0, data.Length);
                        Tcp_stream1.Write(file_byte, file_size - file_len1, 512);
                        file_len1 -= 512;
                        this.dataGVscan.Rows[socket_line1].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line1].Cells[7].Value = "升级中...  " + (int)((file_size - file_len1) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream1.Write(s_data, 0, s_data.Length);
                        Tcp_stream1.Write(file_byte, file_size - file_len1, file_len1);
                        file_len1 -= file_len1;
                        this.dataGVscan.Rows[socket_line1].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line1].Cells[7].Value = "升级中...  " + (int)((file_size - file_len1) * 100 / file_size) + "%";
                        timer1.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line1].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line1].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer2_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket2 != null)
            {
                try
                {
                    if (file_len2 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream2.Write(data, 0, data.Length);
                        Tcp_stream2.Write(file_byte, file_size - file_len2, 512);
                        file_len2 -= 512;
                        this.dataGVscan.Rows[socket_line2].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line2].Cells[7].Value = "升级中...  " + (int)((file_size - file_len2) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream2.Write(s_data, 0, s_data.Length);
                        Tcp_stream2.Write(file_byte, file_size - file_len2, file_len2);
                        file_len2 -= file_len2;
                        this.dataGVscan.Rows[socket_line2].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line2].Cells[7].Value = "升级中...  " + (int)((file_size - file_len2) * 100 / file_size) + "%";
                        timer2.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line2].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line2].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer3_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket3 != null)
            {
                try
                {
                    if (file_len3 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream3.Write(data, 0, data.Length);
                        Tcp_stream3.Write(file_byte, file_size - file_len3, 512);
                        file_len3 -= 512;
                        this.dataGVscan.Rows[socket_line3].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line3].Cells[7].Value = "升级中...  " + (int)((file_size - file_len3) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream3.Write(s_data, 0, s_data.Length);
                        Tcp_stream3.Write(file_byte, file_size - file_len3, file_len3);
                        file_len3 -= file_len3;
                        this.dataGVscan.Rows[socket_line3].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line3].Cells[7].Value = "升级中...  " + (int)((file_size - file_len3) * 100 / file_size) + "%";
                        timer3.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line3].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line3].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer4_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket4 != null)
            {
                try
                {
                    if (file_len4 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream4.Write(data, 0, data.Length);
                        Tcp_stream4.Write(file_byte, file_size - file_len4, 512);
                        file_len4 -= 512;
                        this.dataGVscan.Rows[socket_line4].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line4].Cells[7].Value = "升级中...  " + (int)((file_size - file_len4) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream4.Write(s_data, 0, s_data.Length);
                        Tcp_stream4.Write(file_byte, file_size - file_len4, file_len4);
                        file_len4 -= file_len4;
                        this.dataGVscan.Rows[socket_line4].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line4].Cells[7].Value = "升级中...  " + (int)((file_size - file_len4) * 100 / file_size) + "%";
                        timer4.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line4].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line4].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer5_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket5 != null)
            {
                try
                {
                    if (file_len5 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream5.Write(data, 0, data.Length);
                        Tcp_stream5.Write(file_byte, file_size - file_len5, 512);
                        file_len5 -= 512;
                        this.dataGVscan.Rows[socket_line5].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line5].Cells[7].Value = "升级中...  " + (int)((file_size - file_len5) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream5.Write(s_data, 0, s_data.Length);
                        Tcp_stream5.Write(file_byte, file_size - file_len5, file_len5);
                        file_len5 -= file_len5;
                        this.dataGVscan.Rows[socket_line5].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line5].Cells[7].Value = "升级中...  " + (int)((file_size - file_len5) * 100 / file_size) + "%";
                        timer5.Enabled = false;
                    }

                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line5].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line5].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer6_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket6 != null)
            {
                try
                {
                    if (file_len6 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream6.Write(data, 0, data.Length);
                        Tcp_stream6.Write(file_byte, file_size - file_len6, 512);
                        file_len6 -= 512;
                        this.dataGVscan.Rows[socket_line6].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line6].Cells[7].Value = "升级中...  " + (int)((file_size - file_len6) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream6.Write(s_data, 0, s_data.Length);
                        Tcp_stream6.Write(file_byte, file_size - file_len6, file_len6);
                        file_len6 -= file_len6;
                        this.dataGVscan.Rows[socket_line6].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line6].Cells[7].Value = "升级中...  " + (int)((file_size - file_len6) * 100 / file_size) + "%";
                        timer6.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line6].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line6].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer7_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket7 != null)
            {
                try
                {
                    if (file_len7 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream7.Write(data, 0, data.Length);
                        Tcp_stream7.Write(file_byte, file_size - file_len7, 512);
                        file_len7 -= 512;
                        this.dataGVscan.Rows[socket_line7].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line7].Cells[7].Value = "升级中...  " + (int)((file_size - file_len7) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream7.Write(s_data, 0, s_data.Length);
                        Tcp_stream7.Write(file_byte, file_size - file_len7, file_len7);
                        file_len7 -= file_len7;
                        this.dataGVscan.Rows[socket_line7].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line7].Cells[7].Value = "升级中...  " + (int)((file_size - file_len7) * 100 / file_size) + "%";
                        timer7.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line7].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line7].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer8_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket8 != null)
            {
                try
                {
                    if (file_len8 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream8.Write(data, 0, data.Length);
                        Tcp_stream8.Write(file_byte, file_size - file_len8, 512);
                        file_len8 -= 512;
                        this.dataGVscan.Rows[socket_line8].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line8].Cells[7].Value = "升级中...  " + (int)((file_size - file_len8) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream8.Write(s_data, 0, s_data.Length);
                        Tcp_stream8.Write(file_byte, file_size - file_len8, file_len8);
                        file_len8 -= file_len8;
                        this.dataGVscan.Rows[socket_line8].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line8].Cells[7].Value = "升级中...  " + (int)((file_size - file_len8) * 100 / file_size) + "%";
                        timer8.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line8].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line8].Cells[7].Value = "模块异常";
                }
            }
        }

        private void timer9_Upgrade(object sender, EventArgs e)
        {
            if (Tcp_socket9 != null)
            {
                try
                {
                    if (file_len9 > 512)
                    {
                        //byte[] data = new byte[512];
                        //file_bin.Read(data, 0, data.Length);
                        //Tcp_stream9.Write(data, 0, data.Length);
                        Tcp_stream9.Write(file_byte, file_size - file_len9, 512);
                        file_len9 -= 512;
                        this.dataGVscan.Rows[socket_line9].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line9].Cells[7].Value = "升级中...  " + (int)((file_size - file_len9) * 100 / file_size) + "%";
                    }
                    else
                    {
                        //byte[] s_data = new byte[file_len];
                        //file_bin.Read(s_data, 0, s_data.Length);
                        //Tcp_stream9.Write(s_data, 0, s_data.Length);
                        Tcp_stream9.Write(file_byte, file_size - file_len9, file_len9);
                        file_len9 -= file_len9;
                        this.dataGVscan.Rows[socket_line9].Cells[7].Style.BackColor = Color.LightSkyBlue;
                        this.dataGVscan.Rows[socket_line9].Cells[7].Value = "升级中...  " + (int)((file_size - file_len9) * 100 / file_size) + "%";
                        timer9.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    this.dataGVscan.Rows[socket_line9].Cells[7].Style.BackColor = Color.Red;
                    dataGVscan.Rows[socket_line9].Cells[7].Value = "模块异常";
                }
            }
        }


        void Socket_Read()
        {
            if ((Tcp_socket != null)&&(Tcp_stream!=null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line].Cells[7].Value = "模块异常";
                    }
                    
                    Tcp_stream.Close();
                    Tcp_stream = null;
                    Tcp_socket.Close();
                    Tcp_socket = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream.Close();
                                    Tcp_stream = null;
                                    Tcp_socket.Close();
                                    Tcp_socket = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream.Close();
                                    Tcp_stream = null;
                                    Tcp_socket.Close();
                                    Tcp_socket = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream.Close();
                                Tcp_stream = null;
                                Tcp_socket.Close();
                                Tcp_socket = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count = true;
                                count = 10;
                            }
                            
                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream.Close();
                            Tcp_stream = null;
                            Tcp_socket.Close();
                            Tcp_socket = null;
                        }                        
                    }));
                }
            }
            if ((Tcp_socket1 != null) && (Tcp_stream1 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream1.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line1].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line1].Cells[7].Value = "模块异常";
                    }

                    Tcp_stream1.Close();
                    Tcp_stream1 = null;
                    Tcp_socket1.Close();
                    Tcp_socket1 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream1.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream1.Close();
                                    Tcp_stream1 = null;
                                    Tcp_socket1.Close();
                                    Tcp_socket1 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line1].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream1.Close();
                                    Tcp_stream1 = null;
                                    Tcp_socket1.Close();
                                    Tcp_socket1 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line1].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line1].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream1.Close();
                                Tcp_stream1 = null;
                                Tcp_socket1.Close();
                                Tcp_socket1 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count1 = true;
                                count1 = 10;
                            }

                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line1].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line1].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream1.Close();
                            Tcp_stream1 = null;
                            Tcp_socket1.Close();
                            Tcp_socket1 = null;
                        }
                    }));
                }
            }
            if ((Tcp_socket2 != null) && (Tcp_stream2 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream2.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line2].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line2].Cells[7].Value = "模块异常";
                    }

                    Tcp_stream2.Close();
                    Tcp_stream2 = null;
                    Tcp_socket2.Close();
                    Tcp_socket2 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream2.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream2.Close();
                                    Tcp_stream2 = null;
                                    Tcp_socket2.Close();
                                    Tcp_socket2 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line2].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream2.Close();
                                    Tcp_stream2 = null;
                                    Tcp_socket2.Close();
                                    Tcp_socket2 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line2].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line2].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream2.Close();
                                Tcp_stream2 = null;
                                Tcp_socket2.Close();
                                Tcp_socket2 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count2 = true;
                                count2 = 10;
                            }

                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line2].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line2].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream2.Close();
                            Tcp_stream2 = null;
                            Tcp_socket2.Close();
                            Tcp_socket2 = null;
                        }
                    }));
                }
            }
            if ((Tcp_socket3 != null) && (Tcp_stream3 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream3.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line3].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line3].Cells[7].Value = "模块异常";
                    }

                    Tcp_stream3.Close();
                    Tcp_stream3 = null;
                    Tcp_socket3.Close();
                    Tcp_socket3 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream3.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream3.Close();
                                    Tcp_stream3 = null;
                                    Tcp_socket3.Close();
                                    Tcp_socket3 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line3].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream3.Close();
                                    Tcp_stream3 = null;
                                    Tcp_socket3.Close();
                                    Tcp_socket3 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line3].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line3].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream3.Close();
                                Tcp_stream3 = null;
                                Tcp_socket3.Close();
                                Tcp_socket3 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count3 = true;
                                count3 = 10;
                            }

                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line3].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line3].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream3.Close();
                            Tcp_stream3 = null;
                            Tcp_socket3.Close();
                            Tcp_socket3 = null;
                        }
                    }));
                }
            }
            if ((Tcp_socket4 != null) && (Tcp_stream4 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream4.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line4].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line4].Cells[7].Value = "模块异常";
                    }

                    Tcp_stream4.Close();
                    Tcp_stream4 = null;
                    Tcp_socket4.Close();
                    Tcp_socket4 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream4.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream4.Close();
                                    Tcp_stream4 = null;
                                    Tcp_socket4.Close();
                                    Tcp_socket4 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line4].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream4.Close();
                                    Tcp_stream4 = null;
                                    Tcp_socket4.Close();
                                    Tcp_socket4 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line4].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line4].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream4.Close();
                                Tcp_stream4 = null;
                                Tcp_socket4.Close();
                                Tcp_socket4 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count4 = true;
                                count4 = 10;
                            }

                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line4].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line4].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream4.Close();
                            Tcp_stream4 = null;
                            Tcp_socket4.Close();
                            Tcp_socket4 = null;
                        }
                    }));
                }
            }
            if ((Tcp_socket5 != null) && (Tcp_stream5 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream5.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line5].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line5].Cells[7].Value = "模块异常";
                    }
                    
                    Tcp_stream5.Close();
                    Tcp_stream5 = null;
                    Tcp_socket5.Close();
                    Tcp_socket5 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream5.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream5.Close();
                                    Tcp_stream5 = null;
                                    Tcp_socket5.Close();
                                    Tcp_socket5 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line5].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream5.Close();
                                    Tcp_stream5 = null;
                                    Tcp_socket5.Close();
                                    Tcp_socket5 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line5].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line5].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream5.Close();
                                Tcp_stream5 = null;
                                Tcp_socket5.Close();
                                Tcp_socket5 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count = true;
                                count = 10;
                            }
                            
                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line5].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line5].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream5.Close();
                            Tcp_stream5 = null;
                            Tcp_socket5.Close();
                            Tcp_socket5 = null;
                        }                        
                    }));
                }
            }
            if ((Tcp_socket6 != null) && (Tcp_stream6 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream6.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line6].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line6].Cells[7].Value = "模块异常";
                    }
                    
                    Tcp_stream6.Close();
                    Tcp_stream6 = null;
                    Tcp_socket6.Close();
                    Tcp_socket6 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream6.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream6.Close();
                                    Tcp_stream6 = null;
                                    Tcp_socket6.Close();
                                    Tcp_socket6 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line6].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream6.Close();
                                    Tcp_stream6 = null;
                                    Tcp_socket6.Close();
                                    Tcp_socket6 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line6].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line6].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream6.Close();
                                Tcp_stream6 = null;
                                Tcp_socket6.Close();
                                Tcp_socket6 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count6 = true;
                                count6 = 10;
                            }
                            
                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line6].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line6].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream6.Close();
                            Tcp_stream6 = null;
                            Tcp_socket6.Close();
                            Tcp_socket6 = null;
                        }                        
                    }));
                }
            }
            if ((Tcp_socket7 != null) && (Tcp_stream7 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream7.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line7].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line7].Cells[7].Value = "模块异常";
                    }
                    
                    Tcp_stream7.Close();
                    Tcp_stream7 = null;
                    Tcp_socket7.Close();
                    Tcp_socket7 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream7.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream7.Close();
                                    Tcp_stream7 = null;
                                    Tcp_socket7.Close();
                                    Tcp_socket7 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line7].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream7.Close();
                                    Tcp_stream7 = null;
                                    Tcp_socket7.Close();
                                    Tcp_socket7 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line7].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line7].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream7.Close();
                                Tcp_stream7 = null;
                                Tcp_socket7.Close();
                                Tcp_socket7 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count7 = true;
                                count7 = 10;
                            }
                            
                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line7].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line7].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream7.Close();
                            Tcp_stream7 = null;
                            Tcp_socket7.Close();
                            Tcp_socket7 = null;
                        }                        
                    }));
                }
            }
            if ((Tcp_socket8 != null) && (Tcp_stream8 != null))
            {
               byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream8.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line8].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line8].Cells[7].Value = "模块异常";
                    }
                    
                    Tcp_stream8.Close();
                    Tcp_stream8 = null;
                    Tcp_socket8.Close();
                    Tcp_socket8 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream8.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream8.Close();
                                    Tcp_stream8 = null;
                                    Tcp_socket8.Close();
                                    Tcp_socket8 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line8].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream8.Close();
                                    Tcp_stream8 = null;
                                    Tcp_socket8.Close();
                                    Tcp_socket8 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line8].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line8].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream8.Close();
                                Tcp_stream8 = null;
                                Tcp_socket8.Close();
                                Tcp_socket8 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count8 = true;
                                count8 = 10;
                            }
                            
                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line8].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line8].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream8.Close();
                            Tcp_stream8 = null;
                            Tcp_socket8.Close();
                            Tcp_socket8 = null;
                        }                        
                    }));
                }
            }
            if ((Tcp_socket9 != null) && (Tcp_stream9 != null))
            {
                byte[] buf = new byte[2000];
                Int32 bytes = 0;
                try
                {
                    bytes = Tcp_stream9.Read(buf, 0, buf.Length);
                }
                catch (Exception)
                {
                    if (rak477getversion == false)
                    {
                        this.dataGVscan.Rows[socket_line9].Cells[7].Style.BackColor = Color.Red;
                        dataGVscan.Rows[socket_line9].Cells[7].Value = "模块异常";
                    }
                    
                    Tcp_stream9.Close();
                    Tcp_stream9 = null;
                    Tcp_socket9.Close();
                    Tcp_socket9 = null;
                }
                if (bytes > 0)
                {
                    this.Invoke((EventHandler)(delegate
                    {
                        string read = System.Text.Encoding.UTF8.GetString(buf, 0, 20);
                        if (read.StartsWith("HTTP/1.1 200 OK"))
                        {
                            if (rak477getversion)
                            {
                                try
                                {
                                    bytes = Tcp_stream9.Read(buf, 0, buf.Length);
                                }
                                catch (Exception)
                                {
                                    Tcp_stream9.Close();
                                    Tcp_stream9 = null;
                                    Tcp_socket9.Close();
                                    Tcp_socket9 = null;
                                }
                                if (bytes > 0)
                                {
                                    read = "";
                                    read = System.Text.Encoding.UTF8.GetString(buf, 0, 100);
                                    string key = "module_version=";
                                    int index = read.IndexOf(key);
                                    if (index != -1)
                                    {
                                        int index2 = read.IndexOf("&", key.Length + index);
                                        if (index2 != -1)
                                        {
                                            this.dataGVscan.Rows[socket_line9].Cells[6].Value = read.Substring(key.Length + index, index2 - key.Length);
                                        }
                                    }
                                    Tcp_stream9.Close();
                                    Tcp_stream9 = null;
                                    Tcp_socket9.Close();
                                    Tcp_socket9 = null;
                                }
                            }
                            else
                            {
                                this.dataGVscan.Rows[socket_line9].Cells[7].Style.BackColor = Color.PaleGreen;
                                this.dataGVscan.Rows[socket_line9].Cells[7].Value = "升级成功，开始加载";
                                Tcp_stream9.Close();
                                Tcp_stream9 = null;
                                Tcp_socket9.Close();
                                Tcp_socket9 = null;
                                timerload.Enabled = true;
                                /*                            label4.Visible = true;
                                                            pictureBox1.Visible = true;
                                                            pictureBox2.Visible = true;
                                                            groupBox2.Visible = false;
                                */
                                Is_count9 = true;
                                count9 = 10;
                            }
                            
                        }
                        else
                        {
                            if (rak477getversion == false)
                            {
                                this.dataGVscan.Rows[socket_line9].Cells[7].Style.BackColor = Color.Red;
                                this.dataGVscan.Rows[socket_line9].Cells[7].Value = "升级失败";
                            }
                            Tcp_stream9.Close();
                            Tcp_stream9 = null;
                            Tcp_socket9.Close();
                            Tcp_socket9 = null;
                        }                        
                    }));
                }
            }
        }
        /*********************************************************************************************************
        ** 功能说明：UDP单播接收数据线程
        *********************************************************************************************************/
        void Thread_TCP_Thread()
        {
            while (true)
            {
                Socket_Read();
            }
        }

        private void dataGVscan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int select_num = 0;
            if (this.dataGVscan.CurrentRow.Cells[8].EditedFormattedValue.ToString()=="True")
            {
                this.dataGVscan.CurrentRow.Cells[8].Value = false;
            }
            else
            {
                for (int i = 0; i < this.dataGVscan.RowCount; i++)
                {
                    if (this.dataGVscan.Rows[i].Cells[8].EditedFormattedValue.ToString() == "True")
                    {
                        select_num++;
                    }                        
                }
                if (select_num <= 9)
                {
                    this.dataGVscan.CurrentRow.Cells[8].Value = true;
                }
                else 
                {
                    if (ch_en)
                        MsgBox.Show("Up to 10 modules are allowed to upgrade at the same time.");
                    else
                        MsgBox.Show("最多允许同时升级10个模块");
                }
                    
            }
        }


    }
}
