using LarkFramework.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ConfigRes
{
    public static string defaultConfigRes = "/Config/defaultConfig.json";


    /// <summary>
    /// 初始化配置文件
    /// 如果没找到Config文件，就把默认文件拷贝到指定目录下
    /// </summary>
    public static void InitDefalutConfig()
    {
        //检测配置文件是否存在
        if (File.Exists(FilePathUtils.PersistentDataPath() + defaultConfigRes)) return;

        if (!File.Exists(FilePathUtils.StreamingAssetPath() + defaultConfigRes))
        {
            Debuger.LogError(FilePathUtils.StreamingAssetPath() + defaultConfigRes + " File is Not Exist!");
        }

        //创建目录
        Directory.CreateDirectory(FilePathUtils.PersistentDataPath() + "/Config");
        //复制文件
        File.Copy(FilePathUtils.StreamingAssetPath() + defaultConfigRes, FilePathUtils.PersistentDataPath() + defaultConfigRes, false);

        Debuger.Log(defaultConfigRes + " Init Copy!");
    }

    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T LoadConfig<T>() where T:class
    {
        Debug.Log("[ConfigRes] Load Config:" + defaultConfigRes);

        if (!File.Exists(FilePathUtils.StreamingAssetPath() + defaultConfigRes))
        {
            return null;
        }

        //读取
        StreamReader sr = new StreamReader(FilePathUtils.StreamingAssetPath() + defaultConfigRes);

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
    public static T SaveConfig<T>() where T : class
    {
        Debug.Log("[ConfigRes] Save Config:" + defaultConfigRes);

        if (!File.Exists(FilePathUtils.StreamingAssetPath() + defaultConfigRes))
        {
            return null;
        }

        //读取
        StreamReader sr = new StreamReader(FilePathUtils.StreamingAssetPath() + defaultConfigRes);

        if (sr == null) return null;

        string json = sr.ReadToEnd();

        if (json.Length <= 0) return null;

        return LitJson.JsonMapper.ToObject<T>(json);
    }
}
