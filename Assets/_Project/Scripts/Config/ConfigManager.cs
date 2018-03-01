using LarkFramework;
using LarkFramework.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ConfigManager : SingletonMono<ConfigManager>
{
    public const string LOG_TAG = "[ConfigManager]";
    public string defaultConfigPath = "defalutConfig";

    /// <summary>
    /// 需要实例化的所有config
    /// </summary>
    /// <param name="configs"></param>
    public void Init(string[] configPaths)
    {
        ConfigRes.InitConfigs(configPaths);

        Debuger.Log(LOG_TAG + "Init Finished!");
    }

    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configPath"></param>
    /// <param name="useStreammingPath">从StreamingAssetPath中读取或从PersistentDataPath中读取</param>
    /// <returns></returns>
    public T LoadConfig<T>(string configPath,bool useStreamingAssetPath) where T:class
    {
        return ConfigRes.LoadConfig<T>(configPath,useStreamingAssetPath);
    }

    /// <summary>
    /// 保存配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configPath"></param>
    /// <param name="data">写入的数据</param>
    /// <returns></returns>
    public bool SaveConfig<T>(string configPath,string data) where T : class
    {
        return ConfigRes.SaveConfig<T>(configPath,data);
    }
}