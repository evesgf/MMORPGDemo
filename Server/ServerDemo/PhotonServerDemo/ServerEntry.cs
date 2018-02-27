using Common;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using PhotonServerDemo.Handler;
using PhotonServerDemo.Threads;
using System.Collections.Generic;
using System.IO;

namespace PhotonServerDemo
{
    /// <summary>
    /// 所有的Server端的入口类都集成自ApplicationBase
    /// </summary>
    public class ServerEntry : ApplicationBase
    {
        ////单例
        ////TODO:ApplicationBase有Instance成员，暂不明是否是单例，待查
        //public static ServerEntry Instance
        //{
        //    get;
        //    private set;
        //}

        //日志
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        //处理句柄
        public static Dictionary<OperationCode, HandlerBase> dict_Handler = new Dictionary<OperationCode, HandlerBase>();

        //已链接的客户池
        public static List<ClientPeer> peerList = new List<ClientPeer>();

        private SyncPositionThread syncPositionThread = new SyncPositionThread();

        /// <summary>
        /// 当客户端请求链接
        /// peerBase为一个客户端连接
        /// </summary>
        /// <param name="initRequest"></param>
        /// <returns></returns>
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("[Peer]Link Start!");
            ClientPeer peer= new Client(initRequest);
            peerList.Add(peer);
            return peer;
        }

        /// <summary>
        /// 服务器启动
        /// </summary>
        protected override void Setup()
        {
            #region 日志初始化
            //设置日志所在路径
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath,"log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance); //让Photon识别日志插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);  //读取配置文件
            }
            log.Info("[Log]Init Completed!");
            #endregion

            InitHnadler();

            syncPositionThread.Run();

            log.Info("[Server]Setup Completed!");
        }

        /// <summary>
        /// 服务器关闭
        /// </summary>
        protected override void TearDown()
        {
            syncPositionThread.Stop();

            log.Info("[Server]TearDown Completed!");
        }

        /// <summary>
        /// 初始化处理句柄
        /// </summary>
        public void InitHnadler()
        {
            LoginHandler loginHandler = new LoginHandler();
            dict_Handler.Add(loginHandler.opCode, loginHandler);

            DefaultHandler defaultHandler = new DefaultHandler();
            dict_Handler.Add(defaultHandler.opCode, defaultHandler);

            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            dict_Handler.Add(syncPositionHandler.opCode, syncPositionHandler);

            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            dict_Handler.Add(syncPlayerHandler.opCode, syncPlayerHandler);

            DamageHandler damageHandler = new DamageHandler();
            dict_Handler.Add(damageHandler.opCode, damageHandler);

            SkillHandler skillHandler = new SkillHandler();
            dict_Handler.Add(skillHandler.opCode, skillHandler);
        }
    }
}
