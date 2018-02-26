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
            TickManager.Instance.Init();

            //Config
            ConfigManager.Create().Init();

            var a = ConfigManager.Instance.LoadConfig<ConfigData>();

            PhotonManager.Create().Init();
        }
    }
}
