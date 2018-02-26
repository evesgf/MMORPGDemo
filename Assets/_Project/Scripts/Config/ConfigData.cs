using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData
{
    /// <summary>
    /// 配置名
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 配置版本
    /// </summary>
    public string version { get; set; }

    /// <summary>
    /// 课程列表
    /// </summary>
    public ClassItem[] classItem { get;set;}
}

/// <summary>
/// 课程组件
/// </summary>
public class ClassItem
{
    /// <summary>
    /// 课程名
    /// </summary>
    public string className { get; set; }
    /// <summary>
    /// 创建者
    /// </summary>
    public string creatorName { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public string creatorTime { get; set; }
    /// <summary>
    /// 适用领域
    /// </summary>
    public string domain { get; set; }
    /// <summary>
    /// 适用班级
    /// </summary>
    public string classLevel { get; set; }
}
