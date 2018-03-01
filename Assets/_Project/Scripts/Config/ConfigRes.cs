using LarkFramework.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    /// <summary>
    /// 检查PersistentDataPath中是否存在配置文件
    /// </summary>
    /// <param name="configPath"></param>
    private static void CheckConfig(string configPath)
    {
        //检测配置文件是否存在
        if (File.Exists(FilePathUtils.PersistentDataPath() + configPath))
        {
            //比较修改时间
            var s_Time = new FileInfo(FilePathUtils.StreamingAssetPath() + configPath).LastWriteTime;
            var p_Time = new FileInfo(FilePathUtils.PersistentDataPath() + configPath).LastWriteTime;

            if (s_Time > p_Time)
            {
                //复制文件
                File.Copy(FilePathUtils.StreamingAssetPath() + configPath, FilePathUtils.PersistentDataPath() + configPath, true);
                Debuger.Log(configPath + " Init Copy!");
            }
        }
        else
        {
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
    }


    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="useStreamingAssetPath">从StreamingAssetPath中读取或从PersistentDataPath中读取</param>
    /// <returns></returns>
    public static T LoadConfig<T>(string configPath, bool useStreamingAssetPath) where T : class
    {
        string url = useStreamingAssetPath ? FilePathUtils.StreamingAssetPath() + configPath : FilePathUtils.PersistentDataPath() + configPath;

        //读取
        StreamReader sr = new StreamReader(url);

        if (sr == null) return null;

        string json = sr.ReadToEnd();
        sr.Close();

        if (json.Length <= 0) return null;

        Debug.Log("[ConfigRes] Load Config:" + configPath);
        return LitJson.JsonMapper.ToObject<T>(json);
    }

    /// <summary>
    /// 写入配置文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configPath"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool SaveConfig<T>(string configPath,string data) where T : class
    {
        try
        {
            //编码转换
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            data= reg.Replace(data, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

            //保存
            File.WriteAllText(FilePathUtils.PersistentDataPath() + configPath, data);
            Debug.Log("[ConfigRes] Save Config:" + configPath);
            return true;
        }
        catch (IOException ex)
        {
            Debug.LogError("[ConfigRes] Save Config:" + configPath+" Error!"+ex);
            return false;
        }
    }
}
