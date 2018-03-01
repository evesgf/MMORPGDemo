using LarkFramework.UI;
using LarkFramework.Tick;
using LarkFramework.Net;

namespace LarkFramework.GameEntry
{
    public partial class GameEntry
    {
        private void InitCustomComponents()
        {
            UIManager.Instance.MainPage = UIDef.HomePage;

            //Config
            ConfigManager.Create().Init(new string[] { ConfigDef.ConfigData});

            //var a = ConfigManager.Instance.LoadConfig<ConfigData>(ConfigDef.ConfigData,false);

            PhotonManager.Create().Init();
        }
    }
}
