using LarkFramework.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ConfigRes
{

    /// <summary>
    /// 初始化配置文件
    /// 如果没找到Config文件，就把默认文件拷贝到指定目录下
    /// </summary>
    public static void InitConfigs(string[] configPaths)
    {
        foreach (var path in configPaths)
        {
            CheckConfig(path);
        }
    }

    private static void CheckConfig(string configPath)
    {
        //检测配置文件是否存在
        if (File.Exists(FilePathUtils.PersistentDataPath() + configPath)) return;

        if (!File.Exists(FilePathUtils.StreamingAssetPath() + configPath))
        {
            Debuger.LogError(FilePathUtils.StreamingAssetPath() + configPath + " File is Not Exist!");
        }

        //创建目录
        Directory.CreateDirectory(FilePathUtils.PersistentDataPath() + "/Config");
        //复制文件
        File.Copy(FilePathUtils.StreamingAssetPath() + configPath, FilePathUtils.PersistentDataPath() + configPath, false);

        Debuger.Log(configPath + " Init Copy!");
    }


    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T LoadConfig<T>(string configPath) where T:class
    {
        Debug.Log("[ConfigRes] Load Config:" + configPath);

        if (!File.Exists(FilePathUtils.StreamingAssetPath() + configPath))
        {
            return null;
        }

        //读取
        StreamReader sr = new StreamReader(FilePathUtils.StreamingAssetPath() + configPath);

        if (sr == null) return null;

        string json = sr.ReadToEnd();

        if (json.Length <= 0) return null;

        return LitJson.JsonMapper.ToObject<T>(json);
    }

    /// <summary>
    /// 写入配置文件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T SaveConfig<T>(string configPath) where T : class
    {
        Debug.Log("[ConfigRes] Save Config:" + configPath);

        if (!File.Exists(FilePathUtils.StreamingAssetPath() + configPath))
        {
            return null;
        }

        //读取
        StreamReader sr = new StreamReader(FilePathUtils.StreamingAssetPath() + configPath);

        if (sr == null) return null;

        string json = sr.ReadToEnd();

        if (json.Length <= 0) return null;

        return LitJson.JsonMapper.ToObject<T>(json);
    }
}
