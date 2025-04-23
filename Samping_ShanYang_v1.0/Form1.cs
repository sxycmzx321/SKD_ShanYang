using MySql.Data.MySqlClient;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samping_ShanYang_v1._0
{
    public partial class Form1 : Form
    {
        #region
        // 启动连续采样
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_StartSampling", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_StartSampling(int mci, int tmode);
        // 启动N点采样
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_StartSampling_NPoints", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_StartSampling_NPoints(int mci, int tmode, int Npointsnums);
        // 停止采样
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_StopSampling", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_StopSampling(int mci);
        // 切换系统模式
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_SystemMode", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_SystemMode(int mci, int sysmode);
        // 设置PWM/DAC/IO/计数器/温度通道
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_AdditionalFeature", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_AdditionalFeature(int mci, UInt32 funcNo, UInt32 para1, double para2);
        // 切换信号发生器模式
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_SignalGeneratorParameter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_SignalGeneratorParameter(int mci, UInt32 wmode, UInt32 wfreq, double voltageval);
        // 设置默认参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_DeviceDefaultParameter_SpecialUse", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_DeviceDefaultParameter_SpecialUse(int mci, char[] wrmodel, char[] wrsn, int[] para);
        // 设置网络参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_eNetParameter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_eNetParameter(int mci, byte[] para, int len);
        // 读取当前系统模式
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_SystemMode", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_SystemMode(int mci, ref int sysmode, ref int samplecmd, ref int sdfilefmt, int timeout);
        // 读取默认参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_DeviceDefaultParameter_SpecialUse", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_DeviceDefaultParameter_SpecialUse(int mci, char[] rdmodel, char[] rdswver, char[] rdhdver, char[] rdsn, int[] para, int timeout);
        // 读取网络参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_eNetParameter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_eNetParameter(int mci, byte[] tvalue, int timeout);
        // 获取DLL函数的版本信息
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetVersionLot", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern char[] VK7015NH_GetVersionLot();
        // 读取采集卡的采样状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_ADCSamplingParameter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_ADCSamplingParameter(int mci, ref int tsamplingmode, ref int npoints, int timeout);
        // 读取单通道数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetOneChannel", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetOneChannel(int mci, int CHNum, double[] adcbuffer, int rsamplenum);
        // 读取4通道采样数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFourChannel", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFourChannel(int mci, double[] adcbuffer, int rsamplenum);
        // 读取8通道采样数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetAllChannel", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetAllChannel(int mci, double[] adcbuffer, int rsamplenum);
        // 读取16通道采样数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetEntire16Channels", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetEntire16Channels(int mci, double[] adcbuffer, int rsamplenum);
        // 读取CH1~CH8采样数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8ChannelsForm1to8", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8ChannelsForm1to8(int mci, double[] adcbuffer, int rsamplenum);
        // 读取CH9~CH16采样数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8ChannelsForm9to16", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8ChannelsForm9to16(int mci, double[] adcbuffer, int rsamplenum);
        // 读取单通道数据（带IO状态）
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetOneChannel_WithIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetOneChannel_WithIOStatus(int mci, int CHNum, double[] adcbuffer, int rsamplenum, int ioenable);
        // 读取4通道数据（带IO状态）
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFourChannel_WithIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFourChannel_WithIOStatus(int mci, double[] adcbuffer, int rsamplenum, int ioenable);
        // 读取CH1~CH8数据（带IO状态）
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8ChannelsForm1to8_WithIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8ChannelsForm1to8_WithIOStatus(int mci, double[] adcbuffer, int rsamplenum, int ioenable);
        // 读取CH9~CH16数据（带IO状态）
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8ChannelsForm9to16_WithIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8ChannelsForm9to16_WithIOStatus(int mci, double[] adcbuffer, int rsamplenum, int ioenable);
        // 读取CH9~CH16的采样数据和IO状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get8to16Channel_WithIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get8to16Channel_WithIOStatus(int mci, double[] adcbuffer, int rsamplenum, int ioenable);
        // 读取16通道的采样数据和IO状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetEntire16Channels_WithIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetEntire16Channels_WithIOStatus(int mci, double[] adcbuffer, int rsamplenum, int ioenable);
        // 读取CH1/CH2/CH3/CH4的采样数据和IO1/IO2/IO3/IO4的同步数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8Channel_CH1toCH4_WithIO1toIO4", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8Channel_CH1toCH4_WithIO1toIO4(int mci, double[] adcbuffer, int rsamplenum);
        // 读取CH5/CH6/CH7/CH8的采样数据和IO1/IO2/IO3/IO4的同步数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8Channel_CH5toCH8_WithIO1toIO4", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8Channel_CH5toCH8_WithIO1toIO4(int mci, double[] adcbuffer, int rsamplenum);
        // 读取CH9/CH10/CH11/CH12的采样数据和IO1/IO2/IO3/IO4的同步数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8Channel_CH9toCH12_WithIO1toIO4", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8Channel_CH9toCH12_WithIO1toIO4(int mci, double[] adcbuffer, int rsamplenum);
        // 读取CH13/CH14/CH15/CH16的采样数据和IO1/IO2/IO3/IO4的同步数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetFixed8Channel_CH13toCH16_WithIO1toIO4", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetFixed8Channel_CH13toCH16_WithIO1toIO4(int mci, double[] adcbuffer, int rsamplenum);
        // 读取 IO2 和 IO3 状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_GetIOStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_GetIOStatus(int mci, int[] iostatus);
        // 读取所有 IO 状态函数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_AllIOS", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_AllIOS(int mci, ref UInt32 iobuffer, int timeout);
        // 读取 IO1 状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_IO1", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_IO1(int mci, ref UInt32 iovalue, int timeout);
        // 读取 IO2 状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_IO2", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_IO2(int mci, ref UInt32 iovalue, int timeout);
        // 读取 IO3 状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_IO3", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_IO3(int mci, ref UInt32 iovalue, int timeout);
        // 读取 IO4 状态
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_IO4", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_IO4(int mci, ref UInt32 iovalue, int timeout);
        // 读取 PWM 参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_PWM", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_PWM(int mci, ref double dutyval, ref UInt32 freqval, int timeout);
        // 读取计数器参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_Counter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_Counter(int mci, ref UInt32 countervalue, int timeout);
        // 读取温度参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_Temperature", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_Temperature(int mci, ref double tempvalue, int timeout);
        // 读取频率
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_exFreq", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_exFreq(int mci, ref UInt32 freqvalue, int timeout);
        // 读取外部温湿度值（保留功能）
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_ExTemperatureHumidity", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_ExTemperatureHumidity(int mci, ref double tempvalue, ref double humidity, int timeout);
        // 打开服务器
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_TCPOpen", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_TCPOpen(int portnumber);
        // 关闭服务器
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_TCPClose", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_TCPClose(int portnumber);
        // 读取已打开服务器的端口号
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_Bind_ConnectedClientIP", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_Bind_ConnectedClientIP(int tflag);
        // 读取已打开服务器的端口号
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_Get_ServerPort", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_Get_ServerPort(ref int iport);
        // 读取已连接服务器的采集卡数量
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_Get_ConnectedClientNumbers", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_Get_ConnectedClientNumbers(ref int cnum);
        // 读取当前采集卡的句柄和IP地址
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_Get_ConnectedClientHandle", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_Get_ConnectedClientHandle(int mci, ref int ihadble, byte[] ipadr);
        // 读取当前服务器端接收的字节总数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "Server_Get_RxTotoalBytes", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int Server_Get_RxTotoalBytes(ref int totalbytesnum, int clrflag);
        // 设置文件下载的默认存储路径
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_SaveFileDeafultPath", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_SaveFileDeafultPath(int tflag, byte[] defaultdir);
        // 读取文件目录
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_SDFileRuningInformation", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_SDFileRuningInformation(int mci, ref int para);
        // 进入SD卡文件下载模式
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Enter_SDFileSystem", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Enter_SDFileSystem(int mci);
        // 退出SD卡文件下载模式
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Exit_SDFileSystem", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Exit_SDFileSystem(int mci);
        // 读取文件目录
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_SDFileDIR", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_SDFileDIR(int mci);
        // 读取一个采样记录文件
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_OneSDFile", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_OneSDFile(int mci, int fileindex);
        // 读取所有采样文件
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_MultiSDFiles", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_MultiSDFiles(int mci);
        // 删除一个采样记录文件
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Delete_OneSDFile", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Delete_OneSDFile(int mci, int fileindex);
        // 删除所有采样记录文件
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Delete_MultiSDFiles", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Delete_MultiSDFiles(int mci);
        // 初始化触发器参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_SimulationTriggerMode", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_SimulationTriggerMode(int mci, int status, int trigch, int trigedge, int rdnpoints, int rdnegnpoints, double trigval);
        // 获取触发数据
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_SelectChannelsFromSimulationTrigger", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_SelectChannelsFromSimulationTrigger(int mci, int readdchnum, double[] adcbuffer, int rsamplenum);
        // 获取DAQ卡所有通道的默认ADC校准参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_AllChannels_DefaultADCCalibrationParameter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_AllChannels_DefaultADCCalibrationParameter(int mci, int volrange, ref int para, int timeout);
        // 设置DAQ卡的偏置电压和增益偏移参数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_AllChannels_DefaultADCCalibrationParameter", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_AllChannels_DefaultADCCalibrationParameter(int mci, int volrange, ref int para);
        // 设置DAQ卡单通道的偏置电压
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_OneChannel_DefaultOffsetValue", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_OneChannel_DefaultOffsetValue(int mci, int chno, int volrange, int para);
        // 初始化连接的采集卡参数，专用VK7015
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Initialize_SpecialUse", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Initialize_SpecialUse(int mci, int sr, int npoints, int timeintervals, int bitmode, int[] para);
        // 读取采集卡的采样参数，VK7015专用
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_ADCConfigParameter_SpecialUse", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_ADCConfigParameter(int mci, ref int sr, ref int npoints, ref int timeintervals, ref int bitmode, int[] para);
        // 设置任意波形函数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_SignalGeneratorAnyWave", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_SignalGeneratorAnyWave(int mci, int wfreq, ref int datapi, int len);
        // 获取任意波形函数
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Get_TxAnyWaveStatus", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Get_TxAnyWaveStatus(int mci, int resetflag, ref int status, ref int index);
        // 重置DAQ
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_ResetDevice", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_ResetDevice(int mci);
        // 设置读取方式
        [DllImport("VK7015NH_DAQ.dll", EntryPoint = "VK7015NH_Set_BlockingMethodtoReadADCResult", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern int VK7015NH_Set_BlockingMethodtoReadADCResult(int tmode, int timeout);
        // 导入 SetDllDirectory 函数
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        #endregion
        #region 参数定义
        public static extern bool SetDllDirectory(string lpPathName);
        int samplingNpointCount = 0;                     // 样本计数器
        int samplingNpointNum = 1000;               // N点采样数量
        int totalNumofNpointSamplingTimes = 5;      // N点采样总次数
        int samplingFrequency;              // 采样频率
        int samplingResolution = 2;                 // 采样分辨率         
        int portNum = 9002;                         // 默认端口号
        int curDeviceNum = 0;                       // 当前设备数量
        int deviceNo = 0;                           // 单个DAQ的默认值为0
        int curHandle = 0;                          // 当前设备的句柄
        int NumofADCChannel = 16;                   // ADC通道数量
        string ipAdrStr;                            // IP地址字符串
        byte[] ipAdr = new byte[128];               // 用于存储IP地址
        double[] revResult = new double[512000];    // 最大采样频率点数 x (16个ADC通道)
        int[] initPara = new int[256];
        int getPoints = 0;
        int loopTimes = 0;
        int recvLen = 0;
        int result = -1;
        int i;
        private NotifyIcon notifyIcon;
        MySQLSet mysql;
        private System.Timers.Timer gather = new System.Timers.Timer();
        private bool stopSampling = false;
        private MySqlConnection connection;
        private PlotModel plotModel;
        private PlotModel plotModel2;
        private KalmanFilter kalmanFilter = new KalmanFilter();  // 创建卡尔曼滤波器实例
        #endregion
        // 构造函数
        public Form1()
        {
            InitializeComponent();
            InitializePlotView();
            InitializePlotView2(); // 初始化第二个 plotView


            // 设置 DLL 文件路径，确保 DLL 能被正确加载
            SetDllDirectory(@"E:\ShanYang\Samping_ShanYang_v1.0\Samping_ShanYang_v1.0\bin\Debug");
        }
        public class KalmanFilter
        {
            private double _q = 0.0001; // 过程噪声
            private double _r = 0.1;    // 测量噪声
            private double _x = 0;      // 初始值
            private double _p = 1;      // 初始误差协方差
            private double _k = 0;      // 卡尔曼增益

            public double Update(double measurement)
            {
                // 预测更新
                _p = _p + _q;

                // 测量更新
                _k = _p / (_p + _r);
                _x = _x + _k * (measurement - _x);
                _p = (1 - _k) * _p;

                return _x;
            }
        }
        // 更新 Plot 视图，将滤波后的数据放入第二个图
        private void PlotDenoisedAccelerationCurve(double[] data, int channel)
        {
            double[] filteredData = ApplyKalmanFilter(data);  // 应用卡尔曼滤波

            // 移除已有的曲线（按通道号区分）
            var existingSeries = plotModel2.Series.FirstOrDefault(s => s.Title == $"通道 {channel}");
            if (existingSeries != null)
            {
                plotModel2.Series.Remove(existingSeries);
            }

            // 创建新的折线图
            var lineSeries = new LineSeries
            {
                Title = $"通道 {channel}",
                StrokeThickness = 2,
                MarkerSize = 0,
                MarkerType = MarkerType.None
            };

            // 添加滤波后的数据点到折线图
            for (int i = 0; i < filteredData.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, filteredData[i]));
            }

            // 添加新的曲线到 plotView2
            plotModel2.Series.Add(lineSeries);

            // 刷新图表
            plotModel2.InvalidatePlot(true);
        }
        // 对信号数据应用卡尔曼滤波
        private double[] ApplyKalmanFilter(double[] data)
        {
            double[] filteredData = new double[data.Length];

            // 对每个数据点进行卡尔曼滤波处理
            for (int i = 0; i < data.Length; i++)
            {
                filteredData[i] = kalmanFilter.Update(data[i]);
            }

            return filteredData;
        }
        private void InitializePlotView()
        {
            plotModel = new PlotModel
            {
                Title = "加速度曲线",
                Subtitle = "通道数据实时可视化"
            };

            // 添加 X 轴（时间）
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "时间 (ms)",
                Minimum = 0,
                IsPanEnabled = true,
                IsZoomEnabled = true
            });

            // 添加 Y 轴（加速度）
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "加速度 (m/s²)",
                IsPanEnabled = true,
                IsZoomEnabled = true
            });

            // 将 plotModel 绑定到 plotView1
            plotView1.Model = plotModel;
        }
        private void InitializePlotView2()
        {
            plotModel2 = new PlotModel
            {
                Title = "去噪后的加速度曲线",
                Subtitle = "卡尔曼去噪后的信号"
            };

            // 添加 X 轴（时间）
            plotModel2.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "时间 (ms)",
                Minimum = 0,
                IsPanEnabled = true,
                IsZoomEnabled = true
            });

            // 添加 Y 轴（加速度）
            plotModel2.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "加速度 (m/s²)",
                IsPanEnabled = true,
                IsZoomEnabled = true
            });

            // 将 plotModel2 绑定到 plotView2
            plotView2.Model = plotModel2;
        }
        // 绘制加速度曲线
        // 绘制加速度曲线
        private void PlotAccelerationCurve(double[] data, int channel)
        {
            // 移除已有的曲线（按通道号区分）
            var existingSeries = plotModel.Series.FirstOrDefault(s => s.Title == $"通道 {channel}");
            if (existingSeries != null)
            {
                plotModel.Series.Remove(existingSeries);
            }

            // 创建新的折线图
            var lineSeries = new LineSeries
            {
                Title = $"通道 {channel}",
                StrokeThickness = 2,
                MarkerSize = 0,
                MarkerType = MarkerType.None
            };

            // 添加数据点到折线图
            for (int i = 0; i < data.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, data[i]));
            }

            // 添加新的曲线到图表
            plotModel.Series.Add(lineSeries);

            // 刷新图表
            plotModel.InvalidatePlot(true);

            // 将滤波后的数据绘制到第二个图表
            PlotDenoisedAccelerationCurve(data, channel); // 传递数据到 plotView2
        }


        public bool WriteData(int port, double[][] channels, DateTime startDate)
        {
            string insertQuery;

            // 根据端口号选择表
            if (port == 9001)
            {
                insertQuery = "INSERT INTO `skd_sydb`.`maincurvedata`(`StartDate`, `CH1`, `CH2`, `CH3`, `CH4`, `CH5`, `CH6`, `CH7`, `CH8`, " +
                              "`CH9`, `CH10`, `CH11`, `CH12`, `CH13`, `CH14`, `CH15`, `CH16`) " +
                              "VALUES(@StartDate, @CH1, @CH2, @CH3, @CH4, @CH5, @CH6, @CH7, @CH8, " +
                              "@CH9, @CH10, @CH11, @CH12, @CH13, @CH14, @CH15, @CH16)";
            }
            else if (port == 9002)
            {
                insertQuery = "INSERT INTO `skd_sydb`.`maincurvedata2`(`StartDate`, `CH1`, `CH2`, `CH3`, `CH4`, `CH5`, `CH6`, `CH7`, `CH8`, " +
                              "`CH9`, `CH10`, `CH11`, `CH12`, `CH13`, `CH14`, `CH15`, `CH16`) " +
                              "VALUES(@StartDate, @CH1, @CH2, @CH3, @CH4, @CH5, @CH6, @CH7, @CH8, " +
                              "@CH9, @CH10, @CH11, @CH12, @CH13, @CH14, @CH15, @CH16)";
            }
            else
            {
                AddToListBox("无效的端口号。");
                return false;
            }

            try
            {
                // 确保连接已正确初始化并打开
                if (connection == null)
                {
                    AddToListBox("数据库连接未初始化。正在重新初始化...");
                    string connectionString = "server=127.0.0.1;port=3306;user=root;password=7418529630;database=zsh;SslMode=none;";
                    connection = new MySqlConnection(connectionString);
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    AddToListBox("重新打开数据库连接成功。");
                }

                // 准备数据
                string[] channelData = new string[16];
                for (int i = 0; i < 16; i++)
                {
                    channelData[i] = channels.Length > i && channels[i] != null && channels[i].Length > 0
                        ? string.Join(",", channels[i].Select(value => value == 0 ? "0" : value.ToString()))
                        : "0"; // 默认值为 "0"
                }

                // 插入数据
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate); // 时间戳
                    for (int i = 0; i < 16; i++)
                    {
                        command.Parameters.AddWithValue($"@CH{i + 1}", channelData[i]);
                    }

                    AddToListBox("执行 SQL 查询...");
                    command.ExecuteNonQuery();
                    AddToListBox("数据写入成功。");
                    return true;
                }
            }
            catch (NullReferenceException ex)
            {
                AddToListBox($"NullReferenceException: {ex.Message}");
                AddToListBox("StackTrace: " + ex.StackTrace);
                return false;
            }
            catch (Exception ex)
            {
                AddToListBox($"WriteData 错误: {ex.Message}");
                AddToListBox("StackTrace: " + ex.StackTrace);
                return false;
            }
        }


        private async void btnStartSampling_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取用户输入的参数
                string port = txtPort.Text;
                string samplingRate = txtSamplingRate.Text;

                // 获取用户输入的时间和单位
                string timeInput = txtTimeInput.Text;  // 用户输入的时间
                string timeUnit = cmbTimeUnit.SelectedItem.ToString();  // 选择的时间单位（分钟、秒、小时）

                // 确保用户输入的是有效的数字
                if (!int.TryParse(timeInput, out int timeValue) || timeValue <= 0)
                {
                    AddToListBox("无效的时间输入，请重新输入。");
                    return;
                }

                // 根据选择的时间单位转换为秒
                int totalDelayInSeconds = 0;
                if (timeUnit == "分钟")
                {
                    totalDelayInSeconds = timeValue * 60;  // 转换为秒
                }
                else if (timeUnit == "秒")
                {
                    totalDelayInSeconds = timeValue;  // 直接使用秒数
                }
                else if (timeUnit == "小时")
                {
                    totalDelayInSeconds = timeValue * 3600;  // 转换为秒
                }
                else
                {
                    AddToListBox("请选择正确的时间单位。");
                    return;
                }

                AddToListBox($"设置的延时：{timeValue} {timeUnit}，共 {totalDelayInSeconds} 秒");

                // 验证端口号和采样率
                if (!int.TryParse(port, out int portNum) || portNum <= 0)
                {
                    AddToListBox("无效的端口号，请重新输入。");
                    return;
                }

                if (!int.TryParse(samplingRate, out int samplingFrequency) || samplingFrequency <= 0)
                {
                    AddToListBox("无效的采样率，请重新输入。");
                    return;
                }

                // 打开TCP服务器端口
                AddToListBox("尝试打开TCP服务器端口...");
                result = await Task.Run(() => Server_TCPOpen(portNum));

                if (result < 0)
                {
                    AddToListBox("打开TCP服务器端口失败。");
                    return;
                }
                AddToListBox("TCP服务器端口打开成功。");

                // 查找连接的DAQ设备
                AddToListBox("正在查找DAQ设备...");
                result = -1;
                int loopTimes = 0;

                while (result < 0)
                {
                    loopTimes++;
                    if (loopTimes > 500) // 超时退出，大约10秒
                    {
                        AddToListBox("查找DAQ设备超时，退出。");
                        return;
                    }

                    result = await Task.Run(() => Server_Get_ConnectedClientNumbers(ref curDeviceNum));
                    await Task.Delay(20);
                }

                AddToListBox($"查找DAQ设备成功，已连接的采集卡数量：{curDeviceNum}");

                // 获取当前DAQ的句柄和IP地址
                Array.Clear(ipAdr, 0, 128);
                AddToListBox("获取DAQ的句柄和IP地址...");

                result = await Task.Run(() => Server_Get_ConnectedClientHandle(deviceNo, ref curHandle, ipAdr));

                if (result < 0)
                {
                    AddToListBox("获取DAQ的句柄和IP地址失败。");
                    return;
                }

                string ipAdrStr = Encoding.ASCII.GetString(ipAdr, 0, Array.FindIndex(ipAdr, value => value == 0));
                AddToListBox($"DAQ设备IP地址：{ipAdrStr}");
                AddToListBox($"DAQ句柄：{curHandle:D2}");

                // 设置系统模式
                result = await Task.Run(() => VK7015NH_Set_SystemMode(deviceNo, 0));
                if (result < 0)
                {
                    AddToListBox("设置系统模式失败。");
                    return;
                }
                AddToListBox("系统模式设置成功。");

                // 初始化参数
                AddToListBox("初始化DAQ参数...");
                for (int i = 0; i < 256; i++)
                {
                    initPara[i] = 0;
                }

                result = await Task.Run(() => VK7015NH_Initialize_SpecialUse(deviceNo, samplingFrequency, samplingNpointNum, 60, samplingResolution, initPara));

                if (result < 0)
                {
                    AddToListBox("初始化DAQ参数失败。");
                    return;
                }
                AddToListBox("初始化DAQ参数成功。");

                // 设置阻塞读取数据
                result = await Task.Run(() => VK7015NH_Set_BlockingMethodtoReadADCResult(1, 1000));
                if (result < 0)
                {
                    AddToListBox("设置阻塞读取数据失败。");
                    return;
                }
                AddToListBox("阻塞读取数据设置成功。");

                // 启动采样
                AddToListBox("启动连续采样...");
                result = await Task.Run(() => VK7015NH_StartSampling(deviceNo, 1));

                if (result < 0)
                {
                    AddToListBox("启动连续采样失败。");
                    return;
                }
                AddToListBox("连续采样启动成功。");

                // 清空之前的 ListBox 内容
                if (ListBOXData.InvokeRequired)
                {
                    ListBOXData.Invoke(new Action(() =>
                    {
                        ListBOXData.Items.Clear();
                        ListBOXData.Items.Add("连续采样启动成功。"); // 重新添加该行
                    }));
                }
                else
                {
                    ListBOXData.Items.Clear();
                    ListBOXData.Items.Add("连续采样启动成功。"); // 重新添加该行
                }

                // 数据采样与保存
                int getPoints = (samplingFrequency > 10) ? samplingFrequency / 10 : 1;
                double[][] channels = new double[16][];

                for (int i = 0; i < 16; i++)
                {
                    channels[i] = new double[getPoints];
                }

                while (result >= 0)
                {
                    recvLen = await Task.Run(() => VK7015NH_GetEntire16Channels(deviceNo, revResult, getPoints));

                    if (recvLen > 0)
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            for (int j = 0; j < getPoints; j++)
                            {
                                channels[i][j] = revResult[i * getPoints + j] * 100;
                            }
                        }

                        PlotAccelerationCurve(channels[0], 1);
                        PlotDenoisedAccelerationCurve(channels[0], 1);  // 这里是通道1的滤波数据

                        string[] channelData = new string[16];
                        for (int i = 0; i < 16; i++)
                        {
                            channelData[i] = string.Join(",", channels[i]);
                        }

                        DateTime startDate = DateTime.Now;

                        bool saveResult = await Task.Run(() => WriteData(portNum, channels, startDate));

                        if (saveResult)
                        {
                            AddToListBox("数据成功保存到数据库。");
                        }
                        else
                        {
                            AddToListBox("保存数据失败，数据将保存到本地日志文件。");
                            SaveChannelDataToFile(channelData, startDate);
                        }

                        for (int i = 0; i < 16; i++)
                        {
                            AddToListBox($"CH{i + 1} 数据点: {channelData[i]}");
                        }

                        // 使用用户输入的延时时间进行采样间隔
                        await Task.Delay(TimeSpan.FromSeconds(totalDelayInSeconds));  // 延迟总时间
                    }
                }
            }
            catch (Exception ex)
            {
                AddToListBox($"错误：{ex.Message}");
            }
            finally
            {
                VK7015NH_StopSampling(deviceNo);
                Server_TCPClose(portNum);
                AddToListBox("操作结束。");
            }
        }


        // 保存通道数据到本地文本文件
        private void SaveChannelDataToFile(string[] channelData, DateTime startDate)
        {
            try
            {
                // 文件名，包含时间戳
                string fileName = $"ChannelData_{startDate:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

                // 构造文件内容
                StringBuilder fileContent = new StringBuilder();
                fileContent.AppendLine($"采样时间: {startDate:yyyy-MM-dd HH:mm:ss}");
                for (int i = 0; i < channelData.Length; i++)
                {
                    fileContent.AppendLine($"CH{i + 1} 数据点: {channelData[i]}");
                }

                // 将内容写入文件
                File.WriteAllText(filePath, fileContent.ToString(), Encoding.UTF8);
                AddToListBox($"通道数据保存到文件: {filePath}");
            }
            catch (Exception ex)
            {
                AddToListBox($"保存数据失败: {ex.Message}");
            }
        }

        // 将文本添加到 ListBox 控件
        private void AddToListBox(string message)
        {
            if (ListBOXData.InvokeRequired)
            {
                // 如果跨线程更新 ListBox，使用 Invoke 方法
                ListBOXData.Invoke(new Action(() =>
                {
                    ListBOXData.Items.Add(message);
                    ListBOXData.SelectedIndex = ListBOXData.Items.Count - 1; // 滚动到最新数据
                }));
            }
            else
            {
                ListBOXData.Items.Add(message);
                ListBOXData.SelectedIndex = ListBOXData.Items.Count - 1;
            }
        }

        // 按下停止采样按钮时的事件处理
        private void btnStopSampling_Click(object sender, EventArgs e)
        {
            stopSampling = true;  // 设置停止标志为 true，终止循环
            AddToListBox("停止采样...");

            // 停止采样
            VK7015NH_StopSampling(deviceNo);
            Server_TCPClose(portNum);
            AddToListBox("停止采样成功");

            // 重新启用开始采样按钮
            btnStartSampling.Enabled = true;
            btnStopSampling.Enabled = false;
        }
    }

}

